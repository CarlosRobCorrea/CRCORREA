using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRequisicaoVis : Form
    {
        clsBasicReport clsBr;

        Int32 id;

        static DataTable dtRequisicao;

        clsRequisicaoBLL clsRequisicaoBLL;

        Boolean carregandoRequisicao;
        BackgroundWorker bwrRequisicao;
        
        // ITENS DA REQUISICAO (REQUISICAO1)
        static DataTable dtRequisicao1;

        BackgroundWorker bwrRequisicao1;

        clsRequisicao1BLL clsRequisicao1BLL;

        public frmRequisicaoVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            nudRequisicao.Value = DateTime.Now.Year;

            clsBr = new clsBasicReport(this, dgvRequisicao, ToolTip);
            carregandoRequisicao = false;
            clsRequisicaoBLL = new clsRequisicaoBLL();
            clsRequisicao1BLL = new clsRequisicao1BLL();
            clsInfo.zrowid = 0;
        }
        private void frmRequisicao_ApliVis_Load(object sender, EventArgs e)
        {
            // procurar o dOcumento (RM) == se não encontrar - não deixe incluir
            Int32 IdDocumento = clsInfo.zdocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + "RM" + "' "));
            if (IdDocumento == 0)
            {
                MessageBox.Show("Inclua o Documento RM - sem ele não pode fazer requisisão de material");
                gbxRequisicao.Enabled = false;
                gbxItensRequisicao.Enabled = false;

            }

        }
        private void frmRequisicaoVis_Activated(object sender, EventArgs e)
        {
            bwrRequisicao_Run();
        }

        private void tspRequisicaoIncluir_Click(object sender, EventArgs e)
        {
            frmRequisicao_Apli frmRequisicao_Apli = new frmRequisicao_Apli();
            frmRequisicao_Apli.Init(0, dgvRequisicao.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmRequisicao_Apli, clsInfo.conexaosqldados);

        }

        private void tspRequisicaoAlterar_Click(object sender, EventArgs e)
        {
            if (dgvRequisicao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvRequisicao.CurrentRow.Cells["ID"].Value.ToString());
                frmRequisicao_Apli frmRequisicao_Apli = new frmRequisicao_Apli();
                frmRequisicao_Apli.Init(id, dgvRequisicao.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmRequisicao_Apli, clsInfo.conexaosqldados);
                
            }
        }

        private void tspRequisicaoImprimirEPI_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento !!");
            /*
            frmRelPecas frmRelPecas = new frmRelPecas();
            frmRelPecas.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelPecas);
            */

        }

        private void tspRequisicaoImprimirMnu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento !!");
            /*
            frmRelPecas frmRelPecas = new frmRelPecas();
            frmRelPecas.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelPecas);
            */

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bwrRequisicao_Run()
        {
            if (carregandoRequisicao == false)
            {
                carregandoRequisicao = true;

                //pbxPeca.Visible = true;
                bwrRequisicao = new BackgroundWorker();
                bwrRequisicao.DoWork += new DoWorkEventHandler(bwrRequisicao_DoWork);
                bwrRequisicao.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrRequisicao_RunWorkerCompleted);
                bwrRequisicao.RunWorkerAsync();
            }
        }


        private void bwrRequisicao_DoWork(object sender, DoWorkEventArgs e)
        {
            Int32 ano = 0;
            if (rbnTodas.Checked == true)
            {
                ano = 0;
            }
            else if (rbnAno.Checked == true)
            {
                ano = clsParser.Int32Parse(nudRequisicao.Value.ToString());
            }
            dtRequisicao = clsRequisicaoBLL.GridDados(clsInfo.zfilial, ano, clsInfo.conexaosqldados);
        }


        private void bwrRequisicao_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //  pbxPeca.Visible = false;
                clsRequisicaoBLL.GridMonta(dgvRequisicao, dtRequisicao, clsInfo.zrowid);
                carregandoRequisicao = false;
                clsGridHelper.SelecionaLinha(id,  dgvRequisicao,  1);
                clsGridHelper.FontGrid(dgvRequisicao, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoRequisicao = false;
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            // frmPecasVis_Activated(sender, e);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }
        private void PesquisaRapida()
        {
            dgvRequisicao = clsGridHelper.PesquisaRapida(dgvRequisicao, dtRequisicao, tbxPesquisa.Text);
            clsGridHelper.SelecionaLinha(id, dgvRequisicao, "CODIGO");
            tbxPesquisa.Focus();
            if (dgvRequisicao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvRequisicao.CurrentRow.Cells["CODIGO"].Value.ToString());
            }
            else
            {
                id = 0;
            }
        }
        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }
        private void tbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
        }

        private void rbnTodas_Click(object sender, EventArgs e)
        {
            bwrRequisicao_Run();
        }

        private void rbnAno_Click(object sender, EventArgs e)
        {
            bwrRequisicao_Run();
        }

        private void nudRequisicao_Click(object sender, EventArgs e)
        {
            bwrRequisicao_Run();
        }

        private void dgvRequisicao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = (Int32)dgvRequisicao.CurrentRow.Cells[0].Value;
            if (id > 0)
            {
                //if (gbxItensRequisicao.Visible == false) {gbxItensRequisicao.Visible = true; }
                bwrRequisicao1_Run();
            }
        }
        private void bwrRequisicao1_Run()
        {
                //pbxPeca.Visible = true;
                bwrRequisicao1 = new BackgroundWorker();
                bwrRequisicao1.DoWork += new DoWorkEventHandler(bwrRequisicao1_DoWork);
                bwrRequisicao1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrRequisicao1_RunWorkerCompleted);
                bwrRequisicao1.RunWorkerAsync();
        }
        private void bwrRequisicao1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtRequisicao1 = clsRequisicao1BLL.GridDados(id, clsInfo.conexaosqldados);
        }


        private void bwrRequisicao1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsRequisicao1BLL.GridMonta(dgvRequisicao1, dtRequisicao1, id);
                clsGridHelper.SelecionaLinha(id, dgvRequisicao1, 2);
                clsGridHelper.FontGrid(dgvRequisicao1, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
            }
        }

        private void dgvRequisicao_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspRequisicaoAlterar.PerformClick();
        }

        private void tspRequisicaoItensIncluir_Click(object sender, EventArgs e)
        {

        }
    }
}
