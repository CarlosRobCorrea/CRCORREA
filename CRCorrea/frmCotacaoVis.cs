using CRCorreaFuncoes;
using CRCorreaInfo;
using CRCorreaBLL;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{

    public partial class frmCotacaoVis : Form
    {
        String filtro_status;
        DataTable dtCotacao;

        clsCotacaoInfo clsCotacaoInfo = new clsCotacaoInfo();
        clsCotacaoBLL clsCotacaoBLL = new clsCotacaoBLL();
        clsCotacao1Info clsCotacao1Info = new clsCotacao1Info();
        clsCotacao1BLL clsCotacao1BLL = new clsCotacao1BLL();
        clsCotacao2Info clsCotacao2Info = new clsCotacao2Info();
        clsCotacao2BLL clsCotacao2BLL = new clsCotacao2BLL();


        Control ultimoObjetoFiltroFocado;

        public static Int32 id = 0;
        Int32 id_anterior = 0;
        Int32 id_proximo = 0;
        Int32 idCotacao;

        BackgroundWorker bwrCotacao;
        Boolean carregarCotacao;


        DataTable dtCotacao1;
        DataTable dtCotacao2;
        clsBasicReport clsBR;

        public frmCotacaoVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            bwrCotacao = new BackgroundWorker();
            bwrCotacao.WorkerSupportsCancellation = true;
            bwrCotacao.DoWork += new DoWorkEventHandler(bwrCotacao_DoWork);
            bwrCotacao.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCotacao_RunWorkerCompleted);
            carregarCotacao = false;
            
            clsCotacaoBLL = new clsCotacaoBLL();
            clsCotacao1BLL = new clsCotacao1BLL();
            clsCotacao2BLL = new clsCotacao2BLL();

            //clsBR = new clsBasicReport(this, dgvCotacao, ToolTip);
           
        }

        //void FillFiltros()
        //{
        //    if (rbnTodas.Checked == true)
        //    {
        //        filtro_status = "Abertos";
        //    }
        //    else if (rbnTerminoN.Checked == true)
        //    {
        //        filtro_status = "EmCotacao";
        //    }
        //    else if (rbnTerminoA.Checked == true)
        //    {
        //        filtro_status = "Fechados";
        //    }
        //}

        //private Boolean filtroMudancas()
        //{

        //    if (rbnTodas.Checked == true && filtro_status != "Abertos")
        //    {
        //        filtro_status = "Abertos";
        //        return false;
        //    }
        //    else if (rbnTerminoN.Checked == true && filtro_status != "EmCotacao")
        //    {
        //        filtro_status = "EmCotacao";
        //        return false;
        //    }
        //    else if (rbnTerminoA.Checked == true && filtro_status != "Fechados")
        //    {
        //        filtro_status = "Fechados";
        //        return false;
        //    }
          

        //    return true;

        //}
        private void frmCotacaoVis_Load(object sender, EventArgs e)
        {      
            
        }

        public void CotacaoGrid()
        {
            String situacao = "";  // Todas as Cotações
            if (rbnTerminoA.Checked == true)
            {
                situacao = "A";   // Em Aprovação
            }
            else if (rbnTerminoN.Checked == true)
            {
                situacao = "N";   // Em Andamento
            }
            else if (rbnTerminoR.Checked == true)
            {
                situacao = "R";   // Reprovadas
            }
            else if (rbnTerminoO.Checked == true)
            {
                situacao = "O";   // Ok - Aguarandando Emissão de Pedido de Compra
            }
            else if (rbnTerminoF.Checked == true)
            {
                situacao = "F";   // Encerradas com Pedido de Compras Emitidos
            }

            dtCotacao = clsCotacaoBLL.GridDados(clsInfo.zfilial, situacao);
        }

        private void bwrCotacao_Run()
        {
            if (carregarCotacao == false)
            {      
                //FillFiltros();
                //DesabilitaFiltros();
                carregarCotacao = true;

                bwrCotacao = new BackgroundWorker();
                bwrCotacao.DoWork += new DoWorkEventHandler(bwrCotacao_DoWork);
                bwrCotacao.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCotacao_RunWorkerCompleted);
                //apenas para permitir a funcionalidade de cancelamento
                bwrCotacao.WorkerSupportsCancellation = true;

                bwrCotacao.RunWorkerAsync();
            }
        }

        private void bwrCotacao_DoWork(object sender, DoWorkEventArgs e)
        {
            CotacaoGrid();
            if (bwrCotacao.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void bwrCotacao_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {

                    clsCotacaoBLL.GridMonta(dgvCotacao, dtCotacao, clsInfo.zrowid);
                    PesquisaRapida();
                    //HabilitaFiltros();
                    rbnTodas.BackColor = SystemColors.Control;
                    rbnTerminoN.BackColor = SystemColors.Control;
                    rbnTerminoA.BackColor = SystemColors.Control;
                    if (ultimoObjetoFiltroFocado != null)
                    {
                        ultimoObjetoFiltroFocado.Select();
                    }

                    carregarCotacao = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregarCotacao = false;
            }
          
        }

        //private void DesabilitaFiltros()
        //{
        //    gbxOpcoes.Enabled = false;
        //    gbxCotacao.Enabled = false;
        //    gbxFornecedores.Enabled = false;
        //    gbxItensCotacao.Enabled = false;
        //}

        //private void HabilitaFiltros()
        //{
        //    gbxOpcoes.Enabled = true;
        //    gbxCotacao.Enabled = true;
        //    gbxFornecedores.Enabled = true;
        //    gbxItensCotacao.Enabled = true;
        //}

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCotacao);
            if (id > 0 || id_anterior > 0 || id_proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvCotacao, "NUMERO") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_proximo,
                                   dgvCotacao, "NUMERO") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_anterior,
                                dgvCotacao, "NUMERO") == false)
                        {
                            if (dgvCotacao.Rows.Count > 0)
                            {
                                dgvCotacao.CurrentCell = dgvCotacao.Rows[0].Cells["NUMERO"];
                            }
                        }
                    }
                }
            }
            else if (dgvCotacao.Rows.Count > 0)
            {
                dgvCotacao.CurrentCell = dgvCotacao.Rows[0].Cells["NUMERO"];
            }

            if (dgvCotacao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvCotacao.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvCotacao.CurrentRow.Index > 0)
                {
                    id_anterior = clsParser.Int32Parse(dgvCotacao.Rows[
           dgvCotacao.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_anterior = 0;
                }
                if (dgvCotacao.CurrentRow.Index < dgvCotacao.Rows.Count - 1)
                {
                    id_proximo = clsParser.Int32Parse(dgvCotacao.Rows[
         dgvCotacao.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_proximo = 0;
                }
            }
            else
            {
                id = 0;
                id_anterior = 0;
                id_proximo = 0;
            }
        }

     
    
        //private void rbn_CheckedChanged(object sender, EventArgs e)
        //{
        //    ultimoObjetoFiltroFocado = (Control)sender;

        //    if (filtroMudancas() == false)
        //    {
        //        DesabilitaFiltros();
        //        bwrCotacao_Run();
        //    }
        //}

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

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void tstPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();
        }

        private void tstPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            //cancela o BackgroundWorker fazendo isso ele set o CancellationPending do BackgroundWorker                                                                             //bwrAbertas_RunWorkerCompleted 
            //e no evento DoWork verifica o pedido de cancelamento e cancela a operação 
            //evitando assim a mensagem de referencia de objeto
            carregarCotacao = true;
            bwrCotacao.CancelAsync();
            bwrCotacao.Dispose();
            this.Close();
            this.Dispose();
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {

            if (dgvCotacao.CurrentRow != null)
            {

                id = clsParser.Int32Parse(dgvCotacao.CurrentRow.Cells["ID"].Value.ToString());

                String status = dgvCotacao.CurrentRow.Cells["TERMINO"].Value.ToString();

                if (dgvCotacao.CurrentRow.Index > 0)
                {
                    id_anterior = clsParser.Int32Parse(dgvCotacao.Rows[dgvCotacao.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_anterior = 0;
                }

                if (dgvCotacao.CurrentRow.Index < dgvCotacao.Rows.Count - 1)
                {
                    id_proximo = clsParser.Int32Parse(dgvCotacao.Rows[dgvCotacao.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_proximo = 0;
                }

                if (status != "A")
                {
                    frmCotacao frmCotacao = new frmCotacao();
                    frmCotacao.Init(id, dgvCotacao.Rows);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCotacao, clsInfo.conexaosqldados);
                }
                else
                {
                    MessageBox.Show("Cotação em Aprovação não pode ser alterada !!");
                }
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvCotacao.Select();
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvCotacao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                       dgvCotacao.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
                id = 0;
            }
            id_proximo = 0;
            id_anterior = 0;

            frmCotacao frmCotacao = new frmCotacao();
            frmCotacao.Init(0, dgvCotacao.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmCotacao, clsInfo.conexaosqldados);
        }

        private void frmCotacaoVis_Activated(object sender, EventArgs e)
        {
            bwrCotacao_Run();
           
        }

        private void dgvCotacao_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspCotacaoAlterar.PerformClick();
        }

        private void dgvCotacao_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvCotacao.CurrentRow != null)
            {
                idCotacao = (Int32)dgvCotacao.CurrentRow.Cells[0].Value;
            }
            else
            {
                idCotacao = (Int32)dgvCotacao.Rows[0].Cells[0].Value;
                CotacaoCarregar();
                carregarCotacao1();
                carregarCotacao2();
            }

            if (idCotacao > 0)
            {
                CotacaoCarregar();
                carregarCotacao1();
                carregarCotacao2();
            }
           
        }

        private void carregarCotacao1()
        {
            //carregando o COTACAO1
            dtCotacao1 = clsCotacao1BLL.GridDados(idCotacao, clsInfo.conexaosqldados);            
            if (dtCotacao1 == null)
            {
                dtCotacao1 = clsCotacao1BLL.GridDados(0,
                    clsInfo.conexaosqldados);
            }
          
            clsCotacao1BLL.GridMonta(dgvCotacao1, dtCotacao1, 0);
        }

            //carregando pecas COTACAO2
         private void carregarCotacao2()
         {
            clsCotacao2BLL = new clsCotacao2BLL();
            dtCotacao2 = clsCotacao2BLL.GridDadosTodos(idCotacao, clsInfo.conexaosqldados);
            if (dtCotacao2 == null)
            {
                dtCotacao2 = clsCotacao2BLL.GridDadosTodos(0,
                    clsInfo.conexaosqldados);
            }
              
            // Mostrar os fornecedores na tela
            // Deixar o nome do fornecedor apenas 1 vez
            var idFornecedorAtual = string.Empty;
            var listaOrdenada = dtCotacao2.Select(string.Empty, "IDFORNECEDOR");

            foreach (var item in listaOrdenada)
            {
                if (idFornecedorAtual == string.Empty ||
                    idFornecedorAtual != item["IDFORNECEDOR"].ToString())
                {
                    idFornecedorAtual = item["IDFORNECEDOR"].ToString();

                    continue;
                }

                dtCotacao2.Rows.Remove(item);
            }

            dtCotacao2.AcceptChanges();

            DataColumn dccotacao2_posicao = new DataColumn("cotacao2_posicao", Type.GetType("System.Int32"));
            dtCotacao2.Columns.Add(dccotacao2_posicao);
            clsCotacao2BLL.GridMontaTodos(dgvCotacao2fornecedor, dtCotacao2, 0); 
            // sumir com algumas colunas
            dgvCotacao2fornecedor.Columns["QTDEORCADA"].Visible = false;
            dgvCotacao2fornecedor.Columns["UNID"].Visible = false;
            dgvCotacao2fornecedor.Columns["PRECO"].Visible = false;
            dgvCotacao2fornecedor.Columns["IPI"].Visible = false;
            dgvCotacao2fornecedor.Columns["ICM"].Visible = false;
            dgvCotacao2fornecedor.Columns["TOTALNOTA"].Visible = false;
            dgvCotacao2fornecedor.Columns["CONDPAGTO"].Visible = false;
            dgvCotacao2fornecedor.Columns["PEDIDOCOMPRA"].Visible = false;

        }

         private void CotacaoCarregar()
         {
                 
            clsCotacaoInfo = clsCotacaoBLL.Carregar(idCotacao, clsInfo.conexaosqldados);
            tbxNumero.Text = clsCotacaoInfo.numero.ToString();
            tbxDataMontagem.Text = clsCotacaoInfo.datamontagem.ToString("dd/MM/yyyy");
            tbxDataFechamento.Text = "";
            if (clsCotacaoInfo.datafechamento > clsParser.DateTimeParse("01/01/2000"))
            {
                tbxDataFechamento.Text = clsCotacaoInfo.datafechamento.ToString("dd/MM/yyyy");
            }
            tbxTudoFechado.Text = "";
            if (clsCotacaoInfo.tudofechadoem > clsParser.DateTimeParse("01/01/2000"))
            {
                tbxTudoFechado.Text = clsCotacaoInfo.tudofechadoem.ToString("dd/MM/yyyy");
            }
            tbxComprador.Text = clsCotacaoInfo.comprador.ToString();
            tbxAutorizante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO where ID=" + clsCotacaoInfo.idautorizante + "");
            tbxMotivoReprovado.Text = clsCotacaoInfo.motivoreprovado;
            tbxRespostaComprador.Text = clsCotacaoInfo.respostacomprador;
            tbxTermino.Text = clsCotacaoInfo.termino;
            tbxAno.Text = clsCotacaoInfo.ano.ToString();
            tbxObservacao.Text = clsCotacaoInfo.observar;
            tbxTotalPrevisto.Text = clsCotacaoInfo.totalprevisto.ToString();

            gbxStatusMotivos.Visible = false;
            if (tbxTermino.Text == "R")
            {
                gbxStatusMotivos.Visible = true;
            }
            if (tbxTermino.Text == "O")
            {
                gbxStatusMotivos.Visible = true;
            }
            if (tbxTermino.Text == "F")
            {
                gbxStatusMotivos.Visible = true;
            }

         }

         private void tspCotacaoExcluir_Click(object sender, EventArgs e)
         {
             if (dgvCotacao.CurrentRow != null)
             {
                 id = clsParser.Int32Parse(dgvCotacao.CurrentRow.Cells["ID"].Value.ToString());

                 if (dtCotacao1.Rows.Count == 0)
                 {  // não tem itens na cotação pode apagar

                     DialogResult drt;
                     drt = MessageBox.Show("Deseja mesmo Apagar esta Cotação nro : " + dgvCotacao.CurrentRow.Cells["NUMERO"].Value.ToString() + " ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
                     if (drt == DialogResult.Yes)
                     {
                         String query;
                         SqlConnection scn;
                         SqlCommand scd;
                         scn = new SqlConnection(clsInfo.conexaosqldados);
                         scn.Open();
                         // Apagar as Entregas
                         query = "delete cotacaoentrega where idcotacao = @id";
                         scd = new SqlCommand(query, scn);
                         scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                         scd.ExecuteNonQuery();
                         // Apagar os Itens
                         query = "delete cotacao2 where idcotacao = @id";
                         scd = new SqlCommand(query, scn);
                         scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                         scd.ExecuteNonQuery();
                         // Apagar os Itens
                         query = "delete cotacao1 where idcotacao = @id";
                         scd = new SqlCommand(query, scn);
                         scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                         scd.ExecuteNonQuery();
                         // Apagar o Cabeçalho
                         query = "delete cotacao where id = @id";
                         scd = new SqlCommand(query, scn);
                         scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                         scd.ExecuteNonQuery();
                         scn.Close();
                         bwrCotacao_Run();
                     }
                 }
                 else
                 {
                     MessageBox.Show("Já existe Itens nesta Cotação não pode simplesmente Apagar - Exclua os Itens ");
                 }

             }
             else
             {
                 MessageBox.Show("De um click no Registro!", "Aplisoft", MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
                 dgvCotacao.Select();
             }

         }

         private void rbnTodas_Click(object sender, EventArgs e)
         {
             bwrCotacao_Run();
         }

         private void rbnTerminoN_Click(object sender, EventArgs e)
         {
             bwrCotacao_Run();

         }

         private void rbnTerminoA_Click(object sender, EventArgs e)
         {
             bwrCotacao_Run();
         }

         private void rbnTerminoR_Click(object sender, EventArgs e)
         {
             bwrCotacao_Run();
         }

         private void rbnTerminoO_Click(object sender, EventArgs e)
         {
             bwrCotacao_Run();
         }

         private void rbnTerminoF_CheckedChanged(object sender, EventArgs e)
         {
             bwrCotacao_Run();
         }

         private void dgvCotacao1_MouseDoubleClick(object sender, MouseEventArgs e)
         {
             tspCotacaoAlterar.PerformClick();
         }


    }
}
