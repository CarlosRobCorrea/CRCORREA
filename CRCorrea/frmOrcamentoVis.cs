using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CRCorrea;
using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using CRCorreaBLL;

namespace CRCorrea
{
    public partial class frmOrcamentoVis : Form
    {
        clsOrcamentoBLL clsOrcamentoBLL;

        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        DataTable dtOrcamento;

        //clsBasicReport clsBr;
        
        String filtro_status;
        DateTime filtro_datade;
        DateTime filtro_dataate;

        BackgroundWorker bwrOrcamento;
        Boolean carregando;       
        Control ultimoObjetodeFiltroFocado;

        ParameterFields pfields = new ParameterFields();
        String cognome;

        String tipoproduto = "";
        String GrupoUsuario;

        public frmOrcamentoVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            // E = Envidraçamento
            // T = Todos os produtos
            //tipoproduto = _tipoproduto;
            bwrOrcamento = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrOrcamento.WorkerSupportsCancellation = true;
            bwrOrcamento.DoWork += new DoWorkEventHandler(bwrOrcamento_DoWork);
            bwrOrcamento.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrOrcamento_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
            carregando = false;
            clsOrcamentoBLL = new clsOrcamentoBLL();
            //clsBr = new clsBasicReport(this, dgvOrcamento, toolTip1);
        }

        private void frmOrcamentoVis_Load(object sender, EventArgs e)
        {
            rbnStatusA.Checked = true;
            tbxPeriodoDe.Text = DateTime.Now.AddDays(- DateTime.Now.Day + 1).AddMonths(-6).ToString("dd/MM/yyyy");
            tbxPeriodoAte.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            //tbxPeriodoAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
            labelTipoProduto.Text = "Orçamentos";
            
            GrupoUsuario = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select GRUPO from USUARIO where id = " + clsInfo.zusuarioid, "");
        }

        private void frmOrcamentoVis_Activated(object sender, EventArgs e)
        {
            bwrOrcamento_Run();
        }

        private void frmOrcamentoVis_Shown(object sender, EventArgs e)
        {
            
        }

