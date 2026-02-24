using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPagarBaixaCheque : Form
    {
        
        
        
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        SqlConnection scn;
        SqlCommand scd;
        SqlDataAdapter sda;

        String query;

        Int32 idFornecedorIndicado;

        Int32 idBanco;

        Boolean carregandoAPagar;
        DataTable dtAPagar;
        BackgroundWorker bwrAPagar;
        Int32 idAPagar;
        Int32 id_Anterior;
        Int32 id_Proximo;
        String Pendencias;

        clsPagarBLL clsPagarBLL;

        Decimal Saldo = 0;
        // Baixado
        Boolean carregandoPagando;
        DataTable dtPagando;
        BackgroundWorker bwrPagando;
        Int32 posicao_Pagando;
        Int32 idPagando;
        Int32 id_Anterior1;
        Int32 id_Proximo1;

        // Incluido a Cta Destino/Cheque
        Int32 idBancoIntDes;
        Boolean carregandoCheque;
        DataTable dtCheque;
        BackgroundWorker bwrCheque;
        Int32 posicao_Cheque;
        Int32 idFluxo;

        String vNroLote = "";
        String vDocumento = "";
        Int32 vidDuplicata = 0;
        Int32 vidFormaPagto;
        String vFormaPagto;
        Int32 vidBancoConta;
        Int32 vBancoConta;
        String vnrocheque = "";
        String vboletonro = "";
        String vcomplemento = "";
        String vobserva = "";
        Int32 viddocumento = 0;
        String vnotadoc = "";
        Int32 vidnotafiscal = 0;
        Int32 vidrecebernfv = 0;
        String vnfvendanumero = "";
        Int32 vidcliente = 0;
        String vClienteCognome = "";
        Int32 vposicao = 0;
        Int32 vposicaofim = 0;
        String vTipoBaixa = "";
        Int32 vconferido = 0;
        String vcomobaixou = "";

        // Colunas do Contas a Pagar (O que vai ser baixado)
        GridColuna[] dtAPagarColunas = new GridColuna[]
                                        {
                                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "Filial", 20, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("PF", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Emissao", "EMISSAO", 70, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Doc", "DOCUMENTO", 45, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 70, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("VenctoPrev", "VENCIMENTOPREV", 70, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Titulo", "VALORPAGAR", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Forma Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Bco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Observa", "OBSERVA", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Chegou", "CHEGOU", 50, true, DataGridViewContentAlignment.MiddleCenter)
                                        };

        // Colunas do Contas a Pagar (O que esta sendo baixado)
        GridColuna[] dtPagandoColunas = new GridColuna[]
                                        {
                                            new GridColuna("Posicao", "POSICAO_PAGANDO", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdPagar", "IDPAGAR", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdPagas", "IDPAGAS", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "FILIAL", 20, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Titulo", "VALORTITULO", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Pagto", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D/C", "DEBCRED", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cod", "COBRANCACOD", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("His", "COBRANCAHIS", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("CobrancaNome", "COBRANCANOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Id Historico", "IDHISTORICO", 1, false  , DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Id CentroCusto", "IDCENTROCUSTO", 1, false, DataGridViewContentAlignment.MiddleRight)
                                        };

        // Colunas do Cheque (O que esta sendo baixado)
        GridColuna[] dtChequeColunas = new GridColuna[]
                                        {
                                            new GridColuna("Posicao", "POSICAO_CHEQUE", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdFluxoBco", "IDFLUXO", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Tp.Ch", "TIPOCHEQUE", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdBcoDestino", "IDBANCOINTDES", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Saindo Cta", "BANCOINTDES", 30, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Nome Cta Saindo", "BANCOINTDESNOME", 90, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("IdBco", "IDBANCO", 1, false, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Nro Bco", "BANCO", 30, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Nome Bco", "BANCONOME", 90, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Nro Ch", "NROCHEQUE", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Valor", "VALOR", 75, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Data Dep.", "DATADEPOSITO", 70, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Observação", "OBSERVACAO", 120, true, DataGridViewContentAlignment.MiddleLeft)
                                        };


        // Colunas do Contas Pagas (O que ja foi pago)
        GridColuna[] dtRecebidoColunas = new GridColuna[]
                                        {
                                            new GridColuna("Posicao", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PF", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Emissão", "EMISSAO", 75, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Doc", "DOCUMENTO", 35, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cliente", "COGNOME", 170, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 75, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Receber", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight)
                                        };


        public frmPagarBaixaCheque()
        {
            InitializeComponent();
        }

        public void Init()
        {
            carregandoAPagar = false;

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select COGNOME from CLIENTE order by COGNOME ", tbxCognome);
            clsPagarBLL = new clsPagarBLL();

        }

        private void frmPagarBaixaCheque_Load(object sender, EventArgs e)
        {
            tbxDataBaixa.Text = DateTime.Now.ToString("dd/MM/yyyy");
            bwrAPagar_Run();
            CriarPagandoBaixa();
            CriarCheque();
            bwrCheque_Run();
        }
        private void frmPagarBaixaCheque_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void frmPagarBaixaCheque_Shown(object sender, EventArgs e)
        {
            
        }

        private void tspAlterarPagar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado !!!");
        }

        private void tspIncluirCheque_Click(object sender, EventArgs e)
        {
            tclPagarBaixaCheque.SelectedIndex = 2;
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado !!!");
        }

        private void tspAlterarPagas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado !!!");
        }

        private void tspConcluir_Click(object sender, EventArgs e)
        {
            if (clsParser.DecimalParse(tbxChequesSoma.Text) == 0)
            {
                MessageBox.Show("Você não adicionou nenhum valor para Pagar as Duplicatas");
            }
            else
            {
                // Baixar as Duplicatas
                DialogResult drt;
                drt = MessageBox.Show("Atenção a Baixa fara os seguintes procedimentos : " + Environment.NewLine +
                     " 1. O Lançamento/Duplicata vai sair do Contas a Pagar e ir Para o Contas Pagas " + Environment.NewLine +
                     " 2. Vai incluir estes valores no Aplibank na Cta " + tbxBancoIntDes.Text + Environment.NewLine +
                     " Deseja Prosseguir ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
                if (drt == DialogResult.Yes)
                {
                    // Criar o numero do Lote
                    DateTime databaixa = DateTime.Parse(tbxDataBaixa.Text);
                    DateTime datacredito = DateTime.Parse(tbxDataBaixa.Text);
                    //TimeSpan Compensar;

                    Int32 XNro = 0;
                    Int32 XDuplicata = 0;
                    Int32 XPosicao = 0;

                    XNro = 1;
                    vNroLote = "PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + XNro.ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");

                    using (TransactionScope tse = new TransactionScope())
                    {
                        foreach (DataRow row in dtPagando.Rows)
                        {
                            /////////////////
                            /// Baixar
                            // Cada Titulo tera um lançamento individual na baixa do aplibank
                            // Sendo assim cria um novo lote para diferenciar um lançamento do outro
                            if (clsParser.Int32Parse(row["DUPLICATA"].ToString()) != XDuplicata)
                            {
                                XNro += 1;
                                vNroLote = "PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + XNro.ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");
                                XPosicao = clsParser.Int32Parse(row["POSICAO"].ToString());
                            }
                            else
                            {
                                if (clsParser.Int32Parse(row["POSICAO"].ToString()) != XPosicao)
                                {
                                    XNro += 1;
                                    vNroLote = "PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + XNro.ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");
                                    XPosicao = clsParser.Int32Parse(row["POSICAO"].ToString());
                                }
                            }
                            
                            /////////////////
                            if (clsParser.Int32Parse(row["IDPAGAS"].ToString()) > 0)
                            {
                                vDocumento = "PAGA";
                                vidDuplicata = clsParser.Int32Parse(row["IDPAGAS"].ToString());
                            }
                            else
                            {
                                vDocumento = "PAGAR";
                                vidDuplicata = clsParser.Int32Parse(row["IDPAGAR"].ToString());
                            }
                            if (vDocumento == "PAGAR")
                            {
                                // 1. VERIFICAR SE ESTA DANDO BAIXA DE UM CHEQUE
                                vidFormaPagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDFORMAPAGTO FROM PAGAR where ID=" + vidDuplicata + " "));
                                vFormaPagto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where ID=" + vidFormaPagto + " ");
                                if (vFormaPagto == "CH")
                                {
                                    vidBancoConta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDBANCO from PAGAR where ID=" + vidDuplicata + " "));
                                    vBancoConta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + vidBancoConta).ToString());
                                    vboletonro = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETONRO from PAGAR where ID=" + vidDuplicata + " ");
                                    if (vBancoConta > 0)
                                    {
                                        vnrocheque = "(" + vBancoConta.ToString() + ")" + vboletonro;
                                    }
                                    else
                                    {
                                        vnrocheque = vboletonro;
                                    }
                                }
                                else
                                {
                                    vnrocheque = "";
                                }
                                /* //==>
                                if (tbxBaixaHistorico.Text != "NC")
                                {
                                    complemento = tbxBaixaHistoricoNome.Text;
                                }*/
                                vobserva = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select OBSERVA from PAGAR where ID=" + vidDuplicata + " ");
                                vcomplemento = "";
                                if (vobserva.Length > 0)
                                {
                                    if (vcomplemento.Length > 0)
                                    {
                                        vcomplemento = vcomplemento + "-" + vobserva;
                                    }
                                    else
                                    {
                                        vcomplemento = vobserva;
                                    }

                                }
                                viddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from PAGAR where ID=" + vidDuplicata + " "));
                                vnotadoc = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + viddocumento);
                                if (vnotadoc.Length > 0)
                                {
                                    if (vcomplemento.Length > 0)
                                    {
                                        vcomplemento = vcomplemento + "-" + vnotadoc;
                                    }
                                    else
                                    {
                                        vcomplemento = vnotadoc;
                                    }
                                }

                                vidnotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDNOTAFISCAL FROM PAGAR where id=" + vidDuplicata));
                                vidrecebernfv = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDPAGARNFE FROM PAGAR where id=" + vidDuplicata));

                                if (vnotadoc == "CTO")
                                {
                                    vnfvendanumero = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM CONTRATO where id=" + vidnotafiscal);
                                }
                                else
                                {
                                    vnfvendanumero = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFCOMPRA where id=" + vidnotafiscal);
                                }
                                if (vnfvendanumero.Length > 0)
                                {
                                    if (vcomplemento.Length > 0)
                                    {
                                        vcomplemento = vcomplemento + " : " + vnfvendanumero;
                                        if (vnotadoc == "CTO" && Application.ProductName.ToString().ToUpper() == "APLIESQUADRIA")
                                        {
                                            // " Cto : " + tbxDuplicata.Text + " Edif : " + tbxEdificio.Text + " Apto=" + tbxApartamento.Text + " - " + tbxObserva.Text;
                                            Int32 idedificioapto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDEDIFICIOAPARTAMENTO FROM CONTRATO Where id=" + vidnotafiscal));
                                            Int32 idedificio = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDEDIFICIO FROM EDIFICIOAPARTAMENTO Where id=" + idedificioapto));
                                            String Apartamento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NROAPTO FROM EDIFICIOAPARTAMENTO Where id=" + idedificioapto);
                                            String Edificio = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM EDIFICIO Where id=" + idedificio);
                                            vcomplemento = vcomplemento + " Edif : " + Edificio + " Apto=" + Apartamento;

                                        }
                                    }
                                    else
                                    {
                                        vcomplemento = vnfvendanumero;
                                    }
                                }
                                if (vboletonro.Length >= 2)
                                {
                                    if (vcomplemento.Length > 0)
                                    {
                                        vcomplemento = vcomplemento + "- BO/CH= " + vboletonro;
                                    }
                                    else
                                    {
                                        vcomplemento = "BO/CH= " + vboletonro;
                                    }
                                }
                                vobserva = vcomplemento;
                                vidcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDFORNECEDOR FROM PAGAR where id=" + vidDuplicata));
                                vClienteCognome = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM CLIENTE where id=" + vidcliente);
                                //vClienteCognome = vClienteCognome + " [" + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + vidcliente) + "]";
                                vposicao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT POSICAO FROM PAGAR where id=" + vidDuplicata));
                                vposicaofim = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT POSICAOFIM FROM PAGAR where id=" + vidDuplicata));

                            }
                            else
                            {
                                // Pegar o Id do receber que esta dentro do Contas Recebida
                                vidDuplicata = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDPAGAR FROM PAGAS where ID=" + vidDuplicata + " "));
                            }
                            String DataBaixa;
                            String DataCredito;
                            vTipoBaixa = "D";   // TIPO DA BAIXA DIVERSOS
                            DataBaixa = tbxDataBaixa.Text;
                            DataCredito = tbxDataBaixa.Text;
                            clsFinanceiro.BaixarBanco("PAGAR", vidDuplicata, vNroLote, vboletonro, idBancoIntDes, clsParser.Int32Parse(row["IDHISTORICO"].ToString()),
                                                      clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString()), clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString()),
                                                      DateTime.Parse(DataBaixa), DateTime.Parse(DataCredito),
                                                      clsParser.DecimalParse(row["VALOR"].ToString()),
                                                      row["cobrancacod"].ToString(), row["DEBCRED"].ToString(), vobserva, vClienteCognome, vDocumento,
                                                      vnotadoc, vnfvendanumero, vposicao, vposicaofim, vFormaPagto.Substring(0, 2), clsInfo.zusuario, vnrocheque, vconferido, vTipoBaixa);
                        }
                        // Transferir para o Contas Recebidas
                        foreach (DataRow row in dtPagando.Rows)
                        {
                            if (clsParser.Int32Parse(row["IDPAGAS"].ToString()) > 0)
                            {
                                vDocumento = "PAGA";
                                vidDuplicata = clsParser.Int32Parse(row["IDPAGAS"].ToString());
                            }
                            else
                            {
                                vDocumento = "PAGAR";
                                vidDuplicata = clsParser.Int32Parse(row["IDPAGAR"].ToString());
                            }

                            vcomobaixou = vFormaPagto + "na Cta: " + tbxBancoIntDes.Text;

                            clsFinanceiro.TransferirPago("PAGAR", vidDuplicata, tbxDataBaixa.Text,
                                tbxDataBaixa.Text,
                                clsParser.Int32Parse(row["COBRANCACOD"].ToString()),
                                clsParser.Int32Parse(row["COBRANCAHIS"].ToString()),
                                clsParser.DecimalParse(row["VALOR"].ToString()),
                                row["DEBCRED"].ToString(),
                                clsParser.Int32Parse(row["IDHISTORICO"].ToString()),
                                clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString()),
                                clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString()),
                                vcomobaixou,
                                0,
                                0,
                                0);

                            if (vDocumento == "PAGAR")
                            {
                                if (row["COBRANCACOD"].ToString() == "00" || row["COBRANCACOD"].ToString() == "01")
                                {
                                    // Transferir as Observações do Contas a Receber para o Recebida
                                    clsFinanceiro.TransferirPagoObserva("PAGAR", vidDuplicata);
                                }
                            }

                        }
                        // Ultima fase marcar que foi pago e excluir a duplicata do contas a receber
                        foreach (DataRow row in dtPagando.Rows)
                        {
                            if (clsParser.Int32Parse(row["IDPAGAR"].ToString()) > 0)
                            {
                                vidDuplicata = clsParser.Int32Parse(row["IDPAGAR"].ToString());

                                viddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from PAGAR where ID=" + vidDuplicata + " "));
                                vnotadoc = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + viddocumento);

                                vidnotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDNOTAFISCAL FROM PAGAR where id=" + vidDuplicata));
                                vidrecebernfv = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDPAGARNFE FROM PAGAR where id=" + vidDuplicata));

                                // Marcar na Nota Fiscal que foi Pago
                                if (vnotadoc == "CTO")
                                {
                                    scn = new SqlConnection(clsInfo.conexaosqldados);
                                    scd = new SqlCommand("UPDATE CONTRATORECEBER SET PAGOU=@PAGOU WHERE ID=@ID ", scn);
                                    scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = vidrecebernfv;
                                    scd.Parameters.AddWithValue("@PAGOU", SqlDbType.NVarChar).Value = "S";
                                    scn.Open();
                                    scd.ExecuteNonQuery();
                                    scn.Close();
                                }
                                else
                                {
                                    scn = new SqlConnection(clsInfo.conexaosqldados);
                                    scd = new SqlCommand("UPDATE NFCOMPRAPAGAR SET PAGOU=@PAGOU WHERE ID=@ID ", scn);
                                    scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = vidrecebernfv;
                                    scd.Parameters.AddWithValue("@PAGOU", SqlDbType.NVarChar).Value = "S";
                                    scn.Open();
                                    scd.ExecuteNonQuery();
                                    scn.Close();
                                }
                                // Excluir as Duplicatas se foi Total  // Todas baixadas aqui é baixa total não tem Parcial
                                // Apagar observação do receber
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("delete pagarobserva where idduplicata=@id", scn);
                                scd.Parameters.Add("@id", SqlDbType.Int).Value = vidDuplicata;
                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();
                                // Apagar os itens do receber
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("delete pagar01 where idduplicata=@id", scn);
                                scd.Parameters.Add("@id", SqlDbType.Int).Value = vidDuplicata;
                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();
                                // Apagar o receber
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("delete pagar where id=@id", scn);
                                scd.Parameters.Add("@id", SqlDbType.Int).Value = vidDuplicata;
                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();
                            }
                        }
                        tse.Complete();
                    }
                    this.Close();
                }
            }
        }

        private void tspOrdem_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("UPDATE PAGAR SET VALORBAIXANDO=@DIFERENCA ", scn);
