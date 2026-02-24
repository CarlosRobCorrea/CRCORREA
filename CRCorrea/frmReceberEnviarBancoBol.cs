using CRCorreaBarCod;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using BoletoNet;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace CRCorrea
{
    public enum TipoBanco
    {
        Brasil = 1,
        Bradesco = 237,
        CaixaEconomica = 998,
        HSBC = 997,
        Itau = 341,
        Santander = 33
    }

    public partial class frmReceberEnviarBancoBol : Form
    {
        int idBancos;
        int idBancosDestino;
        int idTab_Banco;
        int idTab_BancoDestino;

        clsBancosBLL clsBancosBLL;
        clsClienteBLL clsClienteBLL;
        clsClienteEnderecoBLL clsClienteenderecoBLL;
        clsEmpresaBLL clsEmpresaBLL;
        clsEmpresaGereBLL clsEmpresaGereBLL;
        clsReceberBLL clsReceberBLL;

        // Receber com boleto
        bool carregandoReceberBoleto = false;
        DataTable dtReceberBoleto;
        BackgroundWorker bwrReceberBoleto;

        GridColuna[] dtReceberBoletoColunas = new GridColuna[]
        {
            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("F.", "Filial", 20, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("PF", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissao", "EMISSAO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("VenctoPrev", "VENCIMENTOPREV", 65, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Vl. Título", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vl. Líq", "VALORLIQUIDO", 80, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vendedor", "VENDEDOR", 70, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("F. Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Bco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Juros Dia", "VALORJUROS", 65, true, DataGridViewContentAlignment.MiddleRight)
        };

        // Receber sem boleto
        bool carregandoReceberSemBoleto = false;
        DataTable dtReceberSemBoleto;
        BackgroundWorker bwrReceberSemBoleto;

        GridColuna[] dtReceberSemBoletoColunas = new GridColuna[]
        {
            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("F.", "Filial", 20, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Duplicata", "DUPLICATA", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("PI", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("PF", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "EMISSAO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Vencimento", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("VenctoPrev", "VENCIMENTOPREV", 65, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Vl. Título", "VALOR", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vl. Líq", "VALORLIQUIDO", 80, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vendedor", "VENDEDOR", 70, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("F. Pagto", "FORMAPAGTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Boleto", "BOLETONRO", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("D", "DV", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Banco", "BANCO", 60, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Juros Dia", "VALORJUROS", 65, true, DataGridViewContentAlignment.MiddleRight)
        };

        // Receber enviar
        bool carregandoReceberEnviar = false;
        DataTable dtReceberEnviar;
        BackgroundWorker bwrReceberEnviar;

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

        public frmReceberEnviarBancoBol()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBancosBLL = new clsBancosBLL();
            clsClienteBLL = new clsClienteBLL();
            clsClienteenderecoBLL = new clsClienteEnderecoBLL();
            clsEmpresaBLL = new clsEmpresaBLL();
            clsEmpresaGereBLL = new clsEmpresaGereBLL();
            clsReceberBLL = new clsReceberBLL();

            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBancosCodigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBancosCodigoDestino);
            
            CarregarDadosEmpresa();
            CarregarGrids();
        }

        void CarregarGrids()
        {
            bwrReceberBoleto_Run();
            bwrReceberSemBoleto_Run();
            bwrReceberEnviar_Run();
        }

        private void frmReceberEnviarBancoBol_Load(object sender, System.EventArgs e)
        {

        }

        private void frmReceberEnviarBancoBol_Activated(object sender, EventArgs e)
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
                if (clsInfo.znomegrid == btnBancos.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancos = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancosCodigo.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancosNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();

                        idTab_Banco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancos));
                        tbxTab_BancoCodigo.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idTab_Banco)).ToString("N0");
                        tbxTab_BancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idTab_Banco);
                    }

                    tbxBancosCodigo.Select();
                    tbxBancosCodigo.SelectAll();

                    bwrReceberBoleto_Run();
                }
                else if (clsInfo.znomegrid == btnBancosDestino.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancosDestino = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancosCodigoDestino.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancosNomeDestino.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();

                        idTab_BancoDestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancosDestino));
                        tbxTab_BancoCodigoDestino.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idTab_BancoDestino)).ToString("N0");
                        tbxTab_BancoNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idTab_BancoDestino);
                    }

                    tbxBancosCodigoDestino.Select();
                    tbxBancosCodigoDestino.SelectAll();

                    bwrReceberSemBoleto_Run();
                }
            }
            else if (ctl.Name == tbxBancosCodigo.Name && !string.IsNullOrEmpty(tbxBancosCodigo.Text))
            {
                idBancos = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + tbxBancosCodigo.Text));
                tbxBancosNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + idBancos);

                idTab_Banco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancos));
                tbxTab_BancoCodigo.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idTab_Banco)).ToString("N0");
                tbxTab_BancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idTab_Banco);

                bwrReceberBoleto_Run();
            }
            else if (ctl.Name == tbxBancosCodigoDestino.Name && !string.IsNullOrEmpty(tbxBancosCodigoDestino.Text))
            {
                idBancosDestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + tbxBancosCodigoDestino.Text));
                tbxBancosNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID = " + idBancosDestino);

                idTab_BancoDestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancosDestino));
                tbxTab_BancoCodigoDestino.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idTab_BancoDestino)).ToString("N0");
                tbxTab_BancoNomeDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idTab_BancoDestino);

                bwrReceberSemBoleto_Run();
            }

            tbxProtestar.Text = clsParser.Int32Parse(tbxProtestar.Text).ToString();
            clsInfo.zrow = null;
            clsInfo.znomegrid = string.Empty;
        }

        private void bwrReceberBoleto_Run()
        {
            if (carregandoReceberBoleto == false)
            {
                carregandoReceberBoleto = true;

                bwrReceberBoleto = new BackgroundWorker();
                bwrReceberBoleto.DoWork += new DoWorkEventHandler(bwrReceberBoleto_DoWork);
                bwrReceberBoleto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberBoleto_RunWorkerCompleted);
                bwrReceberBoleto.RunWorkerAsync();
            }
        }


        private void bwrReceberBoleto_DoWork(object sender, DoWorkEventArgs e)
        {
            dtReceberBoleto = new DataTable();

            var query = "SELECT " +
                "RECEBER.ID, " +
                "RECEBER.FILIAL, " +
                "RECEBER.DUPLICATA, " +
                "RECEBER.POSICAO, " +
                "RECEBER.POSICAOFIM, " +
                "RECEBER.EMISSAO, " +
                "CLIENTE.COGNOME, " +
                "RECEBER.VENCIMENTO, " +
                "RECEBER.VENCIMENTOPREV, " +
                "RECEBER.VALOR, " +
                "RECEBER.VALORLIQUIDO, " +
                "VENDEDOR.COGNOME AS [VENDEDOR], " +
                "SITUACAOTIPOTITULO.CODIGO + '-' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], " +
                "RECEBER.BOLETONRO, " +
                "RECEBER.DV, " +
                "TAB_BANCOS.COGNOME AS [BANCO], " +
                "RECEBER.VALORJUROS " +
            "FROM RECEBER " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID=RECEBER.IDCLIENTE " +
                "LEFT JOIN CLIENTE VENDEDOR ON VENDEDOR.ID=RECEBER.IDVENDEDOR " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=RECEBER.IDFORMAPAGTO " +
                "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID=RECEBER.IDBANCO " +
                "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=RECEBER.IDSITBANCO ";

            query += " WHERE RECEBER.FILIAL = " + clsInfo.zfilial + " ";

            // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
            query += " AND RECEBER.VALORPAGO = 0 ";

            // POIS NÃO FAZ BAIXA PAGAMENTO PARCIAL NESTE FORM
            // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO
            query += " AND RECEBER.VALORBAIXANDO = 0 ";

            // APENAS COM BOLETO DE NUMERO 0 (ZERO)
            query += " AND RECEBER.BOLETONRO > 0 ";

            // Numero de Nota
            // QUANDO INDICA O BANCO ONDE VAI PROCURAR AS DUPLICATAS QUE AINDA NÃO FORAM ENVIADAS PARA O BANCO  
            // SELECIONANDO O BANCO INDICADO DESTINO
            query += " AND RECEBER.IDBANCOINT = " + idBancos;
            query += " ORDER BY BOLETONRO DESC ";

            var sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtReceberBoleto);
        }

        private void bwrReceberBoleto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvBoletoEnviado.DataSource = dtReceberBoleto;
                clsGridHelper.MontaGrid2(dgvBoletoEnviado, dtReceberBoletoColunas, true);

                dgvBoletoEnviado.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvBoletoEnviado.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvBoletoEnviado.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvBoletoEnviado.Columns["VALORJUROS"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvBoletoEnviado, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceberBoleto = false;
            }
        }

        private void bwrReceberSemBoleto_Run()
        {
            if (carregandoReceberSemBoleto == false)
            {
                carregandoReceberSemBoleto = true;

                bwrReceberSemBoleto = new BackgroundWorker();
                bwrReceberSemBoleto.DoWork += new DoWorkEventHandler(bwrReceberSemBoleto_DoWork);
                bwrReceberSemBoleto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberSemBoleto_RunWorkerCompleted);
                bwrReceberSemBoleto.RunWorkerAsync();
            }
        }


        private void bwrReceberSemBoleto_DoWork(object sender, DoWorkEventArgs e)
        {
            dtReceberSemBoleto = new DataTable();

            var query = "SELECT " +
                "RECEBER.ID, " +
                "RECEBER.FILIAL, " +
                "RECEBER.DUPLICATA, " +
                "RECEBER.POSICAO, " +
                "RECEBER.POSICAOFIM, " +
                "RECEBER.EMISSAO, " +
                "CLIENTE.COGNOME, " +
                "RECEBER.VENCIMENTO, " +
                "RECEBER.VENCIMENTOPREV, " +
                "RECEBER.VALOR, " +
                "RECEBER.VALORLIQUIDO, " +
                "VENDEDOR.COGNOME AS [VENDEDOR], " +
                "SITUACAOTIPOTITULO.CODIGO + '-' + SITUACAOTIPOTITULO.NOME AS [FORMAPAGTO], " +
                "RECEBER.BOLETONRO, " +
                "RECEBER.DV, " +
                "TAB_BANCOS.COGNOME AS [BANCO], " +
                "RECEBER.VALORJUROS " +
            "FROM RECEBER " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID=RECEBER.IDCLIENTE " +
                "LEFT JOIN CLIENTE VENDEDOR ON VENDEDOR.ID=RECEBER.IDVENDEDOR " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=RECEBER.IDFORMAPAGTO " +
                "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID=RECEBER.IDBANCO " +
                "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID=RECEBER.IDSITBANCO ";

            query += " WHERE  " +
                "RECEBER.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";

            // NÃO PODE APARECER SE FOI DESCONTADO
            query += " AND SITUACAOTITULO.CODIGO < 4 ";

            // VALOR PAGO TEM QUE SE IGUAL A 0 (ZERO) 
            query += " AND RECEBER.VALORPAGO = 0 ";

            // POIS NÃO FAZ BAIXA PAGAMENTO PARCIAL NESTE FORM
            query += " AND RECEBER.VALORBAIXANDO = 0 "; // NÃO PODE ESTAR SENDO BAIXADO POR OUTRO MODULO

            // APENAS COM BOLETO DE NUMERO 0 (ZERO)
            query += " AND RECEBER.BOLETONRO = 0 ";

            // Numero de Nota
            if (clsParser.Int32Parse(tbxNroNota.Text) > 0)
            {
                query += " AND RECEBER.DUPLICATA >= " + clsParser.Int32Parse(tbxNroNota.Text) + " ";
            }

            // QUANDO INDICA O BANCO ONDE VAI PROCURAR AS DUPLICATAS QUE AINDA NÃO FORAM ENVIADAS PARA O BANCO  
            if (chxBancosTodos.Checked == true)
            {
                // Todos que não possuem o banco indicado  [ SITUACAOTIPOTITULO.CODIGO  ]
                query += " AND SITUACAOTIPOTITULO.CODIGO = 'BO' ";  // SELECIONANDO TUDO O QUE FOR BO
            }
            else
            {
                query += " AND RECEBER.IDBANCO = " + idTab_Banco;  // SELECIONANDO O BANCO INDICADO
            }

            query += " ORDER BY RECEBER.VENCIMENTO, CLIENTE.COGNOME ";

            var sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtReceberSemBoleto);
        }

        private void bwrReceberSemBoleto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvBoletoNaoEnviado.DataSource = dtReceberSemBoleto;
                clsGridHelper.MontaGrid2(dgvBoletoNaoEnviado, dtReceberSemBoletoColunas, true);

                dgvBoletoNaoEnviado.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvBoletoNaoEnviado.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvBoletoNaoEnviado.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvBoletoNaoEnviado.Columns["VALORJUROS"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvBoletoNaoEnviado, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceberSemBoleto = false;
            }
        }

        private void CarregarDadosEmpresa()
        {
            tbxJurosMes.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select JUROSMES FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid)).ToString("N2");
            tbxProtestar.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PROTESTAR FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid)).ToString("N0");
            tbxBoletolinha01.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select BOLETOLINHA01 FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            // tbxSequencia.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select SEQUENCIA FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
            // tbxBoletoProximo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select REGISTRO FROM EMPRESAGERE where EMPRESA= " + clsInfo.zempresaid);
        }

        private void btnBancos_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBancos.Name;

            var frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancos, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private void btnBancosDestino_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBancosDestino.Name;

            var frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancosDestino, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvBoletoNaoEnviado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBoletoNaoEnviado.CurrentRow != null)
            {
                if (clsParser.DateTimeParse(dgvBoletoNaoEnviado.CurrentRow.Cells["VENCIMENTO"].Value.ToString()) < clsParser.DateTimeParse(dgvBoletoNaoEnviado.CurrentRow.Cells["EMISSAO"].Value.ToString()))
                {
                    MessageBox.Show("Data de vencimento menor que a de emissão favor verificar!", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                IncluirBoleto((int)dgvBoletoNaoEnviado.CurrentRow.Cells[0].Value);
            }
        }

        private void IncluirBoleto(int idReceber)
        {
            var clsReceberInfo = clsReceberBLL.Carregar(idReceber, clsInfo.conexaosqldados);

            if (clsReceberInfo.valorjuros == 0)
            {
                if (clsParser.DecimalParse(tbxJurosMes.Text) > 0)
                {
                    clsReceberInfo.valorjuros = Math.Round(((clsReceberInfo.valor * (clsParser.DecimalParse(tbxJurosMes.Text) / 100)) / 30), 2);
                }
                else
                {
                    MessageBox.Show("Obrigatório possuir a taxa de juros mês. Necessário configurar.");
                    return;
                }
            }

            var clsClienteInfo = clsClienteBLL.Carregar(clsReceberInfo.idcliente, clsInfo.conexaosqldados);

            var dtClienteEndereco = new DataTable();

            var query = "select ID from CLIENTEENDERECO WHERE  CLIENTEENDERECO.IDCLIENTE = " + clsClienteInfo.id + " AND LEFT(TIPOENDNOME,1)= '1'";
            var sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

            sda.Fill(dtClienteEndereco);

            int idEnderecoCobranca = 0;

            if (dtClienteEndereco.Rows.Count == 0)
            {
                MessageBox.Show("Atenção o cadastro deste cliente não possui endereço de cobrança." + Environment.NewLine + "Atualize o Cadastro para prosseguir = [" + clsClienteInfo.cognome + "]");
                return;
            }
            else
            {
                idEnderecoCobranca = clsParser.Int32Parse(dtClienteEndereco.Rows[0]["ID"].ToString());
            }

            var clsClienteEnderecoInfo = clsClienteenderecoBLL.Carregar(idEnderecoCobranca, clsInfo.conexaosqldados);

            if (clsClienteEnderecoInfo.endereco.ToString().Length <= 0 ||
                clsClienteEnderecoInfo.bairro.ToString().Length <= 0 ||
                clsClienteEnderecoInfo.cep.ToString().Length <= 0 ||
                clsClienteEnderecoInfo.cidade.ToString().Length <= 0)
            {
                MessageBox.Show("Atenção o cadastro deste cliente não possui endereço de cobrança correto." + Environment.NewLine + "Atualize o Cadastro para prosseguir = [" + clsClienteInfo.cognome + "]");
                return;
            }

            // Se for delga coloca desconto
            decimal vDesconto = 0;
            decimal vValorEspecial = 0;

            if (clsClienteInfo.cognome.ToUpper().IndexOf("DELGA") != -1 ||
                clsClienteInfo.cognome.ToUpper().IndexOf("MAQUI") != -1)
            {
                vDesconto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PISPASEPRETIDO from NFVENDA where ID= " + clsReceberInfo.idnotafiscal));
                vDesconto += clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COFINS1RETIDO from NFVENDA where ID= " + clsReceberInfo.idnotafiscal));
                vValorEspecial = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select sum(totalnota) from nfvenda1 where numero=" + clsReceberInfo.idnotafiscal + " and FATURA = 'S'"));
            }

            // Se for para juntar por cnpj verificar se o cliente já foi cadastrado e acumular o valor
            if (rbnEnviarLote.Checked)
            {
                bool encontrouRegistro = false;

                foreach (DataRow row in dtReceberEnviar.Rows)
                {
                    if (clsParser.Int32Parse(row["IDCLIENTE"].ToString()) == clsReceberInfo.idcliente)
                    {
                        if (clsParser.DateTimeParse(row["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy") == clsParser.DateTimeParse(clsReceberInfo.vencimento.ToString()).ToString("dd/MM/yyyy"))
                        {
                            if (vValorEspecial > 0)
                            {
                                row["VALOR"] = clsParser.DecimalParse(row["VALOR"].ToString()) + vValorEspecial; ;
                                row["VALORLIQUIDO"] = clsParser.DecimalParse(row["VALORLIQUIDO"].ToString()) + vValorEspecial;
                                row["VALORJUROS"] = clsParser.DecimalParse(row["VALORJUROS"].ToString()) + clsReceberInfo.valorjuros;
                            }
                            else
                            {
                                row["VALOR"] = clsParser.DecimalParse(row["VALOR"].ToString()) + clsReceberInfo.valor;
                                row["VALORLIQUIDO"] = clsParser.DecimalParse(row["VALORLIQUIDO"].ToString()) + clsReceberInfo.valorliquido;
                                row["VALORJUROS"] = clsParser.DecimalParse(row["VALORJUROS"].ToString()) + clsReceberInfo.valorjuros;
                            }

                            encontrouRegistro = true;

                            break;
                        }
                    }
                }

                if (encontrouRegistro)
                {
                    return;
                }
            }

            var posicao_receberenviar = 0;

            foreach (DataRow row1 in dtReceberEnviar.Rows)
            {
                if (clsParser.Int32Parse(row1["POSICAO_RECEBERENVIAR"].ToString()) > posicao_receberenviar)
                {
                    posicao_receberenviar = clsParser.Int32Parse(row1["POSICAO_RECEBERENVIAR"].ToString());
                }
            }

            posicao_receberenviar += 1;

            DataRow rowEnviar = dtReceberEnviar.NewRow();

            rowEnviar["POSICAO_RECEBERENVIAR"] = posicao_receberenviar;
            rowEnviar["IDRECEBER"] = clsReceberInfo.id;
            rowEnviar["IDCLIENTE"] = clsReceberInfo.idcliente;
            rowEnviar["FILIAL"] = clsReceberInfo.filial;
            rowEnviar["DUPLICATA"] = clsReceberInfo.duplicata;
            rowEnviar["POSICAO"] = clsReceberInfo.posicao;
            rowEnviar["POSICAOFIM"] = clsReceberInfo.posicaofim;
            rowEnviar["EMISSAO"] = clsReceberInfo.emissao;
            rowEnviar["COGNOME"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id= " + clsReceberInfo.idcliente, "");
            rowEnviar["VENCIMENTO"] = clsReceberInfo.vencimento;

            if (vValorEspecial > 0)
            {
                rowEnviar["VALOR"] = vValorEspecial;
                rowEnviar["VALORLIQUIDO"] = vValorEspecial;
                rowEnviar["VALORDESCONTO"] = vDesconto;
            }
            else
            {
                rowEnviar["VALOR"] = clsReceberInfo.valor;
                rowEnviar["VALORLIQUIDO"] = clsReceberInfo.valorliquido;
                rowEnviar["VALORDESCONTO"] = 0;
            }

            rowEnviar["VENDEDOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id= " + clsReceberInfo.idvendedor);  //dgvReceberAberto.CurrentRow.Cells["VENDEDOR"].Value.ToString();
            rowEnviar["FORMAPAGTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOTIPOTITULO where id= " + clsReceberInfo.idformapagto);  //dgvReceberAberto.CurrentRow.Cells["VENDEDOR"].Value.ToString();
            rowEnviar["BOLETONRO"] = "0";
            rowEnviar["DV"] = 0;
            rowEnviar["BANCO"] = tbxBancosNome.Text;
            rowEnviar["VALORJUROS"] = clsReceberInfo.valorjuros;

            rowEnviar["NOMECLIENTE"] = clsClienteEnderecoInfo.cobnome.Trim().PadRight(39).Substring(0, 39);
            rowEnviar["ENDERECO"] = clsClienteEnderecoInfo.endereco.Trim().PadRight(40).Substring(0, 40);
            rowEnviar["BAIRRO"] = clsClienteEnderecoInfo.bairro.Trim().PadRight(15).Substring(0, 15);
            rowEnviar["CIDADE"] = clsClienteEnderecoInfo.cidade.Trim().PadRight(15).Substring(0, 15);
            rowEnviar["ESTADO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ESTADO from ESTADOS where id= " + clsClienteEnderecoInfo.idestado);
            rowEnviar["CEP"] = clsClienteEnderecoInfo.cep.Replace("-", "");
            rowEnviar["CGC"] = clsClienteInfo.cgc.Trim().PadRight(18).Substring(0, 18);
            rowEnviar["IE"] = clsClienteInfo.ie.Trim().PadRight(16).Substring(0, 16);
            rowEnviar["PESSOA"] = clsClienteInfo.pessoa;
            rowEnviar["DIASPROTESTO"] = tbxProtestar.Text;
            rowEnviar["LINHA01"] = tbxBoletolinha01.Text;

            if (vDesconto > 0)
            {
                // "Desconto de " + vDesconto.ToString("N2");
            }

            if (rbnEnviarIndividual.Checked)
            {
                // tbxBoletolinha03.Text = clsReceberInfo.duplicata.ToString();
                // tbxBoletolinha04.Text = clsReceberInfo.id.ToString();
            }
            else
            {
                // tbxBoletolinha03.Text = clsReceberInfo.duplicata.ToString();
                // tbxBoletolinha04.Text = clsReceberInfo.id.ToString() + "|";
            }

            rowEnviar["REFERENCIA"] = "";
            rowEnviar["REFERENCIA1"] = "";
            rowEnviar["REFERENCIA2"] = "";

            dtReceberEnviar.Rows.Add(rowEnviar);

            // Gravar na Duplicata para que ela saia da lista de Pendencias
            // Vai Gravar numero de Boleto = 1 (Nunca deve existir boleto nro 1)
            var scn = new SqlConnection(clsInfo.conexaosqldados);
            var scd = new SqlCommand("UPDATE RECEBER SET BOLETONRO=@BOLETONRO WHERE ID=@ID ", scn);
            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsReceberInfo.id;
            scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = 1;

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

            bwrReceberBoleto_Run();
            bwrReceberSemBoleto_Run();
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
            dtReceberEnviar.Columns.Add("IDDUPLICATALINHA05", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("REFERENCIA", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("REFERENCIA1", Type.GetType("System.String"));
            dtReceberEnviar.Columns.Add("REFERENCIA2", Type.GetType("System.String"));

            // Gravar na Duplicata 0 (zero) no Boleto as que tiverem Numero 1
            var scn = new SqlConnection(clsInfo.conexaosqldados);
            var scd = new SqlCommand("UPDATE RECEBER SET BOLETONRO=@BOLETONRO WHERE BOLETONRO=1 ", scn);
            scd.Parameters.AddWithValue("@BOLETONRO", SqlDbType.Decimal).Value = 0;

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
        }


        private void bwrReceberEnviar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvBoletoEnviar.DataSource = dtReceberEnviar;
                clsGridHelper.MontaGrid2(dgvBoletoEnviar, dtReceberEnviarColunas, true);
                dgvBoletoEnviar.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvBoletoEnviar.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvBoletoEnviar.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvBoletoEnviar.Columns["VALORJUROS"].DefaultCellStyle.Format = "N2";
                clsGridHelper.FontGrid(dgvBoletoEnviar, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoReceberEnviar = false;
            }
        }

        private void tspGerarArquivo_Click(object sender, EventArgs e)
        {
            if (dtReceberEnviar.Rows.Count == 0) return;

            try
            {
                if (tbxTab_BancoCodigoDestino.Text.Trim() == "1")
                {
                    GeraDadosBanco(TipoBanco.Brasil);
                }
                else if (tbxTab_BancoCodigoDestino.Text.Trim() == "33")
                {
                    GeraDadosBanco(TipoBanco.Santander);
                }
                else if (tbxTab_BancoCodigoDestino.Text.Trim() == "341")
                {
                    GeraDadosBanco(TipoBanco.Itau);
                }
                else
                {
                    MessageBox.Show("Emissão de boleto para o banco " + tbxTab_BancoNomeDestino.Text + " não atendido pelo sistema.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    mensagemErro += ": " + ex.InnerException.Message;
                }

                MessageBox.Show(mensagemErro, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public string GeraArquivoCNAB400(IBanco banco, Cedente cedente, Boletos boletos)
        {
            saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var arquivo = new ArquivoRemessa(TipoArquivo.CNAB400);

                //Valida a Remessa Correspondentes antes de Gerar a mesma...
                string vMsgRetorno = string.Empty;

                bool vValouOK = arquivo.ValidarArquivoRemessa(cedente.Convenio.ToString(), banco, cedente, boletos, 1, out vMsgRetorno);

                if (!vValouOK)
                {
                    MessageBox.Show(String.Concat("Foram localizados inconsistências na validação da remessa!", Environment.NewLine, vMsgRetorno), Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    arquivo.GerarArquivoRemessa(cedente.Convenio.ToString(), banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

                    MessageBox.Show("Arquivo gerado com sucesso!", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return saveFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public string GeraArquivoCNAB240(IBanco banco, Cedente cedente, Boletos boletos)
        {
            saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB240);
                arquivo.GerarArquivoRemessa(cedente.Convenio.ToString(), banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

                MessageBox.Show("Arquivo gerado com sucesso!", "Teste",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                return saveFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public void GeraDadosBanco(TipoBanco tipoBanco)
        {
            var clsEmpresaGereInfo = clsEmpresaGereBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            var clsEmpresaInfo = clsEmpresaBLL.Carregar(clsInfo.zempresaid, clsInfo.conexaosqldados);
            var cnpj = clsEmpresaInfo.cgc.Replace(".", "").Replace("-", "").Replace("/", "");
            var endereco = clsVisual.RemoveAcentos(clsEmpresaInfo.endereco + " " + clsEmpresaInfo.numeroend.Trim() + " " + clsEmpresaInfo.andar.Trim() + " " + clsEmpresaInfo.tipocomple.Trim() + " " + clsEmpresaInfo.comple.Trim()).Trim();
            var ccep = clsEmpresaInfo.cep.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            var clsBancosInfo = clsBancosBLL.Carregar(idBancosDestino, clsInfo.conexaosqlbanco);
            var instrucaoProtesto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where id=" + clsBancosInfo.idbcoprotestar));
            var especieDocumento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSESPECIE where id=" + clsBancosInfo.idbcoespecie);
            var numeroBoletoProximo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NROBOLETO from BANCOS where ID=" + idBancosDestino));

            var agencia = clsBancosInfo.agencia.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            var contaCorrente = clsBancosInfo.contacor.Replace(".", "").Replace("-", "").Replace("/", "").Trim();

            var contaCorrenteDigito = string.Empty;
            var agenciaDigito = string.Empty;

            switch (tipoBanco)
            {
                case TipoBanco.Bradesco:
                    if (agencia.Length < 5) throw new Exception("Nº da Agência não possui os 5 dígitos  ( " + agencia + " ).");

                    agenciaDigito = agencia.Substring(agencia.Length - 1);
                    agencia = agencia.Substring(0, 4);

                    if (contaCorrente.Length < 7) throw new Exception("Nº da Conta Corrente é inválido.");

                    contaCorrenteDigito = contaCorrente.Substring(contaCorrente.Length - 1);
                    contaCorrente = contaCorrente.Substring(0, 5);

                    break;

                case TipoBanco.Brasil:

                    if (agencia.Length < 5) throw new Exception("Nº da Agência não possui os 5 dígitos  ( " + agencia + " ).");

                    agenciaDigito = agencia.Substring(agencia.Length - 1);
                    agencia = agencia.Substring(0, 4);

                    if (contaCorrente.Length < 9) throw new Exception("Nº da Conta Corrente é inválido.");

                    contaCorrenteDigito = contaCorrente.Substring(contaCorrente.Length - 1);
                    contaCorrente = contaCorrente.Substring(0, 5);

                    break;

                case TipoBanco.CaixaEconomica:
                    
                    if (agencia.Length < 4) throw new Exception("Nº da Agência não possui os 4 dígitos  ( " + agencia + " ).");

                    agencia = agencia.Substring(0, 4);

                    if (contaCorrente.Length < 5) throw new Exception("Nº da Conta Corrente é inválido.");

                    contaCorrente = contaCorrente.Substring(0, 5);

                    break;

                case TipoBanco.HSBC:
                    
                    if (agencia.Length < 4) throw new Exception("Nº da Agência não possui os 4 dígitos  ( " + agencia + " ).");

                    agencia = agencia.Substring(0, 4);

                    if (contaCorrente.Length < 5) throw new Exception("Nº da Conta Corrente é inválido.");

                    contaCorrente = contaCorrente.Substring(0, 5);

                    break;

                case TipoBanco.Itau:

                    if (agencia.Length < 4) throw new Exception("Nº da Agência não possui os 4 dígitos  ( " + agencia + " ).");

                    agencia = agencia.Substring(0, 4);

                    if (contaCorrente.Length < 5) throw new Exception("Nº da Conta Corrente é inválido.");

                    contaCorrente = contaCorrente.Substring(0, 5);

                    break;

                case TipoBanco.Santander:

                    if (agencia.Length < 4) throw new Exception("Nº da Agência não possui os 4 dígitos  ( " + agencia + " ).");

                    agencia = agencia.Substring(0, 4);

                    if (contaCorrente.Length < 9) throw new Exception("Nº da Conta Corrente é inválido.");

                    contaCorrente = contaCorrente.Substring(0, 5);

                    break;

                default:
                    throw new Exception("Banco não suportado.");
            }

            var ccarteira = clsBancosInfo.carteira.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            var banco = new Banco((int)tipoBanco);
            var boletos = new Boletos();

            Cedente c = null;

            switch (tipoBanco)
            {
                case TipoBanco.Bradesco:
                case TipoBanco.Brasil:
                    c = new Cedente(clsEmpresaInfo.cgc, clsEmpresaInfo.nome, agencia, agenciaDigito, contaCorrente, contaCorrenteDigito);
                    break;

                case TipoBanco.CaixaEconomica:
                case TipoBanco.HSBC:
                case TipoBanco.Itau:
                case TipoBanco.Santander:
                    c = new Cedente(clsEmpresaInfo.cgc, clsEmpresaInfo.nome, agencia, contaCorrente);

                    break;
            }

            c.Codigo = contaCorrente;

            foreach (DataRow row in dtReceberEnviar.Rows)
            {
                numeroBoletoProximo++;

                var vencimento = DateTime.Parse(row["VENCIMENTO"].ToString());
                var valorBoleto = clsParser.DecimalParse(row["VALOR"].ToString());
                var numeroDocumento = row["DUPLICATA"].ToString();
                var clienteNome = row["NOMECLIENTE"].ToString().ToUpper();
                var clienteCnpj = row["CGC"].ToString().ToUpper();

                row["BOLETONRO"] = numeroBoletoProximo;

                var b = new Boleto(vencimento, valorBoleto, ccarteira, numeroBoletoProximo.ToString(), c);
                
                b.Banco = banco;
                b.NumeroDocumento = numeroDocumento;

                b.Sacado = new Sacado(clienteCnpj, clienteNome);
                b.Sacado.Endereco = new Endereco();
                b.Sacado.Endereco.End = row["ENDERECO"].ToString().ToUpper();
                b.Sacado.Endereco.Bairro = row["BAIRRO"].ToString().ToUpper();
                b.Sacado.Endereco.Cidade = row["CIDADE"].ToString().ToUpper();
                b.Sacado.Endereco.CEP = row["CEP"].ToString().ToUpper();
                b.Sacado.Endereco.UF = row["ESTADO"].ToString().ToUpper();

                switch (tipoBanco)
                {
                    case TipoBanco.Bradesco:
                        if (clsBancosInfo.idbcoprotestar > 0)   b.Instrucoes.Add(new Instrucao_Bradesco(instrucaoProtesto));
                        if (clsBancosInfo.idbcoespecie > 0)     b.EspecieDocumento = new EspecieDocumento_Bradesco(especieDocumento);

                        break;

                    case TipoBanco.Brasil:
                        if (clsBancosInfo.idbcoprotestar > 0)   b.Instrucoes.Add(new Instrucao_BancoBrasil(instrucaoProtesto));
                        if (clsBancosInfo.idbcoespecie > 0)     b.EspecieDocumento = new EspecieDocumento_BancoBrasil(especieDocumento);

                        break;

                    case TipoBanco.CaixaEconomica:
                        if (clsBancosInfo.idbcoprotestar > 0)   b.Instrucoes.Add(new Instrucao_Caixa(instrucaoProtesto));
                        if (clsBancosInfo.idbcoespecie > 0)     b.EspecieDocumento = new EspecieDocumento_Caixa(especieDocumento);

                        break;

                    case TipoBanco.HSBC:

                        break;

                    case TipoBanco.Itau:
                        if (clsBancosInfo.idbcoprotestar > 0)   b.Instrucoes.Add(new Instrucao_Itau(instrucaoProtesto));
                        if (clsBancosInfo.idbcoespecie > 0)     b.EspecieDocumento = new EspecieDocumento_Itau(especieDocumento);

                        break;

                    case TipoBanco.Santander:
                        if (clsBancosInfo.idbcoprotestar > 0)   b.Instrucoes.Add(new Instrucao_Santander(instrucaoProtesto));
                        if (clsBancosInfo.idbcoespecie > 0)     b.EspecieDocumento = new EspecieDocumento_Santander(especieDocumento);

                        break;
                }

                boletos.Add(b);
            }

            var boletosBancario = new List<BoletoBancario>();

            foreach (var item in boletos)
            {
                var bb = new BoletoBancario();

                bb.CodigoBanco = (short)tipoBanco;

                bb.Boleto = item;
                bb.Boleto.Valida();

                boletosBancario.Add(bb);
            }

            var arquivoGerado = string.Empty;
            var htmlGerado = string.Empty;

            switch (tipoBanco)
            {
                case TipoBanco.CaixaEconomica:
                case TipoBanco.HSBC:
                case TipoBanco.Bradesco:
                case TipoBanco.Brasil:
                case TipoBanco.Itau:
                    arquivoGerado = GeraArquivoCNAB400(banco, c, boletos);

                    break;

                case TipoBanco.Santander:
                    arquivoGerado = GeraArquivoCNAB240(banco, c, boletos);

                    break;
            }

            htmlGerado = GeraLayout(boletosBancario);

            var dadosGeradosComSucesso = MessageBox.Show("Todos os dados gerados estão corretos? Se 'Sim' confirme para que as duplicatas selecionadas sejam marcadas como geradas no sistema, assim o sistema poderá identificar os registros correspondentes no arquivo de retorno.", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dadosGeradosComSucesso == DialogResult.Yes)
            {
                MarcarRegistrosEnviados(numeroBoletoProximo, arquivoGerado, htmlGerado);
            }
        }

        private void MarcarRegistrosEnviados(int numeroProximoBoleto, string arquivoGerado, string htmlGerado)
        {
            var numeroControleBoleto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT MAX(NUMEROCONTROLEBOLETO) FROM RECEBER")) + 1;

            // Marcar no contas a receber o numero do boleto e banco enviado
            // Marcar também no contas a receber da nota fiscal
            foreach (DataRow row in dtReceberEnviar.Rows)
            {
                if (rbnEnviarIndividual.Checked)
                {
                    // Envio de Duplicatas uma a uma
                    var idReceberEnviar = clsParser.Int32Parse(row["IDRECEBER"].ToString());
                    var clsReceberInfo = clsReceberBLL.Carregar(idReceberEnviar, clsInfo.conexaosqldados);
                    clsReceberInfo.boletonro = clsParser.DecimalParse(row["BOLETONRO"].ToString());
                    clsReceberInfo.dv = row["DV"].ToString();
                    clsReceberInfo.idbanco = idBancos;
                    clsReceberInfo.idbancoint = idBancosDestino;
                    clsReceberInfo.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTIPOTITULO where CODIGO='" + "BO" + "' "));
                    clsReceberInfo.imprimir = "N";
                    clsReceberInfo.valorjuros = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                    clsReceberInfo.arquivogerado = arquivoGerado;
                    clsReceberInfo.Html = htmlGerado;
                    clsReceberInfo.numerocontroleboleto = numeroControleBoleto;
                    clsReceberBLL.Alterar(clsReceberInfo, clsInfo.conexaosqldados);

                    var cDocumento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + clsReceberInfo.iddocumento);

                    if (cDocumento == "CTO")
                    {
                        var scn = new SqlConnection(clsInfo.conexaosqldados);
                        var scd = new SqlCommand("UPDATE CONTRATORECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);

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
                        var scn = new SqlConnection(clsInfo.conexaosqldados);
                        var scd = new SqlCommand("UPDATE NFVENDARECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);

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
                {
                    // Envio de Duplicatas agrupada por cnpj
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

                            var idReceberEnviar = clsParser.Int32Parse(campo);

                            var clsReceberInfo = clsReceberBLL.Carregar(idReceberEnviar, clsInfo.conexaosqldados);

                            clsReceberInfo.boletonro = clsParser.DecimalParse(row["BOLETONRO"].ToString());
                            clsReceberInfo.dv = row["DV"].ToString();
                            clsReceberInfo.idbanco = idBancos;
                            clsReceberInfo.idbancoint = idBancosDestino;
                            clsReceberInfo.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTIPOTITULO where CODIGO='" + "BO" + "' "));
                            clsReceberInfo.imprimir = "N";
                            clsReceberInfo.valorjuros = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                            clsReceberInfo.arquivogerado = arquivoGerado;
                            clsReceberInfo.Html = htmlGerado;
                            clsReceberInfo.numerocontroleboleto = numeroControleBoleto;
                            clsReceberBLL.Alterar(clsReceberInfo, clsInfo.conexaosqldados);

                            var cDocumento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + clsReceberInfo.iddocumento + " ");

                            if (cDocumento == "CTO")
                            {
                                var scn = new SqlConnection(clsInfo.conexaosqldados);
                                var scd = new SqlCommand("UPDATE CONTRATORECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);

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
                                var scn = new SqlConnection(clsInfo.conexaosqldados);
                                var scd = new SqlCommand("UPDATE NFVENDARECEBER SET BOLETONRO=@BOLETONRO, DV=@DV, IDTIPOPAGA=@IDTIPOPAGA WHERE ID=@ID ", scn);

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
            var terminou = false;
            while (!terminou)
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

            // GRAVAR NA CONTA DO BANCO O ULTIMO NUMERO DE BOLETO ENVIADO
            var scnBanco = new SqlConnection(clsInfo.conexaosqlbanco);
            var scdBanco = new SqlCommand("UPDATE BANCOS SET NROBOLETO=@BOLETONRO WHERE ID=@ID ", scnBanco);

            scdBanco.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idBancosDestino;
            scdBanco.Parameters.AddWithValue("@BOLETONRO", SqlDbType.NVarChar).Value = numeroProximoBoleto.ToString();

            scnBanco.Open();
            scdBanco.ExecuteNonQuery();
            scnBanco.Close();

            CarregarGrids();
        }

        private string GeraLayout(List<BoletoBancario> boletos)
        {
            var html = new StringBuilder();

            foreach (var o in boletos)
            {
                html.Append(o.MontaHtml());
                html.Append("</br></br></br></br></br></br></br></br></br></br>");
            }

            var _arquivo = System.IO.Path.GetTempFileName();

            using (var f = new FileStream(_arquivo, FileMode.Create))
            {
                var w = new StreamWriter(f, System.Text.Encoding.Default);
                w.Write(html.ToString());
                w.Close();
                f.Close();
            }

            var impressaoBoleto = new ImpressaoBoleto();

            impressaoBoleto.webBrowser.Navigate(_arquivo);
            impressaoBoleto.ShowDialog();

            return html.ToString();
        }

        private void GeraLayout(string htmlTexto)
        {
            var _arquivo = System.IO.Path.GetTempFileName();

            using (var f = new FileStream(_arquivo, FileMode.Create))
            {
                var w = new StreamWriter(f, System.Text.Encoding.Default);
                w.Write(htmlTexto);
                w.Close();
                f.Close();
            }

            var impressaoBoleto = new ImpressaoBoleto();

            impressaoBoleto.webBrowser.Navigate(_arquivo);
            impressaoBoleto.ShowDialog();
        }

        private void tspVisualizar_Click(object sender, EventArgs e)
        {
            if (dgvBoletoEnviado.CurrentRow != null)
            {
                var html = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select HTML from RECEBER where ID= " + dgvBoletoEnviado.CurrentRow.Cells["ID"].Value.ToString());

                if (!string.IsNullOrEmpty(html))
                {
                    GeraLayout(html);
                }
                else
                {
                    MessageBox.Show("Esse lançamento não possui dados para re-impressão. (Foi emitido pelo sistema antigo)", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
