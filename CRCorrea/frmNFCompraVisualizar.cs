using CRCorreaBLL; 
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmNFCompraVisualizar : Form
    {
        clsFinanceiro clsFinanceiro = new clsFinanceiro();
        clsBasicReport clsBr;

        SqlConnection scn;
        SqlCommand scd;

        BackgroundWorker bwrNFCompraVisualizar;

        Int32 id;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;


        String filtro_status;

        static DataTable dtNFCompraResumida;

        clsNfCompra1BLL clsNfCompra1BLL;
        clsNfCompra1Info clsNfCompra1Info;
        clsNfCompra1Info clsNfCompra1InfoOld;

        Boolean carregandoNFCompraResumida;

        public frmNFCompraVisualizar()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBr = new clsBasicReport(this, dgvNFCompraResumida, toolTip1, gbxNFCompraResumida);
            carregandoNFCompraResumida = false;
            clsNfCompra1BLL = new clsNfCompra1BLL();
            clsInfo.zrowid = 0;

            nMes.Text = DateTime.Now.Month.ToString().PadLeft(2, '0');
            nAno.Text = DateTime.Now.Year.ToString();


            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxFornecedor_Cognome);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxPecas_Codigo);
            //clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxHistorico);
            //clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxCentroCusto);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxContaContabil);
            //clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBanco_Conta);
        }

        private void frmNFCompraVisualizar_Load(object sender, EventArgs e)
        {
            rbnDoMes.Checked = true;
        }

        private void frmNFCompraVisualizar_Activated(object sender, EventArgs e)
        {
            bwrNFCompraVisualizar_Run();
            TrataCampos((Control)sender);
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            //id = 0;
            //gbxNFCompraResumida.Enabled = false;
            //tclNfCompraResumida.SelectedIndex = 1;
            //gbxNFCompraItem.Visible = true;
            //NFCompraResumidaCarregar();
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            //if (dgvNFCompraResumida.CurrentRow != null)
            //{
            //    id = clsParser.Int32Parse(dgvNFCompraResumida.CurrentRow.Cells["ID"].Value.ToString());
            //    gbxNFCompraResumida.Enabled = false;
            //    tclNfCompraResumida.SelectedIndex = 1;
            //    gbxNFCompraItem.Visible = true;
            //    NFCompraResumidaCarregar();
            //}
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }

        private void ControlKeyDownHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownHora((TextBox)sender, e);
        }
        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void bwrNFCompraVisualizar_Run()
        {
            if (carregandoNFCompraResumida == false)
            {
                carregandoNFCompraResumida = true;
                pbxNFCompraResumida.Visible = true;
                bwrNFCompraVisualizar = new BackgroundWorker();
                bwrNFCompraVisualizar.DoWork += new DoWorkEventHandler(bwrNFCompraVisualizar_DoWork);
                bwrNFCompraVisualizar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrNFCompraVisualizar_RunWorkerCompleted);
                bwrNFCompraVisualizar.RunWorkerAsync();
            }
        }


        private void bwrNFCompraVisualizar_DoWork(object sender, DoWorkEventArgs e)
        {
            if (rbnTodos.Checked == true)
            {
                filtro_status = "T";
            }
            else 
            {
                filtro_status = "A";
            }

            dtNFCompraResumida = clsNfCompra1BLL.GridDadosPeca1(Int32.Parse(nMes.Text) , Int32.Parse(nAno.Text), clsInfo.zfilial, filtro_status);
        }

        private void bwrNFCompraVisualizar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                pbxNFCompraResumida.Visible = false;
                clsNfCompra1BLL.GridMontaPeca1(dgvNFCompraResumida, dtNFCompraResumida, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvNFCompraResumida, 1);
                //for (int x = 0; x < dgvNFCompraResumida.Rows.Count; x++)
                //{
                //    if (clsParser.Int32Parse(dgvNFCompraResumida.Rows[x].Cells["IDFLUXO"].Value.ToString()) > 0)
                //    {
                //        dgvNFCompraResumida.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.SpringGreen;
                //    }
                //}

                carregandoNFCompraResumida = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoNFCompraResumida = false;
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            if (clsParser.Int32Parse(tbxNumero.Text) > 0)
            {
                if (clsParser.DecimalParse(tbxTotalNota.Text) > 0)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                    gbxNFCompraResumida.Enabled = true;
                    tclNfCompraResumida.SelectedIndex = 0;
                    gbxNFCompraItem.Visible = false;

                    bwrNFCompraVisualizar_Run();

                }
                else
                {
                    MessageBox.Show("Acerte o Valor do documento !!");
                }
            }
            else
            {
                MessageBox.Show("Não pode incluir um documento sem numero !!!!");
            }
        }

        private void tspRetornarItem_Click(object sender, EventArgs e)
        {
            gbxNFCompraResumida.Enabled = true;
            tclNfCompraResumida.SelectedIndex = 0;
            gbxNFCompraItem.Visible = false;
        }

        void TrataCampos(Control ctl)
        {
            nMes.Text = clsParser.Int32Parse(nMes.Text).ToString();
            nAno.Text = clsParser.Int32Parse(nAno.Text).ToString();
            if (ctl.Name == nMes.Name || ctl.Name == nAno.Name)
            {
                bwrNFCompraVisualizar_Run();
            }
            tbxNumero.Text = clsParser.Int32Parse(tbxNumero.Text).ToString();
            tbxTotalNota.Text = clsParser.DecimalParse(tbxTotalNota.Text).ToString("N2");

            ctl.Name = "";
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;


        }

        private void dgvNFCompraResumida_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //tspAlterar.PerformClick();
        }

        private void btnIdDocumento_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDocumento.Name;
            frmDocFiscalVis frmDocFiscalVis = new frmDocFiscalVis();
            frmDocFiscalVis.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscalVis, clsInfo.conexaosqldados);
        }
        private DialogResult Salvar()
        {
            DialogResult drt;
            
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            
            if (drt == DialogResult.Yes)
            {
                NFCompraResumidaSalvar();
            }

            return drt;
        }

        private void NFCompraResumidaSalvar()
        {
        }


        private void rbnTodos_Click(object sender, EventArgs e)
        {
            bwrNFCompraVisualizar_Run();
        }

        private void rbnDoMes_Click(object sender, EventArgs e)
        {
            bwrNFCompraVisualizar_Run();
        }

        private void tstbxLocalizar_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvNFCompraResumida);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvNFCompraResumida, "numero") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvNFCompraResumida, "numero") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvNFCompraResumida, "numero") == false)
                        {
                            if (dgvNFCompraResumida.Rows.Count > 0)
                            {
                                dgvNFCompraResumida.CurrentCell = dgvNFCompraResumida.Rows[0].Cells["numero"];
                            }
                        }
                    }
                }
            }
            else if (dgvNFCompraResumida.Rows.Count > 0)
            {
                dgvNFCompraResumida.CurrentCell = dgvNFCompraResumida.Rows[0].Cells["numero"];
            }

            if (dgvNFCompraResumida.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvNFCompraResumida.CurrentRow.Cells["IDNFE1"].Value.ToString());
                if (dgvNFCompraResumida.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvNFCompraResumida.Rows[
           dgvNFCompraResumida.CurrentRow.Index - 1].Cells["IDNFE1"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvNFCompraResumida.CurrentRow.Index < dgvNFCompraResumida.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvNFCompraResumida.Rows[
         dgvNFCompraResumida.CurrentRow.Index + 1].Cells["IDNFE1"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
            }
            else
            {
                id = 0;
                id_Anterior = 0;
                id_Proximo = 0;
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();

        }
    }
}
