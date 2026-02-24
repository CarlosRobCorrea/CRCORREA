using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmEmpresaVis : Form
    {
        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        static DataTable dtEmpresa;
        clsEmpresaBLL clsEmpresaBLL;
        BackgroundWorker bwrEmpresa;

        Boolean carregandoEmpresa;

        public frmEmpresaVis()
        {
            InitializeComponent();
        }
        public void Init()
        {
            bwrEmpresa = new BackgroundWorker();
            //apenas para permitir a funcionaliose de cancelamento
            bwrEmpresa.WorkerSupportsCancellation = true;
            bwrEmpresa.DoWork += new DoWorkEventHandler(bwrEmpresa_DoWork);
            bwrEmpresa.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrEmpresa_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
            carregandoEmpresa = false;
        }

        private void frmEmpresaVis_Load(object sender, EventArgs e)
        {

        }
        public void EmpresaGrid()
        {
            dtEmpresa = clsEmpresaBLL.GridDados(clsInfo.conexaosqldados);
        }

        private void bwrEmpresa_Run()
        {
            if (carregandoEmpresa == false)
            {
                carregandoEmpresa = true;
                //instanciamos a bwrAbertas e adicionamos os eventos 
                //pbxPeca.Visible = true;
                bwrEmpresa = new BackgroundWorker();
                bwrEmpresa.DoWork += new DoWorkEventHandler(bwrEmpresa_DoWork);
                bwrEmpresa.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrEmpresa_RunWorkerCompleted);
                bwrEmpresa.WorkerSupportsCancellation = true;
                bwrEmpresa.RunWorkerAsync();
            }
        }

        private void bwrEmpresa_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwrEmpresa.CancellationPending)
            {
                //temos de setar e.Cancel com true assim o bwrAbertas_RunWorkerCompleted vai receber o cancelamento
                e.Cancel = true;
                return;
            }
            else
            {
                dtEmpresa = clsEmpresaBLL.GridDados(clsInfo.conexaosqldados);
            }
        }

        private void bwrEmpresa_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //se e.Cancel do bwrAbertas_DoWork recebeu true então foi cancelado a execução do BackgroundWorker
                if (e.Cancelled != true)
                {
                    clsEmpresaBLL.GridMonta(dgvEmpresas, dtEmpresa, clsInfo.zrowid);
                    clsGridHelper.FontGrid(dgvEmpresas, 7);
//                    labelQtde.Text = "Total de Itens Cadastrados : " + dgvEmpresas.RowCount.ToString("N0");
                    PesquisaRapida();
                }
                carregandoEmpresa = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoEmpresa = false;
            }
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvEmpresas);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvEmpresas, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvEmpresas, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvEmpresas, "codigo") == false)
                        {
                            if (dgvEmpresas.Rows.Count > 0)
                            {
                                dgvEmpresas.CurrentCell = dgvEmpresas.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvEmpresas.Rows.Count > 0)
            {
                dgvEmpresas.CurrentCell = dgvEmpresas.Rows[0].Cells["codigo"];
            }

            if (dgvEmpresas.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvEmpresas.CurrentRow.Cells["id"].Value.ToString());
                if (dgvEmpresas.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvEmpresas.Rows[
           dgvEmpresas.CurrentRow.Index - 1].Cells["id"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvEmpresas.CurrentRow.Index < dgvEmpresas.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvEmpresas.Rows[
         dgvEmpresas.CurrentRow.Index + 1].Cells["id"].Value.ToString());
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

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (clsInfo.zusuario.ToString().ToUpper() == "SUPERVISOR")
            {
                frmEmpresa frmEmpresa = new frmEmpresa();
                frmEmpresa.Init(0, dgvEmpresas.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmEmpresa, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Não pode Incluir uma nova Empresa");
            }

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvEmpresas.CurrentRow != null)
            {
                frmEmpresa frmEmpresa = new frmEmpresa();
                frmEmpresa.Init(clsParser.Int32Parse(dgvEmpresas.CurrentRow.Cells["id"].Value.ToString()), dgvEmpresas.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmEmpresa, clsInfo.conexaosqldados);
            }

        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {

        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {

        }

        private void tspRelatorios_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpresaVis_Activated(object sender, EventArgs e)
        {
            bwrEmpresa_Run();
        }

        private void dgvEmpresas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvEmpresas.CurrentRow != null)
            {
                frmEmpresa frmEmpresa = new frmEmpresa();
                frmEmpresa.Init(clsParser.Int32Parse(dgvEmpresas.CurrentRow.Cells["id"].Value.ToString()), dgvEmpresas.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmEmpresa, clsInfo.conexaosqldados);
            }

        }
    }
}