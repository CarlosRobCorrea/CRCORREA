using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmCadCliente : Form
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

        clsCadClienteBLL clsCadClienteBLL;
        clsCadClienteInfo clsCadClienteInfo;
        clsCadClienteInfo clsCadClienteInfoOld;

        DataGridViewRowCollection rows;

        clsCadClienteEnderecoBLL clsCadClienteEnderecoBLL;
        clsCadClienteEnderecoInfo clsCadClienteEnderecoInfo;
        DataTable dtClienteEndereco;
        BackgroundWorker bwrClienteEndereco;
        GridColuna[] dtClienteEnderecoColunas;

        clsCadClienteObservaBLL clsCadClienteObservaBLL;
        clsCadClienteObservaInfo clsCadClienteObservaInfo;
        DataTable dtClienteObserva;
        BackgroundWorker bwrClienteObserva;
        GridColuna[] dtClienteObservaColunas;

        clsCadClienteContatoBLL clsCadClienteContatoBLL;
        clsCadClienteContatoInfo clsCadClienteContatoInfo;
        DataTable dtClienteContato;
        BackgroundWorker bwrClienteContato;
        GridColuna[] dtClienteContatoColunas;


        DataTable dtFichaFinanceira;
        DataTable dtFichaComercial;

        public void Init(Int32 _id)
        {
            id = _id;
            //rows = _rows;

           clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tab_cnae order by codigo", tbxCnae_codigo);
            //String query = "select cidades.nome + '   -   ' + ESTADOS.ESTADO from cidades " +
            //    " left join estados on estados.id = CIDADES.IDESTADO ";
           //clsVisual.FillComboBox(cbxCidade, query, clsInfo.conexaosqldados);
            //           clsVisual.FillComboBox(cbxCidade, "select nome from cidades order by nome", clsInfo.conexaosqldados);
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

            //dtClienterequisitoColunas = new GridColuna[]
            //{
            //    new GridColuna("Ordem", "ordem", 70, true, DataGridViewContentAlignment.MiddleRight),
            //    new GridColuna("Requisito", "requisito", 1000, true, DataGridViewContentAlignment.MiddleLeft)
            //};

            clsCadClienteBLL = new clsCadClienteBLL();
            clsCadClienteEnderecoBLL = new clsCadClienteEnderecoBLL();
            clsCadClienteContatoBLL = new clsCadClienteContatoBLL();
            clsCadClienteObservaBLL = new clsCadClienteObservaBLL();
//            clsClienterequisitoBLL = new clsClienterequisitoBLL();
        }

        public frmCadCliente()
        {
            InitializeComponent();
        }

        private void frmEmpresa_Load(object sender, EventArgs e)
        {
            ClienteRegistro();
        }
        private void tspSalvar_Click(object sender, EventArgs e)
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
                clsCadClienteInfo = new clsCadClienteInfo();
                ClienteInfo(clsCadClienteInfo);

                if (clsCadClienteBLL.Equals(clsCadClienteInfoOld, clsCadClienteInfo) == false)
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

        void ClienteRegistro()
        {
            if (id == 0)
            {
                clsCadClienteInfo = new clsCadClienteInfo();
                clsCadClienteInfo.agencia = "";
                clsCadClienteInfo.andar = "";
                clsCadClienteInfo.ativo = "S";
                clsCadClienteInfo.bairro = "";
                clsCadClienteInfo.cadastrado = DateTime.Now;
                clsCadClienteInfo.cep = "";
                clsCadClienteInfo.cgc = "";
                clsCadClienteInfo.cidade = "";
                clsCadClienteInfo.clicontab = 0;
                clsCadClienteInfo.clienteaprovado = "N";
                clsCadClienteInfo.clientedesde = DateTime.Now;
                clsCadClienteInfo.codigocliente = "0";
                clsCadClienteInfo.cognome = "";
                clsCadClienteInfo.comissaocomo = "V";
                clsCadClienteInfo.comissaopagto = "D";
                clsCadClienteInfo.comissaopor = "G";
                clsCadClienteInfo.comple = "";
                clsCadClienteInfo.contato = "";
                clsCadClienteInfo.credito = "";
                clsCadClienteInfo.ctacorrente = "";
                clsCadClienteInfo.dataaprovado = DateTime.MinValue;
                clsCadClienteInfo.datacompra = DateTime.MinValue;
                clsCadClienteInfo.datafichafinanc = DateTime.MinValue;
                clsCadClienteInfo.datanasc = DateTime.MinValue;
                clsCadClienteInfo.ddd = "";
                clsCadClienteInfo.desconto = 0;
                clsCadClienteInfo.descontacofinsent = "N";
                clsCadClienteInfo.descontacofinssai = "N";
                clsCadClienteInfo.descontapispasepent = "N";
                clsCadClienteInfo.descontapispasepsai = "N";
                clsCadClienteInfo.diasemana = "Domingo";
                clsCadClienteInfo.email = "";
                clsCadClienteInfo.emailnfe = "";
                clsCadClienteInfo.endereco = "";
                clsCadClienteInfo.endtipo = "";
                clsCadClienteInfo.freteincluso = "N";
                clsCadClienteInfo.freteporconta = "F";
                clsCadClienteInfo.ftp = "";
                clsCadClienteInfo.homepage = "";
                clsCadClienteInfo.ibge = "";
                clsCadClienteInfo.idbanco = clsInfo.zbanco;
                clsCadClienteInfo.idestado = clsInfo.zempresa_ufid;

                //clsCadClienteInfo.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where codigo='" + clsInfo.zempresa_cidadeibge.ToString().PadRight(8, ' ').Substring(2, 5) + "' ", "0"));
                clsCadClienteInfo.idcondpagto = clsInfo.zcondpagto;

                clsCadClienteInfo.idformapagto = clsInfo.zformapagto;
                clsCadClienteInfo.idprospeccao = 0;
                clsCadClienteInfo.idramo = clsInfo.zramo;
                //clsCadClienteInfo.idregimeapuracao = clsInfo.zregimeapuracao;
                clsCadClienteInfo.idtransportadora = clsInfo.zempresaclienteid;
                clsCadClienteInfo.idredespacho = clsInfo.zempresaclienteid;
                clsCadClienteInfo.idzona = clsInfo.zzona;
                //clsCadClienteInfo.idrepresentante = clsInfo.zempresaclienteid;
                clsCadClienteInfo.idsupervisor = clsInfo.zempresaclienteid;
                clsCadClienteInfo.idcoordenador = clsInfo.zempresaclienteid;
                clsCadClienteInfo.idvendedor = clsInfo.zempresaclienteid;
                clsCadClienteInfo.ie = "";
                clsCadClienteInfo.imunicipal = "";
                clsCadClienteInfo.meiodetransporte = "N";
                clsCadClienteInfo.nome = "";
                clsCadClienteInfo.numeroend = "0";
                clsCadClienteInfo.observa = "";
                clsCadClienteInfo.pais = "BRA";
                clsCadClienteInfo.pessoa = "F";
                clsCadClienteInfo.regiao = "CO";
                clsCadClienteInfo.suframa = "";
                clsCadClienteInfo.telefone = "";
                clsCadClienteInfo.temfaturamento = "N";
                clsCadClienteInfo.tipo = "C";
                clsCadClienteInfo.tipocomple = "";
                clsCadClienteInfo.tiponumero = "Nº";
                clsCadClienteInfo.titularcta = "";
                clsCadClienteInfo.ultdatnf = clsParser.DateTimeParse("01/01/1900");
                clsCadClienteInfo.ultdatorc = clsParser.DateTimeParse("01/01/1900");
                clsCadClienteInfo.isentoipi = "N";
                clsCadClienteInfo.iddizeresnota = clsCadClienteInfo.iddizeresnota;
                clsCadClienteInfo.revendedor = "N";
                clsCadClienteInfo.contribuinte = "S";
                clsCadClienteInfo.alc = "N";
                clsCadClienteInfo.consumo = "S";
                clsCadClienteInfo.datafichafinanc = DateTime.Now;
                clsCadClienteInfo.saldofichafinanc = 0;
            }
            else
            {
                clsCadClienteInfo = clsCadClienteBLL.Carregar(id, clsInfo.conexaosqldados);
            }

            clsCadClienteInfoOld = new clsCadClienteInfo();
            ClienteCampos(clsCadClienteInfo);
            ClienteInfo(clsCadClienteInfoOld);

            if (id != 0)
            { // carregar o fluxo financeiro deste cliente
              //tbxDatade.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
              //tbxDataate.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
              //dtFichaFinanceira = clsFinanceiro.FichaFinanceira(0, id, clsCadClienteInfo.cognome, "VENC", clsParser.DateTimeParse("01/01/1900"), clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()), clsParser.DecimalParse(tbxSaldoFichaFinanc.Text));
              //clsFinanceiro.GridFichaFinanceiraMonta(dgvFichaFinanceira, dtFichaFinanceira, 0);

                //// carregar o fluxo comercial do cliente
                //tbxDatadeC.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
                //tbxDataateC.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");
                //dtFichaComercial = clsFinanceiro.FichaComercial(0, id, clsCadClienteInfo.cognome, clsParser.DateTimeParse("01/01/1900"),Parser.DateTimeParse(DateTime.Now.AddYears(10).ToString()));
                //clsFinanceiro.GridFichaComercialMonta(dgvFichaComercial, dtFichaComercial, 0);

            }
        }

        void ClienteCampos(clsCadClienteInfo info)
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
            //tbxDatanasc.Text = ExibirData(info.datanasc);
            tbxDdd.Text = info.ddd;
            tbxTelefone.Text = info.telefone;
            tbxContato.Text = info.contato;
            cbxTipo.SelectedIndex = SelecionarIndex(info.tipo, 1, cbxTipo);
            cbxPessoa.SelectedIndex = SelecionarIndex(info.pessoa, 1, cbxPessoa);

            PessoaTipo();

            cbxAtivo.SelectedIndex = SelecionarIndex(info.ativo, 1, cbxAtivo);
            tbxCgc.Text = info.cgc;
            tbxIe.Text = info.ie;
            tbxImunicipal.Text = info.imunicipal;
            tbxSuframa.Text = info.suframa;
            tbxCnae_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tab_cnae where id=" + idcnae.ToString(), "");
            tbxEmail.Text = info.email;
            tbxHomepage.Text = info.homepage;
            tbxFtp.Text = info.ftp;
            tbxCep.Text = info.cep;
            //info.endtipo = TipoRua(info.endtipo);
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
            //if (info.idcidade == 0)
            //{
            //    info.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + clsVisual.RemoveAcentos(info.cidade).ToUpper() + "' ", "0"));

            //}
            //tbxCidade.Text = info.cidade;
            //cidade = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from cidades where id=" + info.idcidade, "");
            ////cbxCidade.Text =clsVisual.SelecionarIndex(cidade, int(cidade.Length), cbxCidade);
            //cbxCidade.SelectedIndex = cbxCidade.FindString(cidade);
            //if (cbxCidade.SelectedIndex == -1)
            //{
            //    cbxCidade.SelectedIndex = 0;
            //}
            cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString(), ""));
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
            //tbxCadastrado.Text = ExibirDataCompleta(info.cadastrado);
            tbxZona.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from zonas where id=" + idzona.ToString(), "");
            tbxRamo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from ramo where id=" + idramo.ToString(), "");
            tbxRegimeapuracao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from regimeapuracao where id=" + idregimeapuracao.ToString(), "");
            tbxCodigocliente.Text = info.codigocliente;
            cbxFreteincluso.SelectedIndex = SelecionarIndex(info.freteincluso, 1, cbxFreteincluso);
            cbxFreteporconta.SelectedIndex = SelecionarIndex(info.freteporconta, 1, cbxFreteporconta);
            cbxMeiodetransporte.SelectedIndex = SelecionarIndex(info.meiodetransporte, 1, cbxMeiodetransporte);
            tbxTransportadora.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idtransportadora.ToString(), "");
            tbxRedespacho.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idredespacho.ToString(), "");
            tbxRepresentante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idvendedor, "");
            tbxComissaorepresentante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idvendedor, "");
            tbxCondpagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id=" + idcondpagto.ToString() + " and ATIVO = 'S'", "");
            tbxFormapagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from situacaotipotitulo where id=" + idformapagto.ToString(), "");
            cbxCredito.SelectedIndex = SelecionarIndex(info.credito, 1, cbxCredito);
            tbxLimitecredito.Text = info.limitecredito.ToString("N2");
            tbxNrobanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tab_bancos where id=" + idbanco.ToString(), "");
            tbxBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tab_bancos where id=" + idbanco.ToString(), "");
            tbxAgencia.Text = info.agencia;
            tbxCtacorrente.Text = info.ctacorrente;
            tbxTitularcta.Text = info.titularcta;
            cbxClienteaprovado.SelectedIndex = SelecionarIndex(info.clienteaprovado, 1, cbxClienteaprovado);
            //tbxDataaprovado.Text = ExibirData(info.dataaprovado);
            tbxAprovadonome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usuario from usuario where id=" + idaprovadopor.ToString(), "");
            cbxTemfaturamento.SelectedIndex = SelecionarIndex(info.temfaturamento, 1, cbxTemfaturamento);
            tbxValorminimo.Text = info.valorminimo.ToString();
            //tbxClientedesde.Text = ExibirDataCompleta(info.clientedesde);
            //tbxDatacompra.Text = ExibirData(info.datacompra);
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
                    tbxCgc.Text =clsVisual.CamposVisual("CPF", tbxCgc.Text);
                    tbxIe.Text =clsVisual.CamposVisual("RG", tbxIe.Text);
                    break;
                case "J":
                    tbxCgc.Text =clsVisual.CamposVisual("CGC", tbxCgc.Text);
                    tbxIe.Text =clsVisual.CamposVisual("IE", tbxIe.Text);
                    break;
                default:
                    break;
            }
            tbxCep.Text =clsVisual.CamposVisual("CEP", tbxCep.Text);
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
            bwrClienteContato_Run();
            bwrClienteObserva_Run();
            

            //tbxCognome.Select();
            //tbxCognome.SelectAll();
            cbxPessoa.Select();
            cbxPessoa.SelectAll();
        }

        void ClienteInfo(clsCadClienteInfo info)
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
            info.imunicipal = tbxImunicipal.Text.Trim();
            info.suframa = tbxSuframa.Text.Trim();
            info.email = tbxEmail.Text.Replace(" ", "");
            info.homepage = tbxHomepage.Text;
            info.ftp = tbxFtp.Text;
            info.cep = tbxCep.Text;
            info.endtipo = cbxEnd_tipo.Text.Trim();
            info.endereco = tbxEndereco.Text.Trim();
            info.tiponumero = cbxTiponumero.Text.Substring(0, 1);
            info.numeroend = tbxNumeroend.Text;
            info.andar = cbxAndar.Text.Trim();
            info.tipocomple = cbxTipocomple.Text.Trim();
            info.comple = tbxComple.Text.Trim();
            info.bairro = tbxBairro.Text.Trim();
            info.cidade = tbxCidade.Text.Trim();    
            //info.cidade = cbxCidade.Text.Trim();
            //info.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + cbxCidade.Text.Trim() + "' ", ""));
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
        }

        private void ClienteSalvar()
        {
            try
            {
                using (TransactionScope tse = new TransactionScope())
                {
                    clsCadClienteInfo = new clsCadClienteInfo();
                    ClienteInfo(clsCadClienteInfo);
                    clsCadClienteBLL.VerificaInfo(clsCadClienteInfo);
                    // verifica e se existe algum campo em branco
                    if (id == 0)
                    {
                        clsCadClienteInfo.id = clsCadClienteBLL.Incluir(clsCadClienteInfo, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        clsCadClienteBLL.Alterar(clsCadClienteInfo, clsInfo.conexaosqldados);
                    }

                    // Verificar o Cadastro de Prospecção
                    //clsCadClienteInfo.idprospeccao = clsCadClienteBLL.SicronizarCliente(clsCadClienteInfo.id, clsInfo.conexaosqldados);
                    clsCadClienteInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from cliente where id=" + clsCadClienteInfo.id, "0"));
                    clsCadClienteBLL.Alterar(clsCadClienteInfo, clsInfo.conexaosqldados);

                    clsCadClienteEnderecoInfo = new clsCadClienteEnderecoInfo();
                    foreach (DataRow row in dtClienteEndereco.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            clsCadClienteEnderecoInfo.id = 0;
                            
                            clsCadClienteEnderecoInfo.idcliente = clsCadClienteInfo.id;
                            clsCadClienteEnderecoInfo.idcidade = clsParser.Int32Parse(row["idcidade"].ToString());
                            clsCadClienteEnderecoInfo.idestado = clsParser.Int32Parse(row["idestado"].ToString());
                            clsCadClienteEnderecoInfo.idzona = clsParser.Int32Parse(row["idzona"].ToString());
                            clsCadClienteEnderecoInfo.bairro = row["bairro"].ToString();
                            clsCadClienteEnderecoInfo.cep = row["cep"].ToString();
                            clsCadClienteEnderecoInfo.cgc = row["cgc"].ToString();
                            clsCadClienteEnderecoInfo.cidade = row["cidade"].ToString();
                            clsCadClienteEnderecoInfo.cobnome = row["cobnome"].ToString();
                            clsCadClienteEnderecoInfo.contato = row["contato"].ToString();
                            clsCadClienteEnderecoInfo.ddd = row["ddd"].ToString();
                            clsCadClienteEnderecoInfo.dddfax = row["dddfax"].ToString();
                            clsCadClienteEnderecoInfo.dddi = row["dddi"].ToString();
                            clsCadClienteEnderecoInfo.email = row["email"].ToString();
                            clsCadClienteEnderecoInfo.endereco = row["endereco"].ToString();
                            clsCadClienteEnderecoInfo.ibge = row["ibge"].ToString();
                            clsCadClienteEnderecoInfo.ie = row["ie"].ToString();
                            clsCadClienteEnderecoInfo.observa = row["observa"].ToString();
                            clsCadClienteEnderecoInfo.postal = row["postal"].ToString();
                            clsCadClienteEnderecoInfo.setor = row["setor"].ToString();
                            clsCadClienteEnderecoInfo.telefax = row["telefax"].ToString();
                            clsCadClienteEnderecoInfo.telefone = row["telefone"].ToString();
                            clsCadClienteEnderecoInfo.tipoendnome = row["tipoendnome"].ToString();
                            clsCadClienteEnderecoInfo.uf = row["uf"].ToString();

                            row["id"] = clsCadClienteEnderecoBLL.Incluir(clsCadClienteEnderecoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            clsCadClienteEnderecoInfo.id = clsParser.Int32Parse(row["id"].ToString());
                            clsCadClienteEnderecoInfo.idcliente = clsCadClienteInfo.id;
                            clsCadClienteEnderecoInfo.idcidade = clsParser.Int32Parse(row["idcidade"].ToString());
                            clsCadClienteEnderecoInfo.idestado = clsParser.Int32Parse(row["idestado"].ToString());
                            clsCadClienteEnderecoInfo.idzona = clsParser.Int32Parse(row["idzona"].ToString());

                            clsCadClienteEnderecoInfo.bairro = row["bairro"].ToString();
                            clsCadClienteEnderecoInfo.cep = row["cep"].ToString();
                            clsCadClienteEnderecoInfo.cgc = row["cgc"].ToString();
                            clsCadClienteEnderecoInfo.cidade = row["cidade"].ToString();
                            clsCadClienteEnderecoInfo.cobnome = row["cobnome"].ToString();
                            clsCadClienteEnderecoInfo.contato = row["contato"].ToString();
                            clsCadClienteEnderecoInfo.ddd = row["ddd"].ToString();
                            clsCadClienteEnderecoInfo.dddfax = row["dddfax"].ToString();
                            clsCadClienteEnderecoInfo.dddi = row["dddi"].ToString();
                            clsCadClienteEnderecoInfo.email = row["email"].ToString();
                            clsCadClienteEnderecoInfo.endereco = row["endereco"].ToString();
                            clsCadClienteEnderecoInfo.ibge = row["ibge"].ToString();
                            clsCadClienteEnderecoInfo.ie = row["ie"].ToString();
                            clsCadClienteEnderecoInfo.observa = row["observa"].ToString();
                            clsCadClienteEnderecoInfo.postal = row["postal"].ToString();
                            clsCadClienteEnderecoInfo.setor = row["setor"].ToString();
                            clsCadClienteEnderecoInfo.telefax = row["telefax"].ToString();
                            clsCadClienteEnderecoInfo.telefone = row["telefone"].ToString();
                            clsCadClienteEnderecoInfo.tipoendnome = row["tipoendnome"].ToString();
                            clsCadClienteEnderecoInfo.uf = row["uf"].ToString();

                            clsCadClienteEnderecoBLL.Alterar(clsCadClienteEnderecoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            clsCadClienteEnderecoBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        }
                    }
                    if (dtClienteEndereco.Rows.Count == 0)
                    {

                        if (clsCadClienteInfo.tipo == "C")
                        {
                            MessageBox.Show("Não foi incluido nenhum endereço ? " + Environment.NewLine +
                                        "Obrigatorio endereço de cobrança !!" + Environment.NewLine +
                                        "Sistema vai incluir - acerte os dados posterior a esta inclusão ");
                            clsCadClienteEnderecoInfo = new clsCadClienteEnderecoInfo();
                            clsCadClienteEnderecoInfo.id = 0;
                            clsCadClienteEnderecoInfo.idcliente = clsCadClienteInfo.id;
                            //clsCadClienteEnderecoInfo.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + clsVisual.RemoveAcentos(cbxCidade.Text).ToUpper() + "' ", "0"));
                            clsCadClienteEnderecoInfo.idestado = idestado;
                            clsCadClienteEnderecoInfo.idzona = idzona;

                            clsCadClienteEnderecoInfo.bairro = tbxBairro.Text.PadRight(' ', ' ').Substring(0, 12);
                            clsCadClienteEnderecoInfo.cep = tbxCep.Text;
                            clsCadClienteEnderecoInfo.cgc = tbxCgc.Text;
                            clsCadClienteEnderecoInfo.cidade = tbxCidade.Text;
                            clsCadClienteEnderecoInfo.cobnome = tbxNome.Text;
                            clsCadClienteEnderecoInfo.contato = tbxContato.Text;
                            clsCadClienteEnderecoInfo.ddd = tbxDdd.Text;
                            clsCadClienteEnderecoInfo.dddfax = ""; ;
                            clsCadClienteEnderecoInfo.dddi = "";
                            clsCadClienteEnderecoInfo.email = tbxEmail.Text;
                            clsCadClienteEnderecoInfo.endereco = cbxEnd_tipo.Text + " " + tbxEndereco.Text.PadRight(' ', ' ').Substring(0, 27) + " " + cbxTiponumero.Text + " " + tbxNumeroend.Text + " " + cbxAndar.Text + " " + cbxTipocomple.Text + " " + tbxComple.Text;
                            clsCadClienteEnderecoInfo.ibge = tbxIbge.Text;
                            clsCadClienteEnderecoInfo.ie = tbxIe.Text;
                            clsCadClienteEnderecoInfo.observa = "";
                            clsCadClienteEnderecoInfo.postal = "";
                            clsCadClienteEnderecoInfo.setor = "";
                            clsCadClienteEnderecoInfo.telefax = "";
                            clsCadClienteEnderecoInfo.telefone = tbxTelefone.Text;
                            clsCadClienteEnderecoInfo.tipoendnome = "1 - Cobrança";
                            clsCadClienteEnderecoInfo.uf = cbxUf.Text;

                            Int32 idEndereco = clsCadClienteEnderecoBLL.Incluir(clsCadClienteEnderecoInfo, clsInfo.conexaosqldados);
                        }
                    }
                    clsCadClienteObservaInfo = new clsCadClienteObservaInfo();
                    foreach (DataRow row in dtClienteObserva.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            clsCadClienteObservaInfo.id = 0;
                            clsCadClienteObservaInfo.idemitente = clsParser.Int32Parse(row["idemitente"].ToString());
                            clsCadClienteObservaInfo.idcliente = clsCadClienteInfo.idprospeccao;
                            clsCadClienteObservaInfo.idvendedor = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());

                            clsCadClienteObservaInfo.contatado = row["contatado"].ToString();
                            clsCadClienteObservaInfo.contatoagenda = row["contatoagenda"].ToString();
                            clsCadClienteObservaInfo.contatofim = row["contatofim"].ToString();
                            clsCadClienteObservaInfo.data = clsParser.DateTimeParse(row["data"].ToString());
                            clsCadClienteObservaInfo.dataagenda = clsParser.DateTimeParse(row["dataagenda"].ToString());
                            clsCadClienteObservaInfo.datafim = clsParser.DateTimeParse(row["datafim"].ToString());
                            clsCadClienteObservaInfo.emitente = row["emitente"].ToString();
                            clsCadClienteObservaInfo.fechada = row["fechada"].ToString();
                            clsCadClienteObservaInfo.ligacao = row["ligacao"].ToString();
                            clsCadClienteObservaInfo.observar = row["observar"].ToString();
                            clsCadClienteObservaInfo.observaragenda = row["observaragenda"].ToString();

                            row["id"] = clsCadClienteObservaBLL.Incluir(clsCadClienteObservaInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            clsCadClienteObservaInfo.id = clsParser.Int32Parse(row["ID"].ToString());
                            clsCadClienteObservaInfo.idemitente = clsParser.Int32Parse(row["idemitente"].ToString());
                            clsCadClienteObservaInfo.idcliente = clsCadClienteInfo.idprospeccao;
                            clsCadClienteObservaInfo.idvendedor = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());

                            clsCadClienteObservaInfo.contatado = row["contatado"].ToString();
                            clsCadClienteObservaInfo.contatoagenda = row["contatoagenda"].ToString();
                            clsCadClienteObservaInfo.contatofim = row["contatofim"].ToString();
                            clsCadClienteObservaInfo.data = clsParser.DateTimeParse(row["data"].ToString());
                            clsCadClienteObservaInfo.dataagenda = clsParser.DateTimeParse(row["dataagenda"].ToString());
                            clsCadClienteObservaInfo.datafim = clsParser.DateTimeParse(row["datafim"].ToString());
                            clsCadClienteObservaInfo.emitente = row["emitente"].ToString();
                            clsCadClienteObservaInfo.fechada = row["fechada"].ToString();
                            clsCadClienteObservaInfo.ligacao = row["ligacao"].ToString();
                            clsCadClienteObservaInfo.observar = row["observar"].ToString();
                            clsCadClienteObservaInfo.observaragenda = row["observaragenda"].ToString();

                            clsCadClienteObservaBLL.Alterar(clsCadClienteObservaInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            clsCadClienteObservaBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        }
                    }

                    if (dtClienteContato.Rows.Count == 0)
                    {
                        throw new Exception("Necessário cadastrar ao menos um contato");
                    }
                    clsCadClienteContatoInfo = new clsCadClienteContatoInfo();
                    foreach (DataRow row in dtClienteContato.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            clsCadClienteContatoInfo.id = 0;
                            clsCadClienteContatoInfo.idcliente = clsCadClienteInfo.idprospeccao;

                            clsCadClienteContatoInfo.celular = row["CELULAR"].ToString();
                            clsCadClienteContatoInfo.tipocontato = row["TIPOCONTATO"].ToString();
                            clsCadClienteContatoInfo.contato = row["CONTATO"].ToString();
                            clsCadClienteContatoInfo.ddd = row["DDD"].ToString();
                            clsCadClienteContatoInfo.dddcelular = row["DDDCELULAR"].ToString();
                            clsCadClienteContatoInfo.dddfax = row["DDDFAX"].ToString();
                            clsCadClienteContatoInfo.email = row["EMAIL"].ToString();
                            clsCadClienteContatoInfo.observa = row["OBSERVA"].ToString();
                            clsCadClienteContatoInfo.ramal = clsParser.Int32Parse(row["RAMAL"].ToString());
                            clsCadClienteContatoInfo.ramalfax = clsParser.Int32Parse(row["RAMALFAX"].ToString());
                            clsCadClienteContatoInfo.setor = row["SETOR"].ToString();
                            clsCadClienteContatoInfo.telefax = row["TELEFAX"].ToString();
                            clsCadClienteContatoInfo.telefone = row["TELEFONE"].ToString();

                            row["id"] = clsCadClienteContatoBLL.Incluir(clsCadClienteContatoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            clsCadClienteContatoInfo.id = clsParser.Int32Parse(row["id"].ToString());
                            clsCadClienteContatoInfo.idcliente = clsParser.Int32Parse(row["idcliente"].ToString());

                            clsCadClienteContatoInfo.celular = row["CELULAR"].ToString();
                            clsCadClienteContatoInfo.tipocontato = row["TIPOCONTATO"].ToString();
                            clsCadClienteContatoInfo.contato = row["CONTATO"].ToString();
                            clsCadClienteContatoInfo.ddd = row["DDD"].ToString();
                            clsCadClienteContatoInfo.dddcelular = row["DDDCELULAR"].ToString();
                            clsCadClienteContatoInfo.dddfax = row["DDDFAX"].ToString();
                            clsCadClienteContatoInfo.email = row["EMAIL"].ToString();
                            clsCadClienteContatoInfo.observa = row["OBSERVA"].ToString();
                            clsCadClienteContatoInfo.ramal = clsParser.Int32Parse(row["RAMAL"].ToString());
                            clsCadClienteContatoInfo.ramalfax = clsParser.Int32Parse(row["RAMALFAX"].ToString());
                            clsCadClienteContatoInfo.setor = row["SETOR"].ToString();
                            clsCadClienteContatoInfo.telefax = row["TELEFAX"].ToString();
                            clsCadClienteContatoInfo.telefone = row["TELEFONE"].ToString();

                            clsCadClienteContatoBLL.Alterar(clsCadClienteContatoInfo, clsInfo.conexaosqldados);
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            clsCadClienteContatoBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        }
                    }

                    id = clsCadClienteInfo.id;
                    frmCadClienteVis.id = id;
                    idprospeccao = clsCadClienteInfo.idprospeccao;

                    tse.Complete();
                    //passa para o form de clsVisualização o id
                    frmCadClienteVis.id = id;
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            else if (ctl.Name == tbxZona.Name)
            {

                //idzona = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "ZONAS", "ID", "CODIGO", tbxZona.Text));
                idzona = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from zonas where codigo = '" + tbxZona.Text + "'", "0"));

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
                    tbxComissaorepresentante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idvendedor, "");
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
                    tbxCoordenadorComissao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idcoordenador, "");
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
                    tbxSupervisorComissao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idsupervisor, "");
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
                //if (tbxEndereco.Text == "")
                //{
                //    String[] CEP =clsVisual.BuscaCep(tbxCep.Text);

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
                idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + cbxUf.Text + "' ", "0"));
                if (idestado == 0 && ((TextBox)ctl).Text.Trim().Length > 0)
                {
                    ((ComboBox)ctl).SelectedIndex = 0;
                }
            }

            //if (ctl.Name == cbxCidade.Name)
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

            tbxCognome.Text =clsVisual.RemoveAcentos(tbxCognome.Text);
            tbxNome.Text =clsVisual.RemoveAcentos(tbxNome.Text);
            //            tbxCidade.Text =clsVisual.RemoveAcentos(tbxCidade.Text);

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
                label18.Visible = false;
                tbxImunicipal.Visible = false;
                label46.Visible = false;
                tbxSuframa.Visible = false;
                label45.Visible = false;
                tbxCnae_codigo.Visible = false;
                btnCnae.Visible = false;
                label20.Visible = false;
                tbxHomepage.Visible = false;
                label19.Visible = false;
                tbxFtp.Visible = false;
                gbxContatos.Text = "Contatos / Familiares:";
                label25.Text = "Profissão:";
            }
            else
            {  // JURIDICA
                label35.Text = "Data Fundação:";
                label15.Text = "CNPJ:";
                label17.Text = "IE:";
                label18.Visible = true;
                tbxImunicipal.Visible = true;
                label46.Visible = true;
                tbxSuframa.Visible = true;
                label45.Visible = true;
                tbxCnae_codigo.Visible = true;
                btnCnae.Visible = true;
                label20.Visible = true;
                tbxHomepage.Visible = true;
                label19.Visible = true;
                tbxFtp.Visible = true;
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
                gbxConfiguracao.Visible = false;
            }
            else
            {
                gbxConfiguracao.Visible = true;
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
        public Boolean ClienteMovimenta()
        {
            clsCadClienteInfo = new clsCadClienteInfo();
            ClienteInfo(clsCadClienteInfo);

            if (clsCadClienteBLL.Equals(clsCadClienteInfoOld, clsCadClienteInfo) == false)
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
            frmCadClienteVis frmCadClienteVis = new frmCadClienteVis();
            frmCadClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmCadClienteVis, clsInfo.conexaosqldados);
        }

        private void btnRedespacho_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnRedespacho.Name;
            frmCadClienteVis frmCadClienteVis = new frmCadClienteVis();
            frmCadClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmCadClienteVis, clsInfo.conexaosqldados);
        }

        private void btnIdBanco_Click(object sender, EventArgs e)
        {
            //clsInfo.znomegrid = btnIdBanco.Name;
            //frmTab_BancosPes frmTab_BancosPes = new frmTab_BancosPes();
            //frmTab_BancosPes.Init(clsInfo.conexaosqldados, idbanco);
            //FormHelper.AbrirForm(this.MdiParent, frmTab_BancosPes, clsInfo.conexaosqldados);
        }

        private void btnIdZona_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdZona.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "ZONAS", idzona, "Zona/Região");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdCondPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCondPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONDPAGTO", idcondpagto, "Condição de Pagamento");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idformapagto, "Forma de Pagto");

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
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "REGIMEAPURACAO", idregimeapuracao, "Regimes de Apuração");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnIddizeresnota_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIddizeresnota.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "DIZERESNFV", iddizeresnota, "Dizeres da Nota Fiscal");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
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
        void bwrClienteEndereco_DoWork(object sender, DoWorkEventArgs e)
        {
            String query;
            SqlDataAdapter sda;

            query = "select * " +
                "from " +
                        "clienteendereco " +
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

        void bwrClienteContato_DoWork(object sender, DoWorkEventArgs e)
        {
            if (clsParser.Int32Parse(id.ToString()) > 0)
            {
                idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente=" + id.ToString(), "0"));
            }
            else
            {
                idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente = -1", "0"));
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
                        "dddi, " +
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
                        "clienteprospeccaocontato " +
                "where " +
                        "idcliente = @idcliente";

            dtClienteContato = new DataTable();
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idprospeccao;
            sda.Fill(dtClienteContato);
        }

        void bwrClienteContato_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataColumn dc = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtClienteContato.Columns.Add(dc);
            for (Int32 i = 1; i <= dtClienteContato.Rows.Count; i++)
            {
                dtClienteContato.Rows[i - 1]["posicao"] = i;
            }
            dtClienteContato.AcceptChanges();

            dgvClienteContato.DataSource = dtClienteContato;
            clsGridHelper.MontaGrid2(dgvClienteContato, dtClienteContatoColunas, true);

            VerificaContato();
        }

        void bwrClienteContato_Run()
        {
            bwrClienteContato = new BackgroundWorker();
            bwrClienteContato.DoWork += new DoWorkEventHandler(bwrClienteContato_DoWork);
            bwrClienteContato.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrClienteContato_RunWorkerCompleted);
            bwrClienteContato.RunWorkerAsync();
        }

        void VerificaContato()
        {
            if (dtClienteContato == null)
            {
                return;
            }

            tbxDdd.Text = "";
            tbxTelefone.Text = "";
            tbxContato.Text = "";

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
                idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente=" + id.ToString(), "0"));
            }
            else
            {
                idprospeccao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteprospeccao where idcliente = -1", "0"));
            }

            dtClienteObserva = clsCadClienteObservaBLL.GridDados(idprospeccao, clsInfo.conexaosqldados);
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
        private void CapturarCidadeUF()
        {/*
            if (cbxCidade.Text.Length > 1)
            {
                cbxCidade.Text = cbxCidade.Text.ToUpper().Trim();
                Int32 x = cbxCidade.Text.IndexOf('-');
                Int32 y = cbxCidade.Text.Length;
                String Cidade = cbxCidade.Text.Substring(0, x).Trim();
                String Estado = cbxCidade.Text.Substring(x + 1, (y - (x + 1))).Trim();
                idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + Estado + "' ", "0"));
                idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + Cidade + "' and idestado=" + idestado + " ", "0"));
                cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString(), "SP"));
                // codigo do ibge
                tbxIbge.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ibge from estados where id=" + idestado + " ", "??");
                tbxIbge.Text += Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from cidades where id=" + idcidade + " and idestado=" + idestado + " ", "00000");
            }
            */
        }

        private void btnCep_Click(object sender, EventArgs e)
        {

        }
    }
}
