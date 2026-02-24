using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmUnidadeVis : Form
    {
        clsBasicReport clsBR;
        
        Int32 indexUnidade;

        DataTable dtUnidade;

        public static Int32 indexUnidadeGeral;
        public static DataTable dtUnidadeGeral;
       
        DataGridView dgvUnidade_compl = new DataGridView();
                        
        
        public frmUnidadeVis()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idUnidade)
        {
            indexUnidadeGeral = _idUnidade;
            clsBR = new clsBasicReport(this, dgvUnidade_fil, ttpUnidade, gbxUnidade);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUnidadeVis_Load(object sender, EventArgs e)
        {
                  
        }

        private void frmUnidadeVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmUnidadeVis_Activated(object sender, EventArgs e)
        {           
            bwrUnidade_RunWorkerAsync();
        }
        
        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvUnidade_fil.CurrentRow != null)
            {
                indexUnidade = clsParser.Int32Parse(dgvUnidade_fil.CurrentRow.Cells["ID"].Value.ToString());
                frmUnidade frmUnidade = new frmUnidade();
                frmUnidade.Init(clsParser.Int32Parse(dgvUnidade_fil.CurrentRow.Cells["ID"].Value.ToString()), dgvUnidade_fil, dgvUnidade_compl);

                clsFormHelper.AbrirForm(this.MdiParent, frmUnidade, clsInfo.conexaosqldados);
            }
        }

        
        private void bwrUnidade_RunWorkerAsync()
        {
            pbxUnidade.Visible = true;
            bwrUnidade = new BackgroundWorker();
            bwrUnidade.DoWork += new DoWorkEventHandler(bwrUnidade_DoWork);
            bwrUnidade.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrUnidade_RunWorkerCompleted);
            bwrUnidade.RunWorkerAsync();
        }

        private void bwrUnidade_DoWork(object sender, DoWorkEventArgs e)
        {
            dtUnidade = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
                            "select ID,CODIGO,NOME,UNIDDEC " +
                           "FROM UNIDADE " +
                           "ORDER BY ID");

            //CarregaGridUnidade();
        }

        //public DataTable CarregaGridUnidade()
        //{
        //    try
        //    {           
                
        //        //return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
        //        //  "Unidade ",
        //        //  "ID,CODIGO,NOME,UNIDDEC",
        //        //  "", "ID"); 
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}

        private void bwrUnidade_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvUnidade_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvUnidade_fil.DataSource = dtUnidade;
                // dgvUnidade_compl este é o grid completo utilizado nas atualizações gerais
                dgvUnidade_compl.DataSource = dtUnidade;
                dgvUnidade_fil.Sort(dgvUnidade_fil.Columns["CODIGO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvUnidade_fil, 8);
                if (indexUnidadeGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(indexUnidadeGeral,  dgvUnidade_fil, "CODIGO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexUnidade,  dgvUnidade_fil, "CODIGO");
                }
             

                if (dgvUnidade_fil.CurrentRow != null)
                {
                    indexUnidade = clsParser.Int32Parse(dgvUnidade_fil.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexUnidade = 0;
                }

                pbxUnidade.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvUnidade_fil);
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
                if (dgvUnidade_fil.CurrentRow != null)
                { 
                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                            dgvUnidade_fil.Columns["CODIGO"].HeaderText + " " + dgvUnidade_fil.CurrentRow.Cells["CODIGO"].Value, "Aplisoft",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        using (TransactionScope tsUnidade = new TransactionScope())
                        {
                            //EXCLUINDO  
                            clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "Unidade", " ID = " + dgvUnidade_fil.CurrentRow.Cells["ID"].Value.ToString());

                            dgvUnidade_fil.Rows.Remove(dgvUnidade_fil.CurrentRow);

                            tsUnidade.Complete();
                            tsUnidade.Dispose();
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
        

        private void dgvUnidade_fil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvUnidade_fil.CurrentRow != null)
            {
                clsInfo.zrow = dgvUnidade_fil.CurrentRow;
            }
            this.Close();
            this.Dispose();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmUnidade frmUnidade = new frmUnidade();
            frmUnidade.Init(0, dgvUnidade_fil, dgvUnidade_compl);

            clsFormHelper.AbrirForm(this.MdiParent, frmUnidade, clsInfo.conexaosqldados);
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "UnidadeVis", clsInfo.conexaosqldados);
        }
    }
}
