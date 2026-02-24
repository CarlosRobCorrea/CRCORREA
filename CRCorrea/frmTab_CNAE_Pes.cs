using CRCorreaFuncoes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmTab_CNAE_Pes : Form
    {
        clsBasicReport clsBR;

        Int32 indexTab_CNAE;

        DataTable dtTab_CNAE;

        public static Int32 indexTab_CNAEGeral;
        public static DataTable dtTab_CNAEGeral;

        public static Int32 indexTab_CNAE_UFGeral;
        public static DataTable dtTab_CNAE_UFGeral;
        public static DataRow drTab_CNAE_UFGeral;
        public static ArrayList deleTab_CNAE_UFGeral = new ArrayList();
        public static Boolean achouTab_CNAE_UFGeral;

        DataGridView dgvTab_CNAE_compl = new DataGridView();                       

        
        
                
            
        public frmTab_CNAE_Pes()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBR = new clsBasicReport(this, dgvTab_CNAE_fil, ttpCNAE, gbxCNAE);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCNAEVis_Load(object sender, EventArgs e)
        {
            indexTab_CNAEGeral = 0;
        }

        private void frmCNAEVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmCNAEVis_Activated(object sender, EventArgs e)
        {
            dtTab_CNAE_UFGeral = null;
            drTab_CNAE_UFGeral = null;
            deleTab_CNAE_UFGeral.Clear();
            achouTab_CNAE_UFGeral = false;
            bwrTab_CNAE_RunWorkerAsync();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvTab_CNAE_fil.CurrentRow != null)
            {
                indexTab_CNAE_UFGeral = clsParser.Int32Parse(dgvTab_CNAE_fil.CurrentRow.Cells["ID"].Value.ToString());
            }
            frmTab_CNAE frmTab_CNAE = new frmTab_CNAE();
            frmTab_CNAE.Init(0,  dgvTab_CNAE_fil, dgvTab_CNAE_compl);

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_CNAE, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvTab_CNAE_fil.CurrentRow != null)
            {
                indexTab_CNAE = clsParser.Int32Parse(dgvTab_CNAE_fil.CurrentRow.Cells["ID"].Value.ToString());
                frmTab_CNAE frmTab_CNAE = new frmTab_CNAE();
                frmTab_CNAE.Init(clsParser.Int32Parse(dgvTab_CNAE_fil.CurrentRow.Cells["ID"].Value.ToString()),  dgvTab_CNAE_fil, dgvTab_CNAE_compl);

                clsFormHelper.AbrirForm(this.MdiParent, frmTab_CNAE, clsInfo.conexaosqldados);
            }
        }

        private void bwrTab_CNAE_RunWorkerAsync()
        {
            pbxTab_CNAE.Visible = true;
            bwrTab_CNAE = new BackgroundWorker();
            bwrTab_CNAE.DoWork += new DoWorkEventHandler(bwrTab_CNAE_DoWork);
            bwrTab_CNAE.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_CNAE_RunWorkerCompleted);
            bwrTab_CNAE.RunWorkerAsync();
        }

        private void bwrTab_CNAE_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_CNAE = CarregaGridTab_CNAE();
        }

        public DataTable CarregaGridTab_CNAE()
        {
            try
            {
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                       "TAB_CNAE ",
                       "ID,CODIGO,NOME",
                       "", "ID");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrTab_CNAE_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvTab_CNAE_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvTab_CNAE_fil.DataSource = dtTab_CNAE;
                // dgvTab_CNAE_compl este é o grid completo utilizado nas atualizações gerais
                dgvTab_CNAE_compl.DataSource = dtTab_CNAE;
                dgvTab_CNAE_fil.Sort(dgvTab_CNAE_fil.Columns["CODIGO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvTab_CNAE_fil, 8);
                if (indexTab_CNAEGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(indexTab_CNAEGeral,  dgvTab_CNAE_fil, "CODIGO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexTab_CNAE,  dgvTab_CNAE_fil, "CODIGO");
                }
             

                if (dgvTab_CNAE_fil.CurrentRow != null)
                {
                    indexTab_CNAE = clsParser.Int32Parse(dgvTab_CNAE_fil.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexTab_CNAE = 0;
                }

                pbxTab_CNAE.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvTab_CNAE_fil);
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
        
        private void dgvTab_CNAE_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvTab_CNAE_fil.CurrentRow;
            this.Close();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "CNAE", clsInfo.conexaosqldados);
        }
        
    }
}
