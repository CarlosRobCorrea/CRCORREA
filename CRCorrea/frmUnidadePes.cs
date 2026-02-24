using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmUnidadePes : Form
    {
        clsBasicReport clsBR;
        
        Int32 id;

        DataTable dtUnidade;

        public static Int32 indexUnidadeGeral;
        public static DataTable dtUnidadeGeral;
       
        DataGridView dgvUnidade_compl = new DataGridView();
                        
        public frmUnidadePes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id)
        {
            
            //clsBR = new clsBasicReport(this, dgvUnidade_fil, ttpUnidade, gbxUnidade);

            id = _id;
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUnidadePes_Load(object sender, EventArgs e)
        {
            indexUnidadeGeral = 0;           
        }

        private void frmUnidadePes_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmUnidadePes_Activated(object sender, EventArgs e)
        {           
            bwrUnidade_RunWorkerAsync();
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
            String query = "";
            SqlDataAdapter sda;
            query = "Select ID,CODIGO,NOME,UNIDDEC FROM UNIDADE ORDER BY CODIGO ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dtUnidade = new DataTable();
            sda.Fill(dtUnidade);

        }

        private void bwrUnidade_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {   // dgvUnidade_fil este é o grid filtrado utilizado nos botões de proximo anterior primeiro e ultimo
                dgvUnidade_fil.DataSource = dtUnidade;
                // dgvUnidade_compl este é o grid completo utilizado nas atualizações gerais
                dgvUnidade_compl.DataSource = dtUnidade;
                dgvUnidade_fil.Sort(dgvUnidade_fil.Columns["CODIGO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid( dgvUnidade_fil, 8);

                clsGridHelper.SelecionaLinha(id,  dgvUnidade_fil, "CODIGO");

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
        

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvUnidade_fil.CurrentRow != null)
            {
                clsInfo.zrow = dgvUnidade_fil.CurrentRow;
            }
            this.Close();
            this.Dispose();
        }


        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "UnidadeVis", clsInfo.conexaosqldados);
        }

    }
}
