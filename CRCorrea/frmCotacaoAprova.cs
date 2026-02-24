using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmCotacaoAprova : Form
    {
        Int32 idCotacao1;
        DataGridViewRowCollection rows;

        clsCotacaoInfo clsCotacaoInfo = new clsCotacaoInfo();
        clsCotacaoBLL clsCotacaoBLL = new clsCotacaoBLL();
        clsCotacao1Info clsCotacao1Info = new clsCotacao1Info();
        clsCotacao1BLL clsCotacao1BLL = new clsCotacao1BLL();
        clsCotacao2Info clsCotacao2Info = new clsCotacao2Info();
        clsCotacao2BLL clsCotacao2BLL = new clsCotacao2BLL();

        DataTable dtCotacao2;

        //Notas fiscais de Entrada Itens
        DataTable dtNFCompra1;
        clsNfCompra1Info clsNfCompra1Info = new clsNfCompra1Info();
        clsNfCompra1BLL clsNfCompra1BLL = new clsNfCompra1BLL();

        Int32 idCotacao2Ganhou;
        Int32 idcotacao2_forne0;
        Int32 idcotacao2_forne1;
        Int32 idcotacao2_forne2;
        Int32 idcotacao2_forne3;
        Int32 idcotacao2_forne4;
        
        public frmCotacaoAprova()
        {
            InitializeComponent();            
        }

        public void Init(Int32 _idcotacao1, DataGridViewRowCollection _rows)
        {
            this.idCotacao1 = _idcotacao1;
            this.rows = _rows;

            clsCotacaoBLL = new clsCotacaoBLL();
            clsCotacao1BLL = new clsCotacao1BLL();
            clsCotacao2BLL = new clsCotacao2BLL();
            clsNfCompra1BLL = new clsNfCompra1BLL();

        }

        private void frmCotacaoAprova_Load(object sender, EventArgs e)
        {
            tclCotacaoAprova.SelectedIndex = 0;
            CotacaoCarregar();
        }

        private void frmCotacaoAprova_Activated(object sender, EventArgs e)
        {

        }
        private void tspSalvar_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("O fornecedor indicado será gravado para compras. Deseja Salvar ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                clsCotacao1Info = clsCotacao1BLL.Carregar(idCotacao1, clsInfo.conexaosqldados);
                clsCotacao1Info.idcotacao2ganhou = idCotacao2Ganhou;
                clsCotacao1Info.idfornecedorganhou = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idfornecedor from cotacao2 WHERE ID = " + idCotacao2Ganhou + "", "0"));
                clsCotacao1Info.totalprevisto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select totalnota from cotacao2 WHERE ID = " + idCotacao2Ganhou + "","0"));
                clsCotacao1BLL.Alterar(clsCotacao1Info, clsInfo.conexaosqldados);
                this.Close();
            }
        }

        private void tspVisualizar_Click(object sender, EventArgs e)
        {
            tclCotacaoAprova.SelectedIndex = 1;
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CotacaoCarregar()
        {
            // Carregar a Cotação1 - Itens
            clsCotacao1Info = clsCotacao1BLL.Carregar(idCotacao1, clsInfo.conexaosqldados);

            if (clsCotacao1Info.idsolicitacao > 0 && clsCotacao1Info.idsolicitacao != clsInfo.zsolicitacao)
                tbxSolicitante_Cotacao1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usuario from usuario WHERE ID = (select idsolicitante from solicitacao where id = " + clsCotacao1Info.idsolicitacao + ")");
            else
            {
                tbxSolicitante_Cotacao1.Text = "";
            }
            if (clsCotacao1Info.idcentrocusto > 0)
            {
                tbxArea_Cotacao1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS WHERE ID = " + clsCotacao1Info.idcentrocusto);
            }
            else
            {
                tbxArea_Cotacao1.Text = "";
            }
            // carregar o codigo da peça
            Cotacao1_tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + clsCotacao1Info.idcodigo);
            
            Cotacao1_tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + clsCotacao1Info.idcodigo);
            Cotacao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + clsCotacao1Info.idunidade);
            Cotacao1_tbxComplemento.Text = clsCotacao1Info.complemento;
            Cotacao1_tbxComplemento1.Text = clsCotacao1Info.complemento1;
            idCotacao2Ganhou = clsCotacao1Info.idcotacao2ganhou;

            Cotacao1_tbxQtdeAutorizada.Text = clsParser.DecimalParse(clsCotacao1Info.qtdeautorizada.ToString()).ToString();
            Cotacao1_tbxQtdeOk.Text = clsParser.DecimalParse(clsCotacao1Info.qtdeok.ToString()).ToString();



            // Carregar a Cotação  - Cabeçalho
            clsCotacaoInfo = clsCotacaoBLL.Carregar(clsCotacao1Info.idcotacao, clsInfo.conexaosqldados);
            tbxNumero.Text = clsCotacaoInfo.numero.ToString();
            tbxDataMontagem.Text = clsCotacaoInfo.datamontagem.ToString("dd/MM/yyyy HH:mm");
            tbxDataFechamento.Text = clsCotacaoInfo.datafechamento.ToString("dd/MM/yyyy HH:mm");
            tbxTudoFechado.Text = clsCotacaoInfo.tudofechadoem.ToString("dd/MM/yyyy");
            tbxComprador.Text = clsCotacaoInfo.comprador.ToString();
            tbxAutorizante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO where ID=" + clsCotacaoInfo.idautorizante + "");
            tbxTermino.Text = clsCotacaoInfo.termino;
            tbxObservacao.Text = clsCotacaoInfo.observar;

            // Carregar a Cotacao2 - Itens com Fornecedores

            // Campos para apurar o total e qual dos itens esta ganhando a cotação
            Decimal total_previsto_item = 0;    
            dtCotacao2 = clsCotacao2BLL.GridDados(idCotacao1, clsInfo.conexaosqldados);
            // Passar no maximo 5 fornecedores - avisar se possuir mais
            Int32 qtforne = -1;
            foreach (DataRow row in dtCotacao2.Rows)
            {
                qtforne += 1;
                // Carregar a Cotação2  - Fornecedor - Individual
                clsCotacao2Info = clsCotacao2BLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), clsInfo.conexaosqldados);
                if (qtforne == 0)
                {  // Primeiro fornecedor
                    idcotacao2_forne0 = clsCotacao2Info.id;
                    tbxFornecedor.Text = row["fornecedor"].ToString();
                    tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from condpagto WHERE ID = " + clsParser.Int32Parse(row["idcondpagto"].ToString()) + " ", "");
                    tbxQtdeOrcada.Text = clsParser.DecimalParse(row["qtdeorcada"].ToString()).ToString("N0");
                    tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade WHERE ID = " + clsParser.Int32Parse(row["idunidade"].ToString()) + " ", "");
                    tbxPrecoBruto.Text = clsParser.DecimalParse(row["precobruto"].ToString()).ToString("N4");
                    tbxPrecoDescPor.Text = clsParser.DecimalParse(row["precodescpor"].ToString()).ToString("N2");
                    tbxValorDesconto.Text = clsParser.DecimalParse(row["valordesconto"].ToString()).ToString("N4");
                    tbxPreco.Text = clsParser.DecimalParse(row["preco"].ToString()).ToString("N4");
                    tbxTotalMercado.Text = clsParser.DecimalParse(row["totalmercado"].ToString()).ToString("N4");
                    tbxCustoIPI.Text = clsParser.DecimalParse(row["custoipi"].ToString()).ToString("N2");
                    tbxValorFrete.Text = clsParser.DecimalParse(row["valorfrete"].ToString()).ToString("N4");
                    tbxValorSeguro.Text = clsParser.DecimalParse(row["valorseguro"].ToString()).ToString("N2");
                    tbxValorOutras.Text = clsParser.DecimalParse(row["valoroutras"].ToString()).ToString("N2");
                    tbxTotalNota.Text = clsParser.DecimalParse(row["totalnota"].ToString()).ToString("N2");
                    tbxCustoIcm.Text = clsParser.DecimalParse(row["custoicm"].ToString()).ToString("N2");
                    tbxIcmSubst.Text = clsParser.DecimalParse(row["IcmSubst"].ToString()).ToString("N2");

                    if (idCotacao2Ganhou == clsInfo.zcotacao2)
                    {
                        if (clsParser.DecimalParse(row["totalnota"].ToString()) > 0)
                        {
                            if (total_previsto_item == 0)
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(0, 0);
                            }
                            else if (total_previsto_item > clsParser.DecimalParse(row["totalnota"].ToString()))
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(0, 0);
                            }
                        }
                    }
                    else if (idCotacao2Ganhou == clsCotacao2Info.id)
                    {
                        FornecedorIndicado(0, idcotacao2_forne0);
                    }

                }
                else if (qtforne == 1)
                { // segundo fornecedor
                    idcotacao2_forne1 = clsCotacao2Info.id;
                    tbxFornecedor1.Text = row["fornecedor"].ToString();
                    tbxCondPagto1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from condpagto WHERE ID = " + clsParser.Int32Parse(row["idcondpagto"].ToString()) + " ", "");
                    tbxQtdeOrcada1.Text = clsParser.DecimalParse(row["qtdeorcada"].ToString()).ToString("N0");
                    tbxUnidade1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade WHERE ID = " + clsParser.Int32Parse(row["idunidade"].ToString()) + " ", "");
                    tbxPrecoBruto1.Text = clsParser.DecimalParse(row["precobruto"].ToString()).ToString("N4");
                    tbxPrecoDescPor1.Text = clsParser.DecimalParse(row["precodescpor"].ToString()).ToString("N2");
                    tbxValorDesconto1.Text = clsParser.DecimalParse(row["valordesconto"].ToString()).ToString("N4");
                    tbxPreco1.Text = clsParser.DecimalParse(row["preco"].ToString()).ToString("N4");
                    tbxTotalMercado1.Text = clsParser.DecimalParse(row["totalmercado"].ToString()).ToString("N4");
                    tbxCustoIPI1.Text = clsParser.DecimalParse(row["custoipi"].ToString()).ToString("N2");
                    tbxValorFrete1.Text = clsParser.DecimalParse(row["valorfrete"].ToString()).ToString("N4");
                    tbxValorSeguro1.Text = clsParser.DecimalParse(row["valorseguro"].ToString()).ToString("N2");
                    tbxValorOutras1.Text = clsParser.DecimalParse(row["valoroutras"].ToString()).ToString("N2");
                    tbxTotalNota1.Text = clsParser.DecimalParse(row["totalnota"].ToString()).ToString("N2");
                    tbxCustoIcm1.Text = clsParser.DecimalParse(row["custoicm"].ToString()).ToString("N2");
                    tbxIcmSubst1.Text = clsParser.DecimalParse(row["IcmSubst"].ToString()).ToString("N2");
                    if (idCotacao2Ganhou == clsInfo.zcotacao2)
                    {
                        if (clsParser.DecimalParse(row["totalnota"].ToString()) > 0)
                        {
                            if (total_previsto_item == 0)
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(1, 0);
                            }
                            else if (total_previsto_item > clsParser.DecimalParse(row["totalnota"].ToString()))
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(1, 0);
                            }
                        }
                    }
                    else if (idCotacao2Ganhou == clsCotacao2Info.id)
                    {
                        FornecedorIndicado(1,idcotacao2_forne1);
                    }

                }
                else if (qtforne == 2)
                { // terceiro fornecedor
                    idcotacao2_forne2 = clsCotacao2Info.id;
                    tbxFornecedor2.Text = row["fornecedor"].ToString();
                    tbxCondPagto2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from condpagto WHERE ID = " + clsParser.Int32Parse(row["idcondpagto"].ToString()) + " ", "");
                    tbxQtdeOrcada2.Text = clsParser.DecimalParse(row["qtdeorcada"].ToString()).ToString("N0");
                    tbxUnidade2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade WHERE ID = " + clsParser.Int32Parse(row["idunidade"].ToString()) + " ", "");
                    tbxPrecoBruto2.Text = clsParser.DecimalParse(row["precobruto"].ToString()).ToString("N4");
                    tbxPrecoDescPor2.Text = clsParser.DecimalParse(row["precodescpor"].ToString()).ToString("N2");
                    tbxValorDesconto2.Text = clsParser.DecimalParse(row["valordesconto"].ToString()).ToString("N4");
                    tbxPreco2.Text = clsParser.DecimalParse(row["preco"].ToString()).ToString("N4");
                    tbxTotalMercado2.Text = clsParser.DecimalParse(row["totalmercado"].ToString()).ToString("N4");
                    tbxCustoIPI2.Text = clsParser.DecimalParse(row["custoipi"].ToString()).ToString("N2");
                    tbxValorFrete2.Text = clsParser.DecimalParse(row["valorfrete"].ToString()).ToString("N4");
                    tbxValorSeguro2.Text = clsParser.DecimalParse(row["valorseguro"].ToString()).ToString("N2");
                    tbxValorOutras2.Text = clsParser.DecimalParse(row["valoroutras"].ToString()).ToString("N2");
                    tbxTotalNota2.Text = clsParser.DecimalParse(row["totalnota"].ToString()).ToString("N2");
                    tbxCustoIcm2.Text = clsParser.DecimalParse(row["custoicm"].ToString()).ToString("N2");
                    tbxIcmSubst2.Text = clsParser.DecimalParse(row["IcmSubst"].ToString()).ToString("N2");
                    if (idCotacao2Ganhou == clsInfo.zcotacao2)
                    {

                        if ( clsParser.DecimalParse(row["totalnota"].ToString()) > 0)
                        {
                            if (total_previsto_item == 0)
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(2, 0);
                            }
                            else if (total_previsto_item > clsParser.DecimalParse(row["totalnota"].ToString()))
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(2, 0);
                            }
                        }
                    }
                    else if (idCotacao2Ganhou == clsCotacao2Info.id)
                    {
                        FornecedorIndicado(2, idcotacao2_forne2);
                    }
                }
                else if (qtforne == 3)
                { // quarto fornecedor
                    idcotacao2_forne3 = clsCotacao2Info.id;
                    tbxFornecedor3.Text = row["fornecedor"].ToString();
                    tbxCondPagto3.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from condpagto WHERE ID = " + clsParser.Int32Parse(row["idcondpagto"].ToString()) + " ", "");
                    tbxQtdeOrcada3.Text = clsParser.DecimalParse(row["qtdeorcada"].ToString()).ToString("N0");
                    tbxUnidade3.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade WHERE ID = " + clsParser.Int32Parse(row["idunidade"].ToString()) + " ", "");
                    tbxPrecoBruto3.Text = clsParser.DecimalParse(row["precobruto"].ToString()).ToString("N4");
                    tbxPrecoDescPor3.Text = clsParser.DecimalParse(row["precodescpor"].ToString()).ToString("N2");
                    tbxValorDesconto3.Text = clsParser.DecimalParse(row["valordesconto"].ToString()).ToString("N4");
                    tbxPreco3.Text = clsParser.DecimalParse(row["preco"].ToString()).ToString("N4");
                    tbxTotalMercado3.Text = clsParser.DecimalParse(row["totalmercado"].ToString()).ToString("N4");
                    tbxCustoIPI3.Text = clsParser.DecimalParse(row["custoipi"].ToString()).ToString("N2");
                    tbxValorFrete3.Text = clsParser.DecimalParse(row["valorfrete"].ToString()).ToString("N4");
                    tbxValorSeguro3.Text = clsParser.DecimalParse(row["valorseguro"].ToString()).ToString("N2");
                    tbxValorOutras3.Text = clsParser.DecimalParse(row["valoroutras"].ToString()).ToString("N2");
                    tbxTotalNota3.Text = clsParser.DecimalParse(row["totalnota"].ToString()).ToString("N2");
                    tbxCustoIcm3.Text = clsParser.DecimalParse(row["custoicm"].ToString()).ToString("N2");
                    tbxIcmSubst3.Text = clsParser.DecimalParse(row["IcmSubst"].ToString()).ToString("N2");
                    if (idCotacao2Ganhou == clsInfo.zcotacao2)
                    {

                        if (clsParser.DecimalParse(row["totalnota"].ToString()) > 0)
                        {
                            if (total_previsto_item == 0)
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(3, 0);

                            }
                            else if (total_previsto_item > clsParser.DecimalParse(row["totalnota"].ToString()))
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(3, 0);
                            }
                        }
                    }
                    else if (idCotacao2Ganhou == clsCotacao2Info.id)
                    {
                        FornecedorIndicado(3, idcotacao2_forne3);
                    }

                }
                else if (qtforne == 4)
                {// quinto fornecedor
                    idcotacao2_forne4 = clsCotacao2Info.id;
                    tbxFornecedor4.Text = row["fornecedor"].ToString();
                    tbxCondPagto4.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from condpagto WHERE ID = " + clsParser.Int32Parse(row["idcondpagto"].ToString()) + " ", "");
                    tbxQtdeOrcada4.Text = clsParser.DecimalParse(row["qtdeorcada"].ToString()).ToString("N0");
                    tbxUnidade4.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade WHERE ID = " + clsParser.Int32Parse(row["idunidade"].ToString()) + " ", "");
                    tbxPrecoBruto4.Text = clsParser.DecimalParse(row["precobruto"].ToString()).ToString("N4");
                    tbxPrecoDescPor4.Text = clsParser.DecimalParse(row["precodescpor"].ToString()).ToString("N2");
                    tbxValorDesconto4.Text = clsParser.DecimalParse(row["valordesconto"].ToString()).ToString("N4");
                    tbxPreco4.Text = clsParser.DecimalParse(row["preco"].ToString()).ToString("N4");
                    tbxTotalMercado4.Text = clsParser.DecimalParse(row["totalmercado"].ToString()).ToString("N4");
                    tbxCustoIPI4.Text = clsParser.DecimalParse(row["custoipi"].ToString()).ToString("N2");
                    tbxValorFrete4.Text = clsParser.DecimalParse(row["valorfrete"].ToString()).ToString("N4");
                    tbxValorSeguro4.Text = clsParser.DecimalParse(row["valorseguro"].ToString()).ToString("N2");
                    tbxValorOutras4.Text = clsParser.DecimalParse(row["valoroutras"].ToString()).ToString("N2");
                    tbxTotalNota4.Text = clsParser.DecimalParse(row["totalnota"].ToString()).ToString("N2");
                    tbxCustoIcm4.Text = clsParser.DecimalParse(row["custoicm"].ToString()).ToString("N2");
                    tbxIcmSubst4.Text = clsParser.DecimalParse(row["IcmSubst"].ToString()).ToString("N2");
                    if (idCotacao2Ganhou == clsInfo.zcotacao2)
                    {
                        if (clsParser.DecimalParse(row["totalnota"].ToString()) > 0)
                        {
                            if (total_previsto_item == 0)
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(4, 0);
                            }
                            else if (total_previsto_item > clsParser.DecimalParse(row["totalnota"].ToString()))
                            {
                                total_previsto_item = clsParser.DecimalParse(row["totalnota"].ToString());
                                FornecedorIndicado(4, 0);
                            }
                        }
                    }
                    else if (idCotacao2Ganhou == clsCotacao2Info.id)
                    {
                        FornecedorIndicado(4, idcotacao2_forne4);
                    }

                }
                else
                {
                    MessageBox.Show("Foi cotado mais de 5 (cinco) fornecedores " + Environment.NewLine + 
                                    "Porém a capacidade da tela é de apenas 5 (cinco)");
                }
            }

            // carregando as notas fiscais de entrada relaciona com este produto
            dtNFCompra1 = clsNfCompra1BLL.GridDadosPeca(clsCotacao1Info.idcodigo , clsInfo.conexaosqldados);
            clsNfCompra1BLL.GridMontaPeca(dgvNFCompra, dtNFCompra1, 0);

        }
        private void FornecedorIndicado(Int16 _fornecedor, Int32 _idcotacao2)
        {
            ckxIndicado.Checked = false;
            ckxIndicado.Text = "Não Indicado";
            gbxFornecedor.BackColor = System.Drawing.Color.Transparent;
            ckxIndicado1.Checked = false;
            ckxIndicado1.Text = "Não Indicado";
            gbxFornecedor1.BackColor = System.Drawing.Color.Transparent;
            ckxIndicado2.Checked = false;
            ckxIndicado2.Text = "Não Indicado";
            gbxFornecedor2.BackColor = System.Drawing.Color.Transparent;
            ckxIndicado3.Checked = false;
            ckxIndicado3.Text = "Não Indicado";
            gbxFornecedor3.BackColor = System.Drawing.Color.Transparent;
            ckxIndicado4.Checked = false;
            ckxIndicado4.Text = "Não Indicado";
            gbxFornecedor4.BackColor = System.Drawing.Color.Transparent;

            if (_idcotacao2 > 0)
            {  // Ele indicou fornecedor ou já veio com ele da gravação anterior
                idCotacao2Ganhou = _idcotacao2;
            }

            if (_fornecedor == 0)
            {
                ckxIndicado.Checked = true;
                ckxIndicado.Text = "Sim Indicado";
                gbxFornecedor.BackColor = System.Drawing.Color.PowderBlue;

            }
            else if (_fornecedor == 1)
            {
                ckxIndicado1.Checked = true;
                ckxIndicado1.Text = "Sim Indicado";
                gbxFornecedor1.BackColor = System.Drawing.Color.PowderBlue;
            }
            else if (_fornecedor == 2)
            {
                ckxIndicado2.Checked = true;
                ckxIndicado2.Text = "Sim Indicado";
                gbxFornecedor2.BackColor = System.Drawing.Color.PowderBlue;
            }
            else if (_fornecedor == 3)
            {
                ckxIndicado3.Checked = true;
                ckxIndicado3.Text = "Sim Indicado";
                gbxFornecedor3.BackColor = System.Drawing.Color.PowderBlue;
            }
            else if (_fornecedor == 4)
            {
                ckxIndicado4.Checked = true;
                ckxIndicado4.Text = "Sim Indicado";
                gbxFornecedor4.BackColor = System.Drawing.Color.PowderBlue;
            }

        }

        private void ckxIndicado_Click(object sender, EventArgs e)
        {
            FornecedorIndicado(0, idcotacao2_forne0 );
        }

        private void ckxIndicado1_Click(object sender, EventArgs e)
        {
            FornecedorIndicado(1, idcotacao2_forne1);
        }

        private void ckxIndicado2_Click(object sender, EventArgs e)
        {
            FornecedorIndicado(2, idcotacao2_forne2);
        }

        private void ckxIndicado3_Click(object sender, EventArgs e)
        {
            FornecedorIndicado(3, idcotacao2_forne3);
        }

        private void ckxIndicado4_Click(object sender, EventArgs e)
        {
            FornecedorIndicado(4, idcotacao2_forne4);
        }

        private void tspRetornaHistorico_Click(object sender, EventArgs e)
        {
            tclCotacaoAprova.SelectedIndex = 0;
        }


    }
}
