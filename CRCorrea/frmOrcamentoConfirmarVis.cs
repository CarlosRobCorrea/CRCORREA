using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;

namespace CRCorrea
{
    public partial class frmOrcamentoConfirmarVis : Form
    {
        clsOrcamentoBLL clsOrcamentoBLL;

        Int32 id;

        DataTable dtOrcamento;

        clsBasicReport clsBr;
        
        Boolean carregando;
        Boolean carregandoPrimeiro;
        Boolean carregaActivated;

        

        public frmOrcamentoConfirmarVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsOrcamentoBLL = new clsOrcamentoBLL();

            clsBr = new clsBasicReport(this, dgvOrcamento, toolTip1);
            
            carregando = false;
            carregandoPrimeiro = false;
            carregaActivated = true;
        }

        private void frmOrcamentoVis_Activated(object sender, EventArgs e)
        {
            bwrOrcamento_Run();
        }

        private void frmOrcamentoVis_Shown(object sender, EventArgs e)
        {

        }

        private void bwrOrcamento_Run()
        {
            if (carregando == false)
            {
                if (carregandoPrimeiro == true && carregaActivated == false)
                {
                    return;
                }
                else
                {
                    carregandoPrimeiro = true;
                    carregaActivated = false;
                }

                carregando = true;

                //pbxOrcamento.Visible = true;

                bwrOrcamento = new BackgroundWorker();
                bwrOrcamento.DoWork += new DoWorkEventHandler(bwrOrcamento_DoWork);
                bwrOrcamento.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrOrcamento_RunWorkerCompleted);
                bwrOrcamento.RunWorkerAsync();
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
           clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
           clsVisual.ControlLeave(sender);


            if (sender is TextBox)
            {
                bwrOrcamento_Run();
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
           clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
           clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void bwrOrcamento_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                OrcamentoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OrcamentoGrid()
        {
            dtOrcamento = clsOrcamentoBLL.GridDadosConfirma(0, DateTime.Now.AddDays(-60), DateTime.Now.AddDays(5), "A");
        }

        private void bwrOrcamento_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList altOrcamento = clsBr.GetColunas();

                clsOrcamentoBLL.GridMontaConfirma(dgvOrcamento, dtOrcamento, id);

                clsBr.RecalculaGrid(altOrcamento);

                dgvOrcamento.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                //dgvOrcamento.Columns["TOTALORCAMENTOLIQUIDO"].DefaultCellStyle.Format = "N2";
//                dgvOrcamento.Columns["TOTALCONFIRMADO"].DefaultCellStyle.Format = "N2";

                //dgvOrcamento.Sort(dgvOrcamento.Columns["NUMERO"], ListSortDirection.Ascending);

                //GridHelper.FontGrid(dgvOrcamento, 7);

                clsGridHelper.SelecionaLinha(id, dgvOrcamento, "NUMERO");

                Filtrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregando = false;
                //pbxOrcamento.Visible = false;
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            clsBr.TravaGrid();

            frmProcurar frmProcurar = new frmProcurar();
            frmProcurar.Init(new DataGridView[] { this.dgvOrcamento }, true);

            frmProcurar.ShowDialog();

            clsBr.LiberaGrid();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvOrcamento.CurrentRow;
            this.Close();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Orcamento", clsInfo.conexaosqldados);
        }

        private void dgvOrcamento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbVisualizar.PerformClick();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            Filtrar();
        }

        void Filtrar()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvOrcamento);
        }

        private void dgvOrcamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tsbVisualizar.PerformClick();
            }
        }

        private void tsbVisualizar_Click(object sender, EventArgs e)
        {
            if (dgvOrcamento.CurrentRow != null)
            {
                id = (Int32)(dgvOrcamento.CurrentRow.Cells[0].Value);

                Int32 idcliente;
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcliente from orcamento where id=" + id));

                if (idcliente == clsInfo.zempresaclienteid)
                {
                    MessageBox.Show("O Cliente desse Orçamento não é válido. Coloque o Cliente correto.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcliente from clienteprospeccao where id=" + idcliente)) == 0)
                {
                    //MessageBox.Show("O Cliente é somente um Prospectado." + Environment.NewLine + "Cadastre o Cliente. ", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //frmClienteprospeccao frmClienteprospeccao = new frmClienteprospeccao();
                    //frmClienteprospeccao.Init(idcliente, dgvOrcamento.Rows);
                    //FormHelper.AbrirForm(this.MdiParent, frmClienteprospeccao, clsInfo.conexaosqldados);

                    //return;
                }

                carregaActivated = true;
                frmOrcamentoConfirmar frmOrcamentoConfirmar = new frmOrcamentoConfirmar();
                frmOrcamentoConfirmar.Init((Int32)dgvOrcamento.CurrentRow.Cells[0].Value, dgvOrcamento.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmOrcamentoConfirmar, clsInfo.conexaosqldados);
            }
        }

        private void frmOrcamentoConfirmarVis_Load(object sender, EventArgs e)
        {

        }

    }
}



