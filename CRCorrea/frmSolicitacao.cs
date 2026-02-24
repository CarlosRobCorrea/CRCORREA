using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSolicitacao : Form
    {
        clsSolicitacaoBLL clsSolicitacaoBLL;
        clsSolicitacaoInfo clsSolicitacaoInfo;
        clsSolicitacaoInfo clsSolicitacaoInfoOld;


        //crio variaveis que correspondem aos campos ids da tabela SOLICITACAO
        Int32 id;
        Int32 idSolicitante;
        Int32 idArea;
        Int32 idDepto;
        Int32 idPedidoCompra;
        Int32 idPedidoCompraItem;
        Int32 idCotacao;
        Int32 idCotacaoItem;
        Int32 idCodigo;
        Int32 idHistorico;
        Int32 idCentroCusto;
        Int32 idCodigoCTabil;
        Int32 idOS;
        Int32 idDestino;
        Int32 idUnid;
        Int32 idAutorizanteSol;
        Int32 idAutorizanteAlmox;
        Int32 idAutorizanteGer;
        Int32 fornecedorGanhou; //aqui indicamos qual fornecedor ganhou a solicitação
        Int32 painel;

        DataGridViewRowCollection rows;

        //crio variaveis que correspondem aos campos ids da tabela SOLICITACAOENTREGA
        Int32 solicitacaoentrega_id;
        Int32 solicitacaoentrega_idSolicitacao;
        Int32 solicitacaoentrega_posicao;
        clsSolicitacaoentregaBLL clsSolicitacaoEntregaBLL;
        clsSolicitacaoentregaInfo clsSolicitacaoEntregaInfo;
        clsSolicitacaoentregaInfo clsSolicitacaoEntregaInfoOld;
        DataTable dtSolicitacaoEntrega;

        //crio variaveis que correspondem aos campos ids da tabela SOLICITACAOFORNECEDOR
        Int32 solicitacaoFornecedor_id;
        Int32 solicitacaoFornecedor_idSolicitacao;
        Int32 solicitacaoFornecedor_idFornecedor;
        String solicitacaoFornecedor_preferencia;
        Int32 solicitacaofornecedor_posicao;
        clsSolicitacaoFornecedorBLL clsSolicitacaoFornecedorBLL;
        clsSolicitacaoFornecedorInfo clsSolicitacaoFornecedorInfo;
        clsSolicitacaoFornecedorInfo clsSolicitacaoFornecedorInfoOld;
        DataTable dtSolicitacaoFornecedor;
        BackgroundWorker bwrSolicitacaoFornecedor;

        // cadastro notas fiscais
        DataTable dtNFCompra1;

        public frmSolicitacao()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id, DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;

            clsSolicitacaoBLL = new clsSolicitacaoBLL();
            clsSolicitacaoEntregaBLL = new clsSolicitacaoentregaBLL();
            clsSolicitacaoFornecedorBLL = new clsSolicitacaoFornecedorBLL();

            // devido ao toolstrip perder a propriedade de visible = true inicializamos a propriedade abaixo
            tspSolicitacao.Visible = true;
            tspSolicitacaoEntrega.Visible = true;
            tspSolicitacaoFornecedores.Visible = true;
            tspSolicitacaoFornecedorItem.Visible = true;
            tspSolicitacaoRegistroEntrega.Visible = true;
            // geralmente os textbox de cor cinza seguidos de um botão de lupa é usado o código abaixo para preencher o textbox com o valor da tabela              
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas where ativo='S' order by codigo", tbxCodigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo = 'F'", tbxFornecedoresCredenciados);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select numero from ordemservico", tbxOrdemServico);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas where ativo='S' order by codigo", tbxCodigoDestino);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos", tbxHistorico);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos", tbxCentroCusto);
        }

        private void frmSolicitacao_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void frmSolicitacao_Load(object sender, EventArgs e)
        {
            SolicitacaoCarregar();
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
        }

        private void ControlKeyDownDataHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownDataHora((TextBox)sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void SolicitacaoCarregar()
        {
            clsSolicitacaoInfoOld = new clsSolicitacaoInfo();
            if (id == 0)
            {
                clsSolicitacaoInfo = new clsSolicitacaoInfo();
                clsSolicitacaoInfo.ano = DateTime.Now.Year;
                clsSolicitacaoInfo.aprovadofab = "";
                clsSolicitacaoInfo.aprovadoger = "";
                clsSolicitacaoInfo.aprovadosol = "";
                clsSolicitacaoInfo.area = " ";
                clsSolicitacaoInfo.complemento = "";
                clsSolicitacaoInfo.complemento1 = "";
                clsSolicitacaoInfo.consumo = "C";
                clsSolicitacaoInfo.data = DateTime.Now;
                clsSolicitacaoInfo.emitente = clsInfo.zusuario;
                clsSolicitacaoInfo.filial = clsInfo.zfilial;
                clsSolicitacaoInfo.fornecedorganhou = 0;
                //clsSolicitacaoInfo.idarea = 0;
                clsSolicitacaoInfo.idautorizantealmox = clsInfo.zusuarioid;
                clsSolicitacaoInfo.idautorizanteger = clsInfo.zusuarioid;
                clsSolicitacaoInfo.idautorizantesol = clsInfo.zusuarioid;
                clsSolicitacaoInfo.idcentrocusto = clsInfo.zcentrocustos;
                clsSolicitacaoInfo.idcodigo = clsInfo.zpecas;
                clsSolicitacaoInfo.idcodigoctabil = clsInfo.zcontacontabil;
                clsSolicitacaoInfo.idcotacao = clsInfo.zcotacao;
                clsSolicitacaoInfo.idcotacaoitem = clsInfo.zcotacao1;
                clsSolicitacaoInfo.iddepto = 0;
                clsSolicitacaoInfo.iddestino = 0;
                clsSolicitacaoInfo.idhistorico = clsInfo.zhistoricos;
                clsSolicitacaoInfo.idos = clsInfo.zordemservico;
                clsSolicitacaoInfo.idpedidocompra = clsInfo.zcompras;
                clsSolicitacaoInfo.idpedidocompraitem = clsInfo.zcompras1;
                clsSolicitacaoInfo.idsolicitante = clsInfo.zusuarioid;
                clsSolicitacaoInfo.idarea = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idmaqct from usuario WHERE ID= " + clsSolicitacaoInfo.idsolicitante + " "));
                clsSolicitacaoInfo.area = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from maqct WHERE ID= " + clsSolicitacaoInfo.idsolicitante + " ");
                clsSolicitacaoInfo.idunid = clsInfo.zunidade;
                clsSolicitacaoInfo.fornecedorganhou = 0;
                clsSolicitacaoInfo.motivojustificado = "";
                clsSolicitacaoInfo.motivoreprovado = "";
                clsSolicitacaoInfo.numero = 0;
                clsSolicitacaoInfo.prioridade = "B";
                clsSolicitacaoInfo.qtde = 0;
                clsSolicitacaoInfo.qtdesol = 0;
                clsSolicitacaoInfo.qtdetotal = 0;
                clsSolicitacaoInfo.tipodestino = "";
                clsSolicitacaoInfo.tipoproduto = "00";
            }
            else
            {
                clsSolicitacaoInfo = clsSolicitacaoBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            SolicitacaoCampos(clsSolicitacaoInfo);
            SolicitacaoFillInfo(clsSolicitacaoInfoOld);

            // carregando solicitacao fornecedor
            dtSolicitacaoFornecedor = clsSolicitacaoFornecedorBLL.GridDados(clsSolicitacaoInfo.id, clsInfo.conexaosqldados);
            if (dtSolicitacaoFornecedor == null)
            {
                dtSolicitacaoFornecedor = clsSolicitacaoFornecedorBLL.GridDados(0, clsInfo.conexaosqldados);
            }
            DataColumn dtSolicitacaoFornecedor_posicao = new DataColumn("solicitacaofornecedor_posicao", Type.GetType("System.Int32"));
            dtSolicitacaoFornecedor.Columns.Add(dtSolicitacaoFornecedor_posicao);
            for (Int32 i = 1; i <= dtSolicitacaoFornecedor.Rows.Count; i++)
            {
                dtSolicitacaoFornecedor.Rows[i - 1]["solicitacaofornecedor_posicao"] = i;
            }
            dtSolicitacaoFornecedor.AcceptChanges();
            clsSolicitacaoFornecedorBLL.GridMonta(dgvFornecedores, dtSolicitacaoFornecedor, solicitacaoFornecedor_id);
            //preenche a visualização de baixo dos fornecedores credenciados
            clsSolicitacaoFornecedorBLL.GridMonta(dgvSolicitacaoFornecedor, dtSolicitacaoFornecedor, solicitacaoFornecedor_id);

            verificaNumForne(dtSolicitacaoFornecedor.Rows.Count);

            // carregando solicitacao Entrega
            dtSolicitacaoEntrega = clsSolicitacaoentregaBLL.GridDados(clsSolicitacaoInfo.id, clsInfo.conexaosqldados);
            if (dtSolicitacaoFornecedor == null)
            {
                dtSolicitacaoEntrega = clsSolicitacaoentregaBLL.GridDados(0, clsInfo.conexaosqldados);
            }
            lbxTipoCalculo.SelectedIndex = lbxTipoCalculo.FindString("Unica");
            if (lbxTipoCalculo.SelectedIndex == -1)
            {
                lbxTipoCalculo.SelectedIndex = 0;
            }
            rbnPrioridadeA.PerformClick();
            DataColumn dtSolicitacaoEntrega_posicao = new DataColumn("solicitacaoentrega_posicao", Type.GetType("System.Int32"));
            dtSolicitacaoEntrega.Columns.Add(dtSolicitacaoEntrega_posicao);
            for (Int32 i = 1; i <= dtSolicitacaoEntrega.Rows.Count; i++)
            {
                dtSolicitacaoEntrega.Rows[i - 1]["solicitacaoentrega_posicao"] = i;
            }
            tbxQtdeEntregas.Text = "1";
            dtSolicitacaoEntrega.AcceptChanges();
            clsSolicitacaoentregaBLL.GridMonta(dgvSolicitacaoEntrega, dtSolicitacaoEntrega, solicitacaoentrega_id);
            SolicitacaoEntregaSomar();
            VerificarMudancaProgEntrega();

            bwrSolicitacaoFornecedor_Run();


            if (id > 0)
            {
                if (tbxCodigo.Text != "0")
                {
                    // carregando as notas fiscais de entrada relaciona com este produto
                    dtNFCompra1 = clsNfCompra1BLL.GridDadosPeca(clsSolicitacaoInfo.idcodigo, 20, clsInfo.conexaosqldados);
                    clsNfCompra1BLL.GridMontaPeca(dgvNFCompra, dtNFCompra1, 0);
                }
                else
                {
                    if (dtNFCompra1 != null)
                    {
                        dtNFCompra1.Clear();
                    }
                }
            }
        }

        private void SolicitacaoCampos(clsSolicitacaoInfo info)
        {
            id = info.id;
            idSolicitante = info.idsolicitante;
            idArea = info.idarea;
            idDepto = info.iddepto;
            idPedidoCompra = info.idpedidocompra;
            idPedidoCompraItem = info.idpedidocompraitem;
            idCotacao = info.idcotacao;
            idCotacaoItem = info.idcotacaoitem;
            idCodigo = info.idcodigo;
            idHistorico = info.idhistorico;
            idCentroCusto = info.idcentrocusto;
            idCodigoCTabil = info.idcodigoctabil;
            idOS = info.idos;
            idDestino = info.iddestino;
            idUnid = info.idunid;
            idAutorizanteSol = info.idautorizantesol;
            idAutorizanteAlmox = info.idautorizantealmox;
            idAutorizanteGer = info.idautorizanteger;
            fornecedorGanhou = info.fornecedorganhou;

            tbxAno.Text = info.ano.ToString();
            //
            cbxAprovadoFab.SelectedIndex = cbxAprovadoFab.FindString(info.aprovadofab);
            if (cbxAprovadoFab.SelectedIndex == -1)
            {
                cbxAprovadoFab.SelectedIndex = 0;
            }
            //
            cbxAprovadoGer.SelectedIndex = cbxAprovadoGer.FindString(info.aprovadoger);
            if (cbxAprovadoGer.SelectedIndex == -1)
            {
                cbxAprovadoGer.SelectedIndex = 0;
            }

            //
            cbxAprovadoSol.SelectedIndex = cbxAprovadoSol.FindString(info.aprovadosol);
            if (cbxAprovadoSol.SelectedIndex == -1)
            {
                cbxAprovadoSol.SelectedIndex = 0;
            }

            //
            tbxArea.Text = info.area;
            tbxComplemento.Text = info.complemento;
            tbxComplemento1.Text = info.complemento1;
            if (info.consumo == "S")
            {
                rbnConsumo.Checked = true;
            }
            else
            {
                rbnRevenda.Checked = true;
            }

            tbxData.Text = info.data.ToString("dd/MM/yyyy HH:mm");
            tbxEmitente.Text = info.emitente;
            tbxFilial.Text = info.filial.ToString();



            tbxAutorizanteAlmox.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO WHERE ID= " + idAutorizanteAlmox + " ");
            idAutorizanteGer = info.idautorizanteger;
            tbxAutorizanteGer.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO WHERE ID= " + idAutorizanteGer + " ");
            tbxAutorizanteSol.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO WHERE ID= " + idAutorizanteSol + " ");
            tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS WHERE ID= " + idCentroCusto + " ");
            tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS WHERE ID= " + idCodigo + " ");
            tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE ID= " + idCodigo + " ");

            tbxCotacao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from COTACAO WHERE ID= " + idCotacao + " ");
            if (clsParser.Int32Parse(tbxCotacao.Text) > 0)
            {
                tbxDataCotacao.Text = DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DATAMONTAGEM from COTACAO WHERE ID= " + idCotacao + " ")).ToString("dd/MM/yyyy");
            }

            tbxCodigoDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS WHERE ID= " + idDestino + " ");
            tbxNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE ID= " + idDestino + " ");
            tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS WHERE ID= " + idHistorico + " ");
            tbxOrdemServico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from ORDEMSERVICO WHERE ID= " + idOS) + " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select year(data) from ORDEMSERVICO WHERE ID= " + idOS);

            tbxPedidoCompra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from COMPRAS WHERE ID= " + idPedidoCompra + " ");
            if (clsParser.Int32Parse(tbxPedidoCompra.Text) > 0)
            {
                tbxDataPedido.Text = DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DATA from COMPRAS WHERE ID= " + idPedidoCompra + " ")).ToString("dd/MM/yyyy");
            }

            tbxSolicitante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usuario from usuario WHERE ID= " + idSolicitante + " ");

            tbxUnidSol.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE WHERE ID= " + idUnid + " ");
            tbxMotivoJustificado.Text = info.motivojustificado;
            tbxMotivoReprovado.Text = info.motivoreprovado;

            tbxNumero.Text = info.numero.ToString();
            if (info.prioridade == "A")
            {
                rbnPrioridadeA.Checked = true;
            }
            else if (info.prioridade == "B")
            {
                rbnPrioridadeB.Checked = true;
            }
            else
            {
                rbnPrioridadeM.Checked = true;
            }

            tbxQtde.Text = info.qtde.ToString("N3");
            tbxQtdeSol.Text = info.qtdesol.ToString("N3");
            tbxQtdeTotal.Text = info.qtdetotal.ToString("N3");
            if (info.tipodestino == "M")
            {
                rbnTipoDestinoMaq.Checked = true;
            }
            else
            {
                rbnTipoDestinoItem.Checked = true;
            }

            lbxTipoProduto.SelectedIndex = lbxTipoProduto.FindString(info.tipoproduto);
            if (lbxTipoProduto.SelectedIndex == -1)
            {
                lbxTipoProduto.SelectedIndex = 0;
            }
        }


        private void SolicitacaoFillInfo(clsSolicitacaoInfo info)
        {
            info.id = id;
            info.idarea = idArea;
            info.idautorizantealmox = idAutorizanteAlmox;
            info.idautorizanteger = idAutorizanteGer;
            info.idautorizantesol = idAutorizanteSol;
            info.idcentrocusto = idCentroCusto;
            info.idcodigoctabil = idCodigoCTabil;
            info.idcodigo = idCodigo;
            info.idcotacao = idCotacao;
            info.idcotacaoitem = idCotacaoItem;
            info.iddestino = idDestino;
            info.idhistorico = idHistorico;
            info.idos = idOS;
            info.idpedidocompra = idPedidoCompra;
            info.idpedidocompraitem = idPedidoCompraItem;
            info.idunid = idUnid;
            info.idsolicitante = idSolicitante;
            info.fornecedorganhou = fornecedorGanhou;


            info.ano = clsParser.Int32Parse(tbxAno.Text);
            info.aprovadofab = cbxAprovadoFab.Text.Substring(0, 1);
            info.aprovadoger = cbxAprovadoGer.Text.Substring(0, 1);
            info.aprovadosol = cbxAprovadoSol.Text.Substring(0, 1);
            info.complemento = tbxComplemento.Text;
            info.complemento1 = tbxComplemento1.Text;
            if (rbnConsumo.Checked == true)
            {
                info.consumo = "S";
            }
            else
            {
                info.consumo = "N";
            }
            info.data = DateTime.Parse(tbxData.Text);

            info.emitente = tbxEmitente.Text;
            info.filial = clsParser.Int32Parse(tbxFilial.Text);
            info.fornecedorganhou = 0;

            info.area = tbxArea.Text;
            info.motivojustificado = tbxMotivoJustificado.Text;
            info.motivoreprovado = tbxMotivoReprovado.Text;

            info.numero = clsParser.Int32Parse(tbxNumero.Text);
            if (rbnPrioridadeA.Checked == true)
            {
                info.prioridade = "A";
            }
            else if (rbnPrioridadeB.Checked == true)
            {
                info.prioridade = "B";
            }
            else
            {
                info.prioridade = "M";
            }

            info.qtde = clsParser.DecimalParse(tbxQtde.Text);
            info.qtdesol = clsParser.DecimalParse(tbxQtdeSol.Text);
            info.qtdetotal = clsParser.DecimalParse(tbxQtdeTotal.Text);
            if (rbnTipoDestinoMaq.Checked == true)
            {
                info.tipodestino = "M";
            }
            else
            {
                info.tipodestino = "F";
            }

            if (lbxTipoProduto.Text == "")
            {
                info.tipoproduto = "00";
                lbxTipoProduto.SelectedIndex = 0;
            }
            else
            {
                info.tipoproduto = lbxTipoProduto.Text.Substring(0, 2);
            }
        }


        private Boolean HouveModificacoes()
        {
            clsSolicitacaoInfo = new clsSolicitacaoInfo();
            SolicitacaoFillInfo(clsSolicitacaoInfo);
            if (clsSolicitacaoBLL.Equals(clsSolicitacaoInfo, clsSolicitacaoInfoOld) == false)
            {
                return true;
            }
            return false;
        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                SolicitacaoSalvar();

            }
            return drt;
        }



        private void tsbSolicitacao_Salvar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Salvar() == DialogResult.Cancel)
                {
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsbSolicitacao_Primeiro_Click(object sender, EventArgs e)
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
                    SolicitacaoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbSolicitacao_Anterior_Click(object sender, EventArgs e)
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
                                SolicitacaoCarregar();
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

        private void tsbSolicitacao_Proximo_Click(object sender, EventArgs e)
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
                                SolicitacaoCarregar();
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

        private void tsbSolicitacao_Ultimo_Click(object sender, EventArgs e)
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
                    SolicitacaoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbSolicitacao_Retornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                tsbSolicitacao_Salvar.PerformClick();
            }
            else
            {
                this.Close();
            }
        }

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1 &&
                clsInfo.znomegrid != null &&
                clsInfo.znomegrid != "")
            {
                // primeira parte verificamos apenas os botões
                if (clsInfo.znomegrid == btnIdCodigo.Name)
                {
                    Int32 idcodigo_old = idCodigo;
                    //código
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idCodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                        tbxNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                        idUnid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + idCodigo + ""));
                        tbxUnid.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + idUnid + "");
                        tbxUnidSol.Text = tbxUnid.Text;
                        idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOBCO from pecas where ID = " + idCodigo));
                        tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + idHistorico);

                        lbxTipoProduto.SelectedIndex = lbxTipoProduto.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOPRODUTO from PECAS where ID=" + idCodigo));
                        String consumo = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONSUMO from PECAS where ID=" + idCodigo);
                        if (consumo == "N")
                        {
                            rbnRevenda.Checked = true;
                        }
                        else
                        {
                            rbnConsumo.Checked = true;
                        }
                        if (idcodigo_old != idCodigo)
                        {
                            //carregando as notas fiscais de entrada relaciona com este produto
                            dtNFCompra1 = clsNfCompra1BLL.GridDadosPeca(idCodigo, 20, clsInfo.conexaosqldados);
                            clsNfCompra1BLL.GridMontaPeca(dgvNFCompra, dtNFCompra1, 0);
                        }
                    }
                    //preenche o grid com os fornecedores prospecção da item
                    VerificaSeMudouFornecedor(idcodigo_old);

                    tbxCodigo.Select();
                }
                //
                if (clsInfo.znomegrid == btnIdHistorico.Name)
                {
                    idHistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxHistorico.Select();
                    tbxHistorico.SelectAll();
                }
                // centro custo
                if (clsInfo.znomegrid == btnIdCentroCusto.Name)
                {
                    idCentroCusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCentroCusto.Select();
                    tbxCentroCusto.SelectAll();
                }
                //ordem de servico
                if (clsInfo.znomegrid == btnIdOs.Name)
                {
                    idOS = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxOrdemServico.Text = clsInfo.zrow.Cells["NUMERO"].Value.ToString() + " - " + clsInfo.zrow.Cells["ANO"].Value.ToString();
                }

                if (clsInfo.znomegrid == btnFornecedores.Name)
                {
                    solicitacaoFornecedor_idFornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["id"].Value.ToString());
                    tbxFornecedoresCredenciados.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    TbxFornededorNome.Text = clsInfo.zrow.Cells["nome"].Value.ToString();
                }


                //Destino
                if (clsInfo.znomegrid == btnIdDestino.Name)
                {
                    idDestino = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodigoDestino.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxNomeDestino.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxCodigoDestino.Select();
                    tbxCodigoDestino.SelectAll();
                }
            }
            else
            {
                //Segunda parte verificamos apenas os textboxs
                //código
                if (ctl.Name == tbxCodigo.Name)
                {
                    Int32 idcodigo_old = idCodigo;

                    idCodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxCodigo.Text + "'"));
                    if (idCodigo == 0)
                    {
                        idCodigo = clsInfo.zpecas;
                    }

                    tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where ID=" + idCodigo + "");
                    tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where ID=" + idCodigo + "");
                    idUnid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + idCodigo + ""));
                    tbxUnid.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + idUnid + "");

                    tbxLocacao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select locacao from pecas where ID=" + idCodigo + "");
                    tbxSaldo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select QTDESALDO from pecas where ID=" + idCodigo + "");

                    // capturar o ultimo preço
                    labelValor.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nfcompra.data, nfcompra1.idcodigo, nfcompra1.preco from nfcompra inner join nfcompra1 on nfcompra1.numero= nfcompra.id  where IDCODIGO=" + idCodigo + " order by nfcompra.data desc ")).ToString("N4");

                    //preenche o grid com os fornecedores prospecção da item
                    VerificaSeMudouFornecedor(idcodigo_old);


                }
                //Histórico
                if (ctl.Name == tbxHistorico.Name)
                {
                    idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO='" + tbxHistorico.Text + "'"));
                    if (idHistorico == 0)
                    {
                        idHistorico = clsInfo.zhistoricos;
                    }
                    tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID=" + idHistorico + "");
                }
                //centro custo
                if (ctl.Name == tbxCentroCusto.Name)
                {
                    idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCusto.Text + "'"));
                    if (idCentroCusto == 0)
                    {
                        idCentroCusto = clsInfo.zcentrocustos;
                    }
                    tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID=" + idCentroCusto + "");
                }

                // ordem de serviço
                if (ctl.Name == tbxOrdemServico.Name)
                {
                    Int32 nOs;
                    Int32 yearOs;
                    if (tbxOrdemServico.Text.IndexOf(" - ") == -1)
                    {
                        idOS = clsInfo.zordemservico;
                    }
                    else
                    {
                        nOs = clsParser.Int32Parse(tbxOrdemServico.Text.Substring(0, tbxOrdemServico.Text.IndexOf(" - ")));
                        yearOs = clsParser.Int32Parse(tbxOrdemServico.Text.Split('-')[1].Trim());

                        idOS = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ordemservico where numero = " + nOs + " and year(data) = " + yearOs));

                        if (idOS == 0)
                        {
                            idOS = clsInfo.zordemservico;
                        }
                    }

                    tbxOrdemServico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select convert(varchar, numero) + ' - ' + convert(varchar, year(data)) from ordemservico where id=" + idOS);
                }
                // destino               

                if (ctl.Name == tbxCodigoDestino.Name)
                {
                    idDestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxCodigoDestino.Text + "'"));
                    if (idDestino == 0)
                    {
                        idDestino = clsInfo.zpecas;
                    }
                    tbxCodigoDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where ID=" + idDestino + "");
                    tbxNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where ID=" + idDestino + "");
                }

                if (ctl.Name == tbxQtdeSol.Name)
                {
                    if (rbnPrioridadeA.Checked == true)
                    {
                        rbnPrioridadeA.Select();
                    }
                    else if (rbnPrioridadeB.Checked == true)
                    {
                        rbnPrioridadeB.Select();
                    }
                    else
                    {
                        rbnPrioridadeM.Select();
                    }
                    this.VerificarMudancaProgEntrega();
                }


                if (ctl.Name == SolicitacaoEntrega_tbxQtdeentrega.Name)
                {
                    tsbSolicitacao_Salvar.Select();
                }

                // QTDES
                Int32 qtdes_cd = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select UNIDDEC from UNIDADE WHERE ID= " + idUnid + " "));

                tbxQtdeSol.Text = clsParser.DecimalParse(tbxQtdeSol.Text).ToString("N" + qtdes_cd.ToString());
                tbxQtde.Text = clsParser.DecimalParse(tbxQtdeSol.Text).ToString("N" + qtdes_cd.ToString());
                tbxQtdeTotal.Text = clsParser.DecimalParse(tbxQtdeSol.Text).ToString("N" + qtdes_cd.ToString());

                tbxQtdeProgramada.Text = clsParser.DecimalParse(tbxQtdeProgramada.Text).ToString("N" + qtdes_cd.ToString());

            }
            clsInfo.zrow = null;
            clsInfo.znomegrid = "";
        }


        private void VerificaSeMudouFornecedor(Int32 idcodigo_old)
        {
            // se mudou o codigo - carregar o fornecedor
            if (idcodigo_old != idCodigo)
            {
                // 1. verificar se já tem fornecedor cadastrado
                DataRow[] rowsSolicitacaForne = dtSolicitacaoFornecedor.Select();
                for (Int32 i = 0; i < rowsSolicitacaForne.Length; i++)
                {
                    rowsSolicitacaForne[i].Delete();
                }

                // 2. carregar os novos fornecedores
                DataTable dtPecasForne = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
                                           "select CLIENTEPROSPECCAO.id as CLIENTEPROSPECCAO_ID, CLIENTEPROSPECCAO.COGNOME AS [fornecedor] " +
                                           "FROM PECASFORNE " +
                                           "LEFT JOIN CLIENTEPROSPECCAO ON CLIENTEPROSPECCAO.IDCLIENTE = PECASFORNE.IDFORNECEDOR " +
                                           "WHERE " +
                                           "PECASFORNE.IDCODIGO = " + idCodigo +
                                           "ORDER BY CLIENTEPROSPECCAO.COGNOME");

                if (dtPecasForne.Rows.Count > 0)
                {
                    foreach (DataRow row in dtPecasForne.Rows)
                    {
                        DataRow rowSolicitacaoFornecedor;
                        rowSolicitacaoFornecedor = dtSolicitacaoFornecedor.NewRow();
                        rowSolicitacaoFornecedor["id"] = 0;
                        rowSolicitacaoFornecedor["idsolicitacao"] = id;
                        rowSolicitacaoFornecedor["idfornecedor"] = row["CLIENTEPROSPECCAO_ID"];
                        rowSolicitacaoFornecedor["fornecedor"] = row["fornecedor"];
                        rowSolicitacaoFornecedor["preferencia"] = "";
                        rowSolicitacaoFornecedor["solicitacaofornecedor_posicao"] = dtSolicitacaoFornecedor.Rows.Count + 1;
                        dtSolicitacaoFornecedor.Rows.Add(rowSolicitacaoFornecedor);
                    }
                    dtSolicitacaoEntrega.AcceptChanges();
                }
            }
        }

        private void SolicitacaoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: SOLICITACAO
                clsSolicitacaoInfo = new clsSolicitacaoInfo();

                SolicitacaoFillInfo(clsSolicitacaoInfo);

                if (id == 0)
                {
                    clsSolicitacaoInfo.id = clsSolicitacaoBLL.Incluir(clsSolicitacaoInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsSolicitacaoBLL.Alterar(clsSolicitacaoInfo, clsInfo.conexaosqldados);
                }


                //###############################
                //Tabela: SOLICITACAOENTREGA
                //salvar a programação de entrega
                foreach (DataRow row in dtSolicitacaoEntrega.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idsolicitacao"] = clsSolicitacaoInfo.id;
                    }
                }

                //Verifica se a quantidade solicitada e programada são iguais e se a peça escolhida está ativa            

                if (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ativo from pecas where id = " + this.idCodigo).ToString() != "S")
                {
                    throw new Exception("Peça não pode ser inativa");
                }

                if (clsSolicitacaoInfo.qtdesol != clsParser.DecimalParse(tbxQtdeProgramada.Text))
                {
                    throw new Exception("A soma do total programado ultrapassou a Qtde Solicitada.");
                }

                foreach (DataRow row in dtSolicitacaoEntrega.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsSolicitacaoEntregaBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsSolicitacaoEntregaInfo = new clsSolicitacaoentregaInfo();
                        SolicitacaoEntregaGridToInfo(clsSolicitacaoEntregaInfo, clsParser.Int32Parse(row["solicitacaoentrega_posicao"].ToString()));

                        if (clsSolicitacaoEntregaInfo.id == 0)
                        {
                            clsSolicitacaoEntregaInfo.id = clsSolicitacaoEntregaBLL.Incluir(clsSolicitacaoEntregaInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsSolicitacaoEntregaBLL.Alterar(clsSolicitacaoEntregaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }

                //###############################

                foreach (DataRow row in dtSolicitacaoFornecedor.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idsolicitacao"] = clsSolicitacaoInfo.id;
                    }
                }

                foreach (DataRow row in dtSolicitacaoFornecedor.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsSolicitacaoFornecedorBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsSolicitacaoFornecedorInfo = new clsSolicitacaoFornecedorInfo();
                        SolicitacaoFornecedorGridToInfo(clsSolicitacaoFornecedorInfo, clsParser.Int32Parse(row["solicitacaofornecedor_posicao"].ToString()));

                        if (clsSolicitacaoFornecedorInfo.id == 0)
                        {
                            clsSolicitacaoFornecedorInfo.id = clsSolicitacaoFornecedorBLL.Incluir(clsSolicitacaoFornecedorInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsSolicitacaoFornecedorBLL.Alterar(clsSolicitacaoFornecedorInfo, clsInfo.conexaosqldados);
                        }
                    }
                }

                tse.Complete();
            }
        }

        private void btnIdCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCodigo.Name;
            frmPecasPes frmPecasPes = new frmPecasPes(); // frmEscFormVisPes(clsInfo.conexaosqldados, "PECAS", idcodigo);
            frmPecasPes.Init(idCodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnIdHistorico_Click(object sender, EventArgs e)
        {

            clsInfo.znomegrid = btnIdHistorico.Name;
            frmHistoricosPes frmHistoricosPes = new frmHistoricosPes(); // frmEscFormVisPes(clsInfo.conexaosqlbanco, "HISTORICOS", idhistorico, "ATIVO", "S");
            frmHistoricosPes.Init(clsInfo.conexaosqlbanco, idHistorico);
            clsFormHelper.AbrirForm(this.MdiParent, frmHistoricosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCentroCusto.Name;
            frmCentroCustosPes frmCentroCustosPes = new frmCentroCustosPes(); // frmEscFormVisPes(clsInfo.conexaosqlbanco, "CENTROCUSTOS", idcentrocusto, "ATIVO", "S");
            frmCentroCustosPes.Init(clsInfo.conexaosqlbanco, idCentroCusto);
            clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdOs_Click(object sender, EventArgs e)
        {
            //clsInfo.znomegrid = btnIdOs.Name;
            //frmOrdemServicoPes frmOrdemServicoPes = new frmOrdemServicoPes();// (idos);
            //frmOrdemServicoPes.Init(idOS);
            //clsFormHelper.AbrirForm(this.MdiParent, frmOrdemServicoPes, clsInfo.conexaosqldados);

        }

        private void btnIdDestino_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDestino.Name;
            frmPecasPes frmPecasPes = new frmPecasPes(); // frmEscFormVisPes(clsInfo.conexaosqldados, "PECAS", iddestino);
            frmPecasPes.Init(idDestino);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        void bwrSolicitacaoFornecedor_Run()
        {
            dgvFornecedores.DataSource = null;

            bwrSolicitacaoFornecedor = new BackgroundWorker();
            bwrSolicitacaoFornecedor.DoWork += new DoWorkEventHandler(bwrSolicitacaoFornecedor_DoWork);
            bwrSolicitacaoFornecedor.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSolicitacaoFornecedor_RunWorkerCompleted);
            bwrSolicitacaoFornecedor.RunWorkerAsync();
        }

        void bwrSolicitacaoFornecedor_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSolicitacaoFornecedor = clsSolicitacaoFornecedorBLL.GridDados(id, clsInfo.conexaosqldados);

            dtSolicitacaoFornecedor.Columns.Add("solicitacaofornecedor_posicao", Type.GetType("System.Int32"));
            for (Int32 x = 0; x < dtSolicitacaoFornecedor.Rows.Count; x++)
            {
                dtSolicitacaoFornecedor.Rows[x]["solicitacaofornecedor_posicao"] = x + 1;
            }

            dtSolicitacaoFornecedor.AcceptChanges();
        }

        void bwrSolicitacaoFornecedor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsSolicitacaoFornecedorBLL.GridMonta(dgvFornecedores, dtSolicitacaoFornecedor, solicitacaofornecedor_posicao);
            clsSolicitacaoFornecedorBLL.GridMonta(dgvSolicitacaoFornecedor, dtSolicitacaoFornecedor, solicitacaofornecedor_posicao);
            dgvFornecedores.Sort(dgvFornecedores.Columns["FORNECEDOR"], ListSortDirection.Ascending);

        }
        void SolicitacaoEntregaSoma()
        {
            Double qtdeprograma = 0;
            foreach (DataRow row in dtSolicitacaoEntrega.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    qtdeprograma = qtdeprograma + Double.Parse(row["qtdeentrega"].ToString());
                }
            }
            Int32 qtdes_cd = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select UNIDDEC from UNIDADE WHERE ID= " + idUnid + " "));
            tbxQtdeProgramada.Text = clsParser.DecimalParse(qtdeprograma.ToString()).ToString("N" + qtdes_cd.ToString());

            VerificarMudancaProgEntrega();
        }

        private void SolicitacaoEntregaCarregar()
        {
            clsSolicitacaoEntregaInfo = new clsSolicitacaoentregaInfo();
            clsSolicitacaoEntregaInfoOld = new clsSolicitacaoentregaInfo();

            if (solicitacaoentrega_posicao == 0)
            {
                solicitacaoentrega_posicao = dtSolicitacaoEntrega.Rows.Count + 1;
                DataRow[] rows = dtSolicitacaoEntrega.Select();
                Int32 contador = 1;
                foreach (DataRow row in rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                       row.RowState != DataRowState.Detached)
                    {
                        contador++;
                    }
                }
                clsSolicitacaoEntregaInfo.id = 0;
                clsSolicitacaoEntregaInfo.idsolicitacao = 0;
                clsSolicitacaoEntregaInfo.qtdeentrega = 0;
                clsSolicitacaoEntregaInfo.dataentrega = DateTime.Now.AddDays(1);
            }
            else
            {
                SolicitacaoEntregaGridToInfo(clsSolicitacaoEntregaInfo, solicitacaoentrega_posicao);
            }
            SolicitacaoEntregaCampos(clsSolicitacaoEntregaInfo);
            SolicitacaoEntregaFillInfo(clsSolicitacaoEntregaInfoOld);
            SolicitacaoEntrega_tbxDataEntrega.Select();
        }


        private void SolicitacaoEntregaGridToInfo(clsSolicitacaoentregaInfo info, Int32 posicao)
        {
            DataRow row = dtSolicitacaoEntrega.Select("solicitacaoentrega_posicao = " + posicao)[0];

            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idsolicitacao = clsParser.Int32Parse(row["idsolicitacao"].ToString());
            info.dataentrega = clsParser.DateTimeParse(row["dataentrega"].ToString());
            info.qtdeentrega = clsParser.DecimalParse(row["qtdeentrega"].ToString());
        }

        private void SolicitacaoEntregaCampos(clsSolicitacaoentregaInfo info)
        {
            solicitacaoentrega_id = info.id;
            solicitacaoentrega_idSolicitacao = info.idsolicitacao;
            SolicitacaoEntrega_tbxDataEntrega.Text = info.dataentrega.ToString("dd/MM/yyyy");
            SolicitacaoEntrega_tbxQtdeentrega.Text = info.qtdeentrega.ToString();
        }

        private void SolicitacaoEntregaFillInfo(clsSolicitacaoentregaInfo info)
        {
            info.id = solicitacaoentrega_id;
            info.idsolicitacao = solicitacaoentrega_idSolicitacao;
            info.dataentrega = DateTime.Parse(SolicitacaoEntrega_tbxDataEntrega.Text);
            info.qtdeentrega = clsParser.DecimalParse(SolicitacaoEntrega_tbxQtdeentrega.Text);

        }

        void SolicitacaoEntregaFillInfoToGrid(clsSolicitacaoentregaInfo info)
        {
            DataRow row;
            DataRow[] rows = dtSolicitacaoEntrega.Select("solicitacaoentrega_posicao = " + solicitacaoentrega_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtSolicitacaoEntrega.NewRow();
            }

            row["id"] = info.id;
            row["idsolicitacao"] = info.idsolicitacao;
            row["dataentrega"] = info.dataentrega;
            row["qtdeentrega"] = info.qtdeentrega;

            // Colunas que petencem a outras tabelas
            //            row["codigo"] = Orcamento1_tbxCodigo.Text;

            if (rows.Length == 0)
            {
                row["solicitacaoentrega_posicao"] = solicitacaoentrega_posicao;
                dtSolicitacaoEntrega.Rows.Add(row);
            }

        }

        private Boolean SolicitacaoEntrega_HouveMudanca()
        {
            clsSolicitacaoEntregaInfo = new clsSolicitacaoentregaInfo();
            clsSolicitacaoEntregaBLL = new clsSolicitacaoentregaBLL();
            SolicitacaoEntregaFillInfo(clsSolicitacaoEntregaInfo);


            if (clsSolicitacaoEntregaBLL.Equals(clsSolicitacaoEntregaInfo,
                                                    clsSolicitacaoEntregaInfoOld) == false)
            {
                return true;
            }

            return false;
        }


        private void btnCalculaProgramacao_Click(object sender, EventArgs e)
        {
            try
            {
                clsProgramacaoEntrega clsProgramacaoEntrega = new clsProgramacaoEntrega();

                clsProgramacaoEntrega.Periodo periodo;

                if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "A")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Anual;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString() == "Semestral")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semestre;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "M")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Mes;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "Q")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Quinzena;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString() == "Semanal")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semana;
                }
                else
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semana;
                }


                DataTable dtTemp = clsProgramacaoEntrega.GerarProgramacao(clsParser.DecimalParse(tbxQtdeSol.Text),
                                                        0,
                                                        clsParser.Int32Parse(tbxQtdeEntregas.Text),
                                                        periodo,
                                                        clsParser.DateTimeParse(tbxDiaFixo.Text),
                                                        idUnid,
                                                        ckxPulaSabDom.Checked);

                foreach (DataRow row in dtSolicitacaoEntrega.Rows)
                {
                    if (row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Deleted)
                    {
                        row.Delete();
                    }
                }

                foreach (DataRow row in dtTemp.Rows)
                {
                    DataRow rowEntrega = dtSolicitacaoEntrega.NewRow();

                    rowEntrega["solicitacaoentrega_posicao"] = dtSolicitacaoEntrega.Rows.Count + 1;
                    rowEntrega["qtdeentrega"] = row["qtde"];
                    rowEntrega["dataentrega"] = row["entrega"];

                    dtSolicitacaoEntrega.Rows.Add(rowEntrega);
                }

                SolicitacaoEntregaSoma();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName);
            }
        }

        private void TclResolve()
        {
            tclSolicitacaoEntrega.SelectedIndex = painel;
        }

        //############################# Solicitacaofornecedor #########################################

        void SolicitacaoFornecedorCarregar()
        {
            clsSolicitacaoFornecedorInfo = new clsSolicitacaoFornecedorInfo();
            clsSolicitacaoFornecedorInfoOld = new clsSolicitacaoFornecedorInfo();

            if (solicitacaofornecedor_posicao == 0)
            {
                solicitacaofornecedor_posicao = dtSolicitacaoFornecedor.Rows.Count + 1;

                DataRow[] rows = dtSolicitacaoFornecedor.Select();

                Int32 contador = 1;
                foreach (DataRow row in rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached)
                    {

                        contador++;
                    }
                }
                clsSolicitacaoFornecedorInfo.id = 0;
                clsSolicitacaoFornecedorInfo.idfornecedor = clsInfo.zempresaclienteid;
                clsSolicitacaoFornecedorInfo.idsolicitacao = 0;
                clsSolicitacaoFornecedorInfo.preferencia = "";
            }
            else
            {
                SolicitacaoFornecedorGridToInfo(clsSolicitacaoFornecedorInfo, solicitacaofornecedor_posicao);
            }

            SolicitacaoFornecedorCampos(clsSolicitacaoFornecedorInfo);
            SolicitacaoFornecedorFillInfo(clsSolicitacaoFornecedorInfoOld);
            tbxFornecedoresCredenciados.Select();
        }

        private void SolicitacaoFornecedorGridToInfo(clsSolicitacaoFornecedorInfo info, Int32 solicitacaofornecedor2_posicao)
        {
            DataRow row = dtSolicitacaoFornecedor.Select("solicitacaofornecedor_posicao = " + solicitacaofornecedor2_posicao)[0];
            info.id = Int32.Parse(row["id"].ToString());
            info.idsolicitacao = Int32.Parse(row["idsolicitacao"].ToString());
            info.idfornecedor = Int32.Parse(row["idfornecedor"].ToString());
            info.preferencia = row["preferencia"].ToString();
        }

        private void SolicitacaoFornecedorCampos(clsSolicitacaoFornecedorInfo info)
        {
            solicitacaoFornecedor_id = info.id;
            solicitacaoFornecedor_idSolicitacao = info.idsolicitacao;
            solicitacaoFornecedor_idFornecedor = info.idfornecedor;
            tbxFornecedoresCredenciados.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from clienteprospeccao where id = " + info.idfornecedor, "");
            TbxFornededorNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from clienteprospeccao where id = " + info.idfornecedor, "");
            solicitacaoFornecedor_preferencia = info.preferencia;

        }

        private void SolicitacaoFornecedorFillInfo(clsSolicitacaoFornecedorInfo info)
        {
            info.id = solicitacaoFornecedor_id;
            info.idsolicitacao = solicitacaoFornecedor_idSolicitacao;
            info.idfornecedor = solicitacaoFornecedor_idFornecedor;
            info.preferencia = solicitacaoFornecedor_preferencia;
        }

        private void SolicitacaoFornecedorFillInfoToGrid(clsSolicitacaoFornecedorInfo info)
        {
            DataRow row;
            DataRow[] rows = dtSolicitacaoFornecedor.Select("solicitacaofornecedor_posicao = " + solicitacaofornecedor_posicao);
            tsbSolicitacaoFornecedores_Incluir.Enabled = true;

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtSolicitacaoFornecedor.NewRow();
            }

            row["id"] = info.id;
            row["idsolicitacao"] = info.idsolicitacao;
            row["idfornecedor"] = info.idfornecedor;
            row["fornecedor"] = tbxFornecedoresCredenciados.Text;
            row["preferencia"] = info.preferencia;


            if (rows.Length == 0)
            {
                row["solicitacaofornecedor_posicao"] = solicitacaofornecedor_posicao;
                dtSolicitacaoFornecedor.Rows.Add(row);
            }
            verificaNumForne(dtSolicitacaoFornecedor.Rows.Count);
        }

        private void verificaNumForne(Int32 funcionarios)
        {
            if (funcionarios > 4)
            {
                tsbSolicitacaoFornecedores_Incluir.Enabled = false;
            }
            else
            {
                tsbSolicitacaoFornecedores_Incluir.Enabled = true;
            }
        }

        private Boolean SolicitacaoFornecedor_HouveMudanca()
        {
            clsSolicitacaoFornecedorInfo = new clsSolicitacaoFornecedorInfo();
            clsSolicitacaoFornecedorBLL = new clsSolicitacaoFornecedorBLL();
            SolicitacaoFornecedorFillInfo(clsSolicitacaoFornecedorInfo);

            if (clsSolicitacaoFornecedorBLL.Equals(clsSolicitacaoFornecedorInfo, clsSolicitacaoFornecedorInfoOld) == false)
            {
                return true;
            }

            return false;
        }

        private void tsbSolicitacaoFornecedorItem_Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (SolicitacaoFornecedor_HouveMudanca() == true)
                {
                    DialogResult drt;
                    drt = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (drt == DialogResult.Yes)
                    {
                        SolicitacaoFornecedorFillInfoToGrid(clsSolicitacaoFornecedorInfo);
                        painel = 2;
                        TclResolve();
                    }
                    else if (drt == DialogResult.No)
                    {
                        painel = 2;
                        TclResolve();
                    }
                    else if (drt == DialogResult.Cancel)
                    {
                        painel = 2;
                        TclResolve();
                        return;
                    }
                }
                else
                {
                    painel = 2;
                    TclResolve();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsbSolicitacaoFornecedorItem_retornar_Click(object sender, EventArgs e)
        {
            painel = 2;
            TclResolve();
        }

        private void btnFornecedore_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFornecedores.Name;
            //frmClientePes frmClientePes = new frmClientePes(); // 
            //frmClientePes.Init(0);
            //clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void tsbSolicitacaoFornecedores_Incluir_Click(object sender, EventArgs e)
        {
            painel = 3;
            TclResolve();
            try
            {
                solicitacaofornecedor_posicao = 0;
                SolicitacaoFornecedorCarregar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbSolicitacaoFornecedores_Excluir_Click(object sender, EventArgs e)
        {
            if (dgvFornecedores.CurrentRow != null)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja realmente excluir o item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    dtSolicitacaoFornecedor.Select("solicitacaofornecedor_posicao =" + dgvFornecedores.CurrentRow.Cells["solicitacaofornecedor_posicao"].Value.ToString())[0].Delete();

                }
            }
            verificaNumForne(dgvFornecedores.Rows.Count);
        }

        private void tsbSolicitacaoFornecedores_Alterar_Click(object sender, EventArgs e)
        {
            if (dgvFornecedores.CurrentRow != null)
            {
                painel = 3;
                TclResolve();
                try
                {
                    solicitacaofornecedor_posicao = Int32.Parse(dgvFornecedores.CurrentRow.Cells["solicitacaofornecedor_posicao"].Value.ToString());
                    SolicitacaoFornecedorCarregar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Escolha um Registro!", "Aplisoft");
            }
        }

        private void dgvFornecedores_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbSolicitacaoFornecedores_Alterar.PerformClick();
        }

        //Fim




        private void tclSolicitacaoEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (painel == 3 || painel == 1)
            {
                TclResolve();
            }
            else
            {
                if (tclSolicitacaoEntrega.SelectedIndex == 3)
                {
                    painel = 2;
                    TclResolve();
                }
                else if (tclSolicitacaoEntrega.SelectedIndex == 1)
                {
                    painel = 0;
                    TclResolve();
                }
                else
                {
                    painel = tclSolicitacaoEntrega.SelectedIndex;
                    TclResolve();
                }
            }
        }

        private void tsbSolicitacaoEntrega_Incluir_Click(object sender, EventArgs e)
        {
            painel = 1;
            TclResolve();
            try
            {
                solicitacaoentrega_posicao = 0;

                SolicitacaoEntregaCarregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbSolicitacaoEntrega_Alterar_Click(object sender, EventArgs e)
        {
            if (dgvSolicitacaoEntrega.CurrentRow != null)
            {
                painel = 1;
                TclResolve();
                try
                {
                    solicitacaoentrega_posicao = Int32.Parse(dgvSolicitacaoEntrega.CurrentRow.Cells["solicitacaoentrega_posicao"].Value.ToString());
                    SolicitacaoEntregaCarregar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Escolha um Registro!", "Aplisoft");
            }
        }

        private void tsbSolicitacaoEntrega_Excluir_Click(object sender, EventArgs e)
        {
            if (dgvSolicitacaoEntrega.CurrentRow != null)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja realmente excluir o item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    dtSolicitacaoEntrega.Select("solicitacaoentrega_posicao =" + dgvSolicitacaoEntrega.CurrentRow.Cells["solicitacaoentrega_posicao"].Value.ToString())[0].Delete();
                    SolicitacaoEntregaSomar();
                    VerificarMudancaProgEntrega();

                }

            }
        }

        private void tsbSolicitacaoRegistroEntregao_Retornar_Click(object sender, EventArgs e)
        {
            painel = 0;
            TclResolve();
        }

        private void tsbSolicitacaoRegistroEntregao_Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (SolicitacaoEntrega_HouveMudanca() == true)
                {
                    DialogResult drt;
                    drt = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (drt == DialogResult.Yes)
                    {


                        clsSolicitacaoEntregaInfo = new clsSolicitacaoentregaInfo();
                        SolicitacaoEntregaFillInfo(clsSolicitacaoEntregaInfo);
                        SolicitacaoEntregaFillInfoToGrid(clsSolicitacaoEntregaInfo);
                        SolicitacaoEntregaSomar();
                        if (clsParser.DecimalParse(tbxQtdeSol.Text) < (clsParser.DecimalParse(tbxQtdeProgramada.Text)))
                        {
                            painel = 1;
                            throw new Exception("A soma do total programado ultrapassou a Qtde Solicitada.");
                        }
                        painel = 0;
                        TclResolve();
                    }
                    else if (drt == DialogResult.No)
                    {
                        painel = 0;
                        TclResolve();
                    }
                    else if (drt == DialogResult.Cancel)
                    {
                        painel = 0;
                        TclResolve();
                        return;
                    }



                    VerificarMudancaProgEntrega();
                }
                else
                {
                    painel = 0;
                    TclResolve();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbnPrioridadeA_Click(object sender, EventArgs e)
        {
            tbxDiaFixo.Text = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy");
        }

        private void rbnPrioridadeM_Click(object sender, EventArgs e)
        {
            tbxDiaFixo.Text = DateTime.Now.AddDays(15).ToString("dd/MM/yyyy");
        }

        private void rbnPrioridadeB_Click(object sender, EventArgs e)
        {
            tbxDiaFixo.Text = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
        }

        private void SolicitacaoEntregaSomar()
        {
            //
            tbxQtdeProgramada.Text = "0";
            foreach (DataGridViewRow row in dgvSolicitacaoEntrega.Rows)
            {
                // Somar os campos do cabeçalho
                tbxQtdeProgramada.Text = (clsParser.DecimalParse(tbxQtdeProgramada.Text) + clsParser.DecimalParse(row.Cells["qtdeentrega"].Value.ToString())).ToString();



            }
        }
        private void VerificarMudancaProgEntrega()
        {

            if (clsParser.DecimalParse(tbxQtdeProgramada.Text) > 0)
            {
                gbxPrePlanejamento.Visible = false;

            }
            else
            {
                gbxPrePlanejamento.Visible = true;
            }
            if (clsParser.DecimalParse(tbxQtdeSol.Text) != 0)
            {
                if (clsParser.DecimalParse(tbxQtdeProgramada.Text) < clsParser.DecimalParse(tbxQtdeSol.Text))
                {
                    tsbSolicitacaoEntrega_Incluir.Enabled = true;
                }
                else
                {
                    tsbSolicitacaoEntrega_Incluir.Enabled = false;
                }
            }
            else
            {
                tsbSolicitacaoEntrega_Incluir.Enabled = true;
            }

        }




        //private void tsbSolicitacaoFornecedorItem_Salvar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (solicit == true)
        //        {
        //            DialogResult drt;
        //            drt = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        //            if (drt == DialogResult.Yes)
        //            {
        //                PecasForneFillInfoToGrid(clsPecasForneInfo);
        //                painelpecas = 0;
        //                tclPecasResolve();
        //            }
        //            else if (drt == DialogResult.No)
        //            {
        //                painelpecas = 0;
        //                tclPecasResolve();
        //            }
        //            else if (drt == DialogResult.Cancel)
        //            {
        //                painelpecas = 2;
        //                tclPecasResolve();
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            painelpecas = 0;
        //            tclPecasResolve();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}




    }
}






























