using CRCorreaInfo;
using CRCorreaFuncoes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsComprasEntregaBLL : SQLFactory<clsComprasEntregaInfo>
    {
        public override int Incluir(clsComprasEntregaInfo info, string cnn)
        {
            //using (TransactionScope tse = new TransactionScope())
            //{
                info.id = base.Incluir(info, cnn);

                //IncluirComprasEntregaValores(info, cnn);

                Compra1SicronizarQtdes(info.idcompras1, cnn);

            //    tse.Complete();
            //}

            return info.id;
        }

        public override int Alterar(clsComprasEntregaInfo info, string cnn)
        {
            Int32 result = 0;

            //using (TransactionScope tse = new TransactionScope())
            //{
                result = base.Alterar(info, cnn);

                //IncluirComprasEntregaValores(info, cnn);

                Compra1SicronizarQtdes(info.idcompras1, cnn);

            //    tse.Complete();
            //}

            return result;
        }

        public override bool Excluir(int id, string cnn)
        {
            Boolean result;

            Int32 idcompras1;
            clsComprasEntregaValoresBLL ComprasEntregaValoresBLL = new clsComprasEntregaValoresBLL();

            //using (TransactionScope tse = new TransactionScope())
            //{
                idcompras1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcompras1 from comprasentrega where id=" + id));

                //ComprasEntregaValoresBLL.ExcluirComprasEntrega(id, cnn);

                result = base.Excluir(id, cnn);

                Compra1SicronizarQtdes(idcompras1, cnn);

            //    tse.Complete();
            //}

            return result;
        }

        private void IncluirComprasEntregaValores(clsComprasEntregaInfo info, String cnn)
        {
            clsCondpagtoBLL CondpagtoBLL = new clsCondpagtoBLL();
            clsCondpagtoInfo CondpagtoInfo;

            clsComprasBLL ComprasBLL = new clsComprasBLL();
            clsComprasInfo ComprasInfo = new clsComprasInfo();
            ComprasInfo = ComprasBLL.Carregar(info.idcompras, cnn);

            clsCompras1BLL Compras1BLL = new clsCompras1BLL();
            clsCompras1Info Compras1Info = new clsCompras1Info();
            Compras1Info = Compras1BLL.Carregar(info.idcompras1, cnn);

            clsComprasEntregaValoresBLL ComprasEntregaValoresBLL = new clsComprasEntregaValoresBLL();
            clsComprasEntregaValoresInfo ComprasEntregaValoresInfo = new clsComprasEntregaValoresInfo();
            CondpagtoInfo = CondpagtoBLL.Carregar(ComprasInfo.idcondpagto, cnn);

            // Excluir Valores das Entregas
            ComprasEntregaValoresBLL.ExcluirComprasEntrega(info.id, cnn);

            if (Compras1Info.totalnota == 0 || Compras1Info.qtdefiscal == 0)
            {
                return;
            }

            DateTime datavencimento = info.dataentrega;
            Decimal precoun = clsVisual.Truncar(Compras1Info.totalnota / Compras1Info.qtdefiscal, 5);
            Decimal valorvencimento = clsVisual.Truncar(precoun * info.qtdesaldo, 2);

            if (datavencimento < DateTime.Now)
            {
                datavencimento = DateTime.Now;
            }

            if (valorvencimento > 0)
            {
                DataTable dtValores = clsFinanceiro.GerarFatura(datavencimento, valorvencimento, ComprasInfo.idformapagto, ComprasInfo.idcondpagto);

                foreach (DataRow row in dtValores.Rows)
                {
                    ComprasEntregaValoresInfo = new clsComprasEntregaValoresInfo();

                    ComprasEntregaValoresInfo.idcompras = info.idcompras;
                    ComprasEntregaValoresInfo.idcompras1 = info.idcompras1;
                    ComprasEntregaValoresInfo.idcomprasentrega = info.id;

                    ComprasEntregaValoresInfo.datavencimento = clsParser.DateTimeParse(row["data"].ToString());
                    ComprasEntregaValoresInfo.valorvencimento = clsParser.DecimalParse(row["valor"].ToString());

                    ComprasEntregaValoresBLL.Incluir(ComprasEntregaValoresInfo, cnn);
                }
            }
        }

        private void Compra1SicronizarQtdes(Int32 idcompras1, String cnn)
        {
            clsCompras1Info Compras1Info;
            clsCompras1BLL Compras1BLL = new clsCompras1BLL();

            Compras1Info = Compras1BLL.Carregar(idcompras1, cnn);

            //Compras1Info.qtdeentra = 0;
            //Compras1Info.qtdetotal = 0;
            //Compras1Info.qtdetransfe = 0;

            Compras1Info.qtdebaixada = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdebaixada) from comprasentrega where idcompras1=" + idcompras1));
            Compras1Info.qtdedefeito = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdedefeito) from comprasentrega where idcompras1=" + idcompras1));
            Compras1Info.qtdeentregue = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdeentregue) from comprasentrega where idcompras1=" + idcompras1));
            Compras1Info.qtdeosaux = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdeosaux) from comprasentrega where idcompras1=" + idcompras1));
            Compras1Info.qtdesucata = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdesucata) from comprasentrega where idcompras1=" + idcompras1));
            Compras1Info.qtdesaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdesaldo) from comprasentrega where idcompras1=" + idcompras1));
            Compras1Info.totalprevisto = (Compras1Info.qtdesaldo * Compras1Info.preco);

            if (Compras1Info.codigoemp01 == null) Compras1Info.codigoemp01 = "";
            if (Compras1Info.codigoemp02 == null) Compras1Info.codigoemp02 = "";
            if (Compras1Info.codigoemp03 == null) Compras1Info.codigoemp03 = "";
            if (Compras1Info.codigoemp04 == null) Compras1Info.codigoemp04 = "";
            if (Compras1Info.creditaricm == null) Compras1Info.creditaricm = "";

            Compras1BLL.Alterar(Compras1Info, cnn);

            clsComprasInfo ComprasInfo;
            clsComprasBLL ComprasBLL = new clsComprasBLL();

            ComprasInfo = ComprasBLL.Carregar(Compras1Info.idcompras, cnn);

            if (ComprasInfo.termino == "S")
            {
                // throw new Exception("Não é possível manipular as Qtdes de um Pedido de Compra já finalizado.");
            }

            ComprasInfo.qtdebaixada = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdebaixada) from comprasentrega where idcompras=" + ComprasInfo.id));
            ComprasInfo.qtdedefeito = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdedefeito) from comprasentrega where idcompras=" + ComprasInfo.id));
            ComprasInfo.qtdeentregue = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdeentregue) from comprasentrega where idcompras=" + ComprasInfo.id));
            ComprasInfo.qtdeosaux = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdeosaux) from comprasentrega where idcompras=" + ComprasInfo.id));
            ComprasInfo.qtdesucata = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdesucata) from comprasentrega where idcompras=" + ComprasInfo.id));
            ComprasInfo.qtdesaldo = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdesaldo) from comprasentrega where idcompras=" + ComprasInfo.id));
            ComprasInfo.totalprevisto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(totalprevisto) from compras1 where idcompras=" + ComprasInfo.id));
            if (ComprasInfo.qtdesaldo == 0)
            {
                ComprasInfo.termino = "S";
            }

            ComprasBLL.Alterar(ComprasInfo, cnn);
        }

        public static DataTable GridDados(Int32 Idcompras1)
        { // item do pedido de compras
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT id, idcompras, idcompras1, dataentrega, qtdeentrega, qtdeentregue, qtdedefeito, " +
                    "qtdesucata, qtdebaixada, qtdeosaux, qtdesaldo, idos " +
                    "from comprasentrega ";
            filtro = " WHERE comprasentrega.idcompras1 = @IDCOMPRAS1 ";
            filtro = filtro + " ORDER BY COMPRASENTREGA.DATAENTREGA DESC  ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDCOMPRAS1", SqlDbType.Int).Value = Idcompras1;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 10, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dt_Entrega", "DATAENTREGA", 80, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Prog ", "QTDEENTREGA", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Saldo", "QTDESALDO", 60, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATAENTREGA");
            dgv.Columns["DATAENTREGA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["QTDEENTREGA"].DefaultCellStyle.Format = "N3";
            dgv.Columns["QTDESALDO"].DefaultCellStyle.Format = "N3";
        }

    }
}
