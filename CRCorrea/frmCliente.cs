using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Transactions;
using System.Windows.Forms;


namespace CRCorrea
{
    public partial class frmCliente : Form
    {
        Int32 id;
        Int32 idcidade;
        Int32 idestado;
        Int32 idzona;
        Int32 idramo;
        Int32 idformapagto;
        Int32 idcondpagto;
        Int32 idregimeapuracao;
        Int32 idtransportadora;
        Int32 idredespacho;
        Int32 idbanco;
        Int32 idaprovadopor;
        Int32 idprospeccao;
        Int32 idcnae;
        //Int32 idrepresentante; -- tirei pois o certo éo idvendedor - Ricardo
        Int32 idcoordenador;
        Int32 idsupervisor;
        Int32 idvendedor;
        Int32 iddizeresnota;

        clsClienteBLL clsClienteBLL;
        clsClienteInfo clsClienteInfo;
        clsClienteInfo clsClienteInfoOld;

        DataGridViewRowCollection rows;

        clsClienteEnderecoBLL clsClienteEnderecoBLL;
        clsClienteEnderecoInfo clsClienteEnderecoInfo;
        DataTable dtClienteEndereco;
        BackgroundWorker bwrClienteEndereco;
        GridColuna[] dtClienteEnderecoColunas;

        clsClienteObservaBLL clsClienteObservaBLL;
        clsClienteObservaInfo clsClienteObservaInfo;
        DataTable dtClienteObserva;
        BackgroundWorker bwrClienteObserva;
        GridColuna[] dtClienteObservaColunas;

        clsClienteContatoBLL clsClienteContatoBLL;
        clsClienteContatoInfo clsClienteContatoInfo;
        DataTable dtClienteContato;
        BackgroundWorker bwrClientecontato;
        GridColuna[] dtClienteContatoColunas;

        //clsClienterequisitoBLL clsClienterequisitoBLL;
        //clsClienterequisitoInfo clsClienterequisitoInfo;
        DataTable dtClienterequisito;
        BackgroundWorker bwrClienterequisito;
        GridColuna[] dtClienterequisitoColunas;

        DataTable dtFichaFinanceira;
        DataTable dtFichaComercial;
        DataTable dtFichaComercialCompras;


        SqlConnection scn;
        SqlCommand scd;
        SqlDataReader sdr;
        String cabecalho;

        clsRptFluxoFinanceiroInfo clsRptFluxoFinanceiroInfo = new clsRptFluxoFinanceiroInfo();
        clsRptFluxoFinanceiroBLL clsRptFluxoFinanceiroBLL = new clsRptFluxoFinanceiroBLL();

