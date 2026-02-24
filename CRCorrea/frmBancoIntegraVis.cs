using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmBancoIntegraVis : Form
    {
        DataTable dtBancos;

        public frmBancoIntegraVis()
        {
            InitializeComponent();
        }

        private void frmBancosVis_Load(object sender, EventArgs e)
        {
            bwrBancos.RunWorkerAsync();
        }

        private void bwrBancos_DoWork(object sender, DoWorkEventArgs e)
        {
            dtBancos = CarregarGridBancos();
        }

        public DataTable CarregarGridBancos()
        {
            var bll = new clsBancosBLL();

            try
            {
                if (rbnTodos.Checked)
                {
                    return bll.GridDados(clsInfo.conexaosqlbanco);
                }
                else
                {
                    return bll.GridDados2(rbnAtivos.Checked ? "S" : "N", clsInfo.conexaosqlbanco);
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
            dgvBancos.DataSource = dtBancos;
            
            clsGridHelper.MontaGrid(dgvBancos,
                new String[] { "Id", "Nº Conta", "Ativo", "Nº Banco", "Agência", "Nome", "Nº C.C.", "Transf"},
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
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            var frmBancoIntegra = new frmBancoIntegra();
            frmBancoIntegra.Init(0, dgvBancos.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancoIntegra, clsInfo.conexaosqlbanco);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvBancos.CurrentRow != null)
            {
                var frmBancoIntegra = new frmBancoIntegra();
                frmBancoIntegra.Init((int)dgvBancos.CurrentRow.Cells[0].Value, dgvBancos.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmBancoIntegra, clsInfo.conexaosqlbanco);
            }
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvBancos }, true);

            //frmProcurar.ShowDialog();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvBancos.CurrentRow;

            this.Close();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBancos.CurrentRow != null)
                {
                    var resultado = MessageBox.Show("Deseja realmente 'Excluir' o registro: " + dgvBancos.CurrentRow.Cells[2].Value + "?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (resultado == DialogResult.Yes)
                    {
                        var clsBancosBLL = new clsBancosBLL();
                        clsBancosBLL.Excluir((Int32)dgvBancos.CurrentRow.Cells[0].Value, clsInfo.conexaosqlbanco);

                        dgvBancos.Rows.Remove(dgvBancos.CurrentRow);

                        bwrBancos.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvBancos);
        }

        private void dgvBancos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }
    }
}
