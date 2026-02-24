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
    public partial class frmCfopVis : Form
    {
        /* Ricardo Line 01 */
        /* Ricardo Line 02 */

        DataTable dtCfop;
        Int32 ixCfop;

        //testado pelo Brian

        /* Teste Ricardo */

        clsBasicReport clsBr;

        public frmCfopVis()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id)
        {
            ixCfop = _id;

            clsBr = new clsBasicReport(this, dgvCfop, toolTip);

            
            
            
            
        }

        private void frmCfopVis_Load(object sender, EventArgs e)
        {
            
        }

        private void bwrCfop_RunWork()
        {
            bwrCfop = new BackgroundWorker();
            bwrCfop.DoWork += new DoWorkEventHandler(bwrCfop_DoWork);
            bwrCfop.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCfop_RunWorkerCompleted);
            bwrCfop.RunWorkerAsync();
        }

        private void bwrCfop_DoWork(object sender, DoWorkEventArgs e)
        {
            CarregaGridCfop();
        }

        public void CarregaGridCfop()
        {
            try
            {
                string query = "";
                query = "SELECT " +
                            "CFOP.ID, " +
                            "CFOP.CFOP, " +
                            "CFOP.NOMENOTA, " +
                            "CFOP.DIZER, " +
                            "CONTACONTABIL.CODIGO AS [DEBITO], " +
                            "CONTABIL1.CODIGO AS [CREDITO], " +
                            "CFOP.IDCONTADEB, " +
                            "CFOP.IDCONTACRE " +
                        "FROM CFOP " +
                            "INNER JOIN CONTACONTABIL ON CONTACONTABIL.ID=IDCONTADEB " +
                            "INNER JOIN CONTACONTABIL AS [CONTABIL1] ON CONTABIL1.ID=CFOP.IDCONTACRE ";

                if (rbnAtivo.Checked == false)
                    query += " WHERE CFOP.ATIVO = @ATIVO ";

                SqlDataAdapter sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

                if (rbnAtivoS.Checked) sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = "S";
                else if (rbnAtivoN.Checked) sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = "N";

                dtCfop = new DataTable();
                sda.Fill(dtCfop);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bwrCfop_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArrayList altCfop = clsBr.GetColunas();
            dgvCfop.DataSource = dtCfop;

            clsGridHelper.MontaGrid(dgvCfop, new GridColuna[]
                                        {
                                            new GridColuna("Id", "ID", 10, false, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("CFOP", "CFOP", 90, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Nome Nota", "NOMENOTA", 220, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Descrição", "DIZER", 450, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Cta Débito", "DEBITO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Cta Crédito", "CREDITO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Id Deb.", "IDCONTADEB", 10, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Id Cré.", "IDCONTACRE", 10, false, DataGridViewContentAlignment.MiddleRight)
                                        }, false);

            clsBr.RecalculaGrid(altCfop);
            clsGridHelper.FontGrid(dgvCfop, 8);

                       
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvCfop.CurrentRow != null)
            {
                ixCfop = (Int32)dgvCfop.CurrentRow.Cells[0].Value;
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
                ixCfop = (Int32)dgvCfop.CurrentRow.Cells[0].Value;
                frmCfop frmCfop = new frmCfop();
                frmCfop.Init((Int32)dgvCfop.CurrentRow.Cells[0].Value, dgvCfop.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmCfop, clsInfo.conexaosqldados);
            }

        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //clsBr.TravaGrid();
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvCfop }, true);

            //frmProcurar.ShowDialog();
            //clsBr.LiberaGrid();            
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Cfop", clsInfo.conexaosqldados);
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
                        clsCfopBLL.Excluir((Int32)dgvCfop.CurrentRow.Cells[0].Value, clsInfo.conexaosqldados);
                        // Remove a linha do Grid
                        dgvCfop.Rows.Remove(dgvCfop.CurrentRow);

                        try
                        {
                            ixCfop = (Int32)dgvCfop.Rows[dgvCfop.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                ixCfop = (Int32)dgvCfop.Rows[dgvCfop.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                ixCfop = 0;
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

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmCfopVis_Activated(object sender, EventArgs e)
        {
            if (dtCfop != null && bwrCfop.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                    ixCfop = clsInfo.zidincluir;
            }

            bwrCfop_RunWork();
        }

        private void dgvCfop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }

        private void dgvCfop_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrCfop.IsBusy != true)
            {
                bwrCfop.RunWorkerAsync();
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmCfopVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCfop);
        }








    }
}
