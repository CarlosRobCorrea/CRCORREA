using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    
    public partial class frmDizeresNFVVis : Form
    {
        Int32 indexDizeresNFV;

        DataTable dtDizeresNFV;

        public static Int32 indexDizeresNFVGeral;
        public static DataTable dtDizeresNFVGeral;       

        DataGridView dgvDizeresNFV_compl = new DataGridView();                        

        
        
        
        clsBasicReport clsBr;

        Int32 iddoc;

        public frmDizeresNFVVis()
        {
            InitializeComponent();
        }

        public void Init(Int32 _iddoc)
        {
            iddoc = _iddoc;

            clsBr = new clsBasicReport(this, dgvDizeresNFV_fil, ttpDizeresNFV, gbxDizeresNFV);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDizeresNFVVis_Load(object sender, EventArgs e)
        {
            if (iddoc > 0)
            {
                indexDizeresNFVGeral = iddoc;
            }
            else
            {
                indexDizeresNFVGeral = 0;
            }
        }

        private void frmDizeresNFVVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmDizeresNFVVis_Activated(object sender, EventArgs e)
        {           
            bwrDizeresNFV_RunWorkerAsync();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmDizeresNFV frmDizeresNFV = new frmDizeresNFV();
            frmDizeresNFV.Init(0, dgvDizeresNFV_fil, dgvDizeresNFV_compl);

            clsFormHelper.AbrirForm(this.MdiParent, frmDizeresNFV, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvDizeresNFV_fil.CurrentRow != null)
            {
                indexDizeresNFV= clsParser.Int32Parse(dgvDizeresNFV_fil.CurrentRow.Cells["ID"].Value.ToString());
                frmDizeresNFV frmDizeresNFV = new frmDizeresNFV();
                frmDizeresNFV.Init(clsParser.Int32Parse(dgvDizeresNFV_fil.CurrentRow.Cells["ID"].Value.ToString()), dgvDizeresNFV_fil, dgvDizeresNFV_compl);

                clsFormHelper.AbrirForm(this.MdiParent, frmDizeresNFV, clsInfo.conexaosqldados);
            }
        }
        private void rbnFiltro(object sender, EventArgs e)
        {
            bwrDizeresNFV_RunWorkerAsync();
        }

        private void bwrDizeresNFV_RunWorkerAsync()
        {
            pbxDizeresNFV.Visible = true;
            bwrDizeresNFV = new BackgroundWorker();
            bwrDizeresNFV.DoWork += new DoWorkEventHandler(bwrDizeresNFV_DoWork);
            bwrDizeresNFV.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrDizeresNFV_RunWorkerCompleted);
            bwrDizeresNFV.RunWorkerAsync();
        }

        private void bwrDizeresNFV_DoWork(object sender, DoWorkEventArgs e)
        {
            dtDizeresNFV = CarregaGridDizeresNFV();
        }

        public DataTable CarregaGridDizeresNFV()
        {
            try
            {                                  
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                  "DizeresNFV ",
                  "ID,CODIGO,NOME",
                  "", "ID");    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrDizeresNFV_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvDizeresNFV_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvDizeresNFV_fil.DataSource = dtDizeresNFV;
                // dgvDizeresNFV_compl este é o grid completo utilizado nas atualizações gerais
                dgvDizeresNFV_compl.DataSource = dtDizeresNFV;
                dgvDizeresNFV_fil.Sort(dgvDizeresNFV_fil.Columns["CODIGO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvDizeresNFV_fil, 8);
                if (indexDizeresNFVGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(indexDizeresNFVGeral,  dgvDizeresNFV_fil, "CODIGO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexDizeresNFV,  dgvDizeresNFV_fil, "CODIGO");
                }
             

                if (dgvDizeresNFV_fil.CurrentRow != null)
                {
                    indexDizeresNFV = clsParser.Int32Parse(dgvDizeresNFV_fil.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexDizeresNFV = 0;
                }

                pbxDizeresNFV.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvDizeresNFV_fil);
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
        
        private void dgvDizeresNFV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDizeresNFV_fil.CurrentRow != null)
                { 
                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                            dgvDizeresNFV_fil.Columns["CODIGO"].HeaderText + " " + dgvDizeresNFV_fil.CurrentRow.Cells["CODIGO"].Value, "Aplisoft",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        using (TransactionScope tsDizeresNFV = new TransactionScope())
                        {
                            //EXCLUINDO  
                            clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "DizeresNFV", " ID = " + dgvDizeresNFV_fil.CurrentRow.Cells["ID"].Value.ToString());

                            dgvDizeresNFV_fil.Rows.Remove(dgvDizeresNFV_fil.CurrentRow);

                            tsDizeresNFV.Complete();
                            tsDizeresNFV.Dispose();
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
                clsInfo.zrow = dgvDizeresNFV_fil.CurrentRow;
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

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Dizeres NF", clsInfo.conexaosqldados);
        }
    }
}