//            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idAPagar;
            scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = 0;
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

            this.Close();
        }

        private void tbxSaldoAPagarFinal_TextChanged(object sender, EventArgs e)
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
        }
        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void CamposBaixar(object _ctrl)
        {
        }
        private void bwrAPagar_Run()
        {
            if (carregandoAPagar == false)
            {
                carregandoAPagar = true;

                bwrAPagar = new BackgroundWorker();
                bwrAPagar.DoWork += new DoWorkEventHandler(bwrAPagar_DoWork);
                bwrAPagar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrAPagar_RunWorkerCompleted);
                bwrAPagar.RunWorkerAsync();
            }
        }


        private void bwrAPagar_DoWork(object sender, DoWorkEventArgs e)
        {
            dtAPagar = new DataTable();

            query = "SELECT PAGAR.ID,PAGAR.FILIAL,PAGAR.DUPLICATA,PAGAR.POSICAO " +
                    ",PAGAR.POSICAOFIM, DOCFISCAL.COGNOME AS [DOCUMENTO], PAGAR.EMISSAO,CLIENTE.COGNOME,PAGAR.VENCIMENTO " +
                    ",PAGAR.VENCIMENTOPREV,PAGAR.VALORLIQUIDO - (PAGAR.VALORPAGO + PAGAR.VALORDEVOLVIDO) AS [VALORPAGAR] " +
                    ",PAGAR.VALORLIQUIDO " +
                    ",SITUACAOTIPOTITULO.CODIGO + '-' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], PAGAR.BOLETONRO " +
                    ",PAGAR.DV,TAB_BANCOS.COGNOME AS [BANCO],PAGAR.VALORJUROS,PAGAR.VALORMULTA " +
                    ",PAGAR.VALORDESCONTO,PAGAR.ATEVENCIMENTO, SITUACAOTITULO.CODIGO [SITUACAOTITULO] " +
                    ",PAGAR.OBSERVA AS [OBSERVA], PAGAR.CHEGOU " +
                    "FROM PAGAR " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=PAGAR.IDFORNECEDOR " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=PAGAR.IDFORMAPAGTO " +
                    "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO " +
                    "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID=PAGAR.IDBANCO " +
                    "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=PAGAR.IDSITBANCO ";

            query = query + " WHERE  PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
            query = query + " AND PAGAR.VALORPAGO = " + 0 + " ";  // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
            query = query + " AND PAGAR.VALORBAIXANDO = " + 0 + " "; // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO
            query = query + " AND DOCFISCAL.COGNOME <> 'PRO' AND DOCFISCAL.COGNOME <> 'PCO' "; // NÃO PODE BAIXAR PROVISÃO/PED COMPRA

            if (rbnFornecedoresTodos.Checked == true)
            {
                // Todos os Fornecedores
            }
            else
            {
                query = query + " AND CLIENTE.COGNOME ='" + tbxCognome.Text + "' "; // APENAS DE UM FORNECEDOR
            }
            if (ckxPendencias.Checked == true)
            {
                Int32 idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));
                query = query + " AND PAGAR.IDBANCOINT != " + idBancoInt + " ";
            }
            query = query + " ORDER BY PAGAR.VENCIMENTOPREV, PAGAR.DUPLICATA ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtAPagar);

            if (Saldo == 0)
            { /*
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + PAGAR.VALORBAIXANDO) - (PAGAR.VALORPAGO + PAGAR.VALORDEVOLVIDO)) " +
                        "FROM PAGAR " +
                        "LEFT JOIN CLIENTE ON CLIENTE.ID=PAGAR.IDFORNECEDOR " +
                        "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=PAGAR.IDFORMAPAGTO " +
                        "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO " +
                        "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID=PAGAR.IDBANCO " +
                        "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=PAGAR.IDSITBANCO ";
                query = query + " WHERE  PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
                query = query + " AND PAGAR.VALORPAGO = " + 0 + " ";  // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
                query = query + " AND PAGAR.VALORBAIXANDO = " + 0 + " "; // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO
                query = query + " AND DOCFISCAL.COGNOME <> 'PRO' AND DOCFISCAL.COGNOME <> 'PCO' "; // NÃO PODE BAIXAR PROVISÃO/PED COMPRA
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scd = new SqlCommand(query, scn);
                scn.Open();
                Saldo = clsParser.DecimalParse(scd.ExecuteScalar().ToString());
                scn.Close();  */
            }
        }


        private void bwrAPagar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvAPagar.DataSource = dtAPagar;
                clsGridHelper.MontaGrid2(dgvAPagar, dtAPagarColunas, true);

                dgvAPagar.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvAPagar.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvAPagar.Columns["VENCIMENTOPREV"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvAPagar.Columns["VALORPAGAR"].DefaultCellStyle.Format = "N2";

                PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoAPagar = false;

                tbxSaldoAPagar.Text = clsGridHelper.SomaGrid(dgvAPagar, "VALORPAGAR").ToString("N2");

                if (carregandoPagando == false)
                {
                    bwrPagando_Run();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoAPagar = false;
            }
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvAPagar);
            if (idAPagar > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(idAPagar, dgvAPagar, "DUPLICATA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo, dgvAPagar, "DUPLICATA") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior, dgvAPagar, "DUPLICATA") == false)
                        {
                            if (dgvAPagar.Rows.Count > 0)
                            {
                                dgvAPagar.CurrentCell = dgvAPagar.Rows[0].Cells["DUPLICATA"];
                            }
                        }
                    }
                }
            }
            else if (dgvAPagar.Rows.Count > 0)
            {
                dgvAPagar.CurrentCell = dgvAPagar.Rows[0].Cells["DUPLICATA"];
            }

            if (dgvAPagar.CurrentRow != null)
            {
                idAPagar = clsParser.Int32Parse(dgvAPagar.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvAPagar.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvAPagar.Rows[dgvAPagar.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvAPagar.CurrentRow.Index < dgvAPagar.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvAPagar.Rows[dgvAPagar.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
            }
            else
            {
                idAPagar = 0;
                id_Anterior = 0;
                id_Proximo = 0;
            }
        }

        private void CriarPagandoBaixa()
        {
            dtPagando = new DataTable();
            dtPagando.Columns.Add("POSICAO_PAGANDO", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("IDPAGAR", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("IDPAGAS", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("FILIAL", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("DUPLICATA", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("POSICAO", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("COGNOME", Type.GetType("System.String"));
            dtPagando.Columns.Add("VENCIMENTO", Type.GetType("System.DateTime"));
            dtPagando.Columns.Add("VALORTITULO", Type.GetType("System.Decimal"));
            dtPagando.Columns.Add("VALOR", Type.GetType("System.Decimal"));
            dtPagando.Columns.Add("DEBCRED", Type.GetType("System.String"));
            dtPagando.Columns.Add("COBRANCACOD", Type.GetType("System.String"));
            dtPagando.Columns.Add("COBRANCAHIS", Type.GetType("System.String"));
            dtPagando.Columns.Add("COBRANCANOME", Type.GetType("System.String"));
            dtPagando.Columns.Add("IDHISTORICO", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("IDCENTROCUSTO", Type.GetType("System.Int32"));
            dtPagando.Columns.Add("IDCODIGOCTABIL", Type.GetType("System.Int32"));

        }

        private void bwrPagando_Run()
        {
            if (carregandoPagando == false)
            {
                carregandoPagando = true;

                bwrPagando = new BackgroundWorker();
                bwrPagando.DoWork += new DoWorkEventHandler(bwrPagando_DoWork);
                bwrPagando.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPagando_RunWorkerCompleted);
                bwrPagando.RunWorkerAsync();
            }
        }


        private void bwrPagando_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        private void bwrPagando_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvPagando.DataSource = dtPagando;
                clsGridHelper.MontaGrid2(dgvPagando, dtPagandoColunas, true);
                dgvPagando.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPagando.Columns["VALORTITULO"].DefaultCellStyle.Format = "N2";
                dgvPagando.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvPagando, 7);
                //GridHelper.SelecionaLinha(idRecebendo, dgvPagando, 1);
                PesquisaRapida1(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoPagando = false;

                // Calcular os Totais
                tbxValorPrincipal.Text = 0.ToString("N2");
                tbxValorPagando.Text = 0.ToString("N2");
                tbxValorMultas.Text = 0.ToString("N2");
                tbxValorJuros.Text = 0.ToString("N2");
                tbxValorCartorio.Text = 0.ToString("N2");
                tbxValorTarifas.Text = 0.ToString("N2");

                foreach (DataRow rowTotais in dtPagando.Rows)
                {
                    //SE JÁ TEM TITULO TRAVAR O FRAME PARA NÃO ALTERAR MAIS NADA
                    gbxCabecalho.Enabled = false;
                    // ' VALOR PRINCIPAL DO TITULO
                    tbxValorPrincipal.Text = (clsParser.DecimalParse(tbxValorPrincipal.Text) + clsParser.DecimalParse(rowTotais["VALORTITULO"].ToString())).ToString("N2");
                    switch (rowTotais["COBRANCACOD"].ToString().ToUpper())
                    {
                        case "01":
                            tbxValorPagando.Text = (clsParser.DecimalParse(tbxValorPagando.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "02":
                            tbxValorMultas.Text = (clsParser.DecimalParse(tbxValorMultas.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "03":
                            tbxValorJuros.Text = (clsParser.DecimalParse(tbxValorJuros.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "04":
                            if (rowTotais["COBRANCAHIS"].ToString().ToUpper() == "41")
                            {
                                tbxValorCartorio.Text = (clsParser.DecimalParse(tbxValorCartorio.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            }
                            else
                            {
                                tbxValorTarifas.Text = (clsParser.DecimalParse(tbxValorTarifas.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            }
                            break;
                        case "08":
                            tbxValorDesconto.Text = (clsParser.DecimalParse(tbxValorDesconto.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "09":
                            tbxValorTarifas.Text = (clsParser.DecimalParse(tbxValorTarifas.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        default:
                            MessageBox.Show("Despesa não foi adicionada aos calculos ?? ");
                            break;
                    }
                    // TOTAL DE DEDUÇÕES = tarifas + cartorios
                    tbxTotalDeducoes.Text = (clsParser.DecimalParse(tbxValorTarifas.Text) + clsParser.DecimalParse(tbxValorCartorio.Text)).ToString("N2");
                    //liquido a receber = (vl recebido + multas + juros) - descontos
                    tbxValorPagar.Text = (clsParser.DecimalParse(tbxValorPagando.Text) + clsParser.DecimalParse(tbxValorMultas.Text) + clsParser.DecimalParse(tbxValorJuros.Text)).ToString("N2");

                }
                SomarCheque();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoAPagar = false;
            }

        }
        private void PesquisaRapida1()
        {
            //GridHelper.Filtrar(tbxPesquisa.Text, dgvRecebendo);
            if (posicao_Pagando > 0 || id_Anterior1 > 0 || id_Proximo1 > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(posicao_Pagando, dgvPagando, "DUPLICATA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo1, dgvPagando, "DUPLICATA") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior1, dgvPagando, "DUPLICATA") == false)
                        {
                            if (dgvPagando.Rows.Count > 0)
                            {
                                dgvPagando.CurrentCell = dgvPagando.Rows[0].Cells["DUPLICATA"];
                            }
                        }
                    }
                }
            }
            else if (dgvPagando.Rows.Count > 0)
            {
                dgvPagando.CurrentCell = dgvPagando.Rows[0].Cells["DUPLICATA"];
            }

            if (dgvPagando.CurrentRow != null)
            {
                posicao_Pagando = clsParser.Int32Parse(dgvPagando.CurrentRow.Cells["posicao_Pagando"].Value.ToString());
                if (dgvPagando.CurrentRow.Index > 0)
                {
                    id_Anterior1 = clsParser.Int32Parse(dgvPagando.Rows[dgvPagando.CurrentRow.Index - 1].Cells["posicao_Pagando"].Value.ToString());
                }
                else
                {
                    id_Anterior1 = 0;
                }
                if (dgvPagando.CurrentRow.Index < dgvPagando.Rows.Count - 1)
                {
                    id_Proximo1 = clsParser.Int32Parse(dgvPagando.Rows[dgvPagando.CurrentRow.Index + 1].Cells["posicao_Pagando"].Value.ToString());
                }
                else
                {
                    id_Proximo1 = 0;
                }
            }
            else
            {
                id_Anterior1 = 0;
                id_Proximo1 = 0;
            }
        }

        private void dgvAPagar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvAPagar.CurrentRow != null)
            {
                idAPagar = clsParser.Int32Parse(dgvAPagar.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvAPagar.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvAPagar.Rows[dgvAPagar.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvAPagar.CurrentRow.Index < dgvAPagar.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvAPagar.Rows[dgvAPagar.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
                Baixar();
            }

        }
        private void Baixar()
        {
            if (dgvAPagar.CurrentRow != null)
            {
                idAPagar = clsParser.Int32Parse(dgvAPagar.CurrentRow.Cells["ID"].Value.ToString());
                // verificar se existe Historico
                Int32 idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICO from PAGAR where ID= " + idAPagar));
                Int32 idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTO from PAGAR where ID= " + idAPagar));
                if (idHistorico == 0)
                {
                    MessageBox.Show("Falta Historico do Aplibank nesta Duplicata " + Environment.NewLine +
                                    "Vá na duplicata [" + dgvAPagar.CurrentRow.Cells["DUPLICATA"].Value.ToString() + "]");
                }
                else
                {
                    Decimal Valor = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALOR from PAGAR where ID= " + idAPagar));
                    Decimal ValorDesconto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORDESCONTO from PAGAR where ID= " + idAPagar));
                    Decimal ValorJuros = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORJUROS from PAGAR where ID= " + idAPagar));
                    Decimal ValorMulta = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORMULTA from PAGAR where ID= " + idAPagar));
                    Decimal ValorCredito = 0; // clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORCREDITO from PAGAR where ID= " + idAPagar));
                    Decimal ValorRecebido = 0;
                    Decimal ValorDevolucao = 0;

                    DuplicataInfo DuplicataInfo;
                    DuplicataInfo = clsFinanceiro.CalcularDuplicata("Pagar",
                                                                    dgvAPagar.CurrentRow.Cells["VENCIMENTO"].Value.ToString(),
                                                                    tbxDataBaixa.Text,
                                                                    Valor,
                                                                    ValorDesconto,
                                                                    ValorJuros,
                                                                    ValorMulta,
                                                                    ValorCredito,
                                                                    dgvAPagar.CurrentRow.Cells["ATEVENCIMENTO"].Value.ToString());


                    Decimal ValorLiquido = DuplicataInfo.valorliquido;
                    Decimal Multa = DuplicataInfo.multas;
                    Decimal Juros = DuplicataInfo.juros;
                    Decimal Desconto = DuplicataInfo.descontos;
                    Decimal Saldo = ((Valor + Multa + Juros) - Desconto);
                    Int32 totaldias = DuplicataInfo.ntotaldias;
                    Decimal Diferenca = (Saldo + ValorCredito) - (ValorRecebido + ValorDevolucao);

                    // Baixar o Titulo

                    Int32 x = 1;
                    for (x = 1; x <= 5; x++)
                    {
                        BaixarBoletoInfo BaixarBoletoInfo;
                        BaixarBoletoInfo = clsFinanceiro.BaixarBoletoPagar(x, idAPagar, Juros,
                                         //Parser.Int32Parse(tbxBanco.Text), clsParser.DateTimeParse(tbxDataBaixa.Text),
                                         clsParser.Int32Parse("0".ToString()), clsParser.DateTimeParse(tbxDataBaixa.Text),
                                         clsParser.DateTimeParse(dgvAPagar.CurrentRow.Cells["VENCIMENTO"].Value.ToString()),
                                         //Valor, clsParser.DecimalParse(tbxTarifaBoleto.Text), clsParser.DecimalParse(tbxTarifaCartorio.Text),
                                         Valor, clsParser.DecimalParse("0".ToString()), clsParser.DecimalParse("0".ToString()),
                                         Multa, Desconto, dgvAPagar.CurrentRow.Cells["ATEVENCIMENTO"].Value.ToString(),
                                         0, 0);

                        if (BaixarBoletoInfo.IncluirRegistro == true)
                        { // Incluir um novo registro
                            // procurando a posição
                            posicao_Pagando = 0;
                            foreach (DataRow row1 in dtPagando.Rows)
                            {
                                if (clsParser.Int32Parse(row1["POSICAO_PAGANDO"].ToString()) > posicao_Pagando)
                                {
                                    posicao_Pagando = clsParser.Int32Parse(row1["POSICAO_PAGANDO"].ToString());
                                }
                            }
                            posicao_Pagando += 1;
                            DataRow row = dtPagando.NewRow();
                            row["POSICAO_PAGANDO"] = posicao_Pagando;
                            row["IDPAGAR"] = idAPagar;
                            row["IDPAGAS"] = 0;
                            row["FILIAL"] = clsParser.Int32Parse(dgvAPagar.CurrentRow.Cells["FILIAL"].Value.ToString());
                            row["DUPLICATA"] = clsParser.Int32Parse(dgvAPagar.CurrentRow.Cells["DUPLICATA"].Value.ToString());
                            row["POSICAO"] = clsParser.Int32Parse(dgvAPagar.CurrentRow.Cells["POSICAO"].Value.ToString());
                            row["COGNOME"] = dgvAPagar.CurrentRow.Cells["COGNOME"].Value.ToString();
                            row["VENCIMENTO"] = clsParser.DateTimeParse(dgvAPagar.CurrentRow.Cells["VENCIMENTO"].Value.ToString());
                            row["VALORTITULO"] = BaixarBoletoInfo.ValorTitulo;
                            row["VALOR"] = BaixarBoletoInfo.ValorLancamento;
                            row["DEBCRED"] = BaixarBoletoInfo.DebCred;
                            row["COBRANCACOD"] = BaixarBoletoInfo.Codigo;
                            row["COBRANCAHIS"] = BaixarBoletoInfo.Historico;
                            row["COBRANCANOME"] = BaixarBoletoInfo.Nome;
                            // Vai buscar na Situação da Cobrança
                            idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOPAGAR from SITUACAOCOBRANCACOD where ID= '" + BaixarBoletoInfo.Codigo + "' ").ToString());
                            if (idHistorico == 0)
                            {
                                idHistorico = clsInfo.zhistoricos;
                            }
                            String histo = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID= " + idHistorico + " ").ToString();
                            if (idHistorico == 0 || histo == "NC")
                            { // Se não achar HISTORICO OU FOR nc colocar o Historico da Duplicata
                                idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICO from PAGAR where ID= " + idAPagar + " ").ToString());
                            }
                            if (idHistorico == 0)
                            {
                                idHistorico = clsInfo.zhistoricos;
                            }
                            row["IDHISTORICO"] = idHistorico;
                            idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOPAGAR from SITUACAOCOBRANCACOD where ID= '" + BaixarBoletoInfo.Codigo + "' ").ToString());
                            if (idCentroCusto == 0)
                            {
                                idCentroCusto = clsInfo.zcentrocustos;
                            }
                            String centro = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID= " + idCentroCusto + " ").ToString();
                            if (idCentroCusto == 0 || centro == "NC")
                            { // Se não achar HISTORICO OU FOR nc colocar o Historico da Duplicata
                                idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTO from PAGAR where ID= " + idAPagar + " ").ToString());
                            }
                            if (idCentroCusto == 0)
                            {
                                idCentroCusto = clsInfo.zcentrocustos;
                            }
                            row["IDCENTROCUSTO"] = idCentroCusto;
                            row["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;

                            dtPagando.Rows.Add(row);
                            if (BaixarBoletoInfo.Codigo == "01")
                            {
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("UPDATE PAGAR SET VALORBAIXANDO=@DIFERENCA " +
                                                        "WHERE ID = @ID", scn);
                                scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idAPagar;
                                scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = BaixarBoletoInfo.ValorLancamento;
                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();
                                bwrAPagar_Run();
                            }
                        }
                    }
                }
                bwrPagando_Run();
            }
        }

        private void rbnFornecedoresTodos_Click(object sender, EventArgs e)
        {
            gbxFornecedor.Visible = false;
            tbxCognome.Text = "";
            bwrAPagar_Run();
        }

        private void rbnFornecedoresUnico_Click(object sender, EventArgs e)
        {
            gbxFornecedor.Visible = true;
        }
        private void TrataCampos(Control ctl)
        {
            if (ctl.Name == tbxCognome.Name)
            {
                idFornecedorIndicado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME= '" + tbxCognome.Text + "' "));
                if (idFornecedorIndicado == 0)
                {
                    idFornecedorIndicado = clsInfo.zempresaclienteid;
                }
                tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID= " + idFornecedorIndicado);

                bwrAPagar_Run();
            }


            // Banco Destino
            if (clsInfo.znomegrid == btnIdBancoIntDes.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancoIntDes = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancoIntDes.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancoIntDesNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxBancoIntDes.Select();
                }
            }
            //
            if (ctl.Name == tbxBancoIntDes.Name)
            {
                idBancoIntDes = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA= " + clsParser.Int32Parse(tbxBancoIntDes.Text) + " "));
                if (idBancoIntDes == 0)
                {
                    idBancoIntDes = clsInfo.zbancoint;
                }
                tbxBancoIntDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID= " + idBancoIntDes);
                tbxBancoIntDesNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + idBancoIntDes);
            }
            //
            tbxDiferencaPagar.Text = clsParser.DecimalParse(tbxDiferencaPagar.Text).ToString("N2");
            tbxChequesDiferenca.Text = clsParser.DecimalParse(tbxChequesDiferenca.Text).ToString("N2");
            tbxTotalCheque.Text = clsParser.DecimalParse(tbxTotalCheque.Text).ToString("N2");
            

            tbxValorPrincipal.Text = clsParser.DecimalParse(tbxValorPrincipal.Text).ToString("N2");
            tbxValorPagando.Text = clsParser.DecimalParse(tbxValorPagando.Text).ToString("N2");
            tbxValorMultas.Text = clsParser.DecimalParse(tbxValorMultas.Text).ToString("N2");
            tbxValorJuros.Text = clsParser.DecimalParse(tbxValorJuros.Text).ToString("N2");
            tbxValorDesconto.Text = clsParser.DecimalParse(tbxValorDesconto.Text).ToString("N2");
            tbxValorTarifas.Text = clsParser.DecimalParse(tbxValorTarifas.Text).ToString("N2");
            tbxValorCartorio.Text = clsParser.DecimalParse(tbxValorCartorio.Text).ToString("N2");
            tbxTotalDeducoes.Text = clsParser.DecimalParse(tbxTotalDeducoes.Text).ToString("N2");
            tbxValorPagar.Text = clsParser.DecimalParse(tbxValorPagar.Text).ToString("N2");

            tbxChValor.Text = clsParser.DecimalParse(tbxChValor.Text).ToString("N2");

            tbxChequesSoma.Text = clsParser.DecimalParse(tbxChequesSoma.Text).ToString("N2");
            tbxChequesDiferenca.Text = clsParser.DecimalParse(tbxChequesDiferenca.Text).ToString("N2");
            tbxChequesDiferenca.Text = (clsParser.DecimalParse(tbxChequesSoma.Text) - clsParser.DecimalParse(tbxValorPagar.Text)).ToString("N2");
            tbxDiferencaPagar.Text = tbxChequesDiferenca.Text;

            // se possuir mais de um lançamento para baixar
            // travar a data da baixa
            if (dtPagando.Rows.Count > 0)
            {
                tbxDataBaixa.ReadOnly = true;
                tbxDataBaixa.BackColor = System.Drawing.Color.LemonChiffon; 
            }


            clsInfo.znomegrid = "";
        }

        private void btnIdBancoIntDes_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBancoIntDes.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancoIntDes, clsInfo.conexaosqlbanco);
            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }
        private void CriarCheque()
        {
            dtCheque = new DataTable();
            dtCheque.Columns.Add("POSICAO_CHEQUE", Type.GetType("System.Int32"));
            dtCheque.Columns.Add("IDFLUXO", Type.GetType("System.Int32"));
            dtCheque.Columns.Add("TIPOCHEQUE", Type.GetType("System.String"));
            dtCheque.Columns.Add("IDBANCOINTDES", Type.GetType("System.Int32"));
            dtCheque.Columns.Add("BANCOINTDES", Type.GetType("System.Int32"));
            dtCheque.Columns.Add("BANCOINTDESNOME", Type.GetType("System.String"));
            dtCheque.Columns.Add("IDBANCO", Type.GetType("System.Int32"));
            dtCheque.Columns.Add("BANCO", Type.GetType("System.Int32"));
            dtCheque.Columns.Add("BANCONOME", Type.GetType("System.String"));
            dtCheque.Columns.Add("NROCHEQUE", Type.GetType("System.Int32"));
            dtCheque.Columns.Add("VALOR", Type.GetType("System.Decimal"));
            dtCheque.Columns.Add("DATADEPOSITO", Type.GetType("System.DateTime"));
            dtCheque.Columns.Add("OBSERVACAO", Type.GetType("System.String"));
            
        }


        private void tspChequeIncluir_Click(object sender, EventArgs e)
        {
            posicao_Cheque = 0;
            ChequeCarregar();

        }

        private void tspChequeAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCheque.CurrentRow != null)
            {
                posicao_Cheque = Int32.Parse(dgvCheque.CurrentRow.Cells["posicao_cheque"].Value.ToString());
                ChequeCarregar();
            }

        }

        private void tspChequeImprimir_Click(object sender, EventArgs e)
        {

        }

        private void tspChequeRetornar_Click(object sender, EventArgs e)
        {
            tclPagarBaixaCheque.SelectedIndex = 0;
        }

        private void tspChequeExcluir_Click(object sender, EventArgs e)
        {

        }

        private void tspChequeImpCheque_Click(object sender, EventArgs e)
        {

        }

        private void tspChequeConfigImp_Click(object sender, EventArgs e)
        {

        }

        private void tspChequeSalvar_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja salvar e alterar?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (drt == DialogResult.Yes)
            {
                ChequeFillInfoToGrid();
            }
            else if (drt == DialogResult.Cancel)
            {
                return;
            }
            tclCheque.SelectedIndex = 0;
            gbxChequeRegistro.Visible = false;
            SomarCheque();
        }

        private void tspChequeRetornar1_Click(object sender, EventArgs e)
        {
            tclCheque.SelectedIndex = 0;
            gbxChequeRegistro.Visible = false;
        }

        private void bwrCheque_Run()
        {
            if (carregandoCheque == false)
            {
                carregandoCheque = true;

                bwrCheque = new BackgroundWorker();
                bwrCheque.DoWork += new DoWorkEventHandler(bwrCheque_DoWork);
                bwrCheque.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCheque_RunWorkerCompleted);
                bwrCheque.RunWorkerAsync();
            }
        }


        private void bwrCheque_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        private void bwrCheque_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvCheque.DataSource = dtCheque;
                clsGridHelper.MontaGrid2(dgvCheque, dtChequeColunas, true);

                dgvCheque.Columns["DATADEPOSITO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvCheque.Columns["VALOR"].DefaultCellStyle.Format = "N2";

                PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoCheque = false;

                tbxTotalCheque.Text = clsGridHelper.SomaGrid(dgvCheque, "VALOR").ToString("N2");
                tbxChequesSoma.Text = tbxTotalCheque.Text;

                if (carregandoPagando == false)
                {
                    bwrPagando_Run();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoCheque = false;
            }
        }
        void ChequeCarregar()
        {
            tclCheque.SelectedIndex = 1;
            gbxChequeRegistro.Visible = true;

            if (posicao_Cheque == 0)
            {
                posicao_Cheque = dtCheque.Rows.Count + 1;
                rbnChequeTipoCheque.Checked = true;
                tbxBancoIntDes.Text = "0";
                tbxBancoIntDesNome.Text = "";
                tbxChNroBanco.Text = "0";
                tbxChNomeBanco.Text = "";
                tbxChNroCheque.Text = "";
                tbxChValor.Text = "0";
                tbxChDataDeposito.Text = tbxDataBaixa.Text; // DateTime.Now.ToString("dd/MM/yyyy");
                tbxChObservacao.Text = "";
            }
            else
            {
                ChequeGridToInfo(posicao_Cheque);
            }
            // ChequeCampos(clsPecasPrecoInfo);

            if (rbnChequeTipoCheque.Checked == true)
            {
                rbnChequeTipoCheque.Select();
            }
            else
            {
                rbnChequeTipoDeposito.Select();
            }
        }
        void ChequeGridToInfo(Int32 posicao)
        {
            DataRow row = dtCheque.Select("posicao_cheque = " + posicao)[0];
            idFluxo = Int32.Parse(row["idfluxo"].ToString()); //dtCheque.Columns.Add("IDFLUXO", Type.GetType("System.Int32"));
            if (row["TIPOCHEQUE"].ToString().Substring(0,1) == "C")  //dtCheque.Columns.Add("IDFLUXO", Type.GetType("System.Int32"));
            {
                rbnChequeTipoCheque.Checked = true;
            }
            else
            {
                rbnChequeTipoDeposito.Checked = true;
            }
            idBancoIntDes = clsParser.Int32Parse(row["IDBANCOINTDES"].ToString()); //dtCheque.Columns.Add("IDBANCOINTDES", Type.GetType("System.Int32"));
            tbxBancoIntDes.Text = row["BANCOINTDES"].ToString();
            tbxBancoIntDesNome.Text = row["BANCOINTDESNOME"].ToString();
            idBanco = clsParser.Int32Parse(row["IDBANCO"].ToString());
            tbxChNroBanco.Text = row["BANCO"].ToString();
            tbxChNomeBanco.Text = row["BANCONOME"].ToString();
            tbxChNroCheque.Text = row["NROCHEQUE"].ToString();
            tbxChValor.Text = row["VALOR"].ToString();
            tbxChDataDeposito.Text = clsParser.DateTimeParse(row["DATADEPOSITO"].ToString()).ToString("dd/MM/yyyy");
            tbxChObservacao.Text = row["OBSERVACAO"].ToString();
        }
        void ChequeFillInfoToGrid()
        {
            DataRow row;
            DataRow[] rows = dtCheque.Select("posicao_cheque = " + posicao_Cheque);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtCheque.NewRow();
            }

            row["idfluxo"] = idFluxo;
            if (rbnChequeTipoCheque.Checked == true)
            {
                row["TIPOCHEQUE"] = "Cheque";
            }
            else
            {
                row["TIPOCHEQUE"] = "Deposito";
            }
            row["idBancoIntDes"] = idBancoIntDes;
            row["BANCOINTDES"] = tbxBancoIntDes.Text;
            row["BANCOINTDESNOME"] = tbxBancoIntDesNome.Text;
            row["IDBANCO"]=idBanco;
            row["BANCO"]= tbxChNroBanco.Text;
            row["BANCONOME"] = tbxChNomeBanco.Text;
            row["NROCHEQUE"]= clsParser.Int32Parse(tbxChNroCheque.Text);
            row["VALOR"] = clsParser.DecimalParse(tbxChValor.Text);
            row["DATADEPOSITO"] = clsParser.DateTimeParse(tbxChDataDeposito.Text);
            row["OBSERVACAO"] = tbxChObservacao.Text;
            if (rows.Length == 0)
            {
                row["posicao_cheque"] = posicao_Cheque;
                dtCheque.Rows.Add(row);

            }
        }

        private void dgvPagando_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvPagando.CurrentRow != null)
            {
                posicao_Pagando = clsParser.Int32Parse(dgvPagando.CurrentRow.Cells["POSICAO_PAGANDO"].Value.ToString());
                idPagando = clsParser.Int32Parse(dgvPagando.CurrentRow.Cells["IDPAGAR"].Value.ToString());
                if (dgvPagando.CurrentRow.Index > 0)
                {
                    id_Anterior1 = clsParser.Int32Parse(dgvPagando.Rows[dgvPagando.CurrentRow.Index - 1].Cells["POSICAO_PAGANDO"].Value.ToString());
                }
                else
                {
                    id_Anterior1 = 0;
                }
                if (dgvPagando.CurrentRow.Index < dgvPagando.Rows.Count - 1)
                {
                    id_Proximo1 = clsParser.Int32Parse(dgvPagando.Rows[dgvPagando.CurrentRow.Index + 1].Cells["POSICAO_PAGANDO"].Value.ToString());
                }
                else
                {
                    id_Proximo1 = 0;
                }
                // APAGAR O CONTAS A RECEBER VIRTUAL
                if (dgvPagando.CurrentRow.Cells["COBRANCACOD"].Value.ToString() == "01")
                {   // Todos os Lançamentos da Duplicata
                    Boolean terminou = false;
                    while (terminou == false)
                    {
                        if (dtPagando.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtPagando.Rows)
                            {
                                if (clsParser.Int32Parse(row["IDPAGAR"].ToString()) == idPagando)
                                {
                                    dtPagando.Select("POSICAO_PAGANDO =" + row["POSICAO_PAGANDO"].ToString())[0].Delete();
                                    terminou = false;
                                    break;
                                }
                                else
                                {
                                    terminou = true;
                                }

                            }
                        }
                        else
                        {
                            terminou = true;
                        }
                    }
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scd = new SqlCommand("UPDATE PAGAR SET VALORBAIXANDO=@DIFERENCA WHERE ID = @ID", scn);
                    scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idPagando;
                    scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = 0;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();
                    bwrAPagar_Run();

                }
                else
                {   // Apenas o Lançamento Indicado
                    //query = "select * from RPTRECEBERBAIXA WHERE POSICAO_RECEBENDO=" + clsParser.Int32Parse(dgvPagando.CurrentRow.Cells["POSICAO_RECEBENDO"].Value.ToString());

                    // DEVOLVER PARA O CONTAS A RECEBER SE FOR COD 01
                    if (dgvPagando.CurrentRow.Cells["COBRANCACOD"].Value.ToString() == "01")
                    {
                        dtPagando.Select("POSICAO_PAGANDO =" + dgvPagando.CurrentRow.Cells["POSICAO_PAGANDO"].Value.ToString())[0].Delete();
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("UPDATE PAGAR SET VALORBAIXANDO=@DIFERENCA WHERE ID = @ID", scn);
                        scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idPagando;
                        scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = 0;
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                        bwrAPagar_Run();
                    }
                    else
                    {
                        dtPagando.Select("POSICAO_PAGANDO =" + dgvPagando.CurrentRow.Cells["POSICAO_PAGANDO"].Value.ToString())[0].Delete();
                        bwrAPagar_Run();
                    }
                }
                posicao_Pagando = 0;
                bwrPagando_Run();
            }

        }
        private void SomarCheque()
        {
            tbxSaldoAPagar.Text =  clsGridHelper.SomaGrid(dgvAPagar, "VALORPAGAR").ToString("N2");
//            tbxSaldoBaixando.Text = clsGridHelper.SomaGrid(dgvPagando, "VALOR").ToString("N2");
            tbxSaldoBaixando.Text = tbxValorPrincipal.Text;
            tbxSaldo.Text = (clsParser.DecimalParse(tbxSaldoAPagar.Text) - clsParser.DecimalParse(tbxSaldoBaixando.Text)).ToString("N2");

            tbxChequesSoma.Text = clsGridHelper.SomaGrid(dgvCheque, "VALOR").ToString("N2");
            tbxTotalCheque.Text = tbxChequesSoma.Text;
            tbxChequesDiferenca.Text = (clsParser.DecimalParse(tbxChequesSoma.Text) - clsParser.DecimalParse(tbxValorPagar.Text)).ToString("N2");
            tbxDiferencaPagar.Text = tbxChequesDiferenca.Text;


        }

        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tbxPesquisa.Focus();
        }

        private void ckxPendencias_Click(object sender, EventArgs e)
        {
            if (ckxPendencias.Checked == true)
            {
                ckxPendencias.Text = "Sem as Pendencias";
            }
            else
            {
                ckxPendencias.Text = "Com as Pendencias";
            }
            bwrAPagar_Run();

        }
    }
}