//        clsProcedures clsProcedures = new clsProcedures();
//        
//        clsGrid clsGrid = new clsGrid();

//        ParameterFields pfields = new ParameterFields();

//        clsSolicitacaoBLL clsSolicitacaoBLL;
//        clsSolicitacaoInfo clsSolicitacaoInfo;
//        clsSolicitacaoInfo clsSolicitacaoInfoOld;

//        Int32 id;
//        Int32 idarea;
//        Int32 idautorizantealmox;
//        Int32 idautorizanteger;
//        Int32 idautorizantesol;
//        Int32 idcentrocusto;
//        Int32 idcodigo;
//        Int32 idcodigoctabil;
//        Int32 idcotacao;
//        Int32 idcotacaoitem;
//        Int32 iddepto;
//        Int32 iddestino;
//        Int32 idhistorico;
//        Int32 idos;
//        Int32 idpedidocompra;
//        Int32 idpedidocompraitem;
//        Int32 idsolicitante;
//        Int32 idunid;

//        DataGridViewRowCollection rows;
//        // Programação de Entrega
//        Int32 solicitacaoentrega_id;
//        Int32 solicitacaoentrega_posicao;

//        clsSolicitacaoentregaBLL clsSolicitacaoentregaBLL;
//        clsSolicitacaoentregaInfo clsSolicitacaoentregaInfo;
//        clsSolicitacaoentregaInfo clsSolicitacaoentregaInfoOld;
//        DataTable dtSolicitacaoEntrega;
//        BackgroundWorker bwrSolicitacaoEntrega;


