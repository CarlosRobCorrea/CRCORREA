using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmDocFiscalVis : Form
    {
        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        BackgroundWorker bwrDocFiscal;
        Boolean carregandoDocFiscal;
        DataTable dtDocFiscal;

        clsDocFiscalInfo clsDocFiscalInfo = new clsDocFiscalInfo();
        clsDocFiscalBLL clsDocFiscalBLL = new clsDocFiscalBLL();


        public frmDocFiscalVis()
        {
            InitializeComponent();
        }
        public void Init(Int32 _id)
        {
            id = _id;
            bwrDocFiscal = new BackgroundWorker();
            bwrDocFiscal.WorkerSupportsCancellation = true;
            bwrDocFiscal.DoWork += new DoWorkEventHandler(bwrDocFiscal_DoWork);
            bwrDocFiscal.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrDocFiscal_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            carregandoDocFiscal = false;
            clsDocFiscalBLL = new clsDocFiscalBLL();

        }
        private void frmDocFiscalVis_Load(object sender, EventArgs e)
        {

        }
        private void frmDocFiscalVis_Activated(object sender, EventArgs e)
        {
            bwrDocFiscal_Run();
        }
        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmDocFiscal frmDocFiscal = new frmDocFiscal();
            frmDocFiscal.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscal, clsInfo.conexaosqldados);

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if ((Int32)dgvDocFiscal.CurrentRow.Cells[0].Value > 0)
            {
                id = clsParser.Int32Parse(dgvDocFiscal.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvDocFiscal.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvDocFiscal.Rows[dgvDocFiscal.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvDocFiscal.CurrentRow.Index < dgvDocFiscal.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvDocFiscal.Rows[dgvDocFiscal.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
                frmDocFiscal frmDocFiscal = new frmDocFiscal();
                frmDocFiscal.Init((Int32)dgvDocFiscal.CurrentRow.Cells[0].Value);
                clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscal, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDocFiscal.Select();
            }

        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)dgvDocFiscal.CurrentRow.Cells[0].Value > 0)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: " + dgvDocFiscal.CurrentRow.Cells[2].Value + "?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsDocFiscalBLL.Excluir((Int32)dgvDocFiscal.CurrentRow.Cells[0].Value, clsInfo.conexaosqldados);
                        id = clsGridHelper.NextIndex((Int32)dgvDocFiscal.CurrentRow.Index, dgvDocFiscal);
                        bwrDocFiscal.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDocFiscal.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvDocFiscal.CurrentRow != null)
                clsInfo.zrow = dgvDocFiscal.CurrentRow;

            this.Close();
            this.Dispose();

        }
        private void tspImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Impressão Desativada");
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bwrDocFiscal_Run()
        {
            if (carregandoDocFiscal == false)
            {
                carregandoDocFiscal = true;
                bwrDocFiscal.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrDocFiscal_DoWork(object sender, DoWorkEventArgs e)
        {
            DocFiscalGrid();

            if (bwrDocFiscal.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        private void DocFiscalGrid()
        {
            try
            {
                String Ativo = "";
                if (rbnStatusA.Checked == true)
                {
                    Ativo = "S";
                }
                else if (rbnStatusF.Checked == true)
                {
                    Ativo = "N";
                }

                dtDocFiscal = clsDocFiscalBLL.GridDados(Ativo, clsInfo.conexaosqldados);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // return null;
            }
        }

        private void bwrDocFiscal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    clsDocFiscalBLL.GridMonta(dgvDocFiscal, dtDocFiscal, id);
                    PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid

                }
                carregandoDocFiscal = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                carregandoDocFiscal = false;
            }
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvDocFiscal);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvDocFiscal, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvDocFiscal, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvDocFiscal, "codigo") == false)
                        {
                            if (dgvDocFiscal.Rows.Count > 0)
                            {
                                dgvDocFiscal.CurrentCell = dgvDocFiscal.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvDocFiscal.Rows.Count > 0)
            {
                dgvDocFiscal.CurrentCell = dgvDocFiscal.Rows[0].Cells["codigo"];
            }

            if (dgvDocFiscal.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvDocFiscal.CurrentRow.Cells["id"].Value.ToString());
                if (dgvDocFiscal.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvDocFiscal.Rows[
           dgvDocFiscal.CurrentRow.Index - 1].Cells["id"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvDocFiscal.CurrentRow.Index < dgvDocFiscal.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvDocFiscal.Rows[
         dgvDocFiscal.CurrentRow.Index + 1].Cells["id"].Value.ToString());
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
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
//            TrataCampos((Control)sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void rbnStatusT_Click(object sender, EventArgs e)
        {
            bwrDocFiscal_Run();
        }

        private void rbnStatusA_Click(object sender, EventArgs e)
        {
            bwrDocFiscal_Run();
        }

        private void rbnStatusF_Click(object sender, EventArgs e)
        {
            bwrDocFiscal_Run();
        }

        private void dgvDocFiscal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

    }
}
