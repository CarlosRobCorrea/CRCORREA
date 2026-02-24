using CRCorreaBLL;
using CRCorreaFuncoes;

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    
    public partial class frmEstadosVis : Form
    {
        Int32 indexESTADOS;

        DataTable dtESTADOS;

        public static Int32 indexESTADOSGeral;
        public static DataTable dtESTADOSGeral;

        public static Int32 indexESTADOSICMSGeral;
        public static DataTable dtESTADOSICMSGeral;
        public static DataRow drESTADOSICMSGeral;
        public static ArrayList deleESTADOSICMSGeral = new ArrayList();
        public static Boolean achouESTADOSICMSGeral;

        DataGridView dgvESTADOS_compl = new DataGridView();                        

        clsBasicReport clsBR;
        
        public frmEstadosVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBR = new clsBasicReport(this, dgvESTADOS_fil, ttpESTADOS, gbxESTADOS);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEstadosVis_Load(object sender, EventArgs e)
        {
            indexESTADOSGeral = 0;
        }

        private void frmEstadosVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmEstadosVis_Activated(object sender, EventArgs e)
        {
            dtESTADOSICMSGeral = null;
            drESTADOSICMSGeral = null;
            deleESTADOSICMSGeral.Clear();
            achouESTADOSICMSGeral = false;
            bwrESTADOS_RunWorkerAsync();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvESTADOS_fil.CurrentRow != null)
            {
                indexESTADOSICMSGeral = clsParser.Int32Parse(dgvESTADOS_fil.CurrentRow.Cells["ID"].Value.ToString());
            }
            frmEstados frmEstados_1 = new frmEstados();
            frmEstados_1.Init(0, dgvESTADOS_fil, dgvESTADOS_compl);

            clsFormHelper.AbrirForm(this.MdiParent, frmEstados_1, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvESTADOS_fil.CurrentRow != null)
            {
                indexESTADOS = clsParser.Int32Parse(dgvESTADOS_fil.CurrentRow.Cells["ID"].Value.ToString());
                frmEstados frmEstados_1 = new frmEstados();
                frmEstados_1.Init(clsParser.Int32Parse(dgvESTADOS_fil.CurrentRow.Cells["ID"].Value.ToString()), dgvESTADOS_fil, dgvESTADOS_compl);
                clsFormHelper.AbrirForm(this.MdiParent, frmEstados_1, clsInfo.conexaosqldados);
            }
        }

        private void bwrESTADOS_RunWorkerAsync()
        {
            pbxTab_ESTADOS.Visible = true;
            bwrESTADOS = new BackgroundWorker();
            bwrESTADOS.DoWork += new DoWorkEventHandler(bwrESTADOS_DoWork);
            bwrESTADOS.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrESTADOS_RunWorkerCompleted);
            bwrESTADOS.RunWorkerAsync();
        }

        private void bwrESTADOS_DoWork(object sender, DoWorkEventArgs e)
        {
            dtESTADOS = new DataTable();
            dtESTADOS = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
                           "select ID,ESTADO,ZONAFRANCA,ALIQUOTA,NOMEEXT,CAPITAL,INICEP,FIMCEP,REGIAO,IBGE, IEST " +
                           "FROM ESTADOS " +
                           "ORDER BY ESTADO");

        }

        //public DataTable CarregaGridESTADOS()
        //{
        //    try
        //    {
        //        return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
        //               "ESTADOS ",
        //               "ID,ESTADO,ZONAFRANCA,ALIQUOTA,NOMEEXT,CAPITAL,INICEP,FIMCEP,REGIAO,IBGE, IEST",
        //               "", "ID");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}

        private void bwrESTADOS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvESTADOS_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvESTADOS_fil.DataSource = dtESTADOS;
                // dgvESTADOS_compl este é o grid completo utilizado nas atualizações gerais
                dgvESTADOS_compl.DataSource = dtESTADOS;
                dgvESTADOS_fil.Sort(dgvESTADOS_fil.Columns["ESTADO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvESTADOS_fil, 8);
                if (indexESTADOSGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(indexESTADOSGeral,  dgvESTADOS_fil, "ESTADO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexESTADOS,  dgvESTADOS_fil, "ESTADO");
                }
             

                if (dgvESTADOS_fil.CurrentRow != null)
                {
                    indexESTADOS = clsParser.Int32Parse(dgvESTADOS_fil.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexESTADOS = 0;
                }

                pbxTab_ESTADOS.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvESTADOS_fil);
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
        
        private void dgvESTADOS_fil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Não pode Excluir Estado");
            //try
            //{
            //    if (dgvESTADOS_fil.CurrentRow != null)
            //    { 
            //        if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
            //                dgvESTADOS_fil.Columns["ESTADO"].HeaderText + " " + dgvESTADOS_fil.CurrentRow.Cells["ESTADO"].Value, "Aplisoft",
            //                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            //                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //        {
            //            using (TransactionScope tsCNAE = new TransactionScope())
            //            {
            //                //EXCLUINDO                           
            //                clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "ESTADOSICMS", " IDESTADO = " + dgvESTADOS_fil.CurrentRow.Cells["ID"].Value.ToString());
            //                clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "ESTADOS", " ID = " + dgvESTADOS_fil.CurrentRow.Cells["ID"].Value.ToString());

            //                dgvESTADOS_fil.Rows.Remove(dgvESTADOS_fil.CurrentRow);

            //                tsCNAE.Complete();
            //                tsCNAE.Dispose();
            //            }  


            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK,
            //MessageBoxIcon.Warning);
            //}
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Estados", clsInfo.conexaosqldados);
        }
        
    }
}