//        // Fornecedores
//        Int32 solicitacaofornecedor_id;
//        Int32 solicitacaofornecedor_posicao;
//        clsSolicitacaoFornecedorBLL clsSolicitacaoFornecedorBLL;
//        clsSolicitacaoFornecedorInfo clsSolicitacaoFornecedorInfo;
//        DataTable dtSolicitacaoFornecedor;
//        BackgroundWorker bwrSolicitacaoFornecedor;

//        // cadastro de pecas .. fornecedor
//        DataTable dtPecasForne;
//        // cadastro notas fiscais
//        DataTable dtNFCompra1;

//        Int32 _solicitacaoentrega_guia;
//        private Int32 solicitacaoentrega_guia
//        {
//            get { return _solicitacaoentrega_guia; }
//            set
//            {
//                if (value == 0 || value == 1)
//                {
//                    _solicitacaoentrega_guia = value;

//                    tclSolicitacaoEntrega.SelectedIndex = _solicitacaoentrega_guia;
//                }
//            }
//        }

//        public frmSolicitacao()
//        {
//            InitializeComponent();
//        }

//        public void Init(Int32 id, DataGridViewRowCollection rows)
//        {
//            this.id = id;
//            this.rows = rows;

//            clsSolicitacaoBLL = new clsSolicitacaoBLL();

