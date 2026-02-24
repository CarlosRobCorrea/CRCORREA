using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;

namespace CRCorrea
{
    public partial class frmTab_PaisesVis : Form
    {
        private String conexao;

        private Int32 ixPaises;
        private DataTable dtPaises;
        private clsTab_PaisesBLL clsTab_PaisesBLL;

        
        
        
        clsBasicReport clsBR;
        
        public frmTab_PaisesVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao)
        {
            conexao = _conexao;

            clsTab_PaisesBLL = new clsTab_PaisesBLL();

            
            
            
            clsBR = new clsBasicReport(this, dgvPaises, toolTip1);
        }

        private void frmTab_PaisesVis_Load(object sender, EventArgs e)
        {
            bwrPaises.RunWorkerAsync();
        }

        private DataTable CarregaGridPaises()
        {
            try
            {
                return clsTab_PaisesBLL.GridDados(conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrPaises_DoWork(object sender, DoWorkEventArgs e)
        {
            dtPaises = CarregaGridPaises();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvPaises.CurrentRow != null)
                ixPaises = (Int32)dgvPaises.CurrentRow.Cells[0].Value;
            
            frmTab_Paises frmTab_Paises = new frmTab_Paises();
            frmTab_Paises.Init(conexao, (Int32)dgvPaises.CurrentRow.Cells[0].Value, dgvPaises.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_Paises,conexao);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPaises.CurrentRow != null)
            {
                ixPaises = (Int32)dgvPaises.CurrentRow.Cells[0].Value;
                frmTab_Paises frmTab_Paises = new frmTab_Paises();
                frmTab_Paises.Init(conexao, (Int32)dgvPaises.CurrentRow.Cells[0].Value, dgvPaises.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmTab_Paises,conexao);

            }
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            frmProcurar frmProcurar = new frmProcurar();
            frmProcurar.Init(new DataGridView[] { this.dgvPaises }, true);

            frmProcurar.ShowDialog();
        }

        private void bwrPaises_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvPaises.DataSource = dtPaises;
            clsGridHelper.MontaGrid(dgvPaises,
                                new String[] { "Id.", "UF", "País", "DDD", "DDD Direto", "ISO 3166A", "ISO 3166B", "ISO 3166C", "Nome Int", "Bacen", "Area", "Perimetro", "Fundação", "Continente", "Comex" },
                                new String[] { "ID", "UF","NOME","DDD","DDDDIRETO","ISO3166A","ISO3166B","ISO3166C","NOMEINT","BACEN","AREA","PERIMETRO","ANOFUNDACAO","CONTINENTE","COMEX" },
                                new int[] { 0, 30, 240, 30, 40, 40 , 40, 40, 235, 40, 60, 40, 40, 60, 40 },
                                new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleCenter},
                                new bool[] { false, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                                true, 1, ListSortDirection.Ascending);
            clsGridHelper.FontGrid(dgvPaises, 7);
            clsGridHelper.SelecionaLinha(ixPaises, dgvPaises, 1);
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)dgvPaises.CurrentRow.Cells[0].Value > 0)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: " + dgvPaises.CurrentRow.Cells[2].Value + "?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsTab_PaisesBLL.Excluir((Int32)dgvPaises.CurrentRow.Cells[0].Value, conexao);
                        ixPaises = clsGridHelper.NextIndex((Int32)dgvPaises.CurrentRow.Index, dgvPaises);
                        bwrPaises.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvPaises.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvPaises_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPaises.CurrentRow != null)
                tspAlterar.PerformClick();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvPaises.CurrentRow != null)
                clsInfo.zrow = dgvPaises.CurrentRow;
            this.Close();
            this.Dispose();
        }

        private void frmTab_PaisesVis_Activated(object sender, EventArgs e)
        {
            if (bwrPaises.IsBusy == false)
                bwrPaises.RunWorkerAsync();
            else
            {
                System.Threading.Thread.Sleep(2000);
                if (bwrPaises.IsBusy == false)
                    bwrPaises.RunWorkerAsync();
            }
        }

        private void frmTab_PaisesVis_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zlastid = 0;

            if (bwrPaises.IsBusy)
                bwrPaises.CancelAsync();
        }

        private void frmTab_PaisesVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvPaises);
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Paises", clsInfo.conexaosqldados);
        }
    }
}
