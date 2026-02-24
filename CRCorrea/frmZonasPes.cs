using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmZonasPes : Form
    {
        
        
        
        clsBasicReport Br;
        public static Int32 linhagrid;

        Int32 id;
        String filtro_status;

        DataTable dtZonas;
        Boolean carregandoZonas;

        clsZonasBLL clsZonasBLL = new clsZonasBLL();

        public frmZonasPes()
        {
            InitializeComponent();
        }

        public void Init() // tirado a conexao em 11/09/2009 - carlo)
        {
            linhagrid = new Int32();
            Br = new clsBasicReport(this, dgvZonas, ttpZonasVis, gbxZonas);
        }
        public void Init(String _tipo) // tirado a conexao em 11/09/2009 - carlo)
        {
            linhagrid = new Int32();
            filtro_status = _tipo;
            Br = new clsBasicReport(this, dgvZonas, ttpZonasVis, gbxZonas);
        }


        private void frmZonasPes_Load(object sender, EventArgs e)
        {
            if(filtro_status == "")
            {
                rbnAtivo.Checked = true;
            }
            if (filtro_status == "S")
            {
                rbnAtivoS.Checked = true;
            }
            if (filtro_status == "N")
            {
                rbnAtivoN.Checked = true;
            }
        }

        private void frmZonasPes_Activated(object sender, EventArgs e)
        {
            bwrZonas_Run();
        }
        private void bwrZonas_Run()
        {
            if (carregandoZonas == false)
                carregandoZonas = true;

            //pbxCarregarZonasVis.Visible = true;
            bwrZonas = new BackgroundWorker();
            bwrZonas.DoWork += new DoWorkEventHandler(bwrZonas_DoWork);
            bwrZonas.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrZonas_RunWorkCompleted);
            bwrZonas.RunWorkerAsync();
        }

        private void bwrZonas_DoWork(object sender, DoWorkEventArgs e)
        {
            if (rbnAtivoS.Checked == true)
            {
                filtro_status = "S";
            }
            else if (rbnAtivoN.Checked == true)
            {
                filtro_status = "N";
            }
            else if (rbnAtivo.Checked == true)
            {
                filtro_status = "";
            }

            dtZonas = clsZonasBLL.GridDados(filtro_status, clsInfo.conexaosqldados);
        }

        private void bwrZonas_RunWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsZonasBLL.GridMonta(dgvZonas, dtZonas, clsInfo.zrowid);

                if (linhagrid != 0)
                {
                    clsGridHelper.SelecionaLinha(linhagrid, dgvZonas, "codigo");
                }
                else if (id > 0)
                {
                    clsGridHelper.SelecionaLinha(id, dgvZonas, "codigo");
                }
                else if (id == 0)
                {
                    clsGridHelper.SelecionaLinha(clsParser.Int32Parse(
                        Procedure.PesquisaoPrimeiro(
                        clsInfo.conexaosqldados,
                        "select top 1 id from Zonas order by codigo", "1")), dgvZonas, "codigo");
                }

                // Pesquisa rápida();
                carregandoZonas = false;
                //pbxCarregarZonasVis.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoZonas = false;
                //pbxCarregarZonasVis.Visible = false;
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmZonas frmZonas = new frmZonas();
            frmZonas.Init(0, dgvZonas.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmZonas, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvZonas.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvZonas.CurrentRow.Cells["ID"].Value.ToString());
                frmZonas frmZonas = new frmZonas();
                frmZonas.Init(id, dgvZonas.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmZonas, clsInfo.conexaosqldados);
            }

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            Br.Imprimir(clsInfo.caminhorelatorios, "Cadastro de Zonas/Regiões/Rotas", clsInfo.conexaosqldados);
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            Filtrar();
        }
        void Filtrar()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvZonas);
        }

        private void rbnAtivo_Click(object sender, EventArgs e)
        {
            bwrZonas_Run();
        }

        private void dgvZonas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspEscolher.PerformClick();
        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvZonas.CurrentRow != null)
            {
                clsInfo.zrow = dgvZonas.CurrentRow;
            }
            this.Close();
            this.Dispose();
        }
    }
}
