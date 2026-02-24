using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmClientePes : Form
    {
        String filtro_situacao;
        String filtro_tipo;
        
        DataTable dtCliente;
       
        clsClienteBLL clsClienteBLL;

        //precisamos de 3 variaveis id para controlar o ponteiro de pesquisa
        //o id é publico para que no form de registro possa passar para o form de 
        //visualização  o numero do novo id cadastrado, despois de salvar uma inclusão        
        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        //aqui declaramos bwrAbertas com um BackgroundWorker
        BackgroundWorker bwrClientePes;
        //criamos esta variavel para controlar a chamada do BackgroundWorker
        //assim sistema so chama o BackgroundWorker se ele realmente estiver completado
        
        Boolean carregarClientePes;

        //variavel para controlar o foco dos objetos de filtro
        Control ultimoObjetodeFiltroFocado;

        clsBasicReport clsBR;

        public frmClientePes()
        {
            InitializeComponent();
        }

        public void Init(String _tipo, Int32 _id)
        {
            
            bwrClientePes = new BackgroundWorker();
            bwrClientePes.WorkerSupportsCancellation = true;
            bwrClientePes.DoWork += new DoWorkEventHandler(bwrClientePes_DoWork);
            bwrClientePes.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrClientePes_RunWorkerCompleted);
            carregarClientePes = false;

            id = _id;
            
            

            clsClienteBLL = new clsClienteBLL();

            if (_tipo == "Todos")
            {
                rbnTipo.Checked = true;
            }
            else if (_tipo == "Clientes")
            {
                rbnTipoC.Checked = true;
            }
            else if (_tipo == "Fornecedores")
            {
                rbnTipoF.Checked = true;
            }
            else if (_tipo == "Transportadoras")
            {
                rbnTipoT.Checked = true;
            }
            else if (_tipo == "Vendedores")
            {
                rbnTipoV.Checked = true;
            }

            tspImprimir.Visible = false;
            clsBR = new clsBasicReport(this, dgvClientePes, toolTip1);
        }

        void FillFiltros()
        {
            if (rbnAtivo.Checked == true)
            {
                filtro_situacao = "Todos";
            }
            else if (rbnAtivoS.Checked == true)
            {
                filtro_situacao = "Ativos";
            }
            else if (rbnAtivoN.Checked == true)
            {
                filtro_situacao = "Inativos";
            }

            if (rbnTipo.Checked == true)
            {
                filtro_tipo = "Todos";
            }
            else if (rbnTipoC.Checked == true)
            {
                filtro_tipo = "Clientes";
            }
            else if (rbnTipoF.Checked == true)
            {
                filtro_tipo = "Fornecedores";
            }
            else if (rbnTipoV.Checked == true)
            {
                filtro_tipo = "Vendedores";
            }
            else if (rbnTipoT.Checked == true)
            {
                filtro_tipo = "Transportadoras";
            }           
        }

        private Boolean FiltroMudancas()
        {
            if (rbnAtivo.Checked == true && filtro_situacao != "Todos")
            {
                return false;
            }
            else if (rbnAtivoS.Checked == true && filtro_situacao != "Ativos")
            {
                return false;
            }
            else if (rbnAtivoN.Checked == true && filtro_situacao != "Inativos")
            {
                return false;
            }

            if (rbnTipo.Checked == true && filtro_tipo != "Todos")
            {
                return false;
            }
            else if (rbnTipoC.Checked == true && filtro_tipo != "Clientes")
            {
                return false;
            }
            else if (rbnTipoF.Checked == true && filtro_tipo != "Fornecedores")
            {
                return false;
            }
            else if (rbnTipoV.Checked == true && filtro_tipo != "Vendedores")
            {
                return false;
            }
            else if (rbnTipoT.Checked == true && filtro_tipo != "Transportadoras")
            {
                return false;
            }
            
            return false;
        }

        void bwrClientePes_Run()
        {
            //este procedimento foi criado para iniciar um BackgroundWorker 
            //a variavel carregarCliente controla se BackgroundWorker 
            //ja esta executando ou não se == false não esta em execução
            if (carregarClientePes == false)
            {
                pbxCliente.Visible = true;
                carregarClientePes = true;
                FillFiltros(); //preenchemos a variavel de filtro
                DesabilitaFiltros();//desabilitamos as opções de filtro ate o BackgroundWorker ser completado
                bwrClientePes.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrClientePes_DoWork(object sender, DoWorkEventArgs e)
        {
            //Primeiro puchamos os dados assim se houver algum cancelamento logo a abaixo sera verificado         
            ClienteGrid();
            //Depois verificamos se houve pedido de cancelamento para o BackgroundWorker
            //OBS: para operaçoes em loop o cancellationPending deve ser verificado dentro do loop
            if (bwrClientePes.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void ClienteGrid()
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

            String tipo = "";
            switch (filtro_tipo)
            {
                case "Clientes":
                    tipo = "C";
                    break;
                case "Fornecedores":
                    tipo = "F";
                    break;
                case "Vendedores":
                    tipo = "V";
                    break;
                case "Transportadoras":
                    tipo = "T";
                    break;               
                default:
                    tipo = "";
                    break;
            }

            dtCliente = clsClienteBLL.GridDadosClientePes(situacao,tipo);
        }

        private void bwrClientePes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                    //logo podemos trabalhar com os objetos da tela
                    clsClienteBLL.GridMontaClientePes(dgvClientePes, dtCliente, id);
                    lblRegistros.Text = "Nº Registros: " + dgvClientePes.Rows.GetRowCount(DataGridViewElementStates.Visible);

                    pbxCliente.Visible = false;
                    HabilitaFiltros(); //como o BackgroundWorker se completou habilitamos as oções de filtro
                    PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                    //coloca o foco.  Deve ser o ultimo procedimento de foco para um objeto
                    //dentro do RunWorkerCompleted
                    rbnAtivoS.BackColor = SystemColors.Control;
                    rbnAtivoN.BackColor = SystemColors.Control;
                    rbnAtivo.BackColor = SystemColors.Control;
                    rbnTipo.BackColor = SystemColors.Control;
                    rbnTipoC.BackColor = SystemColors.Control;
                    rbnTipoF.BackColor = SystemColors.Control;
                    rbnTipoV.BackColor = SystemColors.Control;
                    rbnTipoT.BackColor = SystemColors.Control;                   
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
                carregarClientePes = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                carregarClientePes = false;
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            //cancela o BackgroundWorker fazendo isso ele set o CancellationPending do BackgroundWorker                                                                             //bwrAbertas_RunWorkerCompleted 
            //e no evento DoWork verifica o pedido de cancelamento e cancela a operação 
            //evitando assim a mensagem de referencia de objeto
            carregarClientePes = true;
            bwrClientePes.CancelAsync();
            bwrClientePes.Dispose();
            this.Close();
            this.Dispose();
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvClientePes }, true);

            //frmProcurar.ShowDialog();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvClientePes.CurrentRow;
            this.Close();
        }


        private void rbnCheckedChanged(object sender, EventArgs e)
        {
            ultimoObjetodeFiltroFocado = (Control)sender;
            if (FiltroMudancas() == false)
            {
                DesabilitaFiltros();
                bwrClientePes_Run();
            } 
        }

        private void DesabilitaFiltros()
        {
            gbxFiltro.Enabled = false;
            gbxTipo.Enabled = false;
        }

        private void HabilitaFiltros()
        {
            gbxFiltro.Enabled = true;
            gbxTipo.Enabled = true;
        }

        private void frmClientePes_Shown(object sender, EventArgs e)
        {
            
        }
        
        private void frmClientePes_Activated(object sender, EventArgs e)
        {
            bwrClientePes_Run();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Cliente", clsInfo.conexaosqldados);           
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
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvClientePes);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvClientePes, "numero") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvClientePes, "numero") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvClientePes, "numero") == false)
                        {
                            if (dgvClientePes.Rows.Count > 0)
                            {
                                dgvClientePes.CurrentCell = dgvClientePes.Rows[0].Cells["numero"];
                            }
                        }
                    }
                }
            }
            else if (dgvClientePes.Rows.Count > 0)
            {
                dgvClientePes.CurrentCell = dgvClientePes.Rows[0].Cells["numero"];
            }

            if (dgvClientePes.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvClientePes.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvClientePes.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvClientePes.Rows[
           dgvClientePes.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvClientePes.CurrentRow.Index < dgvClientePes.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvClientePes.Rows[
         dgvClientePes.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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

        private void frmClientePes_Load(object sender, EventArgs e)
        {

        }          
    }
}
