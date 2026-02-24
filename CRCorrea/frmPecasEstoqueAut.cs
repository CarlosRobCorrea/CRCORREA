using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecasEstoqueAut : Form
    {
        String query;

        DataTable dtPecas;

        clsMovPecasBLL clsMovPecasBLL;
        clsMovPecasInfo clsMovPecasInfo;

        Decimal preco;

        SqlConnection scn;
        SqlCommand scd;
        SqlDataReader sdr;

        public frmPecasEstoqueAut()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxCodigoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxCodigoAte);
        }
        private void frmPecasEstoqueAut_Load(object sender, EventArgs e)
        {
            tbxDataini.Text = DateTime.Now.ToString("dd/MM/yyyy");
            tbxDatafim.Text = DateTime.Now.ToString("dd/MM/yyyy");
            clsMovPecasBLL = new clsMovPecasBLL();

        }  
        private void frmPecasEstoqueAut_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }
        private void tspExecutar_Click(object sender, EventArgs e)
        {
            if (clsParser.SqlDateTimeParse(tbxDataini.Text).IsNull == false)
            {
                if (clsParser.SqlDateTimeParse(tbxDataini.Text).Value <= clsParser.SqlDateTimeParse(tbxDatafim.Text).Value)
                {
                    String ApagarMovimentacao = "N";
                    DialogResult drt;
                    drt = MessageBox.Show("Deseja apagar os lançamentos da movimentação destes itens ? ", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (drt == DialogResult.Yes)
                    {
                        ApagarMovimentacao = "S";
                    }
                    lbxDescricao.Text = "0 - ABC...";
                    pgrExecuta.Maximum = 0;
                    //
                    gbxItens.Enabled = false;
                    //
                    gbxPeriodo.Enabled = false;
                    //
                    query = "Select * from Pecas ";
                    if (tbxCodigoDe.Text.Length > 0)
                    {
                        query = query + " where CODIGO >= '" + tbxCodigoDe.Text + "' ";
                    }
                    if (tbxCodigoAte.Text.Length > 0)
                    {
                        if (tbxCodigoDe.Text.Length > 0)
                        {
                            query = query + " AND ";
                        }
                        else
                        {
                            query = query + " WHERE ";
                        }
                        query = query + " CODIGO <= '" + tbxCodigoAte.Text + "' ";
                    }

                    scn = new SqlConnection(clsInfo.conexaosqldados);

                    SqlDataAdapter sda;
                    sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                    dtPecas = new DataTable();
                    sda.Fill(dtPecas);
                    foreach (DataRow row in dtPecas.Rows)
                    {
                        if (ApagarMovimentacao == "S")
                        {

                            //RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(dataAtualde + " 00:00", true)
                            scd = new SqlCommand("delete movpecas where idcodigo=@iditem and data >=" + clsParser.SqlDateTimeFormat(tbxDataini.Text + " 00:00",true) + "", scn);
                            scd.Parameters.Add("@iditem", SqlDbType.Int).Value = clsParser.Int32Parse(row["id"].ToString());
                            scn.Open();
                            sdr = scd.ExecuteReader();
                            scn.Close();


                        }
                        if (row["TIPOENTRADA"].ToString() == "00" || row["TIPOENTRADA"].ToString() == "10" || row["TIPOENTRADA"].ToString() == "20" || row["TIPOENTRADA"].ToString() == "40")
                        {
                            // verificar as notas fiscais de entrada
                            AcumulaNFE(clsParser.Int32Parse(row["id"].ToString()), tbxDataini.Text, tbxDatafim.Text);
                            // verificar as requisições de materiais (RM)
                            AcumulaRM(clsParser.Int32Parse(row["id"].ToString()), tbxDataini.Text, tbxDatafim.Text);
                            //// verificar as notas fiscais de saida
                            //AcumulaNFV(clsParser.Int32Parse(row["id"].ToString()), tbxDataini.Text, tbxDatafim.Text);
                            // verificar os Pedidos de Venda
                            AcumulaPED(clsParser.Int32Parse(row["id"].ToString()), tbxDataini.Text, tbxDatafim.Text);
                            // verificar as Ordens de Serviço
                            //AcumulaOS(clsParser.Int32Parse(row["id"].ToString()), tbxDataini.Text, tbxDatafim.Text);
                        }
                    }
                    //
                    MessageBox.Show("Termino de Execução !!");
                    //
                    gbxItens.Enabled = true;
                    //
                    gbxPeriodo.Enabled = true;
                    //

                    
                }
                else
                {
                    MessageBox.Show("Data inicial maior que a data final", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("Informe a data inicial", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            tbxDataini.Select();
            tbxDataini.SelectAll();
        }

        private void tspCancelar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado por hora");
        } 

        private void tspRetornar_Click(object sender, EventArgs e)
        {                    
            this.Dispose();
            this.Close();
        }

        private void btnCodigoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigoDe.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECAS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnCodigoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigoAte.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECAS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }


        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            clsVisual.FormatarCampoNumerico(sender);
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
            if (clsInfo.znomegrid == btnCodigoDe.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxCodigoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCodigoNomeDe.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxCodigoDe.Select();
                }
            }
            if (ctl.Name == tbxCodigoDe.Name)
            {
                if (tbxCodigoDe.Text.Trim().Length > 0)
                {
                    tbxCodigoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' " );
                    tbxCodigoNomeDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' ");
                }
                if (tbxCodigoAte.Text.Trim().Length == 0)
                {
                    tbxCodigoAte.Text = tbxCodigoDe.Text;
                    tbxCodigoNomeAte.Text = tbxCodigoNomeDe.Text;
                }

            }
            if (clsInfo.znomegrid == btnCodigoAte.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxCodigoAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCodigoNomeAte.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxCodigoAte.Select();
                }
            }
            if (ctl.Name == tbxCodigoAte.Name)
            {
                if (tbxCodigoAte.Text.Trim().Length > 0)
                {
                    tbxCodigoAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS WHERE CODIGO= '" + tbxCodigoAte.Text + "' ");
                    tbxCodigoNomeAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE CODIGO= '" + tbxCodigoAte.Text + "' ");
                }
            }
            clsInfo.znomegrid = "";
        }
        private void AcumulaNFE(Int32 idCodigo, String DaData, String AteData)
        {
            
            query = "Select NFCOMPRA.ID AS [IDNFE], NFCOMPRA1.ID AS [IDNFE1], NFCOMPRA.IDDOCUMENTO AS [IDNFEDOC], " +
                    "NFCOMPRA.NUMERO, NFCOMPRA.DATARECEBIMENTO, " +
                    "NFCOMPRA1.QTDE, NFCOMPRA1.PRECO, NFCOMPRA1.CREDITARICM, NFCOMPRA1.TIPOENTRADA, NFCOMPRA1.TOTALMERCADO, NFCOMPRA1.CUSTOICM  " +
                    "from NFCompra1 " +
                    "LEFT JOIN NFCOMPRA ON NFCOMPRA.ID=NFCOMPRA1.NUMERO " +
                    "WHERE NFCOMPRA1.IDCODIGO = @IDCODIGO " +
                    " AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(DaData + " 00:00", true) +
                    " AND NFCOMPRA.DATARECEBIMENTO <= " + clsParser.SqlDateTimeFormat(AteData + " 23:59", true) +
                    " ORDER BY NFCOMPRA.DATARECEBIMENTO, NFCOMPRA1.ID ";
            SqlDataAdapter sda;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idCodigo;

            DataTable dtNFCompras = new DataTable();
            sda.Fill(dtNFCompras);
            foreach (DataRow row in dtNFCompras.Rows)
            {
                if (row["TIPOENTRADA"].ToString().PadRight(2,' ').Substring(0, 2) == "00" || 
                    row["TIPOENTRADA"].ToString().PadRight(2,' ').Substring(0, 2) == "10" || 
                    row["TIPOENTRADA"].ToString().PadRight(2,' ').Substring(0, 2) == "20" || 
                    row["TIPOENTRADA"].ToString().PadRight(2,' ').Substring(0, 2) == "40")
                {
                    if (row["CREDITARICM"].ToString() == "S")
                    {
                        if (clsParser.DecimalParse(row["TOTALMERCADO"].ToString()) > 0)
                        {
                            preco = clsVisual.Truncar(((clsParser.DecimalParse(row["TOTALMERCADO"].ToString()) - clsParser.DecimalParse(row["CUSTOICM"].ToString())) / clsParser.DecimalParse(row["QTDE"].ToString())), 6);
                        }
                    }
                    else
                    {
                        if (clsParser.DecimalParse(row["TOTALMERCADO"].ToString()) > 0)
                        {
                            preco = clsVisual.Truncar(((clsParser.DecimalParse(row["TOTALMERCADO"].ToString())) / clsParser.DecimalParse(row["QTDE"].ToString())), 6);
                        }
                    }
                    // Verificar se já foi lançado
                    clsMovPecasInfo = clsMovPecasBLL.Carregar(clsParser.Int32Parse(row["IDNFEDOC"].ToString()), clsParser.Int32Parse(row["IDNFE"].ToString()), clsParser.Int32Parse(row["IDNFE1"].ToString()), 0, clsInfo.conexaosqldados);
                    if (clsMovPecasInfo.id == 0)
                    {  // Incluir
                        clsMovPecasBLL.MovimentaItem(idCodigo,
                                  "E",
                                  clsParser.DecimalParse(row["QTDE"].ToString()),
                                  DateTime.Parse(row["DATARECEBIMENTO"].ToString()),
                                  clsParser.Int32Parse(row["IDNFEDOC"].ToString()),
                                  clsParser.Int32Parse(row["IDNFE"].ToString()),
                                  clsParser.Int32Parse(row["IDNFE1"].ToString()),
                                  0,
                                  preco,
                                  clsInfo.zusuario,
                                  clsInfo.conexaosqldados);
                    }
                    else
                    {  // Alterar
                        clsMovPecasBLL.MovimentaItem(idCodigo, "E", clsParser.DecimalParse(row["QTDE"].ToString()),
                                  DateTime.Parse(row["DATARECEBIMENTO"].ToString()),
                                  clsParser.Int32Parse(row["IDNFEDOC"].ToString()),
                                  clsParser.Int32Parse(row["IDNFE"].ToString()),
                                  clsParser.Int32Parse(row["IDNFE1"].ToString()),
                                  0,preco, clsInfo.zusuario,
                                  clsInfo.conexaosqldados);
                    }
                }
            }
        }
        private void AcumulaRM(Int32 idCodigo, String DaData, String AteData)
        {
            query = "Select REQUISICAO.ID AS [IDRM], REQUISICAO1.ID AS [IDRM1], " +
                    "REQUISICAO.NUMERO, REQUISICAO.DATA, REQUISICAO1.MOTIVO, " +
                    "REQUISICAO1.QTDE, REQUISICAO1.VALOR, REQUISICAO.EMITENTE " +
                    "from REQUISICAO1 " +
                    "LEFT JOIN REQUISICAO ON REQUISICAO.ID=REQUISICAO1.NUMERO " +
                    "WHERE REQUISICAO1.IDCODIGO = @IDCODIGO " +
                    " AND REQUISICAO.DATA >= " + clsParser.SqlDateTimeFormat(DaData + " 00:00", true) +
                    " AND REQUISICAO.DATA <= " + clsParser.SqlDateTimeFormat(AteData + " 23:59", true) +
                    "ORDER BY REQUISICAO.DATA, REQUISICAO.ID, REQUISICAO1.MOTIVO, REQUISICAO1.id  ";

            SqlDataAdapter sda;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idCodigo;

            DataTable dtRequisicao = new DataTable();
            sda.Fill(dtRequisicao);
            foreach (DataRow row in dtRequisicao.Rows)
            {
                Int32 IdDocumento = clsInfo.zdocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + "RM" + "' "));
                // Verificar se já foi lançado
                clsMovPecasInfo = clsMovPecasBLL.Carregar(IdDocumento, clsParser.Int32Parse(row["IDRM"].ToString()), clsParser.Int32Parse(row["IDRM1"].ToString()), 0, clsInfo.conexaosqldados);
                if (clsMovPecasInfo.id == 0)
                {  // Incluir
                    clsMovPecasBLL.MovimentaItem(idCodigo,
                              row["MOTIVO"].ToString(),
                              clsParser.DecimalParse(row["QTDE"].ToString()),
                              DateTime.Parse(row["DATA"].ToString()),
                              IdDocumento,
                              clsParser.Int32Parse(row["IDRM"].ToString()),
                              clsParser.Int32Parse(row["IDRM1"].ToString()),
                              0,
                              clsParser.DecimalParse(row["VALOR"].ToString()),
                              row["EMITENTE"].ToString(),
                              clsInfo.conexaosqldados);
                }
                else
                {  // Alterar
                    clsMovPecasBLL.MovimentaItem(idCodigo, row["MOTIVO"].ToString(), clsParser.DecimalParse(row["QTDE"].ToString()),
                        DateTime.Parse(row["DATA"].ToString()),IdDocumento,
                              clsParser.Int32Parse(row["IDRM"].ToString()),
                              clsParser.Int32Parse(row["IDRM1"].ToString()),
                              0,
                              clsParser.DecimalParse(row["VALOR"].ToString()),clsInfo.zusuario,
                              clsInfo.conexaosqldados);
                }
            }
        }
        private void AcumulaNFV(Int32 idCodigo, String DaData, String AteData)
        {
            if (idCodigo == 754 || idCodigo == 757)
            {
                //  MessageBox.Show("pare");
            }
            query = "Select NFVENDA.ID AS [IDNFV], NFVENDA1.ID AS [IDNFV1], NFVENDA.IDDOCUMENTO AS [IDNFVDOC], " +
                    "NFVENDA.NUMERO, NFVENDA.DATA, " +
                    "NFVENDA1.IDCODIGO, NFVENDA1.QTDE, NFVENDA1.PRECO, NFVENDA.TIPOENTRADA " +
                    "from NFVENDA1 " +
                    "LEFT JOIN NFVENDA ON NFVENDA.ID=NFVENDA1.NUMERO " +
                    "WHERE NFVENDA1.IDCODIGO = @IDCODIGO " +
                    " AND NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(DaData + " 00:00", true) +
                    " AND NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(AteData + " 23:59", true) +
                    " AND NFVENDA.FILIAL = @FILIAL " +
                    " ORDER BY NFVENDA.DATA, NFVENDA1.ID ";
            SqlDataAdapter sda;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idCodigo;
            sda.SelectCommand.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;

            DataTable dtNFVendas = new DataTable();
            sda.Fill(dtNFVendas);
            foreach (DataRow row in dtNFVendas.Rows)
            {
                String tipoentrada = "";
                String datarecebimento = DateTime.Parse(row["DATA"].ToString()).ToString("dd/MM/yyy");
                // Verificar se já foi lançado
                clsMovPecasInfo = clsMovPecasBLL.Carregar(clsParser.Int32Parse(row["IDNFVDOC"].ToString()), clsParser.Int32Parse(row["IDNFV"].ToString()), clsParser.Int32Parse(row["IDNFV1"].ToString()), 0, clsInfo.conexaosqldados);
                if (row["TIPOENTRADA"].ToString().PadRight(1, ' ').Substring(0, 1) != "C")
                {
                    tipoentrada = row["TIPOENTRADA"].ToString().PadRight(1, ' ').Substring(0, 1);
                }
                else
                {
                     tipoentrada = "C";
                }
                if (clsMovPecasInfo.id == 0)
                {  // Incluir
                    clsMovPecasBLL.MovimentaItem(idCodigo,
                              tipoentrada,
                              clsParser.DecimalParse(row["QTDE"].ToString()),
                              DateTime.Parse(datarecebimento),
                              clsParser.Int32Parse(row["IDNFVDOC"].ToString()),
                              clsParser.Int32Parse(row["IDNFV"].ToString()),
                              clsParser.Int32Parse(row["IDNFV1"].ToString()),
                              0,
                              0, //Parser.DecimalParse(row["PRECO"].ToString()),
                              clsInfo.zusuario,
                              clsInfo.conexaosqldados);
                }
                else
                {  // Alterar
                    clsMovPecasBLL.MovimentaItem(idCodigo, tipoentrada,
                        clsParser.DecimalParse(row["QTDE"].ToString()), DateTime.Parse(datarecebimento),
                          clsParser.Int32Parse(row["IDNFVDOC"].ToString()),
                          clsParser.Int32Parse(row["IDNFV"].ToString()),
                          clsParser.Int32Parse(row["IDNFV1"].ToString()),
                          0,0, clsInfo.zusuario, clsInfo.conexaosqldados);
                }
                if (row["TIPOENTRADA"].ToString().PadRight(2).Substring(0, 1) == "C")
                {
                    
                }
            }
        }
        private void AcumulaOS(Int32 idCodigo, String DaData, String AteData)
        {
            if (idCodigo == 754 || idCodigo == 757)
            {
                //  MessageBox.Show("pare");
            }

            // 
            // OSFOR  - ordem de serviço de fornecimento
            Int32 idtipodocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL WHERE COGNOME='" + "OSFOR" + "' "));

            query = "SELECT ORDEMSERVICOFORNECE.ID AS IDOSFORNECE, ORDEMSERVICO.ID AS IDOS, " +
                    "ORDEMSERVICOFORNECE.QUANTIDADE, ORDEMSERVICO.DATAENTREGA, ORDEMSERVICOFORNECE.VALORUNITARIO " +
                    "from ORDEMSERVICOFORNECE " +
                    "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID=ORDEMSERVICOFORNECE.IDORDEMSERVICO " +
                    "WHERE ORDEMSERVICOFORNECE.IDCODIGO = @IDCODIGO " +
                    " AND ORDEMSERVICO.DATA >= " + clsParser.SqlDateTimeFormat(DaData + " 00:00", true) +
                    " AND ORDEMSERVICO.DATA <= " + clsParser.SqlDateTimeFormat(AteData + " 23:59", true) +
                    " ORDER BY ORDEMSERVICO.DATA, ORDEMSERVICO.ID ";

            SqlDataAdapter sda;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idCodigo;


            DataTable dtOrdemServicoFornece = new DataTable();
            sda.Fill(dtOrdemServicoFornece);
            foreach (DataRow row in dtOrdemServicoFornece.Rows)
            {
                if (clsParser.Int32Parse(row["IDOS"].ToString()) == 1017)
                {
                    //MessageBox.Show("pare");
                }
                clsMovPecasInfo = new clsMovPecasInfo();
                clsMovPecasInfo = clsMovPecasBLL.Carregar(idtipodocumento, clsParser.Int32Parse(row["IDOS"].ToString()), clsParser.Int32Parse(row["IDOSFORNECE"].ToString()), 0, clsInfo.conexaosqldados);

                if (clsMovPecasInfo.id == 0)
                {
                    // Incluir Movimento no Estoque
                    clsMovPecasBLL.MovimentaItem(idCodigo, "S", clsParser.DecimalParse(row["quantidade"].ToString()), clsParser.DateTimeParse(row["DATAENTREGA"].ToString()),
                                                        idtipodocumento, clsParser.Int32Parse(row["IDOS"].ToString()), clsParser.Int32Parse(row["IDOSFORNECE"].ToString()),0, clsParser.DecimalParse(row["VALORUNITARIO"].ToString()),
                                                        clsInfo.zusuario, clsInfo.conexaosqldados);
                }
                else
                {
                    // Altera Movimento no Estoque
                    clsMovPecasBLL.MovimentaItem(clsParser.DecimalParse(row["quantidade"].ToString()), "S", idtipodocumento, clsParser.Int32Parse(row["IDOS"].ToString()), clsParser.Int32Parse(row["IDOSFORNECE"].ToString()), 0, clsParser.DecimalParse(row["VALORUNITARIO"].ToString()), idCodigo, clsParser.DateTimeParse(row["DATAENTREGA"].ToString()), clsInfo.conexaosqldados);
                }
            }

            // OSPRO  - ordem de serviço de producao
            idtipodocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL WHERE COGNOME='" + "OSPRO" + "' "));

            query = "SELECT ORDEMSERVICOPRODUZ.ID AS IDOSPRODUZ, ORDEMSERVICO.ID AS IDOS, " +
                    "ORDEMSERVICOPRODUZ.QUANTIDADE, ORDEMSERVICO.DATAENTREGA, ORDEMSERVICOPRODUZ.VALORUNITARIO " +
                    "from ORDEMSERVICOPRODUZ " +
                    "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID=ORDEMSERVICOPRODUZ.IDORDEMSERVICO " +
                    "WHERE ORDEMSERVICOPRODUZ.IDCODIGO = @IDCODIGO " +
                    " AND ORDEMSERVICO.DATA >= " + clsParser.SqlDateTimeFormat(DaData + " 00:00", true) +
                    " AND ORDEMSERVICO.DATA <= " + clsParser.SqlDateTimeFormat(AteData + " 23:59", true) +
                    " ORDER BY ORDEMSERVICO.DATA, ORDEMSERVICO.ID ";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idCodigo;


            DataTable dtOrdemServicoProduz = new DataTable();
            sda.Fill(dtOrdemServicoProduz);
            foreach (DataRow row in dtOrdemServicoProduz.Rows)
            {
                if (clsParser.Int32Parse(row["IDOS"].ToString()) == 1017)
                {
                    //MessageBox.Show("pare");
                }
                clsMovPecasInfo = new clsMovPecasInfo();
                clsMovPecasInfo = clsMovPecasBLL.Carregar(idtipodocumento, clsParser.Int32Parse(row["IDOS"].ToString()), clsParser.Int32Parse(row["IDOSPRODUZ"].ToString()), 0, clsInfo.conexaosqldados);

                if (clsMovPecasInfo.id == 0)
                {
                    // Incluir Movimento no Estoque
                    clsMovPecasBLL.MovimentaItem(idCodigo, "E", clsParser.DecimalParse(row["quantidade"].ToString()), clsParser.DateTimeParse(row["DATAENTREGA"].ToString()),
                                    idtipodocumento, clsParser.Int32Parse(row["IDOS"].ToString()), clsParser.Int32Parse(row["IDOSPRODUZ"].ToString()), 0, clsParser.DecimalParse(row["VALORUNITARIO"].ToString()),
                                    clsInfo.zusuario, clsInfo.conexaosqldados);
                }
                else
                {
                    // Altera Movimento no Estoque
                    clsMovPecasBLL.MovimentaItem(clsParser.DecimalParse(row["quantidade"].ToString()), "E", idtipodocumento, clsParser.Int32Parse(row["IDOS"].ToString()), clsParser.Int32Parse(row["IDOSPRODUZ"].ToString()), 0, clsParser.DecimalParse(row["VALORUNITARIO"].ToString()), idCodigo, clsParser.DateTimeParse(row["DATAENTREGA"].ToString()), clsInfo.conexaosqldados);
                }
            }
        }
        private void AcumulaPED(Int32 idCodigo, String DaData, String AteData)
        {
            Int32 IdDocumento = clsInfo.zdocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + "PED" + "' "));
            if (idCodigo == 754 || idCodigo == 757)
            {
                //  MessageBox.Show("pare");
            }
            query = "Select PEDIDO.ID AS [IDPED], PEDIDO1.ID AS [IDPED1], " +
                    "PEDIDO.NUMERO, PEDIDO.DATA, " +
                    "PEDIDO1.IDCODIGO, PEDIDO1.QTDE, PEDIDO1.PRECO " +
                    "from PEDIDO1 " +
                    "LEFT JOIN PEDIDO ON PEDIDO.ID=PEDIDO1.IDPEDIDO " +
                    "WHERE PEDIDO1.IDCODIGO = @IDCODIGO " +
                    " AND PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(DaData + " 00:00", true) +
                    " AND PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(AteData + " 23:59", true) +
