using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmEmpresa : Form
    {
        Int32 id;
        Int32 empresasgere_id;
        DataGridViewRowCollection rows;
        Int32 idcidade;
        Int32 idestado;
        Int32 idramo;

        clsEmpresaBLL clsEmpresaBLL;
        clsEmpresaInfo clsEmpresaInfo;
        clsEmpresaInfo clsEmpresaInfoOld;

        clsEmpresaGereBLL clsEmpresaGereBLL;
        clsEmpresaGereInfo clsEmpresaGereInfo;
        clsEmpresaGereInfo clsEmpresaGereInfoOld;

        Int32 empresasgere_idcodcofins;
        Int32 empresasgere_idcodcsll;
        Int32 empresasgere_idcodinss;
        Int32 empresasgere_idcodirrf;
        Int32 empresasgere_idcodpis;
        Int32 empresasgere_idcodpiscofins;
        Int32 empresasgere_idcodiss;
        Int32 empresasgere_idregimeapuracao;
        Int32 empresasgere_idregimetributario;
        Int32 empresasgere_idforirrf;
        Int32 empresasgere_idpagirrf;
        Int32 empresasgere_idforinss;
        Int32 empresasgere_idpaginss;
        Int32 empresasgere_idforpis;
        Int32 empresasgere_idpagpis;
        Int32 empresasgere_idforcofins;
        Int32 empresasgere_idpagcofins;
        Int32 empresasgere_idforcsll;
        Int32 empresasgere_idpagcsll;
        Int32 empresasgere_idforpiscofins;
        Int32 empresasgere_idpagpiscofins;
        Int32 empresasgere_idforiss;
        Int32 empresasgere_idpagiss;


        public frmEmpresa()
        {
            InitializeComponent();
        }
        public void Init(Int32 _id, DataGridViewRowCollection _rows)
        {
            id = _id;
            rows = _rows;

            clsEmpresaBLL = new clsEmpresaBLL();
            clsEmpresaGereBLL = new clsEmpresaGereBLL();
            clsEmpresaGereInfo = new clsEmpresaGereInfo();




            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from ramo order by codigo", tbxRamo);

            //String query = "select cidades.nome + '   -   ' + ESTADOS.ESTADO from cidades " +
            //   " left join estados on estados.id = CIDADES.IDESTADO ";
            //clsVisual.FillComboBox(cbxCidade, query, clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxUf, "select estado from estados order by estado", clsInfo.conexaosqldados);
        }
        public void Init(Int32 _id)
        {
            id = _id;
            rows = null;

            clsEmpresaBLL = new clsEmpresaBLL();
            clsEmpresaGereBLL = new clsEmpresaGereBLL();
            clsEmpresaGereInfo = new clsEmpresaGereInfo();


            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from ramo order by codigo", tbxRamo);

            //String query = "select cidades.nome + '   -   ' + ESTADOS.ESTADO from cidades " +
            //   " left join estados on estados.id = CIDADES.IDESTADO ";
            //clsVisual.FillComboBox(cbxCidade, query, clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxUf, "select estado from estados order by estado", clsInfo.conexaosqldados);

//            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", " (SERIE IS NOT NULL) AND (LEN(COGNOME) > 0)", tbxDocfiscal_codigo);
//            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", " (SERIE IS NOT NULL) AND (LEN(COGNOME) > 0)", tbxDocfiscal_codigoContg);

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from regimeapuracao order by codigo", tbxRegimeApuracao_Nome);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas order by codigo", tbxCodCofins);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas order by codigo", tbxCodCsll);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas order by codigo", tbxCodInss);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas order by codigo", tbxCodIrrf);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas order by codigo", tbxCodIss);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas order by codigo", tbxCodPis);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas order by codigo", tbxCodPisCofins);

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='F' and ativo='S' order by cognome", tbxForCofins);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='F' and ativo='S' order by cognome", tbxForCsll);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='F' and ativo='S' order by cognome", tbxForInss);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='F' and ativo='S' order by cognome", tbxForIrrf);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='F' and ativo='S' order by cognome", tbxForIss);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='F' and ativo='S' order by cognome", tbxForPis);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo='F' and ativo='S' order by cognome", tbxForPiscofins);

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxPagCofins);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxPagCsll);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxPagInss);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxPagIrrf);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxPagIss);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxPagPis);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxPagPiscofins);

            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBcoCtaCheDev);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBcoCtaCheDevEmi);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBcoCtaChePreEmi);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBcoCtaChePreRec);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from docfiscal order by codigo", tbxGeradordocumento_docfiscal_codigo);

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '-' + nome from regimetributario order by codigo", tbxRegimeTributario);

        }

        private void frmEmpresa_Load(object sender, EventArgs e)
        {
            EmpresasRegistro(id);
        }
        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    EmpresasSalvar();
                    //Atualiza o registro do Windows para o funcionamento da NFE
                    //Lê o registro 
                    //clsNFeRegistryKey NFeRegistryKey = new clsNFeRegistryKey();
                    //MessageBox.Show(NFeRegistryKey.RegistryKey(id), Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCognome.Select();
                tbxCognome.SelectAll();
            }

        }
        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close(); ;
        }
        private void EmpresasRegistro(Int32 id)
        {
            clsEmpresaInfoOld = new clsEmpresaInfo();
            clsEmpresaGereInfoOld = new clsEmpresaGereInfo();
            //clsEmpresafolhaInfoOld = new clsEmpresafolhaInfo();

            clsEmpresaInfo = new clsEmpresaInfo();
            clsEmpresaGereInfo = new clsEmpresaGereInfo();
            if (id > 0)
            {
                clsEmpresaInfo = clsEmpresaBLL.Carregar(id, clsInfo.conexaosqldados);

                clsEmpresaGereInfo = clsEmpresaGereBLL.Carregar(clsParser.Int32Parse(
                    Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id FROM EMPRESAgere where empresa = " + id, "0"))
                   , clsInfo.conexaosqldados);


                //clsEmpresafolhaInfo = clsEmpresafolhaBLL.CarregarEmpresa(clsParser.Int32Parse(
                //    Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id FROM EMPRESAfolha where empresa = " + id, "0")), clsInfo.conexaosqldados);

            }
            else
            {
                //Empresa
                //                clsEmpresaInfo.idgeradordocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from docfiscal order by codigo desc", "0"));
                clsEmpresaInfo.idramo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ramo where codigo='NC'", "0"));
                clsEmpresaInfo.pessoa = "J";
                //Gere
                clsEmpresaGereInfo.aceitanrozero = "N";
                clsEmpresaGereInfo.aceitaqtosdias = 0;
                clsEmpresaGereInfo.aceitavalorzero = "N";
                clsEmpresaGereInfo.cta991 = "S";
                clsEmpresaGereInfo.cta995 = "S";



                //clsEmpresaGereInfo.
                //Folha
            }
            EmpresaCampos(clsEmpresaInfo);
            EmpresaInfo(clsEmpresaInfoOld);

            EmpresaGereCampos(clsEmpresaGereInfo);
            EmpresaGereInfo(clsEmpresaGereInfoOld);

            //EmpresasFolhaCampos(clsEmpresafolhaInfo);
            //EmpresasFolhaInfo(clsEmpresafolhaInfoOld);

            gbxGeradordocumento.Visible = (clsInfo.zusuario.ToUpper() == "SUPERVISOR");
            btnConfiguracao.Visible = (clsInfo.zusuario.ToUpper() == "SUPERVISOR");

            //WebServicesFill();

            tbxCognome.Select();
            tbxCognome.SelectAll();
        }

        private void EmpresaCampos(clsEmpresaInfo info)
        {

            id = info.id;
            //            idgeradordocumento = info.idgeradordocumento;
            idramo = info.idramo;
            idcidade = info.idcidade;
            idestado = info.idestado;


            //            tbxGeradordocumento_docfiscal_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from docfiscal where id=" + idgeradordocumento);

            tbxCodigo.Text = info.codigo.ToString();
            tbxCognome.Text = info.cognome;
            tbxNome.Text = info.nome;

            tbxCep.Text = info.cep;
            tbxEndereco.Text = info.endereco;

            cbxTiponumero.SelectedIndex = cbxTiponumero.FindString(info.tiponumero);
            if (cbxTiponumero.SelectedIndex == -1)
            {
                cbxTiponumero.SelectedIndex = 0;
            }

            tbxNumeroEnd.Text = info.numeroend;
            tbxAndar.Text = info.andar;
            tbxTipoComple.Text = info.tipocomple;
            tbxComple.Text = info.comple;
            tbxBairro.Text = info.bairro;
            //String cidade = "";
            //cidade = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from cidades where id=" + info.idcidade, "");
            //cbxCidade.SelectedIndex = cbxCidade.FindString(cidade);
            //if (cbxCidade.SelectedIndex == -1)
            //{
            //    cbxCidade.SelectedIndex = 0;
            //}

            cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString()));
            if (cbxUf.SelectedIndex == -1)
            {
                cbxUf.SelectedIndex = 0;
            }

            //cbxRegiao.SelectedIndex = cbxRegiao.FindString(info.regiao);
            //if (cbxRegiao.SelectedIndex == -1)
            //{
            //    cbxRegiao.SelectedIndex = 0;
            //}

            tbxCadastrado.Text = info.cadastrado;
            tbxIe.Text = info.ie;
            tbxCgc.Text = info.cgc;

            if (clsVisual.ValidaCnpj(tbxCgc.Text) == true)
            {
                tbxCgc.Text = clsVisual.CamposVisual("CGC", tbxCgc.Text);
            }
            else
            {
                if (tbxCognome.Text.Length != 0)
                {
                    MessageBox.Show("CNPJ Inválido.");
                }
            }

            tbxImunicipal.Text = info.imunicipal;
            tbxIbge.Text = info.ibge;
            //tbxRamo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "RAMO", "CODIGO", "ID", idramo) + "=" + Procedure.PesquisaValor(clsInfo.conexaosqldados, "RAMO", "NOME", "ID", idramo);
            tbxRamo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from ramo where id = " + idramo + " ", "");
            tbxDDD.Text = info.ddd;
            tbxTelefone.Text = info.telefone;
            tbxContato.Text = info.contato;

            pbxLogotipo.Image = info.logotipo;


            tbxCognome.Select();
        }

        private void EmpresaInfo(clsEmpresaInfo info)
        {
            //info.PastaUniNFe = tbxCaminhoUniNFe.Text;
            //info.ConfigZeus = tbxConfigZeus.Text;
            //if (idgeradordocumento == 0)
            //{
            //    idgeradordocumento = clsInfo.zdocumento;
            //}

            info.id = id;
            //info.idgeradordocumento = idgeradordocumento;
            info.idramo = idramo;
            info.idcidade = idcidade;
            info.idestado = idestado;
            info.codigo = clsParser.Int32Parse(tbxCodigo.Text);
            info.cognome = tbxCognome.Text;
            info.nome = tbxNome.Text;

            info.pessoa = "J";
            info.cep = tbxCep.Text;
            //info.endtipo = tbxEndtipo.Text;
            info.endereco = tbxEndereco.Text;
            info.tiponumero = cbxTiponumero.Text;
            info.numeroend = tbxNumeroEnd.Text;
            info.andar = tbxAndar.Text;
            info.tipocomple = tbxTipoComple.Text;
            info.comple = tbxComple.Text;
            info.bairro = tbxBairro.Text;
            //info.regiao = cbxRegiao.Text;
            info.cadastrado = tbxCadastrado.Text;
            info.cgc = tbxCgc.Text.ToString().Replace(".", "").Replace("/", "").Replace("-", "");
            info.ie = tbxIe.Text.ToString().Replace(".", "").Replace("/", "").Replace("-", "");
            //info.cnae = tbxCNAE.Text;
            //info.iest = tbxIEST.Text.ToString().Replace(".", "").Replace("/", "").Replace("-", "");
            info.imunicipal = tbxImunicipal.Text;
            //info.email = tbxEmail.Text;
            //info.homepage = tbxHomePage.Text;
            //info.smtp = tbxSmtp.Text;
            //info.smtpporta = tbxSmtpPorta.Text;
            //if (checkSSL.Checked)
            //{
            //    info.usarssl = "S";
            //}
            //else
            //{
            //    info.usarssl = "N";
            //}
            //info.observa = tbxObservacao.Text;
            info.ibge = tbxIbge.Text;
            info.ddd = tbxDDD.Text;
            info.telefone = tbxTelefone.Text;
            info.contato = tbxContato.Text;
            //info.nfe_certnome = tbxNfe_certnome.Text;
            //info.nfe_senha = tbxNfe_senha.Text;
            //info.nfe_usuario = tbxNfe_usuario.Text;
            //info.nfe_vigencia = clsParser.DateTimeParse(tbxNfe_vigencia.Text);
            //info.nfe_xsdpasta = tbxNfe_xsdpasta.Text;
            //info.nfe_tpamb = cbxNfe_tpamb.Text.Substring(0, 5);
            //info.NFe_iddocfiscalserie = iddocumento;
            //info.NFe_iddocfiscalseriecontg = iddocumentocontg;
            info.logotipo = pbxLogotipo.Image;
            //info.proxysite = tbxProxySite.Text;
            //info.proxyporta = tbxProxyPorta.Text;
            //info.proxylogin = tbxProxyLogin.Text;
            //info.proxysenha = tbxProxySenha.Text;
            //info.chavebuscacep = tbxChavebuscacep.Text;
            //info.seriecertificadodigital = tbxSerieCertificadoDigital.Text;
            //info.tipodante = cbxTipoDanfe.Text.Substring(0, 1);
            //info.totalizarcfop = cbxTotalizarCFOP.Text.Substring(0, 1);
            //info.unidadefederal = cbxUnidadeFederal.Text;
            //info.dataeventocartacorrecao = cbxDataEventoCartadeCorrecao.Text;

            //if (rbnProxyNfeSim.Checked == true)
            //{
            //    info.nfeproxy = "S";
            //}
            //else
            //{
            //    info.nfeproxy = "N";
            //}

            //info.mascara_valor_unitario_danfe = tbxMascaraValorUnitarioDanfe.Text;
            //info.mascara_qtde_item_danfe = tbxMascaraQtdeItemDanfe.Text;

            //info.PastaUniNFe = tbxCaminhoUniNFe.Text;
            //info.ConfigZeus = tbxConfigZeus.Text;

        }
        private void EmpresaGereCampos(clsEmpresaGereInfo info)
        {
            empresasgere_id = info.id;
            //empresasgere_empresa = id;
            empresasgere_idcodcofins = info.idcodcofins;
            empresasgere_idcodcsll = info.idcodcsll;
            empresasgere_idcodinss = info.idcodinss;
            empresasgere_idcodirrf = info.idcodirrf;
            empresasgere_idcodpis = info.idcodpis;
            empresasgere_idcodpiscofins = info.idcodpiscofins;
            empresasgere_idcodiss = info.idcodiss;
            empresasgere_idregimeapuracao = info.idregimeapuracao;
            empresasgere_idregimetributario = info.idregimetributario;
            empresasgere_idforcofins = info.idforcofins;
            empresasgere_idforcsll = info.idforcsll;
            empresasgere_idforinss = info.idforinss;
            empresasgere_idforirrf = info.idforirrf;
            empresasgere_idforiss = info.idforiss;
            empresasgere_idforpis = info.idforpis;
            empresasgere_idforpiscofins = info.idforpiscofins;
            empresasgere_idpagcofins = info.idpagcofins;
            empresasgere_idpagcsll = info.idpagcsll;
            empresasgere_idpaginss = info.idpaginss;
            empresasgere_idpagirrf = info.idpagirrf;
            empresasgere_idpagiss = info.idpagiss;
            empresasgere_idpagpis = info.idpagpis;
            empresasgere_idpagpiscofins = info.idpagpiscofins;

            //// Guia Contabilidade
            //rbnempresasgere_CtabilContaVinculadaS.Checked = (info.ctabilcontavinculada == "S");
            //rbnempresasgere_CtabilContaVinculadaN.Checked = (info.ctabilcontavinculada == "N");
            //tbxRegimeApuracao_Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from regimeapuracao where id=" + empresasgere_idregimeapuracao, "0");
            //tbxRegimeTributario.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '-' + nome from regimetributario where id =" + empresasgere_idregimetributario, "0");
            //// Guia Fiscal
            tbxFiscalIrrf.Text = info.fiscalirrf.ToString();
            tbxDarfIrrf.Text = info.darfirrf;
            tbxFiscalInss.Text = info.fiscalinss.ToString();
            tbxDarfInss.Text = info.darfinss;
            tbxFiscalPis.Text = info.fiscalpis.ToString();
            tbxDarfPis.Text = info.darfpis;
            tbxFiscalCofins.Text = info.fiscalcofins.ToString();
            tbxDarfCofins.Text = info.darfcofins;
            tbxFiscalCsll.Text = info.fiscalcsll.ToString();
            tbxDarfCsll.Text = info.darfcsll;
            tbxFiscalPisCofins.Text = info.fiscalpiscofins.ToString();
            tbxDarfPisCofins.Text = info.darfpiscofins;
            tbxIcmsACreditar.Text = info.icmsacreditar.ToString("N2");
            tbxLimitePisCofins.Text = info.limitepiscofins.ToString();
            tbxLeiPisPasep.Text = info.leipispasep.ToString();
            tbxLeiCofins.Text = info.leicofins.ToString();
            tbxLeiPisPasepRetido.Text = info.leipispasepretido.ToString();
            tbxLeiCofinsRetido.Text = info.leicofinsretido.ToString();

            tbxFiscalIss.Text = info.fiscaliss.ToString();
            tbxDarfIss.Text = info.darfiss;
            tbxCodIrrf.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas where id=" + empresasgere_idcodirrf, "");
            tbxCodInss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas where id=" + empresasgere_idcodinss, "");
            tbxCodPis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas where id=" + empresasgere_idcodpis, "");
            tbxCodCofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas where id=" + empresasgere_idcodcofins, "");
            tbxCodCsll.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas where id=" + empresasgere_idcodcsll, "");
            tbxCodPisCofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas where id=" + empresasgere_idcodpiscofins, "");
            tbxCodIss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from pecas where id=" + empresasgere_idcodiss, "");
            //tbxForCofins.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", empresasgere_idforcofins);
            tbxForCofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id=" + empresasgere_idforcofins, "");
            //tbxForCsll.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", empresasgere_idforcsll);
            tbxForCsll.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id=" + empresasgere_idforcsll, "");
            //tbxForInss.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", empresasgere_idforinss);
            tbxForInss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id=" + empresasgere_idforinss, "");
            //tbxForIrrf.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", empresasgere_idforirrf);
            tbxForIrrf.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id=" + empresasgere_idforirrf, "");
            //tbxForIss.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", empresasgere_idforiss);
            tbxForIss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id=" + empresasgere_idforiss, "");
            //tbxForPis.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", empresasgere_idforpis);
            tbxForPis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id=" + empresasgere_idforpis, "");
            //tbxForPiscofins.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", empresasgere_idforpiscofins);
            tbxForPiscofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id=" + empresasgere_idforpiscofins, "");
            tbxPagCofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where id=" + empresasgere_idpagcofins, "");
            tbxPagCsll.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where id=" + empresasgere_idpagcsll, "");
            tbxPagInss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where id=" + empresasgere_idpaginss, "");
            tbxPagIrrf.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where id=" + empresasgere_idpagirrf, "");
            tbxPagIss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where id=" + empresasgere_idpagiss, "");
            tbxPagPis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where id=" + empresasgere_idpagpis, "");
            tbxPagPiscofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where id=" + empresasgere_idpagpiscofins, "");

            // Guia Gerencial
            cbxGerAutorizaSolicitacao.SelectedIndex = cbxGerAutorizaSolicitacao.FindString(info.gerautorizasolicitacao);
            if (cbxGerAutorizaSolicitacao.SelectedIndex == -1) cbxGerAutorizaSolicitacao.SelectedIndex = 1;

            cbxGerAutorizaCompra.SelectedIndex = cbxGerAutorizaCompra.FindString(info.gerautorizacompra);
            if (cbxGerAutorizaCompra.SelectedIndex == -1) cbxGerAutorizaCompra.SelectedIndex = 1;

            cbxGerCotacaoPreco.SelectedIndex = cbxGerCotacaoPreco.FindString(info.gercotacaopreco);
            if (cbxGerCotacaoPreco.SelectedIndex == -1) cbxGerCotacaoPreco.SelectedIndex = 1;

            cbxGerDivergenciaCompra.SelectedIndex = cbxGerDivergenciaCompra.FindString(info.gerdivergenciacompra);
            if (cbxGerDivergenciaCompra.SelectedIndex == -1) cbxGerDivergenciaCompra.SelectedIndex = 1;

            cbxGerAutorizaPedido.SelectedIndex = cbxGerAutorizaPedido.FindString(info.gerautorizapedido);
            if (cbxGerAutorizaPedido.SelectedIndex == -1) cbxGerAutorizaPedido.SelectedIndex = 1;

            cbxGerAutorizaFaturar.SelectedIndex = cbxGerAutorizaFaturar.FindString(info.gerautorizafaturar);
            if (cbxGerAutorizaFaturar.SelectedIndex == -1) cbxGerAutorizaFaturar.SelectedIndex = 1;

            tbxAutorizaComprador.Text = info.autorizacomprador.ToString();
            tbxAutorizaGerente.Text = info.autorizagerente.ToString();
            tbxAutorizaNormal.Text = info.autorizanormal.ToString();

            lbxTipoAutorizacaoSolicitacao.SelectedIndex = info.metodoautsolic;
            if (lbxTipoAutorizacaoSolicitacao.SelectedIndex == -1) lbxTipoAutorizacaoSolicitacao.SelectedIndex = 1;

            lbxTipoChecaSolicitacao.SelectedIndex = info.checarsolic;
            if (lbxTipoChecaSolicitacao.SelectedIndex == -1) lbxTipoChecaSolicitacao.SelectedIndex = 1;

            // Guia Compras
            tbxQtdeMais.Text = info.qtdemais.ToString();
            tbxQtdeMenos.Text = info.qtdemenos.ToString();
            tbxQtdeMaisUn.Text = info.qtdemaisun.ToString();
            tbxQtdeMenosUn.Text = info.qtdemenosun.ToString();
            tbxValorMais.Text = info.valormais.ToString();
            tbxValorMenos.Text = info.valormenos.ToString();
            tbxTotalMais.Text = info.totalmais.ToString();
            tbxTotalMenos.Text = info.totalmenos.ToString();
            tbxVenceMais.Text = info.vencemais.ToString();
            tbxVenceMenos.Text = info.vencemenos.ToString();
            tbxQtdePedCKg.Text = info.qtdepedckg.ToString();
            tbxQtdePedCun.Text = info.qtdepedcun.ToString();

            // Guia Financeiro
            cbxCta991.SelectedIndex = cbxCta991.FindString(info.cta991);
            if (cbxCta991.SelectedIndex == -1) cbxCta991.SelectedIndex = 1;

            cbxCta995.SelectedIndex = cbxCta995.FindString(info.cta995);
            if (cbxCta995.SelectedIndex == -1) cbxCta995.SelectedIndex = 1;

            tbxJurosMes.Text = info.jurosmes.ToString("N2");
            tbxProtestar.Text = info.protestar.ToString();
            tbxMulta.Text = info.multa.ToString("N2");

            tbxSequencia.Text = info.sequencia.ToString();
            tbxRegistro.Text = info.registro.ToString();
            tbxBoletolinha01.Text = info.boletolinha01;
            tbxBoletolinha02.Text = info.boletolinha02;
            tbxBoletolinha03.Text = info.boletolinha03;
            tbxBoletolinha04.Text = info.boletolinha04;
            tbxBoletolinha05.Text = info.boletolinha05;

            cbxAceitaNroZero.SelectedIndex = cbxAceitaNroZero.FindString(info.aceitanrozero);
            if (cbxAceitaNroZero.SelectedIndex == -1) cbxAceitaNroZero.SelectedIndex = 1;

            cbxAceitaValorZero.SelectedIndex = cbxAceitaValorZero.FindString(info.aceitavalorzero);
            if (cbxAceitaValorZero.SelectedIndex == -1) cbxAceitaValorZero.SelectedIndex = 1;

            tbxAceitaQtosDias.Text = info.aceitaqtosdias.ToString();

            //cbxDespesaConduta.SelectedIndex = cbxDespesaConduta.FindString(info.despesaconduta);
            //if (cbxDespesaConduta.SelectedIndex == -1) cbxDespesaConduta.SelectedIndex = 1;

            //tbxDespesaProcedure.Text = info.despesaprocedure;
            rbnGrupoDespesa0.Checked = (info.grupodespesa == 0);
            rbnGrupoDespesa1.Checked = (info.grupodespesa == 1);
            rbnGrupoDespesa2.Checked = (info.grupodespesa == 2);
            rbnGrupoDespesa3.Checked = (info.grupodespesa == 3);
            //rbnCentroCusto0.Checked = (info.centrocusto == 0);
            //rbnCentroCusto1.Checked = (info.centrocusto == 1);
            //rbnCentroCusto2.Checked = (info.centrocusto == 2);
            //rbnSetorCentroCusto0.Checked = (info.setorcentrocusto == 0);
            //rbnSetorCentroCusto1.Checked = (info.setorcentrocusto == 1);
            //rbnSetorCentroCusto2.Checked = (info.setorcentrocusto == 2);
            //rbnSetorCentroCusto3.Checked = (info.setorcentrocusto == 3);
            //rbnSetorCentroCusto4.Checked = (info.setorcentrocusto == 4);

            if (info.baixarcomhistorico == "CX")
            {
                rbnBaixarcomhistoricoCX.Checked = true;
            }
            else
            {
                rbnBaixarcomhistoricoNF.Checked = true;
            }
            if (info.baixatemnotafiscal == "S")
            {
                ckxBaixacomNotaFiscal.Checked = true;
                ckxBaixacomNotaFiscal.Text = "Sim pode Baixar sem Nota Fiscal";
            }
            else
            {
                ckxBaixacomNotaFiscal.Checked = false;
                ckxBaixacomNotaFiscal.Text = "Não pode Baixar sem Nota Fiscal";
            }
            // Guia Faturamento
            //cbxSomaPesoNFV.SelectedIndex = cbxSomaPesoNFV.FindString(info.somapesonfv);
            //if (cbxSomaPesoNFV.SelectedIndex == -1) cbxSomaPesoNFV.SelectedIndex = 1;

            //cbxListaPreFat.SelectedIndex = cbxListaPreFat.FindString(info.listaprefat);
            //if (cbxListaPreFat.SelectedIndex == -1) cbxListaPreFat.SelectedIndex = 1;

            //cbxPedidoFrete.SelectedIndex = cbxPedidoFrete.FindString(info.pedidofrete);
            //if (cbxPedidoFrete.SelectedIndex == -1) cbxPedidoFrete.SelectedIndex = 1;

            //cbxComissaoVaria.SelectedIndex = cbxComissaoVaria.FindString(info.comissaovaria);
            //if (cbxComissaoVaria.SelectedIndex == -1) cbxComissaoVaria.SelectedIndex = 1;

            // Guia Custos
            //            tbxBaseMP.Text = info.basemp.ToString();
            tbxAdm.Text = info.adm.ToString();
            tbxFinanceira.Text = info.financeira.ToString();
            tbxComissao.Text = info.comissao.ToString();
            tbxRejeicao.Text = info.rejeicao.ToString();
            tbxPis.Text = info.pis.ToString();
            tbxCofins.Text = info.cofins.ToString();
            tbxContSocial.Text = info.contsocial.ToString();
            tbxIrpj.Text = info.irpj.ToString();
            tbxIcm.Text = info.icm.ToString();
            tbxIpi.Text = info.ipi.ToString();
            tbxLucro.Text = info.lucro.ToString();
            tbxBola.Text = info.bola.ToString();
            tbxMarkup.Text = info.markup.ToString();
            rbnCustoPorDentroA.Checked = (info.custopordentro == "A");
            rbnCustoPorDentroB.Checked = (info.custopordentro == "B");
            rbnCustoPorDentroC.Checked = (info.custopordentro == "C");
            rbnCustoPorDentroA.Checked = (info.custopordentro == null);

            // Guia Aplibank
            //cbxBcoPlanoContas.SelectedIndex = cbxBcoPlanoContas.FindString(info.bcoplanocontas);
            //if (cbxBcoPlanoContas.SelectedIndex == -1) cbxBcoPlanoContas.SelectedIndex = 1;

            tbxBcoArquivoMortoDias.Text = info.bcoarquivomortodias.ToString();

            if (info.bcoarquivodata == null) tbxBcoArquivoData.Text = DateTime.Now.ToString();
            else tbxBcoArquivoData.Text = info.bcoarquivodata.ToString("dd/MM/yyyy");

            cbxBcoChequeUnico.SelectedIndex = cbxBcoChequeUnico.FindString(info.bcochequeunico);
            if (cbxBcoChequeUnico.SelectedIndex == -1) cbxBcoChequeUnico.SelectedIndex = 1;

            cbxTransfBcoHist.SelectedIndex = cbxTransfBcoHist.FindString(info.transfbcohist);
            if (cbxTransfBcoHist.SelectedIndex == -1) cbxTransfBcoHist.SelectedIndex = 1;
            cbxTransfBcoCusto.SelectedIndex = cbxTransfBcoCusto.FindString(info.transfbcocusto);
            if (cbxTransfBcoCusto.SelectedIndex == -1) cbxTransfBcoCusto.SelectedIndex = 1;
            // Guia Diversos
            //cbxTemProspeccao.SelectedIndex = cbxTemProspeccao.FindString(info.temprospeccao);
            //if (cbxTemProspeccao.SelectedIndex == -1) cbxTemProspeccao.SelectedIndex = 1;
            //tbxPeddescgd.Text = info.peddescgd.ToString("N2");
            //tbxPeddescpzo.Text = info.peddescpzo.ToString("N2");
            //tbxPeddescvis.Text = info.peddescvis.ToString("N2");
            tbxRepresentanteLegal.Text = info.representantelegal;
            tbxCPFRepresentante.Text = info.cpfrepresentante;
            tbxCargoRepsesentante.Text = info.cargorepresentante;
        }

        private void EmpresaGereInfo(clsEmpresaGereInfo info)
        {
            info.id = empresasgere_id;
            info.empresa = id;
            info.idcodcofins = empresasgere_idcodcofins;
            info.idcodcsll = empresasgere_idcodcsll;
            info.idcodinss = empresasgere_idcodinss;
            info.idcodirrf = empresasgere_idcodirrf;
            info.idcodpis = empresasgere_idcodpis;
            info.idcodpiscofins = empresasgere_idcodpiscofins;
            info.idcodiss = empresasgere_idcodiss;
            info.idregimeapuracao = empresasgere_idregimeapuracao;
            info.idregimetributario = empresasgere_idregimetributario;
            info.idforcofins = empresasgere_idforcofins;
            info.idforcsll = empresasgere_idforcsll;
            info.idforinss = empresasgere_idforinss;
            info.idforirrf = empresasgere_idforirrf;
            info.idforiss = empresasgere_idforiss;
            info.idforpis = empresasgere_idforpis;
            info.idforpiscofins = empresasgere_idforpiscofins;
            info.idpagcofins = empresasgere_idpagcofins;
            info.idpagcsll = empresasgere_idpagcsll;
            info.idpaginss = empresasgere_idpaginss;
            info.idpagirrf = empresasgere_idpagirrf;
            info.idpagiss = empresasgere_idpagiss;
            info.idpagpis = empresasgere_idpagpis;
            info.idpagpiscofins = empresasgere_idpagpiscofins;

            // Guia Contabilidade
            //if (rbnempresasgere_CtabilContaVinculadaS.Checked)
            //{
            //    info.ctabilcontavinculada = "S";
            //}
            //else
            //{
            //    info.ctabilcontavinculada = "N";
            //}

            // Guia Fiscal
            info.fiscalirrf = clsParser.DecimalParse(tbxFiscalIrrf.Text);
            info.darfirrf = tbxDarfIrrf.Text;
            info.fiscalinss = clsParser.DecimalParse(tbxFiscalInss.Text);
            info.darfinss = tbxDarfInss.Text;
            info.fiscalpis = clsParser.DecimalParse(tbxFiscalPis.Text);
            info.darfpis = tbxDarfPis.Text;
            info.fiscalcofins = clsParser.DecimalParse(tbxFiscalCofins.Text);
            info.darfcofins = tbxDarfCofins.Text;
            info.fiscalcsll = clsParser.DecimalParse(tbxFiscalCsll.Text);
            info.darfcsll = tbxDarfCsll.Text;
            info.fiscalpiscofins = clsParser.DecimalParse(tbxFiscalPisCofins.Text);
            info.darfpiscofins = tbxDarfPisCofins.Text;
            info.icmsacreditar = clsParser.DecimalParse(tbxIcmsACreditar.Text);
            info.limitepiscofins = clsParser.DecimalParse(tbxLimitePisCofins.Text);
            info.leipispasep = clsParser.DecimalParse(tbxLeiPisPasep.Text);
            info.leicofins = clsParser.DecimalParse(tbxLeiCofins.Text);
            //
            info.leipispasepretido = clsParser.DecimalParse(tbxLeiPisPasepRetido.Text);
            info.leicofinsretido = clsParser.DecimalParse(tbxLeiCofinsRetido.Text);
            //
            info.fiscaliss = clsParser.DecimalParse(tbxFiscalIss.Text);
            info.darfiss = tbxDarfIss.Text;

            // Guia Gerencial
            info.gerautorizasolicitacao = cbxGerAutorizaSolicitacao.Text.Substring(0, 1);
            info.gerautorizacompra = cbxGerAutorizaCompra.Text.Substring(0, 1);
            info.gercotacaopreco = cbxGerCotacaoPreco.Text.Substring(0, 1);
            info.gerdivergenciacompra = cbxGerDivergenciaCompra.Text.Substring(0, 1);
            info.gerautorizapedido = cbxGerAutorizaPedido.Text.Substring(0, 1);
            info.gerautorizafaturar = cbxGerAutorizaFaturar.Text.Substring(0, 1);
            info.autorizacomprador = clsParser.DecimalParse(tbxAutorizaComprador.Text);
            info.autorizagerente = clsParser.DecimalParse(tbxAutorizaGerente.Text);
            info.autorizanormal = clsParser.DecimalParse(tbxAutorizaNormal.Text);
            info.metodoautsolic = clsParser.Int32Parse(lbxTipoAutorizacaoSolicitacao.Text.ToString().Substring(0, 1));
            info.checarsolic = clsParser.Int32Parse(lbxTipoChecaSolicitacao.Text.ToString().Substring(0, 1));
            info.cta991 = cbxCta991.Text.Substring(0, 1);
            info.cta995 = cbxCta995.Text.Substring(0, 1);

            // Guia Compras
            info.qtdemais = clsParser.DecimalParse(tbxQtdeMais.Text);
            info.qtdemenos = clsParser.DecimalParse(tbxQtdeMenos.Text);
            info.qtdemaisun = clsParser.DecimalParse(tbxQtdeMaisUn.Text);
            info.qtdemenosun = clsParser.DecimalParse(tbxQtdeMenosUn.Text);
            info.valormais = clsParser.DecimalParse(tbxValorMais.Text);
            info.valormenos = clsParser.DecimalParse(tbxValorMenos.Text);
            info.totalmais = clsParser.DecimalParse(tbxTotalMais.Text);
            info.totalmenos = clsParser.DecimalParse(tbxTotalMenos.Text);
            info.vencemais = clsParser.DecimalParse(tbxVenceMais.Text);
            info.vencemenos = clsParser.DecimalParse(tbxVenceMenos.Text);
            info.qtdepedckg = clsParser.DecimalParse(tbxQtdePedCKg.Text);
            info.qtdepedcun = clsParser.DecimalParse(tbxQtdePedCun.Text);

            // Guia Financeiro
            info.jurosmes = clsParser.DecimalParse(tbxJurosMes.Text);
            info.protestar = clsParser.Int32Parse(tbxProtestar.Text);
            info.multa = clsParser.DecimalParse(tbxMulta.Text);
            info.sequencia = clsParser.Int32Parse(tbxSequencia.Text);
            info.registro = clsParser.Int32Parse(tbxRegistro.Text);
            info.boletolinha01 = tbxBoletolinha01.Text;
            info.boletolinha02 = tbxBoletolinha02.Text;
            info.boletolinha03 = tbxBoletolinha03.Text;
            info.boletolinha04 = tbxBoletolinha04.Text;
            info.boletolinha05 = tbxBoletolinha05.Text;
            info.aceitaqtosdias = clsParser.Int32Parse(tbxAceitaQtosDias.Text);
            //            info.despesaconduta = cbxDespesaConduta.Text.Substring(0, 1);
            info.aceitanrozero = cbxAceitaNroZero.Text.Substring(0, 1);
            info.aceitavalorzero = cbxAceitaValorZero.Text.Substring(0, 1);
            //            info.despesaprocedure = tbxDespesaProcedure.Text;
            if (rbnGrupoDespesa0.Checked) info.grupodespesa = 0;
            if (rbnGrupoDespesa1.Checked) info.grupodespesa = 1;
            if (rbnGrupoDespesa2.Checked) info.grupodespesa = 2;
            if (rbnGrupoDespesa3.Checked) info.grupodespesa = 3;
            //if (rbnCentroCusto0.Checked) info.centrocusto = 0;
            //if (rbnCentroCusto1.Checked) info.centrocusto = 1;
            //if (rbnCentroCusto2.Checked) info.centrocusto = 2;
            //if (rbnSetorCentroCusto0.Checked) info.setorcentrocusto = 0;
            //if (rbnSetorCentroCusto1.Checked) info.setorcentrocusto = 1;
            //if (rbnSetorCentroCusto2.Checked) info.setorcentrocusto = 2;
            //if (rbnSetorCentroCusto3.Checked) info.setorcentrocusto = 3;
            //if (rbnSetorCentroCusto4.Checked) info.setorcentrocusto = 4;
            if (rbnBaixarcomhistoricoCX.Checked)
            {
                info.baixarcomhistorico = "CX";
            }
            else
            {
                info.baixarcomhistorico = "NF";
            }
            if (ckxBaixacomNotaFiscal.Checked == true)
            {
                info.baixatemnotafiscal = "S";

            }
            else
            {
                info.baixatemnotafiscal = "N";
            }
            // Guia Faturamento
            //info.somapesonfv = cbxSomaPesoNFV.Text.Substring(0, 1);
            //info.listaprefat = cbxListaPreFat.Text.Substring(0, 1);
            //info.pedidofrete = cbxPedidoFrete.Text.Substring(0, 1);
            //info.comissaovaria = cbxComissaoVaria.Text.Substring(0, 1);

            // Guia Custos
            //            info.basemp = clsParser.DecimalParse(tbxBaseMP.Text);
            info.adm = clsParser.DecimalParse(tbxAdm.Text);
            info.financeira = clsParser.DecimalParse(tbxFinanceira.Text);
            info.comissao = clsParser.DecimalParse(tbxComissao.Text);
            info.rejeicao = clsParser.DecimalParse(tbxRejeicao.Text);
            info.pis = clsParser.DecimalParse(tbxPis.Text);
            info.cofins = clsParser.DecimalParse(tbxCofins.Text);
            info.contsocial = clsParser.DecimalParse(tbxContSocial.Text);
            info.irpj = clsParser.DecimalParse(tbxIrpj.Text);
            info.icm = clsParser.DecimalParse(tbxIcm.Text);
            info.ipi = clsParser.DecimalParse(tbxIpi.Text);
            info.lucro = clsParser.DecimalParse(tbxLucro.Text);
            info.bola = clsParser.DecimalParse(tbxBola.Text);
            info.markup = clsParser.DecimalParse(tbxMarkup.Text);
            if (rbnCustoPorDentroA.Checked) info.custopordentro = "A";
            if (rbnCustoPorDentroB.Checked) info.custopordentro = "B";
            if (rbnCustoPorDentroC.Checked) info.custopordentro = "C";
            else info.custopordentro = "A";

            // Guia Aplibank
            //            info.bcoplanocontas = cbxBcoPlanoContas.Text.Substring(0, 1);
            info.bcoarquivomortodias = clsParser.Int32Parse(tbxBcoArquivoMortoDias.Text);
            info.bcoarquivodata = clsParser.DateTimeParse(tbxBcoArquivoData.Text);
            info.bcochequeunico = cbxBcoChequeUnico.Text.Substring(0, 1);
            info.transfbcohist = cbxTransfBcoHist.Text.Substring(0, 1);
            info.transfbcocusto = cbxTransfBcoCusto.Text.Substring(0, 1);

            // Guia Diversos
            //info.temprospeccao = cbxTemProspeccao.Text.Substring(0, 1);
            //info.peddescgd = clsParser.DecimalParse(tbxPeddescgd.Text);
            //info.peddescpzo = clsParser.DecimalParse(tbxPeddescpzo.Text);
            //info.peddescvis = clsParser.DecimalParse(tbxPeddescvis.Text);
            info.representantelegal = tbxRepresentanteLegal.Text;
            info.cpfrepresentante = tbxCPFRepresentante.Text;
            info.cargorepresentante = tbxCargoRepsesentante.Text;
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
        private void ControlKeyDownCgc(object sender, KeyEventArgs e)
        {
            //if (cbxPessoa.Text.Substring(0, 1) == "F")
            //{
            //clsVisual.ControlKeyDownCpf((TextBox)sender, e);
            //}
            //else if (cbxPessoa.Text.Substring(0, 1) == "J")
            //{
                clsVisual.ControlKeyDownCnpj((TextBox)sender, e);
            //}
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
            //if (cbxPessoa.Text.Substring(0, 1) == "J")
            //{
            clsVisual.ControlKeyDownIE((TextBox)sender, e, cbxUf.Text);
            //}
            //else
            //{
            //    clsVisual.ControlKeyDown(sender, e);
            //}
        }
        private void ControlKeyDownTelefone(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownTelefone((TextBox)sender, e);
        }


        void TrataCampos(Control ctl)
        {
            //PessoaTipo();

            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                if (clsInfo.znomegrid == btnRamo.Name)
                {
                    idramo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRamo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxRamo.Select();
                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIrrf.Name)
                {
                    empresasgere_idcodirrf = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodIrrf.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDarfIrrf.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from PECAS where ID=" + empresasgere_idcodirrf, "0");
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnForIrrf.Name)
                {
                    empresasgere_idforirrf = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxForIrrf.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPagIrrf.Name)
                {
                    empresasgere_idpagirrf = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPagIrrf.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnInss.Name)
                {
                    empresasgere_idcodinss = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodInss.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDarfInss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from PECAS where ID=" + empresasgere_idcodinss, "0");
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnForInss.Name)
                {
                    empresasgere_idforinss = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxForInss.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPagInss.Name)
                {
                    empresasgere_idpaginss = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPagInss.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPis.Name)
                {
                    empresasgere_idcodpis = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodPis.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDarfPis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from PECAS where ID=" + empresasgere_idcodpis, "0");

                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnForPis.Name)
                {
                    empresasgere_idforpis = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxForPis.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPagPis.Name)
                {
                    empresasgere_idpagpis = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPagPis.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnCofins.Name)
                {
                    empresasgere_idcodcofins = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodCofins.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDarfCofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from PECAS where ID=" + empresasgere_idcodcofins, "0");
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnForCofins.Name)
                {
                    empresasgere_idforcofins = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxForCofins.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPagCofins.Name)
                {
                    empresasgere_idpagcofins = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPagCofins.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnCsll.Name)
                {
                    empresasgere_idcodcsll = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodCsll.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDarfCsll.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from PECAS where ID=" + empresasgere_idcodcsll, "0");
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnForCsll.Name)
                {
                    empresasgere_idforcsll = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxForCsll.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPagCsll.Name)
                {
                    empresasgere_idpagcsll = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPagCsll.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPisCofinsCsll.Name)
                {
                    empresasgere_idcodpiscofins = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodPisCofins.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDarfPisCofins.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from PECAS where ID=" + empresasgere_idcodpiscofins, "0");
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnForPiscofins.Name)
                {
                    empresasgere_idforpiscofins = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxForPiscofins.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPagPiscofins.Name)
                {
                    empresasgere_idpagpiscofins = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPagPiscofins.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIss.Name)
                {
                    empresasgere_idcodiss = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodIss.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDarfIss.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from PECAS where ID=" + empresasgere_idcodiss, "0");
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnForIss.Name)
                {
                    empresasgere_idforiss = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxForIss.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPagIss.Name)
                {
                    empresasgere_idpagiss = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPagIss.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    //                    tbxRamo.Select();
                    //                    tbxRamo.SelectAll();
                }




            }
            else if (ctl.Name == tbxCognome.Name)
            {
                tbxCognome.Text = tbxCognome.Text.Trim();

                if (tbxCognome.Text != "")
                {
                    Int32 id_registro = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cognome='" + tbxCognome.Text + "'", "0"));
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
                    Int32 id_registro = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cgc='" + s + "'", "0"));
                    if (id_registro > 0 && id_registro != id)
                    {
                        MessageBox.Show("CPF/RG já existe. Tente novamente.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxCgc.Select();
                        tbxCgc.SelectAll();
                    }
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
                    tbxCidade.Text = Cidade.Replace("-", " ");
                    //cbxCidade.SelectedIndex = SelecionarIndex(Cidade, 0, Cidade);
                    // cbxCidade.SelectedIndex = cbxCidade.FindString(Cidade + "   -   " + Cep.UF);

                    cbxUf.Text = Cep.UF;
                    tbxIbge.Text = Cep.IBGE;
                }
            }
            else if (ctl.Name == cbxUf.Name)
            {
                //idestado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "ESTADOS", "ID", "ESTADO", cbxUf.Text));
                idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + cbxUf.Text + "' ", "0"));
                if (idestado == 0 && ((TextBox)ctl).Text.Trim().Length > 0)
                {
                    ((ComboBox)ctl).SelectedIndex = 0;
                }
            }
            else if (ctl.Name == tbxRamo.Name)
            {
                String[] s;
                s = tbxRamo.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //idramo = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "RAMO", "ID", "CODIGO", s[0]));
                    idramo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from RAMO where CODIGO='" + s[0] + "' ", "0"));

                }
                else
                {
                    idramo = 0;
                }

                if (idramo == 0 && tbxRamo.Text.Length > 0)
                {
                    tbxRamo.Text = "";
                    tbxRamo.Select();
                    tbxRamo.SelectAll();
                }
                else if (idramo == 0)
                {
                    tbxRamo.Text = "";
                }
            }
            else if (ctl.Name == tbxRegimeApuracao_Nome.Name)
            {
                String[] s;
                s = tbxRegimeApuracao_Nome.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idregimeapuracao = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "REGIMEAPURACAO", "ID", "CODIGO", s[0]));
                    empresasgere_idregimeapuracao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from REGIMEAPURACAO where CODIGO='" + s[0] + "' ", "0"));

                }
                else
                {
                    empresasgere_idregimeapuracao = 0;
                }

                if (empresasgere_idregimeapuracao == 0 && tbxRegimeApuracao_Nome.Text.Length > 0)
                {
                    tbxRegimeApuracao_Nome.Text = "";
                    tbxRegimeApuracao_Nome.Select();
                    tbxRegimeApuracao_Nome.SelectAll();
                }
                else if (empresasgere_idregimeapuracao == 0)
                {
                    tbxRegimeApuracao_Nome.Text = "";
                }
            }
            else if (ctl.Name == tbxRegimeTributario.Name)
            {
                String[] s;
                s = tbxRegimeTributario.Text.Split('-');
                if (s[0].Length > 0)
                {
                    empresasgere_idregimetributario = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from REGIMETRIBUTARIO where CODIGO='" + s[0] + "' "));
                }
                else
                {
                    empresasgere_idregimetributario = 0;
                }

                if (empresasgere_idregimetributario == 0 && tbxRegimeTributario.Text.Length > 0)
                {
                    tbxRegimeTributario.Text = "";
                    tbxRegimeTributario.Select();
                    tbxRegimeTributario.SelectAll();
                }
                else if (empresasgere_idregimetributario == 0)
                {
                    tbxRegimeTributario.Text = "";
                }
            }
            else if (ctl.Name == tbxCodCofins.Name)
            {
                String[] s;
                s = tbxCodCofins.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idcodcofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ID", "CODIGO", s[0]));
                    empresasgere_idcodcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idcodcofins = 0;
                }

                if (empresasgere_idcodcofins == 0 && tbxCodCofins.Text.Length > 0)
                {
                    tbxCodCofins.Text = "";
                    tbxCodCofins.Select();
                    tbxCodCofins.SelectAll();
                }
                else if (empresasgere_idcodcofins == 0)
                {
                    tbxCodCofins.Text = "";
                }
            }
            else if (ctl.Name == tbxCodCsll.Name)
            {
                String[] s;
                s = tbxCodCsll.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idcodcsll = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ID", "CODIGO", s[0]));
                    empresasgere_idcodcsll = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idcodcsll = 0;
                }

                if (empresasgere_idcodcsll == 0 && tbxCodCsll.Text.Length > 0)
                {
                    tbxCodCsll.Text = "";
                    tbxCodCsll.Select();
                    tbxCodCsll.SelectAll();
                }
                else if (empresasgere_idcodcsll == 0)
                {
                    tbxCodCsll.Text = "";
                }
            }
            else if (ctl.Name == tbxGeradordocumento_docfiscal_codigo.Name)
            {
                //idgeradordocumento = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "ID", "COGNOME", tbxGeradordocumento_docfiscal_codigo.Text));
                //idgeradordocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + tbxGeradordocumento_docfiscal_codigo.Text + "' ", "0"));

                //if (idgeradordocumento == 0 && tbxGeradordocumento_docfiscal_codigo.Text.Length > 0)
                //{
                //    tbxGeradordocumento_docfiscal_codigo.Text = "";
                //    tbxGeradordocumento_docfiscal_codigo.Select();
                //    tbxGeradordocumento_docfiscal_codigo.SelectAll();
                //}
                //else if (idgeradordocumento == 0)
                //{
                //    tbxGeradordocumento_docfiscal_codigo.Text = "";
                //}
            }
            else if (ctl.Name == tbxCodInss.Name)
            {
                String[] s;
                s = tbxCodInss.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idcodinss = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ID", "CODIGO", s[0]));
                    empresasgere_idcodinss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idcodinss = 0;
                }

                if (empresasgere_idcodinss == 0 && tbxCodInss.Text.Length > 0)
                {
                    tbxCodInss.Text = "";
                    tbxCodInss.Select();
                    tbxCodInss.SelectAll();
                }
                else if (empresasgere_idcodinss == 0)
                {
                    tbxCodInss.Text = "";
                }
            }
            else if (ctl.Name == tbxCodIrrf.Name)
            {
                String[] s;
                s = tbxCodIrrf.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idcodirrf = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ID", "CODIGO", s[0]));
                    empresasgere_idcodinss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idcodirrf = 0;
                }

                if (empresasgere_idcodirrf == 0 && tbxCodIrrf.Text.Length > 0)
                {
                    tbxCodIrrf.Text = "";
                    tbxCodIrrf.Select();
                    tbxCodIrrf.SelectAll();
                }
                else if (empresasgere_idcodirrf == 0)
                {
                    tbxCodIrrf.Text = "";
                }
            }
            else if (ctl.Name == tbxCodIss.Name)
            {
                String[] s;
                s = tbxCodIss.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idcodiss = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ID", "CODIGO", s[0]));
                    empresasgere_idcodiss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idcodiss = 0;
                }

                if (empresasgere_idcodiss == 0 && tbxCodIss.Text.Length > 0)
                {
                    tbxCodIss.Text = "";
                    tbxCodIss.Select();
                    tbxCodIss.SelectAll();
                }
                else if (empresasgere_idcodiss == 0)
                {
                    tbxCodIss.Text = "";
                }
            }
            else if (ctl.Name == tbxCodPis.Name)
            {
                String[] s;
                s = tbxCodPis.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idcodpis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ID", "CODIGO", s[0]));
                    empresasgere_idcodpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idcodpis = 0;
                }

                if (empresasgere_idcodpis == 0 && tbxCodPis.Text.Length > 0)
                {
                    tbxCodPis.Text = "";
                    tbxCodPis.Select();
                    tbxCodPis.SelectAll();
                }
                else if (empresasgere_idcodpis == 0)
                {
                    tbxCodPis.Text = "";
                }
            }
            else if (ctl.Name == tbxCodPisCofins.Name)
            {
                String[] s;
                s = tbxCodPisCofins.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idcodpiscofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ID", "CODIGO", s[0]));
                    empresasgere_idcodpiscofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idcodpiscofins = 0;
                }

                if (empresasgere_idcodpiscofins == 0 && tbxCodPisCofins.Text.Length > 0)
                {
                    tbxCodPisCofins.Text = "";
                    tbxCodPisCofins.Select();
                    tbxCodPisCofins.SelectAll();
                }
                else if (empresasgere_idcodpiscofins == 0)
                {
                    tbxCodPisCofins.Text = "";
                }
            }
            else if (ctl.Name == tbxForCofins.Name)
            {
                if (tbxForCofins.Text.Length > 0)
                {
                    //empresasgere_idforcofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxForCofins.Text));
                    empresasgere_idforcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxForCofins.Text + "' ", "0"));
                }
                else
                {
                    empresasgere_idforcofins = 0;
                }

                if (empresasgere_idforcofins == 0 && tbxForCofins.Text.Length > 0)
                {
                    tbxForCofins.Text = "";
                    tbxForCofins.Select();
                    tbxForCofins.SelectAll();
                }
                else if (empresasgere_idforcofins == 0)
                {
                    tbxForCofins.Text = "";
                }
            }
            else if (ctl.Name == tbxForCsll.Name)
            {
                if (tbxForCsll.Text.Length > 0)
                {
                    //empresasgere_idforcsll = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxForCsll.Text));
                    empresasgere_idforcsll = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxForCsll.Text + "' ", "0"));
                }
                else
                {
                    empresasgere_idforcsll = 0;
                }

                if (empresasgere_idforcsll == 0 && tbxForCsll.Text.Length > 0)
                {
                    tbxForCsll.Text = "";
                    tbxForCsll.Select();
                    tbxForCsll.SelectAll();
                }
                else if (empresasgere_idforcsll == 0)
                {
                    tbxForCsll.Text = "";
                }
            }
            else if (ctl.Name == tbxForInss.Name)
            {
                if (tbxForInss.Text.Length > 0)
                {
                    //empresasgere_idforinss = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxForInss.Text));
                    empresasgere_idforinss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxForInss.Text + "' ", "0"));
                }
                else
                {
                    empresasgere_idforinss = 0;
                }

                if (empresasgere_idforinss == 0 && tbxForInss.Text.Length > 0)
                {
                    tbxForInss.Text = "";
                    tbxForInss.Select();
                    tbxForInss.SelectAll();
                }
                else if (empresasgere_idforinss == 0)
                {
                    tbxForInss.Text = "";
                }
            }
            else if (ctl.Name == tbxForIrrf.Name)
            {
                if (tbxForIrrf.Text.Length > 0)
                {
                    //empresasgere_idforirrf = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxForIrrf.Text));
                    empresasgere_idforirrf = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxForIrrf.Text + "' ", "0"));
                }
                else
                {
                    empresasgere_idforirrf = 0;
                }

                if (empresasgere_idforirrf == 0 && tbxForIrrf.Text.Length > 0)
                {
                    tbxForIrrf.Text = "";
                    tbxForIrrf.Select();
                    tbxForIrrf.SelectAll();
                }
                else if (empresasgere_idforirrf == 0)
                {
                    tbxForIrrf.Text = "";
                }
            }
            else if (ctl.Name == tbxForIss.Name)
            {
                if (tbxForIss.Text.Length > 0)
                {
                    //empresasgere_idforiss = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxForIss.Text));
                    empresasgere_idforiss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxForIss.Text + "' ", "0"));
                }
                else
                {
                    empresasgere_idforiss = 0;
                }

                if (empresasgere_idforiss == 0 && tbxForIss.Text.Length > 0)
                {
                    tbxForIss.Text = "";
                    tbxForIss.Select();
                    tbxForIss.SelectAll();
                }
                else if (empresasgere_idforiss == 0)
                {
                    tbxForIss.Text = "";
                }
            }
            else if (ctl.Name == tbxForPis.Name)
            {
                if (tbxForPis.Text.Length > 0)
                {
                    //empresasgere_idforpis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxForPis.Text));
                    empresasgere_idforpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxForPis.Text + "' ", "0"));
                }
                else
                {
                    empresasgere_idforpis = 0;
                }

                if (empresasgere_idforpis == 0 && tbxForPis.Text.Length > 0)
                {
                    tbxForPis.Text = "";
                    tbxForPis.Select();
                    tbxForPis.SelectAll();
                }
                else if (empresasgere_idforpis == 0)
                {
                    tbxForPis.Text = "";
                }
            }
            else if (ctl.Name == tbxForPiscofins.Name)
            {
                if (tbxForPiscofins.Text.Length > 0)
                {
                    //empresasgere_idforpiscofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxForPiscofins.Text));
                    empresasgere_idforpiscofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxForPiscofins.Text + "' ", "0"));
                }
                else
                {
                    empresasgere_idforpiscofins = 0;
                }

                if (empresasgere_idforpiscofins == 0 && tbxForPiscofins.Text.Length > 0)
                {
                    tbxForPiscofins.Text = "";
                    tbxForPiscofins.Select();
                    tbxForPiscofins.SelectAll();
                }
                else if (empresasgere_idforpiscofins == 0)
                {
                    tbxForPiscofins.Text = "";
                }
            }
            else if (ctl.Name == tbxPagCofins.Name)
            {
                String[] s;
                s = tbxPagCofins.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpagcofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpagcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpagcofins = 0;
                }

                if (empresasgere_idpagcofins == 0 && tbxPagCofins.Text.Length > 0)
                {
                    tbxPagCofins.Text = "";
                    tbxPagCofins.Select();
                    tbxPagCofins.SelectAll();
                }
                else if (empresasgere_idpagcofins == 0)
                {
                    tbxPagCofins.Text = "";
                }
            }
            else if (ctl.Name == tbxPagCofins.Name)
            {
                String[] s;
                s = tbxPagCofins.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpagcofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpagcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpagcofins = 0;
                }

                if (empresasgere_idpagcofins == 0 && tbxPagCofins.Text.Length > 0)
                {
                    tbxPagCofins.Text = "";
                    tbxPagCofins.Select();
                    tbxPagCofins.SelectAll();
                }
                else if (empresasgere_idpagcofins == 0)
                {
                    tbxPagCofins.Text = "";
                }
            }
            else if (ctl.Name == tbxPagCsll.Name)
            {
                String[] s;
                s = tbxPagCsll.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpagcsll = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpagcsll = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpagcsll = 0;
                }

                if (empresasgere_idpagcsll == 0 && tbxPagCsll.Text.Length > 0)
                {
                    tbxPagCsll.Text = "";
                    tbxPagCsll.Select();
                    tbxPagCsll.SelectAll();
                }
                else if (empresasgere_idpagcsll == 0)
                {
                    tbxPagCsll.Text = "";
                }
            }
            else if (ctl.Name == tbxPagInss.Name)
            {
                String[] s;
                s = tbxPagInss.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpaginss = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpaginss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpaginss = 0;
                }

                if (empresasgere_idpaginss == 0 && tbxPagInss.Text.Length > 0)
                {
                    tbxPagInss.Text = "";
                    tbxPagInss.Select();
                    tbxPagInss.SelectAll();
                }
                else if (empresasgere_idpaginss == 0)
                {
                    tbxPagInss.Text = "";
                }
            }
            else if (ctl.Name == tbxPagIrrf.Name)
            {
                String[] s;
                s = tbxPagIrrf.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpagirrf = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpagirrf = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpagirrf = 0;
                }

                if (empresasgere_idpagirrf == 0 && tbxPagIrrf.Text.Length > 0)
                {
                    tbxPagIrrf.Text = "";
                    tbxPagIrrf.Select();
                    tbxPagIrrf.SelectAll();
                }
                else if (empresasgere_idpagirrf == 0)
                {
                    tbxPagIrrf.Text = "";
                }
            }
            else if (ctl.Name == tbxPagIss.Name)
            {
                String[] s;
                s = tbxPagIss.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpagiss = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpagiss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpagiss = 0;
                }

                if (empresasgere_idpagiss == 0 && tbxPagIss.Text.Length > 0)
                {
                    tbxPagIss.Text = "";
                    tbxPagIss.Select();
                    tbxPagIss.SelectAll();
                }
                else if (empresasgere_idpagiss == 0)
                {
                    tbxPagIss.Text = "";
                }
            }
            else if (ctl.Name == tbxPagPis.Name)
            {
                String[] s;
                s = tbxPagPis.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpagpis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpagpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpagpis = 0;
                }

                if (empresasgere_idpagpis == 0 && tbxPagPis.Text.Length > 0)
                {
                    tbxPagPis.Text = "";
                    tbxPagPis.Select();
                    tbxPagPis.SelectAll();
                }
                else if (empresasgere_idpagpis == 0)
                {
                    tbxPagPis.Text = "";
                }
            }
            else if (ctl.Name == tbxPagPiscofins.Name)
            {
                String[] s;
                s = tbxPagPiscofins.Text.Split('=');
                if (s[0].Length > 0)
                {
                    //empresasgere_idpagpiscofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "ID", "CODIGO", s[0]));
                    empresasgere_idpagpiscofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + s[0] + "' ", "0"));
                }
                else
                {
                    empresasgere_idpagpiscofins = 0;
                }

                if (empresasgere_idpagpiscofins == 0 && tbxPagPiscofins.Text.Length > 0)
                {
                    tbxPagPiscofins.Text = "";
                    tbxPagPiscofins.Select();
                    tbxPagPiscofins.SelectAll();
                }
                else if (empresasgere_idpagpiscofins == 0)
                {
                    tbxPagPiscofins.Text = "";
                }
            }


            tbxCognome.Text = clsVisual.RemoveAcentos(tbxCognome.Text);
            tbxNome.Text = clsVisual.RemoveAcentos(tbxNome.Text);
            if (ckxBaixacomNotaFiscal.Checked == true)
            {
                ckxBaixacomNotaFiscal.Text = "Sim pode Baixar sem Nota Fiscal";
            }
            else
            {
                ckxBaixacomNotaFiscal.Text = "Não pode Baixar sem Nota Fiscal";
            }
            tbxFiscalCofins.Text = (clsParser.DecimalParse(tbxFiscalCofins.Text)).ToString("N2");
            tbxFiscalCsll.Text = (clsParser.DecimalParse(tbxFiscalCsll.Text)).ToString("N2");
            tbxFiscalInss.Text = (clsParser.DecimalParse(tbxFiscalInss.Text)).ToString("N2");
            tbxFiscalIrrf.Text = (clsParser.DecimalParse(tbxFiscalIrrf.Text)).ToString("N2");
            tbxFiscalIss.Text = (clsParser.DecimalParse(tbxFiscalIss.Text)).ToString("N2");
            tbxFiscalPis.Text = (clsParser.DecimalParse(tbxFiscalPis.Text)).ToString("N2");
            tbxFiscalPisCofins.Text = (clsParser.DecimalParse(tbxFiscalPisCofins.Text)).ToString("N2");

            tbxLeiCofins.Text = (clsParser.DecimalParse(tbxLeiCofins.Text)).ToString("N2");
            tbxLeiCofinsRetido.Text = (clsParser.DecimalParse(tbxLeiCofinsRetido.Text)).ToString("N2");
            tbxLeiPisPasep.Text = (clsParser.DecimalParse(tbxLeiPisPasep.Text)).ToString("N2");
            tbxLeiPisPasepRetido.Text = (clsParser.DecimalParse(tbxLeiPisPasepRetido.Text)).ToString("N2");
            tbxLeiPisPasepRetido.Text = (clsParser.DecimalParse(tbxLeiPisPasepRetido.Text)).ToString("N2");
            tbxLimitePisCofins.Text = (clsParser.DecimalParse(tbxLimitePisCofins.Text)).ToString("N2");

            Decimal soma;
            soma = clsParser.DecimalParse(tbxAdm.Text);
            soma = soma + clsParser.DecimalParse(tbxFinanceira.Text);
            soma = soma + clsParser.DecimalParse(tbxComissao.Text);
            soma = soma + clsParser.DecimalParse(tbxRejeicao.Text);
            soma = soma + clsParser.DecimalParse(tbxPis.Text);
            soma = soma + clsParser.DecimalParse(tbxCofins.Text);
            soma = soma + clsParser.DecimalParse(tbxContSocial.Text);
            soma = soma + clsParser.DecimalParse(tbxIrpj.Text);
            soma = soma + clsParser.DecimalParse(tbxIcm.Text);
            soma = soma + clsParser.DecimalParse(tbxIpi.Text);
            soma = soma + clsParser.DecimalParse(tbxLucro.Text);
            soma = soma + clsParser.DecimalParse(tbxBola.Text);
            tbxSoma.Text = soma.ToString("N2");
            if (clsParser.DecimalParse(tbxSoma.Text) > 0)
            {
                soma = 100 - clsParser.DecimalParse(tbxSoma.Text);
                soma = 100 / soma;
                tbxMarkup.Text = soma.ToString("N4");
            }

            tbxJurosMes.Text = clsParser.DecimalParse(tbxJurosMes.Text).ToString("N2");
            tbxMulta.Text = clsParser.DecimalParse(tbxMulta.Text).ToString("N2");

            clsInfo.znomegrid = "";
            clsInfo.zrow = null;

        }

        private void btnCep_Click(object sender, EventArgs e)
        {
            BuscaCEP Cep = new BuscaCEP();
            Cep.Buscar(tbxCep.Text);


            tbxEndereco.Text = Cep.Logradouro;
            tbxBairro.Text = Cep.Bairro;
            String Cidade = Cep.Localidade;
            Cidade = clsVisual.RemoveAcentos(Cidade);
            tbxCidade.Text = Cidade.Replace("-", " ");
            //cbxCidade.SelectedIndex = SelecionarIndex(Cidade, 0, Cidade);
            //cbxCidade.SelectedIndex = cbxCidade.FindString(Cidade + "   -   " + Cep.UF);

            cbxUf.Text = Cep.UF;
            tbxIbge.Text = Cep.IBGE;
        }

        private void frmEmpresa_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void btnRamo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnRamo";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "RAMO", idramo, 20, 50, "Ramo de Atividade");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }
        private void EmpresasSalvar()
        {
            var scn = new SqlConnection(clsInfo.conexaosqldados);

            scn.Open();

            //            var transacao = scn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            try
            {
                clsEmpresaInfo = new clsEmpresaInfo();

                EmpresaInfo(clsEmpresaInfo);

                if (id == 0)
                {
                    id = clsEmpresaBLL.Incluir(clsEmpresaInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsEmpresaBLL.Alterar(clsEmpresaInfo, clsInfo.conexaosqldados);
                }

                EmpresasGereSalvar();
                //  EmpresasFolhaSalvar(transacao);

                if (Application.ProductName.ToString().ToUpper() != "FOLHA")
                {
                    Clientes_Atualizar(clsEmpresaInfo, clsInfo.conexaosqldados);
                }

                //                transacao.Commit();

            }
            catch (Exception ex)
            {
                //                transacao.Rollback();

                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //scn.Close();
            }
        }
        private void EmpresasGereSalvar()
        {
            clsEmpresaGereInfo = new clsEmpresaGereInfo();

            EmpresaGereInfo(clsEmpresaGereInfo);

            if (empresasgere_id == 0)
            {
                empresasgere_id = clsEmpresaGereBLL.Incluir(clsEmpresaGereInfo, clsInfo.conexaosqldados);
            }
            else
            {
                clsEmpresaGereBLL.Alterar(clsEmpresaGereInfo, clsInfo.conexaosqldados);
            }
        }

        private void Clientes_Atualizar(clsEmpresaInfo clsEmpresaInfo, String transacao)
        {
            int idCliente;

            if (clsVisual.ValidaCnpj(clsEmpresaInfoOld.cgc) == false)
            {
                idCliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "Select ID from CLIENTE where CGC = '" + clsEmpresaInfo.cgc + "'", "0"));

                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "Select ID from CLIENTE where CGC = '" + clsEmpresaInfo.cgc + "' and id <> " + idCliente, "0")) > 0)
                {
                    throw new Exception("Já existe cliente com este CNPJ");
                }
            }
            else
            {
                idCliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "Select ID from CLIENTE where CGC = '" + clsEmpresaInfoOld.cgc + "'", "0"));
            }
            /*
            if (idCliente > 0)
            {
                /*
                // Alterando os valores do Cabeçalho -- valores locais
                clsClienteInfo clsClienteInfo = new clsClienteInfo();
                clsClienteBLL clsClienteBLL = new clsClienteBLL();

                clsClienteInfo = clsClienteBLL.Carregar(idCliente, transacao);

                clsClienteInfo.id = idCliente;
                clsClienteInfo.cognome = clsEmpresaInfo.cognome;
                clsClienteInfo.nome = clsEmpresaInfo.nome;
                clsClienteInfo.pessoa = clsEmpresaInfo.pessoa;
                clsClienteInfo.tipo = "E";
                clsClienteInfo.ie = clsEmpresaInfo.ie;
                clsClienteInfo.cgc = clsEmpresaInfo.cgc;
                clsClienteInfo.imunicipal = clsEmpresaInfo.imunicipal;
                clsClienteInfo.endtipo = clsEmpresaInfo.endtipo;
                clsClienteInfo.endereco = clsEmpresaInfo.endereco;
                clsClienteInfo.tiponumero = clsEmpresaInfo.tiponumero;
                clsClienteInfo.numeroend = clsEmpresaInfo.numeroend;
                clsClienteInfo.andar = clsEmpresaInfo.andar;
                clsClienteInfo.tipocomple = clsEmpresaInfo.tipocomple;
                clsClienteInfo.comple = clsEmpresaInfo.comple;
                clsClienteInfo.bairro = clsEmpresaInfo.bairro;
                //clsClienteInfo.cidade = clsEmpresaInfo.cidade;
                clsClienteInfo.idestado = clsEmpresaInfo.idestado;
                clsClienteInfo.idcidade = clsEmpresaInfo.idcidade;
                clsClienteInfo.ibge = clsEmpresaInfo.ibge;
                clsClienteInfo.cep = clsEmpresaInfo.cep;
                clsClienteInfo.regiao = clsEmpresaInfo.regiao;
                clsClienteInfo.homepage = clsEmpresaInfo.homepage;
                clsClienteInfo.email = clsEmpresaInfo.email;
                clsClienteInfo.idregimeapuracao = clsEmpresaGereInfo.idregimeapuracao;
                clsClienteInfo.ultdatnf = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.ultdatorc = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.ultnronf = 0;
                clsClienteInfo.ultvalnf = 0;
                if (clsClienteInfo.idformapagto == 0)
                {
                    clsClienteInfo.idformapagto = clsInfo.zformapagto;
                }

                clsClienteBLL.Alterar(clsClienteInfo, transacao);
            }
            else
            {
                //MessageBox.Show("Cliente não Cadastrado - Porque ?? - Precisa revisar esta inclusão ");

                int cli_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT top 1 MAX(NUMERO) + 1 FROM CLIENTE group by NUMERO ORDER BY NUMERO DESC"));

                if (cli_numero == 0)
                {
                    cli_numero = 1;
                }

                var clsClienteInfo = new clsClienteInfo();
                var clsClienteBLL = new clsClienteBLL();

                clsClienteInfo = clsClienteBLL.Carregar(0, transacao);

                clsClienteInfo.id = 0;
                clsClienteInfo.numero = cli_numero;
                clsClienteInfo.cognome = clsEmpresaInfo.cognome;
                clsClienteInfo.nome = clsEmpresaInfo.nome;
                clsClienteInfo.pessoa = clsEmpresaInfo.pessoa;
                clsClienteInfo.tipo = "E";
                clsClienteInfo.ie = clsEmpresaInfo.ie;
                clsClienteInfo.cgc = clsEmpresaInfo.cgc;
                clsClienteInfo.imunicipal = clsEmpresaInfo.imunicipal;
                clsClienteInfo.endtipo = clsEmpresaInfo.endtipo;
                clsClienteInfo.endereco = clsEmpresaInfo.endereco;
                clsClienteInfo.tiponumero = clsEmpresaInfo.tiponumero;
                clsClienteInfo.numeroend = clsEmpresaInfo.numeroend;
                clsClienteInfo.andar = clsEmpresaInfo.andar;
                clsClienteInfo.tipocomple = clsEmpresaInfo.tipocomple;
                clsClienteInfo.comple = clsEmpresaInfo.comple;
                clsClienteInfo.bairro = clsEmpresaInfo.bairro;
                //clsClienteInfo.cidade = clsEmpresaInfo.cidade;
                clsClienteInfo.idcidade = clsEmpresaInfo.idcidade;
                clsClienteInfo.idestado = clsEmpresaInfo.idestado;
                clsClienteInfo.ibge = clsEmpresaInfo.ibge;
                clsClienteInfo.cep = clsEmpresaInfo.cep;
                clsClienteInfo.regiao = clsEmpresaInfo.regiao;
                clsClienteInfo.homepage = clsEmpresaInfo.homepage;
                clsClienteInfo.email = clsEmpresaInfo.email;
                clsClienteInfo.idregimeapuracao = 1;// clsInfo.zregimeapuracao;
                clsClienteInfo.idformapagto = 1;// clsInfo.zformapagto;
                clsClienteInfo.idcondpagto = 1;//clsInfo.zcondpagto;
                clsClienteInfo.idtransportadora = 1;//clsInfo.zempresaclienteid;
                clsClienteInfo.idrepresentante = 1;//clsInfo.zempresaclienteid;
                clsClienteInfo.idcoordenador = 1;//clsInfo.zempresaclienteid;
                clsClienteInfo.idsupervisor = 1;//clsInfo.zempresaclienteid;
                clsClienteInfo.idzona = 1;//clsInfo.zzona;
                clsClienteInfo.idramo = 1;//clsInfo.zramo;
                clsClienteInfo.contato = clsEmpresaInfo.contato;
                clsClienteInfo.telefone = clsEmpresaInfo.telefone;
                clsClienteInfo.ativo = "S";
                clsClienteInfo.suframa = "";
                clsClienteInfo.pais = "";
                clsClienteInfo.ftp = "";
                clsClienteInfo.codigocliente = "";
                clsClienteInfo.freteincluso = "";
                clsClienteInfo.freteporconta = "";
                clsClienteInfo.meiodetransporte = "";
                clsClienteInfo.credito = "";
                clsClienteInfo.agencia = "";
                clsClienteInfo.titularcta = "";
                clsClienteInfo.ctacorrente = "";
                clsClienteInfo.clienteaprovado = "";
                clsClienteInfo.ddd = "";
                clsClienteInfo.comissaopor = "";
                clsClienteInfo.comissaopagto = "";
                clsClienteInfo.comissaocomo = "";
                clsClienteInfo.descontapispasepent = "";
                clsClienteInfo.descontapispasepsai = "";
                clsClienteInfo.descontacofinsent = "";
                clsClienteInfo.descontacofinssai = "";
                clsClienteInfo.isentoipi = "";
                clsClienteInfo.revendedor = "";
                clsClienteInfo.contribuinte = "";
                clsClienteInfo.alc = "";
                clsClienteInfo.consumo = "";
                clsClienteInfo.temfaturamento = "N";
                clsClienteInfo.valorminimo = 0;
                clsClienteInfo.ultdatnf = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.ultdatorc = clsParser.DateTimeParse("01/01/1900");
                clsClienteInfo.ultnronf = 0;
                clsClienteInfo.ultvalnf = 0;

                idCliente = clsClienteBLL.Incluir(clsClienteInfo, transacao);
                
            }
          */
        }

        private void ckxBaixacomNotaFiscal_Click(object sender, EventArgs e)
        {
            if (ckxBaixacomNotaFiscal.Checked == true)
            {
                ckxBaixacomNotaFiscal.Text = "Sim pode Baixar sem Nota Fiscal";
            }
            else
            {
                ckxBaixacomNotaFiscal.Text = "Não pode Baixar sem Nota Fiscal";
            }
        }

        private void btnIrrf_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnIrrf";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECAS", empresasgere_idcodirrf, 30, 60, "Cadastro Codigo de Despesas");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);
    
        }

        private void btnForIrrf_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnForIrrf";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", empresasgere_idforirrf);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnInss_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnInss";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECAS", empresasgere_idcodinss, 30, 60, "Cadastro Codigo de Despesas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void btnForInss_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnForInss";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", empresasgere_idforinss);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnPis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPis";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECAS", empresasgere_idcodpis, 30, 60, "Cadastro Codigo de Despesas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void btnForPis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnForPis";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", empresasgere_idforpis);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnCofins_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnCofins";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECAS", empresasgere_idcodcofins, 30, 60, "Cadastro Codigo de Despesas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void btnForCofins_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnForCofins";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", empresasgere_idforcofins);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnPagIrrf_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPagIrrf";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", empresasgere_idpagirrf, "Condição Pagto IRRF");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnPagInss_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPagInss";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", empresasgere_idpaginss, "Cadastro Codigo de Despesas");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnPagPis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPagPis";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", empresasgere_idpagpis, "Cadastro de Despesas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnPagCofins_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPagCofins";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", empresasgere_idpagcofins, "Cadastro Condição Pagamento");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnCsll_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnCsll";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECAS", empresasgere_idcodcsll, 30, 60, "Cadastro de Despesas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void btnForCsll_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnForCsll";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", empresasgere_idforcsll);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnPagCsll_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPagCsll";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", empresasgere_idpagcsll, "Condição de Pagto");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnPisCofinsCsll_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPisCofinsCsll";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECAS", empresasgere_idcodpiscofins, 30, 60, "Cadastro de Despesas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void btnForPiscofins_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnForPiscofins";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", empresasgere_idforpiscofins);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnPagPiscofins_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPagPiscofins";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", empresasgere_idpagpiscofins, "Cadastro de Pagamento");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnIss_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnIss";
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECAS", empresasgere_idcodiss, 30, 60, "Cadastro de Despesas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void btnForIss_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnForIss";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", empresasgere_idforiss);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnPagIss_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnPagIss";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", empresasgere_idpagiss, "Cadastro de Cond Pagto");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }
    }
}
