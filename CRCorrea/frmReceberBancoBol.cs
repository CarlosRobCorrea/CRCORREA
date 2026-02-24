using CRCorreaBarCod;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Shared;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmReceberBancoBol : Form
    {
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        clsReceberInfo clsReceberInfo = new clsReceberInfo();
        clsReceberBLL clsReceberBLL = new clsReceberBLL();

        clsClienteInfo clsClienteInfo = new clsClienteInfo();
        clsClienteBLL clsClienteBLL = new clsClienteBLL();

        clsClienteEnderecoInfo clsClienteEnderecoInfo = new clsClienteEnderecoInfo();
        clsClienteEnderecoBLL clsClienteenderecoBLL = new clsClienteEnderecoBLL();

        clsEmpresaInfo clsEmpresaInfo = new clsEmpresaInfo();
        clsEmpresaBLL clsEmpresaBLL = new clsEmpresaBLL();

        clsBancosInfo clsBancosInfo = new clsBancosInfo();
        clsBancosBLL clsBancosBLL = new clsBancosBLL();

        clsEmpresaGereInfo clsEmpresaGereInfo = new clsEmpresaGereInfo();
        clsEmpresaGereBLL clsEmpresaGereBLL = new clsEmpresaGereBLL();

        SqlConnection scn;
        SqlCommand scd;
        SqlDataAdapter sda;

        String query;

        int painelPrincipal = 0;

        Int32 idBanco = 0;
        Int32 idBancoInt = 0;
        Int32 idBancoIntDes = 0;      

        // Em Aberto
        Boolean carregandoReceberAberto;
        DataTable dtReceberAberto;
        BackgroundWorker bwrReceberAberto;
        Int32 idReceberAberto;
        // No Banco (Já colocado)
        Boolean carregandoReceberBanco;
        DataTable dtReceberBanco;
        BackgroundWorker bwrReceberBanco;

        // Carregando o banco Indicado para enviar
        Int32 idBcoEspecie;
        Int32 idBcoMoeda;
        Int32 idBcoAceite;
        Int32 idBcoProtestar;
        Int32 idBcoImpressao;
        Int32 idBcoModalidade;
        Int32 idBcoProtestar2;       

        Boolean carregandoReceberEnviar;
        DataTable dtReceberEnviar;
        BackgroundWorker bwrReceberEnviar;
        Int32 posicao_receberenviar;
        Int32 idReceberEnviar;
        Int32 id_AnteriorEnviar;
        Int32 id_ProximoEnviar;

        DataTable dtReceberEnviar_Imprimir;

        Decimal vDesconto = 0;
        Decimal vValorEspecial = 0;

        String ArquivoBanco;
        Int32 nsequencia;

        Int32 Reg;
        String cDv = "";

        DataTable dtClienteEndereco;

        // Colunas do Contas a Receber (O que vai ser baixado)
        GridColuna[] dtReceberAbertoColunas = new GridColuna[]
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
                                            new GridColuna("F. Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Bco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Juros Dia", "VALORJUROS", 65, true, DataGridViewContentAlignment.MiddleRight)
                                        };


        // Colunas do Contas a Receber (O que esta no Banco
        GridColuna[] dtReceberBancoColunas = new GridColuna[]
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
                                            new GridColuna("F. Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Bco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Juros Dia", "VALORJUROS", 65, true, DataGridViewContentAlignment.MiddleRight)
                                        };

        // Colunas do Contas a Receber (O que esta no Banco
        GridColuna[] dtReceberEnviarColunas = new GridColuna[]
                                        {
                                            new GridColuna("Posicao", "POSICAO_RECEBERENVIAR", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Id", "IDRECEBER", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Id", "IDCLIENTE", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("F", "Filial", 20, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("PF", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Emissao", "EMISSAO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Vl.Titulo", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vl.Liq", "VALORLIQUIDO", 80, false, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Vendedor", "VENDEDOR", 70, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("F. Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Bco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Juros Dia", "VALORJUROS", 65, true, DataGridViewContentAlignment.MiddleRight)
                                        };

        Boolean carregandoDocumentos;
        DataTable dtDocumentos;
        BackgroundWorker bwrDocumentos;

        String tipoDocumento;

        // Colunas do Documentos de Acordo com a Lupa que apertar
        GridColuna[] dtDocumentosColunas = new GridColuna[]
                                        {
                                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Codigo", "CODIGO", 40, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Descrição", "NOME", 350, true, DataGridViewContentAlignment.MiddleLeft)
                                        };

        public frmReceberBancoBol()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsReceberBLL = new clsReceberBLL();
            clsClienteBLL = new clsClienteBLL();
            clsClienteenderecoBLL = new clsClienteEnderecoBLL();

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tab_bancos order by codigo", tbxBanco);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBancoInt);

        }

        private void frmReceberBancoBol_Load(object sender, EventArgs e)
        {
            tspTool.Visible = true;
            tspBanco.Visible = true;
            tbxArquivoDestino.Text = clsInfo.arquivos;

            CriarReceberEnviar();


            tbxBancoInt.Text = "0";
            tbxBancoIntDes.Text = "0";

            tbxJurosMes.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select JUROSMES FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid)).ToString("N2");
            tbxProtestar.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PROTESTAR FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid)).ToString("N0");
            tbxBoletolinha01.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETOLINHA01 FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            tbxBoletolinha02.Text = ""; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETOLINHA02 FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            tbxBoletolinha03.Text = ""; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETOLINHA03 FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            tbxBoletolinha04.Text = ""; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETOLINHA04 FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            tbxBoletolinha05.Text = "";
            tbxSequencia.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select SEQUENCIA FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            tbxBoletoProximo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select REGISTRO FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
                     

            bwrReceberAberto_Run();

            tbxBancoInt.Select();
            tbxBancoInt.SelectAll();
        }

        private void frmReceberBancoBol_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
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

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == btnIdBcoEspecie.Name)
                {
                    idBcoEspecie = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoEspecie.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoEspecie.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();

                }
                else if (clsInfo.znomegrid == btnIdBcoMoeda.Name)
                {
                    idBcoMoeda = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoMoeda.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoMoeda.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdBcoAceite.Name)
                {
                    idBcoAceite = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoAceite.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoAceite.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdBcoProtestar.Name)
                {
                    idBcoProtestar = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoProtestar.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoProtestar.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();

                }
                else if (clsInfo.znomegrid == btnIdBcoImpressao.Name)
                {
                    idBcoImpressao = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoImpressao.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoImpressao.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdBcoModalidade.Name)
                {
                    idBcoModalidade = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoModalidade.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoModalidade.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdProtestar2.Name)
                {
                    idBcoProtestar2 = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoProtestar2.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoProtestar2.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }

            }
            //
            if (ctl.Name == tbxBcoEspecie.Name)
            {
                 idBcoEspecie = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSESPECIE where TAB_BANCOSESPECIE.CODIGO='" + tbxBcoEspecie.Text + "'  AND  TAB_BANCOSESPECIE.IDCODIGO = " + idBancoIntDes + " "));
                if ( idBcoEspecie == 0)
                {
                    //idbcoespecie = //clsInfo.zbanco;
                }
                tbxBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSESPECIE where ID= " + idBcoEspecie);
                lblBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSESPECIE where ID= " + idBcoEspecie);
            }
            if (ctl.Name == tbxBcoMoeda.Name)
            {
                 idBcoMoeda = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSMOEDA where CODIGO='" + tbxBcoMoeda.Text + "' AND  TAB_BANCOSMOEDA.IDCODIGO = " + idBancoIntDes + " "));
                if (idBcoMoeda == 0)
                {
                    //idbcoespecie = //clsInfo.zbanco;
                }
                tbxBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMOEDA where ID= " + idBcoMoeda);
                lblBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMOEDA where ID= " + idBcoMoeda);
            }
            if (ctl.Name == tbxBcoAceite.Name)
            {
                idBcoAceite = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSACEITE where CODIGO='" + tbxBcoAceite.Text + "' AND  TAB_BANCOSACEITE.IDCODIGO = " + idBancoIntDes + " "));
                if (idBcoAceite == 0)
                {
                    //idbcoaceite = //clsInfo.zbanco;
                }
                tbxBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSACEITE where ID= " + idBcoAceite);
                lblBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSACEITE where ID= " + idBcoAceite);
            }

            if (ctl.Name == tbxBcoProtestar.Name)
            {
                idBcoProtestar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSPROTESTAR where CODIGO='" + tbxBcoProtestar.Text + "' AND  TAB_BANCOSPROTESTAR.IDCODIGO = " + idBancoIntDes + " "));
                if (idBcoProtestar == 0)
                {
                    //idbcoprotestar = //clsInfo.zbanco;
                }
                tbxBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar);
                lblBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar);
            }

            if (ctl.Name == tbxBcoImpressao.Name)
            {
                idBcoImpressao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSIMPRESSAO where CODIGO='" + tbxBcoImpressao.Text + "' AND  TAB_BANCOSIMPRESSAO.IDCODIGO = " + idBancoIntDes + " "));
                if (idBcoImpressao == 0)
                {
                    //idbcoimpressao = //clsInfo.zbanco;
                }
                tbxBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSIMPRESSAO where ID= " + idBcoProtestar);
                lblBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSIMPRESSAO where ID= " + idBcoProtestar);
            }
            if (ctl.Name == tbxBcoModalidade.Name)
            {
                idBcoModalidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSMODALIDADE where CODIGO='" + tbxBcoModalidade.Text + "' AND  TAB_BANCOSMODALIDADE.IDCODIGO = " + idBancoIntDes + " "));
                if (idBcoModalidade == 0)
                {
                    //idbcomodalidade = //clsInfo.zbanco;
                }
                tbxBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMODALIDADE where ID= " + idBcoModalidade);
                lblBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMODALIDADE where ID= " + idBcoModalidade);
            }

            if (ctl.Name == tbxBcoProtestar2.Name)
            {
                idBcoProtestar2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSPROTESTAR where CODIGO='" + tbxBcoProtestar2.Text + "'  AND  TAB_BANCOSPROTESTAR.IDCODIGO = " + idBancoIntDes + " "));
                if (idBcoProtestar2 == 0)
                {
                    //idbcoprotestar2 = //clsInfo.zbanco;
                }
                tbxBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar);
                lblBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar);
            }
            //
            if (clsInfo.znomegrid == btnIdBancoInt.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancoInt = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancoInt.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancoIntNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();


                        idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancoInt));
                        tbxBanco.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco)).ToString("N0");
                        tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);


                    }
                    idBancoIntDes = idBancoInt;
                    tbxBancoIntDes.Text = tbxBancoInt.Text;
                    tbxBancoIntDesNome.Text = tbxBancoIntNome.Text;
                    tbxBancoInt.Select();
                }
                bwrReceberAberto_Run();

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


                idBancoIntDes = idBancoInt;
                tbxBancoIntDes.Text = tbxBancoInt.Text;
                tbxBancoIntDesNome.Text = tbxBancoIntNome.Text;

                idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancoInt));
                tbxBanco.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco)).ToString("N0");
                tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);

                bwrReceberAberto_Run();

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
                bwrReceberAberto_Run();
                TrazDadosEmpresa_Banco();
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

                Int32 idBancoDes = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancoIntDes));
                tbxBancoDes.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBancoDes)).ToString("N0");
                TrazDadosEmpresa_Banco();            
            }

            if (chxBancosTodos.Checked == true)
            {
                chxBancosTodos.Text = "Adicionar todas sem Banco Indicado sendo [BO=Boleto]";
            }
            else
            {
                chxBancosTodos.Text = "Apenas as Duplicatas com Banco Febraban Indicado (" + tbxBanco.Text + ")";
            }

            tbxNroNota.Text = clsParser.Int32Parse(tbxNroNota.Text).ToString();

            bwrReceberAberto_Run();

            clsInfo.znomegrid = "";
        }

        private void btnIdBancoInt_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBancoInt.Name;
            frmTab_BancosPes frmTab_BancosPes = new frmTab_BancosPes();
            frmTab_BancosPes.Init(clsInfo.conexaosqlbanco, idBancoInt);
            clsFormHelper.AbrirForm(this.MdiParent, frmTab_BancosPes, clsInfo.conexaosqlbanco);

        }
        private void btnIdBancoIntDes_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBancoIntDes.Name;
            frmTab_BancosPes frmTab_BancosPes = new frmTab_BancosPes();
            frmTab_BancosPes.Init(clsInfo.conexaosqlbanco, idBancoIntDes);
            clsFormHelper.AbrirForm(this.MdiParent, frmTab_BancosPes, clsInfo.conexaosqlbanco);
        }

        private void chxBancosTodos_Click(object sender, EventArgs e)
        {
            if (chxBancosTodos.Checked == true)
            {
                chxBancosTodos.Text = "Adicionar todas sem Banco Indicado sendo [BO=Boleto]";
                bwrReceberAberto_Run();
            }
            else
            {
                chxBancosTodos.Text = "Apenas as Duplicatas com Banco Febraban Indicado (" + tbxBanco.Text + ")";
                bwrReceberAberto_Run();
            }
        }

        private void tspConfigurarEspecial_Click(object sender, EventArgs e)
        {
            painelPrincipal = 1;
            tclResolve();
            TrazDadosEmpresa_Banco();
        }

        private void tspImprimirConfiguracao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void tspImprimirDuplicata_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void tspImprimirBoleto_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            if (dgvReceberEnviar.Rows.Count > 0)
            {
                resultado = MessageBox.Show("Deseja Imprimir os Boletos para o Banco " + tbxBancoIntDesNome.Text + " ? ", "Aplisoft",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    using (TransactionScope tse = new TransactionScope())
                    {
                        if (clsParser.Int32Parse(tbxBancoDes.Text) == 1)
                        {//Banco do Brasil
                            BoletoBrasil();
                        }
                        else if (clsParser.Int32Parse(tbxBancoDes.Text) == 33)
                        {//Banco Santander
                            BoletoSantander();
                        }
                    }

                    ////DialogResult resultado;
                    //resultado = MessageBox.Show("Deseja Enviar o Arquivo de Remessa?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    //if (resultado == DialogResult.Yes)
                    //{
                        tspEnviarBanco.PerformClick();
                    //}
                    //else if (resultado == DialogResult.Cancel)
                    //{

                    //}
                }
            }
        }

        private void tspEnviarBanco_Click(object sender, EventArgs e)
        {
            if (clsParser.DecimalParse(tbxJurosMes1.Text) > 0)
            {
                dtReceberEnviar_Imprimir = new DataTable();

                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Enviar as Remessa para o Banco " + tbxBancoNome.Text + " ? ", "Aplisoft",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {


                    // Verificar se o Diretorio Existe
                    tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text;
                    DirectoryInfo dir = new DirectoryInfo(@tbxArquivoDestino.Text);
                    if (dir.Exists)
                    {

                        using (TransactionScope tse = new TransactionScope())
                        {
                            if (clsParser.Int32Parse(tbxBancoDes.Text) == 1)
                            {//Banco do Brasil
                                RemessaBrasil();
                            }
                            else if (clsParser.Int32Parse(tbxBancoDes.Text) == 33)
                            {//Banco Santander
                                RemessaSantander();
                            }
                            else if (clsParser.Int32Parse(tbxBancoDes.Text) == 341)
                            {//Banco Santander
                                RemessaItau();
                            }
                            // Copiar o Datatable
                            dtReceberEnviar_Imprimir = dtReceberEnviar.Copy();

                            // Marcar no contas a receber o numero do boleto e banco enviado
                            // Marcar também no contas a receber da nota fiscal
                            foreach (DataRow row in dtReceberEnviar.Rows)
                            {
                                if (rbnEnviarNormal.Checked == true)
                                {  // Envio de Duplicatas uma a uma
                                    idReceberEnviar = clsParser.Int32Parse(row["IDRECEBER"].ToString());
                                    clsReceberInfo = clsReceberBLL.Carregar(idReceberEnviar, clsInfo.conexaosqldados);
                                    clsReceberInfo.boletonro = clsParser.DecimalParse(row["BOLETONRO"].ToString());
                                    clsReceberInfo.dv = row["DV"].ToString();
                                    clsReceberInfo.idbanco = idBanco;
                                    clsReceberInfo.idbancoint = idBancoIntDes;
                                    clsReceberInfo.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTIPOTITULO where CODIGO='" + "BO" + "' "));
                                    clsReceberInfo.imprimir = "N";
                                    clsReceberInfo.valorjuros = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                                    clsReceberBLL.Alterar(clsReceberInfo, clsInfo.conexaosqldados);
                                    String cDocumento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + clsReceberInfo.iddocumento + " ");
                                    if (cDocumento == "CTO")
                                    {
                                        scn = new SqlConnection(clsInfo.conexaosqldados);
                                        scd = new SqlCommand("UPDATE CONTRATORECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);
                                        scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsReceberInfo.idrecebernfv;
                                        scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = clsReceberInfo.boletonro;
                                        scd.Parameters.AddWithValue("@DV", SqlDbType.NVarChar).Value = clsReceberInfo.dv;
                                        scd.Parameters.AddWithValue("@IDTIPOPAGA", SqlDbType.Int).Value = clsReceberInfo.idformapagto;
                                        scn.Open();
                                        scd.ExecuteNonQuery();
                                        scn.Close();

                                    }
                                    else
                                    {
                                        scn = new SqlConnection(clsInfo.conexaosqldados);
                                        scd = new SqlCommand("UPDATE NFVENDARECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);
                                        scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsReceberInfo.idrecebernfv;
                                        scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = clsReceberInfo.boletonro;
                                        scd.Parameters.AddWithValue("@DV", SqlDbType.NVarChar).Value = clsReceberInfo.dv;
                                        scd.Parameters.AddWithValue("@IDTIPOPAGA", SqlDbType.Int).Value = clsReceberInfo.idformapagto;
                                        scn.Open();
                                        scd.ExecuteNonQuery();
                                        scn.Close();
                                    }
                                }
                                else
                                {  // Envio de Duplicatas agrupada por cnpj
                                    Int32 qtcaracter = row["IDDUPLICATALINHA05"].ToString().Length;
                                    Int32 posicaoatual = 0;
                                    Int32 posicaoinicial = 0;
                                    Int32 posicaofinal = 0;

                                    String campo = "";

                                    Boolean ok = false;
                                    for (Int32 i = 1; i < qtcaracter; i++)
                                    {
                                        if (row["IDDUPLICATALINHA05"].ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
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
                                            campo = (row["IDDUPLICATALINHA05"].ToString().Substring(posicaoinicial, (posicaofinal - posicaoinicial)));
                                            posicaoinicial = (posicaofinal + 1);

                                            idReceberEnviar = clsParser.Int32Parse(campo);
                                            clsReceberInfo = clsReceberBLL.Carregar(idReceberEnviar, clsInfo.conexaosqldados);
                                            clsReceberInfo.boletonro = clsParser.DecimalParse(row["BOLETONRO"].ToString());
                                            clsReceberInfo.dv = row["DV"].ToString();
                                            clsReceberInfo.idbanco = idBanco;
                                            clsReceberInfo.idbancoint = idBancoIntDes;
                                            clsReceberInfo.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTIPOTITULO where CODIGO='" + "BO" + "' "));
                                            clsReceberInfo.imprimir = "N";
                                            clsReceberInfo.valorjuros = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                                            clsReceberBLL.Alterar(clsReceberInfo, clsInfo.conexaosqldados);
                                            String cDocumento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + clsReceberInfo.iddocumento + " ");
                                            if (cDocumento == "CTO")
                                            {
                                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                                scd = new SqlCommand("UPDATE CONTRATORECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);
                                                scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsReceberInfo.idrecebernfv;
                                                scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = clsReceberInfo.boletonro;
                                                scd.Parameters.AddWithValue("@DV", SqlDbType.NVarChar).Value = clsReceberInfo.dv;
                                                scd.Parameters.AddWithValue("@IDTIPOPAGA", SqlDbType.Int).Value = clsReceberInfo.idformapagto;
                                                scn.Open();
                                                scd.ExecuteNonQuery();
                                                scn.Close();

                                            }
                                            else
                                            {
                                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                                scd = new SqlCommand("UPDATE NFVENDARECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);
                                                scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsReceberInfo.idrecebernfv;
                                                scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = clsReceberInfo.boletonro;
                                                scd.Parameters.AddWithValue("@DV", SqlDbType.NVarChar).Value = clsReceberInfo.dv;
                                                scd.Parameters.AddWithValue("@IDTIPOPAGA", SqlDbType.Int).Value = clsReceberInfo.idformapagto;
                                                scn.Open();
                                                scd.ExecuteNonQuery();
                                                scn.Close();
                                            }
                                        }
                                    }
                                }

                            }


                            // Todos os Lançamentos da Duplicata
                            Boolean terminou = false;
                            while (terminou == false)
                            {
                                if (dtReceberEnviar.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dtReceberEnviar.Rows)
                                    {
                                        dtReceberEnviar.Select("POSICAO_RECEBERENVIAR =" + row["POSICAO_RECEBERENVIAR"].ToString())[0].Delete();
                                        terminou = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    terminou = true;
                                }
                            }
                            //
                            // GRAVAR NA CONTA DO BANCO O ULTIMO NUMERO DE BOLETO ENVIADO
                            scn = new SqlConnection(clsInfo.conexaosqlbanco);
                            scd = new SqlCommand("UPDATE BANCOS SET NROBOLETO=@BOLETONRO WHERE ID=@ID ", scn);
                            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idBancoIntDes;
                            scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.NVarChar).Value = (clsParser.DecimalParse(tbxBoletoProximo1.Text) - 1).ToString();
                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();
                            //
                            tse.Complete();
                        }
                        bwrReceberAberto_Run();
                        bwrReceberBanco_Run();
                    }
                    else
                    {
                        resultado = MessageBox.Show("Diretorio onde será copiado não existe podemos criar ? " + tbxArquivoDestino.Text + " ? ", "Aplisoft",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultado == DialogResult.Yes)
                        {
                            dir = new DirectoryInfo(@clsInfo.arquivos);
                            DirectoryInfo sub = dir.CreateSubdirectory(tbxBancoNome.Text);
                            MessageBox.Show("Termino de Procedimento .. Tente Transferir novamente.. ");
                        }
                    }

                    ////imprimindo o boleto 
                    resultado = MessageBox.Show("Deseja Imprimir os Boletos para o Banco " + tbxBancoIntDesNome.Text + " ? ", "Aplisoft",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tse = new TransactionScope())
                        {
                            if (clsParser.Int32Parse(tbxBancoDes.Text) == 1)
                            {//Banco do Brasil
                             // BoletoBrasil();
                            }
                            else if (clsParser.Int32Parse(tbxBancoDes.Text) == 33)
                            {//Banco Santander
                             // BoletoSantander();
                            }
                            else if (clsParser.Int32Parse(tbxBancoDes.Text) == 341)
                            {//Banco Itau
                                BoletoItau(dtReceberEnviar_Imprimir);
                            }

                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Indique o Valor do Juros Mês");
            }

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bwrReceberAberto_Run()
        {
            if (carregandoReceberAberto == false)
            {
                carregandoReceberAberto = true;

                bwrReceberAberto = new BackgroundWorker();
                bwrReceberAberto.DoWork += new DoWorkEventHandler(bwrReceberAberto_DoWork);
                bwrReceberAberto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberAberto_RunWorkerCompleted);
                bwrReceberAberto.RunWorkerAsync();
            }
        }


        private void bwrReceberAberto_DoWork(object sender, DoWorkEventArgs e)
        {
            dtReceberAberto = new DataTable();
            String query = "SELECT RECEBER.ID,RECEBER.FILIAL,RECEBER.DUPLICATA,RECEBER.POSICAO " +
                    ",RECEBER.POSICAOFIM,RECEBER.EMISSAO,CLIENTE.COGNOME,RECEBER.VENCIMENTO " +
                    ",RECEBER.VENCIMENTOPREV,RECEBER.VALOR,RECEBER.VALORLIQUIDO,VENDEDOR.COGNOME AS [VENDEDOR] " +
                    ",SITUACAOTIPOTITULO.CODIGO + '-' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], RECEBER.BOLETONRO " +
                    ",RECEBER.DV,TAB_BANCOS.COGNOME AS [BANCO],RECEBER.VALORJUROS " +
                    "FROM RECEBER " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=RECEBER.IDCLIENTE " +
                    "LEFT JOIN CLIENTE VENDEDOR ON VENDEDOR.ID=RECEBER.IDVENDEDOR " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=RECEBER.IDFORMAPAGTO " +
                    "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID=RECEBER.IDBANCO " +
                    "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=RECEBER.IDSITBANCO ";

            query = query + " WHERE  "; //RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
            query = query + " SITUACAOTITULO.CODIGO < " + 4 + " ";  // NÃO PODE APARECER SE FOI DESCONTADO
            query = query + " AND RECEBER.VALORPAGO = " + 0 + " ";  // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
            // POIS NÃO FAZ BAIXA PAGAMENTO PARCIAL NESTE FORM
            query = query + " AND RECEBER.VALORBAIXANDO = " + 0 + " "; // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO
            // APENAS COM BOLETO DE NUMERO 0 (ZERO)
            query = query + " AND RECEBER.BOLETONRO = " + 0 + " ";
            // Numero de Nota
            if (clsParser.Int32Parse(tbxNroNota.Text) > 0)
            {
                query = query + " AND RECEBER.DUPLICATA >= " + clsParser.Int32Parse(tbxNroNota.Text) + " ";
            }

            // QUANDO INDICA O BANCO ONDE VAI PROCURAR AS DUPLICATAS QUE AINDA NÃO FORAM ENVIADAS PARA O BANCO  
            if (chxBancosTodos.Checked == true)
            { // Todos que não possuem o banco indicado  [ SITUACAOTIPOTITULO.CODIGO  ]
                query = query + " AND SITUACAOTIPOTITULO.CODIGO = 'BO' ";  // SELECIONANDO TUDO O QUE FOR BO
            }
            else
            {
                query = query + " AND RECEBER.IDBANCO = " + idBanco;  // SELECIONANDO O BANCO INDICADO
                //query = query + " AND RECEBER.IDBANCOINT = " + idBancoInt;  // SELECIONANDO O BANCO INDICADO
            }
            query = query + " ORDER BY RECEBER.VENCIMENTO, CLIENTE.COGNOME ";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtReceberAberto);

        }


        private void bwrReceberAberto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvReceberAberto.DataSource = dtReceberAberto;
                clsGridHelper.MontaGrid2(dgvReceberAberto, dtReceberAbertoColunas, true);
                dgvReceberAberto.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvReceberAberto.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvReceberAberto.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvReceberAberto.Columns["VALORJUROS"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvReceberAberto, 7);
                //GridHelper.SelecionaLinha(idAReceber, dgvAReceber, 1);
                PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoReceberAberto = false;

                // Qtde de Duplicatas // Somar Total em aberto
                Decimal total = 0;
                foreach (DataRow row in dtReceberAberto.Rows)
                {
                    total += clsParser.DecimalParse(row["VALOR"].ToString());
                }
                gbxReceberAberto.Text = "Títulos sem Boleto emitido: ( " + dtReceberAberto.Rows.Count + ") Titulos R$ = " + total.ToString("N2");

                bwrReceberBanco_Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceberAberto = false;
            }
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvReceberAberto);
            if (idReceberEnviar > 0 || id_AnteriorEnviar > 0 || id_ProximoEnviar > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(idReceberEnviar, dgvReceberAberto, "duplicata") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_ProximoEnviar,
                                   dgvReceberAberto, "duplicata") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_AnteriorEnviar,
                                dgvReceberAberto, "duplicata") == false)
                        {
                            if (dgvReceberAberto.Rows.Count > 0)
                            {
                                dgvReceberAberto.CurrentCell = dgvReceberAberto.Rows[0].Cells["duplicata"];
                            }
                        }
                    }
                }
            }
            else if (dgvReceberAberto.Rows.Count > 0)
            {
                dgvReceberAberto.CurrentCell = dgvReceberAberto.Rows[0].Cells["duplicata"];
            }

            if (dgvReceberAberto.CurrentRow != null)
            {
                idReceberEnviar = clsParser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvReceberAberto.CurrentRow.Index > 0)
                {
                    id_AnteriorEnviar = clsParser.Int32Parse(dgvReceberAberto.Rows[dgvReceberAberto.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_AnteriorEnviar = 0;
                }
                if (dgvReceberAberto.CurrentRow.Index < dgvReceberAberto.Rows.Count - 1)
                {
                    id_ProximoEnviar = clsParser.Int32Parse(dgvReceberAberto.Rows[dgvReceberAberto.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_ProximoEnviar = 0;
                }
            }
            else
            {
                idReceberEnviar = 0;
                id_AnteriorEnviar = 0;
                id_ProximoEnviar = 0;
            }
        }

        private void bwrReceberBanco_Run()
        {
            if (carregandoReceberBanco == false)
            {
                carregandoReceberBanco = true;

                bwrReceberBanco = new BackgroundWorker();
                bwrReceberBanco.DoWork += new DoWorkEventHandler(bwrReceberBanco_DoWork);
                bwrReceberBanco.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberBanco_RunWorkerCompleted);
                bwrReceberBanco.RunWorkerAsync();
            }
        }


        private void bwrReceberBanco_DoWork(object sender, DoWorkEventArgs e)
        {

            dtReceberBanco = new DataTable();
            String query = "SELECT RECEBER.ID,RECEBER.FILIAL,RECEBER.DUPLICATA,RECEBER.POSICAO " +
                    ",RECEBER.POSICAOFIM,RECEBER.EMISSAO,CLIENTE.COGNOME,RECEBER.VENCIMENTO " +
                    ",RECEBER.VENCIMENTOPREV,RECEBER.VALOR,RECEBER.VALORLIQUIDO,VENDEDOR.COGNOME AS [VENDEDOR] " +
                    ",SITUACAOTIPOTITULO.CODIGO + '-' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], RECEBER.BOLETONRO " +
                    ",RECEBER.DV,TAB_BANCOS.COGNOME AS [BANCO],RECEBER.VALORJUROS " +
                    "FROM RECEBER " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=RECEBER.IDCLIENTE " +
                    "LEFT JOIN CLIENTE VENDEDOR ON VENDEDOR.ID=RECEBER.IDVENDEDOR " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=RECEBER.IDFORMAPAGTO " +
                    "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID=RECEBER.IDBANCO " +
                    "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=RECEBER.IDSITBANCO ";

            query = query + " WHERE "; // RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
            query = query + " SITUACAOTITULO.CODIGO < " + 90 + " ";  // NÃO PODE APARECER SE FOI DESCONTADO
            query = query + " AND RECEBER.VALORPAGO = " + 0 + " ";  // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
            // POIS NÃO FAZ BAIXA PAGAMENTO PARCIAL NESTE FORM
            query = query + " AND RECEBER.VALORBAIXANDO = " + 0 + " "; // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO
            // APENAS COM BOLETO DE NUMERO 0 (ZERO)
            query = query + " AND RECEBER.BOLETONRO > " + 0 + " ";
            // Numero de Nota
            // QUANDO INDICA O BANCO ONDE VAI PROCURAR AS DUPLICATAS QUE AINDA NÃO FORAM ENVIADAS PARA O BANCO  
            query = query + " AND RECEBER.IDBANCOINT = " + idBancoInt;  // SELECIONANDO O BANCO INDICADO DESTINO
            query = query + " ORDER BY BOLETONRO DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtReceberBanco);

        }


        private void bwrReceberBanco_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvReceberBanco.DataSource = dtReceberBanco;
                clsGridHelper.MontaGrid2(dgvReceberBanco, dtReceberBancoColunas, true);
                dgvReceberBanco.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvReceberBanco.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvReceberBanco.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvReceberBanco.Columns["VALORJUROS"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvReceberBanco, 7);
                //GridHelper.SelecionaLinha(idAReceber, dgvAReceber, 1);
                PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                carregandoReceberBanco = false;
                // Qtde de Duplicatas // Somar Total colocado no banco de destino
                Decimal total = 0;
                foreach (DataRow row in dtReceberBanco.Rows)
                {
                    total += clsParser.DecimalParse(row["VALOR"].ToString());
                }
                gbxReceberBanco.Text = "Boletos Enviados para o Banco Cta (" + tbxBancoIntDes.Text + ") Qtde=(" + dtReceberBanco.Rows.Count + ")Titulos  R$ =  " + total.ToString("N2");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceberBanco = false;
            }
        }

        private void tspRetornar1_Click(object sender, EventArgs e)
        {
            //painelPrincipal = 0;
            //tclResolve();
            //tclDuplicatas.SelectedIndex = 0;
            tclDuplicatas.SelectedTab = tabEnviar;
        }

        private void btnConfiguracao_Click(object sender, EventArgs e)
        {
            tclDuplicatas.SelectedTab = tabBanco;
        }

        private void dgvReceberAberto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvReceberAberto.CurrentRow != null)
            {
                if (clsParser.DateTimeParse(dgvReceberAberto.CurrentRow.Cells["VENCIMENTO"].Value.ToString()) <
                   clsParser.DateTimeParse(dgvReceberAberto.CurrentRow.Cells["EMISSAO"].Value.ToString()))
                {
                    MessageBox.Show("Data de vencimento menor que a de emissão favor verificar!", "ApliSoft",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                idReceberAberto = (Int32)dgvReceberAberto.CurrentRow.Cells[0].Value;
                CarregarReceberEnviar(idReceberAberto);
            }
        }
        private void dgvReceberEnviar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvReceberEnviar.CurrentRow != null)
            {
                // Procurar antes as duplicatas ou duplica e liberar
                idReceberEnviar = clsParser.Int32Parse(dgvReceberEnviar.CurrentRow.Cells["POSICAO_RECEBERENVIAR"].Value.ToString());

                if (idReceberEnviar > 0)
                {
                    foreach (DataRow row in dtReceberEnviar.Rows)
                    {
                        if (clsParser.Int32Parse(row["POSICAO_RECEBERENVIAR"].ToString()) == idReceberEnviar)
                        {
                            Int32 qtcaracter = row["IDDUPLICATALINHA05"].ToString().Length;
                            Int32 posicaoatual = 0;
                            Int32 posicaoinicial = 0;
                            Int32 posicaofinal = 0;

                            String campo = "";

                            Boolean ok = false;
                            for (Int32 i = 1; i < qtcaracter; i++)
                            {
                                if (row["IDDUPLICATALINHA05"].ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
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
                                    campo = (row["IDDUPLICATALINHA05"].ToString().Substring(posicaoinicial, (posicaofinal - posicaoinicial)));
                                    campo = campo.Replace("|", "");
                                    posicaoinicial = (posicaofinal + 1);

                                    idReceberEnviar = clsParser.Int32Parse(campo);
                                    clsReceberInfo = clsReceberBLL.Carregar(idReceberEnviar, clsInfo.conexaosqldados);
                                    clsReceberInfo.boletonro = 0;
                                    clsReceberInfo.dv = "";
                                    clsReceberInfo.imprimir = "N";
                                    clsReceberInfo.valorjuros = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                                    clsReceberBLL.Alterar(clsReceberInfo, clsInfo.conexaosqldados);


                                }
                            }
                            dtReceberEnviar.Select("POSICAO_RECEBERENVIAR =" + dgvReceberEnviar.CurrentRow.Cells["POSICAO_RECEBERENVIAR"].Value.ToString())[0].Delete();
                            break;
                        }
                    }
                    dtReceberEnviar.AcceptChanges();
                    // Renumerar todos os numeros de boleto após ter apagado
                    tbxBoletoProximo1.Text = tbxNroBoleto.Text;
                    foreach (DataRow row in dtReceberEnviar.Rows)
                    {
                        // Calcular o Digito
                        tbxBoletoProximo1.Text = (clsParser.DecimalParse(tbxBoletoProximo1.Text) + 1).ToString();
                        if (clsParser.Int32Parse(tbxBancoDes.Text) == 1)
                        {
                            //Brasil
                            cDv = clsFinanceiro.Digito_Mod11_Brasil(tbxBoletoProximo1.Text);
                        }
                        if (clsParser.Int32Parse(tbxBancoDes.Text) == 1)
                        {
                            //Santander
                            cDv = clsFinanceiro.Digito_Mod11_Santander(tbxBoletoProximo1.Text.PadLeft(12, '0'));
                        }
                        
                        row["BOLETONRO"] = tbxBoletoProximo1.Text;
                        row["DV"] = cDv;
                    }
                    dtReceberEnviar.AcceptChanges();
                    tbxBoletoProximo1.Text = (clsParser.DecimalParse(tbxBoletoProximo1.Text) + 1).ToString();
                    //
                    bwrReceberAberto_Run();
                    bwrReceberEnviar_Run();
                }
                else
                {
                    MessageBox.Show("Não localizou .. o item ");
                }
            }
        }
        private void CriarReceberEnviar()
        {
            dtReceberEnviar = new DataTable();
            dtReceberEnviar.Columns.Add("POSICAO_RECEBERENVIAR", Type.GetType("System.Int32"));
            dtReceberEnviar.Columns.Add("IDRECEBER", Type.GetType("System.Int32"));
            dtReceberEnviar.Columns.Add("IDCLIENTE", Type.GetType("System.Int32"));
            dtReceberEnviar.Columns.Add("FILIAL", Type.GetType("System.Int32"));
            dtReceberEnviar.Columns.Add("DUPLICATA", Type.GetType("System.Int32"));
            dtReceberEnviar.Columns.Add("POSICAO", Type.GetType("System.Int32"));
            dtReceberEnviar.Columns.Add("POSICAOFIM", Type.GetType("System.Int32"));
            dtReceberEnviar.Columns.Add("EMISSAO", Type.GetType("System.DateTime"));
            dtReceberEnviar.Columns.Add("COGNOME", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("VENCIMENTO", Type.GetType("System.DateTime"));
            dtReceberEnviar.Columns.Add("VALOR", Type.GetType("System.Decimal"));
            dtReceberEnviar.Columns.Add("VALORLIQUIDO", Type.GetType("System.Decimal"));
            dtReceberEnviar.Columns.Add("VENDEDOR", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("FORMAPAGTO", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("BOLETONRO", Type.GetType("System.Decimal"));
            dtReceberEnviar.Columns.Add("DV", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("BANCO", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("VALORJUROS", Type.GetType("System.Decimal"));
            dtReceberEnviar.Columns.Add("VALORDESCONTO", Type.GetType("System.Decimal"));

            dtReceberEnviar.Columns.Add("NOMECLIENTE", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("ENDERECO", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("BAIRRO", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("CIDADE", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("ESTADO", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("CEP", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("CGC", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("IE", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("PESSOA", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("DIASPROTESTO", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("LINHA01", Type.GetType("System.String"));
            //dtReceberEnviar.Columns.Add("LINHA02", Type.GetType("System.String"));
            //dtReceberEnviar.Columns.Add("LINHA03", Type.GetType("System.String"));
            //dtReceberEnviar.Columns.Add("LINHA04", Type.GetType("System.String"));
            //dtReceberEnviar.Columns.Add("LINHA05", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("IDDUPLICATALINHA05", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("REFERENCIA", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("REFERENCIA1", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("REFERENCIA2", Type.GetType("System.String"));


            // Gravar na Duplicata 0 (zero) no Boleto as que tiverem Numero 1
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("UPDATE RECEBER SET BOLETONRO=@BOLETONRO WHERE BOLETONRO=1 ", scn);
            //scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsParser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["ID"].Value.ToString());
            scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = 0;
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
        }
        private void bwrReceberEnviar_Run()
        {
            if (carregandoReceberEnviar == false)
            {
                carregandoReceberEnviar = true;

                bwrReceberEnviar = new BackgroundWorker();
                bwrReceberEnviar.DoWork += new DoWorkEventHandler(bwrReceberEnviar_DoWork);
                bwrReceberEnviar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberEnviar_RunWorkerCompleted);
                bwrReceberEnviar.RunWorkerAsync();

            }
        }


        private void bwrReceberEnviar_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
            dtReceberEnviar = new DataTable();
            
            query = "SELECT RECEBER.ID,RECEBER.FILIAL,RECEBER.DUPLICATA,RECEBER.POSICAO " +
                    ",RECEBER.POSICAOFIM,RECEBER.EMISSAO,CLIENTE.COGNOME,RECEBER.VENCIMENTO " +
                    ",RECEBER.VENCIMENTOPREV,RECEBER.VALOR,RECEBER.VALORLIQUIDO,VENDEDOR.COGNOME AS [VENDEDOR] " +
                    ",SITUACAOTIPOTITULO.CODIGO + '-' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], RECEBER.BOLETONRO ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtReceberEnviar);
            */
        }


        private void bwrReceberEnviar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvReceberEnviar.DataSource = dtReceberEnviar;
                clsGridHelper.MontaGrid2(dgvReceberEnviar, dtReceberEnviarColunas, true);
                dgvReceberEnviar.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvReceberEnviar.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvReceberEnviar.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvReceberEnviar.Columns["VALORJUROS"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvReceberEnviar, 7);

                // Se ja tiver alguma duplicata para enviar não pode mais alterar o cabeçalho
                if (dtReceberEnviar.Rows.Count > 0)
                {
                    gbxParametros.Enabled = false;
                    gbxTipoEnvio.Enabled = false;

                    tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text;
                }
                else
                {
                    gbxParametros.Enabled = true;
                    gbxTipoEnvio.Enabled = true;
                }

                // Qtde de Duplicatas // Somar Total para Enviar
                Decimal total1 = 0;
                foreach (DataRow row in dtReceberEnviar.Rows)
                {
                    total1 = total1 + clsParser.DecimalParse(row["VALOR"].ToString());
                }
                gbxReceberEnviar.Text = "Títulos serão enviados para o Banco " + tbxBancoNome.Text + " ( " + dtReceberEnviar.Rows.Count + ") Titulos R$ = " + total1.ToString("N2");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceberEnviar = false;
            }
        }

        private void rbnEnviarNormal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RemessaBrasil()
        {
            //clsEmpresaGereInfo = clsEmpresaGereBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);

            // Abrir a empresa e pegar os dados
            Boolean continua1 = true;
            //dizeresnf = clsVisual.RemoveAcentos(dizeresnf);
            clsEmpresaInfo = clsEmpresaBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);

            /*    eEmp = PegSQL(cARQ1, cSQL1, 11, _
                  Array("NOME", "CGC", "ENDTIPO", "ENDERECO", "TIPONUMERO", "NUMEROEND", _ue
                        "ANDAR", "TIPOCOMPLE", "COMPLE", "CEP", "CIDADE"), */
            String ccgc = clsEmpresaInfo.cgc; // eEmp(1) = Tirace(eEmp(1))
            ccgc = ccgc.Replace(".", "");
            ccgc = ccgc.Replace("-", "");
            ccgc = ccgc.Replace("/", "");
            String cendereco = clsEmpresaInfo.endereco + " " + clsEmpresaInfo.numeroend.Trim() + " " +
                               clsEmpresaInfo.andar.Trim() + " " + clsEmpresaInfo.tipocomple.Trim() + " " + clsEmpresaInfo.comple.Trim();
            cendereco = clsVisual.RemoveAcentos(cendereco);
            String ccep = clsEmpresaInfo.cep; //TiraOut(eEmp(9))
            ccep = ccep.Replace(".", "");
            ccep = ccep.Replace("-", "");
            ccep = ccep.Replace("/", "");

            //pegar a conta e carteira do banco indicado
            clsBancosInfo = clsBancosBLL.Carregar(idBancoIntDes, clsInfo.conexaosqlbanco);
            /*              Array("BANCO", "AGENCIA", "CONTACOR", "CARTEIRA"), _*/
            String cagencia = clsBancosInfo.agencia; //Tirace(eBco(1)) 
            cagencia = cagencia.Replace(".", "");
            cagencia = cagencia.Replace("-", "");
            cagencia = cagencia.Replace("/", "");

            if (cagencia.Length != 5)
            {
                MessageBox.Show("Numero da agencia não possui os 5 digitos  ( " + cagencia + " )  ??? ");
                continua1 = false;
            }
            String cctacorrente = clsBancosInfo.contacor; //Tirace(eBco(2))
            cctacorrente = cctacorrente.Replace(".", "");
            cctacorrente = cctacorrente.Replace("-", "");
            cctacorrente = cctacorrente.Replace("/", "");

            if (cctacorrente.Length != 6)
            {  // desativado 
                // MessageBox.Show("Numero da conta não possui os 6 digitos sem os pontos e tracos  ( " + cctacorrente + " )  ??? ");
                // continua1 = false;
            }
            String ccarteira = clsBancosInfo.carteira; // tirace
            ccarteira = ccarteira.Replace(".", "");
            ccarteira = ccarteira.Replace("-", "");
            ccarteira = ccarteira.Replace("/", "");

            if (ccarteira.Length > 11)
            {
                MessageBox.Show("Numero da Carteira de Cobança > 11 Caracteres Nro Convenio+Carteira+VariacaoCarteira");
                continua1 = false;
            }
            else if (ccarteira.Length < 11)
            {
                MessageBox.Show("Numero da Carteira de Cobrança < 11 Caracteres Nro Convenio+Carteira+VariacaoCarteira");
                continua1 = false;
            }
            if (tbxBoletolinha03.Text.Length > 3)
            {
                tbxBoletolinha03.Text = "Duplicatas :" + tbxBoletolinha03.Text;
            }

            if (continua1 == true)
            {
                // Criar o nome do arquivo (dia + mes)
                tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text;


                String DataStr = DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00"); // +DateTime.Now.Year.ToString().Substring(2, 2);
                // NESTE CASO A EMPRESA REGISTRA O BOLETO  E O BANCO DO BRASIL IMPRIME O BOLETO
                nsequencia = clsParser.Int32Parse(tbxSequencia.Text) + 1;
                ArquivoBanco = "CBR" + DataStr + "." + nsequencia.ToString("000");
                // Verifica se ja não existe
                tbxArquivoDestino.Text = tbxArquivoDestino.Text + "\\" + ArquivoBanco;

                // caminho e nome do arquivo
                // vamos verificar se este arquivo existe
                if (File.Exists(tbxArquivoDestino.Text))
                {  // se existe criar um que não exista
                    Boolean terminou = false;
                    while (terminou == false)
                    {
                        nsequencia = nsequencia + 1;
                        ArquivoBanco = "CBR" + DataStr + "." + nsequencia.ToString("000");
                        tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text + "\\" + ArquivoBanco;
                        if (File.Exists(tbxArquivoDestino.Text))
                        {  // se existe criar um que não exista
                            // continuar
                        }
                        else
                        {
                            // sair
                            terminou = true;
                        }
                    }
                }
                Reg = 1;
                /*
                RegAA = 1
                CLI = 1
                CliStr = Str(CLI)
                Edicao = "" */

                //TextWriter Arquivo = new StreamWriter(ArquivoBanco);
                //Arquivo.Write("01");

                StreamWriter Arquivo = new StreamWriter(tbxArquivoDestino.Text);
                //Open "" + DataArq For Output As #1
                // '=============== Registro Header ====================

                Arquivo.Write("01");                                     // POS 1/2    [01] [A]  Tipo de Registro(2)
                Arquivo.Write(cagencia);                                 // POS 3/6    [04] [N]  Prefixo da Agencia
                // POS 7      [01] [A]  Dv da Agencia
                Arquivo.Write(cctacorrente.PadLeft(10, '0'));            // POS 8/16   [09] [N]  Codigo do Cedente - Conta
                // POS 17     [01] [A]  Dv - Codigo Cedente
                Arquivo.Write(ccarteira.Substring(6, 2).PadLeft(3, '0')); // POS 18/20  [03] [N] 'Carteira do convênio(2)
                Arquivo.Write(ccarteira.Substring(8, 3).PadRight(3, '0')); // POS 21/23  [03] [N] 'Variacao
                Arquivo.Write(ccarteira.Substring(0, 6).PadLeft(6, '0'));                     // POS 24/29  [06] [N] 'Numero do convenio
                Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaInfo.nome).PadRight(45, ' ').Substring(0,45));    // POS 30/74  [45] [A] 'nome do cedente
                Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaInfo.cognome).PadRight(10, ' ').Substring(0,10)); // POS 75/84  [10] [A] 'COGnome do cedente
                Arquivo.Write("01");                                     // POS 85/86  [02] [N] 'Tipo de Impressão
                Arquivo.Write(clsVisual.RemoveAcentos(cendereco).PadRight(60, ' '));               // POS 87/146 [60] [A] 'Endereço p/Devolução
                Arquivo.Write(ccep.PadRight(8, '0'));                     // POS 147/154 [08] [A] 'Cep p/Devolução
//                String cidade = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from cidades where id=" + clsEmpresaInfo.idcidade, "");
//                Arquivo.Write(clsVisual.RemoveAcentos(cidade).PadRight(20, ' ').Substring(0, 20));  // POS 155/174 [20] [A] 'PRAÇA PARA DEVOLUÇÃO
                Arquivo.Write("0000000");                                // POS 175/181 [07] [N] 'SEQUENCIAL DA REMESSA
                Arquivo.Write("N");                                      // POS 182/182 [01] [A] 'S= CONFERE N=NÃOCONFERE
                Arquivo.Write("".PadRight(4, ' '));                       //  POS 183/186 [04] [A] 'tipo de carne
                Arquivo.Write("CBR454".PadRight(8, ' '));                 // POS 187/194 [08] [N] 'IDENTIFICADOR DO ARQUIVO
                Arquivo.Write("".PadRight(3, ' '));                       // POS 195/197 [03] [A] 'RESERVADO BCO
                Arquivo.Write("".PadRight(53, ' '));                      // POS 198/250 [53] [A] 'PARA CARNE
                // Carriage Return e Line Feed ---                      ' Nova Linha
                //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                Arquivo.WriteLine();
                //==>  LINHA04 - DESCONTO (DELGA)
                //==>  LINHA05 - NUMERO DE DUPLICATAS ENVIADAS NO VALOR ACUMULADO POR CNPJ

                Reg += 1;
                Arquivo.Write("02");                         // POS 1/2  [02] [A]  tIPO DO REGISTRO
                Arquivo.Write("1");                          // POS 3/3  [01] [A]  1 FONTE INSTRUÇÃO 1
                Arquivo.Write("1");                          // POS 4/4  [01] [A]  1 FONTE INSTRUÇÃO 2
                Arquivo.Write("1");                          // POS 5/5  [01] [A]  1 FONTE INSTRUÇÃO 3
                Arquivo.Write("1");                          // POS 6/6  [01] [A]  1 FONTE INSTRUÇÃO 4
                //Arquivo.Write(tbxBoletolinha01.Text.PadRight(60,' ').Substring(0,60)); // POS   7/066 [60] [A]  Instrução 1
                //Substitui pelo cep para ver se resolve o problema do programa antigo
                
                Arquivo.Write(clsVisual.RemoveAcentos("Protestar " +
                    Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select NOME from TAB_BANCOSPROTESTAR where ID= "
                    + clsBancosInfo.idbcoprotestar, "")).PadRight(60, ' ').Substring(0, 60)); // lblBcoProtestar2.Text

//                Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaGereInfo.boletolinha01).PadRight(60, ' ').Substring(0, 60));  //tbxBoletolinha01.Text// POS  67/126 [60] [A]  Instrução 2
                Arquivo.Write(clsVisual.RemoveAcentos(tbxBoletolinha02.Text).PadRight(60, ' ').Substring(0, 60)); // POS  67/126 [60] [A]  Instrução 2
                //Arquivo.Write(tbxBoletolinha03.Text.PadRight(60,' ').Substring(0,60)); // POS 127/186 [60] [A]  Instrução 3
                Arquivo.Write(" ".PadRight(60, ' ').Substring(0, 60)); // POS 187/246 [60] [A]  Instrução 3
                Arquivo.Write(" ".PadRight(6, ' ').Substring(0, 4));   // POS 247/250 [04] [A]  espaços
                // Carriage Return e Line Feed ---                      ' Nova Linha
                //Arquivo.Write(Environment.NewLine); // "\r\n"; (char)13 + (char)10;  
                Arquivo.WriteLine();
                //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                /*Reg += Reg;
                Arquivo.Write("03");  // POS 1/2  [02] [A]   TIPO DO REGISTRO
                Arquivo.Write("1");   // POS 3/3  [01] [A]  1 FONTE INSTRUÇÃO 1
                Arquivo.Write("1");   // POS 4/4  [01] [A]  1 FONTE INSTRUÇÃO 2
                Arquivo.Write("1");   // POS 5/5  [01] [A]  1 FONTE INSTRUÇÃO 3
                Arquivo.Write(tbxBoletolinha01.Text.PadRight(80,' ').Substring(0,80)); // POS   6/085  [80] [A]  Instrução 1
                Arquivo.Write(tbxBoletolinha02.Text.PadRight(80,' ').Substring(0,80)); // POS  86/165  [80] [A]  Instrução 2
                Arquivo.Write(tbxBoletolinha03.Text.PadRight(80,' ').Substring(0,80)); // POS 166/245  [80] [A]  Instrução 3
                Arquivo.Write(" ".PadRight(5,' ').Substring(0,5)); // POS 246/250  [05] [A]  espaços
                // Carriage Return e Line Feed ---                      ' Nova Linha
                //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                Arquivo.WriteLine();
                //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Reg += Reg;
                Arquivo.Write("04");  // POS 1/2  [02] [A]   TIPO DO REGISTRO
                Arquivo.Write("1");   // POS 3/3  [01] [A]  1 FONTE INSTRUÇÃO 1
                Arquivo.Write("1");   // POS 4/4  [01] [A]  1 FONTE INSTRUÇÃO 2
                Arquivo.Write("1");   // POS 5/5  [01] [A]  1 FONTE INSTRUÇÃO 3
                Arquivo.Write(tbxBoletolinha01.Text.PadRight(80,' ').Substring(0,80)); // POS   6/085  [80] [A]  Instrução 1
                Arquivo.Write(tbxBoletolinha02.Text.PadRight(80,' ').Substring(0,80)); // POS  86/165  [80] [A]  Instrução 2
                Arquivo.Write(tbxBoletolinha03.Text.PadRight(80,' ').Substring(0,80)); // POS 166/245  [80] [A]  Instrução 3
                Arquivo.Write(" ".PadRight(5,' ').Substring(0,5)); // POS 246/250  [05] [A]  espaços
                // Carriage Return e Line Feed ---                      ' Nova Linha
                //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                Arquivo.WriteLine();
                //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                */
                Reg += 1;
                Arquivo.Write("05");                         // POS 1/2  [02] [A]  tIPO DO REGISTRO
                Arquivo.Write("1");                          // POS 3/3  [01] [A]  1 FONTE INSTRUÇÃO 1
                Arquivo.Write("1");                          // POS 4/4  [01] [A]  1 FONTE INSTRUÇÃO 2
                Arquivo.Write("1");                          // POS 5/5  [01] [A]  1 FONTE INSTRUÇÃO 3
                Arquivo.Write("1");                          // POS 6/6  [01] [A]  1 FONTE INSTRUÇÃO 4
                
                //Arquivo.Write(clsVisual.RemoveAcentos("Protestar " + lblBcoProtestar2.Text).PadRight(60,' ').Substring(0,60)); // POS   7/066 [60] [A]  Instrução 1
                Arquivo.Write(clsVisual.RemoveAcentos("Protestar " +
                    Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select NOME from TAB_BANCOSPROTESTAR where ID= "
                    + clsBancosInfo.idbcoprotestar, "")).PadRight(60, ' ').Substring(0, 60)); // POS   7/066 [60] [A]  Instrução 1
               
                //Arquivo.Write(clsVisual.RemoveAcentos(tbxBoletolinha01.Text).PadRight(60, ' ').Substring(0, 60)); //POS  67/126 [60] [A]  Instrução 2
                //Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaGereInfo.boletolinha01).PadRight(60, ' ').Substring(0, 60));  //POS  67/126 [60] [A]  Instrução 2
               
                Arquivo.Write(clsVisual.RemoveAcentos(tbxBoletolinha02.Text).PadRight(60, ' ').Substring(0, 60)); // POS 127/186 [60] [A]  Instrução 3 
                Arquivo.Write(clsVisual.RemoveAcentos(tbxBoletolinha03.Text).PadRight(60, ' ').Substring(0, 60)); // POS 187/246 [60] [A]  Instrução 3
                Arquivo.Write(" ".PadRight(6, ' ').Substring(0, 4));   // POS 247/250 [04] [A]  espaços
                // Carriage Return e Line Feed ---                      ' Nova Linha
                //Arquivo.Write(Environment.NewLine); // "\r\n";(char)13 + (char)10;  
                Arquivo.WriteLine();
                //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ///   Incluir as Duplicatas no arquivo 
                foreach (DataRow row in dtReceberEnviar.Rows)
                {
                    //'==========Registro de transação Tipo 1 - Detalhe-Remessa===
                    Reg += 1;
                    Arquivo.Write("10");  // POS 1/2    [02] [A]   ' Tipo de Registro, informar 01
                    if (clsClienteInfo.pessoa == "F") // POS 3/3    [01] [A]  ' CPF /CNPJ
                    {
                        Arquivo.Write("1");  // 1:cpf
                    }
                    else
                    {
                        Arquivo.Write("2");   // 2:cgc
                    }
                    Arquivo.Write(clsVisual.RemoveAcentos(row["CGC"].ToString().Trim()).PadLeft(15, '0').Substring(0, 15)); // POS 004/018 [15] [N]  ' cnpj do Cliente
                    Arquivo.Write(clsVisual.RemoveAcentos(row["NOMECLIENTE"].ToString().Trim()).PadRight(37, ' ').Substring(0, 37)); // POS 019/078 [60] [A]   Nome do cliente(37)
                    Arquivo.Write(" ".PadRight(23, ' ').Substring(0, 23));
                    Arquivo.Write(clsVisual.RemoveAcentos(row["ENDERECO"].ToString().Trim()).PadRight(37, ' ').Substring(0, 37)); // POS 079/138 [60] [A]  Endereço do cliente(37)
                    Arquivo.Write(" ".PadRight(23, ' ').Substring(0, 23));
                    Arquivo.Write(row["CEP"].ToString().PadRight(8, '0').Substring(0, 8));  //POS 139/146 [08] [N]  cep do cliente(8)
                    Arquivo.Write(clsVisual.RemoveAcentos(row["CIDADE"].ToString()).PadRight(18, ' ').Substring(0, 18)); //POS 147/164 [18] [A]  Cidade do cliente(8)
                    Arquivo.Write(clsVisual.RemoveAcentos(row["ESTADO"].ToString()).PadRight(2, ' ').Substring(0, 2)); //POS 165/166 [02] [A]  Uf do Cliente
                    //Data da Emissão                                    ' POS 167/172 [06] [N]  DDMMAA
                    String data = clsParser.DateTimeParse(row["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) +
                                  data.Substring(3, 2) +
                                  data.Substring(8, 2));
                    //Data da Vencimento                                 ' POS 173/178 [06] [N]  DDMMAA
                    data = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) +
                                  data.Substring(3, 2) +
                                  data.Substring(8, 2));


                    Arquivo.Write(clsBancosInfo.bcoaceite.PadLeft(1,' ').Substring(0,1));  // tbxBcoAceite.Text // POS 179/179 [01] [A]         'Aceite - S ou N(1)
                    Arquivo.Write(clsBancosInfo.bcoespecie.PadLeft(2, ' ').Substring(0,2)); //tbxBcoEspecie.Text  // POS 180/181 [02] [A] Sigla da Espécie
                    //  Numero do Boleto do Banco
                    Arquivo.Write(row["BOLETONRO"].ToString().PadLeft(11, '0').Substring(0, 11)); ; // ' POS 182/192 [11] [N]
                    Arquivo.Write(row["DV"].ToString().PadLeft(1, '0').Substring(0, 1)); ; // ' POS 193/193 [01] [N]
                    //  Nosso Numero de Duplicata
                    if (clsParser.Int32Parse(row["POSICAO"].ToString()) > 0)
                    {
                        Arquivo.Write((row["DUPLICATA"].ToString() + '-' + row["POSICAO"].ToString()).PadRight(15, ' ').Substring(0, 15)); // POS 194/208 [15] [A]
                    }
                    else
                    {
                        Arquivo.Write((row["DUPLICATA"].ToString() + "-1").PadRight(15, ' ').Substring(0, 15)); // POS 194/208 [15] [A]
                    }
                    Arquivo.Write(clsBancosInfo.bcomoeda.PadLeft(2, '0').Substring(0, 2)); // tbxBcoMoeda.Text.PadLeft(2, '0').Substring(0, 2)); // POS 209/210 [02] [N]   ' Tipo da Moeda
                    Arquivo.Write("0".PadRight(15, '0').Substring(0, 15)); //  POS 211/225 [15] [N]   ' Vl Moeda Variavel
                    // Valor do Titulo  //  POS 226/240 [15] [N]
                    String vvalor = clsParser.DecimalParse(row["VALOR"].ToString()).ToString();
                    vvalor = vvalor.Replace(".", "");
                    vvalor = vvalor.Replace(",", "");
                    Arquivo.Write(vvalor.ToString().PadLeft(15, '0').Substring(0, 15));

                    Arquivo.Write(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select codigo from TAB_BANCOSPROTESTAR where ID= "
                    + clsBancosInfo.idbcoprotestar, "").PadLeft(2, '0').Substring(0, 2)); //tbxBcoProtestar.Text.PadLeft(2, '0').Substring(0, 2) // POS 241/242 [02] [N]    'Dias para protesto(2)

                   //Arquivo.Write(clsEmpresaGereInfo.protestar.ToString().PadLeft(2, '0').Substring(0, 2)); //tbxBcoProtestar.Text.PadLeft(2, '0').Substring(0, 2) // POS 241/242 [02] [N]    'Dias para protesto(2)
                   
                    
                    Arquivo.Write(" ".PadRight(6, ' ').Substring(0, 6));  // POS 243/248 [06] [N]    'Data limite para desconto(8)
                    Arquivo.Write("00");                               //' POS 249/250 [02] [N]    'Qtde de parcelas
                    // Carriage Return e Line Feed ---                      ' Nova Linha
                    //Arquivo.Write(Environment.NewLine); // "\r\n";(char)13 + (char)10;  
                    Arquivo.WriteLine();
                }
                //  =============== Registro Trailer ====================
                Arquivo.Write("99");                  // POS 01/02 [002] [N]  TIPO DO REGISTRO = 99
                Arquivo.Write(Reg.ToString().PadLeft(15, '0').Substring(0, 15));  // POS 03/17 [015] [N]  Qtde de Registros
                Arquivo.Write(" ".PadRight(233, ' ').Substring(0, 233));  //POS 03/08 [233] [N]  Qtde de Registros
                Arquivo.Close();
                MessageBox.Show("Gravado com sucesso!");
            }
        }
        private void RemessaItau()
        {
            //clsEmpresaGereInfo = clsEmpresaGereBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            // Abrir a empresa e pegar os dados
            Boolean continua1 = true;
            //dizeresnf = clsVisual.RemoveAcentos(dizeresnf);
            clsEmpresaInfo = clsEmpresaBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            // Se for HS
            if (clsInfo.zempresacliente_cognome.Contains("HOPENS") || clsInfo.zempresacliente_cognome.Contains("ENVIDRAÇAMENTO"))
            {
                if (clsParser.Int32Parse(tbxBancoIntDes.Text) == 60)
                {
                    clsEmpresaInfo.nome = "hs envidracamento c m e ltda";
                    clsEmpresaInfo.cgc = "17.164.548/0001-70";
                }
            }
            String cnpj = clsEmpresaInfo.cgc; // eEmp(1) = Tirace(eEmp(1))
            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("-", "");
            cnpj = cnpj.Replace("/", "");
/*
            String cendereco = clsEmpresaInfo.endtipo + " " + clsEmpresaInfo.endereco + " " + clsEmpresaInfo.numeroend.Trim() + " " +
                               clsEmpresaInfo.andar.Trim() + " " + clsEmpresaInfo.tipocomple.Trim() + " " + clsEmpresaInfo.comple.Trim();
            cendereco = clsVisual.RemoveAcentos(cendereco);
            String ccep = clsEmpresaInfo.cep; //TiraOut(eEmp(9))
            ccep = ccep.Replace(".", "");
            ccep = ccep.Replace("-", "");
            ccep = ccep.Replace("/", "");*/

            //pegar a conta e carteira do banco indicado
            clsBancosInfo = clsBancosBLL.Carregar(idBancoIntDes, clsInfo.conexaosqlbanco);
            /*              Array("BANCO", "AGENCIA", "CONTACOR", "CARTEIRA"), _*/
            String cagencia = clsBancosInfo.agencia; //Tirace(eBco(1)) 
            cagencia = cagencia.Replace(".", "");
            cagencia = cagencia.Replace("-", "");
            cagencia = cagencia.Replace("/", "");

            if (cagencia.Length != 4)
            {
                MessageBox.Show("Numero da agencia não possui os 4 digitos  ( " + cagencia + " )  ??? ");
                continua1 = false;
            }
            String cctacorrente = clsBancosInfo.contacor; //Tirace(eBco(2))
            cctacorrente = cctacorrente.Replace(".", "");
            cctacorrente = cctacorrente.Replace("-", "");
            cctacorrente = cctacorrente.Replace("/", "");

            if (cctacorrente.Length != 6)
            {  // desativado 
                //MessageBox.Show("Numero da conta não possui os 6 digitos sem os pontos e tracos  ( " + cctacorrente + " )  ??? ");
                //continua1 = false;
            }
            String ccarteira = clsBancosInfo.carteira; // tirace
            ccarteira = ccarteira.Replace(".", "");
            ccarteira = ccarteira.Replace("-", "");
            ccarteira = ccarteira.Replace("/", "");

/*            if (ccarteira.Length > 11)
            {
                MessageBox.Show("Numero da Carteira de Cobança > 11 Caracteres Nro Convenio+Carteira+VariacaoCarteira");
                continua1 = false;
            }
            else if (ccarteira.Length < 11)
            {
                MessageBox.Show("Numero da Carteira de Cobrança < 11 Caracteres Nro Convenio+Carteira+VariacaoCarteira");
                continua1 = false;
            }
            if (tbxBoletolinha03.Text.Length > 3)
            {
                tbxBoletolinha03.Text = "Duplicatas :" + tbxBoletolinha03.Text;
            } */

            if (continua1 == true)
            {
                // Criar o nome do arquivo (dia + mes)
                tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text;
                String DataStr = DateTime.Now.Year.ToString().Substring(2, 2)+DateTime.Now.Month.ToString("00")+DateTime.Now.Day.ToString("00");
                String DataStrDDMMAA = DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString().Substring(2, 2);
                ArquivoBanco = DataStr + ".rem";
                // Verifica se ja não existe
                tbxArquivoDestino.Text = tbxArquivoDestino.Text + "\\" + ArquivoBanco;
                // caminho e nome do arquivo 
                if (File.Exists(tbxArquivoDestino.Text))
                {  // se existe criar um que não exista
                    Boolean terminou = false;
                    while (terminou == false)
                    {
                        String xcampo = ArquivoBanco.Substring(6, 2);
                        Int32 seq = clsParser.Int32Parse(xcampo) + 1;
                        ArquivoBanco = ArquivoBanco.Substring(0, 6) + seq.ToString().PadLeft(2, '0') + ".rem";

                        tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text + "\\" + ArquivoBanco;
                        if (File.Exists(tbxArquivoDestino.Text))
                        {  // se existe criar um que não exista
                            // continuar
                            terminou = false;
                        }
                        else
                        {
                            // sair
                            terminou = true;
                        }
                    }
                }
                Reg = 1;
                /*
                RegAA = 1
                CLI = 1
                CliStr = Str(CLI)
                Edicao = "" */

                StreamWriter Arquivo = new StreamWriter(tbxArquivoDestino.Text);
                //Open "" + DataArq For Output As #1
                // '=============== Registro Header ====================

                Arquivo.Write("0");                                      // POS 01 [01]   'Identificação do Registro(1)
                Arquivo.Write("1");                                      // POS 02 [01]   'Identificação do Movimento de Remessa(1)
                Arquivo.Write("REMESSA");                                // POS 03 [07]   'Extensão do Nome do Movimento
                Arquivo.Write("01");                                     // POS 10 [02]   'Identificação do Tipo do Serviço
                Arquivo.Write("COBRANCA       ");                        // POS 12 [15]   'Extensão do Nome do Movimento
                Arquivo.Write(cagencia);                                 // POS 27 [04]   'Identificação Agencia da Conta
                Arquivo.Write("00");                                     // POS 31 [02]   'Complemento de Registro
                Arquivo.Write(cctacorrente);                             // POS 33 [05]   'Numero da Conta da Empresa
                                                                         // POS 38 [01]   'Digito de Conferencia
                Arquivo.Write("".PadRight(8, ' '));                       // POS 39 [08]   'Complemento de Registro
                Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaInfo.nome).PadRight(30, ' ').Substring(0, 30));    // POS 47  [30]  'nome da Empresa por Extenso
                Arquivo.Write("341");                                    // POS 77 [03]   'Numero do Banco na Camara de Compensação                                                                         
                Arquivo.Write("BANCO ITAU SA  ");                        // POS 80 [15]   'Nome do Banco na Camara de Compensação                                                                         
                Arquivo.Write(DataStrDDMMAA);                            // POS 95 [06]   'Data da Geração do Arquivo
                Arquivo.Write("".PadRight(294, ' '));                    // POS 101 [294] 'Complemento do Registro
                Arquivo.Write("000001");                                 // POS 395 [03]   'Numero sequencial de arquivo
                Arquivo.WriteLine();                                     // Carriage Return e Line Feed ---                      ' Nova Linha
                //'==========Registro de transação Tipo 1 - Detalhe-Remessa===
                ///   Incluir as Duplicatas no arquivo 
                foreach (DataRow row in dtReceberEnviar.Rows)
                {
                    Reg += 1;
                    Arquivo.Write("1");                                      // POS 01 [01]   'Identificação do Registro
                    Arquivo.Write("02");                                     // POS 02 [02]   'Tipo Inscrição da Empresa Emitente 
                    Arquivo.Write(cnpj);                                     // POS 04 [14]   'Nº DE INSCRIÇÃO DA EMPRESA (CPF/CNPJ)
                    Arquivo.Write(cagencia);                                 // POS 18 [04]   'Identificação Agencia da Conta
                    Arquivo.Write("00");                                     // POS 22 [02]   'Complemento de Registro
                    Arquivo.Write(cctacorrente);                             // POS 24 [05]   'Numero da Conta da Empresa
                                                                             // POS 29 [01]   'Digito de Conferencia
                    Arquivo.Write("".PadRight(4, ' '));                      // POS 30 [04]   'Complemento do Registro
                    Arquivo.Write("".PadRight(4, ' '));                      // POS 34 [04]   'Instrução/Alegação a ser Cancelada(4)

                    String Titulo = row["DUPLICATA"].ToString().PadLeft(8, '0') + '-' + row["POSICAO"].ToString().PadLeft(2, '0');
                    Arquivo.Write(Titulo.PadRight(25, ' ').Substring(0,25));  // POS 38 [25]   'IDENTIFICAÇÃO DO TÍTULO NA EMPRESA
                    // POS 63 [08]   'IDENTIFICAÇÃO DO TÍTULO NO BANCO - numero boleto / nosso numero
                    if (ccarteira == "112")
                    {
                        Arquivo.Write("".PadLeft(8, '0'));
                    }
                    else
                    {
                        Arquivo.Write((row["BOLETONRO"].ToString() + row["DV"].ToString()).PadLeft(8, '0').Substring(0, 8));
                    }

                    Arquivo.Write("".PadRight(13, '0'));                      // POS 71 [13]   'Qtde de Moeda Variavel
                    Arquivo.Write(ccarteira);                                 // POS 84 [03]   'NÚMERO DA CARTEIRA NO BANCO
                    Arquivo.Write("".PadRight(21, ' '));                      // POS 87 [13]   'Identificação operação do banco
                    Arquivo.Write("I");                                       // POS 108 [1]   'Codigo da Carteira
                    Arquivo.Write("01");                                      // POS 109 [2]   'identificação da ocorrencia
                    // POS 111 [10]  'Nº DO DOCUMENTO DE COBRANÇA (DUPL.,NP ETC.)
                    String Duplicata = row["DUPLICATA"].ToString().PadLeft(6, '0') + '-' + row["POSICAO"].ToString().PadLeft(2, '0');
                    Arquivo.Write(Duplicata.PadRight(10,' '));                               
                    // POS 121 [06]  DATA DE VENCIMENTO DO TÍTULO
                    String data = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) + data.Substring(3, 2) + data.Substring(8, 2)); 
                    // POS 127 [13]  Valor do Titulo  
                    String vvalor = clsParser.DecimalParse(row["VALOR"].ToString()).ToString("N2");
                    vvalor = vvalor.Replace(".", "");
                    vvalor = vvalor.Replace(",", "");
                    Arquivo.Write(vvalor.ToString().PadLeft(13, '0'));
                    Arquivo.Write("341");                                     // POS 140 [3]   'Nº DO BANCO NA CÂMARA DE COMPENSAÇÃO
                    Arquivo.Write("00000");                                   // POS 143 [5]   'AGÊNCIA ONDE O TÍTULO SERÁ COBRADO
                    Arquivo.Write("01");                                      // POS 148 [2]   'Especie do titulo 01-duplicata mercantil
                    Arquivo.Write("N");                                       // POS 150 [1]   'Aceite do Titulo N= NÃO ACEITE  S= SIM ACEITE(1)
                    // POS 151 [06]  DATA DE EMISSAO DO TÍTULO
                    data = clsParser.DateTimeParse(row["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) + data.Substring(3, 2) + data.Substring(8, 2)); 
                    // POS 157 [02]  PRIMEIRA INSTRUÇÃO PARA COBRANÇA
                    Arquivo.Write("05");                                      // (05)=Receber conforme instruções no proprio titulo
                    // POS 159 [02]  SEGUNDA INSTRUÇÃO PARA COBRANÇA
                    if (clsParser.Int32Parse(row["DIASPROTESTO"].ToString()) > 0)
                    {
                        Arquivo.Write("09");                                  // (09)=Protestar
                    }
                    else
                    {
                        Arquivo.Write("10");                                  // (10)=Não Protestar
                    }
                    // POS 161 [13]   'Valor de  Mora por 1 dia de Atraso
                    vvalor = clsParser.DecimalParse(row["VALORJUROS"].ToString()).ToString("N2");
                    vvalor = vvalor.Replace(".", "");
                    vvalor = vvalor.Replace(",", "");
                    Arquivo.Write(vvalor.ToString().PadLeft(13, '0'));
                    Arquivo.Write("".PadRight(6,'0'));                      // POS 174 [06]   'Data limite para desconto
                    Arquivo.Write("".PadRight(13,'0'));                     // POS 180 [13]   'Valor Desconto a ser concedido
                    Arquivo.Write("".PadRight(13,'0'));                     // POS 193 [13]   'VALOR DO I.O.F. RECOLHIDO P/ NOTAS SEGURO
                    Arquivo.Write("".PadRight(13,'0'));                     // POS 206 [13]   'VALOR DO ABATIMENTO A SER CONCEDIDO
                    if (clsClienteInfo.pessoa == "F")                       // POS 219 [01]   'IDENTIFICAÇÃO DO TIPO DE INSCRIÇÃO/PAGADOR
                    {
                        Arquivo.Write("01");  // 1:cpf
                    }
                    else
                    {
                        Arquivo.Write("02");   // 2:cgc
                    }
                    Arquivo.Write(row["CGC"].ToString().Trim().PadRight(14, ' ').Substring(0, 14));      // POS 221 [14] 'CPF/cnpj do Cliente
                    Arquivo.Write(clsVisual.RemoveAcentos(row["NOMECLIENTE"].ToString()).Trim().PadRight(31, ' ').Substring(0, 30)); // POS 235 [30] NOME DO PAGADOR
                    Arquivo.Write("".PadRight(10,' '));                                                                       // POS 265 [10] cOMPLEMENTO
                    Arquivo.Write(clsVisual.RemoveAcentos(row["ENDERECO"].ToString()).Trim().PadRight(40, ' ').Substring(0, 40)); // POS 275 [40] ENDERECO
                    Arquivo.Write(clsVisual.RemoveAcentos(row["BAIRRO"].ToString()).Trim().PadRight(12, ' ').Substring(0, 12).PadRight(12,' '));                                                                   // POS 315 [12] Bairro
                    Arquivo.Write(clsVisual.RemoveAcentos(row["CEP"].ToString()).Trim().PadRight(8, ' ').Substring(0, 8));   // POS 327 [8] cep
                    Arquivo.Write(clsVisual.RemoveAcentos(row["CIDADE"].ToString()).PadRight(15, ' ').Substring(0, 15));     // POS 335 [15] Cidade do cliente(8)
                    Arquivo.Write(clsVisual.RemoveAcentos(row["ESTADO"].ToString()).PadRight(2, ' ').Substring(0, 2));       // POS 350 [02] Uf do Cliente
                    Arquivo.Write("".PadRight(30,' '));                                                                   // POS 352 [12] NOME DO SACADOR OU AVALISTA
                    Arquivo.Write("".PadRight(4,' '));                                                                   // POS 382 [4] COMPLEMENTO DO REGISTRO
                    // POS 386 [06]  DDMMAA   Data da Vencimento 
                    data = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) + data.Substring(3, 2) + data.Substring(8, 2));
                    Arquivo.Write(row["DIASPROTESTO"].ToString().Trim().PadLeft(2, '0').Substring(0, 2));                  // POS 392 [02]  QTDE DIAS PROTESTO
                    Arquivo.Write("".PadRight(1,' '));                                                                  // POS 394 [1] COMPLEMENTO DO REGISTRO
                    Arquivo.Write(Reg.ToString().PadLeft(6,'0'));                                                       // POS 395 [06] Nº SEQÜENCIAL DO REGISTRO NO ARQUIVO
                    // Carriage Return e Line Feed ---                      ' Nova Linha
                    Arquivo.WriteLine();
                }
                //  =============== Registro Trailer ====================
                Arquivo.Write("9");                  //  [001] [N]  TIPO DO REGISTRO = 99
                Arquivo.Write("".PadRight(393,' '));  // [393] [N]  Qtde de Registros
                Reg = Reg + 1;
                Arquivo.Write(Reg.ToString().PadLeft(6,'0')); 
                Arquivo.WriteLine();
                Arquivo.Close();
                MessageBox.Show("Gravado com sucesso!");
            }
        }

        private void RemessaSantander()
        {
            // Abrir a empresa e pegar os dados
            Boolean continua1 = true;
            //dizeresnf = clsVisual.RemoveAcentos(dizeresnf);

            //clsEmpresaGereInfo = clsEmpresaGereBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            clsEmpresaInfo = clsEmpresaBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            String ccgc = clsEmpresaInfo.cgc; // eEmp(1) = Tirace(eEmp(1))
            ccgc = ccgc.Replace(".", "");
            ccgc = ccgc.Replace("-", "");
            ccgc = ccgc.Replace("/", "");
            String cendereco = clsEmpresaInfo.endereco + " " + clsEmpresaInfo.numeroend.Trim() + " " +
                               clsEmpresaInfo.andar.Trim() + " " + clsEmpresaInfo.tipocomple.Trim() + " " + clsEmpresaInfo.comple.Trim();
            cendereco = clsVisual.RemoveAcentos(cendereco);
            String ccep = clsEmpresaInfo.cep; //TiraOut(eEmp(9))
            ccep = ccep.Replace(".", "");
            ccep = ccep.Replace("-", "");
            ccep = ccep.Replace("/", "");


            //pegar a conta e carteira do banco indicado
            clsBancosInfo = clsBancosBLL.Carregar(idBancoIntDes, clsInfo.conexaosqlbanco);
            String cagencia = clsBancosInfo.agencia; //Tirace(eBco(1)) 
            cagencia = cagencia.Replace(".", "");
            cagencia = cagencia.Replace("-", "");
            cagencia = cagencia.Replace("/", "");

            /*
            if (cagencia.Length != 5)
            {
                MessageBox.Show("Numero da agencia não possui os 5 digitos  ( " + cagencia + " )  ??? ");
                continua1 = false;
            }
             */
            String cctacorrente = clsBancosInfo.contacor; //Tirace(eBco(2))
            cctacorrente = cctacorrente.Replace(".", "");
            cctacorrente = cctacorrente.Replace("-", "");
            cctacorrente = cctacorrente.Replace("/", "");
            /*
                        if (cctacorrente.Length != 6)
                        {  // desativado 
                            // MessageBox.Show("Numero da conta não possui os 6 digitos sem os pontos e tracos  ( " + cctacorrente + " )  ??? ");
                            // continua1 = false;
                        } */

            String ccarteira = clsBancosInfo.carteira; // tirace
            /*
            ccarteira = ccarteira.Replace(".", "");
            ccarteira = ccarteira.Replace("-", "");
            ccarteira = ccarteira.Replace("/", "");

            if (ccarteira.Length > 11)
            {
                MessageBox.Show("Numero da Carteira de Cobança > 11 Caracteres Nro Convenio+Carteira+VariacaoCarteira");
                continua1 = false;
            }
            else if (ccarteira.Length < 11)
            {
                MessageBox.Show("Numero da Carteira de Cobrança < 11 Caracteres Nro Convenio+Carteira+VariacaoCarteira");
                continua1 = false;
            }
            if (tbxBoletolinha03.Text.Length > 3)
            {
                tbxBoletolinha03.Text = "Duplicatas :" + tbxBoletolinha03.Text;
            }
            */
            if (continua1 == true)
            {
                // Criar o nome do arquivo (dia + mes + ano)
                tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text;

                String DataStr = DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString().Substring(2, 2);
                nsequencia = clsParser.Int32Parse(tbxSequencia.Text) + 1; // Sequencia das Transmissões Header

                Int32 nsequenciaarquivo = 1;
                ArquivoBanco = "CB" + DataStr + "." + nsequenciaarquivo.ToString("0000");
                // Verifica se ja não existe
                tbxArquivoDestino.Text = tbxArquivoDestino.Text + "\\" + ArquivoBanco;

                // caminho e nome do arquivo
                // vamos verificar se este arquivo existe
                if (File.Exists(tbxArquivoDestino.Text))
                {  // se existe criar um que não exista
                    Boolean terminou = false;
                    while (terminou == false)
                    {
                        nsequenciaarquivo = nsequenciaarquivo + 1;
                        ArquivoBanco = "CB" + DataStr + "." + nsequenciaarquivo.ToString("0000");
                        tbxArquivoDestino.Text = clsInfo.arquivos + tbxBancoNome.Text + "\\" + ArquivoBanco;
                        if (File.Exists(tbxArquivoDestino.Text))
                        {  // se existe criar um que não exista
                            // continuar
                        }
                        else
                        {
                            // sair
                            terminou = true;
                        }
                    }
                }
                
                /*
                RegAA = 1
                CLI = 1
                CliStr = Str(CLI)
                Edicao = "" */

                //TextWriter Arquivo = new StreamWriter(ArquivoBanco);
                //Arquivo.Write("01");

                StreamWriter Arquivo = new StreamWriter(tbxArquivoDestino.Text);
                //Open "" + DataArq For Output As #1
                // '=============== Registro Header [arquivo remessa] ====================
                Reg = 1;
                Arquivo.Write(clsBancosInfo.banco.ToString().PadLeft(3, '0').Substring(0,3)); //"033"  // POS 1/3    [03] [N]  Codigo do Banco na Compensação
                Arquivo.Write("0000");                                   // POS 4/7    [04] [N]  Lote do Serviço 
                Arquivo.Write("0");                                      // POS 8/8    [01] [N]  Tipo do Serviço
                Arquivo.Write("".PadRight(8, ' '));                      // POS 9/16   [08] [A]  Brancos
                if (clsEmpresaInfo.pessoa.ToString().ToUpper() == "J")
                {
                    Arquivo.Write("2");                                     // POS 17/17  [01] [N]  1=cpf 2=cnpj
                }
                else
                {
                    Arquivo.Write("1");
                }

                Arquivo.Write(clsEmpresaInfo.cgc.PadLeft(15, '0').Substring(0, 15)); //"009095695000107") POS 18/32  [15] [N]  Nro de Incrição da empresa
                Arquivo.Write(clsBancosInfo.codigodetransmissao.PadLeft(15, '0').Substring(0,15)); //"371700005759382") POS 33/47  [15] [N]  Codigo de Transmissão
                
                Arquivo.Write("".PadRight(25, ' '));                     // POS 48/72  [25] [A]  Brancos
                Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaInfo.nome).PadRight(30, ' ').Substring(0, 30));   // POS 73/102 [30] [A]  nome do cedente
                Arquivo.Write(clsBancosInfo.nome.PadRight(30, ' ').Substring(0,30)); // BANCO SANTANDER" // POS 103/132 [30] [A]  Banco Santander
                Arquivo.Write("".PadRight(10, ' '));                     // POS 133/142 [10] [A]  Brancos
                Arquivo.Write("1");                                      // POS 143/143 [01] [N]  Remessa
                String Data = (DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("0000")).ToString();
                Arquivo.Write(Data);                                     // POS 144/151 [08] [N]  Data de Geração do Arquivo (DDMMAAAA)
                Arquivo.Write("".PadRight(6, ' '));                      // POS 152/157 [06] [A]  Brancos
                Arquivo.Write(nsequencia.ToString().PadLeft(6, '0').Substring(0,6));    // POS 158/163 [06] [N]  Nro Sequencial do Arquivo
                Arquivo.Write("040");                                    // POS 164/166 [03] [N]  Nro Versão do Layout Arquivo
                Arquivo.Write("".PadRight(74, ' '));                     // POS 167/240 [06] [A]  Brancos
                // Carriage Return e Line Feed ---Environment.NewLine; "\r\n";(char)13 + (char)10;  
                Arquivo.WriteLine();

                // '=============== Registro Header [lote remessa] ====================
                Reg += 1;
                Arquivo.Write(clsBancosInfo.banco.ToString().PadLeft(3, '0').Substring(0,3)); // "033" // POS 001/003 [03] [N]  Codigo do Banco na Compensação
                Arquivo.Write("0001");                                   // POS 004/007 [04] [N]  Lote do Serviço 
                Arquivo.Write("1");                                       // POS 008/008 [01] [N]  Tipo do registro
                Arquivo.Write("R");                                      // POS 009/009 [01] [A]  Tipo da Ooperação R(Remessa)
                Arquivo.Write("01");                                     // POS 010/011 [02] [N]  Tipo do Serviço 01(Cobrança)
                Arquivo.Write("".PadRight(2, ' '));                      // POS 012/013 [02] [A]  Brancos
                Arquivo.Write("030");                                    // POS 014/016 [03] [N]  Nro Versão do Layout Lote
                Arquivo.Write("".PadRight(1, ' '));                      // POS 017/017 [01] [A]  Brancos
                if(clsEmpresaInfo.pessoa.ToString().ToUpper() == "J")
                {
                    Arquivo.Write("2");                                      // POS 018/018 [01] [N]  1=cpf 2=cnpj
                }
                else
                {
                    Arquivo.Write("1");
                }
                Arquivo.Write(clsEmpresaInfo.cgc.PadLeft(15, '0').Substring(0, 15)); //"009095695000107" // POS 019/033 [15] [N]  Nro de Incrição da empresa
                Arquivo.Write("".PadRight(20, ' '));                     // POS 034/053 [20] [A]  Brancos
                Arquivo.Write(clsBancosInfo.codigodetransmissao.PadLeft(15, '0').Substring(0, 15)); //"371700005759382" // POS 054/068 [15] [N]  Codigo de Transmissão
                
                Arquivo.Write("".PadRight(5, ' '));                      // POS 069/073 [05] [A]  Brancos
                Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaInfo.nome).PadRight(30).Substring(0, 30));   // POS 074/103 [30] [A]  nome do cedente


