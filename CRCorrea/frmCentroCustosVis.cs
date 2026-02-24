using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorrea;

namespace CRCorrea
{
    public partial class frmCentroCustosVis : Form
    {
        String conexao;
        DataTable dtCentroCustos;
        Int32 indexCentroCustosValor;
        clsBasicReport clsBRCentroCustos;

        
        

        public frmCentroCustosVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao)
        {
            conexao = _conexao;

            clsBRCentroCustos = new clsBasicReport(this, dgvCentroCustos, ttpCentrocustos);

            
            
        }

        private void frmCentroCustosVis_Load(object sender, EventArgs e)
        {
            bwrCentroCustos.RunWorkerAsync();
        }

        private void bwrCentroCustos_DoWork(object sender, DoWorkEventArgs e)
        {
            dtCentroCustos = CarregaGridCentroCustos();
        }

        public DataTable CarregaGridCentroCustos()
        {
            clsCentrocustosBLL clsCentrocustosBLL = new clsCentrocustosBLL();
            try
            {
                if (rbnTodos.Checked == true)
                {
                    return clsCentrocustosBLL.GridDados(conexao);
                }
                else if (rbnAtivos.Checked == true)
                {
                    return clsCentrocustosBLL.GridDados("S", conexao);
                }
                else
                {
                    return clsCentrocustosBLL.GridDados("N", conexao);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrCentrocustos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArrayList altCentroCustos = clsBRCentroCustos.GetColunas();

            dgvCentroCustos.DataSource = dtCentroCustos;

            
            clsGridHelper.MontaGrid(dgvCentroCustos,
                new String[] { "Id", "Codigo", "Descrição", "Ativo" },
                new String[] { "ID", "CODIGO", "NOME", "ATIVO" },
                new int[] { 1, 150, 600, 50 },
                new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                        DataGridViewContentAlignment.MiddleCenter},
                new bool[] { false, true, true, true },
                true, 1, ListSortDirection.Ascending);

            clsBRCentroCustos.RecalculaGrid(altCentroCustos);

            if (indexCentroCustosValor > 0)
            {
                foreach (DataGridViewRow linha in dgvCentroCustos.Rows)
                {
                    foreach (DataGridViewCell celula in dgvCentroCustos.Rows[linha.Index].Cells)
                    {
                        if (dgvCentroCustos.Columns[celula.ColumnIndex].Name == "ID")
                        {
                            {
                                if (dgvCentroCustos.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexCentroCustosValor.ToString().ToLower())
                                {
                                    dgvCentroCustos.FirstDisplayedCell = dgvCentroCustos.Rows[celula.RowIndex].Cells[1];
                                    dgvCentroCustos[1, celula.RowIndex].Selected = true;
                                    dgvCentroCustos.Select();
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
            if (dgvCentroCustos.CurrentRow != null)
            {
                indexCentroCustosValor = (Int32)dgvCentroCustos.CurrentRow.Cells[0].Value;
            }
            frmCentroCustos frmCentroCustos = new frmCentroCustos();
            frmCentroCustos.Init(conexao, 0, dgvCentroCustos.Rows);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustos,conexao);

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCentroCustos.CurrentRow != null)
            {
                clsInfo.zidincluir = 0;
                indexCentroCustosValor = (Int32)dgvCentroCustos.CurrentRow.Cells[0].Value;
                frmCentroCustos frmCentroCustos = new frmCentroCustos();
                frmCentroCustos.Init(conexao, (Int32)dgvCentroCustos.CurrentRow.Cells[0].Value, dgvCentroCustos.Rows);

                
                clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustos,conexao);
            }

        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvCentroCustos }, true);

            //frmProcurar.ShowDialog();

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRCentroCustos.Imprimir(clsInfo.caminhorelatorios, "Banco_CentroCustos",conexao);
        }

        private void tspImprimirMnu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento");
            /*
            frmRelatorios frmRelatorios = new frmRelatorios();
            frmRelatorios.Init("Modulo","Grupo = Banco_CentroCustos");

            frmRelatorios.ShowDialog();
            */

        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvCentroCustos.CurrentRow;
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
                if (dgvCentroCustos.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: "
                        + dgvCentroCustos.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsCentrocustosBLL clsCentrocustosBLL = new clsCentrocustosBLL();
                        clsCentrocustosBLL.Excluir((Int32)dgvCentroCustos.CurrentRow.Cells[0].Value, conexao);
                        // Remove a linha do Grid
                        dgvCentroCustos.Rows.Remove(dgvCentroCustos.CurrentRow);

                        try
                        {
                            indexCentroCustosValor = (Int32)dgvCentroCustos.Rows[dgvCentroCustos.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                indexCentroCustosValor = (Int32)dgvCentroCustos.Rows[dgvCentroCustos.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                indexCentroCustosValor = 0;
                            }
                        }

                        bwrCentroCustos.RunWorkerAsync();
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

        private void frmCentroCustosVis_Activated(object sender, EventArgs e)
        {
            if (dtCentroCustos != null && bwrCentroCustos.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                {
                    indexCentroCustosValor = clsInfo.zidincluir;
                }
                bwrCentroCustos.RunWorkerAsync();
            }
        }

        private void dgvCentroCustos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }

        private void dgvCentroCustos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrCentroCustos.IsBusy != true)
            {
                bwrCentroCustos.RunWorkerAsync();
            }
        }

        private void frmCentroCustosVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCentroCustos);
        }

        private void dgvCentroCustos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