//            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "SELECT CODIGO FROM PECAS WHERE ATIVO='S' ORDER BY CODIGO", tbxCodigo);
//            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "SELECT CODIGO FROM PECAS WHERE ATIVO='S' ORDER BY CODIGO", tbxCodigoDestino);
//            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS ORDER BY CODIGO", tbxHistorico);
//            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM CENTROCUSTOS ORDER BY CODIGO", tbxCentroCusto);
//            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "SELECT CONVERT(VARCHAR, NUMERO) + ' - ' + CONVERT(VARCHAR, YEAR(DATA)) FROM ORDEMSERVICO ORDER BY NUMERO, YEAR(DATA)", tbxOrdemServico);

//            clsSolicitacaoentregaBLL = new clsSolicitacaoentregaBLL();
//            clsSolicitacaoFornecedorBLL = new clsSolicitacaoFornecedorBLL();
//        }

//        private void frmSolicitacao_Load(object sender, EventArgs e)
//        {
//            Pecastipo_Carrega();

//SolicitacaoCarregar();

//            tbxQtdeEntregas.Text = "1";
//            tbxDiaFixo.Text = DateTime.Now.ToString("dd/MM/yyyy");
//        }

//        private void frmSolicitacao_Activated(object sender, EventArgs e)
//        {
//            TrataCampos((Control)sender);
//        }

