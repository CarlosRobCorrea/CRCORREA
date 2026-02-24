using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmHistoricosPes : Form
    {
        private const Int32 zFormuNumero = 200;
        private String conexao;
        private DataTable dtHistoricos;

        private clsBasicReport clsBRHistoricos;

        
        
        
        Int32 idHistorico;


        private clsSelectInsertUpdateBLL clsSelectInsertUpdateBLL = new clsSelectInsertUpdateBLL();
        

        clsHistoricosBLL clsHistoricosBLL = new clsHistoricosBLL();

        public frmHistoricosPes()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _idHistorico)
        {
            conexao = _conexao;
            idHistorico = _idHistorico;

            
            
            clsBRHistoricos = new clsBasicReport(this, dgvHistoricos, toolTip1);
            clsHistoricosBLL = new clsHistoricosBLL();

        }

        private void frmHistoricosPes_Load(object sender, EventArgs e)
        {
            bwrHistoricos.RunWorkerAsync();
        }

        private void bwrHistoricos_DoWork(object sender, DoWorkEventArgs e)
        {
            String ativos = "";
            if (rbnAtivos.Checked == true)
            {
                ativos = "S";
            }
            else if (rbnInativos.Checked == true)
            {
                ativos = "N";
            }
            dtHistoricos =  clsHistoricosBLL.GridDados(ativos,clsInfo.conexaosqlbanco);

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

            clsBRHistoricos.RecalculaGrid(altHistoricos);

            clsGridHelper.SelecionaLinha(idHistorico, dgvHistoricos, 1);
            dgvHistoricos.Select();
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
                            idHistorico = (Int32)dgvHistoricos.Rows[dgvHistoricos.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                idHistorico = (Int32)dgvHistoricos.Rows[dgvHistoricos.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                idHistorico = 0;
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
        private void frmHistoricosPes_Activated(object sender, EventArgs e)
        {
            if (dtHistoricos != null && bwrHistoricos.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                {
                    idHistorico = clsInfo.zidincluir;
                }
                bwrHistoricos.RunWorkerAsync();
            }
        }

        private void dgvHistoricos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                //  tspAlterar.PerformClick();
            }
        }

        private void frmHistoricosPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvHistoricos);
        }
    }
}
