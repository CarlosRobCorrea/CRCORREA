using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmClienteVis : Form
    {
        //Para usar o BackgroundWorker nos formularios da Aplisoft devemos proceder da forma abaixo:
        //1º o BackgroundWorker roda em segundo plano sendo assim o usamos para preencher os grids 
        ///porque isso libera os demais objetos da tela para uso em quanto o grid é preenchido.

        //2º mas se sairmos de um formulario que esta com um BackgroundWorker já rodando devemos 
        //cancela-lo antes de sair do formulario senão teremos a mensagem de 
        //“Referencia de objeto não definida para a Instancia de um objeto”
        //isto porque o BackgroundWorker esta tentando acessar um grid ou qualquer 
        //outro objeto de um form que já foi fechado por this.Close();   

        //3º para isso vamos criar uma instancia do BackgroundWorker diretamente no codigo fonte e 
        //não no modo Design e seguir a tecnica de cancelamento abaixo.

        String filtro_situacao;
        String filtro_tipo;
        
        DataTable dtCliente;
     
        clsClienteBLL clsClienteBLL;

        //precisamos de 3 variaveis id para controlar o ponteiro de pesquisa
        //o id é publico para que no form de registro possa passar para o form de 
        //visualização  o numero do novo id cadastrado, despois de salvar uma inclusão        
        public static Int32 id;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        //aqui declaramos bwrAbertas com um BackgroundWorker
        BackgroundWorker bwrCliente;
        //criamos esta variavel para controlar a chamada do BackgroundWorker
        //assim sistema so chama o BackgroundWorker se ele realmente estiver completado

        Boolean carregarCliente;

        //variavel para controlar o foco dos objetos de filtro
        Control ultimoObjetodeFiltroFocado;

        //clsBasicReport clsBR;

        public frmClienteVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            //Instanciamos, colocamos os eventos e habilitamos o suporte e cancelamento do BackgroundWorker 
            bwrCliente = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrCliente.WorkerSupportsCancellation = true;
            bwrCliente.DoWork += new DoWorkEventHandler(bwrCliente_DoWork);
            bwrCliente.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCliente_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
            carregarCliente = false;
            
            

            clsClienteBLL = new clsClienteBLL();
            //clsBR = new clsBasicReport(this, dgvCliente, toolTip1);
        }

        public void Init(String _tipo,
                         Int32 _id)
        {
            //
            bwrCliente = new BackgroundWorker();
            bwrCliente.WorkerSupportsCancellation = true;
            bwrCliente.DoWork += new DoWorkEventHandler(bwrCliente_DoWork);
            bwrCliente.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCliente_RunWorkerCompleted);
            carregarCliente = false;
            //   
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
            else if (_tipo == "Funcionarios")
            {
                rbnTipoU.Checked = true;
            }

            // Se não é para aparecer favor chamar o frmclientepes
            /*tspIncluir.Visible = false;
            tspAlterar.Visible = false;
            tspExcluir.Visible = false;
            tspImprimir.Visible = false;*/
            //clsBR = new clsBasicReport(this, dgvCliente, toolTip1);
        }


        private void frmClienteVis_Load(object sender, EventArgs e)
        {
            
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
            else if (rbnTipoU.Checked == true)
            {
                filtro_tipo = "Funcionarios";
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
            else if (rbnTipoU.Checked == true && filtro_tipo != "Funcionarios")
            {
                return false;
            }

            return true;
        }

        void bwrCliente_Run()
        {
            //este procedimento foi criado para iniciar um BackgroundWorker 
            //a variavel carregarCliente controla se BackgroundWorker 
            //ja esta executando ou não se == false não esta em execução
            if (carregarCliente == false)
            {
//                pbxCliente.Visible = true;
                carregarCliente = true;
                FillFiltros(); //preenchemos a variavel de filtro
                DesabilitaFiltros();//desabilitamos as opções de filtro ate o BackgroundWorker ser completado
                bwrCliente.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrCliente_DoWork(object sender, DoWorkEventArgs e)
        {
           //Primeiro puchamos os dados assim se houver algum cancelamento logo a abaixo sera verificado         
            ClienteGrid();
            //Depois verificamos se houve pedido de cancelamento para o BackgroundWorker
            //OBS: para operaçoes em loop o cancellationPending deve ser verificado dentro do loop
            if(bwrCliente.CancellationPending)
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
                case "Funcionarios":
                    tipo = "U";
                    break;
                default:
                    tipo = "";
                    break;
            }

            dtCliente = clsClienteBLL.GridDadosCliente(situacao,tipo);
        }

        private void bwrCliente_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                    //logo podemos trabalhar com os objetos da tela
                    clsClienteBLL.GridMontaCliente(dgvCliente,dtCliente,id);                    
                    lblRegistros.Text = "Nº Registros: " + dgvCliente.Rows.GetRowCount(DataGridViewElementStates.Visible);        
                    
                    
                    //pbxCliente.Visible = false;
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
                    rbnTipoU.BackColor = SystemColors.Control;
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
                carregarCliente = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                carregarCliente = false;
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            //cancela o BackgroundWorker fazendo isso ele set o CancellationPending do BackgroundWorker                                                                             //bwrAbertas_RunWorkerCompleted 
            //e no evento DoWork verifica o pedido de cancelamento e cancela a operação 
            //evitando assim a mensagem de referencia de objeto
            carregarCliente = true;
            bwrCliente.CancelAsync();
            bwrCliente.Dispose();
            this.Close();
            this.Dispose();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCliente.CurrentRow != null)
                {
                    id = clsParser.Int32Parse(dgvCliente.CurrentRow.Cells["ID"].Value.ToString());

                    if (dgvCliente.CurrentRow.Index > 0)
                    {
                        id_Anterior = clsParser.Int32Parse(dgvCliente.Rows[dgvCliente.CurrentRow.Index -
                                       1].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Anterior = 0;
                    }

                    if (dgvCliente.CurrentRow.Index < dgvCliente.Rows.Count - 1)
                    {
                        id_Proximo = clsParser.Int32Parse(dgvCliente.Rows[dgvCliente.CurrentRow.Index + 1
                                  ].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Proximo = 0;
                    }

                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " + dgvCliente.Columns[2].HeaderText + " " + dgvCliente.CurrentRow.Cells[2].Value, Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsClienteBLL.Excluir(Int32.Parse(dgvCliente.CurrentRow.Cells["id"].Value.ToString()), clsInfo.conexaosqldados);
                        dgvCliente.Rows.Remove(dgvCliente.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvCliente.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvCliente.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvCliente.Rows[dgvCliente.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvCliente.CurrentRow.Index < dgvCliente.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvCliente.Rows[dgvCliente.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
                frmCliente frmCliente = new frmCliente();
                frmCliente.Init(id, dgvCliente.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmCliente, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvCliente.Select();
            }
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvCliente.CurrentRow;
            
            this.Close();
        }

        private void dgvCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvCliente.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                       dgvCliente.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
                id = 0;
            }
            id_Proximo = 0;
            id_Anterior = 0;

            frmCliente frmCliente = new frmCliente();
            frmCliente.Init(0, dgvCliente.Rows);
            clsFormHelper.AbrirForm(this.MdiParent, frmCliente, clsInfo.conexaosqldados);
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

        private void frmClienteVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmClienteVis_Activated(object sender, EventArgs e)
        {
            //basicamente no activated de um formulario chamamos 
            //uma função que executa nosso BackgroundWorker
            //mas podemos usar esta função em outro locais conforme necessidade
            bwrCliente_Run();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //clsBR.Imprimir(clsInfo.caminhorelatorios, "Cliente", clsInfo.conexaosqldados);            
        }

        private void rbn_CheckedChanged(object sender, EventArgs e)
        {
            ultimoObjetodeFiltroFocado = (Control)sender;

            if (FiltroMudancas() == false)
            {
                DesabilitaFiltros();
                bwrCliente_Run();
            }
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
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvCliente);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvCliente, "numero") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvCliente, "numero") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvCliente, "numero") == false)
                        {
                            if (dgvCliente.Rows.Count > 0)
                            {
                                dgvCliente.CurrentCell = dgvCliente.Rows[0].Cells["numero"];
                            }
                        }
                    }
                }
            }
            else if (dgvCliente.Rows.Count > 0)
            {
                dgvCliente.CurrentCell = dgvCliente.Rows[0].Cells["numero"];
            }

            if (dgvCliente.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvCliente.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvCliente.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvCliente.Rows[
           dgvCliente.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvCliente.CurrentRow.Index < dgvCliente.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvCliente.Rows[
         dgvCliente.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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

        private void tspTool_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