//                    " AND PEDIDO.FILIAL = @FILIAL " +
                    " ORDER BY PEDIDO.DATA, PEDIDO1.ID ";
            SqlDataAdapter sda;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idCodigo;
            //sda.SelectCommand.Parameters.Add("@FILIAL", SqlDbType.Int).Value = 1;

            DataTable dtPEDIDO = new DataTable();
            sda.Fill(dtPEDIDO);
            foreach (DataRow row in dtPEDIDO.Rows)
            {
                String tipoentrada = "";
                String datarecebimento = DateTime.Parse(row["DATA"].ToString()).ToString("dd/MM/yyy");
                // Verificar se já foi lançado
                clsMovPecasInfo = clsMovPecasBLL.Carregar(IdDocumento, clsParser.Int32Parse(row["IDPED"].ToString()), clsParser.Int32Parse(row["IDPED1"].ToString()), 0, clsInfo.conexaosqldados);

                if (clsMovPecasInfo.id == 0)
                {  // Incluir
                    clsMovPecasBLL.MovimentaItem(idCodigo,"S",
                              clsParser.DecimalParse(row["QTDE"].ToString()),
                              DateTime.Parse(datarecebimento),
                              clsParser.Int32Parse(IdDocumento.ToString()),
                              clsParser.Int32Parse(row["IDPED"].ToString()),
                              clsParser.Int32Parse(row["IDPED1"].ToString()),
                              0,
                              0, //Parser.DecimalParse(row["PRECO"].ToString()),
                              clsInfo.zusuario,
                              clsInfo.conexaosqldados);
                }
                else
                {  // Alterar
                    clsMovPecasBLL.MovimentaItem(idCodigo, "S",
                        clsParser.DecimalParse(row["QTDE"].ToString()), DateTime.Parse(datarecebimento),
                          clsParser.Int32Parse(IdDocumento.ToString()),
                          clsParser.Int32Parse(row["IDPED"].ToString()),
                          clsParser.Int32Parse(row["IDPED1"].ToString()),
                          0, 0, clsInfo.zusuario, clsInfo.conexaosqldados);
                }
            }
        }

    }
}
