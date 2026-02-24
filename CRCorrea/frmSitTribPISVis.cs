using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    
    public partial class frmSitTribPISVis: Form
    {
        Int32 indexSitTribPIS;

        DataTable dtSitTribPIS;

        public static Int32 indexSitTribPISGeral;
        public static DataTable dtSitTribPISGeral;
       
        DataGridView dgvSitTribPIS_compl = new DataGridView();                    

        
        
        

        Int32 iddoc;

        clsBasicReport clsBR;

        public frmSitTribPISVis()
        {
            InitializeComponent();
        }

        public void Init(Int32 _iddoc)
        {
            iddoc = _iddoc;
            clsBR = new clsBasicReport(this, dgvSitTribPIS_fil, ttpSitTribPIS, gbxSitTribPISVis);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSitTribPISVis_Load(object sender, EventArgs e)
        {
            if (iddoc > 0)
            {
                indexSitTribPISGeral = iddoc;
            }
            else
            {
                indexSitTribPISGeral = 0;
            }
        }

        private void frmSitTribPISVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmSitTribPISVis_Activated(object sender, EventArgs e)
        {           
            bwrSitTribPIS_RunWorkerAsync();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmSitTribPIS frmSitTribPIS = new frmSitTribPIS();
            frmSitTribPIS.Init(0, dgvSitTribPIS_fil, dgvSitTribPIS_compl);

            clsFormHelper.AbrirForm(this.MdiParent, frmSitTribPIS,clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSitTribPIS_fil.CurrentRow != null)
            {
                indexSitTribPIS = clsParser.Int32Parse(dgvSitTribPIS_fil.CurrentRow.Cells["ID"].Value.ToString());
                frmSitTribPIS frmSitTribPIS = new frmSitTribPIS();
                frmSitTribPIS.Init(clsParser.Int32Parse(dgvSitTribPIS_fil.CurrentRow.Cells["ID"].Value.ToString()), dgvSitTribPIS_fil, dgvSitTribPIS_compl);

                clsFormHelper.AbrirForm(this.MdiParent, frmSitTribPIS, clsInfo.conexaosqldados);
            }
        }
      

        private void bwrSitTribPIS_RunWorkerAsync()
        {
            pbxSitTribPISVis.Visible = true;
            bwrSitTribPIS = new BackgroundWorker();
            bwrSitTribPIS.DoWork += new DoWorkEventHandler(bwrSitTribPIS_DoWork);
            bwrSitTribPIS.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSitTribPIS_RunWorkerCompleted);
            bwrSitTribPIS.RunWorkerAsync();
        }

        private void bwrSitTribPIS_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSitTribPIS = CarregaGridSitTribPIS();
        }

        public DataTable CarregaGridSitTribPIS()
        {
            try
            {                                 
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                  "SitTribPIS ",
                  "ID,CODIGO,NOME",
                  "", "ID"); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void bwrSitTribPIS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvSitTribPIS_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvSitTribPIS_fil.DataSource = dtSitTribPIS;
                // dgvSitTribPIS_compl este é o grid completo utilizado nas atualizações gerais
                dgvSitTribPIS_compl.DataSource = dtSitTribPIS;
                dgvSitTribPIS_fil.Sort(dgvSitTribPIS_fil.Columns["CODIGO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvSitTribPIS_fil, 8);
                if (indexSitTribPISGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(indexSitTribPISGeral,  dgvSitTribPIS_fil, "CODIGO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexSitTribPIS,  dgvSitTribPIS_fil, "CODIGO");
                }
             

                if (dgvSitTribPIS_fil.CurrentRow != null)
                {
                    indexSitTribPIS = clsParser.Int32Parse(dgvSitTribPIS_fil.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexSitTribPIS = 0;
                }

                pbxSitTribPISVis.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvSitTribPIS_fil);
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
        
        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSitTribPIS_fil.CurrentRow != null)
                { 
                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                            dgvSitTribPIS_fil.Columns["CODIGO"].HeaderText + " " + dgvSitTribPIS_fil.CurrentRow.Cells["CODIGO"].Value, "Aplisoft",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        using (TransactionScope tsSitTribPIS = new TransactionScope())
                        {
                            //EXCLUINDO  
                            clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "SitTribPIS", " ID = " + dgvSitTribPIS_fil.CurrentRow.Cells["ID"].Value.ToString());

                            dgvSitTribPIS_fil.Rows.Remove(dgvSitTribPIS_fil.CurrentRow);

                            tsSitTribPIS.Complete();
                            tsSitTribPIS.Dispose();
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
                clsInfo.zrow = dgvSitTribPIS_fil.CurrentRow;
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

        private void dgvSitTribPIS_fil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Situação Tributaria PIS", clsInfo.conexaosqldados);
        }
        
    }
}
