using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class frmIpiPes : Form
    {
     
        private String conexao;
        private Int32 indexIpiValor;
        private DataTable dtIpi;

        
        

        public frmIpiPes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idpesquisa)
        {
            conexao = clsInfo.conexaosqldados;
            indexIpiValor = _idpesquisa;
        }

        private void frmIpiPes_Load(object sender, EventArgs e)
        {
            bwrIpi.RunWorkerAsync();
        }

        private void bwrIpi_DoWork(object sender, DoWorkEventArgs e)
        {
            dtIpi = CarregaIpi();
        }

        private void bwrIpi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            dgvIpi.DataSource = dtIpi;

            
            clsGridHelper.MontaGrid(dgvIpi,
                    new String[] { "id", "Codigo", "Nome", "Aliquota", "Tipo", "Pis Pasep", "Cofins" },
                    new String[] { "ID", "CODIGO", "NOME", "ALIQUOTA", "TIPO", "PISPASEP", "COFINS"},
                   new Int32[] { 0, 70, 320, 70, 70, 70, 70},
                   new DataGridViewContentAlignment[] {
                         DataGridViewContentAlignment.MiddleLeft,                                                                     
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter},
                   new Boolean[] { false, true, true, true, true, true, true },
                                   true, 1,
                       ListSortDirection.Ascending);
            clsGridHelper.SelecionaLinha(indexIpiValor, dgvIpi, 1);
            bwrIpi.Dispose();
            //if (indexIpiValor > 0)
            //{
            //    foreach (DataGridViewRow linha in dgvIpi.Rows)
            //    {
            //        foreach (DataGridViewCell celula in dgvIpi.Rows[linha.Index].Cells)
            //        {
            //            if (dgvIpi.Columns[celula.ColumnIndex].Name == "ID")
            //            {
            //                if (dgvIpi.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexIpiValor.ToString().ToLower())
            //                {
            //                    try
            //                    {
            //                        dgvIpi.FirstDisplayedCell = dgvIpi.Rows[celula.RowIndex].Cells[1];
            //                        dgvIpi[2, celula.RowIndex].Selected = true;
            //                        dgvIpi.Select();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        MessageBox.Show(ex.Message);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        public DataTable CarregaIpi()
        {
            clsIpiBLL clsIpiBLL = new clsIpiBLL();
            try
            {

                if (rbnResumoT.Checked == true)
                {
                    return clsIpiBLL.GridDados("", "", conexao);
                }
                else if (rbnResumoS.Checked == true)
                {
                    return clsIpiBLL.GridDados("", "", conexao);
                }
                else 
                {
                    return clsIpiBLL.GridDados("", "", conexao);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        private void tspRetornar_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }
        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvIpi }, true);

            //frmProcurar.ShowDialog();
        }
        private void dgvIpi_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
         //   tspAlterar.PerformClick();
        }
        private void frmIpiPes_Activated(object sender, EventArgs e)
        {
            if (dtIpi != null && bwrIpi.IsBusy == false)
            {
                bwrIpi.RunWorkerAsync();
            }
        }
        private void rbnFiltro(object sender, EventArgs e)
        {
            if (bwrIpi.IsBusy != true)
            {
                bwrIpi.RunWorkerAsync();
            }
        }
        private void dgvIpi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvIpi.CurrentRow;
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

        private void tspImprimir_Click(object sender, EventArgs e)
        {

        }

        private void tspOrdem_Click(object sender, EventArgs e)
        {

        }

        private void frmIpiPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvIpi);
        }
    }
    
}