//        private void tspSalvar_Click(object sender, EventArgs e)
//        {
//            SolicitacaoEntregaSoma();

//            if (clsParser.DecimalParse(tbxQtdeProgramada.Text) != clsParser.DecimalParse(tbxQtdeTotal.Text))
//            {
//                MessageBox.Show("Acerte a Qtde da Programação de Entrega dos Itens " + Environment.NewLine +
//                                "" + Environment.NewLine +
//                                "");
//                if (gbxPrePlanejamento.Enabled == true)
//                {
//                    tbxQtdeEntregas.Select();
//                }
//            }
//            else
//            {
//                if (Salvar() == DialogResult.Cancel)
//                {
//                    return;
//                }
//                this.Close();
//            }
//        }

//        private void tspExcluir_Click(object sender, EventArgs e)
//        {

//        }

//        private void tspPrimeiro_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (HouveModificacoes() == true)
//                {
//                    if (Salvar() == DialogResult.Cancel)
//                    {
//                        return;
//                    }
//                }
//                if (rows != null)
//                {
//                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
//                    SolicitacaoCarregar();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }

//        private void tspAnterior_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (HouveModificacoes() == true)
//                {
//                    if (Salvar() == DialogResult.Cancel)
//                    {
//                        return;
//                    }
//                }
//                if (rows != null)
//                {
//                    foreach (DataGridViewRow row in rows)
//                    {
//                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
//                        {
//                            if (row.Index != 0)
//                            {
//                                id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
//                                SolicitacaoCarregar();
//                                break;
//                            }
//                            else
//                            {
//                                break;
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            } 

//        }

//        private void tspProximo_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (HouveModificacoes() == true)
//                {
//                    if (Salvar() == DialogResult.Cancel)
//                    {
//                        return;
//                    }
//                }
//                if (rows != null)
//                {
//                    foreach (DataGridViewRow row in rows)
//                    {
//                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
//                        {
//                            if (row.Index < rows.Count - 1)
//                            {
//                                id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
//                                SolicitacaoCarregar();
//                                break;
//                            }
//                            else
//                            {
//                                break;
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            } 


//        }

//        private void tspUltimo_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (HouveModificacoes() == true)
//                {
//                    if (Salvar() == DialogResult.Cancel)
//                    {
//                        return;
//                    }
//                }
//                if (rows != null)
//                {
//                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
//                    SolicitacaoCarregar();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }


//        }

//        private void tspRetornar_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void Pecastipo_Carrega()
//        {
//            lbxTipoProduto.Items.Clear();
//            String query;
//            SqlDataAdapter sda;
//            query = "select ID, CODIGO + ' - ' + NOME AS [CODIGONOME] FROM PECASTIPO ORDER BY CODIGO";

//            DataTable dtTemp = new DataTable();
//            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
//            sda.Fill(dtTemp);
//            foreach (DataRow row in dtTemp.Rows)
//            {
//                lbxTipoProduto.Items.Add(row["CODIGONOME"].ToString());
//            }
//        }
//        private void SolicitacaoCarregar()
//        {
//            clsSolicitacaoInfoOld = new clsSolicitacaoInfo();

//            if (id == 0)
//            {
//                clsSolicitacaoInfo = new clsSolicitacaoInfo();
//                clsSolicitacaoInfo.ano = DateTime.Now.Year;
//                clsSolicitacaoInfo.aprovadofab = "S";
//                clsSolicitacaoInfo.aprovadoger = "S";
//                clsSolicitacaoInfo.aprovadosol = "S";
//                clsSolicitacaoInfo.area = "ADM";
//                clsSolicitacaoInfo.complemento = "";
//                clsSolicitacaoInfo.complemento1 = "";
//                clsSolicitacaoInfo.consumo = "S";
//                clsSolicitacaoInfo.data = DateTime.Now;
//                //clsSolicitacaoInfo.descricaoesp = "";
//                clsSolicitacaoInfo.emitente = clsInfo.zusuario;
//                clsSolicitacaoInfo.filial = clsInfo.zfilial;
//                clsSolicitacaoInfo.fornecedorganhou = clsInfo.zempresaclienteid;
//                clsSolicitacaoInfo.id = 0;
//                clsSolicitacaoInfo.idarea = 0;
//                clsSolicitacaoInfo.idautorizantealmox = clsInfo.zusuarioid;
//                clsSolicitacaoInfo.idautorizanteger = clsInfo.zusuarioid;
//                clsSolicitacaoInfo.idautorizantesol = clsInfo.zusuarioid;
//                clsSolicitacaoInfo.idcentrocusto = clsInfo.zcentrocustos;
//                clsSolicitacaoInfo.idcodigo = clsInfo.zpecas;
//                clsSolicitacaoInfo.idcodigoctabil = clsInfo.zcontacontabil;
//                clsSolicitacaoInfo.idcotacao = clsInfo.zcotacao;
//                clsSolicitacaoInfo.idcotacaoitem = clsInfo.zcotacao1;
//                clsSolicitacaoInfo.iddepto = 0;
//                clsSolicitacaoInfo.iddestino = clsInfo.zpecas;
//                clsSolicitacaoInfo.idhistorico = clsInfo.zhistoricos;
//                clsSolicitacaoInfo.idos = clsInfo.zordemservico;
//                clsSolicitacaoInfo.idpedidocompra = clsInfo.zcompras;
//                clsSolicitacaoInfo.idpedidocompraitem = clsInfo.zcompras1;
//                clsSolicitacaoInfo.idsolicitante = 0;
//                clsSolicitacaoInfo.idunid = clsInfo.zunidade;
//                clsSolicitacaoInfo.motivojustificado = "";
//                clsSolicitacaoInfo.motivoreprovado = "";
//                //clsSolicitacaoInfo.msg = "";
//                clsSolicitacaoInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT MAX(NUMERO + 1) FROM SOLICITACAO where  YEAR(DATA) = YEAR(GETDATE()) AND FILIAL =" + clsInfo.zfilial + ""));
//                if (clsSolicitacaoInfo.numero == 0)
//                {
//                    clsSolicitacaoInfo.numero = 1;
//                }
//                clsSolicitacaoInfo.prioridade = "B";
//                clsSolicitacaoInfo.qtde = 0;
//                clsSolicitacaoInfo.qtdesol = 0;
//                clsSolicitacaoInfo.qtdetotal = 0;
//                clsSolicitacaoInfo.solicitante = clsInfo.zusuario;
//                clsSolicitacaoInfo.tipodestino = "";
//                clsSolicitacaoInfo.tipoentrada = "00";
//                //clsSolicitacaoInfo.tipopedido = "";
//                clsSolicitacaoInfo.tipoproduto = "M";
//            }
//            else
//            {
//                clsSolicitacaoInfo = clsSolicitacaoBLL.Carregar(id, clsInfo.conexaosqldados);
//            }
//            SolicitacaoCampos(clsSolicitacaoInfo);
//            SolicitacaoFillInfo(clsSolicitacaoInfoOld);

