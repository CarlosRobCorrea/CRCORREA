using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmCotacao : Form
    {
        // TABELA: COTACAO
        clsCotacaoBLL clsCotacaoBLL;
        clsCotacaoInfo clsCotacaoInfo;
        clsCotacaoInfo clsCotacaoInfoOld;
        clsCotacaoInfo clsCotacaoInfoRef;  // Quando puxa da cotação para jogar no pedido de compra

        Int32 id;
        Int32 idautorizante;
        String termino;
        Int32 filial;

        DataGridViewRowCollection rows;

        String query;
        SqlDataAdapter sda;
        SqlConnection scn;
        SqlCommand scd;

        Int32 painel;
        // TABELA: COTACAO1
        Int32 cotacao1_id;
        Int32 cotacao1_idcentrocusto;
        Int32 cotacao1_idcodigo;
        Int32 cotacao1_idcodigoctabil;
        Int32 cotacao1_idcotacao;
        Int32 cotacao1_idcotacao2ganhou;
        Int32 cotacao1_iddestino;
        Int32 cotacao1_idfornecedorganhou;
        Int32 cotacao1_idhistorico;
        Int32 cotacao1_idordemservico;
        Int32 cotacao1_idpedidocompra;
        Int32 cotacao1_idpedidocompraitem;
        Int32 cotacao1_idsittriba;
        Int32 cotacao1_idsittribb;
        Int32 cotacao1_idsolicitacao;
        Int32 cotacao1_idunidade;
        Int32 Cotacao1_posicao;
        Decimal totalEntregas;

        clsCotacao1BLL clsCotacao1BLL;
        clsCotacao1Info clsCotacao1Info;
        clsCotacao1Info clsCotacao1InfoOld;
        clsCotacao1Info clsCotacao1InfoRef;  // Quando puxa da cotação para jogar no pedido de compra

        DataTable dtCotacao1;

        // TABELA: COTACAO2
        Int32 cotacao2_id;
        Int32 cotacao2_idcondpagto;
        Int32 cotacao2_idcotacao;
        Int32 cotacao2_idcotacao1;
        Int32 cotacao2_idpedidocompra;
        Int32 cotacao2_idpedidocompraitem;
        Int32 cotacao2_idformapagto;
        Int32 cotacao2_idfornecedor;
        Int32 cotacao2_idipi;
        Int32 cotacao2_idunidade;
        Int32 cotacao2_idunidadeinterna;
        Int32 cotacao2_idsittriba;
        Int32 cotacao2_idsittribb;
        Int32 cotacao2_idsittribipi;
        Int32 cotacao2_idsittribpis;
        Int32 cotacao2_idsittribcofins;
        Int32 cotacao2_posicao;
        String cotacao2_fornecedor;
        String cotacao2_formapagto;
        String cotacao2_condpagto;


        clsCotacao2BLL clsCotacao2BLL;
        clsCotacao2Info clsCotacao2Info;
        clsCotacao2Info clsCotacao2InfoOld;
        clsCotacao2Info clsCotacao2InfoRef;  // Quando puxa da cotação para jogar no pedido de compra

        DataTable dtCotacao2;
        // TABELA: COTACAOENTREGA
        Int32 cotacaoentrega_id;
        Int32 cotacaoentrega_idcotacao;
        Int32 cotacaoentrega_idcotacao1;
        Int32 cotacaoentrega_idcotacao2;
        Int32 cotacaoentrega_posicao;

        clsCotacaoentregaBLL clsCotacaoentregaBLL;
        clsCotacaoentregaInfo clsCotacaoentregaInfo;
        clsCotacaoentregaInfo clsCotacaoentregaInfoOld;
        clsCotacaoentregaInfo clsCotacaoentregaInfoRef;

        DataTable dtCotacaoEntrega;
        DataTable dtCotacaoEntregaRef;
        Int32 solicitacao_id;
        // TABELA: solicitacao
        clsSolicitacaoInfo clsSolicitacaoInfo;
        clsSolicitacaoBLL clsSolicitacaoBLL;

        // TABELA: solicitacaoFORNECEDOR
        DataTable dtSolicitacao;

        BackgroundWorker bwrSolicitacao;
        // #########################################
        // USO COMUM
        Boolean carregarSolicitacao;

        DataTable dtCompras1;
        Int32 idPedidoCompra = 0;
        clsComprasInfo clsComprasInfo = new clsComprasInfo();
        clsComprasBLL clsComprasBLL = new clsComprasBLL();

        clsCompras1Info clsCompras1Info = new clsCompras1Info();
        clsCompras1BLL clsCompras1BLL = new clsCompras1BLL();

        clsComprasEntregaInfo clsComprasEntregaInfo = new clsComprasEntregaInfo();
        clsComprasEntregaBLL clsComprasEntregaBLL = new clsComprasEntregaBLL();

        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();


        Int32 _guiaatual;
        Int32 guiaatual
        {
            get
            {
                return _guiaatual;
            }

            set
            {
                if (value == 0 ||
                    value == 1 ||
                    value == 2)
                {
                    _guiaatual = value;
                    tclCotacao.SelectedIndex = _guiaatual;
                }
            }
        }

        public frmCotacao()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id, DataGridViewRowCollection _rows)
        {

            bwrSolicitacao = new BackgroundWorker();
            bwrSolicitacao.WorkerSupportsCancellation = true;
            bwrSolicitacao.DoWork += new DoWorkEventHandler(bwrSolicitacao_DoWork);
            bwrSolicitacao.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSolicitacao_RunWorkerCompleted);
          

            this.id = _id;
            this.rows = _rows;

            clsCotacaoBLL = new clsCotacaoBLL();
            clsCotacao1BLL = new clsCotacao1BLL();
            clsCotacao2BLL = new clsCotacao2BLL();
            clsCotacaoentregaBLL = new clsCotacaoentregaBLL();
            clsSolicitacaoBLL = new clsSolicitacaoBLL();

            clsComprasBLL = new clsComprasBLL();
            clsCompras1BLL = new clsCompras1BLL();
            clsComprasEntregaBLL = new clsComprasEntregaBLL();

            // geralmente os textbox de cor cinza seguidos de um botão de lupa é usado o código abaixo para preencher o textbox com o valor da tabela              

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas where ativo='S' order by codigo", Cotacao1_tbxCodigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS ORDER BY CODIGO", Cotacao1_tbxHistorico);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM CENTROCUSTOS ORDER BY CODIGO", Cotacao1_tbxCentroCusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "SELECT CONVERT(VARCHAR, NUMERO) + ' - ' + CONVERT(VARCHAR, YEAR(DATA)) FROM ORDEMSERVICO ORDER BY NUMERO, YEAR(DATA)", Cotacao1_tbxOrdemServico);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas where ativo='S' order by codigo", Cotacao1_tbxCodigoDestino);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from UNIDADE order by codigo", Cotacao1_tbxUnidade);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from SITUACAOTIPOTITULO order by codigo", Cotacao2_tbxFormaPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from CONDPAGTO where ativo = 's' order by codigo", Cotacao2_tbxCondPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from UNIDADE order by codigo", Cotacao2_tbxUnidade);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from UNIDADE order by codigo", Cotacao2_tbxUnidade_Interno);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from IPI order by codigo", Cotacao2_tbxClassFiscal);
            clsVisual.FillComboBox(Cotacao2_cbxSittribipi, "select codigo from sittribipi order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Cotacao2_cbxSittriba, "select codigo from sittributariaa order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Cotacao2_cbxSittribb, "select codigo from sittributariab order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Cotacao2_cbxSittribpis, "select codigo from sittribpis order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Cotacao2_cbxSittribcofins1, "select codigo from sittribcofins order by codigo", clsInfo.conexaosqldados);

        }

        private void frmCotacao_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void frmCotacao_Load(object sender, EventArgs e)
        {
            Pecastipo_Carrega();
            CotacaoCarregar();
        }

        private void frmCotacao_Shown(object sender, EventArgs e)
        {

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

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);

            if (((Control)sender).Name == tbxRespostaComprador.Name && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }

        }

        private void ControlKeyDownDataHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownDataHora((TextBox)sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void CotacaoCarregar()
        {
            clsCotacaoInfoOld = new clsCotacaoInfo();

            if (id == 0)
            {
                clsCotacaoInfo = new clsCotacaoInfo();

                clsCotacaoInfo.ano = DateTime.Now.Year;
                clsCotacaoInfo.comprador = clsInfo.zusuario;
                clsCotacaoInfo.datafechamento = DateTime.MinValue;
                clsCotacaoInfo.datamontagem = DateTime.Now;
                clsCotacaoInfo.id = 0;
                clsCotacaoInfo.idautorizante = clsInfo.zusuarioid;
                clsCotacaoInfo.motivoreprovado = "";
                clsCotacaoInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                "SELECT MAX(NUMERO + 1) FROM COTACAO where  YEAR(DATAMONTAGEM) = YEAR(GETDATE()) AND FILIAL = " + clsInfo.zfilial));
                clsCotacaoInfo.observar = "";
                clsCotacaoInfo.respostacomprador = "";
                clsCotacaoInfo.termino = "N";
                clsCotacaoInfo.totalprevisto = 0;
                clsCotacaoInfo.tudofechadoem = DateTime.MinValue;
                clsCotacaoInfo.filial = clsInfo.zfilial;
            }
            else
            {
                clsCotacaoInfo = clsCotacaoBLL.Carregar(id, clsInfo.conexaosqldados);
            }

            CotacaoCampos(clsCotacaoInfo);
            CotacaoFillInfo(clsCotacaoInfoOld);

            //carregando o COTACAO1 - Itens da Cotação
            dtCotacao1 = clsCotacao1BLL.GridDados(id, clsInfo.conexaosqldados);
            if (dtCotacao1 == null)
            {
                dtCotacao1 = clsCotacao1BLL.GridDados(0,
                    clsInfo.conexaosqldados);
            }
            DataColumn dccontacao1_posicao = new DataColumn("cotacao1_posicao", Type.GetType("System.Int32"));
            dtCotacao1.Columns.Add(dccontacao1_posicao);
            for (Int32 i = 1; i <= dtCotacao1.Rows.Count; i++)
            {
                dtCotacao1.Rows[i - 1]["cotacao1_posicao"] = i;
            }
            dtCotacao1.AcceptChanges();
            clsCotacao1BLL.GridMonta(dgvCotacao1, dtCotacao1, cotacao1_id);

            //carregando pecas COTACAO2 - Fornecedores com os Itens
            clsCotacao2BLL = new clsCotacao2BLL();
            dtCotacao2 = clsCotacao2BLL.GridDadosTodos(clsCotacaoInfo.id, clsInfo.conexaosqldados);
            if (dtCotacao2 == null)
            {
                dtCotacao2 = clsCotacao2BLL.GridDadosTodos(0,
                    clsInfo.conexaosqldados);
            }

            DataColumn dccotacao2_posicao = new DataColumn("cotacao2_posicao", Type.GetType("System.Int32"));
            dtCotacao2.Columns.Add(dccotacao2_posicao);

            for (Int32 i = 1; i <= dtCotacao2.Rows.Count; i++)
            {
                dtCotacao2.Rows[i - 1]["cotacao2_posicao"] = i;
            }

            DataColumn dccotacao2_posicao1_ref = new DataColumn("cotacao2_posicao1_ref", Type.GetType("System.Int32"));
            dtCotacao2.Columns.Add(dccotacao2_posicao1_ref);

            for (Int32 i = 0; i < dtCotacao1.Rows.Count; i++)
            {
                for (Int32 i2 = 0; i2 < dtCotacao2.Rows.Count; i2++)
                {
                    if (dtCotacao2.Rows[i2]["idcotacao1"].ToString() == dtCotacao1.Rows[i]["id"].ToString())
                    {
                        dtCotacao2.Rows[i2]["cotacao2_posicao1_ref"] = dtCotacao1.Rows[i]["cotacao1_posicao"];
                    }
                }
            }

            dtCotacao2.AcceptChanges();
            clsCotacao2BLL.GridMontaTodos(dgvCotacao2, dtCotacao2, cotacao2_posicao);


            //   CARREGANDO COTACAOENTREGA
            //CotacaoEntrega_tbxQtdeEntregas.Text = "0";
            clsCotacaoentregaBLL = new clsCotacaoentregaBLL();
            dtCotacaoEntrega = clsCotacaoentregaBLL.GridDadosTodos(id, clsInfo.conexaosqldados);
            if (dtCotacaoEntrega == null)
            {
                dtCotacaoEntrega = clsCotacaoentregaBLL.GridDadosTodos(0,
                    clsInfo.conexaosqldados);
            }

            DataColumn dccotacaoentrega_posicao = new DataColumn("cotacaoentrega_posicao", Type.GetType("System.Int32"));
            dtCotacaoEntrega.Columns.Add(dccotacaoentrega_posicao);

            for (Int32 i = 1; i <= dtCotacaoEntrega.Rows.Count; i++)
            {
                dtCotacaoEntrega.Rows[i - 1]["cotacaoentrega_posicao"] = i;
            }

            DataColumn dccotacaoentrega_posicao_ref_cotacao1 = new DataColumn("cotacaoentrega_posicao_ref_cotacao1", Type.GetType("System.Int32"));
            dtCotacaoEntrega.Columns.Add(dccotacaoentrega_posicao_ref_cotacao1);

            DataColumn dccotacaoentrega_posicao_ref_cotacao2 = new DataColumn("cotacaoentrega_posicao_ref_cotacao2", Type.GetType("System.Int32"));
            dtCotacaoEntrega.Columns.Add(dccotacaoentrega_posicao_ref_cotacao2);
            CotacaoEntregaSomar();
            VerificarMudancaProgEntrega();

            for (Int32 i = 0; i < dtCotacao2.Rows.Count; i++)
            {
                for (Int32 i2 = 0; i2 < dtCotacaoEntrega.Rows.Count; i2++)
                {
                    if (dtCotacaoEntrega.Rows[i2]["idcotacao2"].ToString() == dtCotacao2.Rows[i]["id"].ToString()
                         && dtCotacaoEntrega.Rows[i2]["idcotacao1"].ToString() == dtCotacao2.Rows[i]["idcotacao1"].ToString())
                    {
                        dtCotacaoEntrega.Rows[i2]["cotacaoentrega_posicao_ref_cotacao1"] = dtCotacao2.Rows[i]["cotacao2_posicao1_ref"];
                        dtCotacaoEntrega.Rows[i2]["cotacaoentrega_posicao_ref_cotacao2"] = dtCotacao2.Rows[i]["cotacao2_posicao"];
                    }
                }
            }

            dtCotacaoEntrega.AcceptChanges();
            clsCotacaoentregaBLL.GridMontaTodos(dgvCotacaoEntrega, dtCotacaoEntrega, cotacaoentrega_posicao);

            //
            if (termino == "A")
            {  // Em Aprovação
                Analisar_Cotacao();
            }
            else if (termino == "R")
            {  // Reprovado
                gbxStatusMotivos.Visible = true;
            }
            else if (termino == "O")
            {  // Aguardando emissão do Pedido de Compra
                gbxStatusMotivos.Visible = true;
                btnEnviar.Visible = false;
                tbxRespostaComprador.BackColor = System.Drawing.Color.LemonChiffon;
                tbxRespostaComprador.ReadOnly = true;
                btnFechar_Cotacao_Enviar.Visible = false;
                tsbCotacao_Salvar.Enabled = false;
                btnPedidoCompra.Visible = true;
            }
            else if (termino == "F")
            {  // Fechada e Aprovada
                tsbCotacao_Salvar.Enabled = false;
                btnEnviar.Visible = false;
                tbxRespostaComprador.BackColor = System.Drawing.Color.LemonChiffon;
                tbxRespostaComprador.ReadOnly = true;
                btnFechar_Cotacao_Enviar.Visible = false;
                tsbCotacao_Salvar.Enabled = false;
            }
            else
            {  // Em Aberto
                Analisar_Cotacao();
            }
            // 
            bwrSolicitacao_Run();
        }

        private void CotacaoCampos(clsCotacaoInfo info)
        {
            id = info.id;
            filial = info.filial;
            idautorizante = info.idautorizante;

            //Autorizante_Carrega();

            tbxNumero.Text = info.numero.ToString();
            tbxAno.Text = info.ano.ToString();
            tbxComprador.Text = info.comprador;
            tbxObservar.Text = info.observar;

            if (info.datafechamento.Year < info.datamontagem.Year) tbxDataFechamento.Text = "";
            else tbxDataFechamento.Text = info.datafechamento.ToString("dd/MM/yyyy HH:mm");

            tbxDataMontagem.Text = info.datamontagem.ToString("dd/MM/yyyy HH:mm");
            tbxMotivoReprovado.Text = info.motivoreprovado;
            tbxRespostaComprador.Text = info.respostacomprador;
            tbxTermino.Text = info.termino;
            termino = info.termino;

            tbxTotalPrevisto.Text = clsCotacaoInfo.totalprevisto.ToString("N2");

            if (info.tudofechadoem.Year < info.datamontagem.Year) tbxTudoFechado.Text = "";
            else tbxTudoFechado.Text = info.tudofechadoem.ToString("dd/MM/yyyy HH:mm");

        }

        private void CotacaoFillInfo(clsCotacaoInfo info)
        {
            info.id = id;
            info.filial = filial;
            info.idautorizante = idautorizante;

            info.numero = clsParser.Int32Parse(tbxNumero.Text);
            info.ano = clsParser.Int32Parse(tbxAno.Text);
            info.comprador = tbxComprador.Text;
            info.observar = tbxObservar.Text;

            if (tbxDataFechamento.Text == "") info.datafechamento = DateTime.MinValue;
            else info.datafechamento = DateTime.Parse(tbxDataFechamento.Text);

            info.datamontagem = DateTime.Parse(tbxDataMontagem.Text);
            info.motivoreprovado = tbxMotivoReprovado.Text;
            info.respostacomprador = tbxRespostaComprador.Text;
            info.totalprevisto = clsParser.DecimalParse(tbxTotalPrevisto.Text);

            if (tbxTudoFechado.Text == "") info.tudofechadoem = DateTime.MinValue;
            else info.tudofechadoem = DateTime.Parse(tbxTudoFechado.Text);

            info.termino = termino;
        }

        private Boolean HouveModificacoes()
        {
            clsCotacaoInfo = new clsCotacaoInfo();
            CotacaoFillInfo(clsCotacaoInfo);
            if (clsCotacaoBLL.Equals(clsCotacaoInfo, clsCotacaoInfoOld) == false)
            {
                return true;
            }

            foreach (DataRow row in dtCotacao1.Rows)
            {
                if (row.RowState == DataRowState.Added ||
                    row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Modified)
                {
                    return true;
                }
            }

            foreach (DataRow row in dtCotacao2.Rows)
            {
                if (row.RowState == DataRowState.Added ||
                    row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Modified)
                {
                    return true;
                }
            }

            foreach (DataRow row in dtCotacaoEntrega.Rows)
            {
                if (row.RowState == DataRowState.Added ||
                    row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Modified)
                {
                    return true;
                }
            }

            return false;
        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                CotacaoSalvar();
            }
            return drt;
        }

        private void tsbCotacao_Salvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == DialogResult.Cancel)
            {
                return;
            }
            carregarSolicitacao = true;
            bwrSolicitacao.CancelAsync();
            bwrSolicitacao.Dispose();
            this.Close();
            this.Dispose();

        }

        private void CotacaoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: COTACAO
                clsCotacaoInfo = new clsCotacaoInfo();
                CotacaoFillInfo(clsCotacaoInfo);
                if (id == 0)
                {
                    clsCotacaoInfo.id = clsCotacaoBLL.Incluir(clsCotacaoInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsCotacaoBLL.Alterar(clsCotacaoInfo, clsInfo.conexaosqldados);
                }


                // COTACAO1 COLOCANDO AS REFERENCIAS)
                foreach (DataRow row in dtCotacao1.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idcotacao"] = clsCotacaoInfo.id;
                    }
                }

                foreach (DataRow row in dtCotacao1.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        //Registrando na solicitação que ela está em uso
                        if (Int32.Parse(row["idsolicitacao", DataRowVersion.Original].ToString()) != clsInfo.zsolicitacao)
                        {
                            clsSolicitacaoInfo = new clsSolicitacaoInfo();
                            clsSolicitacaoBLL = new clsSolicitacaoBLL();
                            clsSolicitacaoInfo = clsSolicitacaoBLL.Carregar(Int32.Parse(row["idsolicitacao", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                            clsSolicitacaoInfo.idcotacao = clsInfo.zcotacao;
                            clsSolicitacaoInfo.idcotacaoitem = clsInfo.zcotacao1;
                            clsSolicitacaoBLL.Alterar(clsSolicitacaoInfo, clsInfo.conexaosqldados);
                        }

                        clsCotacao1BLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);

                        continue;
                    }
                    else
                    {
                        clsCotacao1Info = new clsCotacao1Info();
                        Cotacao1GridToInfo(clsCotacao1Info, Int32.Parse(row["cotacao1_posicao"].ToString()));

                        if (clsCotacao1Info.id == 0)
                        {
                            clsCotacao1Info.id = clsCotacao1BLL.Incluir(clsCotacao1Info, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsCotacao1BLL.Alterar(clsCotacao1Info, clsInfo.conexaosqldados);
                        }

                        //Registrando na solicitação que ela está em uso
                        if (clsCotacao1Info.idsolicitacao != clsInfo.zsolicitacao)
                        {
                            clsSolicitacaoInfo = new clsSolicitacaoInfo();
                            clsSolicitacaoBLL = new clsSolicitacaoBLL();
                            clsSolicitacaoInfo = clsSolicitacaoBLL.Carregar(clsCotacao1Info.idsolicitacao, clsInfo.conexaosqldados);
                            clsSolicitacaoInfo.idcotacao = clsCotacaoInfo.id;
                            clsSolicitacaoInfo.idcotacaoitem = clsCotacao1Info.id;
                            clsSolicitacaoBLL.Alterar(clsSolicitacaoInfo, clsInfo.conexaosqldados);
                        }


                        //colocando as referencias                     
                        foreach (DataRow linha in dtCotacao2.Rows)
                        {
                           
                            if (linha.RowState != DataRowState.Deleted ||
                                linha.RowState != DataRowState.Detached ||
                                linha.RowState != DataRowState.Unchanged)
                            {
                                if (linha["cotacao2_posicao1_ref"] != null)
                                {
                                    if (linha["cotacao2_posicao1_ref"].ToString() == row["cotacao1_posicao"].ToString())
                                    {
                                        linha["idcotacao"] = clsCotacaoInfo.id;
                                        linha["idcotacao1"] = clsCotacao1Info.id;
                                    }
                                }
                            }
                        }
                    }
                }
                //salvando cotacao2
                foreach (DataRow row in dtCotacao2.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsCotacao2BLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsCotacao2Info = new clsCotacao2Info();
                        Cotacao2GridToInfo(clsCotacao2Info, Int32.Parse(row["cotacao2_posicao"].ToString()));

                        if (clsCotacao2Info.id == 0)
                        {
                            clsCotacao2Info.id = clsCotacao2BLL.Incluir(clsCotacao2Info, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsCotacao2BLL.Alterar(clsCotacao2Info, clsInfo.conexaosqldados);
                        }

                        //colocando as referencias                     

                        
                        foreach (DataRow linha in dtCotacaoEntrega.Rows)
                        {
                            if (linha.RowState != DataRowState.Deleted &&
                                linha.RowState != DataRowState.Detached &&
                                linha.RowState != DataRowState.Unchanged)
                            {
                                if (linha["cotacaoentrega_posicao_ref_cotacao1"].ToString() == row["cotacao2_posicao1_ref"].ToString()
                                    && linha["cotacaoentrega_posicao_ref_cotacao2"].ToString() == row["cotacao2_posicao"].ToString())
                                {
                                    linha["idcotacao"] = clsCotacaoInfo.id;
                                    linha["idcotacao1"] = row["idcotacao1"];
                                    linha["idcotacao2"] = clsCotacao2Info.id;
                                }
                            }
                        }
                    }
                }


                //salvando as entregas

                foreach (DataRow row in dtCotacaoEntrega.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsCotacaoentregaBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsCotacaoentregaInfo = new clsCotacaoentregaInfo();
                        CotacaoEntregaGridToInfo(clsCotacaoentregaInfo, Int32.Parse(row["cotacaoentrega_posicao"].ToString()));

                        if (clsCotacaoentregaInfo.id == 0)
                        {
                            clsCotacaoentregaInfo.id = clsCotacaoentregaBLL.Incluir(clsCotacaoentregaInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsCotacaoentregaBLL.Alterar(clsCotacaoentregaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }


                tse.Complete();
            }
        }


        private void tsbCotacao_Primeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    CotacaoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbCotacao_Anterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                CotacaoCarregar();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbCotacao_Proximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                CotacaoCarregar();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbCotacao_Ultimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    CotacaoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbCotacao_Retornar_Click(object sender, EventArgs e)
        {
            // Não pode permirir alteração - 26/10/2014 carlos
            //if (HouveModificacoes() == true)
            //{
            //    tsbCotacao_Salvar.PerformClick();
            //}
            //else
            //{
                this.Close();
            //}

        }

        private void Pecastipo_Carrega()
        {
            Cotacao1_lbxTipoEntrada1.Items.Clear();
            String query;
            SqlDataAdapter sda;
            query = "select ID, CODIGO + ' - ' + NOME AS [CODIGONOME] FROM PECASTIPO ORDER BY CODIGO";

            DataTable dtTemp = new DataTable();
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtTemp);
            foreach (DataRow row in dtTemp.Rows)
            {
                Cotacao1_lbxTipoEntrada1.Items.Add(row["CODIGONOME"].ToString());
            }
        }


        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
               clsInfo.zrow.Index != -1 &&
               clsInfo.znomegrid != null &&
               clsInfo.znomegrid != "")
            {
                //TRATA OS BOTÕES DO COTACAO1  


                if (clsInfo.znomegrid == Cotacao1_btnIdCodigo.Name)
                {
                    Cotacao1_tbxCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                 
                    Int32 idcodigo_old = cotacao1_idcodigo;
                    //código
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        cotacao1_idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        clsPecasInfo = clsPecasBLL.Carregar(cotacao1_idcodigo, clsInfo.conexaosqldados);
                        Cotacao1_tbxCodigo.Text = clsPecasInfo.codigo;
                        Cotacao1_tbxNome.Text = clsPecasInfo.nome;
                        cotacao1_idunidade = clsPecasInfo.idunidade;
                        Cotacao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + cotacao1_idunidade + "");
                        cotacao1_idhistorico = clsPecasInfo.idhistoricobco;
                        Cotacao1_tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + cotacao1_idhistorico);
                        Cotacao1_lbxTipoEntrada1.SelectedIndex = Cotacao1_lbxTipoEntrada1.FindString(clsPecasInfo.tipoproduto);
                        pbxFoto.Image = clsPecasInfo.foto;
                        pbxFoto1.Image = clsPecasInfo.foto;
                        //Cotacao1_lbxTipoEntrada.SelectedIndex = Cotacao1_lbxTipoEntrada.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOENTRADA from PECAS where ID=" + cotacao1_idcodigo));
                        String consumo = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONSUMO from PECAS where ID=" + cotacao1_idcodigo);
                        if (consumo == "N")
                        {
                            Cotacao1_rbnRevenda.Checked = true;
                        }
                        else
                        {
                            Cotacao1_rbnConsumo.Checked = true;
                        }
                        Cotacao1_tbxCodigo.Select();
                        Cotacao1_tbxCodigo.SelectAll();
                        VerificaSeMudouFornecedor(idcodigo_old);
                        verificaNumForne(dgvCotacao2.Rows.Count);


                    }
                }
                    else if (clsInfo.znomegrid == Cotacao1_btnIdDestino.Name)
                    {
                        Cotacao1_tbxCodigoDestino.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                        if (Cotacao1_CodigoDestino_Check() == true)
                        {
                            Cotacao1_CodigoDestino_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == Cotacao1_btnIdOs.Name)
                    {
                        Cotacao1_tbxOrdemServico.Text = clsInfo.zrow.Cells["NUMERO"].Value.ToString() + " - " + clsParser.DateTimeParse(clsInfo.zrow.Cells["data"].Value.ToString()).ToString("yyyy");

                        if (Cotacao1_OrdemServico_Check() == true)
                        {
                            Cotacao1_OrdemServico_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == Cotacao1_btnIdHistorico.Name)
                    {
                        Cotacao1_tbxHistorico.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                        if (Cotacao1_Historico_Check() == true)
                        {
                            Cotacao1_Historico_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == Cotacao1_btnIdCentroCusto.Name)
                    {
                        Cotacao1_tbxCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                        if (Cotacao1_CentroCusto_Check() == true)
                        {
                            Cotacao1_CentroCusto_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == Cotacao1_btnIdUnidade.Name)
                    {
                        Cotacao1_tbxUnidade.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                        if (Cotacao1_Unidade_Check() == true)
                        {
                            Cotacao1_Unidade_Carrega();
                        }
                    }

                    // botões cotação 2
                    if (clsInfo.znomegrid == btnIdFormapagto_Cotacao2.Name)
                    {
                        Cotacao2_tbxFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString() + " = " + clsInfo.zrow.Cells["NOME"].Value.ToString(); ;

                        if (Cotacao2_FormaPagto_Check() == true)
                        {
                            Cotacao2_FormaPagto_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == btnIdCondpagto_Cotacao2.Name)
                    {
                        Cotacao2_tbxCondPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString() + " = " + clsInfo.zrow.Cells["NOME"].Value.ToString();

                        if (Cotacao2_CondPagto_Check() == true)
                        {
                            Cotacao2_CondPagto_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == btnIdUnidade_Cotacao2.Name)
                    {
                        Cotacao2_tbxUnidade.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                        if (Cotacao2_Unidade_Check() == true)
                        {
                            Cotacao2_Unidade_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == btnIdIPI_Cotacao2.Name)
                    {
                        Cotacao2_tbxClassFiscal.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                        if (Cotacao2_Ipi_Check() == true)
                        {
                            Cotacao2_Ipi_Carrega();
                        }
                    }
                    else if (clsInfo.znomegrid == Cotacao2_btnFornecedor.Name)
                    {
                        cotacao2_idfornecedor = Int32.Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        Cotacao2_tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id = " + cotacao2_idfornecedor);
                        cotacao2_fornecedor = Cotacao2_tbxCognome.Text;
                        Cotacao2_tbxContato.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONTATO from CLIENTE where id = " + cotacao2_idfornecedor);
                        Cotacao2_tbxTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from CLIENTE where id = " + cotacao2_idfornecedor);
                        Cotacao2_tbxTelefone.Text += "-" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from CLIENTE where id = " + cotacao2_idfornecedor);
                        cotacao2_idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDFORMAPAGTO from CLIENTE where id = " + cotacao2_idfornecedor));
                        Cotacao2_tbxFormaPagto.Text = Cotacao2_tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from situacaotipotitulo where id=" + cotacao2_idformapagto);
                        cotacao2_idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCONDPAGTO from CLIENTE where id = " + cotacao2_idfornecedor));
                        Cotacao2_tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from condpagto where id=" + cotacao2_idcondpagto);
                        Cotacao2_tbxCognome.Select();
                    }
                }
                else
                {
                    // TABELA: COTACAO1
                    // TRATA OS TEXTOS DO COTACAO1  
                    if (ctl.Name == Cotacao1_tbxCodigo.Name)
                    {
                        Int32 idcodigo_old = cotacao1_idcotacao;
                        if (Cotacao1_Codigo_Check() == true)
                        {
                            Cotacao1_Codigo_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao2_tbxCognome.Name)
                    { // Verificar se já não existe codigo similar

                        cotacao2_idfornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + Cotacao2_tbxCognome.Text + "'"));
                        if (cotacao2_idfornecedor == 0)
                        {
                            cotacao2_idfornecedor = clsInfo.zempresaclienteid;
                            Cotacao2_tbxCognome.Text = clsInfo.zempresacliente_cognome;
                        }
                        else
                        {
                        Cotacao2_tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id = " + cotacao2_idfornecedor);
                        cotacao2_fornecedor = Cotacao2_tbxCognome.Text;
                        Cotacao2_tbxContato.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONTATO from CLIENTE where id = " + cotacao2_idfornecedor);
                        Cotacao2_tbxTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from CLIENTE where id = " + cotacao2_idfornecedor);
                        Cotacao2_tbxTelefone.Text += "-" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from CLIENTE where id = " + cotacao2_idfornecedor);
                        cotacao2_idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDFORMAPAGTO from CLIENTE where id = " + cotacao2_idfornecedor));
                        Cotacao2_tbxFormaPagto.Text = Cotacao2_tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from situacaotipotitulo where id=" + cotacao2_idformapagto);
                        cotacao2_idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCONDPAGTO from CLIENTE where id = " + cotacao2_idfornecedor));
                        Cotacao2_tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from condpagto where id=" + cotacao2_idcondpagto);
                    }
                }
                    else if (ctl.Name == Cotacao1_tbxCodigoDestino.Name)
                    {
                        if (Cotacao1_CodigoDestino_Check() == true)
                        {
                            Cotacao1_CodigoDestino_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao1_tbxOrdemServico.Name)
                    {
                        if (Cotacao1_OrdemServico_Check() == true)
                        {
                            Cotacao1_OrdemServico_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao1_tbxHistorico.Name)
                    {
                        if (Cotacao1_Historico_Check() == true)
                        {
                            Cotacao1_Historico_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao1_tbxCentroCusto.Name)
                    {
                        if (Cotacao1_CentroCusto_Check() == true)
                        {
                            Cotacao1_CentroCusto_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao1_tbxUnidade.Name)
                    {
                        if (Cotacao1_Unidade_Check() == true)
                        {
                            Cotacao1_Unidade_Carrega();
                        }
                    }
                    //  TRATA OS TEXTOS DO COTACAO2    
                    if (ctl.Name == Cotacao2_tbxFormaPagto.Name)
                    {
                        if (Cotacao2_FormaPagto_Check() == true)
                        {
                            Cotacao2_FormaPagto_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao2_tbxCondPagto.Name)
                    {
                        if (Cotacao2_CondPagto_Check() == true)
                        {
                            Cotacao2_CondPagto_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao2_tbxUnidade.Name)
                    {
                        if (Cotacao2_Unidade_Check() == true)
                        {
                            Cotacao2_Unidade_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao2_tbxClassFiscal.Name)
                    {
                        if (Cotacao2_Ipi_Check() == true)
                        {
                            Cotacao2_Ipi_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao2_tbxQtdeorcada.Name)
                    {
                        if (clsParser.DecimalParse(Cotacao2_tbxQtdeinterna.Text) <= 0)
                        {
                            Cotacao2_tbxQtdeinterna.Text = clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text).ToString("n5");
                        }
                        CotacaoEntregaSomar();
                        VerificarMudancaProgEntrega();
                    }
                    else if (ctl.Name == Cotacao2_tbxUnidade.Name)
                    {
                        if (Cotacao2_Unidade_Check() == true)
                        {
                            Cotacao2_Unidade_Carrega();
                        }

                        if (Cotacao2_tbxUnidade_Interno.Text.Trim().Length == 0 ||
                            cotacao2_idunidadeinterna == 0)
                        {
                            cotacao2_idunidadeinterna = cotacao2_idunidade;
                            Cotacao2_Unidade_Interna_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao2_tbxUnidade_Interno.Name)
                    {
                        if (Cotacao2_Unidade_Interna_Check() == true)
                        {
                            Cotacao2_Unidade_Interna_Carrega();
                        }
                    }
                    else if (ctl.Name == Cotacao2_tbxCognome.Name)
                    {
                        if (Cotacao2_Fornecedor_Check() == true)
                        {
                            Cotacao2_Fornecedor_Carrega();
                        }
                    }

                    if (ctl.Name == Cotacao2_tbxPrecoDescPor.Name)
                    {
                        if (clsParser.DecimalParse(Cotacao2_tbxPrecoDescPor.Text) > 0)
                        {
                            Cotacao2_tbxValorDesconto.Text = (clsParser.DecimalParse(Cotacao2_tbxPrecoBruto.Text) * (clsParser.DecimalParse(Cotacao2_tbxPrecoDescPor.Text) / 100)).ToString("N2");
                        }
                        else
                        {
                            Cotacao2_tbxValorDesconto.Text = 0.ToString("N2");
                        }
                    }
                    else
                    {
                        Cotacao2_tbxValorDesconto.Text = clsParser.DecimalParse(Cotacao2_tbxValorDesconto.Text).ToString("N6");
                    }

                    if (ctl.Name == Cotacao2_tbxValorDesconto.Name)
                    {
                        if (clsParser.DecimalParse(Cotacao2_tbxValorDesconto.Text) > 0)
                        {
                            Cotacao2_tbxPrecoDescPor.Text = (clsParser.DecimalParse(Cotacao2_tbxValorDesconto.Text) / (clsParser.DecimalParse(Cotacao2_tbxPrecoBruto.Text)) * 100).ToString("N2");
                        }
                        else
                        {
                            Cotacao2_tbxPrecoDescPor.Text = 0.ToString("N2");
                        }
                    }
                    else
                    {
                        Cotacao2_tbxPrecoDescPor.Text = clsParser.DecimalParse(Cotacao2_tbxPrecoDescPor.Text).ToString("N2");
                    }
                }



                Cotacao1_tbxQtdeOk.Text = clsParser.DecimalParse(Cotacao1_tbxQtdeOk.Text).ToString("N5");
                Cotacao1_tbxQtdeAutorizada.Text = Cotacao1_tbxQtdeOk.Text;

                // CALCULOS
                Cotacao2Calcular();

                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
        

        void Cotacao1Carregar()
        {
            clsCotacao1Info = new clsCotacao1Info();
            clsCotacao1InfoOld = new clsCotacao1Info();

            if (Cotacao1_posicao == 0)
            {
                Cotacao1_posicao = dtCotacao1.Rows.Count + 1;

                clsCotacao1Info.idcentrocusto = clsInfo.zcentrocustos;
                clsCotacao1Info.idcodigo = clsInfo.zpecas;
                clsCotacao1Info.idcodigoctabil = clsInfo.zcontacontabil;
                clsCotacao1Info.idcotacao = clsInfo.zcotacao;
                clsCotacao1Info.idcotacao2ganhou = 0;
                clsCotacao1Info.iddestino = clsInfo.zpecas;
                clsCotacao1Info.idfornecedorganhou = 0; //clsInfo.zempresaclienteprospeccaoid;
                clsCotacao1Info.idhistorico = clsInfo.zhistoricos;
                clsCotacao1Info.idordemservico = clsInfo.zordemservico;
                clsCotacao1Info.idpedidocompra = clsInfo.zcompras;
                clsCotacao1Info.idpedidocompraitem = clsInfo.zcompras1;
                clsCotacao1Info.idsittriba = clsInfo.zsituacaotriba;
                clsCotacao1Info.idsittribb = clsInfo.zsituacaotribb;
                clsCotacao1Info.idsolicitacao = clsInfo.zsolicitacao;
                clsCotacao1Info.idunidade = clsInfo.zunidade;

                clsCotacao1Info.complemento = "";
                clsCotacao1Info.complemento1 = "";
                clsCotacao1Info.consumo = "S";
                clsCotacao1Info.descricaoesp = "";
                clsCotacao1Info.msg = "";
                clsCotacao1Info.observar = "";
                clsCotacao1Info.qtdeautorizada = 0;
                clsCotacao1Info.qtdeok = 0;
                clsCotacao1Info.tipodestino = "I";
                clsCotacao1Info.tipoentrada = "";
                clsCotacao1Info.tipoproduto = "";
                clsCotacao1Info.totalprevisto = 0;
            }
            else
            {
                Cotacao1GridToInfo(clsCotacao1Info, Cotacao1_posicao);
            }

            Cotacao1Campos(clsCotacao1Info);
            Cotacao1FillInfo(clsCotacao1InfoOld);

            if (cotacao1_id > 0)
            {
                gbxCodigo.Enabled = false;
                gbxDestino.Enabled = false;
                gbxHistorico.Enabled = false;
                Cotacao1_lbxTipoEntrada1.Enabled = false;
                Cotacao1_lbxTipoProduto.Enabled = false;
            }
            else
            {
                gbxCodigo.Enabled = true;
                gbxDestino.Enabled = true;
                gbxHistorico.Enabled = true;
                Cotacao1_lbxTipoEntrada1.Enabled = true;
                Cotacao1_lbxTipoProduto.Enabled = true;
            }


            dtCotacao2.DefaultView.RowFilter = "cotacao2_posicao1_ref=" + Cotacao1_posicao;
            guiaatual = 1;
        }

        private void Cotacao1GridToInfo(clsCotacao1Info info, Int32 posicao)
        {
            DataRow row = dtCotacao1.Select("cotacao1_posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());
            info.idcentrocusto = clsParser.Int32Parse(row["idcentrocusto"].ToString());
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idcodigoctabil = clsParser.Int32Parse(row["idcodigoctabil"].ToString());
            info.idcotacao = clsParser.Int32Parse(row["idcotacao"].ToString());
            info.idcotacao2ganhou = clsParser.Int32Parse(row["idcotacao2ganhou"].ToString());
            info.iddestino = clsParser.Int32Parse(row["iddestino"].ToString());
            info.idfornecedorganhou = clsParser.Int32Parse(row["idfornecedorganhou"].ToString());
            info.idhistorico = clsParser.Int32Parse(row["idhistorico"].ToString());
            info.idordemservico = clsParser.Int32Parse(row["idordemservico"].ToString());
            info.idpedidocompra = clsParser.Int32Parse(row["idpedidocompra"].ToString());
            info.idpedidocompraitem = clsParser.Int32Parse(row["idpedidocompraitem"].ToString());
            info.idsittriba = clsParser.Int32Parse(row["idsittriba"].ToString());
            info.idsittribb = clsParser.Int32Parse(row["idsittribb"].ToString());
            info.idsolicitacao = clsParser.Int32Parse(row["idsolicitacao"].ToString());
            info.idunidade = clsParser.Int32Parse(row["idunidade"].ToString());

            info.complemento = row["complemento"].ToString();
            info.complemento1 = row["complemento1"].ToString();
            info.consumo = row["consumo"].ToString();
            info.descricaoesp = row["descricaoesp"].ToString();
            info.qtdeautorizada = clsParser.DecimalParse(row["qtdeautorizada"].ToString());
            info.qtdeok = clsParser.DecimalParse(row["qtdeok"].ToString());
            info.tipodestino = row["tipodestino"].ToString();
            info.tipoentrada = row["tipoentrada"].ToString();
            info.tipoproduto = row["tipoproduto"].ToString();
            info.msg = row["msg"].ToString();
            info.observar = row["observar"].ToString();
            info.totalprevisto = clsParser.DecimalParse(row["totalprevisto"].ToString());
        }

        private void Cotacao1Campos(clsCotacao1Info info)
        {
            cotacao1_id = info.id;
            cotacao1_idcentrocusto = info.idcentrocusto;
            cotacao1_idcodigo = info.idcodigo;
            cotacao1_idcotacao = info.idcotacao;
            cotacao1_idcotacao2ganhou = info.idcotacao2ganhou;
            cotacao1_idcodigoctabil = info.idcodigoctabil;
            cotacao1_iddestino = info.iddestino;
            cotacao1_idfornecedorganhou = info.idfornecedorganhou;
            cotacao1_idhistorico = info.idhistorico;
            cotacao1_idordemservico = info.idordemservico;
            cotacao1_idpedidocompra = info.idpedidocompra;
            cotacao1_idpedidocompraitem = info.idpedidocompraitem;
            cotacao1_idsittriba = info.idsittriba;
            cotacao1_idsittribb = info.idsittribb;
            cotacao1_idsolicitacao = info.idsolicitacao;
            cotacao1_idunidade = info.idunidade;

            if (cotacao1_idsolicitacao > 0 && cotacao1_idsolicitacao != clsInfo.zsolicitacao)
                tbxSolicitante_Cotacao1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usuario from usuario WHERE ID = (select idsolicitante from solicitacao where id = " + cotacao1_idsolicitacao + ")");
            else
            {
                tbxSolicitante_Cotacao1.Text = "";
            }
            if (cotacao1_idcentrocusto > 0)
                tbxArea_Cotacao1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS WHERE ID = " + cotacao1_idcentrocusto);
            else
            {
                tbxArea_Cotacao1.Text = "";
            }

            if (cotacao1_idcentrocusto == 0) cotacao1_idcentrocusto = clsInfo.zcentrocustos;
            if (cotacao1_idcodigo == 0) cotacao1_idcodigo = clsInfo.zpecas;
            if (cotacao1_iddestino == 0) cotacao1_iddestino = cotacao1_idcodigo;
            if (cotacao1_idhistorico == 0) cotacao1_idhistorico = clsInfo.zhistoricos;
            if (cotacao1_idordemservico == 0) cotacao1_idordemservico = clsInfo.zordemservico;
            if (cotacao1_idpedidocompra == 0) cotacao1_idpedidocompra = clsInfo.zcompras;
            if (cotacao1_idsittriba == 0) cotacao1_idsittriba = clsInfo.zsituacaotriba;
            if (cotacao1_idsittribb == 0) cotacao1_idsittribb = clsInfo.zsituacaotribb;
            if (cotacao1_idsolicitacao == 0) cotacao1_idsolicitacao = clsInfo.zsolicitacao;
            if (cotacao1_idunidade == 0) cotacao1_idunidade = clsInfo.zunidade;

            Cotacao1_Codigo_Carrega();
            Cotacao1_CentroCusto_Carrega();
            Cotacao1_Historico_Carrega();
            Cotacao1_CodigoDestino_Carrega();
            Cotacao1_OrdemServico_Carrega();
            Cotacao1_Unidade_Carrega();

            Cotacao1_tbxComplemento.Text = info.complemento;
            Cotacao1_tbxComplemento1.Text = info.complemento1;
            //Cotacao1_tbxDescricaoEsp.Text = info.descricaoesp;

            if (info.consumo == "S") Cotacao1_rbnConsumo.Checked = true;
            else Cotacao1_rbnRevenda.Checked = true;

            Cotacao1_tbxPedidoCompra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from COMPRAS WHERE ID= " + cotacao1_idpedidocompra);

            if (clsParser.Int32Parse(Cotacao1_tbxPedidoCompra.Text) > 0)
            {
                Cotacao1_tbxDataPedido.Text = DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DATA from COMPRAS WHERE ID= " + cotacao1_idpedidocompra + " ")).ToString("dd/MM/yyyy");
            }
            else
            {
                Cotacao1_tbxDataPedido.Text = "";
            }

            Cotacao1_tbxSolicitacao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from solicitacao WHERE ID= " + cotacao1_idsolicitacao);

            if (clsParser.Int32Parse(Cotacao1_tbxSolicitacao.Text) > 0)
            {
                Cotacao1_tbxDataSolicitacao.Text = DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DATA from SOLICITACAO WHERE ID= " + cotacao1_idsolicitacao + " ")).ToString("dd/MM/yyyy");
            }
            else
            {
                Cotacao1_tbxDataSolicitacao.Text = "";
            }

            //info.msg
            //info.observar

            Cotacao1_tbxQtdeAutorizada.Text = clsParser.DecimalParse(info.qtdeautorizada.ToString()).ToString();
            Cotacao1_tbxQtdeOk.Text = clsParser.DecimalParse(info.qtdeok.ToString()).ToString();

            if (info.tipodestino == "M") Cotacao1_rbnTipoDestinoMaq.Checked = true;
            else Cotacao1_rbnTipoDestinoItem.Checked = true;

            Cotacao1_lbxTipoProduto.SelectedIndex = clsVisual.SelecionarIndex(info.tipoentrada, 2, Cotacao1_lbxTipoProduto);
            Cotacao1_lbxTipoEntrada1.SelectedIndex = clsVisual.SelecionarIndex(info.tipoproduto, 1, Cotacao1_lbxTipoEntrada1);
            Cotacao1_tbxTotalPrevisto.Text = clsParser.DecimalParse(info.totalprevisto.ToString()).ToString();
        }

        private void Cotacao1FillInfo(clsCotacao1Info info)
        {
            info.id = cotacao1_id;

            info.idcentrocusto = cotacao1_idcentrocusto;
            info.idcodigo = cotacao1_idcodigo;
            info.idcodigoctabil = cotacao1_idcodigoctabil;
            info.idcotacao = cotacao1_idcotacao;
            info.idcotacao2ganhou = clsInfo.zcotacao2;
            info.iddestino = cotacao1_iddestino;
            info.idfornecedorganhou = clsInfo.zempresaclienteid;
            info.idhistorico = cotacao1_idhistorico;
            info.idordemservico = cotacao1_idordemservico;
            info.idpedidocompra = cotacao1_idpedidocompra;
            info.idpedidocompraitem = cotacao1_idpedidocompraitem;
            info.idsittriba = cotacao1_idsittriba;
            info.idsittribb = cotacao1_idsittribb;
            info.idsolicitacao = cotacao1_idsolicitacao;
            info.idunidade = cotacao1_idunidade;



            info.complemento = Cotacao1_tbxComplemento.Text;
            info.complemento1 = Cotacao1_tbxComplemento1.Text;
            //info.descricaoesp = Cotacao1_tbxDescricaoEsp.Text;

            if (Cotacao1_rbnConsumo.Checked == true) info.consumo = "S";
            else info.consumo = "S";

            info.qtdeautorizada = clsParser.DecimalParse(Cotacao1_tbxQtdeAutorizada.Text);
            info.qtdeok = clsParser.DecimalParse(Cotacao1_tbxQtdeOk.Text);

            if (Cotacao1_rbnTipoDestinoMaq.Checked == true) info.tipodestino = "M";
            else info.tipodestino = "F";

            info.tipoproduto = Cotacao1_lbxTipoProduto.Text.Substring(0, 2); 
            info.totalprevisto = clsParser.DecimalParse(Cotacao1_tbxTotalPrevisto.Text);
        }

        void Cotacao1FillInfoToGrid(clsCotacao1Info info)
        {
            DataRow row;
            DataRow[] rows = dtCotacao1.Select("cotacao1_posicao = " + Cotacao1_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtCotacao1.NewRow();
            }


            row["id"] = info.id;
            row["idcentrocusto"] = info.idcentrocusto;
            row["idcodigo"] = info.idcodigo;
            row["idcodigoctabil"] = info.idcodigoctabil;
            row["idcotacao"] = info.idcotacao;
            row["idcotacao2ganhou"] = info.idcotacao2ganhou;
            row["iddestino"] = info.iddestino;
            row["idfornecedorganhou"] = info.idfornecedorganhou;
            row["idhistorico"] = info.idhistorico;
            row["idordemservico"] = info.idordemservico;
            row["idpedidocompra"] = info.idpedidocompra;
            row["idpedidocompraitem"] = info.idpedidocompraitem;
            row["idsittriba"] = info.idsittriba;
            row["idsittribb"] = info.idsittribb;
            row["idsolicitacao"] = info.idsolicitacao;
            row["idunidade"] = info.idunidade;

            row["complemento"] = info.complemento;
            row["complemento1"] = info.complemento1;
            row["consumo"] = info.consumo;
            row["descricaoesp"] = info.descricaoesp;
            row["qtdeautorizada"] = info.qtdeautorizada;
            row["qtdeok"] = info.qtdeok;
            row["tipodestino"] = info.tipodestino;
            row["tipoentrada"] = info.tipoentrada;
            row["tipoproduto"] = info.tipoproduto;
            row["totalprevisto"] = info.totalprevisto;

            // Colunas que petencem a outras tabelas
            row["codigo"] = Cotacao1_tbxCodigo.Text;
            row["nome"] = Cotacao1_tbxNome.Text;
            row["unid"] = Cotacao1_tbxUnidade.Text;
            row["NROSOL"] = Cotacao1_tbxSolicitacao.Text;
            if (Cotacao1_tbxOrdemServico.Text.IndexOf(" - ") != -1)
            {
                row["NROOS"] = clsParser.Int32Parse(Cotacao1_tbxOrdemServico.Text.Substring(0, Cotacao1_tbxOrdemServico.Text.IndexOf(" - ")));
            }
            else
            {
                row["NROOS"] = clsParser.Int32Parse(Cotacao1_tbxOrdemServico.Text);
            }

            row["PEDCOMPRANRO"] = Cotacao1_tbxPedidoCompra.Text;

            if (rows.Length == 0)
            {
                row["cotacao1_posicao"] = Cotacao1_posicao;
                dtCotacao1.Rows.Add(row);
            }
        }

        private void tspCotacao1Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar este item da cotação ? ", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsCotacao1Info = new clsCotacao1Info();
                    Cotacao1FillInfo(clsCotacao1Info);
                    Cotacao1FillInfoToGrid(clsCotacao1Info);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                guiaatual = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspCotacao1Incluir_Click(object sender, EventArgs e)
        {
            Cotacao1_posicao = 0;
            Cotacao1Carregar();

        }

        private void tspCotacao1Alterar_Click(object sender, EventArgs e)
        {
            if (dgvCotacao1.CurrentRow != null)
            {
                Cotacao1_posicao = Int32.Parse(dgvCotacao1.CurrentRow.Cells["cotacao1_posicao"].Value.ToString());
                Cotacao1Carregar();
                verificaNumForne(dgvCotacao2.Rows.Count);
            }
        }

        private void tspCotacao1Excluir_Click(object sender, EventArgs e)
        {
            //deletando da COTACAOENTREGA a SOLICITACAOENTREGA 

            DataRow[] rowsEnt = dtCotacaoEntrega.Select("cotacaoentrega_posicao_ref_cotacao1=" + dgvCotacao1.CurrentRow.Cells["cotacao1_posicao"].Value.ToString());

            for (Int32 i = 0; i < rowsEnt.Length; i++)
            {
                rowsEnt[i].Delete();
            }

            //deletando da COTACAO2 a SOLICITACAOFORNECEDOR        
            DataRow[] rows = dtCotacao2.Select("cotacao2_posicao1_ref=" + dgvCotacao1.CurrentRow.Cells["cotacao1_posicao"].Value.ToString());

            for (Int32 i = 0; i < rows.Length; i++)
            {
                rows[i].Delete();
            }


            //deletando da COTACAO1 a SOLICITACAO
            dtCotacao1.Select("cotacao1_posicao=" + dgvCotacao1.CurrentRow.Cells["cotacao1_posicao"].Value.ToString())[0].Delete();

            //reload do dtSolicitacao
            bwrSolicitacao_Run();
        }

        private void tspCotacao1Retornar_Click(object sender, EventArgs e)
        {
            guiaatual = 0;
        }


        private void bwrSolicitacao_Run()
        {

            if (carregarSolicitacao == false)
            {
                //filial = clsParser.Int32Parse(cbxFilial.Text.Substring(0, 2));
                               
                carregarSolicitacao = true;
             
                bwrSolicitacao.RunWorkerAsync();
            }
           
        }

        private void bwrSolicitacao_DoWork(object sender, DoWorkEventArgs e)
        {
            solicitacaoGrid();
            if (bwrSolicitacao.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            
        }

        private void solicitacaoGrid()
        {
            dtSolicitacao = clsSolicitacaoBLL.GridDados(DateTime.MinValue, DateTime.MaxValue, clsParser.Int32Parse("0"), "A");
        }

        private void bwrSolicitacao_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //retirando as solicitações que já estão em cotação
            try
            {
                if (e.Cancelled != true)
                {


                    Int32 cont = dtCotacao1.Rows.Count;
                    foreach (DataRow rowCot in dtCotacao1.Rows)
                    {
                        if (rowCot.RowState != DataRowState.Deleted && rowCot.RowState != DataRowState.Detached) //apenas as linhas não deletadas
                        {
                            if (rowCot["idsolicitacao"].ToString() != "0")
                            {
                                DataRow[] rows = dtSolicitacao.Select("id=" + rowCot["idsolicitacao"].ToString());

                                for (Int32 i = 0; i < rows.Length; i++)
                                {
                                    rows[i].Delete();
                                }
                            }
                        }
                    }

                    clsSolicitacaoBLL.GridMonta(dgvSolicitacao, dtSolicitacao, 0);

                    carregarSolicitacao = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregarSolicitacao = false;
            }
        }

        private void dgvSolicitacao_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvSolicitacao.CurrentRow != null)
                {
                    solicitacao_id = (Int32)dgvSolicitacao.CurrentRow.Cells[0].Value;
                    CarregarItemCotacao(solicitacao_id);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void CarregarItemCotacao(Int32 _solicitacao_id)
        {
            ValidarFornecedoresSolicitacao();
            clsSolicitacaoInfo = clsSolicitacaoBLL.Carregar(_solicitacao_id, clsInfo.conexaosqldados);
            int posicaoItem = 0;
            foreach (DataRow row1 in dtCotacao1.Rows)
            {
                if (row1.RowState != DataRowState.Deleted &&
                    row1.RowState != DataRowState.Detached)
                {
                    if (clsParser.Int32Parse(row1["cotacao1_posicao"].ToString()) > posicaoItem)
                    {
                        posicaoItem = clsParser.Int32Parse(row1["cotacao1_posicao"].ToString());
                    }
                }
            }

            posicaoItem += 1;
            DataRow row = dtCotacao1.NewRow();
            row["cotacao1_posicao"] = posicaoItem;
            row["ID"] = 0;
            row["IDCOTACAO"] = 0;
            row["IDCODIGO"] = clsSolicitacaoInfo.idcodigo;
            row["COMPLEMENTO"] = clsSolicitacaoInfo.complemento;
            row["COMPLEMENTO1"] = clsSolicitacaoInfo.complemento1;
            row["DESCRICAOESP"] = "";
            row["IDSITTRIBA"] = clsInfo.zsituacaotriba;
            row["IDSITTRIBB"] = clsInfo.zsituacaotribb;
            row["MSG"] = "";
            row["CONSUMO"] = clsSolicitacaoInfo.consumo;
            row["IDHISTORICO"] = clsSolicitacaoInfo.idhistorico;
            row["IDCENTROCUSTO"] = clsSolicitacaoInfo.idcentrocusto;
            row["IDCODIGOCTABIL"] = clsSolicitacaoInfo.idcodigoctabil;
            row["TIPOPRODUTO"] = clsSolicitacaoInfo.tipoproduto;
            row["TIPOENTRADA"] = clsSolicitacaoInfo.tipoentrada;
            row["IDORDEMSERVICO"] = clsSolicitacaoInfo.idos;
            row["IDOSITEM"] = clsInfo.zordemservico;
            row["TIPODESTINO"] = clsSolicitacaoInfo.tipodestino;
            row["IDDESTINO"] = clsSolicitacaoInfo.iddestino;
            row["QTDEAUTORIZADA"] = clsSolicitacaoInfo.qtde;
            row["QTDEOK"] = clsSolicitacaoInfo.qtde;
            row["IDUNIDADE"] = clsSolicitacaoInfo.idunid;
            row["IDSOLICITACAO"] = clsSolicitacaoInfo.id;
            row["IDPEDIDOCOMPRA"] = clsSolicitacaoInfo.idpedidocompra;
            row["IDPEDIDOCOMPRAITEM"] = clsSolicitacaoInfo.idpedidocompraitem;
            row["IDFORNECEDORGANHOU"] = clsInfo.zempresaclienteid;
            row["OBSERVAR"] = "";
            row["IDCOTACAO2GANHOU"] = clsInfo.zcotacao2;
            row["TOTALPREVISTO"] = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TOTALPREVISTO from compras WHERE ID = " + clsSolicitacaoInfo.idpedidocompra).ToString());
            //
            if (clsSolicitacaoInfo.idcodigo > 0)
            {
                row["CODIGO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS where id =" + clsSolicitacaoInfo.idcodigo);
                row["NOME"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id =" + clsSolicitacaoInfo.idcodigo);
            }
            else
            {
                row["CODIGO"] = "";
                row["NOME"] = "";
            }
            //
            row["NROSOL"] = clsSolicitacaoInfo.numero;
            //
            if (clsSolicitacaoInfo.idos > 0)
            {
                row["NROOS"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from ORDEMSERVICO WHERE ID = " + clsSolicitacaoInfo.idos);
            }
            else
            {
                row["NROOS"] = "";
            }
            //
            if (clsSolicitacaoInfo.idpedidocompra > 0)
            {
                row["PEDCOMPRANRO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from compras WHERE ID = " + clsSolicitacaoInfo.idpedidocompra);
            }
            else
            {
                row["PEDCOMPRANRO"] = "";
            }
            //
            if (clsSolicitacaoInfo.idunid > 0)
            {
                row["UNID"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE WHERE ID =  " + clsSolicitacaoInfo.idunid);
            }
            else
            {
                row["UNID"] = "";
            }

            dtCotacao1.Rows.Add(row);


            //apagando a linha clicada
            dtSolicitacao.Select("ID = " + dgvSolicitacao.CurrentRow.Cells["ID"].Value.ToString())[0].Delete();
            DataTable dtSolicacaoFor_temp = clsSolicitacaoFornecedorBLL.GridDados(solicitacao_id, clsInfo.conexaosqldados);
            Int32 posicaoItem2 = 0;

            // Pegar a posicao da cotacao2
            foreach (DataRow row2 in dtCotacao2.Rows)
            {
                if (row2.RowState != DataRowState.Deleted &&
                    row2.RowState != DataRowState.Detached)
                {
                    if (clsParser.Int32Parse(row2["cotacao2_posicao"].ToString()) > posicaoItem2)
                    {
                        posicaoItem2 = clsParser.Int32Parse(row2["cotacao2_posicao"].ToString());
                    }
                }
            }

            foreach (DataRow linhaFor in dtSolicacaoFor_temp.Rows)
            {               

                posicaoItem2 += 1;

                DataRow rowCot2 = dtCotacao2.NewRow();
                rowCot2["cotacao2_posicao"] = posicaoItem2;
                rowCot2["cotacao2_posicao1_ref"] = posicaoItem;

                rowCot2["ID"] = 0;
                rowCot2["IDCOTACAO"] = id;
                rowCot2["IDCOTACAO1"] = 0;
                rowCot2["IDFORNECEDOR"] = linhaFor["IDFORNECEDOR"];
                rowCot2["IDFORMAPAGTO"] = clsInfo.zformapagto;
                rowCot2["IDCONDPAGTO"] = clsInfo.zcondpagto;
                rowCot2["IDUNIDADE"] = clsInfo.zunidade;
                rowCot2["IDUNIDADEINTERNA"] = clsInfo.zunidade;
                rowCot2["IDPEDIDOCOMPRA"] = clsInfo.zcompras;
                rowCot2["IDPEDIDOCOMPRAITEM"] = clsInfo.zcompras1;
                rowCot2["IDSITTRIBA"] = clsInfo.zsituacaotriba;
                rowCot2["IDSITTRIBB"] = clsInfo.zsituacaotribb;
                rowCot2["IDSITTRIBIPI"] = clsInfo.zsittribipi;
                rowCot2["IDSITTRIBPIS"] = clsInfo.zsittribpis;
                rowCot2["IDSITTRIBCOFINS"] = clsInfo.zsittribcofins;
                rowCot2["IDIPI"] = clsInfo.zipi;
                rowCot2["CONTATO"] = "";
                rowCot2["TELEFONE"] = "";
                rowCot2["QTDEINTERNA"] = 0;
                rowCot2["QTDEORCADA"] = clsSolicitacaoInfo.qtdesol;
                rowCot2["FATORCONV"] = 0;
                rowCot2["PRECOBRUTO"] = 0;
                rowCot2["PRECODESCPOR"] = 0;
                rowCot2["VALORDESCONTO"] = 0;
                rowCot2["PRECO"] = 0;
                rowCot2["TOTALMERCADO"] = 0;
                rowCot2["VALORFRETE"] = 0;
                rowCot2["VALORFRETEICMS"] = "";
                rowCot2["VALORSEGURO"] = 0;
                rowCot2["VALORSEGUROICMS"] = "";
                rowCot2["VALOROUTRAS"] = 0;
                rowCot2["VALOROUTRASICMS"] = "";
                rowCot2["TOTALNOTA"] = 0;
                rowCot2["PESO"] = 0;
                rowCot2["TOTALPESO"] = 0;
                rowCot2["BASEMP"] = 0;
                rowCot2["REDUCAO"] = 0;
                rowCot2["BASEICM"] = 0;
                rowCot2["ICM"] = 0;
                rowCot2["CUSTOICM"] = 0;
                rowCot2["BASEICMSUBST"] = 0;
                rowCot2["ICMSUBST"] = 0;
                rowCot2["ICMSSUBSTREDUCAO"] = 0;
                rowCot2["ICMINTERNO"] = 0;
                rowCot2["BCPISPASEP"] = 0;
                rowCot2["ALIQPISPASEP"] = 0;
                rowCot2["PISPASEP"] = 0;
                rowCot2["BCCOFINS"] = 0;
                rowCot2["ALIQCOFINS1"] = 0;
                rowCot2["COFINS1"] = 0;
                rowCot2["BCIPI"] = 0;
                rowCot2["IPI"] = 0;
                rowCot2["CUSTOIPI"] = 0;
                rowCot2["IRRFPORC"] = 0;
                rowCot2["IRRF"] = 0;
                rowCot2["INSSPORC"] = 0;
                rowCot2["INSS"] = 0;
                rowCot2["PISCOFINCSLLPORC"] = 0;
                rowCot2["PISCOFINSCSLL"] = 0;
                rowCot2["ISSPORC"] = 0;
                rowCot2["ISS"] = 0;
                rowCot2["PISPORC"] = 0;
                rowCot2["PIS"] = 0;
                rowCot2["COFINSPORC"] = 0;
                rowCot2["COFINS"] = 0;
                rowCot2["CSLLPORC"] = 0;
                rowCot2["CSLL"] = 0;
                rowCot2["FORNECEDOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                             "select cognome from CLIENTE where id = " + linhaFor["IDFORNECEDOR"].ToString(), "").ToString();

                rowCot2["FORMAPAGTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                           " select codigo from SITUACAOTIPOTITULO where id = " + clsInfo.zformapagto, "").ToString();

                rowCot2["CONDPAGTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                           " select codigo from CONDPAGTO where id = " + clsInfo.zcondpagto, "").ToString();


                dtCotacao2.Rows.Add(rowCot2);


                AdicionandoEntregas(posicaoItem2, posicaoItem, _solicitacao_id);
            }
        }


        private void ValidarFornecedoresSolicitacao()
        {

            //AGORA Validaremos os fornecedores da solicitacao
            DataTable dtSolicacaoFor_temp = clsSolicitacaoFornecedorBLL.GridDados(solicitacao_id, clsInfo.conexaosqldados);
            if (dtCotacao2.Rows.Count > 0)
            {
                int contador = dtCotacao2.Rows.Count;
                Boolean igual = false;

                foreach (DataRow linhaForne in dtSolicacaoFor_temp.Rows)
                {
                    igual = false;
                    foreach (DataRow rowCot2 in dtCotacao2.Rows)
                    {
                        if (rowCot2["FORNECEDOR"].ToString() == linhaForne["FORNECEDOR"].ToString())
                        {
                            igual = true;
                            break;


                        }
                    }
                    if (igual != true)
                    {
                        if (contador == 5)
                        {
                            throw new Exception("Fornecedores desta solicitação não podem ser inseridos nesta cotação");

                        }
                        else
                        {

                            contador++;

                        }

                    }
                }
            }

        }

        private void ValidarFornecedoresCotacao(Int32 idFornecedor)
        {
            if (dtCotacao2.Rows.Count > 0)
            {
                int contador = dtCotacao2.Rows.Count;
                Boolean igual = false;

                foreach (DataRow row in dtCotacao2.Rows)
                {
                    igual = false;
                    foreach (DataRow rowCot2 in dtCotacao2.Rows)
                    {
                        if (rowCot2["idfornecedor"].ToString() == idFornecedor.ToString())
                        {
                            igual = true;
                            break;


                        }
                    }
                    if (igual != true)
                    {
                        if (contador == 5)
                        {
                            throw new Exception("O fornecededor é incompatível para essa Cotacao");

                        }
                        else
                        {

                            contador++;

                        }

                    }
                }
            }
        }

        private void AdicionandoEntregas(Int32 _posicaoCotacao2, Int32 _posicaoCotacao1, Int32 _solicitacao_id)
        {
            //TRANSFERIMOS TODAS AS ENTREGAS DA SOLICITACAO PARA A COTAÇÃO, CADA COTACAO 2 RECEBERÁ TODAS AS ENTREGAS DA SOLICITAÇÃO
            DataTable dtSolicacaoEntrega_temp = clsSolicitacaoentregaBLL.GridDados(_solicitacao_id, clsInfo.conexaosqldados);

            foreach (DataRow linhaFor in dtSolicacaoEntrega_temp.Rows)
            {
                Int32 posicaoItem3 = 0;

                foreach (DataRow row1 in dtCotacaoEntrega.Rows)
                {
                    if (row1.RowState != DataRowState.Deleted &&
                   row1.RowState != DataRowState.Detached)
                    {
                        if (clsParser.Int32Parse(row1["cotacaoentrega_posicao"].ToString()) > posicaoItem3)
                        {
                            posicaoItem3 = clsParser.Int32Parse(row1["cotacaoentrega_posicao"].ToString());
                        }
                    }
                }

                posicaoItem3 += 1;

                DataRow rowCotEnt = dtCotacaoEntrega.NewRow();
                rowCotEnt["cotacaoentrega_posicao"] = posicaoItem3;
                rowCotEnt["cotacaoentrega_posicao_ref_cotacao1"] = _posicaoCotacao1;
                rowCotEnt["cotacaoentrega_posicao_ref_cotacao2"] = _posicaoCotacao2;
                rowCotEnt["ID"] = 0;
                rowCotEnt["IDCOTACAO"] = id;
                rowCotEnt["IDCOTACAO1"] = 0;
                rowCotEnt["IDCOTACAO2"] = 0;
                rowCotEnt["QTDEENTREGA"] = linhaFor["QTDEENTREGA"];
                rowCotEnt["DATAENTREGA"] = linhaFor["DATAENTREGA"];

                dtCotacaoEntrega.Rows.Add(rowCotEnt);
                this.CotacaoEntregaSomar();

            }
        }

        private void dgvCotacao1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspCotacao1Alterar.PerformClick();
        }

        void Cotacao2Carregar()
        {
            clsCotacao2Info = new clsCotacao2Info();
            clsCotacao2InfoOld = new clsCotacao2Info();

            if (cotacao2_posicao == 0)
            {
                cotacao2_posicao = dtCotacao2.Rows.Count + 1;

                clsCotacao2Info.idcondpagto = clsInfo.zcondpagto;
                clsCotacao2Info.idcotacao = id;
                clsCotacao2Info.idcotacao1 = cotacao1_id;
                clsCotacao2Info.idformapagto = clsInfo.zformapagto;
                clsCotacao2Info.idfornecedor = clsInfo.zempresaid;  //clsInfo.zempresaclienteprospeccaoid;
                clsCotacao2Info.idipi = clsInfo.zipi;
                clsCotacao2Info.idunidade = clsInfo.zunidade;
                clsCotacao2Info.idunidadeinterna = clsInfo.zunidade;
                clsCotacao2Info.idpedidocompra = clsInfo.zcompras;
                clsCotacao2Info.idpedidocompraitem = clsInfo.zcompras1;
                clsCotacao2Info.idsittriba = clsInfo.zsituacaotriba;
                clsCotacao2Info.idsittribb = clsInfo.zsituacaotribb;
                clsCotacao2Info.idsittribpis = clsInfo.zsittribpis;
                clsCotacao2Info.idsittribipi = clsInfo.zsittribipi;
                clsCotacao2Info.idsittribcofins = clsInfo.zsittribcofins;
                clsCotacao2Info.qtdeinterna = clsParser.DecimalParse(Cotacao1_tbxQtdeAutorizada.Text);
                clsCotacao2Info.qtdeorcada = clsParser.DecimalParse(Cotacao1_tbxQtdeAutorizada.Text);
                clsCotacao2Info.valorfreteicms = "N";
                clsCotacao2Info.valoroutrasicms = "N";
                clsCotacao2Info.valorseguroicms = "N";
            }
            else
            {
                Cotacao2GridToInfo(clsCotacao2Info, cotacao2_posicao);
            }

            Decimal por = clsCotacao2Info.irrfporc +
                          clsCotacao2Info.inssporc +
                          clsCotacao2Info.piscofincsllporc +
                          clsCotacao2Info.issporc +
                          clsCotacao2Info.pisporc +
                          clsCotacao2Info.cofinsporc +
                          clsCotacao2Info.csllporc;

            if (por == 0 &&
                Cotacao1_lbxTipoProduto.Text.Substring(0, 2) == "30")
            {
                clsCotacao2Info.irrfporc = clsInfo.zfiscalirrf;
                clsCotacao2Info.inssporc = clsInfo.zfiscalinss;
                clsCotacao2Info.piscofincsllporc = clsInfo.zfiscalpiscofins;
                clsCotacao2Info.issporc = clsInfo.zfiscaliss;
                clsCotacao2Info.pisporc = clsInfo.zfiscalpis;
                clsCotacao2Info.cofinsporc = clsInfo.zfiscalcofins;
                clsCotacao2Info.csllporc = clsInfo.zfiscalcsll;
            }
            else
            {
                clsCotacao2Info.irrfporc = 0;
                clsCotacao2Info.inssporc = 0;
                clsCotacao2Info.piscofincsllporc = 0;
                clsCotacao2Info.issporc = 0;
                clsCotacao2Info.pisporc = 0;
                clsCotacao2Info.cofinsporc = 0;
                clsCotacao2Info.csllporc = 0;
            }

            Cotacao2Campos(clsCotacao2Info);
            Cotacao2FillInfo(clsCotacao2InfoOld);

            if (Cotacao1_lbxTipoProduto.Text.Substring(0, 2) == "30") gbxPrestacaoServico.Enabled = true;
            else gbxPrestacaoServico.Enabled = false;

            gbxCodigo2.Enabled = false;

            guiaatual = 2;

            dtCotacaoEntrega.DefaultView.RowFilter = "cotacaoentrega_posicao_ref_cotacao2=" + cotacao2_posicao;

            Cotacao2_tbxContato.Select();
            Cotacao2_tbxContato.SelectAll();

        }

        private void Cotacao2GridToInfo(clsCotacao2Info info, Int32 posicao)
        {
            DataRow row = dtCotacao2.Select("cotacao2_posicao = " + posicao)[0];

            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idcotacao = clsParser.Int32Parse(row["idcotacao"].ToString());
            info.idcotacao1 = clsParser.Int32Parse(row["idcotacao1"].ToString());  
            info.idfornecedor = clsParser.Int32Parse(row["idfornecedor"].ToString());
            info.idformapagto = clsParser.Int32Parse(row["idformapagto"].ToString());
            info.idcondpagto = clsParser.Int32Parse(row["idcondpagto"].ToString());
            info.idunidade = clsParser.Int32Parse(row["idunidade"].ToString());
            info.idunidadeinterna = clsParser.Int32Parse(row["idunidadeinterna"].ToString());
            info.idpedidocompra = clsParser.Int32Parse(row["idpedidocompra"].ToString());
            info.idpedidocompraitem = clsParser.Int32Parse(row["idpedidocompraitem"].ToString());
            info.idsittriba = clsParser.Int32Parse(row["idsittriba"].ToString());
            info.idsittribb = clsParser.Int32Parse(row["idsittribb"].ToString());
            info.idsittribipi = clsParser.Int32Parse(row["idsittribipi"].ToString());
            info.idsittribpis = clsParser.Int32Parse(row["idsittribpis"].ToString());
            info.idsittribcofins = clsParser.Int32Parse(row["idsittribcofins"].ToString());
            info.idipi = clsParser.Int32Parse(row["idipi"].ToString());

            info.contato = row["contato"].ToString().Substring(0, 19);
            info.telefone = row["telefone"].ToString();
            info.qtdeinterna = clsParser.DecimalParse(row["qtdeinterna"].ToString());
            info.qtdeorcada = clsParser.DecimalParse(row["qtdeorcada"].ToString());
            info.fatorconv = clsParser.DecimalParse(row["fatorconv"].ToString());
            info.precobruto = clsParser.DecimalParse(row["precobruto"].ToString());
            info.precodescpor = clsParser.DecimalParse(row["precodescpor"].ToString());
            info.valordesconto = clsParser.DecimalParse(row["valordesconto"].ToString());
            info.preco = clsParser.DecimalParse(row["preco"].ToString());
            info.totalmercado = clsParser.DecimalParse(row["totalmercado"].ToString());
            info.valorfrete = clsParser.DecimalParse(row["valorfrete"].ToString());
            info.valorfreteicms = row["valorfreteicms"].ToString();
            info.valorseguro = clsParser.DecimalParse(row["valorseguro"].ToString());
            info.valorseguroicms = row["valorseguroicms"].ToString();
            info.valoroutras = clsParser.DecimalParse(row["valoroutras"].ToString());
            info.valoroutrasicms = row["valoroutrasicms"].ToString();
            info.totalnota = clsParser.DecimalParse(row["totalnota"].ToString());
            info.peso = clsParser.DecimalParse(row["peso"].ToString());
            info.basemp = clsParser.DecimalParse(row["basemp"].ToString());
            info.reducao = clsParser.DecimalParse(row["reducao"].ToString());
            info.baseicm = clsParser.DecimalParse(row["baseicm"].ToString());
            info.icm = clsParser.DecimalParse(row["icm"].ToString());
            info.custoicm = clsParser.DecimalParse(row["custoicm"].ToString());
            info.baseicmsubst = clsParser.DecimalParse(row["baseicmsubst"].ToString());
            info.icmsubst = clsParser.DecimalParse(row["icmsubst"].ToString());
            //info.icmssubstreducao = clsParser.DecimalParse(row["icmssubstreducao"].ToString());
            info.icminterno = clsParser.DecimalParse(row["icminterno"].ToString());
            info.bcpispasep = clsParser.DecimalParse(row["bcpispasep"].ToString());
            info.aliqpispasep = clsParser.DecimalParse(row["aliqpispasep"].ToString());
            info.pispasep = clsParser.DecimalParse(row["pispasep"].ToString());
            //info.bccofins1 = clsParser.DecimalParse(row["bccofins1"].ToString());
            info.aliqcofins1 = clsParser.DecimalParse(row["aliqcofins1"].ToString());
            info.cofins1 = clsParser.DecimalParse(row["cofins1"].ToString());
            info.bcipi = clsParser.DecimalParse(row["bcipi"].ToString());
            info.ipi = clsParser.DecimalParse(row["ipi"].ToString());
            info.custoipi = clsParser.DecimalParse(row["custoipi"].ToString());
            info.cofins = clsParser.DecimalParse(row["cofins"].ToString());
            info.cofinsporc = clsParser.DecimalParse(row["cofinsporc"].ToString());
            info.contato = row["contato"].ToString().Substring(0, 19); ;
            info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
            info.csll = clsParser.DecimalParse(row["csll"].ToString());
            info.csllporc = clsParser.DecimalParse(row["csllporc"].ToString());
            info.irrf = clsParser.DecimalParse(row["irrf"].ToString());
            info.irrfporc = clsParser.DecimalParse(row["irrfporc"].ToString());
            info.inss = clsParser.DecimalParse(row["inss"].ToString());
            info.inssporc = clsParser.DecimalParse(row["inssporc"].ToString());
            info.piscofinscsll = clsParser.DecimalParse(row["piscofinscsll"].ToString());
            info.piscofincsllporc = clsParser.DecimalParse(row["piscofincsllporc"].ToString());
            info.iss = clsParser.DecimalParse(row["iss"].ToString());
            info.issporc = clsParser.DecimalParse(row["issporc"].ToString());
            info.pis = clsParser.DecimalParse(row["pis"].ToString());
            info.pisporc = clsParser.DecimalParse(row["pisporc"].ToString());
            info.cofins = clsParser.DecimalParse(row["cofins"].ToString());
            info.cofinsporc = clsParser.DecimalParse(row["cofinsporc"].ToString());
            cotacao2_fornecedor = row["fornecedor"].ToString();
            cotacao2_formapagto = row["formapagto"].ToString();
            cotacao2_condpagto = row["condpagto"].ToString();

        }

        private void Cotacao2Campos(clsCotacao2Info info)
        {
            cotacao2_id = info.id;
            cotacao2_idcotacao = info.idcotacao;
            cotacao2_idcotacao1 = info.idcotacao1;
            cotacao2_idfornecedor = info.idfornecedor;
            cotacao2_idformapagto = info.idformapagto;
            cotacao2_idcondpagto = info.idcondpagto;
            cotacao2_idunidade = info.idunidade;
            cotacao2_idunidadeinterna = info.idunidadeinterna;
            cotacao2_idpedidocompra = info.idpedidocompra;
            cotacao2_idpedidocompraitem = info.idpedidocompraitem;
            cotacao2_idsittriba = info.idsittriba;
            cotacao2_idsittribb = info.idsittribb;
            cotacao2_idsittribipi = info.idsittribipi;
            cotacao2_idsittribpis = info.idsittribpis;
            cotacao2_idsittribcofins = info.idsittribcofins;
            cotacao2_idipi = info.idipi;

            Cotacao2_Fornecedor_Carrega();
            Cotacao2_FormaPagto_Carrega();
            Cotacao2_CondPagto_Carrega();
            Cotacao2_Unidade_Carrega();
            Cotacao2_Unidade_Interna_Carrega();
            Cotacao2_Ipi_Carrega();
            Cotacao2_Sittriba_Carrega();
            Cotacao2_Sittribb_Carrega();
            Cotacao2_Sittribcofins1_Carrega();
            Cotacao2_Sittribipi_Carrega();
            Cotacao2_Sittribpis_Carrega();

            Cotacao2_tbxContato.Text = info.contato;
            Cotacao2_tbxTelefone.Text = info.telefone;
            Cotacao2_tbxQtdeinterna.Text = info.qtdeinterna.ToString("n5");
            Cotacao2_tbxQtdeorcada.Text = info.qtdeorcada.ToString("n5");
            Cotacao2_tbxFatorConv.Text = info.fatorconv.ToString("n5");
            Cotacao2_tbxPrecoBruto.Text = info.precobruto.ToString("n5");
            Cotacao2_tbxPrecoDescPor.Text = info.precodescpor.ToString("n5");
            Cotacao2_tbxValorDesconto.Text = info.valordesconto.ToString("n2");
            Cotacao2_tbxPreco.Text = info.preco.ToString("n2");
            Cotacao2_tbxTotalMercado.Text = info.totalmercado.ToString("n2");
            Cotacao2_tbxValorFrete.Text = info.valorfrete.ToString("n2");
            Cotacao2_ckxValorFreteIcms.Checked = (info.valorfreteicms == "S");
            Cotacao2_tbxValorSeguro.Text = info.valorseguro.ToString("n2");
            Cotacao2_ckxValorSeguroIcms.Checked = (info.valorseguroicms == "S");
            Cotacao2_tbxValorOutras.Text = info.valoroutras.ToString("n2");
            Cotacao2_ckxValorOutrasIcms.Checked = (info.valoroutrasicms == "S");
            Cotacao2_tbxTotalNota.Text = info.totalnota.ToString("n2");
            Cotacao2_tbxPeso.Text = info.peso.ToString("n3");
            Cotacao2_tbxBaseMP.Text = info.basemp.ToString("n2");
            Cotacao2_tbxReducao.Text = info.reducao.ToString("n2");
            Cotacao2_tbxBaseIcm.Text = info.baseicm.ToString("n2");
            Cotacao2_tbxIcm.Text = info.icm.ToString();
            Cotacao2_tbxCustoIcm.Text = info.custoicm.ToString();
            Cotacao2_tbxBaseIcmSubst.Text = info.baseicmsubst.ToString("n2");
            Cotacao2_tbxIcmsubst.Text = info.icmsubst.ToString("n2");
            //Cotacao2_tbxIcmssubstreducao.Text = info.icmssubstreducao.ToString("n2");
            Cotacao2_tbxIcminterno.Text = info.icminterno.ToString("n2");
            Cotacao2_tbxBcpispasep.Text = info.bcpispasep.ToString("n2");
            Cotacao2_tbxAliqpispasep.Text = info.aliqpispasep.ToString("n2");
            Cotacao2_tbxPispasep.Text = info.pispasep.ToString("n2");
            //Cotacao2_tbxBccofins1.Text = info.bccofins1.ToString("n2");
            Cotacao2_tbxAliqcofins1.Text = info.aliqcofins1.ToString("n2");
            Cotacao2_tbxCofins1.Text = info.cofins1.ToString("n2");
            Cotacao2_tbxBcipi.Text = info.bcipi.ToString("n2");
            Cotacao2_tbxIPI.Text = info.ipi.ToString("n2");
            Cotacao2_tbxCustoIPI.Text = info.custoipi.ToString();
            Cotacao2_tbxCofins.Text = info.cofins.ToString("n2");
            Cotacao2_tbxCofinsPorc.Text = info.cofinsporc.ToString("n2");
            Cotacao2_tbxContato.Text = info.contato;
            Cotacao2_tbxPesoTotal.Text = info.totalpeso.ToString("n2");
            Cotacao2_tbxCsll.Text = info.csll.ToString();
            Cotacao2_tbxCsllPorc.Text = info.csllporc.ToString();
            Cotacao2_tbxIrrf.Text = info.irrf.ToString("n2");
            Cotacao2_tbxIrrfPorc.Text = info.irrfporc.ToString("n2");
            Cotacao2_tbxInss.Text = info.inss.ToString("n2");
            Cotacao2_tbxInssPorc.Text = info.inssporc.ToString("n2");
            Cotacao2_tbxPisCofinsCsll.Text = info.piscofinscsll.ToString("n2");
            Cotacao2_tbxPisCofinsCsllPorc.Text = info.piscofincsllporc.ToString("n2");
            Cotacao2_tbxIss.Text = info.iss.ToString("n2");
            Cotacao2_tbxIssPorc.Text = info.issporc.ToString("n2");
            Cotacao2_tbxPis.Text = info.pis.ToString("n2");
            Cotacao2_tbxPisPorc.Text = info.pisporc.ToString("n2");
            Cotacao2_tbxCofins.Text = info.cofins.ToString("n2");
            Cotacao2_tbxCofinsPorc.Text = info.cofinsporc.ToString("n2");

            // vem do tab anterior
            Cotacao1_tbxCodigoCop.Text = Cotacao1_tbxCodigo.Text;
            Cotacao1_tbxNomeCop.Text = Cotacao1_tbxNome.Text;
            Cotacao1_tbxComplementoCop.Text = Cotacao1_tbxComplemento.Text;
            Cotacao1_tbxComplemento1Cop.Text = Cotacao1_tbxComplemento1.Text;
            //Cotacao1_tbxDescricaoEspCop.Text = Cotacao1_tbxDescricaoEsp.Text;

            // padronizar a Programação de Entrega
            if (clsParser.Int32Parse(CotacaoEntrega_tbxQtdeEntregas.Text) == 0)
            {
                CotacaoEntrega_tbxQtdeEntregas.Text = "1";
                CotacaoEntrega_PrimeiraEntrega.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                CotacaoEntrega_lbxPeriodoEntrega.SelectedIndex = CotacaoEntrega_lbxPeriodoEntrega.FindString("Unica");

            }

        }

        private void Cotacao2FillInfo(clsCotacao2Info info)
        {
            info.id = cotacao2_id;
            info.idcotacao = cotacao2_idcotacao;
            info.idcotacao1 = cotacao2_idcotacao1;
            info.idfornecedor = cotacao2_idfornecedor;
            info.idformapagto = cotacao2_idformapagto;
            info.idcondpagto = cotacao2_idcondpagto;
            info.idunidade = cotacao2_idunidade;
            info.idunidadeinterna = cotacao2_idunidadeinterna;
            info.idpedidocompra = cotacao2_idpedidocompra;
            info.idpedidocompraitem = cotacao2_idpedidocompraitem;
            info.idsittriba = cotacao2_idsittriba;
            info.idsittribb = cotacao2_idsittribb;
            info.idsittribipi = cotacao2_idsittribipi;
            info.idsittribpis = cotacao2_idsittribpis;
            info.idsittribcofins = cotacao2_idsittribcofins;
            info.idipi = cotacao2_idipi;

            info.contato = Cotacao2_tbxContato.Text;
            info.telefone = Cotacao2_tbxTelefone.Text;
            info.qtdeinterna = clsParser.DecimalParse(Cotacao2_tbxQtdeinterna.Text);
            info.qtdeorcada = clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text);
            info.fatorconv = clsParser.DecimalParse(Cotacao2_tbxFatorConv.Text);
            info.precobruto = clsParser.DecimalParse(Cotacao2_tbxPrecoBruto.Text);
            info.precodescpor = clsParser.DecimalParse(Cotacao2_tbxPrecoDescPor.Text);
            info.valordesconto = clsParser.DecimalParse(Cotacao2_tbxValorDesconto.Text);
            info.preco = clsParser.DecimalParse(Cotacao2_tbxPreco.Text);
            info.totalmercado = clsParser.DecimalParse(Cotacao2_tbxTotalMercado.Text);
            info.valorfrete = clsParser.DecimalParse(Cotacao2_tbxValorFrete.Text);

            if (Cotacao2_ckxValorFreteIcms.Checked == true) info.valorfreteicms = "S";
            else info.valorfreteicms = "N";

            info.valorseguro = clsParser.DecimalParse(Cotacao2_tbxValorSeguro.Text);

            if (Cotacao2_ckxValorSeguroIcms.Checked == true) info.valorseguroicms = "S";
            else info.valorseguroicms = "N";

            info.valoroutras = clsParser.DecimalParse(Cotacao2_tbxValorOutras.Text);
            if (Cotacao2_ckxValorOutrasIcms.Checked == true) info.valoroutrasicms = "S";
            else info.valoroutrasicms = "N";

            info.totalnota = clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text);
            info.peso = clsParser.DecimalParse(Cotacao2_tbxPeso.Text);
            info.basemp = clsParser.DecimalParse(Cotacao2_tbxBaseMP.Text);
            info.reducao = clsParser.DecimalParse(Cotacao2_tbxReducao.Text);
            info.baseicm = clsParser.DecimalParse(Cotacao2_tbxBaseIcm.Text);
            info.icm = clsParser.DecimalParse(Cotacao2_tbxIcm.Text);
            info.custoicm = clsParser.DecimalParse(Cotacao2_tbxCustoIcm.Text);
            info.baseicmsubst = clsParser.DecimalParse(Cotacao2_tbxBaseIcmSubst.Text);
            info.icmsubst = clsParser.DecimalParse(Cotacao2_tbxIcmsubst.Text);
            //info.icmssubstreducao = clsParser.DecimalParse(Cotacao2_tbxIcmssubstreducao.Text);
            info.icminterno = clsParser.DecimalParse(Cotacao2_tbxIcminterno.Text);
            info.bcpispasep = clsParser.DecimalParse(Cotacao2_tbxBcpispasep.Text);
            info.aliqpispasep = clsParser.DecimalParse(Cotacao2_tbxAliqpispasep.Text);
            info.pispasep = clsParser.DecimalParse(Cotacao2_tbxPispasep.Text);
            //info.bccofins1 = clsParser.DecimalParse(Cotacao2_tbxBccofins1.Text);
            info.aliqcofins1 = clsParser.DecimalParse(Cotacao2_tbxAliqcofins1.Text);
            info.cofins1 = clsParser.DecimalParse(Cotacao2_tbxCofins1.Text);
            info.bcipi = clsParser.DecimalParse(Cotacao2_tbxBcipi.Text);
            info.ipi = clsParser.DecimalParse(Cotacao2_tbxIPI.Text);
            info.custoipi = clsParser.DecimalParse(Cotacao2_tbxCustoIPI.Text);
            info.cofins = clsParser.DecimalParse(Cotacao2_tbxCofins.Text);
            info.cofinsporc = clsParser.DecimalParse(Cotacao2_tbxCofinsPorc.Text);
            info.contato = Cotacao2_tbxContato.Text;
            info.totalpeso = clsParser.DecimalParse(Cotacao2_tbxPesoTotal.Text);
            info.csll = clsParser.DecimalParse(Cotacao2_tbxCsll.Text);
            info.csllporc = clsParser.DecimalParse(Cotacao2_tbxCsllPorc.Text);
            info.irrf = clsParser.DecimalParse(Cotacao2_tbxIrrf.Text);
            info.irrfporc = clsParser.DecimalParse(Cotacao2_tbxIrrfPorc.Text);
            info.inss = clsParser.DecimalParse(Cotacao2_tbxInss.Text);
            info.inssporc = clsParser.DecimalParse(Cotacao2_tbxInssPorc.Text);
            info.piscofinscsll = clsParser.DecimalParse(Cotacao2_tbxPisCofinsCsll.Text);
            info.piscofincsllporc = clsParser.DecimalParse(Cotacao2_tbxPisCofinsCsllPorc.Text);
            info.iss = clsParser.DecimalParse(Cotacao2_tbxIss.Text);
            info.issporc = clsParser.DecimalParse(Cotacao2_tbxIssPorc.Text);
            info.pis = clsParser.DecimalParse(Cotacao2_tbxPis.Text);
            info.pisporc = clsParser.DecimalParse(Cotacao2_tbxPisPorc.Text);
            info.cofins = clsParser.DecimalParse(Cotacao2_tbxCofins.Text);
            info.cofinsporc = clsParser.DecimalParse(Cotacao2_tbxCofinsPorc.Text);
        }

        void Cotacao2FillInfoToGrid(clsCotacao2Info info)
        {
            ValidarFornecedoresCotacao(info.idfornecedor);
            DataRow row;
            DataRow[] rows = dtCotacao2.Select("cotacao2_posicao = " + cotacao2_posicao);
            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtCotacao2.NewRow();
            }

            row["id"] = info.id;
            row["idcotacao"] = info.idcotacao;
            row["idcotacao1"] = info.idcotacao1;
            row["idfornecedor"] = info.idfornecedor;
            row["idformapagto"] = info.idformapagto;
            row["idcondpagto"] = info.idcondpagto;
            row["idunidade"] = info.idunidade;
            row["idunidadeinterna"] = info.idunidadeinterna;
            row["idpedidocompra"] = info.idpedidocompra;
            row["idpedidocompraitem"] = info.idpedidocompraitem;
            row["idsittriba"] = info.idsittriba;
            row["idsittribb"] = info.idsittribb;
            row["idsittribipi"] = info.idsittribipi;
            row["idsittribpis"] = info.idsittribpis;
            row["idsittribcofins"] = info.idsittribcofins;
            row["idipi"] = info.idipi;

            row["contato"] = info.contato.PadLeft(19,' ').Substring(0, 19);
            row["telefone"] = info.telefone;
            row["qtdeinterna"] = info.qtdeinterna;
            row["qtdeorcada"] = info.qtdeorcada;
            row["fatorconv"] = info.fatorconv;
            row["precobruto"] = info.precobruto;
            row["precodescpor"] = info.precodescpor;
            row["valordesconto"] = info.valordesconto;
            row["preco"] = info.preco;
            row["totalmercado"] = info.totalmercado;
            row["valorfrete"] = info.valorfrete;
            row["valorfreteicms"] = info.valorfreteicms;
            row["valorseguro"] = info.valorseguro;
            row["valorseguroicms"] = info.valorseguroicms;
            row["valoroutras"] = info.valoroutras;
            row["valoroutrasicms"] = info.valoroutrasicms;
            row["totalnota"] = info.totalnota;
            row["peso"] = info.peso;
            row["basemp"] = info.basemp;
            row["reducao"] = info.reducao;
            row["baseicm"] = info.baseicm;
            row["icm"] = info.icm;
            row["custoicm"] = info.custoicm;
            row["baseicmsubst"] = info.baseicmsubst;
            row["icmsubst"] = info.icmsubst;
            //row["icmssubstreducao"] = info.icmssubstreducao;
            row["icminterno"] = info.icminterno;
            row["bcpispasep"] = info.bcpispasep;
            row["aliqpispasep"] = info.aliqpispasep;
            row["pispasep"] = info.pispasep;
            //row["bccofins1"] = info.bccofins1;
            row["aliqcofins1"] = info.aliqcofins1;
            row["cofins1"] = info.cofins1;
            row["bcipi"] = info.bcipi;
            row["ipi"] = info.ipi;
            row["custoipi"] = info.custoipi;
            row["cofins"] = info.cofins;
            row["cofinsporc"] = info.cofinsporc;
            row["contato"] = info.contato.PadLeft(19, ' ').Substring(0, 19);
            row["totalpeso"] = info.totalpeso;
            row["csll"] = info.csll;
            row["csllporc"] = info.csllporc;
            row["irrf"] = info.irrf;
            row["irrfporc"] = info.irrfporc;
            row["inss"] = info.inss;
            row["inssporc"] = info.inssporc;
            row["piscofinscsll"] = info.piscofinscsll;
            row["piscofincsllporc"] = info.piscofincsllporc;
            row["iss"] = info.iss;
            row["issporc"] = info.issporc;
            row["pis"] = info.pis;
            row["pisporc"] = info.pisporc;
            row["cofins"] = info.cofins;
            row["cofinsporc"] = info.cofinsporc;
            row["fornecedor"] = cotacao2_fornecedor;

            row["formapagto"] = cotacao2_formapagto;

            row["condpagto"] = cotacao2_condpagto;

            // CAMPOS DE RUNTIME
            //row["UNID"] = Cotacao2_tbxUnidade.Text;
            //row["FORNECEDOR"] = Cotacao2_tbxCognome.Text;
            //row["CONDPAGTO"] = Cotacao2_tbxCondPagto.Text;
            //row["NUMERO"] = Cotacao1_tbxSolicitacao.Text;          

            if (rows.Length == 0)
            {
                row["cotacao2_posicao"] = cotacao2_posicao;
                row["cotacao2_posicao1_ref"] = Cotacao1_posicao;
                dtCotacao2.Rows.Add(row);
            }
        }

        //        #region Lupas

        //private Boolean Autorizante_Check()
        //{
        //    Int32 idtmp = clsParser.Int32Parse(Procedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from usuario where usuario='" + tbxAutorizante.Text + "'"));

        //    if (idtmp == idautorizante)
        //    {
        //        return false;
        //    }
        //    else if (idtmp == 0)
        //    {
        //        idtmp = clsInfo.zusuarioid;
        //    }

        //    idautorizante = idtmp;

        //    return true;
        //}

        //private void Autorizante_Carrega()
        //{
        //    tbxAutorizante.Text = Procedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usuario from usuario where id = "  + idautorizante);
        //}

        private Boolean Cotacao1_Codigo_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + Cotacao1_tbxCodigo.Text + "'"));

            if (idtmp == cotacao1_idcodigo)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zpecas;
            }

            cotacao1_idcodigo = idtmp;

            cotacao1_idunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idunidade from pecas where id=" + cotacao1_idcodigo));
            Cotacao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + cotacao1_idunidade );

            return true;
        }

        private void Cotacao1_Codigo_Carrega()
        {
            //cotacao1_idcodigo =  clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + Cotacao1_tbxCodigo.Text + "'"));
            clsPecasInfo = clsPecasBLL.Carregar(cotacao1_idcodigo, clsInfo.conexaosqldados);

            Cotacao1_tbxCodigo.Text = clsPecasInfo.codigo;
            Cotacao1_tbxNome.Text = clsPecasInfo.nome;
            pbxFoto.Image = clsPecasInfo.foto;
            pbxFoto1.Image = clsPecasInfo.foto;
            cotacao1_idunidade = clsPecasInfo.idunidade;
            Cotacao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + cotacao1_idunidade + "");
            cotacao1_idhistorico = clsPecasInfo.idhistoricobco;
            Cotacao1_tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + cotacao1_idhistorico);
            Cotacao1_lbxTipoEntrada1.SelectedIndex = Cotacao1_lbxTipoEntrada1.FindString(clsPecasInfo.tipoproduto);
            //String consumo = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONSUMO from PECAS where ID=" + cotacao1_idcodigo);
            //if (consumo == "N")
            //{
            //    Cotacao1_rbnRevenda.Checked = true;
            //}
            //else
            //{
            //    Cotacao1_rbnConsumo.Checked = true;
            //}


        }

        private Boolean Cotacao1_CodigoDestino_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + Cotacao1_tbxCodigoDestino.Text + "'"));

            if (idtmp == cotacao1_iddestino)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zpecas;
            }

            cotacao1_iddestino = idtmp;

            return true;
        }

        private void Cotacao1_CodigoDestino_Carrega()
        {
            Cotacao1_tbxCodigoDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + cotacao1_iddestino);
            Cotacao1_tbxNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + cotacao1_iddestino);
        }

        private Boolean Cotacao1_OrdemServico_Check()
        {
            Int32 osNumero;
            Int32 osAno;

            if (Cotacao1_tbxOrdemServico.Text.Trim().Length > 0)
            {
                osNumero = clsParser.Int32Parse(Cotacao1_tbxOrdemServico.Text.Trim().Substring(0, Cotacao1_tbxOrdemServico.Text.IndexOf(" - ")));
                osAno = clsParser.Int32Parse(Cotacao1_tbxOrdemServico.Text.Trim().Substring(Cotacao1_tbxOrdemServico.Text.IndexOf(" - ") + 3));
            }
            else
            {
                osNumero = 0;
                osAno = 0;
            }

            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ordemservico where numero=" + osNumero + " and year(data)=" + osAno));

            if (idtmp == cotacao1_idordemservico)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zordemservico;
            }

            cotacao1_idordemservico = idtmp;

            return true;
        }

        private void Cotacao1_OrdemServico_Carrega()
        {
            Cotacao1_tbxOrdemServico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CONVERT(VARCHAR, NUMERO) + ' - ' + CONVERT(VARCHAR, YEAR(DATA)) FROM ORDEMSERVICO WHERE ID=" + cotacao1_idordemservico);
        }

        private Boolean Cotacao1_Historico_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from historicos where codigo='" + Cotacao1_tbxHistorico.Text + "'"));

            if (idtmp == cotacao1_idhistorico)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zhistoricos;
            }

            cotacao1_idhistorico = idtmp;

            return true;
        }

        private void Cotacao1_Historico_Carrega()
        {
            Cotacao1_tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from historicos where id=" + cotacao1_idhistorico);
        }

        private Boolean Cotacao1_CentroCusto_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from centrocustos where codigo='" + Cotacao1_tbxCentroCusto.Text + "'"));

            if (idtmp == cotacao1_idcentrocusto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcentrocustos;
            }

            cotacao1_idcentrocusto = idtmp;

            return true;
        }

        private void Cotacao1_CentroCusto_Carrega()
        {
            Cotacao1_tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from centrocustos where id=" + cotacao1_idcentrocusto);
        }

        private Boolean Cotacao1_Unidade_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from unidade where codigo='" + Cotacao1_tbxUnidade.Text + "'"));

            if (idtmp == cotacao1_idunidade)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zunidade;
            }

            cotacao1_idunidade = idtmp;

            return true;
        }

        private void Cotacao1_Unidade_Carrega()
        {
            Cotacao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + cotacao1_idunidade);
        }

        private Boolean Cotacao2_FormaPagto_Check()
        {
            String codigo;

            if (Cotacao2_tbxFormaPagto.Text.Trim().Length > 0)
            {
                codigo = Cotacao2_tbxFormaPagto.Text.Trim().Substring(0, Cotacao2_tbxFormaPagto.Text.Trim().IndexOf(" = "));
            }
            else
            {
                codigo = "";
            }

            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo='" + codigo + "'"));

            if (idtmp == cotacao2_idformapagto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zformapagto;
            }

            cotacao2_idformapagto = idtmp;

            return true;
        }

        private void Cotacao2_FormaPagto_Carrega()
        {
            Cotacao2_tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from situacaotipotitulo where id=" + cotacao2_idformapagto);
            cotacao2_formapagto = Cotacao2_tbxFormaPagto.Text;
        }

        private Boolean Cotacao2_CondPagto_Check()
        {
            String codigo;

            if (Cotacao2_tbxCondPagto.Text.Trim().Length > 0)
            {
                codigo = Cotacao2_tbxCondPagto.Text.Trim().Substring(0, Cotacao2_tbxCondPagto.Text.Trim().IndexOf(" = "));
            }
            else
            {
                codigo = "";
            }

            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo='" + codigo + "'"));

            if (idtmp == cotacao2_idcondpagto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcondpagto;
            }

            cotacao2_idcondpagto = idtmp;

            return true;
        }

        private void Cotacao2_CondPagto_Carrega()
        {

            Cotacao2_tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from condpagto where id=" + cotacao2_idcondpagto);
            cotacao2_condpagto = Cotacao2_tbxCondPagto.Text;
        }

        private Boolean Cotacao2_Unidade_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from unidade where codigo='" + Cotacao2_tbxUnidade.Text + "'"));

            if (idtmp == cotacao2_idunidade)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zunidade;
            }

            cotacao2_idunidade = idtmp;

            return true;
        }

        private void Cotacao2_Unidade_Carrega()
        {
            Cotacao2_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + cotacao2_idunidade);
        }

        private Boolean Cotacao2_Unidade_Interna_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from unidade where codigo='" + Cotacao2_tbxUnidade_Interno.Text + "'"));

            if (idtmp == cotacao2_idunidadeinterna)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zunidade;
            }

            cotacao2_idunidadeinterna = idtmp;

            return true;
        }

        private void Cotacao2_Unidade_Interna_Carrega()
        {
            Cotacao2_tbxUnidade_Interno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + cotacao2_idunidadeinterna);
        }

        private Boolean Cotacao2_Ipi_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ipi where codigo='" + Cotacao2_tbxClassFiscal.Text + "'"));

            if (idtmp == cotacao2_idipi)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zipi;
            }

            cotacao2_idipi = idtmp;

            return true;
        }

        private void Cotacao2_Ipi_Carrega()
        {
            Cotacao2_tbxClassFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from ipi where id=" + cotacao2_idipi);
        }

        private void Cotacao2_Sittribipi_Carrega()
        {
            Cotacao2_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribipi where id=" + cotacao2_idsittribipi), 2, Cotacao2_cbxSittribipi);
        }

        private void Cotacao2_Sittriba_Carrega()
        {
            Cotacao2_cbxSittriba.SelectedIndex = clsVisual.SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittributariaa where id=" + cotacao2_idsittriba), 1, Cotacao2_cbxSittriba);
        }

        private void Cotacao2_Sittribb_Carrega()
        {
            Cotacao2_cbxSittribb.SelectedIndex = clsVisual.SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittributariab where id=" + cotacao2_idsittribb), 2, Cotacao2_cbxSittribb);
        }

        private void Cotacao2_Sittribpis_Carrega()
        {
            Cotacao2_cbxSittribpis.SelectedIndex = clsVisual.SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribpis where id=" + cotacao2_idsittribpis), 2, Cotacao2_cbxSittribpis);
        }

        private void Cotacao2_Sittribcofins1_Carrega()
        {
            Cotacao2_cbxSittribcofins1.SelectedIndex = clsVisual.SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribcofins where id=" + cotacao2_idsittribcofins), 2, Cotacao2_cbxSittribcofins1);
        }

        private Boolean Cotacao2_Fornecedor_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from CLIENTE where cognome='" + Cotacao2_tbxCognome.Text + "'"));


            if (idtmp == cotacao2_idfornecedor)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zempresaclienteid; //zempresaclienteprospeccaoid;
            }

            cotacao2_idfornecedor = idtmp;

            return true;
        }

        private void Cotacao2_Fornecedor_Carrega()
        {
            Cotacao2_tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTE where id=" + cotacao2_idfornecedor);
            cotacao2_fornecedor = Cotacao2_tbxCognome.Text;
        }




        private void tspCotacao2Incluir_Click(object sender, EventArgs e)
        {
            cotacao2_posicao = 0;
            Cotacao2Carregar();

            totalEntregas = 0;

        }

        private void tspCotacao2Alterar_Click(object sender, EventArgs e)
        {
            if (dgvCotacao2.CurrentRow != null)
            {
                cotacao2_posicao = Int32.Parse(dgvCotacao2.CurrentRow.Cells["cotacao2_posicao"].Value.ToString());
                Cotacao2Carregar();
                VerificarMudancaProgEntrega();



            }
        }

        private void tspCotacao2Excluir_Click(object sender, EventArgs e)
        {
            if (dgvCotacao2.CurrentRow != null)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja realmente excluir o item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    dtCotacao2.Select("cotacao2_posicao =" + dgvCotacao2.CurrentRow.Cells["cotacao2_posicao"].Value.ToString())[0].Delete();

                }
            }
            verificaNumForne(dgvCotacao2.Rows.Count);
        }
        private void tspCotacao2Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar preços deste fornecedor ? ", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsCotacao2Info = new clsCotacao2Info();
                    Cotacao2FillInfo(clsCotacao2Info);
                    Cotacao2FillInfoToGrid(clsCotacao2Info);
                    this.verificaNumForne(dgvCotacao2.Rows.Count);
                    verificaQuantidadeSolicitada();
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                guiaatual = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspCotacao2Retornar_Click(object sender, EventArgs e)
        {
            guiaatual = 1;
        }

        private void Cotacao2Calcular()
        {
            Cotacao2_tbxQtdeorcada.Text = clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text).ToString("N3");
            Cotacao2_tbxFatorConv.Text = clsParser.DecimalParse(Cotacao2_tbxFatorConv.Text).ToString("N4");
            Cotacao2_tbxPeso.Text = clsParser.DecimalParse(Cotacao2_tbxPeso.Text).ToString("N4");
            Cotacao2_tbxPesoTotal.Text = (clsParser.DecimalParse(Cotacao2_tbxPeso.Text) * clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text)).ToString("N3");

            Cotacao2_tbxPrecoBruto.Text = clsParser.DecimalParse(Cotacao2_tbxPrecoBruto.Text).ToString("N6");
            Cotacao2_tbxPrecoDescPor.Text = clsParser.DecimalParse(Cotacao2_tbxPrecoDescPor.Text).ToString("N2");

            //
            Cotacao2_tbxValorDesconto.Text = clsParser.DecimalParse(Cotacao2_tbxValorDesconto.Text).ToString("N2");
            Cotacao2_tbxPrecoDescPor.Text = clsParser.DecimalParse(Cotacao2_tbxPrecoDescPor.Text).ToString("N2");
            Cotacao2_tbxPreco.Text = (clsParser.DecimalParse(Cotacao2_tbxPrecoBruto.Text) - clsParser.DecimalParse(Cotacao2_tbxValorDesconto.Text)).ToString("N6");
            Cotacao2_tbxTotalMercado.Text = (clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text) * clsParser.DecimalParse(Cotacao2_tbxPreco.Text)).ToString("N2");
            Cotacao2_tbxIPI.Text = clsParser.DecimalParse(Cotacao2_tbxIPI.Text).ToString("N2");
            if (clsParser.DecimalParse(Cotacao2_tbxIPI.Text) > 0)
            {
                Cotacao2_tbxCustoIPI.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalMercado.Text) * (clsParser.DecimalParse(Cotacao2_tbxIPI.Text) / 100)).ToString("N2");
            }
            else
            {
                Cotacao2_tbxCustoIPI.Text = 0.ToString("N2");
            }
            Cotacao2_tbxValorFrete.Text = clsParser.DecimalParse(Cotacao2_tbxValorFrete.Text).ToString("N2");
            Cotacao2_tbxValorSeguro.Text = clsParser.DecimalParse(Cotacao2_tbxValorSeguro.Text).ToString("N2");
            Cotacao2_tbxValorOutras.Text = clsParser.DecimalParse(Cotacao2_tbxValorOutras.Text).ToString("N2");
            Cotacao2_tbxTotalNota.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalMercado.Text) + clsParser.DecimalParse(Cotacao2_tbxCustoIPI.Text)).ToString("N2");

            Cotacao2_tbxBaseMP.Text = clsParser.DecimalParse(Cotacao2_tbxBaseMP.Text).ToString("N2");
            Cotacao2_tbxIcm.Text = clsParser.DecimalParse(Cotacao2_tbxIcm.Text).ToString("N2");

            if (clsParser.DecimalParse(Cotacao2_tbxBaseMP.Text) > 0)
            {
                if (Cotacao1_rbnConsumo.Checked == true)
                {
                    Cotacao2_tbxBaseIcm.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxBaseMP.Text) / 100)).ToString("N2");
                }
                else
                {
                    Cotacao2_tbxBaseIcm.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalMercado.Text) * (clsParser.DecimalParse(Cotacao2_tbxBaseMP.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_tbxBaseIcm.Text = 0.ToString("N2");
            }
            if (clsParser.DecimalParse(Cotacao2_tbxIcm.Text) > 0)
            {
                Cotacao2_tbxCustoIcm.Text = (clsParser.DecimalParse(Cotacao2_tbxBaseIcm.Text) * (clsParser.DecimalParse(Cotacao2_tbxIcm.Text) / 100)).ToString("N2");
            }
            else
            {
                Cotacao2_tbxCustoIcm.Text = 0.ToString("N2");
            }

            if (Cotacao2_ckxCreditaIcmsCusto.Checked == true)
            {
                Cotacao2_ckxCreditaIcmsCusto.Text = "Sim Creditar ICM's+Pis+Cofins";
                if (clsParser.DecimalParse(Cotacao2_tbxPreco.Text) > 0)
                {
                    Double indice = 0;
                    indice = Double.Parse(clsInfo.zfiscalpis.ToString()) + Double.Parse(clsInfo.zfiscalcofins.ToString()) + Double.Parse(Cotacao2_tbxIcm.Text);
                    if (indice > 0)
                    {
                        Cotacao2_tbxPrecoCustounit.Text = (Double.Parse(Cotacao2_tbxPreco.Text) * ((100 - indice) / 100)).ToString("N6");
                    }
                }
            }
            else
            {
                Cotacao2_ckxCreditaIcmsCusto.Text = "Não Creditar ICM's+Pis+Cofins";
                Cotacao2_tbxPrecoCustounit.Text = clsParser.DecimalParse(Cotacao2_tbxPreco.Text).ToString("N6");
            }
            // impostos
            Cotacao2_tbxIrrfPorc.Text = clsParser.DecimalParse(Cotacao2_tbxIrrfPorc.Text).ToString("N2");
            Cotacao2_tbxInssPorc.Text = clsParser.DecimalParse(Cotacao2_tbxInssPorc.Text).ToString("N2");
            Cotacao2_tbxPisCofinsCsllPorc.Text = clsParser.DecimalParse(Cotacao2_tbxPisCofinsCsllPorc.Text).ToString("N2");
            Cotacao2_tbxIssPorc.Text = clsParser.DecimalParse(Cotacao2_tbxIssPorc.Text).ToString("N2");
            Cotacao2_tbxPisPorc.Text = clsParser.DecimalParse(Cotacao2_tbxPisPorc.Text).ToString("N2");
            Cotacao2_tbxCofinsPorc.Text = clsParser.DecimalParse(Cotacao2_tbxCofinsPorc.Text).ToString("N2");
            Cotacao2_tbxCsllPorc.Text = clsParser.DecimalParse(Cotacao2_tbxCsllPorc.Text).ToString("N2");

            if (Cotacao2_ckxIrrf.Checked == true)
            {
                Cotacao2_ckxIrrf.Text = "Sim Tem I.R.R.F.";
                if (clsParser.DecimalParse(Cotacao2_tbxIrrfPorc.Text) > 0)
                {
                    Cotacao2_tbxIrrf.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxIrrfPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_ckxIrrf.Text = "Não Tem I.R.R.F.";
                Cotacao2_tbxIrrf.Text = 0.ToString("N2");
            }
            if (Cotacao2_ckxInss.Checked == true)
            {
                Cotacao2_ckxInss.Text = "Sim Tem I.N.S.S.";
                if (clsParser.DecimalParse(Cotacao2_tbxInssPorc.Text) > 0)
                {
                    Cotacao2_tbxInss.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxInssPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_ckxInss.Text = "Não Tem I.N.S.S.";
                Cotacao2_tbxInss.Text = 0.ToString("N2");
            }
            if (Cotacao2_ckxPisCofinsCsll.Checked == true)
            {
                Cotacao2_ckxPisCofinsCsll.Text = "Sim Pis/Cofins/Csll";
                if (clsParser.DecimalParse(Cotacao2_tbxPisCofinsCsllPorc.Text) > 0)
                {
                    Cotacao2_tbxPisCofinsCsll.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxPisCofinsCsllPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_ckxPisCofinsCsll.Text = "Não Pis/Cofins/Csll";
                Cotacao2_tbxPisCofinsCsll.Text = 0.ToString("N2");
            }
            if (Cotacao2_ckxIss.Checked == true)
            {
                Cotacao2_ckxIss.Text = "Sim Tem I.S.S.";
                if (clsParser.DecimalParse(Cotacao2_tbxIssPorc.Text) > 0)
                {
                    Cotacao2_tbxIss.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxIssPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_ckxIss.Text = "Não Tem I.S.S.";
                Cotacao2_tbxIss.Text = 0.ToString("N2");
            }
            if (Cotacao2_ckxPis.Checked == true)
            {
                Cotacao2_ckxPis.Text = "Sim Tem PIS";
                if (clsParser.DecimalParse(Cotacao2_tbxPisPorc.Text) > 0)
                {
                    Cotacao2_tbxPis.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxPisPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_ckxPis.Text = "Não Tem PIS";
                Cotacao2_tbxPis.Text = 0.ToString("N2");
            }
            if (Cotacao2_ckxCofins.Checked == true)
            {
                Cotacao2_ckxCofins.Text = "Sim Tem Cofins";
                if (clsParser.DecimalParse(Cotacao2_tbxCofinsPorc.Text) > 0)
                {
                    Cotacao2_tbxCofins.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxCofinsPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_ckxCofins.Text = "Não Tem Cofins";
                Cotacao2_tbxCofins.Text = 0.ToString("N2");
            }
            if (Cotacao2_ckxCsll.Checked == true)
            {
                Cotacao2_ckxCsll.Text = "Sim Tem Cofins";
                if (clsParser.DecimalParse(Cotacao2_tbxCsllPorc.Text) > 0)
                {
                    Cotacao2_tbxCsll.Text = (clsParser.DecimalParse(Cotacao2_tbxTotalNota.Text) * (clsParser.DecimalParse(Cotacao2_tbxCsllPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                Cotacao2_ckxCsll.Text = "Não Tem Cofins";
                Cotacao2_tbxCsll.Text = 0.ToString("N2");
            }


            // Rever calculos
            if (termino == "S")
            {  // Fechada e Aprovada
                tsbCotacao_Salvar.Enabled = false;
            }
            else if (termino == "A")
            {  // Em Aprovação
                Analisar_Cotacao();
            }
            else if (termino == "R")
            {  // Reprovado
                Analisar_Cotacao();
            }
            else if (termino == "O")
            {  // Aguardando Emissao do Pedido de Compras
                //Analisar_Cotacao();
            }
            else if (termino == "F")
            {  // Pedido Fechado com Pedido de Compras Emitido
                //Analisar_Cotacao();
            }

            else
            {  // Em Aberto
                Analisar_Cotacao();
            }


        }

        private void dgvCotacao2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspCotacao2Alterar.PerformClick();
        }



        private void tspCotacaoEntregaIncluir_Click(object sender, EventArgs e)
        {
            cotacaoentrega_posicao = 0;
            painel = 1;
            TclResolve();

        }

        private void tspCotacaoEntregaAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCotacaoEntrega.CurrentRow != null)
            {
                cotacaoentrega_posicao = Int32.Parse(dgvCotacaoEntrega.CurrentRow.Cells["cotacaoentrega_posicao"].Value.ToString());
                CotacaEntregaCarregar();
                painel = 1;
                TclResolve();
            }

        }

        private void tspCotacaoEntregaExcluir_Click(object sender, EventArgs e)
        {
            if (dgvCotacaoEntrega.CurrentRow != null)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja realmente excluir o item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    dtCotacaoEntrega.Select("cotacaoentrega_posicao =" + dgvCotacaoEntrega.CurrentRow.Cells["cotacaoentrega_posicao"].Value.ToString())[0].Delete();
                    CotacaoEntregaSomar();
                    VerificarMudancaProgEntrega();

                }

            }
        }

        private void tspCotacaoEntregaSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar programação entrega ? ", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    if (clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text) < (totalEntregas + clsParser.DecimalParse(CotacaoEntrega_tbxQtdeentrega.Text)))
                    {
                        throw new Exception("A soma do total programado ultrapassou a Qtde Solicitada.");
                    }
                    clsCotacaoentregaInfo = new clsCotacaoentregaInfo();
                    CotacaoEntregaFillInfo(clsCotacaoentregaInfo);
                    CotacaoEntregaFillInfoToGrid(clsCotacaoentregaInfo);
                    painel = 0;
                    TclResolve();
                    CotacaoEntregaSomar();
                    VerificarMudancaProgEntrega();
                }
                else if (drt == DialogResult.No)
                {
                    painel = 0;
                    TclResolve();
                }

                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspCotacaoEntregaRetornar_Click(object sender, EventArgs e)
        {
            painel = 0;
            TclResolve();
        }

        void CotacaEntregaCarregar()
        {
            clsCotacaoentregaInfo = new clsCotacaoentregaInfo();
            clsCotacaoentregaInfoOld = new clsCotacaoentregaInfo();

            if (cotacaoentrega_posicao == 0)
            {
                cotacaoentrega_posicao = dtCotacaoEntrega.Rows.Count + 1;

                clsCotacaoentregaInfo.id = 0;
                clsCotacaoentregaInfo.idcotacao = id;
                clsCotacaoentregaInfo.idcotacao1 = cotacao1_id;
                clsCotacaoentregaInfo.idcotacao2 = cotacao2_id;
                clsCotacaoentregaInfo.dataentrega = DateTime.Now;
                clsCotacaoentregaInfo.qtdeentrega = 0;
            }
            else
            {
                CotacaoEntregaGridToInfo(clsCotacaoentregaInfo, cotacaoentrega_posicao);
            }

            CotacaoEntregaCampos(clsCotacaoentregaInfo);
            CotacaoEntregaFillInfo(clsCotacaoentregaInfoOld);



            CotacaoEntrega_tbxDataEntrega.Select();
        }

        private void CotacaoEntregaGridToInfo(clsCotacaoentregaInfo info, Int32 posicao)
        {
            DataRow row = dtCotacaoEntrega.Select("cotacaoentrega_posicao = " + posicao)[0];

            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idcotacao = clsParser.Int32Parse(row["idcotacao"].ToString());
            info.idcotacao1 = clsParser.Int32Parse(row["idcotacao1"].ToString());
            info.idcotacao2 = clsParser.Int32Parse(row["idcotacao2"].ToString());
            info.dataentrega = DateTime.Parse(row["dataentrega"].ToString());
            info.qtdeentrega = clsParser.DecimalParse(row["qtdeentrega"].ToString());

        }
        private void CotacaoEntregaCampos(clsCotacaoentregaInfo info)
        {
            cotacaoentrega_id = info.id;

            cotacaoentrega_idcotacao = info.idcotacao;
            cotacaoentrega_idcotacao1 = info.idcotacao1;
            cotacaoentrega_idcotacao2 = info.idcotacao2;

            CotacaoEntrega_tbxDataEntrega.Text = info.dataentrega.ToString("dd/MM/yyyy");
            CotacaoEntrega_tbxQtdeentrega.Text = info.qtdeentrega.ToString("N5");
        }
        private void CotacaoEntregaFillInfo(clsCotacaoentregaInfo info)
        {
            info.id = cotacaoentrega_id;

            info.idcotacao = cotacaoentrega_idcotacao;
            info.idcotacao1 = cotacaoentrega_idcotacao1;
            info.idcotacao2 = cotacaoentrega_idcotacao2;
            info.dataentrega = clsParser.DateTimeParse(CotacaoEntrega_tbxDataEntrega.Text);
            info.qtdeentrega = clsParser.DecimalParse(CotacaoEntrega_tbxQtdeentrega.Text);
        }

        void CotacaoEntregaFillInfoToGrid(clsCotacaoentregaInfo info)
        {
            DataRow row;
            DataRow[] rows = dtCotacaoEntrega.Select("cotacaoentrega_posicao = " + cotacaoentrega_posicao);
            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtCotacaoEntrega.NewRow();
            }
            row["id"] = info.id;

            row["idcotacao"] = info.idcotacao;
            row["idcotacao1"] = info.idcotacao1;
            row["idcotacao2"] = info.idcotacao2;
            row["dataentrega"] = info.dataentrega;
            row["qtdeentrega"] = info.qtdeentrega;

            if (rows.Length == 0)
            {
                row["cotacaoentrega_posicao"] = cotacaoentrega_posicao;
                row["cotacaoentrega_posicao_ref_cotacao2"] = cotacao2_posicao;
                row["cotacaoentrega_posicao_ref_cotacao1"] = Cotacao1_posicao;
                dtCotacaoEntrega.Rows.Add(row);
            }
        }

        private void btnIdFormapagto_Cotacao2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFormapagto_Cotacao2.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", cotacao2_idformapagto, "Tipo Titulo");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
            //            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", cotacao2_idformapagto);
            //            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdCondpagto_Cotacao2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCondpagto_Cotacao2.Name;
            frmCondPagtoPes frmCondPagtoPes = new frmCondPagtoPes(); //(clsInfo.conexaosqldados, cotacao2_idcondpagto);
            clsFormHelper.AbrirForm(this.MdiParent, frmCondPagtoPes, clsInfo.conexaosqldados);
        }

        private void btnIdUnidade_Cotacao2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdUnidade_Cotacao2.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes(); //(clsInfo.conexaosqldados, "UNIDADE", cotacao2_idunidade);
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdIPI_Cotacao2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdIPI_Cotacao2.Name;
            frmIpiPes frmIpiPes = new frmIpiPes(); //(clsInfo.conexaosqldados, cotacao2_idipi);
            clsFormHelper.AbrirForm(this.MdiParent, frmIpiPes, clsInfo.conexaosqldados);
        }

        private void Cotacao2_ckxCreditaIcmsCusto_Click(object sender, EventArgs e)
        {
            if (Cotacao2_ckxCreditaIcmsCusto.Checked == false)
            {
                Cotacao2_ckxCreditaIcmsCusto.Text = "Não Creditar ICM's+Pis+Cofins";
            }
            else
            {
                Cotacao2_ckxCreditaIcmsCusto.Text = "Sim Creditar ICM's+Pis+Cofins";
            }
        }

        private void Cotacao2_MaoObraTributada(object sender, EventArgs e)
        {
            Cotacao2Calcular();
        }

        private void Cotacao1_btnIdCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Cotacao1_btnIdCodigo.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(cotacao1_idcodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void Cotacao1_btnIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Cotacao1_btnIdCentroCusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes(); // frmEscFormVisPes(clsInfo.conexaosqlbanco, "CENTROCUSTOS", idcentrocusto, "ATIVO", "S");
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", cotacao1_idcentrocusto, "Centro de Custos");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void Cotacao1_btnIdHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Cotacao1_btnIdHistorico.Name;
            frmHistoricosPes frmHistoricosPes = new frmHistoricosPes(); // frmEscFormVisPes(clsInfo.conexaosqlbanco, "HISTORICOS", idhistorico, "ATIVO", "S");
            frmHistoricosPes.Init(clsInfo.conexaosqlbanco, cotacao1_idhistorico);
            clsFormHelper.AbrirForm(this.MdiParent, frmHistoricosPes, clsInfo.conexaosqlbanco);
        }

        private void Cotacao1_btnIdOs_Click(object sender, EventArgs e)
        {
            //clsInfo.znomegrid = Cotacao1_btnIdOs.Name;
            //frmOrdemServicoPes frmOrdemServicoPes = new frmOrdemServicoPes();// (idos);
            //frmOrdemServicoPes.Init(this.cotacao1_idordemservico);
            //clsFormHelper.AbrirForm(this.MdiParent, frmOrdemServicoPes, clsInfo.conexaosqldados);
        }

        private void Cotacao1_btnIdDestino_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Cotacao1_btnIdDestino.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECAS", cotacao1_iddestino, "Peças");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void Cotacao1_btnIdUnidade_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Cotacao1_btnIdUnidade.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "UNIDADE", cotacao1_idunidade, "Unidade");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void tclCotacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            tclCotacao.SelectedIndex = guiaatual;
        }

        //        private void frmCotacao_Activated(object sender, EventArgs e)
        //        {
        //            Lupa();
        //        }

        private void Cotacao2_Sittrib(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;

            if (ctl.Name == Cotacao2_cbxSittribipi.Name)
            {
                cotacao2_idsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribipi where codigo='" + Cotacao2_cbxSittribipi.Text + "'"));
            }
            else if (ctl.Name == Cotacao2_cbxSittriba.Name)
            {
                cotacao2_idsittriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariaa where codigo='" + Cotacao2_cbxSittriba.Text + "'"));
            }
            else if (ctl.Name == Cotacao2_cbxSittribb.Name)
            {
                cotacao2_idsittribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariab where codigo='" + Cotacao2_cbxSittribb.Text + "'"));
            }
            else if (ctl.Name == Cotacao2_cbxSittribpis.Name)
            {
                cotacao2_idsittribpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribpis where codigo='" + Cotacao2_cbxSittribpis.Text + "'"));
            }
            else if (ctl.Name == Cotacao2_cbxSittribcofins1.Name)
            {
                cotacao2_idsittribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribcofins where codigo='" + Cotacao2_cbxSittribcofins1.Text + "'"));
            }
        }

        private void tspSolicitcaoentregaRetornar_Click(object sender, EventArgs e)
        {
            painel = 0;
            TclResolve();
        }

        private void tspSolicitcaoentregaSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar as entregas ? ", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsCotacaoentregaInfo = new clsCotacaoentregaInfo();
                    CotacaoEntregaFillInfo(clsCotacaoentregaInfo);
                    CotacaoEntregaFillInfoToGrid(clsCotacaoentregaInfo);
                    painel = 0;
                    TclResolve();
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CotacaoEntregaSomar();
            VerificarMudancaProgEntrega();
        }

        private void CotacaoEntrega_btnCalculaProgramacao_Click(object sender, EventArgs e)
        {
            try
            {
                clsProgramacaoEntrega clsProgramacaoEntrega = new clsProgramacaoEntrega();

                clsProgramacaoEntrega.Periodo periodo;

                if (CotacaoEntrega_lbxPeriodoEntrega.SelectedItem.ToString().Substring(0, 1) == "A")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Anual;
                }
                else if (CotacaoEntrega_lbxPeriodoEntrega.SelectedItem.ToString() == "Semestral")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semestre;
                }
                else if (CotacaoEntrega_lbxPeriodoEntrega.SelectedItem.ToString().Substring(0, 1) == "M")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Mes;
                }
                else if (CotacaoEntrega_lbxPeriodoEntrega.SelectedItem.ToString().Substring(0, 1) == "Q")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Quinzena;
                }
                else if (CotacaoEntrega_lbxPeriodoEntrega.SelectedItem.ToString() == "Semanal")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semana;
                }
                else
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semana;
                }

                DataTable dtTemp = clsProgramacaoEntrega.GerarProgramacao(clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text),
                                                        0,
                                                        clsParser.Int32Parse(CotacaoEntrega_tbxQtdeEntregas.Text),
                                                        periodo,
                                                        clsParser.DateTimeParse(CotacaoEntrega_PrimeiraEntrega.Text),
                                                        cotacao2_idunidade,
                                                        CotacaoEntrega_chkDiasUteis.Checked);

                foreach (DataRow row in dtCotacaoEntrega.Rows)
                {
                    if (row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Deleted)
                    {
                        if (row["cotacaoentrega_posicao_ref_cotacao2"].ToString() == cotacao2_posicao.ToString())
                        {
                            row.Delete();
                        }
                    }
                }

                foreach (DataRow row in dtTemp.Rows)
                {
                    DataRow rowEntrega = dtCotacaoEntrega.NewRow();

                    rowEntrega["cotacaoentrega_posicao"] = dtCotacaoEntrega.Rows.Count + 1;
                    rowEntrega["cotacaoentrega_posicao_ref_cotacao2"] = cotacao2_posicao;
                    rowEntrega["cotacaoentrega_posicao_ref_cotacao1"] = Cotacao1_posicao;
                    rowEntrega["qtdeentrega"] = row["qtde"];
                    rowEntrega["dataentrega"] = row["entrega"];

                    dtCotacaoEntrega.Rows.Add(rowEntrega);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName);
            }
        }

        private void tspSolicitcaoentregaIncluir_Click(object sender, EventArgs e)
        {
            cotacaoentrega_posicao = 0;
            CotacaoEntregaCarregar();
        }

        private void CotacaoEntregaCarregar()
        {
            clsCotacaoentregaInfo = new clsCotacaoentregaInfo();
            clsCotacaoentregaInfoOld = new clsCotacaoentregaInfo();

            if (cotacaoentrega_posicao == 0)
            {
                cotacaoentrega_posicao = dtCotacaoEntrega.Rows.Count + 1;

                clsCotacaoentregaInfo.dataentrega = DateTime.Now.AddDays(1);
            }
            else
            {
                CotacaoEntregaGridToInfo(clsCotacaoentregaInfo, cotacaoentrega_posicao);
            }

            CotacaoEntregaCampos(clsCotacaoentregaInfo);
            CotacaoEntregaFillInfo(clsCotacaoentregaInfoOld);

            painel = 0;
            TclResolve();

            CotacaoEntrega_tbxDataEntrega.Select();
        }

        private void tclCotacaoEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            tclCotacaoEntrega.SelectedIndex = painel;
        }

        private void tspSolicitcaoentregaAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCotacaoEntrega.CurrentRow != null)
            {
                cotacaoentrega_posicao = Int32.Parse(dgvCotacaoEntrega.CurrentRow.Cells["cotacaoentrega_posicao"].Value.ToString());
                CotacaoEntregaCarregar();
            }
        }

        private void tspSolicitcaoentregaExcluir_Click(object sender, EventArgs e)
        {
            if (dgvCotacaoEntrega.CurrentRow != null)
            {
                DialogResult drt;

                drt = MessageBox.Show("Deseja realmente Excluir o registro selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    dtCotacaoEntrega.Select("cotacaoentrega_posicao=" + dgvCotacaoEntrega.CurrentRow.Cells["cotacaoentrega_posicao"].Value.ToString())[0].Delete();
                }
            }
        }

        private void verificaNumForne(Int32 fornecedores)
        {
            if (fornecedores > 4)
            {
                tspCotacao2Incluir.Enabled = false;
            }
            else
            {
                tspCotacao2Incluir.Enabled = true;
            }
        }

        private void CotacaoEntregaSomar()
        {
            //
            totalEntregas = 0;
            Int32 ref_cotacaoentrega_posicao_ref_cotacao1 = 0;
            //foreach (DataGridViewRow row in dgvCotacaoEntrega.Rows)
            foreach (DataRow row in dtCotacaoEntrega.Rows)
            {
                if (ref_cotacaoentrega_posicao_ref_cotacao1 != clsParser.Int32Parse(row["cotacaoentrega_posicao_ref_cotacao1"].ToString()))
                {
                    ref_cotacaoentrega_posicao_ref_cotacao1 = clsParser.Int32Parse(row["cotacaoentrega_posicao_ref_cotacao1"].ToString());
                    totalEntregas += clsParser.DecimalParse(row["qtdeentrega"].ToString());
                }
            }
        }

        private void VerificarMudancaProgEntrega()
        {

            if (totalEntregas > 0)
            {
                gbxProgEntrega.Visible = false;

            }
            else
            {
                gbxProgEntrega.Visible = true;
            }

            if (clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text) != 0)
            {
                if (clsParser.DecimalParse(totalEntregas.ToString()) < clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text))
                {
                    tspSolicitcaoentregaIncluir.Enabled = true;
                }
                else
                {
                    tspSolicitcaoentregaIncluir.Enabled = false;
                }
            }
            else
            {
                tspSolicitcaoentregaIncluir.Enabled = true;
            }
        }

        private void verificaQuantidadeSolicitada()
        {
            if (totalEntregas > clsParser.DecimalParse(Cotacao2_tbxQtdeorcada.Text))
                throw new Exception(" O total de entregas deve ser igual da quantidade orçada");
        }

        private void tclCotacaoEntrega_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            TclResolve();
        }

        private void TclResolve()
        {
            tclCotacaoEntrega.SelectedIndex = painel;
        }

        private void Cotacao2_btnFornecedor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Cotacao2_btnFornecedor.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("", cotacao2_idfornecedor);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void VerificaSeMudouFornecedor(Int32 idcodigo_old)
        {
            // se mudou o codigo - carregar o fornecedor
            if (idcodigo_old != cotacao1_idcodigo)
            {
                // 1. verificar se já tem fornecedor cadastrado
                DataRow[] rowsSolicitacaForne = dtCotacao2.Select();
                for (Int32 i = 0; i < rowsSolicitacaForne.Length; i++)
                {
                    rowsSolicitacaForne[i].Delete();
                }

                // 2. carregar os novos fornecedores
                DataTable dtPecasForne = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
                                           "select CLIENTE.id as CLIENTE_ID, CLIENTE.COGNOME AS [fornecedor] " +
                                           "FROM PECASFORNE " +
                                           "LEFT JOIN CLIENTE ON CLIENTE.ID = PECASFORNE.IDFORNECEDOR " +
                                           "WHERE " +
                                           "PECASFORNE.IDCODIGO = " + cotacao1_idcodigo +
                                           "ORDER BY CLIENTE.COGNOME");

                if (dtPecasForne.Rows.Count > 0)
                {

                    Int32 posicaoItem2 = 0;
                   
                    foreach (DataRow rowCotForne in dtPecasForne.Rows)
                    {
                        
                        posicaoItem2 += 1;
                        DataRow rowCot2 = dtCotacao2.NewRow();
                        rowCot2["cotacao2_posicao"] = posicaoItem2;
                        rowCot2["cotacao2_posicao1_ref"] = Cotacao1_posicao;

                        rowCot2["id"] = 0;
                        rowCot2["idcotacao"] = id;
                        rowCot2["idcotacao1"] = 0;
                        rowCot2["idfornecedor"] = rowCotForne["CLIENTE_ID"];
                        rowCot2["IDFORMAPAGTO"] = clsInfo.zformapagto;
                        rowCot2["IDCONDPAGTO"] = clsInfo.zcondpagto;
                        rowCot2["IDUNIDADE"] = clsInfo.zunidade;
                        rowCot2["IDUNIDADEINTERNA"] = clsInfo.zunidade;
                        rowCot2["IDPEDIDOCOMPRA"] = clsInfo.zcompras;
                        rowCot2["IDPEDIDOCOMPRAITEM"] = clsInfo.zcompras1;
                        rowCot2["IDSITTRIBA"] = clsInfo.zsituacaotriba;
                        rowCot2["IDSITTRIBB"] = clsInfo.zsituacaotribb;
                        rowCot2["IDSITTRIBIPI"] = clsInfo.zsittribipi;
                        rowCot2["IDSITTRIBPIS"] = clsInfo.zsittribpis;
                        rowCot2["IDSITTRIBCOFINS"] = clsInfo.zsittribcofins;
                        rowCot2["IDIPI"] = clsInfo.zipi;
                        rowCot2["CONTATO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                   " select contato from CLIENTE where id = " + rowCot2["idfornecedor"].ToString(), "").ToString();
                        rowCot2["TELEFONE"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                   " select telefone from CLIENTE where id = " + rowCot2["idfornecedor"].ToString(), "").ToString(); ;
                        rowCot2["QTDEINTERNA"] = 0;
                        rowCot2["QTDEORCADA"] = 0;
                        rowCot2["FATORCONV"] = 0;
                        rowCot2["PRECOBRUTO"] = 0;
                        rowCot2["PRECODESCPOR"] = 0;
                        rowCot2["VALORDESCONTO"] = 0;
                        rowCot2["PRECO"] = 0;
                        rowCot2["TOTALMERCADO"] = 0;
                        rowCot2["VALORFRETE"] = 0;
                        rowCot2["VALORFRETEICMS"] = "";
                        rowCot2["VALORSEGURO"] = 0;
                        rowCot2["VALORSEGUROICMS"] = "";
                        rowCot2["VALOROUTRAS"] = 0;
                        rowCot2["VALOROUTRASICMS"] = "";
                        rowCot2["TOTALNOTA"] = 0;
                        rowCot2["PESO"] = 0;
                        rowCot2["TOTALPESO"] = 0;
                        rowCot2["BASEMP"] = 0;
                        rowCot2["REDUCAO"] = 0;
                        rowCot2["BASEICM"] = 0;
                        rowCot2["ICM"] = 0;
                        rowCot2["CUSTOICM"] = 0;
                        rowCot2["BASEICMSUBST"] = 0;
                        rowCot2["ICMSUBST"] = 0;
                        rowCot2["ICMSSUBSTREDUCAO"] = 0;
                        rowCot2["ICMINTERNO"] = 0;
                        rowCot2["BCPISPASEP"] = 0;
                        rowCot2["ALIQPISPASEP"] = 0;
                        rowCot2["PISPASEP"] = 0;
                        rowCot2["BCCOFINS"] = 0;
                        rowCot2["ALIQCOFINS1"] = 0;
                        rowCot2["COFINS1"] = 0;
                        rowCot2["BCIPI"] = 0;
                        rowCot2["IPI"] = 0;
                        rowCot2["CUSTOIPI"] = 0;
                        rowCot2["IRRFPORC"] = 0;
                        rowCot2["IRRF"] = 0;
                        rowCot2["INSSPORC"] = 0;
                        rowCot2["INSS"] = 0;
                        rowCot2["PISCOFINCSLLPORC"] = 0;
                        rowCot2["PISCOFINSCSLL"] = 0;
                        rowCot2["ISSPORC"] = 0;
                        rowCot2["ISS"] = 0;
                        rowCot2["PISPORC"] = 0;
                        rowCot2["PIS"] = 0;
                        rowCot2["COFINSPORC"] = 0;
                        rowCot2["COFINS"] = 0;
                        rowCot2["CSLLPORC"] = 0;
                        rowCot2["CSLL"] = 0;
                        rowCot2["FORNECEDOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                                     "select cognome from CLIENTE where id = " + rowCot2["IDFORNECEDOR"].ToString(), "").ToString();

                        rowCot2["FORMAPAGTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                   " select codigo from SITUACAOTIPOTITULO where id = " + clsInfo.zformapagto, "").ToString();

                        rowCot2["CONDPAGTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                   " select codigo from CONDPAGTO where id = " + clsInfo.zcondpagto, "").ToString();
                        dtCotacao2.Rows.Add(rowCot2);
                      
                    }
                    dtCotacao2.AcceptChanges();
                    dtCotacaoEntrega.AcceptChanges();
                    
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Analisar_Cotacao()
        {
            // Verificar a se tudo foi cotado

            // Campos para apurar o total e qual dos itens esta ganhando a cotação
            Decimal total_previsto_item = 0;    

            String tudo_foi_cotado = "";

            tbxTotalPrevisto.Text = "0";   // Total Geral da cotação
            // Pegar o Item da Cotação
            foreach (DataRow row in dtCotacao1.Rows)
            {
                if (row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Detached)
                {
                    continue;   // não somar se apagou
                }
                else
                {
                    total_previsto_item = 0;
                    // Abrir a Cotação 2 (fornecedores com os itens )
                    // Olhar um a um para ver ser esta preenchido
                    // Analisar o Item Ganhador (1. preço)
                    foreach (DataRow row2 in dtCotacao2.Rows)
                    {
                        if (row2.RowState == DataRowState.Deleted ||
                            row2.RowState == DataRowState.Detached)
                        {
                            continue;   // não somar se apagou
                        }
                        else
                        {
                            if (clsParser.DecimalParse(row2["totalnota"].ToString()) == 0)
                            {
                                // Falta fechar algum item ainda
                                tudo_foi_cotado = "N";
                            }
                            if (row2["cotacao2_posicao1_ref"].ToString() == row["cotacao1_posicao"].ToString())
                            {
                               if (clsParser.DecimalParse(row2["totalnota"].ToString()) > 0)
                               {
                                    if (total_previsto_item == 0)
                                    {
                                        total_previsto_item = clsParser.DecimalParse(row2["totalnota"].ToString());
                                    }
                                    else if (total_previsto_item > clsParser.DecimalParse(row2["totalnota"].ToString()))
                                    {
                                        total_previsto_item = clsParser.DecimalParse(row2["totalnota"].ToString());
                                    }
                               }
                            }
                        }
                    }
                    row["totalprevisto"] = total_previsto_item;
                    //Somar o valor escolhido
                    tbxTotalPrevisto.Text = (clsParser.DecimalParse(tbxTotalPrevisto.Text) + total_previsto_item).ToString("N2");
                }
            }
            if (tudo_foi_cotado == "")
            {
                // Todos itens foram cotados
                // Liberar botão para transferencia
                Label_Status.Text = "Todos os itens foram Cotados";
                Label_Status.BackColor = System.Drawing.Color.LightGreen;
                btnFechar_Cotacao_Enviar.Enabled = true;
                btnFechar_Cotacao_Enviar.BackColor = System.Drawing.Color.Green;
                btnFechar_Cotacao_Enviar.Text = "Fechar a Cotação e Enviar para Aprovação";
            }
            else
            {
                Label_Status.Text = "Falta Cotar itens - Verifique ";
                btnFechar_Cotacao_Enviar.Enabled = false;
                btnFechar_Cotacao_Enviar.BackColor = System.Drawing.Color.RoyalBlue;
                btnFechar_Cotacao_Enviar.Text = "Ainda não pode Fechar a Cotação e Enviar para Aprovação";
            }
        }
        private void btnFechar_Cotacao_Enviar_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja mesmo enviar esta cotação para aprovação ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                // fazer o procedimento para ir para aproxima tela
                // Status passa para A (em aprovação)
                termino = "A";
                // Data de Fechamento para a Data de Hoje
                tbxDataFechamento.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                // A aprovação fica na frmCotacaoAprovaVis.cs
                tsbCotacao_Salvar.PerformClick();
            }
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            termino = "A";
            tsbCotacao_Salvar.PerformClick();
        }

        private void btnPedidoCompra_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Salvar() == DialogResult.Cancel)
                if (EmitirPedidoCompra() == DialogResult.Cancel)
                {
                    return;
                }
                MessageBox.Show("Pedidos de Compra - Emitidos");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            //DialogResult drt;
            //drt = MessageBox.Show("Emitir Pedido de Compras ?  Cotação será finalizada !! ", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            //if (drt == DialogResult.Yes)
            //{
            //    EmitirPedidoCompra();
            //}

            //MessageBox.Show("Pedidos de Compra - Emitido !!");
            //this.Close();

        }
        private DialogResult EmitirPedidoCompra()
        {
            DialogResult drt;
            drt = MessageBox.Show("Emitir Pedido de Compras ?  Cotação será finalizada !! ", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                // Verificar se os fornecedores estão Cadastrados como Cliente
                Boolean bok = true;
                Int32 idCliente = 0;
                foreach (DataRow row in dtCotacao1.Rows)
                {
                    idCliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where ID=" + clsParser.Int32Parse(row["IDFORNECEDORGANHOU"].ToString()) + " ", "0"));
                    if (idCliente == 0)
                    {
                        String Cognome = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + clsParser.Int32Parse(row["IDFORNECEDORGANHOU"].ToString()) + " ", "0");
                        bok = false;  // se não estiver 
                        MessageBox.Show("Cadastre o CNPJ neste seu fornecedor Prospectado  " + Cognome + " e ele " + Environment.NewLine +
                                        "automaticamente passara a ser de fornecedor cadastrado - Obrigado !!");

                        frmCliente frmCliente = new frmCliente();
                        frmCliente.Init(clsParser.Int32Parse(row["IDFORNECEDORGANHOU"].ToString()), dgvCotacao1.Rows);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCliente, clsInfo.conexaosqldados);
                        frmCliente.ShowDialog();
                    }
                }
                if (bok == true)
                {
                    dtCotacao1.DefaultView.Sort = "idfornecedorganhou";
                    idCliente = 0;
                    idPedidoCompra = 0;
                    foreach (DataRow row in dtCotacao1.Rows)
                    {
                        Int32 idClienteNew = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where ID=" + clsParser.Int32Parse(row["IDFORNECEDORGANHOU"].ToString()) + " ", "0"));
                        Int32 idCotacao1 = clsParser.Int32Parse(row["id"].ToString());
                        if (idCliente != idClienteNew)
                        {
                            idPedidoCompra = 0;
                            idCliente = idClienteNew;
                        }

                        if (idPedidoCompra == 0)
                        {
                            // Incluir Novo Pedido
                            EmitirPedidoCompra_New(0, idCotacao1);

                        }
                        else
                        {
                            // Alterar Pedido Compra - incluir itens apenas
                            EmitirPedidoCompra_New(idPedidoCompra, idCotacao1);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Reinicie o Processo - Não foi emitido nenhum Pedido de Compras" + Environment.NewLine +
                                   "Vamos rever se o Cadastro de Fornecedores agora confere ! - Obrigado !! ");
                }

            }

            return drt;

        }
        private void EmitirPedidoCompra_New(Int32 _idPedidoCompra, Int32 _idCotacao1)
        {
            //using (TransactionScope tse = new TransactionScope())
            //{


                // Puxar os dados da cotacao1 o item
                clsCotacao1InfoRef = clsCotacao1BLL.Carregar(_idCotacao1, clsInfo.conexaosqldados);
                // Puxar os dados da cotacao2 que ganhou
                clsCotacao2InfoRef = clsCotacao2BLL.Carregar(clsCotacao1InfoRef.idcotacao2ganhou, clsInfo.conexaosqldados);

                clsComprasInfo = new clsComprasInfo();
                clsComprasInfo = clsComprasBLL.Carregar(_idPedidoCompra, clsInfo.conexaosqldados);
                if (_idPedidoCompra == 0)
                {
                    clsComprasInfo.ano = DateTime.Now.Year;
                    clsComprasInfo.cofins = 0;
                    clsComprasInfo.cofins1 = 0;
                    clsComprasInfo.contato = clsCotacao2InfoRef.contato;
                    clsComprasInfo.csll = 0;
                    clsComprasInfo.data = DateTime.Now;
                    clsComprasInfo.filial = clsInfo.zfilial;
                    clsComprasInfo.frete = "C";
                    clsComprasInfo.fretepaga = "0";
                    clsComprasInfo.id = 0;
                    clsComprasInfo.idautorizante = clsCotacaoInfo.idautorizante;
                    clsComprasInfo.idcomprador = clsInfo.zusuarioid;
                    clsComprasInfo.idcondpagto = clsCotacao2InfoRef.idcondpagto;
                if (clsComprasInfo.idcondpagto == 0)
                {
                    clsComprasInfo.idcondpagto = clsInfo.zcondpagto;
                }
                    clsComprasInfo.idcontato = 0;
                    clsComprasInfo.idemitente = clsInfo.zusuarioid;
                    clsComprasInfo.idformapagto = clsCotacao2InfoRef.idformapagto;
                if (clsComprasInfo.idformapagto == 0)
                {
                    clsComprasInfo.idformapagto = clsInfo.zformapagto;
                }
                    clsComprasInfo.idfornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where ID=" + clsCotacao2InfoRef.idfornecedor + " ", "0"));
                    if (clsComprasInfo.idfornecedor ==0 )
                    {
                    clsComprasInfo.idfornecedor = clsInfo.zempresaclienteid;
                    }
                    clsComprasInfo.idtransportadora = clsComprasInfo.idfornecedor;
                    clsComprasInfo.inss = 0;
                    clsComprasInfo.irrf = 0;
                    clsComprasInfo.iss = 0;
                    clsComprasInfo.numero = 0;
                    clsComprasInfo.observa = "";
                    clsComprasInfo.pis = 0;
                    clsComprasInfo.piscofinscsll = 0;
                    clsComprasInfo.pispasep = 0;
                    clsComprasInfo.qtdebaixada = 0;
                    clsComprasInfo.qtdedefeito = 0;
                    clsComprasInfo.qtdeentregue = 0;
                    clsComprasInfo.qtdeosaux = 0;
                    clsComprasInfo.qtdesaldo = 0;
                    clsComprasInfo.qtdesucata = 0;
                    clsComprasInfo.setor = "N";
                    clsComprasInfo.setorfator = 1;
                    clsComprasInfo.situacao = "0";
                    clsComprasInfo.termino = "N";
                    clsComprasInfo.tipofrete = "N";
                    clsComprasInfo.totalbaseicm = 0;
                    clsComprasInfo.totalbaseicmsubst = 0;
                    clsComprasInfo.totalfrete = 0;
                    clsComprasInfo.totalicm = 0;
                    clsComprasInfo.totalicmsubst = 0;
                    clsComprasInfo.totalfrete = 0;
                    clsComprasInfo.totalicm = 0;
                    clsComprasInfo.totalicmsubst = 0;
                    clsComprasInfo.totalipi = 0;
                    clsComprasInfo.totalmercadoria = 0;
                    clsComprasInfo.totaloutras = 0;
                    clsComprasInfo.totalpeca = 0;
                    clsComprasInfo.totalpecaentra = 0;
                    clsComprasInfo.totalpecatransfe = 0;
                    clsComprasInfo.totalpedido = 0;
                    clsComprasInfo.totalpeso = 0;
                    clsComprasInfo.totalpecaentra = 0;
                    clsComprasInfo.totalpecatransfe = 0;
                    clsComprasInfo.totalpedido = 0;
                    clsComprasInfo.totalpeso = 0;
                    clsComprasInfo.totalprevisto = 0;
                    clsComprasInfo.totalseguro = 0;
                    clsComprasInfo.transporte = "N";

                    clsComprasInfo.id = clsComprasBLL.Incluir(clsComprasInfo, clsInfo.conexaosqldados);
                    idPedidoCompra = clsComprasInfo.id;
                }
                else
                {
                    idPedidoCompra = _idPedidoCompra;
                }
                // Cadastrar os itens do
                clsCompras1Info = new clsCompras1Info();
                clsCompras1Info.aliqcofins1 = clsCotacao2InfoRef.aliqcofins1;
                clsCompras1Info.aliqpispasep = clsCotacao2InfoRef.aliqpispasep;
                clsCompras1Info.baseicm = clsCotacao2InfoRef.baseicm;
                clsCompras1Info.baseicmsubst = clsCotacao2InfoRef.baseicmsubst;
                clsCompras1Info.basemp = clsCotacao2InfoRef.basemp;
                //clsCompras1Info.bccofins1 = 
                clsCompras1Info.bcipi = clsCotacao2InfoRef.bcipi;
                clsCompras1Info.bcpispasep = clsCotacao2InfoRef.bcpispasep;
                clsCompras1Info.calculoautomatico = "S";
                clsCompras1Info.codigoemp01 = "";
                clsCompras1Info.codigoemp02 = "";
                clsCompras1Info.codigoemp03 = "";
                clsCompras1Info.codigoemp04 = "";
                clsCompras1Info.cofins = clsCotacao2InfoRef.cofins;
                clsCompras1Info.cofins1 = clsCotacao2InfoRef.cofins1;
                clsCompras1Info.cofinsporc = clsCotacao2InfoRef.cofinsporc;
                clsCompras1Info.complemento = clsCotacao1InfoRef.complemento;
                clsCompras1Info.complemento1 = clsCotacao1InfoRef.complemento1;
                clsCompras1Info.consumo = "S";
                clsCompras1Info.creditaricm = "N";
                clsCompras1Info.csll = clsCotacao2InfoRef.csll;
                clsCompras1Info.csllporc = clsCotacao2InfoRef.csllporc;
                clsCompras1Info.custoicm = clsCotacao2InfoRef.custoicm;
                clsCompras1Info.custoipi = clsCotacao2InfoRef.custoipi;
                clsCompras1Info.desconto = 0;
                clsCompras1Info.descricaoesp = "";
                clsCompras1Info.fatorconv = clsCotacao2InfoRef.fatorconv;
                clsCompras1Info.icm = clsCotacao2InfoRef.icm;
                clsCompras1Info.icminterno = clsCotacao2InfoRef.icminterno;
                clsCompras1Info.icmsubst = clsCotacao2InfoRef.icmsubst;
                clsCompras1Info.id = 0;
                clsCompras1Info.idcentrocusto = clsCotacao1InfoRef.idcentrocusto;
                clsCompras1Info.idcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CFOP where CFOP='" + "5102" + "' "));
                clsCompras1Info.idcfopfis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CFOP where CFOP='" + "5102" + "' "));
                clsCompras1Info.idcodigo = clsCotacao1InfoRef.idcodigo;
                clsCompras1Info.idcodigo1 = clsCotacao1InfoRef.iddestino;
                clsCompras1Info.idcodigoctabil = clsCotacao1InfoRef.idcodigoctabil;
                clsCompras1Info.idcompras = idPedidoCompra;
                clsCompras1Info.idcotacao = clsCotacaoInfo.id;
                clsCompras1Info.idcotacao2 = clsCotacao2InfoRef.id;
                clsCompras1Info.idcotacaoitem = clsCotacao1InfoRef.id;
                clsCompras1Info.iddestino = clsCotacao1InfoRef.iddestino;
                clsCompras1Info.idhistorico = clsCotacao1InfoRef.idhistorico;
                clsCompras1Info.idipi = clsCotacao2InfoRef.idipi;
                clsCompras1Info.idos= clsCotacao1InfoRef.idordemservico;
                clsCompras1Info.idositem = 0;
                clsCompras1Info.idsittriba = clsCotacao2InfoRef.idsittriba;
                clsCompras1Info.idsittribb = clsCotacao2InfoRef.idsittribb;
                clsCompras1Info.idsittribcofins1 = clsCotacao2InfoRef.idsittribcofins;
                clsCompras1Info.idsittribipi = clsCotacao2InfoRef.idsittribipi;
                clsCompras1Info.idsittribpis = clsCotacao2InfoRef.idsittribpis;
                clsCompras1Info.idsolicitacao = clsCotacao1InfoRef.idsolicitacao;
                clsCompras1Info.idtiponota = clsInfo.ztiponota;
                clsCompras1Info.idunid = clsCotacao2InfoRef.idunidadeinterna;
                if (clsCompras1Info.idunid == 0)
            {
                clsCompras1Info.idunid = clsInfo.zunidade;
            }
                clsCompras1Info.idunidfiscal = clsCotacao2InfoRef.idunidade;
            if (clsCompras1Info.idunidfiscal == 0)
            {
                clsCompras1Info.idunidfiscal = clsInfo.zunidade;
            }
            clsCompras1Info.inss = clsCotacao2InfoRef.inss;
                clsCompras1Info.inssporc = clsCotacao2InfoRef.inssporc;
                clsCompras1Info.ipi = clsCotacao2InfoRef.ipi;
                clsCompras1Info.irrf = clsCotacao2InfoRef.irrf;
                clsCompras1Info.irrfporc = clsCotacao2InfoRef.irrfporc;
                clsCompras1Info.iss = clsCotacao2InfoRef.iss;
                clsCompras1Info.issporc = clsCotacao2InfoRef.issporc;
                clsCompras1Info.item = 0;
                clsCompras1Info.msg = "";
                clsCompras1Info.observar = clsCotacao1InfoRef.observar;
                clsCompras1Info.peso = clsCotacao2InfoRef.peso;
                clsCompras1Info.pis = clsCotacao2InfoRef.pis;
                clsCompras1Info.piscofinscsllporc = clsCotacao2InfoRef.piscofincsllporc;
                clsCompras1Info.piscofinscsll = clsCotacao2InfoRef.piscofinscsll;
                clsCompras1Info.pispasep = clsCotacao2InfoRef.pispasep;
                clsCompras1Info.pisporc = clsCotacao2InfoRef.pisporc;
                clsCompras1Info.preco = clsCotacao2InfoRef.preco;
                clsCompras1Info.precobruto = clsCotacao2InfoRef.precobruto;
                clsCompras1Info.qtde = clsCotacao2InfoRef.qtdeinterna;
                clsCompras1Info.qtdebaixada = 0;
                clsCompras1Info.qtdedefeito = 0;
                clsCompras1Info.qtdeentra = 0;
                clsCompras1Info.qtdeentregue = 0;
                clsCompras1Info.qtdefiscal = clsCotacao2InfoRef.qtdeinterna;
                clsCompras1Info.qtdeosaux = 0;
                clsCompras1Info.qtdesaldo = clsCotacao2InfoRef.qtdeinterna;
                clsCompras1Info.qtdesucata = 0;
                clsCompras1Info.qtdetotal = clsCotacao2InfoRef.qtdeinterna;
                clsCompras1Info.qtdetransfe = 0;
                clsCompras1Info.reducao = clsCotacao2InfoRef.reducao;
                clsCompras1Info.termino = "N";
                clsCompras1Info.tipodestino = "I";
                //clsCompras1Info.tipoentrada = "00";
                clsCompras1Info.totalmercado = clsCotacao2InfoRef.totalmercado;
                clsCompras1Info.totalnota = clsCotacao2InfoRef.totalnota;
                clsCompras1Info.totalpeso= clsCotacao2InfoRef.totalpeso;
                clsCompras1Info.totalprevisto = clsCotacao2InfoRef.totalmercado;
                clsCompras1Info.valordesconto = clsCotacao2InfoRef.valordesconto; 
                clsCompras1Info.valorfrete = clsCotacao2InfoRef.valorfrete;
                clsCompras1Info.valorfreteicms = clsCotacao2InfoRef.valorfreteicms;
                clsCompras1Info.valoroutras = clsCotacao2InfoRef.valoroutras;
                clsCompras1Info.valoroutrasicms = clsCotacao2InfoRef.valoroutrasicms;
                clsCompras1Info.valorseguro = clsCotacao2InfoRef.valorseguro;
                clsCompras1Info.valorseguroicms = clsCotacao2InfoRef.valorseguroicms;
                clsCompras1Info.id = clsCompras1BLL.Incluir(clsCompras1Info, clsInfo.conexaosqldados);
           
                // Adicionar as Entregas Previstas do Item
                // Puxar os dados da cotacao2 que ganhou
                String query;
                SqlDataAdapter sda;
                query = "select * FROM COTACAOENTREGA WHERE IDCOTACAO2=" + clsCotacao1InfoRef.idcotacao2ganhou + " ";
                DataTable dtCotacaoEntregaRef = new DataTable();
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtCotacaoEntregaRef);
                foreach (DataRow row in dtCotacaoEntregaRef.Rows)
                {
                    clsCotacaoentregaInfoRef  = new clsCotacaoentregaInfo(); 
                    //clsCotacao2InfoRef = clsCotacao2BLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), clsInfo.conexaosqldados);
                    clsCotacaoentregaInfoRef = clsCotacaoentregaBLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), clsInfo.conexaosqldados);
                    clsComprasEntregaInfo.dataentrega = clsCotacaoentregaInfoRef.dataentrega;
                    clsComprasEntregaInfo.id = 0;
                    clsComprasEntregaInfo.idcompras = idPedidoCompra;
                    clsComprasEntregaInfo.idcompras1 = clsCompras1Info.id;
                    clsComprasEntregaInfo.idos = clsInfo.zordemservico;
                    clsComprasEntregaInfo.qtdebaixada = 0;
                    clsComprasEntregaInfo.qtdedefeito = 0;
                    clsComprasEntregaInfo.qtdeentrega = clsCotacaoentregaInfoRef.qtdeentrega;
                    clsComprasEntregaInfo.qtdeentregue = 0;
                    clsComprasEntregaInfo.qtdeosaux = 0;
                    clsComprasEntregaInfo.qtdesaldo = clsCotacaoentregaInfoRef.qtdeentrega;
                    clsComprasEntregaInfo.qtdesucata = 0;

                    clsComprasEntregaInfo.id = clsComprasEntregaBLL.Incluir(clsComprasEntregaInfo, clsInfo.conexaosqldados);
                }
                // Somar os Itens e totalizar no Pedido de Compras
                Totalizar_Compra();

                // Gravar na Cotação a Data de Termino (tudofechadoem)
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scn.Open();
                scd = new SqlCommand("update cotacao set TUDOFECHADOEM = @TUDOFECHADOEM, TERMINO = @TERMINO  where id=@id ", scn);
                scd.Parameters.AddWithValue("@id", id);
                scd.Parameters.AddWithValue("@TUDOFECHADOEM", DateTime.Now);
                scd.Parameters.AddWithValue("@TERMINO", "F");
                scd.ExecuteNonQuery();

                // Gravar o Id do Compras / Compras1 nas cotações [cotacao1]
                clsCotacao1InfoRef.idpedidocompra = idPedidoCompra;
                clsCotacao1InfoRef.idpedidocompraitem = clsCompras1Info.id;
                clsCotacao1BLL.Alterar(clsCotacao1InfoRef, clsInfo.conexaosqldados);
                // Gravar o Id do Compras / Compras1 nas cotações [cotacao2]
                clsCotacao2InfoRef.idpedidocompra = idPedidoCompra;
                clsCotacao2InfoRef.idpedidocompraitem = clsCompras1Info.id;
                clsCotacao2BLL.Alterar(clsCotacao2InfoRef, clsInfo.conexaosqldados);

            //    tse.Complete();
            //}


        }
        private void Totalizar_Compra()
        {
            //
            Decimal vTotalbaseicm = 0;
            Decimal vTotalbaseicmsubst = 0;
            Decimal vCofins = 0;
            Decimal vCofins1 = 0;
            Decimal vCsll = 0;
            Decimal vTotalicm = 0;
            Decimal vTotalipi = 0;
            Decimal vInss = 0;
            Decimal vIrrf = 0;
            Decimal vIss = 0;
            Decimal vPis = 0;
            Decimal vPisCofinsCsll = 0;
            Decimal vPisPasep = 0;
            Decimal vQtdebaixada = 0;
            Decimal vQtdedefeito = 0;
            Decimal vQtdeentregue = 0;
            Decimal vTotalpeca = 0;
            Decimal vTotalpeso = 0;
            Decimal vQtdeosaux = 0;
            Decimal vQtdesaldo = 0;
            Decimal vQtdesucata = 0;
            Decimal vTotalmercadoria = 0;
            Decimal vTotalpedido = 0;
            Decimal vTotalprevisto = 0;
            Decimal vTotalfrete = 0;
            Decimal vTotaloutras = 0;
            Decimal vTotalseguro = 0;

            String query;
            SqlDataAdapter sda;
            query = "select * FROM COMPRAS1 WHERE IDCOMPRAS=" + idPedidoCompra + " ";
            DataTable dtCompras1 = new DataTable();
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtCompras1);
            foreach (DataRow row in dtCompras1.Rows)
            {
                // Somar os campos do cabeçalho
                vTotalbaseicm = vTotalbaseicm + clsParser.DecimalParse(row["baseicm"].ToString());
                vTotalbaseicmsubst = vTotalbaseicmsubst + clsParser.DecimalParse(row["baseicmsubst"].ToString());
                vCofins = vCofins + clsParser.DecimalParse(row["cofins"].ToString());
                vCofins1 = vCofins1 + clsParser.DecimalParse(row["cofins1"].ToString());
                vCsll = vCsll + clsParser.DecimalParse(row["csll"].ToString());
                vTotalicm = vTotalicm + clsParser.DecimalParse(row["custoicm"].ToString());
                vTotalipi = vTotalipi + clsParser.DecimalParse(row["custoipi"].ToString());
                // info.desconto = row["desconto"].ToString());
                vInss = vInss + clsParser.DecimalParse(row["inss"].ToString());
                //vTotalipi = vTotalipi + clsParser.DecimalParse(row["ipi"].ToString());
                vIrrf = vIrrf + clsParser.DecimalParse(row["irrf"].ToString());
                vIss = vIss + clsParser.DecimalParse(row["iss"].ToString());
                vPis = vPis + clsParser.DecimalParse(row["pis"].ToString());
                vPisCofinsCsll = vPisCofinsCsll + clsParser.DecimalParse(row["piscofinscsll"].ToString());
                vPisPasep = vPisPasep + clsParser.DecimalParse(row["pispasep"].ToString());
                vQtdebaixada = vQtdebaixada + clsParser.DecimalParse(row["qtdebaixada"].ToString());
                vQtdedefeito = vQtdedefeito + clsParser.DecimalParse(row["qtdedefeito"].ToString());
                vQtdeentregue = vQtdeentregue + clsParser.DecimalParse(row["qtdeentregue"].ToString());
                vTotalpeca = vTotalpeca + clsParser.DecimalParse(row["qtdefiscal"].ToString());
                vQtdeosaux = vQtdeosaux + clsParser.DecimalParse(row["qtdeosaux"].ToString());
                vQtdesaldo = vQtdesaldo + clsParser.DecimalParse(row["qtdesaldo"].ToString());
                vQtdesucata = vQtdesucata + clsParser.DecimalParse(row["qtdesucata"].ToString());
                vTotalpeso = vTotalpeso + clsParser.DecimalParse(row["totalpeso"].ToString());
                vTotalmercadoria = vTotalmercadoria + clsParser.DecimalParse(row["totalmercado"].ToString());
                vTotalpedido = vTotalpedido + clsParser.DecimalParse(row["totalnota"].ToString());
                vTotalprevisto = vTotalprevisto + clsParser.DecimalParse(row["totalprevisto"].ToString());
                vTotalfrete = vTotalfrete + clsParser.DecimalParse(row["valorfrete"].ToString());
                vTotaloutras = vTotaloutras + clsParser.DecimalParse(row["valoroutras"].ToString());
                vTotalseguro = vTotalseguro + clsParser.DecimalParse(row["valorseguro"].ToString());
            }
            //Gravar no Cabeçalho do Pedido de Compras
            // Carregar
            clsComprasInfo = clsComprasBLL.Carregar(idPedidoCompra, clsInfo.conexaosqldados);  
            clsComprasInfo.cofins = vCofins;
            clsComprasInfo.cofins1 = vCofins1;
            clsComprasInfo.csll = vCsll;
            clsComprasInfo.inss = vInss;
            clsComprasInfo.irrf = vIrrf;
            clsComprasInfo.iss = vIss;
            clsComprasInfo.pis = vPis;
            clsComprasInfo.piscofinscsll = vPisCofinsCsll;
            clsComprasInfo.pispasep = vPisPasep;
            clsComprasInfo.totalbaseicm = vTotalbaseicm;
            clsComprasInfo.totalbaseicmsubst = vTotalbaseicmsubst;
            clsComprasInfo.totalfrete = vTotalfrete;
            clsComprasInfo.totalicm = vTotalicm;
            //clsComprasInfo.totalicmsubst =
            clsComprasInfo.totalipi = vTotalipi;
            clsComprasInfo.totalmercadoria = vTotalmercadoria;
            clsComprasInfo.totaloutras = vTotaloutras;
            clsComprasInfo.totalpeca = vTotalpeca;
            clsComprasInfo.totalpecaentra = 0;
            clsComprasInfo.totalpecatransfe = 0;
            clsComprasInfo.totalpedido = vTotalpedido;
            clsComprasInfo.totalpeso = vTotalpeso;
            clsComprasInfo.totalprevisto = vTotalprevisto;
            clsComprasInfo.totalseguro = vTotalseguro;
            // Gravar a Alteração no cabeçalho do Pedido de Compras
            clsComprasInfo.id = clsComprasBLL.Alterar(clsComprasInfo, clsInfo.conexaosqldados);
        }

        private void tsbCotacao_Imprimir_Click(object sender, EventArgs e)
        {
            //if (tspSalvar.Enabled == true)
            //{
            //    PedidoSalvar();
            //}

            // Imprimir Entrada da despesa
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = id;
            field.Name = "id";
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            //field = new ParameterField();
            //field.Name = "condpagto";
            //valor = new ParameterDiscreteValue();
            //valor.Value = CondPagto;
            //field.CurrentValues.Add(valor);
            //parameters.Add(field);


            frmCrystalReport.Init(clsInfo.caminhorelatorios, "COTACAO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

        }
    }
}

