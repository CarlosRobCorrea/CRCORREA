using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPagasVis : Form
    {
        Int32 indexpagas;
        Int32 idpagas;

        DataTable dtpagas;

        DataGridView dgvPagas_compl = new DataGridView();

        public static Int32 indexpagasGeral;

        
        
        
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        DateTime DataDe;
        DateTime DataAte;

        Boolean bnCarregou;
        String Filtro_Situacao;
        String query;

        DialogResult resultado;

        Int32 idpagarnfe;
        Int32 idnotafiscal;
        Int32 iddocumento;
        String documento;

        clsBasicReport clsBR;

        public frmPagasVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            bnCarregou = false;

            clsBR = new clsBasicReport(this, dgvPagas, ttpPagas, gbxTransporte);

            // Por Período
            rbnTipoP.Checked = true;
            tbxDataDe.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            tbxDataAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void FillFiltros()
        {
            try
            {
                if (rbnDataPag.Checked == true)
                {
                    Filtro_Situacao = "0";
                }
                else if (rbnDataVenc.Checked == true)
                {
                    Filtro_Situacao = "1";
                }
                DataDe = DateTime.Parse(tbxDataDe.Text + " 00:00:00");
                DataAte = DateTime.Parse(tbxDataAte.Text + " 23:59:59");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean EditouFiltros()
        {
            switch (Filtro_Situacao)
            {
                case "0":
                    if (rbnDataPag.Checked == false) return true;
                    break;
                case "1":
                    if (rbnDataVenc.Checked == false) return true;
                    break;
            }

            if (DataDe != DateTime.Parse(tbxDataDe.Text + " 00:00:00"))
            {
                return true;
            }
            else if (DataAte != DateTime.Parse(tbxDataAte.Text + " 23:59:59"))
            {
                return true;
            }

            return false;
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmpagasVis_Load(object sender, EventArgs e)
        {
            indexpagasGeral = 0;
        }

        private void frmpagasVis_Shown(object sender, EventArgs e)
        {
            clsFormHelper.VerificarForm(this, ttpPagas, clsInfo.conexaosqldados);
        }

        private void frmpagasVis_Activated(object sender, EventArgs e)
        {
            FillFiltros();
            bwrpagas_RunWorkerAsync();
        }

        private void bwrpagas_RunWorkerAsync()
        {
            bnCarregou = false;
            pbxPagas.Visible = true;
            bwrPagas = new BackgroundWorker();
            bwrPagas.DoWork += new DoWorkEventHandler(bwrpagas_DoWork);
            bwrPagas.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrpagas_RunWorkerCompleted);
            bwrPagas.RunWorkerAsync();
        }

        private void bwrpagas_DoWork(object sender, DoWorkEventArgs e)
        {
            dtpagas = CarregaGridpagas();
        }

        public DataTable CarregaGridpagas()
        {
            try
            {
                query = "PAGAS.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString());

                if (rbnTipoT.Checked == true)
                {
                    //todas                   
                    return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                "PAGAS LEFT JOIN PAGAS01 ON PAGAS.ID = PAGAS01.IDDUPLICATA " +
                                "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAS.IDFORNECEDOR " +
                                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAS.IDDOCUMENTO " +
                                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAS.IDFORMAPAGTO " +
                                "LEFT JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID = PAGAS01.IDCOBRANCACOD ",
                                " PAGAS.ID, PAGAS.DUPLICATA, PAGAS.POSICAO , PAGAS.POSICAOFIM , PAGAS.EMISSAO, " +
                                "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, PAGAS.VENCIMENTO, " +
                                "PAGAS.VALOR, SITUACAOCOBRANCACOD.CODIGO AS [COB], PAGAS01.DATAENVIO, PAGAS01.VALOR AS [VALORPAGTO]",
                                query.ToString(), "PAGAS01.DATAENVIO");
                    
                }
                else
                {
                    //periodo
                    if (rbnDataPag.Checked == true)    // listas contas pagas com pagamento
                    {
                        query = query + " AND PAGAS.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataDe, true) +
                            " AND PAGAS.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(DataAte, true);
                        return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                    "PAGAS LEFT JOIN PAGAS01 ON PAGAS.ID = PAGAS01.IDDUPLICATA " +
                                    "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAS.IDFORNECEDOR " +
                                    "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAS.IDDOCUMENTO " +
                                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAS.IDFORMAPAGTO " +
                                    "LEFT JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID = PAGAS01.IDCOBRANCACOD ",
                                    " PAGAS.ID, PAGAS.DUPLICATA, PAGAS.POSICAO , PAGAS.POSICAOFIM , PAGAS.EMISSAO, " +
                                    "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, PAGAS.VENCIMENTO, " +
                                    "PAGAS.VALOR, SITUACAOCOBRANCACOD.CODIGO AS [COB], PAGAS01.DATAENVIO, PAGAS01.VALOR AS [VALORPAGTO]",
                                    query.ToString(), "PAGAS01.DATAENVIO");

                    }
                    else
                    {
                        query = query + " AND PAGAS.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataDe, true) +
                            " AND PAGAS.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(DataAte, true);
                        return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                    "PAGAS LEFT JOIN PAGAS01 ON PAGAS.ID = PAGAS01.IDDUPLICATA " +
                                    "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAS.IDFORNECEDOR " +
                                    "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAS.IDDOCUMENTO " +
                                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAS.IDFORMAPAGTO " +
                                    "LEFT JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID = PAGAS01.IDCOBRANCACOD ",
                                    " PAGAS.ID, PAGAS.DUPLICATA, PAGAS.POSICAO , PAGAS.POSICAOFIM , PAGAS.EMISSAO, " +
                                    "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, PAGAS.VENCIMENTO, " +
                                    "PAGAS.VALOR, SITUACAOCOBRANCACOD.CODIGO AS [COB], PAGAS01.DATAENVIO, PAGAS01.VALOR AS [VALORPAGTO]",
                                    query.ToString(), "PAGAS.VENCIMENTO");

                        //query = query + " AND PAGAS.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataDe, true) +
                        //    " AND PAGAS.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(DataAte, true);
                        //return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                        //            "PAGAS LEFT JOIN PAGAS01 ON PAGAS.ID = PAGAS01.IDDUPLICATA " +
                        //            "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAS.IDFORNECEDOR " +
                        //            "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAS.IDDOCUMENTO " +
                        //            "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAS.IDFORMAPAGTO " +
                        //            "LEFT JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID = PAGAS01.IDCOBRANCACOD ",
                        //            " PAGAS.ID, PAGAS.DUPLICATA, PAGAS.POSICAO , PAGAS.POSICAOFIM , PAGAS.EMISSAO, " +
                        //            "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, PAGAS.VENCIMENTO, " +
                        //            "PAGAS.VALOR, SITUACAOCOBRANCACOD.CODIGO AS [COB], PAGAS01.DATAOK, PAGAS01.VALOR AS [VALORPAGTO]",
                        //            query.ToString(), "PAGAS.VENCIMENTO");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrpagas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                pbxPagas.Visible = false;
                dgvPagas.DataSource = dtpagas;
                
                    clsGridHelper.MontaGrid(dgvPagas,
                            new String[] { "ID", "Duplicata", "Pos", "Fim", "Emissão", "Doc", "Cognome", "Vencimento", "A Pagar", "Cob", "Pagou Em", "Valor Pago" },
                            new String[] { "ID", "DUPLICATA", "POSICAO", "POSICAOFIM", "EMISSAO", "DOCUMENTO", "COGNOME", "VENCIMENTO", "VALOR", "COB", "DATAENVIO", "VALORPAGTO" },
                            new int[] { 1, 65, 25, 25, 75, 35, 170, 75, 100, 35, 75, 100 },
                            new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleRight},
                            new bool[] { false, true, true, true, true, true, true, true, true, true, true, true },
                            true, 7, ListSortDirection.Ascending);
                    dgvPagas.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvPagas.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvPagas.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvPagas.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                    dgvPagas.Columns["VALORPAGTO"].DefaultCellStyle.Format = "N2";
               
               


                clsGridHelper.FontGrid(dgvPagas, 8);
                clsGridHelper.SelecionaLinha(indexpagas, dgvPagas, "DUPLICATA");
                bnCarregou = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvPagas);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void tspVisualizar_Click(object sender, EventArgs e)
        {
            if (dgvPagas.CurrentRow != null)
            {
                indexpagas = clsParser.Int32Parse(dgvPagas.CurrentRow.Cells["ID"].Value.ToString());
                frmPagas frmPagas = new frmPagas();
                frmPagas.Init(clsParser.Int32Parse(dgvPagas.CurrentRow.Cells["ID"].Value.ToString()));

                
                clsFormHelper.AbrirForm(this.MdiParent, frmPagas, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvPagas.Select();
            }
        }

        private void tbxDataAte_Leave(object sender, EventArgs e)
        {
            if (EditouFiltros())
            {
                FillFiltros();
                bwrpagas_RunWorkerAsync();
            }
            clsVisual.ControlLeave(sender);
        }

        private void rbnValue_CheckedChanged(object sender, EventArgs e)
        {
            if (bnCarregou == true)
            {
                FillFiltros();
                bwrpagas_RunWorkerAsync();
            }
            if (rbnTipoT.Checked == true)
            {
                gbxPeriodo.Enabled = false;
            }
            else
            {
                gbxPeriodo.Enabled = true;
            }
        }

        private void dgvPagas_DoubleClick(object sender, EventArgs e)
        {

        }

        private void tspExtornar_Click(object sender, EventArgs e)
        {
            resultado = MessageBox.Show("Deseja Estornar/Cancelar o Pagamento : " + Environment.NewLine +
                          "Duplicata = " + dgvPagas.CurrentRow.Cells["DUPLICATA"].Value.ToString() + Environment.NewLine +
                          "Cognome = " + dgvPagas.CurrentRow.Cells["COGNOME"].Value.ToString() + Environment.NewLine +
                          "Vencimento = " + dgvPagas.CurrentRow.Cells["VENCIMENTO"].Value.ToString() + Environment.NewLine +
                          "", "Aplisoft",
                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1);
            if (resultado == DialogResult.Yes)
            {
                idpagarnfe = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDPAGARNFE from PAGAS where ID = " + clsParser.Int32Parse(dgvPagas.CurrentRow.Cells["ID"].Value.ToString()), "0"));
                idpagas = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PAGAS where IDPAGARNFE = " + clsParser.Int32Parse(idpagarnfe.ToString()), "0"));
                idnotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDNOTAFISCAL from PAGAS where ID = " + clsParser.Int32Parse(dgvPagas.CurrentRow.Cells["ID"].Value.ToString()), "0"));
                iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID = " + idnotafiscal, "0"));
                documento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID = " + iddocumento, "");

                switch (documento.PadRight(3, ' ').Substring(0, 3))
                {
                    case "NFE":
                        break;
                    case "NFV":
                        break;
                    case "DES":
                        break;


                    default:
                        resultado = DialogResult.No;
                        break;
                }
                if (resultado == DialogResult.Yes)
                {  // efetuar a baixa
                    using (TransactionScope tse = new TransactionScope())
                    {
                        clsFinanceiro.ExtornoPagas(documento, idnotafiscal, idpagarnfe, idpagas);
                        frmpagasVis_Activated(sender, e);
                        tse.Complete();
                    }

                }
            }
        }

        private void dgvPagas_fil_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspVisualizar.PerformClick();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            frmRelPagar frmRelPagar = new frmRelPagar();
            frmRelPagar.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelPagar, clsInfo.conexaosqldados);

        }

        private void dgvPagas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
