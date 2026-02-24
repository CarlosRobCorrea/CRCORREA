using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{ 
    public partial class frmBancosPes : Form
    {
        private String conexaobanco;
        
        private DataTable dtBancos;
        private Int32 indexBancosValor;
        private clsBasicReport clsBRBancos;

        Int32 id;

        public frmBancosPes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id, String _conexaobanco)
        {
            conexaobanco = _conexaobanco;
//            conexaodados = _conexaodados;
            id = _id;

            clsBRBancos = new clsBasicReport(this, dgvBancos,ttpBancos);

            
        }

        private void frmBancosPes_Load(object sender, EventArgs e)
        {
            // carrega grid evolução de valores
            bwrBancos.RunWorkerAsync(); // carrega GRID Evolucao Salario            
        }

        private void bwrBancos_DoWork(object sender, DoWorkEventArgs e)
        {
            dtBancos = CarregaGridBancos();
        }

        public DataTable CarregaGridBancos()
        {
            clsBancosBLL clsBancosBLL = new clsBancosBLL();
            try
            {
                if (rbnTodos.Checked == true)
                {
                    return clsBancosBLL.GridDados(conexaobanco);
                }
                else if (rbnAtivos.Checked == true)
                {
                    return clsBancosBLL.GridDados2("S", conexaobanco);
                }
                else
                {
                    return clsBancosBLL.GridDados2("N", conexaobanco);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

        }


        private void bwrBancos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArrayList altBancos = clsBRBancos.GetColunas();
            dgvBancos.DataSource = dtBancos;

            
            clsGridHelper.MontaGrid(dgvBancos,
                new String[] { "Id", "Nrº Cta", "Ativo","Nrº Bco", "Agencia", "Nome", "Nrº C.C." , "Transf"},
                new String[] { "ID", "CONTA", "ATIVO", "BANCO", "AGENCIA", "NOME", "CONTACOR", "CTATRANSITORIA" },
                new int[] { 1, 50, 20, 50, 150, 400, 200, 50 },
                new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                        DataGridViewContentAlignment.MiddleCenter},
                new bool[] { false, true, true, true, true, true, true, true },
                true, 2, ListSortDirection.Ascending);

            clsBRBancos.RecalculaGrid(altBancos);

            if (indexBancosValor > 0)
            {
                foreach (DataGridViewRow linha in dgvBancos.Rows)
                {
                    foreach (DataGridViewCell celula in dgvBancos.Rows[linha.Index].Cells)
                    {
                        if (dgvBancos.Columns[celula.ColumnIndex].Name == "ID")
                        {
                            if (dgvBancos.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexBancosValor.ToString().ToLower())
                            {
                                try
                                {
                                    dgvBancos.FirstDisplayedCell = dgvBancos.Rows[celula.RowIndex].Cells[1];
                                    dgvBancos[1, celula.RowIndex].Selected = true;
                                    dgvBancos.Select();
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvBancos.CurrentRow != null)
            {
                indexBancosValor = (Int32)dgvBancos.CurrentRow.Cells[0].Value;
            }
            frmBancos frmBancos = new frmBancos();
            frmBancos.Init(0, dgvBancos.Rows);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmBancos,conexaobanco);

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvBancos.CurrentRow != null)
            {
                clsInfo.zidincluir = 0;
                indexBancosValor = (Int32)dgvBancos.CurrentRow.Cells[0].Value;
                frmBancos frmBancos = new frmBancos();
                frmBancos.Init(indexBancosValor, dgvBancos.Rows);

                
                clsFormHelper.AbrirForm(this.MdiParent, frmBancos,conexaobanco);
            }

        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvBancos }, true);

            //frmProcurar.ShowDialog();

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRBancos.Imprimir(clsInfo.caminhorelatorios, "Banco_Contas", clsInfo.conexaosqldados);
        }
        private void tspImprimirMnu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento");
            /*
            frmRelatorios frmRelatorios = new frmRelatorios();
            frmRelatorios.Init("Modulo","Grupo = Banco_Contas");

            frmRelatorios.ShowDialog();
            */
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvBancos.CurrentRow;
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
                if (dgvBancos.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: "
                        + dgvBancos.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsBancosBLL clsBancosBLL = new clsBancosBLL();
                        clsBancosBLL.Excluir((Int32)dgvBancos.CurrentRow.Cells[0].Value, conexaobanco);
                        // Remove a linha do Grid
                        dgvBancos.Rows.Remove(dgvBancos.CurrentRow);

                        try
                        {
                            indexBancosValor = (Int32)dgvBancos.Rows[dgvBancos.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                indexBancosValor = (Int32)dgvBancos.Rows[dgvBancos.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                indexBancosValor = 0;
                            }
                        }

                        bwrBancos.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void tspOrdem_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmBancosPes_Activated(object sender, EventArgs e)
        {

        }

        private void dgvBancos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvBancos.CurrentRow != null)
            {
//                tspAlterar.PerformClick();
            }

        }
        private void dgvBancos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            //    tspAlterar.PerformClick();
            }
        }
        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrBancos.IsBusy != true)
            {
                bwrBancos.RunWorkerAsync();
            }
        }

        private void dgvBancos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmBancosPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvBancos);
        }

        private void tspTool_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}