//                Arquivo.Write(clsVisual.RemoveAcentos(clsEmpresaGereInfo.boletolinha01).PadRight(40, ' ').Substring(0, 40)); // tbxBoletolinha01.Text // POS  104/143 [40] [A]  Mensagem 1

                Arquivo.Write(clsVisual.RemoveAcentos(tbxBoletolinha02.Text).PadRight(40, ' ').Substring(0, 40)); // POS  144/183 [40] [A]  Mensagem 2
                
                Arquivo.Write("0".PadRight(8, '0'));                     // POS 184/191 [08] [N]  Nro da Remessa/Retorno
                Arquivo.Write(Data);                                     // POS 192/199 [08] [N]  Data de Geração do Arquivo (DDMMAAAA)
                Arquivo.Write(" ".PadRight(41, ' '));                    // POS 200/240 [41] [A]  Brancos
                // Carriage Return e Line Feed ---                      ' Nova Linha
                //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                Arquivo.WriteLine();
                ///   Incluir as Duplicatas no arquivo 
                
                // REGISTRO DETALHE
                Int32 Reg_P_Q_R = 0;
               
                foreach (DataRow row in dtReceberEnviar.Rows)
                { //'==========Registro Detalhe SEGMENTO P
                    Reg_P_Q_R += 1;
                    Reg += 1;
                    //Reg_P += 1;
                    Arquivo.Write(clsBancosInfo.banco.ToString().PadLeft(3, '0').Substring(0,3)); //"033")                               // POS 001/003 [03] [A] Codigo do Banco na Compensação
                    Arquivo.Write("0001");                               // POS 004/007 [04] [N] Nro do Lote Remessa
                    Arquivo.Write("3");                                  // POS 008/008 [01] [N] Tipo de registro
                    Arquivo.Write(Reg_P_Q_R.ToString().PadLeft(5, '0'));        // POS 009/013 [05] [N] Nro Sequencial do Registro de Lote
                    Arquivo.Write("P");                                  // POS 014/014 [01] [N] Cod. Segmento do registro detalhe
                    Arquivo.Write(" ");                                  // POS 015/015 [01] [A] Brancos
                    Arquivo.Write("01");                                 // POS 016/017 [02] [N] 01 - Entrada Titulo - codgo de moviemnto de remessa
                    Arquivo.Write(cagencia.PadLeft(4,'0').Substring(0,4)); // POS 018/021 [04] [N] Agencia do cedente 
                    Arquivo.Write("0");                                    // POS 022/022 [01] [N] Digito da Agencia do cedente
                    Arquivo.Write(cctacorrente.PadLeft(9,'0').Substring(0,9)); // POS 023/031 [09] [N] Numero da Cta Corrente
                    Arquivo.Write("0");                                        // POS 032/032 [01] [N] Digito da Cta Corrente
                    Arquivo.Write(cctacorrente.PadLeft(9,'0').Substring(0,9)); // POS 033/041 [09] [N] Numero da Cta Cobrança
                    Arquivo.Write("0");                                  // POS 042/042 [01] [N] Digito da Cta Cobrança - para a newpainting
                    Arquivo.Write("".PadRight(2, ' '));                  // POS 043/044 [02] [A] Brancos
                    Arquivo.Write((row["BOLETONRO"].ToString() + row["DV"].ToString()).PadLeft(13, '0').Substring(0, 13));  // ' POS 045/057 [13] [N]
                    Arquivo.Write("5");                                  // POS 058/058 [01] [N] Tipo Cobranca
                    Arquivo.Write("1");                                  // POS 059/059 [01] [N] Forma Cadastramento
                    Arquivo.Write("2");                                  // POS 060/060 [01] [N] Tipo Documento 1-Tradicional 2-Escritural
                    Arquivo.Write("".PadRight(1, ' '));                  // POS 061/061 [01] [A] Brancos
                    Arquivo.Write("".PadRight(1, ' '));                  // POS 062/062 [01] [A] Brancos
                    //  Nosso Numero de Duplicata
                    if (clsParser.Int32Parse(row["POSICAO"].ToString()) > 0)
                    {
                        Arquivo.Write((row["DUPLICATA"].ToString() + '-' + row["POSICAO"].ToString()).PadRight(15, ' ').Substring(0, 15)); // POS 063/077 [15] [A]
                    }
                    else
                    {
                        Arquivo.Write((row["DUPLICATA"].ToString() + "-1").PadRight(15, ' ').Substring(0, 15)); // POS 063/077 [15] [A]
                    }
                    //Data da Vencimento
                    String data = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) +
                                  data.Substring(3, 2) +
                                  data.Substring(6, 4));                 // POS 078/085 [08] [N] DDMMAAAA
                    String vvalor = clsParser.DecimalParse(row["VALOR"].ToString()).ToString();
                    vvalor = vvalor.Replace(".", "");
                    vvalor = vvalor.Replace(",", "");
                    Arquivo.Write(vvalor.ToString().PadLeft(15, '0').Substring(0, 15)); // POS 086/100 [15] [N] VALOR TITULO
                    Arquivo.Write("0".PadRight(4, '0').Substring(0, 4));                // POS 101/104 [04] [N] Agencia Encarregada da Cobrança
                    Arquivo.Write("0");                                                 // POS 105/105 [01] [N] Digito Agencia Encarregada da Cobrança
                    Arquivo.Write("".PadRight(1, ' '));                                 // POS 106/106 [01] [A] Brancos
                    Arquivo.Write("02");                                                // POS 107/108 [02] [N] Especie do Titulo
                    Arquivo.Write("N");                                                 // POS 109/109 [01] [A] Aceite = (N)
                    //Data da Emissão                                   
                    data = clsParser.DateTimeParse(row["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) +
                                  data.Substring(3, 2) +
                                  data.Substring(6, 4));                                // POS 110/117 [08] [N] DDMMAAAA
                    Arquivo.Write("1");                                                 // POS 118/118 [01] [N] Codigo Juros de Mora
                    // Data do Juros de Mora
                    data = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) +
                                  data.Substring(3, 2) +
                                  data.Substring(6, 4));                                // POS 119/126 [08] [N] DDMMAAAA
                    // Juros de Mora
                    vvalor = clsParser.DecimalParse(row["VALORJUROS"].ToString()).ToString();
                    vvalor = vvalor.Replace(".", "");
                    vvalor = vvalor.Replace(",", "");
                    Arquivo.Write(vvalor.ToString().PadLeft(15, '0').Substring(0, 15)); // POS 127/141 [15] [N] VALOR JUROS MORA
                    Arquivo.Write("0".PadRight(1, '0'));                                // POS 142/142 [01] [N] Codigo do Desconto 1
                    Arquivo.Write("0".PadRight(8, '0').Substring(0, 8));                // POS 143/150 [08] [N] Data do Desconto 1 DDMMAAAA
                    Arquivo.Write("0".PadRight(15, '0').Substring(0, 15));              // POS 151/165 [15] [N] Valor ou Porcentagen Desconto
                    Arquivo.Write("0".PadRight(15, '0').Substring(0, 15));              // POS 166/180 [15] [N] Valor do IOF a ser recolhido
                    Arquivo.Write("0".PadRight(15, '0').Substring(0, 15));              // POS 181/195 [15] [N] Valor do Abatimento
                    Arquivo.Write(" ".PadRight(25, ' ').Substring(0, 25));              // POS 196/220 [25] [A] Identificacao Titulo na Empresa
                    Arquivo.Write("0");                                                 // POS 221/221 [01] [N] Codigo para Protesto
                    Arquivo.Write("00");                                                // POS 222/223 [02] [N] Qtde dias para Protesto
                    Arquivo.Write("2");                                                 // POS 224/224 [01] [N] Codigo para Baixa/DevoluçãoProtesto
                    Arquivo.Write("0");                                                 // POS 225/225 [01] [N] Reservado para Banco (Zero Fixo)
                    Arquivo.Write("00");                                                // POS 226/227 [02] [N] Qtde dias para Baixa/Devolução
                    Arquivo.Write("00");                                                // POS 228/229 [02] [N] '00 - Real  Tipo da Moeda
                    Arquivo.Write(" ".PadRight(11, ' ').Substring(0,11));              // POS 230/240 [11] [A] Reservado Banco
                    // Carriage Return e Line Feed ---                      ' Nova Linha
                    //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                    Arquivo.WriteLine();

                    //Registro Detalhe SEGMENTO Q
                    Reg += 1;
                    Reg_P_Q_R += 1;
                    Arquivo.Write(clsBancosInfo.banco.ToString().PadLeft(3, '0').Substring(0, 3)); //"033" // POS 001/003 [03] [A] Codigo do Banco na Compensação
                    Arquivo.Write("0001");                               // POS 004/007 [04] [N] Nro do Lote Remessa
                    Arquivo.Write("3");                                  // POS 008/008 [01] [N] Tipo de registro
                    Arquivo.Write(Reg_P_Q_R.ToString().PadLeft(5, '0'));       // POS 009/013 [05] [N] Nro Sequencial do Registro de Lote
                    Arquivo.Write("Q");                                  // POS 014/014 [01] [N] Cod. Segmento do registro detalhe
                    Arquivo.Write(" ");                                  // POS 015/015 [01] [A] Brancos
                    Arquivo.Write("01");                                 // POS 016/017 [02] [N] 01 - Entrada Titulo
                    if (clsClienteInfo.pessoa == "F")                    // POS 018/018 [01] [N] '1=CPF 2=CNPJ
                    {
                        Arquivo.Write("1");  // 1:cpf
                    }
                    else
                    {
                        Arquivo.Write("2");   // 2:cgc
                    }

                    Arquivo.Write(row["CGC"].ToString().Trim().PadLeft(15, '0').Substring(0, 15));          // POS 019/033 [15] [N]  ' cnpj do Cliente
                    Arquivo.Write(clsVisual.RemoveAcentos(row["NOMECLIENTE"].ToString()).PadRight(40, ' ').Substring(0, 40)); // POS 034/073 [40] [A]  Nome do cliente(37)
                    Arquivo.Write(clsVisual.RemoveAcentos(row["ENDERECO"].ToString()).PadRight(40, ' ').Substring(0, 40));    // POS 074/113 [40] [A]  Endereço do cliente(37)
                    Arquivo.Write(clsVisual.RemoveAcentos(row["BAIRRO"].ToString()).PadRight(15, ' ').Substring(0, 15));      // POS 114/128 [15] [A]  Bairro cliente(37)
                    Arquivo.Write(row["CEP"].ToString().PadRight(8, '0').Substring(0, 8));                  // POS 129/136 [08] [N]  cep do cliente(8)
                    Arquivo.Write(clsVisual.RemoveAcentos(row["CIDADE"].ToString()).PadRight(15, ' ').Substring(0, 15));             // POS 137/151 [15] [A]  Cidade do cliente(8)
                    Arquivo.Write(clsVisual.RemoveAcentos(row["ESTADO"].ToString()).PadRight(2, ' ').Substring(0, 2));               // POS 152/153 [02] [A]  Uf do Cliente
                    Arquivo.Write("0");                    // POS 154/154 [01] [N] Tipo do Avalista CPF/CNPJ

                    Arquivo.Write("0".PadLeft(15, '0').Substring(0, 15));      // POS 155/169 [15] [N] numero cpf/cnpj avalista
                    
                    
                    Arquivo.Write(" ".PadRight(40, ' ').Substring(0, 40));                                  // POS 170/209 [40] [A] Nome do Avalista(37)
                    if (clsParser.Int32Parse(row["POSICAOFIM"].ToString()) > 1)
                    {
                        Arquivo.Write("001");                                   // POS 210/212 [03] [N] 000- não possui carne 001-possui carne
                        Arquivo.Write(row["POSICAO"].ToString().PadLeft(3, '0').Substring(0, 3));   // POS 213/215 [03] [N] SEQUENCIAL OU NUMERO INICIAL DA PARCELA
                        Arquivo.Write(row["POSICAOFIM"].ToString().PadLeft(3, '0').Substring(0, 3));    // POS 216/218 [03] [N] Quantidade Total de Parcelas
                    }
                    else
                    {
                        Arquivo.Write("000");                                // POS 210/212 [03] [N] 000- não possui carne 001-possui carne
                        Arquivo.Write("000");                                // POS 213/215 [03] [N] SEQUENCIAL OU NUMERO INICIAL DA PARCELA
                        Arquivo.Write("000");                                 // POS 216/218 [03] [N] Quantidade Total de Parcelas
                    }
                    Arquivo.Write("000");                                                                   // POS 219/221 [03] [N] numero do plano
                    Arquivo.Write(" ".PadRight(19, ' ').Substring(0, 19));                                  // POS 222/240 [19] [A] Reservado para o Banco
                    // Carriage Return e Line Feed ---                      ' Nova Linha
                    //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                    Arquivo.WriteLine();


                    //Registro Detalhe SEGMENTO R
                    Reg += 1;
                    Reg_P_Q_R += 1;
                    Arquivo.Write(clsBancosInfo.banco.ToString().PadLeft(3, '0').Substring(0, 3)); //"033" // POS 001/003 [03] [A] Codigo do Banco na Compensação
                    Arquivo.Write("0001");                               // POS 004/007 [04] [N] Nro do Lote Remessa
                    Arquivo.Write("3");                                  // POS 008/008 [01] [N] Tipo de registro
                    Arquivo.Write(Reg_P_Q_R.ToString().PadLeft(5, '0'));       // POS 009/013 [05] [N] Nro Sequencial do Registro de Lote
                    Arquivo.Write("R");                                  // POS 014/014 [01] [N] Cod. Segmento do registro detalhe
                    Arquivo.Write(" ");                                  // POS 015/015 [01] [A] Brancos
                    Arquivo.Write("01");                                 // POS 016/017 [02] [N] 01 - Entrada Titulo
                    Arquivo.Write("0".PadRight(1, '0').Substring(0, 1)); // POS 018/018 [01] [N] Codigo do Desconto 2
                    Arquivo.Write("0".PadRight(8, '0').Substring(0, 8)); // POS 019/026 [08] [N] Data do Desconto 2
                    Arquivo.Write("0".PadRight(15, '0').Substring(0, 15)); // POS 027/041 [15] [N] Valor do Desconto 2
                    Arquivo.Write(" ".PadRight(24, ' ').Substring(0, 24)); // POS 042/065 [24] [A] Uso do Banco
                    Arquivo.Write("2".PadRight(1, '0').Substring(0, 1)); // POS 066/066 [01] [N] Codigo Multa 1=fixo 2 Percentual
                    // Data da Multa
                    data = clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                    Arquivo.Write(data.Substring(0, 2) +
                                  data.Substring(3, 2) +
                                  data.Substring(6, 4));                // POS 067/074 [08] [N] DDMMAAAA
                    // Porcentagem
                    vvalor = ((2 * 100) / 100).ToString("N2");
                    vvalor = vvalor.Replace(".", "");
                    vvalor = vvalor.Replace(",", "");
                    Arquivo.Write(vvalor.ToString().PadLeft(15, '0').Substring(0, 15)); // POS 075/089 [15] [N] Porcentagem da Multa
                    Arquivo.Write(" ".PadRight(10, ' ').Substring(0, 10));              // POS 090/099 [10] [A] Uso Banco
                    Arquivo.Write(" ".PadRight(40, ' ').Substring(0, 40));              // POS 100/139 [40] [A] Mensagem 3
                    Arquivo.Write(" ".PadRight(40, ' ').Substring(0, 40));              // POS 140/179 [40] [A] Mensagem 4
                    Arquivo.Write(" ".PadRight(61, ' ').Substring(0, 61));              // POS 180/240 [61] Uso Banco
                    // Carriage Return e Line Feed ---                      ' Nova Linha
                    //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                    Arquivo.WriteLine();

                    // Pula uma linha em branco  ??

                }
                // ========== Trailer de Lote Remessa ====================
                Reg += 1;
                Reg_P_Q_R = Reg_P_Q_R + 2;
                Arquivo.Write(clsBancosInfo.banco.ToString().PadLeft(3, '0').Substring(0, 3)); //"033"   // POS 001/003 [03] [A] Codigo do Banco na Compensação
                Arquivo.Write("0001");                               // POS 004/007 [04] [N] Nro do Lote Remessa
                Arquivo.Write("5");                                  // POS 008/008 [01] [N] Tipo de registro
                Arquivo.Write(" ".PadRight(9, ' ').Substring(0, 9)); // POS 009/017 [61] Uso Banco
                Arquivo.Write(Reg_P_Q_R.ToString().PadLeft(6, '0').Substring(0, 6)); // POS 018/023 [06] Qtde de Registros do Lote
                Arquivo.Write(" ".PadRight(217, ' ').Substring(0, 217)); // POS 024/240 [217] Uso Banco
                // Carriage Return e Line Feed ---                      ' Nova Linha
                //Environment.NewLine; "\r\n";(char)13 + (char)10;  
                Arquivo.WriteLine();

                // Linha em Branco 
                //Arquivo.Write(" ".PadRight(240, ' ').Substring(0, 240)); // POS 001/240 [240] Uso Banco
                //Arquivo.WriteLine();

                // ========== Trailer Arquivo de Remessa ====================

                Reg += 1;
                Arquivo.Write(clsBancosInfo.banco.ToString().PadLeft(3, '0').Substring(0, 3)); //"033" // POS 001/003 [03] [A] Codigo do Banco na Compensação
                Arquivo.Write("9999");                               // POS 004/007 [04] [N] Nro do Lote Remessa
                Arquivo.Write("9");                                  // POS 008/008 [01] [N] Tipo de registro
                Arquivo.Write(" ".PadRight(9, ' ').Substring(0, 9)); // POS 009/017 [09] Uso Banco
                Arquivo.Write("000001");                             // POS 018/023 [06] [N] Qtde de Lotes
                Arquivo.Write(Reg.ToString().PadLeft(6, '0').Substring(0, 6));  //POS 024/029 [06] [N]  Qtde de Registros
                Arquivo.Write(" ".PadRight(211, ' ').Substring(0, 211));  //POS 030/240 [211] [A]  Uso Banco
                Arquivo.Close();
                MessageBox.Show("Gravado com sucesso!");
            }
        }

        private void CarregarReceberEnviar(Int32 _idReceberAberto)
        {
            //pegar a conta e carteira do banco indicado
            clsBancosInfo = clsBancosBLL.Carregar(idBancoIntDes, clsInfo.conexaosqlbanco);

            // Carregar o contas a Receber
            Boolean continua = true;
            idReceberAberto = _idReceberAberto;
            clsReceberInfo = clsReceberBLL.Carregar(idReceberAberto, clsInfo.conexaosqldados);
            //1. verificar se possui o juros
            if (clsReceberInfo.valorjuros == 0)
            {
                if (clsParser.DecimalParse(tbxJurosMes.Text) > 0)
                {
                    clsReceberInfo.valorjuros = Math.Round(((clsReceberInfo.valor * (clsParser.DecimalParse(tbxJurosMes.Text) / 100)) / 30), 2);
                }
                else
                {
                    MessageBox.Show("Obrigatorio possui a Taxa de Juros Mes - Favor adicionar !!");
                    continua = false;
                }
            }
            if (continua == true)
            {

                clsClienteInfo = clsClienteBLL.Carregar(clsReceberInfo.idcliente, clsInfo.conexaosqldados);
                dtClienteEndereco = new DataTable();
                String query = "select ID from CLIENTEENDERECO ";
                query = query + " WHERE  CLIENTEENDERECO.IDCLIENTE = " + clsClienteInfo.id + " ";
                query = query + " AND LEFT(TIPOENDNOME,1)= '1'";
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtClienteEndereco);
                Int32 idEnderecoCobranca = 0;
                if (dtClienteEndereco.Rows.Count > 0)
                {
                    foreach (DataRow rowendereco in dtClienteEndereco.Rows)
                    {
                        idEnderecoCobranca = clsParser.Int32Parse(rowendereco["ID"].ToString());
                    }

                }
                if (idEnderecoCobranca == 0)
                {
                    MessageBox.Show("Atenção o cadastro deste cliente não possui endereço de cobrança ?");
                    MessageBox.Show("Atualize o Cadastro para prosseguir = [" + clsClienteInfo.cognome + "]");
                    continua = false;
                }
                else
                { // Ja captura os campos do endereço do cliente
                    clsClienteEnderecoInfo = clsClienteenderecoBLL.Carregar(idEnderecoCobranca, clsInfo.conexaosqldados);

                    if (clsClienteEnderecoInfo.endereco.ToString().Length <= 0 ||
                        clsClienteEnderecoInfo.bairro.ToString().Length <= 0 ||
                        clsClienteEnderecoInfo.cep.ToString().Length <= 0 ||
                        clsClienteEnderecoInfo.cidade.ToString().Length <= 0)
                    {
                        MessageBox.Show("Atenção o cadastro deste cliente não possui endereço de cobrança correto?" +
                            Environment.NewLine + " Atualize o Cadastro para prosseguir = [" + clsClienteInfo.cognome + "]");
                   
                        continua = false;
                    }                    
                }
                if (continua == true)
                {
                    //' VER QUAL O BANCO
                    cDv = "";
                    if (clsParser.Int32Parse(tbxBancoDes.Text) == 1)
                    {//  [1]=BANCO DO BRASIL  MOD11
                        if (tbxBoletoProximo1.Text.Length == 11)
                        {
                            // Calcular o Digito 
                            cDv = clsFinanceiro.Digito_Mod11_Brasil(tbxBoletoProximo1.Text);
                        }
                        else
                        {
                            MessageBox.Show("Não foi Colocado 11 digitos no Nro do Boleto ");
                            continua = false;
                        }
                    }
                    if (clsParser.Int32Parse(tbxBancoDes.Text) == 33)
                    {//  [33]=BANCO SANTANDER  MOD11
                        if (tbxBoletoProximo1.Text.Length > 0)
                        {
                            // Calcular o Digito 
                            cDv = clsFinanceiro.Digito_Mod11_Santander(tbxBoletoProximo1.Text.PadLeft(12, '0'));
                        }
                        else
                        {
                            MessageBox.Show("Não foi Colocado NENHUM digito no Nro do Boleto ");
                            continua = false;
                        }
                    }
                    else if (clsParser.Int32Parse(tbxBancoDes.Text) == 237)
                    {  // Banco Bradesco
                        /*
                                                   LenDig = Len(Trim(Text(6)))
                                                   RegStr = CStr(Format(Text(6), "000000"))
                                                   nSoma = 0
                                                   Cons = "65432765432"
                                                   Nosso = Right("0000000000" & Trim(RegStr), 11)
                            
                                                   For I = 1 To 11
                                                       nProduto = Val(Mid(Cons, I, 1)) * Val(Mid(Nosso, I, 1))
                                                       nSoma = nSoma + nProduto
                                                   Next
                            
                                                   nSoma = nSoma + 63  'NSOMAndo a Carteira 9 * a Constante 7
                                                   Digito = Int(nSoma / 11)  'Dividindo entre o Módulo 11
                                                   Residual = nSoma - Digito * 11
                                                   Digito = 11 - Residual
                                                   cDv = CStr(Digito)
                                                   If Digito = 10 Then
                                                      cDv = "P"
                                                   End If
                                                   If Residual = 0 Then
                                                      cDv = 0
                                                   End If
                                                   If Residual = 1 Then
                                                       cDv = "P"
                                                   End If
                         */



                    }
                    else if (clsParser.Int32Parse(tbxBancoDes.Text) == 341)
                    {
                        // banco itau
                        String Nosso = "";
                        /*cagencia      ' numero da agencia (4 digitos)
                          cctacorrente  ' numero da conta  (5 digitos) sem o dac
                          ccarteira     ' numero da carteira de cobranca (3 digitos) 
                          tbxBoletoProximo1.Text ' proximo numero de boleto ' */

                        String cagencia = clsBancosInfo.agencia; //Tirace(eBco(1)) 
                        cagencia = cagencia.Replace(".", "");
                        cagencia = cagencia.Replace("-", "");
                        cagencia = cagencia.Replace("/", "");
                        if (cagencia.Length != 4)
                        {
                            MessageBox.Show("Numero da agencia não possui os 4 digitos  ( " + cagencia + " )  ??? ");
                            continua = false;
                        }

                        String cctacorrente = clsBancosInfo.contacor; //Tirace(eBco(2))
                        cctacorrente = cctacorrente.Replace(".", "");
                        cctacorrente = cctacorrente.Replace("-", "");
                        cctacorrente = cctacorrente.Replace("/", "");
                        if (cctacorrente.Length != 6)
                        {  
                            //MessageBox.Show("Numero da conta não possui os 6 digitos sem os pontos e tracos  ( " + cctacorrente + " )  ??? ");
                            //continua = false;
                        }


                        String ccarteira = clsBancosInfo.carteira; // tirace
                        ccarteira = ccarteira.Replace(".", "");
                        ccarteira = ccarteira.Replace("-", "");
                        ccarteira = ccarteira.Replace("/", "");
                        if (ccarteira.Length != 3)
                        {
                            MessageBox.Show("Numero da carteira não possui os 3 digitos sem os pontos e tracos  ( " + ccarteira + " )  ??? ");
                            continua = false;
                        }

                        Nosso = cagencia + cctacorrente.Substring(0, 5) + ccarteira + tbxBoletoProximo1.Text.PadLeft(8, '0'); // + & eBancoCon & eBancoCar & Right("00000000" & Trim(Text(6)), 8)

                        if (Nosso.Length == 20)
                        {   // codigo composto corretamente
                            // Calcular o Digito 
                            cDv = clsFinanceiro.Digito_Mod10_Itau(Nosso);
                        }
                        else
                        {
                            // codigo composto corretamente
                            MessageBox.Show("Não foi composto corretamente o Nro do Boleto ");
                            continua = false;
                        }

                    }
                    if (continua == true)
                    {
                        // Se for delga coloca desconto
                        vDesconto = 0;
                        vValorEspecial = 0;
                        if (clsClienteInfo.cognome.ToUpper().IndexOf("DELGA") != -1 ||
                            clsClienteInfo.cognome.ToUpper().IndexOf("MAQUI") != -1)
                        {
                            vDesconto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PISPASEPRETIDO from NFVENDA where ID= " + clsReceberInfo.idnotafiscal));
                            vDesconto += clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COFINS1RETIDO from NFVENDA where ID= " + clsReceberInfo.idnotafiscal));
                            clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PISPASEP from NFVENDA where ID= " + clsReceberInfo.idnotafiscal));
                            vValorEspecial = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select sum(totalnota) from nfvenda1 where numero=" + clsReceberInfo.idnotafiscal + " and FATURA = 'S'"));

                        }
                        // Se for para juntar por cnpj verificar se o cliente já foi cadastrado e acumular o valor
                        if (rbnEnviarCNPJ.Checked == true)
                        {
                            foreach (DataRow row1 in dtReceberEnviar.Rows)
                            {
                                if (clsParser.Int32Parse(row1["IDCLIENTE"].ToString()) == clsReceberInfo.idcliente)
                                {
                                    if (clsParser.DateTimeParse(row1["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy") == clsParser.DateTimeParse(clsReceberInfo.vencimento.ToString()).ToString("dd/MM/yyyy"))
                                    {
                                        if (vValorEspecial > 0)
                                        {
                                            row1["VALOR"] = clsParser.DecimalParse(row1["VALOR"].ToString()) + vValorEspecial; ;
                                            row1["VALORLIQUIDO"] = clsParser.DecimalParse(row1["VALORLIQUIDO"].ToString()) + vValorEspecial;
                                            //row1["VALORJUROS"] = clsParser.DecimalParse(row1["VALORJUROS"].ToString()) + clsParser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALORJUROS"].Value.ToString());
                                            row1["VALORJUROS"] = clsParser.DecimalParse(row1["VALORJUROS"].ToString()) + clsReceberInfo.valorjuros; // clsParser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALORJUROS"].Value.ToString());
                                        }
                                        else
                                        {
                                            row1["VALOR"] = clsParser.DecimalParse(row1["VALOR"].ToString()) + clsReceberInfo.valor; //Parser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALOR"].Value.ToString());
                                            row1["VALORLIQUIDO"] = clsParser.DecimalParse(row1["VALORLIQUIDO"].ToString()) + clsReceberInfo.valorliquido; // clsParser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALORLIQUIDO"].Value.ToString());
                                            row1["VALORJUROS"] = clsParser.DecimalParse(row1["VALORJUROS"].ToString()) + clsReceberInfo.valorjuros; // clsParser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALORJUROS"].Value.ToString());
                                        }
                                        // Agrupando as duplicatas 
                                        if (rbnEnviarCNPJ.Checked == true)
                                        {
                                            tbxBoletolinha03.Text = tbxBoletolinha03.Text + "-" + clsReceberInfo.duplicata.ToString();
                                        }
                                        // Agrupando os Id das duplicatas
                                        //row1["IDDUPLICATALINHA05"] = row1["IDDUPLICATALINHA05"].ToString() + "|" +  clsReceberInfo.id.ToString();
                                        tbxBoletolinha04.Text = tbxBoletolinha04.Text + "|" + clsReceberInfo.id.ToString();
                                        continua = false;
                                        break;
                                    }
                                    else
                                    {
                                        if (dtReceberEnviar.Rows.Count > 0)
                                        {
                                            MessageBox.Show("Na mensagem que vai no corpo do Boleto informando o numero das duplicatas " + Environment.NewLine +
                                                            "que fazem parte do valor do Boleto é uma mensagem unica no arquivo remessa " + Environment.NewLine +
                                                            "padronizado pelo proprio Banco conforme norma Febraban. " + Environment.NewLine +
                                                            "Sendo assim voce deve acumular individualmente por cliente,data de vencimento " + Environment.NewLine +
                                                            "diversos arquivos de remessa. Como as remessa são numeradas individualmente, " + Environment.NewLine +

                                                            "depois poderá envia-las ao Banco.");
                                            continua = false;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (dtReceberEnviar.Rows.Count > 0)
                                    {
                                        MessageBox.Show("Na mensagem que vai no corpo do Boleto informando o numero das duplicatas " + Environment.NewLine +
                                                        "que fazem parte do valor do Boleto é uma mensagem unica no arquivo remessa " + Environment.NewLine +
                                                        "padronizado pelo proprio Banco conforme norma Febraban. " + Environment.NewLine +
                                                        "Sendo assim voce deve acumular individualmente por cliente,data de vencimento " + Environment.NewLine +
                                                        "diversos arquivos de remessa. Como as remessa são numeradas individualmente, " + Environment.NewLine +

                                                        "depois poderá envia-las ao Banco.");
                                        continua = false;
                                        break;

                                    }
                                }
                            }
                        }
                    }
                    // 
                    if (continua == true)
                    {
                        // procurando a posição
                        posicao_receberenviar = 0;
                        foreach (DataRow row1 in dtReceberEnviar.Rows)
                        {
                            if (clsParser.Int32Parse(row1["POSICAO_RECEBERENVIAR"].ToString()) > posicao_receberenviar)
                            {
                                posicao_receberenviar = clsParser.Int32Parse(row1["POSICAO_RECEBERENVIAR"].ToString());
                            }
                        }
                        posicao_receberenviar += 1;
                        DataRow row = dtReceberEnviar.NewRow();
                        row["POSICAO_RECEBERENVIAR"] = posicao_receberenviar;
                        row["IDRECEBER"] = clsReceberInfo.id; //Parser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["ID"].Value.ToString());
                        row["IDCLIENTE"] = clsReceberInfo.idcliente;
                        row["FILIAL"] = clsReceberInfo.filial; //Parser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["FILIAL"].Value.ToString());
                        row["DUPLICATA"] = clsReceberInfo.duplicata; // clsParser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["DUPLICATA"].Value.ToString());
                        row["POSICAO"] = clsReceberInfo.posicao; // clsParser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["POSICAO"].Value.ToString());
                        row["POSICAOFIM"] = clsReceberInfo.posicaofim; // clsParser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["POSICAOFIM"].Value.ToString());
                        row["EMISSAO"] = clsReceberInfo.emissao; // clsParser.DateTimeParse(dgvReceberAberto.CurrentRow.Cells["EMISSAO"].Value.ToString());
                        row["COGNOME"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id= " + clsReceberInfo.idcliente,"");  //dgvReceberAberto.CurrentRow.Cells["COGNOME"].Value.ToString();
                        row["VENCIMENTO"] = clsReceberInfo.vencimento; // clsParser.DateTimeParse(dgvReceberAberto.CurrentRow.Cells["VENCIMENTO"].Value.ToString());
                        if (vValorEspecial > 0)
                        {
                            row["VALOR"] = vValorEspecial;
                            row["VALORLIQUIDO"] = vValorEspecial;
                            row["VALORDESCONTO"] = vDesconto;
                        }
                        else
                        {
                            row["VALOR"] = clsReceberInfo.valor; // clsParser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALOR"].Value.ToString());
                            row["VALORLIQUIDO"] = clsReceberInfo.valorliquido; // clsParser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALORLIQUIDO"].Value.ToString());
                            row["VALORDESCONTO"] = 0;
                        }
                        row["VENDEDOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id= " + clsReceberInfo.idvendedor);  //dgvReceberAberto.CurrentRow.Cells["VENDEDOR"].Value.ToString();
                        row["FORMAPAGTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOTIPOTITULO where id= " + clsReceberInfo.idformapagto);  //dgvReceberAberto.CurrentRow.Cells["VENDEDOR"].Value.ToString();
                        row["BOLETONRO"] = tbxBoletoProximo1.Text;
                        row["DV"] = cDv;
                        row["BANCO"] = tbxBancoNome.Text;
                        row["VALORJUROS"] = clsReceberInfo.valorjuros; //Parser.DecimalParse(dgvReceberAberto.CurrentRow.Cells["VALORJUROS"].Value.ToString());
                        //
                        row["NOMECLIENTE"] = clsClienteEnderecoInfo.cobnome.Trim().PadRight(39).Substring(0, 39);
                        row["ENDERECO"] = clsClienteEnderecoInfo.endereco.Trim().PadRight(40).Substring(0, 40);
                        row["BAIRRO"] = clsClienteEnderecoInfo.bairro.Trim().PadRight(15).Substring(0, 15);
                        row["CIDADE"] = clsClienteEnderecoInfo.cidade.Trim().PadRight(15).Substring(0, 15);
                        row["ESTADO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ESTADO from ESTADOS where id= " + clsClienteEnderecoInfo.idestado);
                        row["CEP"] = clsClienteEnderecoInfo.cep.Replace("-", "");
                        row["CGC"] = clsClienteInfo.cgc.Trim().PadRight(18).Substring(0, 18);
                        row["IE"] = clsClienteInfo.ie.Trim().PadRight(16).Substring(0, 16);
                        row["PESSOA"] = clsClienteInfo.pessoa;
                        row["DIASPROTESTO"] = tbxProtestar1.Text;
                        row["LINHA01"] = tbxBoletolinha01.Text;
                        if (vDesconto > 0)
                        {
                            tbxBoletolinha02.Text = "Desconto de " + vDesconto.ToString("N2");
                        }
                        if (rbnEnviarCNPJ.Checked == true)
                        {
                            if (tbxBoletolinha03.Text.Length == 0)
                            {
                                tbxBoletolinha03.Text = clsReceberInfo.duplicata.ToString();
                                tbxBoletolinha04.Text = clsReceberInfo.id.ToString();
                            }
                        }
                        else
                        {
                            tbxBoletolinha03.Text = clsReceberInfo.duplicata.ToString();
                            tbxBoletolinha04.Text = clsReceberInfo.id.ToString() + "|";
                        }
                        row["REFERENCIA"] = "";
                        row["REFERENCIA1"] = "";
                        row["REFERENCIA2"] = "";
                        dtReceberEnviar.Rows.Add(row);
                        // como é inclusão somar + 1 no numero do boleto
                        tbxBoletoProximo1.Text = (clsParser.DecimalParse(tbxBoletoProximo1.Text) + 1).ToString();
                    }
                    // Gravar na Duplicata para que ela saia da lista de Pendencias
                    // Vai Gravar numero de Boleto = 1 (Nunca deve existir boleto nro 1)
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scd = new SqlCommand("UPDATE RECEBER SET BOLETONRO=@BOLETONRO WHERE ID=@ID ", scn);
                    scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsReceberInfo.id;  //Parser.Int32Parse(dgvReceberAberto.CurrentRow.Cells["ID"].Value.ToString());
                    scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = 1;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();

                    bwrReceberEnviar_Run();
                    bwrReceberAberto_Run();

                }
            }
        }

        private void btnTransferirAutomatico_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dtReceberAberto.Rows)
            {
                // Enviar todas as duplicatas
                idReceberAberto = clsParser.Int32Parse(row["ID"].ToString());
                CarregarReceberEnviar(idReceberAberto);
                //
            }

        }

        private void tbxBoletolinha04_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tbxPesquisa.Focus();

        }

        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tbxPesquisa.Focus();

        }

        private void BoletoBrasil()
        {

        }
        private void BoletoItau(DataTable dtReceberEnviar_Imprimir) 
        {
            //para o codigo de barras            
            Barcode Barcode = new Barcode();

            // Abrir a empresa e pegar os dados
            //dizeresnf = clsVisual.RemoveAcentos(dizeresnf);
            clsEmpresaInfo = clsEmpresaBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            String ccgc = clsEmpresaInfo.cgc; // eEmp(1) = Tirace(eEmp(1))
            ccgc = ccgc.Replace(".", "");
            ccgc = ccgc.Replace("-", "");
            ccgc = ccgc.Replace("/", "");
            String cendereco = clsEmpresaInfo.endereco + " " + clsEmpresaInfo.numeroend.Trim() + " " +
                               clsEmpresaInfo.andar.Trim() + " " + clsEmpresaInfo.tipocomple.Trim() + " " + clsEmpresaInfo.comple.Trim();
            cendereco = clsVisual.RemoveAcentos(cendereco);
            String ccep = clsEmpresaInfo.cep; //TiraOut(eEmp(9))
            ccep = ccep.Replace(".", "");
            ccep = ccep.Replace("-", "");
            ccep = ccep.Replace("/", "");

            //pegar a conta e carteira do banco indicado
            clsBancosInfo = clsBancosBLL.Carregar(idBancoIntDes, clsInfo.conexaosqlbanco);
            String cagencia = clsBancosInfo.agencia; //Tirace(eBco(1)) 
            cagencia = cagencia.Replace(".", "");
            cagencia = cagencia.Replace("-", "");
            cagencia = cagencia.Replace("/", "");


            String cctacorrente = clsBancosInfo.contacor; //Tirace(eBco(2))
            cctacorrente = cctacorrente.Replace(".", "");
            cctacorrente = cctacorrente.Replace("-", "");
            cctacorrente = cctacorrente.Replace("/", "");

            String ccarteira = clsBancosInfo.carteira; // tirace

            //criando o dt temporario data o Boleto
            DataTable dtImprimirBoleto = new DataTable();

            DataColumn dc_posicao = new DataColumn("dc_posicao", Type.GetType("System.Int32"));
            DataColumn dc_numero_do_banco = new DataColumn("dc_numero_do_banco", Type.GetType("System.String"));
            DataColumn dc_linha_digitavel = new DataColumn("dc_linha_digitavel", Type.GetType("System.String"));
            DataColumn dc_local_de_pagamento = new DataColumn("dc_local_de_pagamento", Type.GetType("System.String"));
            DataColumn dc_vencimento = new DataColumn("dc_vencimento", Type.GetType("System.DateTime"));
            DataColumn dc_cedente = new DataColumn("dc_cedente", Type.GetType("System.String"));
            DataColumn dc_agencia_identificacao_cedente = new DataColumn("dc_agencia_identificacao_cedente", Type.GetType("System.String"));
            DataColumn dc_data_do_documento = new DataColumn("dc_data_do_documento", Type.GetType("System.DateTime"));
            DataColumn dc_numero_do_documento = new DataColumn("dc_numero_do_documento", Type.GetType("System.String"));
            DataColumn dc_especie_do_documento = new DataColumn("dc_especie_do_documento", Type.GetType("System.String"));
            DataColumn dc_aceite = new DataColumn("dc_aceite", Type.GetType("System.String"));
            DataColumn dc_data_do_processamento = new DataColumn("dc_data_do_processamento", Type.GetType("System.DateTime"));
            DataColumn dc_nosso_numero = new DataColumn("dc_nosso_numero", Type.GetType("System.String"));
            DataColumn dc_carteira = new DataColumn("dc_carteira", Type.GetType("System.String"));
            DataColumn dc_especie = new DataColumn("dc_especie", Type.GetType("System.String"));
            DataColumn dc_quantidade = new DataColumn("dc_quantidade", Type.GetType("System.Int32"));
            DataColumn dc_valor = new DataColumn("dc_valor", Type.GetType("System.Decimal"));
            DataColumn dc_valor_documento = new DataColumn("dc_valor_documento", Type.GetType("System.Decimal"));
            DataColumn dc_instrucoes_linha1 = new DataColumn("dc_instrucoes_linha1", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha2 = new DataColumn("dc_instrucoes_linha2", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha3 = new DataColumn("dc_instrucoes_linha3", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha4 = new DataColumn("dc_instrucoes_linha4", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha5 = new DataColumn("dc_instrucoes_linha5", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha6 = new DataColumn("dc_instrucoes_linha6", Type.GetType("System.String"));
            DataColumn dc_desconto = new DataColumn("dc_desconto", Type.GetType("System.Decimal"));
            DataColumn dc_abatimento = new DataColumn("dc_abatimento", Type.GetType("System.Decimal"));
            DataColumn dc_mora = new DataColumn("dc_mora", Type.GetType("System.Decimal"));
            DataColumn dc_outros_acrescimos = new DataColumn("dc_outros_acrescimos", Type.GetType("System.Decimal"));
            DataColumn dc_valor_cobrado = new DataColumn("dc_valor_cobrado", Type.GetType("System.Decimal"));
            DataColumn dc_sacado_linha1 = new DataColumn("dc_sacado_linha1", Type.GetType("System.String"));
            DataColumn dc_sacado_linha2 = new DataColumn("dc_sacado_linha2", Type.GetType("System.String"));
            DataColumn dc_sacado_linha3 = new DataColumn("dc_sacado_linha3", Type.GetType("System.String"));
            DataColumn dc_sacado_linha4 = new DataColumn("dc_sacado_linha4", Type.GetType("System.String"));
            DataColumn dc_sacado_linha5 = new DataColumn("dc_sacado_linha5", Type.GetType("System.String"));
            DataColumn dc_sacado_linha6 = new DataColumn("dc_sacado_linha6", Type.GetType("System.String"));
            DataColumn dc_cpf_cnpjsacado = new DataColumn("dc_cpf_cnpjsacado", Type.GetType("System.String"));
            DataColumn dc_cpf_cnpjcedente = new DataColumn("dc_cpf_cnpjcedente", Type.GetType("System.String"));
            DataColumn dc_codBarra = new DataColumn("dc_codBarra", Type.GetType("System.String"));
            DataColumn dc_imagem_codBarras_2de5_intercalado = new DataColumn("dc_imagem_codBarras_2de5_intercalado", Type.GetType("System.Byte[]"));
            dc_imagem_codBarras_2de5_intercalado.AllowDBNull = true;

            dtImprimirBoleto.Columns.Add(dc_posicao);
            dtImprimirBoleto.Columns.Add(dc_numero_do_banco);
            dtImprimirBoleto.Columns.Add(dc_linha_digitavel);
            dtImprimirBoleto.Columns.Add(dc_local_de_pagamento);
            dtImprimirBoleto.Columns.Add(dc_vencimento);
            dtImprimirBoleto.Columns.Add(dc_cedente);
            dtImprimirBoleto.Columns.Add(dc_agencia_identificacao_cedente);
            dtImprimirBoleto.Columns.Add(dc_data_do_documento);
            dtImprimirBoleto.Columns.Add(dc_numero_do_documento);
            dtImprimirBoleto.Columns.Add(dc_especie_do_documento);
            dtImprimirBoleto.Columns.Add(dc_aceite);
            dtImprimirBoleto.Columns.Add(dc_data_do_processamento);
            dtImprimirBoleto.Columns.Add(dc_nosso_numero);
            dtImprimirBoleto.Columns.Add(dc_carteira);
            dtImprimirBoleto.Columns.Add(dc_especie);
            dtImprimirBoleto.Columns.Add(dc_quantidade);
            dtImprimirBoleto.Columns.Add(dc_valor);
            dtImprimirBoleto.Columns.Add(dc_valor_documento);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha1);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha2);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha3);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha4);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha5);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha6);
            dtImprimirBoleto.Columns.Add(dc_desconto);
            dtImprimirBoleto.Columns.Add(dc_abatimento);
            dtImprimirBoleto.Columns.Add(dc_mora);
            dtImprimirBoleto.Columns.Add(dc_outros_acrescimos);
            dtImprimirBoleto.Columns.Add(dc_valor_cobrado);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha1);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha2);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha3);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha4);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha5);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha6);
            dtImprimirBoleto.Columns.Add(dc_cpf_cnpjsacado);
            dtImprimirBoleto.Columns.Add(dc_cpf_cnpjcedente);
            dtImprimirBoleto.Columns.Add(dc_codBarra);
            dtImprimirBoleto.Columns.Add(dc_imagem_codBarras_2de5_intercalado);

            //adicionando valores ao dt temporario             
            for (int x = 0; x < dtReceberEnviar_Imprimir.Rows.Count; x++)
            {
                DataRow row = dtImprimirBoleto.NewRow();

                row["dc_posicao"] = x + 1;

                row["dc_numero_do_banco"] = clsBancosInfo.banco.ToString().PadLeft(3, '0') + "-7"; // Complemento Santander

                DateTime dat = clsParser.DateTimeParse(dtReceberEnviar_Imprimir.Rows[x]["VENCIMENTO"].ToString());

                String fatorVencimento = dat.Subtract(clsParser.DateTimeParse("07/10/1997 00:00:00")).Days.ToString();

                String cod_barra = clsBancosInfo.banco.ToString().PadLeft(3, '0') + //banco
                    clsBancosInfo.bcomoeda.PadLeft(2, '0').Substring(1, 1) + // moeda
                    fatorVencimento +
                    dtReceberEnviar_Imprimir.Rows[x]["VALOR"].ToString().Replace(",", "").Replace(".", "").PadLeft(10, '0').Substring(0, 10) + //valor Nominal
                    clsBancosInfo.carteira.Substring(0, 3) + // Psk código cedente conta bancaria
                    //(dtReceberEnviar_Imprimir.Rows[x]["BOLETONRO"].ToString() + dtReceberEnviar_Imprimir.Rows[x]["DV"].ToString()).PadLeft(9, '0').Substring(0, 9) + // nosso numero
                    (dtReceberEnviar_Imprimir.Rows[x]["BOLETONRO"].ToString()).PadLeft(9, '0').Substring(0, 9) + // nosso numero
                    cagencia + cctacorrente + "000"; 
                String DV_Barra = clsFinanceiro.Digito_Mod11Itau(cod_barra.ToString());

                ////1° Grupo
                String grupo1 = clsBancosInfo.banco.ToString().PadLeft(3, '0') + clsBancosInfo.bcomoeda.PadLeft(2, '0').Substring(1, 1) + 
                     clsBancosInfo.carteira.PadLeft(3, ' ').Substring(0, 3); 
                String grupo1Dv = clsFinanceiro.Digito_Mod10_Itau(grupo1);

                ////2° Grupo
                String grupo2 = clsBancosInfo.carteira.PadLeft(3, ' ').Substring(0, 3) + //"382"  Codigo do Cedente
                     (dtReceberEnviar_Imprimir.Rows[x]["BOLETONRO"].ToString() + dtReceberEnviar_Imprimir.Rows[x]["DV"].ToString()).PadLeft(13, '0').Substring(0, 7); // nosso numero
                String grupo2Dv = clsFinanceiro.Digito_Mod10_Itau(grupo2);

                //////3° Grupo
                String grupo3 = (dtReceberEnviar_Imprimir.Rows[x]["BOLETONRO"].ToString() + dtReceberEnviar_Imprimir.Rows[x]["DV"].ToString()).PadLeft(13, '0').Substring(7, 6) +// nosso numero
                    "0" + //IOS
                     clsBancosInfo.bcomodalidade.PadLeft(3, ' ').Substring(0, 3); // "101" Carteira
                String grupo3Dv = clsFinanceiro.Digito_Mod10_Itau(grupo3);

                /////4° Grupo
                String grupo4 = DV_Barra;

                ////5° Grupo
                String grupo5 = fatorVencimento +
                  dtReceberEnviar_Imprimir.Rows[x]["VALOR"].ToString().Replace(",", "").Replace(".", "").PadLeft(10, '0').Substring(0, 10); //valor Nominal

                row["dc_linha_digitavel"] = (grupo1 + grupo1Dv).Insert(5, ".") + " " +
                                            (grupo2 + grupo2Dv).Insert(5, ".") + " " +
                                            (grupo3 + grupo3Dv).Insert(5, ".") + " " +
                                             grupo4 + " " +
                                             grupo5;

                ///////////////////////////////////////////////////////////////////////////////////

                row["dc_local_de_pagamento"] = "Pagar Preferencialmente no Grupo Itau";

                row["dc_vencimento"] = dtReceberEnviar_Imprimir.Rows[x]["VENCIMENTO"];

                row["dc_cedente"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select nome FROM EMPRESA where codigo = " + clsInfo.zfilial, "");

                //row["dc_agencia_identificacao_cedente"] = cagencia.ToString() + "/" + cctacorrente.ToString();
                row["dc_agencia_identificacao_cedente"] = cagencia.ToString() + "/" + clsBancosInfo.contacor;

                row["dc_data_do_documento"] = dtReceberEnviar_Imprimir.Rows[x]["EMISSAO"];

                row["dc_numero_do_documento"] = dtReceberEnviar_Imprimir.Rows[x]["DUPLICATA"];

                row["dc_especie_do_documento"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSESPECIE " +
                                     "where id = " + clsBancosInfo.idbcoespecie, "DM").ToUpper(); //"DM";

                row["dc_aceite"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSACEITE " +
                                     "where id = " + clsBancosInfo.idbcoaceite, "N").ToUpper(); // "N";

                row["dc_data_do_processamento"] = DateTime.Now;

//                row["dc_nosso_numero"] = dtReceberEnviar_Imprimir.Rows[x]["BOLETONRO"].ToString().PadLeft(12, '0')
//                    + "-" + dtReceberEnviar_Imprimir.Rows[x]["DV"].ToString(); 

                row["dc_carteira"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMODALIDADE " +
                                     "where id = " + clsBancosInfo.idbcomodalidade, "").ToUpper().PadLeft(3, ' ').Substring(0, 3); // "101 - Cobrança Simples ECR";

                row["dc_nosso_numero"] = row["dc_carteira"] + "/" +
                    dtReceberEnviar_Imprimir.Rows[x]["BOLETONRO"].ToString().PadLeft(8, '0')
                    + "-" + dtReceberEnviar_Imprimir.Rows[x]["DV"].ToString();


                row["dc_especie"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from TAB_BANCOSMOEDA " +
                                     "where id = " + clsBancosInfo.idbcomoeda, "REAL").ToUpper().Replace("PARA", ""); // "REAL";

                row["dc_quantidade"] = "0";

                row["dc_valor"] = "0";

                row["dc_valor_documento"] = dtReceberEnviar_Imprimir.Rows[x]["VALOR"];

                row["dc_instrucoes_linha1"] = tbxBoletolinha01.Text;
                row["dc_instrucoes_linha2"] = tbxBoletolinha02.Text;

                row["dc_instrucoes_linha3"] = "Juros de Mora de R$" +
                    clsParser.DecimalParse(dtReceberEnviar_Imprimir.Rows[x]["VALORJUROS"].ToString()).ToString() +
                    " ao dia.";

                //row["dc_instrucoes_linha4"] = "Multa de " + ((2 * 100) / 100).ToString("N2") + "% após vencimento.";
                row["dc_instrucoes_linha4"] = "Multa de " + ((clsParser.DecimalParse(tbxJurosMes1.Text) * 100) / 100).ToString("N2") + "% após vencimento.";
                row["dc_instrucoes_linha5"] = "";
                row["dc_instrucoes_linha6"] = "";

                row["dc_desconto"] = dtReceberEnviar_Imprimir.Rows[x]["VALORDESCONTO"];

                row["dc_abatimento"] = "0";
                row["dc_mora"] = "0";
                row["dc_outros_acrescimos"] = "0";

                row["dc_valor_cobrado"] = dtReceberEnviar_Imprimir.Rows[x]["VALORLIQUIDO"].ToString().ToUpper();

                row["dc_sacado_linha1"] = dtReceberEnviar_Imprimir.Rows[x]["NOMECLIENTE"].ToString().ToUpper()
                    + " - CNPJ/CPF : " + dtReceberEnviar_Imprimir.Rows[x]["CGC"].ToString();

                row["dc_sacado_linha2"] = dtReceberEnviar_Imprimir.Rows[x]["ENDERECO"].ToString().ToUpper()
                    + " Bairro: " + dtReceberEnviar_Imprimir.Rows[x]["BAIRRO"].ToString().ToUpper();

                row["dc_sacado_linha3"] = dtReceberEnviar_Imprimir.Rows[x]["CEP"].ToString()
                        + "        " + dtReceberEnviar_Imprimir.Rows[x]["CIDADE"].ToString().ToUpper()
                        + " / " + dtReceberEnviar_Imprimir.Rows[x]["ESTADO"].ToString().ToUpper();

                row["dc_sacado_linha4"] = "";
                row["dc_sacado_linha5"] = "Sacador/Avalista:";
                row["dc_sacado_linha6"] = "";

                row["dc_cpf_cnpjsacado"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                             "select cgc from cliente where id = " + clsParser.Int32Parse(dtReceberEnviar_Imprimir.Rows[x]["IDCLIENTE"].ToString()), "");

                row["dc_cpf_cnpjcedente"] = ccgc;

                //row["dc_codBarra"] = (grupo1 + grupo1Dv).Insert(5, ".") + " " +
                //                            (grupo2 + grupo2Dv).Insert(5, ".") + " " +
                //                            (grupo3 + grupo3Dv).Insert(5, ".") + " " +
                //                            grupo4 + " " +
                //                            grupo5;

                cod_barra = cod_barra.Insert(4, DV_Barra);
                row["dc_codBarra"] = cod_barra.ToString();

                //imagem do codigo de barras
                /*
                ptbCodBarras.Image = Barcode.Encode(TYPE.Interleaved2of5,
                             cod_barra.ToString(),
                             Color.Black, Color.White, 400, 20);

                row["dc_imagem_codBarras_2de5_intercalado"] = clsParser.imageToByteArray(ptbCodBarras.Image);
                */
                int largura = 400;
                int altura = 20;
                Bitmap qrcode = GerarQRCode(largura, altura, cod_barra);
                qrcode.Save(@"C:\Users\Public\imagem.jpeg");
                row["dc_imagem_codBarras_2de5_intercalado"] = clsParser.imageToByteArray(qrcode); 

                dtImprimirBoleto.Rows.Add(row);
                dtImprimirBoleto.AcceptChanges();
            }
            //fim adicionando valores 

            //parametros -- opcional ---
            String cabecalho = "";
            ParameterFields pfields = new ParameterFields();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);
            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);

            //imprimindo os boletos
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            frmCrystalReport.Init(clsInfo.caminhorelatorios, "BOLETO_ITAU.RPT", dtImprimirBoleto,
                                   pfields, "", clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
        }

        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0, PureBarcode = true};
                bw.Options = encOptions;
                //bw.Format = ZXing.BarcodeFormat.QR_CODE;
                //bw.Format = ZXing.BarcodeFormat.CODE_128;
                bw.Format = ZXing.BarcodeFormat.CODABAR;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        private void BoletoSantander()
        {
            //para o codigo de barras            
            Barcode Barcode = new Barcode();

            // Abrir a empresa e pegar os dados
            //dizeresnf = clsVisual.RemoveAcentos(dizeresnf);
            clsEmpresaInfo = clsEmpresaBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            String ccgc = clsEmpresaInfo.cgc; // eEmp(1) = Tirace(eEmp(1))
            ccgc = ccgc.Replace(".", "");
            ccgc = ccgc.Replace("-", "");
            ccgc = ccgc.Replace("/", "");
            String cendereco = clsEmpresaInfo.endereco + " " + clsEmpresaInfo.numeroend.Trim() + " " +
                               clsEmpresaInfo.andar.Trim() + " " + clsEmpresaInfo.tipocomple.Trim() + " " + clsEmpresaInfo.comple.Trim();
            cendereco = clsVisual.RemoveAcentos(cendereco);
            String ccep = clsEmpresaInfo.cep; //TiraOut(eEmp(9))
            ccep = ccep.Replace(".", "");
            ccep = ccep.Replace("-", "");
            ccep = ccep.Replace("/", "");

            //pegar a conta e carteira do banco indicado
            clsBancosInfo = clsBancosBLL.Carregar(idBancoIntDes, clsInfo.conexaosqlbanco);
            String cagencia = clsBancosInfo.agencia; //Tirace(eBco(1)) 
            cagencia = cagencia.Replace(".", "");
            cagencia = cagencia.Replace("-", "");
            cagencia = cagencia.Replace("/", "");


            String cctacorrente = clsBancosInfo.contacor; //Tirace(eBco(2))
            cctacorrente = cctacorrente.Replace(".", "");
            cctacorrente = cctacorrente.Replace("-", "");
            cctacorrente = cctacorrente.Replace("/", "");

            String ccarteira = clsBancosInfo.carteira; // tirace

            //criando o dt temporario data o Boleto
            DataTable dtImprimirBoleto = new DataTable();

            DataColumn dc_posicao = new DataColumn("dc_posicao", Type.GetType("System.Int32"));
            DataColumn dc_numero_do_banco = new DataColumn("dc_numero_do_banco", Type.GetType("System.String"));
            DataColumn dc_linha_digitavel = new DataColumn("dc_linha_digitavel", Type.GetType("System.String"));
            DataColumn dc_local_de_pagamento = new DataColumn("dc_local_de_pagamento", Type.GetType("System.String"));
            DataColumn dc_vencimento = new DataColumn("dc_vencimento", Type.GetType("System.DateTime"));
            DataColumn dc_cedente = new DataColumn("dc_cedente", Type.GetType("System.String"));
            DataColumn dc_agencia_identificacao_cedente = new DataColumn("dc_agencia_identificacao_cedente", Type.GetType("System.String"));
            DataColumn dc_data_do_documento = new DataColumn("dc_data_do_documento", Type.GetType("System.DateTime"));
            DataColumn dc_numero_do_documento = new DataColumn("dc_numero_do_documento", Type.GetType("System.String"));
            DataColumn dc_especie_do_documento = new DataColumn("dc_especie_do_documento", Type.GetType("System.String"));
            DataColumn dc_aceite = new DataColumn("dc_aceite", Type.GetType("System.String"));
            DataColumn dc_data_do_processamento = new DataColumn("dc_data_do_processamento", Type.GetType("System.DateTime"));
            DataColumn dc_nosso_numero = new DataColumn("dc_nosso_numero", Type.GetType("System.String"));
            DataColumn dc_carteira = new DataColumn("dc_carteira", Type.GetType("System.String"));
            DataColumn dc_especie = new DataColumn("dc_especie", Type.GetType("System.String"));
            DataColumn dc_quantidade = new DataColumn("dc_quantidade", Type.GetType("System.Int32"));
            DataColumn dc_valor = new DataColumn("dc_valor", Type.GetType("System.Decimal"));
            DataColumn dc_valor_documento = new DataColumn("dc_valor_documento", Type.GetType("System.Decimal"));
            DataColumn dc_instrucoes_linha1 = new DataColumn("dc_instrucoes_linha1", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha2 = new DataColumn("dc_instrucoes_linha2", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha3 = new DataColumn("dc_instrucoes_linha3", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha4 = new DataColumn("dc_instrucoes_linha4", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha5 = new DataColumn("dc_instrucoes_linha5", Type.GetType("System.String"));
            DataColumn dc_instrucoes_linha6 = new DataColumn("dc_instrucoes_linha6", Type.GetType("System.String"));
            DataColumn dc_desconto = new DataColumn("dc_desconto", Type.GetType("System.Decimal"));
            DataColumn dc_abatimento = new DataColumn("dc_abatimento", Type.GetType("System.Decimal"));
            DataColumn dc_mora = new DataColumn("dc_mora", Type.GetType("System.Decimal"));
            DataColumn dc_outros_acrescimos = new DataColumn("dc_outros_acrescimos", Type.GetType("System.Decimal"));
            DataColumn dc_valor_cobrado = new DataColumn("dc_valor_cobrado", Type.GetType("System.Decimal"));
            DataColumn dc_sacado_linha1 = new DataColumn("dc_sacado_linha1", Type.GetType("System.String"));
            DataColumn dc_sacado_linha2 = new DataColumn("dc_sacado_linha2", Type.GetType("System.String"));
            DataColumn dc_sacado_linha3 = new DataColumn("dc_sacado_linha3", Type.GetType("System.String"));
            DataColumn dc_sacado_linha4 = new DataColumn("dc_sacado_linha4", Type.GetType("System.String"));
            DataColumn dc_sacado_linha5 = new DataColumn("dc_sacado_linha5", Type.GetType("System.String"));
            DataColumn dc_sacado_linha6 = new DataColumn("dc_sacado_linha6", Type.GetType("System.String"));
            DataColumn dc_cpf_cnpjsacado = new DataColumn("dc_cpf_cnpjsacado", Type.GetType("System.String"));
            DataColumn dc_cpf_cnpjcedente = new DataColumn("dc_cpf_cnpjcedente", Type.GetType("System.String"));
            DataColumn dc_codBarra = new DataColumn("dc_codBarra", Type.GetType("System.String"));
            DataColumn dc_imagem_codBarras_2de5_intercalado = new DataColumn("dc_imagem_codBarras_2de5_intercalado", Type.GetType("System.Byte[]"));
            dc_imagem_codBarras_2de5_intercalado.AllowDBNull = true;
            
            dtImprimirBoleto.Columns.Add(dc_posicao);
            dtImprimirBoleto.Columns.Add(dc_numero_do_banco);
            dtImprimirBoleto.Columns.Add(dc_linha_digitavel);
            dtImprimirBoleto.Columns.Add(dc_local_de_pagamento);
            dtImprimirBoleto.Columns.Add(dc_vencimento);
            dtImprimirBoleto.Columns.Add(dc_cedente);
            dtImprimirBoleto.Columns.Add(dc_agencia_identificacao_cedente);
            dtImprimirBoleto.Columns.Add(dc_data_do_documento);
            dtImprimirBoleto.Columns.Add(dc_numero_do_documento);
            dtImprimirBoleto.Columns.Add(dc_especie_do_documento);
            dtImprimirBoleto.Columns.Add(dc_aceite);
            dtImprimirBoleto.Columns.Add(dc_data_do_processamento);
            dtImprimirBoleto.Columns.Add(dc_nosso_numero);
            dtImprimirBoleto.Columns.Add(dc_carteira);
            dtImprimirBoleto.Columns.Add(dc_especie);
            dtImprimirBoleto.Columns.Add(dc_quantidade);
            dtImprimirBoleto.Columns.Add(dc_valor);
            dtImprimirBoleto.Columns.Add(dc_valor_documento);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha1);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha2);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha3);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha4);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha5);
            dtImprimirBoleto.Columns.Add(dc_instrucoes_linha6);
            dtImprimirBoleto.Columns.Add(dc_desconto);
            dtImprimirBoleto.Columns.Add(dc_abatimento);
            dtImprimirBoleto.Columns.Add(dc_mora);
            dtImprimirBoleto.Columns.Add(dc_outros_acrescimos);
            dtImprimirBoleto.Columns.Add(dc_valor_cobrado);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha1);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha2);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha3);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha4);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha5);
            dtImprimirBoleto.Columns.Add(dc_sacado_linha6);
            dtImprimirBoleto.Columns.Add(dc_cpf_cnpjsacado);
            dtImprimirBoleto.Columns.Add(dc_cpf_cnpjcedente);
            dtImprimirBoleto.Columns.Add(dc_codBarra);
            dtImprimirBoleto.Columns.Add(dc_imagem_codBarras_2de5_intercalado);

            //adicionando valores ao dt temporario             
            for (int x = 0; x < dtReceberEnviar.Rows.Count; x++)
            {
                DataRow row = dtImprimirBoleto.NewRow();

                row["dc_posicao"] = x + 1;

                row["dc_numero_do_banco"] = clsBancosInfo.banco.ToString().PadLeft(3, '0') + "-7"; // Complemento Santander

                DateTime dat = clsParser.DateTimeParse(dtReceberEnviar.Rows[x]["VENCIMENTO"].ToString());

                String fatorVencimento = dat.Subtract(clsParser.DateTimeParse("07/10/1997 00:00:00")).Days.ToString();

                String cod_barra = clsBancosInfo.banco.ToString().PadLeft(3, '0') + //banco
                    clsBancosInfo.bcomoeda.PadLeft(2, '0').Substring(1, 1) + // moeda
//                    "V" + // colocado apenas para ser contado como o 5o Caracter (Não pode calcular nada com ele)
                    fatorVencimento +
                    dtReceberEnviar.Rows[x]["VALOR"].ToString().Replace(",", "").Replace(".", "").PadLeft(10, '0').Substring(0, 10) + //valor Nominal
                    "9" + //Valor fixo
                    clsBancosInfo.convenio.Substring(0, 7) + // Psk código cedente conta bancaria
                   (dtReceberEnviar.Rows[x]["BOLETONRO"].ToString() + dtReceberEnviar.Rows[x]["DV"].ToString()).PadLeft(13, '0').Substring(0, 13) + // nosso numero
                    "0" +//iof
                    clsBancosInfo.bcomodalidade.PadLeft(3, ' ').Substring(0, 3); // "101" Cobrança Simples 

                ////    //1° Grupo
                //String grupo1 = clsBancosInfo.banco.ToString().PadLeft(3, '0') + clsBancosInfo.bcomoeda.PadLeft(2, '0').Substring(1, 1) + "9" +
                //     fatorVencimento; // "5759"
                
                //DEXEI ASSIM - COMO NO DIA 13/11 - ONDE O COD PSK SÁI CORRETO, O QUE FALTA CORREÇÃO É APENAS O DV
                String DV_Barra = clsFinanceiro.Digito_Mod11BarrasSantander(cod_barra.ToString());




                //    //1° Grupo
                String grupo1 = clsBancosInfo.banco.ToString().PadLeft(3, '0') + clsBancosInfo.bcomoeda.PadLeft(2, '0').Substring(1, 1) + "9" +
                     clsBancosInfo.convenio.PadLeft(7, ' ').Substring(0, 4); // "5759"


                String grupo1Dv = clsFinanceiro.Digito_Mod10Santander(grupo1);
                //ATÉ AQUI - DANI

                ////2° Grupo

                String grupo2 = clsBancosInfo.convenio.PadLeft(7, ' ').Substring(4, 3) + //"382"  Codigo do Cedente
                     (dtReceberEnviar.Rows[x]["BOLETONRO"].ToString() + dtReceberEnviar.Rows[x]["DV"].ToString()).PadLeft(13, '0').Substring(0, 7); // nosso numero

                String grupo2Dv = clsFinanceiro.Digito_Mod10Santander(grupo2);

                //////3° Grupo

                String grupo3 = (dtReceberEnviar.Rows[x]["BOLETONRO"].ToString() + dtReceberEnviar.Rows[x]["DV"].ToString()).PadLeft(13,'0').Substring(7, 6) +// nosso numero
                    "0" + //IOS
                     clsBancosInfo.bcomodalidade.PadLeft(3, ' ').Substring(0, 3); // "101" Carteira
                String grupo3Dv = clsFinanceiro.Digito_Mod10Santander(grupo3);

                /////4° Grupo

                String grupo4 = DV_Barra;

                ////5° Grupo

                String grupo5 = fatorVencimento +
                  dtReceberEnviar.Rows[x]["VALOR"].ToString().Replace(",", "").Replace(".", "").PadLeft(10, '0').Substring(0, 10); //valor Nominal


                row["dc_linha_digitavel"] = (grupo1 + grupo1Dv).Insert(5, ".") + " " +
                                            (grupo2 + grupo2Dv).Insert(5, ".") + " " +
                                            (grupo3 + grupo3Dv).Insert(5, ".") + " " +
                                            grupo4 + " " +
                                            grupo5;

                ///////////////////////////////////////////////////////////////////////////////////

                row["dc_local_de_pagamento"] = "Pagar Preferencialmente no Grupo Santarder Banespa - GC";

                row["dc_vencimento"] = dtReceberEnviar.Rows[x]["VENCIMENTO"];

                row["dc_cedente"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select nome FROM EMPRESA where codigo = "+ clsInfo.zfilial, "");

                row["dc_agencia_identificacao_cedente"] = cagencia.ToString() + " / " + cctacorrente.ToString();

                row["dc_data_do_documento"] = dtReceberEnviar.Rows[x]["EMISSAO"];

                row["dc_numero_do_documento"] = dtReceberEnviar.Rows[x]["DUPLICATA"];

                row["dc_especie_do_documento"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSESPECIE " +
                                     "where id = " + clsBancosInfo.idbcoespecie, "DM").ToUpper(); //"DM";

                row["dc_aceite"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSACEITE " +
                                     "where id = " + clsBancosInfo.idbcoaceite, "N").ToUpper(); // "N";

                row["dc_data_do_processamento"] = DateTime.Now;

                row["dc_nosso_numero"] = dtReceberEnviar.Rows[x]["BOLETONRO"].ToString().PadLeft(12, '0')
                    + "-" + dtReceberEnviar.Rows[x]["DV"].ToString();

                row["dc_carteira"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMODALIDADE " +
                                     "where id = " + clsBancosInfo.idbcomodalidade, "").ToUpper().PadLeft(3, ' ').Substring(0, 3); // "101 - Cobrança Simples ECR";

                row["dc_especie"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from TAB_BANCOSMOEDA " +
                                     "where id = " + clsBancosInfo.idbcomoeda,"REAL").ToUpper().Replace("PARA",""); // "REAL";

                row["dc_quantidade"] = "0";

                row["dc_valor"] = "0";

                row["dc_valor_documento"] = dtReceberEnviar.Rows[x]["VALOR"];

                row["dc_instrucoes_linha1"] = tbxBoletolinha01.Text;
                row["dc_instrucoes_linha2"] = tbxBoletolinha02.Text;

                row["dc_instrucoes_linha3"] = "Juros de Mora de R$" +
                    clsParser.DecimalParse(dtReceberEnviar.Rows[x]["VALORJUROS"].ToString()).ToString() +
                    " ao dia.";

                row["dc_instrucoes_linha4"] = "Multa de " + ((2 * 100) / 100).ToString("N2") + "% após vencimento.";
                row["dc_instrucoes_linha5"] = "";
                row["dc_instrucoes_linha6"] = "";

                row["dc_desconto"] = dtReceberEnviar.Rows[x]["VALORDESCONTO"];

                row["dc_abatimento"] = "0";
                row["dc_mora"] = "0";
                row["dc_outros_acrescimos"] = "0";

                row["dc_valor_cobrado"] = dtReceberEnviar.Rows[x]["VALORLIQUIDO"].ToString().ToUpper();

                row["dc_sacado_linha1"] = dtReceberEnviar.Rows[x]["NOMECLIENTE"].ToString().ToUpper()
                    + " - CNPJ/CPF : " + dtReceberEnviar.Rows[x]["CGC"].ToString();

                row["dc_sacado_linha2"] = dtReceberEnviar.Rows[x]["ENDERECO"].ToString().ToUpper()
                    + " Bairro: " + dtReceberEnviar.Rows[x]["BAIRRO"].ToString().ToUpper();

                row["dc_sacado_linha3"] = dtReceberEnviar.Rows[x]["CEP"].ToString()
                        + "        " + dtReceberEnviar.Rows[x]["CIDADE"].ToString().ToUpper()
                        + " / " + dtReceberEnviar.Rows[x]["ESTADO"].ToString().ToUpper();

                row["dc_sacado_linha4"] = "";
                row["dc_sacado_linha5"] = "Sacador/Avalista:";
                row["dc_sacado_linha6"] = "";

                row["dc_cpf_cnpjsacado"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                             "select cgc from cliente where id = " + clsParser.Int32Parse(dtReceberEnviar.Rows[x]["IDCLIENTE"].ToString()), "");

                row["dc_cpf_cnpjcedente"] = ccgc;

                //row["dc_codBarra"] = (grupo1 + grupo1Dv).Insert(5, ".") + " " +
                //                            (grupo2 + grupo2Dv).Insert(5, ".") + " " +
                //                            (grupo3 + grupo3Dv).Insert(5, ".") + " " +
                //                            grupo4 + " " +
                //                            grupo5;

                cod_barra = cod_barra.Insert(4, DV_Barra);
                row["dc_codBarra"] = cod_barra.ToString();

                //imagem do codigo de barras

                ptbCodBarras.Image = Barcode.Encode(TYPE.Interleaved2of5,
                             cod_barra.ToString(),
                             Color.Black, Color.White, 400, 20);

                row["dc_imagem_codBarras_2de5_intercalado"] = clsParser.imageToByteArray(ptbCodBarras.Image);

                dtImprimirBoleto.Rows.Add(row);
                dtImprimirBoleto.AcceptChanges();
            }
            //fim adicionando valores 

            //parametros -- opcional ---
            String cabecalho = "";
            ParameterFields pfields = new ParameterFields();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);
            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);  

            //imprimindo os boletos
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            frmCrystalReport.Init(clsInfo.caminhorelatorios, "BOLETO_SANTANDER.RPT", dtImprimirBoleto,
                                   pfields, "", clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
           
        }

        private void tspSalvar1_Click(object sender, EventArgs e)
        {
            DialogResult drt = Salvar();

            if (drt == DialogResult.Yes)
            {
                painelPrincipal = 0;
                tclResolve();               
            }
            if (drt == DialogResult.No)
            {
                painelPrincipal = 0;
                tclResolve();               
            }                      
        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                BancosSalvar();
            }
            return drt;
        }

        private void BancosSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: Bancos
                clsBancosInfo = new clsBancosInfo();
                //salva no banco interno de destino
                clsBancosInfo = clsBancosBLL.Carregar(idBancoIntDes, clsInfo.conexaosqlbanco);

                clsBancosInfo.idbcoespecie = idBcoEspecie;
                clsBancosInfo.idbcomoeda = idBcoMoeda;
                clsBancosInfo.idbcoaceite = idBcoAceite;
                clsBancosInfo.idbcoprotestar = idBcoProtestar;
                clsBancosInfo.idbcoprotestar2 = idBcoProtestar2;
                clsBancosInfo.idbcoimpressao = idBcoImpressao;
                clsBancosInfo.idbcomodalidade = idBcoModalidade; 

                clsBancosInfo.bcoespecie = tbxBcoEspecie.Text;
                clsBancosInfo.bcomoeda = tbxBcoMoeda.Text;
                clsBancosInfo.bcoaceite = tbxBcoAceite.Text;
                clsBancosInfo.bcoprotestar = tbxBcoProtestar.Text;
                clsBancosInfo.bcoimpressao = tbxBcoImpressao.Text;
                clsBancosInfo.bcomodalidade = tbxBcoModalidade.Text;

                clsBancosInfo.limite = clsParser.DecimalParse(tbxLimite.Text);
                //clsBancosInfo.nroboleto = tbxNroBoleto.Text;
                clsBancosInfo.layoutposicoes = tbxLayoutPosicoes.Text;
                clsBancosInfo.codigodetransmissao = tbxBcoCodigoTransmissao.Text;               

                clsBancosBLL.Alterar(clsBancosInfo, clsInfo.conexaosqlbanco);
                //

                //salvando na empresa
                clsEmpresaGereInfo = new clsEmpresaGereInfo();

                clsEmpresaGereInfo = clsEmpresaGereBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);

                clsEmpresaGereInfo.jurosmes = clsParser.DecimalParse(tbxJurosMes.Text);
                clsEmpresaGereInfo.protestar = clsParser.Int32Parse(tbxProtestar.Text);
                clsEmpresaGereInfo.boletolinha01 = tbxBoletolinha01.Text;
                clsEmpresaGereInfo.sequencia = clsParser.Int32Parse(tbxSequencia.Text);
                clsEmpresaGereInfo.registro = clsParser.Int32Parse(tbxBoletoProximo.Text);

                clsEmpresaGereBLL.Alterar(clsEmpresaGereInfo, clsInfo.conexaosqldados);
                //
                tse.Complete();
                MessageBox.Show("Dados salvos com sucesso!", "Aplisoft", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);                    
            }
        }

        private void bwrDocumentos_Run()
        {
            if (carregandoDocumentos == false)
            {
                carregandoDocumentos = true;

                bwrDocumentos = new BackgroundWorker();
                bwrDocumentos.DoWork += new DoWorkEventHandler(bwrDocumentos_DoWork);
                bwrDocumentos.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrDocumentos_RunWorkerCompleted);
                bwrDocumentos.RunWorkerAsync();
            }
        }


        private void bwrDocumentos_DoWork(object sender, DoWorkEventArgs e)
        {
            query = "";
            dtDocumentos = new DataTable();

            if (tipoDocumento.ToUpper() == "ESPECIE")
            {
                query = "SELECT TAB_BANCOSESPECIE.ID,TAB_BANCOSESPECIE.CODIGO, TAB_BANCOSESPECIE.NOME " +
                               "FROM TAB_BANCOSESPECIE ";
                query = query + " WHERE  TAB_BANCOSESPECIE.IDCODIGO = " + idBancoIntDes;
                query = query + " ORDER BY TAB_BANCOSESPECIE.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "MOEDA")
            {
                query = "SELECT TAB_BANCOSMOEDA.ID,TAB_BANCOSMOEDA.CODIGO, TAB_BANCOSMOEDA.NOME " +
                               "FROM TAB_BANCOSMOEDA ";
                query = query + " WHERE  TAB_BANCOSMOEDA.IDCODIGO = " + idBancoIntDes;
                query = query + " ORDER BY TAB_BANCOSMOEDA.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "ACEITE")
            {
                query = "SELECT TAB_BANCOSACEITE.ID,TAB_BANCOSACEITE.CODIGO, TAB_BANCOSACEITE.NOME " +
                               "FROM TAB_BANCOSACEITE ";
                query = query + " WHERE  TAB_BANCOSACEITE.IDCODIGO = " + idBancoIntDes;
                query = query + " ORDER BY TAB_BANCOSACEITE.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "PROTESTAR")
            {
                query = "SELECT TAB_BANCOSPROTESTAR.ID,TAB_BANCOSPROTESTAR.CODIGO, TAB_BANCOSPROTESTAR.NOME " +
                               "FROM TAB_BANCOSPROTESTAR ";
                query = query + " WHERE  TAB_BANCOSPROTESTAR.IDCODIGO = " + idBancoIntDes;
                query = query + " ORDER BY TAB_BANCOSPROTESTAR.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "IMPRESSAO")
            {
                query = "SELECT TAB_BANCOSIMPRESSAO.ID,TAB_BANCOSIMPRESSAO.CODIGO, TAB_BANCOSIMPRESSAO.NOME " +
                               "FROM TAB_BANCOSIMPRESSAO ";
                query = query + " WHERE  TAB_BANCOSIMPRESSAO.IDCODIGO = " + idBancoIntDes;
                query = query + " ORDER BY TAB_BANCOSIMPRESSAO.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "MODALIDADE")
            {
                query = "SELECT TAB_BANCOSMODALIDADE.ID,TAB_BANCOSMODALIDADE.CODIGO, TAB_BANCOSMODALIDADE.NOME " +
                               "FROM TAB_BANCOSMODALIDADE ";
                query = query + " WHERE  TAB_BANCOSMODALIDADE.IDCODIGO = " + idBancoIntDes;
                query = query + " ORDER BY TAB_BANCOSMODALIDADE.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "PROTESTAR2")
            {
                query = "SELECT TAB_BANCOSPROTESTAR.ID,TAB_BANCOSPROTESTAR.CODIGO, TAB_BANCOSPROTESTAR.NOME " +
                               "FROM TAB_BANCOSPROTESTAR ";
                query = query + " WHERE  TAB_BANCOSPROTESTAR.IDCODIGO = " + idBancoIntDes;
                query = query + " ORDER BY TAB_BANCOSPROTESTAR.CODIGO ";
            }
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtDocumentos);
        }

        private void bwrDocumentos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvDocumentos.DataSource = dtDocumentos;
                clsGridHelper.MontaGrid2(dgvDocumentos, dtDocumentosColunas, true);
                clsGridHelper.FontGrid(dgvDocumentos, 7);
                Ponteiro(tipoDocumento);
                carregandoDocumentos = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoDocumentos = false;
            }
        }
        private void btnIdBcoEspecie_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Especie";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }        

        private void btnIdBcoMoeda_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Moeda";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoAceite_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Aceite";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoProtestar_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Protestar";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoImpressao_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Impressao";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoModalidade_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Modalidade";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdProtestar2_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Protestar2";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void dgvDocumentos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvDocumentos.CurrentRow != null)
            {
                if (tipoDocumento.ToUpper() == "ESPECIE")
                {
                    idBcoEspecie = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoEspecie.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoEspecie.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoEspecie.Select();
                    tbxBcoEspecie.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "MOEDA")
                {
                    idBcoMoeda = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoMoeda.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoMoeda.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoMoeda.Select();
                    tbxBcoMoeda.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "ACEITE")
                {
                    idBcoAceite = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoAceite.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoAceite.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoAceite.Select();
                    tbxBcoAceite.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "PROTESTAR")
                {
                    idBcoProtestar = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoProtestar.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoProtestar.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoProtestar.Select();
                    tbxBcoProtestar.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "IMPRESSAO")
                {
                    idBcoImpressao = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoImpressao.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoImpressao.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoImpressao.Select();
                    tbxBcoImpressao.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "MODALIDADE")
                {
                    idBcoModalidade = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoModalidade.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoModalidade.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoModalidade.Select();
                    tbxBcoModalidade.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "PROTESTAR2")
                {
                    idBcoProtestar2 = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoProtestar2.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoProtestar2.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoProtestar2.Select();
                    tbxBcoProtestar2.SelectAll();
                }
            }
        }

        private void Ponteiro(String _tipoDocumento)
        {
            dgvDocumentos.MultiSelect = false;
            String _codigo = "";

            switch (_tipoDocumento.ToUpper())
            {
                case "ESPECIE":
                    _codigo = tbxBcoEspecie.Text;
                    break;
                case "MOEDA":
                    _codigo = tbxBcoMoeda.Text;
                    break;
                case "ACEITE":
                    _codigo = tbxBcoAceite.Text;
                    break;
                case "PROTESTAR":
                    _codigo = tbxBcoProtestar.Text;
                    break;
                case "IMPRESSAO":
                    _codigo = tbxBcoImpressao.Text;
                    break;
                case "MODALIDADE":
                    _codigo = tbxBcoModalidade.Text;
                    break;
                case "PROTESTAR2":
                    _codigo = tbxBcoProtestar2.Text;
                    break;
                default:
                    _codigo = "";
                    break;
            }

            if (clsGridHelper.SelecionaLinha_ReturnBoolean(_codigo, dgvDocumentos, "codigo") == false)
            {
                if (dgvDocumentos.Rows.Count > 0)
                {
                    dgvDocumentos.CurrentCell = dgvDocumentos.Rows[0].Cells["codigo"];
                }
            }
        }

        private void tclDuplicatas_SelectedIndexChanged(object sender, EventArgs e)
        {
            tclResolve();
        }

        private void tclResolve()
        {
            if (painelPrincipal == 0)
            {
                //tclDuplicatas.SelectedIndex = 0;
                tclDuplicatas.SelectedTab = tabBanco;
            }
            else
            {
                //tclDuplicatas.SelectedIndex = 1;
                tclDuplicatas.SelectedTab = tabEnviar;
            }
        }

        private void TrazDadosEmpresa_Banco()
        {
            // Carregar os Dados para envio da cobrança do Aplibank
            idBcoEspecie = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBCOESPECIE from BANCOS where ID= " + idBancoIntDes));
            tbxBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM TAB_BANCOSESPECIE WHERE ID=" + idBcoEspecie);
            lblBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM TAB_BANCOSESPECIE WHERE ID= " + idBcoEspecie);
            idBcoMoeda = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBCOMOEDA from BANCOS where ID= " + idBancoIntDes));
            tbxBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSMOEDA where ID= " + idBcoMoeda);
            lblBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from TAB_BANCOSMOEDA where ID= " + idBcoMoeda);
            idBcoAceite = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBCOACEITE from BANCOS where ID= " + idBancoIntDes));
            tbxBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSACEITE where ID= " + idBcoAceite);
            lblBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from TAB_BANCOSACEITE where ID= " + idBcoAceite);
            idBcoProtestar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBCOPROTESTAR from BANCOS where ID= " + idBancoIntDes));
            tbxBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar);
            lblBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar);
            idBcoImpressao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBCOIMPRESSAO from BANCOS where ID= " + idBancoIntDes));
            tbxBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSIMPRESSAO where ID= " + idBcoImpressao);
            lblBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSIMPRESSAO where ID= " + idBcoImpressao);
            idBcoModalidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select idbcomodalidade from BANCOS where ID= " + idBancoIntDes));
            tbxBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMODALIDADE where ID= " + idBcoModalidade);
            lblBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMODALIDADE where ID= " + idBcoModalidade);

            tbxLimite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select LIMITE from BANCOS where ID= " + idBancoIntDes);
            tbxNroBoleto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NROBOLETO from BANCOS where ID= " + idBancoIntDes);
            tbxLayoutPosicoes.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select LAYOUTPOSICOES from BANCOS where ID= " + idBancoIntDes);

            idBcoProtestar2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBCOPROTESTAR from BANCOS where ID= " + idBancoIntDes));
            tbxBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar2);
            lblBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from TAB_BANCOSPROTESTAR where ID= " + idBcoProtestar2);

            tbxBoletoProximo.Text = (clsParser.DecimalParse(tbxNroBoleto.Text) + 1).ToString();
            tbxBoletoProximo1.Text = tbxBoletoProximo.Text;
            tbxSequencia1.Text = tbxSequencia.Text;
            tbxJurosMes1.Text = tbxJurosMes.Text;
            tbxProtestar1.Text = tbxProtestar.Text;
            tbxBoletolinha011.Text = tbxBoletolinha01.Text;

            tbxBoletolinha02.Text = "";
            tbxBoletolinha03.Text = "";
            tbxBoletolinha04.Text = "";
            tbxBoletolinha05.Text = "";

            tbxBcoNroInscrEmpresa.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select cgc FROM EMPRESA where id = " + clsInfo.zempresaid, "");

            tbxBcoCodigoTransmissao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco,
                           "select codigodetransmissao from BANCOS where ID= " + idBancoIntDes);


            //Dados da empresa
            tbxJurosMes.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select JUROSMES FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid)).ToString("N2");
            tbxProtestar.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PROTESTAR FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid)).ToString("N0");
            tbxBoletolinha01.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETOLINHA01 FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            tbxBoletolinha02.Text = "";
            tbxBoletolinha03.Text = "";
            tbxBoletolinha04.Text = "";
            tbxBoletolinha05.Text = "";
            tbxSequencia.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select SEQUENCIA FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            tbxBoletoProximo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select REGISTRO FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
       
        }

        private void dgvReceberBanco_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvReceberBanco_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvReceberBanco.CurrentRow != null)
            {
                if (clsParser.DateTimeParse(dgvReceberBanco.CurrentRow.Cells["VENCIMENTO"].Value.ToString()) <
                   clsParser.DateTimeParse(dgvReceberBanco.CurrentRow.Cells["EMISSAO"].Value.ToString()))
                {
                    MessageBox.Show("Data de vencimento menor que a de emissão favor verificar!", "ApliSoft",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                idReceberAberto = (Int32)dgvReceberBanco.CurrentRow.Cells[0].Value;
                if (idReceberAberto > 0)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja retirar este boleto do Banco ? ", "Aplisoft",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("UPDATE RECEBER SET IDBANCO=@IDBANCO, IDBANCOINT=@IDBANCOINT, IDSITBANCO=@IDSITBANCO, BOLETONRO=@BOLETONRO, DV=@DV WHERE ID=@ID ", scn);
                        scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idReceberAberto;
                        scd.Parameters.AddWithValue("@IDBANCO", SqlDbType.Int).Value = clsInfo.zbanco;
                        scd.Parameters.AddWithValue("@IDBANCOINT", SqlDbType.Int).Value = clsInfo.zbancoint;
                        scd.Parameters.AddWithValue("@IDSITBANCO", SqlDbType.Int).Value = clsInfo.zsituacaotitulo;
                        scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.AddWithValue("@DV", SqlDbType.NVarChar).Value = "";
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();

                        bwrReceberAberto_Run();
                    }
                }
                
            }

        }
    }
}
