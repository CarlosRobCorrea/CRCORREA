using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    
    public partial class frmSitTribIPIVis : Form
    {
        clsBasicReport clsBR;

        Int32 indexSitTribIPI;

        DataTable dtSitTribIPI;

        public static Int32 indexSitTribIPIGeral;
        public static DataTable dtSitTribIPIGeral;
       
        DataGridView dgvSitTribIPI_compl = new DataGridView();
                        
        
        
        

        Int32 iddoc;

        public frmSitTribIPIVis()
        {
            InitializeComponent();
        }

        public void Init(Int32 _iddoc)
        {
            iddoc = _iddoc;
            clsBR = new clsBasicReport(this, dgvSitTribIPI_fil, ttpSitTribIPI, gbxSitTribIPI);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSitTribIPIVis_Load(object sender, EventArgs e)
        {
            if (iddoc > 0)
            {
                indexSitTribIPIGeral = iddoc;
            }
            else
            {
                indexSitTribIPIGeral = 0;
            }
        }

        private void frmSitTribIPIVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmSitTribIPIVis_Activated(object sender, EventArgs e)
        {           
            bwrSitTribIPI_RunWorkerAsync();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmSitTribIPI frmSitTribIPI = new frmSitTribIPI();
            frmSitTribIPI.Init(0, dgvSitTribIPI_fil, dgvSitTribIPI_compl);

            clsFormHelper.AbrirForm(this.MdiParent, frmSitTribIPI, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSitTribIPI_fil.CurrentRow != null)
            {
                indexSitTribIPI = clsParser.Int32Parse(dgvSitTribIPI_fil.CurrentRow.Cells["ID"].Value.ToString());
                frmSitTribIPI frmSitTribIPI = new frmSitTribIPI();
                frmSitTribIPI.Init(clsParser.Int32Parse(dgvSitTribIPI_fil.CurrentRow.Cells["ID"].Value.ToString()), dgvSitTribIPI_fil, dgvSitTribIPI_compl);

                clsFormHelper.AbrirForm(this.MdiParent, frmSitTribIPI, clsInfo.conexaosqldados);
            }
        }     

        private void bwrSitTribIPI_RunWorkerAsync()
        {
            pbxSitTribIPI.Visible = true;
            bwrSitTribIPI = new BackgroundWorker();
            bwrSitTribIPI.DoWork += new DoWorkEventHandler(bwrSitTribIPI_DoWork);
            bwrSitTribIPI.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSitTribIPI_RunWorkerCompleted);
            bwrSitTribIPI.RunWorkerAsync();
        }

        private void bwrSitTribIPI_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSitTribIPI = CarregaGridSitTribIPI();
        }

        public DataTable CarregaGridSitTribIPI()
        {
            try
            {                                 
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                  "SITTRIBIPI ",
                  "ID,CODIGO,NOME",
                  "", "ID"); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrSitTribIPI_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvSitTribIPI_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvSitTribIPI_fil.DataSource = dtSitTribIPI;
                // dgvSitTribIPI_compl este é o grid completo utilizado nas atualizações gerais
                dgvSitTribIPI_compl.DataSource = dtSitTribIPI;
                dgvSitTribIPI_fil.Sort(dgvSitTribIPI_fil.Columns["CODIGO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvSitTribIPI_fil, 8);
                if (indexSitTribIPIGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(indexSitTribIPIGeral,  dgvSitTribIPI_fil, "CODIGO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexSitTribIPI,  dgvSitTribIPI_fil, "CODIGO");
                }
             

                if (dgvSitTribIPI_fil.CurrentRow != null)
                {
                    indexSitTribIPI = clsParser.Int32Parse(dgvSitTribIPI_fil.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexSitTribIPI = 0;
                }

                pbxSitTribIPI.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvSitTribIPI_fil);
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
        
        private void dgvSitTribIPI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSitTribIPI_fil.CurrentRow != null)
                { 
                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                            dgvSitTribIPI_fil.Columns["CODIGO"].HeaderText + " " + dgvSitTribIPI_fil.CurrentRow.Cells["CODIGO"].Value, "Aplisoft",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        using (TransactionScope tsSitTribIPI = new TransactionScope())
                        {
                            //EXCLUINDO  
                            clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "SITTRIBIPI", " ID = " + dgvSitTribIPI_fil.CurrentRow.Cells["ID"].Value.ToString());

                            dgvSitTribIPI_fil.Rows.Remove(dgvSitTribIPI_fil.CurrentRow);

                            tsSitTribIPI.Complete();
                            tsSitTribIPI.Dispose();
                        }  


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
            }
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvSitTribIPI_fil.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Close();
                this.Dispose();
            }
        }

        private void dgvSitTribIPI_fil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Situação Tributaria IPI", clsInfo.conexaosqldados);
        }
        
    }
}