//            // carregando a programação de entrega
//            dtSolicitacaoEntrega = clsSolicitacaoentregaBLL.GridDados(clsSolicitacaoInfo.id, clsInfo.conexaosqldados);

//            DataColumn dcPosicao = new DataColumn("solicitacaoentrega_posicao", Type.GetType("System.Int32"));
//            dtSolicitacaoEntrega.Columns.Add(dcPosicao);

//            for (Int32 i = 1; i <= dtSolicitacaoEntrega.Rows.Count; i++)
//            {
//                dtSolicitacaoEntrega.Rows[i - 1]["solicitacaoentrega_posicao"] = i;
//            }
//            dtSolicitacaoEntrega.AcceptChanges();
//            clsSolicitacaoentregaBLL.GridMonta(dgvSolicitacaoEntrega, dtSolicitacaoEntrega, solicitacaoentrega_posicao);
//            SolicitacaoEntregaSoma();

//            // posicionar no mensal da programação de entrega
//            lbxTipoCalculo.SelectedIndex = lbxTipoCalculo.FindString("Mensal");
//            if (lbxTipoCalculo.SelectedIndex == -1)
//            {
//                lbxTipoCalculo.SelectedIndex = 0;
//            }

//            bwrSolicitacaoFornecedor_Run();

//            if (id > 0)
//            {
//                if (tbxCodigo.Text != "0")
//                {
//                    // carregando as notas fiscais de entrada relaciona com este produto
//                    dtNFCompra1 = clsNfCompra1BLL.GridDadosPeca(clsSolicitacaoInfo.idcodigo, 20, clsInfo.conexaosqldados);
//                    clsNfCompra1BLL.GridMontaPeca(dgvNFCompra, dtNFCompra1, 0);
//                }
//                else
//                {
//                    if (dtNFCompra1 != null)
//                    {
//                        dtNFCompra1.Clear();
//                    }
//                }
//            }

//            Boolean podealterar = true;

//            if (idpedidocompra != 0 &&
//                idpedidocompra != clsInfo.zcompras)
//            {
//                podealterar = false;
//            }
//            else if (idcotacao != 0 &&
//                     idcotacao != clsInfo.zcotacao)
//            {
//                podealterar = false;
//            }

//            if (podealterar == false)
//            {
//                tspSalvar.Enabled = false;
//            }
//            else
//            {
//                tspSalvar.Enabled = true;
//            }
//        }

//        private void SolicitacaoCampos(clsSolicitacaoInfo Info)
//        {
//            id = Info.id;
//            tbxAno.Text = Info.ano.ToString();
//            cbxAprovadoFab.Text = Info.aprovadofab;
//            cbxAprovadoGer.Text = Info.aprovadoger;
//            cbxAprovadoSol.Text = Info.aprovadosol;
//            tbxArea.Text = Info.area;
//            tbxComplemento.Text = Info.complemento;
//            tbxComplemento1.Text = Info.complemento1;
//            if (Info.consumo == "S")
//            {
//                rbnConsumo.Checked = true;
//            }
//            else
//            {
//                rbnRevenda.Checked = true;
//            }
//            //Info.descricaoesp = "";
//            tbxData.Text = Info.data.ToString("dd/MM/yyyy HH:mm");
//            tbxEmitente.Text = Info.emitente;
//            tbxFilial.Text = Info.filial.ToString();
//            //Info.fornecedorganhou;
//            //Info.idarea = 0;
//            idautorizantealmox = Info.idautorizantealmox;
//            tbxAutorizanteAlmox.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO WHERE ID= " + idautorizantealmox + " ");
//            idautorizanteger = Info.idautorizanteger;
//            tbxAutorizanteGer.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO WHERE ID= " + idautorizanteger + " ");
//            idautorizantesol = Info.idautorizantesol;
//            tbxAutorizanteSol.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO WHERE ID= " + idautorizantesol + " ");
//            idcentrocusto = Info.idcentrocusto;
//            tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS WHERE ID= " + idcentrocusto + " ");
//            idcodigo = Info.idcodigo;
//            tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS WHERE ID= " + idcodigo + " ");
//            tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE ID= " + idcodigo + " ");
//            //Info.idcodigoctabil
//            idcotacao = Info.idcotacao;
//            tbxCotacao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from COTACAO WHERE ID= " + idcotacao + " ");
//            if (clsParser.Int32Parse(tbxCotacao.Text) > 0)
//            {
//                tbxDataCotacao.Text = DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DATAMONTAGEM from COTACAO WHERE ID= " + idcotacao + " ")).ToString("dd/MM/yyyy");
//            }
//            idcotacaoitem = Info.idcotacaoitem;
//            //Info.iddepto = 0;
//            iddestino = Info.iddestino;
//            tbxCodigoDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS WHERE ID= " + iddestino + " ");
//            tbxNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE ID= " + iddestino + " ");
//            idhistorico = Info.idhistorico;
//            tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS WHERE ID= " + idhistorico + " ");
//            idos = Info.idos;
//            tbxOrdemServico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from ORDEMSERVICO WHERE ID= " + idos) + " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select year(data) from ORDEMSERVICO WHERE ID= " + idos);

//            idpedidocompra = Info.idpedidocompra;
//            tbxPedidoCompra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from COMPRAS WHERE ID= " + idpedidocompra + " ");
//            if (clsParser.Int32Parse(tbxPedidoCompra.Text) > 0)
//            {
//                tbxDataPedido.Text =DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DATA from COMPRAS WHERE ID= " + idpedidocompra + " ")).ToString("dd/MM/yyyy");
//            }
//            idpedidocompraitem = Info.idpedidocompraitem;
//            //Info.idsolicitante = 0;
//            tbxSolicitante.Text = Info.solicitante;
//            idunid = Info.idunid;
//            tbxUnidSol.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE WHERE ID= " + idunid + " ");
//            tbxMotivoJustificado.Text = Info.motivojustificado;
//            tbxMotivoReprovado.Text = Info.motivoreprovado;
//            //Info.msg = "";
//            tbxNumero.Text = Info.numero.ToString();
//            if (Info.prioridade == "A")
//            {
//                rbnPrioridadeA.Checked = true;
//            }
//            else if (Info.prioridade == "B")
//            {
//                rbnPrioridadeB.Checked = true;
//            }
//            else
//            {
//                rbnPrioridadeM.Checked = true;
//            }

//            tbxQtde.Text = Info.qtde.ToString("N3");
//            tbxQtdeSol.Text = Info.qtdesol.ToString("N3");
//            tbxQtdeTotal.Text = Info.qtdetotal.ToString("N3");
//            if (Info.tipodestino == "M")
//            {
//                rbnTipoDestinoMaq.Checked = true;
//            }
//            else
//            {
//                rbnTipoDestinoItem.Checked = true;
//            }
//            lbxTipoEntrada.SelectedIndex = lbxTipoEntrada.FindString(Info.tipoentrada);
//            if (lbxTipoEntrada.SelectedIndex == -1)
//            {
//                lbxTipoEntrada.SelectedIndex = 0;
//            }

//            //Info.tipopedido = "";

//            lbxTipoProduto.SelectedIndex = lbxTipoProduto.FindString(Info.tipoproduto);
//            if (lbxTipoProduto.SelectedIndex == -1)
//            {
//                lbxTipoProduto.SelectedIndex = 0;
//            }
//        }
//        private void SolicitacaoFillInfo(clsSolicitacaoInfo info)
//        {
//            info.id = id;
//            info.ano = clsParser.Int32Parse(tbxAno.Text);
//            info.area = tbxArea.Text;
//            info.aprovadofab = cbxAprovadoFab.Text;
//            info.aprovadoger = cbxAprovadoGer.Text;
//            info.aprovadosol = cbxAprovadoSol.Text;
//            info.complemento = tbxComplemento.Text;
//            info.complemento1 = tbxComplemento1.Text;
//            if (rbnConsumo.Checked == true)
//            {
//                info.consumo = "S"; 
//            }
//            else
//            {
//                info.consumo = "N"; 
//            }
//            info.data = DateTime.Parse(tbxData.Text);
//            //Info.descricaoesp = "";
//            info.emitente = tbxEmitente.Text;
//            info.filial = clsParser.Int32Parse(tbxFilial.Text);
//            //Info.fornecedorganhou;
//            //clsSolicitacaoinfo.idarea = 0;
//            info.idautorizantealmox = idautorizantealmox;
//            info.idautorizanteger = idautorizanteger;
//            info.idautorizantesol = idautorizantesol;
//            info.idcentrocusto = idcentrocusto;
//            info.idcodigo = idcodigo;
//            info.idcotacao = idcotacao;
//            info.idcotacaoitem = idcotacaoitem;
//            info.iddestino = iddestino;
//            info.idhistorico = idhistorico;
//            info.idos = idos;
//            info.idpedidocompra = idpedidocompra;
//            info.idpedidocompraitem = idpedidocompraitem;
//            info.idunid = idunid;
//            info.motivojustificado = tbxMotivoJustificado.Text;
//            info.motivoreprovado = tbxMotivoReprovado.Text;
//            //clsSolicitacaoinfo.msg = "";
//            info.numero = clsParser.Int32Parse(tbxNumero.Text);
//            if (rbnPrioridadeA.Checked == true)
//            {
//                info.prioridade = "A";
//            }
//            else if (rbnPrioridadeB.Checked == true)
//            {
//                info.prioridade = "B";
//            }
//            else
//            {
//                info.prioridade = "M";
//            }

//            info.qtde = clsParser.DecimalParse(tbxQtde.Text);
//            info.qtdesol = clsParser.DecimalParse(tbxQtdeSol.Text);
//            info.qtdetotal = clsParser.DecimalParse(tbxQtdeTotal.Text);
//            if (rbnTipoDestinoMaq.Checked == true)
//            {
//                info.tipodestino = "M";
//            }
//            else
//            {
//                info.tipodestino = "F";
//            }
//            info.tipoentrada = lbxTipoEntrada.Text.Substring(0, 2);
//            //clsSolicitacaoinfo.tipopedido = "";
//            info.solicitante = tbxSolicitante.Text;
//            info.tipoproduto = lbxTipoProduto.Text.Substring(0, 1);
//        }

//        private void TrataCampos(Control ctl)
//        {
//            if (clsInfo.znomegrid == btnIdCodigo.Name)
//            {
//                if (clsInfo.zrow != null)
//                {
//                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
//                    {
//                        idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
//                        tbxCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
//                        tbxNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
//                        idunid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + idcodigo + ""));
//                        tbxUnid.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + idunid + "");
//                        tbxUnidSol.Text = tbxUnid.Text;

//                        foreach (DataRow row in dtSolicitacaoFornecedor.Rows)
//                        {
//                            if (row.RowState != DataRowState.Deleted &&
//                                row.RowState != DataRowState.Detached)
//                            {
//                                row.Delete();
//                            }
//                        }
//                    }
//                }
//                tbxCodigo.Select();
//            }
//            if (ctl.Name == tbxCodigo.Name)
//            {
//                Int32 idcodigo_old = idcodigo;

//                idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxCodigo.Text + "'"));
//                if (idcodigo == 0)
//                {
//                    idcodigo = clsInfo.zpecas;
//                }
//                tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where ID=" + idcodigo + "");
//                tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where ID=" + idcodigo + "");
//                idunid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + idcodigo + ""));
//                tbxUnid.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + idunid + "");
//                tbxUnidSol.Text = tbxUnid.Text;
//                // capturar o ultimo preço
//                labelValor.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nfcompra.data, nfcompra1.idcodigo, nfcompra1.preco from nfcompra inner join nfcompra1 on nfcompra1.numero= nfcompra.id  where IDCODIGO=" + idcodigo + " order by nfcompra.data desc ")).ToString("N4");

