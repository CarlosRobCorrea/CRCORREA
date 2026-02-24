using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CRCorrea;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CRCorrea

{
    public partial class frmSenha : Form
    {
        String conexao;

        public Byte tentativas;
        public static Boolean resultado;

        String query;
        SqlConnection scn;
        SqlCommand scd;

        String[] str;

        Form frmEmpresasVis;

        // transferencia das tabelas de txt para a base sql
        private Int32 qtcaracter;
        private Int32 posicaoatual;
        private Int32 posicaoinicial;
        private Int32 posicaofinal;
        private Boolean ok;
        private String campo;

        clsEstadosInfo clsEstadosInfo = new clsEstadosInfo();
        clsEstadosBLL clsEstadosBLL = new clsEstadosBLL();

        public frmSenha()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Form _frmEmpresaVis)
        {
            conexao = _conexao;
            frmEmpresasVis = _frmEmpresaVis;
            tentativas = 1;
            resultado = false;
        }

        private void frmSenha_Load(object sender, EventArgs e)
        {
            tbxEmpresa.Text = "1";
            tbxMes.Text = DateTime.Now.Month.ToString();
            tbxAno.Text = DateTime.Now.Year.ToString();
            tmr.Start();
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SendKeys.Send("{Tab}");
        }

        private void FormGotFocus(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void FormLostFocus(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DateTime Inicio = DateTime.Now;
            try
            {
                tentativas += 1;

                clsUsuarioInfo usuarioInfo = new clsUsuarioInfo();
                clsUsuarioInfo usuarioInfo2 = new clsUsuarioInfo();
                clsUsuarioBLL usuarioBLL = new clsUsuarioBLL();

                clsCriptografia Criptografia = new clsCriptografia();

                usuarioInfo = usuarioBLL.Carregar(tbxUsuario.Text, conexao);


                if (usuarioInfo.Id == 0 && tbxUsuario.Text == "SUPERVISOR")
                {
                    usuarioInfo = new clsUsuarioInfo();
                    //usuarioInfo.Area = "";
                    usuarioInfo.Ativo = true;
                    //usuarioInfo.Chefedele = "";
                    usuarioInfo.Datault = DateTime.Now;
                    usuarioInfo.Dataval = DateTime.Now.AddYears(3);
                    //usuarioInfo.Depto = "";
                    usuarioInfo.Email = "";
                    usuarioInfo.Emailsenha = "";
                    //usuarioInfo.equivalente = "";
                    usuarioInfo.Gruposystem = "";
                    usuarioInfo.Horafim = DateTime.Parse("01/01/2000 00:00");
                    usuarioInfo.Horaini = DateTime.Parse("01/01/2000 23:59");
                    //usuarioInfo.idchefedele = 0;
                    usuarioInfo.Nivelusuario = "";
                    //usuarioInfo.nomedepto = "";
                    //usuarioInfo.nomefolha = "";
                    // usuarioInfo.secao = "";
                    usuarioInfo.Senha = "";
                    //usuarioInfo.setor = "";
                    usuarioInfo.Trocar = DateTime.Now.AddYears(3);
                    usuarioInfo.Trocasenha = "S";
                    usuarioInfo.Usuario = "SUPERVISOR";
                    usuarioInfo.Weekend = true;

                    usuarioInfo.Id = usuarioBLL.Incluir(usuarioInfo, conexao);
                    //usuarioInfo.idchefedele = usuarioInfo.Id;
                    usuarioBLL.Alterar(usuarioInfo, conexao);
                }
                else
                {
                    clsInfo.zfuncionarioid = usuarioInfo.idfolha;
                }
                if (usuarioInfo.Senha == "")
                {
                    MessageBox.Show("Necessário cadastrar sua Senha.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmSenhaTroca SenhaTroca = new frmSenhaTroca();
                    SenhaTroca.Init(conexao, usuarioInfo.Id);

                    SenhaTroca.ShowDialog();

                    usuarioInfo2 = usuarioBLL.Carregar(tbxUsuario.Text, conexao);
                    if (usuarioInfo.Senha == usuarioInfo2.Senha)
                        this.Close();

                    tentativas -= 1;
                    tbxSenha.Text = "";
                    tbxSenha.Select();
                    tbxSenha.SelectAll();
                    return;
                }

                // Só verifica se não for 'Supervisor'
                if (tbxUsuario.Text != "SUPERVISOR")
                {
                    if (usuarioInfo.Senha != null)
                    {
                        if (Criptografia.Criptografar(tbxSenha.Text).ToUpper() != usuarioInfo.Senha.ToUpper())
                            throw new Exception("Usuário ou Senha não conferem.");

                        // Verifica se Usuário está Ativo
                        if (usuarioInfo.Ativo == false)
                            throw new Exception("Usuário não Ativo no Sistema.");

                        // Se for Fim de Semana(Sábado ou Domingo) verifica se pode ter acesso
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday ||
                            DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                            if (usuarioInfo.Weekend == false)
                                throw new Exception("Seu Horário de Acesso ao sistema não esta permitido.");

                        if (usuarioInfo.Dataval.Subtract(DateTime.Now).Days >= 0 &&
                            usuarioInfo.Dataval.Subtract(DateTime.Now).Days <= 5 &&
                            usuarioInfo.Trocasenha == "S")
                            MessageBox.Show("Faltam " + usuarioInfo.Dataval.Subtract(DateTime.Now).Days.ToString() + " para expirar Validade de acesso.\nContate o Depto de TI para a re-validação.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Verifica se está dentro da Data de Validade
                        if (usuarioInfo.Dataval.Subtract(DateTime.Now).Days < 0)
                            throw new Exception("Data de Validade expirou. Contate Depto de TI.");

                        if (usuarioInfo.Trocar.Subtract(DateTime.Now).Days < 0 &&
                            usuarioInfo.Trocasenha == "S")
                        {
                            MessageBox.Show("Data de Troca/Validade de Senha ultrapassada. Troque sua senha.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            frmSenhaTroca SenhaTroca = new frmSenhaTroca();
                            SenhaTroca.Init(conexao, usuarioInfo.Id);

                            SenhaTroca.ShowDialog();

                            usuarioInfo2 = usuarioBLL.Carregar(tbxUsuario.Text, conexao);
                            if (usuarioInfo.Senha == usuarioInfo2.Senha)
                                this.Close();

                            tentativas -= 1;
                            tbxSenha.Text = "";
                            tbxSenha.Select();
                            tbxSenha.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario não Cadastrado !!!!", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tentativas -= 1;
                        tbxSenha.Text = "";
                        tbxSenha.Select();
                        tbxSenha.SelectAll();
                        return;

                    }

                }
                else
                {
                    if (Criptografia.Criptografar(tbxSenha.Text).ToUpper() != usuarioInfo.Senha.ToUpper())
                        throw new Exception("Usuário ou Senha não conferem.");
                }


                clsInfo.zfilial = clsParser.Int32Parse(tbxEmpresa.Text);
                clsInfo.zusuario = tbxUsuario.Text;
                clsInfo.zusuarioid = usuarioInfo.Id;
                clsInfo.zmes = Int32.Parse(tbxMes.Text);
                clsInfo.zano = Int32.Parse(tbxAno.Text);
                clsInfo.zempresaid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID FROM EMPRESA where CODIGO=" + clsInfo.zfilial + "", "0"));

                if (clsInfo.zempresaid == 0)
                {

                    MessageBox.Show("Cadastre-se com os Dados de Sua Empresa !!! - Inicializando Sistema");

                    IncluirIdTabelas();

                    // Cadastrar diversos Id antes de Incluir uma nova empresa
                    frmEmpresa frmEmpresa = new frmEmpresa();
                    frmEmpresa.Init(0);
                    frmEmpresa.ShowDialog();

                    // clsFormHelper.AbrirForm(this.MdiParent, frmEmpresasVis,conexao);
                    tmr.Enabled = false;
                }
                else
                {
                    // pesquisar a empresa como cliente pegar o zempresaidcliente
                    String CGC = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                        "select CGC FROM EMPRESA where ID=" + clsInfo.zempresaid + " ", "");
                    clsInfo.zempresaclienteid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from CLIENTE where CGC='" + CGC + "' "));
                    if (clsInfo.zempresaclienteid == 0)
                    {
                        MessageBox.Show("Empresa Não cadastrada no Sistema");
                    }
                    clsInfo.zempresacliente_cognome = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                        "select COGNOME FROM EMPRESA where ID=" + clsInfo.zempresaid + " ", "");
                    clsInfo.zempresa_cnpj = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc FROM EMPRESA where ID=" + clsInfo.zempresaid + " ").ToString();
                    clsInfo.zempresa_ufid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDESTADO FROM EMPRESA where ID=" + clsInfo.zempresaid + " ").ToString());
                    clsInfo.zempresa_uf = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                        "select ESTADO from ESTADOS where ID=" + clsInfo.zempresa_ufid + " ", "");
                    //                    clsInfo.pastaUniNFe = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PASTAUNINFE FROM EMPRESA where ID=" + clsInfo.zempresaid + " ", "");
                    //                    clsInfo.configZeus = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONFIGZEUS FROM EMPRESA where ID=" + clsInfo.zempresaid + " ", "");

                    clsInfo.zempresa_cidadeibge = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IBGE FROM EMPRESA where ID=" + clsInfo.zempresaid + " "));

                    // ID DO CLIENTE PROSPECCAO
                    //                    DataTable dtClienteProspeccao = new DataTable();
                    //                    dtClienteProspeccao = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "CLIENTEPROSPECCAO", "ID, COGNOME", "IDCLIENTE = " + clsInfo.zempresaclienteid + " ", "ID");
                    //clsInfo.zempresaclienteprospeccaoid = clsParser.Int32Parse(dtClienteProspeccao.Rows[0]["ID"].ToString());
                    //                    clsInfo.zempresaclienteprospeccaoid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, 
                    //                        "select ID from CLIENTEPROSPECCAO where IDCLIENTE=" + clsInfo.zempresaclienteid + " ", "0").ToString());

                    //                    clsInfo.zempresaclienteprospeccao_cognome = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, 
                    //                        "select ID from CLIENTEPROSPECCAO where IDCLIENTE=" + clsInfo.zempresaclienteid + " ","").ToString();

                    Int32 empresasgere_id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " "));
                    //Parser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "EMPRESASGERE", "ID", "EMPRESA", clsInfo.zempresaid));


                    if (empresasgere_id > 0 && clsInfo.zempresaclienteid > 0)
                    {
                        //    clsInfo.zfiscalirrf = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FISCALIRRF FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalinss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FISCALINSS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FISCALPIS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FISCALCOFINS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalcsll = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FISCALCSLL FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscaliss = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FISCALISS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalpiscofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FISCALPISCOFINS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zleipispasep = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select LEIPISPASEP FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zleicofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select LEICOFINS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalirrfid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODIRRF FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalinssid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODINSS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalpisid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODPIS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalcofinsid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODCOFINS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalcsllid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODCSLL FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalcsllid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODCSLL FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalpiscofinsid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODPISCOFINS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        //    clsInfo.zfiscalissid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCODISS FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ").ToString());
                        clsInfo.zbaixarcomhistorico = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                            "select BAIXARCOMHISTORICO FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ", "").ToString();
                        clsInfo.zbaixatemnotafiscal = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                            "select BAIXATEMNOTAFISCAL FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ", "").ToString();
                        clsInfo.zregimeapuracao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDREGIMEAPURACAO FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ", "0").ToString());
                        //clsInfo.zregimetributario_Simples_Normal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDREGIMETRIBUTARIO FROM EMPRESAGERE where EMPRESA=" + clsInfo.zempresaid + " ", "0").ToString());
                    }


                    // variaveis default de tabelas (ordem alfabetica de tabela
                    clsInfo.zbanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TAB_BANCOS where CODIGO=" + clsParser.Int32Parse("0")));
                    clsInfo.zbancoint = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA=" + clsParser.Int32Parse("0")));
                    clsInfo.zcentrocustos = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + "ADM" + "' "));
                    //clsInfo.zcertificado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CERTIFICADO where CERTIFICADO=" + 0 + " "));
                    clsInfo.zcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CFOP where CFOP='" + "0" + "' "));
                    clsInfo.zclassificacao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA where CODIGO='" + "NC" + "' "));
                    clsInfo.zclassificacao1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA1 where idclassifica= " + clsInfo.zclassificacao + " and CODIGO='" + "NC" + "' "));
                    clsInfo.zclassificacao2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA2 where idclassifica= " + clsInfo.zclassificacao + " and IDCLASSIFICA1=" + clsInfo.zclassificacao1 + " AND CODIGO='" + "NC" + "' "));
                    clsInfo.zcompras = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COMPRAS where NUMERO=" + 0 + " "));
                    clsInfo.zcompras1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COMPRAS1 where IDCOMPRAS=" + clsInfo.zcompras + " "));
                    clsInfo.zcomprasentrega = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COMPRASENTREGA where IDCOMPRAS1=" + clsInfo.zcompras1 + " "));
                    clsInfo.zcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + "0" + "' "));
                    clsInfo.zcontacontabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO='" + "0" + "' "));
                    clsInfo.zcotacao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COTACAO where NUMERO=" + 0 + " "));
                    clsInfo.zcotacao1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COTACAO1 where IDCOTACAO=" + clsInfo.zcotacao + " "));
                    clsInfo.zcotacao2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COTACAO2 where IDCOTACAO1=" + clsInfo.zcotacao1 + " "));
                    clsInfo.zdizeresnf = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DIZERESNFV where CODIGO='" + "000" + "' "));
                    clsInfo.zdocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + "NFE1" + "' "));
                    clsInfo.zformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTIPOTITULO where CODIGO='" + "BO" + "' "));
                    clsInfo.zhistoricos = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO='" + "NC" + "' "));
                    clsInfo.zipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from IPI where CODIGO='" + "0" + "' "));
                    clsInfo.zmarca = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASTIPO where CODIGO='" + "NC" + "' "));
                    clsInfo.znfcompra = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRA where NUMERO=" + 0 + " "));
                    clsInfo.znfcompra1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRA1 where NUMERO=" + clsInfo.znfcompra + " "));
                    clsInfo.znfcomprapagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRAPAGAR where IDNOTA=" + clsInfo.znfcompra + " "));
                    ///clsInfo.znfcomprapagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRAVALES where NUMERO=" + 0 + " "));
                    clsInfo.znfvenda = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFVENDA where NUMERO=" + 0 + " "));
                    clsInfo.znfvenda1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFVENDA1 where NUMERO=" + clsInfo.znfvenda + " "));
                    //if (Application.ProductName.ToString().ToUpper() == "APLITRANSVEICULOS")
                    //{// TRANSPORTE DE VEICULOS
                    //    clsInfo.zctrc = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONHECIMENTO where NUMERO=" + 0 + " "));
                    //    clsInfo.zctrc1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONHECIMENTO1 where IDCONHECIMENTO=" + clsInfo.zctrc + " "));
                    //}
                    //clsInfo.zoperacoes = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from OPERACOES where CODIGO='" + "NDA" + "' "));
                    clsInfo.zorcamento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ORCAMENTO where NUMERO=" + 0 + " "));
                    clsInfo.zorcamento1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ORCAMENTO1 where IDORCAMENTO=" + clsInfo.zorcamento + " "));
                    clsInfo.zordemservico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ORDEMSERVICO where NUMERO=" + 0 + " "));
                    clsInfo.zpecas = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + "0" + "' "));
                    //clsInfo.zpecasfamilia = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASFAMILIA where CODIGO='" + "NC" + "' "));
                    clsInfo.zpedido = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PEDIDO where NUMERO=" + 0 + " "));
                    clsInfo.zpedido1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PEDIDO1 where IDPEDIDO=" + clsInfo.zpedido + " "));
                    clsInfo.zpedido2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PEDIDO2 where IDPEDIDO1=" + clsInfo.zpedido1 + " "));
                    clsInfo.zramo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from RAMO where CODIGO='" + "NC" + "' "));
                    clsInfo.zregimeapuracao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from REGIMEAPURACAO where CODIGO='" + "NC" + "' "));

                    //clsInfo.zregimetributario_Simples_Normal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from REGIMETRIBUTARIO where id = " +
                    //    clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDREGIMETRIBUTARIO FROM EMPRESAgere where empresa = " +  clsInfo.zempresaid , "1")),"1"));

                    //clsInfo.zsituacaocobrancacod = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO='" + "00" + "' "));
                    clsInfo.zsituacaotitulo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTITULO where CODIGO=" + 0 + " "));
                    clsInfo.zsituacaotriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAA where CODIGO='" + "0" + "' ", "0"));
                    //if (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from REGIMETRIBUTARIO where id = " + clsInfo.zregimetributario_Simples_Normal, "3") == "3")
                    //{
                    clsInfo.zsituacaotribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAB where CODIGO='" + "00" + "' ", "00"));
                    //else
                    //{
                    //    clsInfo.zsituacaotribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIbutariab where CODIGO='" + "102" + "' ","0"));
                    //}

                    clsInfo.zsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBIPI where CODIGO='" + "53" + "' "));
                    clsInfo.zsittribpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBPIS where CODIGO='" + "01" + "' "));
                    //clsInfo.zsittribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBCOFINS where CODIGO='" + "01" + "' "));
                    clsInfo.zsolicitacao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SOLICITACAO where NUMERO=" + 0 + " "));
                    //clsInfo.ztiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TIPONOTA where CODIGO='0000-000'"));
                    //clsInfo.ztolerancia = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TOLERANCIA where CODIGO='" + "NC" + "' "));
                    clsInfo.zunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where CODIGO='" + "PC" + "' "));
                    clsInfo.zzona = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ZONAS where CODIGO='" + "NC" + "' "));


                    /////////////////////
                    // Verificar se todos possuem id
                    // Se não possuir - não reinicializar o sistema
                    // Bancos (febraban)


                    bwrDadosPadrao.RunWorkerAsync();

                    // VERIFICA AS TABELAS DE AUDITORIA
                    //                    AuditoriaDados.MontaTabelas();

                    // 
                    DateTime final = DateTime.Now;
                    TimeSpan tempo;
                    tempo = final.Subtract(Inicio);

                    //MessageBox.Show("Tempo Ocorrido :'" + tempo.TotalMinutes + "' ");

                    resultado = true;
                    tmr.Stop();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxUsuario.Select();
                tbxUsuario.SelectAll();
                if (tentativas > 3)
                {
                    this.Close();
                    MessageBox.Show("Número de tentativas acabado.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            resultado = false;
            this.Close();
        }

        private void btnEmpresaMais_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxEmpresa.Text == "" || Int32.Parse(tbxEmpresa.Text) < 1)
                    tbxEmpresa.Text = "1";
                tbxEmpresa.Text = (Int32.Parse(tbxEmpresa.Text) + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft");
            }
        }

        private void btnEmpresaMenos_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxEmpresa.Text == "" || Int32.Parse(tbxEmpresa.Text) < 1)
                    tbxEmpresa.Text = "1";
                tbxEmpresa.Text = (Int32.Parse(tbxEmpresa.Text) - 1).ToString();
                if (tbxEmpresa.Text == "" || Int32.Parse(tbxEmpresa.Text) < 1)
                    tbxEmpresa.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft");
            }
        }

        private void btnMesMais_Click(object sender, EventArgs e)
        {
            try
            {
                tbxMes.Text = (Int32.Parse(tbxMes.Text) + 1).ToString();
                if (tbxMes.Text == "" || Int32.Parse(tbxMes.Text) < 1 || Int32.Parse(tbxMes.Text) > 12)
                    tbxMes.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft");
            }
        }

        private void btnMesMenos_Click(object sender, EventArgs e)
        {
            try
            {
                tbxMes.Text = (Int32.Parse(tbxMes.Text) - 1).ToString();
                if (tbxMes.Text == "" || Int32.Parse(tbxMes.Text) < 1)
                    tbxMes.Text = "12";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft");
            }
        }

        private void btnAnoMais_Click(object sender, EventArgs e)
        {
            try
            {
                tbxAno.Text = (Int32.Parse(tbxAno.Text) + 1).ToString();
                if (tbxAno.Text == "" || Int32.Parse(tbxAno.Text) < 1)
                    tbxAno.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft");
            }
        }

        private void btnAnoMenos_Click(object sender, EventArgs e)
        {
            try
            {
                tbxAno.Text = (Int32.Parse(tbxAno.Text) - 1).ToString();
                if (tbxAno.Text == "" || Int32.Parse(tbxAno.Text) < 1)
                    tbxAno.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft");
            }
        }

        private void frmSenha_Activated(object sender, EventArgs e)
        {
            Lupa();
            tmr.Enabled = true;
        }

        private void Lupa()
        {
            if (clsInfo.zrow != null && clsInfo.zrow.Index != -1)
                if (clsInfo.znomegrid == "dgvEmpresas")
                    btnOk.PerformClick();
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            if (pbr.Value + 100 <= 60000)
                pbr.Value = pbr.Value + 100;
            else
                btnCancelar.PerformClick();
        }

        private void frmSenha_Shown(object sender, EventArgs e)
        {

        }

        private void bwrDadosPadrao_DoWork(object sender, DoWorkEventArgs e)
        {
            //clsInfo.rdtDocumento = new ReportDocument();
            //
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = clsInfo.conexaosqldados;
            conn.Open();

            SqlDataAdapter sdar = new SqlDataAdapter();
            sdar.SelectCommand = new SqlCommand("SELECT @@langid, @@language", conn);
            clsInfo.sqlLanguage = new DataTable();
            sdar.Fill(clsInfo.sqlLanguage);

            conn.Close();
        }

        private void frmSenha_Deactivate(object sender, EventArgs e)
        {
            tmr.Enabled = false;
        }
        private void IncluirIdTabelas()
        {
            // variaveis default de tabelas (ordem alfabetica de tabela
            clsInfo.zbanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TAB_BANCOS where CODIGO=" + clsParser.Int32Parse("0")));
            clsInfo.zbancoint = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA=" + clsParser.Int32Parse("0")));
            clsInfo.zcentrocustos = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + "ADM" + "' "));
            clsInfo.zcertificado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CERTIFICADO where CERTIFICADO=" + 0 + " "));
            clsInfo.zcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CFOP where CFOP='" + "0" + "' "));
            clsInfo.zclassificacao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA where CODIGO='" + "NC" + "' "));
            clsInfo.zclassificacao1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA1 where idclassifica= " + clsInfo.zclassificacao + " and CODIGO='" + "NC" + "' "));
            clsInfo.zclassificacao2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA2 where idclassifica= " + clsInfo.zclassificacao + " and IDCLASSIFICA1=" + clsInfo.zclassificacao1 + " AND CODIGO='" + "NC" + "' "));
            clsInfo.zcompras = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COMPRAS where NUMERO=" + 0 + " "));
            clsInfo.zcompras1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COMPRAS1 where IDCOMPRAS=" + clsInfo.zcompras + " "));
            clsInfo.zcomprasentrega = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COMPRASENTREGA where IDCOMPRAS1=" + clsInfo.zcompras1 + " "));
            clsInfo.zcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONDPAGTO where CODIGO='" + "0" + "' "));
            clsInfo.zcontacontabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO='" + "0" + "' "));
            clsInfo.zcotacao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COTACAO where NUMERO=" + 0 + " "));
            clsInfo.zcotacao1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COTACAO1 where IDCOTACAO=" + clsInfo.zcotacao + " "));
            clsInfo.zcotacao2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from COTACAO2 where IDCOTACAO1=" + clsInfo.zcotacao1 + " "));
            clsInfo.zdizeresnf = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DIZERESNFV where CODIGO='" + "000" + "' "));
            clsInfo.zdocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + "NFE1" + "' "));
            clsInfo.zformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTIPOTITULO where CODIGO='" + "BO" + "' "));
            clsInfo.zhistoricos = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO='" + "NC" + "' "));
            clsInfo.zipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from IPI where CODIGO='" + "0" + "' "));
            clsInfo.znfcompra = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRA where NUMERO=" + 0 + " "));
            clsInfo.znfcompra1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRA1 where NUMERO=" + clsInfo.znfcompra + " "));
            clsInfo.znfcomprapagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRAPAGAR where IDNOTA=" + clsInfo.znfcompra + " "));
            clsInfo.znfcompravales = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRAVALES where NUMERO=" + 0 + " "));
            clsInfo.znfvenda = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFVENDA where NUMERO=" + 0 + " "));
            clsInfo.znfvenda1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFVENDA1 where NUMERO=" + clsInfo.znfvenda + " "));
            //if (Application.ProductName.ToString().ToUpper() == "APLITRANSVEICULOS")
            //{// TRANSPORTE DE VEICULOS
            //    clsInfo.zctrc = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONHECIMENTO where NUMERO=" + 0 + " "));
            //    clsInfo.zctrc1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONHECIMENTO1 where IDCONHECIMENTO=" + clsInfo.zctrc + " "));
            //}

            clsInfo.zoperacoes = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from OPERACOES where CODIGO='" + "NDA" + "' "));
            clsInfo.zorcamento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ORCAMENTO where NUMERO=" + 0 + " "));
            clsInfo.zorcamento1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ORCAMENTO1 where IDORCAMENTO=" + clsInfo.zorcamento + " "));
            clsInfo.zordemservico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ORDEMSERVICO where NUMERO=" + 0 + " "));
            clsInfo.zpecas = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + "0" + "' "));
            clsInfo.zpecasfamilia = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASFAMILIA where CODIGO='" + "NC" + "' "));
            clsInfo.zpedido = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PEDIDO where NUMERO=" + 0 + " "));
            clsInfo.zpedido1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PEDIDO1 where IDPEDIDO=" + clsInfo.zpedido + " "));
            clsInfo.zpedido2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PEDIDO2 where IDPEDIDO1=" + clsInfo.zpedido1 + " "));
            clsInfo.zramo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from RAMO where CODIGO='" + "NC" + "' "));
            clsInfo.zregimeapuracao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from REGIMEAPURACAO where CODIGO='" + "NC" + "' "));
            clsInfo.zsituacaocobrancacod = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO='" + "00" + "' "));
            clsInfo.zsituacaotitulo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTITULO where CODIGO=" + 0 + " "));
            clsInfo.zsituacaotriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAA where CODIGO='" + "0" + "' "));
            clsInfo.zsituacaotribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIbutariab where CODIGO='" + "00" + "' "));

            clsInfo.zsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBIPI where CODIGO='" + "53" + "' "));
            clsInfo.zsittribpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBPIS where CODIGO='" + "01" + "' "));
            clsInfo.zsittribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBCOFINS where CODIGO='" + "01" + "' "));

            clsInfo.zsolicitacao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SOLICITACAO where NUMERO=" + 0 + " "));
            clsInfo.zunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where CODIGO='" + "PC" + "' "));
            clsInfo.ztiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TIPONOTA where CODIGO='0000-000'"));
            clsInfo.ztolerancia = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TOLERANCIA where CODIGO='" + "NC" + "' "));
            clsInfo.zzona = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ZONAS where CODIGO='" + "NC" + "' "));


            // Incluir os Id Default e outros
            scn = new SqlConnection(clsInfo.conexaosqldados);
            if (clsInfo.zramo == 0)
            {
                //clsInfo.zramo = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "RAMO", "CODIGO, NOME, PADRAO", "'NC','NADA CONSTA', 'S'");
                query = "insert into ramo (CODIGO, NOME, PADRAO) VALUES (@CODIGO, @NOME, @PADRAO) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@CODIGO", SqlDbType.NVarChar).Value = "NC";
                scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = "NADA CONSTA";
                scd.Parameters.Add("@PADRAO", SqlDbType.NVarChar).Value = "S";
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            Int32 idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ESTADOS where ESTADO='" + "SP" + "' "));
            Int32 nRegistros = 0;
            if (idestado == 0)
            {  //Estados da Federação
               //  Carregar a tabela estados dentro de um listbox
                str = File.ReadAllLines(clsInfo.arquivos + "\\Estados.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino ESTADOS
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;
                    campo = "";

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }
                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsEstadosInfo = new clsEstadosInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:  //  estado
                                    clsEstadosInfo.estado = campo; // linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2: // ZONAFRANCA
                                    clsEstadosInfo.zonafranca = campo; // linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3: //  ALIQUOTA
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsEstadosInfo.aliquota = clsParser.DecimalParse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 4:  // NOMEEXT
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsEstadosInfo.nomeext = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 5:   //CAPITAL
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsEstadosInfo.capital = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 6:  // INICEP
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsEstadosInfo.inicep = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 7:  // FIMCEP
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsEstadosInfo.fimcep = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 8:  // REGIAO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsEstadosInfo.regiao = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 9:  // IBGE
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsEstadosInfo.ibge = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;

                            }
                        }
                    }
                    //        // 
                    clsEstadosInfo clsEstadosInfo1 = new clsEstadosInfo();
                    clsEstadosInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + clsEstadosInfo.estado + "' "));
                    clsEstadosInfo1 = clsEstadosBLL.Carregar(clsEstadosInfo1.id, clsInfo.conexaosqldados);
                    if (clsEstadosInfo1 == null)
                    {
                        clsEstadosInfo1 = new clsEstadosInfo();
                    }
                    if (clsEstadosInfo1.aliquota == 0 || clsEstadosInfo1.estado == clsEstadosInfo.estado)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsEstadosInfo1.aliquota == 0)
                        { // incluir
                            scd = new SqlCommand("INSERT INTO ESTADOS (" +
                                    "ESTADO, ZONAFRANCA, ALIQUOTA, NOMEEXT, CAPITAL, INICEP, FIMCEP, REGIAO, IBGE, IEST" +
                                    ") VALUES ( " +
                                    "@ESTADO, @ZONAFRANCA, @ALIQUOTA, @NOMEEXT, @CAPITAL, @INICEP, @FIMCEP, @REGIAO, @IBGE, @IEST);" +
                                    "SELECT SCOPE_IDENTITY()", scn);
                        }
                        else
                        { // alterar
                            scd = new SqlCommand("UPDATE ESTADOS SET " +
                                                "ESTADO=@ESTADO, ZONAFRANCA=@ZONAFRANCA, ALIQUOTA=@ALIQUOTA, NOMEEXT=@NOMEEXT, " +
                                                "CAPITAL=@CAPITAL, INICEP=@INICEP, FIMCEP=@FIMCEP, REGIAO=@REGIAO, IBGE=@IBGE," +
                                                "IEST=@IEST " +
                                                "WHERE ID = @ID", scn);
                            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsEstadosInfo1.id;
                        }
                        scd.Parameters.AddWithValue("@ESTADO", SqlDbType.NVarChar).Value = clsEstadosInfo.estado;
                        scd.Parameters.AddWithValue("@ZONAFRANCA", SqlDbType.NVarChar).Value = clsEstadosInfo.zonafranca;
                        scd.Parameters.AddWithValue("@ALIQUOTA", SqlDbType.Decimal).Value = clsEstadosInfo.aliquota;
                        scd.Parameters.AddWithValue("@NOMEEXT", SqlDbType.NVarChar).Value = clsEstadosInfo.nomeext;
                        scd.Parameters.AddWithValue("@CAPITAL", SqlDbType.NVarChar).Value = clsEstadosInfo.capital;
                        scd.Parameters.AddWithValue("@INICEP", SqlDbType.NVarChar).Value = clsEstadosInfo.inicep;
                        scd.Parameters.AddWithValue("@FIMCEP", SqlDbType.NVarChar).Value = clsEstadosInfo.fimcep;
                        scd.Parameters.AddWithValue("@REGIAO", SqlDbType.NVarChar).Value = clsEstadosInfo.regiao;
                        scd.Parameters.AddWithValue("@IBGE", SqlDbType.NVarChar).Value = clsEstadosInfo.ibge;
                        scd.Parameters.AddWithValue("@IEST", SqlDbType.NVarChar).Value = "N";
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                    }
                }

                //    // icms destino  [ESTADOSICMS]
                //    str = File.ReadAllLines(clsInfo.arquivos + "\\estadosicms.txt");
                //    if (str.LongCount() > 0)
                //    {
                //        nRegistros = (Int32)str.LongCount();
                //    }
                //    if (nRegistros > 0)
                //    {
                //        lbxOrigem.Items.Clear();
                //        foreach (String linha in str)
                //        {
                //            lbxOrigem.Items.Add(linha);
                //        }
                //    }
                //    // Descarregando a tabela dentro do arquivo de destino ESTADOS
                //    foreach (String linha in lbxOrigem.Items)
                //    {
                //        qtcaracter = linha.ToString().Length;
                //        posicaoatual = 0;
                //        posicaoinicial = 0;
                //        posicaofinal = 0;

                //        ok = false;
                //        for (Int32 i = 1; i < qtcaracter; i++)
                //        {
                //            if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                //            {
                //                posicaoatual++;
                //                posicaofinal = i;
                //                if (i == (qtcaracter - 1))
                //                {
                //                    posicaofinal = i + 1;
                //                }
                //                ok = true;
                //            }
                //            else
                //            {
                //                ok = false;
                //            }

                //            if (ok == true)
                //            {
                //                campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                //                // posicaoinicial = (posicaofinal + 1);
                //                if (posicaoinicial == 0)
                //                {
                //                    clsEstadosicmsInfo = new clsEstadosicmsInfo();
                //                }
                //                switch (posicaoatual)
                //                {
                //                    case 1:   // idestado
                //                        campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                //                        clsEstadosicmsInfo.idestado = clsParser.Int32Parse(campo);
                //                        posicaoinicial = (posicaofinal + 1);
                //                        break;
                //                    case 2:  // id estado destino
                //                        campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                //                        clsEstadosicmsInfo.idestadodestino = clsParser.Int32Parse(campo);
                //                        posicaoinicial = (posicaofinal + 1);
                //                        break;
                //                    case 3:  // ALIQUOTA
                //                        campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                //                        clsEstadosicmsInfo.aliquota = clsParser.DecimalParse(campo);
                //                        posicaoinicial = (posicaofinal + 1);
                //                        break;
                //                    case 4:  // iva
                //                        campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                //                        clsEstadosicmsInfo.iva = clsParser.DecimalParse(campo);
                //                        posicaoinicial = (posicaofinal + 1);
                //                        break;
                //                    case 5:  // iddizeresnf
                //                        campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                //                        posicaoinicial = (posicaofinal + 1);
                //                        break;
                //                }
                //            }
                //        }
                //        // 
                //        clsEstadosicmsInfo clsEstadosicmsInfo1 = new clsEstadosicmsInfo();
                //        clsEstadosicmsInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estadosicms where idestado=" + clsEstadosicmsInfo.idestado + " and idestadodestino=" + clsEstadosicmsInfo.idestadodestino + " "));
                //        clsEstadosicmsInfo1 = clsEstadosicmsBLL.Carregar(clsEstadosicmsInfo1.id, clsInfo.conexaosqldados);
                //        if (clsEstadosicmsInfo1 == null)
                //        {
                //            clsEstadosicmsInfo1 = new clsEstadosicmsInfo();
                //        }
                //        if (clsEstadosicmsInfo1.aliquota == 0 || clsEstadosicmsInfo1.idestado == clsEstadosicmsInfo.idestado && clsEstadosicmsInfo1.idestadodestino == clsEstadosicmsInfo.idestadodestino)
                //        {
                //            scn = new SqlConnection(clsInfo.conexaosqldados);
                //            if (clsEstadosicmsInfo1.aliquota == 0)
                //            { // incluir
                //                scd = new SqlCommand("INSERT INTO ESTADOSICMS (" +
                //                        "IDESTADO, IDESTADODESTINO, ALIQUOTA, IVA " +
                //                        ") VALUES ( " +
                //                        "@IDESTADO, @IDESTADODESTINO, @ALIQUOTA, @IVA   );" +
                //                        "SELECT SCOPE_IDENTITY()", scn);
                //            }
                //            else
                //            { // alterar
                //                scd = new SqlCommand("UPDATE ESTADOSICMS SET " +
                //                                    "IDESTADO=@IDESTADO, IDESTADODESTINO=@IDESTADODESTINO, ALIQUOTA=@ALIQUOTA, IVA=@IVA " +
                //                                    "WHERE ID = @ID", scn);
                //                scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsEstadosicmsInfo1.id;
                //            }
                //            scd.Parameters.AddWithValue("@IDESTADO", SqlDbType.Int).Value = clsEstadosicmsInfo.idestado;
                //            scd.Parameters.AddWithValue("@IDESTADODESTINO", SqlDbType.Int).Value = clsEstadosicmsInfo.idestadodestino;
                //            scd.Parameters.AddWithValue("@ALIQUOTA", SqlDbType.Decimal).Value = clsEstadosicmsInfo.aliquota;
                //            scd.Parameters.AddWithValue("@IVA", SqlDbType.Decimal).Value = clsEstadosicmsInfo.iva;
                //            scn.Open();
                //            scd.ExecuteNonQuery();
                //            scn.Close();
                //        }
            }
            //    // Bancos (febraban)  * TAB_BANCOS
            if (clsInfo.zbanco == 0)
            {
                clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
                clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();

                str = File.ReadAllLines(clsInfo.arquivos + "\\tab_bancos.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsTab_BancosInfo = new clsTab_BancosInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:  // codigo
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_BancosInfo.codigo = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // cognome
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_BancosInfo.cognome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // ativo
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_BancosInfo.ativo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 4:   //nome
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_BancosInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsTab_BancosInfo clsTab_BancosInfo1 = new clsTab_BancosInfo();
                    clsTab_BancosInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tab_bancos where codigo=" + clsTab_BancosInfo.codigo + " "));
                    clsTab_BancosInfo1 = clsTab_BancosBLL.Carregar(clsTab_BancosInfo1.id, clsInfo.conexaosqldados);
                    if (clsTab_BancosInfo1 == null)
                    {
                        clsTab_BancosInfo1 = new clsTab_BancosInfo();
                    }
                    if (clsTab_BancosInfo1.codigo == 0 || clsTab_BancosInfo1.codigo == clsTab_BancosInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsTab_BancosInfo1.codigo == 0)
                        { // incluir
                            scd = new SqlCommand("INSERT INTO TAB_BANCOS (" +
                                    "CODIGO, COGNOME, ATIVO, NOME " +
                                    ") VALUES ( " +
                                    "@CODIGO, @COGNOME, @ATIVO, @NOME   );" +
                                    "SELECT SCOPE_IDENTITY()", scn);
                        }
                        else
                        { // alterar
                            scd = new SqlCommand("UPDATE TAB_BANCOS SET " +
                                                "CODIGO=@CODIGO, COGNOME=@COGNOME, ATIVO=@ATIVO, NOME=@NOME  " +
                                                "WHERE ID = @ID", scn);
                            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsTab_BancosInfo1.id;
                        }
                        scd.Parameters.AddWithValue("@CODIGO", SqlDbType.Int).Value = clsTab_BancosInfo.codigo;
                        scd.Parameters.AddWithValue("@" +
                            "COGNOME", SqlDbType.Int).Value = clsTab_BancosInfo.cognome;
                        scd.Parameters.AddWithValue("@ATIVO", SqlDbType.Decimal).Value = clsTab_BancosInfo.ativo;
                        scd.Parameters.AddWithValue("@NOME", SqlDbType.Decimal).Value = clsTab_BancosInfo.nome;
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                    }
                }
            }
            //// Bancos (aplibank)
            if (clsInfo.zbancoint == 0)
            {
                clsBancosInfo clsBancosInfo = new clsBancosInfo();
                clsBancosBLL clsBancosBLL = new clsBancosBLL();
                clsBancosInfo = new clsBancosInfo();
                clsBancosInfo.agencia = "CAIXA INTERNO";
                clsBancosInfo.ativo = "S";
                clsBancosInfo.banco = 0;
                clsBancosInfo.bcoaceite = "";
                clsBancosInfo.bcoespecie = "";
                clsBancosInfo.bcoimpressao = "";
                clsBancosInfo.bcomodalidade = "";
                clsBancosInfo.bcomoeda = "R";
                clsBancosInfo.bcoprotestar = "";
                clsBancosInfo.carteira = "";
                clsBancosInfo.compensar = 0;
                clsBancosInfo.conta = 0;
                clsBancosInfo.contabil = "";
                clsBancosInfo.contacor = "";
                clsBancosInfo.convenio = "";
                clsBancosInfo.ctatransitoria = "N";
                clsBancosInfo.id = 0;
                clsBancosInfo.idbanco = 1;
                clsBancosInfo.idbcoaceite = 0;
                clsBancosInfo.idbcoimpressao = 0;
                clsBancosInfo.idbcomodalidade = 0;
                clsBancosInfo.idbcomoeda = 0;
                clsBancosInfo.idbcoprotestar = 0;
                clsBancosInfo.idbcoprotestar2 = 0;
                clsBancosInfo.idcontabil = 0;
                clsBancosInfo.layoutposicoes = "";
                clsBancosInfo.limite = 0;
                clsBancosInfo.nome = "CAIXA INTERNO";
                clsBancosInfo.nroboleto = "0";
                clsBancosInfo.subcarteira = "";
                clsBancosInfo.txboleto = 0;
                clsBancosInfo.txcartorio = 0;
                clsBancosInfo.variacao = "0";
                clsBancosBLL.Incluir(clsBancosInfo, clsInfo.conexaosqlbanco);
            }
            // Centro de Custos
            if (clsInfo.zcentrocustos == 0)
            {
                clsCentrocustosInfo clsCentrocustosInfo = new clsCentrocustosInfo();
                clsCentrocustosBLL clsCentrocustosBLL = new clsCentrocustosBLL();
                clsCentrocustosInfo = new clsCentrocustosInfo();
                clsCentrocustosInfo.ativo = "S";
                clsCentrocustosInfo.codigo = "ADM";
                clsCentrocustosInfo.data = "01/01/1900";
                clsCentrocustosInfo.nome = "ADMINISTRACAO";
                clsCentrocustosBLL.Incluir(clsCentrocustosInfo, clsInfo.conexaosqlbanco);
            }
            if (clsInfo.zhistoricos == 0)
            {
                clsHistoricosInfo clsHistoricosInfo = new clsHistoricosInfo();
                clsHistoricosBLL clsHistoricosBLL = new clsHistoricosBLL();
                clsHistoricosInfo = new clsHistoricosInfo();
                clsHistoricosInfo.ativo = "S";
                clsHistoricosInfo.cobranca = "";
                clsHistoricosInfo.codigo = "NC";
                clsHistoricosInfo.contabil = "";
                clsHistoricosInfo.idcobranca = 0;
                clsHistoricosInfo.idcontabil = 0;
                clsHistoricosInfo.nivel = "";
                clsHistoricosInfo.nome = "NADACONSTA";
                clsHistoricosInfo.tipo = "D";
                clsHistoricosBLL.Incluir(clsHistoricosInfo, clsInfo.conexaosqlbanco);
            }
            if (clsInfo.zclassificacao == 0)
            {
                clsPecasclassificaInfo clsPecasclassificaInfo = new clsPecasclassificaInfo();
                clsPecasclassificaBLL clsPecasclassificaBLL = new clsPecasclassificaBLL();
                clsPecasclassificaInfo = new clsPecasclassificaInfo();
                clsPecasclassificaInfo.codigo = "NC";
                clsPecasclassificaInfo.nome = "NADA CONSTA";
                clsPecasclassificaBLL.Incluir(clsPecasclassificaInfo, clsInfo.conexaosqldados);

                clsPecasclassifica1Info clsPecasclassifica1Info = new clsPecasclassifica1Info();
                clsPecasclassifica1BLL clsPecasclassifica1BLL = new clsPecasclassifica1BLL();
                clsPecasclassifica1Info = new clsPecasclassifica1Info();
                clsPecasclassifica1Info.idclassifica = 1;
                clsPecasclassifica1Info.codigo = "NC";
                clsPecasclassifica1Info.nome = "NADA CONSTA";
                clsPecasclassifica1BLL.Incluir(clsPecasclassifica1Info, clsInfo.conexaosqldados);

                clsPecasclassifica2Info clsPecasclassifica2Info = new clsPecasclassifica2Info();
                clsPecasclassifica2BLL clsPecasclassifica2BLL = new clsPecasclassifica2BLL();
                clsPecasclassifica2Info = new clsPecasclassifica2Info();
                clsPecasclassifica2Info.idclassifica = 1;
                clsPecasclassifica2Info.idclassifica1 = 1;
                clsPecasclassifica2Info.codigo = "NC";
                clsPecasclassifica2Info.nome = "NADA CONSTA";
                clsPecasclassifica2BLL.Incluir(clsPecasclassifica2Info, clsInfo.conexaosqldados);

            }
            if (clsInfo.zcontacontabil == 0)
            {
                clsContacontabilInfo clsContacontabilInfo = new clsContacontabilInfo();
                clsContacontabilBLL clsContacontabilBLL = new clsContacontabilBLL();
                clsContacontabilInfo.ativo = "S";
                clsContacontabilInfo.codigo = "0";
                clsContacontabilInfo.nivel = 0;
                clsContacontabilInfo.nome = "NADA CONSTA";
                clsContacontabilInfo.reduzido = 0;
                clsContacontabilInfo.tipo = "A";
                clsContacontabilInfo.verfabrica = "";
                clsContacontabilBLL.Incluir(clsContacontabilInfo, clsInfo.conexaosqldados);
            }
            if (clsInfo.zcondpagto == 0)
            {
                clsCondpagtoInfo clsCondpagtoInfo = new clsCondpagtoInfo();
                clsCondpagtoBLL clsCondpagtoBLL = new clsCondpagtoBLL();
                clsCondpagtoInfo.cai1 = 0;
                clsCondpagtoInfo.cai2 = 0;
                clsCondpagtoInfo.cai3 = 0;
                clsCondpagtoInfo.cai4 = 0;
                clsCondpagtoInfo.codigo = "0";
                clsCondpagtoInfo.codigodespfinanc = "";
                clsCondpagtoInfo.dadata = "E";
                clsCondpagtoInfo.despfinanc = "";
                clsCondpagtoInfo.dia1 = 0;
                clsCondpagtoInfo.dia2 = 0;
                clsCondpagtoInfo.dia3 = 0;
                clsCondpagtoInfo.dia4 = 0;
                clsCondpagtoInfo.dia5 = 0;
                clsCondpagtoInfo.dia6 = 0;
                clsCondpagtoInfo.dia7 = 0;
                clsCondpagtoInfo.dia8 = 0;
                clsCondpagtoInfo.dia9 = 0;
                clsCondpagtoInfo.dia10 = 0;
                clsCondpagtoInfo.ipi = "";
                clsCondpagtoInfo.juros = 0;
                clsCondpagtoInfo.nome = "A Vista";
                clsCondpagtoInfo.padrao = "";
                clsCondpagtoInfo.parcelas = 0;
                clsCondpagtoInfo.por1 = 0;
                clsCondpagtoInfo.por2 = 0;
                clsCondpagtoInfo.por3 = 0;
                clsCondpagtoInfo.por4 = 0;
                clsCondpagtoInfo.por5 = 0;
                clsCondpagtoInfo.por6 = 0;
                clsCondpagtoInfo.por7 = 0;
                clsCondpagtoInfo.por8 = 0;
                clsCondpagtoInfo.por9 = 0;
                clsCondpagtoInfo.por10 = 0;
                clsCondpagtoInfo.qtdedias = 0;
                clsCondpagtoInfo.semana = "QDA";
                clsCondpagtoInfo.st = "";
                clsCondpagtoInfo.totaldias = 0;
                clsCondpagtoBLL.Incluir(clsCondpagtoInfo, clsInfo.conexaosqldados);
            }
            if (clsInfo.zcfop == 0)
            {
                clsCfopInfo clsCfopInfo = new clsCfopInfo();
                clsCfopBLL clsCfopBLL = new clsCfopBLL();

                str = File.ReadAllLines(clsInfo.arquivos + "\\CFOP.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {

                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsCfopInfo = new clsCfopInfo();
                            }
                            switch (posicaoatual)
                            {//0|NAO DETERMINADO|NAO DETERMINADO|E|1|1|
                                case 1:  //  cfop
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsCfopInfo.cfop = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // nome nota
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsCfopInfo.nomenota = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // nome completo
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsCfopInfo.dizer = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 4:  // tipo
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsCfopInfo.tipo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 5:  // id cta cred
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsCfopInfo.idcontacre = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 6:  // id cta deb
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsCfopInfo.idcontadeb = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;

                            }
                        }
                    }
                    clsCfopInfo.cfopunico = "";
                    clsCfopInfo.ativo = "S";
                    if (clsCfopInfo.idcontacre == 0)
                    {
                        clsCfopInfo.idcontacre = 1;
                    }
                    if (clsCfopInfo.idcontadeb == 0)
                    {
                        clsCfopInfo.idcontadeb = 1;
                    }

                    //clsCfopInfo.dizer = "";
                    // 
                    clsCfopInfo clsCfopInfo1 = new clsCfopInfo();
                    clsCfopInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cfop where cfop=" + clsCfopInfo.cfop + " "));
                    clsCfopInfo1 = clsCfopBLL.Carregar(clsCfopInfo1.id, clsInfo.conexaosqldados);
                    if (clsCfopInfo1.cfop == null)
                    {
                        clsCfopInfo1 = new clsCfopInfo();
                    }
                    if (clsCfopInfo1.cfop == null || clsCfopInfo1.cfop == clsCfopInfo.cfop)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsCfopInfo1.cfop == null)
                        { // incluir
                            clsCfopBLL.Incluir(clsCfopInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsCfopBLL.Alterar(clsCfopInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zdizeresnf == 0)
            {
                clsDizeresnfvInfo clsDizeresnfvInfo = new clsDizeresnfvInfo();
                clsDizeresnfvBLL clsDizeresnfvBLL = new clsDizeresnfvBLL();
                clsDizeresnfvInfo.codigo = "000";
                clsDizeresnfvInfo.nome = "NADA CONSTA";
                clsDizeresnfvInfo.lin01 = "";
                clsDizeresnfvInfo.lin02 = "";
                clsDizeresnfvInfo.lin03 = "";
                clsDizeresnfvInfo.lin04 = "";
                clsDizeresnfvInfo.lin05 = "";
                clsDizeresnfvInfo.lin06 = "";
                clsDizeresnfvBLL.Incluir(clsDizeresnfvInfo, clsInfo.conexaosqldados);
                clsInfo.zdizeresnf = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DIZERESNFV where CODIGO='" + "000" + "' "));

            }
            clsDocFiscalInfo clsDocFiscalInfo = new clsDocFiscalInfo();
            clsDocFiscalBLL clsDocFiscalBLL = new clsDocFiscalBLL();

            if (clsInfo.zdocumento == 0)
            {

                lbxOrigem.Items.Clear();
                str = File.ReadAllLines(clsInfo.arquivos + "\\DOCFISCAL.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))    //  separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsDocFiscalInfo = new clsDocFiscalInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsDocFiscalInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // COGNOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsDocFiscalInfo.cognome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // nome completo
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsDocFiscalInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 4:  // SERIE
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsDocFiscalInfo.serie = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 5:  // aTIVO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsDocFiscalInfo.ativo = "";
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsDocFiscalInfo clsDocFiscalInfo1 = new clsDocFiscalInfo();
                    clsDocFiscalInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from DOCFISCAL where cognome='" + clsDocFiscalInfo.cognome + "' "));
                    clsDocFiscalInfo1 = clsDocFiscalBLL.Carregar(clsDocFiscalInfo1.id, clsInfo.conexaosqldados);
                    if (clsDocFiscalInfo1.cognome == null)
                    {
                        clsDocFiscalInfo1 = new clsDocFiscalInfo();
                    }
                    if (clsDocFiscalInfo1.cognome == null || clsDocFiscalInfo1.cognome == clsDocFiscalInfo.cognome)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsDocFiscalInfo1.cognome == null)
                        { // incluir
                            clsDocFiscalBLL.Incluir(clsDocFiscalInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsDocFiscalBLL.Alterar(clsDocFiscalInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zformapagto == 0)
            { // SituacaoTipoTitulo
                clsSituacaotipotituloInfo clsSituacaotipotituloInfo = new clsSituacaotipotituloInfo();
                clsSituacaotipotituloBLL clsSituacaotipotituloBLL = new clsSituacaotipotituloBLL();

                str = File.ReadAllLines(clsInfo.arquivos + "\\SITUACAOTIPOTITULO.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                lbxOrigem.Items.Clear();
                if (nRegistros > 0)
                {
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSituacaotipotituloInfo = new clsSituacaotipotituloInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaotipotituloInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsSituacaotipotituloInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSituacaotipotituloInfo clsSituacaotipotituloInfo1 = new clsSituacaotipotituloInfo();
                    clsSituacaotipotituloInfo.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo='" + clsSituacaotipotituloInfo.codigo + "' "));
                    clsSituacaotipotituloInfo1 = clsSituacaotipotituloBLL.Carregar(clsSituacaotipotituloInfo.id, clsInfo.conexaosqldados);
                    if (clsSituacaotipotituloInfo1.codigo == null)
                    {
                        clsSituacaotipotituloInfo1 = new clsSituacaotipotituloInfo();
                    }
                    if (clsSituacaotipotituloInfo1.codigo == null || clsSituacaotipotituloInfo1.codigo == clsSituacaotipotituloInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSituacaotipotituloInfo1.codigo == null)
                        { // incluir
                            clsSituacaotipotituloBLL.Incluir(clsSituacaotipotituloInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSituacaotipotituloBLL.Alterar(clsSituacaotipotituloInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zoperacoes == 0)
            {
                clsOperacoesInfo clsOperacoesInfo = new clsOperacoesInfo();
                clsOperacoesBLL clsOperacoesBLL = new clsOperacoesBLL();
                clsOperacoesInfo.codigo = "NDA";
                clsOperacoesInfo.nome = "NADA CONSTA";
                clsOperacoesInfo.ativo = "S";
                clsOperacoesBLL.Incluir(clsOperacoesInfo, clsInfo.conexaosqldados);
            }

            if (clsInfo.zsituacaocobrancacod == 0)
            {
                clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();
                clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
                str = File.ReadAllLines(clsInfo.arquivos + "\\SITUACAOCOBRANCACOD.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // COGNOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // idhistorico
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.idhistbco = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 4:  // historico bco
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.histbco = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 5:  // id historico pagar
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.idhistoricopagar = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 6:  // historico pagar
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.historicopagar = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 7:  // id centrocusto pagar
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.idcentrocustopagar = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                //este de biaxo tambem estava como 7 so acertei
                                case 8:  // Centrocusto pagar
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.centrocustopagar = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 9:  // id HISTORICO RECEBER
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.idhistoricoreceber = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                // "IDCENTROCUSTORECEBER, CENTROCUSTORECEBER"
                                case 10:   //Centrocusto pagar
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.historicoreceber = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 11:  // ID CENTROCUSTO RECEBER
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.idcentrocustoreceber = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 12:  // Centrocusto RECEBER
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsSituacaocobrancacodInfo.centrocustoreceber = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo1 = new clsSituacaocobrancacodInfo();
                    clsSituacaocobrancacodInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Situacaocobrancacod where codigo='" + clsSituacaocobrancacodInfo.codigo + "' "));
                    clsSituacaocobrancacodInfo1 = clsSituacaocobrancacodBLL.Carregar(clsSituacaocobrancacodInfo1.id, clsInfo.conexaosqldados);
                    if (clsSituacaocobrancacodInfo1.codigo == null)
                    {
                        clsSituacaocobrancacodInfo1 = new clsSituacaocobrancacodInfo();
                    }
                    if (clsSituacaocobrancacodInfo1.codigo == null || clsSituacaocobrancacodInfo1.codigo == clsSituacaocobrancacodInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSituacaocobrancacodInfo1.codigo == null)
                        { // incluir
                            clsSituacaocobrancacodBLL.Incluir(clsSituacaocobrancacodInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSituacaocobrancacodBLL.Alterar(clsSituacaocobrancacodInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
                // Cobranca Cod 1
                str = File.ReadAllLines(clsInfo.arquivos + "\\SITUACAOCOBRANCACOD1.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                clsSituacaocobrancacod1Info clsSituacaocobrancacod1Info = new clsSituacaocobrancacod1Info();
                clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();

                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                //clsSituacaocobrancacod1Info clsSituacaocobrancacod1Info = new clsSituacaocobrancacod1Info();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // idcobrancacod
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.idcobrancacod = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // idhistorico
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.idhistoricopagar = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 4:  // historico PAGAR
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.historicopagar = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                //
                                case 5:  // id CENTROCUSTO pagar
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.idcentrocustopagar = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 6:  // CENTROCUSTO pagar
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.centrocustopagar = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 7:  // id HISTORICO RECEBER
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.idhistoricoreceber = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 8:  // Historico receber
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacodInfo.historicoreceber = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 9:  // IDCENTROCUSTORECEBER
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.idcentrocustoreceber = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 10:  // CENTROCUSTORECEBER
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaocobrancacod1Info.centrocustoreceber = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSituacaocobrancacod1Info clsSituacaocobrancacod1Info1 = new clsSituacaocobrancacod1Info();
                    clsSituacaocobrancacod1Info1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Situacaocobrancacod1 where codigo='" + clsSituacaocobrancacod1Info.codigo + "' "));
                    clsSituacaocobrancacod1Info1 = clsSituacaocobrancacod1BLL.Carregar(clsSituacaocobrancacod1Info1.id, clsInfo.conexaosqldados);
                    if (clsSituacaocobrancacod1Info1.codigo == null)
                    {
                        clsSituacaocobrancacod1Info1 = new clsSituacaocobrancacod1Info();
                    }
                    if (clsSituacaocobrancacod1Info1.codigo == null || clsSituacaocobrancacod1Info1.codigo == clsSituacaocobrancacod1Info1.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSituacaocobrancacod1Info1.codigo == null)
                        { // incluir
                            clsSituacaocobrancacod1BLL.Incluir(clsSituacaocobrancacod1Info, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSituacaocobrancacod1BLL.Alterar(clsSituacaocobrancacod1Info, clsInfo.conexaosqldados);
                        }
                    }
                }
            }

            if (clsInfo.zsituacaotitulo == 0)
            {
                clsSituacaotituloInfo clsSituacaotituloInfo = new clsSituacaotituloInfo();
                clsSituacaotituloBLL clsSituacaotituloBLL = new clsSituacaotituloBLL();

                str = File.ReadAllLines(clsInfo.arquivos + "\\SITUACAOTITULO.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                //Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))   //   separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSituacaotituloInfo = new clsSituacaotituloInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1: // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSituacaotituloInfo.codigo = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2: // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsSituacaotituloInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSituacaotituloInfo clsSituacaotituloInfo1 = new clsSituacaotituloInfo();
                    clsSituacaotituloInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Situacaotitulo where codigo=" + clsSituacaotituloInfo.codigo + " "));
                    clsSituacaotituloInfo1 = clsSituacaotituloBLL.Carregar(clsSituacaotituloInfo1.id, clsInfo.conexaosqldados);
                    if (clsSituacaotituloInfo1.nome == null)
                    {
                        clsSituacaotituloInfo1 = new clsSituacaotituloInfo();
                    }
                    if (clsSituacaotituloInfo1.nome == null || clsSituacaotituloInfo1.codigo == clsSituacaotituloInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSituacaotituloInfo1.nome == null)
                        { // incluir
                            clsSituacaotituloBLL.Incluir(clsSituacaotituloInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSituacaotituloBLL.Alterar(clsSituacaotituloInfo, clsInfo.conexaosqldados);
                        }
                    }
                }

            }
            if (clsInfo.zsituacaotriba == 0)
            {
                clsSittributariaaInfo clsSittributariaaInfo = new clsSittributariaaInfo();
                clsSittributariaaBLL clsSittributariaaBLL = new clsSittributariaaBLL();

                str = File.ReadAllLines(clsInfo.arquivos + "\\SITTRIBUTARIAA.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                //Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSittributariaaInfo = new clsSittributariaaInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:    //CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittributariaaInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittributariaaInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSittributariaaInfo clsSittributariaaInfo1 = new clsSittributariaaInfo();
                    clsSittributariaaInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Sittributariaa where codigo=" + clsSittributariaaInfo.codigo + " "));
                    clsSittributariaaInfo1 = clsSittributariaaBLL.Carregar(clsSittributariaaInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittributariaaInfo1.codigo == null)
                    {
                        clsSittributariaaInfo1 = new clsSittributariaaInfo();
                    }
                    if (clsSittributariaaInfo1.codigo == null || clsSittributariaaInfo1.codigo == clsSittributariaaInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittributariaaInfo1.codigo == null)
                        { // incluir
                            clsSittributariaaBLL.Incluir(clsSittributariaaInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittributariaaBLL.Alterar(clsSittributariaaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zsituacaotribb == 0)
            {
                clsSittributariabInfo clsSittributariabInfo = new clsSittributariabInfo();
                clsSittributariabBLL clsSittributariabBLL = new clsSittributariabBLL();
                //
                str = File.ReadAllLines(clsInfo.arquivos + "\\SITTRIBUTARIAB.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSittributariabInfo = new clsSittributariabInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittributariabInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittributariabInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // IDREFERENCIA DO IMPOSTO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsSittributariabInfo.idreferencia = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;

                            }
                        }
                    }
                    // 
                    clsSittributariabInfo clsSittributariabInfo1 = new clsSittributariabInfo();
                    clsSittributariabInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Sittributariab where codigo=" + clsSittributariabInfo.codigo + " "));
                    clsSittributariabInfo1 = clsSittributariabBLL.Carregar(clsSittributariabInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittributariabInfo1.codigo == null)
                    {
                        clsSittributariabInfo1 = new clsSittributariabInfo();
                    }
                    if (clsSittributariabInfo1.codigo == null || clsSittributariabInfo1.codigo == clsSittributariabInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittributariabInfo1.codigo == null)
                        { // incluir
                            clsSittributariabBLL.Incluir(clsSittributariabInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittributariabBLL.Alterar(clsSittributariabInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }

            Int32 idSitefd = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Sitefd"));
            if (idSitefd == 0)
            {
                clsSitefdInfo clsSitefdInfo = new clsSitefdInfo();
                clsSitefdBLL clsSitefdBLL = new clsSitefdBLL();

                // SITEFD   
                str = File.ReadAllLines(clsInfo.arquivos + "\\SITEFD.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSitefdInfo = new clsSitefdInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSitefdInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSitefdInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // NATUREZA
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSitefdInfo.natureza = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 4:  // DETALHAMENTO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    if (campo.Length > 499)
                                    {
                                        campo = campo.Substring(0, 499);
                                    }
                                    clsSitefdInfo.detalhamento = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 5:  // SOBRE
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsSitefdInfo.sobre = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSitefdInfo clsSitefdInfo1 = new clsSitefdInfo();
                    clsSitefdInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Sitefd where codigo=" + clsSitefdInfo.codigo + " "));
                    clsSitefdInfo1 = clsSitefdBLL.Carregar(clsSitefdInfo1.id, clsInfo.conexaosqldados);
                    if (clsSitefdInfo1.codigo == null)
                    {
                        clsSitefdInfo1 = new clsSitefdInfo();
                    }
                    if (clsSitefdInfo1.codigo == null || clsSitefdInfo1.codigo == clsSitefdInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSitefdInfo1.codigo == null)
                        { // incluir
                            clsSitefdBLL.Incluir(clsSitefdInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSitefdBLL.Alterar(clsSitefdInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zsittribcofins == 0)
            {
                clsSittribcofinsInfo clsSittribcofinsInfo = new clsSittribcofinsInfo();
                clsSittribcofinsBLL clsSittribcofinsBLL = new clsSittribcofinsBLL();
                // SITTRIBCOFINS
                str = File.ReadAllLines(clsInfo.arquivos + "\\SittribCofins.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            //posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSittribcofinsInfo = new clsSittribcofinsInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittribcofinsInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsSittribcofinsInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSittribcofinsInfo clsSittribcofinsInfo1 = new clsSittribcofinsInfo();
                    clsSittribcofinsInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribcofins where codigo=" + clsSittribcofinsInfo.codigo + " "));
                    clsSittribcofinsInfo1 = clsSittribcofinsBLL.Carregar(clsSittribcofinsInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittribcofinsInfo1.codigo == null)
                    {
                        clsSittribcofinsInfo1 = new clsSittribcofinsInfo();
                    }
                    if (clsSittribcofinsInfo1.codigo == null || clsSittribcofinsInfo1.codigo == clsSittribcofinsInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittribcofinsInfo1.codigo == null)
                        { // incluir
                            clsSittribcofinsBLL.Incluir(clsSittribcofinsInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittribcofinsBLL.Alterar(clsSittribcofinsInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zsittribipi == 0)
            {
                clsSitTribIpiInfo clsSitTribIpiInfo = new clsSitTribIpiInfo();
                clsSittribipiBLL clsSittribipiBLL = new clsSittribipiBLL();
                // SITTRIBIPI  
                str = File.ReadAllLines(clsInfo.arquivos + "\\SITTRIBIPI.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSitTribIpiInfo = new clsSitTribIpiInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSitTribIpiInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:   //NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSitTribIpiInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSitTribIpiInfo clsSitTribIpiInfo1 = new clsSitTribIpiInfo();
                    clsSitTribIpiInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Sittribipi where codigo=" + clsSitTribIpiInfo.codigo + " "));
                    clsSitTribIpiInfo1 = clsSittribipiBLL.Carregar(clsSitTribIpiInfo1.id, clsInfo.conexaosqldados);
                    if (clsSitTribIpiInfo1.codigo == null)
                    {
                        clsSitTribIpiInfo1 = new clsSitTribIpiInfo();
                    }
                    if (clsSitTribIpiInfo1.codigo == null || clsSitTribIpiInfo1.codigo == clsSitTribIpiInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSitTribIpiInfo1.codigo == null)
                        { // incluir
                            clsSittribipiBLL.Incluir(clsSitTribIpiInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittribipiBLL.Alterar(clsSitTribIpiInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zsittribpis == 0)
            {
                clsSittribpisInfo clsSittribpisInfo = new clsSittribpisInfo();
                clsSittribpisBLL clsSittribpisBLL = new clsSittribpisBLL();
                // SITTRIBPIS 
                str = File.ReadAllLines(clsInfo.arquivos + "\\SITTRIBPIS.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))   //   separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            { // SITTRIBPIS
                                clsSittribpisInfo = new clsSittribpisInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittribpisInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittribpisInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSittribpisInfo clsSittribpisInfo1 = new clsSittribpisInfo();
                    clsSittribpisInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Sittribpis where codigo=" + clsSittribpisInfo.codigo + " "));
                    clsSittribpisInfo1 = clsSittribpisBLL.Carregar(clsSittribpisInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittribpisInfo1.codigo == null)
                    {
                        clsSittribpisInfo1 = new clsSittribpisInfo();
                    }
                    if (clsSittribpisInfo1.codigo == null || clsSittribpisInfo1.codigo == clsSittribpisInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittribpisInfo1.codigo == null)
                        { // incluir
                            clsSittribpisBLL.Incluir(clsSittribpisInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {  //alterar
                            clsSittribpisBLL.Alterar(clsSittribpisInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zregimeapuracao == 0)
            {
                clsRegimeapuracaoInfo clsRegimeapuracaoInfo = new clsRegimeapuracaoInfo();
                clsRegimeapuracaoBLL clsRegimeapuracaoBLL = new clsRegimeapuracaoBLL();

                str = File.ReadAllLines(clsInfo.arquivos + "\\REGIMEAPURACAO.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {  //REGIMEAPURACAO
                                clsRegimeapuracaoInfo = new clsRegimeapuracaoInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsRegimeapuracaoInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsRegimeapuracaoInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:  // SUBSTITUICAO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsRegimeapuracaoInfo.substituicao = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;

                            }
                        }
                    }
                    // 
                    clsRegimeapuracaoInfo clsRegimeapuracaoInfo1 = new clsRegimeapuracaoInfo();
                    clsRegimeapuracaoInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from REGIMEAPURACAO where codigo='" + clsRegimeapuracaoInfo.codigo + "' "));
                    clsRegimeapuracaoInfo1 = clsRegimeapuracaoBLL.Carregar(clsRegimeapuracaoInfo1.id, clsInfo.conexaosqldados);
                    if (clsRegimeapuracaoInfo1.codigo == null)
                    {
                        clsRegimeapuracaoInfo1 = new clsRegimeapuracaoInfo();
                    }
                    if (clsRegimeapuracaoInfo1.codigo == null || clsRegimeapuracaoInfo1.codigo == clsRegimeapuracaoInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsRegimeapuracaoInfo1.codigo == null)
                        { // incluir
                            clsRegimeapuracaoBLL.Incluir(clsRegimeapuracaoInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsRegimeapuracaoBLL.Alterar(clsRegimeapuracaoInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            // REGIME TRIBUTARIO

            Int32 idRegime = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from REGIMETRIBUTARIO"));
            if (idRegime == 0)
            {
                clsRegimeTributarioInfo clsRegimeTributarioInfo = new clsRegimeTributarioInfo();
                clsRegimeTributarioBLL clsRegimeTributarioBLL = new clsRegimeTributarioBLL();
                // SITEFD   
                str = File.ReadAllLines(clsInfo.arquivos + "\\REGIMETRIBUTARIO.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {

                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsRegimeTributarioInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsRegimeTributarioInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsRegimeTributarioInfo clsRegimeTributarioInfo1 = new clsRegimeTributarioInfo();
                    clsRegimeTributarioInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from REGIMETRIBUTARIO where codigo=" + clsRegimeTributarioInfo.codigo + " "));
                    clsRegimeTributarioInfo1 = clsRegimeTributarioBLL.Carregar(clsRegimeTributarioInfo1.id, clsInfo.conexaosqldados);
                    if (clsRegimeTributarioInfo1.codigo == null)
                    {
                        clsRegimeTributarioInfo1 = new clsRegimeTributarioInfo();
                    }
                    if (clsRegimeTributarioInfo1.codigo == null || clsRegimeTributarioInfo1.codigo == clsRegimeTributarioInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsRegimeTributarioInfo1.codigo == null)
                        { // incluir
                            clsRegimeTributarioBLL.Incluir(clsRegimeTributarioInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsRegimeTributarioBLL.Alterar(clsRegimeTributarioInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            //// UNIDADE
            if (clsInfo.zunidade == 0)
            {
                clsUnidadeInfo clsUnidadeInfo = new clsUnidadeInfo();
                clsUnidadeBLL clsUnidadeBLL = new clsUnidadeBLL();
                str = File.ReadAllLines(clsInfo.arquivos + "\\UNIDADE.txt");
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                }
                if (nRegistros > 0)
                {
                    lbxOrigem.Items.Clear();
                    foreach (String linha in str)
                    {
                        lbxOrigem.Items.Add(linha);
                    }
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))      //separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            { // REGIMEAPURACAO
                                clsUnidadeInfo = new clsUnidadeInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:    //CODIGO
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsUnidadeInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:   //NOME
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsUnidadeInfo.nome = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 3:   //UNIDADEDEC
                                    //campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsUnidadeInfo.uniddec = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsUnidadeInfo clsUnidadeInfo1 = new clsUnidadeInfo();
                    clsUnidadeInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from unidade where codigo='" + clsUnidadeInfo.codigo + "' "));
                    clsUnidadeInfo1 = clsUnidadeBLL.Carregar(clsUnidadeInfo1.id, clsInfo.conexaosqldados);
                    if (clsUnidadeInfo1.codigo == null)
                    {
                        clsUnidadeInfo1 = new clsUnidadeInfo();
                    }
                    if (clsUnidadeInfo1.codigo == null || clsUnidadeInfo1.codigo == clsUnidadeInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsUnidadeInfo1.codigo == null)
                        { // incluir
                            clsUnidadeBLL.Incluir(clsUnidadeInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsUnidadeBLL.Alterar(clsUnidadeInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            if (clsInfo.zzona == 0)
            {

                //
                clsZonasInfo clsZonasInfo = new clsZonasInfo();
                clsZonasBLL clsZonasBLL = new clsZonasBLL();

                clsZonasInfo.ativo = "S";
                clsZonasInfo.codigo = "NC";
                clsZonasInfo.nome = "NADA CONSTA";
                clsZonasInfo.qtde = 0;
                clsZonasInfo.iduf = 27;
                clsZonasBLL.Incluir(clsZonasInfo, clsInfo.conexaosqldados);
            }
            if (clsInfo.zipi == 0)
            {
                clsIpiInfo clsIpiInfo = new clsIpiInfo();
                clsIpiBLL clsIpiBLL = new clsIpiBLL();
                clsIpiInfo.codigo = "0";
                clsIpiInfo.nome = "NADA CONSTA";
                clsIpiInfo.aliqicm = 0;
                clsIpiInfo.aliqii = 0;
                clsIpiInfo.aliqipi = 0;
                clsIpiInfo.aliqmer = 0;
                clsIpiInfo.aliquota = 0;
                clsIpiInfo.ativo = "N";
                clsIpiInfo.cofins = "0";
                clsIpiInfo.pispasep = "0";
                clsIpiInfo.tipo = "";
                //sIpiInfo.unidade = "";
                clsIpiBLL.Incluir(clsIpiInfo, clsInfo.conexaosqldados);
            }
            if (clsInfo.ztolerancia == 0)
            {
                //"TOLERANCIA", "CODIGO, NOME", "'NC', 'NADA CONSTA'");
                clsToleranciaInfo clsToleranciaInfo = new clsToleranciaInfo();
                clsToleranciaBLL clsToleranciaBLL = new clsToleranciaBLL();
                clsToleranciaInfo.ativo = "";
                clsToleranciaInfo.codigo = "NC";
                clsToleranciaInfo.nome = "NADA CONSTA";
                clsToleranciaInfo.norma = "";
                clsToleranciaInfo.tipo = "";
                clsToleranciaBLL.Incluir(clsToleranciaInfo, clsInfo.conexaosqldados);
            }
            if (clsInfo.zpecasfamilia == 0)
            {
                clsPecasFamiliaInfo clsPecasFamiliaInfo = new clsPecasFamiliaInfo();
                clsPecasFamiliaBLL clsPecasFamiliaBLL = new clsPecasFamiliaBLL();

                clsPecasFamiliaInfo.codigo = "NC";
                clsPecasFamiliaInfo.nome = "NADA CONSTA";
                clsPecasFamiliaInfo.idunidade = 1;
                clsPecasFamiliaInfo.tipo = "";
                clsPecasFamiliaBLL.Incluir(clsPecasFamiliaInfo, clsInfo.conexaosqldados);
            }
            if (clsInfo.ztiponota == 0)
            {
                clsTiponotaInfo clsTiponotaInfo = new clsTiponotaInfo();
                clsTiponotaBLL clsTiponotaBLL = new clsTiponotaBLL();
                clsTiponotaInfo.codigo = "0000-000";
                clsTiponotaInfo.devolucao = "N";
                clsTiponotaInfo.fatura = "N";
                clsTiponotaInfo.idcfopbx = 1;
                clsTiponotaInfo.idcfopbxman = 1;
                clsTiponotaInfo.idcfopdef = 1;
                clsTiponotaInfo.idcfopdev = 1;
                clsTiponotaInfo.idcfopent = 1;
                clsTiponotaInfo.idcfopmor = 1;
                clsTiponotaInfo.idcfopok = 1;
                clsTiponotaInfo.idcstcofins = 1;
                clsTiponotaInfo.idcsticms = 1;
                clsTiponotaInfo.idcstipi = 1;
                clsTiponotaInfo.idcstpis = 1;
                clsTiponotaInfo.iddizeres = 1;
                clsTiponotaInfo.iddizeres2 = 1;
                clsTiponotaInfo.movimentacao = "S";
                clsTiponotaInfo.nome = "NADA CONSTA";
                clsTiponotaInfo.somaproduto = "S";
                clsTiponotaInfo.tipo = "V";
                clsTiponotaBLL.Incluir(clsTiponotaInfo, clsInfo.conexaosqldados);
            }


            if (clsInfo.zpecas == 0)
            {

                // Pre Cadastrar a Empresa 
                query = "insert into EMPRESA (CODIGO, COGNOME, PESSOA, NOME, CGC, IDESTADO, IDRAMO, CONTATO) " +
                        "VALUES (@CODIGO, @COGNOME, @PESSOA, @NOME, @CGC, @IDESTADO, @IDRAMO, @CONTATO); select SCOPE_IDENTITY()";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@CODIGO", SqlDbType.Int).Value = ("1".ToString());
                scd.Parameters.Add("@COGNOME", SqlDbType.NVarChar).Value = "CASACORREA";
                scd.Parameters.Add("@PESSOA", SqlDbType.NVarChar).Value = "J";
                scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = "C R CORREA";
                scd.Parameters.Add("@CGC", SqlDbType.NVarChar).Value = "38029184000195";
                scd.Parameters.Add("@IDESTADO", SqlDbType.Int).Value = clsParser.Int32Parse("27".ToString());
                scd.Parameters.Add("@IDRAMO", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@CONTATO", SqlDbType.NVarChar).Value = "CASACORREA";
                scn.Open();
                //scd.ExecuteNonQuery();   
                Int32 idEmpresa = clsParser.Int32Parse(scd.ExecuteScalar().ToString()); //APENAS SE FOR PUXAR O ID
                scn.Close();
                // Pre Cadastrar o Cliente
                query = "insert into CLIENTE (NUMERO, COGNOME, NOME, PESSOA, ATIVO, CGC, IDESTADO, IDVENDEDOR, IDZONA, IDRAMO, IDCONDPAGTO, IDREGIMEAPURACAO, IDTRANSPORTADORA, IDFORMAPAGTO, SUFRAMA, IDCNAE, PAIS, FTP, CODIGOCLIENTE, FRETEINCLUSO, FRETEPORCONTA, MEIODETRANSPORTE, CREDITO, TEMFATURAMENTO, AGENCIA , TITULARCTA , CTACORRENTE , CLIENTEAPROVADO , DDD , TELEFONE , CONTATO , DESCONTAPISPASEPENT , DESCONTAPISPASEPSAI, DESCONTACOFINSENT, DESCONTACOFINSSAI, ISENTOIPI, REVENDEDOR, CONTRIBUINTE, ALC, CONSUMO) " +
                        "VALUES (@NUMERO, @COGNOME, @NOME, @PESSOA, @ATIVO, @CGC, @IDESTADO, @IDVENDEDOR, @IDZONA, @IDRAMO, @IDCONDPAGTO, @IDREGIMEAPURACAO, @IDTRANSPORTADORA, @IDFORMAPAGTO, @SUFRAMA, @IDCNAE, @PAIS, @FTP, @CODIGOCLIENTE, @FRETEINCLUSO, @FRETEPORCONTA, @MEIODETRANSPORTE, @CREDITO, @TEMFATURAMENTO, @AGENCIA, @TITULARCTA, @CTACORRENTE, @CLIENTEAPROVADO, @DDD, @TELEFONE, @CONTATO, @DESCONTAPISPASEPENT, @DESCONTAPISPASEPSAI, @DESCONTACOFINSENT, @DESCONTACOFINSSAI, @ISENTOIPI, @REVENDEDOR, @CONTRIBUINTE, @ALC, @CONSUMO) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = ("1".ToString());
                scd.Parameters.Add("@COGNOME", SqlDbType.NVarChar).Value = "CASACORREA";
                scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = "C R CORREA";
                scd.Parameters.Add("@PESSOA", SqlDbType.NVarChar).Value = "J";
                scd.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = "S";
                scd.Parameters.Add("@CGC", SqlDbType.NVarChar).Value = "38029184000195";
                scd.Parameters.Add("@IDESTADO", SqlDbType.Int).Value = clsParser.Int32Parse("27".ToString());
                scd.Parameters.Add("@IDVENDEDOR", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDZONA", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDRAMO", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDCONDPAGTO", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDREGIMEAPURACAO", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDTRANSPORTADORA", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@SUFRAMA", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@IDCNAE", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@PAIS", SqlDbType.NVarChar).Value = "BRA";
                scd.Parameters.Add("@FTP", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@CODIGOCLIENTE", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@FRETEINCLUSO", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@FRETEPORCONTA", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@MEIODETRANSPORTE", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@CREDITO", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@TEMFATURAMENTO", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@TITULARCTA", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@CTACORRENTE", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@CLIENTEAPROVADO", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@DDD", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@TELEFONE", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@CONTATO", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@DESCONTAPISPASEPENT", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@DESCONTAPISPASEPSAI", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@DESCONTACOFINSENT", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@DESCONTACOFINSSAI", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@ISENTOIPI", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@REVENDEDOR", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@CONTRIBUINTE", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@ALC", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@CONSUMO", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@AGENCIA", SqlDbType.NVarChar).Value = "";
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
                // PEÇAS
                query = "insert into PECAS (CODIGO, NOME, ATIVO, PESOUNIT, PESOBRUTO, IDUNIDADE, IDUNIDADECOM, IDCLASSIFICA, IDCLASSIFICA1, IDCLASSIFICA2, IDHISTORICOBCO, IDIPI, IDSITTRIBA, IDSITTRIBVENDA, IDCLIENTE) " +
                        "VALUES (@CODIGO,@NOME,@ATIVO,@PESOUNIT,@PESOBRUTO,@IDUNIDADE,@IDUNIDADECOM,@IDCLASSIFICA,@IDCLASSIFICA1,@IDCLASSIFICA2,@IDHISTORICOBCO,@IDIPI,@IDSITTRIBA,@IDSITTRIBVENDA, @IDCLIENTE) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@CODIGO", SqlDbType.NVarChar).Value = ("0".ToString());
                scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = "NAO DETERMINADO";
                scd.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = "S";
                scd.Parameters.Add("@PESOUNIT", SqlDbType.NVarChar).Value = clsParser.Int32Parse("0".ToString());
                scd.Parameters.Add("@PESOBRUTO", SqlDbType.NVarChar).Value = clsParser.Int32Parse("0".ToString());
                scd.Parameters.Add("@IDUNIDADE", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDUNIDADECOM", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDCLASSIFICA", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDCLASSIFICA1", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDCLASSIFICA2", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDHISTORICOBCO", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDIPI", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDSITTRIBVENDA", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = clsParser.Int32Parse("1".ToString());
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
                // Cadastrar o EmpresasGere
                query = "insert into EMPRESAGERE (EMPRESA, IDCODIRRF,IDFORIRRF,IDPAGIRRF,IDCODINSS,IDFORINSS,IDPAGINSS, " +
                        "IDCODPIS,IDFORPIS,IDPAGPIS,IDCODCOFINS,IDFORCOFINS,IDPAGCOFINS,IDCODCSLL,IDFORCSLL,IDPAGCSLL, " +
                        "IDCODPISCOFINS,IDFORPISCOFINS,IDPAGPISCOFINS,IDCODISS,IDFORISS,IDPAGISS,IDISS) " +
                        "VALUES (@EMPRESA, @IDCODIRRF,@IDFORIRRF,@IDPAGIRRF,@IDCODINSS,@IDFORINSS,@IDPAGINSS," +
                        "@IDCODPIS,@IDFORPIS,@IDPAGPIS,@IDCODCOFINS,@IDFORCOFINS,@IDPAGCOFINS,@IDCODCSLL,@IDFORCSLL,@IDPAGCSLL," +
                        "@IDCODPISCOFINS,@IDFORPISCOFINS,@IDPAGPISCOFINS,@IDCODISS,@IDFORISS,@IDPAGISS,@IDISS) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@EMPRESA", SqlDbType.Int).Value = idEmpresa;
                scd.Parameters.Add("@IDCODIRRF", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORIRRF", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPAGIRRF", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODINSS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORINSS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPAGINSS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODPIS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORPIS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPAGPIS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODCOFINS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORCOFINS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPAGCOFINS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODCSLL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORCSLL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPAGCSLL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODPISCOFINS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORPISCOFINS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPAGPISCOFINS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODISS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORISS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPAGISS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDISS", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();

            }

            if (clsInfo.zcertificado == 0)
            {
                query = "insert into CERTIFICADO (CERTIFICADO, IDCLIENTE) VALUES (@CERTIFICADO, @IDCLIENTE) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@CERTIFICADO", SqlDbType.Int).Value = clsParser.Int32Parse("0".ToString());
                //scd.Parameters.Add("@ANOCERT", SqlDbType.Int).Value =DateTime.Now.Year;
                scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = clsInfo.zempresaid;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zorcamento == 0)
            {
                query = "insert into ORCAMENTO (FILIAL, NUMERO, DATA,  IDCLIENTE, IDVENDEDOR) VALUES ( " +
                        "@FILIAL, @NUMERO, @DATA, @IDCLIENTE, @IDVENDEDOR ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDVENDEDOR", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            if (clsInfo.zorcamento1 == 0)
            {
                query = "insert into ORCAMENTO1 (IDORCAMENTO, IDPEDIDO, IDPEDIDOITEM, IDORDEMSERVICO, IDCODIGO) VALUES ( " +
                        "@IDORCAMENTO, @IDPEDIDO, @IDPEDIDOITEM, @IDORDEMSERVICO, @IDCODIGO ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDORCAMENTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORDEMSERVICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }


            if (clsInfo.zpedido == 0)
            {
                query = "insert into PEDIDO (FILIAL, NUMERO, DATA,  IDCLIENTE, IDREPRESENTANTE, " +
                        "IDFORMAPAGTO, IDCONDPAGTO) VALUES ( " +
                        "@FILIAL, @NUMERO, @DATA, @IDCLIENTE, @IDREPRESENTANTE,  " +
                        "@IDFORMAPAGTO, @IDCONDPAGTO ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDREPRESENTANTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCONDPAGTO", SqlDbType.Int).Value = 1;

                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zpedido1 == 0)
            {

                query = "insert into PEDIDO1 (IDPEDIDO, IDORCAMENTO, IDORCAMENTOITEM, IDORDEMSERVICO, IDCODIGO, IDCFOP, " +
                        "IDSITTRIBA, IDSITTRIBB, IDUNIDADE, IDIPI) VALUES ( " +
                        "@IDPEDIDO, @IDORCAMENTO, @IDORCAMENTOITEM, @IDORDEMSERVICO, @IDCODIGO, @IDCFOP, " +
                        "@IDSITTRIBA, @IDSITTRIBB, @IDUNIDADE, @IDIPI ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORCAMENTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORCAMENTOITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORDEMSERVICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCFOP", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNIDADE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDIPI", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();

            }

            if (clsInfo.zpedido2 == 0)
            {
                query = "insert into PEDIDO2 (IDPEDIDO, IDPEDIDO1) VALUES ( " +
                        "@IDPEDIDO, @IDPEDIDO1 ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDO1", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zordemservico == 0)
            {
                query = "insert into ORDEMSERVICO (FILIAL, NUMERO, DATA, IDCLIENTE, IDCODIGO) VALUES ( " +
                        "@FILIAL, @NUMERO, @DATA, @IDCLIENTE, @IDCODIGO ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@ANO", SqlDbType.Int).Value = DateTime.Now.Year;
                scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = 1;
                //scd.Parameters.Add("@IDUNID", SqlDbType.Int).Value = 1;
                //scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = 1;
                //scd.Parameters.Add("@IDPEDIDOITEM", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zcompras == 0)
            {
                query = "insert into COMPRAS (FILIAL, NUMERO, DATA, ANO, IDFORNECEDOR, IDEMITENTE, IDAUTORIZANTE, IDCOMPRADOR, " +
                        "IDTRANSPORTADORA, IDFORMAPAGTO, IDCONDPAGTO) VALUES ( " +
                        "@FILIAL, @NUMERO, @DATA, @ANO, @IDFORNECEDOR, @IDEMITENTE, @IDAUTORIZANTE, @IDCOMPRADOR, " +
                        "@IDTRANSPORTADORA, @IDFORMAPAGTO, @IDCONDPAGTO ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@ANO", SqlDbType.Int).Value = DateTime.Now.Year;
                scd.Parameters.Add("@IDFORNECEDOR", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDEMITENTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDAUTORIZANTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOMPRADOR", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDTRANSPORTADORA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCONDPAGTO", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();

            }

            if (clsInfo.zsolicitacao == 0)
            {
                query = "insert into SOLICITACAO (NUMERO, DATA, ANO, EMITENTE, IDSOLICITANTE, IDPEDIDOCOMPRA, IDPEDIDOCOMPRAITEM, " +
                        "IDCODIGO, IDCFOP, IDSITTRIBA, IDSITTRIBB, IDHISTORICO, IDCENTROCUSTO, IDCODIGOCTABIL, IDOS, IDUNID, " +
                        "IDAUTORIZANTESOL, IDAUTORIZANTEALMOX, IDAUTORIZANTEGER  ) VALUES ( " +
                        "@NUMERO, @DATA, @ANO, @EMITENTE, @IDSOLICITANTE, @IDPEDIDOCOMPRA, @IDPEDIDOCOMPRAITEM,   " +
                        "@IDCODIGO, @IDCFOP, @IDSITTRIBA, @IDSITTRIBB, @IDHISTORICO, @IDCENTROCUSTO, @IDCODIGOCTABIL, @IDOS, @IDUNID, " +
                        "@IDAUTORIZANTESOL, @IDAUTORIZANTEALMOX, @IDAUTORIZANTEGER  ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@ANO", SqlDbType.Int).Value = DateTime.Now.Year;
                scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = "SUPERVISOR";
                scd.Parameters.Add("@IDSOLICITANTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOCOMPRA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOCOMPRAITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCFOP", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDOS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNID", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDAUTORIZANTESOL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDAUTORIZANTEALMOX", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDAUTORIZANTEGER", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zcotacao == 0)
            {
                query = "insert into COTACAO (NUMERO, ANO, DATAMONTAGEM, DATAFECHAMENTO, TUDOFECHADOEM, COMPRADOR, TERMINO, IDAUTORIZANTE) VALUES ( " +
                        "@NUMERO, @ANO, @DATAMONTAGEM, @DATAFECHAMENTO, @TUDOFECHADOEM, @COMPRADOR, @TERMINO, @IDAUTORIZANTE ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@ANO", SqlDbType.Int).Value = DateTime.Now.Year;
                scd.Parameters.Add("@DATAMONTAGEM", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@DATAFECHAMENTO", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@TUDOFECHADOEM", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@COMPRADOR", SqlDbType.NVarChar).Value = "SUPERVISOR";
                scd.Parameters.Add("@TERMINO", SqlDbType.NVarChar).Value = "S";
                scd.Parameters.Add("@IDAUTORIZANTE", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zcompras1 == 0)
            {
                query = "insert into COMPRAS1 (IDCOMPRAS, IDSOLICITACAO, IDCOTACAO, IDCOTACAOITEM, IDCOTACAO2, IDSITTRIBA, " +
                        "IDSITTRIBB, IDHISTORICO, IDCENTROCUSTO,IDCODIGOCTABIL, IDOS, IDUNIDFISCAL, IDUNID, IDIPI  ) VALUES ( " +
                        "@IDCOMPRAS, @IDSOLICITACAO, @IDCOTACAO, @IDCOTACAOITEM, @IDCOTACAO2, @IDSITTRIBA, " +
                        "@IDSITTRIBB, @IDHISTORICO, @IDCENTROCUSTO,@IDCODIGOCTABIL, @IDOS, @IDUNIDFISCAL, @IDUNID, @IDIPI  ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDCOMPRAS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSOLICITACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOTACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOTACAOITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOTACAO2", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDOS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNIDFISCAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNID", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDIPI", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zcomprasentrega == 0)
            {
                query = "insert into COMPRASENTREGA (IDCOMPRAS, IDCOMPRAS1, IDOS) VALUES ( " +
                        "@IDCOMPRAS, @IDCOMPRAS1, @IDOS ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDCOMPRAS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOMPRAS1", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDOS", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }



            if (clsInfo.zcotacao1 == 0)
            {
                query = "insert into COTACAO1 (IDCOTACAO, IDCODIGO, IDCFOP, IDSITTRIBA, IDSITTRIBB, IDHISTORICO, IDCENTROCUSTO, " +
                        "IDCODIGOCTABIL, IDORDEMSERVICO, IDUNIDADE, IDSOLICITACAO, IDPEDIDOCOMPRA, IDPEDIDOCOMPRAITEM, " +
                        "IDCOTACAO2GANHOU) VALUES ( " +
                        "@IDCOTACAO, @IDCODIGO, @IDCFOP, @IDSITTRIBA, @IDSITTRIBB, @IDHISTORICO, @IDCENTROCUSTO, " +
                        "@IDCODIGOCTABIL, @IDORDEMSERVICO, @IDUNIDADE, @IDSOLICITACAO, @IDPEDIDOCOMPRA, @IDPEDIDOCOMPRAITEM, " +
                        "@IDCOTACAO2GANHOU ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDCOTACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCFOP", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORDEMSERVICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNIDADE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSOLICITACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOCOMPRA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOCOMPRAITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOTACAO2GANHOU", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.zcotacao2 == 0)
            {
                query = "insert into COTACAO2 (IDCOTACAO, IDCOTACAO1, IDFORNECEDOR, IDCONDPAGTO, IDUNIDADE, IDIPI, IDPEDIDOCOMPRA, " +
                        "IDPEDIDOCOMPRAITEM, IDSITTRIBA, IDSITTRIBB ) VALUES ( " +
                        "@IDCOTACAO, @IDCOTACAO1, @IDFORNECEDOR, @IDCONDPAGTO, @IDUNIDADE, @IDIPI, @IDPEDIDOCOMPRA,  " +
                        "@IDPEDIDOCOMPRAITEM, @IDSITTRIBA, @IDSITTRIBB ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDCOTACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOTACAO1", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORNECEDOR", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCONDPAGTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNIDADE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDIPI", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOCOMPRA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOCOMPRAITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.znfcompra == 0)
            {
                query = "insert into NFCOMPRA (FILIAL, NUMERO, DATA, DATARECEBIMENTO, IDDOCUMENTO, IDFORNECEDOR, IDCONDPAGTO, " +
                        "IDFORMAPAGTO, IDTRANSPORTADORA ) VALUES ( " +
                        "@FILIAL, @NUMERO, @DATA, @DATARECEBIMENTO, @IDDOCUMENTO, @IDFORNECEDOR, @IDCONDPAGTO, " +
                        "@IDFORMAPAGTO, @IDTRANSPORTADORA   ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@DATARECEBIMENTO", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@IDDOCUMENTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORNECEDOR", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCONDPAGTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDTRANSPORTADORA", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }

            if (clsInfo.znfvenda == 0)
            {
                query = "insert into NFVENDA (FILIAL, NUMERO, DATA, DATASAIDA, IDCLIENTE, IDOPERACAO, IDCONDPAGTO, " +
                        "IDFORMAPAGTO,  IDTRANSPORTADORA ) VALUES ( " +
                        "@FILIAL, @NUMERO, @DATA, @DATASAIDA, @IDCLIENTE, @IDOPERACAO, @IDCONDPAGTO, " +
                        "@IDFORMAPAGTO, @IDTRANSPORTADORA   ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;
                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@DATASAIDA", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDOPERACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCONDPAGTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDTRANSPORTADORA", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            if (clsInfo.znfcompra1 == 0)
            {
                query = "insert into NFCOMPRA1 (NUMERO, IDPEDIDO, IDITEMPEDIDO, IDITEMPEDIDOENTREGA, " +
                        "IDCOTACAO, IDITEMCOTACAO, IDSOLICITACAO, IDPEDIDOVENDA, IDPEDIDOVENDAITEM, IDORDEMSERVICO, " +
                        "IDCODIGO, IDSITTRIBA, IDSITTRIBB, IDCFOP, IDUNIDFISCAL, IDUNID, IDHISTORICO, IDCENTROCUSTO, " +
                        "IDCODIGOCTABIL, IDIPI, IDNOTAFISCAL ) VALUES ( " +
                        "@NUMERO, @IDPEDIDO, @IDITEMPEDIDO, @IDITEMPEDIDOENTREGA, " +
                        "@IDCOTACAO, @IDITEMCOTACAO, @IDSOLICITACAO, @IDPEDIDOVENDA, @IDPEDIDOVENDAITEM, @IDORDEMSERVICO, " +
                        "@IDCODIGO, @IDSITTRIBA, @IDSITTRIBB, @IDCFOP, @IDUNIDFISCAL, @IDUNID, @IDHISTORICO, @IDCENTROCUSTO, " +
                        "@IDCODIGOCTABIL, @IDIPI, @IDNOTAFISCAL ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDITEMPEDIDO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDITEMPEDIDOENTREGA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCOTACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDITEMCOTACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSOLICITACAO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOVENDA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOVENDAITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORDEMSERVICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCFOP", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNIDFISCAL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNID", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDIPI", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDNOTAFISCAL", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            if (clsInfo.znfcomprapagar == 0)
            {
                query = "insert into NFCOMPRAPAGAR (IDNOTA, IDTIPOPAGA) VALUES ( " +
                        "@IDNOTA, @IDTIPOPAGA ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@IDNOTA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDTIPOPAGA", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            if (clsInfo.znfvenda1 == 0)
            {
                query = "insert into NFVENDA1 (NUMERO, IDPEDIDO, IDPEDIDOITEM, IDPEDIDO2, IDORDEMSERVICO, IDORCAMENTO, " +
                        "IDORCAMENTOITEM, IDCERTIFICADO, IDCODIGO, IDSITTRIBA, IDSITTRIBB, IDCFOP, IDCFOPFIS, IDHISTORICO, " +
                        "IDCENTROCUSTO, IDCODIGOCTABIL, IDUNID, IDIPI, IDNFCOMPRA, IDITEMNFCOMPRA ) VALUES ( " +
                        "@NUMERO, @IDPEDIDO, @IDPEDIDOITEM, @IDPEDIDO2, @IDORDEMSERVICO, @IDORCAMENTO, " +
                        "@IDORCAMENTOITEM, @IDCERTIFICADO, @IDCODIGO, @IDSITTRIBA, @IDSITTRIBB, @IDCFOP, @IDCFOPFIS, @IDHISTORICO, " +
                        "@IDCENTROCUSTO, @IDCODIGOCTABIL, @IDUNID, @IDIPI, @IDNFCOMPRA, @IDITEMNFCOMPRA ) ";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDOITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDPEDIDO2", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORDEMSERVICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORCAMENTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDORCAMENTOITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCERTIFICADO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCFOP", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCFOPFIS", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDUNID", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDIPI", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDNFCOMPRA", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@IDITEMNFCOMPRA", SqlDbType.Int).Value = 1;
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            // Incluir os itens da DANFE
            ///* NFETIPOAMBIENTE ==  ID,TIPODEAMBIENTE  ,NFECANCELAMENTO  ,NFECONSULTA  ,NFEINUTILIZACAO  ,NFERECEPCAO  ,NFERETRECEPCAO  ,NFESTATUSSERVICO  ,RECEPCAOEVENTO
            //1	1 - 1	https://nfe.fazenda.sp.gov.br/nfeweb/services/nfecancelamento2.asmx	https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta2.asmx	https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao2.asmx	https://nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao2.asmx	https://nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao2.asmx	https://nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico2.asmx	https://nfe.fazenda.sp.gov.br/eventosWEB/services/RecepcaoEvento.asmx
            //2	1 - 2	x	x	x	x	x	x	NULL
            //3	1 - 3	https://www.scan.fazenda.gov.br/NfeCancelamento2/NfeCancelamento2.asmx	https://www.scan.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx	https://www.scan.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx	https://www.scan.fazenda.gov.br/NfeRecepcao2/NfeRecepcao2.asmx	https://www.scan.fazenda.gov.br/NfeRetRecepcao2/NfeRetRecepcao2.asmx	https://www.scan.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx	NULL
            //4	2 - 1	https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeCancelamento2.asmx	https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeConsulta2.asmx	https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeInutilizacao2.asmx	https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeRecepcao2.asmx	https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeRetRecepcao2.asmx	https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeStatusServico2.asmx	https://homologacao.nfe.fazenda.sp.gov.br/eventosWEB/services/RecepcaoEvento.asmx
            //*/
        }
    }
}

