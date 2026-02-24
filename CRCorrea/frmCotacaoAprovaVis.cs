using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;


namespace CRCorrea
{
    public partial class frmCotacaoAprovaVis : Form
    {
        DataTable dtCotacao;
        DataTable dtCotacao1;
        DataTable dtCotacao2;

        clsCotacaoInfo clsCotacaoInfo = new clsCotacaoInfo();
        clsCotacaoBLL clsCotacaoBLL = new clsCotacaoBLL();
        clsCotacao1Info clsCotacao1Info = new clsCotacao1Info();
        clsCotacao1BLL clsCotacao1BLL = new clsCotacao1BLL();
        clsCotacao2Info clsCotacao2Info = new clsCotacao2Info();
        clsCotacao2BLL clsCotacao2BLL = new clsCotacao2BLL();

        public static Int32 id = 0;
        Int32 id_anterior = 0;
        Int32 id_proximo = 0;
        Int32 idCotacao1;

        BackgroundWorker bwrCotacao;
        Boolean carregarCotacao;

        BackgroundWorker bwrCotacaoReprovadas;
        Boolean carregarCotacaoReprovadas;
        DataTable dtCotacaoReprovadas;

        public frmCotacaoAprovaVis()
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

            bwrCotacaoReprovadas = new BackgroundWorker();
            bwrCotacaoReprovadas.WorkerSupportsCancellation = true;
            bwrCotacaoReprovadas.DoWork += new DoWorkEventHandler(bwrCotacaoReprovadas_DoWork);
            bwrCotacaoReprovadas.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCotacaoReprovadas_RunWorkerCompleted);
            carregarCotacaoReprovadas = false;


