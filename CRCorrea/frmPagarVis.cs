using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPagarVis : Form
    {
        Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        DataTable dtPagar;

        clsPagarBLL clsPagarBLL;        

        public static DataTable dtPagarAcumula = new DataTable();
        public static DataRow drPagarAcumula;

        String dataAtual;
        String tipoData;
        String tipoLista;

        clsBasicReport clsBr;

        BackgroundWorker bwrPagar;
        BackgroundWorker bwrPagarAcumula;
        //criamos esta variavel para controlar a chamada do BackgroundWorker
        //assim sistema so chama o BackgroundWorker se ele realmente estiver completado
        Boolean carregandoPagar;
        Boolean carregandoPagarAcumula;

        String query;

        SqlConnection scn;
        SqlCommand scd;

        DialogResult resultado;
       
        public Decimal valorcalculo;

        String tbxVencimento;
        String tbxDataBaixa;
        Decimal tbxValor;
        Decimal tbxValorDesconto;
        Decimal tbxValorJuros;
        Decimal tbxValorMulta;
        String atevencimento;
        Decimal tbxDiferenca;
        String Pendencias = "S";
        public frmPagarVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBr = new clsBasicReport(this, dgvPagar, toolTip1, gbxPagar);
            carregandoPagar = false;
            carregandoPagarAcumula = false;
            clsPagarBLL = new clsPagarBLL();
        }

        private void frmPagarVis_Load(object sender, EventArgs e)
        { 
            rbnApartirdeHoje.Checked = true;
            tbxDataAtual.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dataAtual = tbxDataAtual.Text;
            // Calcular os Valores com o Juros + Multas etc..
            calcularprevisao();
            // 
            TotalizaContas();
            //
            rbnApartirdeHoje.Select();
           
        }
        private void frmPagarVis_Activated(object sender, EventArgs e)
        {
            try
            {
                TotalizaContas();
                bwrPagar_Run();
                bwrPagarAcumula_Run();                           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void frmPagarVis_Shown(object sender, EventArgs e)
        {
            //FormHelper.VerificarForm(this, toolTip1, clsInfo.conexaosqldados);
        }        
         
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            dataAtual = tbxDataAtual.Text;
            Control ctl = (Control)sender;
            if (ctl.Name == tbxDataAtual.Name)
            {
                bwrPagar_Run();
                TotalizaContas();
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void LocalizarPagar()
        {
            dgvPagar = clsGridHelper.PesquisaRapida(dgvPagar, dtPagar, tstPesquisa.Text);
            clsGridHelper.SelecionaLinha(id, dgvPagar, 1);
            tstPesquisa.Focus();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            carregandoPagar = true;
            carregandoPagarAcumula = true;
            bwrPagar.CancelAsync();
            bwrPagarAcumula.CancelAsync();
            this.Close();
            this.Dispose();
        }

        private void bwrPagar_Run()
        {
            if (carregandoPagar == false)
            {
                carregandoPagar = true;
                pbxPagar.Visible = true;
                bwrPagar = new BackgroundWorker();
                bwrPagar.DoWork += new DoWorkEventHandler(bwrPagar_DoWork);
                bwrPagar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPagar_RunWorkerCompleted);
                bwrPagar.WorkerSupportsCancellation = true;
                bwrPagar.RunWorkerAsync();
            }
        }

        private void bwrPagar_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwrPagar.CancellationPending)
            {
                //temos de setar e.Cancel com true assim o bwrAbertas_RunWorkerCompleted vai receber o cancelamento
                e.Cancel = true;
                return;
            }
            else
            {
                //dtPagar = CarregaGridPagar();
                if (rbnTodos.Checked == true)
                {
                    tipoLista = "T";
                }
                else if (rbnUmDia.Checked == true)
                {
                    // mantem a data atual que digitou na tela
                    tipoLista = "D"; // Apenas do dia marcado
                }
                else
                {
                    if (rbnApartirdeHoje.Checked == true)
                    {
                        tipoLista = "F"; // futuro
                    }
                    else
                    {
                        tipoLista = "A"; // Atraso
                    }
                }
                if (rbnDataVenc.Checked == true)
                {
                    tipoData = "VENC";
                }
                else
                {
                    tipoData = "PREV";
                }
                
                if (ckxPendencias.Checked == true)
                {
                    Pendencias = "S";
                }
                else
                {
                    Pendencias = "N";
                }
                    // a data atual saiu dos rbn
               dtPagar = clsPagarBLL.GridDados(clsInfo.zfilial, dataAtual, tipoData, tipoLista, Pendencias);
            }
        }

        private void bwrPagar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    pbxPagar.Visible = false;
                    clsPagarBLL.GridMonta(dgvPagar, dtPagar, clsInfo.zrowid);

                    clsGridHelper.FontGrid(dgvPagar, 7);
                    PesquisaRapida();
                    PintaGrid(dgvPagar);
                }
                tbxTotalVisualizacao.Text = clsParser.DecimalParse(clsGridHelper.SomaGrid(dgvPagar, "VALORPAGAR").ToString()).ToString("N2");
                carregandoPagar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoPagar = false;
            }
        }

        private void TotalizaContas()
        {
            tbxAPagarVencido.Text = "0,00";
            tbxAPagarNoMes.Text = "0,00";
            tbxAPagar90.Text = "0,00";
            tbxAPagarTudo.Text = "0,00";

            //calcula tudo 
            // Com Pendendias
            Int32 idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));

            // VENCIDO
            scn = new SqlConnection(clsInfo.conexaosqldados);
            
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND PAGAR.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            else
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND PAGAR.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }

            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAPagarVencido.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // A PAGAR NO DIA
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) + " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            else
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) + " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAPagarNoMes.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // A PAGAR NOS PROXIMOS 07 DIAS DATEADD(DD, 07, '2006-06-03 16:16:57.670')
            String dataatual07 = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual07 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            else
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual07 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAPagar07.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // A PAGAR NOS PROXIMOS 30 DIAS DATEADD(DD, 30, '2006-06-03 16:16:57.670')
            String dataatual30 = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual30 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            else
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual30 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAPagar30.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // A PAGAR NOS PROXIMOS 60 DIAS DATEADD(DD, 30, '2006-06-03 16:16:57.670')
            String dataatual60 = DateTime.Now.AddDays(60).ToString("dd/MM/yyyy");
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual60 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            else
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual60 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAPagar60.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();

            // A PAGAR NOS PROXIMOS 90 DIAS DATEADD(DD, 30, '2006-06-03 16:16:57.670')
            String dataatual90 = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
            if (rbnDataVenc.Checked == true)
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            else
            {
                query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                        " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true) +
                        " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
            }
            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAPagar90.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
            // TUDO A SER PAGO
            query = "SELECT SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) FROM PAGAR " +
                    " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + "";
            if (Pendencias == "S")
            {
                query = query + " AND IDBANCOINT != " + idBancoInt;
            }

            scd = new SqlCommand(query, scn);
            scn.Open();
            tbxAPagarTudo.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
            scn.Close();
        
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            id = 0;
            id_Proximo = 0;
            id_Anterior = 0;

            frmPagar frmPagar = new frmPagar();
            frmPagar.Init(0, dgvPagar.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmPagar, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPagar.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvPagar.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvPagar.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvPagar.Rows[dgvPagar.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvPagar.CurrentRow.Index < dgvPagar.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvPagar.Rows[dgvPagar.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
                frmPagar frmPagar = new frmPagar();
                frmPagar.Init(id, dgvPagar.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmPagar, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvPagar.Select();
            }
        }

        private void dgvPagar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        public bool CheckDate(string cDate)
        {
            try
            {
                Convert.ToDateTime(cDate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void bwrPagarAcumula_Run()
        {
            if (carregandoPagarAcumula == false)
            {
                carregandoPagarAcumula = true;
                if (rbnAcumulaVencido.Checked == true)
                {
                    valorcalculo = clsParser.DecimalParse(tbxAPagarVencido.Text) * 100;
                }
                else if (rbnAcumulaDia.Checked == true)
                {
                    valorcalculo = clsParser.DecimalParse(tbxAPagarNoMes.Text) * 100;
                }
                else if (rbnAcumula30.Checked == true)
                {
                    valorcalculo = clsParser.DecimalParse(tbxAPagar30.Text) * 100;
                }
                else if (rbnAcumula60.Checked == true)
                {
                    valorcalculo = clsParser.DecimalParse(tbxAPagar60.Text) * 100;
                }
                else if (rbnAcumula90.Checked == true)
                {
                    valorcalculo = clsParser.DecimalParse(tbxAPagar90.Text) * 100;
                }
                else
                {
                    valorcalculo = clsParser.DecimalParse(tbxAPagarTudo.Text) * 100;
                }

                pbxPagarAcumula.Visible = true;
                bwrPagarAcumula = new BackgroundWorker();
                bwrPagarAcumula.DoWork += new DoWorkEventHandler(bwrPagarAcumula_DoWork);
                bwrPagarAcumula.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPagarAcumula_RunWorkerCompleted);
                bwrPagarAcumula.WorkerSupportsCancellation = true;
                bwrPagarAcumula.RunWorkerAsync();
            }
        }

        private void bwrPagarAcumula_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwrPagarAcumula.CancellationPending)
            {
                //temos de setar e.Cancel com true assim o bwrAbertas_RunWorkerCompleted vai receber o cancelamento
                e.Cancel = true;
                return;
            }
            else
            {
              
                dtPagarAcumula = CarregaGridPagarAcumula();
            }
        }

        public DataTable CarregaGridPagarAcumula()
        {
            try
            {
                //por cliente
                query = "SELECT CLIENTE.COGNOME, " +
                        "SUM((PAGAR.VALORLIQUIDO+ pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) AS [SALDODEVEDOR], " +
                        "(((SUM((PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO)) * 100) / CONVERT(DECIMAL(12,2), '" + valorcalculo.ToString().Replace(',', '.') + "')) * 100) AS PORCENTAGEM " +
                        "from PAGAR " +
                        "LEFT JOIN CLIENTE ON CLIENTE.ID=PAGAR.IDFORNECEDOR " +
                        " WHERE PAGAR.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";

                if (rbnAcumulaVencido.Checked == true)
                {
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND PAGAR.VENCIMENTO < " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                    }
                    else
                    {
                        query = query + " AND PAGAR.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);

                    }
                }
                else if (rbnAcumulaDia.Checked == true)
                {
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 23:59", true);
                    }
                    else
                    {
                        query = query + " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 23:59", true);
                    }
                }
                else if (rbnAcumula30.Checked == true)
                {
                    String dataatual30 = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(dataatual30 + " 23:59", true);
                    }
                    else
                    {
                        query = query + " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual30 + " 23:59", true);
                    }
                }
                else if (rbnAcumula60.Checked == true)
                {
                    String dataatual60 = DateTime.Now.AddDays(60).ToString("dd/MM/yyyy");
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(dataatual60 + " 23:59", true);
                    }
                    else
                    {
                        query = query + " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual60 + " 23:59", true);
                    }
                }
                else if (rbnAcumula90.Checked == true)
                {
                    String dataatual90 = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
                    if (rbnDataVenc.Checked == true)
                    {
                        query = query + " AND PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
                    }
                    else
                    {
                        query = query + " AND PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxDataAtual.Text + " 00:00", true);
                        query = query + " AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(dataatual90 + " 23:59", true);
                    }
                }
                else
                {
                    //tudo não precisa filtrar 
                }
                Int32 idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));
                if (Pendencias == "S")
                {
                    query = query + " AND IDBANCOINT != " + idBancoInt;
                }
                query = query + " GROUP BY CLIENTE.COGNOME";
                SqlDataAdapter sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                //scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        private void bwrPagarAcumula_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    dgvPagarAcumula.DataSource = dtPagarAcumula;
                    clsGridHelper.FontGrid(dgvPagarAcumula, 8);
                    pbxPagarAcumula.Visible = false;
                    dgvPagarAcumula.DataSource = dtPagarAcumula;
                    clsGridHelper.MontaGrid(dgvPagarAcumula,
                            new String[] { "Fornecedor", "Saldo Devedor", "%" },
                            new String[] { "COGNOME", "SALDODEVEDOR", "PORCENTAGEM" },
                            new int[] { 150, 120, 65 },
                            new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight},
                            new bool[] { true, true, true },
                            true, 1, ListSortDirection.Ascending);


                    dgvPagarAcumula.Columns["SALDODEVEDOR"].DefaultCellStyle.Format = "N2";
                    dgvPagarAcumula.Columns["PORCENTAGEM"].DefaultCellStyle.Format = "N2";

                    clsGridHelper.FontGrid(dgvPagarAcumula, 8);
                    PesquisaRapida();
                }
                carregandoPagarAcumula = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                carregandoPagarAcumula = false;
            }
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Contas a Pagar", clsInfo.conexaosqldados);
        }

        private void tspMnuImprimir_Click(object sender, EventArgs e)
        {
            frmRelPagar frmRelPagar = new frmRelPagar();
            frmRelPagar.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelPagar, clsInfo.conexaosqldados);
        }

        private void rbnAcumulaVencido_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrPagarAcumula_Run();
        }

        private void rbnAcumulaDia_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrPagarAcumula_Run();
        }
        private void rbnAcumula30_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrPagarAcumula_Run();
        }

        private void rbnAcumula60_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrPagarAcumula_Run();
        }

        private void rbnAcumula90_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrPagarAcumula_Run();
        }

        private void rbnAcumulaTudo_Click(object sender, EventArgs e)
        {
            TotalizaContas();
            bwrPagarAcumula_Run();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            resultado = MessageBox.Show("Deseja Excluir a Duplicata : " + dgvPagar.CurrentRow.Cells["DUPLICATA"].Value.ToString() + " - " + dgvPagar.CurrentRow.Cells["COGNOME"].Value.ToString() + " )", "Aplisoft",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {
                id = clsParser.Int32Parse(dgvPagar.CurrentRow.Cells["ID"].Value.ToString());

                // verificar se o numero da nota fiscal é diferente de 0 (zero)
                Int32 idNotaFiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDNOTAFISCAL from PAGAR where id=" + id + ""));
                Int32 NroNotaFiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from NFCOMPRA where id=" + idNotaFiscal + "" ));
                if (NroNotaFiscal > 0)
                {
                    MessageBox.Show("Duplicata não pode ser excluida pois existe Nota Fiscal/ Despesa !!!");
                }
                else
                {
                    id = clsParser.Int32Parse(dgvPagar.CurrentRow.Cells["ID"].Value.ToString());
                    Decimal Valor;
                    Valor = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valorpago from PAGAR where ID=" + id.ToString(), "0"));
                    //Valor = clsParser.DecimalParse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PAGAR", "VALORPAGO", "ID", id.ToString()));
                    if (Valor > 0)
                    {
                        MessageBox.Show("Duplicata não pode ser excluida pois já houve Pagamento !!!");
                    }
                    else
                    {
                        Valor = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select VALORDEVOLVIDO from PAGAR where ID=" + id.ToString(),"0"));
                        //Valor = clsParser.DecimalParse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "PAGAR", "VALORDEVOLVIDO", "ID", id.ToString()));
                        if (Valor > 0)
                        {
                            MessageBox.Show("Duplicata não pode ser excluida pois tem mercadoria Devolvida !!!");
                        }
                        else
                        {
                            // apagar o contas a pagar observação + clsPagas01 + pagar
                            // Apagar observação do pagar
                            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                            SqlCommand scd = new SqlCommand("delete pagarobserva where idduplicata=@id", scn);
                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();
                            // Apagar os itens do pagar
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scd = new SqlCommand("delete pagar01 where idduplicata=@id", scn);
                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();
                            // Apagar o pagar
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scd = new SqlCommand("delete pagar where id=@id", scn);
                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();

                            MessageBox.Show("Processo de Exclusão concluido");
                            bwrPagar_Run();
                            bwrPagarAcumula_Run();
                        }
                    }
                }
            }
        }

        private void calcularprevisao()
        { // Colocamos numa função financeiro
            // Objetivo é resumir o total com juros e multa para mostrar no fluxo e na tela
            // Mostrar no Grid sem entrar no registro
            Decimal tbxValorLiquido;
            Decimal tbxMulta;
            Decimal tbxJuros;
            Decimal tbxDesconto;

            // a data atual saiu dos rbn
            dtPagar = clsPagarBLL.GridDadosTodosCampos(0, dataAtual, "PREV", "A");
            foreach (DataRow row in dtPagar.Rows)
            {
                if (row["DUPLICATA"].ToString() == "17")
                {
                    //MessageBox.Show("pare");
                }
                tbxVencimento = row["VENCIMENTO"].ToString();
                if (DateTime.Parse(row["VENCIMENTOPREV"].ToString()) > DateTime.Now)
                {
                    tbxDataBaixa = (DateTime.Parse(row["VENCIMENTOPREV"].ToString())).ToString("dd/MM/yyyy");
                }
                else
                {
                    tbxDataBaixa = dataAtual;
                }

                tbxValor = clsParser.DecimalParse(row["VALOR"].ToString());
                tbxValorDesconto = clsParser.DecimalParse(row["VALORDESCONTO"].ToString());
                tbxValorJuros = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                tbxValorMulta = clsParser.DecimalParse(row["VALORMULTA"].ToString());
                atevencimento = row["ATEVENCIMENTO"].ToString(); 

                DuplicataInfo DuplicataInfo;
                DuplicataInfo = clsFinanceiro.CalcularDuplicata("Pagar", tbxVencimento, tbxDataBaixa,
                                                                        tbxValor, +
                                                                        tbxValorDesconto, +
                                                                        tbxValorJuros, +
                                                                        tbxValorMulta, +
                                                                        0, atevencimento);

                    
                tbxValorLiquido = DuplicataInfo.valorliquido; //.ToString("N2");
                tbxMulta = DuplicataInfo.multas; //.ToString("N2");
                tbxJuros = DuplicataInfo.juros; //.ToString("N2");
                tbxDesconto = DuplicataInfo.descontos; //.ToString("N2");
                //tbxSaldo = ((tbxValor + tbxMulta + tbxJuros) - tbxDesconto); //.ToString("N2");
                //totaldias = DuplicataInfo.ntotaldias;
                //tbxDiferenca = (tbxSaldo - (clsParser.DecimalParse(row["VALORPAGO"].ToString()) + clsParser.DecimalParse(row["VALORDEVOLVIDO"].ToString()))); //.ToString("N2");
                tbxDiferenca = (tbxMulta + tbxJuros);
                // Gravar no pagar a diferença no campo valorbaixando
                // ele será somado quando faz o calculo no grid
                if (tbxDiferenca > 0)
                {
                    // Gravar no pagar
                    SqlConnection scn;
                    SqlCommand scd;
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scd = new SqlCommand("UPDATE PAGAR SET " +
                                            "VALORBAIXANDO=@DIFERENCA " +
                                            "WHERE ID = @ID", scn);
                    scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
                    scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = tbxDiferenca;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();

                }

            }
        }

        private void rbnTodos_Click(object sender, EventArgs e)
        {
            if (rbnTodos.Checked == true)
            {
                gbxData.Visible = false;
                tbxDataAtual.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else if (rbnUmDia.Checked == true)
            {
                // mantem a data atual que digitou na tela
                gbxData.Visible = true;
                tbxDataAtual.Select();
                tbxDataAtual.SelectAll();
                if (tbxDataAtual.Text == "")
                {
                    tbxDataAtual.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                tipoLista = "D"; // Apenas do dia marcado
            }
            else if (rbnUmDia.Checked == true)
            {
                if (tbxDataAtual.Text == "")
                {
                    tbxDataAtual.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
              
            }

            else
            {
                gbxData.Visible = false;
                tbxDataAtual.Text = DateTime.Now.ToString("dd/MM/yyyy");
           
            }


            dataAtual = tbxDataAtual.Text;
            bwrPagar_Run();        
            TotalizaContas();
        }

        private void tstPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstPesquisa.Focus();
        }

        private void tstPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            PintaGrid(dgvPagar);
            tstPesquisa.Focus();
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvPagar);
            tbxTotalVisualizacao.Text = clsParser.DecimalParse(clsGridHelper.SomaGrid(dgvPagar, "VALORPAGAR").ToString()).ToString("N2");

            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvPagar,
             "DUPLICATA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                    dgvPagar, "DUPLICATA") == false)
                    {
                        clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                        dgvPagar, "DUPLICATA");
                    }
                }
            }
            else
            {
                if (dgvPagar.Rows.Count > 0)
                {
                    dgvPagar.CurrentCell = dgvPagar.Rows[0].Cells[1];
                }
            }
            if (dgvPagar.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvPagar.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvPagar.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(
                                  dgvPagar.Rows[dgvPagar.CurrentRow.Index -
                                  1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvPagar.CurrentRow.Index < dgvPagar.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(
                                 dgvPagar.Rows[dgvPagar.CurrentRow.Index +
                                 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
            }
            else
            {
                id = 0;
                id_Anterior = 0;
                id_Proximo = 0;
            }
        }

        private void tspAnalise_Click(object sender, EventArgs e)
        {
            resultado = MessageBox.Show("Deseja Verificar se Todos os Documentos " + Environment.NewLine + 
                                        "não Pagos Existem no Contas a Pagar ?  "  + " ", "Aplisoft",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {
                clsPagarBLL clsPagarBLL = new clsPagarBLL();

                Int32 idpagar;
                Int32 iddocumento;
                Int32 idnota = 0;
                Int32 idnota_Ver = 0;
                //1. Pegar todas as parcelas das notas fiscais não pagas
                query = "SELECT NFCOMPRAPAGAR.ID AS [IDNFE],NFCOMPRAPAGAR.IDNOTA,NFCOMPRA.NUMERO,NFCOMPRAPAGAR.POSICAO,NFCOMPRAPAGAR.POSICAOFIM,NFCOMPRAPAGAR.DATA,NFCOMPRAPAGAR.PAGOU,NFCOMPRAPAGAR.VALOR,NFCOMPRAPAGAR.IDTIPOPAGA,NFCOMPRAPAGAR.BOLETONRO,NFCOMPRAPAGAR.DV " +
                        "FROM NFCOMPRAPAGAR " +
                        "INNER JOIN NFCOMPRA ON NFCOMPRA.ID=NFCOMPRAPAGAR.IDNOTA " +
                        "WHERE NFCOMPRAPAGAR.PAGOU='N' AND NFCOMPRA.NUMERO > 0 " +
                        "ORDER BY NFCOMPRAPAGAR.IDNOTA ";
                SqlDataAdapter sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                //scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    // Localizar dentro do contas a pagar este lançamento
                    idpagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PAGAR where IDPAGARNFE= " + clsParser.Int32Parse(row["IDNFE"].ToString()) + " "));
                    if (idpagar == 0)
                    {
                        idnota = clsParser.Int32Parse(row["IDNOTA"].ToString());

                        if (idnota != idnota_Ver)
                        {
                            idnota_Ver = idnota;
                            // transferir esta parcela
                            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iddocumento from nfcompra where id=" + idnota + ""));
                            clsPagarBLL.SicronizarPorDocumento(idnota, iddocumento, clsInfo.conexaosqldados);
                        }
                    }
                }
                MessageBox.Show("Termino de Verificação !!!");
            }
        }

        private void tbxAPagarVencido_TextChanged(object sender, EventArgs e)
        {

        }

        public void PintaGrid(DataGridView _datagrid)
        {   
            int idpagarnfe;
            int pagarnfe;
            foreach (DataGridViewRow row in _datagrid.Rows)
            {
                // Destacando itens com cores
                idpagarnfe = clsParser.Int32Parse(row.Cells["IDNOTAFISCAL"].Value.ToString());
                pagarnfe = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select situacao from nfcompra where id =" + idpagarnfe));
                if (pagarnfe == 30 && row.Cells["DOCUMENTO"].Value.ToString() != "PCO")
                {
                    _datagrid.Rows[row.Index].DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }

        private void dgvPagar_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PintaGrid(dgvPagar);
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
            bwrPagar_Run();
        }

        private void rbnAtrasados_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckxPendencias_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
