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
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmCfopPes : Form
    {
        
        DataTable dtCfop;
        Int32 indexCfopValor;
        String conexaodados;
        clsBasicReport clsBRCfop;

        

        public frmCfopPes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idpesquisa)
        {
            indexCfopValor = _idpesquisa;

//            clsBRCfop = new clsBasicReport(this, dgvCfop, toolTip);
        }

        private void frmCfopPes_Load(object sender, EventArgs e)
        {
            conexaodados = clsInfo.conexaosqldados;
            bwrCfop.RunWorkerAsync();
        }

        private void bwrCfop_DoWork(object sender, DoWorkEventArgs e)
        {
            dtCfop = CarregaGridCfop();
        }

        public DataTable CarregaGridCfop()
        {
            clsCfopBLL clsCfopBLL = new clsCfopBLL();
            try
            {

                if (rbnAtivo.Checked) // todos
                {
                    return clsCfopBLL.GridDados("T", conexaodados);
                }
                else if (rbnAtivoS.Checked) // ativos
                {
                    return clsCfopBLL.GridDados("S", conexaodados);
                }
                else
                {
                    return clsCfopBLL.GridDados("N", conexaodados);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrCfop_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            ArrayList altCfop = clsBRCfop.GetColunas();

            dgvCfop.DataSource = dtCfop;
            clsGridHelper.MontaGrid(dgvCfop,
                new String[] { "Id", "C.F.O.P.", "Sai Nota", "Descrição", "Cta Debito", "Cta Credito", "IDCONTADEB", "IDCONTACRE" },
                new String[] { "ID", "CFOP", "NOMENOTA", "DIZER", "DEBITO", "CREDITO", "IDCONTADEB", "IDCONTACRE" },
                new int[] { 0, 90, 220, 450, 80, 80, 0, 0 },
                new DataGridViewContentAlignment[]
                        {
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter},
                new bool[] { false, true, true, true, true, true, false, false },
                true, 1, ListSortDirection.Ascending);

            clsBRCfop.RecalculaGrid(altCfop);

            clsGridHelper.FontGrid(dgvCfop, 8);

            dgvCfop.Select();

            clsGridHelper.SelecionaLinha(indexCfopValor, dgvCfop, 1);
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvCfop.CurrentRow != null)
            {
                indexCfopValor = (Int32)dgvCfop.CurrentRow.Cells[0].Value;
            }
            frmCfop frmCfop = new frmCfop();
            frmCfop.Init(0, dgvCfop.Rows);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmCfop, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCfop.CurrentRow != null)
            {
                clsInfo.zidincluir = 0;
                indexCfopValor = (Int32)dgvCfop.CurrentRow.Cells[0].Value;
                frmCfop frmCfop = new frmCfop();
                frmCfop.Init((Int32)dgvCfop.CurrentRow.Cells[0].Value, dgvCfop.Rows);

                
                clsFormHelper.AbrirForm(this.MdiParent, frmCfop, clsInfo.conexaosqldados);
            }

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRCfop.Imprimir(clsInfo.caminhorelatorios, "Cfop", clsInfo.conexaosqldados);
        }

        private void tspImprimirMnu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento");
            /*
            frmRelatorios frmRelatorios = new frmRelatorios();
            frmRelatorios.Init("Modulo","Grupo = CFOP");

            frmRelatorios.ShowDialog();
            */

        }


        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvCfop.CurrentRow;
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
                if (dgvCfop.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: "
                        + dgvCfop.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsCfopBLL clsCfopBLL = new clsCfopBLL();
                        clsCfopBLL.Excluir((Int32)dgvCfop.CurrentRow.Cells[0].Value, conexaodados);
                        // Remove a linha do Grid
                        dgvCfop.Rows.Remove(dgvCfop.CurrentRow);

                        try
                        {
                            indexCfopValor = (Int32)dgvCfop.Rows[dgvCfop.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                indexCfopValor = (Int32)dgvCfop.Rows[dgvCfop.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                indexCfopValor = 0;
                            }
                        }

                        bwrCfop.RunWorkerAsync();
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
        private void frmCfopPes_Activated(object sender, EventArgs e)
        {
            if (dtCfop != null && bwrCfop.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                {
                    indexCfopValor = clsInfo.zidincluir;
                }
                bwrCfop.RunWorkerAsync();
            }
        }

        private void dgvCfop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                //                tspAlterar.PerformClick();
            }
        }

        private void dgvCfop_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //            tspAlterar.PerformClick();
        }

        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrCfop.IsBusy != true)
            {
                bwrCfop.RunWorkerAsync();
            }
        }

        private void dgvCfop_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void rbnAtivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmCfopPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCfop);
        }
    }
}
