using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmReceberVis : Form
    {
        Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        DataTable dtReceber;

        clsReceberBLL clsReceberBLL;

        public static DataTable dtReceberAcumula = new DataTable();
        public static DataRow drReceberAcumula;

        String dataAtualde;
        String dataAtualate;

        String tipoData;
        String tipoLista;
        String tipoConferencia;
        String tipoInadimplecia;
        Int32 idBancoInt;

        String query;

        SqlConnection scn;
        SqlCommand scd;
        SqlDataAdapter sda;

        BackgroundWorker bwrReceberTipoDocumento;

        DialogResult resultado;
        Boolean carregandoReceber;
        Boolean carregandoReceberTipoDocumento;

        //variavel para controlar o foco dos objetos de filtro
        Control ultimoObjetodeFiltroFocado;

        public Decimal valorcalculo;

/*        String tbxVencimento;
        String tbxDataBaixa;
        Decimal tbxValor;
        Decimal tbxValorDesconto;
        Decimal tbxValorJuros;
        Decimal tbxValorMulta;
        Decimal valorcredito;
        String atevencimento;
        Decimal tbxsaldo;
        Int32 totaldias;
        Decimal tbxDiferenca; */

        DataTable dtTabela;
        GridColuna[] dtReceberColunas;

        public frmReceberVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            carregandoReceber = false;
            clsReceberBLL = new clsReceberBLL();
        }

        private void frmReceberVis_Load(object sender, EventArgs e)
        {
            id = 0;
            rbnApartirdeHoje.Checked = true;
            rbnConferidosTodos.Checked = true;
            rbnDataVenc.Checked = true;

            tbxDataAtualDe.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dataAtualde = tbxDataAtualDe.Text;
            dataAtualate = tbxDataAtualDe.Text;

            rbnApartirdeHoje.Select();
            tipoInadimplecia = "Nao";
            ckxInadimplente.Checked = true;
            ckxInadimplente.Text = "Sem os Inadimplentes";

            idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));

            CriarTabelaDocumento();

            dtReceberColunas = new GridColuna[]
            {
                new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("F", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Nro Dup", "DUPLICATA", 65, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("P.", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("P.Fim", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Emissão", "EMISSAO", 65, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Doc", "DOCUMENTO", 35, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Cliente", "COGNOME", 150, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Vendedor", "VENDEDOR_COGNOME", 100, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Telefone", "TELEFONE", 5, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Vencimento", "VENCIMENTO", 65, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Venc. Prev  ", "VENCIMENTOPREV", 65, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Valor Titulo", "VALORRECEBER", 100, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Tipo", "TITULO", 35, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Observa", "OBSERVA", 100, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Enviou", "CHEGOU", 45, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Banco", "BANCO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            };


        }
        private void frmReceberVis_Activated(object sender, EventArgs e)
        {
            try
            {
                  TotalizaContas();
                  bwrReceber_Run();
                  bwrReceberAcumula_RunWorkerAsync();                           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void frmReceberVis_Shown(object sender, EventArgs e)
        {
            clsFormHelper.VerificarForm(this, toolTip1, clsInfo.conexaosqldados);
        }
        
        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            Filtrar();
        }

        private void Filtrar()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvReceber);
            tbxTotalVisualizacao.Text = clsParser.DecimalParse(clsGridHelper.SomaGrid(dgvReceber, "VALORRECEBER").ToString()).ToString("N2");
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }
        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }
        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }
        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            Control ctl = (Control)sender;
            if (ctl is TextBox)
            {
                if (ctl.Name == tbxDataAtualDe.Name || ctl.Name == tbxDataAtualAte.Name)
                {
                    //ultimoObjetodeFiltroFocado = (Control)sender;
                    //if (FiltroMudancas() == false)
                    //{
                    bwrReceber_Run();
                    //}
                    //if (rbnDataVenc.Checked == true)
                    //{
                    //    rbnDataVenc.Select();
                    //}
                    //else 
                    //{
                    //    rbnDataPrev.Select();
                    //}

                }
            }
        }
        private void tspIncluir_Click(object sender, EventArgs e)
        {
            id = 0;
            frmReceber frmReceber = new frmReceber();
            frmReceber.Init(0, dgvReceber.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmReceber, clsInfo.conexaosqldados);

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvReceber.CurrentRow != null)
            {
                id = (Int32)dgvReceber.CurrentRow.Cells[0].Value;
                frmReceber frmReceber = new frmReceber();
                frmReceber.Init(id, dgvReceber.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmReceber, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvReceber.Select();
            }

        }
        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //clsBr.Imprimir(clsInfo.caminhorelatorios, "Contas a Receber", clsInfo.conexaosqldados);
        }
        private void tspMnuImprimir_Click(object sender, EventArgs e)
        {
            frmRelReceber frmRelReceber = new frmRelReceber();
            frmRelReceber.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelReceber, clsInfo.conexaosqldados);
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            resultado = MessageBox.Show("Deseja Excluir a Duplicata : " + dgvReceber.CurrentRow.Cells["DUPLICATA"].Value.ToString() + " - " + dgvReceber.CurrentRow.Cells["COGNOME"].Value.ToString() + " )", "Aplisoft",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {
                id = clsParser.Int32Parse(dgvReceber.CurrentRow.Cells["ID"].Value.ToString());
                Decimal Valor;

                Valor = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORPAGO from RECEBER where ID = " + id, "0"));
                //Valor = clsParser.DecimalParse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "RECEBER", "VALORPAGO", "ID", id.ToString()));

                if (Valor > 0)
                {
                    MessageBox.Show("Duplicata não pode ser excluida pois já houve Pagamento !!!");
                }
                else
                {
                    Valor = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORDEVOLVIDO from RECEBER where ID = " + id, "0"));
                    //Valor = clsParser.DecimalParse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "RECEBER", "VALORDEVOLVIDO", "ID", id.ToString()));
                    if (Valor > 0)
                    {
                        MessageBox.Show("Duplicata não pode ser excluida pois tem mercadoria Devolvida !!!");
                    }
                    else
                    {
                        // apagar o contas a pagar observação + clsPagas01 + pagar
                        // Apagar observação do pagar
                        SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                        SqlCommand scd = new SqlCommand("delete RECEBEROBSERVA where idduplicata=@id", scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                        // Apagar os itens do pagar
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("delete RECEBER01 where idduplicata=@id", scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                        // Apagar o pagar
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("delete RECEBER where id=@id", scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();

                        MessageBox.Show("Processo de Exclusão concluido");
                        bwrReceber_Run();
                        bwrReceberAcumula_RunWorkerAsync();
                    }
                }
            }

        }
        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        void FillFiltros()
        {
            if (rbnTodos.Checked == true)
            {
                tipoLista = "T";
                tbxDataAtualDe.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tbxDataAtualAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                gbxData.Visible = false;
            }
            else if (rbnApartirdeHoje.Checked == true)
            {
                tipoLista = "H"; // futuro
                tbxDataAtualDe.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tbxDataAtualAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                gbxData.Visible = false;
            }
            else if (rbnAtrasados.Checked == true)
            {
                tipoLista = "A"; // Atraso
                tbxDataAtualDe.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tbxDataAtualAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                gbxData.Visible = false;
            }
            else if (rbnUmDia.Checked == true)
            {
                // mantem a data atual que digitou na tela
                tipoLista = "D"; // Apenas do dia marcado
                gbxData.Visible = true;
                if (tbxDataAtualDe.Text == "")
                {
                    tbxDataAtualDe.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    tbxDataAtualAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }

            if (rbnConferidosTodos.Checked == true)
            {
                tipoConferencia = "T";
            }
            else if (rbnConferidos.Checked == true)
            {
                tipoConferencia = "S";
            }
            else if (rbnDataPrev.Checked == true)
            {
                tipoConferencia = "N";
            }

            // Ordem da Lista
            if (rbnDataVenc.Checked == true)
            {
                tipoData = "VENC";  // Por data de vencimento
            }
            else if (rbnDataPrev.Checked == true)
            {
                tipoData = "PREV";  // Por data prevista
            }

            // Inadimplencia
            if (ckxInadimplente.Checked == true)
            {
                tipoInadimplecia = "Sim";  // Se os Inadimplentes
            }
            else 
            {
                tipoInadimplecia = "Nao";  // Com os Inadimplentes
            }


        }

        private Boolean FiltroMudancas()
        {
            if (rbnTodos.Checked == true && tipoLista != "T")
            {
                return false;
            }
            else if (rbnApartirdeHoje.Checked == true && tipoLista != "H")
            {
                return false;
            }
            else if (rbnAtrasados.Checked == true && tipoLista != "A")
            {
                return false;
            }
            else if (rbnUmDia.Checked == true && tipoLista != "D")
            {
                return false;
            }
            //
            if (rbnConferidosTodos.Checked == true && tipoConferencia != "T")
            {
                return false;
            }
            else if (rbnConferidos.Checked == true && tipoConferencia != "S")
            {
                return false;
            }
            else if (rbnConferidosNao.Checked == true && tipoConferencia != "N")
            {
                return false;
            }


            //
            if (rbnDataVenc.Checked == true && tipoData != "VENC")
            {
                return false;
            }
            else if (rbnDataPrev.Checked == true && tipoData != "PREV")
            {
                return false;
            }
            //

            return true;
        }
        private void rbnTodos_Click(object sender, EventArgs e)
        {
            ultimoObjetodeFiltroFocado = (Control)sender;

            if (FiltroMudancas() == false)
            {
                bwrReceber_Run();
            }
        }

        private void bwrReceber_Run()
        {
            //este procedimento foi criado para iniciar um BackgroundWorker 
            //a variavel carregandoReceber controla se BackgroundWorker 
            //ja esta executando ou não se == false não esta em execução
            if (carregandoReceber == false && carregandoReceberTipoDocumento == false)
            {
                pbxReceber.Visible = true;
                carregandoReceber = true;
                FillFiltros(); //preenchemos a variavel de filtro
                DesabilitaFiltros();//desabilitamos as opções de filtro ate o BackgroundWorker ser completado
//                bwrReceber.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
                bwrReceber = new BackgroundWorker();
                bwrReceber.DoWork += new DoWorkEventHandler(bwrReceber_DoWork);
                bwrReceber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceber_RunWorkerCompleted);
                bwrReceber.RunWorkerAsync();

            }

/*
            if (carregandoReceber == false)
            {
                carregandoReceber = true;
                pbxReceber.Visible = true;
                bwrReceber = new BackgroundWorker();
                bwrReceber.DoWork += new DoWorkEventHandler(bwrReceber_DoWork);
                bwrReceber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceber_RunWorkerCompleted);
                bwrReceber.RunWorkerAsync();
            }
 */
        }
        private void bwrReceber_DoWork(object sender, DoWorkEventArgs e)
        {
            //Primeiro puchamos os dados assim se houver algum cancelamento logo a abaixo sera verificado         
            ReceberGrid();
            //Depois verificamos se houve pedido de cancelamento para o BackgroundWorker
            //OBS: para operaçoes em loop o cancellationPending deve ser verificado dentro do loop
            if (bwrReceber.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        public void ReceberGrid()
        {
            Int32 Filial = clsInfo.zfilial;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT RECEBER.ID AS [ID], RECEBER.FILIAL, RECEBER.DUPLICATA, RECEBER.POSICAO , RECEBER.POSICAOFIM, RECEBER.EMISSAO, " +
                //"DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, NFVENDA.COGNOME AS [CLIENTE_COGNOME], NFVENDA.TELEFONE, RECEBER.VENCIMENTO, RECEBER.VENCIMENTOPREV, " +
                "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, VENDEDOR.COGNOME AS [VENDEDOR_COGNOME], '' as TELEFONE, RECEBER.VENCIMENTO, RECEBER.VENCIMENTOPREV, " +
                "(RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO) AS [VALORRECEBER], " +
                "SITUACAOTIPOTITULO.CODIGO AS [TITULO], RECEBER.BOLETONRO, " + 
                "RECEBER.OBSERVA AS [OBSERVA], RECEBER.CHEGOU, " +
                "TAB_BANCOS.COGNOME AS [BANCO], RECEBER.IDNOTAFISCAL, RECEBER.DESPESAPUBLICA " +
                "FROM RECEBER " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = RECEBER.IDCLIENTE  " +
                "LEFT JOIN CLIENTE AS VENDEDOR ON VENDEDOR.ID = RECEBER.IDVENDEDOR  " +
                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBER.IDDOCUMENTO  " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBER.IDFORMAPAGTO  " +
                "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID = RECEBER.IDBANCO  " +
                "LEFT JOIN NFVENDA ON NFVENDA.ID = RECEBER.IDNOTAFISCAL ";
            
            dataAtualde = tbxDataAtualDe.Text;
            dataAtualate = tbxDataAtualAte.Text;

            //FILIAL
            if (Filial > 0)
            { // filial especifica
                filtro = "RECEBER.FILIAL = @FILIAL ";
            }
            if (rbnConferidosTodos.Checked == true)
            {
                // Todos Conferidos e não conferidos
            }
            else if (rbnConferidos.Checked == true)
            {
                // Só os Conferidos
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "RECEBER.DESPESAPUBLICA = 'S' ";
                //ckxDespesaPublica
            }
            else
            {
                // Só os Não Conferidos
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "RECEBER.DESPESAPUBLICA = 'N' ";
            }
            //vencimento
            if (tipoData == "VENC")
            {
                if (tipoLista == "F") // Futuro
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                }
                else if (tipoLista == "H")  // A partir de Hoje
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                }
                else if (tipoLista == "A")  // Atraso
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTO < " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                }
                else if (tipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                    filtro = filtro + "AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(dataAtualate + " 23:59", true);
                }
            }
            else
            { // DT PREVISTA
                if (tipoLista == "F")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + " RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                }
                else if (tipoLista == "H")  // A partir de Hoje
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                }
                else if (tipoLista == "A")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + " RECEBER.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                }
                else if (tipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true);
                    filtro = filtro + "AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataAtualate + " 23:59", true);
                }

            }
            // Inadimplentes
            if (tipoInadimplecia == "Sim")
            {
                filtro = filtro + "AND RECEBER.IDBANCOINT != " + idBancoInt; 
            }
            else
            {
                // não declarar
            }
            if (filtro.Length > 2)
            {
                filtro = " WHERE " + filtro;
            }

            //ordenação
            if (tipoData == "VENC")
            {
                filtro = filtro + " ORDER BY RECEBER.VENCIMENTO, RECEBER.DUPLICATA ";
            }
            else
            {
                filtro = filtro + " ORDER BY RECEBER.VENCIMENTOPREV, RECEBER.DUPLICATA ";
            }

            query = query + " " + filtro;

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@Filial", SqlDbType.Int).Value = Filial;
            dtReceber = new DataTable();
            sda.Fill(dtReceber);
        }
        private void bwrReceber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                pbxReceber.Visible = false;
                if (clsInfo.zempresacliente_cognome == "FERPAL")
                {
                    dgvReceber.DataSource = dtReceber;
                    clsGridHelper.MontaGrid2(dgvReceber, dtReceberColunas, true);

                    //clsReceberBLL.GridMonta(dgvReceber, dtReceber, clsInfo.zrowid);
                }
                else
                {
                    clsReceberBLL.GridMonta(dgvReceber, dtReceber, clsInfo.zrowid);
                }
                clsGridHelper.FontGrid(dgvReceber, 7);
                clsGridHelper.SelecionaLinha(id, dgvReceber, 1);
                tbxTotalVisualizacao.Text = clsParser.DecimalParse(clsGridHelper.SomaGrid(dgvReceber, "VALORRECEBER").ToString()).ToString("N2");
                carregandoReceber = false;


                // Mostrar o Acumulado por Tipo de Documento
                bwrReceberTipoDocumento_Run();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceber = false;
            }
        }
        private void dgvReceber_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }
        private void TotalizaContas()
        {
            tbxAReceberVencido.Text = "0,00";
            tbxAReceberNoMes.Text = "0,00";
            tbxAReceber90.Text = "0,00";
            tbxAReceberTudo.Text = "0,00";

            //calcula tudo 
            // VENCIDO
            scn = new SqlConnection(clsInfo.conexaosqldados);
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) FROM RECEBER " +
                        " WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND RECEBER.VENCIMENTO < " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);
            }
            else
            {
                query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) FROM RECEBER " +
                        " WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND RECEBER.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);
            }

            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAReceberVencido.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // A RECEBER NO DIA
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) FROM RECEBER " +
                        " WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true) + " AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 23:59", true);
            }
            else
            {
                query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) FROM RECEBER " +
                        " WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true) + " AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 23:59", true);
            }
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAReceberNoMes.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // A RECEBER NOS PROXIMOS 90 DIAS DATEADD(DD, 30, '2006-06-03 16:16:57.670')
            String dataatual90 = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) FROM RECEBER " +
                        " WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true) +
                        " AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
            }
            else
            {
                query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) FROM RECEBER " +
                        " WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true) +
                        " AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
            }
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAReceber90.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // TUDO A SER PAGO
            query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) FROM RECEBER " +
                    " WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + "";
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAReceberTudo.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();

        }

        private void rbnAcumulaVencido_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrReceberAcumula_RunWorkerAsync();
        }

        private void rbnAcumulaDia_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrReceberAcumula_RunWorkerAsync();

        }

        private void rbnAcumula90_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrReceberAcumula_RunWorkerAsync();
        }

        private void rbnAcumulaTudo_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrReceberAcumula_RunWorkerAsync();
        }

        private void bwrReceberAcumula_RunWorkerAsync()
        {
            if (rbnAcumulaVencido.Checked == true)
            {
                valorcalculo = clsParser.DecimalParse(tbxAReceberVencido.Text) * 100;
            }
            else if (rbnAcumulaDia.Checked == true)
            {
                valorcalculo = clsParser.DecimalParse(tbxAReceberNoMes.Text) * 100;
            }
            else if (rbnAcumula90.Checked == true)
            {
                valorcalculo = clsParser.DecimalParse(tbxAReceber90.Text) * 100;
            }
            else
            {
                valorcalculo = clsParser.DecimalParse(tbxAReceberTudo.Text) * 100;
            }

            pbxReceberAcumula.Visible = true;
            bwrReceberAcumula = new BackgroundWorker();
            bwrReceberAcumula.DoWork += new DoWorkEventHandler(bwrReceberAcumula_DoWork);
            bwrReceberAcumula.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberAcumula_RunWorkerCompleted);
            bwrReceberAcumula.RunWorkerAsync();
        }
        private void bwrReceberAcumula_DoWork(object sender, DoWorkEventArgs e)
        {
            dtReceberAcumula = CarregaGridReceberAcumula();
        }
        public DataTable CarregaGridReceberAcumula()
        {
            try
            {
                String query;
                //Montar a clsVisualização por Fornecedor com Porcentagem
                dtReceberAcumula = new DataTable();
                query = "SELECT CLIENTE.COGNOME, " +
                        "SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) AS [SALDODEVEDOR], " +
                        "(((SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) * 100) / CONVERT(DECIMAL(12,2), '" + valorcalculo.ToString("0.00").Replace(',', '.') + "')) * 100) AS PORCENTAGEM " +
                        "from RECEBER " +
                        "LEFT JOIN CLIENTE ON CLIENTE.ID=RECEBER.IDCLIENTE " +
                        "WHERE RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
                if (rbnAcumulaVencido.Checked == true)
                {
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND RECEBER.VENCIMENTO < " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);
                    }
                    else
                    {
                        query = query + " AND RECEBER.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);

                    }
                }
                else if (rbnAcumulaDia.Checked == true)
                {
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);
                        query = query + " AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 23:59", true);
                    }
                    else
                    {
                        query = query + " AND RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);
                        query = query + " AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 23:59", true);
                    }

                }
                else if (rbnAcumula90.Checked == true)
                {
                    String dataatual90 = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);
                        query = query + " AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
                    }
                    else
                    {
                        query = query + " AND RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtualDe.Text + " 00:00", true);
                        query = query + " AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
                    }
                }
                else
                {
                    //tudo não precisa filtrar 
                }
                // if (rbnAcumulaVencido.Checked == true)
                // { // o que esta vencido
                query = query + " GROUP BY CLIENTE.COGNOME";
                // } 
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtReceberAcumula);
                return dtReceberAcumula;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrReceberAcumula_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                dgvReceberAcumula.DataSource = dtReceberAcumula;
                clsGridHelper.FontGrid(dgvReceberAcumula, 8);
                pbxReceberAcumula.Visible = false;
                dgvReceberAcumula.DataSource = dtReceberAcumula;
                clsGridHelper.MontaGrid(dgvReceberAcumula,
                        new String[] { "Fornecedor", "Saldo Devedor", "%" },
                        new String[] { "COGNOME", "SALDODEVEDOR", "PORCENTAGEM" },
                        new int[] { 150, 120, 65 },
                        new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight},
                        new bool[] { true, true, true },
                        true, 1, ListSortDirection.Ascending);


                dgvReceberAcumula.Columns["SALDODEVEDOR"].DefaultCellStyle.Format = "N2";
                dgvReceberAcumula.Columns["PORCENTAGEM"].DefaultCellStyle.Format = "N2";

                clsGridHelper.FontGrid(dgvReceberAcumula, 8);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void tbxDataAtual_TextChanged(object sender, EventArgs e)
        {

        }
        private void CriarTabelaDocumento()
        {
            dtTabela = new DataTable();
            dtTabela.Columns.Add("Tipo", Type.GetType("System.String"));
            dtTabela.Columns.Add("ValorConferido", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("ValoraConferir", Type.GetType("System.Decimal"));
        }

        private void bwrReceberTipoDocumento_Run()
        {
            if (carregandoReceberTipoDocumento == false)
            {
                carregandoReceberTipoDocumento = true;
                //instanciamos a bwrAbertas e adicionamos os eventos 
                bwrReceberTipoDocumento = new BackgroundWorker();
                bwrReceberTipoDocumento.DoWork += new DoWorkEventHandler(bwrReceberTipoDocumento_DoWork);
                bwrReceberTipoDocumento.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberTipoDocumento_RunWorkerCompleted);
                //apenas para permitir a funcionalidade de cancelamento
                bwrReceberTipoDocumento.WorkerSupportsCancellation = true;

                bwrReceberTipoDocumento.RunWorkerAsync();
            }
        }

        private void bwrReceberTipoDocumento_DoWork(object sender, DoWorkEventArgs e)
        {
            //dtGrupo = clsPecasclassificaBLL.GridDados(clsInfo.conexaosqldados);
            // Acumular por tipo de documento
            // Apagar o Acumulado por Documento
            for (int x = 0; x < dtTabela.Rows.Count; x++)
            {
                dtTabela.Rows[x].Delete();
                dtTabela.AcceptChanges();
            }
            // Acumular o Contas a Receber
            foreach (DataRow rowReceber in dtReceber.Rows)
            {
                // Localizar se Tem o Tipo já cadastrado
                Boolean OK = true;
                foreach (DataRow row in dtTabela.Rows)
                {
                    if (row["TIPO"].ToString() == rowReceber["TITULO"].ToString())
                    {
                        if (rowReceber["DESPESAPUBLICA"].ToString() == "S")
                        {
                            row["VALORCONFERIDO"] = clsParser.DecimalParse(row["VALORCONFERIDO"].ToString()) + clsParser.DecimalParse(rowReceber["VALORRECEBER"].ToString());
                        }
                        else
                        {
                            row["VALORACONFERIR"] = clsParser.DecimalParse(row["VALORACONFERIR"].ToString()) + clsParser.DecimalParse(rowReceber["VALORRECEBER"].ToString());
                        }
                        OK = false;
                        break;
                    }
                }
                if (OK == true)
                {
                    DataRow row1 = dtTabela.NewRow();
                    row1["TIPO"] = rowReceber["TITULO"];
                    if (rowReceber["DESPESAPUBLICA"].ToString() == "S")
                    {
                        row1["VALORCONFERIDO"] = clsParser.DecimalParse(rowReceber["VALORRECEBER"].ToString());
                        row1["VALORACONFERIR"] = 0;

                   }
                    else
                    {
                        row1["VALORCONFERIDO"] = 0;
                        row1["VALORACONFERIR"] = clsParser.DecimalParse(rowReceber["VALORRECEBER"].ToString());
                    }
                    dtTabela.Rows.Add(row1);
                }
            }
            dtTabela.AcceptChanges();


        }

        private void bwrReceberTipoDocumento_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                //logo podemos trabalhar com os objetos da tela
                dgvReceberTipoDocumento.DataSource = dtTabela;
                clsGridHelper.MontaGrid(dgvReceberTipoDocumento,
                        new String[] { "Tipo", "Vl a Conferir", "Vl Conferido" },
                        new String[] { "TIPO", "VALORACONFERIR", "VALORCONFERIDO" },
                        new int[] { 80, 90, 90 },
                        new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight},
                        new bool[] { true, true, true },
                        true, 1, ListSortDirection.Ascending);

                dgvReceberTipoDocumento.Columns["VALORACONFERIR"].DefaultCellStyle.Format = "N2";
                dgvReceberTipoDocumento.Columns["VALORCONFERIDO"].DefaultCellStyle.Format = "N2";

                clsGridHelper.FontGrid(dgvReceberTipoDocumento, 8);

                //pbxReceberTipoDocumento.Visible = false;
                HabilitaFiltros(); //como o BackgroundWorker se completou habilitamos as oções de filtro
                PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid

                //coloca o foco.  Deve ser o ultimo procedimento de foco para um objeto
                //dentro do RunWorkerCompleted
                rbnTodos.BackColor = SystemColors.Control;
                rbnAtrasados.BackColor = SystemColors.Control;
                rbnApartirdeHoje.BackColor = SystemColors.Control;
                rbnUmDia.BackColor = SystemColors.Control;
                rbnConferidosTodos.BackColor = SystemColors.Control;
                rbnConferidos.BackColor = SystemColors.Control;
                rbnConferidosNao.BackColor = SystemColors.Control;
                tbxDataAtualDe.BackColor = SystemColors.Control;
                tbxDataAtualAte.BackColor = SystemColors.Control;
                rbnDataVenc.BackColor = SystemColors.Control;
                rbnDataPrev.BackColor = SystemColors.Control;

                if (ultimoObjetodeFiltroFocado != null)
                {
                    ultimoObjetodeFiltroFocado.Select();
                }
                carregandoReceberTipoDocumento = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceberTipoDocumento = false;
            }
        }
        private void DesabilitaFiltros()
        {
            gbxConferidos.Enabled = false;
            gbxOpcoes.Enabled = false;
            gbxData.Enabled = false;
            gbxPorData.Enabled = false;
        }

        private void HabilitaFiltros()
        {
            gbxConferidos.Enabled = true;
            gbxOpcoes.Enabled = true;
            gbxData.Enabled = true;
            gbxPorData.Enabled = true;
        }

        private void DesabilitaFiltrosAcumula()
        {
            gbxAcumulaOp.Enabled = false;
        }

        private void HabilitaFiltrosAcumula()
        {
            gbxAcumulaOp.Enabled = true;
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvReceber);
            carregandocores();
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvReceber, "DUPLICATA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvReceber, "DUPLICATA") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvReceber, "DUPLICATA") == false)
                        {
                            if (dgvReceber.Rows.Count > 0)
                            {
                                dgvReceber.CurrentCell = dgvReceber.Rows[0].Cells["DUPLICATA"];
                            }
                        }
                    }
                }
            }
            else if (dgvReceber.Rows.Count > 0)
            {
                dgvReceber.CurrentCell = dgvReceber.Rows[0].Cells["DUPLICATA"];
            }

            if (dgvReceber.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvReceber.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvReceber.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvReceber.Rows[
           dgvReceber.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvReceber.CurrentRow.Index < dgvReceber.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvReceber.Rows[
         dgvReceber.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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
        public void carregandocores()
        {
            // Colocando as Cores nos Itens
            for (Int32 X = 0; X < dgvReceber.Rows.Count; X++)
            {
                switch (dgvReceber.Rows[X].Cells["DESPESAPUBLICA"].Value.ToString())
                {
                    case "S":
                        dgvReceber.Rows[X].DefaultCellStyle.BackColor = Color.White;
                        break;
                    default:    // > 11 e < 22
                        dgvReceber.Rows[X].DefaultCellStyle.BackColor = Color.Aquamarine;
                        break;
                }
            }
        }

        private void ckxInadimplente_Click(object sender, EventArgs e)
        {
            if (ckxInadimplente.Checked == true)
            {
                ckxInadimplente.Text = "Sem os Inadimplentes";
            }
            else
            {
                ckxInadimplente.Text = "Com os Inadimplentes";
            }

            //ultimoObjetodeFiltroFocado = (Control)sender;

            //if (FiltroMudancas() == false)
            //{
                bwrReceber_Run();
            //}

        }

        private void rbnConferidos_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
