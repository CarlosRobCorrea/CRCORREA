using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPedidoVis : Form
    {
        String query;
        SqlDataAdapter sda;

        DataTable dtPedido;
        BackgroundWorker bwrPedido;
        clsPedidoBLL clsPedidoBLL;
        Int32 id;
        String GrupoUsuario;
        String Usuario = "";


        DataTable dtPedido1;
        BackgroundWorker bwrPedido1;
        clsPedido1BLL clsPedido1BLL;

        DataTable dtPedidoPendente;
        BackgroundWorker bwrPedidoPendente;

        //clsBasicReport clsBr;

        GridColuna[] dtPedidoColuna = new GridColuna[]
                                        {
                                            new GridColuna("Id", "ID", 10, false, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Nº", "NUMERO", 50, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Data", "DATA", 75, true, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Cliente", "CLIENTE", 130, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Vendedor", "VENDEDOR", 60, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Qt.Itens", "QTDESALDO", 60, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Total Mercad.", "TOTALMERCADORIA", 95, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Total Pedido", "TOTALPEDIDO", 105, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("R$ Desconto", "TOTALDESCONTO", 75, true, DataGridViewContentAlignment.MiddleRight)
                                        };

        GridColuna[] dtPedido1Coluna = new GridColuna[]
                                        {
                                            new GridColuna("Id", "ID", 10, false, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Item", "NITEM", 50, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Código", "CODIGO", 140, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Nome", "DESCRICAO", 280, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Qtde", "QTDE", 90, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Qt.Itens", "QTDESALDO", 90, true, DataGridViewContentAlignment.MiddleRight)
                                        };
        GridColuna[] dtPedidoPendenteColuna = new GridColuna[]
                                        {
                                            new GridColuna("Cliente", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Total Faturar", "TOTALPEDIDO", 100, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("__%__", "PARTICIPACAO", 50, true, DataGridViewContentAlignment.MiddleRight)
                                        };
        GridColuna[] dtPedidoPendenteItemColuna = new GridColuna[]
                                {
                                            new GridColuna("Produto", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Qtde", "QTDE", 70, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Total Faturar", "TOTALPEDIDO", 100, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("__%__", "PARTICIPACAO", 50, true, DataGridViewContentAlignment.MiddleRight),
                                            new GridColuna("Grupo", "GRUPO", 50, true, DataGridViewContentAlignment.MiddleCenter)
                                };



        public frmPedidoVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            //clsBr = new clsBasicReport(this, dgvPedido, ttpPedido, gbxPedido);

            clsPedidoBLL = new clsPedidoBLL();
            clsPedido1BLL = new clsPedido1BLL();
        }

        private void frmPedidoVis_Load(object sender, EventArgs e)
        {
            GrupoUsuario = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select GRUPO from USUARIO where id = " + clsInfo.zusuarioid, "");
            
            tbxDataDe.Text = "01/" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Today.Year.ToString();
            tbxDataAte.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            
            if (GrupoUsuario == "V")
            {
                Usuario = clsInfo.zusuario;
                gbxResumoPor.Visible = false;
                rbnResumoCliente.Checked = true;
            }

            bwrPedido_Run();
        }
        private void dgvPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPedido.CurrentRow != null)
            {
                id = Int32.Parse(dgvPedido.CurrentRow.Cells[0].Value.ToString());
                bwrPedido1_Run();
            }
            else
            {
                dgvPedido1.DataSource = null;
            }
        }

        private void dgvPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPedido.CurrentRow != null)
            {
                if (tspAlterar.Enabled == true)
                {
                    tspAlterar.PerformClick();
                }
                else
                {
                    id = (Int32)dgvPedido.CurrentRow.Cells[0].Value;
                    frmPedido frmPedido = new frmPedido();
                    frmPedido.Init(id, dgvPedido.Rows);

                    clsFormHelper.AbrirForm(this.MdiParent, frmPedido, clsInfo.conexaosqldados);
                }
            }

        }

        private void dgvPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }
        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvPedido.CurrentRow != null)
            {
                id = (Int32)dgvPedido.CurrentRow.Cells[0].Value;
            }

            frmPedido frmPedido = new frmPedido();
            frmPedido.Init(0, dgvPedido.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmPedido, clsInfo.conexaosqldados);
        }
        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPedido.CurrentRow != null)
            {
                id = (Int32)dgvPedido.CurrentRow.Cells[0].Value;
                frmPedido frmPedido = new frmPedido();
                frmPedido.Init(id, dgvPedido.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmPedido, clsInfo.conexaosqldados);
            }
        }
        private void tspEscolher_Click(object sender, EventArgs e)
        {
            if (dgvPedido.CurrentRow != null)
            {
                clsInfo.zrow = dgvPedido.CurrentRow;
            }
            this.Close();
            this.Dispose();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvPedido);
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //clsBr.Imprimir(clsInfo.caminhorelatorios, "Pedidos de Venda", clsInfo.conexaosqldados);            
        }

        //////////////////////////////
        private void bwrPedido_Run()
        {
            bwrPedido = new BackgroundWorker();
            bwrPedido.DoWork += new DoWorkEventHandler(bwrPedido_DoWork);
            bwrPedido.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPedido_RunWorkerCompleted);
            bwrPedido.RunWorkerAsync();
        }

        private void bwrPedido_DoWork(object sender, DoWorkEventArgs e)
        {
            dtPedido = new DataTable();
            query = "";
            query = "SELECT " +
                    "    PEDIDO.ID, " +
                    "    PEDIDO.NUMERO, " +
                    "    PEDIDO.DATA, " +
                    "    CLIENTE.COGNOME AS CLIENTE, " +
                    "    VENDEDOR.COGNOME AS VENDEDOR, " +
                    "    PEDIDO.QTDESALDO, " +
                    "    PEDIDO.TOTALMERCADORIA, " +
                    "    PEDIDO.TOTALPEDIDO, " +
                    "    PEDIDO.TOTALDESCONTO " +
                    "FROM " +
                    "    PEDIDO " +
                    "       INNER JOIN CLIENTE ON CLIENTE.ID = PEDIDO.IDCLIENTE " +
                    "       INNER JOIN CLIENTE as VENDEDOR ON VENDEDOR.ID = PEDIDO.IDVENDEDOR " +
                    "WHERE " +
                    "    PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxDataDe.Text + " 00:00", true) + " AND " +
                    "    PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxDataAte.Text + " 23:59", true);

            if (rbnTodas.Checked == false)
            {
                query += " AND PEDIDO.QTDESALDO > 0 ";
            }
            // Verificar se é Vendedor - Ai imprime só as vendas dele
            if (Usuario != "")
            {
                query += " AND VENDEDOR.COGNOME ='" + Usuario + "' ";
            }
            query = query + " ORDER BY PEDIDO.DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtPedido);
        }
        private void bwrPedido_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvPedido.DataSource = dtPedido;
                clsGridHelper.MontaGrid2(dgvPedido, dtPedidoColuna, true);
                clsGridHelper.FontGrid(dgvPedido, 8);
                clsGridHelper.SelecionaLinha(id, dgvPedido, 1);
                dgvPedido.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
                dgvPedido.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPedido.Columns["QTDESALDO"].DefaultCellStyle.Format = "N0";
                dgvPedido.Columns["TOTALMERCADORIA"].DefaultCellStyle.Format = "N2";
                dgvPedido.Columns["TOTALPEDIDO"].DefaultCellStyle.Format = "N2";
                dgvPedido.Columns["TOTALDESCONTO"].DefaultCellStyle.Format = "N2";
                dgvPedido.Select();

                if (dtPedido.Rows.Count > 0)
                {
                    bwrPedidoPendente_Run();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmPedidoVis_Activated(object sender, EventArgs e)
        {
            //bwrPedido_Run();
        }

        private void rbnTodas_Click(object sender, EventArgs e)
        {
           bwrPedido_Run();
        }

        private void rbnPendentes_Click(object sender, EventArgs e)
        {
           bwrPedido_Run();
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

        void TrataCampos(Control ctl)
        {
            if (clsInfo.znomegrid == tbxDataDe.Name)
            {
                bwrPedido_Run();
            }
            if (ctl.Name == tbxDataAte.Name)
            {
                bwrPedido_Run();
            }
        }
        private void bwrPedido1_Run()
        {
            bwrPedido1 = new BackgroundWorker();
            bwrPedido1.DoWork += new DoWorkEventHandler(bwrPedido1_DoWork);
            bwrPedido1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPedido1_RunWorkerCompleted);
            bwrPedido1.RunWorkerAsync();
        }
        private void bwrPedido1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtPedido1 = new DataTable();
            query = "SELECT " +
                        "PEDIDO1.ID, " +
                        "PEDIDO1.NITEM, " +
                        "PECAS.CODIGO, " +
                        "CASE WHEN PECAS.CODIGO = '0' THEN PEDIDO1.COMPLEMENTO " +
                        "WHEN PEDIDO1.COMPLEMENTO <> '' THEN PECAS.NOME + ' - ' + PEDIDO1.COMPLEMENTO " +
                        "ELSE PECAS.NOME END AS DESCRICAO, " +
                        "PEDIDO1.QTDE, " +
                        "PEDIDO1.QTDESALDO " +
                    "FROM " +
                        "PEDIDO1 INNER JOIN PECAS ON PEDIDO1.IDCODIGO = PECAS.ID " +
                    "WHERE " +
                        "PEDIDO1.IDPEDIDO = @PEDIDO_ID " +
                    "ORDER BY NITEM";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@PEDIDO_ID", SqlDbType.Int).Value = id;
            sda.Fill(dtPedido1);
//            dtPedido1 = clsNfCompra1BLL.GridDados(id, clsInfo.conexaosqldados);
        }
        private void bwrPedido1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
