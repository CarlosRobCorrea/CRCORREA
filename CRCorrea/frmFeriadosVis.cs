using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmFeriadosVis : Form
    {
        private String conexao;
        private Int32 ixFeriados;
        private DataTable dtFeriados;
        
        
        private clsBasicReport clsBRFeriados;
        private clsFeriadosBLL clsFeriadosBLL;
        
        

        public frmFeriadosVis()
        {
            InitializeComponent();
        }

        public void Init() // tirado a conexao em 11/09/2009 - carlo)
        {
            
            
            clsFeriadosBLL = new clsFeriadosBLL();
            clsBRFeriados = new clsBasicReport(this, dgvFeriados, toolTip);
            

            conexao = clsInfo.conexaosqldados; // _conexao;
        }

        private void bwrFeriados_DoWork(object sender, DoWorkEventArgs e)
        {
            dtFeriados = clsFeriadosBLL.GridCarrega(conexao);  
        }

        private void bwrFeriados_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArrayList altFeriados = clsBRFeriados.GetColunas();
            dgvFeriados.DataSource = dtFeriados;

            clsGridHelper.MontaGrid(dgvFeriados,
                                new String[] { "id", "Data", "Descrição" },
                                new String[] { "ID", "DATA", "DESCRICAO" },
                                new Int32[] { 0, 80, 800 },
                                new DataGridViewContentAlignment[] { 
                                                DataGridViewContentAlignment.MiddleLeft, 
                                                DataGridViewContentAlignment.MiddleLeft,
                                                DataGridViewContentAlignment.MiddleLeft },
                                new Boolean[] { false, true, true },
                                true, 1, ListSortDirection.Descending);

            clsBRFeriados.RecalculaGrid(altFeriados);

            clsGridHelper.SelecionaLinha(ixFeriados, dgvFeriados,1);
        }

        private void frmFeriados_Load(object sender, EventArgs e)
        {
            bwrFeriados.RunWorkerAsync();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvFeriados.CurrentRow != null)
                ixFeriados = (Int32)dgvFeriados.CurrentRow.Cells[0].Value;
            frmFeriados frmFeriados = new frmFeriados();
            frmFeriados.Init(0, dgvFeriados.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmFeriados, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvFeriados.CurrentRow != null)
            {
                ixFeriados = (Int32)dgvFeriados.CurrentRow.Cells[0].Value;
                frmFeriados frmFeriados = new frmFeriados();
                frmFeriados.Init((Int32)dgvFeriados.CurrentRow.Cells[0].Value, dgvFeriados.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmFeriados, clsInfo.conexaosqldados);
            }
            else
            {
                dgvFeriados.Select();
            }
        }
                
        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFeriados.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                                            dgvFeriados.CurrentRow.Cells[0].Value + "",
                                            "Aplisoft",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button2) == 
                                            DialogResult.Yes)
                    {
                        clsFeriadosBLL.Excluir((Int32)dgvFeriados.CurrentRow.Cells[0].Value, conexao);
                        ixFeriados = clsGridHelper.NextIndex((Int32)dgvFeriados.CurrentRow.Index, dgvFeriados);
                        if (bwrFeriados.IsBusy == false)
                            bwrFeriados.RunWorkerAsync();
                    }
                }
                else
                {
                    dgvFeriados.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dgvFeriados_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void frmFeriadosVis_Activated(object sender, EventArgs e)
        {
            if (clsInfo.zlastid != 0)
                ixFeriados = clsInfo.zlastid;
            if (bwrFeriados.IsBusy == false)
                bwrFeriados.RunWorkerAsync();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvFeriados.CurrentRow != null)
                clsInfo.zrow = dgvFeriados.CurrentRow;
            this.Close();
            this.Dispose();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBRFeriados.Imprimir(clsInfo.zarquivoreport, "Feriados", clsInfo.conexaosqldados);
        }

        private void frmFeriadosVis_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zlastid = 0;
        }

        private void dgvFeriados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvFeriados.CurrentRow != null)
                tspAlterar.PerformClick();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvFeriados);
        }

        private void dgvFeriados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
