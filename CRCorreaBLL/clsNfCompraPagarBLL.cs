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
    public class clsNfCompraPagarBLL : SQLFactory<clsNfCompraPagarInfo>
    {
        public override int Incluir(clsNfCompraPagarInfo info, string cnn)
        {
            Int32 result;
            Int32 iddocumento;

            
            clsPagarBLL PagarBLL = new clsPagarBLL();

            VerificaInfo(info);

            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iddocumento from nfcompra where id=" + info.idnota));

            result = base.Incluir(info, cnn);

            PagarBLL.SicronizarPorDocumento(info.idnota, iddocumento, cnn);

            return result;
        }

        public override int Alterar(clsNfCompraPagarInfo info, string cnn)
        {
            Int32 result = 0;

            VerificaInfo(info);

            Int32 iddocumento;

            
            clsPagarBLL PagarBLL = new clsPagarBLL();

            if (info.pagou == "S" ||
                clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select count(*) from nfcomprapagar where idnota=" + info.idnota + " and pagou='S'")) > 0)
            {
                throw new Exception("Não é possível alterar a parcela pois já houve baixas.");
            }

            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iddocumento from nfcompra where id=" + info.idnota));

            result = base.Alterar(info, cnn);

            PagarBLL.SicronizarPorDocumento(info.idnota, iddocumento, cnn);

            return result;
        }

        public override bool Excluir(int id, string cnn)
        {
            Boolean result = true;

            Int32 idnota;
            Int32 iddocumento;
            String pagou;

            
            clsPagarBLL PagarBLL = new clsPagarBLL();

            idnota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idnota from nfcomprapagar where id=" + id));
            pagou = Procedure.PesquisaoPrimeiro(cnn, "select pagou from nfcomprapagar where id=" + id);
            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iddocumento from nfcompra where id=" + idnota));

            if (pagou == "S" ||
                clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select count(*) from nfcomprapagar where idnota=" + idnota + " and pagou='S'")) > 0)
            {
                throw new Exception("Não é possível Excluir a parcela pois já houve baixas.");
            }

            result = base.Excluir(id, cnn);

            if (result == true)
            {
                result = PagarBLL.ExcluirPorLancamento(id, iddocumento, cnn);
            }
            else
            {
                PagarBLL.ExcluirPorLancamento(id, iddocumento, cnn);
            }

            return result;
        }

        public void VerificaInfo(clsNfCompraPagarInfo info)
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
            query = "SELECT NFCOMPRAPAGAR.ID,NFCOMPRAPAGAR.IDNOTA,NFCOMPRAPAGAR.IDTIPOPAGA, " +
                    "NFCOMPRAPAGAR.POSICAO,NFCOMPRAPAGAR.POSICAOFIM,NFCOMPRAPAGAR.DATA,NFCOMPRAPAGAR.VALOR, " +
                    "NFCOMPRAPAGAR.PAGOU, " +
                    "SITUACAOTIPOTITULO.CODIGO + ' = ' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], " +
                    "NFCOMPRAPAGAR.BOLETONRO, NFCOMPRAPAGAR.DV  " +
                    "from NFCOMPRAPAGAR  " +
                    "left JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = NFCOMPRAPAGAR.IDTIPOPAGA ";
            filtro = " where NFCOMPRAPAGAR.IDNOTA = @IDNOTA " ;
            
            query = query + filtro + " ORDER BY NFCOMPRAPAGAR.DATA, NFCOMPRAPAGAR.POSICAO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDNOTA", SqlDbType.Int).Value = idNota;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public  static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Nota", "IDNOTA", 0, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Id TipoPaga", "IDTIPOPAGA", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pos I", "POSICAO", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Pos F", "POSICAOFIM", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Vencimento", "DATA", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor Parcela", "VALOR", 125, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pagou ?", "PAGOU", 60, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("Forma Pgto", "FORMAPAGTO", 120, true, DataGridViewContentAlignment.MiddleLeft),
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