//                clsNfCompra1BLL.GridMonta(dgvPedido1, dtPedido1, clsInfo.zrowid);
//                clsGridHelper.SelecionaLinha(id, dgvPedido1, 1);
                dgvPedido1.DataSource = dtPedido1;
                clsGridHelper.MontaGrid2(dgvPedido1, dtPedido1Coluna, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {

            }
        }
        private void bwrPedidoPendente_Run()
        {
            bwrPedidoPendente = new BackgroundWorker();
            bwrPedidoPendente.DoWork += new DoWorkEventHandler(bwrPedidoPendente_DoWork);
            bwrPedidoPendente.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPedidoPendente_RunWorkerCompleted);
            bwrPedidoPendente.RunWorkerAsync();
        }
        private void bwrPedidoPendente_DoWork(object sender, DoWorkEventArgs e)
        {
            dtPedidoPendente = new DataTable();
            if (rbnResumoCliente.Checked == true)
            {
                query = "SELECT " +
                            "CLIENTE.COGNOME AS COGNOME, " +
                            "SUM(PEDIDO.TOTALPEDIDO) AS TOTALPEDIDO " +
                        "FROM " +
                            "CLIENTE " +
                            "INNER JOIN PEDIDO ON PEDIDO.IDCLIENTE = CLIENTE.ID " +
                            "INNER JOIN CLIENTE AS VENDEDOR ON VENDEDOR.ID = PEDIDO.IDVENDEDOR " +
                        "WHERE " +
                        "PEDIDO.QTDESALDO > 0 AND PEDIDO.TOTALPEDIDO > 0 AND " +
                        "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxDataDe.Text + " 00:00", true) + " AND " +
                        "PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxDataAte.Text + " 23:59", true) + " ";
                if (Usuario != "")
                {
                    query += " AND VENDEDOR.COGNOME ='" + Usuario + "' ";
                }
                query = query + " GROUP BY " +
                            "CLIENTE.COGNOME " +
                        "ORDER BY " +
                            "TOTALPEDIDO DESC ";

                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtPedidoPendente);
                DataColumn dcPercentual = new DataColumn("PARTICIPACAO", Type.GetType("System.Decimal"));
                dtPedidoPendente.Columns.Add(dcPercentual);
            }
            else if (rbnResumoVendedor.Checked == true)
            {
                query = "SELECT " +
                            "CLIENTE.COGNOME AS COGNOME, " +
                            "SUM(PEDIDO.TOTALPEDIDO) AS TOTALPEDIDO " +
                        "FROM " +
                            "CLIENTE " +
                            "INNER JOIN PEDIDO ON PEDIDO.IDVENDEDOR = CLIENTE.ID " +
                        "WHERE " +
                        "PEDIDO.QTDESALDO > 0 AND PEDIDO.TOTALPEDIDO > 0 AND " +
                        "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxDataDe.Text + " 00:00", true) + " AND " +
                        "PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxDataAte.Text + " 23:59", true) + " ";
                query = query + " GROUP BY " +
                            "CLIENTE.COGNOME " +
                        "ORDER BY " +
                            "TOTALPEDIDO DESC ";

                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtPedidoPendente);
                DataColumn dcPercentual = new DataColumn("PARTICIPACAO", Type.GetType("System.Decimal"));
                dtPedidoPendente.Columns.Add(dcPercentual);
            }
            else if (rbnResumoProduto.Checked == true)
            {  // Por tipo de Produto Campo Ramo do Cadastro de Classificação)
                query = "SELECT " +
                            "PECASCLASSIFICA.NOME AS COGNOME, " +
                            "SUM(PEDIDO1.TOTALNOTA) AS TOTALPEDIDO " +
                        "FROM " +
                        "PECAS " +
                        "INNER JOIN PEDIDO1 ON PEDIDO1.IDCODIGO = PECAS.ID " +
                        "INNER JOIN PEDIDO ON PEDIDO.ID = PEDIDO1.IDPEDIDO " +
                        "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                        "WHERE " +
                        "PEDIDO.QTDESALDO > 0 AND PEDIDO.TOTALPEDIDO > 0 AND " +
                        "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxDataDe.Text + " 00:00", true) + " AND " +
                        "PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxDataAte.Text + " 23:59", true) + " ";
                query = query + " GROUP BY " +
                            "PECASCLASSIFICA.NOME " +
                        "ORDER BY " +
                            "TOTALPEDIDO DESC ";

                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtPedidoPendente);
                DataColumn dcPercentual = new DataColumn("PARTICIPACAO", Type.GetType("System.Decimal"));
                dtPedidoPendente.Columns.Add(dcPercentual);

            }
            else
            { // Por item de Produto acumulando a qtde de cada item
                query = "SELECT " +
                            "PECAS.CODIGO + ' ' + PECAS.NOME AS COGNOME, " +
                            "SUM(PEDIDO1.QTDE) AS QTDE, " +
                            "SUM(PEDIDO1.TOTALNOTA) AS TOTALPEDIDO, " +
                            "PECASCLASSIFICA.CODIGO AS Grupo " +
                        "FROM " +
                        "PECAS " +
                        "INNER JOIN PEDIDO1 ON PEDIDO1.IDCODIGO = PECAS.ID " +
                        "INNER JOIN PEDIDO ON PEDIDO.ID = PEDIDO1.IDPEDIDO " +
                        "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                        "WHERE " +
                        "PEDIDO.QTDESALDO > 0 AND PEDIDO.TOTALPEDIDO > 0 AND " +
                        "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxDataDe.Text + " 00:00", true) + " AND " +
                        "PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxDataAte.Text + " 23:59", true) + " ";
                query = query + " GROUP BY " +
                            "PECAS.CODIGO, PECAS.NOME, PECASCLASSIFICA.CODIGO " +
                        "ORDER BY " +
                            "TOTALPEDIDO DESC ";

                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtPedidoPendente);
                DataColumn dcPercentual = new DataColumn("Participacao", Type.GetType("System.Decimal"));
                //DataColumn dcGrupo = new DataColumn("Grupo", Type.GetType("System.String"));
                dtPedidoPendente.Columns.Add(dcPercentual);
                //dtPedidoPendente.Columns.Add(dcGrupo);

            }
        }
        private void bwrPedidoPendente_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvPedidoPendente.DataSource = dtPedidoPendente;
                Decimal total = 0;
                Decimal valor = 0;

                for (Int32 X = 0; X < dgvPedidoPendente.Rows.Count; X++)
                {
                    total += clsParser.DecimalParse(dgvPedidoPendente.Rows[X].Cells["TOTALPEDIDO"].Value.ToString());
                }
                tbxTotalvalorpendente.Text = total.ToString("N2");

                for (Int32 X = 0; X < dgvPedidoPendente.Rows.Count; X++)
                {
                    valor = Decimal.Multiply(clsParser.DecimalParse(dgvPedidoPendente.Rows[X].Cells["TOTALPEDIDO"].Value.ToString()), 100);
                    valor = Decimal.Divide(valor, total);
                    valor = Decimal.Round(valor, 3);
                    dgvPedidoPendente.Rows[X].Cells["PARTICIPACAO"].Value = clsParser.DecimalParse(valor.ToString());
                    valor = 0;
                }
                if (rbnResumoCliente.Checked == true || rbnResumoVendedor.Checked == true || rbnResumoProduto.Checked == true)
                {
                    clsGridHelper.MontaGrid2(dgvPedidoPendente, dtPedidoPendenteColuna, true);
                }
                else
                {
                    clsGridHelper.MontaGrid2(dgvPedidoPendente, dtPedidoPendenteItemColuna, true);
                    clsGridHelper.FontGrid(dgvPedidoPendente, 7);
                }
                dgvPedidoPendente.Columns["TOTALPEDIDO"].DefaultCellStyle.Format = "N2";
                dgvPedidoPendente.Columns["PARTICIPACAO"].DefaultCellStyle.Format = "N2";
                //dgvPedidoPendente.Columns["GRUPO"].DefaultCellStyle.Format = "N2";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Casa Correa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void tstbxLocalizar_Click(object sender, EventArgs e)
        {

        }

        private void rbnResumoCliente_Click(object sender, EventArgs e)
        {
            gbxResumoPedido.Text = "Total de Pedidos por Cliente";
            bwrPedidoPendente_Run();
        }

        private void rbnResumoVendedor_Click(object sender, EventArgs e)
        {
            gbxResumoPedido.Text = "Total de Pedidos por Vendedor";
            bwrPedidoPendente_Run();
        }

        private void rbnResumoProduto_Click(object sender, EventArgs e)
        {
            gbxResumoPedido.Text = "Total de Pedidos por Grupo de Produtos";
            bwrPedidoPendente_Run();

        }

        private void rbnResumoProdutoItem_Click(object sender, EventArgs e)
        {
            gbxResumoPedido.Text = "Total de Pedidos por Item de Produto";
            bwrPedidoPendente_Run();

        }

        private void tspMenuImprimir_Click(object sender, EventArgs e)
        {
            frmRelPedidos frmRelPedidos = new frmRelPedidos();
            frmRelPedidos.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelPedidos, clsInfo.conexaosqldados);

        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.CurrentRow != null)
                {
                    DialogResult drt;

                    drt = MessageBox.Show("Deseja realmente Cancelar o Pedido: " + dgvPedido.CurrentRow.Cells["numero"].Value.ToString() + "?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (drt == DialogResult.Yes)
                    {
                        clsPedidoBLL = new clsPedidoBLL();
                        clsPedidoBLL.Excluir(clsParser.Int32Parse(dgvPedido.CurrentRow.Cells["id"].Value.ToString()), clsInfo.conexaosqldados);
                        bwrPedido_Run();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
