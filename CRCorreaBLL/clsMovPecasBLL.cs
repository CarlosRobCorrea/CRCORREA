using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;

using CRCorreaInfo;
using CRCorreaFuncoes;

namespace CRCorreaBLL
{
    public class clsMovPecasBLL : SQLFactory<clsMovPecasInfo>
    {
        public enum MovTipo
        {
            Entrada,
            Saida
        }

        public clsMovPecasInfo Carregar(Int32 idtipodocumento, Int32 iddocumento, Int32 iddocumentoitem, Int32 iddocumentoitemsub, String cnn)
        {
            Int32 id = 0;

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "select id from movpecas where idtipodocumento = @idtipodocumento and iddocumento = @iddocumento and iddocumentoitem = @iddocumentoitem and iddocumentoitemsub = @iddocumentoitemsub";
            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idtipodocumento", SqlDbType.Int).Value = idtipodocumento;
            sda.SelectCommand.Parameters.Add("@iddocumento", SqlDbType.Int).Value = iddocumento;
            sda.SelectCommand.Parameters.Add("@iddocumentoitem", SqlDbType.Int).Value = iddocumentoitem;
            sda.SelectCommand.Parameters.Add("@iddocumentoitemsub", SqlDbType.Int).Value = iddocumentoitemsub;
            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                id = clsParser.Int32Parse(row["id"].ToString());
            }

            return Carregar(id, cnn);
        }
        /*
        public clsMovPecasInfo Carregar(String controle, String cnn)
        {
            Int32 id;
            

            
            id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from movpecas where controle=convert(bigint, '" + controle + "')"));

            return Carregar(id, cnn);
        }
        */
        public override int Incluir(clsMovPecasInfo info, string cnn)
        {
            Int32 result = -1;

            VerificaInfo(info);

            result = base.Incluir(info, cnn);

            DataTable dt = new DataTable();
            string query = "";
            SqlDataAdapter sda;

            Int32 TipoMovimento = 0;
            if (info.operacao == "E")
            {// coloca 0(zero) ---> AAAAMMDD0XXX = ano+mes+dia+tipomovimento+id
                TipoMovimento = 0;
            }
            else
            {// coloca 1(HUM) ---> AAAAMMDD1XXX = ano+mes+dia+tipomovimento+id
                TipoMovimento = 1;
            }
            query = "SELECT " +
                    "CONVERT(BIGINT, SUBSTRING(CONVERT(NVARCHAR, '" + info.data + "',103),7,4) + " +
                    "SUBSTRING(CONVERT(NVARCHAR, '" + info.data + "',103),4,2) + " +
                    "SUBSTRING(CONVERT(NVARCHAR, '" + info.data + "', 103),1,2) + " +
                    "RIGHT(CONVERT(NVARCHAR, " + TipoMovimento + "),1) + " +
                    "RIGHT(CONVERT(NVARCHAR, (" + result + " * 123)), 3))";

            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                info.id = result;
                //info.controle = clsParser.Int64Parse(sdr[0].ToString());

                Alterar(info, cnn);
            }

