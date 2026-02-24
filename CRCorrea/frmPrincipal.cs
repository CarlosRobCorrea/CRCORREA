using CRCorreaFuncoes;
using CRCorreaBLL;
using CRCorreaInfo;
using CRCorrea;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace CRCorrea
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        public void Init()
        {
            clsInfo.conexaosqlbanco = Properties.Settings.Default.conexaosqlbanco;
            clsInfo.conexaosqldados = Properties.Settings.Default.conexaosqldados;
            //clsInfo.conexaosqlfolha = Properties.Settings.Default.conexaosqlfolha;
            clsInfo.caminhorelatorios = Properties.Settings.Default.caminhorelatorios;
            //clsInfo.imagens = Properties.Settings.Default.imagens;
            clsInfo.arquivos = Properties.Settings.Default.arquivos;
            clsInfo.saidaxml = Properties.Settings.Default.SaidaXML;
            


        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            frmEmpresaVis frmEmpresaVis = new frmEmpresaVis();
            frmEmpresaVis.Init();

            frmSenha frmSenha;

            frmSenha = new frmSenha();
            frmSenha.Init(Properties.Settings.Default.conexaosqldados, frmEmpresaVis);
            frmSenha.ShowDialog();
            if (frmSenha.resultado == true)
            {
                this.Visible = true;
                clsInfo.zmodulo = clsParser.Int32Parse(this.Tag.ToString());

            }
            else
            {
                Application.Exit();
                return;
            }

            if (clsInfo.zusuario != "SUPERVISOR")
            {
                /*
                clsConfiguracaoBLL clsConfiguracaoBLL = new clsConfiguracaoBLL();
                clsConfiguracaoInfo clsConfiguracaoInfo = new clsConfiguracaoInfo()
                clsConfiguracaoInfo = clsConfiguracaoBLL.Carregar(clsInfo.conexaosqldados);
                if (clsConfiguracaoInfo.id == 0)
                {
                    clsConfiguracaoInfo.data1 = DateTime.Now.AddDays(365);
                    clsConfiguracaoInfo.data2 = DateTime.Now;
                    clsConfiguracaoInfo.data3 = DateTime.Now.AddDays(157);
                    clsConfiguracaoInfo.data4 = DateTime.Now.AddDays(361);
                    clsConfiguracaoInfo.dataultima = clsConfiguracaoInfo.data1.AddDays(5);
                    clsConfiguracaoInfo.revalidar = "A";
                    clsConfiguracaoInfo.tiposoftware = "P";
                    clsConfiguracaoInfo.id = clsConfiguracaoBLL.Incluir(clsConfiguracaoInfo, clsInfo.conexaosqldados);
                }
                TimeSpan dias;
                dias = DateTime.Now.Subtract(clsConfiguracaoInfo.dataultima);
                if (dias.Days <= 0 && dias.Days > -5 && DateTime.Now.Date.Date < clsConfiguracaoInfo.dataultima.Date.Date)
                {
                    MessageBox.Show("Problema Atualização de Sistema .. consultar Aplisoft (11)-2988.1442 (" + dias.Days + " dias)");
                }
                else
                {
                    if (dias.Days > 0 || DateTime.Now.Date.Date >= clsConfiguracaoInfo.dataultima.Date.Date)
                    {
                        MessageBox.Show("Favor Atualizar o Sistema .. consultar Aplisoft (11)-2988.1442 ");
                        Application.Exit();
                        return;
                    }
                }

                // Formulários do sistema não pode aparecer
                msp_E03.Visible = false;
                */
            }

            // Estação e Ip            
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            sspEstacaoValor.Text = Environment.MachineName + " - " + addressList[0].ToString().Trim();
            // Usuário
            sspUsuarioValor.Text = clsInfo.zusuario;

            // Local dos Relatórios
            clsInfo.zarquivoreport = Application.StartupPath + "\\Rel\\";

            tmr.Enabled = true;

            // Verificar se for a primeira entrada de qualquer um dos usuarios
            // Efetuar alguns metodos para atualizar o sistema
            if (clsParser.Int32Parse((DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select datault from USUARIO order by datault desc")).Day).ToString()) != DateTime.Now.Day)
            {
                // Procedimentos_Diarios();
            }
            // Gravar a Entrada acesso no sistema
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(clsInfo.conexaosqldados);
            scn.Open();
            scd = new SqlCommand("update usuario set " +
                                 "datault= @data " +
                                 "where id=@idusuario ", scn);
            scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
            scd.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = clsInfo.zusuarioid;
            scd.ExecuteNonQuery();
            scn.Close();



            //try
            //{
            //    //Process.Start(Application.StartupPath + "\\" + clsInfo.zfilial + ".reg");
            //}
            //catch
            //{
            //    //MessageBox.Show("Não foi possível encontrar o arquivo " + clsInfo.zfilial + ".reg", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            if (clsInfo.zusuario == "SUPERVISOR")
            {
                //msp_E03.Visible = true;
            }
            else
            {
                //msp_E03.Visible = false;
            }

            clsFormHelper.VerificaControles(this, clsInfo.conexaosqldados);

            //this.Text = "ApliFerpal  - " + clsInfo.zempresacliente_cognome + " v." + Application.ProductVersion + " Filial[" + clsInfo.zfilial.ToString("000") + "]";
            this.Text = "ACasaCorrea - " + " Filial [ " + clsInfo.zfilial.ToString("000") + " ]" + " v." +
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            checaAgenda();

        }
        private void checaAgenda()
        {
            //if (clsParser.Int32Parse(Procedure.PesquisaValor3(clsInfo.conexaosqldados, "CLIENTEPROSPECCAOOBSERVA", "ID", "IDVENDEDOR", clsInfo.zusuarioid.ToString(), "FECHADA", "A")) > 0)
            if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from CLIENTEOBSERVA where IDVENDEDOR = " + clsInfo.zusuarioid + " and fechada = 'A' ")) > 0)
            {

/*                frmClienteprospeccaoobservaVis frmClienteprospeccaoobservaVis = new frmClienteprospeccaoobservaVis();
                frmClienteprospeccaoobservaVis.Init();
                frmClienteprospeccaoobservaVis.ShowDialog();
*/
            }
        }

        private void mspSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mspMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mspConfEmpresa_Click(object sender, EventArgs e)
        {
            frmEmpresaVis frmEmpresaVis = new frmEmpresaVis();
            frmEmpresaVis.Init();
            clsFormHelper.AbrirForm(this, frmEmpresaVis, clsInfo.conexaosqldados);
        }

        private void mspConfiguracao_Click(object sender, EventArgs e)
        {

        }

        private void mspConfFormularios_Click(object sender, EventArgs e)
        {
            frmFormsScanVis frmFormsScanVis = new frmFormsScanVis();
            frmFormsScanVis.Init((new String[] { "ApliFerpal.exe", "CRCorrea.dll", "Aplisoft.Functions.dll" }), clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(this, frmFormsScanVis, clsInfo.conexaosqldados);
        }

        private void mspConfUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarioVis frmUsuarioVis = new frmUsuarioVis();
            //CRCorrea.frmUsuarioVis frmUsuarioVis = new CRCorrea.frmUsuarioVis();
            frmUsuarioVis.Init(clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this, frmUsuarioVis, clsInfo.conexaosqldados);

        }

        private void mspCondPagto_Click(object sender, EventArgs e)
        {
            frmCondPagtoVis frmCondPagtoVis = new frmCondPagtoVis();
            frmCondPagtoVis.Init(Properties.Settings.Default.conexaosqldados);
            clsFormHelper.AbrirForm(this, frmCondPagtoVis, clsInfo.conexaosqldados);
        }

        private void mspCadClientes_Click(object sender, EventArgs e)
        {
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("", 0);
            clsFormHelper.AbrirForm(this, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void mspRelatorios_Click(object sender, EventArgs e)
        {
            frmRelatorios frmRelatorios = new frmRelatorios();
            clsFormHelper.AbrirForm(this, frmRelatorios, clsInfo.conexaosqldados);

        }

        private void mspMovEntradaNFXML_Click(object sender, EventArgs e)
        {
            frmNFCompraXMLVis frmNFCompraXML = new frmNFCompraXMLVis();
            frmNFCompraXML.Init();
            clsFormHelper.AbrirForm(this, frmNFCompraXML, clsInfo.conexaosqldados);
        }

        private void mspTabBancosBco_Click(object sender, EventArgs e)
        {
            frmTab_BancosVis frmTab_BancosVis = new frmTab_BancosVis();
            frmTab_BancosVis.Init(clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(this, frmTab_BancosVis, clsInfo.conexaosqldados);

        }

        private void mspTabBancosCob_Click(object sender, EventArgs e)
        {
            frmSituacaocobrancacodVis frmSituacaocobrancacodVis = new frmSituacaocobrancacodVis();
            frmSituacaocobrancacodVis.Init(clsInfo.conexaosqldados, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this, frmSituacaocobrancacodVis, clsInfo.conexaosqldados);

        }

        private void mspTabBancosForma_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(Properties.Settings.Default.conexaosqldados, "SITUACAOTIPOTITULO", 0, 2, 20, "Tipo Titulo");

            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void mspTabBancosSituacao_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(Properties.Settings.Default.conexaosqldados, "SITUACAOTITULO", 0, 5, 50, "Situação Titulo");

            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void mspTabBancosImporta_Click(object sender, EventArgs e)
        {
            frmImportarFebraban frmImportarFebraban = new frmImportarFebraban();
            frmImportarFebraban.Init(clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this, frmImportarFebraban, clsInfo.conexaosqldados);

        }

        private void mspTabContabil_Click(object sender, EventArgs e)
        {

        }

        private void mspTabFeriados_Click(object sender, EventArgs e)
        {
            frmFeriadosVis frmFeriadosVis = new frmFeriadosVis();
            frmFeriadosVis.Init();
            clsFormHelper.AbrirForm(this, frmFeriadosVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscal_Click(object sender, EventArgs e)
        {

        }

        private void mspTabRegCidade_Click(object sender, EventArgs e)
        {

        }

        private void mspTabRegEstado_Click(object sender, EventArgs e)
        {

        }

        private void mspTabRegRamos_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "RAMO", 0, 20, 50,"Ramos de Atividades");

            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void mspTabRegZonas_Click(object sender, EventArgs e)
        {

        }

        private void mspMovFinanAPagarAPagar_Click(object sender, EventArgs e)
        {
            frmPagarVis frmPagarVis = new frmPagarVis();
            frmPagarVis.Init();

            clsFormHelper.AbrirForm(this, frmPagarVis, clsInfo.conexaosqldados);

        }

        private void mspMovFinanAPagarBaixa_Click(object sender, EventArgs e)
        {

        }

        private void mspMovFinanAPagarBaixaCh_Click(object sender, EventArgs e)
        {
            frmPagarBaixaCheque frmPagarBaixaCheque = new frmPagarBaixaCheque();
            frmPagarBaixaCheque.Init();
            clsFormHelper.AbrirForm(this, frmPagarBaixaCheque, clsInfo.conexaosqldados);

        }

        private void mspMovFinanAPagarBaixaEle_Click(object sender, EventArgs e)
        {

        }

        private void mspMovFinanAPagarPagas_Click(object sender, EventArgs e)
        {
            frmPagasVis frmPagasVis = new frmPagasVis();
            frmPagasVis.Init();
            clsFormHelper.AbrirForm(this, frmPagasVis, clsInfo.conexaosqldados);

        }

        private void mspMovFinanAPagarAdianta_Click(object sender, EventArgs e)
        {

        }

        private void mspMovFinanAPagarPrev_Click(object sender, EventArgs e)
        {

        }

        private void mspMovFinanAReceberBaixar_Click(object sender, EventArgs e)
        {

        }

        private void mspMovFinanAReceberReceber_Click(object sender, EventArgs e)
        {
            frmReceberVis frmReceberVis = new frmReceberVis();
            frmReceberVis.Init();
            clsFormHelper.AbrirForm(this, frmReceberVis, clsInfo.conexaosqldados);

        }

        private void mspMovFinanAReceberBaixarCh_Click(object sender, EventArgs e)
        {
            frmReceberBaixaCheque frmReceberBaixaCheque = new frmReceberBaixaCheque();
            frmReceberBaixaCheque.Init();

            clsFormHelper.AbrirForm(this, frmReceberBaixaCheque, clsInfo.conexaosqldados);

        }

        private void mspMovFinanAReceberBaixarEle_Click(object sender, EventArgs e)
        {
            frmReceberBancoBol frmReceberBancoBol = new frmReceberBancoBol();
            frmReceberBancoBol.Init();

            clsFormHelper.AbrirForm(this, frmReceberBancoBol, clsInfo.conexaosqldados);

        }
        private void mspMovFinanAReceberBaixarSemi_Click(object sender, EventArgs e)
        {
            frmReceberBaixaSemi frmReceberBaixaSemi = new frmReceberBaixaSemi();
            frmReceberBaixaSemi.Init();

            clsFormHelper.AbrirForm(this, frmReceberBaixaSemi, clsInfo.conexaosqldados);

        }

        private void mspMovEntradaNFManual_Click(object sender, EventArgs e)
        {
            frmNFCompraVis frmNFCompraVis = new frmNFCompraVis();
            frmNFCompraVis.Init();
            clsFormHelper.AbrirForm(this, frmNFCompraVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalCFOP_Click(object sender, EventArgs e)
        {
            frmCfopVis frmCfopVis = new frmCfopVis();
            frmCfopVis.Init(0);

            clsFormHelper.AbrirForm(this, frmCfopVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalIPI_Click(object sender, EventArgs e)
        {
            frmIpiVis frmIpiVis = new frmIpiVis();
            frmIpiVis.Init();

            clsFormHelper.AbrirForm(this, frmIpiVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalDizeresNota_Click(object sender, EventArgs e)
        {
            frmDizeresNFVVis frmDizeresNFVVis = new frmDizeresNFVVis();
            frmDizeresNFVVis.Init(0);
            clsFormHelper.AbrirForm(this, frmDizeresNFVVis, clsInfo.conexaosqldados);
        }

        private void mspTabFiscalDocFiscal_Click(object sender, EventArgs e)
        {
            frmDocFiscalVis frmDocFiscalVis = new frmDocFiscalVis();
            frmDocFiscalVis.Init(0);

            clsFormHelper.AbrirForm(this, frmDocFiscalVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalRegimeApuracao_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "REGIMEAPURACAO", 0, 6, 50, "Regime Apuracao");

            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);
        }

        private void mspTabFiscalTributario_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(Properties.Settings.Default.conexaosqldados, "REGIMETRIBUTARIO", 0, 2, 100, "Regime Tributario");

            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalSitTribOrigem_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(Properties.Settings.Default.conexaosqldados, "SITTRIBUTARIAA", 0, 20, 40, "Situacao Tributaria Origem");

            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalSitTribICMS_Click(object sender, EventArgs e)
        {
            frmSittributariabVis frmSittributariabVis = new frmSittributariabVis();
            frmSittributariabVis.Init();

            clsFormHelper.AbrirForm(this, frmSittributariabVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalSitTribIPI_Click(object sender, EventArgs e)
        {
            frmSitTribIPIVis frmSitTribIPIVis = new frmSitTribIPIVis();
            frmSitTribIPIVis.Init(0);

            clsFormHelper.AbrirForm(this, frmSitTribIPIVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalSitTribPIS_Click(object sender, EventArgs e)
        {
            frmSitTribPISVis frmSitTribPISVis = new frmSitTribPISVis();
            frmSitTribPISVis.Init(0);
            clsFormHelper.AbrirForm(this, frmSitTribPISVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalSitTribCofins_Click(object sender, EventArgs e)
        {
            frmSitTribCOFINSVis frmSitTribCOFINSVis = new frmSitTribCOFINSVis();
            frmSitTribCOFINSVis.Init(0);

            clsFormHelper.AbrirForm(this, frmSitTribCOFINSVis, clsInfo.conexaosqldados);

        }

        private void mspTabFiscalTipoNota_Click(object sender, EventArgs e)
        {
            frmTiponotaVis frmTiponotaVis = new frmTiponotaVis();
            frmTiponotaVis.Init();
            clsFormHelper.AbrirForm(this, frmTiponotaVis, clsInfo.conexaosqldados);

        }

        private void mspMovComprasSol_Click(object sender, EventArgs e)
        {
            frmSolicitacaoVis frmSolicitacaoVis = new frmSolicitacaoVis();
            frmSolicitacaoVis.Init();
            clsFormHelper.AbrirForm(this, frmSolicitacaoVis, clsInfo.conexaosqldados);

        }

        private void mspMovComprasCot_Click(object sender, EventArgs e)
        {
            frmCotacaoVis frmCotacaoVis = new frmCotacaoVis();
            frmCotacaoVis.Init();
            clsFormHelper.AbrirForm(this, frmCotacaoVis, clsInfo.conexaosqldados);

        }

        private void mspMovComprasPed_Click(object sender, EventArgs e)
        {
            frmComprasVis frmComprasVis = new frmComprasVis();
            frmComprasVis.Init();
            clsFormHelper.AbrirForm(this, frmComprasVis, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisGeral_Click(object sender, EventArgs e)
        {
            frmPecasVis frmPecasVis = new frmPecasVis();
            frmPecasVis.Init(0);
            clsFormHelper.AbrirForm(this, frmPecasVis, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisCentroCusto_Click(object sender, EventArgs e)
        {
            frmMaqCtVis frmMaqCtVis = new frmMaqCtVis();
            frmMaqCtVis.Init();
            clsFormHelper.AbrirForm(this, frmMaqCtVis, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisAcertoEstoqueMan_Click(object sender, EventArgs e)
        {

            frmPecasEstoqueMan frmPecasEstoqueMan = new frmPecasEstoqueMan();
            clsFormHelper.AbrirForm(this, frmPecasEstoqueMan, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisAcertoEstoqueAut_Click(object sender, EventArgs e)
        {
            //frmPecasEstoqueAut frmPecasEstoqueAut = new frmPecasEstoqueAut();
            //frmPecasEstoqueAut.Init();

            //clsFormHelper.AbrirForm(this, frmPecasEstoqueAut, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisGrupos_Click(object sender, EventArgs e)
        {
            frmPecasClassificaVis frmPecasClassificaVis = new frmPecasClassificaVis();
            frmPecasClassificaVis.Init();

            clsFormHelper.AbrirForm(this, frmPecasClassificaVis, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisTipos_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(Properties.Settings.Default.conexaosqldados, "PECASTIPO", 0, 30, 30, "Tipos de Peças");
            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisFamilias_Click(object sender, EventArgs e)
        {
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(Properties.Settings.Default.conexaosqldados, "PECASFAMILIA", 0, 20, 40, "Familia das Peças");
            clsFormHelper.AbrirForm(this, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisUnidade_Click(object sender, EventArgs e)
        {
            frmUnidadeVis frmUnidadeVis = new frmUnidadeVis();
            frmUnidadeVis.Init(0);
            clsFormHelper.AbrirForm(this, frmUnidadeVis, clsInfo.conexaosqldados);

        }

        private void mspCadMateriais_Click(object sender, EventArgs e)
        {

        }

        private void mspMovReq_Click(object sender, EventArgs e)
        {
            frmRequisicaoVis frmRequisicaoVis = new frmRequisicaoVis();
            frmRequisicaoVis.Init();
            clsFormHelper.AbrirForm(this, frmRequisicaoVis, clsInfo.conexaosqldados);

        }

        private void mspMovimentacao_Click(object sender, EventArgs e)
        {

        }

        private void mspMovPV_Click(object sender, EventArgs e)
        {

        }

        private void mspMovFinanFecharCaixa_Click(object sender, EventArgs e)
        {
            frmFechamentoCaixa frmFechamentoCaixa = new frmFechamentoCaixa();
            frmFechamentoCaixa.Init();
            clsFormHelper.AbrirForm(this, frmFechamentoCaixa, clsInfo.conexaosqldados);
        }

        private void mspMovFinanceiro_Click(object sender, EventArgs e)
        {

        }

        private void mspCadMateriaisAcertoSaldo_Click(object sender, EventArgs e)
        {
            frmPecasAcertaSaldo frmPecasAcertaSaldo = new frmPecasAcertaSaldo();
            frmPecasAcertaSaldo.Init();
            clsFormHelper.AbrirForm(this, frmPecasAcertaSaldo, clsInfo.conexaosqldados);

        }

        private void mspMovOrcamento1_Click(object sender, EventArgs e)
        {
            frmOrcamentoVis frmOrcamentoVis = new frmOrcamentoVis();
            frmOrcamentoVis.Init();
            clsFormHelper.AbrirForm(this, frmOrcamentoVis, clsInfo.conexaosqldados);

        }

        private void mspMovOrcaConfirma_Click(object sender, EventArgs e)
        {
            frmOrcamentoConfirmarVis frmOrcamentoConfirmarVis = new frmOrcamentoConfirmarVis();
            frmOrcamentoConfirmarVis.Init();
            clsFormHelper.AbrirForm(this, frmOrcamentoConfirmarVis, clsInfo.conexaosqldados);

        }

        private void mspMovComprasAprova_Click(object sender, EventArgs e)
        {
            frmCotacaoAprovaVis frmCotacaoAprovaVis = new frmCotacaoAprovaVis();
            frmCotacaoAprovaVis.Init();
            clsFormHelper.AbrirForm(this, frmCotacaoAprovaVis, clsInfo.conexaosqldados);

        }

        private void mspMovPVNor_Click(object sender, EventArgs e)
        {
            frmPedidoVis frmPedidoVis = new frmPedidoVis();
            frmPedidoVis.Init();
            clsFormHelper.AbrirForm(this, frmPedidoVis, clsInfo.conexaosqldados);
        }

        private void mspMovPVPDV_Click(object sender, EventArgs e)
        {
            frmPedidoPDV frmPedidoPDV = new frmPedidoPDV();
            frmPedidoPDV.Init();
            clsFormHelper.AbrirForm(this, frmPedidoPDV, clsInfo.conexaosqldados);

        }

        private void mspConfAcerto_Click(object sender, EventArgs e)
        {
            frmAcerto frmAcerto = new frmAcerto();
            frmAcerto.Init();
            clsFormHelper.AbrirForm(this, frmAcerto, clsInfo.conexaosqldados);

        }

        private void mspImpNCM_Click(object sender, EventArgs e)
        {
            /* importacao da tabela desativada
            DialogResult resultado;
            String query;
            SqlConnection scn;
            SqlCommand scd;

            String[] str;

            // transferencia das tabelas de txt para a base sql
            Int32 nRegistros = 0;
            Int32 qtcaracter;
            Int32 posicaoatual;
            Int32 posicaoinicial;
            Int32 posicaofinal;
            Boolean ok;
            String campo;

            clsIpiInfo clsIpiInfo = new clsIpiInfo();
            clsIpiBLL clsIpiBLL = new clsIpiBLL();

            resultado = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (resultado == DialogResult.Yes)
            {

                str = File.ReadAllLines(clsInfo.arquivos + "\\NCM.txt");
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
                        if (linha.ToString().Substring(i, 1) == ";" || i == (qtcaracter - 1))      //separacao |
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
                                clsIpiInfo = new clsIpiInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:  //  codigo
                                    clsIpiInfo.codigo = campo; // linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsIpiInfo.codigo = clsVisual.RemoveAcentos(clsIpiInfo.codigo);
                                    clsIpiInfo.codigo = clsIpiInfo.codigo.ToUpper();
                                    clsIpiInfo.codigo = clsIpiInfo.codigo.Replace(".", "");
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2: // nome
                                    clsIpiInfo.nome = campo; // linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsIpiInfo.nome = clsVisual.RemoveAcentos(clsIpiInfo.nome);
                                    clsIpiInfo.nome = clsIpiInfo.nome.ToUpper();
                                    if (clsIpiInfo.nome.Length > 254)
                                    {
                                        clsIpiInfo.nome = clsIpiInfo.nome.Substring(0, 254);
                                    }
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    //        // 
                    clsIpiInfo clsIpiInfo1 = new clsIpiInfo();
                    clsIpiInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + clsIpiInfo.codigo + "' "));
                    clsIpiInfo1 = clsIpiBLL.Carregar(clsIpiInfo1.id, clsInfo.conexaosqldados);
                    if (clsIpiInfo1 == null)
                    {
                        clsIpiInfo1 = new clsIpiInfo();
                    }
                    if (clsIpiInfo1.id == 0)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsIpiInfo1.aliquota == 0)
                        { // incluir
                            scd = new SqlCommand("INSERT INTO IPI (" +
                                    "CODIGO, NOME, ATIVO" +
                                    ") VALUES ( " +
                                    "@CODIGO, @NOME, @ATIVO);" +
                                    "SELECT SCOPE_IDENTITY()", scn);
                        }
                        else
                        { // alterar
                            scd = new SqlCommand("UPDATE IPI SET " +
                                                "CODIGO=@CODIGO, NOME=@NOME, ATIVO=@ATIVO " +
                                                "WHERE ID = @ID", scn);
                            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsIpiInfo1.id;
                        }
                        scd.Parameters.AddWithValue("@CODIGO", SqlDbType.NVarChar).Value = clsIpiInfo.codigo;
                        scd.Parameters.AddWithValue("@NOME", SqlDbType.NVarChar).Value = clsIpiInfo.nome;
                        scd.Parameters.AddWithValue("@ATIVO", SqlDbType.NVarChar).Value = "S";
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                    }
                }

            }
            MessageBox.Show("Termino da Transferencia");
            */

            frmImportarNCM_XML frmImportarNCM_XML = new frmImportarNCM_XML();
            frmImportarNCM_XML.Init();
            clsFormHelper.AbrirForm(this, frmImportarNCM_XML, clsInfo.conexaosqldados);

        }

        private void mspNotaPromissoria_Click(object sender, EventArgs e)
        {
            frmNotaPromissoria frmNotaPromissoria = new frmNotaPromissoria();
            frmNotaPromissoria.Init(0);
            clsFormHelper.AbrirForm(this, frmNotaPromissoria, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisExportar_Click(object sender, EventArgs e)
        {
            frmPecasExportar frmPecasExportar = new frmPecasExportar();
            frmPecasExportar.Init(0);
            clsFormHelper.AbrirForm(this, frmPecasExportar, clsInfo.conexaosqldados);

        }

        private void mspCadMateriaisNCM_Click(object sender, EventArgs e)
        {
            frmPecasNCMMan frmPecasNCMMan = new frmPecasNCMMan();
            clsFormHelper.AbrirForm(this, frmPecasNCMMan, clsInfo.conexaosqldados);

        }
    }
}
