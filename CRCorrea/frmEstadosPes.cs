using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;

namespace CRCorrea
{
    public partial class frmEstadosPes : Form
    {
        private Int32 ixEstados;
        private DataTable dtEstados;
        private clsEstadosBLL clsEstadosBLL;

        
        
        


        public frmEstadosPes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idpesquisa)
        {
            ixEstados = _idpesquisa;
            clsEstadosBLL = new clsEstadosBLL();

            
            
            
        }

        private void frmEstadosPes_Load(object sender, EventArgs e)
        {
            bwrEstados.RunWorkerAsync();
        }

        private void bwrEstados_DoWork(object sender, DoWorkEventArgs e)
        {
            dtEstados = CarregaGridEstados();
        }

        public DataTable CarregaGridEstados()
        {
            clsEstadosBLL clsEstadosBLL = new clsEstadosBLL();
            try
            {
                return clsEstadosBLL.GridDados(clsInfo.conexaosqldados);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrEstados_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvEstados.DataSource = dtEstados;

            
            clsGridHelper.MontaGrid(dgvEstados,
                                new String[] { "Id.", "UF", "Nome", "Capital" },
                                new String[] { "ID", "ESTADO", "NOMEEXT", "CAPITAL" },
                                new Int32[] { 10, 80, 260, 260 },
                                new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft},
                                new Boolean[] { false, true, true, true },
                                true, 1, ListSortDirection.Ascending);

            if (ixEstados > 0)
            {
                foreach (DataGridViewRow linha in dgvEstados.Rows)
                {
                    foreach (DataGridViewCell celula in dgvEstados.Rows[linha.Index].Cells)
                    {
                        if (dgvEstados.Columns[celula.ColumnIndex].Name == "ID")
                        {
                            if (dgvEstados.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == ixEstados.ToString().ToLower())
                            {
                                try
                                {
                                    dgvEstados.FirstDisplayedCell = dgvEstados.Rows[celula.RowIndex].Cells[1];
                                    dgvEstados[1, celula.RowIndex].Selected = true;
                                    dgvEstados.Select();
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvEstados.CurrentRow;
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


        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        private void dgvEstados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                //tspAlterar.PerformClick();
            }
        }

        private void dgvEstados_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           // tspAlterar.PerformClick();
        }

        private void frmEstadosPes_Activated(object sender, EventArgs e)
        {
            if (dtEstados != null && bwrEstados.IsBusy == false)
            {
                if (clsInfo.zidincluir != 0)
                {
                    ixEstados = clsInfo.zidincluir;
                }
                bwrEstados.RunWorkerAsync();
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvEstados);
        }

    }       
}
