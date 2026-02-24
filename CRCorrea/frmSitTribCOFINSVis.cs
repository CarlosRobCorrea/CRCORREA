using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    
    public partial class frmSitTribCOFINSVis : Form
    {
        clsBasicReport clsBR;

        Int32 indexSitTribCOFINS;

        DataTable dtSitTribCOFINS;

        public static Int32 indexSitTribCOFINSGeral;
        public static DataTable dtSitTribCOFINSGeral;       

        DataGridView dgvSitTribCOFINS_compl = new DataGridView();                        

        
        
        

        Int32 iddoc;

        public frmSitTribCOFINSVis()
        {
            InitializeComponent();
        }

        public void Init(Int32 _iddoc)
        {
            iddoc = _iddoc;
            clsBR = new clsBasicReport(this, dgvSitTribCOFINS_fil, ttpSitTribCOFINS, gbxSitTribCOFINS);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSitTribCOFINSVis_Load(object sender, EventArgs e)
        {
            if (iddoc > 0)
            {
                indexSitTribCOFINSGeral = iddoc;
            }
            else
            {
                indexSitTribCOFINSGeral = 0;
            }
        }

        private void frmSitTribCOFINSVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmSitTribCOFINSVis_Activated(object sender, EventArgs e)
        {           
            bwrSitTribCOFINS_RunWorkerAsync();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmSitTribCOFINS frmSitTribCOFINS = new frmSitTribCOFINS();
            frmSitTribCOFINS.Init(0, dgvSitTribCOFINS_fil, dgvSitTribCOFINS_compl);

            clsFormHelper.AbrirForm(this.MdiParent, frmSitTribCOFINS, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSitTribCOFINS_fil.CurrentRow != null)
            {
                indexSitTribCOFINS = clsParser.Int32Parse(dgvSitTribCOFINS_fil.CurrentRow.Cells["ID"].Value.ToString());
                frmSitTribCOFINS frmSitTribCOFINS = new frmSitTribCOFINS();
                frmSitTribCOFINS.Init(clsParser.Int32Parse(dgvSitTribCOFINS_fil.CurrentRow.Cells["ID"].Value.ToString()), dgvSitTribCOFINS_fil, dgvSitTribCOFINS_compl);

                clsFormHelper.AbrirForm(this.MdiParent, frmSitTribCOFINS, clsInfo.conexaosqldados);
            }
        }      

        private void bwrSitTribCOFINS_RunWorkerAsync()
        {
            pbxSitTribCOFINS.Visible = true;
            bwrSitTribCOFINS = new BackgroundWorker();
            bwrSitTribCOFINS.DoWork += new DoWorkEventHandler(bwrSitTribCOFINS_DoWork);
            bwrSitTribCOFINS.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSitTribCOFINS_RunWorkerCompleted);
            bwrSitTribCOFINS.RunWorkerAsync();
        }

        private void bwrSitTribCOFINS_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSitTribCOFINS = CarregaGridSitTribCOFINS();
        }

        public DataTable CarregaGridSitTribCOFINS()
        {
            try
            {                                 
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                  "SitTribCOFINS ",
                  "ID,CODIGO,NOME",
                  "", "ID"); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrSitTribCOFINS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvSitTribCOFINS_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvSitTribCOFINS_fil.DataSource = dtSitTribCOFINS;
                // dgvSitTribCOFINS_compl este é o grid completo utilizado nas atualizações gerais
                dgvSitTribCOFINS_compl.DataSource = dtSitTribCOFINS;
                dgvSitTribCOFINS_fil.Sort(dgvSitTribCOFINS_fil.Columns["CODIGO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvSitTribCOFINS_fil, 8);
                if (indexSitTribCOFINSGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(indexSitTribCOFINSGeral,  dgvSitTribCOFINS_fil, "CODIGO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexSitTribCOFINS,  dgvSitTribCOFINS_fil, "CODIGO");
                }
             

                if (dgvSitTribCOFINS_fil.CurrentRow != null)
                {
                    indexSitTribCOFINS = clsParser.Int32Parse(dgvSitTribCOFINS_fil.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexSitTribCOFINS = 0;
                }

                pbxSitTribCOFINS.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvSitTribCOFINS_fil);
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
                if (dgvSitTribCOFINS_fil.CurrentRow != null)
                { 
                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                            dgvSitTribCOFINS_fil.Columns["CODIGO"].HeaderText + " " + dgvSitTribCOFINS_fil.CurrentRow.Cells["CODIGO"].Value, "Aplisoft",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        using (TransactionScope tsSitTribCOFINS = new TransactionScope())
                        {
                            //EXCLUINDO  
                            clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "SitTribCOFINS", " ID = " + dgvSitTribCOFINS_fil.CurrentRow.Cells["ID"].Value.ToString());

                            dgvSitTribCOFINS_fil.Rows.Remove(dgvSitTribCOFINS_fil.CurrentRow);

                            tsSitTribCOFINS.Complete();
                            tsSitTribCOFINS.Dispose();
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
                clsInfo.zrow = dgvSitTribCOFINS_fil.CurrentRow;
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

        private void dgvSitTribCOFINS_fil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Situação Tributaria COFINS", clsInfo.conexaosqldados);
        }
        
    }
}
