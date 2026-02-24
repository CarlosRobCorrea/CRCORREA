using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaBLL;


namespace CRCorrea
{
    public partial class frmContacontabilPes : Form
    {
        private Int32 ixEscForm;        

        
        
        

        DataTable dtContacontabil;
        Int32 indexContacontabilValor;

        String conexaodados;

        clsBasicReport clsBRContacontabil;

        public frmContacontabilPes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idpesquisa)
        {
            ixEscForm = _idpesquisa;            
            clsBRContacontabil = new clsBasicReport(this, dgvContacontabil, toolTip);

            
            
            
        }

        private void frmContacontabilPes_Load(object sender, EventArgs e)
        {
            conexaodados = clsInfo.conexaosqldados;

            bwrContacontabil.RunWorkerAsync();
        }

        private void bwrContacontabil_DoWork(object sender, DoWorkEventArgs e)
        {
            dtContacontabil = CarregaGridContacontabil();
        }

        public DataTable CarregaGridContacontabil()
        {
            clsContacontabilBLL clsContacontabilBLL = new clsContacontabilBLL();
            try
            {
                return clsContacontabilBLL.GridDados(conexaodados);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrContacontabil_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            ArrayList altContacontabil = clsBRContacontabil.GetColunas();

            dgvContacontabil.DataSource = dtContacontabil;

            
            clsGridHelper.MontaGrid(dgvContacontabil,
                new String[] { "Id", "Cod. Red", "Tipo", "Classificação", "Descrição" },
                new String[] { "ID", "REDUZIDO", "TIPO", "CODIGO", "NOME" },
                new int[] { 0, 90, 40, 150, 580 },
                new DataGridViewContentAlignment[]
                        {
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                new bool[] { false, true, true, true, true },
                true, 1, ListSortDirection.Ascending);

            clsBRContacontabil.RecalculaGrid(altContacontabil);

            clsGridHelper.FontGrid(dgvContacontabil, 8);
            
            dgvContacontabil.Select();

            clsGridHelper.SelecionaLinha(ixEscForm, dgvContacontabil, 1);
        }
        private void frmContacontabilPes_Activated(object sender, EventArgs e)
        {
            if (dtContacontabil != null && bwrContacontabil.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                {
                    indexContacontabilValor = clsInfo.zidincluir;
                }
                bwrContacontabil.RunWorkerAsync();
            }
        }

        private void dgvContacontabil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                //tspAlterar.PerformClick();
            }
        }

        private void dgvContacontabil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // tspAlterar.PerformClick();
        }

        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrContacontabil.IsBusy != true)
            {
                bwrContacontabil.RunWorkerAsync();
            }
        }

        private void dgvContacontabil_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            //             CamposCliente(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvContacontabil.CurrentRow != null)
            {
                indexContacontabilValor = (Int32)dgvContacontabil.CurrentRow.Cells[0].Value;
            }
            frmContacontabil frmContacontabil = new frmContacontabil();
            frmContacontabil.Init(0, dgvContacontabil.Rows);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmContacontabil, clsInfo.conexaosqldados);

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvContacontabil.CurrentRow != null)
            {
                clsInfo.zidincluir = 0;
                indexContacontabilValor = (Int32)dgvContacontabil.CurrentRow.Cells[0].Value;
                frmContacontabil frmContacontabil = new frmContacontabil();
                frmContacontabil.Init((Int32)dgvContacontabil.CurrentRow.Cells[0].Value, dgvContacontabil.Rows);

                
                clsFormHelper.AbrirForm(this.MdiParent, frmContacontabil, clsInfo.conexaosqldados);
            }

        }

       private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRContacontabil.Imprimir(clsInfo.zarquivoreport, "Contacontabil", clsInfo.conexaosqldados);
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvContacontabil.CurrentRow;
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
                if (dgvContacontabil.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: "
                        + dgvContacontabil.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsContacontabilBLL clsContacontabilBLL = new clsContacontabilBLL();
                        clsContacontabilBLL.Excluir((Int32)dgvContacontabil.CurrentRow.Cells[0].Value, conexaodados);
                        // Remove a linha do Grid
                        dgvContacontabil.Rows.Remove(dgvContacontabil.CurrentRow);

                        try
                        {
                            indexContacontabilValor = (Int32)dgvContacontabil.Rows[dgvContacontabil.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                indexContacontabilValor = (Int32)dgvContacontabil.Rows[dgvContacontabil.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                indexContacontabilValor = 0;
                            }
                        }

                        bwrContacontabil.RunWorkerAsync();
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

        private void frmContacontabilPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvContacontabil);
        }
    }
}
