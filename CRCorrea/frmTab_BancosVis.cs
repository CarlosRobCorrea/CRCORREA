using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmTab_BancosVis : Form
    {
        private String conexao;
        
        private DataTable dtTab_Bancos;
        private Int32 indexTab_BancosValor;

        private clsBasicReport clsBasicReport;

        public frmTab_BancosVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao)
        {
            clsBasicReport = new clsBasicReport(this, dgvTab_Bancos, toolTip);

            conexao = _conexao;
        }

        private void frmTab_BancoVis_Load(object sender, EventArgs e)
        {
            bwrTab_Bancos.RunWorkerAsync();
        }

        private void bwrTab_Bancos_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_Bancos = CarregaGridTab_Bancos();
        }

        public DataTable CarregaGridTab_Bancos()
        {
            clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();
            try
            {
                if (rbnTodos.Checked == true)
                    return clsTab_BancosBLL.GridDados("", conexao);
                else if (rbnAtivos.Checked == true)
                    return clsTab_BancosBLL.GridDados("S", conexao);
                else
                    return clsTab_BancosBLL.GridDados("N", conexao);
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

            if (indexTab_BancosValor > 0)
            {
                foreach (DataGridViewRow linha in dgvTab_Bancos.Rows)
                {
                    foreach (DataGridViewCell celula in dgvTab_Bancos.Rows[linha.Index].Cells)
                    {
                        if (dgvTab_Bancos.Columns[celula.ColumnIndex].Name == "ID")
                        {
                            {
                                if (dgvTab_Bancos.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosValor.ToString().ToLower())
                                {
                                    dgvTab_Bancos.FirstDisplayedCell = dgvTab_Bancos.Rows[celula.RowIndex].Cells[1];
                                    dgvTab_Bancos[1, celula.RowIndex].Selected = true;
                                    dgvTab_Bancos.Select();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvTab_Bancos.CurrentRow != null)
                indexTab_BancosValor = (Int32) dgvTab_Bancos.CurrentRow.Cells[0].Value;
            frmTab_Bancos frmTab_Bancos = new frmTab_Bancos();
            frmTab_Bancos.Init(conexao, 0, dgvTab_Bancos.Rows);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmTab_Bancos,conexao);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvTab_Bancos.CurrentRow != null)
            {
                clsInfo.zidincluir = 0;
                indexTab_BancosValor = (Int32)dgvTab_Bancos.CurrentRow.Cells[0].Value;
                frmTab_Bancos frmTab_Bancos = new frmTab_Bancos();
                frmTab_Bancos.Init(conexao, (Int32)dgvTab_Bancos.CurrentRow.Cells[0].Value,dgvTab_Bancos.Rows);

                
                clsFormHelper.AbrirForm(this.MdiParent, frmTab_Bancos, conexao);
            }
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //clsBasicReport.LiberaGrid();
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvTab_Bancos }, true);

            //frmProcurar.ShowDialog();
            //clsBasicReport.TravaGrid();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBasicReport.Imprimir(clsInfo.caminhorelatorios, "Bancos (Febraban)", clsInfo.conexaosqlbanco);
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
            try
            {
                if (dgvTab_Bancos.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: "
                        + dgvTab_Bancos.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();
                        clsTab_BancosBLL.Excluir((Int32)dgvTab_Bancos.CurrentRow.Cells[0].Value, conexao);
                        // Remove a linha do Grid
                        dgvTab_Bancos.Rows.Remove(dgvTab_Bancos.CurrentRow);

                        try
                        {
                            indexTab_BancosValor = (Int32)dgvTab_Bancos.Rows[dgvTab_Bancos.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                indexTab_BancosValor = (Int32)dgvTab_Bancos.Rows[dgvTab_Bancos.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                indexTab_BancosValor = 0;
                            }
                        }

                        bwrTab_Bancos.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmTab_BancosVis_Activated(object sender, EventArgs e)
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
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }

        private void dgvTab_Bancos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrTab_Bancos.IsBusy != true)
                bwrTab_Bancos.RunWorkerAsync();
        }

        private void dgvTab_Bancos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvTab_Bancos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rbnInativos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmTab_BancosVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvTab_Bancos);
        }
    }
}
