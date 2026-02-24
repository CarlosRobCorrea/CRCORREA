using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecasVis : Form
    {
        
        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        static DataTable dtPecas;

        static DataTable dtPecasTipo;

        clsPecasBLL clsPecasBLL;
        BackgroundWorker bwrPecas;
        BackgroundWorker bwrPecasTipo;
        Boolean carregandoPecas;

        public frmPecasVis()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idcodigo)
        {
            id = _idcodigo;

            bwrPecas = new BackgroundWorker();
            //apenas para permitir a funcionaliose de cancelamento
            bwrPecas.WorkerSupportsCancellation = true;
            bwrPecas.DoWork += new DoWorkEventHandler(bwrPecas_DoWork);
            bwrPecas.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPecas_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
            carregandoPecas = false;

            clsPecasBLL = new clsPecasBLL();

            clsInfo.zrowid = 0;
        }


        private void frmPecasVis_Load(object sender, EventArgs e)
        {

        }
        private void frmPecasVis_Activated(object sender, EventArgs e)
        {
            bwrPecas_Run();
            tbxPesquisa.Select();
            tbxPesquisa.SelectAll();

        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvPecas.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvPecas.CurrentRow.Cells["id"].Value.ToString());
            }
            else
            {
                id = 0;
            }
            id_Proximo = 0;
            id_Anterior = 0;
            frmPecas frmPecas = new frmPecas();
            frmPecas.Init(0, dgvPecas.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmPecas, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPecas.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvPecas.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvPecas.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvPecas.Rows[dgvPecas.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }


                if (dgvPecas.CurrentRow.Index < dgvPecas.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvPecas.Rows[dgvPecas.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }

                frmPecas frmPecas = new frmPecas();
                frmPecas.Init(id, dgvPecas.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmPecas, clsInfo.conexaosqldados);
            }
        }
        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //clsBr.Imprimir(clsInfo.caminhorelatorios, "Cadastro de Materiais / Produtos", clsInfo.conexaosqldados);
        }
        private void tspImprimirMnu_Click(object sender, EventArgs e)
        {
            frmRelPecas frmRelPecas = new frmRelPecas();
            frmRelPecas.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelPecas, clsInfo.conexaosqldados);
        }
        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bwrPecas_Run()
        {
            if (carregandoPecas == false)
            {
                carregandoPecas = true;
                //instanciamos a bwrAbertas e adicionamos os eventos 
                pbxPeca.Visible = true;
                bwrPecas = new BackgroundWorker();
                bwrPecas.DoWork += new DoWorkEventHandler(bwrPecas_DoWork);
                bwrPecas.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPecas_RunWorkerCompleted);
                //apenas para permitir a funcionalidade de cancelamento
                bwrPecas.WorkerSupportsCancellation = true;

                bwrPecas.RunWorkerAsync();
            }
        }


        private void bwrPecas_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwrPecas.CancellationPending)
            {
                //temos de setar e.Cancel com true assim o bwrAbertas_RunWorkerCompleted vai receber o cancelamento
                e.Cancel = true;
                return;
            }
            else
            {
                String ativo = "";
                if (rbnAtivoSim.Checked == true)
                {
                    ativo = "S";
                }
                else if (rbnAtivoNao.Checked == true)
                {
                    ativo = "N";
                }
                else if (rbnAtivoRevisao.Checked == true)
                {
                    ativo = "R";
                }
                String tipo = "-";
                if (tipo == " ")
                {
                    tipo = "-";
                }
                dtPecas = clsPecasBLL.GridDados(ativo, tipo, clsInfo.conexaosqldados);
            }
        }

        private void bwrPecas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //se e.Cancel do bwrAbertas_DoWork recebeu true então foi cancelado a execução do BackgroundWorker
                if (e.Cancelled != true)
                {
                    pbxPeca.Visible = false;
                    //clsPecasBLL.GridMonta(dgvPecas, dtPecas, clsInfo.zrowid);
                    clsPecasBLL.ConfigurarGrade(dgvPecas, dtPecas, clsInfo.zrowid);
                    clsGridHelper.SelecionaLinha(id, dgvPecas, 1);
                    //clsGridHelper.FontGrid(dgvPecas, 8);
                    labelQtde.Text = "Total de Itens Cadastrados : " + dgvPecas.RowCount.ToString("N0");
                    PesquisaRapida();
                    if (id ==0)
                    {
                        tbxPesquisa.Select();

                    }
                }
                carregandoPecas = false;

                PintarGrid(dgvPecas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoPecas = false;
            }


        }
        public void PintarGrid(DataGridView _datagrid)
        {
            foreach (DataGridViewRow dgvPecasItem in _datagrid.Rows)
            {
                // Destacando itens com cores
                if (clsParser.DecimalParse(dgvPecasItem.Cells["QTDESALDO"].Value.ToString()) < 0)
                {
                    _datagrid.Rows[dgvPecasItem.Index].DefaultCellStyle.BackColor = Color.Salmon;
                }
                else if (clsParser.DecimalParse(dgvPecasItem.Cells["ESTOQUEMIN"].Value.ToString()) > clsParser.DecimalParse(dgvPecasItem.Cells["QTDESALDO"].Value.ToString()))
                {
                    _datagrid.Rows[dgvPecasItem.Index].DefaultCellStyle.BackColor = Color.DarkOrange;
                }
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {

        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            frmPecasVis_Activated(sender, e);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {

        }



        private void dgvPeca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }


        private void rbnAtivoNao_Click(object sender, EventArgs e)
        {
            bwrPecas_Run();
        }

        private void rbnAtivoSim_Click(object sender, EventArgs e)
        {
            bwrPecas_Run();
        }

        private void rbnAtivoTodos_CheckedChanged(object sender, EventArgs e)
        {
            bwrPecas_Run();
        }

        private void dgvPecasTipo_MouseClick(object sender, MouseEventArgs e)
        {
            bwrPecas_Run();
        }

        private void rbnAtivoTodos_MouseClick(object sender, MouseEventArgs e)
        {
            id = 0;
            bwrPecas_Run();
        }

        private void rbnAtivoSim_CheckedChanged(object sender, EventArgs e)
        {
            id = 0;
            bwrPecas_Run();
        }

        private void rbnAtivoNao_CheckedChanged(object sender, EventArgs e)
        {
            id = 0;
            bwrPecas_Run();
        }
        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tbxPesquisa.Focus();
        }

        //private void tbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        //{
        //    PesquisaRapida();
        //    tbxPesquisa.Focus();
        //}

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvPecas);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvPecas, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvPecas, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvPecas, "codigo") == false)
                        {
                            if (dgvPecas.Rows.Count > 0)
                            {
                                dgvPecas.CurrentCell = dgvPecas.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvPecas.Rows.Count > 0)
            {
                dgvPecas.CurrentCell = dgvPecas.Rows[0].Cells["codigo"];
            }

            if (dgvPecas.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvPecas.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvPecas.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvPecas.Rows[
           dgvPecas.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvPecas.CurrentRow.Index < dgvPecas.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvPecas.Rows[
         dgvPecas.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
            }
            else
            {
                id = 0;
                id_Anterior = 0;
                id_Proximo = 0;
            }
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvPecas.CurrentRow != null)
            {
                clsInfo.zrow = dgvPecas.CurrentRow;
            }
            this.Close();
            this.Dispose();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        //
        // Fim - Cadastro PecasTipo

    }
}
