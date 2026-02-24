using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{ 
    public partial class frmReceberBaixaSemi : Form
    {
        
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        SqlConnection scn;
        SqlCommand scd;
        SqlDataAdapter sda;


        String query;
        SqlDataReader sdrFeriado;

        Int32 idBanco;
        Int32 idBancoInt;
        Int32 idBancoIntDes;

        DateTime databaixa;

        Boolean carregandoAReceber;
        DataTable dtAReceber;
        BackgroundWorker bwrAReceber;
        Int32 idAReceber;
        Int32 id_Anterior;
        Int32 id_Proximo;

        Decimal Saldo;

        Boolean carregandoRecebendo;
        DataTable dtRecebendo;
        BackgroundWorker bwrRecebendo;
        Int32 idRecebendo;
        Int32 posicao_Recebendo;
        Int32 id_Anterior1;
        Int32 id_Proximo1;

        clsReceberBLL clsReceberBLL;
        clsReceberInfo clsReceberInfo;
        String atevencimento;

        Int32 receber1_idcobrancacod; 
        Int32 receber1_idcobrancahis;
        Int32 receber1_idhistoricocob;
        Int32 receber1_idcentrocustocob;

        Int32 receber1_idcobrancacodDesp;
        Int32 receber1_idhistoricocobDesp;
        Int32 receber1_idcentrocustocobDesp;

        Int32 receber1_idcobrancahisDesp;

        // Contas Recebidas
        Int32 idCliente;
        BackgroundWorker bwrRecebida;
        Int32 idRecebida;
        Boolean carregandoRecebida;
        DataTable dtRecebida;

        clsRecebidaBLL clsRecebidaBLL;
        clsRecebidaInfo clsRecebidaInfo;

        Int32 recebida_IdCobrancaCodRec;
        Int32 recebida_IdCobrancaHisRec;

        Int32 recebida_IdHistoricoCobrancaRec;
        Int32 recebida_IdCentroCustoCobrancaRec;

        // Varaveis utilizadas para Baixar
        Int32 vidDuplicata;
        String vDocumento;
        Int32  vidFormaPagto;
        String vFormaPagto;
        Int32 vidBancoConta;
        Int32 vBancoConta;
        String vnrocheque = "";
        Int32 vconferido = 0;
        String vcomplemento = "";
        String vobserva = "";
        String vboletonro = "";
        String vnotadoc = ""; // É O DOCUMENTO
        Int32 viddocumento = 0;
        Int32 vidnotafiscal = 0;
        String vnfvendanumero = "";
        String vNroLote = "";
        Int32 vidcliente = 0;
        String vClienteCognome = "";
        Int32 vposicao = 0;
        Int32 vposicaofim = 0;
        String vcomobaixou = "";
        Int32 vidrecebernfv = 0;
        String vTipoBaixa = "";


        // Colunas do Contas a Receber (O que vai ser baixado)
        GridColuna[] dtAReceberColunas = new GridColuna[]
                                        {
                                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "Filial", 20, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PF", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Emissao", "EMISSAO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("VenctoPrev", "VENCIMENTOPREV", 65, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Titulo", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Liq", "VALORLIQUIDO", 80, false, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vendedor", "VENDEDOR", 70, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Forma Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Bco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vl.Juros", "VALORJUROS", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Multa", "VALORMULTA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Desconto", "VALORDESCONTO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Até Venc", "ATEVENCIMENTO", 25, true, DataGridViewContentAlignment.MiddleCenter)
                                        };
        GridColuna[] dtAReceberColunasFerpal = new GridColuna[]
                                {
                                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "Filial", 20, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PF", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Emissao", "EMISSAO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Cliente", "CLIENTE_COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Telefone", "TELEFONE", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("VenctoPrev", "VENCIMENTOPREV", 65, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Titulo", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Liq", "VALORLIQUIDO", 80, false, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vendedor", "VENDEDOR", 70, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Forma Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Bco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vl.Juros", "VALORJUROS", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Multa", "VALORMULTA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Desconto", "VALORDESCONTO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Até Venc", "ATEVENCIMENTO", 25, true, DataGridViewContentAlignment.MiddleCenter)
                                };


        // Colunas do Contas a Receber (O que esta sendo baixado)
        GridColuna[] dtRecebendoColunas = new GridColuna[]
                                        {
                                            new GridColuna("Posicao", "POSICAO_RECEBENDO", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdReceber", "IDRECEBER", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdRecebida", "IDRECEBIDA", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "FILIAL", 20, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Titulo", "VALORTITULO", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Pagto", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Boleto", "BOLETONUMERO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D/C", "DEBCRED", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cod", "COBRANCACOD", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("His", "COBRANCAHIS", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("CobrancaNome", "COBRANCANOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vl.Comissao", "VALORCOMISSAO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Id Historico", "IDHISTORICO", 1, false  , DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Id CentroCusto", "IDCENTROCUSTO", 1, false, DataGridViewContentAlignment.MiddleRight)
                                        };
        // Colunas do Contas a Receber (O que esta sendo baixado Ferpal)
        GridColuna[] dtRecebendoColunasFerpal = new GridColuna[]
                                        {
                                            new GridColuna("Posicao", "POSICAO_RECEBENDO", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdReceber", "IDRECEBER", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("IdRecebida", "IDRECEBIDA", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "FILIAL", 20, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Cliente", "CLIENTE_COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Telefone", "TELEFONE", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Titulo", "VALORTITULO", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Pagto", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Boleto", "BOLETONUMERO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D/C", "DEBCRED", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cod", "COBRANCACOD", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("His", "COBRANCAHIS", 25, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("CobrancaNome", "COBRANCANOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vl.Comissao", "VALORCOMISSAO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Id Historico", "IDHISTORICO", 1, false  , DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Id CentroCusto", "IDCENTROCUSTO", 1, false, DataGridViewContentAlignment.MiddleRight)
                                        };


        // Colunas do Contas a Receber (O que esta sendo baixado)
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

        public frmReceberBaixaSemi()
        {
            InitializeComponent();
        }

        public void Init()
        {
            carregandoAReceber = false;

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tab_bancos order by codigo", tbxBanco);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBancoInt);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaocobrancacod order by codigo", tbxCobrancaCod);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaocobrancacod order by codigo", tbxCobrancaCodDesp);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxClienteCognomeRecebido);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaocobrancacod order by codigo", tbxCobrancaCodRec);

            clsReceberBLL = new clsReceberBLL();

            clsRecebidaBLL = new clsRecebidaBLL();
            
            rbnTotalUnico.Checked = true;
            rbnTotalUnico.BackColor = Color.Green;
        }

        private void frmReceberBaixaSemi_Load(object sender, EventArgs e)
        {
            idBancoInt = clsInfo.zbancoint;
            tbxDataBaixa.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            CalcularDataBaixa();
            tbxBancoInt.Select();
            tbxBancoInt.SelectAll();
            Saldo = 0;
            posicao_Recebendo = 0;
            bwrAReceber_Run();
            CriarRecebendoBaixa();
            //
            tbxDataRecebida.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
        }

        private void frmReceberBaixaSemi_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void tspIncluirDesp_Click(object sender, EventArgs e)
        {
            if (dgvAReceber.CurrentRow != null)
            {
                idAReceber = (Int32)dgvAReceber.CurrentRow.Cells[0].Value;
                ReceberCarregar();
                gbxTitulo.Visible = true;
                tclReceberSemiAutomatica.SelectedIndex = 1;
                tbxCobrancaCod.Select();
                tbxCobrancaCod.SelectAll();

            }
            else
            {
                MessageBox.Show("Indique o titulo que deseja incluir lançamento !!");
            }

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvAReceber.CurrentRow != null)
            {
                idAReceber = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvAReceber.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvAReceber.Rows[dgvAReceber.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvAReceber.CurrentRow.Index < dgvAReceber.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvAReceber.Rows[dgvAReceber.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
                // Efetuar o Processo             
                gbxTitulo.Visible = true;
                tclReceberSemiAutomatica.SelectedIndex = 3;
                tbxCobrancaCodDesp.Select();
                tbxCobrancaCodDesp.SelectAll();

            }
        }

        private void tspIncluirRecebida_Click(object sender, EventArgs e)
        {
            // Efetuar o Processo Direto pois não depende de nenhum registro da primeira tela             
            gbxRecebido.Visible = true;
            tclReceberSemiAutomatica.SelectedIndex = 2;
            tbxClienteCognomeRecebido.Select();
            tbxClienteCognomeRecebido.SelectAll();

        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            String informa;
            if (rbnTotalUnico.Checked == true)
            {
                informa = "Será efetuado num único Lançamento dentro do Aplibank ";
                vTipoBaixa = "D"; // Diversas  
            }
            else
            {
                informa = "Cada Vencimento de Duplicata sera Lançado separadamente na respectiva data de Vencimento não respeitando a Data da Baixa colocada acima. ";
                vTipoBaixa = "U"; // Unica
            }
            DialogResult drt;
            drt = MessageBox.Show("Atenção a Baixa fara os seguintes procedimentos : "  + Environment.NewLine + 
                 " 1. O Lançamento/Duplicata vai sair do Contas a Receber e ir Para o Contas Recebidas " + Environment.NewLine +
                 " 2. Vai incluir estes valores no Aplibank na Cta " + tbxBancoIntDes.Text + Environment.NewLine +
                 " 3. " + informa + Environment.NewLine + 
                 " Deseja Prosseguir ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                // Criar o numero do Lote
                DateTime databaixa = DateTime.Parse(tbxDataBaixa.Text);
                DateTime datacredito = DateTime.Parse(tbxDataCredito.Text);
                //TimeSpan Compensar;

                Int32 XNro = 0;
                Int32 XDuplicata = 0;
                Int32 XPosicao = 0;

                XNro = 1;
                vNroLote = "PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + XNro.ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");

                using (TransactionScope tse = new TransactionScope())
                {
                    foreach (DataRow row in dtRecebendo.Rows)
                    {
                        /////////////////
                        /// Baixar
                        if (rbnTotalIndividual.Checked == true)
                        {  // Cada Titulo tera um lançamento individual na baixa do aplibank
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
                        }
                        /////////////////
                        if (clsParser.Int32Parse(row["IDRECEBIDA"].ToString()) > 0)
                        {
                            vDocumento = "RECEBIDA";
                            vidDuplicata = clsParser.Int32Parse(row["IDRECEBIDA"].ToString());
                        }
                        else
                        {
                            vDocumento = "RECEBER";
                            vidDuplicata = clsParser.Int32Parse(row["IDRECEBER"].ToString());
                        }
                        if (vDocumento == "RECEBER")
                        {
                            // 1. VERIFICAR SE ESTA DANDO BAIXA DE UM CHEQUE
                            vidFormaPagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDFORMAPAGTO FROM RECEBER where ID=" + vidDuplicata + " "));
                            vFormaPagto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where ID=" + vidFormaPagto + " ");
                            if (vFormaPagto == "CH")
                            {
                                vidBancoConta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDBANCO from RECEBER where ID=" + vidDuplicata + " "));
                                vBancoConta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + vidBancoConta).ToString());
                                vboletonro = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETONRO from RECEBER where ID=" + vidDuplicata + " ");
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
                            vobserva = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select OBSERVA from RECEBER where ID=" + vidDuplicata + " ");
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
                            viddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from RECEBER where ID=" + vidDuplicata + " "));
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

                            vidnotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDNOTAFISCAL FROM RECEBER where id=" + vidDuplicata));
                            vidrecebernfv = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDRECEBERNFV FROM RECEBER where id=" + vidDuplicata));

                            if (vnotadoc == "CTO")
                            {
                                vnfvendanumero = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM CONTRATO where id=" + vidnotafiscal);
                            }
                            else if (vnotadoc == "PED")
                            {
                                vnfvendanumero = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM PEDIDO where id=" + vidnotafiscal);
                            }
                            else
                            {
                                vnfvendanumero = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFVENDA where id=" + vidnotafiscal);
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
                            vidcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCLIENTE FROM RECEBER where id=" + vidDuplicata));
                            vClienteCognome = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM CLIENTE where id=" + vidcliente);
                            //vClienteCognome = vClienteCognome + " [" + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + vidcliente) + "]";
                            vposicao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT POSICAO FROM RECEBER where id=" + vidDuplicata));
                            vposicaofim = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT POSICAOFIM FROM RECEBER where id=" + vidDuplicata));

                        }
                        else
                        {
                            // Pegar o Id do receber que esta dentro do Contas Recebida
                            vidDuplicata = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDRECEBER FROM RECEBIDA where ID=" + vidDuplicata + " "));                            
                        }
                        String DataBaixa;
                        String DataCredito;
                        if (vTipoBaixa == "D")
                        {
                            DataBaixa = tbxDataBaixa.Text;
                            DataCredito = tbxDataCredito.Text;
                        }
                        else
                        {
                            DataBaixa = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                            DataCredito = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                        }


                        clsFinanceiro.BaixarBanco("RECEBER", vidDuplicata, vNroLote, vboletonro, idBancoIntDes, clsParser.Int32Parse(row["IDHISTORICO"].ToString()),
                                                  clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString()), clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString()),
                                                  DateTime.Parse(DataBaixa), DateTime.Parse(DataCredito),
                                                  clsParser.DecimalParse(row["VALOR"].ToString()),
                                                  row["cobrancacod"].ToString(), row["DEBCRED"].ToString(), vobserva, vClienteCognome,vDocumento,
                                                  vnotadoc, vnfvendanumero, vposicao, vposicaofim, vFormaPagto.Substring(0, 2), clsInfo.zusuario, vnrocheque, vconferido, vTipoBaixa);
                    }
                    // Transferir para o Contas Recebidas
                    foreach (DataRow row in dtRecebendo.Rows)
                    {   
                        if (clsParser.Int32Parse(row["IDRECEBIDA"].ToString()) > 0)
                        {
                            vDocumento = "RECEBIDA";
                            vidDuplicata = clsParser.Int32Parse(row["IDRECEBIDA"].ToString());
                        }
                        else
                        {
                            vDocumento = "RECEBER";
                            vidDuplicata = clsParser.Int32Parse(row["IDRECEBER"].ToString());
                        }

                        vcomobaixou = vFormaPagto + "na Cta: " +  tbxBancoIntDes.Text;

                        clsFinanceiro.TransferirPago("RECEBER", vidDuplicata, tbxDataBaixa.Text,
                            tbxDataCredito.Text,
                            clsParser.Int32Parse(row["COBRANCACOD"].ToString()),
                            clsParser.Int32Parse(row["COBRANCAHIS"].ToString()),
                            clsParser.DecimalParse(row["VALOR"].ToString()),
                            row["DEBCRED"].ToString(),
                            clsParser.Int32Parse(row["IDHISTORICO"].ToString()),
                            clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString()),
                            clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString()),
                            vcomobaixou,
                            clsParser.DecimalParse(row["VALORCOMISSAO"].ToString()),
                            clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString()),
                            clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString()));

                        if (vDocumento == "RECEBER")
                        {
                            if (row["COBRANCACOD"].ToString() == "00" || row["COBRANCACOD"].ToString() == "01" )
                            {
                                // Transferir as Observações do Contas a Receber para o Recebida
                                clsFinanceiro.TransferirPagoObserva("RECEBER", vidDuplicata);
                            }
                        }

                    }
                    // Ultima fase marcar que foi pago e excluir a duplicata do contas a receber
                    foreach (DataRow row in dtRecebendo.Rows)
                    {
                        if (clsParser.Int32Parse(row["IDRECEBER"].ToString()) > 0)
                        {
                            vidDuplicata = clsParser.Int32Parse(row["IDRECEBER"].ToString());

                            viddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from RECEBER where ID=" + vidDuplicata + " "));
                            vnotadoc = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + viddocumento);

                            vidnotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDNOTAFISCAL FROM RECEBER where id=" + vidDuplicata));
                            vidrecebernfv = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDRECEBERNFV FROM RECEBER where id=" + vidDuplicata));

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
                            else if (vnotadoc == "PED")
                            {
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("UPDATE PEDIDORECEBER SET PAGOU=@PAGOU WHERE ID=@ID ", scn);
                                scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = vidrecebernfv;
                                scd.Parameters.AddWithValue("@PAGOU", SqlDbType.NVarChar).Value = "S";
                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();
                            }
                            else

                            {
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("UPDATE NFVENDARECEBER SET PAGOU=@PAGOU WHERE ID=@ID ", scn);
                                scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = vidrecebernfv;
                                scd.Parameters.AddWithValue("@PAGOU", SqlDbType.NVarChar).Value = "S";
                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();
                            }
                            // Excluir as Duplicatas se foi Total  // Todas baixadas aqui é baixa total não tem Parcial
                            // Apagar observação do receber
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scd = new SqlCommand("delete receberobserva where idduplicata=@id", scn);
                            scd.Parameters.Add("@id", SqlDbType.Int).Value = vidDuplicata;
                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();
                            // Apagar os itens do receber
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scd = new SqlCommand("delete receber01 where idduplicata=@id", scn);
                            scd.Parameters.Add("@id", SqlDbType.Int).Value = vidDuplicata;
                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();
                            // Apagar o receber
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scd = new SqlCommand("delete receber where id=@id", scn);
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

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("UPDATE RECEBER SET VALORBAIXANDO=@DIFERENCA " , scn);
            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idAReceber;
            scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = 0;
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

            this.Close();
        }
        private void btnIdBancoInt_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBancoInt.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancoInt, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }
        private void btnIdBancoIntDes_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBancoIntDes.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancoIntDes, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
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
        private void TrataCampos(Control ctl)
        {
            if (clsInfo.znomegrid == btnIdBancoInt.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancoInt = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancoInt.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancoIntNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();

                        tbxTarifaBoleto.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select TXBOLETO from BANCOS where ID= " + idBancoInt)).ToString("N2");
                        tbxTarifaCartorio.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select TXCARTORIO from BANCOS where ID= " + idBancoInt)).ToString("N2");
                        tbxDiasCompensacao.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select COMPENSAR from BANCOS where ID= " + idBancoInt)).ToString("N0");

                        idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancoInt));
                        tbxBanco.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco)).ToString("N0");
                        tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);

                        tbxDataCredito.Text = (clsParser.DateTimeParse(tbxDataBaixa.Text).AddDays(clsParser.Int32Parse(tbxDiasCompensacao.Text))).ToString("dd/MM/yyyy");
                        
                        CalcularDataBaixa();
                        Saldo = 0;
                        bwrAReceber_Run();

                    }
                    idBancoIntDes = idBancoInt;
                    tbxBancoIntDes.Text = tbxBancoInt.Text;
                    tbxBancoIntDesNome.Text = tbxBancoIntNome.Text;
                    tbxBancoInt.Select();
                }

            }
            if (ctl.Name == tbxBancoInt.Name)
            {
                idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA= " + clsParser.Int32Parse(tbxBancoInt.Text) + " "));
                if (idBancoInt == 0)
                {
                    idBancoInt = clsInfo.zbancoint;
                }
                tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID= " + idBancoInt);
                tbxBancoIntNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + idBancoInt);
                tbxTarifaBoleto.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select TXBOLETO from BANCOS where ID= " + idBancoInt)).ToString("N2");
                tbxTarifaCartorio.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select TXCARTORIO from BANCOS where ID= " + idBancoInt)).ToString("N2");
                tbxDiasCompensacao.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select COMPENSAR from BANCOS where ID= " + idBancoInt)).ToString("N0");

                tbxDataCredito.Text = (clsParser.DateTimeParse(tbxDataBaixa.Text).AddDays(clsParser.Int32Parse(tbxDiasCompensacao.Text))).ToString("dd/MM/yyyy");
                CalcularDataBaixa();

                idBancoIntDes = idBancoInt;
                tbxBancoIntDes.Text = tbxBancoInt.Text;
                tbxBancoIntDesNome.Text = tbxBancoIntNome.Text;

                idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancoInt));
                tbxBanco.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco)).ToString("N0");
                tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);

                Saldo = 0;

                bwrAReceber_Run();


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
            tbxDataCredito.Text = (clsParser.DateTimeParse(tbxDataBaixa.Text).AddDays(clsParser.Int32Parse(tbxDiasCompensacao.Text))).ToString("dd/MM/yyyy");
            if (ctl.Name == tbxDataBaixa.Name)
            {
                tbxDataBaixa.Text = clsParser.DateTimeParse(tbxDataBaixa.Text).ToString("dd/MM/yyyy");
                CalcularDataBaixa();
            }
            if (ctl.Name == tbxDataCredito.Name)
            {
                tbxDataCredito.Text = clsParser.DateTimeParse(tbxDataCredito.Text).ToString("dd/MM/yyyy");
                CalcularDataBaixa();
            }
            if (ctl.Name == tbxDiasCompensacao.Name)
            {
                tbxDataCredito.Text = clsParser.Int32Parse(tbxDataCredito.Text).ToString();
                CalcularDataBaixa();
            }
            // Incluindo Despesa no Titulo a Receber
            if (clsInfo.znomegrid == btnIdCobrancaCod.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        receber1_idcobrancacod = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCobrancaCod.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCobrancaNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        receber1_idhistoricocob = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod));
                        tbxHistoricoCobranca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from historicos where ID= " + receber1_idhistoricocob);
                        receber1_idcentrocustocob = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod));
                        tbxCentroCustoCobranca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from centrocustos where ID= " + receber1_idcentrocustocob);
                    }
                    tbxCobrancaCod.Select();
                }
            }

            if (ctl.Name == tbxCobrancaCod.Name)
            {
                receber1_idcobrancacod = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO= '" + tbxCobrancaCod.Text + "' "));
                if (receber1_idcobrancacod == 0)
                {
                    receber1_idcobrancacod = clsInfo.zsituacaocobrancacod;
                }
                tbxCobrancaCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod);
                tbxCobrancaNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod);
                receber1_idhistoricocob = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod));
                tbxHistoricoCobranca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from historicos where ID= " + receber1_idhistoricocob);
                receber1_idcentrocustocob = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod));
                tbxCentroCustoCobranca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from centrocustos where ID= " + receber1_idcentrocustocob);

                // Codigo = 01/02/03 = credito !!
                if (tbxCobrancaCod.Text == "01" || tbxCobrancaCod.Text == "02" || tbxCobrancaCod.Text == "03")
                {
                    tbxCobrancaDebCred.Text = "C";
                }
                else
                {
                    tbxCobrancaDebCred.Text = "D";
                }
            }
            if (clsInfo.znomegrid == btnIdCobrancaHis.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        receber1_idcobrancahis = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCobrancaHis.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCobrancaNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod);
                        tbxCobrancaNome.Text += " - " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxCobrancaHis.Select();
                }
            }

            if (ctl.Name == tbxCobrancaHis.Name)
            {
                receber1_idcobrancahis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD1 where IDCOBRANCACOD = " + receber1_idcobrancacod + " AND CODIGO= '" + tbxCobrancaHis.Text + "' "));
                if (receber1_idcobrancahis > 0)
                {
                    tbxCobrancaHis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD1 where ID= " + receber1_idcobrancahis);
                    tbxCobrancaNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacod);
                    tbxCobrancaNome.Text += " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD1 where ID= " + receber1_idcobrancahis);
                }
                else
                {
                    tbxCobrancaHis.Text = "";
                }
            }
            //
            if (clsInfo.znomegrid == btnIdHistoricoCobranca.Name)
            {
                if (clsInfo.zrow != null)
                {

                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        receber1_idhistoricocob = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxHistoricoCobranca.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    }
                    tbxHistoricoCobranca.Select();
                    tbxHistoricoCobranca.SelectAll();
                }
            }
            if (ctl.Name == tbxHistoricoCobranca.Name)
            {
                receber1_idhistoricocob = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT ID FROM HISTORICOS where CODIGO='" + tbxHistoricoCobranca.Text + "' "));
                if (receber1_idhistoricocob == 0)
                {
                    receber1_idhistoricocob = clsInfo.zhistoricos;
                }
                tbxHistoricoCobranca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + receber1_idhistoricocob);
            }
            if (clsInfo.znomegrid == btnIdCentroCustoCobranca.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        receber1_idcentrocustocob = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCentroCustoCobranca.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    }
                }
                tbxCentroCustoCobranca.Select();
                tbxCentroCustoCobranca.SelectAll();
            }
            if (ctl.Name == tbxCentroCustoCobranca.Name)
            {
                receber1_idcentrocustocob = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCusto.Text + "' "));
                if (receber1_idcentrocustocob == 0)
                {
                    receber1_idcentrocustocob = clsInfo.zcentrocustos;
                }
                tbxCentroCustoCobranca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + receber1_idcentrocustocob);
            }
            //
            // Incluindo Despesa no Titulo a Receber
            if (clsInfo.znomegrid == btnIdCobrancaCodDes.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        receber1_idcobrancacodDesp = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCobrancaCodDesp.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCobrancaNomeDes.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        receber1_idhistoricocobDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacodDesp));
                        tbxHistoricoCobrancaDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from historicos where ID= " + receber1_idhistoricocob);
                        receber1_idcentrocustocobDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacodDesp));
                        tbxCentroCustoCobrancaDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from centrocustos where ID= " + receber1_idcentrocustocob);
                    }
                    tbxCobrancaCodDesp.Select();
                }
            }

            if (ctl.Name == tbxCobrancaCodDesp.Name)
            {
                receber1_idcobrancacodDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO= '" + tbxCobrancaCodDesp.Text + "' "));
                if (receber1_idcobrancacodDesp == 0)
                {
                    receber1_idcobrancacodDesp = clsInfo.zsituacaocobrancacod;
                }
                tbxCobrancaCodDesp.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacodDesp);
                tbxCobrancaNomeDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacodDesp);
                receber1_idhistoricocobDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacodDesp));
                tbxHistoricoCobrancaDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from historicos where ID= " + receber1_idhistoricocobDesp);
                receber1_idcentrocustocobDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTORECEBER from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacodDesp));
                tbxCentroCustoCobrancaDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from centrocustos where ID= " + receber1_idcentrocustocobDesp);

                // Codigo = 01/02/03 = credito !!
                if (tbxCobrancaCodDesp.Text == "01" || tbxCobrancaCodDesp.Text == "02" || tbxCobrancaCodDesp.Text == "03")
                {
                    tbxCobrancaDebCredDes.Text = "C";
                }
                else
                {
                    tbxCobrancaDebCredDes.Text = "D";
                }
            }
            if (clsInfo.znomegrid == btnIdCobrancaHisDes.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsInfo.zrow.Cells["ID"].Value.ToString() != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            receber1_idcobrancahisDesp = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                            tbxCobrancaHisDes.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                            tbxCobrancaNomeDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancahisDesp);
                            tbxCobrancaNomeDes.Text += " - " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        }
                    }
                    tbxCobrancaHisDes.Select();
                }
            }

            if (ctl.Name == tbxCobrancaHisDes.Name)
            {
                receber1_idcobrancahisDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD1 where IDCOBRANCACOD = " + receber1_idcobrancacod + " AND CODIGO= '" + tbxCobrancaHisDes.Text + "' "));
                if (receber1_idcobrancahisDesp > 0)
                {
                    tbxCobrancaHisDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD1 where ID= " + receber1_idcobrancahisDesp);
                    tbxCobrancaNomeDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + receber1_idcobrancacodDesp);
                    tbxCobrancaNomeDes.Text += " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD1 where ID= " + receber1_idcobrancahisDesp);
                }
                else
                {
                    tbxCobrancaHisDes.Text = "";
                }
            }
            //
            if (clsInfo.znomegrid == btnIdHistoricoCobrancaDes.Name)
            {
                if (clsInfo.zrow != null)
                {

                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        receber1_idhistoricocobDesp = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxHistoricoCobrancaDes.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    }
                    tbxHistoricoCobrancaDes.Select();
                    tbxHistoricoCobrancaDes.SelectAll();
                }
            }
            if (ctl.Name == tbxHistoricoCobrancaDes.Name)
            {
                receber1_idhistoricocobDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT ID FROM HISTORICOS where CODIGO='" + tbxHistoricoCobrancaDes.Text + "' "));
                if (receber1_idhistoricocobDesp == 0)
                {
                    receber1_idhistoricocobDesp = clsInfo.zhistoricos;
                }
                tbxHistoricoCobrancaDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + receber1_idhistoricocobDesp);
            }
            if (clsInfo.znomegrid == btnIdCentroCustoCobrancaDes.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        receber1_idcentrocustocobDesp = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCentroCustoCobrancaDes.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    }
                }
                tbxCentroCustoCobrancaDes.Select();
                tbxCentroCustoCobrancaDes.SelectAll();
            }
            if (ctl.Name == tbxCentroCustoCobrancaDes.Name)
            {
                receber1_idcentrocustocobDesp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCustoCobrancaDes.Text + "' "));
                if (receber1_idcentrocustocobDesp == 0)
                {
                    receber1_idcentrocustocobDesp = clsInfo.zcentrocustos;
                }
                tbxCentroCustoCobrancaDes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + receber1_idcentrocustocobDesp);
            }
            // Contas Recebidas
            if (clsInfo.znomegrid == btnIdCliente.Name)
            {
                if (clsInfo.zrow != null)
                {
                    idCliente = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClienteCognomeRecebido.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                    //tbxClienteNomeRecebido.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "NOME", "ID", idCliente);
                    tbxClienteNomeRecebido.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,"select nome from cliente where id = "+ idCliente,"0");
                    tbxClienteCognomeRecebido.Select();
                    tbxClienteCognomeRecebido.SelectAll();
                }
            }

            if (ctl.Name == tbxClienteCognomeRecebido.Name)
            {
                idCliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CLIENTE where COGNOME='" + tbxClienteCognomeRecebido.Text + "' "));
                if (idCliente == 0)
                {
                    idCliente = clsInfo.zempresaclienteid;
                }
                tbxClienteCognomeRecebido.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idCliente);
                tbxClienteNomeRecebido.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM CLIENTE where id=" + idCliente);

                bwrRecebida_Run();
            }
            //
            if (ctl.Name == tbxDataRecebida.Name)
            {
                tbxDataRecebida.Text = clsParser.DateTimeParse(tbxDataRecebida.Text).ToString("dd/MM/yyyy");
                bwrRecebida_Run();
            }

            // Incluindo Despesa no Titulo do Contas Recebidas
            if (clsInfo.znomegrid == btnIdCobrancaCodRec.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida_IdCobrancaCodRec = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCobrancaCodRec.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCobrancaNomeRec.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        recebida_IdHistoricoCobrancaRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICORECEBER from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaCodRec));
                        tbxHistoricoCobrancaRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from historicos where ID= " + recebida_IdHistoricoCobrancaRec);
                        recebida_IdCentroCustoCobrancaRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTORECEBER from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaCodRec));
                        tbxCentroCustoCobrancaRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from centrocustos where ID= " + recebida_IdCentroCustoCobrancaRec);
                    }
                    tbxCobrancaCodRec.Select();
                }
            }

            if (ctl.Name == tbxCobrancaCodRec.Name)
            {
                recebida_IdCobrancaCodRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO= '" + tbxCobrancaCodRec.Text + "' "));
                if (recebida_IdCobrancaCodRec == 0)
                {
                    recebida_IdCobrancaCodRec = clsInfo.zsituacaocobrancacod;
                }
                tbxCobrancaCodRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaCodRec);
                tbxCobrancaNomeRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaCodRec);
                recebida_IdHistoricoCobrancaRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICORECEBER from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaCodRec));
                tbxHistoricoCobrancaRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from historicos where ID= " + recebida_IdHistoricoCobrancaRec);
                recebida_IdCentroCustoCobrancaRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTORECEBER from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaCodRec));
                tbxCentroCustoCobrancaRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from centrocustos where ID= " + recebida_IdCentroCustoCobrancaRec);

                // Codigo = 01/02/03 = credito !!
                if (tbxCobrancaCodRec.Text == "01" || tbxCobrancaCodRec.Text == "02" || tbxCobrancaCodRec.Text == "03")
                {
                    tbxCobrancaDebCredRec.Text = "C";
                }
                else
                {
                    tbxCobrancaDebCredRec.Text = "D";
                }
            }
            if (clsInfo.znomegrid == btnIdCobrancaHisRec.Name)
            {
                if (clsInfo.zrow != null && clsInfo.zrow.Index != -1)
                {
                    if (clsInfo.zrow.Cells["ID"].Value.ToString() != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            recebida_IdCobrancaHisRec = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                            tbxCobrancaHisRec.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                            tbxCobrancaNomeRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaHisRec);
                            tbxCobrancaNomeRec.Text += " - " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        }
                    }
                    tbxCobrancaHisRec.Select();
                }
            }

            if (ctl.Name == tbxCobrancaHisRec.Name)
            {
                recebida_IdCobrancaHisRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD1 where IDCOBRANCACOD = " + recebida_IdCobrancaCodRec + " AND CODIGO= '" + tbxCobrancaHisRec.Text + "' "));
                if (recebida_IdCobrancaHisRec > 0)
                {
                    tbxCobrancaHisRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD1 where ID= " + recebida_IdCobrancaCodRec);
                    tbxCobrancaNomeRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID= " + recebida_IdCobrancaCodRec);
                    tbxCobrancaNomeRec.Text += " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD1 where ID= " + recebida_IdCobrancaHisRec);
                }
                else
                {
                    tbxCobrancaHisDes.Text = "";
                }
            }
            //
            if (clsInfo.znomegrid == btnIdHistoricoCobrancaRec.Name)
            {
                if (clsInfo.zrow != null)
                {

                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida_IdHistoricoCobrancaRec = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxHistoricoCobrancaRec.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    }
                    tbxHistoricoCobrancaRec.Select();
                    tbxHistoricoCobrancaRec.SelectAll();
                }
            }
            if (ctl.Name == tbxHistoricoCobrancaRec.Name)
            {
                recebida_IdHistoricoCobrancaRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT ID FROM HISTORICOS where CODIGO='" + tbxHistoricoCobrancaRec.Text + "' "));
                if (recebida_IdHistoricoCobrancaRec == 0)
                {
                    recebida_IdHistoricoCobrancaRec = clsInfo.zhistoricos;
                }
                tbxHistoricoCobrancaRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + recebida_IdHistoricoCobrancaRec);
            }
            if (clsInfo.znomegrid == btnIdCentroCustoCobrancaRec.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida_IdCentroCustoCobrancaRec = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCentroCustoCobrancaRec.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    }
                }
                tbxCentroCustoCobrancaRec.Select();
                tbxCentroCustoCobrancaRec.SelectAll();
            }
            if (ctl.Name == tbxCentroCustoCobrancaRec.Name)
            {
                recebida_IdCentroCustoCobrancaRec = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCustoCobrancaRec.Text + "' "));
                if (recebida_IdCentroCustoCobrancaRec == 0)
                {
                    recebida_IdCentroCustoCobrancaRec = clsInfo.zcentrocustos;
                }
                tbxCentroCustoCobrancaRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + recebida_IdCentroCustoCobrancaRec);
            }
            //
            tbxCobrancaValor.Text = clsParser.DecimalParse(tbxCobrancaValor.Text).ToString("N2");
            tbxComissaoVendedor.Text = clsParser.DecimalParse(tbxComissaoVendedor.Text).ToString("N2");
            tbxComissaoGerencia.Text = clsParser.DecimalParse(tbxComissaoGerencia.Text).ToString("N2");
            tbxCobrancaValorDes.Text = clsParser.DecimalParse(tbxCobrancaValorDes.Text).ToString("N2");
            tbxCobrancaValorRec.Text = clsParser.DecimalParse(tbxCobrancaValorRec.Text).ToString("N2");

            clsInfo.znomegrid = "";
        }
        private void rbnTotalIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnTotalUnico.Checked == true)
            {
                rbnTotalUnico.BackColor =  Color.Green;
                rbnTotalIndividual.BackColor = SystemColors.Control;
            }
            else if(rbnTotalIndividual.Checked == true)
            {
                rbnTotalIndividual.BackColor = Color.Green;
                rbnTotalUnico.BackColor = SystemColors.Control; 
            }
        }

        private void CalcularDataBaixa()
        {
            databaixa = clsParser.DateTimeParse(tbxDataBaixa.Text); 

            Boolean terminou = false;
            while (terminou == false)
            {
                if (databaixa.DayOfWeek == DayOfWeek.Saturday)
                {
                    databaixa = databaixa.AddDays(-1);
                }
                if (databaixa.DayOfWeek == DayOfWeek.Sunday)
                {
                    databaixa = databaixa.AddDays(-2);
                }
                // Verifica se neste dia não é feriado ?
                query = "select * from FERIADOS WHERE DATA=" + clsParser.SqlDateTimeFormat(databaixa.ToString("dd/MM/yyyy") + " 00:00", true);
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scn.Open();
                scd = new SqlCommand(query, scn);
                sdrFeriado = scd.ExecuteReader();
                if (sdrFeriado.Read())
                { // achou
                    databaixa.AddDays(-1);
                }
                else
                {
                    terminou = true;
                }
                scn.Close();
            }
            tbxDataBaixa.Text = databaixa.ToString("dd/MM/yyyy");
            // Calcular Data do Credito atravez da qtde de dias de compensação
            databaixa = databaixa.AddDays(clsParser.Int32Parse(tbxDiasCompensacao.Text));

            terminou = false;
            while (terminou == false)
            {
                if (databaixa.DayOfWeek == DayOfWeek.Saturday)
                {
                    databaixa = databaixa.AddDays(2);
                }
                if (databaixa.DayOfWeek == DayOfWeek.Sunday)
                {
                    databaixa = databaixa.AddDays(1);
                }
                // Verifica se neste dia não é feriado ?
                query = "select * from FERIADOS WHERE DATA=" + clsParser.SqlDateTimeFormat(databaixa.ToString("dd/MM/yyyy") + " 00:00", true);
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scn.Open();
                scd = new SqlCommand(query, scn);
                sdrFeriado = scd.ExecuteReader();
                if (sdrFeriado.Read())
                { // achou
                    databaixa.AddDays(1);
                }
                else
                {
                    terminou = true;
                }
                scn.Close();
            }

            tbxDataCredito.Text = databaixa.ToString("dd/MM/yyyy");
        }
        private void bwrAReceber_Run()
        {
            if (carregandoAReceber == false)
            {
                carregandoAReceber = true;

                bwrAReceber = new BackgroundWorker();
                bwrAReceber.DoWork += new DoWorkEventHandler(bwrAReceber_DoWork);
                bwrAReceber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrAReceber_RunWorkerCompleted);
                bwrAReceber.RunWorkerAsync();
            }
        }


        private void bwrAReceber_DoWork(object sender, DoWorkEventArgs e)
        {
            dtAReceber = new DataTable();
            query = "SELECT RECEBER.ID,RECEBER.FILIAL,RECEBER.DUPLICATA,RECEBER.POSICAO " +
                    ",RECEBER.POSICAOFIM,RECEBER.EMISSAO,CLIENTE.COGNOME, '' AS [CLIENTE_COGNOME], '' as TELEFONE,RECEBER.VENCIMENTO " +
                    ",RECEBER.VENCIMENTOPREV,RECEBER.VALOR,RECEBER.VALORLIQUIDO,VENDEDOR.COGNOME AS [VENDEDOR] " +
                    ",SITUACAOTIPOTITULO.CODIGO + '-' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], RECEBER.BOLETONRO " +
                    ",RECEBER.DV,TAB_BANCOS.COGNOME AS [BANCO],RECEBER.VALORJUROS,RECEBER.VALORMULTA " +
                    ",RECEBER.VALORDESCONTO,RECEBER.ATEVENCIMENTO, SITUACAOTITULO.CODIGO [SITUACAOTITULO] " +
                    "FROM RECEBER " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=RECEBER.IDCLIENTE " +
                    "LEFT JOIN CLIENTE VENDEDOR ON VENDEDOR.ID=RECEBER.IDVENDEDOR " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=RECEBER.IDFORMAPAGTO " +
                    "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID=RECEBER.IDBANCO "  +
                    "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=RECEBER.IDSITBANCO " +
                    "LEFT JOIN NFVENDA ON NFVENDA.ID = RECEBER.IDNOTAFISCAL ";

            query = query + " WHERE  RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " "; 
            query = query + " AND RECEBER.IDBANCOINT = " + idBancoInt;  // SELECIONANDO O BANCO INDICADO
            query = query + " AND SITUACAOTITULO.CODIGO < " + 90 + " ";  // NÃO PODE APARECER SE FOI DESCONTADO
            query = query + " AND RECEBER.VALORPAGO = " + 0 + " ";  // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
                                                                    //POIS NÃO FAZ BAIXA PAGAMENTO PARCIAL NESTE FORM
            query = query + " AND RECEBER.VALORBAIXANDO = " + 0 + " "; // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO

            if (rbnVencimento.Checked == true)
            {
                query = query + " ORDER BY RECEBER.VENCIMENTO, RECEBER.DUPLICATA ";
            }
            else
            {
                query = query + " ORDER BY CLIENTE.COGNOME, RECEBER.VENCIMENTO, RECEBER.DUPLICATA ";
            }
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtAReceber);

            if (Saldo == 0)
            {
                    query = "SELECT SUM((RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO)) " +
                            "FROM RECEBER " +
                            "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=RECEBER.IDSITBANCO ";
                    query = query + " WHERE  RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " "; 
                    query = query + " AND RECEBER.IDBANCOINT = " + idBancoInt;  // SELECIONANDO O BANCO INDICADO
                    query = query + " AND SITUACAOTITULO.CODIGO < " + 90 + " ";  // NÃO PODE APARECER SE FOI DESCONTADO
                    query = query + " AND RECEBER.VALORPAGO = " + 0 + " ";  // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
                                                                            //POIS NÃO FAZ BAIXA PAGAMENTO PARCIAL NESTE FORM
                    query = query + " AND RECEBER.VALORBAIXANDO = " + 0 + " "; // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scd = new SqlCommand(query, scn);
                    scn.Open();
                    Saldo  = clsParser.DecimalParse(scd.ExecuteScalar().ToString());
                    scn.Close();
                    
            }
        }


        private void bwrAReceber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvAReceber.DataSource = dtAReceber;
                if (clsInfo.zempresacliente_cognome == "FERPAL")
                {
                    clsGridHelper.MontaGrid2(dgvAReceber, dtAReceberColunasFerpal, true);
                }
                else
                {
                    clsGridHelper.MontaGrid2(dgvAReceber, dtAReceberColunas, true);
                }

                dgvAReceber.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvAReceber.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvAReceber.Columns["VENCIMENTOPREV"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvAReceber.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvAReceber.Columns["VALORLIQUIDO"].DefaultCellStyle.Format = "N2";
                dgvAReceber.Columns["VALORJUROS"].DefaultCellStyle.Format = "N2";
                dgvAReceber.Columns["VALORMULTA"].DefaultCellStyle.Format = "N2";
                dgvAReceber.Columns["VALORDESCONTO"].DefaultCellStyle.Format = "N2";
                
                clsGridHelper.FontGrid(dgvAReceber, 7);
                //GridHelper.SelecionaLinha(idAReceber, dgvAReceber, 1);
                PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoAReceber = false;

                if (carregandoRecebendo == false)
                {
                    bwrRecebendo_Run();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoAReceber = false;
            }
        }

        private void rbnVencimento_Click(object sender, EventArgs e)
        {
            bwrAReceber_Run();
        }

        private void rbnCliente_Click(object sender, EventArgs e)
        {
            bwrAReceber_Run();

        }

        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tbxPesquisa.Focus();

        }

        private void tbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tbxPesquisa.Focus();
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvAReceber);
            if (idAReceber > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(idAReceber, dgvAReceber, "DUPLICATA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo, dgvAReceber, "DUPLICATA") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior, dgvAReceber, "DUPLICATA") == false)
                        {
                            if (dgvAReceber.Rows.Count > 0)
                            {
                                dgvAReceber.CurrentCell = dgvAReceber.Rows[0].Cells["DUPLICATA"];
                            }
                        }
                    }
                }
            }
            else if (dgvAReceber.Rows.Count > 0)
            {
                dgvAReceber.CurrentCell = dgvAReceber.Rows[0].Cells["DUPLICATA"];
            }

            if (dgvAReceber.CurrentRow != null)
            {
                idAReceber = clsParser.Int32Parse(
                      dgvAReceber.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvAReceber.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvAReceber.Rows[dgvAReceber.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvAReceber.CurrentRow.Index < dgvAReceber.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvAReceber.Rows[dgvAReceber.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
            }
            else
            {
                idAReceber = 0;
                id_Anterior = 0;
                id_Proximo = 0;
            }
        }
        private void PesquisaRapida1()
        {
            //GridHelper.Filtrar(tbxPesquisa.Text, dgvRecebendo);
            if (posicao_Recebendo > 0 || id_Anterior1 > 0 || id_Proximo1 > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(posicao_Recebendo, dgvRecebendo, "DUPLICATA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo1, dgvRecebendo, "DUPLICATA") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior1, dgvRecebendo, "DUPLICATA") == false)
                        {
                            if (dgvRecebendo.Rows.Count > 0)
                            {
                                dgvRecebendo.CurrentCell = dgvRecebendo.Rows[0].Cells["DUPLICATA"];
                            }
                        }
                    }
                }
            }
            else if (dgvRecebendo.Rows.Count > 0)
            {
                dgvRecebendo.CurrentCell = dgvRecebendo.Rows[0].Cells["DUPLICATA"];
            }

            if (dgvRecebendo.CurrentRow != null)
            {
                posicao_Recebendo = clsParser.Int32Parse(dgvRecebendo.CurrentRow.Cells["posicao_Recebendo"].Value.ToString());
                if (dgvRecebendo.CurrentRow.Index > 0)
                {
                    id_Anterior1 = clsParser.Int32Parse(dgvRecebendo.Rows[dgvRecebendo.CurrentRow.Index - 1].Cells["posicao_Recebendo"].Value.ToString());
                }
                else
                {
                    id_Anterior1 = 0;
                }
                if (dgvRecebendo.CurrentRow.Index < dgvRecebendo.Rows.Count - 1)
                {
                    id_Proximo1 = clsParser.Int32Parse(dgvRecebendo.Rows[dgvRecebendo.CurrentRow.Index + 1].Cells["posicao_Recebendo"].Value.ToString());
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
        private void dgvAReceber_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvAReceber.CurrentRow != null)
            {
                if (idBancoInt == 0 || idBancoIntDes == 0)
                {
                    MessageBox.Show("Usuário você deixou de indicar o Banco Interno ou o Banco Interno Destino");
                }
                else
                {
                    idAReceber = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["ID"].Value.ToString());

                    if (dgvAReceber.CurrentRow.Index > 0)
                    {
                        id_Anterior = clsParser.Int32Parse(dgvAReceber.Rows[dgvAReceber.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Anterior = 0;
                    }


                    if (dgvAReceber.CurrentRow.Index < dgvAReceber.Rows.Count - 1)
                    {
                        id_Proximo = clsParser.Int32Parse(dgvAReceber.Rows[dgvAReceber.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Proximo = 0;
                    }
                    if (clsParser.Int32Parse(tbxBancoIntDes.Text) > 0)
                    {
                        Baixar();
                    }
                    else
                    {
                        MessageBox.Show("Obrigatorio colocar a conta de destino destas Baixas !!");
                    }
                }
            }
        }
        private void CriarRecebendoBaixa()
        {
            dtRecebendo = new DataTable();
            dtRecebendo.Columns.Add("POSICAO_RECEBENDO", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("IDRECEBER", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("IDRECEBIDA", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("FILIAL", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("DUPLICATA", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("POSICAO", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("COGNOME", Type.GetType("System.String"));
            dtRecebendo.Columns.Add("CLIENTE_COGNOME", Type.GetType("System.String"));
            dtRecebendo.Columns.Add("TELEFONE", Type.GetType("System.String"));
            dtRecebendo.Columns.Add("VENCIMENTO", Type.GetType("System.DateTime"));
            dtRecebendo.Columns.Add("VALORTITULO", Type.GetType("System.Decimal"));
            dtRecebendo.Columns.Add("VALOR", Type.GetType("System.Decimal"));
            dtRecebendo.Columns.Add("DEBCRED", Type.GetType("System.String"));
            dtRecebendo.Columns.Add("COBRANCACOD", Type.GetType("System.String"));
            dtRecebendo.Columns.Add("COBRANCAHIS", Type.GetType("System.String"));
            dtRecebendo.Columns.Add("COBRANCANOME", Type.GetType("System.String"));
            dtRecebendo.Columns.Add("VALORCOMISSAO", Type.GetType("System.Decimal"));
            dtRecebendo.Columns.Add("VALORCOMISSAOGER", Type.GetType("System.Decimal"));
            dtRecebendo.Columns.Add("VALORCOMISSAOSUP", Type.GetType("System.Decimal"));
            dtRecebendo.Columns.Add("IDHISTORICO", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("IDCENTROCUSTO", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("IDCODIGOCTABIL", Type.GetType("System.Int32"));
            dtRecebendo.Columns.Add("BOLETONUMERO", Type.GetType("System.String"));

        }
        private void bwrRecebendo_Run()
        {
            if (carregandoRecebendo == false)
            {
                carregandoRecebendo = true;

                bwrRecebendo = new BackgroundWorker();
                bwrRecebendo.DoWork += new DoWorkEventHandler(bwrRecebendo_DoWork);
                bwrRecebendo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrRecebendo_RunWorkerCompleted);
                bwrRecebendo.RunWorkerAsync();
            }
        }


        private void bwrRecebendo_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        private void bwrRecebendo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvRecebendo.DataSource = dtRecebendo;
                if (clsInfo.zempresacliente_cognome == "FERPAL")
                {
                    clsGridHelper.MontaGrid2(dgvRecebendo, dtRecebendoColunasFerpal, true);
                }
                else
                {
                    clsGridHelper.MontaGrid2(dgvRecebendo, dtRecebendoColunas, true);
                }
                dgvRecebendo.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvRecebendo.Columns["VALORTITULO"].DefaultCellStyle.Format = "N2";
                dgvRecebendo.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvRecebendo.Columns["VALORCOMISSAO"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvRecebendo, 7);
                //GridHelper.SelecionaLinha(idRecebendo, dgvRecebendo, 1);
                PesquisaRapida1(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoRecebendo = false;

                // Calcular os Totais
                tbxValorPrincipal.Text = "0";
                tbxValorRecebendo.Text = "0";
                tbxMultas.Text = "0";
                tbxJuros.Text = "0";
                tbxCartorio.Text = "0";
                tbxTarifas.Text = "0";

                foreach (DataRow rowTotais in dtRecebendo.Rows)
                {
                    //SE JÁ TEM TITULO TRAVAR O FRAME PARA NÃO ALTERAR MAIS NADA
                    gbxCabecalho.Enabled = false;
                    // ' VALOR PRINCIPAL DO TITULO
                    tbxValorPrincipal.Text = (clsParser.DecimalParse(tbxValorPrincipal.Text) + clsParser.DecimalParse(rowTotais["VALORTITULO"].ToString())).ToString("N2");
                    switch (rowTotais["COBRANCACOD"].ToString().ToUpper())
                    {
                        case "01":
                            tbxValorRecebendo.Text = (clsParser.DecimalParse(tbxValorRecebendo.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "02":
                            tbxMultas.Text = (clsParser.DecimalParse(tbxMultas.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "03":
                            tbxJuros.Text = (clsParser.DecimalParse(tbxJuros.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "04":
                            if (rowTotais["COBRANCAHIS"].ToString().ToUpper() == "41")
                            {
                                tbxCartorio.Text = (clsParser.DecimalParse(tbxCartorio.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            }
                            else
                            {
                                tbxTarifas.Text = (clsParser.DecimalParse(tbxTarifas.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            }
                            break;
                        case "08":
                            tbxDesconto.Text = (clsParser.DecimalParse(tbxDesconto.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        case "09":
                            tbxTarifas.Text = (clsParser.DecimalParse(tbxTarifas.Text) + clsParser.DecimalParse(rowTotais["VALOR"].ToString())).ToString("N2");
                            break;
                        default:
                            MessageBox.Show("Despesa não foi adicionada aos calculos ?? ");
                            break;
                    }
                    // TOTAL DE DEDUÇÕES = tarifas + cartorios
                    tbxTotalDeducoes.Text = (clsParser.DecimalParse(tbxTarifas.Text) + clsParser.DecimalParse(tbxCartorio.Text)).ToString("N2");
                    //liquido a receber = (vl recebido + multas + juros) - descontos
                    tbxValorReceber.Text = (clsParser.DecimalParse(tbxValorRecebendo.Text) + clsParser.DecimalParse(tbxMultas.Text) + clsParser.DecimalParse(tbxJuros.Text)).ToString("N2");
                    tbxSaldoAReceber.Text = Saldo.ToString("N2");
                    tbxSaldoBaixando.Text = tbxValorPrincipal.Text;
                    tbxSaldo.Text = (clsParser.DecimalParse(tbxSaldoAReceber.Text) - clsParser.DecimalParse(tbxSaldoBaixando.Text)).ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoAReceber = false;
            }
        }
        private void Baixar()
        {
            if (dgvAReceber.CurrentRow != null)
            {
                idAReceber = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["ID"].Value.ToString());
                // verificar se existe Historico
                Int32 idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICO from RECEBER where ID= " + idAReceber));
                Int32 idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTO from RECEBER where ID= " + idAReceber));
                if (idHistorico == 0)
                {
                    MessageBox.Show("Falta Historico do Aplibank nesta Duplicata " + Environment.NewLine +
                                    "Vá na duplicata [" + dgvAReceber.CurrentRow.Cells["DUPLICATA"].Value.ToString() + "]");
                }
                else
                {
                    Decimal Valor = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALOR from RECEBER where ID= " + idAReceber));
                    Decimal ValorDesconto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORDESCONTO from RECEBER where ID= " + idAReceber));
                    Decimal ValorJuros = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORJUROS from RECEBER where ID= " + idAReceber));
                    Decimal ValorMulta = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORMULTA from RECEBER where ID= " + idAReceber));
                    Decimal ValorCredito = 0; // clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORCREDITO from RECEBER where ID= " + idAReceber));
                    Decimal ValorRecebido = 0;
                    Decimal ValorDevolucao = 0;
                    Decimal ValorComissao = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORCOMISSAO from RECEBER where ID= " + idAReceber));
                    Decimal ValorComissaoGer = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORCOMISSAOGER from RECEBER where ID= " + idAReceber));
                    Decimal ValorComissaoSup = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORCOMISSAOSUP from RECEBER where ID= " + idAReceber));

                    DuplicataInfo DuplicataInfo;
                    DuplicataInfo = clsFinanceiro.CalcularDuplicata("Receber",
                                                                    dgvAReceber.CurrentRow.Cells["VENCIMENTO"].Value.ToString(),
                                                                    tbxDataBaixa.Text,
                                                                    Valor,
                                                                    ValorDesconto, 
                                                                    ValorJuros, 
                                                                    ValorMulta, 
                                                                    ValorCredito,
                                                                    dgvAReceber.CurrentRow.Cells["ATEVENCIMENTO"].Value.ToString());


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
                        BaixarBoletoInfo = clsFinanceiro.BaixarBoleto(x, idAReceber, Juros, 
                                         clsParser.Int32Parse(tbxBanco.Text), clsParser.DateTimeParse(tbxDataBaixa.Text), 
                                         clsParser.DateTimeParse(dgvAReceber.CurrentRow.Cells["VENCIMENTO"].Value.ToString()), 
                                         Valor, clsParser.DecimalParse(tbxTarifaBoleto.Text), clsParser.DecimalParse(tbxTarifaCartorio.Text), 
                                         Multa, Desconto, dgvAReceber.CurrentRow.Cells["ATEVENCIMENTO"].Value.ToString(), 
                                         ValorComissao, ValorComissaoGer);

                        if (BaixarBoletoInfo.IncluirRegistro == true)
                        { // Incluir um novo registro
                            // procurando a posição
                            posicao_Recebendo = 0;
                            foreach (DataRow row1 in dtRecebendo.Rows)
                            {
                                if (clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString()) > posicao_Recebendo)
                                {
                                    posicao_Recebendo = clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString());
                                }
                            }
                            posicao_Recebendo += 1;
                            DataRow row = dtRecebendo.NewRow();
                            row["POSICAO_RECEBENDO"]=posicao_Recebendo;
                            row["IDRECEBER"] = idAReceber;
                            row["IDRECEBIDA"]= 0;
                            row["FILIAL"] = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["FILIAL"].Value.ToString());
                            row["DUPLICATA"] = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["DUPLICATA"].Value.ToString());
                            row["POSICAO"] = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["POSICAO"].Value.ToString());
                            row["COGNOME"] = dgvAReceber.CurrentRow.Cells["COGNOME"].Value.ToString();
                            row["CLIENTE_COGNOME"] = dgvAReceber.CurrentRow.Cells["CLIENTE_COGNOME"].Value.ToString();
                            row["TELEFONE"] = dgvAReceber.CurrentRow.Cells["TELEFONE"].Value.ToString();
                            row["VENCIMENTO"] = clsParser.DateTimeParse(dgvAReceber.CurrentRow.Cells["VENCIMENTO"].Value.ToString()); 
                            row["VALORTITULO"]= BaixarBoletoInfo.ValorTitulo;
                            row["VALOR"]= BaixarBoletoInfo.ValorLancamento;
                            row["DEBCRED"] = BaixarBoletoInfo.DebCred;
                            row["DEBCRED"] = BaixarBoletoInfo.DebCred;
                            row["BOLETONUMERO"] = dgvAReceber.CurrentRow.Cells["BOLETONRO"].Value.ToString();
                            row["COBRANCAHIS"]= BaixarBoletoInfo.Historico; 
                            row["COBRANCANOME"]= BaixarBoletoInfo.Nome;
                            row["VALORCOMISSAO"]= BaixarBoletoInfo.VlComissao;
                            row["VALORCOMISSAOGER"]= BaixarBoletoInfo.VlComissaoGer;
                            row["VALORCOMISSAOSUP"] = 0;
                            //Brunno adicionou
                            row["COBRANCACOD"] = BaixarBoletoInfo.Codigo;
                           
                            // Vai buscar na Situação da Cobrança
                            idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICORECEBER from SITUACAOCOBRANCACOD where ID= '" + BaixarBoletoInfo.Codigo + "' ").ToString());
                            if (idHistorico == 0)
                            {
                                idHistorico = clsInfo.zhistoricos;
                            }
                            String histo = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID= " + idHistorico + " ").ToString();
                            if (idHistorico == 0 || histo == "NC")
                            { // Se não achar HISTORICO OU FOR nc colocar o Historico da Duplicata
                                idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICO from RECEBER where ID= " + idAReceber + " ").ToString());
                            }
                            if (idHistorico == 0)
                            {
                                idHistorico = clsInfo.zhistoricos;
                            }
                            row["IDHISTORICO"] = idHistorico;
                            idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTORECEBER from SITUACAOCOBRANCACOD where ID= '" + BaixarBoletoInfo.Codigo + "' ").ToString());
                            if (idCentroCusto == 0)
                            {
                                idCentroCusto = clsInfo.zcentrocustos;
                            }
                            String centro = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID= " + idCentroCusto + " ").ToString();
                            if (idCentroCusto == 0 || centro == "NC")
                            { // Se não achar HISTORICO OU FOR nc colocar o Historico da Duplicata
                                idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTO from RECEBER where ID= " + idAReceber + " ").ToString());
                            }
                            if (idCentroCusto == 0)
                            {
                                idCentroCusto = clsInfo.zcentrocustos;
                            }
                            row["IDCENTROCUSTO"] = idCentroCusto;
                            row["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;

                            dtRecebendo.Rows.Add(row);
                           if (BaixarBoletoInfo.Codigo == "01")
                           {
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("UPDATE RECEBER SET VALORBAIXANDO=@DIFERENCA " +
                                                        "WHERE ID = @ID", scn);
                                scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idAReceber;
                                scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = BaixarBoletoInfo.ValorLancamento;
                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();
                                bwrAReceber_Run();
                           }
                        }
                    }
                }
                bwrRecebendo_Run();
            }
        }
        private void dgvRecebendo_MouseDoubleClick(object sender, MouseEventArgs e)
        { 
            if (dgvRecebendo.CurrentRow != null)
            {
                posicao_Recebendo = clsParser.Int32Parse(dgvRecebendo.CurrentRow.Cells["POSICAO_RECEBENDO"].Value.ToString());
                idRecebendo = clsParser.Int32Parse(dgvRecebendo.CurrentRow.Cells["IDRECEBER"].Value.ToString());
                if (dgvRecebendo.CurrentRow.Index > 0)
                {
                    id_Anterior1 = clsParser.Int32Parse(dgvRecebendo.Rows[dgvRecebendo.CurrentRow.Index - 1].Cells["POSICAO_RECEBENDO"].Value.ToString());
                }
                else
                {
                    id_Anterior1 = 0;
                }
                if (dgvRecebendo.CurrentRow.Index < dgvRecebendo.Rows.Count - 1)
                {
                    id_Proximo1 = clsParser.Int32Parse(dgvRecebendo.Rows[dgvRecebendo.CurrentRow.Index + 1].Cells["posicao_recebendo"].Value.ToString());
                }
                else
                {
                    id_Proximo1 = 0;
                }
                // APAGAR O CONTAS A RECEBER VIRTUAL
                if (dgvRecebendo.CurrentRow.Cells["COBRANCACOD"].Value.ToString() ==  "01")
                {   // Todos os Lançamentos da Duplicata
                    Boolean terminou = false;
                    while (terminou == false)
                    {
                        if (dtRecebendo.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtRecebendo.Rows)
                            {
                                if (clsParser.Int32Parse(row["IDRECEBER"].ToString()) == idRecebendo)
                                {
                                    dtRecebendo.Select("POSICAO_RECEBENDO =" + row["POSICAO_RECEBENDO"].ToString())[0].Delete();
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
                    scd = new SqlCommand("UPDATE RECEBER SET VALORBAIXANDO=@DIFERENCA WHERE ID = @ID", scn);
                    scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idRecebendo;
                    scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = 0;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();
                    bwrAReceber_Run();

                }
                else
                {   // Apenas o Lançamento Indicado
                   //query = "select * from RPTRECEBERBAIXA WHERE POSICAO_RECEBENDO=" + clsParser.Int32Parse(dgvRecebendo.CurrentRow.Cells["POSICAO_RECEBENDO"].Value.ToString());
                   
                   // DEVOLVER PARA O CONTAS A RECEBER SE FOR COD 01
                    if (dgvRecebendo.CurrentRow.Cells["COBRANCACOD"].Value.ToString() == "01")
                    {
                        dtRecebendo.Select("POSICAO_RECEBENDO =" + dgvRecebendo.CurrentRow.Cells["POSICAO_RECEBENDO"].Value.ToString())[0].Delete();
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("UPDATE RECEBER SET VALORBAIXANDO=@DIFERENCA WHERE ID = @ID", scn);
                        scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idRecebendo;
                        scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = 0;
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                        bwrAReceber_Run();
                    }
                    else
                    {
                        dtRecebendo.Select("POSICAO_RECEBENDO =" + dgvRecebendo.CurrentRow.Cells["POSICAO_RECEBENDO"].Value.ToString())[0].Delete();
                        bwrAReceber_Run();
                    }
                }
                posicao_Recebendo = 0;
                bwrRecebendo_Run();

            }
        }
        private void ReceberCarregar()
        {
            clsReceberInfo = new clsReceberInfo();
            clsReceberInfo = clsReceberBLL.Carregar(idAReceber, clsInfo.conexaosqldados);
            ReceberCampos(clsReceberInfo);

            if (gbxBaixar.Enabled == true)
            {
                tbxCobrancaCod.Select();
                tbxCobrancaCod.SelectAll();
            }
            else
            {
                tbxCobrancaCod.Select();
                tbxCobrancaCod.SelectAll();
            }
            //
            calcular();
            //VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
            //bwrRecebida01_RunWorkerAsync();


        }
        private void ReceberCampos(clsReceberInfo info)
        {

            //id = info.id;
            if (info.atevencimento == "S") { ckxAteVencimento.Checked = true; } else { ckxAteVencimento.Checked = false; }
            if (ckxAteVencimento.Checked == true)
            {
                ckxAteVencimento.Text = "Sim Descontar após Vencimento";
                atevencimento = "S";
            }
            else
            {
                ckxAteVencimento.Text = "Não Descontar após o Vencimento";
                atevencimento = "N";
            }
            //info.baixa;
            tbxBoleto.Text = info.boleto;
            tbxBoletoNro.Text = info.boletonro.ToString();
            if (info.chegou == "S")
            {
                ckxChegou.Checked = true;
                ckxChegou.Text = "Sim - enviou a cobrança";
            }
            else
            {
                ckxChegou.Checked = false;
                ckxChegou.Text = "Não - enviou a cobrança";
            }

//            tbxDataLanca.Text = info.datalanca.ToString("dd/MM/yyyy");
            if (info.despesapublica == "S")
            {
                ckxDespesaPublica.Checked = true;
                ckxDespesaPublica.Text = "Sim - conferido";
            }
            else
            {
                ckxDespesaPublica.Checked = false;
                ckxDespesaPublica.Text = "Não - conferido";
            }
            tbxDuplicata.Text = info.duplicata.ToString();
            tbxDv.Text = info.dv;
            tbxEmissao.Text = info.emissao.ToString("dd/MM/yyyy");
            //tbxEmitente.Text = info.emitente;
            tbxFilial.Text = info.filial.ToString();
            
            tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + info.idbanco);
            tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + info.idbanco);

            tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID= " + info.idbancoint);
            tbxBancoNomeInterno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + info.idbancoint);

            tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + info.idcentrocusto);
            tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID= " + info.idcentrocusto);

            tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID= " + info.idcodigoctabil);
            tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID= " + info.idcodigoctabil);
            tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + info.iddocumento);

            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + info.idformapagto);
            tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + info.idformapagto);

            tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + info.idcliente);

            tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + info.idhistorico);
            tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT NOME FROM HISTORICOS where id=" + info.idhistorico);

            if (tbxDocumento.Text == "CTO")
            {
                tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM CONTRATO where id=" + info.idnotafiscal);
                tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOTALLIQUIDO FROM CONTRATO where id=" + info.idnotafiscal);
                tbxNotaDoc.Text = tbxDocumento.Text;
            }
            else
            {
                tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFVENDA where id=" + info.idnotafiscal);
                tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOTALNOTAFISCAL FROM NFVENDA where id=" + info.idnotafiscal);
                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFVENDA where id=" + info.idnotafiscal)) > 0)
                {
                    Int32 idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFVENDA where id=" + info.idnotafiscal));
                    if (idDocNFV == 0)
                        idDocNFV = clsInfo.zdocumento;

                    tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM DOCFISCAL where id=" + idDocNFV);
                }
                else
                {
                    tbxNotaDoc.Text = "";
                }

            }
            tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTITULO where id=" + info.idsitbanco);
            tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTITULO where id=" + info.idsitbanco);

            tbxVendedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + info.idvendedor);

            //info.imprimir;
            tbxObserva.Text = info.observa;
            tbxPosicao.Text = info.posicao.ToString();
            tbxParcelas.Text = info.posicaofim.ToString();

            tbxValor.Text = info.valor.ToString("N2");
            //info.valorbaixando;
            tbxValorComissao.Text = info.valorcomissao.ToString("N2");
//            tbxValorComissaoGer.Text = info.valorcomissaoger.ToString("N2");

            tbxValorDesconto.Text = info.valordesconto.ToString("N2");
            tbxDevolucao.Text = info.valordevolvido.ToString("N2");
            tbxValorJuros.Text = info.valorjuros.ToString("N2");
            tbxValorLiquido.Text = info.valorliquido.ToString("N2");
            tbxValorMulta.Text = info.valormulta.ToString("N2");
            tbxRecebido.Text = info.valorpago.ToString("N2");
            tbxVencimento.Text = info.vencimento.ToString("dd/MM/yyyy");
            tbxVencimentoPrev.Text = info.vencimentoprev.ToString("dd/MM/yyyy");

            tbxMulta.Text = "0";
            tbxDesconto.Text = "0";
            tbxSaldo.Text = "0";
            tbxRecebido.Text = "0";
            tbxDevolucao.Text = info.valordevolvido.ToString("N2");
            tbxDiferenca.Text = "0";
            tbxPagando.Text = "0";

            // Se a Data da Baixa for superior a 3 dias enviar uma mensagem
            //dataBaixa = DateTime.Parse(tbxDataBaixa.Text);
            //dataVencimento = DateTime.Parse(tbxVencimento.Text);
            /*
            dias = dataBaixa.Subtract(dataVencimento).Days;
            if (dias > 0)
            {
                MessageBox.Show("Data da Baixa com " + dias + " dias de diferença." + Environment.NewLine + "Diferença entre a Data de Vencimento e a Data da Baixa ");
                tbxDataBaixa.Select();
                tbxDataBaixa.SelectAll();
            }
            if (tbxFormaPagto.Text.Substring(0, 2) == "CH")
            { // Se for cheque colocar a data de deposito como credito
                tbxDataCredito.Text = tbxVencimento.Text;
            }
            else
            {
                tbxDataCredito.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            */
        }
        private void calcular()
        { //Colocamos numa função financeiro
            DuplicataInfo DuplicataInfo;
            DuplicataInfo = clsFinanceiro.CalcularDuplicata("Receber", tbxVencimento.Text, tbxDataBaixa.Text,
                                                                    clsParser.DecimalParse(tbxValor.Text), +
                                                                    clsParser.DecimalParse(tbxValorDesconto.Text), +
                                                                    clsParser.DecimalParse(tbxValorJuros.Text), +
                                                                    clsParser.DecimalParse(tbxValorMulta.Text), +
                                                                    clsParser.DecimalParse(tbxValorCredito.Text),
                                                                    atevencimento);


            tbxValorLiquido.Text = DuplicataInfo.valorliquido.ToString("N2");
            tbxMulta.Text = DuplicataInfo.multas.ToString("N2");
            tbxJuros.Text = DuplicataInfo.juros.ToString("N2");
            tbxDesconto.Text = DuplicataInfo.descontos.ToString("N2");
            tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
            //totaldias = DuplicataInfo.ntotaldias;
            //tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");
            tbxDiferenca.Text = (((clsParser.DecimalParse(tbxSaldo.Text) + clsParser.DecimalParse(tbxValorCredito.Text)) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");
            //Receber01Grid();
        }

        private void tspSalvarTitulo_Click(object sender, EventArgs e)
        {
            // Incluir um novo registro
            // procurando a posição
            posicao_Recebendo = 0;
            foreach (DataRow row1 in dtRecebendo.Rows)
            {
                if (clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString()) > posicao_Recebendo)
                {
                    posicao_Recebendo = clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString());
                }
            }
            posicao_Recebendo += 1;
            DataRow row = dtRecebendo.NewRow();
            row["POSICAO_RECEBENDO"] = posicao_Recebendo;
            row["IDRECEBER"] = idAReceber;
            row["IDRECEBIDA"] = 0;
            row["FILIAL"] = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["FILIAL"].Value.ToString());
            row["DUPLICATA"] = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["DUPLICATA"].Value.ToString());
            row["POSICAO"] = clsParser.Int32Parse(dgvAReceber.CurrentRow.Cells["POSICAO"].Value.ToString());
            row["COGNOME"] = dgvAReceber.CurrentRow.Cells["COGNOME"].Value.ToString();
            row["CLIENTE_COGNOME"] = dgvAReceber.CurrentRow.Cells["CLIENTE_COGNOME"].Value.ToString();
            row["TELEFONE"] = dgvAReceber.CurrentRow.Cells["TELEFONE"].Value.ToString();
            row["VENCIMENTO"] = clsParser.DateTimeParse(dgvAReceber.CurrentRow.Cells["VENCIMENTO"].Value.ToString());
            row["VALORTITULO"] = 0;
            row["VALOR"] = tbxCobrancaValor.Text;
            row["DEBCRED"] = tbxCobrancaDebCred.Text;
            row["COBRANCACOD"] = tbxCobrancaCod.Text;
            row["COBRANCAHIS"] = tbxCobrancaHis.Text;
            row["COBRANCANOME"] = tbxCobrancaNome.Text;
            row["VALORCOMISSAO"] = 0;
            row["VALORCOMISSAOGER"] = 0;
            row["VALORCOMISSAOSUP"] = 0;
            row["IDHISTORICO"] = receber1_idhistoricocob;
            row["IDCENTROCUSTO"] = receber1_idcobrancacod;
            row["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;

            dtRecebendo.Rows.Add(row);
            bwrAReceber_Run();
            gbxTitulo.Visible = false;
            tclReceberSemiAutomatica.SelectedIndex = 0;
        }

        private void tspRetornarTitulo_Click(object sender, EventArgs e)
        {
            gbxTitulo.Visible = false;
            tclReceberSemiAutomatica.SelectedIndex = 0;
        }
        private void btnIdCobrancaCod_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCobrancaCod.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", 0, "Situacao Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnIdCobrancaHis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCobrancaHis.Name;
            frmSituacaoCobrancaCod1Pes frmSituacaoCobrancaCod1Pes = new frmSituacaoCobrancaCod1Pes();
            frmSituacaoCobrancaCod1Pes.Init(0, receber1_idcobrancacod);

            clsFormHelper.AbrirForm(this.MdiParent, frmSituacaoCobrancaCod1Pes, clsInfo.conexaosqldados);

        }

        private void btnIdHistoricoCobranca_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdHistoricoCobranca.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", receber1_idhistoricocob, "Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);

        }

        private void btnIdCentroCustoCobranca_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCentroCustoCobranca.Name;
            frmCentroCustosPes frmCentroCustosPes = new frmCentroCustosPes();
            frmCentroCustosPes.Init(clsInfo.conexaosqldados, receber1_idcentrocustocob);

            clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosPes, clsInfo.conexaosqlbanco);
        }


        private void tspSalvarDespesa_Click(object sender, EventArgs e)
        {
            // Incluir um novo registro
            // procurando a posição
            posicao_Recebendo = 0;
            foreach (DataRow row1 in dtRecebendo.Rows)
            {
                if (clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString()) > posicao_Recebendo)
                {
                    posicao_Recebendo = clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString());
                }
            }
            posicao_Recebendo += 1;
            DataRow row = dtRecebendo.NewRow();
            row["POSICAO_RECEBENDO"] = posicao_Recebendo;
            row["IDRECEBER"] = 0;
            row["IDRECEBIDA"] = 0;
            row["FILIAL"] = clsInfo.zfilial;
            row["DUPLICATA"] = 0;
            row["POSICAO"] = 0;
            row["COGNOME"] = tbxBancoIntDesNome.Text;
            row["VENCIMENTO"] = tbxDataCredito.Text;
            row["VALORTITULO"] = 0;
            row["VALOR"] = tbxCobrancaValorDes.Text;
            row["DEBCRED"] = tbxCobrancaDebCredDes.Text;
            row["COBRANCACOD"] = tbxCobrancaCodDesp.Text;
            row["COBRANCAHIS"] = tbxCobrancaHisDes.Text;
            row["COBRANCANOME"] = tbxCobrancaNomeDes.Text;
            row["VALORCOMISSAO"] = 0;
            row["VALORCOMISSAOGER"] = 0;
            row["VALORCOMISSAOSUP"] = 0;
            row["IDHISTORICO"] =   receber1_idhistoricocobDesp;
            row["IDCENTROCUSTO"] = receber1_idcentrocustocobDesp;
            row["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
            dtRecebendo.Rows.Add(row);

            bwrAReceber_Run();
            gbxTitulo.Visible = false;
            tclReceberSemiAutomatica.SelectedIndex = 0;

        }

        private void tspRetornarDespesa_Click(object sender, EventArgs e)
        {
            gbxTitulo.Visible = false;
            tclReceberSemiAutomatica.SelectedIndex = 0;

        }
        private void btnIdCobrancaCodDes_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCobrancaCodDes.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", 0, "Situacao Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnIdCobrancaHisDes_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCobrancaHisDes.Name;
            frmSituacaoCobrancaCod1Pes frmSituacaoCobrancaCod1Pes = new frmSituacaoCobrancaCod1Pes();
            frmSituacaoCobrancaCod1Pes.Init(0, receber1_idcobrancacodDesp);

            clsFormHelper.AbrirForm(this.MdiParent, frmSituacaoCobrancaCod1Pes, clsInfo.conexaosqldados);
        }

        private void btnIdHistoricoCobrancaDes_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdHistoricoCobrancaDes.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", receber1_idhistoricocobDesp, "Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);

        }

        private void btnIdCentroCustoCobrancaDes_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCentroCustoCobrancaDes.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", receber1_idcentrocustocobDesp, "Centro de Custos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);

        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idCliente);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void bwrRecebida_Run()
        {
            if (carregandoRecebida == false)
            {
                carregandoRecebida = true;

                bwrRecebida = new BackgroundWorker();
                bwrRecebida.DoWork += new DoWorkEventHandler(bwrRecebida_DoWork);
                bwrRecebida.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrRecebida_RunWorkerCompleted);
                bwrRecebida.RunWorkerAsync();
            }
        }


        private void bwrRecebida_DoWork(object sender, DoWorkEventArgs e)
        {
            dtRecebida = new DataTable();
            query = "select RECEBIDA.ID, RECEBIDA.FILIAL, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO, " +
                    "RECEBIDA.POSICAOFIM , RECEBIDA.EMISSAO, " +
                    "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, RECEBIDA.VENCIMENTO, RECEBIDA.VALOR " +
                    "FROM RECEBIDA " +
                    "left JOIN CLIENTE ON CLIENTE.ID = RECEBIDA.IDCLIENTE  " +
                    "left JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBIDA.IDDOCUMENTO  " +
                    "left JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBIDA.IDFORMAPAGTO " +
                    "WHERE RECEBIDA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
            query = query + " AND RECEBIDA.IDCLIENTE = " + idCliente;  // SELECIONANDO O CLIENTE
            query = query + " AND RECEBIDA.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataRecebida.Text + " 00:00", true);
            query = query + " ORDER BY CLIENTE.COGNOME, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO ";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtRecebida);

        }


        private void bwrRecebida_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvRecebido.DataSource = dtRecebida;
                clsGridHelper.MontaGrid2(dgvRecebido, dtRecebidoColunas, true);

                dgvRecebido.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvRecebido.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvRecebido.Columns["VALOR"].DefaultCellStyle.Format = "N2";

                clsGridHelper.FontGrid(dgvRecebido, 7);
                //GridHelper.SelecionaLinha(idAReceber, dgvAReceber, 1);
                PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoRecebida = false;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoAReceber = false;
            }
        }

        private void tspSalvarRecebido_Click(object sender, EventArgs e)
        {
            // Incluir um novo registro
            // procurando a posição
            posicao_Recebendo = 0;
            foreach (DataRow row1 in dtRecebendo.Rows)
            {
                if (clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString()) > posicao_Recebendo)
                {
                    posicao_Recebendo = clsParser.Int32Parse(row1["POSICAO_RECEBENDO"].ToString());
                }
            }
            posicao_Recebendo += 1;
            DataRow row = dtRecebendo.NewRow();
            row["POSICAO_RECEBENDO"] = posicao_Recebendo;
            row["IDRECEBER"] = 0;
            row["IDRECEBIDA"] = idRecebida;
            row["FILIAL"] = tbxFilialRec.Text;
            row["DUPLICATA"] = tbxDuplicataRec.Text;
            row["POSICAO"] = tbxPosicaoRec.Text;
            row["COGNOME"] = tbxClienteCognomeRecebido.Text;
            row["VENCIMENTO"] = tbxVencimentoRec.Text;
            row["VALORTITULO"] = 0;
            row["VALOR"] = tbxCobrancaValorRec.Text;
            row["DEBCRED"] = tbxCobrancaDebCredRec.Text;
            row["COBRANCACOD"] = tbxCobrancaCodRec.Text;
            row["COBRANCAHIS"] = tbxCobrancaHisRec.Text;
            row["COBRANCANOME"] = tbxCobrancaNomeRec.Text;
            row["VALORCOMISSAO"] = 0;
            row["VALORCOMISSAOGER"] = 0;
            row["VALORCOMISSAOSUP"] = 0;
            row["IDHISTORICO"] = recebida_IdHistoricoCobrancaRec;
            row["IDCENTROCUSTO"] = recebida_IdCentroCustoCobrancaRec;
            row["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
            dtRecebendo.Rows.Add(row);
            bwrAReceber_Run();
            gbxRecebido.Visible = false;
            tclReceberSemiAutomatica.SelectedIndex = 0;
        }

        private void tspRetornarRecebido_Click(object sender, EventArgs e)
        {
            gbxRecebido.Visible = false;
            tclReceberSemiAutomatica.SelectedIndex = 0;

        }

        private void tspRetornarRecebido1_Click(object sender, EventArgs e)
        {
            gbxRecebido.Visible = false;
            tclReceberSemiAutomatica.SelectedIndex = 0;

        }

        private void dgvRecebido_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvRecebido.CurrentRow != null)
            {
                idRecebida = (Int32)dgvRecebido.CurrentRow.Cells[0].Value;
                RecebidaCarregar();
                gbxRecebidaBaixar.Visible = true;
                tbxCobrancaCodRec.Select();
                tbxCobrancaCodRec.SelectAll();

            }
            else
            {
                MessageBox.Show("Indique o titulo que deseja incluir lançamento !!");
            }

        }
        // Recebidas
        private void RecebidaCarregar()
        {
            clsRecebidaInfo = new clsRecebidaInfo();
            clsRecebidaInfo = clsRecebidaBLL.Carregar(idRecebida, clsInfo.conexaosqldados);
            RecebidaCampos(clsRecebidaInfo);

            if (gbxRecebidaBaixar.Visible == true)
            {
                tbxCobrancaCodRec.Select();
                tbxCobrancaCodRec.SelectAll();
            }
            //
            //calcular();
            //VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
            //bwrRecebida01_RunWorkerAsync();


        }
        private void RecebidaCampos(clsRecebidaInfo info)
        {

            //id = info.id;
            if (info.atevencimento == "S") { ckxAteVencimento.Checked = true; } else { ckxAteVencimento.Checked = false; }
            if (ckxAteVencimentoRec.Checked == true)
            {
                ckxAteVencimentoRec.Text = "Sim Descontar após Vencimento";
            }
            else
            {
                ckxAteVencimentoRec.Text = "Não Descontar após o Vencimento";
            }

            //info.baixa;
            //tbxBoleto.Text = info.boleto;
            //tbxBoletoNro.Text = info.boletonro.ToString();
            /*if (info.chegou == "S")
            {
                ckxChegou.Checked = true;
                ckxChegou.Text = "Sim - enviou a cobrança";
            }
            else
            {
                ckxChegou.Checked = false;
                ckxChegou.Text = "Não - enviou a cobrança";
            }
            */
            //            tbxDataLanca.Text = info.datalanca.ToString("dd/MM/yyyy");
            /*if (info.despesapublica == "S")
            {
                ckxDespesaPublica.Checked = true;
                ckxDespesaPublica.Text = "Sim - conferido";
            }
            else
            {
                ckxDespesaPublica.Checked = false;
                ckxDespesaPublica.Text = "Não - conferido";
            }*/
            tbxDuplicataRec.Text = info.duplicata.ToString();  
            //tbxDv.Text = info.dv;
            tbxEmissaoRec.Text = info.emissao.ToString("dd/MM/yyyy");
            //tbxEmitente.Text = info.emitente;
            tbxFilialRec.Text = info.filial.ToString();
            /*
            //tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + info.idbanco);
            //tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + info.idbanco);

            //tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID= " + info.idbancoint);
            //tbxBancoNomeInterno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + info.idbancoint);

            tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + info.idcentrocusto);
            tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID= " + info.idcentrocusto);

            tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID= " + info.idcodigoctabil);
            tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID= " + info.idcodigoctabil);
             */
            tbxDocumentoRec.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + info.iddocumento);
            /*
            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + info.idformapagto);
            tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + info.idformapagto);

            tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + info.idcliente);

            tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + info.idhistorico);
            tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT NOME FROM HISTORICOS where id=" + info.idhistorico);

            if (tbxDocumento.Text == "CTO")
            {
                tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM CONTRATO where id=" + info.idnotafiscal);
                tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOTALLIQUIDO FROM CONTRATO where id=" + info.idnotafiscal);
                tbxNotaDoc.Text = tbxDocumento.Text;
            }
            else
            {
                tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFVENDA where id=" + info.idnotafiscal);
                tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOTALNOTAFISCAL FROM NFVENDA where id=" + info.idnotafiscal);
                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFVENDA where id=" + info.idnotafiscal)) > 0)
                {
                    Int32 idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFVENDA where id=" + info.idnotafiscal));
                    if (idDocNFV == 0)
                        idDocNFV = clsInfo.zdocumento;

                    tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM DOCFISCAL where id=" + idDocNFV);
                }
                else
                {
                    tbxNotaDoc.Text = "";
                }

            }
            tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTITULO where id=" + info.idsitbanco);
            tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTITULO where id=" + info.idsitbanco);

            tbxVendedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + info.idvendedor);

            //info.imprimir;
            tbxObserva.Text = info.observa;
             */
            tbxPosicaoRec.Text = info.posicao.ToString();
            /*
            tbxParcelas.Text = info.posicaofim.ToString();
            */
            tbxValorRec.Text = info.valor.ToString("N2");
            //info.valorbaixando;
            //tbxValorComissao.Text = info.valorcomissao.ToString("N2");
            //            tbxValorComissaoGer.Text = info.valorcomissaoger.ToString("N2");

            tbxValorDescontoRec.Text = info.valordesconto.ToString("N2");
            tbxDevolucaoRec.Text = info.valordevolvido.ToString("N2");
            tbxValorJurosRec.Text = info.valorjuros.ToString("N2");
            tbxValorLiquidoRec.Text = info.valorliquido.ToString("N2");
            tbxValorMultaRec.Text = info.valormulta.ToString("N2");
            tbxRecebidoRec.Text = info.valorpago.ToString("N2");
            tbxVencimentoRec.Text = info.vencimento.ToString("dd/MM/yyyy");
            

            tbxMultaRec.Text = "0";
            tbxDescontoRec.Text = "0";
            tbxSaldoRec.Text = "0";
            tbxRecebidoRec.Text = "0";
            tbxDevolucaoRec.Text = info.valordevolvido.ToString("N2");
            tbxDiferencaRec.Text = "0";
            tbxPagandoRec.Text = "0";

            // Se a Data da Baixa for superior a 3 dias enviar uma mensagem
            //dataBaixa = DateTime.Parse(tbxDataBaixa.Text);
            //dataVencimento = DateTime.Parse(tbxVencimento.Text);
            /*
            dias = dataBaixa.Subtract(dataVencimento).Days;
            if (dias > 0)
            {
                MessageBox.Show("Data da Baixa com " + dias + " dias de diferença." + Environment.NewLine + "Diferença entre a Data de Vencimento e a Data da Baixa ");
                tbxDataBaixa.Select();
                tbxDataBaixa.SelectAll();
            }
            if (tbxFormaPagto.Text.Substring(0, 2) == "CH")
            { // Se for cheque colocar a data de deposito como credito
                tbxDataCredito.Text = tbxVencimento.Text;
            }
            else
            {
                tbxDataCredito.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            */
        }

        private void btnIdCobrancaCodRec_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCobrancaCodRec.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", 0, "Situacao Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnIdCobrancaHisRec_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCobrancaHisRec.Name;
            frmSituacaoCobrancaCod1Pes frmSituacaoCobrancaCod1Pes = new frmSituacaoCobrancaCod1Pes();
            frmSituacaoCobrancaCod1Pes.Init(0, recebida_IdCobrancaCodRec);

            clsFormHelper.AbrirForm(this.MdiParent, frmSituacaoCobrancaCod1Pes, clsInfo.conexaosqldados);

        }

        private void btnIdHistoricoCobrancaRec_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdHistoricoCobrancaRec.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", recebida_IdHistoricoCobrancaRec, "Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);

        }

        private void btnIdCentroCustoCobrancaRec_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCentroCustoCobrancaRec.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", recebida_IdCentroCustoCobrancaRec, "Centro de Custos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);

        }

        private void tbxBancoIntDes_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

