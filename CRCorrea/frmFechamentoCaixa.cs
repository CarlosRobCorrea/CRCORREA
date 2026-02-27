using CRCorreaFuncoes;
using CRCorreaBLL;
using CRCorreaInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace CRCorrea
{
    public partial class frmFechamentoCaixa : Form
    {
        Int32 id;
        String query;
        String query1;
        SqlDataAdapter sda;
        DataTable dtFechamentoCaixa;
        DataTable dtFechamentoCaixaAno;
        DataTable dtPedido;
        DataTable dtPedido_0;
        String formapagto = "";
        Decimal ValorPix = 0;
        Decimal ValorDinheiro = 0;
        Decimal ValorCartao = 0;
        Decimal ValorCartaoCredito = 0;
        Decimal ValorParcela = 0;
        Decimal TotalFaturado = 0;
        Decimal TotalCusto = 0;
        Decimal TotalMesAnterior = 0;
        Decimal ValorCusto = 0;

        Int32 MES = 0;
        Int32 ANO = 0;
        Int32 ANOMES = 0;

        clsFechamentoCaixaBLL clsFechamentoCaixaBLL = new clsFechamentoCaixaBLL();
        clsFechamentoCaixaInfo clsFechamentoCaixaInfo = new clsFechamentoCaixaInfo();

        BackgroundWorker bwrFechamentoCaixa;
        BackgroundWorker bwrFechamentoCaixaAno;

        GridColuna[] dtFechamentoCaixaColuna = new GridColuna[]
                                {
                                            new GridColuna("Id", "ID", 10, false, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Data", "DATA", 75, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("___Pix__", "PIX", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Dinheiro", "DINHEIRO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Ct_Debito", "CARTAODEBITO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Ct_Credito", "CARTAOCREDITO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Parcelado", "PARCELADO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Total Dia", "TOTALDIA", 75, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Total Custo", "TOTALCUSTO", 75, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Apuração", "APURADO", 75, true, DataGridViewContentAlignment.MiddleRight)
                                };
        GridColuna[] dtFechamentoCaixaColunaAno = new GridColuna[]
                                {
                                            new GridColuna("Id", "ID", 10, false, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("AnoMes", "ANOMES", 75, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Total Mes", "TOTALMES", 75, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Tot Custo", "TOTALCUSTOMES", 75, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Apurado", "APURADOMES", 75, true, DataGridViewContentAlignment.MiddleRight)
                                };



        public frmFechamentoCaixa()
        {
            InitializeComponent();
        }
        public void Init()
        {
        }
        private void frmFechamentoCaixa_Load(object sender, EventArgs e)
        {
            tbxDataFechamento.Text = DateTime.Now.ToString("dd/MM/yyyy");
            bwrFechamentoCaixa_Run();

            bwrFechamentoCaixaAno_Run();
        }

        private void frmFechamentoCaixa_Activated(object sender, EventArgs e)
        {
            bwrFechamentoCaixa_Run();
        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            //TrataCampos((Control)sender);

        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }
        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            // Filtrar a Data
            ValorPix = 0;
            ValorDinheiro = 0;
            ValorCartao = 0;
            ValorCartaoCredito = 0;
            ValorParcela = 0;
            TotalCusto = 0;

            dtPedido = new DataTable();
            query = "";
            query = "SELECT PEDIDO.NUMERO, PEDIDO.DATA, PEDIDO.QTDESALDO,PEDIDO.TOTALPEDIDO, PEDIDO.IDFORMAPAGTO, PEDIDO.TOTALCUSTO " +
                     "FROM PEDIDO " +
                     "WHERE " +
                     "    PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxDataFechamento.Text + " 00:00", true) + " AND " +
                    "    PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxDataFechamento.Text + " 23:59", true);
             
            query = query + " ORDER BY PEDIDO.DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtPedido);
            foreach (DataRow row in dtPedido.Rows)
            {

                formapagto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo where id = " + clsParser.Int32Parse( row["idformapagto"].ToString()));
                switch (formapagto)
                {
                    case "PI": //pIX
                        ValorPix = ValorPix + clsParser.DecimalParse(row["TOTALPEDIDO"].ToString());
                        break;
                    case "DI": //pIX
                        ValorDinheiro = ValorDinheiro + clsParser.DecimalParse(row["TOTALPEDIDO"].ToString());
                        break;
                    case "CA": //pIX
                        ValorCartao = ValorCartao + clsParser.DecimalParse(row["TOTALPEDIDO"].ToString());
                        break;
                    case "CC": //pIX
                        ValorCartaoCredito = ValorCartaoCredito + clsParser.DecimalParse(row["TOTALPEDIDO"].ToString());
                        break;
                    case "BO": //pIX
                        ValorParcela = ValorParcela + clsParser.DecimalParse(row["TOTALPEDIDO"].ToString());
                        break;
                    default:
                        ValorParcela = ValorParcela + clsParser.DecimalParse(row["TOTALPEDIDO"].ToString());
                        break;
                }
                TotalCusto = TotalCusto + clsParser.DecimalParse(row["TOTALCUSTO"].ToString());
            }
            TotalFaturado = ValorPix + ValorDinheiro + ValorCartao + ValorCartaoCredito + ValorParcela;
            // Gravar o Registro com o Fechamento
            clsFechamentoCaixaInfo = new clsFechamentoCaixaInfo();
            clsFechamentoCaixaInfo.id = id;
            clsFechamentoCaixaInfo.data = clsParser.DateTimeParse(tbxDataFechamento.Text);
            clsFechamentoCaixaInfo.dinheiro = ValorDinheiro;
            clsFechamentoCaixaInfo.parcelado = ValorParcela;
            clsFechamentoCaixaInfo.pix = ValorPix;
            clsFechamentoCaixaInfo.totaldia = (ValorCartao + ValorCartaoCredito + ValorParcela + ValorDinheiro + ValorPix);
            clsFechamentoCaixaInfo.cartaodebito = ValorCartao;
            clsFechamentoCaixaInfo.cartaocredito = ValorCartaoCredito;
            clsFechamentoCaixaInfo.totalcusto = TotalCusto;
            clsFechamentoCaixaInfo.apurado = TotalFaturado - TotalCusto;


            // Verificar se já não fechou este dia
            DateTime DATA = clsParser.DateTimeParse(tbxDataFechamento.Text);
            clsFechamentoCaixaInfo.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from FECHAMENTOCAIXA where data = " + clsParser.SqlDateTimeFormat(DATA.ToString("dd/MM/yyyy") + " 00:00", true)));

            if (clsFechamentoCaixaInfo.id == 0)
            {
                clsFechamentoCaixaInfo.id = clsFechamentoCaixaBLL.Incluir(clsFechamentoCaixaInfo, clsInfo.conexaosqldados);
                id = clsFechamentoCaixaInfo.id;
            }
            else
            {
                clsFechamentoCaixaBLL.Alterar(clsFechamentoCaixaInfo, clsInfo.conexaosqldados);
            }
            // Verificar se ja tem que fechar o mes anterior para lançar no Anual
            int mesano = clsParser.Int32Parse(tbxDataFechamento.Text.Substring(3, 2));
            int mesanoano = clsParser.Int32Parse(tbxDataFechamento.Text.Substring(6, 4));
            if (mesano == 1)
            {
                mesano = 12;
                mesanoano = mesanoano - 1;  // voltar o ano 
            }
            else
            {
                mesano = mesano - 1;
            }
            String Anomes = mesano.ToString().PadLeft(2, '0');
            Anomes = mesanoano.ToString() + Anomes;
            Int32 idFechamentoAno = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from fechamentocaixaano where anomes = " + clsParser.Int32Parse(Anomes)));
            if (idFechamentoAno ==0)
            {
                // Fechar o Caixa do Mes Anterior e Lançar no Fechamento Anual
                Decimal valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select sum(totaldia) from fechamentocaixa " +
                "where " +
                " month(data)= " + mesano +
                " and year(data) = " + mesanoano ));

                Decimal valorcusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select sum(totalcusto) from fechamentocaixa " +
                "where " +
                " month(data)= " + mesano +
                " and year(data) = " + mesanoano));


                SqlConnection scn;
                SqlCommand scd;
                scn = new SqlConnection(clsInfo.conexaosqldados);

                query = "insert into fechamentocaixaano (anomes,totalmes,totalcustomes,apuradomes) values (@anomes, @totalmes, @totalcusto, @apuradomes)";
                scd = new SqlCommand(query, scn);
                scd.Parameters.Add("@anomes", SqlDbType.Int).Value = clsParser.Int32Parse(Anomes);
                scd.Parameters.Add("@totalmes", SqlDbType.Decimal).Value = valoracumulado;
                scd.Parameters.Add("@totalcusto", SqlDbType.Decimal).Value = valorcusto;
                if (valorcusto != 0)
                   { scd.Parameters.Add("@apuradomes", SqlDbType.Decimal).Value = valoracumulado - valorcusto; }
                else
                   { scd.Parameters.Add("@apuradomes", SqlDbType.Decimal).Value = 0; }

                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            bwrFechamentoCaixa_Run();
            bwrFechamentoCaixaAno_Run();
        }
        private void bwrFechamentoCaixa_Run()
        {
            MES = clsParser.Int32Parse(tbxDataFechamento.Text.Substring(3, 2));
            ANO = clsParser.Int32Parse(tbxDataFechamento.Text.Substring(6, 4));

            bwrFechamentoCaixa = new BackgroundWorker();
            bwrFechamentoCaixa.DoWork += new DoWorkEventHandler(bwrFechamentoCaixa_DoWork);
            bwrFechamentoCaixa.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrFechamentoCaixa_RunWorkerCompleted);
            bwrFechamentoCaixa.RunWorkerAsync();
        }

        private void bwrFechamentoCaixa_DoWork(object sender, DoWorkEventArgs e)
        {
            dtFechamentoCaixa = new DataTable();
            query = "";
            query = "SELECT " +
                    "    FechamentoCaixa.ID, " +
                    "    FechamentoCaixa.DATA, " +
                    "    FechamentoCaixa.PIX, " +
                    "    FechamentoCaixa.DINHEIRO, " +
                    "    FechamentoCaixa.CARTAODEBITO, " +
                    "    FechamentoCaixa.CARTAOCREDITO, " +
                    "    FechamentoCaixa.PARCELADO, " +
                    "    FechamentoCaixa.TOTALDIA, " +
                    "    FechamentoCaixa.TOTALCUSTO, " +
                    "    FechamentoCaixa.APURADO " +
                    "FROM " +
                    "    FechamentoCaixa " +
                    "WHERE " +
                    "    MONTH(FechamentoCaixa.DATA)= " + MES  + " AND " +
                    "    YEAR(FechamentoCaixa.DATA)= " + ANO;
                    

            query = query + " ORDER BY FechamentoCaixa.DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtFechamentoCaixa);
        }
        private void bwrFechamentoCaixa_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvFechamentoCaixa.DataSource = dtFechamentoCaixa;
                clsGridHelper.MontaGrid2(dgvFechamentoCaixa, dtFechamentoCaixaColuna, true);
                clsGridHelper.FontGrid(dgvFechamentoCaixa, 8);
                clsGridHelper.SelecionaLinha(id, dgvFechamentoCaixa, 1);
                //dgvFechamentoCaixa.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
                dgvFechamentoCaixa.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvFechamentoCaixa.Columns["PIX"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixa.Columns["TOTALDIA"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixa.Columns["DINHEIRO"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixa.Columns["CARTAODEBITO"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixa.Columns["CARTAOCREDITO"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixa.Columns["TOTALCUSTO"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixa.Columns["APURADO"].DefaultCellStyle.Format = "N2";

                dgvFechamentoCaixa.Select();

                SomarFaturamento();
            //    if (dtFechamentoCaixa.Rows.Count > 0)
            //    {
            //        bwrFechamentoCaixa_Run();
            //    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bwrFechamentoCaixaAno_Run()
        {

            bwrFechamentoCaixaAno = new BackgroundWorker();
            bwrFechamentoCaixaAno.DoWork += new DoWorkEventHandler(bwrFechamentoCaixaAno_DoWork);
            bwrFechamentoCaixaAno.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrFechamentoCaixaAno_RunWorkerCompleted);
            bwrFechamentoCaixaAno.RunWorkerAsync();
        }

        private void bwrFechamentoCaixaAno_DoWork(object sender, DoWorkEventArgs e)
        {
            dtFechamentoCaixaAno = new DataTable();
            query1 = "";
            query1 = "SELECT " +
                    "    FechamentoCaixaAno.ID, " +
                    "    FechamentoCaixaAno.ANOMES, " +
                    "    FechamentoCaixaAno.TOTALMES, " +
                    "    FechamentoCaixaAno.TOTALCUSTOMES, " +
                    "    FechamentoCaixaAno.APURADOMES " +
                    "FROM " +
                    "    FechamentoCaixaAno ";

            query1 = query1 + " ORDER BY FechamentoCaixaAno.ANOMES DESC ";
            sda = new SqlDataAdapter(query1, clsInfo.conexaosqldados);
            sda.Fill(dtFechamentoCaixaAno);

            TotalMesAnterior = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TOTALMES from FechamentoCaixaAno order by anomes desc" ));
        }
        private void bwrFechamentoCaixaAno_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvFechamentoCaixaAno.DataSource = dtFechamentoCaixaAno;
                clsGridHelper.MontaGrid2(dgvFechamentoCaixaAno, dtFechamentoCaixaColunaAno, true);
                clsGridHelper.FontGrid(dgvFechamentoCaixaAno, 8);
                clsGridHelper.SelecionaLinha(id, dgvFechamentoCaixaAno, 1);
                //dgvFechamentoCaixa.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
                dgvFechamentoCaixaAno.Columns["TOTALMES"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixaAno.Columns["TOTALCUSTOMES"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixaAno.Columns["APURADOMES"].DefaultCellStyle.Format = "N2";
                dgvFechamentoCaixaAno.Select();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SomarFaturamento()
        {
            tbxTotalMes.Text = "0";
            tbxTotalApurado.Text = "0";

            Decimal soma = 0;
            Decimal soma1 = 0;


            foreach (DataRow row in dtFechamentoCaixa.Rows)
            {
                    soma = (soma + clsParser.DecimalParse(row["TOTALDIA"].ToString()));
                    soma1 = (soma1 + clsParser.DecimalParse(row["APURADO"].ToString()));
            }
            tbxTotalMes.Text = soma.ToString("N2");
            tbxTotalApurado.Text = soma1.ToString("N2");
            Decimal TaxaCrescimento = 0;
            if (soma > 0)
            {
                TaxaCrescimento = TotalMesAnterior / soma;
            }
            TaxaCrescimento = (1 - TaxaCrescimento)*100;
            tbxTaxaCrescimo.Text = TaxaCrescimento.ToString("N2");
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento");
        }

        private void btnRevisaCusto_Click(object sender, EventArgs e)
        {
            // Filtrar a Data
            ValorPix = 0;
            ValorDinheiro = 0; 
            ValorCartao = 0;
            ValorCartaoCredito = 0;
            ValorParcela = 0;
            TotalCusto = 0;

            dtPedido = new DataTable();
            query = "";
            query = "select pedido.id as [idpedido], pedido.numero,pedido.data, pedido1.qtde, pedido1.totalnota, pecas.id as [idcodigo], pecas.codigo, pecas.nome as produto, pecasclassifica.codigo as grupo," +
                    "pedido1.preco, pedido1.precocusto " +
                    "from pedido " +
                    "left join pedido1 on pedido1.idpedido = pedido.id " +
                    "left join pecas on pecas.id = pedido1.idcodigo " +                            
                    "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica " +
                    "WHERE " +
                    "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxDataFechamento.Text + " 00:00", true) + " AND " +
                    "PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxDataFechamento.Text + " 23:59", true) +
                    "AND PEDIDO1.PRECOCUSTO <= 0 " ;
            query = query + " ORDER BY PEDIDO.DATA ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtPedido);
            foreach (DataRow row in dtPedido.Rows)
            {
                ValorCusto = 0;
                // Verificar se tem o preço de custo na peça
                ValorCusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select precocompra from pecas where id = " + clsParser.Int32Parse(row["idcodigo"].ToString())));
                if (ValorCusto == 0)
                {
                    // Verificar se tem o preço de custo na classificação
                    if (clsParser.DecimalParse(row["preco"].ToString()) > 0)
                    {
                        ValorCusto = clsParser.DecimalParse(((clsParser.DecimalParse(row["preco"].ToString()) * 70) / 100).ToString("N4"));
                    }
                    else if (clsParser.DecimalParse(row["totalnota"].ToString()) > 0)
                    {
                        Decimal precosos = clsParser.DecimalParse(row["totalnota"].ToString()) / clsParser.DecimalParse(row["qtde"].ToString());
                        ValorCusto = ((precosos * 70) / 100);
                    }
                }
                if (ValorCusto == 0)
                {
                    MessageBox.Show("Anote não achou o custo do item: " + row["codigo"].ToString());
                }
                else
                {
                    // Atualizar o custo do item
                    SqlConnection scn;
                    SqlCommand scd;
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    query = "update pedido1 set precocusto = @precocusto, totalcustoitem = @totalcustoitem where idpedido = @idpedido and idcodigo = @idcodigo";
                    scd = new SqlCommand(query, scn);
                    scd.Parameters.Add("@precocusto", SqlDbType.Decimal).Value = ValorCusto;
                    scd.Parameters.Add("@totalcustoitem", SqlDbType.Decimal).Value = ValorCusto * clsParser.DecimalParse(row["qtde"].ToString()); ;
                    scd.Parameters.Add("@idpedido", SqlDbType.Int).Value = clsParser.Int32Parse(row["idpedido"].ToString());
                    scd.Parameters.Add("@idcodigo", SqlDbType.Int).Value = clsParser.Int32Parse(row["idcodigo"].ToString());
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();
                    // Somar o total de custo no cabecalho do pedido
                    Decimal valorcusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                        "select sum(totalcustoitem) from pedido1 " +
                                        "where " +
                                        "pedido1.idpedido= " + clsParser.Int32Parse(row["idpedido"].ToString())));


                    query = "update pedido set totalcusto = @totalcusto where id= @idpedido";
                    scd = new SqlCommand(query, scn);
                    scd.Parameters.Add("@idpedido", SqlDbType.Int).Value = clsParser.Int32Parse(row["idpedido"].ToString());
                    scd.Parameters.Add("@totalcusto", SqlDbType.Decimal).Value = valorcusto;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();



                }
            }
            bwrFechamentoCaixa_Run();
            bwrFechamentoCaixaAno_Run();

        }

        private void dgvFechamentoCaixa_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvFechamentoCaixa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbxDataFechamento.Text = dgvFechamentoCaixa.CurrentRow.Cells["data"].Value.ToString();
            tbxDataFechamento.Text = tbxDataFechamento.Text.Substring(0, 10);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRevisaCustoTodos_Click(object sender, EventArgs e)
        {
            // Filtrar a Data
            Int32 Anomes = 0;
            Int32 AnomesFinal = clsParser.Int32Parse(tbxDataFechamento.Text.Substring(6, 4) + tbxDataFechamento.Text.Substring(3, 2));
            String DataFechamento = "01/" + tbxDataFechamento.Text.Substring(3, 2) + "/" + tbxDataFechamento.Text.Substring(6, 4);
            dtPedido = new DataTable();
            query = "";
            query = "select pedido.id, pedido1.id as [idped1], pedido.numero,pedido.data, pedido1.qtde, pedido1.totalnota, pecas.id as [idcodigo], pecas.codigo, pecas.nome as produto, pecasclassifica.codigo as grupo," +
                    "pedido1.preco, pedido1.precocusto, pedido1.totalcustoitem " +
                    "from pedido " +
                    "left join pedido1 on pedido1.idpedido = pedido.id " +
                    "left join pecas on pecas.id = pedido1.idcodigo " +
                    "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica " +
                    "WHERE " +
                    "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat("07/08/2025" + " 00:00", true) +
                    "AND PEDIDO.DATA < " + clsParser.SqlDateTimeFormat(DataFechamento + " 00:00", true);
            query = query + " ORDER BY PEDIDO.id ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtPedido);
            foreach (DataRow row in dtPedido.Rows)
            {
                if (clsParser.DecimalParse(row["precocusto"].ToString()) == 0 & clsParser.Int32Parse(row["idped1"].ToString()) > 0)
                {
                    ValorCusto = 0;
                    // Verificar se tem o preço de custo na peça
                    ValorCusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select precocompra from pecas where id = " + clsParser.Int32Parse(row["idcodigo"].ToString())));
                    if (ValorCusto == 0)
                    {
                        // Verificar se tem o preço de custo na classificação
                        if (clsParser.DecimalParse(row["preco"].ToString()) > 0)
                        {
                            ValorCusto = clsParser.DecimalParse(((clsParser.DecimalParse(row["preco"].ToString()) * 70) / 100).ToString("N4"));

                        }
                        else if (clsParser.DecimalParse(row["totalnota"].ToString()) > 0)
                        {
                            Decimal precosos = clsParser.DecimalParse(row["totalnota"].ToString()) / clsParser.DecimalParse(row["qtde"].ToString());
                            ValorCusto = ((precosos * 70) / 100);
                        }

                    }
                    if (ValorCusto == 0)
                    {
                        MessageBox.Show("Anote não achou o custo do item: " + row["codigo"].ToString()+ "-" + row["produto"].ToString());
                    }
                    else
                    {
                        // Atualizar o custo do item
                        SqlConnection scn;
                        SqlCommand scd;
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        query = "update pedido1 set precocusto = @precocusto, totalcustoitem = @totalcustoitem where idpedido = @idpedido and idcodigo = @idcodigo";
                        scd = new SqlCommand(query, scn);
                        scd.Parameters.Add("@precocusto", SqlDbType.Decimal).Value = ValorCusto;
                        scd.Parameters.Add("@totalcustoitem", SqlDbType.Decimal).Value = ValorCusto * clsParser.DecimalParse(row["qtde"].ToString()); ;
                        scd.Parameters.Add("@idpedido", SqlDbType.Int).Value = clsParser.Int32Parse(row["id"].ToString());
                        scd.Parameters.Add("@idcodigo", SqlDbType.Int).Value = clsParser.Int32Parse(row["idcodigo"].ToString());
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                        // Somar o total de custo no cabecalho do pedido
                        Decimal valorcusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                            "select sum(totalcustoitem) from pedido1 " +
                                            "where " +
                                            "pedido1.idpedido= " + clsParser.Int32Parse(row["id"].ToString())));


                        query = "update pedido set totalcusto = @totalcusto where id= @idpedido";
                        scd = new SqlCommand(query, scn);
                        scd.Parameters.Add("@idpedido", SqlDbType.Int).Value = clsParser.Int32Parse(row["id"].ToString());
                        scd.Parameters.Add("@totalcusto", SqlDbType.Decimal).Value = valorcusto;
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                    }
                }
                else
                {  // Fazer o total do pedido novamente
                   // Somar o total de custo no cabecalho do pedido
                    SqlConnection scn;
                    SqlCommand scd;
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    Decimal valorcusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                        "select sum(totalcustoitem) from pedido1 " +
                                        "where " +
                                        "pedido1.idpedido= " + clsParser.Int32Parse(row["id"].ToString())));
                    query = "update pedido set totalcusto = @totalcusto where id= @idpedido";
                    scd = new SqlCommand(query, scn);
                    scd.Parameters.Add("@idpedido", SqlDbType.Int).Value = clsParser.Int32Parse(row["id"].ToString());
                    scd.Parameters.Add("@totalcusto", SqlDbType.Decimal).Value = valorcusto;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();

                }
            }

            DateTime DataFechamentoPrevista = clsParser.DateTimeParse("01/07/2025");
            DateTime DataFechamentoAtual = clsParser.DateTimeParse("01/02/2026");

            int QtdeDiasAteHoje = (DataFechamentoAtual - DataFechamentoPrevista).Days;


            for (int i = 0; i < QtdeDiasAteHoje; i++)
            { // acumular no fechamento de caixa diario
                DataFechamentoPrevista =  DataFechamentoPrevista.AddDays(1);

                dtPedido_0 = new DataTable();
                query = "";
                query = "SELECT PEDIDO.ID,PEDIDO.NUMERO, PEDIDO.DATA, PEDIDO.QTDESALDO,PEDIDO.TOTALPEDIDO, PEDIDO.IDFORMAPAGTO, PEDIDO.TOTALCUSTO " +
                         "FROM PEDIDO " +
                         "WHERE " +
                         "    PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(DataFechamentoPrevista.ToString("dd/MM/yyyy") + " 00:00", true) + " AND " +
                        "    PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(DataFechamentoPrevista.ToString("dd/MM/yyyy") + " 23:59", true);

                query = query + " ORDER BY PEDIDO.DATA DESC ";
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtPedido_0);
                if (dtPedido_0.Rows.Count > 0)
                {
                    ValorPix = 0;
                    ValorDinheiro = 0;
                    ValorCartao = 0;
                    ValorCartaoCredito = 0;
                    ValorParcela = 0;
                    TotalCusto = 0;

                    foreach (DataRow row1 in dtPedido_0.Rows)
                    {

                        formapagto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo where id = " + clsParser.Int32Parse(row1["idformapagto"].ToString()));
                        switch (formapagto)
                        {
                            case "PI": //pIX
                                ValorPix = ValorPix + clsParser.DecimalParse(row1["TOTALPEDIDO"].ToString());
                                break;
                            case "DI": //pIX
                                ValorDinheiro = ValorDinheiro + clsParser.DecimalParse(row1["TOTALPEDIDO"].ToString());
                                break;
                            case "CA": //pIX
                                ValorCartao = ValorCartao + clsParser.DecimalParse(row1["TOTALPEDIDO"].ToString());
                                break;
                            case "CC": //pIX
                                ValorCartaoCredito = ValorCartaoCredito + clsParser.DecimalParse(row1["TOTALPEDIDO"].ToString());
                                break;
                            case "BO": //pIX
                                ValorParcela = ValorParcela + clsParser.DecimalParse(row1["TOTALPEDIDO"].ToString());
                                break;
                            default:
                                ValorParcela = ValorParcela + clsParser.DecimalParse(row1["TOTALPEDIDO"].ToString());
                                break;
                        }
                        TotalCusto = TotalCusto + clsParser.DecimalParse(row1["TOTALCUSTO"].ToString());
                    }
                    TotalFaturado = ValorPix + ValorDinheiro + ValorCartao + ValorCartaoCredito + ValorParcela;
                    if (TotalFaturado > 0)
                    {
                        // Gravar o Registro com o Fechamento
                        if (TotalCusto > TotalFaturado)
                        {
                            TotalCusto = ((TotalFaturado * 70) / 100);
                        }

                        clsFechamentoCaixaInfo = new clsFechamentoCaixaInfo();
                        clsFechamentoCaixaInfo.id = id;
                        clsFechamentoCaixaInfo.data = DataFechamentoPrevista;
                        clsFechamentoCaixaInfo.dinheiro = ValorDinheiro;
                        clsFechamentoCaixaInfo.parcelado = ValorParcela;
                        clsFechamentoCaixaInfo.pix = ValorPix;
                        clsFechamentoCaixaInfo.totaldia = (ValorCartao + ValorCartaoCredito + ValorParcela + ValorDinheiro + ValorPix);
                        clsFechamentoCaixaInfo.cartaodebito = ValorCartao;
                        clsFechamentoCaixaInfo.cartaocredito = ValorCartaoCredito;
                        clsFechamentoCaixaInfo.totalcusto = TotalCusto;
                        clsFechamentoCaixaInfo.apurado = TotalFaturado - TotalCusto;


                        // Verificar se já não fechou este dia
                        DateTime DATA = clsParser.DateTimeParse(DataFechamentoPrevista.ToString());
                        clsFechamentoCaixaInfo.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from FECHAMENTOCAIXA where data = " + clsParser.SqlDateTimeFormat(DATA.ToString("dd/MM/yyyy") + " 00:00", true)));

                        if (clsFechamentoCaixaInfo.id == 0)
                        {
                            clsFechamentoCaixaInfo.id = clsFechamentoCaixaBLL.Incluir(clsFechamentoCaixaInfo, clsInfo.conexaosqldados);
                            id = clsFechamentoCaixaInfo.id;
                        }
                        else
                        {
                            clsFechamentoCaixaBLL.Alterar(clsFechamentoCaixaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }

            }
            Int32 QtdeMeses = 7;
            Int32 AnoMes = 202507;
            for (int i = 0; i < QtdeMeses; i++)
            { // acumular no fechamento de caixa anual
              // Acumular os meses
                String Anomes2 = (clsParser.Int32Parse(AnoMes.ToString().Substring(4, 2))).ToString();
                Anomes2 = Anomes2.PadLeft(2, '0');
                Int32 AnomesAcumula = clsParser.Int32Parse(AnoMes.ToString().Substring(0,4) + Anomes2);
                Int32 idFechamentoAno = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from fechamentocaixaano where anomes = " + AnomesAcumula));
                if (idFechamentoAno > 0)
                {
                    // Fechar o Caixa do Mes Anterior e Lançar no Fechamento Anual
                    Decimal valoracumulado = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select sum(totaldia) from fechamentocaixa " +
                    "where " +
                    " month(data)= " + Anomes2 +
                    " and year(data) = " + clsParser.Int32Parse(AnoMes.ToString().Substring(0, 4))));

                    Decimal valorcusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select sum(totalcusto) from fechamentocaixa " +
                    "where " +
                    " month(data)= " + Anomes2 +
                    " and year(data) = " + clsParser.Int32Parse(AnoMes.ToString().Substring(0, 4))));

                    if (valorcusto > 0 || valoracumulado > 0)
                    {
                        SqlConnection scn;
                        SqlCommand scd;
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        query = "update fechamentocaixaano set totalmes = @totalmes, totalcustomes = @totalcusto, apuradomes = @apuradomes where id = @idFechamentoAno";
                        //query = "insert into fechamentocaixaano (anomes,totalmes,totalcustomes,apuradomes) values (@anomes, @totalmes, @totalcusto, @apuradomes)";
                        scd = new SqlCommand(query, scn);
                        scd.Parameters.Add("@idFechamentoAno", SqlDbType.Int).Value = idFechamentoAno;
                        scd.Parameters.Add("@totalmes", SqlDbType.Decimal).Value = valoracumulado;
                        scd.Parameters.Add("@totalcusto", SqlDbType.Decimal).Value = valorcusto;
                        if (valorcusto != 0)
                        { scd.Parameters.Add("@apuradomes", SqlDbType.Decimal).Value = valoracumulado - valorcusto; }
                        else
                        { scd.Parameters.Add("@apuradomes", SqlDbType.Decimal).Value = 0; }

                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();
                    }
                }
                Int32 Mes12 = clsParser.Int32Parse(Anomes2) + 1;
                if (Mes12 > 12)
                {
                    Int32 Ano = clsParser.Int32Parse(AnoMes.ToString().Substring(0, 4)) + 1;
                    String Acumula = Ano.ToString() + "01";
                    AnoMes = clsParser.Int32Parse(Acumula);
                }
                else
                {
                    String Acumula = AnoMes.ToString().Substring(0, 4).ToString() + Mes12.ToString().PadLeft(2, '0');
                    AnoMes = clsParser.Int32Parse(Acumula);
                }


            }
            MessageBox.Show("Revisão de custo finalizada, revise os itens que não foram encontrados o custo para atualizar manualmente", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bwrFechamentoCaixa_Run();
            bwrFechamentoCaixaAno_Run();

        }
    }
}
