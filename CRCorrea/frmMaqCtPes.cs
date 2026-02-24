using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmMaqCtPes : Form
    {
        clsMaqctBLL clsMaqctBLL;
        clsMaqctprecoBLL clsMaqctPrecoBLL;

        DataTable dtMaqct;
        GridColuna[] dtMaqctColunas;

        DataTable dtMaqctPreco;
        GridColuna[] dtMaqctPrecoColunas;

        clsBasicReport BR;
        
        

        String filtro_ativo;

        Boolean carregando;
        Boolean carregandoPrimeiro;
        Boolean carregaActivated;

        BackgroundWorker bwrMaqct;
        BackgroundWorker bwrMaqctPreco;

        Int32 maqct_id;

        public frmMaqCtPes()
        {
            InitializeComponent();
        }

        public void Init()
        {
            BR = new clsBasicReport(this, dgvMaqCt, ToolTip);
            
            

            dtMaqctColunas = new GridColuna[]
            {
                new GridColuna("Código", "CODIGO", 120, true,DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Nome", "NOME", 180, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("A.", "ATIVO", 35, true, DataGridViewContentAlignment.MiddleCenter)
            };

            dtMaqctPrecoColunas = new GridColuna[]
            {
                new GridColuna("Data", "DATA", 90, true,DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Valor", "VALOR", 90, true, DataGridViewContentAlignment.MiddleRight)
            };

            carregando = false;
            carregandoPrimeiro = false;
            carregaActivated = true;

            clsMaqctBLL = new clsMaqctBLL();
            clsMaqctPrecoBLL = new clsMaqctprecoBLL();
        }

        private void frmMaqCtPes_Load(object sender, EventArgs e)
        {
            rbnAtivo.Checked = true;
        }

        private void FillFiltros()
        {
            if (rbnAtivo.Checked == true) filtro_ativo = "T";
            else if (rbnAtivoS.Checked == true) filtro_ativo = "S";
            else if (rbnAtivoN.Checked == true) filtro_ativo = "N";
            else filtro_ativo = "";
        }

        private bool FiltroMudancas()
        {
            if (filtro_ativo == "T" && rbnAtivo.Checked == false)
            {
                return true;
            }
            else if (filtro_ativo == "S" && rbnAtivoS.Checked == false)
            {
                return true;
            }
            else if (filtro_ativo == "N" && rbnAtivoN.Checked == false)
            {
                return true;
            }

            return false;
        }

        void Filtrar()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvMaqCt);
        }

        private void StatusCheckedChanged(object sender, EventArgs e)
        {
            bwrMaqct_Run();
        }

        private void bwrMaqct_Run()
        {
            if (carregando == false)
            {
                if (carregandoPrimeiro == true && carregaActivated == false)
                {
                    if (FiltroMudancas() == false)
                    {
                        return;
                    }
                }
                else
                {
                    carregandoPrimeiro = true;
                    carregaActivated = false;
                }

                carregando = true;

                FillFiltros();

                bwrMaqct = new BackgroundWorker();
                bwrMaqct.DoWork += new DoWorkEventHandler(bwrMaqct_DoWork);
                bwrMaqct.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrMaqct_RunWorkerCompleted);
                bwrMaqct.RunWorkerAsync();
            }
        }

        private void bwrMaqct_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MaqctGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MaqctGrid()
        {
            dtMaqct = clsMaqctBLL.CarregaGrid(clsInfo.conexaosqldados, filtro_ativo);
        }

        private void bwrMaqct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList altOrcamento = BR.GetColunas();
                dgvMaqCt.DataSource = dtMaqct;

                clsGridHelper.MontaGrid2(dgvMaqCt, dtMaqctColunas, true);

                BR.RecalculaGrid(altOrcamento);

                Filtrar();

                bwrMaqctPreco_Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregando = false;
            }
        }


        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvMaqCt.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvIndices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                //tspAlterar.PerformClick();
            }
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

        private void bwrMaqctPreco_Run()
        {
            dgvMaqCtPreco.DataSource = null;

            bwrMaqctPreco = new BackgroundWorker();
            bwrMaqctPreco.DoWork += new DoWorkEventHandler(bwrMaqctPreco_DoWork);
            bwrMaqctPreco.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrMaqctPreco_RunWorkerCompleted);
            bwrMaqctPreco.RunWorkerAsync();
        }

        void bwrMaqctPreco_DoWork(object sender, DoWorkEventArgs e)
        {
            dtMaqctPreco = new DataTable();

            dtMaqctPreco = clsMaqctPrecoBLL.CarregaGrid(clsInfo.conexaosqldados, maqct_id);
        }

        void bwrMaqctPreco_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvMaqCtPreco.DataSource = dtMaqctPreco;
            clsGridHelper.MontaGrid2(dgvMaqCtPreco, dtMaqctPrecoColunas, true);
        }

        private void dgvIndices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //tspAlterar.PerformClick();
        }

        private void frmMaqCtPes_Activated(object sender, EventArgs e)
        {
            bwrMaqct_Run();
        }
        
        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvMaqCt);
        }

        private void dgvMaqCt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMaqCt.CurrentRow != null)
            {
                maqct_id = clsParser.Int32Parse(dgvMaqCt.CurrentRow.Cells["id"].Value.ToString());
                bwrMaqctPreco_Run();
            }
        }

    }
}
