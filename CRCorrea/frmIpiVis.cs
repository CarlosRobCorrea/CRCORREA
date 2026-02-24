using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmIpiVis : Form
    {
        Int32 id;

        BackgroundWorker bwrIpi;
        DataTable dtIpi;
        clsIpiBLL clsIpiBLL;
        Boolean carregandoIpi;

        clsBasicReport clsBR;
        String ativo;
        String resumo;


        public frmIpiVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            carregandoIpi = false;
            clsIpiBLL = new clsIpiBLL();
            clsInfo.zrowid = 0;
            clsBR = new clsBasicReport(this, dgvIpi, toolTip1);
        }

        private void frmIpiVis_Load(object sender, EventArgs e)
        {

        }
        private void frmIpiVis_Activated(object sender, EventArgs e)
        {
            bwrIpi_Run();
        }
        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmIpi frmIpi = new frmIpi();
            frmIpi.Init(0, dgvIpi.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmIpi, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvIpi.CurrentRow != null)
            {
                if ((Int32)dgvIpi.CurrentRow.Cells[0].Value > 0)
                {
                    id = (Int32)dgvIpi.CurrentRow.Cells["id"].Value;
                    frmIpi frmIpi = new frmIpi();
                    frmIpi.Init(id, dgvIpi.Rows);

                    clsFormHelper.AbrirForm(this.MdiParent, frmIpi, clsInfo.conexaosqldados);
                }
                else
                {
                    MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvIpi.Select();
                }
            }

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {

        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bwrIpi_Run()
        {
            if (carregandoIpi == false)
            {
                carregandoIpi = true;

                dgvIpi.DataSource = null;
                bwrIpi = new BackgroundWorker();
                bwrIpi.DoWork += new DoWorkEventHandler(bwrIpi_DoWork);
                bwrIpi.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrIpi_RunWorkerCompleted);
                bwrIpi.RunWorkerAsync();
            }
        }


        private void bwrIpi_DoWork(object sender, DoWorkEventArgs e)
        {
            ativo = "";
            if (rbnAtivoS.Checked == true)
            {
                ativo = "S";
            }
            else if (rbnAtivoN.Checked == true)
            {
                ativo = "N";
            }
            resumo = "";
            if (rbnResumoS.Checked == true)
            {
                resumo = "T";
            }
            else if (rbnResumoN.Checked == true)
            {
                resumo = "S";
            }

            dtIpi = clsIpiBLL.GridDados(ativo, resumo, clsInfo.conexaosqldados);
        }


        private void bwrIpi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsIpiBLL.GridMonta(dgvIpi, dtIpi, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvIpi, "CODIGO");
                //labelQtde.Text = "Total de Itens Cadastrados : " + dgvIpi.RowCount.ToString("N0");

                carregandoIpi = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);

            bwrIpi_Run();
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void rbnAtivo_Click(object sender, EventArgs e)
        {
            ativo = "";
            bwrIpi_Run();
        }

        private void rbnAtivoS_CheckedChanged(object sender, EventArgs e)
        {
            ativo = "S";
            bwrIpi_Run();
        }

        private void rbnAtivoN_CheckedChanged(object sender, EventArgs e)
        {
            ativo = "N";
            bwrIpi_Run();
        }
        private void rbnResumoT_Click(object sender, EventArgs e)
        {
            resumo = "";
            bwrIpi_Run();
        }
        private void rbnResumoS_Click(object sender, EventArgs e)
        {
            resumo = "T";
            bwrIpi_Run();
        }
        private void rbnResumoN_Click(object sender, EventArgs e)
        {
            resumo = "S";
            bwrIpi_Run();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvIpi);
        }

        private void tstbxLocalizar_MouseUp(object sender, MouseEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvIpi);
        }

        private void dgvIpi_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }
    }
}
