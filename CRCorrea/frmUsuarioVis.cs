using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmUsuarioVis : Form
    {
        String filtro_situacao;

        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        String conexao;

        DataTable dtUsuario;

        clsUsuarioBLL clsUsuarioBLL;

        Boolean CarregandoUsuario;
        //variavel para controlar o foco dos objetos de filtro
        Control ultimoObjetodeFiltroFocado;

        BackgroundWorker bwrUsuario;

        clsBasicReport clsBR;

        public frmUsuarioVis()
        {
            InitializeComponent();
        }

        public void Init(string _conexao)
        {
            bwrUsuario = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrUsuario.WorkerSupportsCancellation = true;
            bwrUsuario.DoWork += new DoWorkEventHandler(bwrUsuario_DoWork);
            bwrUsuario.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrUsuario_RunWorkerCompleted);

            conexao = _conexao;
            CarregandoUsuario = false;

            clsUsuarioBLL = new clsUsuarioBLL();

            clsBR = new clsBasicReport(this, dgvUsuario, conexao);
        }

        private void tsbIncluir_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvUsuario.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
                id = 0;
            }
            id_Proximo = 0;
            id_Anterior = 0;

            frmUsuario frmUsuario2 = new frmUsuario();
            frmUsuario2.Init(0, dgvUsuario.Rows, conexao);

            clsFormHelper.AbrirForm(this.MdiParent, frmUsuario2, conexao);
        }

        private void frmUsuarioVis_Load(object sender, EventArgs e)
        {

        }

        private void frmUsuarioVis_Activated(object sender, EventArgs e)
        {
            bwrUsuario_Run();

        }

        private void tstbxProcurar_Click(object sender, EventArgs e)
        {
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvUsuario);

        }

        private void tsbAlterar_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvUsuario.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvUsuario.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvUsuario.Rows[dgvUsuario.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvUsuario.CurrentRow.Index < dgvUsuario.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvUsuario.Rows[dgvUsuario.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }

                frmUsuario frmUsuario = new frmUsuario();
                frmUsuario.Init(id, dgvUsuario.Rows,conexao);

                clsFormHelper.AbrirForm(this.MdiParent, frmUsuario, conexao);
            }

        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuario.CurrentRow != null)
                {
                    id = clsParser.Int32Parse(dgvUsuario.CurrentRow.Cells["ID"].Value.ToString());

                    if (dgvUsuario.CurrentRow.Index > 0)
                    {
                        id_Anterior = clsParser.Int32Parse(dgvUsuario.Rows[dgvUsuario.CurrentRow.Index -
                                       1].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Anterior = 0;
                    }

                    if (dgvUsuario.CurrentRow.Index < dgvUsuario.Rows.Count - 1)
                    {
                        id_Proximo = clsParser.Int32Parse(dgvUsuario.Rows[dgvUsuario.CurrentRow.Index + 1
                                  ].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Proximo = 0;
                    }

                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " + dgvUsuario.Columns[2].HeaderText + " " + dgvUsuario.CurrentRow.Cells[2].Value, Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsUsuarioBLL.Excluir(Int32.Parse(dgvUsuario.CurrentRow.Cells["id"].Value.ToString()),conexao);
                        dgvUsuario.Rows.Remove(dgvUsuario.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsbRetornar_Click(object sender, EventArgs e)
        {
            //cancela o BackgroundWorker fazendo isso ele set o CancellationPending do BackgroundWorker                                                                             //bwrAbertas_RunWorkerCompleted 
            //e no evento DoWork verifica o pedido de cancelamento e cancela a operação 
            //evitando assim a mensagem de referencia de objeto
            CarregandoUsuario = true;
            bwrUsuario.CancelAsync();
            bwrUsuario.Dispose();
            this.Close();
            this.Dispose();
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Usuário",conexao);
        }

        private void tsbEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvUsuario.CurrentRow;
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

        private void dgvUsuario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbAlterar.PerformClick();
        }

        void FillFiltros()
        {
            if (rbnTodos.Checked == true)
            {
                filtro_situacao = "Todos";
            }
            else if (rbnAtivos.Checked == true)
            {
                filtro_situacao = "Ativos";
            }
            else if (rbnInativos.Checked == true)
            {
                filtro_situacao = "Inativos";
            }
        }

        private Boolean FiltroMudancas()
        {
            if (rbnTodos.Checked == true && filtro_situacao != "Todos")
            {
                return false;
            }
            else if (rbnAtivos.Checked == true && filtro_situacao != "Ativos")
            {
                return false;
            }
            else if (rbnInativos.Checked == true && filtro_situacao != "Inativos")
            {
                return false;
            }

            return true;
        }

        private void bwrUsuario_Run()
        {
            //este procedimento foi criado para iniciar um BackgroundWorker 
            //a variavel carregarCliente controla se BackgroundWorker 
            //ja esta executando ou não se == false não esta em execução
            if (CarregandoUsuario == false)
            {
                pbxUsuario.Visible = true;
                CarregandoUsuario = true;
                FillFiltros(); //preenchemos a variavel de filtro
                DesabilitaFiltros();//desabilitamos as opções de filtro ate o BackgroundWorker ser completado
                bwrUsuario.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrUsuario_DoWork(object sender, DoWorkEventArgs e)
        {
            //Primeiro puchamos os dados assim se houver algum cancelamento logo a abaixo sera verificado         
            UsuarioGrid();
            //Depois verificamos se houve pedido de cancelamento para o BackgroundWorker
            //OBS: para operaçoes em loop o cancellationPending deve ser verificado dentro do loop
            if (bwrUsuario.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void UsuarioGrid()
        {
            String situacao = "";
            switch (filtro_situacao)
            {
                case "Ativos":
                    situacao = "S";
                    break;
                case "Inativos":
                    situacao = "N";
                    break;
                default:
                    situacao = "";
                    break;
            }
            dtUsuario = clsUsuarioBLL.GridDados(situacao,conexao);
        }

        private void bwrUsuario_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                    //logo podemos trabalhar com os objetos da tela
                    clsUsuarioBLL.GridMonta(dgvUsuario, dtUsuario, id);

                    pbxUsuario.Visible = false;
                    HabilitaFiltros(); //como o BackgroundWorker se completou habilitamos as oções de filtro
                    PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid

                    //coloca o foco.  Deve ser o ultimo procedimento de foco para um objeto
                    //dentro do RunWorkerCompleted
                    rbnTodos.BackColor = SystemColors.Control;
                    rbnAtivos.BackColor = SystemColors.Control;
                    rbnInativos.BackColor = SystemColors.Control;

                    if (ultimoObjetodeFiltroFocado != null)
                    {
                        ultimoObjetodeFiltroFocado.Select();
                    }

                }
                //carregarCliente  so recebe false agora porque o BackgroundWorker ja 
                //se completou assim ele fica liberado para nova execução
                //Nota no caso de haver pedido de cancelamento o BackgroundWorker não executa o codigo acima 
                //Nota geralmente nos nossos formularios o pedido de cancelamento veem junto com a saida do formulario
                //assim o sistema fecha e não teremos problema de BackgroundWorker em segundo plano chamando os 
                //objetos do form
                CarregandoUsuario = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                CarregandoUsuario = false;
            }
        }

        private void rbn_CheckedChanged(object sender, EventArgs e)
        {
            ultimoObjetodeFiltroFocado = (Control)sender;

            if (FiltroMudancas() == false)
            {
                DesabilitaFiltros();
                bwrUsuario_Run();
            }
        }

        private void DesabilitaFiltros()
        {
            gbxGroup.Enabled = false;
        }

        private void HabilitaFiltros()
        {
            gbxGroup.Enabled = true;
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
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvUsuario);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvUsuario, "USUARIO") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvUsuario, "USUARIO") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvUsuario, "USUARIO") == false)
                        {
                            if (dgvUsuario.Rows.Count > 0)
                            {
                                dgvUsuario.CurrentCell = dgvUsuario.Rows[0].Cells["USUARIO"];
                            }
                        }
                    }
                }
            }
            else if (dgvUsuario.Rows.Count > 0)
            {
                dgvUsuario.CurrentCell = dgvUsuario.Rows[0].Cells["USUARIO"];
            }

            if (dgvUsuario.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvUsuario.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvUsuario.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvUsuario.Rows[
           dgvUsuario.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvUsuario.CurrentRow.Index < dgvUsuario.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvUsuario.Rows[
         dgvUsuario.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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
    }

}
