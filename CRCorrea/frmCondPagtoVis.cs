using CRCorreaBLL;
using CRCorreaFuncoes;

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmCondPagtoVis : Form
    {
       

        clsBasicReport clsBR;

        BackgroundWorker bwrCondPagto;

        Int32 id;

        static DataTable dtCondPagto;

        clsCondpagtoBLL clsCondpagtoBLL;

        Boolean carregandoCondPagto;

        String filtro = "";

        // APAGAR ABAIXO
        private String conexao;

        public frmCondPagtoVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao)
        {
            clsBR = new clsBasicReport(this, dgvCondpagto, toolTip1, gbxCondPagto);
            carregandoCondPagto = false;
            clsCondpagtoBLL = new clsCondpagtoBLL();
            clsInfo.zrowid = 0;

            // apagar abaixo
            conexao = _conexao;
        }
        private void frmCondPagtoVis_Load(object sender, EventArgs e)
        {

        }

        private void frmCondPagtoVis_Activated(object sender, EventArgs e)
        {
            bwrCondPagto_Run();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmCondPagto frmCondPagto = new frmCondPagto();
            frmCondPagto.Init(0, dgvCondpagto.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmCondPagto, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCondpagto.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvCondpagto.CurrentRow.Cells["ID"].Value.ToString());
                frmCondPagto frmCondPagto = new frmCondPagto();
                frmCondPagto.Init(id, dgvCondpagto.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmCondPagto, clsInfo.conexaosqldados);
            }
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Cond. Pagamento", clsInfo.conexaosqldados);
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Não disponivel neste Modulo !!");
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }


        private void dgvCondpagto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCondpagto.CurrentRow != null)
                tspAlterar.PerformClick();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvCondpagto.CurrentRow != null)
                clsInfo.zrow = dgvCondpagto.CurrentRow;
            this.Close();
            this.Dispose();
        }



        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCondpagto);
        }

        private void bwrCondPagto_Run()
        {
            if (carregandoCondPagto == false)
            {
                carregandoCondPagto = true;
                //pbxCondPagto.Visible = true;
                bwrCondPagto = new BackgroundWorker();
                bwrCondPagto.DoWork += new DoWorkEventHandler(bwrCondPagto_DoWork);
                bwrCondPagto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCondPagto_RunWorkerCompleted);
                bwrCondPagto.RunWorkerAsync();
            }
        }


        private void bwrCondPagto_DoWork(object sender, DoWorkEventArgs e)
        {
            dtCondPagto = clsCondpagtoBLL.GridDados(clsInfo.conexaosqldados, filtro);
        }


        private void bwrCondPagto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //pbxCondPagto.Visible = false;
                clsCondpagtoBLL.GridMonta(dgvCondpagto, dtCondPagto, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvCondpagto, 1);
                carregandoCondPagto = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoCondPagto = false;
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
                bwrCondPagto_Run();
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

        private void dgvCondpagto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void rbnInativo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnAtivo.Checked)
            {
                filtro = "S";
            }
            else if (rbnInativo.Checked)
            {
                filtro = "N";
            }
            else
            {
                filtro = "";
            }

            bwrCondPagto_Run();
        }

    }
}
