using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaInfo;


namespace CRCorreaBLL
{ 
    public class clsCondpagtoBLL : SQLFactory<clsCondpagtoInfo>
    {
        public static DataTable GridDados(String cnn, String filtro)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;

            query = "SELECT CONDPAGTO.ID, CONDPAGTO.CODIGO, CONDPAGTO.NOME, CONDPAGTO.ATIVO " +
                    "FROM CONDPAGTO ";

            if (!String.IsNullOrEmpty(filtro))
            {
                query += "where ATIVO = '" + filtro + "'";
            }

            query +=  " ORDER BY CONDPAGTO.CODIGO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo", "CODIGO", 220, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 670, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Ativo", "ATIVO", 50, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }


        /*
        public clsCondpagtoInfo Carregar(String conexao, Int32 id)
        {
            clsCondpagtoDAL clsCondpagtoDAL = new clsCondpagtoDAL();
            return clsCondpagtoDAL.Carregar(conexao, id);
        }

        public Int32 Incluir(String conexao, clsCondpagtoInfo info)
        {
            clsCondpagtoDAL clsCondpagtoDAL = new clsCondpagtoDAL();
            return clsCondpagtoDAL.Incluir(conexao, info);
        }

        public Int32 Alterar(String conexao, clsCondpagtoInfo info)
        {
            clsCondpagtoDAL clsCondpagtoDAL = new clsCondpagtoDAL();
            return clsCondpagtoDAL.Alterar(conexao, info);
        }

        public void Excluir(String conexao, Int32 id)
        {
            clsCondpagtoDAL clsCondpagtoDAL = new clsCondpagtoDAL();
            clsCondpagtoDAL.Excluir(conexao, id);
        }

        public DataTable GerarParcelas(DateTime dataemissao, clsCondpagtoInfo info, Decimal totalnota, Decimal totalipi, Decimal totalst, String conexao)
        {
            DateTime datavencimento = dataemissao;

            DataTable dt = new DataTable();

            DataColumn dcPosicao = new DataColumn("POSICAO", Type.GetType("System.Int32"));
            DataColumn dcPosicaoFim = new DataColumn("POSICAOFIM", Type.GetType("System.Int32"));
            DataColumn dcData = new DataColumn("DATA", Type.GetType("System.DateTime"));
            DataColumn dcValor = new DataColumn("VALOR", Type.GetType("System.Decimal"));
            DataColumn dcValorComissao = new DataColumn("VALORCOMISSAO", Type.GetType("System.Decimal"));
            DataColumn dcValorComissaoSupervisor = new DataColumn("VALORCOMISSAOSUPERVISOR", Type.GetType("System.Decimal"));
            DataColumn dcValorComissaoCoordenador = new DataColumn("VALORCOMISSAOCOORDENADOR", Type.GetType("System.Decimal"));

            dt.Columns.Add(dcPosicao);
            dt.Columns.Add(dcPosicaoFim);
            dt.Columns.Add(dcData);
            dt.Columns.Add(dcValor);
            dt.Columns.Add(dcValorComissao);
            dt.Columns.Add(dcValorComissaoSupervisor);
            dt.Columns.Add(dcValorComissaoCoordenador);

            totalnota = clsVisual.Truncar(totalnota, 2);
            totalipi = clsVisual.Truncar(totalipi, 2);
            totalst = clsVisual.Truncar(totalst, 2);

            Boolean Parcela1 = false;
            if (info.parcelas > 1)
            {
                if (info.st == "S" || info.ipi == "S")
                {
                    if (totalipi > 0 || totalst > 0)
                    {
                        Parcela1 = true;
                    }
                }
            }

            DataRow row;

            Decimal valorparcela = 0;

            valorparcela = totalnota;

            if (Parcela1 == true)
            {
                if (info.st == "S" && totalst > 0)
                {
                    valorparcela = valorparcela - totalst;
                }

                if (info.ipi == "S" && totalipi > 0)
                {
                    valorparcela = valorparcela - totalipi;
                }

                info.parcelas = info.parcelas - 1;
            }

            valorparcela = clsVisual.Truncar(valorparcela / info.parcelas, 2);

            for (int x = 0; x < info.parcelas; x++)
            {
                row = dt.NewRow();

                row["posicao"] = x + 1;
                row["posicaofim"] = info.parcelas;
                row["valor"] = 0;

                if (Parcela1 == true)
                {
                    if (info.st == "S" && totalst > 0)
                    {
                        row["valor"] = totalst;
                        totalnota = totalnota - totalst;
                    }

                    if (info.ipi == "S" && totalipi > 0)
                    {
                        row["valor"] = Decimal.Parse(row["valor"].ToString()) + totalipi;
                        totalnota = totalnota - totalipi;
                    }

                    if (info.cai1 > 0)
                    {
                        if (dataemissao.Day > info.cai1)
                        {
                            datavencimento = DateTime.Parse(info.cai1 + "/" + dataemissao.AddMonths(1).Month + "/" + dataemissao.AddMonths(1).Year);
                        }
                        else
                        {
                            datavencimento = DateTime.Parse(info.cai1 + "/" + dataemissao.Month + "/" + dataemissao.Year);
                        }
                    }
                    else if (info.semana.ToUpper() == "DIU") // Dia úteis
                    {
                        if (dataemissao.DayOfWeek == DayOfWeek.Saturday) { datavencimento = dataemissao.AddDays(2); }
                        else if (dataemissao.DayOfWeek == DayOfWeek.Sunday) { datavencimento = dataemissao.AddDays(1); }
                    }
                    else if (info.semana.ToUpper() == "SEG")
                    {
                        if (dataemissao.DayOfWeek != DayOfWeek.Monday)
                        {
                            if (dataemissao.DayOfWeek == DayOfWeek.Tuesday) { datavencimento = dataemissao.AddDays(6); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Wednesday) { datavencimento = dataemissao.AddDays(5); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Thursday) { datavencimento = dataemissao.AddDays(4); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Friday) { datavencimento = dataemissao.AddDays(3); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Saturday) { datavencimento = dataemissao.AddDays(2); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Sunday) { datavencimento = dataemissao.AddDays(1); }
                        }
                    }
                    else if (info.semana.ToUpper() == "TER")
                    {
                        if (dataemissao.DayOfWeek != DayOfWeek.Tuesday)
                        {
                            if (dataemissao.DayOfWeek == DayOfWeek.Wednesday) { datavencimento = dataemissao.AddDays(6); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Thursday) { datavencimento = dataemissao.AddDays(5); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Friday) { datavencimento = dataemissao.AddDays(4); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Saturday) { datavencimento = dataemissao.AddDays(3); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Sunday) { datavencimento = dataemissao.AddDays(2); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Monday) { datavencimento = dataemissao.AddDays(1); }
                        }
                    }
                    else if (info.semana.ToUpper() == "QUA")
                    {
                        if (dataemissao.DayOfWeek != DayOfWeek.Wednesday)
                        {
                            if (dataemissao.DayOfWeek == DayOfWeek.Thursday) { datavencimento = dataemissao.AddDays(6); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Friday) { datavencimento = dataemissao.AddDays(5); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Saturday) { datavencimento = dataemissao.AddDays(4); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Sunday) { datavencimento = dataemissao.AddDays(3); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Monday) { datavencimento = dataemissao.AddDays(2); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Tuesday) { datavencimento = dataemissao.AddDays(1); }
                        }
                    }
                    else if (info.semana.ToUpper() == "QUI")
                    {
                        if (dataemissao.DayOfWeek != DayOfWeek.Thursday)
                        {
                            if (dataemissao.DayOfWeek == DayOfWeek.Friday) { datavencimento = dataemissao.AddDays(6); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Saturday) { datavencimento = dataemissao.AddDays(5); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Sunday) { datavencimento = dataemissao.AddDays(4); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Monday) { datavencimento = dataemissao.AddDays(3); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Tuesday) { datavencimento = dataemissao.AddDays(2); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Wednesday) { datavencimento = dataemissao.AddDays(1); }
                        }
                    }
                    else if (info.semana.ToUpper() == "SEX")
                    {
                        if (dataemissao.DayOfWeek != DayOfWeek.Friday)
                        {
                            if (dataemissao.DayOfWeek == DayOfWeek.Saturday) { datavencimento = dataemissao.AddDays(6); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Sunday) { datavencimento = dataemissao.AddDays(5); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Monday) { datavencimento = dataemissao.AddDays(4); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Tuesday) { datavencimento = dataemissao.AddDays(3); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Wednesday) { datavencimento = dataemissao.AddDays(2); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Thursday) { datavencimento = dataemissao.AddDays(1); }
                        }
                    }
                    else if (info.semana.ToUpper() == "SAB")
                    {
                        if (dataemissao.DayOfWeek != DayOfWeek.Saturday)
                        {
                            if (dataemissao.DayOfWeek == DayOfWeek.Sunday) { datavencimento = dataemissao.AddDays(6); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Monday) { datavencimento = dataemissao.AddDays(5); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Tuesday) { datavencimento = dataemissao.AddDays(4); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Wednesday) { datavencimento = dataemissao.AddDays(3); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Thursday) { datavencimento = dataemissao.AddDays(2); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Friday) { datavencimento = dataemissao.AddDays(1); }
                        }
                    }
                    else if (info.semana.ToUpper() == "DOM")
                    {
                        if (dataemissao.DayOfWeek != DayOfWeek.Sunday)
                        {
                            if (dataemissao.DayOfWeek == DayOfWeek.Monday) { datavencimento = dataemissao.AddDays(6); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Tuesday) { datavencimento = dataemissao.AddDays(5); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Wednesday) { datavencimento = dataemissao.AddDays(4); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Thursday) { datavencimento = dataemissao.AddDays(3); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Friday) { datavencimento = dataemissao.AddDays(2); }
                            else if (dataemissao.DayOfWeek == DayOfWeek.Saturday) { datavencimento = dataemissao.AddDays(1); }
                        }
                    }
                    else
                    {
                        datavencimento = dataemissao.AddDays(info.dia1);
                    }

                    row["data"] = datavencimento;
                }
                else
                {
                    row["valor"] = valorparcela;

                    if (info.cai1 > 0 && 
                        ((Parcela1 == true && x == 1) ||
                         (Parcela1 == true && x == 5) ||
                         (Parcela1 == true && x == 9) ||
                         (Parcela1 == false && x == 0) ||
                         (Parcela1 == false && x == 4) ||
                         (Parcela1 == false && x == 8)))
                    {
                        dataemissao = DateTime.Parse(info.cai1 + "/" + dataemissao.Month + "/" + dataemissao.Year);
                    }
                    else if (info.cai2 > 0 &&
                        ((Parcela1 == true && x == 2) ||
                         (Parcela1 == true && x == 6) ||
                         (Parcela1 == true && x == 10) ||
                         (Parcela1 == false && x == 1) ||
                         (Parcela1 == false && x == 5) ||
                         (Parcela1 == false && x == 9)))
                    {
                        dataemissao = DateTime.Parse(info.cai2 + "/" + dataemissao.Month + "/" + dataemissao.Year);
                    }
                    else if (info.cai3 > 0 &&
                        ((Parcela1 == true && x == 3) ||
                         (Parcela1 == true && x == 7) ||
                         (Parcela1 == true && x == 11) ||
                         (Parcela1 == false && x == 2) ||
                         (Parcela1 == false && x == 6) ||
                         (Parcela1 == false && x == 10)))
                    {
                        dataemissao = DateTime.Parse(info.cai3 + "/" + dataemissao.Month + "/" + dataemissao.Year);
                    }
                    else if (info.cai4 > 0 &&
                        ((Parcela1 == true && x == 4) ||
                         (Parcela1 == true && x == 8) ||
                         (Parcela1 == true && x == 12) ||
                         (Parcela1 == false && x == 3) ||
                         (Parcela1 == false && x == 5) ||
                         (Parcela1 == false && x == 11)))
                    {
                        dataemissao = DateTime.Parse(info.cai4 + "/" + dataemissao.Month + "/" + dataemissao.Year);
                    }

                    row["data"] = dataemissao;
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        public Boolean ComparaInfo(clsCondpagtoInfo info, clsCondpagtoInfo info2)
        {
            if (info.id != info2.id)
            {
                return false;
            }
            if (info.codigo != info2.codigo)
            {
                return false;
            }
            if (info.nome != info2.nome)
            {
                return false;
            }
            if (info.ipi != info2.ipi)
            {
                return false;
            }
            if (info.despfinanc != info2.despfinanc)
            {
                return false;
            }
            if (info.codigodespfinanc != info2.codigodespfinanc)
            {
                return false;
            }
            if (info.parcelas != info2.parcelas)
            {
                return false;
            }
            if (info.qtdedias != info2.qtdedias)
            {
                return false;
            }
            if (info.totaldias != info2.totaldias)
            {
                return false;
            }
            if (info.dia1 != info2.dia1)
            {
                return false;
            }
            if (info.dia2 != info2.dia2)
            {
                return false;
            }
            if (info.dia3 != info2.dia3)
            {
                return false;
            }
            if (info.dia4 != info2.dia4)
            {
                return false;
            }
            if (info.dia5 != info2.dia5)
            {
                return false;
            }
            if (info.dia6 != info2.dia6)
            {
                return false;
            }
            if (info.dia7 != info2.dia7)
            {
                return false;
            }
            if (info.dia8 != info2.dia8)
            {
                return false;
            }
            if (info.dia9 != info2.dia9)
            {
                return false;
            }
            if (info.dia10 != info2.dia10)
            {
                return false;
            }
            if (info.por1 != info2.por1)
            {
                return false;
            }
            if (info.por2 != info2.por2)
            {
                return false;
            }
            if (info.por3 != info2.por3)
            {
                return false;
            }
            if (info.por4 != info2.por4)
            {
                return false;
            }
            if (info.por5 != info2.por5)
            {
                return false;
            }
            if (info.por6 != info2.por6)
            {
                return false;
            }
            if (info.por7 != info2.por7)
            {
                return false;
            }
            if (info.por8 != info2.por8)
            {
                return false;
            }
            if (info.por9 != info2.por9)
            {
                return false;
            }
            if (info.por10 != info2.por10)
            {
                return false;
            }
            if (info.dadata != info2.dadata)
            {
                return false;
            }
            if (info.cai1 != info2.cai1)
            {
                return false;
            }
            if (info.cai2 != info2.cai2)
            {
                return false;
            }
            if (info.cai3 != info2.cai3)
            {
                return false;
            }
            if (info.cai4 != info2.cai4)
            {
                return false;
            }
            if (info.semana != info2.semana)
            {
                return false;
            }
            if (info.padrao != info2.padrao)
            {
                return false;
            }
            if (info.st != info2.st)
            {
                return false;
            }
            if (info.juros != info2.juros)
            {
                return false;
            }

            return true;
        }

        public DataTable CarregaGrid(String conexao)
        {
            clsCondpagtoDAL clsCondpagtoDAL = new clsCondpagtoDAL();
            return clsCondpagtoDAL.CarregaGrid(conexao);
        }
         */
    }
         
}