        private void bwrOrcamento_Run()
        {
            if (carregando == false)
            {
                pbxOrcamento.Visible = true;
                carregando = true;
                FillFiltros(); //preenchemos a variavel de filtro
                DesabilitaFiltros();//desabilitamos as opções de filtro ate o BackgroundWorker ser completado
                bwrOrcamento.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);

            if (sender is TextBox)
            {
                bwrOrcamento_Run();
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

        private void FillFiltros()
        {
            if (rbnStatusT.Checked == true) filtro_status = "T";
            else if (rbnStatusA.Checked == true) filtro_status = "A";
            else if (rbnStatusF.Checked == true) filtro_status = "F";
            else if (rbnStatusC.Checked == true) filtro_status = "C";

            filtro_datade = clsParser.DateTimeParse(tbxPeriodoDe.Text);
            filtro_dataate = clsParser.DateTimeParse(tbxPeriodoAte.Text);
        }

        private void bwrOrcamento_DoWork(object sender, DoWorkEventArgs e)
        {
            OrcamentoGrid();
            if (bwrOrcamento.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void OrcamentoGrid()
        {
            String Usuario = "";
            if (GrupoUsuario == "V")
            {
                Usuario = clsInfo.zusuario;
            }

            if (rbnStatusT.Checked == true)
            {
                filtro_status = "T";
            }
            else if (rbnStatusA.Checked == true)
            {
                filtro_status = "A";
            }
            else if (rbnStatusF.Checked == true)
            {
                filtro_status = "F";
            }
            Int32 filial;
            if (rbnFilial00.Checked == true)
            {
                filial = 0;
            }
            else if (rbnFilial01.Checked == true)
            {
                filial = 1;
            }
            else 
            {
                filial = 2;
            }

           dtOrcamento = clsOrcamentoBLL.GridDados(filial, filtro_datade, filtro_dataate, filtro_status);
        }

        private void bwrOrcamento_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                    //logo podemos trabalhar com os objetos da tela
                    //clsOrcamentoBLL.GridMonta(dgvOrcamento, dtOrcamento);
                   // lblRegistros.Text = "Nº Registros: " + dgvOrcamento.Rows.GetRowCount(DataGridViewElementStates.Visible);

                    pbxOrcamento.Visible = false;
                    HabilitaFiltros(); //como o BackgroundWorker se completou habilitamos as oções de filtro
                    PesquisaRapida(); // a pesquisarapida que controla a pos~ição do ponteiro do grid
                    
                    //////////////////////////
                    //ArrayList altOrcamento = clsBr.GetColunas();

                    clsOrcamentoBLL.GridMonta(dgvOrcamento, dtOrcamento, id);

//                    clsBr.RecalculaGrid(altOrcamento);

                    dgvOrcamento.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvOrcamento.Columns["TOTALORCAMENTOBRUTO"].DefaultCellStyle.Format = "N2";
                    //                    dgvOrcamento.Columns["TOTALCONFIRMADO"].DefaultCellStyle.Format = "N2";

                    // Fazer um loop do dtorcamento que foi filtrado
                    // somar os totais
                    // Totais
                    //carlos em 10/07/2012
                    //query = "SELECT SUM(ORCAMENTO.TOTALCONFIRMADO) FROM ORCAMENTO " +
                    //        "WHERE ORCAMENTO.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                    //        "AND ORCAMENTO.DATA >= " + clsParser.SqlDateTimeFormat(tbxPeriodoDe.Text + " 00:00", true) + " " +
                    //        "AND ORCAMENTO.DATA <= " + clsParser.SqlDateTimeFormat(tbxPeriodoAte.Text + " 23:59", true);

                    tbxTotalOrcado.Text = clsGridHelper.SomaGrid(dgvOrcamento, "TOTALORCAMENTOBRUTO").ToString("N2");

                    tbxTotalConfirmado.Text = "0";

                    foreach (DataRow linha in dtOrcamento.Rows)
                    {
                        

                        //tbxTotalConfirmado.Text = (clsParser.DecimalParse(tbxTotalConfirmado.Text) + clsParser.DecimalParse(linha["TOTALCONFIRMADO"].ToString())).ToString("N2");
                        //tbxTotalOrcado.Text = (clsParser.DecimalParse(tbxTotalOrcado.Text) + clsParser.DecimalParse(linha["TOTALORCAMENTOLIQUIDO"].ToString())).ToString("N2");
                    }
                    //tbxTotalConfirmado.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, query)).ToString("N2");

                    //query = "SELECT SUM(ORCAMENTO.TOTALORCAMENTOLIQUIDO) FROM ORCAMENTO " +
                    //        "WHERE ORCAMENTO.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " " +
                    //        "AND ORCAMENTO.DATA >= " + clsParser.SqlDateTimeFormat(tbxPeriodoDe.Text + " 00:00", true) + " " +
                    //        "AND ORCAMENTO.DATA <= " + clsParser.SqlDateTimeFormat(tbxPeriodoAte.Text + " 23:59", true);
                    //tbxTotalOrcado.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, query)).ToString("N2");
                   
                    //dgvOrcamento.Sort(dgvOrcamento.Columns["NUMERO"], ListSortDirection.Ascending);

                    clsGridHelper.FontGrid(dgvOrcamento, 7);

                    clsGridHelper.SelecionaLinha(id, dgvOrcamento, "NUMERO");

                    //////////////////////////

                    //coloca o foco.  Deve ser o ultimo procedimento de foco para um objeto
                    //dentro do RunWorkerCompleted
                   
                    //if (ultimoObjetodeFiltroFocado != null)
                    //{
                    //    ultimoObjetodeFiltroFocado.Select();
                    //}

                }
                //carregarCliente  so recebe false agora porque o BackgroundWorker ja 
                //se completou assim ele fica liberado para nova execução
                //Nota no caso de haver pedido de cancelamento o BackgroundWorker não executa o codigo acima 
                //Nota geralmente nos nossos formularios o pedido de cancelamento veem junto com a saida do formulario
                //assim o sistema fecha e não teremos problema de BackgroundWorker em segundo plano chamando os 
                //objetos do form
                carregando = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                carregando = false;
            }           
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                //clsOrcamentoMaterialBLL OrcamentoMaterial = new clsOrcamentoMaterialBLL();
                if (dgvOrcamento.CurrentRow != null)
                {
                    // Verificar se o orçamento não foi confirmado?
                    if (clsParser.DecimalParse(dgvOrcamento.CurrentRow.Cells["TOTALCONFIRMADO"].Value.ToString()) > 0)
                    {
                        MessageBox.Show("Orçamento confirmado não pode ser apagado !!");
                    }
                    else
                    {                           
                        DialogResult drt;
                        drt = MessageBox.Show("Deseja realmente Excluir o Orçamento:" + Environment.NewLine +
                                                dgvOrcamento.CurrentRow.Cells["NUMERO"].Value.ToString() + " - " +
                                                clsParser.DateTimeParse(dgvOrcamento.CurrentRow.Cells["DATA"].Value.ToString()).ToString("dd/MM/yyyy") + " - " +
                                                dgvOrcamento.CurrentRow.Cells["CLIENTE"].Value.ToString() + " ?",
                                                Application.CompanyName,
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
                        if (drt == DialogResult.Yes)
                        {
                            //Rotina de Exclusão
                            //Selecionando os itens do orçamento
                            DataTable dtOrcItens = new DataTable();
                            SqlDataAdapter sda = new SqlDataAdapter("select id, idorcamento from orcamento1 where idorcamento = " 
                                + clsParser.Int32Parse(
                                dgvOrcamento.CurrentRow.Cells["ID"].Value.ToString()), clsInfo.conexaosqldados);

                            sda.Fill(dtOrcItens);

                            //Excluindo Orcamento Material
                            for (Int32  i = 0; i < dtOrcItens.Rows.Count; i++)
                            {
                                //OrcamentoMaterial.Excluir(clsParser.Int32Parse(dtOrcItens.Rows[i]["id"].ToString()),clsInfo.conexaosqldados);
                            }

                            
                            //Ecluindo os itens do orcamento Formato da Linguagem C#
                            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                            SqlCommand scd = new SqlCommand("delete from orcamento1 where idorcamento = "+
                                clsParser.Int32Parse(dgvOrcamento.CurrentRow.Cells["ID"].Value.ToString()), scn);

                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();

                            //Ecluindo Orçamento Cond. Pagto Formato da Linguagem C#
                            SqlConnection scn1 = new SqlConnection(clsInfo.conexaosqldados);
                            SqlCommand scd1 = new SqlCommand("delete from orcamentocondpagto where idorcamento = " +
                                clsParser.Int32Parse(dgvOrcamento.CurrentRow.Cells["ID"].Value.ToString()), scn1);

                            scn1.Open();
                            scd1.ExecuteNonQuery();
                            scn1.Close();

                            //Ecluindo Orçamento Cond. Pagto Formato da Linguagem C#
                            SqlConnection scn2 = new SqlConnection(clsInfo.conexaosqldados);
                            SqlCommand scd2 = new SqlCommand("delete from orcamentoreceber where idorcamento = " +
                                clsParser.Int32Parse(dgvOrcamento.CurrentRow.Cells["ID"].Value.ToString()), scn2);

                            scn2.Open();
                            scd2.ExecuteNonQuery();
                            scn2.Close();


                            //Excluindo o Orçamento
                            //clsOrcamentoBLL.Excluir(clsParser.Int32Parse(dgvOrcamento.CurrentRow.Cells["ID"].Value.ToString()),clsInfo.conexaosqldados);
                            
                            bwrOrcamento_Run();
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {   
            id = 0;
            frmOrcamento frmOrcamento = new frmOrcamento();
            frmOrcamento.Init(id, dgvOrcamento.Rows);
            clsFormHelper.AbrirForm(this.MdiParent, frmOrcamento, clsInfo.conexaosqldados);
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //clsBr.TravaGrid();
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvOrcamento }, true);
            //frmProcurar.ShowDialog();
            //clsBr.LiberaGrid();
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvOrcamento.CurrentRow != null)
            {
                id = (Int32)(dgvOrcamento.CurrentRow.Cells[0].Value);

                frmOrcamento frmOrcamento = new frmOrcamento();
                frmOrcamento.Init(id, dgvOrcamento.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmOrcamento, clsInfo.conexaosqldados);
            }
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvOrcamento.CurrentRow;
            this.Close();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //clsBr.Imprimir(clsInfo.caminhorelatorios, "Orcamento", clsInfo.conexaosqldados);
        }

        private void DesabilitaFiltros()
        {
            gbxStatus.Enabled = false;
            gbxPeriodo.Enabled = false;
        }

        private void HabilitaFiltros()
        {
            gbxStatus.Enabled = true;
            gbxPeriodo.Enabled = true;
        }



        private bool FiltroMudancas()
        {
            if (filtro_status == "T" && rbnStatusT.Checked == false)
            {
                return true;
            }
            else if (filtro_status == "A" && rbnStatusA.Checked == false)
            {
                return true;
            }
            else if (filtro_status == "F" && rbnStatusF.Checked == false)
            {
                return true;
            }
            else if (filtro_status == "C" && rbnStatusC.Checked == false)
            {
                return true;
            }

            if (filtro_datade.ToString("dd/MM/yy") != clsParser.DateTimeParse(tbxPeriodoDe.Text).ToString("dd/MM/yy"))
            {
                return true;
            }
            else if (filtro_dataate.ToString("dd/MM/yy") != clsParser.DateTimeParse(tbxPeriodoAte.Text).ToString("dd/MM/yy"))
            {
                return true;
            }

            return false;
        }

        private void StatusCheckedChanged(object sender, EventArgs e)
        {
            ultimoObjetodeFiltroFocado = (Control)sender;

            if (FiltroMudancas() != false)
            {
                DesabilitaFiltros();
                bwrOrcamento_Run();
            }

        }

        private void tspOrcamento_Imprimir1_Click(object sender, EventArgs e)
        {
            //try
            //{
            ////    DataTable Rel = new DataTable();
            //    if (dgvOrcamento.CurrentRow != null)
            //    {
            //        Int32 idorcamento;
            //        idorcamento = clsParser.Int32Parse(dgvOrcamento.CurrentRow.Cells["id"].Value.ToString());
                                       

            //        String query;
            //        SqlDataAdapter sda;
            //        Aplisoft.DataSets.DADOS.Espaco.Orcamento.dOrcamento dsOrcamento = new Aplisoft.DataSets.DADOS.Espaco.Orcamento.dOrcamento();

            //        query = "SELECT  " +
            //                "ORCAMENTO.NUMERO, " +
            //                "ORCAMENTO.DATA, " +
            //                "ORCAMENTO.SITUACAO, " +
            //                "ORCAMENTO.ATENCAO,  " +
            //                "CLIENTE.NOME, " +
            //                "CLIENTE.DDD, " +
            //                "CLIENTE.TELEFONE, " +
            //                "ORCAMENTO.EMAIL, " +
            //                "VENDEDOR.COGNOME AS VENDEDOR_COGNOME, " +
            //                "VENDEDOR.DDD AS VENDEDOR_DDD, " +
            //                "VENDEDOR.TELEFONE AS VENDEDOR_TELEFONE, " +
            //                "VENDEDOR.EMAIL AS VENDEDOR_EMAIL, " +
            //                "ORCAMENTO.OBSERVA, " +
            //                "(SELECT TOP 1 COR FROM ORCAMENTO1 WHERE IDORCAMENTO = ORCAMENTO.ID) AS ORCAMENTO1_COR, " +
            //                "(SELECT TOP 1 VIDROESPESSURA FROM ORCAMENTO1 WHERE IDORCAMENTO = ORCAMENTO.ID) AS ORCAMENTO1_VIDROESPESSURA, " +
            //                "EDIFICIO.NUMERO	AS EDIFICIO_NUMERO, " +
            //                "EDIFICIO.NOME	AS EDIFICIO_NOME, " +
            //                "EDIFICIO.CEP	AS EDIFICIO_CEP, " +
            //                "EDIFICIO.ENDTIPO	AS EDIFICIO_ENDTIPO, " +
            //                "EDIFICIO.ENDERECO	AS EDIFICIO_ENDERECO, " +
            //                "EDIFICIO.TIPONUMERO	AS EDIFICIO_TIPONUMERO, " +
            //                "EDIFICIO.NUMEROEND	AS EDIFICIO_NUMEROEND, " +
            //                "EDIFICIO.ANDAR	AS EDIFICIO_ANDAR, " +
            //                "EDIFICIO.TIPOCOMPLE	AS EDIFICIO_TIPOCOMPLE, " +
            //                "EDIFICIO.COMPLE	AS EDIFICIO_COMPLE, " +
            //                "EDIFICIO.BAIRRO	AS EDIFICIO_BAIRRO, " +
            //                "EDIFICIO.CIDADE	AS EDIFICIO_CIDADE, " +
            //                "ESTADOS_EDIFICIO.ESTADO AS EDIFICIO_ESTADO, " +
            //                "EMPRESASGERE.REPRESENTANTELEGAL, " +
            //                "EMPRESASGERE.CARGOREPRESENTANTE, " +
            //                "EMPRESAS.CIDADE AS EMPRESA_CIDADE, " +
            //                "EMPRESAS.TELEFONE AS EMPRESA_TELEFONE, " +
            //                "EMPRESAS.DDD AS EMPRESA_DDD, " +
            //                "EMPRESAS.HOMEPAGE AS EMPRESA_HOMEPAGE, " +
            //                "VENDEDOR.NOME AS VENDEDOR_NOME, " +
            //                "EMPRESAS.ENDTIPO AS EMPRESA_ENDTIPO, " +
            //                "EMPRESAS.ENDERECO AS EMPRESA_ENDERECO, " +
            //                "EMPRESAS.TIPONUMERO AS EMPRESA_TIPONUMERO, " +
            //                "EMPRESAS.NUMEROEND AS EMPRESA_NUMEROEND, " +
            //                "EMPRESAS.BAIRRO AS EMPRESA_BAIRRO, " +
            //                "ESTADOS_EMPRESA.ESTADO AS EMPRESA_UF, " +
            //                "EMPRESAS.CEP AS EMPRESA_CEP, " +
            //                "EMPRESAS.DDD2 AS EMPRESA_DDD2, " +
            //                "EMPRESAS.TELEFONE2 AS EMPRESA_TELEFONE2, " +
            //                "CLIENTE.CGC AS CLIENTE_CGC " +
            //                "FROM " +
            //                "ORCAMENTO  " +
            //                "INNER JOIN EDIFICIOAPARTAMENTO ON EDIFICIOAPARTAMENTO.ID = ORCAMENTO.IDEDIFICIOAPARTAMENTO " +
            //                "INNER JOIN EDIFICIO ON EDIFICIO.ID = EDIFICIOAPARTAMENTO.IDEDIFICIO  " +
            //                "INNER JOIN CLIENTEPROSPECCAO AS CLIENTE ON CLIENTE.ID = ORCAMENTO.IDCLIENTE  " +
            //                "INNER JOIN CLIENTEPROSPECCAO AS VENDEDOR ON VENDEDOR.ID = ORCAMENTO.IDVENDEDOR  " +
            //                "INNER JOIN ESTADOS AS ESTADOS_EDIFICIO ON ESTADOS_EDIFICIO.ID = EDIFICIO.IDESTADO  " +
            //                "INNER JOIN ORCAMENTO1 ON ORCAMENTO1.IDORCAMENTO = ORCAMENTO.ID " +
            //                "INNER JOIN EMPRESASGERE ON EMPRESASGERE.EMPRESA = ORCAMENTO.FILIAL  " +
            //                "INNER JOIN EMPRESAS ON EMPRESAS.CODIGO = ORCAMENTO.FILIAL " +
            //                "INNER JOIN ESTADOS AS ESTADOS_EMPRESA ON ESTADOS_EMPRESA.ID = EMPRESAS.IDESTADO  " +
            //                "where " +
            //                    "orcamento.id = @idorcamento ";

            //        sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            //        sda.SelectCommand.Parameters.Add("@idorcamento", SqlDbType.Int).Value = idorcamento;
            //        sda.Fill(dsOrcamento, "dtOrcamento");

            //        query = "SELECT " +
            //                    "orcamento1.id, " +
            //                    "idorcamento, " +
            //                    "local," +
            //                    "ORCAMENTO1.TIPOVIDRO AS ORCAMENTO1_TIPOVIDRO, " +
            //                    " ORCAMENTO1.COR as ORCAMENTO1_COR, " +
            //                    " ORCAMENTO1.VIDROCOR as ORCAMENTO1_VIDROCOR, " +
            //                    " ORCAMENTO1.VIDROESPESSURA as ORCAMENTO1_VIDROESPESSURA, " +
            //                    " ORCAMENTO1.ITEM as ORCAMENTO1_ITEM, " +
            //                    " ORCAMENTO1.QTDE as ORCAMENTO1_QTDE, " +
            //                    " ORCAMENTO1.LINEAR as ORCAMENTO1_LINEAR, " +
            //                    " ORCAMENTO1.ALTURA as ORCAMENTO1_ALTURA, " +
            //                    " PECAS.NOME as PECAS_NOME, " +
            //                    " ORCAMENTO1.PRECOBRUTO, " +
            //                    " ORCAMENTO1.COMPLEMENTO " +

            //                "from  " +
            //                    "ORCAMENTO1 INNER JOIN PECAS ON PECAS.ID = ORCAMENTO1.IDCODIGO " +
                            
            //                "where " +
            //                    "idorcamento = @idorcamento " +
            //                " order by local ";

            //        sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            //        sda.SelectCommand.Parameters.Add("@idorcamento", SqlDbType.Int).Value = idorcamento;
            //        sda.Fill(dsOrcamento, "dtOrcamento1");

            //        //query = "SELECT " +
            //        //            "ORCAMENTOCONDPAGTO.id, " +
            //        //            "ORCAMENTOCONDPAGTO.idorcamento, " +
            //        //            "CONDPAGTO.NOME, " +
            //        //            "(SELECT COUNT(*) FROM ORCAMENTORECEBER WHERE IDORCAMENTOCONDPAGTO = ORCAMENTOCONDPAGTO.id) AS PARCELAS, " +
            //        //            "(SELECT SUM(VALOR) FROM ORCAMENTORECEBER WHERE IDORCAMENTOCONDPAGTO = ORCAMENTOCONDPAGTO.id) AS TOTAL, " +
            //        //            "(SELECT TOP 1 VALOR FROM ORCAMENTORECEBER WHERE IDORCAMENTOCONDPAGTO = ORCAMENTOCONDPAGTO.id) AS PARCELAS_TOTAL " +
            //        //        "from  " +
            //        //            "ORCAMENTOCONDPAGTO " +
            //        //                "INNER JOIN CONDPAGTO ON CONDPAGTO.ID = ORCAMENTOCONDPAGTO.IDCONDPAGTO " +
            //        //        "where " +
            //        //            "ORCAMENTOCONDPAGTO.idorcamento = @idorcamento " +
            //        //        " order by CONDPAGTO.NOME ";
            //        query = "SELECT " +
            //        "ORCAMENTOCONDPAGTO.id, " +
            //        "ORCAMENTOCONDPAGTO.idorcamento, " +
            //        "CONDPAGTO.NOME, " +
            //        "ORCAMENTO.IDFORMAPAGTO, " +
            //        "SITUACAOTIPOTITULO.NOME AS FORMAPAGTO, " +
            //        "(SELECT COUNT(*) FROM ORCAMENTORECEBER WHERE IDORCAMENTOCONDPAGTO = ORCAMENTOCONDPAGTO.id) AS PARCELAS, " +
            //        "(SELECT SUM(VALOR) FROM ORCAMENTORECEBER WHERE IDORCAMENTOCONDPAGTO = ORCAMENTOCONDPAGTO.id) AS TOTAL, " +
            //        "(SELECT TOP 1 VALOR FROM ORCAMENTORECEBER WHERE IDORCAMENTOCONDPAGTO = ORCAMENTOCONDPAGTO.id) AS PARCELAS_TOTAL " +
            //        "from " +
            //        "ORCAMENTOCONDPAGTO " +
            //        "INNER JOIN CONDPAGTO ON CONDPAGTO.ID = ORCAMENTOCONDPAGTO.IDCONDPAGTO " +
            //        "INNER JOIN ORCAMENTO ON ORCAMENTO.ID = ORCAMENTOCONDPAGTO.IDORCAMENTO " +
            //        "INNER JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = ORCAMENTO.IDFORMAPAGTO "+
            //        "where " +
            //        "ORCAMENTOCONDPAGTO.idorcamento = @idorcamento " +
            //        " order by CONDPAGTO.NOME ";

            //        sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            //        sda.SelectCommand.Parameters.Add("@idorcamento", SqlDbType.Int).Value = idorcamento;
            //        sda.Fill(dsOrcamento, "dtOrcamentoCondPagto");

            //        String emaildestinatario = "";
            //        emaildestinatario = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select clienteprospeccao.email from clienteprospeccao inner join orcamento on orcamento.idcliente = clienteprospeccao.id where orcamento.id = " + idorcamento);
                     
            //        if (emaildestinatario == null || emaildestinatario.ToString().Trim().Length <= 0)
            //        {
            //            emaildestinatario = "";
            //        }
            //        pfields.Clear();
            //        //DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            //        DataTable dtCarregarCampos = new DataTable();
            //        dtCarregarCampos = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
            //            "Select * from EMPRESAS WHERE ID=" + clsInfo.zempresaid.ToString() + " Order by ID ");

            //        ParameterField pfieldEmpresa = new ParameterField();
            //        ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            //        if (dtCarregarCampos.Rows.Count > 0)
            //        {
            //            disvalEmpresa.Value = dtCarregarCampos.Rows[0]["NOME"].ToString();
            //            pfieldEmpresa.Name = "EMPRESA";
            //            pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            //        }
            //        pfields.Add(pfieldEmpresa);

            //        if (clsInfo.zempresacliente_cognome.IndexOf("CORTINA") != -1)
            //        {
            //            cognome = "CORTINA";
            //        }
            //        else
            //        {
            //            cognome = clsInfo.zempresacliente_cognome;
            //        }
            //        DialogResult resultado;                   
            //        resultado = MessageBox.Show("Deseja enviar por E-mail?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                   
                    
            //        //PAPEL COMUM
            //        if (resultado == DialogResult.Yes)
            //        {
            //            frmCrystalReport frmCrystalReport = new frmCrystalReport();
            //            frmCrystalReport.Init(clsInfo.caminhorelatorios,
            //                "DADOS_ORCAMENTO_CLIENTE_LOGO"+"_"+cognome+".RPT",
            //                dsOrcamento,
            //                pfields,
            //                emaildestinatario,
            //                "Orcamento n." + dgvOrcamento.CurrentRow.Cells[3].Value.ToString() + "_" + clsParser.SqlDateTimeParse(dgvOrcamento.CurrentRow.Cells["DATA"].Value.ToString()).Value.ToString("yyyy"),
            //                "Orcamento n." + dgvOrcamento.CurrentRow.Cells[3].Value.ToString() + "_" + clsParser.SqlDateTimeParse(dgvOrcamento.CurrentRow.Cells["DATA"].Value.ToString()).Value.ToString("yyyy"),
            //                clsInfo.conexaosqldados);
            //            if(System.IO.File.Exists(clsInfo.caminhorelatorios+"DADOS_ORCAMENTO_CLIENTE_LOGO"+"_"+cognome+".RPT"))
            //            {
            //                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            //            }
            //            else
            //            {
            //                MessageBox.Show("Em Desenvolvimento","Aplisoft");
            //            }

            //        }

            //        //PAPEL TIMBRADO
            //        else
            //        {
            //            frmCrystalReport frmCrystalReport = new frmCrystalReport();
            //                frmCrystalReport.Init(clsInfo.caminhorelatorios,
            //                    "DADOS_ORCAMENTO_CLIENTE"+"_"+cognome+".RPT",
            //                    dsOrcamento,
            //                    pfields,
            //                    emaildestinatario,
            //                    "Orcamento n." + dgvOrcamento.CurrentRow.Cells[3].Value.ToString() + "_" + clsParser.SqlDateTimeParse(dgvOrcamento.CurrentRow.Cells["DATA"].Value.ToString()).Value.ToString("yyyy"),
            //                    "Orcamento n." + dgvOrcamento.CurrentRow.Cells[3].Value.ToString() + "_" + clsParser.SqlDateTimeParse(dgvOrcamento.CurrentRow.Cells["DATA"].Value.ToString()).Value.ToString("yyyy"),
            //                    clsInfo.conexaosqldados);
            //                if (System.IO.File.Exists(clsInfo.caminhorelatorios + "DADOS_ORCAMENTO_CLIENTE" + "_" + cognome + ".RPT"))
            //                {
            //                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Em Desenvolvimento", "Aplisoft");
            //                    //frmCrystalReport.Init(clsInfo.caminhorelatorios,
            //                    //"APLIESQUADRIAS_ORCAMENTO_APLISOFT.RPT",
            //                    //dsOrcamento,
            //                    //pfields,
            //                    //emaildestinatario,
            //                    //"Orcamento n." + dgvOrcamento.CurrentRow.Cells[3].Value.ToString() + "_" + clsParser.SqlDateTimeParse(dgvOrcamento.CurrentRow.Cells["DATA"].Value.ToString()).Value.ToString("yyyy"),
            //                    //"Orcamento n." + dgvOrcamento.CurrentRow.Cells[3].Value.ToString() + "_" + clsParser.SqlDateTimeParse(dgvOrcamento.CurrentRow.Cells["DATA"].Value.ToString()).Value.ToString("yyyy"),
            //                    //clsInfo.conexaosqldados);
            //                }            
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Selecione um Orcamento!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
        }       

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvOrcamento);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvOrcamento, "numero") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvOrcamento, "numero") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvOrcamento, "numero") == false)
                        {
                            if (dgvOrcamento.Rows.Count > 0)
                            {
                                dgvOrcamento.CurrentCell = dgvOrcamento.Rows[0].Cells["numero"];
                            }
                        }
                    }
                }
            }
            else if (dgvOrcamento.Rows.Count > 0)
            {
                dgvOrcamento.CurrentCell = dgvOrcamento.Rows[0].Cells["numero"];
            }

            if (dgvOrcamento.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvOrcamento.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvOrcamento.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvOrcamento.Rows[
           dgvOrcamento.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvOrcamento.CurrentRow.Index < dgvOrcamento.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvOrcamento.Rows[
         dgvOrcamento.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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

        private void tstPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tstPesquisa.Focus();
        }

        private void tstPesquisa_MouseUp_1(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstPesquisa.Focus();
        }

        private void rbnFilial00_Click(object sender, EventArgs e)
        {
            bwrOrcamento_Run();
        }

        private void rbnFilial01_Click(object sender, EventArgs e)
        {
            bwrOrcamento_Run();
        }

        private void rbnFilial02_Click(object sender, EventArgs e)
        {
            bwrOrcamento_Run();
        }

        private void dgvOrcamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }
    }
}



