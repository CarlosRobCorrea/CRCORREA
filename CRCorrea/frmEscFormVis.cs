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
    public partial class frmEscFormVis : Form
    {
        private String conexao;
        private String tabela;
        private String nometabela;
        private Int32 maxlengthcodigo;
        private Int32 maxlengthnome;
        private Int32 ixEscForm;
        private DataTable dtEscForm;
        
        
        //private clsBasicReport clsBREscForm;
        private clsEscFormBLL clsEscFormBLL;

        public frmEscFormVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         String _tabela,
                         Int32 _ix,
                         Int32 _maxlengthcodigo,
                         Int32 _maxlengthnome,
                         String _nometabela)
        {
            conexao = _conexao;
            tabela = _tabela;
            nometabela = _nometabela;

            maxlengthcodigo = _maxlengthcodigo;
            maxlengthnome = _maxlengthnome;

            if (maxlengthcodigo == 0)
            {
                maxlengthcodigo = 1;
            }
            if (maxlengthnome == 0)
            {
                maxlengthnome = 1;
            }


            ixEscForm = _ix;

            
            
            //clsBREscForm = new clsBasicReport(this, dgvEscForm, toolTip);
            clsEscFormBLL = new clsEscFormBLL(tabela);
        }

        private void frmEscFormVis_Load(object sender, EventArgs e)
        {
            lblNomeTabela.Text = nometabela;

            /* HABILITAR QUANDO JÁ TIVER SIDO DETERMINADO O VALOR DA PROPRIEDADE TAG
            if (FormHelper.ConfigForm(clsParser.Int32Parse(this.Tag.ToString()),
                                    this,
                                    this.Text,
                                    clsInfo.conexaosqldados,
                                    toolTip1) == false)
            {
                this.Close();
                this.Dispose();
            }
            */

            bwrEscForm.RunWorkerAsync();

            /* HABILITAR QUANDO JÁ TIVER SIDO DETERMINADO O VALOR DA PROPRIEDADE TAG
            if (FormHelper.AcessaForm(clsParser.Int32Parse(this.Tag.ToString()),
                                    this,
                                    this.Text,
                                    clsInfo.conexaosqldados,
                                    toolTip1) == false)
            {
                this.Close();
                this.Dispose();
            }

            
             */
        }

        private void bwrEscForm_DoWork(object sender, DoWorkEventArgs e)
        {
            dtEscForm = CarregaGridEscForm();
        }

        private DataTable CarregaGridEscForm()
        {
            try
            {
                return clsEscFormBLL.GridDados("", "", conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrEscForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
//            ArrayList altEscForm = clsBREscForm.GetColunas();
            dgvEscForm.DataSource = dtEscForm;
        
            clsGridHelper.MontaGrid(dgvEscForm,
                    new String[] { "Id.", "Código", "Nome"},
                    new String[] { "ID", "CODIGO", "NOME"},
                    new int[] { 10, 120, 650},
                    new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleLeft,                                    
                                    DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true },
                    true, 1, ListSortDirection.Ascending);

            if (dgvEscForm.RowCount > 1)
            {
                Int32 numero;
                if (Int32.TryParse(dgvEscForm.Rows[1].Cells[1].Value.ToString(), out numero) == false)
                {
                    dgvEscForm.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }

            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvEscForm);

            //clsBREscForm.RecalculaGrid(altEscForm);
            clsGridHelper.SelecionaLinha(ixEscForm, dgvEscForm,1);
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmEscForm frmEscForm = new frmEscForm();
            frmEscForm.Init(conexao, 0, tabela,maxlengthcodigo,maxlengthnome,nometabela);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscForm, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if ((Int32)dgvEscForm.CurrentRow.Cells[0].Value > 0)
            {
                frmEscForm frmEscForm = new frmEscForm();
                frmEscForm.Init(conexao, (Int32)dgvEscForm.CurrentRow.Cells[0].Value, tabela,maxlengthcodigo,maxlengthnome, nometabela);

                clsFormHelper.AbrirForm(this.MdiParent, frmEscForm, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvEscForm.Select();
            }
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)dgvEscForm.CurrentRow.Cells[0].Value > 0)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: " + dgvEscForm.CurrentRow.Cells[2].Value + "?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsEscFormBLL.Excluir((Int32)dgvEscForm.CurrentRow.Cells[0].Value, conexao);
                        ixEscForm = clsGridHelper.NextIndex((Int32)dgvEscForm.CurrentRow.Index, dgvEscForm);
                        bwrEscForm.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvEscForm.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void frmEscFormVis_Activated(object sender, EventArgs e)
        {
            if (bwrEscForm.IsBusy == false)
            {
                bwrEscForm.RunWorkerAsync();
            }
            else
            {
                //clsBREscForm.RecalculaGrid(clsBREscForm.GetColunas());
            }
        }

        private void dgvEscForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }

        private void dgvEscForm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvEscForm.CurrentRow != null)
                clsInfo.zrow = dgvEscForm.CurrentRow;
            this.Close();
            this.Dispose();
        }

        private void tspRetornar_Click_(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //clsBREscForm.Imprimir(clsInfo.caminhorelatorios, tabela, clsInfo.conexaosqldados);
        }

        private void frmEscFormVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvEscForm);
        }

        private void dgvEscForm_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }       
}