            clsCotacaoBLL = new clsCotacaoBLL();
            clsCotacao1BLL = new clsCotacao1BLL();
            clsCotacao2BLL = new clsCotacao2BLL();
            
        }

        private void frmCotacaoAprovaVis_Load(object sender, EventArgs e)
        {

        }
        private void frmCotacaoAprovaVis_Activated(object sender, EventArgs e)
        {
            bwrCotacao_Run();
            bwrCotacaoReprovadas_Run();
            Cotacao1Carregar();
            Analisar_Cotacao();
        }

        private void frmCotacaoAprovaVis_Shown(object sender, EventArgs e)
        {
            clsFormHelper.VerificarForm(this, ToolTip, clsInfo.conexaosqldados);
        }
        private void tspAlterar_Click(object sender, EventArgs e)
        {
            //if (dgvCotacao.CurrentRow != null)
            //{
            //    id = (Int32)dgvCotacao.CurrentRow.Cells[0].Value;
            //    frmCompras_Apli frmCompras_Apli = new frmCompras_Apli();
            //    frmCompras_Apli.Init(id, dgvCompras.Rows);
            //    FormHelper.AbrirForm(this.MdiParent, frmCompras_Apli, clsInfo.conexaosqldados);
            //}

        }
        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCotacao);
        }
        private void bwrCotacao_Run()
        {
            if (carregarCotacao == false)
            {
                //FillFiltros();
                //DesabilitaFiltros();
                carregarCotacao = true;
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
                    //rbnTodas.BackColor = SystemColors.Control;
                    //rbnEmAberto.BackColor = SystemColors.Control;
                    //rbnAprova.BackColor = SystemColors.Control;
                    //if (ultimoObjetoFiltroFocado != null)
                    //{
                    //    ultimoObjetoFiltroFocado.Select();
                    //}

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
        public void CotacaoGrid()
        {
            String situacao = "A";  // Apenas as Situções em Aprovação
            dtCotacao = clsCotacaoBLL.GridDados(clsInfo.zfilial, situacao);
        }
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

        private void dgvCotacao_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvCotacao.CurrentRow != null)
            {
                id = (Int32)dgvCotacao.CurrentRow.Cells[0].Value;
                CotacaoCarregar();
                Cotacao1Carregar();
                Analisar_Cotacao();
            }
        }
        private void CotacaoCarregar()
        {
            clsCotacaoInfo = clsCotacaoBLL.Carregar(id, clsInfo.conexaosqldados);
            tbxNumero.Text = clsCotacaoInfo.numero.ToString();
            tbxDataMontagem.Text = clsCotacaoInfo.datamontagem.ToString("dd/MM/yyyy HH:mm");
            tbxDataFechamento.Text = clsCotacaoInfo.datafechamento.ToString("dd/MM/yyyy HH:mm");
            tbxTudoFechado.Text = clsCotacaoInfo.tudofechadoem.ToString("dd/MM/yyyy HH:mm");
            tbxComprador.Text = clsCotacaoInfo.comprador.ToString();
            tbxAutorizante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO where ID=" + clsCotacaoInfo.idautorizante + "");
            tbxMotivoReprovado.Text = clsCotacaoInfo.motivoreprovado;
            tbxRespostaComprador.Text =  clsCotacaoInfo.respostacomprador;
            tbxTermino.Text = clsCotacaoInfo.termino;
            tbxObservacao.Text = clsCotacaoInfo.observar;
            tbxTotalPrevisto.Text = clsCotacaoInfo.totalprevisto.ToString("N2");

            btnDevolver.Visible = true;
            

        }
        private void Cotacao1Carregar()
        {
            //carregando o COTACAO1
            dtCotacao1 = clsCotacao1BLL.GridDados(id, clsInfo.conexaosqldados);
            if (dtCotacao1 == null)
            {
                dtCotacao1 = clsCotacao1BLL.GridDados(0,
                    clsInfo.conexaosqldados);
            }

            clsCotacao1BLL.GridMonta(dgvCotacao1, dtCotacao1, 0);
        }
        private void dgvCotacao1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvCotacao1.CurrentRow != null)
            {
                idCotacao1 = (Int32)dgvCotacao1.CurrentRow.Cells[0].Value;
                Cotacao2Carregar();
            }

        }
        private void dgvCotacao1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvCotacao1.CurrentRow != null)
            {
                idCotacao1 = (Int32)dgvCotacao1.CurrentRow.Cells[0].Value;
                frmCotacaoAprova frmCotacaoAprova = new frmCotacaoAprova();
                frmCotacaoAprova.Init(idCotacao1, dgvCotacao1.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmCotacaoAprova, clsInfo.conexaosqldados);

            }

        }

        //carregando pecas COTACAO2 - Apenas com os fornecedores do item indicado acima
        private void Cotacao2Carregar()
        {
            dtCotacao2 = clsCotacao2BLL.GridDados(idCotacao1, clsInfo.conexaosqldados);
            clsCotacao2BLL.GridMonta(dgvCotacao2, dtCotacao2, 0);
            dgvCotacao2.Columns["PRECO"].DefaultCellStyle.Format = "N4";
            dgvCotacao2.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";

        }
        private void Analisar_Cotacao()
        {
            // Verificar se todos já tem o fornecedor indicado para liberar para Emissão do Pedido de Compra
            Boolean bok = false;
            foreach (DataRow row in dtCotacao1.Rows)
            {
                if (row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Detached)
                {
                    continue;   
                }
                else
                {

                    if (row["fornecedor"].ToString() == clsInfo.zempresacliente_cognome)
                    {
                        bok = false;
                        break;
                    }
                    else
                    {
                        bok = true;
                    }
                }
            }
            if (bok == true)
            {
                btnAprovaCotacao.Visible = true;
            }
            else
            {
                btnAprovaCotacao.Visible = false;
            }

        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            gbxStatusMotivos.Visible = true;
        }

        private void btnEnviarNao_Click(object sender, EventArgs e)
        {
            gbxStatusMotivos.Visible = false;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            gbxStatusMotivos.Visible = false;
            clsCotacaoInfo = clsCotacaoBLL.Carregar(id, clsInfo.conexaosqldados);
            clsCotacaoInfo.motivoreprovado = tbxMotivoReprovado.Text;
            clsCotacaoInfo.respostacomprador = tbxRespostaComprador.Text;
            clsCotacaoInfo.termino = "R";
            clsCotacaoBLL.Alterar(clsCotacaoInfo, clsInfo.conexaosqldados);
            id = 0;  // zera onde estava e volta no topo
            bwrCotacao_Run();
            bwrCotacaoReprovadas_Run();
            Cotacao1Carregar();

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

            if (((Control)sender).Name == tbxMotivoReprovado.Name && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void bwrCotacaoReprovadas_Run()
        {
            if (carregarCotacaoReprovadas == false)
            {
                //FillFiltros();
                //DesabilitaFiltros();
                carregarCotacaoReprovadas = true;
                bwrCotacaoReprovadas.RunWorkerAsync();
            }
        }

        private void bwrCotacaoReprovadas_DoWork(object sender, DoWorkEventArgs e)
        {
            CotacaoReprovadasGrid();
            if (bwrCotacaoReprovadas.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void bwrCotacaoReprovadas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {

                    clsCotacaoBLL.GridMonta(dgvCotacaoReprovadas, dtCotacaoReprovadas, clsInfo.zrowid);
                    PesquisaRapida();
                    //HabilitaFiltros();
                    //rbnTodas.BackColor = SystemColors.Control;
                    //rbnEmAberto.BackColor = SystemColors.Control;
                    //rbnAprova.BackColor = SystemColors.Control;
                    //if (ultimoObjetoFiltroFocado != null)
                    //{
                    //    ultimoObjetoFiltroFocado.Select();
                    //}

                    carregarCotacaoReprovadas = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregarCotacaoReprovadas = false;
            }

        }
        public void CotacaoReprovadasGrid()
        {
            string TipoLista = "A";  // em autorização

            // Vamos verificar pelo campo motivoreprovado (se possui digitação imprime)
            // E que não foi Aprovado definitivamente
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT id, filial, numero, datamontagem, datafechamento, tudofechadoem, comprador, termino, totalprevisto, observar, " +
                    "motivoreprovado, respostacomprador, ano, idautorizante from cotacao WHERE COTACAO.NUMERO > @NUMERO " +
                    " and COTACAO.FILIAL = @FILIAL " +
                    " and COTACAO.TERMINO = @TERMINO " +
                    " ORDER BY COTACAO.NUMERO DESC  ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("NUMERO", SqlDbType.Int).Value = 0;
            sda.SelectCommand.Parameters.Add("TERMINO", SqlDbType.NVarChar).Value = TipoLista;
            sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = clsInfo.zfilial;
            dtCotacaoReprovadas = new DataTable();
            sda.Fill(dtCotacaoReprovadas);
            //dtCotacaoReprovadas = clsCotacaoBLL.GridDados(clsInfo.zfilial, situacao);
        }

        private void btnAprovaCotacao_Click(object sender, EventArgs e)
        {
            // Aprovar esta Cotação para Emissão de Pedido de Compra
            gbxStatusMotivos.Visible = false;
            clsCotacaoInfo = clsCotacaoBLL.Carregar(id, clsInfo.conexaosqldados);
            clsCotacaoInfo.motivoreprovado = tbxMotivoReprovado.Text;
            clsCotacaoInfo.respostacomprador = tbxRespostaComprador.Text;
            clsCotacaoInfo.termino = "O";  //ok pronto para emitir Pedido de Compra
            clsCotacaoBLL.Alterar(clsCotacaoInfo, clsInfo.conexaosqldados);
            id = 0;  // zera onde estava e volta no topo
            bwrCotacao_Run();
            bwrCotacaoReprovadas_Run();
            Cotacao1Carregar();

        }

    }
}
