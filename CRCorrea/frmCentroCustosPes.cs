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
    public partial class frmCentroCustosPes : Form
    {
        String conexao;
        DataTable dtCentroCustos;
        Int32 indexCentroCustosValor;
        clsBasicReport clsBRCentroCustos;

        public frmCentroCustosPes()
        {
            InitializeComponent();
        }

        public void Init(String _conexao, Int32 _idCentrocusto)
        {
            conexao = _conexao;
            indexCentroCustosValor = _idCentrocusto;

            clsBRCentroCustos = new clsBasicReport(this, dgvCentroCustos, ttpCentrocustos);

            
            
        }

        private void frmCentroCustosPes_Load(object sender, EventArgs e)
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
            clsGridHelper.SelecionaLinha(indexCentroCustosValor, dgvCentroCustos, 1);
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

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRCentroCustos.Imprimir(clsInfo.caminhorelatorios, "Banco_CentroCustos", clsInfo.conexaosqlbanco);
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

        private void frmCentroCustosPes_Activated(object sender, EventArgs e)
        {

        }

        
        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrCentroCustos.IsBusy != true)
            {
                bwrCentroCustos.RunWorkerAsync();
            }
        }

        private void frmCentroCustosPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCentroCustos);
        }

    }
}
