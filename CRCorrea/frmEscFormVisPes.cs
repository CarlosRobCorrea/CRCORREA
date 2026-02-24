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
    public partial class frmEscFormVisPes : Form
    {
        private Int32 ixEscForm;
                
        private String conexao;
        private String tabela;
        private object filtro;
        private String campo;
        private String nometabela;

        private DataTable dtEscForm;
      

        //clsBasicReport clsBREscForm;
        clsEscFormBLL clsEscFormBLL;        

        public frmEscFormVisPes()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         String _tabela,
                         Int32 _idpesquisa,
                         String _nometabela)
        {
            tspEscFormVisPes.Cursor = Cursors.Hand;
            
            ixEscForm = _idpesquisa;
            conexao = _conexao;
            tabela = _tabela;
            nometabela = _nometabela;
            filtro = "";
            campo = "";

            
            
            

//            clsBREscForm = new clsBasicReport(this, dgvEscForm, ToolTip);
            clsEscFormBLL = new clsEscFormBLL(tabela);
        }
        public void Init(String _conexao,
                         String _tabela,
                         Int32 _idpesquisa,
                         String _campo,
                         String _filtro)
        {
            tspEscFormVisPes.Cursor = Cursors.Hand;
            ixEscForm = _idpesquisa;
            filtro = _filtro;
            campo = _campo;
           
            conexao = _conexao;
            tabela = _tabela;

            
            
            //clsBREscForm = new clsBasicReport(this, dgvEscForm, ToolTip);
            clsEscFormBLL = new clsEscFormBLL(tabela);
        }

        public void Init(String _conexao,
                         String _tabela,
                         Int32 _idpesquisa,
                         String _campo,
                         int _filtro)
        {
            tspEscFormVisPes.Cursor = Cursors.Hand;
            ixEscForm = _idpesquisa;
            filtro = _filtro;
            campo = _campo;

            conexao = _conexao;
            tabela = _tabela;

            //clsBREscForm = new clsBasicReport(this, dgvEscForm, ToolTip);
            clsEscFormBLL = new clsEscFormBLL(tabela);
        }

        // modificado por williams - 17/04/2009 função modificada para receber um id para filtro apartir de uma lupa
        //
        //

        private void frmEscFormVisPes_Load(object sender, EventArgs e)
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

            bwrEscFormVisPes_RunWorkerAsync();

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

        private void bwrEscFormVisPes_RunWorkerAsync()
        {
            bwrEscFormVisPes = new BackgroundWorker();
            bwrEscFormVisPes.DoWork += new DoWorkEventHandler(bwrEscFormVisPes_DoWork);
            bwrEscFormVisPes.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrEscFormVisPes_RunWorkerCompleted);
            bwrEscFormVisPes.RunWorkerAsync();
        }

        private void bwrEscFormVisPes_DoWork(object sender, DoWorkEventArgs e)
        {
            dtEscForm = CarregaGridEscForm();
        }

        private DataTable CarregaGridEscForm()
        {
            try
            {
                if (filtro.GetType() == typeof(string))
                {
                    return clsEscFormBLL.GridDados(campo, (string)filtro, conexao);
                }
                else if (filtro.GetType() == typeof(int))
                {
                    return clsEscFormBLL.GridDados(campo, (int)filtro, conexao);
                }
                else
                {
                    return clsEscFormBLL.GridDados(conexao);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrEscFormVisPes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
//            ArrayList altEscForm = clsBREscForm.GetColunas();
            dgvEscForm.DataSource = dtEscForm;

            clsGridHelper.MontaGrid(dgvEscForm,
                    new String[] { "Id.", "Código", "Nome" },
                    new String[] { "ID", "CODIGO", "NOME" },
                    new int[] { 0, 300, 684 },
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
            //clsBREscForm.RecalculaGrid(altEscForm);            
            clsGridHelper.SelecionaLinha(ixEscForm, dgvEscForm,1);
            tstbxLocalizar.Focus();

            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvEscForm);
        }

        private void frmEscFormVisPes_Activated(object sender, EventArgs e)
        {
            if (bwrEscFormVisPes.IsBusy == false)
            {
                bwrEscFormVisPes.RunWorkerAsync();
            }
            else
            {
                //clsBREscForm.RecalculaGrid(clsBREscForm.GetColunas());
            }
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvEscForm.CurrentRow != null)
            {
                clsInfo.zrow = dgvEscForm.CurrentRow;
            }
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
            //clsBREscForm.Imprimir(clsInfo.zarquivoreport, tabela);
        }

        private void dgvEscForm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspEscolher.PerformClick();
        }

        
        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvEscForm);
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

        private void frmEscFormVisPes_Shown(object sender, EventArgs e)
        {
            
        }

        private void dgvEscForm_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }        
    }       
}
