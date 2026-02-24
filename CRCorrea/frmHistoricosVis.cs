using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmHistoricosVis : Form
    {
        private const Int32 zFormuNumero = 200;
        private String conexao;
        private DataTable dtHistoricos;
        private Int32 indexHistoricosValor;

        private clsBasicReport clsBRHistoricos;

        
        

        private clsSelectInsertUpdateBLL clsSelectInsertUpdateBLL = new clsSelectInsertUpdateBLL();
        

        SqlDataAdapter sda;
        String query;

        public frmHistoricosVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao)
        {
            conexao = _conexao;

            
            
            clsBRHistoricos = new clsBasicReport(this, dgvHistoricos, toolTip1);

        }

        private void frmHistoricosVis_Load(object sender, EventArgs e)
        {
            bwrHistoricos.RunWorkerAsync();
        }

        private void bwrHistoricos_DoWork(object sender, DoWorkEventArgs e)
        {
            dtHistoricos = new DataTable();
            query = "select ID,CODIGO, NOME, " +
                "case ATIVO when 'S' then 'Ativa' when 'N' then 'Inativa' end as ATIVO, " +
                "case TIPO when 'D' then 'Debito' when 'C' then 'Credito' end as TIPO " +
                "FROM HISTORICOS ";
            if (rbnAtivos.Checked == true)
            {
                query = query + " WHERE ATIVO = 'S' ";
            }
            else if (rbnInativos.Checked == true)
            {
                query = query + " WHERE ATIVO != 'S' ";
            }
            query = query + " ORDER BY CODIGO ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqlbanco);
            sda.Fill(dtHistoricos);
        }

        private void bwrHistoricos_RunWorkerAsync()
        {
            bwrHistoricos = new BackgroundWorker();
            bwrHistoricos.DoWork += new DoWorkEventHandler(bwrHistoricos_DoWork);
            bwrHistoricos.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrHistoricos_RunWorkerCompleted);
            bwrHistoricos.RunWorkerAsync();
        }

        private void bwrHistoricos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArrayList altHistoricos = clsBRHistoricos.GetColunas();

            dgvHistoricos.DataSource = dtHistoricos;

            
            clsGridHelper.MontaGrid(dgvHistoricos,
                new String[] { "Id", "Codigo", "Descrição", "Ativo"},
                new String[] { "ID", "CODIGO", "NOME", "ATIVO"},
                new int[] { 1, 150, 600, 50},
                new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,                         
                        DataGridViewContentAlignment.MiddleCenter},
                new bool[] { false, true, true, true},
                true, 1, ListSortDirection.Ascending);

            clsBRHistoricos.RecalculaGrid(altHistoricos);

            if (indexHistoricosValor > 0)
            {
                foreach (DataGridViewRow linha in dgvHistoricos.Rows)
                {
                    foreach (DataGridViewCell celula in dgvHistoricos.Rows[linha.Index].Cells)
                    {
                        if (dgvHistoricos.Columns[celula.ColumnIndex].Name == "ID")
                        {
                            {
                                if (dgvHistoricos.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexHistoricosValor.ToString().ToLower())
                                {
                                    dgvHistoricos.FirstDisplayedCell = dgvHistoricos.Rows[celula.RowIndex].Cells[1];
                                    dgvHistoricos[1, celula.RowIndex].Selected = true;
                                    dgvHistoricos.Select();
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
            if (dgvHistoricos.CurrentRow != null)
            {
                indexHistoricosValor = (Int32)dgvHistoricos.CurrentRow.Cells[0].Value;
            }
            frmHistoricos frmHistoricos = new frmHistoricos();
            frmHistoricos.Init(conexao, 0, dgvHistoricos.Rows);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmHistoricos,conexao);

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvHistoricos.CurrentRow != null)
            {
                clsInfo.zidincluir = 0;
                indexHistoricosValor = (Int32)dgvHistoricos.CurrentRow.Cells[0].Value;
                frmHistoricos frmHistoricos = new frmHistoricos();
                frmHistoricos.Init(conexao, (Int32)dgvHistoricos.CurrentRow.Cells[0].Value, dgvHistoricos.Rows);

                
                clsFormHelper.AbrirForm(this.MdiParent, frmHistoricos,conexao);
            }

        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvHistoricos }, true);

            //frmProcurar.ShowDialog();

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRHistoricos.Imprimir(clsInfo.caminhorelatorios, "Historicos do Aplibank", clsInfo.conexaosqlbanco);
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvHistoricos.CurrentRow;
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
                if (dgvHistoricos.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: "
                        + dgvHistoricos.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsHistoricosBLL clsHistoricosBLL = new clsHistoricosBLL();
                        clsHistoricosBLL.Excluir((Int32)dgvHistoricos.CurrentRow.Cells[0].Value, conexao);
                        // Remove a linha do Grid
                        dgvHistoricos.Rows.Remove(dgvHistoricos.CurrentRow);

                        try
                        {
                            indexHistoricosValor = (Int32)dgvHistoricos.Rows[dgvHistoricos.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                indexHistoricosValor = (Int32)dgvHistoricos.Rows[dgvHistoricos.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                indexHistoricosValor = 0;
                            }
                        }

                        bwrHistoricos.RunWorkerAsync();
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
        private void frmHistoricosVis_Activated(object sender, EventArgs e)
        {
            if (dtHistoricos != null && bwrHistoricos.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                {
                    indexHistoricosValor = clsInfo.zidincluir;
                }
                bwrHistoricos.RunWorkerAsync();
            }
        }

        private void dgvHistoricos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }

        private void dgvHistoricos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrHistoricos.IsBusy != true)
            {
                bwrHistoricos.RunWorkerAsync();
            }
        }

        private void frmHistoricosVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvHistoricos);
        }

    }
}