            return result;
        }

        public override int Alterar(clsMovPecasInfo info, string cnn)
        {
            Int32 result;

            VerificaInfo(info);

            result = base.Alterar(info, cnn);

            //MovPecasAtualizar(info, cnn);
            //greidi  07/05/2012   gambi
            DateTime dt = new DateTime();
            if (info.data > clsParser.DateTimeParse("01/01/2000"))
            {
                dt = info.data;
            }
            else
            {
                dt = DateTime.Now;
            }

            AtualizaEstoque(info.idcodigo, dt.AddDays(-1), cnn);
            AtualizarCadastroItem(info.idcodigo, cnn, info.data, info.documento, info.operacao, info.valor);
            MovPecasAtualizar(info, cnn);

            return result;
        }

        public int AlterarSemAtualizar(clsMovPecasInfo info, string cnn)
        {
            Int32 result;

            VerificaInfo(info);

            result = base.Alterar(info, cnn);

            MovPecasAtualizar(info, cnn);

            return result;
        }

        public override Boolean Excluir(Int32 id, String cnn)
        {
            Boolean result;
            clsMovPecasInfo info = Carregar(id, cnn);

            result = base.Excluir(id, cnn);

            info.operacao = "X";

            MovPecasAtualizar(info, cnn);
            AtualizaEstoque(info.idcodigo, info.data.AddDays(-1), cnn);
            AtualizarCadastroItem(info.idcodigo, cnn, info.data, info.documento, info.operacao, info.valor);

            return result;
        }

        public Boolean ExcluirDocumentoItem(Int32 idtipodocumento, Int32 iddocumento, Int32 iddocumentoitem, String cnn)
        {
            Boolean result = true;

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "select id from movpecas where idtipodocumento = @idtipodocumento and iddocumento = @iddocumento and iddocumentoitem = @iddocumentoitem";
            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idtipodocumento", SqlDbType.Int).Value = idtipodocumento;
            sda.SelectCommand.Parameters.Add("@iddocumento", SqlDbType.Int).Value = iddocumento;
            sda.SelectCommand.Parameters.Add("@iddocumentoitem", SqlDbType.Int).Value = iddocumentoitem;
            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (result == true)
                {
                    result = Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
                else
                {
                    Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
            }

            return result;
        }
        public Boolean ExcluirDocumentoItem(Int32 idtipodocumento, Int32 iddocumento, Int32 iddocumentoitem, Int32 iddocumentoitemsub, String cnn)
        {
            Boolean result = true;

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "select id from movpecas where idtipodocumento = @idtipodocumento and iddocumento = @iddocumento and iddocumentoitem = @iddocumentoitem and iddocumentoitemsub = @iddocumentoitemsub and verificado = 'S' ";
            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idtipodocumento", SqlDbType.Int).Value = idtipodocumento;
            sda.SelectCommand.Parameters.Add("@iddocumento", SqlDbType.Int).Value = iddocumento;
            sda.SelectCommand.Parameters.Add("@iddocumentoitem", SqlDbType.Int).Value = iddocumentoitem;
            sda.SelectCommand.Parameters.Add("@iddocumentoitemsub", SqlDbType.Int).Value = iddocumentoitemsub;
            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (result == true)
                {
                    result = Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
                else
                {
                    Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
            }

            return result;
        }

        public void VerificaInfo(clsMovPecasInfo _info)
        {

        }
        public void AtualizaEstoqueDesdeInicio(Int32 idcodigo, DateTime data, String cnn)
        {
            
            clsMovPecasInfo movpecasInfo;

            // capturar os valores anteriores a esta data 
            String dataTxt = data.ToString("dd/MM/yyyy");
            // capturar o lançamento anterior a este com a data anterior primeiro
            //Decimal qtdeanterior = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select saldo from movpecas where idcodigo=" + idcodigo + " and data < " + clsParser.SqlDateTimeFormat(dataTxt + " 00:00", true) + "  order by data, operacao, id"));
            Decimal qtdeanterior = 0;
            Decimal saldo = qtdeanterior;
            Decimal valortotal = 0;
            Decimal valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select valoracumulado from movpecas where idcodigo=" + idcodigo + " and data < " + clsParser.SqlDateTimeFormat(dataTxt + " 00:00", true) + "  order by data, operacao, id"));
            Decimal customedio = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select customedio from movpecas where idcodigo=" + idcodigo + "and data < " + clsParser.SqlDateTimeFormat(dataTxt + " 00:00", true) + "  order by data, operacao, id"));

            // capturar o lançamento anterior a este 
            String query;
            SqlDataAdapter sda;
            DataTable dtMov = new DataTable();
            query = "select * from movpecas where idcodigo = @idcodigo and data <= " + clsParser.SqlDateTimeFormat(dataTxt + " 23:59:59", true) + "  order by data, operacao, id";
            dtMov = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcodigo", SqlDbType.Int).Value = idcodigo;
            sda.Fill(dtMov);
            foreach (DataRow row in dtMov.Rows)
            {
                movpecasInfo = Carregar(clsParser.Int32Parse(row["id"].ToString()), cnn);
                if (movpecasInfo.operacao == "E")
                {
                    qtdeanterior += movpecasInfo.qtde;
                    valortotal = movpecasInfo.qtde * movpecasInfo.valor;
                    valoracumulado += valortotal;
                    saldo += movpecasInfo.qtde;
                    customedio = 0;
                    if (saldo > 0)
                    {
                        customedio = Math.Round(valoracumulado / saldo, 4);
                    }
                }
                else
                {
                    qtdeanterior -= movpecasInfo.qtdesaida;
                    //qtdeanterior -= movpecasInfo.qtde;
                    valortotal = movpecasInfo.qtdesaida * customedio;
                    valoracumulado -= valortotal;
                    saldo -= movpecasInfo.qtdesaida;
                    customedio = 0;
                    if (saldo > 0)
                    {
                        customedio = Math.Round(valoracumulado / saldo, 4);
                    }

                }
                movpecasInfo.qtdeanterior = qtdeanterior;
                movpecasInfo.customedio = customedio;
                movpecasInfo.saldo = saldo;
                movpecasInfo.valoracumulado = valoracumulado;
                movpecasInfo.valortotal = valortotal;
                AlterarSemAtualizar(movpecasInfo, cnn);
            }
        }

        public void AtualizaEstoque(Int32 idcodigo, DateTime data, String cnn)
        {
            
            clsMovPecasInfo movpecasInfo;

            // capturar os valores anteriores a esta data 
            String dataTxt = data.ToString("dd/MM/yyyy");
            // capturar o lançamento anterior a este com a data anterior primeiro
            Decimal qtdeanterior = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select saldo from movpecas where idcodigo=" + idcodigo + " and data < " + clsParser.SqlDateTimeFormat(dataTxt + " 00:00", true) + "  order by data desc, id desc"));
            Decimal saldo = qtdeanterior;
            Decimal valortotal = 0;
            Decimal valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select valoracumulado from movpecas where idcodigo=" + idcodigo + " and data < " + clsParser.SqlDateTimeFormat(dataTxt + " 00:00", true) + " order by data desc, id desc"));
            Decimal customedio = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select customedio from movpecas where idcodigo=" + idcodigo + "and data < " + clsParser.SqlDateTimeFormat(dataTxt + " 00:00", true) + " order by data desc, id desc"));

            // capturar o lançamento anterior a este 
            String query;
            SqlDataAdapter sda;
            DataTable dtMov = new DataTable();
            query = "select * from movpecas where idcodigo = @idcodigo and data >= " + clsParser.SqlDateTimeFormat(dataTxt + " 00:00:00", true) + "  order by data, operacao, id";
            dtMov = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcodigo", SqlDbType.Int).Value = idcodigo;
            sda.Fill(dtMov);
            foreach (DataRow row in dtMov.Rows)
            {
                movpecasInfo = Carregar(clsParser.Int32Parse(row["id"].ToString()), cnn);
                if (movpecasInfo.operacao == "E")
                {
                    qtdeanterior += movpecasInfo.qtde;
                    valortotal = movpecasInfo.qtde * movpecasInfo.valor;
                    valoracumulado += valortotal;
                    saldo += movpecasInfo.qtde;
                    customedio = 0;
                    if (saldo > 0)
                    {
                        customedio = Math.Round(valoracumulado / saldo, 4);
                    }
                }
                else
                {
                    qtdeanterior -= movpecasInfo.qtdesaida;
                    //qtdeanterior -= movpecasInfo.qtde;
                    valortotal = movpecasInfo.qtdesaida * customedio;
                    valoracumulado -= valortotal;
                    saldo -= movpecasInfo.qtdesaida;
                    customedio = 0;
                    if (saldo > 0)
                    {
                        customedio = Math.Round(valoracumulado / saldo, 4);
                    }

                }
                movpecasInfo.qtdeanterior = qtdeanterior;
                movpecasInfo.customedio = customedio;
                movpecasInfo.saldo = saldo;
                movpecasInfo.valoracumulado = valoracumulado;
                movpecasInfo.valortotal = valortotal;
                AlterarSemAtualizar(movpecasInfo, cnn);
            }
        }

        public void MovPecasAtualizar(clsMovPecasInfo info, String cnn)
        {
            // Atualiza a tabela MOVPECASMES
            clsMovPecasMesInfo MovPecasMesInfo;
            clsMovPecasMesBLL MovPecasMesBLL = new clsMovPecasMesBLL();

            DataTable dtMov;
            SqlDataAdapter sda;
            SqlConnection scn;
            SqlCommand scd;
            String query;
            Int32 anomes;

            // acumulo a movimentação de estoque deste item - qtde entrada // qtde saida //
            String dataauxde = "01" + "/" + info.data.Month + "/" + info.data.Year;
            String dataauxate = "";

            DateTime dat1 = clsParser.DateTimeParse(dataauxde);
            dataauxde = dat1.Day.ToString().PadLeft(2, '0') + "/" +
                        dat1.Month.ToString().PadLeft(2, '0') + "/" +
                        dat1.Year.ToString().PadLeft(4, '0');

            dat1 = dat1.AddMonths(1);
            dat1 = dat1.AddDays(-1);

            dataauxate = dat1.Day.ToString().PadLeft(2, '0') + "/" +
                         dat1.Month.ToString().PadLeft(2, '0') + "/" +
                         dat1.Year.ToString().PadLeft(4, '0');
            // Verifica se existe movimentação anterior a essa para manter o movpecasmes
            // Se não havia movimentação então excluir os registros do movpecasmes
            query = " select data, qtde,qtdesaida, valoracumulado from movpecas " +
            "WHERE data < " + clsParser.SqlDateTimeFormat(dataauxde + " 00:00:00", true) +
            " AND idcodigo = @idcodigo ";
            dtMov = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcodigo", SqlDbType.Int).Value = info.idcodigo;
            sda.Fill(dtMov);
            if (dtMov.Rows.Count == 0)
            {  // Não existe movimentação anterior  
                // Excluir qualquer movpecasmes com data anterior a esta 
                anomes = clsParser.Int32Parse(dataauxde.Substring(6, 4) + dataauxde.Substring(3, 2));
                scn = new SqlConnection(cnn);
                scn.Open();
                query = "DELETE MOVPECASMES WHERE ANOMES<@ANOMES AND IDCODIGO=@IDCODIGO";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@ANOMES", SqlDbType.Int).Value = anomes;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = info.idcodigo;
                scd.ExecuteNonQuery();
                scn.Close();
            }


            // Acumular para Adicionar no MovPecasMes
            Decimal qtanterior = 0;
            Decimal qtEntrada = 0;
            Decimal qtSaida = 0;
            Decimal qtSaldo = 0;
            Decimal ValorAcum = 0;
            Decimal qtSaida90dias = 0;

            // Saldo Anterior
            /*            query = " select data, qtde,qtdesaida, valoracumulado from movpecas " +
                        "WHERE data >= " + clsParser.SqlDateTimeFormat(dataauxde + " 00:00:00", true)
                        + " AND " +
                        " data <= " + clsParser.SqlDateTimeFormat(dataauxate + " 23:59:59", true) +
                        " AND idcodigo = @idcodigo "; */


            qtanterior = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select saldo from movpecas " +
                         "where idcodigo=" + info.idcodigo + " and data < " + clsParser.SqlDateTimeFormat(dataauxde + " 00:00", true) + "  order by data desc, id desc"));
            qtEntrada = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select sum(qtde) from movpecas " +
                         "where idcodigo=" + info.idcodigo +
                         " and data >= " + clsParser.SqlDateTimeFormat(dataauxde + " 00:00:00", true) +
                         " and data <= " + clsParser.SqlDateTimeFormat(dataauxate + " 23:59:59", true)));
            qtSaida = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select sum(qtdesaida) from movpecas " +
                         "where idcodigo=" + info.idcodigo +
                         " and data >= " + clsParser.SqlDateTimeFormat(dataauxde + " 00:00:00", true) +
                         " and data <= " + clsParser.SqlDateTimeFormat(dataauxate + " 23:59:59", true)));
            qtSaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select saldo from movpecas " +
                         "where idcodigo=" + info.idcodigo +
                         " and data <= " + clsParser.SqlDateTimeFormat(dataauxate + " 23:59:59", true) +
                         "  order by data desc, id desc"));
            ValorAcum = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select valoracumulado from movpecas " +
                         "where idcodigo=" + info.idcodigo +
                         " and data <= " + clsParser.SqlDateTimeFormat(dataauxate + " 23:59:59", true) +
                         "  order by data desc, id desc"));

            MovPecasMesInfo = MovPecasMesBLL.Carregar(info.idcodigo, info.data.ToString("yyyyMM"), cnn);

            if (MovPecasMesInfo == null || MovPecasMesInfo.id == 0)
            {
                MovPecasMesInfo = new clsMovPecasMesInfo();

                MovPecasMesInfo.anomes = clsParser.Int32Parse(info.data.ToString("yyyyMM"));
                MovPecasMesInfo.idcodigo = info.idcodigo;

                dataauxde = "01" + "/" + info.data.Month + "/" + info.data.Year;
                //MovPecasMesInfo.valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select valoracumulado from movpecas where year(data) <= " + info.data.Year + " and month(data) < " + info.data.Month + " and idcodigo = " + info.idcodigo + "  order by data desc"));
                //MovPecasMesInfo.qtdesaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select top 1 saldo from movpecas where year(data) <= " + info.data.Year + " and month(data) < " + info.data.Month + " and idcodigo = 18 order by data desc"));
                //if (MovPecasMesInfo.qtdesaldo == 0)
                // {
                //     MovPecasMesInfo.valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select valoracumulado from movpecas where year(data) < " + info.data.Year + " and idcodigo = " + info.idcodigo + " order by data desc"));
                //     MovPecasMesInfo.qtdesaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select top 1 saldo from movpecas where year(data) < " + info.data.Year + " and idcodigo = 18 order by data desc"));
                //}

                MovPecasMesInfo.qtdeinicio = qtanterior;
                MovPecasMesInfo.qtdeentra = qtEntrada; // info.qtde;
                MovPecasMesInfo.qtdesaida = qtSaida;
                MovPecasMesInfo.qtdesaldo = qtSaldo;  //((MovPecasMesInfo.qtdeinicio + MovPecasMesInfo.qtdeentra) - MovPecasMesInfo.qtdesaida);
                MovPecasMesInfo.valoracumulado = ValorAcum;

                if (MovPecasMesInfo.qtdesaldo != 0)
                {
                    MovPecasMesInfo.customediomes = MovPecasMesInfo.valoracumulado / MovPecasMesInfo.qtdesaldo;
                }
                else
                {
                    MovPecasMesInfo.customediomes = 0;
                }
                MovPecasMesBLL.Incluir(MovPecasMesInfo, cnn);
            }
            else
            {
                dataauxde = "01" + "/" + info.data.Month + "/" + info.data.Year;

                dat1 = clsParser.DateTimeParse(dataauxde);
                dat1 = dat1.AddMonths(1);
                dat1 = dat1.AddDays(-1);

                dataauxde = dat1.Day.ToString().PadLeft(2, '0') + "/" +
                            dat1.Month.ToString().PadLeft(2, '0') + "/" +
                            dat1.Year.ToString().PadLeft(4, '0');


                //MovPecasMesInfo.valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select valoracumulado from movpecas where data <= " + clsParser.SqlDateTimeFormat(dataauxde + " 23:59:59", true) + " and idcodigo = " + info.idcodigo + " order by data desc"));
                //MovPecasMesInfo.qtdesaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select top 1 saldo from movpecas where data <= " + clsParser.SqlDateTimeFormat(dataauxde + " 23:59:59", true) + " and idcodigo = " + info.idcodigo + " order by data desc"));
                //if (MovPecasMesInfo.qtdesaldo == 0)
                //{
                //    MovPecasMesInfo.valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select valoracumulado from movpecas where year(data) < " + dat1.Year.ToString() + " and idcodigo = " + info.idcodigo + " order by data desc"));
                //    MovPecasMesInfo.qtdesaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select top 1 saldo from movpecas where year(data) < " + dat1.Year.ToString() + " and idcodigo = " + info.idcodigo + " order by data desc"));
                //}


                MovPecasMesInfo.qtdeinicio = qtanterior;
                MovPecasMesInfo.qtdeentra = qtEntrada; // info.qtde;
                MovPecasMesInfo.qtdesaida = qtSaida;
                MovPecasMesInfo.qtdesaldo = qtSaldo;   //((MovPecasMesInfo.qtdeinicio + MovPecasMesInfo.qtdeentra) - MovPecasMesInfo.qtdesaida);
                //MovPecasMesInfo.qtdesaldo = (MovPecasMesInfo.qtdeentra - MovPecasMesInfo.qtdesaida);
                MovPecasMesInfo.valoracumulado = ValorAcum;
                if (MovPecasMesInfo.qtdesaldo != 0)
                {
                    MovPecasMesInfo.customediomes = MovPecasMesInfo.valoracumulado / MovPecasMesInfo.qtdesaldo;
                }
                else
                {
                    MovPecasMesInfo.customediomes = 0;
                }
                MovPecasMesBLL.Alterar(MovPecasMesInfo, cnn);
            }
        }

        public DataTable GridDados(Int32 idcodigo, DateTime dataini, DateTime datafim, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "IDCODIGO, " +
                        "DATA, " +
                        "DOCUMENTO, " +
                        "NUMERO, " +
                        "COGNOME, " +
                        "OPERACAO, " +
//                        "COMPLEMENTOCOG, " +
                        "QTDE, " +
	                    "QTDESAIDA, " +
                        "SALDO, " +
                        "VALOR, " +
                        "VALORTOTAL, " +
                        "VALORACUMULADO, " +
                        "CUSTOMEDIO " +
                        //"CONTROLE " +
                    "FROM " +
                        "MOVPECAS " +
                    "WHERE " +
                        "IDCODIGO = @IDCODIGO AND " +
                        "CONVERT(DATETIME,DATA,102) >= CONVERT(DATETIME,@DATAINI,102) AND " +
                        "CONVERT(DATETIME,DATA,102) <= CONVERT(DATETIME,@DATAFIM,102) " +
                    "ORDER BY " +
                        "DATA, OPERACAO, ID ";

                        /*"DATA, ID "; nÃO DEU CERTO O ID DA ENTRADA FICOU DEPOIS- ENTÃO OPERACAO TEM QUE SER PRIMEIRO; */

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcodigo", SqlDbType.Int).Value = idcodigo;
            sda.SelectCommand.Parameters.Add("@dataini", SqlDbType.DateTime).Value = dataini;
            sda.SelectCommand.Parameters.Add("@datafim", SqlDbType.DateTime).Value = datafim;
            
            sda.Fill(dt);

            return dt;
        }

        public static GridColuna[] gridDadosColunas = new GridColuna[]
        {
            new GridColuna("Data", "DATA", 94, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc.", "DOCUMENTO", 37, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc. Nº", "NUMERO", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Histórico", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tp", "OPERACAO", 10, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("PO/Linha", "COMPLEMENTOCOG", 75, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qt Entra", "QTDE", 85, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Saida", "QTDESAIDA", 85, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Saldo", "SALDO", 85, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vl Unit", "VALOR", 85, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Mov", "VALORTOTAL", 85, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vl Acumulado", "VALORACUMULADO", 105, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Custo Médio", "CUSTOMEDIO", 85, true, DataGridViewContentAlignment.MiddleRight)
            //new GridColuna("Controle", "CONTROLE", 1, false, DataGridViewContentAlignment.MiddleLeft)
        };

        public void gridDadosMonta(DataGridView dgv, DataTable dtDados)
        {
            

            dgv.DataSource = dtDados;

            clsGridHelper.MontaGrid2(dgv, gridDadosColunas, true);

            dgv.Columns["data"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        public Decimal QtdeTransitoFabricando(Int32 idcodigo)
        {
            return 0;
        }

        public Decimal QtdeFabricaVendido(Int32 idcodigo)
        {
            return 0;
        }

        public Decimal QtdeExpedicao(Int32 idcodigo)
        {
            return 0;
        }

        public Decimal SaldoPrateleira(Int32 idcodigo)
        {
            return 0;
        }

        

        public void MovimentaItem(Int32 idcodigo,
                                  String movTipo,
                                  Decimal qtde,
                                  DateTime data,
                                  Int32 idtipodocumento,
                                  Int32 iddocumento,
                                  Int32 iddocumentoitem,
                                  Int32 iddocumentoitemsub,
                                  Decimal precounitario,
                                  String usuario,
                                  String cnn)
        {

            // Quando Inclui
            clsMovPecasInfo movpecasInfo = new clsMovPecasInfo();
            Object result;
            Int32 idcliente;
            String tipomov = movTipo;
            // Verificar se já existe este documento
            movpecasInfo.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from movpecas where idtipodocumento = " + idtipodocumento + " and iddocumento = " + iddocumento + " and iddocumentoitem =" + iddocumentoitem + " and iddocumentoitemsub = " + iddocumentoitemsub + ""));
            // Pegar o Tipo do Documento
            movpecasInfo.documento = Procedure.PesquisaoPrimeiro(cnn, "select cognome from docfiscal where id=" + idtipodocumento, "");
            if (movpecasInfo.documento.Length > 3)
            {
                movpecasInfo.documento = movpecasInfo.documento.Substring(0, 3);
            }

            if (tipomov == "S")
            {
                tipomov = "1";
            }
            else
            {
                tipomov = "0";
            }
            movpecasInfo.idcodigo = idcodigo;
            movpecasInfo.iddocumento = iddocumento;
            movpecasInfo.iddocumentoitem = iddocumentoitem;
            movpecasInfo.iddocumentoitemsub = iddocumentoitemsub;
            movpecasInfo.idtipodocumento = idtipodocumento;
            movpecasInfo.data = clsParser.DateTimeParse(data.ToString("dd/MM/yyyy"));
            movpecasInfo.hora = data;
            movpecasInfo.dataretorno = data;
            movpecasInfo.customedio = 0;
            movpecasInfo.motivo = "";
            movpecasInfo.operacao = movTipo;
            movpecasInfo.qtdeanterior = 0;
            movpecasInfo.saldo = 0;
            movpecasInfo.setor = "";
            movpecasInfo.usuario = usuario;
            if (movpecasInfo.operacao == "E")
            {
                movpecasInfo.qtde = qtde;
                movpecasInfo.qtdesaida = 0;
            }
            else
            {
                movpecasInfo.qtde = 0;
                movpecasInfo.qtdesaida = qtde;
            }
            if (movpecasInfo.operacao == "E")
            { // mantem o preço adicionado
                movpecasInfo.valor = precounitario;
                movpecasInfo.valortotal = qtde * precounitario;
            }
            else
            { // se for saida pega o ultimo preço médio anterior a este
                result = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select customedio from movpecas where idcodigo=" + movpecasInfo.idcodigo + " and verificado='S' order by data desc, id desc"));
                if (result != null)
                {
                    movpecasInfo.valor = clsParser.DecimalParse(result.ToString());
                    movpecasInfo.valortotal = qtde * movpecasInfo.valor;
                }
            }
            result = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select top(1) qtde from movpecas where idcodigo=" + movpecasInfo.idcodigo + " and verificado='S' order by data desc, id desc"));
            if (result != null)
            {
                movpecasInfo.qtdeanterior = clsParser.DecimalParse(result.ToString());
            }
            result = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select saldo from movpecas where idcodigo=" + movpecasInfo.idcodigo + " and verificado='S' order by data desc, id desc"));
            if (result != null)
            {
                movpecasInfo.saldo = clsParser.DecimalParse(result.ToString());
            }
            if (movpecasInfo.operacao == "E")
            {
                movpecasInfo.saldo += qtde;
            }
            else
            {
                movpecasInfo.saldo -= qtde;
            }

            result = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select valoracumulado from movpecas where idcodigo=" + movpecasInfo.idcodigo + " and verificado='S' order by data desc, id desc"));
            if (result != null)
            {
                if (movpecasInfo.operacao == "E")
                {
                    movpecasInfo.valoracumulado = clsParser.DecimalParse(result.ToString()) + movpecasInfo.valortotal;
                }
                else
                {
                    movpecasInfo.valoracumulado = clsParser.DecimalParse(result.ToString()) - movpecasInfo.valortotal;
                }
            }
            else
            {
                if (movpecasInfo.operacao == "E")
                {
                    movpecasInfo.valoracumulado = movpecasInfo.valortotal;
                }
                else
                {
                    movpecasInfo.valoracumulado = -1 * movpecasInfo.valortotal;
                }
            }
            if (movpecasInfo.saldo != 0 && movpecasInfo.valoracumulado != 0)
            {
                movpecasInfo.customedio = movpecasInfo.valoracumulado / movpecasInfo.saldo;
            }
            else
            {
                movpecasInfo.customedio = movpecasInfo.valor;
            }


            if (movpecasInfo.documento.IndexOf("PCO") != -1)
            {
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idfornecedor from compras where id=" + iddocumento));
                movpecasInfo.idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcondpagto from compras where id=" + iddocumento));
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from compras where id=" + iddocumento));
            }
            else if (movpecasInfo.documento.IndexOf("NFE") != -1)
            {
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idfornecedor from nfcompra where id=" + iddocumento));
                movpecasInfo.idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcondpagto from nfcompra where id=" + iddocumento));
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from nfcompra where id=" + iddocumento));
            }
            else if (movpecasInfo.documento.IndexOf("NFV") != -1)
            {
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from nfvenda where id=" + iddocumento));
                movpecasInfo.idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcondpagto from nfvenda where id=" + iddocumento));
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from nfvenda where id=" + iddocumento));
            }
            else if (movpecasInfo.documento.IndexOf("PED") != -1)
            {
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from pedido where id=" + iddocumento));
                movpecasInfo.idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcondpagto from pedido where id=" + iddocumento));
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from pedido where id=" + iddocumento));
            }

            else if (movpecasInfo.documento.IndexOf("CTO") != -1)
            {
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from contrato where id=" + iddocumento));
                movpecasInfo.idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcondpagto from contrato where id=" + iddocumento));
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from contrato where id=" + iddocumento));
            }
            else if (movpecasInfo.documento.IndexOf("OS") != -1)
            {
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from ordemservico where id=" + iddocumento));
                movpecasInfo.idcondpagto = clsInfo.zcondpagto;
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from ordemservico where id=" + iddocumento));
            }
            else if (movpecasInfo.documento.IndexOf("OSF") != -1)
            {
                idcliente =  clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from ordemservico where id=" + iddocumento));
                movpecasInfo.idcondpagto = clsInfo.zcondpagto;
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from ordemservico where id=" + iddocumento));
            }
            else if (movpecasInfo.documento.IndexOf("RM") != -1)
            {
                idcliente = clsInfo.zempresaclienteid;
                movpecasInfo.idcondpagto = clsInfo.zcondpagto;
                movpecasInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from  REQUISICAO where id=" + iddocumento));
            }
            else
            {
                idcliente = clsInfo.zempresaclienteid;
                movpecasInfo.idcondpagto = clsInfo.zcondpagto;
            }
            movpecasInfo.cognome = Procedure.PesquisaoPrimeiro(cnn, "select cognome from cliente where id=" + idcliente);
            movpecasInfo.verificado = "S";
            if (movpecasInfo.id == 0)
            {
                Incluir(movpecasInfo, cnn);
            }
            else
            {
                Alterar(movpecasInfo, cnn);
            }
        }
        public void MovimentaItem(Decimal qtde,
                                  String movTipo,
                                  Int32 idtipodocumento,
                                  Int32 iddocumento,
                                  Int32 iddocumentoitem,
                                  Int32 iddocumentoitemsub,
                                  Decimal precounitario,
                                  Int32 idcodigo,
                                  DateTime data,
                                  String cnn)
        {
            // QUANDO ESTA ALTERANDO
            clsMovPecasInfo info = Carregar(idtipodocumento, iddocumento, iddocumentoitem, iddocumentoitemsub, cnn);

            Object result;
            info.data = data;

            if (movTipo == "E")
            {
                info.operacao = "E";
                info.qtde = qtde;
                info.qtdesaida = 0;
                info.valor = precounitario;

            }
            else if (movTipo == "S")
            {
                info.operacao = "S";
                info.qtde = 0;
                info.qtdesaida = qtde;
            }
            if (info == null || info.id == 0)
            {
                return;
            }
            info.valortotal = qtde * precounitario;
            if (info.operacao == "E")
            {
                info.saldo += qtde;
                info.valoracumulado = info.valoracumulado + info.valortotal;
            }
            else
            {
                info.saldo -= qtde;
                info.valoracumulado = info.valoracumulado - info.valortotal;
            }
            result = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select top(1) qtde from movpecas where idcodigo=" + info.idcodigo + " and verificado='S' order by data desc, operacao desc, id desc"));
            if (result != null)
            {
                info.qtdeanterior = clsParser.DecimalParse(result.ToString());
            }

            if (info.saldo != 0 && info.valoracumulado != 0)
            {
                info.customedio = info.valoracumulado / info.saldo;
            }
            info.idcodigo = idcodigo;
            info.verificado = "S";
            Alterar(info, cnn);
        }

        public void MovimentaItem_Old(Decimal qtde,
                                  String movTipo,
                                  Int32 idtipodocumento,
                                  Int32 iddocumento,
                                  Int32 iddocumentoitem,
                                  Int32 iddocumentoitemsub,
                                  Decimal precounitario,
                                  Int32 idcodigo,
                                  DateTime data,
                                  String cnn)
        {
            clsMovPecasInfo info = Carregar(idtipodocumento, iddocumento, iddocumentoitem, iddocumentoitemsub, cnn);
            info.data = data;
            if (movTipo == "E")
            {
                info.operacao = "E";
                info.qtde = qtde;
                info.qtdesaida = 0;
                info.valor = precounitario;

            }
            else if (movTipo == "S")
            {
                info.operacao = "S";
                info.qtde = 0;
                info.qtdesaida = qtde;
            }
            if (info == null || info.id == 0)
            {
                return;
            }
            info.valortotal = qtde * precounitario;
            if (info.operacao == "E")
            {
                info.saldo += qtde;
                info.valoracumulado = info.valoracumulado + info.valortotal;
            }
            else
            {
                info.saldo -= qtde;
                info.valoracumulado = info.valoracumulado - info.valortotal;
            }
            if (info.saldo != 0 && info.valoracumulado != 0)
            {
                info.customedio = info.valoracumulado / info.saldo;
            }
            info.idcodigo = idcodigo;
            info.verificado = "S";
            Alterar(info, cnn);
        }
        public void AtualizarCadastroItem(Int32 idcodigo, String cnn, DateTime dtdocumento, string documento, string operacao, Decimal valor)
        {
            
            Decimal QtdeInicio = 0;
            Decimal QtdeEntra = 0;
            Decimal QtdeSaida = 0;
            Decimal QtdeSaida90dias = 0; // acumular ultimas saidas dos 90 dias
            Decimal QtdeSaldo = 0;

            // Pegar a Data Atual
            String dataInicio = "01/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year + "";
            QtdeInicio = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select saldo from movpecas " +
                         "where idcodigo=" + idcodigo + " and data < " + clsParser.SqlDateTimeFormat(dataInicio + " 00:00", true) + "  order by data desc, id desc"));
            QtdeEntra = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select sum(qtde) from movpecas " +
                         "where idcodigo=" + idcodigo + " and data >= " + clsParser.SqlDateTimeFormat(dataInicio + " 00:00", true) + "  "));

            QtdeSaida = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select sum(qtdesaida) from movpecas " +
                         "where idcodigo=" + idcodigo + " and data >= " + clsParser.SqlDateTimeFormat(dataInicio + " 00:00", true) + "  "));

            QtdeSaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select saldo from movpecas " +
                         "where idcodigo=" + idcodigo + " and data > " + clsParser.SqlDateTimeFormat(dataInicio + " 00:00", true) + "  order by data desc, id desc"));

            QtdeSaida90dias = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn,
                         "select sum(qtdesaida) from movpecas " +
                         "where idcodigo=" + idcodigo +
                         " and data >= " + clsParser.SqlDateTimeFormat(dtdocumento.AddDays(-90), true) +
                         " and data <= " + clsParser.SqlDateTimeFormat(dtdocumento.ToString("dd/MM/yyyy") + " 23:59:59", true)));

            Decimal EstoqueMin = 0;
            if (QtdeSaida90dias > 0)
            {
                EstoqueMin = Math.Round(((QtdeSaida90dias / 90) * 30), 0);
            }

            // Gravar no Cadastro da Peça
            String query;
            SqlConnection scn;
            SqlCommand scd;
            scn = new SqlConnection(cnn);
            query = "update pecas set qtdeinicio=@qtdeinicio, qtdeentra=@qtdeentra, qtdesaida=@qtdesaida, qtdesaldo=@qtdesaldo, estoquemin=@estoquemin where id=@idcodigo ";
            scd = new SqlCommand(query, scn);
            scd.Parameters.Add("@idcodigo", SqlDbType.Int).Value = idcodigo;
            scd.Parameters.Add("@qtdeinicio", SqlDbType.Decimal).Value = QtdeInicio;
            scd.Parameters.Add("@qtdeentra", SqlDbType.Decimal).Value = QtdeEntra;
            scd.Parameters.Add("@qtdesaida", SqlDbType.Decimal).Value = QtdeSaida;
            scd.Parameters.Add("@qtdesaldo", SqlDbType.Decimal).Value = QtdeSaldo;
            scd.Parameters.Add("@estoquemin", SqlDbType.Decimal).Value = EstoqueMin;
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

            // Se for RM e operação E=Entrada - verificar se tem preço de compra no cadastro 
            if (documento == "RM" && operacao == "E")
            {
                // verificar se no pecas tem valor na ultima compra
                Decimal precocompra = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select precocompra from pecas where id=" + idcodigo + " "));
                if (precocompra == 0)
                {
                    // Gravar o preco 
                    query = "update pecas set precocompra=@precocompra where id=@idcodigo ";
                    scd = new SqlCommand(query, scn);
                    scd.Parameters.Add("@idcodigo", SqlDbType.Int).Value = idcodigo;
                    scd.Parameters.Add("@precocompra", SqlDbType.Decimal).Value = valor;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();
                }

            }


        }

    }
}
