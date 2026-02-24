
using CRCorreaBLL;
using CRCorreaFuncoes;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmNFCompraVis : Form
    {

        clsBasicReport clsBr;
        String query;
        SqlDataAdapter sda;
        SqlConnection scn;
        SqlCommand scd;

        Int32 id;
        String filtro_status;
        String tipo_lista;

        static DataTable dtNFCompra;
        static DataTable dtNFCompra1;
        static DataTable dtGastoFornecedor;

        clsNfCompraBLL NFCompraBLL;

        Boolean carregandoNFCompra;

        public frmNFCompraVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBr = new clsBasicReport(this, dgvNFCompra, ttpNFComprasVis, gbxNFCompra);
        }

        private void frmNFCompraVis_Load(object sender, EventArgs e)
        {
            rbnPeriodoE.Checked = true;
            tbxPeriodoI.Text = "01/" + DateTime.Now.Month.ToString().PadLeft(2,'0') + "/" + DateTime.Now.Year.ToString();
            tbxPeriodoF.Text = (DateTime.Parse(tbxPeriodoI.Text).AddMonths(1).AddDays(-1)).ToString("dd/MM/yyyy");

            rbnPeriodoE.Select();

        }

        private void frmNFCompraVis_Activated(object sender, EventArgs e)
        {
            bwrNFCompra_Run();
            bwrNFCompra1_Run();
            
        }

        private void frmNFCompraVis_Shown(object sender, EventArgs e)
        {

        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmNFCompra frmNFCompra = new frmNFCompra();
            frmNFCompra.Init(0, dgvNFCompra.Rows);
            clsFormHelper.AbrirForm(this.MdiParent, frmNFCompra, clsInfo.conexaosqldados);

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvNFCompra.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvNFCompra.CurrentRow.Cells["ID"].Value.ToString());
                frmNFCompra frmNFCompra = new frmNFCompra();
                frmNFCompra.Init(id, dgvNFCompra.Rows);
                clsFormHelper.AbrirForm(this.MdiParent, frmNFCompra, clsInfo.conexaosqldados);

            }

        }

        private void tspImprimirMNU_Click(object sender, EventArgs e)
        {
            frmRelNFCompra frmRelNFCompra = new frmRelNFCompra();
            frmRelNFCompra.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelNFCompra, clsInfo.conexaosqldados);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void TrataCampos(Control ctl)
        {
            //bwrNFCompra_Run();
            tbxPeriodoI.Text = DateTime.Parse(tbxPeriodoI.Text).ToString("dd/MM/yyyy");
            tbxPeriodoF.Text = DateTime.Parse(tbxPeriodoF.Text).ToString("dd/MM/yyyy");
            if (ctl.Name == tbxPeriodoF.Name)
            {
                bwrNFCompra_Run();
            }

        }
        private void bwrNFCompra_Run()
        {
            SomaDespesasMes();
            if (carregandoNFCompra == false)
            {
                carregandoNFCompra = true;
                //pbxCompras.Visible = true;
                bwrNFCompra = new BackgroundWorker();
                bwrNFCompra.DoWork += new DoWorkEventHandler(bwrNFCompra_DoWork);
                bwrNFCompra.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrNFCompra_RunWorkerCompleted);
                bwrNFCompra.RunWorkerAsync();
            }
        }


        private void bwrNFCompra_DoWork(object sender, DoWorkEventArgs e)
        {
            
            if (rbnPeriodoT.Checked == true)
            {
                filtro_status = "T";
            }
            else if (rbnPeriodoE.Checked == true)
            {
                filtro_status = "E";
            }
            if (rbnSituacaoTodas.Checked == true)
            {
                tipo_lista = "T";
            }
            else if (rbnSituacaoCompras.Checked == true)
            {
                tipo_lista = "1";
            }
            else if (rbnSituacaoConsignacao.Checked == true)
            {
                tipo_lista = "2";
            }
            else if (rbnSituacaoDevolucao.Checked == true)
            {
                tipo_lista = "3";
            }
            else if (rbnSituacaoRemessa.Checked == true)
            {
                tipo_lista = "4";
            }
            dtNFCompra = clsNfCompraBLL.GridDados(clsInfo.zfilial, filtro_status, tbxPeriodoI.Text, tbxPeriodoF.Text, tipo_lista);
        }


        private void bwrNFCompra_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsNfCompraBLL.GridMonta(dgvNFCompra, dtNFCompra, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvNFCompra, 1);

                PesquisaRapida();
                bwrGastosFornecedor_RunWorkerAsync();

                carregandoNFCompra = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoNFCompra = false;
            }
        }

        private void bwrNFCompra1_Run()
        {
            bwrNFCompra1 = new BackgroundWorker();
            bwrNFCompra1.DoWork += new DoWorkEventHandler(bwrNFCompra1_DoWork);
            bwrNFCompra1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrNFCompra1_RunWorkerCompleted);
            bwrNFCompra1.RunWorkerAsync();
        }
        private void bwrNFCompra1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtNFCompra1 = clsNfCompra1BLL.GridDados(id, clsInfo.conexaosqldados);
        }
        private void bwrNFCompra1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsNfCompra1BLL.GridMonta(dgvNFCompra1, dtNFCompra1, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvNFCompra1, 1);

                PintaGrid(dgvNFCompra1);

                //PesquisaRapida();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {

            }
        }

        private void bwrGastosFornecedor_RunWorkerAsync()
        {
            //
            SomaDespesasMes();
            //            
            pbxCarregarGastosFornecedor.Visible = true;
            bwrGastosFornecedor = new BackgroundWorker();
            bwrGastosFornecedor.DoWork += new DoWorkEventHandler(bwrGastosFornecedor_DoWork);
            bwrGastosFornecedor.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrGastosFornecedor_RunWorkerCompleted);
            bwrGastosFornecedor.RunWorkerAsync();
        }

        private void bwrGastosFornecedor_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                dtGastoFornecedor = CarregaGridNFCompra1Acumula();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public DataTable CarregaGridNFCompra1Acumula()
        {
            try
            {
                dtGastoFornecedor = new DataTable();
                String query;
                Decimal valorcalculo = clsParser.DecimalParse(tbxTotalMes.Text);

                if (rbnResumoCliente.Checked == true)
                { /// POR FORNECEDOR
                    if (valorcalculo > 0)
                    {
                        //Montar a Visualização por Fornecedor com Porcentagem
                        query = "SELECT CLIENTE.COGNOME, " +
                                "SUM((NFCOMPRA.TOTALNOTAFISCAL) - (NFCOMPRA.IRRF + NFCOMPRA.INSS + NFCOMPRA.PISCOFINSCSLL+NFCOMPRA.ISS+NFCOMPRA.PIS+NFCOMPRA.COFINS+NFCOMPRA.CSLL)) AS [TOTALNOTA], " +
                                "(((SUM((NFCOMPRA.TOTALNOTAFISCAL) - (NFCOMPRA.IRRF + NFCOMPRA.INSS + NFCOMPRA.PISCOFINSCSLL+NFCOMPRA.ISS+NFCOMPRA.PIS+NFCOMPRA.COFINS+NFCOMPRA.CSLL)) * 100) / CONVERT(DECIMAL(12,2), '" + valorcalculo.ToString().Replace(',', '.') + "'))) AS PORCENTAGEM  " +
                                "from NFCOMPRA " +
                                "LEFT JOIN CLIENTE ON CLIENTE.ID=NFCOMPRA.IDFORNECEDOR " +
                                // "INNER JOIN NFCOMPRA1 ON NFCOMPRA1.NUMERO = NFCOMPRA.ID " +
                                "WHERE NFCOMPRA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
                        //query = query + " AND NFCOMPRA1.FATURA= 'S' ";
                        query = query + " AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPeriodoI.Text + " 00:00", true);
                        query = query + " AND NFCOMPRA.DATARECEBIMENTO <= " + clsParser.SqlDateTimeFormat(tbxPeriodoF.Text + " 23:59", true);
                        query = query + " GROUP BY CLIENTE.COGNOME";
                        //// } 
                        sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                        sda.Fill(dtGastoFornecedor);
                        return dtGastoFornecedor;
                    }
                    else
                    {
                        dtGastoFornecedor = null;
                        return dtGastoFornecedor;
                    }
                }
                else if (rbnResumoProduto.Checked == true)
                {  // POR GRUPO DE PRODUTO
                    if (valorcalculo > 0)
                    {
                        //Montar a Visualização por Fornecedor com Porcentagem
                        query = "SELECT PECASCLASSIFICA.NOME COGNOME, SUM((NFCOMPRA1.TOTALNOTA)) AS [TOTALNOTA], " +
                                "(((SUM(NFCOMPRA1.TOTALNOTA) * 100) / CONVERT(DECIMAL(12,2), '" + valorcalculo.ToString().Replace(',', '.') + "'))) AS PORCENTAGEM  " +
                                "FROM NFCOMPRA1 " +
                                "LEFT JOIN NFCOMPRA ON NFCOMPRA.ID = NFCOMPRA1.NUMERO " +
                                "LEFT JOIN PECAS ON PECAS.ID = NFCOMPRA1.IDCODIGO " +
                                "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                                "WHERE NFCOMPRA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
                        query = query + " AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPeriodoI.Text + " 00:00", true);
                        query = query + " AND NFCOMPRA.DATARECEBIMENTO <= " + clsParser.SqlDateTimeFormat(tbxPeriodoF.Text + " 23:59", true);
                        query = query + " GROUP BY PECASCLASSIFICA.NOME";
                        query = query + " ORDER BY TOTALNOTA DESC";
                        //// } 
                        sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                        sda.Fill(dtGastoFornecedor);
                        return dtGastoFornecedor;
                    }
                    else
                    {
                        dtGastoFornecedor = null;
                        return dtGastoFornecedor;
                    }
                }
                else if (rbnResumoVendedor.Checked == true)
                {
                    dtGastoFornecedor = null;
                    return dtGastoFornecedor;
                }
                else if (rbnResumoProdutoItem.Checked == true)
                { // POR ITEM DE PRODUTO
                    if (valorcalculo > 0)
                    {
                        query = "SELECT PECAS.CODIGO + ' ' + PECAS.NOME AS COGNOME, SUM(NFCOMPRA1.QTDE) AS QTDE, SUM(NFCOMPRA1.TOTALNOTA) AS TOTALNOTA, " +
                                "(((SUM(NFCOMPRA1.TOTALNOTA) * 100) / CONVERT(DECIMAL(12,2), '" + valorcalculo.ToString().Replace(',', '.') + "'))) AS PORCENTAGEM,  " +
                                "PECASCLASSIFICA.CODIGO AS GRUPO " +
                                "FROM PECAS " +
                                "INNER JOIN NFCOMPRA1 ON NFCOMPRA1.IDCODIGO = PECAS.ID " +
                                "INNER JOIN NFCOMPRA ON NFCOMPRA.ID = NFCOMPRA1.NUMERO " +
                                "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                                "WHERE NFCOMPRA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";
                        //query = query + " AND NFCOMPRA1.FATURA= 'S' ";
                        query = query + " AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPeriodoI.Text + " 00:00", true);
                        query = query + " AND NFCOMPRA.DATARECEBIMENTO <= " + clsParser.SqlDateTimeFormat(tbxPeriodoF.Text + " 23:59", true);
                        query = query + " GROUP BY PECAS.CODIGO, PECAS.NOME,PECASCLASSIFICA.CODIGO ";
                        query = query + " ORDER BY TOTALNOTA DESC";
                        //// } 
                        sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                        sda.Fill(dtGastoFornecedor);
                        return dtGastoFornecedor;
                    }
                    else
                    {
                        dtGastoFornecedor = null;
                        return dtGastoFornecedor;
                    }
                }
                else
                {
                    dtGastoFornecedor = null;
                    return dtGastoFornecedor;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrGastosFornecedor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvGastos.DataSource = dtGastoFornecedor;

                //gbxNFCompraGasto.Text = "Gastos por Fornecedor - Mês: " + clsParser.DecimalParse(tbxTotalMes.Text).ToString("N2");
                if (dtGastoFornecedor != null)
                {


                    clsGridHelper.MontaGrid(dgvGastos,
                                  new String[] { "Fornecedor", "Total", "%" },

                                  new String[] { "COGNOME", "TOTALNOTA", "PORCENTAGEM" },

                                  new Int32[] { 150, 80, 50 },

                                  new DataGridViewContentAlignment[] {
                                        DataGridViewContentAlignment.MiddleLeft,
                                        DataGridViewContentAlignment.MiddleRight,
                                        DataGridViewContentAlignment.MiddleRight},
                                  new Boolean[] { true, true, true },
                                  true, 2, ListSortDirection.Descending);
                    dgvGastos.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
                    dgvGastos.Columns["PORCENTAGEM"].DefaultCellStyle.Format = "N3";
                    clsGridHelper.FontGrid(dgvGastos, 7);
                    //dgvGastos.Font = new Font("Tahoma", 7, FontStyle.Regular);
                }
                else
                {
                    if (dgvGastos.DataSource != null)
                    {
                        ((DataTable)dgvGastos.DataSource).Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                pbxCarregarGastosFornecedor.Visible = false;
            }
        }
        private void rbnPeriodoT_Click(object sender, EventArgs e)
        {
            bwrNFCompra_Run();
        }

        private void dgvNFCompra_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void tbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvNFCompra);

            clsGridHelper.SelecionaLinha(id, dgvNFCompra, "NUMERO");
            tbxPesquisa.Focus();
            if (dgvNFCompra.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvNFCompra.CurrentRow.Cells["NUMERO"].Value.ToString());
            }
            else
            {
                id = 0;
            }
        }

        public void PintaGrid(DataGridView _datagrid)
        {
/*            foreach (DataGridViewRow dgvrNFCompra1Item in _datagrid.Rows)
            {
                // Destacando itens com cores
                if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "10")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                }
                else if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "20")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "30")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.Thistle;
                }
                else if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "40")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.LightBlue;
                }
                else if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "24" || dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "44")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                }
                else if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "25" || dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "45")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                else if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "26")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.PowderBlue;
                }
                else if (dgvrNFCompra1Item.Cells["TIPOENTRADA"].Value.ToString() == "90")
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                else
                {
                    _datagrid.Rows[dgvrNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
            } */
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNFCompra.CurrentRow != null)
                {
                    DialogResult drt;

                    drt = MessageBox.Show("Deseja realmente Excluir a NF: " + dgvNFCompra.CurrentRow.Cells["numero"].Value.ToString() + "?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (drt == DialogResult.Yes)
                    {
                        NFCompraBLL = new clsNfCompraBLL();
                        NFCompraBLL.Excluir(clsParser.Int32Parse(dgvNFCompra.CurrentRow.Cells["id"].Value.ToString()), clsInfo.conexaosqldados);
                        bwrNFCompra_Run();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SomaDespesasMes()
        {
            if (rbnPeriodoE.Checked == true)
            {
                DateTime data = DateTime.Parse("01/" + DateTime.Parse(tbxPeriodoI.Text).Month + "/" + DateTime.Parse(tbxPeriodoI.Text).Year);
                String DataMesAntde = data.AddMonths(-1).ToString("dd/MM/yyyy");
                String DataMesAntate = data.ToString("dd/MM/yyyy");
                //
                // Soma o Mes anterior
                scn = new SqlConnection(clsInfo.conexaosqldados);
                //query = "SELECT SUM((NFCOMPRA.TOTALNOTAFISCAL) - (NFCOMPRA.IRRF + NFCOMPRA.INSS + NFCOMPRA.PISCOFINSCSLL+NFCOMPRA.ISS+NFCOMPRA.PIS+NFCOMPRA.COFINS+NFCOMPRA.CSLL)) " +
                //        " FROM NFCOMPRA INNER JOIN NFCOMPRA1 ON NFCOMPRA1.NUMERO = NFCOMPRA.ID " +
                //        " WHERE NFCOMPRA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND NFCOMPRA1.FATURA = 'S' AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(DataMesAntde + " 00:00", true) + " AND NFCOMPRA.DATARECEBIMENTO < " + clsParser.SqlDateTimeFormat(DataMesAntate + " 00:00", true);
                query = "SELECT SUM((NFCOMPRA.TOTALNOTAFISCAL) - (NFCOMPRA.IRRF + NFCOMPRA.INSS + NFCOMPRA.PISCOFINSCSLL+NFCOMPRA.ISS+NFCOMPRA.PIS+NFCOMPRA.COFINS+NFCOMPRA.CSLL)) " +
                        " FROM NFCOMPRA " +
                        " WHERE NFCOMPRA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(DataMesAntde + " 00:00", true) + " AND NFCOMPRA.DATARECEBIMENTO < " + clsParser.SqlDateTimeFormat(DataMesAntate + " 00:00", true);

                scd = new SqlCommand(query, scn);
                scn.Open();
                tbxTotalMesAnterior.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
                scn.Close();

                // Soma o Mes atual
                DataMesAntate = ((data).AddMonths(1)).ToString("dd/MM/yyyy");
                scn = new SqlConnection(clsInfo.conexaosqldados);
                //query = "SELECT SUM((NFCOMPRA.TOTALNOTAFISCAL) - (NFCOMPRA.IRRF + NFCOMPRA.INSS + NFCOMPRA.PISCOFINSCSLL+NFCOMPRA.ISS+NFCOMPRA.PIS+NFCOMPRA.COFINS+NFCOMPRA.CSLL)) " +
                //        " FROM NFCOMPRA INNER JOIN NFCOMPRA1 ON NFCOMPRA1.NUMERO = NFCOMPRA.ID " +
                //        " WHERE NFCOMPRA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND NFCOMPRA1.FATURA = 'S'  AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPeriodoI.Text + " 00:00", true) + " AND NFCOMPRA.DATARECEBIMENTO < " + clsParser.SqlDateTimeFormat(DataMesAntate + " 00:00", true);
                query = "SELECT SUM((NFCOMPRA.TOTALNOTAFISCAL) - (NFCOMPRA.IRRF + NFCOMPRA.INSS + NFCOMPRA.PISCOFINSCSLL+NFCOMPRA.ISS+NFCOMPRA.PIS+NFCOMPRA.COFINS+NFCOMPRA.CSLL)) " +
                        " FROM NFCOMPRA " +
                        " WHERE NFCOMPRA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " AND NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPeriodoI.Text + " 00:00", true) + " AND NFCOMPRA.DATARECEBIMENTO < " + clsParser.SqlDateTimeFormat(DataMesAntate + " 00:00", true);

                scd = new SqlCommand(query, scn);
                scn.Open();
                tbxTotalMes.Text = clsParser.DecimalParse(scd.ExecuteScalar().ToString()).ToString("N2");
                scn.Close();

            }
            else
            {
                tbxTotalMesAnterior.Text = "0,00";
                tbxTotalMes.Text = "0,00";
            }

        }

        private void dgvNFCompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvNFCompra.CurrentRow != null)
                {
                    id = Int32.Parse(dgvNFCompra.CurrentRow.Cells[0].Value.ToString());
                    bwrNFCompra1_Run();
                    bwrGastosFornecedor_RunWorkerAsync();
                }
            }
            catch
            {

            }
        }

        private void rbnResumoCliente_Click(object sender, EventArgs e)
        {
            gbxNFCompraGasto.Text = "Total de Gastos por Fornecedor";
            bwrGastosFornecedor_RunWorkerAsync(); 

        }

        private void rbnResumoProduto_Click(object sender, EventArgs e)
        {
            gbxNFCompraGasto.Text = "Total de Grupo de Despesas";
            bwrGastosFornecedor_RunWorkerAsync();

        }

        private void rbnResumoProdutoItem_Click(object sender, EventArgs e)
        {
            gbxNFCompraGasto.Text = "Total de Gastos por Item de Produto";
            bwrGastosFornecedor_RunWorkerAsync();

        }

        private void rbnResumoVendedor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desativado");
        }

        private void tbxTotalMes_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tsbImportar_Click(object sender, EventArgs e)
        {
            frmImpNFCompra frmImpNFCompra = new frmImpNFCompra();
            frmImpNFCompra.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmImpNFCompra, clsInfo.conexaosqldados);
        }

        private void tsbEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvNFCompra.CurrentRow;
            this.Close();
        }
    }
}
