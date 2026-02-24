using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecasCadastroPes : Form
    {
        
        clsBasicReport clsBr;

        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;
        String tipo = "";

        static DataTable dtPecas;

        static DataTable dtPecasTipo;

        clsPecasCadastroBLL clsPecasCadastroBLL;
        BackgroundWorker bwrPecas;
        BackgroundWorker bwrPecasTipo;
        Boolean carregandoPecas;

        public frmPecasCadastroPes()
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
            clsPecasCadastroBLL = new clsPecasCadastroBLL();
            //clsBr = new clsBasicReport(this, dgvPecas, toolTip1);

            clsInfo.zrowid = 0;

            tstPesquisa.Select();
            tstPesquisa.SelectAll();

        }


        private void frmPecasPes_Load(object sender, EventArgs e)
        {

            tstPesquisa.Select();
            tstPesquisa.SelectAll();

        }
        private void frmPecasPes_Activated(object sender, EventArgs e)
        {
            bwrPecas_Run();
        }

        //private void tspIncluir_Click(object sender, EventArgs e)
        //{
        //    if (dgvPecas.CurrentRow != null)
        //    {
        //        id = clsParser.Int32Parse(dgvPecas.CurrentRow.Cells["id"].Value.ToString());
        //    }
        //    else
        //    {
        //        id = 0;
        //    }
        //    id_Proximo = 0;
        //    id_Anterior = 0;
        //    frmPecas frmPecas = new frmPecas();
        //    frmPecas.Init(0, dgvPecas.Rows);

        //    clsFormHelper.AbrirForm(this.MdiParent, frmPecas, clsInfo.conexaosqldados);
        //}

        //private void tspAlterar_Click(object sender, EventArgs e)
        //{
        //    if (dgvPecas.CurrentRow != null)
        //    {
        //        id = clsParser.Int32Parse(dgvPecas.CurrentRow.Cells["ID"].Value.ToString());

        //        if (dgvPecas.CurrentRow.Index > 0)
        //        {
        //            id_Anterior = clsParser.Int32Parse(dgvPecas.Rows[dgvPecas.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
        //        }
        //        else
        //        {
        //            id_Anterior = 0;
        //        }


        //        if (dgvPecas.CurrentRow.Index < dgvPecas.Rows.Count - 1)
        //        {
        //            id_Proximo = clsParser.Int32Parse(dgvPecas.Rows[dgvPecas.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
        //        }
        //        else
        //        {
        //            id_Proximo = 0;
        //        }

        //        frmPecas frmPecas = new frmPecas();
        //        frmPecas.Init(id, dgvPecas.Rows);

        //        clsFormHelper.AbrirForm(this.MdiParent, frmPecas, clsInfo.conexaosqldados);
        //    }
        //}
        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Cadastro de Materiais / Produtos", clsInfo.conexaosqldados);
        }
        private void tspImprimirMnu_Click(object sender, EventArgs e)
        {
            //frmRelPecasEsquadrias frmRelPecasEsquadrias = new frmRelPecasEsquadrias();
            //frmRelPecasEsquadrias.Init();
            //FormHelper.AbrirForm(this.MdiParent, frmRelPecasEsquadrias, clsInfo.conexaosqldados);
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
                tipo = "";
                dtPecas = clsPecasCadastroBLL.GridDados(ativo, tipo, clsInfo.conexaosqldados);
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
                    //clsPecasCadastroBLL.GridMonta(dgvPecas, dtPecas, clsInfo.zrowid);
                    clsPecasCadastroBLL.ConfigurarGrade(dgvPecas, dtPecas, clsInfo.zrowid);
                    clsGridHelper.FontGrid(dgvPecas, 7);
                    labelQtde.Text = "Total de Itens Cadastrados : " + dgvPecas.RowCount.ToString("N0");
                    //PesquisaRapida();
                    clsGridHelper.SelecionaLinha(id, dgvPecas, 1);
                    tstPesquisa.Select();
                    tstPesquisa.SelectAll();

                }
                carregandoPecas = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoPecas = false;
            }
            tstPesquisa.Select();
            tstPesquisa.SelectAll();
        }

        private void ControlEnter(object sender, EventArgs e)
        {

        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            frmPecasPes_Activated(sender, e);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {

        }



        private void dgvPeca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
             tspEscolher.PerformClick();
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
        private void tstPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tstPesquisa.Focus();
        }

        private void tstPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstPesquisa.Focus();
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvPecas);
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
            try
            {
                clsInfo.zrow = dgvPecas.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Close();
            }

        }

        private void tstPesquisa_Enter(object sender, EventArgs e)
        {
            //Visual.ControlEnter(sender);
        }

        private void tstPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void tstPesquisa_Click(object sender, EventArgs e)
        {

        }

        //
        // Fim - Cadastro PecasTipo

    }
}