        public frmCliente()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id, DataGridViewRowCollection _rows)
        {
            id = _id;
            rows = _rows;
        }
        public void Init(Int32 _id)
        {
            id = _id;
            //rows = _rows;
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tab_cnae order by codigo", tbxCnae_codigo);
            //String query = "select cidades.nome + '   -   ' + ESTADOS.ESTADO from cidades " +
            //    " left join estados on estados.id = CIDADES.IDESTADO ";
            //clsVisual.FillComboBox(cbxCidade, query, clsInfo.conexaosqldados);
            //            clsVisual.FillComboBox(cbxCidade, "select nome from cidades order by nome", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxUf, "select estado from estados order by estado", clsInfo.conexaosqldados);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from zonas order by codigo", tbxZona);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from ramo order by codigo", tbxRamo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from regimeapuracao order by codigo", tbxRegimeapuracao);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente ", tbxTransportadora);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente ", tbxRedespacho);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='V' or tipo='E'", tbxRepresentante);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from condpagto where ativo = 'S' order by codigo", tbxCondpagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select nome from situacaotipotitulo order by nome", tbxFormapagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tab_bancos order by codigo", tbxNrobanco);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select usuario from usuario where ativo=1", tbxAprovadonome);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='V' or tipo='E'", tbxCoordenador);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='V' or tipo='E'", tbxSupervisor);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from dizeresnfv order by codigo", tbxCodigo_Dizeres);

            dtClienteEnderecoColunas = new GridColuna[]
            {
                new GridColuna("Tipo", "tipoendnome", 110, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Nome", "cobnome", 120, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Endereço", "endereco", 240, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Bairro", "bairro", 100, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Cidade", "cidade", 100, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("UF", "uf", 35, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("CEP", "cep", 70, true, DataGridViewContentAlignment.MiddleLeft)
            };

            dtClienteObservaColunas = new GridColuna[]
            {
                new GridColuna("Tipo", "LIGACAO", 30, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Data", "DATA", 90, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Recebeu Recado", "EMITENTE", 95, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Recado Destinado", "DESTINATARIO", 95, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Pessoa Contatado", "contatado", 95, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Conteudo Abordado", "observar", 500, true, DataGridViewContentAlignment.MiddleLeft)

            };

            dtClienteContatoColunas = new GridColuna[]
            {
                new GridColuna("Tipo", "tipocontato", 75, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Contato", "contato", 120, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Setor", "setor", 80, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("", "ddd", 20, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Telefone", "telefone", 70, true, DataGridViewContentAlignment.MiddleCenter)
            };

            dtClienterequisitoColunas = new GridColuna[]
            {
                new GridColuna("Ordem", "ordem", 70, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Requisito", "requisito", 1000, true, DataGridViewContentAlignment.MiddleLeft)
            };

            clsClienteBLL = new clsClienteBLL();
            clsClienteEnderecoBLL = new clsClienteEnderecoBLL();
            clsClienteContatoBLL = new clsClienteContatoBLL();
            clsClienteObservaBLL = new clsClienteObservaBLL();
            //clsClienterequisitoBLL = new clsClienterequisitoBLL();



            ClienteRegistro();
        }

        void ClienteRegistro()
        {
            if (id == 0)
            {
                clsClienteInfo = new clsClienteInfo();
                clsClienteInfo.agencia = "";
                clsClienteInfo.andar = "";
                clsClienteInfo.ativo = "S";
                clsClienteInfo.bairro = "";
                clsClienteInfo.cadastrado = DateTime.Now;
                clsClienteInfo.cep = "";

                clsDiversos Cgc = new clsDiversos();
                Cgc.CriarNumeroCnpj("");

                clsClienteInfo.cgc = Cgc.NumeroCNPJ;
                clsClienteInfo.clicontab = 0;
                clsClienteInfo.clienteaprovado = "N";
                clsClienteInfo.clientedesde = DateTime.Now;
                clsClienteInfo.codigocliente = "0";
                clsClienteInfo.cognome = "";
                clsClienteInfo.comissaocomo = "V";
                clsClienteInfo.comissaopagto = "D";
                clsClienteInfo.comissaopor = "G";
                clsClienteInfo.comple = "";
                clsClienteInfo.contato = "";
                clsClienteInfo.credito = "";
                clsClienteInfo.ctacorrente = "";
                clsClienteInfo.dataaprovado = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.datacompra = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.datafichafinanc = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.datanasc = DateTime.MinValue;
                clsClienteInfo.ddd = "";
                clsClienteInfo.desconto = 0;
                clsClienteInfo.descontacofinsent = "N";
                clsClienteInfo.descontacofinssai = "N";
                clsClienteInfo.descontapispasepent = "N";
                clsClienteInfo.descontapispasepsai = "N";
                clsClienteInfo.diasemana = "Domingo";
                clsClienteInfo.email = "";
                clsClienteInfo.emailnfe = "";
                clsClienteInfo.endereco = "";
                clsClienteInfo.endtipo = "";
                clsClienteInfo.freteincluso = "N";
                clsClienteInfo.freteporconta = "F";
                clsClienteInfo.ftp = "";
                clsClienteInfo.homepage = "";
                clsClienteInfo.ibge = "";
                clsClienteInfo.idbanco = clsInfo.zbanco;
                clsClienteInfo.idestado = clsInfo.zempresa_ufid;

                clsClienteInfo.cidade = "";
                clsClienteInfo.idcondpagto = clsInfo.zcondpagto; 

                clsClienteInfo.idformapagto = clsInfo.zformapagto;
                clsClienteInfo.idprospeccao = 0;
                clsClienteInfo.idramo = clsInfo.zramo;
                clsClienteInfo.idregimeapuracao = clsInfo.zregimeapuracao;
                clsClienteInfo.idtransportadora = clsInfo.zempresaclienteid;
                clsClienteInfo.idredespacho = clsInfo.zempresaclienteid;
                clsClienteInfo.idzona = clsInfo.zzona;
                //clsClienteInfo.idrepresentante = clsInfo.zempresaclienteid;
                clsClienteInfo.idsupervisor = clsInfo.zempresaclienteid;
                clsClienteInfo.idcoordenador = clsInfo.zempresaclienteid;
                clsClienteInfo.idvendedor = clsInfo.zempresaclienteid;
                clsClienteInfo.ie = "";
                clsClienteInfo.imunicipal = "";
                clsClienteInfo.meiodetransporte = "N";
                clsClienteInfo.nome = "";
                clsClienteInfo.numeroend = "0";
                clsClienteInfo.observa = "";
                clsClienteInfo.pais = "BRA";
                clsClienteInfo.pessoa = "J";
                clsClienteInfo.pix = "";
                clsClienteInfo.regiao = "CO";
                clsClienteInfo.suframa = "";
                clsClienteInfo.telefone = "";
                clsClienteInfo.temfaturamento = "N";
                clsClienteInfo.tipo = "C";
                clsClienteInfo.tipocomple = "";
                clsClienteInfo.tiponumero = "Nº";
                clsClienteInfo.titularcta = "";
                clsClienteInfo.ultdatnf = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.ultdatorc = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.isentoipi = "N";
                clsClienteInfo.iddizeresnota = clsClienteInfo.iddizeresnota;
                clsClienteInfo.revendedor = "N";
                clsClienteInfo.contribuinte = "S";
                clsClienteInfo.alc = "N";
                clsClienteInfo.consumo = "S";
                clsClienteInfo.datafichafinanc = DateTime.Now;
                clsClienteInfo.saldofichafinanc = 0;
            }
            else
            {
                clsClienteInfo = clsClienteBLL.Carregar(id, clsInfo.conexaosqldados);
            }

            clsClienteInfoOld = new clsClienteInfo();
            ClienteCampos(clsClienteInfo);
            ClienteInfo(clsClienteInfoOld);

            if (id != 0)
            { // carregar o fluxo financeiro deste cliente
               //tbxDatade.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
               //tbxDataate.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
               //dtFichaFinanceira = clsFinanceiro.FichaFinanceira(0, id, clsClienteInfo.cognome, "VENC", clsParser.DateTimeParse("01/01/1900"), clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()), clsParser.DecimalParse(tbxSaldoFichaFinanc.Text));
               //clsFinanceiro.GridFichaFinanceiraMonta(dgvFichaFinanceira, dtFichaFinanceira, 0);
               
               //// carregar o fluxo comercial do cliente
               //tbxDatadeC.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
               //tbxDataateC.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
               //dtFichaComercial = clsFinanceiro.FichaComercial(0, id, clsClienteInfo.cognome, clsParser.DateTimeParse("01/01/1900"),Parser.DateTimeParse(DateTime.Now.AddYears(10).ToString()));
               //clsFinanceiro.GridFichaComercialMonta(dgvFichaComercial, dtFichaComercial, 0);

            }
            cbxPessoa.Select();
            cbxPessoa.SelectAll();
        }

        void ClienteCampos(clsClienteInfo info)
        {
            id = info.id;
            idaprovadopor = info.idaprovadopor;
            idbanco = info.idbanco;
            idcnae = info.idcnae;
            idcnae = info.idcnae;
            idcondpagto = info.idcondpagto;
            idcoordenador = info.idcoordenador;
            idestado = info.idestado;
            idformapagto = info.idformapagto;
            idprospeccao = info.idprospeccao;
            idramo = info.idramo;
            idregimeapuracao = info.idregimeapuracao;
            idsupervisor = info.idsupervisor;
            idtransportadora = info.idtransportadora;
            idredespacho = info.idredespacho;
            idzona = info.idzona;
            idvendedor = info.idvendedor;
            iddizeresnota = info.iddizeresnota;

            tbxNumero.Text = info.numero.ToString();
            tbxCognome.Text = info.cognome;
            tbxNome.Text = info.nome;
            tbxDatanasc.Text = ExibirData(info.datanasc);
            tbxDdd.Text = info.ddd;
            tbxTelefone.Text = info.telefone;
            tbxContato.Text = info.contato;
            cbxTipo.SelectedIndex = SelecionarIndex(info.tipo, 1, cbxTipo);
            cbxPessoa.SelectedIndex = SelecionarIndex(info.pessoa, 1, cbxPessoa);

            PessoaTipo();

            cbxAtivo.SelectedIndex = SelecionarIndex(info.ativo, 1, cbxAtivo);
            tbxCgc.Text = info.cgc;
            tbxIe.Text = info.ie;
            //tbxImunicipal.Text = info.imunicipal;
            tbxSuframa.Text = info.suframa;
            tbxCnae_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tab_cnae where id=" + idcnae.ToString(),"");
            tbxEmail.Text = info.email;
            tbxHomepage.Text = info.homepage;
            //tbxFtp.Text = info.ftp;
            tbxCep.Text = info.cep;
            info.endtipo = TipoRua(info.endtipo);
            cbxEnd_tipo.SelectedIndex = cbxEnd_tipo.FindString(info.endtipo);
            if (cbxEnd_tipo.SelectedIndex == -1)
            {
                cbxEnd_tipo.SelectedIndex = 0;
            }
            tbxEndereco.Text = info.endereco;
            cbxTiponumero.SelectedIndex = cbxTiponumero.FindString(info.tiponumero);
            if (cbxTiponumero.SelectedIndex == -1)
            {
                cbxTiponumero.SelectedIndex = 0;
            }
            tbxNumeroend.Text = info.numeroend;
            //tbxAndar.Text = info.andar;
            //tbxTipocomple.Text = info.tipocomple;
            cbxAndar.SelectedIndex = cbxAndar.FindString(info.andar);
            cbxTipocomple.SelectedIndex = cbxTipocomple.FindString(info.tipocomple);
            tbxComple.Text = info.comple;
            tbxBairro.Text = info.bairro;
            tbxCidade.Text = info.cidade;   
            cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString(),""));
            if (cbxUf.SelectedIndex == -1)
            {
                cbxUf.SelectedIndex = 0;
            }
            cbxRegiao.SelectedIndex = cbxRegiao.FindString(info.regiao);
            if (cbxRegiao.SelectedIndex == -1)
            {
                cbxRegiao.SelectedIndex = 0;
            }
            tbxIbge.Text = info.ibge;
            tbxCadastrado.Text = ExibirDataCompleta(info.cadastrado);
            tbxZona.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from zonas where id=" + idzona.ToString(),"");
            tbxRamo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from ramo where id=" + idramo.ToString(),"");
            tbxRegimeapuracao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from regimeapuracao where id=" + idregimeapuracao.ToString(), "");
            tbxCodigocliente.Text = info.codigocliente;
            cbxFreteincluso.SelectedIndex = SelecionarIndex(info.freteincluso, 1, cbxFreteincluso);
            cbxFreteporconta.SelectedIndex = SelecionarIndex(info.freteporconta, 1, cbxFreteporconta);
            cbxMeiodetransporte.SelectedIndex = SelecionarIndex(info.meiodetransporte, 1, cbxMeiodetransporte);
            tbxTransportadora.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idtransportadora.ToString(), "");
            tbxRedespacho.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idredespacho.ToString(), "");           
            tbxRepresentante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idvendedor, "");
            tbxComissaorepresentante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idvendedor, "");
            tbxCondpagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id=" + idcondpagto.ToString() + "");
            tbxFormapagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from situacaotipotitulo where id=" + idformapagto.ToString(), "");
            cbxCredito.SelectedIndex = SelecionarIndex(info.credito, 1, cbxCredito);
            tbxLimitecredito.Text = info.limitecredito.ToString("N2");
            tbxNrobanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tab_bancos where id=" + idbanco.ToString(), "");
            tbxBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tab_bancos where id=" + idbanco.ToString(), "");
            tbxAgencia.Text = info.agencia;
            tbxCtacorrente.Text = info.ctacorrente;
            tbxTitularcta.Text = info.titularcta;
            tbxPix.Text = info.pix;
            cbxClienteaprovado.SelectedIndex = SelecionarIndex(info.clienteaprovado, 1, cbxClienteaprovado);
            tbxDataaprovado.Text = ExibirData(info.dataaprovado);
            tbxAprovadonome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usuario from usuario where id=" + idaprovadopor.ToString(), "");
            cbxTemfaturamento.SelectedIndex = SelecionarIndex(info.temfaturamento, 1, cbxTemfaturamento);
            tbxValorminimo.Text = info.valorminimo.ToString();
            tbxClientedesde.Text = ExibirDataCompleta(info.clientedesde);
            tbxDatacompra.Text = ExibirData(info.datacompra);
            tbxMaiorcompra.Text = info.maiorcompra.ToString();
            cbxComissaocomo.SelectedIndex = SelecionarIndex(info.comissaocomo, 1, cbxComissaocomo);
            cbxComissaopagto.SelectedIndex = SelecionarIndex(info.comissaopagto, 1, cbxComissaopagto);
            cbxComisssaopor.SelectedIndex = SelecionarIndex(info.comissaopor, 1, cbxComisssaopor);
            tbxCoordenador.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idcoordenador.ToString(), "");
            tbxCoordenadorComissao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idcoordenador, "");
            tbxSupervisor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idsupervisor.ToString(), "");
            tbxSupervisorComissao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idsupervisor, "");
            tbxComissaoaliquota.Text = info.comissaoaliquota.ToString();
            rbnDescontacofinsent.Checked = (info.descontacofinsent == "S");
            rbnDescontacofinssai.Checked = (info.descontacofinssai == "S");
            rbnDescontapispasepent.Checked = (info.descontapispasepent == "S");
            rbnDescontapispasepsai.Checked = (info.descontapispasepsai == "S");
            tbxDependenteirrf.Text = info.dependenteirrf.ToString();
            tbxEmailnfe.Text = info.emailnfe;
            cbxIsentoipi.SelectedIndex = SelecionarIndex(info.isentoipi, 1, cbxIsentoipi);
            cbxRevendedor.SelectedIndex = SelecionarIndex(info.revendedor, 1, cbxRevendedor);
            cbxContribuinte.SelectedIndex = SelecionarIndex(info.contribuinte, 1, cbxContribuinte);
            cbxAlc.SelectedIndex = SelecionarIndex(info.alc, 1, cbxAlc);
            tbxCodigo_Dizeres.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from dizeresnfv where id=" + iddizeresnota.ToString(), "");
            switch (cbxPessoa.Text.Substring(0, 1))
            {
                case "F":
                    tbxCgc.Text = clsVisual.CamposVisual("CPF", tbxCgc.Text);
                    tbxIe.Text = clsVisual.CamposVisual("RG", tbxIe.Text);
                    break;
                case "J":
                    tbxCgc.Text = clsVisual.CamposVisual("CGC", tbxCgc.Text);
                    tbxIe.Text = clsVisual.CamposVisual("IE", tbxIe.Text);
                    break;
                default:
                    break;
            }
            tbxCep.Text = clsVisual.CamposVisual("CEP", tbxCep.Text);
            if (info.consumo == null)
            {
                info.consumo = "S";
            }
            cbxConsumo.SelectedIndex = SelecionarIndex(info.consumo, 1, cbxConsumo);
            if (info.datafichafinanc == null)
            {
                info.datafichafinanc = DateTime.Parse("01/01/1900");
            }
            tbxDataFichaFinanc.Text = DateTime.Parse(info.datafichafinanc.ToString()).ToString("dd/MM/yyyy");
            tbxSaldoFichaFinanc.Text = clsParser.DecimalParse(info.saldofichafinanc.ToString()).ToString("N2");

            tbxObserva.Text = info.observa;

            tbxDesconto.Text = info.desconto.ToString("N2");
            if (info.diasemana == null)
            {
                info.diasemana = "";
            }
            cbxDiaSemana.SelectedIndex = cbxDiaSemana.FindString(info.diasemana.Trim());

            bwrClienteEndereco_Run();
            bwrClientecontato_Run();
            bwrClienteObserva_Run();
            
            //tbxCognome.Select();
            //tbxCognome.SelectAll();
            cbxPessoa.Select();
            cbxPessoa.SelectAll();
        }

        void ClienteInfo(clsClienteInfo info)
        {
            info.id = id;
            info.idaprovadopor = idaprovadopor;
            info.idbanco = idbanco;
            info.idcnae = idcnae;
            info.idcondpagto = idcondpagto;
            info.idcoordenador = idcoordenador;
            info.idestado = idestado;
            info.idformapagto = idformapagto;
            info.idprospeccao = idprospeccao;
            info.idramo = idramo;
            info.idregimeapuracao = idregimeapuracao;
            info.idsupervisor = idsupervisor;
            info.idtransportadora = idtransportadora;
            info.idredespacho = idredespacho;
            info.idzona = idzona;
            info.idvendedor = idvendedor;
            info.iddizeresnota = iddizeresnota;

            info.numero = clsParser.Int32Parse(tbxNumero.Text);
            info.cognome = tbxCognome.Text.Trim();
            info.nome = tbxNome.Text.Trim();
            info.datanasc = clsParser.DateTimeParse(tbxDatanasc.Text);
            info.ddd = tbxDdd.Text.Trim();
            info.telefone = tbxTelefone.Text.Trim();
            info.contato = tbxContato.Text;
            info.tipo = cbxTipo.Text.Substring(0, 1);
            info.pessoa = cbxPessoa.Text.Substring(0, 1);
            info.ativo = cbxAtivo.Text.Substring(0, 1);
            info.cgc = tbxCgc.Text;
            info.ie = tbxIe.Text.Trim();
            //info.imunicipal = tbxImunicipal.Text.Trim();
            info.suframa = tbxSuframa.Text.Trim();
            info.email = tbxEmail.Text.Replace(" ", "");
            info.homepage = tbxHomepage.Text;
            //info.ftp = tbxFtp.Text;
            info.cep = tbxCep.Text;
            info.endtipo = cbxEnd_tipo.Text.Trim();
            info.endereco = tbxEndereco.Text.Trim();
            info.tiponumero = cbxTiponumero.Text.Substring(0, 1);
            info.numeroend = tbxNumeroend.Text;
            info.andar =  cbxAndar.Text.Trim();
            info.tipocomple = cbxTipocomple.Text.Trim();
            info.comple = tbxComple.Text.Trim();
            info.bairro = tbxBairro.Text.Trim();
            info.cidade = tbxCidade.Text.Trim();
            info.regiao = cbxRegiao.Text.Trim();
            info.ibge = tbxIbge.Text.Trim();
            info.cadastrado = clsParser.DateTimeParse(tbxCadastrado.Text);
            info.codigocliente = tbxCodigocliente.Text.Trim();
            info.freteincluso = cbxFreteincluso.Text.Substring(0, 1);
            info.freteporconta = cbxFreteporconta.Text.Substring(0, 1);
            info.meiodetransporte = cbxMeiodetransporte.Text.Substring(0, 1);
            info.credito = cbxCredito.Text.Substring(0, 1);
            info.limitecredito = clsParser.DecimalParse(tbxLimitecredito.Text);
            info.agencia = tbxAgencia.Text;
            info.ctacorrente = tbxCtacorrente.Text.Trim();
            info.titularcta = tbxTitularcta.Text.Trim();
            info.clienteaprovado = cbxClienteaprovado.Text.Substring(0, 1);
            info.dataaprovado = clsParser.DateTimeParse(tbxDataaprovado.Text);
            info.temfaturamento = cbxTemfaturamento.Text.Substring(0, 1);
            info.valorminimo = clsParser.DecimalParse(tbxValorminimo.Text);
            info.clientedesde = clsParser.DateTimeParse(tbxClientedesde.Text);
            
            info.datacompra = clsParser.DateTimeParse(tbxDatacompra.Text);
            info.maiorcompra = clsParser.DecimalParse(tbxMaiorcompra.Text);
            info.comissaocomo = cbxComissaocomo.Text.Substring(0, 1);
            info.comissaopagto = cbxComissaopagto.Text.Substring(0, 1);
            info.comissaopor = cbxComisssaopor.Text.Substring(0, 1);
            info.comissaocoordenador = 0;
            info.comissaosupervisor = 0;
            info.comissaoaliquota = clsParser.DecimalParse(tbxComissaoaliquota.Text);
            if (rbnDescontacofinsent.Checked == true)
            {
                info.descontacofinsent = "S";
            }
            else
            {
                info.descontacofinsent = "N";
            }

            if (rbnDescontacofinssai.Checked == true)
            {
                info.descontacofinssai = "S";
            }
            else
            {
                info.descontacofinssai = "N";
            }


            if (rbnDescontapispasepent.Checked == true)
            {
                info.descontapispasepent = "S";
            }
            else
            {
                info.descontapispasepent = "N";
            }

            if (rbnDescontapispasepsai.Checked == true)
            {
                info.descontapispasepsai = "S";
            }
            else
            {
                info.descontapispasepsai = "N";
            }
            info.dependenteirrf = clsParser.Int32Parse(tbxDependenteirrf.Text);
            if (info.pais == null)
            {
                info.pais = "BRA";
            }
            info.emailnfe = tbxEmailnfe.Text.Trim().Replace(" ", "");
            info.isentoipi = cbxIsentoipi.Text.Substring(0, 1);
            info.revendedor = cbxRevendedor.Text.Substring(0, 1);
            info.contribuinte = cbxContribuinte.Text.Substring(0, 1);
            info.alc = cbxAlc.Text.Substring(0, 1);
            info.consumo = cbxConsumo.Text.Substring(0, 1);
            info.datafichafinanc = DateTime.Parse(tbxDataFichaFinanc.Text);
            info.saldofichafinanc = clsParser.DecimalParse(tbxSaldoFichaFinanc.Text);
            info.observa = tbxObserva.Text;
            info.desconto = clsParser.DecimalParse(tbxDesconto.Text);
            info.diasemana = cbxDiaSemana.Text;
            info.pix = tbxPix.Text;
        }

        private void ClienteSalvar()
        {
            try
            {
                using (TransactionScope tse = new TransactionScope())
                {
                    clsClienteInfo = new clsClienteInfo();
                    ClienteInfo(clsClienteInfo);
                    clsClienteBLL.VerificaInfo(clsClienteInfo);
                    // verifica e se existe algum campo em branco
                    if (id == 0)
                    {
                        clsClienteInfo.id = clsClienteBLL.Incluir(clsClienteInfo, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        clsClienteBLL.Alterar(clsClienteInfo, clsInfo.conexaosqldados);
                    }

                    // Verificar o Cadastro de Prospecção
//                    clsClienteInfo.idprospeccao = clsClienteBLL.SicronizarCliente(clsClienteInfo.id, clsInfo.conexaosqldados);
                    clsClienteInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from cliente where id=" + clsClienteInfo.id, "0"));
                    clsClienteBLL.Alterar(clsClienteInfo, clsInfo.conexaosqldados);

                    clsClienteEnderecoInfo = new clsClienteEnderecoInfo();
                    foreach (DataRow row in dtClienteEndereco.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            clsClienteEnderecoInfo.id = 0;
                            clsClienteEnderecoInfo.idcliente = clsClienteInfo.id;
                            clsClienteEnderecoInfo.idcidade = clsParser.Int32Parse(row["idcidade"].ToString());
                            clsClienteEnderecoInfo.idestado = clsParser.Int32Parse(row["idestado"].ToString());
                            clsClienteEnderecoInfo.idzona = clsParser.Int32Parse(row["idzona"].ToString());

                            clsClienteEnderecoInfo.bairro = row["bairro"].ToString();
                            clsClienteEnderecoInfo.cep = row["cep"].ToString();
                            clsClienteEnderecoInfo.cgc = row["cgc"].ToString();
                            clsClienteEnderecoInfo.cidade = row["cidade"].ToString();
                            clsClienteEnderecoInfo.cobnome = row["cobnome"].ToString();
                            clsClienteEnderecoInfo.contato = row["contato"].ToString();
                            clsClienteEnderecoInfo.ddd = row["ddd"].ToString();
                            clsClienteEnderecoInfo.dddfax = row["dddfax"].ToString();
                            clsClienteEnderecoInfo.dddi = row["dddi"].ToString();
                            clsClienteEnderecoInfo.email = row["email"].ToString();
                            clsClienteEnderecoInfo.endereco = row["endereco"].ToString();
                            clsClienteEnderecoInfo.ibge = row["ibge"].ToString();
                            clsClienteEnderecoInfo.ie = row["ie"].ToString();
                            clsClienteEnderecoInfo.observa = row["observa"].ToString();
                            clsClienteEnderecoInfo.postal = row["postal"].ToString();
                            clsClienteEnderecoInfo.setor = row["setor"].ToString();
                            clsClienteEnderecoInfo.telefax = row["telefax"].ToString();
                            clsClienteEnderecoInfo.telefone = row["telefone"].ToString();
                            clsClienteEnderecoInfo.tipoendnome = row["tipoendnome"].ToString();
                            clsClienteEnderecoInfo.uf = row["uf"].ToString();

                            row["id"] = clsClienteEnderecoBLL.Incluir(clsClienteEnderecoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            clsClienteEnderecoInfo.id = clsParser.Int32Parse(row["id"].ToString());
                            clsClienteEnderecoInfo.idcliente = clsClienteInfo.id;
                            clsClienteEnderecoInfo.idcidade = clsParser.Int32Parse(row["idcidade"].ToString());
                            clsClienteEnderecoInfo.idestado = clsParser.Int32Parse(row["idestado"].ToString());
                            clsClienteEnderecoInfo.idzona = clsParser.Int32Parse(row["idzona"].ToString());

                            clsClienteEnderecoInfo.bairro = row["bairro"].ToString();
                            clsClienteEnderecoInfo.cep = row["cep"].ToString();
                            clsClienteEnderecoInfo.cgc = row["cgc"].ToString();
                            clsClienteEnderecoInfo.cidade = row["cidade"].ToString();
                            clsClienteEnderecoInfo.cobnome = row["cobnome"].ToString();
                            clsClienteEnderecoInfo.contato = row["contato"].ToString();
                            clsClienteEnderecoInfo.ddd = row["ddd"].ToString();
                            clsClienteEnderecoInfo.dddfax = row["dddfax"].ToString();
                            clsClienteEnderecoInfo.dddi = row["dddi"].ToString();
                            clsClienteEnderecoInfo.email = row["email"].ToString();
                            clsClienteEnderecoInfo.endereco = row["endereco"].ToString();
                            clsClienteEnderecoInfo.ibge = row["ibge"].ToString();
                            clsClienteEnderecoInfo.ie = row["ie"].ToString();
                            clsClienteEnderecoInfo.observa = row["observa"].ToString();
                            clsClienteEnderecoInfo.postal = row["postal"].ToString();
                            clsClienteEnderecoInfo.setor = row["setor"].ToString();
                            clsClienteEnderecoInfo.telefax = row["telefax"].ToString();
                            clsClienteEnderecoInfo.telefone = row["telefone"].ToString();
                            clsClienteEnderecoInfo.tipoendnome = row["tipoendnome"].ToString();
                            clsClienteEnderecoInfo.uf = row["uf"].ToString();

                            clsClienteEnderecoBLL.Alterar(clsClienteEnderecoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            clsClienteEnderecoBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        }
                    }
                    if (dtClienteEndereco.Rows.Count == 0)
                    {

                        if (clsClienteInfo.tipo == "C")
                        {
                            MessageBox.Show("Não foi incluido nenhum endereço ? " + Environment.NewLine +
                                        "Obrigatorio endereço de cobrança !!" + Environment.NewLine +
                                        "Sistema vai incluir - acerte os dados posterior a esta inclusão ");
                            clsClienteEnderecoInfo = new clsClienteEnderecoInfo();
                            clsClienteEnderecoInfo.id = 0;
                            clsClienteEnderecoInfo.idcliente = clsClienteInfo.id;
                            //clsClienteEnderecoInfo.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + clsVisual.RemoveAcentos(cbxCidade.Text).ToUpper() + "' ", "0"));
                            clsClienteEnderecoInfo.idestado = idestado;
                            clsClienteEnderecoInfo.idzona = idzona;

                            clsClienteEnderecoInfo.bairro = tbxBairro.Text.PadRight(' ', ' ').Substring(0, 12);
                            clsClienteEnderecoInfo.cep = tbxCep.Text;
                            clsClienteEnderecoInfo.cgc = tbxCgc.Text;
                            clsClienteEnderecoInfo.cidade = tbxCidade.Text;
                            clsClienteEnderecoInfo.cobnome = tbxNome.Text;
                            clsClienteEnderecoInfo.contato = tbxContato.Text;
                            clsClienteEnderecoInfo.ddd = tbxDdd.Text;
                            clsClienteEnderecoInfo.dddfax = ""; ;
                            clsClienteEnderecoInfo.dddi = "";
                            clsClienteEnderecoInfo.email = tbxEmail.Text;
                            clsClienteEnderecoInfo.endereco = cbxEnd_tipo.Text + " " + tbxEndereco.Text.PadRight(' ', ' ').Substring(0, 27) + " " + cbxTiponumero.Text + " " + tbxNumeroend.Text + " " + cbxAndar.Text + " " + cbxTipocomple.Text + " " + tbxComple.Text;
                            clsClienteEnderecoInfo.ibge = tbxIbge.Text;
                            clsClienteEnderecoInfo.ie = tbxIe.Text;
                            clsClienteEnderecoInfo.observa = "";
                            clsClienteEnderecoInfo.postal = "";
                            clsClienteEnderecoInfo.setor = "";
                            clsClienteEnderecoInfo.telefax = "";
                            clsClienteEnderecoInfo.telefone = tbxTelefone.Text;
                            clsClienteEnderecoInfo.tipoendnome = "1 - Cobrança";
                            clsClienteEnderecoInfo.uf = cbxUf.Text;

                            Int32 idEndereco = clsClienteEnderecoBLL.Incluir(clsClienteEnderecoInfo, clsInfo.conexaosqldados);
                        }
                    }
                    clsClienteObservaInfo = new clsClienteObservaInfo();
                    foreach (DataRow row in dtClienteObserva.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            clsClienteObservaInfo.id = 0;
                            clsClienteObservaInfo.idemitente = clsParser.Int32Parse(row["idemitente"].ToString());
                            clsClienteObservaInfo.idcliente = clsClienteInfo.idprospeccao;
                            clsClienteObservaInfo.idvendedor = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());

                            clsClienteObservaInfo.contatado = row["contatado"].ToString();
                            clsClienteObservaInfo.contatoagenda = row["contatoagenda"].ToString();
                            clsClienteObservaInfo.contatofim = row["contatofim"].ToString();
                            clsClienteObservaInfo.data =  clsParser.DateTimeParse(row["data"].ToString());
                            clsClienteObservaInfo.dataagenda = clsParser.DateTimeParse(row["dataagenda"].ToString());
                            clsClienteObservaInfo.datafim = clsParser.DateTimeParse(row["datafim"].ToString());
                            clsClienteObservaInfo.emitente = row["emitente"].ToString();
                            clsClienteObservaInfo.fechada = row["fechada"].ToString();
                            clsClienteObservaInfo.ligacao = row["ligacao"].ToString();
                            clsClienteObservaInfo.observar = row["observar"].ToString();
                            clsClienteObservaInfo.observaragenda = row["observaragenda"].ToString();

                            row["id"] = clsClienteObservaBLL.Incluir(clsClienteObservaInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            clsClienteObservaInfo.id = clsParser.Int32Parse(row["ID"].ToString());
                            clsClienteObservaInfo.idemitente = clsParser.Int32Parse(row["idemitente"].ToString());
                            clsClienteObservaInfo.idcliente = clsClienteInfo.idprospeccao;
                            clsClienteObservaInfo.idvendedor = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());

                            clsClienteObservaInfo.contatado = row["contatado"].ToString();
                            clsClienteObservaInfo.contatoagenda = row["contatoagenda"].ToString();
                            clsClienteObservaInfo.contatofim = row["contatofim"].ToString();
                            clsClienteObservaInfo.data = clsParser.DateTimeParse(row["data"].ToString());
                            clsClienteObservaInfo.dataagenda = clsParser.DateTimeParse(row["dataagenda"].ToString());
                            clsClienteObservaInfo.datafim = clsParser.DateTimeParse(row["datafim"].ToString());
                            clsClienteObservaInfo.emitente = row["emitente"].ToString();
                            clsClienteObservaInfo.fechada = row["fechada"].ToString();
                            clsClienteObservaInfo.ligacao = row["ligacao"].ToString();
                            clsClienteObservaInfo.observar = row["observar"].ToString();
                            clsClienteObservaInfo.observaragenda = row["observaragenda"].ToString();

                            clsClienteObservaBLL.Alterar(clsClienteObservaInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            clsClienteObservaBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        }
                    }

                    if (dtClienteContato.Rows.Count == 0)
                    {
                        throw new Exception("Necessário cadastrar ao menos um contato");
                    }
                    clsClienteContatoInfo = new clsClienteContatoInfo();
                    foreach (DataRow row in dtClienteContato.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            clsClienteContatoInfo.id = 0;
                            clsClienteContatoInfo.idcliente = clsClienteInfo.id;

                            clsClienteContatoInfo.celular = row["CELULAR"].ToString();
                            clsClienteContatoInfo.tipocontato = row["TIPOCONTATO"].ToString().Substring(0, 1);
                            clsClienteContatoInfo.contato = row["CONTATO"].ToString();
                            clsClienteContatoInfo.ddd = row["DDD"].ToString();
                            clsClienteContatoInfo.dddcelular = row["DDDCELULAR"].ToString();
                            clsClienteContatoInfo.dddfax = row["DDDFAX"].ToString();
                            clsClienteContatoInfo.email = row["EMAIL"].ToString();
                            clsClienteContatoInfo.observa = row["OBSERVA"].ToString();
                            clsClienteContatoInfo.ramal = clsParser.Int32Parse(row["RAMAL"].ToString());
                            clsClienteContatoInfo.ramalfax = clsParser.Int32Parse(row["RAMALFAX"].ToString());
                            clsClienteContatoInfo.setor = row["SETOR"].ToString();
                            clsClienteContatoInfo.telefax = row["TELEFAX"].ToString();
                            clsClienteContatoInfo.telefone = row["TELEFONE"].ToString();

                            row["id"] = clsClienteContatoBLL.Incluir(clsClienteContatoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            clsClienteContatoInfo.id = clsParser.Int32Parse(row["id"].ToString());
                            clsClienteContatoInfo.idcliente = clsParser.Int32Parse(row["idcliente"].ToString());

                            clsClienteContatoInfo.celular = row["CELULAR"].ToString();
                            clsClienteContatoInfo.tipocontato = row["TIPOCONTATO"].ToString().Substring(0, 1);
                            clsClienteContatoInfo.contato = row["CONTATO"].ToString();
                            clsClienteContatoInfo.ddd = row["DDD"].ToString();
                            clsClienteContatoInfo.dddcelular = row["DDDCELULAR"].ToString();
                            clsClienteContatoInfo.dddfax = row["DDDFAX"].ToString();
                            clsClienteContatoInfo.email = row["EMAIL"].ToString();
                            clsClienteContatoInfo.observa = row["OBSERVA"].ToString();
                            clsClienteContatoInfo.ramal = clsParser.Int32Parse(row["RAMAL"].ToString());
                            clsClienteContatoInfo.ramalfax = clsParser.Int32Parse(row["RAMALFAX"].ToString());
                            clsClienteContatoInfo.setor = row["SETOR"].ToString();
                            clsClienteContatoInfo.telefax = row["TELEFAX"].ToString();
                            clsClienteContatoInfo.telefone = row["TELEFONE"].ToString();

                            clsClienteContatoBLL.Alterar(clsClienteContatoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            clsClienteContatoBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        }
                    }

                    // Cliente  - Requisito
                    //if (dtClienterequisito != null)
                    //{
                    //    foreach (DataRow row in dtClienterequisito.Rows)
                    //    {
                    //        clsClienterequisitoInfo = new clsClienterequisitoInfo();
                    //        if (row.RowState == DataRowState.Added)
                    //        {
                    //            clsClienterequisitoInfo.id = 0;
                    //            clsClienterequisitoInfo.idcliente = clsClienteInfo.id;
                    //            clsClienterequisitoInfo.ordem = clsParser.Int32Parse(row["ordem"].ToString());
                    //            clsClienterequisitoInfo.requisito = row["requisito"].ToString();

                    //            row["id"] = clsClienterequisitoBLL.Incluir(clsClienterequisitoInfo, clsInfo.conexaosqldados);
                    //        }
                    //        else if (row.RowState == DataRowState.Modified)
                    //        {
                    //            clsClienterequisitoInfo.id = clsParser.Int32Parse(row["id"].ToString());
                    //            clsClienterequisitoInfo.idcliente = clsClienteInfo.id;
                    //            clsClienterequisitoInfo.ordem = clsParser.Int32Parse(row["ordem"].ToString());
                    //            clsClienterequisitoInfo.requisito = row["requisito"].ToString();

                    //            clsClienterequisitoBLL.Alterar(clsClienterequisitoInfo, clsInfo.conexaosqldados);
                    //        }
                    //        else if (row.RowState == DataRowState.Deleted)
                    //        {
                    //            clsClienterequisitoBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                    //        }
                    //    }
                    //}

                    id = clsClienteInfo.id;
                    frmClienteVis.id = id;
                    idprospeccao = clsClienteInfo.idprospeccao;

                    tse.Complete();
                    //passa para o form de visualização o id
                    frmClienteVis.id = id;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            if (dtClienterequisito != null)
            {
                dtClienterequisito.AcceptChanges();
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

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void ControlKeyDownCep(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownCep((TextBox)sender, e);
        }

        private void ControlKeyDownNumero(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownNumero((TextBox)sender, e);
        }

        private void ControlKeyDownIe(object sender, KeyEventArgs e)
        {
            if (cbxPessoa.Text.Substring(0, 1) == "J")
            {
                clsVisual.ControlKeyDownIE((TextBox)sender, e, cbxUf.Text);
            }
            else
            {
                clsVisual.ControlKeyDown(sender, e);
            }
        }

        void TrataCampos(Control ctl)
        {
            PessoaTipo();

            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {

                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
            if (ctl.Name == tbxCognome.Name)
            {
                tbxCognome.Text = tbxCognome.Text.Trim();

                if (tbxCognome.Text != "")
                {
                    Int32 id_registro = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cognome='" + tbxCognome.Text + "'","0"));
                    if (id_registro > 0 && id_registro != id)
                    {
                        MessageBox.Show("Cognome já existe. Tente novamente.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxCognome.Select();
                        tbxCognome.SelectAll();
                    }
                }
            }
            else if (ctl.Name == tbxCgc.Name)
            {
                tbxCgc.Text = tbxCgc.Text.Trim();
                String s = tbxCgc.Text;
                s = s.Replace(".", "");
                s = s.Replace("-", "");
                s = s.Replace("/", "");

                if (s != "")
                {
                    Int32 id_registro = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cgc='" + s + "'","0"));
                    if (id_registro > 0 && id_registro != id)
                    {
                        MessageBox.Show("CPF/RG já existe. Tente novamente.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxCgc.Select();
                        tbxCgc.SelectAll();
                    }
                }
            }
            else if (ctl.Name == tbxZona.Name)
            {

                //idzona = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "ZONAS", "ID", "CODIGO", tbxZona.Text));
                idzona = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from zonas where codigo = '" + tbxZona.Text + "'","0"));

                if (idzona == 0 && tbxZona.Text.Length > 0)
                {
                    tbxZona.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxRamo.Name)
            {
                //idramo = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "RAMO", "ID", "CODIGO", tbxRamo.Text));
                idramo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ramo where codigo = '" + tbxRamo.Text + "'", "0"));
                if (idramo == 0 && tbxRamo.Text.Length > 0)
                {
                    tbxRamo.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxRegimeapuracao.Name)
            {
                //idregimeapuracao = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "REGIMEAPURACAO", "ID", "CODIGO", tbxRegimeapuracao.Text));
                idregimeapuracao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from regimeapuracao where codigo = '" + tbxRegimeapuracao.Text + "'", "0"));
                if (idregimeapuracao == 0 && tbxRegimeapuracao.Text.Length > 0)
                {
                    tbxRegimeapuracao.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxCnae_codigo.Name)
            {
                //idcnae = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_CNAE", "ID", "CODIGO", tbxCnae_codigo.Text));
                idcnae = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tab_cnae where codigo = '" + tbxCnae_codigo.Text + "'", "0"));
                if (idcnae == 0 && tbxCnae_codigo.Text.Length > 0)
                {
                    tbxCnae_codigo.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }                
            else if (ctl.Name == tbxTransportadora.Name)
            {
                idtransportadora = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cognome ='" + tbxTransportadora.Text + "'", "0"));
                if (idtransportadora == 0 && tbxTransportadora.Text.Length > 0)
                {
                    tbxTransportadora.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxRedespacho.Name)
            {
                    idredespacho = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cognome ='" + tbxRedespacho.Text + "'", "0"));
                    if (idredespacho == 0 && tbxRedespacho.Text.Length > 0)
                {
                    tbxRedespacho.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxRepresentante.Name)
            {
                //idvendedor = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxRepresentante.Text));
                idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cognome ='" + tbxRepresentante.Text + "'", "0"));
                if (idvendedor == 0 && tbxRepresentante.Text.Length > 0)
                {
                    tbxRepresentante.Text = "";
                    tbxComissaorepresentante.Text = "0";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
                else
                {
                    tbxComissaorepresentante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idvendedor,"");
                }
            }
            else if (ctl.Name == tbxCoordenador.Name)
            {
                //idcoordenador = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxCoordenador.Text));
                idcoordenador = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cognome ='" + tbxCoordenador.Text + "'", "0"));
                if (idcoordenador == 0 && tbxCoordenador.Text.Length > 0)
                {
                    tbxCoordenador.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
                else
                {
                    tbxCoordenadorComissao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idcoordenador,"");
                }
            }
            else if (ctl.Name == tbxSupervisor.Name)
            {
                //idsupervisor = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxSupervisor.Text));
                idsupervisor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cognome ='" + tbxSupervisor.Text + "'", "0"));
                if (idsupervisor == 0 && tbxSupervisor.Text.Length > 0)
                {
                    tbxSupervisor.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
                else
                {
                    tbxSupervisorComissao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idsupervisor,"");
                }
            }
            else if (ctl.Name == tbxCondpagto.Name)
            {
                //idcondpagto = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", tbxCondpagto.Text));
                idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo ='" + tbxCondpagto.Text + "'", "0"));
                if (idcondpagto == 0 && tbxCondpagto.Text.Length > 0)
                {
                    tbxCondpagto.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxFormapagto.Name)
            {
                //idformapagto = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "ID", "NOME", tbxFormapagto.Text));
                idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where nome ='" + tbxFormapagto.Text + "'", "0"));
                if (idformapagto == 0 && tbxFormapagto.Text.Length > 0)
                {
                    tbxFormapagto.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxNrobanco.Name)
            {
                //idbanco = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "ID", "CODIGO", tbxNrobanco.Text));
                idbanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tab_bancos where codigo ='" + tbxNrobanco.Text + "'", "0"));

                //tbxBanco.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "COGNOME", "ID", idbanco);
                tbxBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from tab_bancos where id = " + idbanco, "");

                if (idbanco == 0 && tbxNrobanco.Text.Length > 0)
                {
                    tbxNrobanco.Text = "";
                    tbxBanco.Text = "";

                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxCodigo_Dizeres.Name)
            {
                //iddizeresnota = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DIZERESNFV", "ID", "CODIGO", tbxCodigo_Dizeres.Text));
                iddizeresnota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from dizeresnfv where codigo ='" + tbxCodigo_Dizeres.Text + "'", "0"));
                if (iddizeresnota == 0 && tbxCodigo_Dizeres.Text.Length > 0)
                {
                    tbxCodigo_Dizeres.Text = "";
                    ctl.Select();
                    ((TextBox)ctl).SelectAll();
                }
            }
            else if (ctl.Name == tbxCep.Name)
            {
                if (tbxEndereco.Text == "")
                {
                    BuscaCEP Cep = new BuscaCEP();
                    Cep.Buscar(tbxCep.Text);

                    tbxEndereco.Text = Cep.Logradouro;
                    tbxBairro.Text = Cep.Bairro;
                    String Cidade = Cep.Localidade;
                    Cidade = clsVisual.RemoveAcentos(Cidade);
                    Cidade = Cidade.Replace("-", " ");
                    //cbxCidade.SelectedIndex = SelecionarIndex(Cidade, 0, Cidade);
                    tbxCidade.Text = Cidade;
                    idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + Cep.UF + "' ", "0"));
                    cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString(), "SP"));
//                    cbxUf.Text = Cep.UF;
                    tbxIbge.Text = Cep.IBGE;


                }

                //if (tbxEndereco.Text == "")
                //{
                //    String[] CEP = clsVisual.BuscaCep(tbxCep.Text);

                //    if (CEP != null)
                //    {
                //        cbxUf.SelectedIndex = cbxUf.FindString(CEP[0].ToString().ToUpper());
                //        tbxCidade.Text = CEP[1].ToString().ToUpper();
                //        tbxBairro.Text = CEP[2].ToString().ToUpper();
                //        String EndTipo = CEP[3].ToString().ToUpper();
                //        tbxEndereco.Text = CEP[4].ToString().ToUpper();
                //        tbxIbge.Text = CEP[5].ToString().ToUpper();
                //        cbxRegiao.SelectedIndex = cbxRegiao.FindString(CEP[6].ToUpper());

                //        EndTipo = TipoRua(EndTipo);

                //        cbxEnd_tipo.SelectedIndex = cbxEnd_tipo.FindString(EndTipo);
                //        if (cbxEnd_tipo.SelectedIndex == -1)
                //        {
                //            cbxEnd_tipo.SelectedIndex = 0;
                //        }

                //    }

                //    cbxEnd_tipo.Select();
                //    cbxEnd_tipo.SelectAll();
                //}
            }


            if (ctl.Name == cbxUf.Name)
            {
                //idestado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "ESTADOS", "ID", "ESTADO", cbxUf.Text));
                idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + cbxUf.Text + "' ","0"));
                if (idestado == 0 && ((TextBox)ctl).Text.Trim().Length > 0)
                {
                    ((ComboBox)ctl).SelectedIndex = 0;
                }
            }

            //if (ctl.Name ==  cbxCidade.Name)
            //{
            //    CapturarCidadeUF();
            //}

            tbxLimitecredito.Text = clsParser.DecimalParse(tbxLimitecredito.Text).ToString("N2");
            tbxValorminimo.Text = clsParser.DecimalParse(tbxValorminimo.Text).ToString("N2");
            tbxDataFichaFinanc.Text = clsParser.DateTimeParse(tbxDataFichaFinanc.Text).ToString("dd/MM/yyyy");
            if (tbxDatade.Text == "")
            {
                tbxDatade.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
                tbxDataate.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                tbxDatade.Text = clsParser.DateTimeParse(tbxDatade.Text).ToString("dd/MM/yyyy");
                tbxDataate.Text = clsParser.DateTimeParse(tbxDataate.Text).ToString("dd/MM/yyyy");
            }
            tbxSaldoFichaFinanc.Text = clsParser.DecimalParse(tbxSaldoFichaFinanc.Text).ToString("N2");
            if (tbxDatadeC.Text == "")
            {
                tbxDatadeC.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
                tbxDataateC.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                tbxDatadeC.Text = clsParser.DateTimeParse(tbxDatadeC.Text).ToString("dd/MM/yyyy");
                tbxDataateC.Text = clsParser.DateTimeParse(tbxDataateC.Text).ToString("dd/MM/yyyy");
            }
            if (tbxDatadeComp.Text == "")
            {
                tbxDatadeComp.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
                tbxDataateComp.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                tbxDatadeComp.Text = clsParser.DateTimeParse(tbxDatadeComp.Text).ToString("dd/MM/yyyy");
                tbxDataateComp.Text = clsParser.DateTimeParse(tbxDatadeComp.Text).ToString("dd/MM/yyyy");
            }

            tbxCognome.Text = clsVisual.RemoveAcentos(tbxCognome.Text);
            tbxNome.Text = clsVisual.RemoveAcentos(tbxNome.Text);
            //            tbxCidade.Text = clsVisual.RemoveAcentos(tbxCidade.Text);

            // Configuração
            tbxCoordenadorComissao.Text = clsParser.DecimalParse(tbxCoordenadorComissao.Text).ToString("N2");
            tbxSupervisorComissao.Text = clsParser.DecimalParse(tbxSupervisorComissao.Text).ToString("N2");
            tbxComissaoaliquota.Text = clsParser.DecimalParse(tbxComissaoaliquota.Text).ToString("N2");

        }

        void PessoaTipo()
        {
            if (cbxTipo.Text != "" && cbxTipo.Text.Substring(0, 1) == "U")
            {
                if (cbxPessoa.Text != "" && cbxPessoa.Text.Substring(0, 1) != "F")
                {
                    cbxPessoa.SelectedIndex = 0;
                }
            }

            if (cbxPessoa.Text == "")
            {
                cbxPessoa.SelectedIndex = 0;
            }

            if (cbxPessoa.Text.Substring(0, 1).ToUpper() == "F")
            { // FISICA
                label35.Text = "Data Nascim.:";
                label15.Text = "CPF:";
                label17.Text = "RG:";
                //label18.Visible = false;
                //tbxImunicipal.Visible = false;
                label46.Visible = false;
                tbxSuframa.Visible = false;
                label45.Visible = false;
                tbxCnae_codigo.Visible = false;
                btnCnae.Visible = false;
                label20.Visible = false;
                tbxHomepage.Visible = false;
                //label19.Visible = false;
                //tbxFtp.Visible = false;
                gbxContatos.Text = "Contatos / Familiares:";
                label25.Text = "Profissão:";
            }
            else
            {  // JURIDICA
                label35.Text = "Data Fundação:";
                label15.Text = "CNPJ:";
                label17.Text = "IE:";
                //label18.Visible = true;
                //tbxImunicipal.Visible = true;
                label46.Visible = true;
                tbxSuframa.Visible = true;
                label45.Visible = true;
                tbxCnae_codigo.Visible = true;
                btnCnae.Visible = true;
                label20.Visible = true;
                tbxHomepage.Visible = true;
                //label19.Visible = true;
                //tbxFtp.Visible = true;
                gbxContatos.Text = "Contatos dentro da Empresa";
                label25.Text = "Ramo Atividade:";
            }
        }

        private void ControlKeyDownCgc(object sender, KeyEventArgs e)
        {
            if (cbxPessoa.Text.Substring(0, 1) == "F")
            {
                clsVisual.ControlKeyDownCpf((TextBox)sender, e);
            }
            else if (cbxPessoa.Text.Substring(0, 1) == "J")
            {
                clsVisual.ControlKeyDownCnpj((TextBox)sender, e);
            }
        }

        private void frmCliente_Activated(object sender, EventArgs e)
        {
            Lupa();
        }

        public void Lupa()
        {
            if (cbxTipo.Text.Substring(0, 1) == "E")
            { // se for empresa - não pode mudar o cgc
                tbxCognome.ReadOnly = true;
                tbxNome.ReadOnly = true;
                tbxCgc.ReadOnly = true;
            }
            if (cbxTipo.Text.Substring(0, 1) != "V")
            {
                //gbxConfiguracao.Visible = false;
                gbxConfiguracao.Enabled = false;
            }
            else
            {
                //gbxConfiguracao.Visible = true;
                gbxConfiguracao.Enabled = true;
            }

            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                if (clsInfo.znomegrid == btnIdZona.Name)
                {
                    idzona = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxZona.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxZona.Select();
                    tbxZona.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdRamo.Name)
                {
                    idramo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRamo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxRamo.Select();
                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdRegimeApuaracao.Name)
                {
                    idregimeapuracao = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRegimeapuracao.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxRegimeapuracao.Select();
                    tbxRegimeapuracao.SelectAll();
                }
                else if (clsInfo.znomegrid == btnRepresentante.Name)
                {
                    idvendedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRepresentante.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxRepresentante.Select();
                    tbxRepresentante.SelectAll();
                }
                else if (clsInfo.znomegrid == btnCoordenador.Name)
                {
                    idcoordenador = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCoordenador.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxCoordenador.Select();
                    tbxCoordenador.SelectAll();
                }
                else if (clsInfo.znomegrid == btnSupervisor.Name)
                {
                    idsupervisor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSupervisor.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxSupervisor.Select();
                    tbxSupervisor.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdCondPagto.Name)
                {
                    idcondpagto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCondpagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCondpagto.Select();
                    tbxCondpagto.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdFormaPagto.Name)
                {
                    idformapagto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxFormapagto.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxFormapagto.Select();
                    tbxFormapagto.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdTransportadora.Name)
                {
                    idtransportadora = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxTransportadora.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxTransportadora.Select();
                    tbxTransportadora.SelectAll();
                }
                else if (clsInfo.znomegrid == btnRedespacho.Name)
                {
                    idredespacho = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRedespacho.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxRedespacho.Select();
                    tbxRedespacho.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdBanco.Name)
                {
                    idbanco = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxNrobanco.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxBanco.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxNrobanco.Select();
                    tbxNrobanco.SelectAll();
                }
                else if (clsInfo.znomegrid == btnCnae.Name)
                {
                    idcnae = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCnae_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCnae_codigo.Select();
                    tbxCnae_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnUsuario.Name)
                {
                    idaprovadopor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxAprovadonome.Text = clsInfo.zrow.Cells["USUARIO"].Value.ToString();
                    tbxAprovadonome.Select();
                    tbxAprovadonome.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIddizeresnota.Name)
                {
                    iddizeresnota = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodigo_Dizeres.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCodigo_Dizeres.Select();
                    tbxCodigo_Dizeres.SelectAll();
                }

            }
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;

            VerificaContato();
        }

        private void btnRegistroPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta() == false)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Primeiro Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistroAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta() == false)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Registro Anterior:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistroProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta() == true)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistroUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta())
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar ?",
                                            Application.CompanyName,
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    ClienteSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxCognome.Select();
                    tbxCognome.SelectAll();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxCognome.Select();
                tbxCognome.SelectAll();
            }
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                clsClienteInfo = new clsClienteInfo();
                ClienteInfo(clsClienteInfo);

                if (clsClienteBLL.Equals(clsClienteInfoOld, clsClienteInfo) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        ClienteSalvar();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        tbxNumero.Select();
                        tbxNumero.SelectAll();
                        return;
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxNumero.Select();
                tbxNumero.SelectAll();
            }
        }

        public Boolean ClienteMovimenta()
        {
            clsClienteInfo = new clsClienteInfo();
            ClienteInfo(clsClienteInfo);

            if (clsClienteBLL.Equals(clsClienteInfoOld, clsClienteInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    ClienteSalvar();
                }
                else if (drt == DialogResult.Cancel)
                {
                    tbxCognome.Select();
                    tbxCognome.SelectAll();
                    return false;
                }
            }

            return true;
        }

        private void btnIdTransportadora_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdTransportadora.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }

        private void btnRedespacho_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnRedespacho.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }

        private void btnIdBanco_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBanco.Name;
            frmTab_BancosPes frmTab_BancosPes = new frmTab_BancosPes();
            frmTab_BancosPes.Init(clsInfo.conexaosqldados, idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_BancosPes, clsInfo.conexaosqldados);
        }

        private void btnIdZona_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdZona.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "ZONAS", idzona, "Zonas/Regiões");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdCondPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCondPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", idcondpagto, "Condição de Pagamentos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idformapagto, "Tipo Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdRamo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdRamo.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "RAMO", idramo, "Ramos de Atividade");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdRegimeApuaracao_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdRegimeApuaracao.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "REGIMEAPURACAO", idzona, "Regime Apuração");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnCep_Click(object sender, EventArgs e)
        {
            BuscaCEP Cep = new BuscaCEP();
            Cep.Buscar(tbxCep.Text);

            tbxEndereco.Text = Cep.Logradouro;
            tbxBairro.Text = Cep.Bairro;
            String Cidade = Cep.Localidade;
            Cidade = clsVisual.RemoveAcentos(Cidade);
            Cidade = Cidade.Replace("-", " ");
            //cbxCidade.SelectedIndex = SelecionarIndex(Cidade, 0, Cidade);
            tbxCidade.Text = Cidade;
            idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + Cep.UF + "' ", "0"));
            cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString(), "SP"));
            tbxIbge.Text = Cep.IBGE;
        }

        private void btnIddizeresnota_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIddizeresnota.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "DIZERESNFV", iddizeresnota, "Dizeres da Nota");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta() == true)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Primeiro Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta() == true)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Registro Anterior:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta() == true)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClienteMovimenta() == true)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    ClienteRegistro();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                clsClienteInfo = new clsClienteInfo();
                ClienteInfo(clsClienteInfo);

                if (clsClienteBLL.Equals(clsClienteInfoOld, clsClienteInfo) == false ||
                    MudouSubRegistros() == true)
                {
                    tspSalvar.PerformClick();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxCognome.Select();
                tbxCognome.SelectAll();
            }
        }

        private bool MudouSubRegistros()
        {
            if (dtClienteEndereco != null)
            {
                foreach (DataRow row in dtClienteEndereco.Rows)
                {
                    if (row.RowState == DataRowState.Added ||
                        row.RowState == DataRowState.Deleted ||
                        row.RowState == DataRowState.Modified)
                    {
                        return true;
                    }
                }
            }

            if (dtClienteContato != null)
            {
                foreach (DataRow row in dtClienteContato.Rows)
                {
                    if (row.RowState == DataRowState.Added ||
                        row.RowState == DataRowState.Deleted ||
                        row.RowState == DataRowState.Modified)
                    {
                        return true;
                    }
                }
            }

            if (dtClienteObserva != null)
            {
                foreach (DataRow row in dtClienteObserva.Rows)
                {
                    if (row.RowState == DataRowState.Added ||
                        row.RowState == DataRowState.Deleted ||
                        row.RowState == DataRowState.Modified)
                    {
                        return true;
                    }
                }
            }

            if (dtClienterequisito != null)
            {
                foreach (DataRow row in dtClienterequisito.Rows)
                {
                    if (row.RowState == DataRowState.Added ||
                        row.RowState == DataRowState.Deleted ||
                        row.RowState == DataRowState.Modified)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        private void CarregaGridFinanc()
        {
            try
            {
                dtFichaFinanceira = clsClienteBLL.GridDadosFinac(id, clsParser.SqlDateTimeParse(tbxDatade.Text).Value, clsParser.SqlDateTimeParse(tbxDataate.Text).Value, clsInfo.conexaosqldados);

                dgvFichaFinanceira.DataSource = dtFichaFinanceira;
                clsGridHelper.MontaGrid(dgvFichaFinanceira,
                         new String[] { "ID", "Nro Dup.", "I" , "F" , "Emissão", "Doc.", "Cog Nome", "Dt Vence",
                                       "Valor", "Forma Pag.", "Observa"},
                         new String[] {"ID", "DUPLICATA", "POSICAO" , "POSICAOFIM" , "EMISSAO", "DOCUMENTO", "COGNOME", "VENCIMENTO",
                                    "VAL", "TITULO", "OBSERVA" },
                             new int[] { 1, 85, 25, 25, 60, 40, 120, 60, 
                                95, 110, 200},
                         new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                         new bool[] { false, true, true, true, true, true, true,
                             true, true,  true, true},
                         true, 0, ListSortDirection.Ascending);


                dgvFichaFinanceira.Columns["VAL"].DefaultCellStyle.Format = "N2";
                dgvFichaFinanceira.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvFichaFinanceira.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                clsGridHelper.FontGrid(dgvFichaFinanceira, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbxDatade_Leave(object sender, EventArgs e)
        {
            CarregaGridFinanc();
        }
        

        private void btnRepresentante_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnRepresentante.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idvendedor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }
        private void btnCoordenador_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCoordenador.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idcoordenador);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }
        private void btnSupervisor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSupervisor.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idsupervisor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnCnae_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCnae.Name;
            frmTab_CNAE_Pes frmTab_CNAE_Pes = new frmTab_CNAE_Pes();
            frmTab_CNAE_Pes.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_CNAE_Pes, clsInfo.conexaosqldados);
        }

        String ExibirData(DateTime d)
        {
            if (d != null && d != DateTime.MinValue)
            {
                return d.ToString("dd/MM/yyyy");
            }
            else
            {
                return "00/00/0000";
            }
        }

        String ExibirDataCompleta(DateTime d)
        {
            if (d != null && d != DateTime.MinValue)
            {
                return d.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                return "00/00/0000 00:00";
            }
        }

        Int32 SelecionarIndex(String valor, Int32 nLetras, ComboBox c)
        {
            String s = "";
            Int32 index = 0;
            Int32 resultado = 0;

            foreach (Object obj in c.Items)
            {
                s = obj.ToString();

                if (s.Substring(0, nLetras) == valor)
                {
                    resultado = index;
                    break;
                }

                index++;
            }

            return resultado;
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnUsuario.Name;
            frmUsuarioPes frmUsuarioPes = new frmUsuarioPes();
            frmUsuarioPes.Init(idaprovadopor,clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(this.MdiParent, frmUsuarioPes, clsInfo.conexaosqldados);
        }

        void bwrClientecontato_DoWork(object sender, DoWorkEventArgs e)
        {
            if(clsParser.Int32Parse(id.ToString()) > 0)
            {
                //idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente=" + id.ToString(),"0"));
                idprospeccao = id;
            }
            else
            {
                //idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente = -1","0"));
                idprospeccao = id;
            }
            String query;
            SqlDataAdapter sda;

            query = "select " +
                        "id, " +
                        "idcliente, " +
                        "tipocontato, " +
                        "contato, " +
                        "setor, " +
                        "email, " +
                        "ddd, " +
                        "telefone, " +
                        "ramal, " +
                        "dddfax, " +
                        "telefax, " +
                        "ramalfax, " +
                        "dddcelular, " +
                        "celular, " +
                        "observa " +
                "from " +
                        "ClienteContato " +
                "where " +
                        "idcliente = @idcliente";

            dtClienteContato = new DataTable();
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = id;
            sda.Fill(dtClienteContato);
        }

        void bwrClientecontato_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataColumn dc = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtClienteContato.Columns.Add(dc);
            for (Int32 i = 1; i <= dtClienteContato.Rows.Count; i++)
            {
                dtClienteContato.Rows[i - 1]["posicao"] = i;
            }
            dtClienteContato.AcceptChanges();

            dgvClientecontato.DataSource = dtClienteContato;
            clsGridHelper.MontaGrid2(dgvClientecontato, dtClienteContatoColunas, true);

            VerificaContato();
        }

        void bwrClientecontato_Run()
        {
            bwrClientecontato = new BackgroundWorker();
            bwrClientecontato.DoWork += new DoWorkEventHandler(bwrClientecontato_DoWork);
            bwrClientecontato.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrClientecontato_RunWorkerCompleted);
            bwrClientecontato.RunWorkerAsync();
        }

        void VerificaContato()
        {
            if (dtClienteContato == null)
            {
                return;
            }


            DataTable dteTemp = dtClienteContato.Copy();
            dteTemp.AcceptChanges();

            foreach (DataRow row in dteTemp.Rows)
            {
                tbxDdd.Text = row["ddd"].ToString();
                tbxTelefone.Text = row["telefone"].ToString();
                tbxContato.Text = row["contato"].ToString();

                if (row["tipocontato"].ToString().Length > 0 &&
                    row["tipocontato"].ToString().Substring(0, 1) == "1")
                {
                    break;
                }
            }
        }

        void bwrClienteObserva_Run()
        {
            bwrClienteObserva = new BackgroundWorker();
            bwrClienteObserva.DoWork += new DoWorkEventHandler(bwrClienteObserva_DoWork);
            bwrClienteObserva.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrClienteObserva_RunWorkerCompleted);
            bwrClienteObserva.RunWorkerAsync();
        }

        private void bwrClienteObserva_DoWork(object sender, DoWorkEventArgs e)
        {           
            if (clsParser.Int32Parse(id.ToString()) > 0)
            {
                //idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente=" + id.ToString(),"0"));
                idprospeccao = id;
            }
            else
            {
                //idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente = -1","0"));
                idprospeccao = id;
            }

            dtClienteObserva = clsClienteObservaBLL.GridDados(id, clsInfo.conexaosqldados);
        }

        private void bwrClienteObserva_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataColumn dc = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtClienteObserva.Columns.Add(dc);
            for (Int32 i = 1; i <= dtClienteObserva.Rows.Count; i++)
            {
                dtClienteObserva.Rows[i - 1]["posicao"] = i;
            }
            dtClienteObserva.AcceptChanges();

            dgvClienteObserva.DataSource = dtClienteObserva;
            clsGridHelper.MontaGrid2(dgvClienteObserva, dtClienteObservaColunas, true);
            dgvClienteObserva.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(0, dgvClienteObserva, "DATA");
            dgvClienteObserva.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";


        }


        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    ClienteSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxCognome.Select();
                    tbxCognome.SelectAll();
                    return;
                }
                frmClienteVis.id = clsClienteInfo.id;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxCognome.Select();
                tbxCognome.SelectAll();
            }
        }

        private void tspClienteobserva_Incluir_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                // pegar o id do cadastro de prospeccao para incluir a observação no cliente correto
                Int32 idclienteprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where id=" + id.ToString(),"0"));
                id = idclienteprospeccao;
                frmClienteObserva frmClienteObserva = new frmClienteObserva();
                frmClienteObserva.Init(0, 0, dtClienteObserva, true, id);

                frmClienteObserva.ShowDialog();
            }
            else
            {
                MessageBox.Show("Antes de incluir uma observação - Sai do Cadastro e Grave e depois retorne aqui para incluir a observação");
            }

        }

        private void tspClienteobserva_Alterar_Click(object sender, EventArgs e)
        {
            if (dgvClienteObserva.CurrentRow != null)
            {
                frmClienteObserva frmClienteObserva = new frmClienteObserva();
                frmClienteObserva.Init(clsParser.Int32Parse(dgvClienteObserva.CurrentRow.Cells["posicao"].Value.ToString()), id, dtClienteObserva, true,id);

                frmClienteObserva.ShowDialog();
            }
        }

        private void dgvClienteobserva_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspClienteobserva_Alterar.PerformClick();
        }

        private void tspClienteobserva_Excluir_Click(object sender, EventArgs e)
        {
            if (dgvClienteObserva.CurrentRow != null)
            {
                if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                        dgvClienteObserva.Columns[2].HeaderText + " " +
                        dgvClienteObserva.CurrentRow.Cells[2].Value + "?",
                        Application.CompanyName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    dtClienteObserva.Select("posicao=" + dgvClienteObserva.CurrentRow.Cells["posicao"].Value.ToString())[0].Delete();
                }
            }
        }

        private void tsbClientecontato_incluir_Click(object sender, EventArgs e)
        {
            frmClienteContato frmClienteContato = new frmClienteContato();
            frmClienteContato.Init(0, id, dtClienteContato, tbxNumero.Text, tbxCognome.Text, tbxEmail.Text, tbxNome.Text);

            frmClienteContato.ShowDialog();
            VerificaContato();
        }

        private void tsbClientecontato_alterar_Click(object sender, EventArgs e)
        {
            if (dgvClientecontato.CurrentRow != null)
            {
                frmClienteContato frmClienteContato = new frmClienteContato();
                frmClienteContato.Init(clsParser.Int32Parse(dgvClientecontato.CurrentRow.Cells["posicao"].Value.ToString()), id, dtClienteContato, tbxNumero.Text, tbxCognome.Text, tbxEmail.Text, tbxCognome.Text);

                frmClienteContato.ShowDialog();
                VerificaContato();
            }
        }

        private void tsbClientecontato_excluir_Click(object sender, EventArgs e)
        {
            if (dgvClientecontato.CurrentRow != null)
            {
                if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " + dgvClientecontato.Columns[3].HeaderText + " " + dgvClientecontato.CurrentRow.Cells[3].Value, Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    dtClienteContato.Select("posicao=" + dgvClientecontato.CurrentRow.Cells["posicao"].Value.ToString())[0].Delete();
                    VerificaContato();
                }
            }
        }

        private void dgvClientecontato_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbClientecontato_alterar.PerformClick();
        }

        private void tspClienteEndereco_Incluir_Click(object sender, EventArgs e)
        {
            frmClienteEndereco frmClienteEndereco = new frmClienteEndereco();
            frmClienteEndereco.Init(0, id, dtClienteEndereco, 
                tbxCognome.Text,
                tbxNome.Text,
                tbxCgc.Text,
                tbxCep.Text,
                tbxEndereco.Text,
                tbxNumeroend.Text,
                cbxAndar.Text,
                cbxTipocomple.Text,
                tbxBairro.Text,
                tbxCidade.Text,
                cbxUf.Text,
                tbxIbge.Text,
                tbxEmail.Text,
                tbxDdd.Text,
                tbxTelefone.Text);

            frmClienteEndereco.ShowDialog();
        }

        private void tspClienteEndereco_Alterar_Click(object sender, EventArgs e)
        {
            if (dgvClienteEndereco.CurrentRow != null)
            {
                frmClienteEndereco frmClienteEndereco = new frmClienteEndereco();
                frmClienteEndereco.Init(clsParser.Int32Parse(dgvClienteEndereco.CurrentRow.Cells["posicao"].Value.ToString()), id, dtClienteEndereco,
                    tbxCognome.Text,
                    tbxNome.Text,
                    tbxCgc.Text,
                    tbxCep.Text,
                    tbxEndereco.Text,
                    tbxNumeroend.Text,
                    cbxAndar.Text,
                    cbxTipocomple.Text,
                    tbxBairro.Text,
                    tbxCidade.Text,
                    cbxUf.Text,
                    tbxIbge.Text,
                    tbxEmail.Text,
                    tbxDdd.Text,
                    tbxTelefone.Text);
                frmClienteEndereco.ShowDialog();
            }
        }

        private void dgvClienteEndereco_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspClienteEndereco_Alterar.PerformClick();
        }

        private void tspClienteEndereco_Excluir_Click(object sender, EventArgs e)
        {
            if (dgvClienteEndereco.CurrentRow != null)
            {
                if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                        dgvClienteEndereco.Columns[2].HeaderText + " " +
                        dgvClienteEndereco.CurrentRow.Cells[2].Value + "?",
                        Application.CompanyName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    dtClienteEndereco.Select("posicao=" + dgvClienteEndereco.CurrentRow.Cells["posicao"].Value.ToString())[0].Delete();
                }
            }
        }

        void bwrClienteEndereco_DoWork(object sender, DoWorkEventArgs e)
        {
            String query;
            SqlDataAdapter sda;

            query = "select * " +
                "from " +
                        "ClienteEndereco " +
                "where " +
                        "idcliente = @idcliente";

            dtClienteEndereco = new DataTable();
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = id;
            sda.Fill(dtClienteEndereco);
        }

        void bwrClienteEndereco_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataColumn dc = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtClienteEndereco.Columns.Add(dc);
            for (Int32 i = 1; i <= dtClienteEndereco.Rows.Count; i++)
            {
                dtClienteEndereco.Rows[i - 1]["posicao"] = i;
            }
            dtClienteEndereco.AcceptChanges();

            dgvClienteEndereco.DataSource = dtClienteEndereco;
            clsGridHelper.MontaGrid2(dgvClienteEndereco, dtClienteEnderecoColunas, true);
        }

        void bwrClienteEndereco_Run()
        {
            bwrClienteEndereco = new BackgroundWorker();
            bwrClienteEndereco.DoWork += new DoWorkEventHandler(bwrClienteEndereco_DoWork);
            bwrClienteEndereco.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrClienteEndereco_RunWorkerCompleted);
            bwrClienteEndereco.RunWorkerAsync();
        }

        private void tsbRequisitoIncluir_Click(object sender, EventArgs e)
        {
            //if (dtClienterequisito != null)
            //{
                //frmClienterequisito frmClienterequisito = new frmClienterequisito();
                //frmClienterequisito.Init(0, ref dtClienterequisito);

                //frmClienterequisito.ShowDialog();
            //}
        }


        private void tbxCidade_TextChanged(object sender, EventArgs e)
        {

        }
        String TipoRua(String _EndTipo)
        {
            if (_EndTipo != null)
            {
                switch (_EndTipo.PadRight(2).Substring(0, 2).ToUpper())
                {
                    case "RU":
                        _EndTipo = "R";
                        break;
                    case "AV":
                        _EndTipo = "AV";
                        break;
                    case "AL":
                        _EndTipo = "R";
                        break;
                    case "ES":
                        _EndTipo = "EST";
                        break;
                    case "PR":
                        _EndTipo = "PÇ";
                        break;
                    case "TR":
                        _EndTipo = "TV";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _EndTipo = "R";
            }

            return _EndTipo;
        }
        private void btnRefazerFichaFinanceira_Click(object sender, EventArgs e)
        {
            dtFichaFinanceira = clsFinanceiro.FichaFinanceira(0, id, clsClienteInfo.cognome, "VENC", clsParser.DateTimeParse(tbxDatade.Text), clsParser.DateTimeParse(tbxDataate.Text), clsParser.DecimalParse(tbxSaldoFichaFinanc.Text));
            clsFinanceiro.GridFichaFinanceiraMonta(dgvFichaFinanceira, dtFichaFinanceira, 0);
        }

        private void btnImprimirFicha_Click(object sender, EventArgs e)
        {
            cabecalho = "Ficha Financeira de Cliente";

            // Apagar arquivo rpt
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("delete rptfluxofinanceiro", scn);
            scn.Open();
            sdr = scd.ExecuteReader();
            scn.Close();

            clsRptFluxoFinanceiroBLL = new clsRptFluxoFinanceiroBLL();
            // Preparar a Tabela ja Filtrando os dados solicitados
            foreach (DataRow row in dtFichaFinanceira.Rows)
            {
                clsRptFluxoFinanceiroInfo = new clsRptFluxoFinanceiroInfo();
               
                clsRptFluxoFinanceiroInfo.FILIAL = clsParser.Int32Parse(row["FILIAL"].ToString());
                clsRptFluxoFinanceiroInfo.NOTAFISCAL = row["DUPLICATA"].ToString();
                clsRptFluxoFinanceiroInfo.POSICAO = clsParser.Int32Parse(row["POSICAO"].ToString());
                clsRptFluxoFinanceiroInfo.POSICAOFIM = clsParser.Int32Parse(row["POSICAOFIM"].ToString());
                clsRptFluxoFinanceiroInfo.EMISSAO = clsParser.DateTimeParse(row["EMISSAO"].ToString());
                clsRptFluxoFinanceiroInfo.TABELA = row["TABELA"].ToString();
                clsRptFluxoFinanceiroInfo.CLIENTE = row["CLIENTE"].ToString();
                clsRptFluxoFinanceiroInfo.VENCIMENTO = clsParser.DateTimeParse(row["VENCIMENTO"].ToString());
                clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(row["CREDITO"].ToString());
                clsRptFluxoFinanceiroInfo.DEBITO = clsParser.DecimalParse(row["DEBITO"].ToString());
                clsRptFluxoFinanceiroInfo.SALDO = clsParser.DecimalParse(row["SALDO"].ToString());
                clsRptFluxoFinanceiroInfo.OBSERVA = row["OBSERVA"].ToString();
                clsRptFluxoFinanceiroInfo.ID = clsRptFluxoFinanceiroBLL.Incluir(clsRptFluxoFinanceiroInfo, clsInfo.conexaosqldados);
            }

            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = cabecalho;
            field.Name = "CABECALHO";
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Empresa
            valor = new ParameterDiscreteValue();
            field = new ParameterField();
            valor.Value = clsInfo.zempresacliente_cognome;
            field.Name = "EMPRESA";
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Imprimir 
            frmCrystalReport.Init(clsInfo.caminhorelatorios, "Ficha_Financeira.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

        }

        private void btnRefazerFichaComercial_Click(object sender, EventArgs e)
        {
            dtFichaComercial = clsFinanceiro.FichaComercial(0, id, clsClienteInfo.cognome, clsParser.DateTimeParse(tbxDatadeC.Text), clsParser.DateTimeParse(tbxDataateC.Text));
            clsFinanceiro.GridFichaComercialMonta(dgvFichaComercial, dtFichaComercial, 0);

        }

        private void cbxTipocomple_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxAndar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gbxQualidade_Enter(object sender, EventArgs e)
        {

        }

        private void dgvClientecontato_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbClientecontato_alterar.PerformClick();
        }

        private void dgvClienteEndereco_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbxCidade_Click(object sender, EventArgs e)
        {
            //TrataCampos((Control)sender);
        }

        private void cbxCidade_KeyUp(object sender, KeyEventArgs e)
        {
            //TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseClick(object sender, MouseEventArgs e)
        {
            //TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseUp(object sender, MouseEventArgs e)
        {
            //TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseDown(object sender, MouseEventArgs e)
        {
            //TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseMove(object sender, MouseEventArgs e)
        {
            //TrataCampos((Control)sender);
        }

        private void tclDadosGerais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tclDadosGerais.SelectedIndex == 3)
            {
                // carregar o fluxo financeiro deste cliente
                if (dgvFichaFinanceira.Rows.Count == 0)
                {
                    tbxDatade.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
                    tbxDataate.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
                    dtFichaFinanceira = clsFinanceiro.FichaFinanceira(0, id, clsClienteInfo.cognome, "VENC", clsParser.DateTimeParse("01/01/1900"), clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()), clsParser.DecimalParse(tbxSaldoFichaFinanc.Text));
                    clsFinanceiro.GridFichaFinanceiraMonta(dgvFichaFinanceira, dtFichaFinanceira, 0);
                }
            }
            if (tclDadosGerais.SelectedIndex == 4)
            {
                if (dgvFichaComercial.Rows.Count == 0)
                {
                    // carregar o fluxo comercial do cliente
                    tbxDatadeC.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
                    tbxDataateC.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
                    dtFichaComercial = clsFinanceiro.FichaComercial(0, id, clsClienteInfo.cognome, clsParser.DateTimeParse("01/01/1900"), clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()));
                    clsFinanceiro.GridFichaComercialMonta(dgvFichaComercial, dtFichaComercial, 0);
                }
            }
        }
        //private void CapturarCidadeUF()
        //{
        //    if (cbxCidade.Text.Length > 1)
        //    {
        //        cbxCidade.Text = cbxCidade.Text.ToUpper().Trim();
        //        Int32 x = cbxCidade.Text.IndexOf('-');
        //        Int32 y = cbxCidade.Text.Length;
        //        String Cidade = cbxCidade.Text.Substring(0, x).Trim();
        //        String Estado = cbxCidade.Text.Substring(x + 1, (y - (x + 1))).Trim();
        //        idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + Estado + "' ", "0"));
        //        idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + Cidade + "' and idestado=" + idestado + " ", "0"));
        //        cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString(), "SP"));
        //        // codigo do ibge
        //        tbxIbge.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ibge from estados where id=" + idestado + " ", "??");
        //        tbxIbge.Text += Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from cidades where id=" + idcidade + " and idestado=" + idestado + " ", "00000");
        //    }
        //}

        private void tbxCgc_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void tbxSuframa_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxCnae_codigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirFichaComercial_Click(object sender, EventArgs e)
        {
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();

            ParameterFields parameters = new ParameterFields();
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();

            field.Name = "EMPRESA";
            valor.Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from empresa where id=" + clsInfo.zempresaid + " ");
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cabeçalho
            field = new ParameterField();
            field.Name = "CABECALHO";
            valor = new ParameterDiscreteValue();
            cabecalho = "Ficha Comercial ";
            valor.Value = cabecalho;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Parametros do Relatorio
            // Cliente de
            field = new ParameterField();
            field.Name = "ClienteDe";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxCognome.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Da Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoDe";
            valor = new ParameterDiscreteValue();
            if (tbxDatadeC.Text.Trim() == "")
            {
                tbxDatadeC.Text = "01/01/1900";
            }
            valor.Value = tbxDatadeC.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Ate Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoAte";
            valor = new ParameterDiscreteValue();
            if (tbxDataateC.Text.Trim() == "")
            {
                tbxDataateC.Text = "01/01/2100";
            }
            valor.Value = tbxDataateC.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            //// Vendedor de
            //field = new ParameterField();
            //field.Name = "VendedorDe";
            //valor = new ParameterDiscreteValue();
            //valor.Value = tbxItemVendedorDe.Text;
            //field.CurrentValues.Add(valor);
            //parameters.Add(field);
            //// Vendedor ate
            //field = new ParameterField();
            //field.Name = "VendedorAte";
            //valor = new ParameterDiscreteValue();
            //valor.Value = tbxItemVendedorAte.Text;
            //field.CurrentValues.Add(valor);
            //parameters.Add(field);
            frmCrystalReport.Init(clsInfo.caminhorelatorios, "PedidoVendas_Comercial.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

        }

        private void btnRefazerFichaComercialCompra_Click(object sender, EventArgs e)
        {
            dtFichaComercialCompras= clsFinanceiro.FichaComercialCompras(0, id, clsClienteInfo.cognome, clsParser.DateTimeParse(tbxDatadeC.Text), clsParser.DateTimeParse(tbxDataateC.Text));
            clsFinanceiro.GridFichaComercialComprasMonta(dgvFichaComercialCompras, dtFichaComercialCompras, 0);
        }
    }
}