//                // se mudou o codigo - carregar o fornecedor
//                if (idcodigo_old != idcodigo)
//                {
//                    // 1. verificar se já tem fornecedor cadastrado
//                    foreach (DataRow row in dtSolicitacaoFornecedor.Rows)
//                    {
//                        if (row.RowState != DataRowState.Deleted &&
//                            row.RowState != DataRowState.Detached)
//                        {
//                            row.Delete();
//                        }
//                    }

//                    // 2. carregar os novos fornecedores
//                    String query;
//                    SqlDataAdapter sda;
//                    query = "select " +
//                                "CLIENTEPROSPECCAO.COGNOME AS [FORNECEDOR], " +
//                                "PECASFORNE.* " +
//                            "FROM PECASFORNE " +
//                                "LEFT JOIN CLIENTEPROSPECCAO ON CLIENTEPROSPECCAO.ID=PECASFORNE.IDFORNECEDOR " +
//                            "WHERE " +
//                                "PECASFORNE.IDCODIGO = @IDCODIGO " +
//                            "ORDER BY CLIENTEPROSPECCAO.COGNOME";
//                    dtPecasForne = new DataTable();
//                    sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
//                    sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idcodigo;

//                    sda.Fill(dtPecasForne);

//                    foreach (DataRow row in dtPecasForne.Rows)
//                    {
//                        DataRow rowSolicitacaoFornecedor;

//                        rowSolicitacaoFornecedor = dtSolicitacaoFornecedor.NewRow();
//                        rowSolicitacaoFornecedor["idfornecedor"] = row["idfornecedor"];
//                        rowSolicitacaoFornecedor["solicitacaofornecedor_posicao"] = dtSolicitacaoFornecedor.Rows.Count + 1;
//                        rowSolicitacaoFornecedor["fornecedor"] = row["fornecedor"];
//                        rowSolicitacaoFornecedor["preferencia"] = "";

//                        dtSolicitacaoFornecedor.Rows.Add(rowSolicitacaoFornecedor);
//                    }
//                }

//                if (idcodigo_old != idcodigo)
//                {
//                    // carregando as notas fiscais de entrada relaciona com este produto
//                    dtNFCompra1 = clsNfCompra1BLL.GridDadosPeca(clsSolicitacaoInfo.idcodigo, 20, clsInfo.conexaosqldados);
//                    clsNfCompra1BLL.GridMontaPeca(dgvNFCompra, dtNFCompra1, 0);
//                }
//            }

//            if (clsInfo.znomegrid == btnIdHistorico.Name)
//            {
//                if (clsInfo.zrow != null)
//                {
//                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
//                    {
//                        idhistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
//                        tbxHistorico.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
//                    }
//                }

//                tbxHistorico.Select();
//                tbxHistorico.SelectAll();
//            }

//            if (ctl.Name == tbxHistorico.Name)
//            {
//                idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO='" + tbxHistorico.Text + "'"));
//                if (idhistorico == 0)
//                {
//                    idhistorico = clsInfo.zhistoricos;
//                }
//                tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID=" + idhistorico + "");
//            }

//            if (clsInfo.znomegrid == btnIdCentroCusto.Name)
//            {
//                if (clsInfo.zrow != null)
//                {
//                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
//                    {
//                        idcentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
//                        tbxCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
//                    }
//                }

//                tbxCentroCusto.Select();
//                tbxCentroCusto.SelectAll();
//            }

//            if (ctl.Name == tbxCentroCusto.Name)
//            {
//                idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCusto.Text + "'"));
//                if (idcentrocusto == 0)
//                {
//                    idcentrocusto = clsInfo.zcentrocustos;
//                }
//                tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID=" + idcentrocusto + "");
//            }

//            if (clsInfo.znomegrid == btnIdOs.Name)
//            {
//                if (clsInfo.zrow != null)
//                {
//                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
//                    {
//                        idos = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
//                        tbxOrdemServico.Text = clsInfo.zrow.Cells["NUMERO"].Value.ToString() + " - " + clsParser.DateTimeParse(clsInfo.zrow.Cells["data"].Value.ToString()).ToString("yyyy");
//                    }
//                }
//            }

//            if (ctl.Name == tbxOrdemServico.Name)
//            {
//                Int32 nOs;
//                Int32 yearOs;
//                if (tbxOrdemServico.Text.IndexOf(" - ") == -1)
//                {
//                    idos = clsInfo.zordemservico;
//                }
//                else
//                {
//                    nOs = clsParser.Int32Parse(tbxOrdemServico.Text.Substring(0, tbxOrdemServico.Text.IndexOf(" - ")));
//                    yearOs = clsParser.Int32Parse(tbxOrdemServico.Text.Split('-')[1].Trim());

//                    idos = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ordemservico where numero = " + nOs + " and year(data) = " + yearOs));

//                    if (idos == 0)
//                    {
//                        idos = clsInfo.zordemservico;
//                    }
//                }

//                tbxOrdemServico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select convert(varchar, numero) + ' - ' + convert(varchar, year(data)) from ordemservico where id=" + idos);
//            }

//            if (clsInfo.znomegrid == btnIdDestino.Name)
//            {
//                if (clsInfo.zrow != null)
//                {
//                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
//                    {
//                        iddestino = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
//                        tbxCodigoDestino.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
//                        tbxNomeDestino.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
//                    }
//                }
//                tbxCodigoDestino.Select();
//                tbxCodigoDestino.SelectAll();
//            }

//            if (ctl.Name == tbxCodigoDestino.Name)
//            {
//                iddestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxCodigoDestino.Text + "'"));
//                if (iddestino == 0)
//                {
//                    iddestino = clsInfo.zpecas;
//                }
//                tbxCodigoDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where ID=" + iddestino + "");
//                tbxNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where ID=" + iddestino + "");
//            }

//            // QTDES
//            Int32 qtdes_cd = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select UNIDDEC from UNIDADE WHERE ID= " + idunid + " "));
//            tbxQtdeSol.Text =  clsParser.DecimalParse(tbxQtdeSol.Text).ToString("N" + qtdes_cd.ToString());
//            tbxQtde.Text = clsParser.DecimalParse(tbxQtdeSol.Text).ToString("N" + qtdes_cd.ToString());
//            tbxQtdeTotal.Text = clsParser.DecimalParse(tbxQtdeSol.Text).ToString("N" + qtdes_cd.ToString());
//            if (ctl.Name == tbxQtdeSol.Name)
//            {
//                if (rbnPrioridadeA.Checked == true)
//                {
//                    rbnPrioridadeA.Select();
//                }
//                else if (rbnPrioridadeB.Checked == true)
//                {
//                    rbnPrioridadeB.Select();
//                }
//                else
//                {
//                    rbnPrioridadeM.Select();
//                }
//            }

//            tbxQtdeProgramada.Text = clsParser.DecimalParse(tbxQtdeProgramada.Text).ToString("N" + qtdes_cd.ToString());
//            clsInfo.zrow = null;
//            clsInfo.znomegrid = "";
//            if (ctl.Name == SolicitacaoEntrega_tbxQtdeentrega.Name)
//            {
//                tspSolicitcaoentregaSalvar.Select();
//            }
//        }

//        private Boolean HouveModificacoes()
//        {
//            clsSolicitacaoInfo = new clsSolicitacaoInfo();
//            SolicitacaoFillInfo(clsSolicitacaoInfo);
//            if (clsSolicitacaoBLL.Equals(clsSolicitacaoInfo, clsSolicitacaoInfoOld) == false)
//            {
//                return true;
//            }
//            return false;
//        }
//        private DialogResult Salvar()
//        {
//            DialogResult drt;
//            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
//            if (drt == DialogResult.Yes)
//            {
//                SolicitacaoSalvar();
//            }
//            return drt;

//        }
//        private void SolicitacaoSalvar()
//        {
//                using (TransactionScope tse = new TransactionScope())
//                {
//                    // ###############################
//                    // Tabela: SOLICITACAO
//                    clsSolicitacaoInfo = new clsSolicitacaoInfo();
//                    SolicitacaoFillInfo(clsSolicitacaoInfo);

//                    if (id == 0)
//                    {
//                        clsSolicitacaoInfo.id = clsSolicitacaoBLL.Incluir(clsSolicitacaoInfo, clsInfo.conexaosqldados);
//                    }
//                    else
//                    {
//                        clsSolicitacaoBLL.Alterar(clsSolicitacaoInfo, clsInfo.conexaosqldados);
//                    }

//                    // salvar a programação de entrega
//                    foreach (DataRow row in dtSolicitacaoEntrega.Rows)
//                    {
//                        if (row.RowState != DataRowState.Deleted &&
//                            row.RowState != DataRowState.Detached &&
//                            row.RowState != DataRowState.Unchanged)
//                        {
//                            row["idsolicitacao"] = clsSolicitacaoInfo.id;
//                        }
//                    }

//                    foreach (DataRow row in dtSolicitacaoFornecedor.Rows)
//                    {
//                        if (row.RowState != DataRowState.Deleted &&
//                            row.RowState != DataRowState.Detached &&
//                            row.RowState != DataRowState.Unchanged)
//                        {
//                            row["idsolicitacao"] = clsSolicitacaoInfo.id;
//                        }
//                    }

//                    // ###############################
//                    // Tabela: SOLICITACAOENTREGA
//                    foreach (DataRow row in dtSolicitacaoEntrega.Rows)
//                    {
//                        if (row.RowState == DataRowState.Detached ||
//                            row.RowState == DataRowState.Unchanged)
//                        {
//                            continue;
//                        }
//                        else if (row.RowState == DataRowState.Deleted)
//                        {
//                            clsSolicitacaoentregaBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
//                            continue;
//                        }
//                        else
//                        {
//                            clsSolicitacaoentregaInfo = new clsSolicitacaoentregaInfo();
//                            SolicitacaoEntregaGridToInfo(clsSolicitacaoentregaInfo, Int32.Parse(row["solicitacaoentrega_posicao"].ToString()));

//                            if (clsSolicitacaoentregaInfo.id == 0)
//                            {
//                                clsSolicitacaoentregaInfo.id = clsSolicitacaoentregaBLL.Incluir(clsSolicitacaoentregaInfo, clsInfo.conexaosqldados);
//                            }
//                            else
//                            {
//                                clsSolicitacaoentregaBLL.Alterar(clsSolicitacaoentregaInfo, clsInfo.conexaosqldados);
//                            }
//                        }
//                    }

//                    // ###############################
//                    // Tabela: SOLICITACAOFORNECEDOR
//                    foreach (DataRow row in dtSolicitacaoFornecedor.Rows)
//                    {
//                        if (row.RowState == DataRowState.Detached ||
//                            row.RowState == DataRowState.Unchanged)
//                        {
//                            continue;
//                        }
//                        else if (row.RowState == DataRowState.Deleted)
//                        {
//                            clsSolicitacaoFornecedorBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
//                            continue;
//                        }
//                        else
//                        {
//                            clsSolicitacaoFornecedorInfo = new clsSolicitacaoFornecedorInfo();
//                            SolicitacaoFornecedorGridToInfo(clsSolicitacaoFornecedorInfo, Int32.Parse(row["solicitacaofornecedor_posicao"].ToString()));

//                            if (clsSolicitacaoFornecedorInfo.id == 0)
//                            {
//                                clsSolicitacaoFornecedorInfo.id = clsSolicitacaoFornecedorBLL.Incluir(clsSolicitacaoFornecedorInfo, clsInfo.conexaosqldados);
//                            }
//                            else
//                            {
//                                clsSolicitacaoFornecedorBLL.Alterar(clsSolicitacaoFornecedorInfo, clsInfo.conexaosqldados);
//                            }
//                        }
//                    }

//                    tse.Complete();
//                }
//        }

//        private void SolicitacaoFornecedorGridToInfo(clsSolicitacaoFornecedorInfo clsSolicitacaoFornecedorInfo, int p)
//        {
//            DataRow row = dtSolicitacaoFornecedor.Select("solicitacaofornecedor = " + p)[0];

//            clsSolicitacaoFornecedorInfo.id = Int32.Parse(row["id"].ToString());
//            clsSolicitacaoFornecedorInfo.idfornecedor = Int32.Parse(row["idfornecedor"].ToString());
//            clsSolicitacaoFornecedorInfo.idsolicitacao = Int32.Parse(row["idsolicitacao"].ToString());
//            clsSolicitacaoFornecedorInfo.preferencia = row["preferencia"].ToString();
//        }

//        private void ControlEnter(object sender, EventArgs e)
//        {
//            clsVisual.ControlEnter(sender);
//        }
//        private void ControlLeave(object sender, EventArgs e)
//        {
//            clsVisual.ControlLeave(sender);
//            TrataCampos((Control)sender);
//        }
//        private void ControlKeyDown(object sender, KeyEventArgs e)
//        {
//            clsVisual.ControlKeyDown(sender, e);
//        }
//        private void ControlKeyDownData(object sender, KeyEventArgs e)
//        {
//            clsVisual.ControlKeyDownData((TextBox)sender, e);
//        }

