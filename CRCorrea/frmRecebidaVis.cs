using CRCorreaBLL;
using CRCorreaFuncoes;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRecebidaVis : Form
    {
        String filtro_tipo;
        String filtro_pagdata;

        String datapagDe;
        String datapagAte;      

        //teve esta mudança por usar duas griddados
        String tipo = ""; 
        String pagdata = "";

        
        

        DataTable dtRecebida;
        clsRecebidaBLL clsRecebidaBLL;
        clsFinanceiro clsFinanceiro;

        //precisamos de 3 variaveis id para controlar o ponteiro de pesquisa
        //o id é publico para que no form de registro possa passar para o form de 
        //visualização  o numero do novo id cadastrado, despois de salvar uma inclusão        
        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        //aqui declaramos bwrAbertas com um BackgroundWorker
        //criamos esta variavel para controlar a chamada do BackgroundWorker
        //assim sistema so chama o BackgroundWorker se ele realmente estiver completado

        Boolean carregarRecebida;

        //variavel para controlar o foco dos objetos de filtro
        Control ultimoObjetodeFiltroFocado;

        DialogResult resultado;

        clsBasicReport clsBR;

        Int32 idReceberNFV;
        Int32 idnotafiscal;
        Int32 iddocumento;
        String documento;

        public frmRecebidaVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            //Instanciamos, colocamos os eventos e habilitamos o suporte e cancelamento do BackgroundWorker 
            bwrRecebida = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrRecebida.WorkerSupportsCancellation = true;
            bwrRecebida.DoWork += new DoWorkEventHandler(bwrRecebida_DoWork);
            bwrRecebida.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrRecebida_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
            carregarRecebida = false;
            
            

            datapagDe = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            datapagAte = DateTime.Now.ToString("dd/MM/yyyy");

            tbxDataDe.Text = datapagDe;
            tbxDataAte.Text = datapagAte;         

            clsRecebidaBLL = new clsRecebidaBLL();
            clsFinanceiro = new clsFinanceiro();          
            clsBR = new clsBasicReport(this, dgvRecebida, ttpPagas, gbxRecebida);
        }

        void FillFiltros()
        {
            if (rbnTodos.Checked == true)
            {
                filtro_tipo = "T";              
            }
            else if (rbnApartirdeHoje.Checked == true)
            {
                filtro_tipo = "P"; // futuro                
            }

            if (rbnDataPag.Checked == true)
            {
               filtro_pagdata = "P";
            }
            else if (rbnDataVenc.Checked == true)
            {
                filtro_pagdata = "V";
            } 

            datapagDe = tbxDataDe.Text;
            datapagAte = tbxDataAte.Text;            
        }

        private Boolean FiltroMudancas()
        {
            if (rbnTodos.Checked == true && filtro_tipo != "T")
            {
                return false;
            }
            else if (rbnApartirdeHoje.Checked == true && filtro_tipo != "P")
            {
                return false;
            }
           
            //
            if (rbnDataPag.Checked == true && filtro_pagdata != "P")
            {
                return false;
            }
            else if (rbnDataVenc.Checked == true && filtro_pagdata != "V")
            {
                return false;
            }            

            return true;
        }

        void bwrRecebida_Run()
        {
            //este procedimento foi criado para iniciar um BackgroundWorker 
            //a variavel carregarCliente controla se BackgroundWorker 
            //ja esta executando ou não se == false não esta em execução
            if (carregarRecebida == false)
            {
                pbxRecebida.Visible = true;
                carregarRecebida = true;
                FillFiltros(); //preenchemos a variavel de filtro
                DesabilitaFiltros();//desabilitamos as opções de filtro ate o BackgroundWorker ser completado
                bwrRecebida.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrRecebida_DoWork(object sender, DoWorkEventArgs e)
        {
            //Primeiro puchamos os dados assim se houver algum cancelamento logo a abaixo sera verificado         
            RecebidaGrid();
            //Depois verificamos se houve pedido de cancelamento para o BackgroundWorker
            //OBS: para operaçoes em loop o cancellationPending deve ser verificado dentro do loop
            if (bwrRecebida.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void RecebidaGrid()
        {
            //tipo so é publica no rom devida a este form usar duas griddados
            tipo = "";
            switch (filtro_tipo)
            {
                case "T":
                    tipo = "T";
                    break;
                case "P":
                    tipo = "P";
                    break;
                default:
                    tipo = "";
                    break;
            }

            pagdata = "";
            switch (filtro_pagdata)
            {
                case "P":
                    pagdata = "P";
                    break;
                case "V":
                    pagdata = "V";
                    break;
                default:
                    pagdata = "";
                    break;
            }

            if (pagdata == "P")
            {
                dtRecebida = clsRecebidaBLL.GridDadosRecebidasPorPagamento(tipo, tbxDataDe.Text, tbxDataAte.Text);
            }
            else
            {
                dtRecebida = clsRecebidaBLL.GridDadosRecebidasPorVencimento(tipo, tbxDataDe.Text, tbxDataAte.Text);
            }
        }

        private void bwrRecebida_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                    //logo podemos trabalhar com os objetos da tela
                    if (pagdata == "P")
                    {
                        clsRecebidaBLL.GridMontaRecebidasPorPagamento(dgvRecebida, dtRecebida, id);
                    }
                    else
                    {
                        clsRecebidaBLL.GridMontaRecebidasPorVencimento(dgvRecebida, dtRecebida, id);
                    }

                    pbxRecebida.Visible = false;
                    HabilitaFiltros(); //como o BackgroundWorker se completou habilitamos as oções de filtro
                    PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid

                    //coloca o foco.  Deve ser o ultimo procedimento de foco para um objeto
                    //dentro do RunWorkerCompleted
                    rbnTodos.BackColor = SystemColors.Control;                   
                    rbnApartirdeHoje.BackColor = SystemColors.Control;
                    rbnDataPag.BackColor = SystemColors.Control;
                    rbnDataVenc.BackColor = SystemColors.Control;
                  
                   
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
                carregarRecebida = false;
            }
            catch (Exception ex)
            {
                pbxRecebida.Visible = false;
                HabilitaFiltros();
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                pbxRecebida.Visible = false;
                HabilitaFiltros();
                carregarRecebida = false;
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            //cancela o BackgroundWorker fazendo isso ele set o CancellationPending do BackgroundWorker                                                                             //bwrAbertas_RunWorkerCompleted 
            //e no evento DoWork verifica o pedido de cancelamento e cancela a operação 
            //evitando assim a mensagem de referencia de objeto
            carregarRecebida = true;
            bwrRecebida.CancelAsync();
            bwrRecebida.Dispose();
            this.Close();
            this.Dispose();
        }

        private void frmRecebidaVis_Load(object sender, EventArgs e)
        {
            tbxDataDe.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            tbxDataAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }  
   
        private void frmRecebidaVis_Shown(object sender, EventArgs e)
        {
           
        }

        private void frmRecebidaVis_Activated(object sender, EventArgs e)
        {
            bwrRecebida_Run();
        }             
        
        //public DataTable CarregaGridRecebida()
        //{
        //    try
        //    {
        //        dtRecebida = new DataTable();
        //        if (rbnDataPag.Checked == true)
        //        {   // Por Data de Pagamento
        //            query = "select RECEBIDA.ID, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO, " +
        //                    "RECEBIDA.POSICAOFIM , RECEBIDA.EMISSAO, " +
        //                    "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, RECEBIDA.VENCIMENTO, RECEBIDA.VALOR, " +
        //                    "RECEBIDA01.DATAENVIO, RECEBIDA01.VALOR AS [VALORPAGTO] " +
        //                    "FROM RECEBIDA " +
        //                    "left JOIN RECEBIDA01 ON RECEBIDA.ID = RECEBIDA01.IDDUPLICATA " +
        //                    "left JOIN CLIENTE ON CLIENTE.ID = RECEBIDA.IDCLIENTE  " +
        //                    "left JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBIDA.IDDOCUMENTO  " +
        //                    "left JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBIDA.IDFORMAPAGTO " +
        //                    "WHERE RECEBIDA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
        //            if (rbnTipoP.Checked == true)
        //            {
        //                query = query + " AND RECEBIDA01.DATAOK > " + clsParser.SqlDateTimeFormat(tbxDataDe.Text + " 00:00",true) + " AND RECEBIDA01.DATAOK < " + clsParser.SqlDateTimeFormat(tbxDataAte.Text + " 23:59",true);
                        
        //                // Parte inferior fazia parte do codigo antigo
        //                //query = query + " AND RECEBIDA.VENCIMENTO > " + clsParser.SqlDateTimeFormat(tbxDataDeVenc.Text + " 00:00",true) + " AND RECEBIDA.VENCIMENTO < " + clsParser.SqlDateTimeFormat(tbxDataAteVenc.Text + " 23:59",true);
        //            }
        //        }
        //        else
        //        { // Por data de Vencimento
        //            query = "select RECEBIDA.ID, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO, " +
        //                    "RECEBIDA.POSICAOFIM , RECEBIDA.EMISSAO, " +
        //                    "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, RECEBIDA.VENCIMENTO, RECEBIDA.VALOR " +
        //                    "FROM RECEBIDA " +
        //                    "left JOIN CLIENTE ON CLIENTE.ID = RECEBIDA.IDCLIENTE  " +
        //                    "left JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBIDA.IDDOCUMENTO  " +
        //                    "left JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBIDA.IDFORMAPAGTO " +
        //                    "WHERE RECEBIDA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
        //            if (rbnTipoP.Checked == true)
        //            {
        //               query = query + " AND RECEBIDA.VENCIMENTO > " + clsParser.SqlDateTimeFormat(tbxDataDeVenc.Text + " 00:00",true) + " AND RECEBIDA.VENCIMENTO < " + clsParser.SqlDateTimeFormat(tbxDataAteVenc.Text + " 23:59",true);
        //            }
        //        }
        //        sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
        //        sda.Fill(dtRecebida);
        //        return dtRecebida;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    } 
        //}        

        private void tspVisualizar_Click(object sender, EventArgs e)
        {
            if (dgvRecebida.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvRecebida.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvRecebida.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvRecebida.Rows[dgvRecebida.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvRecebida.CurrentRow.Index < dgvRecebida.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvRecebida.Rows[dgvRecebida.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }

                id = clsParser.Int32Parse(dgvRecebida.CurrentRow.Cells["ID"].Value.ToString());
                frmRecebida frmRecebida = new frmRecebida();
                frmRecebida.Init(id);

                clsFormHelper.AbrirForm(this.MdiParent, frmRecebida, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvRecebida.Select();
            }
        }

        private void DesabilitaFiltros()
        {
            gbxOpcoes.Enabled = false;
            gbxPeriodo.Enabled = false;          
            gbxPor.Enabled = false;
            gbxRecebida.Enabled = false;
        }

        private void HabilitaFiltros()
        {
            gbxOpcoes.Enabled = true;
            gbxPeriodo.Enabled = true;           
            gbxPor.Enabled = true;
            gbxRecebida.Enabled = true;
        }

        private void tspExtornar_Click(object sender, EventArgs e)
        {
                resultado = MessageBox.Show("Deseja Estornar/Cancelar o Pagamento : " +  Environment.NewLine +
                              "Duplicata = " + dgvRecebida.CurrentRow.Cells["DUPLICATA"].Value.ToString() + Environment.NewLine +
                              "Cognome = " + dgvRecebida.CurrentRow.Cells["COGNOME"].Value.ToString() + Environment.NewLine +
                              "Vencimento = " + dgvRecebida.CurrentRow.Cells["VENCIMENTO"].Value.ToString() + Environment.NewLine + 
                              "", "Aplisoft",
                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    idReceberNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDRECEBERNFV from RECEBIDA where id=" + clsParser.Int32Parse(dgvRecebida.CurrentRow.Cells["ID"].Value.ToString())));
                    id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from RECEBIDA where IDRECEBERNFV=" + idReceberNFV + "").ToString());
                    idnotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDNOTAFISCAL from RECEBIDA where ID=" + +clsParser.Int32Parse(dgvRecebida.CurrentRow.Cells["ID"].Value.ToString()) + "").ToString());
                    documento = dgvRecebida.CurrentRow.Cells["DOCUMENTO"].Value.ToString();
                    iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + documento + "'").ToString());

                    if (documento.Length >= 3)
                    {
                        switch (documento.Substring(0, 3))
                        {
                            case "NFE":
                                break;
                            case "NFV":
                                break;
                            case "DES":
                                break;
                            case "CTO":
                                break;
                            default:
                                resultado = DialogResult.No;
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não tem o tipo do Documento ? ");
                        resultado = DialogResult.No;
                    }
                    if (resultado == DialogResult.Yes)
                    {  // efetuar a baixa
                        //using (TransactionScope tse = new TransactionScope())  // dava bug em 23/08/16
                        //{
                            //clsFinanceiro.ExtornoRecebida(documento, idnotafiscal, idReceberNFV, id);
                            clsFinanceiro.GravarNotaRecePaga(documento, idnotafiscal, idReceberNFV, id);
                            bwrRecebida_Run();
                        //    tse.Complete();
                        //}
                    }
                }
        }

        private void dgvRecebida_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspVisualizar.PerformClick();
        }       

        private void tspMnuImprimir_Click(object sender, EventArgs e)
        {
            frmRelReceber frmRelReceber = new frmRelReceber();
            frmRelReceber.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelReceber, clsInfo.conexaosqldados);
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Contas Pagas", clsInfo.conexaosqldados);
        }

        private void rbn_CheckedChanged(object sender, EventArgs e)
        {
            ultimoObjetodeFiltroFocado = (Control)sender;

            if (FiltroMudancas() == false)
            {
                DesabilitaFiltros();
                bwrRecebida_Run();
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }

        void TrataCampos(Control ctl)
        {

            if (ctl is TextBox)
            {
                if (ctl.Name == tbxDataAte.Name)
                {
                    DesabilitaFiltros();
                    bwrRecebida_Run();
                }
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
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
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvRecebida);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvRecebida, "DUPLICATA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvRecebida, "DUPLICATA") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvRecebida, "DUPLICATA") == false)
                        {
                            if (dgvRecebida.Rows.Count > 0)
                            {
                                dgvRecebida.CurrentCell = dgvRecebida.Rows[0].Cells["DUPLICATA"];
                            }
                        }
                    }
                }
            }
            else if (dgvRecebida.Rows.Count > 0)
            {
                dgvRecebida.CurrentCell = dgvRecebida.Rows[0].Cells["DUPLICATA"];
            }

            if (dgvRecebida.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvRecebida.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvRecebida.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvRecebida.Rows[
           dgvRecebida.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvRecebida.CurrentRow.Index < dgvRecebida.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvRecebida.Rows[
         dgvRecebida.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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
