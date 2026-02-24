using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;


namespace CRCorreaBLL
{
    public class clsPedidoReceberBLL : SQLFactory<clsPedidoReceberInfo>
    {
        public override int Incluir(clsPedidoReceberInfo info, string cnn)
        {
            Int32 result;
            Int32 iddocumento;


            clsReceberBLL ReceberBLL = new clsReceberBLL();

            VerificaInfo(info);

            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from docfiscal where cognome='PED'"));

            result = base.Incluir(info, cnn);

            ReceberBLL.SicronizarPorDocumento(info.idnota, iddocumento, cnn);

            return result;
        }

        public override int Alterar(clsPedidoReceberInfo info, string cnn)
        {
            Int32 result = 0;

            VerificaInfo(info);

            Int32 iddocumento;


            clsReceberBLL ReceberBLL = new clsReceberBLL();

            if (info.pagou == "S" ||
                clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select count(*) from pedidoreceber where idnota=" + info.idnota + " and pagou='S'")) > 0)
            {
                throw new Exception("Não é possível alterar a parcela pois já houve baixas.");
            }

            //iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iddocumento from pedido where id=" + info.idnota));
            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from docfiscal where cognome='PED'"));

            result = base.Alterar(info, cnn);

            ReceberBLL.SicronizarPorDocumento(info.idnota, iddocumento, cnn);

            return result;
        }

        public override bool Excluir(int id, string cnn)
        {
            Boolean result = true;

            Int32 idnota;
            Int32 iddocumento;
            String pagou;


            clsReceberBLL ReceberBLL = new clsReceberBLL();

            idnota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idnota from pedidoreceber where id=" + id));
            pagou = Procedure.PesquisaoPrimeiro(cnn, "select pagou from pedidoreceber where id=" + id);
            //iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iddocumento from nfcompra where id=" + idnota));
            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from docfiscal where cognome='PED'"));

            if (pagou == "S" ||
                clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select count(*) from pedidoreceber where idnota=" + idnota + " and pagou='S'")) > 0)
            {
                throw new Exception("Não é possível Excluir a parcela pois já houve baixas.");
            }

            result = base.Excluir(id, cnn);

            if (result == true)
            {
                result = ReceberBLL.ExcluirPorLancamento(id, iddocumento, cnn);
            }
            else
            {
                ReceberBLL.ExcluirPorLancamento(id, iddocumento, cnn);
            }

            return result;
        }

        public void VerificaInfo(clsPedidoReceberInfo info)
        {
            if (info.valor == 0)
            {
                throw new Exception("Não é possível Salvar uma parcela com o valor 0(zero).");
            }
        }

        public static DataTable GridDados(Int32 idNota)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PEDIDORECEBER.ID,PEDIDORECEBER.IDNOTA,PEDIDORECEBER.IDTIPOPAGA, " +
                    "PEDIDORECEBER.POSICAO,PEDIDORECEBER.POSICAOFIM,PEDIDORECEBER.DATA,PEDIDORECEBER.VALOR, " +
                    "PEDIDORECEBER.PAGOU, " +
                    "SITUACAOTIPOTITULO.CODIGO + ' = ' + SITUACAOTIPOTITULO.NOME AS [TIPOPAGA], " +
                    "PEDIDORECEBER.BOLETONRO, PEDIDORECEBER.DV  " +
                    "from PEDIDORECEBER  " +
                    "left JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PEDIDORECEBER.IDTIPOPAGA ";
            filtro = " where PEDIDORECEBER.IDNOTA = @IDNOTA ";

            query = query + filtro + " ORDER BY PEDIDORECEBER.DATA, PEDIDORECEBER.POSICAO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDNOTA", SqlDbType.Int).Value = idNota;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Nota", "IDNOTA", 0, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Id TipoPaga", "IDTIPOPAGA", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pos I", "POSICAO", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Pos F", "POSICAOFIM", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Vencimento", "DATA", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor Parcela", "VALOR", 125, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pagou ?", "PAGOU", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Forma Pgto", "TIPOPAGA", 120, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Boleto Nro", "BOLETONRO", 170, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("DV", "DV",  40, true, DataGridViewContentAlignment.MiddleLeft),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATA");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALOR"].DefaultCellStyle.Format = "N2";
        }

    }
}
