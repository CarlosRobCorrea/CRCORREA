using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{ 
    public partial class frmTab_BancosPes : Form
    {
        private String conexao;
        
        private DataTable dtTab_Bancos;
        private Int32 indexTab_BancosValor;

        private clsBasicReport clsBasicReport;

        public frmTab_BancosPes()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _idBanco)
        {
            indexTab_BancosValor = _idBanco;
            clsBasicReport = new clsBasicReport(this, dgvTab_Bancos,toolTip);

            conexao = _conexao;
        }

        private void frmTab_BancoPes_Load(object sender, EventArgs e)
        {
            bwrTab_Bancos.RunWorkerAsync();
        }

        private void bwrTab_Bancos_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_Bancos = CarregaGridTab_Bancos();
        }

        public DataTable CarregaGridTab_Bancos()
        {
            //clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();
            try
            {
/*                if (rbnTodos.Checked == true)
                    return clsTab_BancosBLL.CarregaGrid(conexao);
                else if (rbnAtivos.Checked == true)
                    return clsTab_BancosBLL.CarregaGrid2(conexao, "S");
                else
                    return clsTab_BancosBLL.CarregaGrid2(conexao, "N"); */
                String tabela = "TAB_BANCOS";
                String query = "ID, CODIGO, COGNOME, NOME, ATIVO, HOMEPAGE ";
                String ordem = " CODIGO ";
                String filtro = "";
                if (rbnTodos.Checked == true)
                {
                }
                else if (rbnAtivos.Checked == true)
                {
                    filtro = "ATIVO = 'S'";
                }
                else if (rbnInativos.Checked == true)
                {
                    filtro = "ATIVO = 'N'";
                }
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, tabela, query, filtro, ordem);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrTab_Bancos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArrayList dgvTab_BancosOrder = clsBasicReport.GetColunas();

            dgvTab_Bancos.DataSource = dtTab_Bancos;

            
            clsGridHelper.MontaGrid(dgvTab_Bancos,
                new String[] { "Id", "Codigo", "Cognome", "Nome", "Ativo", "Site" },
                new String[] { "ID", "CODIGO", "COGNOME", "NOME", "ATIVO", "HOMEPAGE"},
                new int[] { 1, 100, 120, 300, 50,200},
                new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft},
                new bool[] { false, true, true, true, true, true },
                true, 1, ListSortDirection.Ascending);

            if (dgvTab_BancosOrder != null && dgvTab_BancosOrder.Count > 0)
                clsBasicReport.RecalculaGrid(dgvTab_BancosOrder);

            clsGridHelper.SelecionaLinha(indexTab_BancosValor, dgvTab_Bancos, "CODIGO");

        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
        }
                

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBasicReport.Imprimir(clsInfo.caminhorelatorios, "Bancos (Febraban)", clsInfo.conexaosqldados);
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvTab_Bancos.CurrentRow;
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

        private void tspExcluir_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmTab_BancosPes_Activated(object sender, EventArgs e)
        {
            if (dtTab_Bancos != null && bwrTab_Bancos.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                {
                    indexTab_BancosValor = clsInfo.zidincluir;
                }
                bwrTab_Bancos.RunWorkerAsync();
            }
            else
            {
                ArrayList dgvTab_BancosOrder = clsBasicReport.GetColunas();
                if (dgvTab_BancosOrder != null && dgvTab_BancosOrder.Count > 0)
                    clsBasicReport.RecalculaGrid(dgvTab_BancosOrder);
            }
        }

        private void dgvTab_Bancos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
//                e.Handled = true;
//                tspAlterar.PerformClick();
            }
        }

        private void dgvTab_Bancos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
//            tspAlterar.PerformClick();
        }

        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrTab_Bancos.IsBusy != true)
                bwrTab_Bancos.RunWorkerAsync();
        }

        private void dgvTab_Bancos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void frmTab_BancosPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvTab_Bancos);
        }
    }
}
