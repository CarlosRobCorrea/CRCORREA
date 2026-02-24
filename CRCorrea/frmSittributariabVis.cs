using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSittributariabVis : Form
    {
        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;
        BackgroundWorker bwrSittributariaB;

        private DataTable dtSittributariaBForm;
        
        
        private clsBasicReport clsBRSittributariaBForm;
        private clsSittributariabBLL clsSittributariabBLL;

        public frmSittributariabVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            
            
            clsBRSittributariaBForm = new clsBasicReport(this, dgvSittributariaB, toolTip);
            clsSittributariabBLL = new clsSittributariabBLL();


            bwrSittributariaB = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrSittributariaB.WorkerSupportsCancellation = true;
            bwrSittributariaB.DoWork += new DoWorkEventHandler(bwrSittributariaB_DoWork);
            bwrSittributariaB.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSittributariaB_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
        }

        private void frmSittributariabVis_Load(object sender, EventArgs e)
        {

        }

        void bwrSittributariaB_Run()
        {
            {
                pbxSittributariaB.Visible = true;
                bwrSittributariaB.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrSittributariaB_DoWork(object sender, DoWorkEventArgs e)
        {
            //Primeiro puchamos os dados assim se houver algum cancelamento logo a abaixo sera verificado         
            SittributariaBGrid();
            //Depois verificamos se houve pedido de cancelamento para o BackgroundWorker
            //OBS: para operaçoes em loop o cancellationPending deve ser verificado dentro do loop
            if (bwrSittributariaB.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void SittributariaBGrid()
        {
            dtSittributariaBForm = clsSittributariabBLL.GridDados("", "", clsInfo.conexaosqldados);
        }

        private void bwrSittributariaB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    clsSittributariabBLL.GridMonta(dgvSittributariaB, dtSittributariaBForm, id);

                    pbxSittributariaB.Visible = false;
                    PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvSittributariaB.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                       dgvSittributariaB.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
                id = 0;
            }
            id_Proximo = 0;
            id_Anterior = 0;

            frmSittributariab frmSittributariab = new frmSittributariab();
            frmSittributariab.Init(0, dgvSittributariaB.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmSittributariab, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSittributariaB.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvSittributariaB.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvSittributariaB.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvSittributariaB.Rows[dgvSittributariaB.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvSittributariaB.CurrentRow.Index < dgvSittributariaB.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvSittributariaB.Rows[dgvSittributariaB.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }

                frmSittributariab frmSittributariab = new frmSittributariab();
                frmSittributariab.Init(id, dgvSittributariaB.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmSittributariab, clsInfo.conexaosqldados);
            }
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSittributariaB.CurrentRow != null)
                {
                    id = clsParser.Int32Parse(dgvSittributariaB.CurrentRow.Cells["ID"].Value.ToString());

                    if (dgvSittributariaB.CurrentRow.Index > 0)
                    {
                        id_Anterior = clsParser.Int32Parse(dgvSittributariaB.Rows[dgvSittributariaB.CurrentRow.Index -
                                       1].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Anterior = 0;
                    }

                    if (dgvSittributariaB.CurrentRow.Index < dgvSittributariaB.Rows.Count - 1)
                    {
                        id_Proximo = clsParser.Int32Parse(dgvSittributariaB.Rows[dgvSittributariaB.CurrentRow.Index + 1
                                  ].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Proximo = 0;
                    }

                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " + dgvSittributariaB.Columns[2].HeaderText + " " + dgvSittributariaB.CurrentRow.Cells[2].Value, Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsSittributariabBLL.Excluir(Int32.Parse(dgvSittributariaB.CurrentRow.Cells["id"].Value.ToString()), clsInfo.conexaosqldados);
                        dgvSittributariaB.Rows.Remove(dgvSittributariaB.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void frmSittributariabVis_Activated(object sender, EventArgs e)
        {
            bwrSittributariaB_Run();
        }

        private void dgvSittributariaB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }

        private void dgvSittributariaB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspSittributariaBolher_Click(object sender, EventArgs e)
        {
            if (dgvSittributariaB.CurrentRow != null)
                clsInfo.zrow = dgvSittributariaB.CurrentRow;
            this.Close();
            this.Dispose();
        }

        private void tspRetornar_Click_(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRSittributariaBForm.Imprimir(clsInfo.caminhorelatorios, string.Empty, clsInfo.conexaosqldados);
        }

        private void frmSittributariabVis_Shown(object sender, EventArgs e)
        {

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

        private void tstPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tstPesquisa.Focus();
        }

        private void tstPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstPesquisa.Focus();
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvSittributariaB);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvSittributariaB, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvSittributariaB, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvSittributariaB, "codigo") == false)
                        {
                            if (dgvSittributariaB.Rows.Count > 0)
                            {
                                dgvSittributariaB.CurrentCell = dgvSittributariaB.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvSittributariaB.Rows.Count > 0)
            {
                dgvSittributariaB.CurrentCell = dgvSittributariaB.Rows[0].Cells["codigo"];
            }

            if (dgvSittributariaB.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvSittributariaB.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvSittributariaB.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvSittributariaB.Rows[
           dgvSittributariaB.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvSittributariaB.CurrentRow.Index < dgvSittributariaB.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvSittributariaB.Rows[
         dgvSittributariaB.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            bwrSittributariaB.CancelAsync();
            bwrSittributariaB.Dispose();
            this.Close();
            this.Dispose();
        }
    }
}

