//        private void btnIdCodigo_Click(object sender, EventArgs e)
//        {
//            clsInfo.znomegrid = btnIdCodigo.Name;
//            frmPecasPes frmPecasPes = new frmPecasPes(); // frmEscFormVisPes(clsInfo.conexaosqldados, "PECAS", idcodigo);
//            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

//        }

//        private void btnIdHistorico_Click(object sender, EventArgs e)
//        {
//            clsInfo.znomegrid = btnIdHistorico.Name;
//            frmHistoricosPes frmHistoricosPes = new frmHistoricosPes(); // frmEscFormVisPes(clsInfo.conexaosqlbanco, "HISTORICOS", idhistorico, "ATIVO", "S");
//            clsFormHelper.AbrirForm(this.MdiParent, frmHistoricosPes, clsInfo.conexaosqlbanco);
//        }

//        private void btnIdCentroCusto_Click(object sender, EventArgs e)
//        {
//            clsInfo.znomegrid = btnIdCentroCusto.Name;
//            frmCentroCustosPes frmCentroCustosPes = new frmCentroCustosPes(); // frmEscFormVisPes(clsInfo.conexaosqlbanco, "CENTROCUSTOS", idcentrocusto, "ATIVO", "S");
//            clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosPes, clsInfo.conexaosqlbanco);
//        }

//        private void btnIdOs_Click(object sender, EventArgs e)
//        {
//            clsInfo.znomegrid = btnIdOs.Name;
//            frmOrdemServicoPes frmOrdemServicoPes = new frmOrdemServicoPes();// (idos);
//            clsFormHelper.AbrirForm(this.MdiParent, frmOrdemServicoPes, clsInfo.conexaosqldados);
//        }

//        private void btnIdDestino_Click(object sender, EventArgs e)
//        {
//            clsInfo.znomegrid = btnIdDestino.Name;
//            frmPecasPes frmPecasPes = new frmPecasPes(); // frmEscFormVisPes(clsInfo.conexaosqldados, "PECAS", iddestino);
//            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
//        }

//        private void gbxJustificando_Enter(object sender, EventArgs e)
//        {

//        }
//        void bwrSolicitacaoFornecedor_Run()
//        {
//            dgvFornecedores.DataSource = null;

//            bwrSolicitacaoFornecedor = new BackgroundWorker();
//            bwrSolicitacaoFornecedor.DoWork += new DoWorkEventHandler(bwrSolicitacaoFornecedor_DoWork);
//            bwrSolicitacaoFornecedor.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSolicitacaoFornecedor_RunWorkerCompleted);
//            bwrSolicitacaoFornecedor.RunWorkerAsync();
//        }

//        void bwrSolicitacaoFornecedor_DoWork(object sender, DoWorkEventArgs e)
//        {
//            dtSolicitacaoFornecedor = clsSolicitacaoFornecedorBLL.GridDados(id, clsInfo.conexaosqldados);

//            dtSolicitacaoFornecedor.Columns.Add("solicitacaofornecedor_posicao", Type.GetType("System.Int32"));
//            for (Int32 x = 0; x < dtSolicitacaoFornecedor.Rows.Count; x++)
//            {
//                dtSolicitacaoFornecedor.Rows[x]["solicitacaofornecedor_posicao"] = x + 1;
//            }

//            dtSolicitacaoFornecedor.AcceptChanges();
//        }

//        void bwrSolicitacaoFornecedor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {
//            clsSolicitacaoFornecedorBLL.GridMonta(dgvFornecedores, dtSolicitacaoFornecedor, solicitacaofornecedor_posicao);
//            dgvFornecedores.Sort(dgvFornecedores.Columns["FORNECEDOR"], ListSortDirection.Ascending);

//        }
//        void SolicitacaoEntregaSoma()
//        {
//            Double qtdeprograma = 0;
//            foreach (DataRow row in dtSolicitacaoEntrega.Rows)
//            {
//                if (row.RowState != DataRowState.Deleted &&
//                    row.RowState != DataRowState.Detached)
//                {
//                    qtdeprograma = qtdeprograma + Double.Parse(row["qtdeentrega"].ToString());
//                }
//            }
//            Int32 qtdes_cd = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select UNIDDEC from UNIDADE WHERE ID= " + idunid + " "));
//            tbxQtdeProgramada.Text = clsParser.DecimalParse(qtdeprograma.ToString()).ToString("N" + qtdes_cd.ToString());
//            if (clsParser.DecimalParse(tbxQtdeProgramada.Text) > 0)
//            {
//                gbxPrePlanejamento.Visible = false;
//            }
//            else
//            {
//                gbxPrePlanejamento.Visible = true;
//            }

//        }

//        private void dgvSolicitacaoEntrega_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
//        {
//            tspSolicitcaoentregaAlterar.PerformClick();
//        }

//        private void tspSolicitcaoentregaIncluir_Click(object sender, EventArgs e)
//        {
//            solicitacaoentrega_posicao = 0;
//            SolicitacaoEntregaCarregar();
//        }

//        private void tspSolicitcaoentregaAlterar_Click(object sender, EventArgs e)
//        {
//            if (dgvSolicitacaoEntrega.CurrentRow != null)
//            {
//                solicitacaoentrega_posicao = Int32.Parse(dgvSolicitacaoEntrega.CurrentRow.Cells["solicitacaoentrega_posicao"].Value.ToString());
//                SolicitacaoEntregaCarregar();
//            }
//        }

//        private void tspSolicitcaoentregaExcluir_Click(object sender, EventArgs e)
//        {
//            if (dgvSolicitacaoEntrega.CurrentRow != null)
//            {
//                DialogResult drt;

//                drt = MessageBox.Show("Deseja realmente Excluir o registro selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//                if (drt == DialogResult.Yes)
//                {
//                    dtSolicitacaoEntrega.Select("solicitacaoentrega_posicao=" + dgvSolicitacaoEntrega.CurrentRow.Cells["solicitacaoentrega_posicao"].Value.ToString())[0].Delete();
//                }
//            }
//        }

//        private void tspSolicitcaoentregaSalvar_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                DialogResult drt;
//                drt = MessageBox.Show("Deseja salvar e alterar?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

//                if (drt == DialogResult.Yes)
//                {
//                    clsSolicitacaoentregaInfo = new clsSolicitacaoentregaInfo();
//                    SolicitacaoEntregaFillInfo(clsSolicitacaoentregaInfo);
//                    SolicitacaoEntregaFillInfoToGrid(clsSolicitacaoentregaInfo);
//                }
//                else if (drt == DialogResult.Cancel)
//                {
//                    return;
//                }

//                SolicitacaoEntregaSoma();

//                solicitacaoentrega_guia = 0;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }
//        private void SolicitacaoEntregaCarregar()
//        {
//            clsSolicitacaoentregaInfo = new clsSolicitacaoentregaInfo();
//            clsSolicitacaoentregaInfoOld = new clsSolicitacaoentregaInfo();

//            if (solicitacaoentrega_posicao == 0)
//            {
//                solicitacaoentrega_posicao = dtSolicitacaoEntrega.Rows.Count + 1;

//                clsSolicitacaoentregaInfo.dataentrega = DateTime.Now.AddDays(1);
//            }
//            else
//            {
//                SolicitacaoEntregaGridToInfo(clsSolicitacaoentregaInfo, solicitacaoentrega_posicao);
//            }

//            SolicitacaoEntregaCampos(clsSolicitacaoentregaInfo);
//            SolicitacaoEntregaFillInfo(clsSolicitacaoentregaInfoOld);

//            solicitacaoentrega_guia = 1;

//            SolicitacaoEntrega_tbxDataEntrega.Select();
//        }

//        private void SolicitacaoEntregaCampos(clsSolicitacaoentregaInfo info)
//        {
//            solicitacaoentrega_id = info.id;
//            SolicitacaoEntrega_tbxDataEntrega.Text = info.dataentrega.ToString("dd/MM/yyyy");
//            SolicitacaoEntrega_tbxQtdeentrega.Text = info.qtdeentrega.ToString();
//        }

//        private void SolicitacaoEntregaFillInfo(clsSolicitacaoentregaInfo info)
//        {
//            info.id = solicitacaoentrega_id;
//            info.dataentrega = DateTime.Parse(SolicitacaoEntrega_tbxDataEntrega.Text);
//            info.idsolicitacao = id;
//            info.qtdeentrega = clsParser.DecimalParse(SolicitacaoEntrega_tbxQtdeentrega.Text);
//        }

//        void SolicitacaoEntregaFillInfoToGrid(clsSolicitacaoentregaInfo info)
//        {
//            DataRow row;
//            DataRow[] rows = dtSolicitacaoEntrega.Select("solicitacaoentrega_posicao = " + solicitacaoentrega_posicao);

//            if (rows.Length > 0)
//            {
//                row = rows[0];
//            }
//            else
//            {
//                row = dtSolicitacaoEntrega.NewRow();
//            }

//            row["id"] = info.id;
//            row["idsolicitacao"] = info.idsolicitacao;
//            row["dataentrega"] = info.dataentrega;
//            row["qtdeentrega"] = info.qtdeentrega;

//            // Colunas que petencem a outras tabelas
////            row["codigo"] = Orcamento1_tbxCodigo.Text;

//            if (rows.Length == 0)
//            {
//                row["solicitacaoentrega_posicao"] = solicitacaoentrega_posicao;
//                dtSolicitacaoEntrega.Rows.Add(row);
//            }
//        }
//        private void SolicitacaoEntregaGridToInfo(clsSolicitacaoentregaInfo info, Int32 posicao)
//        {
//            DataRow row = dtSolicitacaoEntrega.Select("solicitacaoentrega_posicao = " + posicao)[0];

//            info.id = clsParser.Int32Parse(row["id"].ToString());
//            info.dataentrega = clsParser.DateTimeParse(row["dataentrega"].ToString());
//            info.idsolicitacao = clsParser.Int32Parse(row["idsolicitacao"].ToString());
//            info.qtdeentrega = clsParser.DecimalParse(row["qtdeentrega"].ToString());
//        }

//        private void tspSolicitcaoentregaRetornar_Click(object sender, EventArgs e)
//        {
//            solicitacaoentrega_guia = 0;
//        }

//        private void btnCalculaProgramacao_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                clsProgramacaoEntrega clsProgramacaoEntrega = new clsProgramacaoEntrega();

//                clsProgramacaoEntrega.Periodo periodo;

//                if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "A")
//                {
//                    periodo = clsProgramacaoEntrega.Periodo.Anual;
//                }
//                else if (lbxTipoCalculo.SelectedItem.ToString() == "Semestral")
//                {
//                    periodo = clsProgramacaoEntrega.Periodo.Semestre;
//                }
//                else if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "M")
//                {
//                    periodo = clsProgramacaoEntrega.Periodo.Mes;
//                }
//                else if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "Q")
//                {
//                    periodo = clsProgramacaoEntrega.Periodo.Quinzena;
//                }
//                else if (lbxTipoCalculo.SelectedItem.ToString() == "Semanal")
//                {
//                    periodo = clsProgramacaoEntrega.Periodo.Semana;
//                }
//                else
//                {
//                    periodo = clsProgramacaoEntrega.Periodo.Semana;
//                }

//                DataTable dtTemp = clsProgramacaoEntrega.GerarProgramacao(clsParser.DecimalParse(tbxQtdeSol.Text),
//                                                        0,
//                                                        clsParser.Int32Parse(tbxQtdeEntregas.Text),
//                                                        periodo,
//                                                        clsParser.DateTimeParse(tbxDiaFixo.Text),
//                                                        idunid,
//                                                        ckxPulaSabDom.Checked);

//                foreach (DataRow row in dtSolicitacaoEntrega.Rows)
//                {
//                    if (row.RowState != DataRowState.Detached &&
//                        row.RowState != DataRowState.Deleted)
//                    {
//                        row.Delete();
//                    }
//                }

//                foreach (DataRow row in dtTemp.Rows)
//                {
//                    DataRow rowEntrega = dtSolicitacaoEntrega.NewRow();

//                    rowEntrega["solicitacaoentrega_posicao"] = dtSolicitacaoEntrega.Rows.Count + 1;
//                    rowEntrega["qtdeentrega"] = row["qtde"];
//                    rowEntrega["dataentrega"] = row["entrega"];

//                    dtSolicitacaoEntrega.Rows.Add(rowEntrega);
//                }

//                SolicitacaoEntregaSoma();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, Application.CompanyName);
//            }
//        }

//        private void tbxDiaFixo_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void gbxPrePlanejamento_Enter(object sender, EventArgs e)
//        {

//        }

//        private void tclSolicitacaoEntrega_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            tclSolicitacaoEntrega.SelectedIndex = solicitacaoentrega_guia;
//        }
//    }
//}
