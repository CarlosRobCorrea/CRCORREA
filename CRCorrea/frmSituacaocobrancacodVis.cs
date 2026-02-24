using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;

namespace CRCorrea
{
    public partial class frmSituacaocobrancacodVis : Form
    {
        String conexao;
        String conexao_banco;

        Int32 ixSituacaocobrancacod;
        DataTable dtSituacaocobrancacod;
        clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL;

        DataTable dtSituacaocobrancacod1;
        clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL;

        
        
        

        clsBasicReport clsBR;


        public frmSituacaocobrancacodVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         String _conexao_banco)
        {
            conexao = _conexao;
            conexao_banco = _conexao_banco;

            clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
            clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();

            
            
            
            clsBR = new clsBasicReport(this, dgvSituacaocobrancacod, ttpSituacaocobrancacod);
        }

        private void frmSituacaocobrancacodVis_Load(object sender, EventArgs e)
        {
            bwrSituacaocobrancacod.RunWorkerAsync();
        }

        private DataTable CarregaGridSituacaocobrancacod()
        {
            try
            {
                return clsSituacaocobrancacodBLL.GridDados(conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrSituacaocobrancacod_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSituacaocobrancacod = CarregaGridSituacaocobrancacod();
        }

        private void bwrSituacaocobrancacod_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvSituacaocobrancacod.DataSource = dtSituacaocobrancacod;
            clsGridHelper.MontaGrid(dgvSituacaocobrancacod,
                                new String[] { "Id.", "Cód.", "Nome" },
                                new String[] { "ID", "CODIGO", "NOME" },
                                new int[] { 10, 140, 360 },
                                new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft},
                                new bool[] { false, true, true },
                                true, 1, ListSortDirection.Ascending);
            clsGridHelper.SelecionaLinha(ixSituacaocobrancacod, dgvSituacaocobrancacod, 1);

            if (bwrSituacaocobrancacod1.IsBusy == false)
                bwrSituacaocobrancacod1.RunWorkerAsync();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvSituacaocobrancacod.CurrentRow != null)
                ixSituacaocobrancacod = (Int32)dgvSituacaocobrancacod.CurrentRow.Cells[0].Value;
            frmSituacaocobrancacod frmSituacaocobrancacod = new frmSituacaocobrancacod();
            frmSituacaocobrancacod.Init(conexao, conexao_banco, 0, dgvSituacaocobrancacod.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmSituacaocobrancacod,conexao);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSituacaocobrancacod.CurrentRow != null)
            {
                ixSituacaocobrancacod = (Int32)dgvSituacaocobrancacod.CurrentRow.Cells[0].Value;
                frmSituacaocobrancacod frmSituacaocobrancacod = new frmSituacaocobrancacod();
                frmSituacaocobrancacod.Init(conexao, conexao_banco, (Int32)dgvSituacaocobrancacod.CurrentRow.Cells[0].Value, dgvSituacaocobrancacod.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmSituacaocobrancacod,conexao);
            }
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvSituacaocobrancacod }, true);

            //frmProcurar.ShowDialog();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)dgvSituacaocobrancacod.CurrentRow.Cells[0].Value > 0)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: " + dgvSituacaocobrancacod.CurrentRow.Cells[2].Value + "?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsSituacaocobrancacodBLL.Excluir((Int32)dgvSituacaocobrancacod.CurrentRow.Cells[0].Value, conexao);
                        ixSituacaocobrancacod = clsGridHelper.NextIndex((Int32)dgvSituacaocobrancacod.CurrentRow.Index, dgvSituacaocobrancacod);
                        bwrSituacaocobrancacod.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSituacaocobrancacod.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSituacaocobrancacod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSituacaocobrancacod.CurrentRow != null)
                tspAlterar.PerformClick();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvSituacaocobrancacod.CurrentRow != null)
                clsInfo.zrow = dgvSituacaocobrancacod.CurrentRow;
            this.Close();
            this.Dispose();
        }

        private void frmSituacaocobrancacodVis_Activated(object sender, EventArgs e)
        {
            if (bwrSituacaocobrancacod.IsBusy == false)
                bwrSituacaocobrancacod.RunWorkerAsync();
        }

        private void frmSituacaocobrancacodVis_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zlastid = 0;
        }

        private void bwrSituacaocobrancacod1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSituacaocobrancacod1 = CarregaGridSituacaocobrancacod1();
        }

        private DataTable CarregaGridSituacaocobrancacod1()
        {
            try
            {
                if (dgvSituacaocobrancacod.CurrentRow != null)
                    return clsSituacaocobrancacod1BLL.GridDados(clsParser.Int32Parse(dgvSituacaocobrancacod.CurrentRow.Cells[0].Value.ToString()), conexao);
                else
                    return clsSituacaocobrancacod1BLL.GridDados(0, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrSituacaocobrancacod1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvSituacaocobrancacod1.DataSource = dtSituacaocobrancacod1;
            clsGridHelper.MontaGrid(dgvSituacaocobrancacod1,
                                new String[] { "Id.", "Principal", "Cód.", "Nome" },
                                new String[] { "ID", "PRINCIPAL", "CODIGO", "NOME" },
                                new int[] { 10, 140, 140, 360 },
                                new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft},
                                new bool[] { false, true, true, true },
                                true, 1, ListSortDirection.Ascending);
            clsGridHelper.SelecionaLinha(0, dgvSituacaocobrancacod1, 1);
        }

        private void dgvSituacaocobrancacod_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bwrSituacaocobrancacod1.IsBusy == false)
                bwrSituacaocobrancacod1.RunWorkerAsync();
        }

        private void frmSituacaocobrancacodVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvSituacaocobrancacod);
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Cod. Situação de Cobrança", clsInfo.conexaosqldados);
        }
    }
}
