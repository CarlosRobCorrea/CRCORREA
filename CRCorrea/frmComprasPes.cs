using CRCorreaBLL;
using CRCorreaInfo;
using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmComprasPes : Form
    {
        private DataTable dtCompras;
        private DataTable dtCompras1;
        private DataTable dtComprasAtraso;
        private String aberto;
        private Int32 idCompras;
        private Int32 idfornecedor;
        Boolean carregandoCompras;

        public frmComprasPes()
        {
            InitializeComponent();
        }
        public void Init()
        {
           // clsBr = new clsBasicReport(this, dgvCompras, ttpCompras, gbxCompras);
        }


        private void frmComprasPes_Load(object sender, EventArgs e)
        {
            nudAnoDe.Value = DateTime.Now.Year - 1;
            nudAnoAte.Value = DateTime.Now.Year;         
        }

        private void frmComprasPes_Shown(object sender, EventArgs e)
        {
            clsFormHelper.VerificarForm(this, ttpCompras, clsInfo.conexaosqldados);
        }

        private void frmComprasPes_Activated(object sender, EventArgs e)
        {
            //bwrCompras_RunWorkerAsync();
            bwrCompras_Run();
        }

        private void bwrCompras_Run()
        {
            if (carregandoCompras == false)
            {
                carregandoCompras = true;
                //pbxCompras.Visible = true;
                bwrCompras = new BackgroundWorker();
                bwrCompras.DoWork += new DoWorkEventHandler(bwrCompras_DoWork);
                bwrCompras.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCompras_RunWorkerCompleted);
                bwrCompras.RunWorkerAsync();
            }
        }

        public DataTable CarregaGridCompras()
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            try
            {
                if (rbnTodas.Checked == true)
                {
                    query = "SELECT COMPRAS.ID, COMPRAS.FILIAL, COMPRAS.NUMERO, COMPRAS.DATA, " +
                            "CLIENTE.COGNOME, CLIENTE.CONTATO AS CONTATO, " +
                            "CASE COMPRAS.TERMINO WHEN 'S' THEN 'Fechado' ELSE 'Aberto' END AS [TERM], " +
                            "COMPRAS.TOTALPECA,COMPRAS.TOTALPECAENTRA,COMPRAS.TOTALPECATRANSFE, " +
                            "(COMPRAS.TOTALPECA-(COMPRAS.TOTALPECAENTRA+COMPRAS.TOTALPECATRANSFE)) AS [SALDO], " +
                            "COMPRAS.TOTALPEDIDO, COMPRAS.TOTALPREVISTO, COMPRAS.OBSERVA, " +
                            "COMPRAS.IDFORNECEDOR AS [COMPRAS_IDFORNECEDOR], " +
                            "COMPRAS.IDAUTORIZANTE, " +
                            "FROM COMPRAS " +
                            "LEFT JOIN CLIENTE ON CLIENTE.ID=COMPRAS.IDFORNECEDOR " +
                            "WHERE YEAR(COMPRAS.DATA) >= @DOANO AND YEAR(COMPRAS.DATA) <= @ATEANO ";


                    query = query + " AND COMPRAS.FILIAL = @FILIAL ";
                    if (rbnEmAberto.Checked == true) // Aberto
                    {
                        query = query + " AND COMPRAS.TERMINO != @TERMINO ";
                    }
                    else if (rbnFechado.Checked == true) // Fechado
                    {
                        query = query + " AND COMPRAS.TERMINO = @TERMINO ";
                    }
                    query = query + " ORDER BY COMPRAS.DATA DESC ";
                    sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                    sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = 1; // Filial;
                    sda.SelectCommand.Parameters.Add("DOANO", SqlDbType.Int).Value = nudAnoDe;
                    sda.SelectCommand.Parameters.Add("ATEANO", SqlDbType.Int).Value = nudAnoAte;
                    sda.SelectCommand.Parameters.Add("TERMINO", SqlDbType.NVarChar).Value = 'S';
                    dt = new DataTable();
                    sda.Fill(dt);
                    return dt;



                    //return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "COMPRAS " +
                    //                            "INNER JOIN CLIENTE ON CLIENTE.ID=COMPRAS.IDFORNECEDOR ",
                }
                else
                {
                    if (rbnEmAberto.Checked)
                    {
                        aberto = "N";
                    }
                    else
                    {
                        aberto = "S";
                    }

                    return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "COMPRAS " +
                                            "INNER JOIN CLIENTE ON CLIENTE.ID=COMPRAS.IDFORNECEDOR ",
                                            "COMPRAS.ID AS [ID], " +
                                            "COMPRAS.NUMERO AS [COMPRAS_NUMERO], " +
                                            "COMPRAS.DATA AS [COMPRAS_DATA], " +
                                            "CLIENTE.COGNOME AS [CLIENTE_COGNOME], " +
                                            "CASE COMPRAS.TERMINO WHEN 'S' THEN 'Fechado' ELSE 'Aberto' END AS [TERM], " +
                                            "COMPRAS.TOTALPECA AS [COMPRAS_TOTALPECA], " +
                                            "COMPRAS.TOTALPECAENTRA AS [COMPRAS_TOTALPECAENTRA], " +
                                            "COMPRAS.TOTALPECATRANSFE AS [COMPRAS_TOTALPECATRANSFE], " +
                                            "(COMPRAS.TOTALPECA - (COMPRAS.TOTALPECAENTRA+COMPRAS.TOTALPECATRANSFE)) AS [SALDO], " +
                                            "COMPRAS.TOTALPEDIDO AS [COMPRAS_TOTALPEDIDO], " +
                                            "COMPRAS.IDFORNECEDOR AS [COMPRAS_IDFORNECEDOR], " +
                                            "COMPRAS.IDAUTORIZANTE, " +
                                            "CLIENTE.CONTATO AS CONTATO_",
                                            "YEAR(COMPRAS.DATA) >= + " + clsParser.Int32Parse(nudAnoDe.Value.ToString()) + "AND " +
                                            "YEAR(COMPRAS.DATA) <= + " + clsParser.Int32Parse(nudAnoAte.Value.ToString()) + " AND " +
                                            "COMPRAS.TERMINO = '" + aberto.ToString() + "'",
                                            "COMPRAS.DATA DESC ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrCompras_DoWork(object sender, DoWorkEventArgs e)
        {
            dtCompras = CarregaGridCompras();
        }

        private void bwrCompras_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvCompras.DataSource = dtCompras;
                dgvCompras.Sort(dgvCompras.Columns["COMPRAS_NUMERO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid(dgvCompras, 8);

                clsGridHelper.Filtrar(tbxPesquisa.Text, dgvCompras);
                clsGridHelper.SelecionaLinha(idCompras, dgvCompras, "COMPRAS_NUMERO");
                if (dgvCompras.CurrentRow != null)
                {
                    idCompras = clsParser.Int32Parse(dgvCompras.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    idCompras = 0;
                }

                bwrCompras1_RunWorkerAsync();
                bwrComprasAtraso_RunWorkerAsync();

                pbxCompras.Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvCompras.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Close();
                this.Dispose();
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }


        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.FormatarCampoNumerico(sender);
            clsVisual.ControlLeave(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void rbnEmAberto_Click(object sender, EventArgs e)
        {
            bwrCompras_Run();
        }

        private void rbnEmAberto_Click_1(object sender, EventArgs e)
        {
            bwrCompras_Run();
        }

        private void rbnFechado_Click(object sender, EventArgs e)
        {
            bwrCompras_Run();
        }

        private void nudAnoDe_Click(object sender, EventArgs e)
        {
            bwrCompras_Run();
        }

        private void nudAnoAte_Click(object sender, EventArgs e)
        {
            bwrCompras_Run();
        }


        private void bwrCompras1_RunWorkerAsync()
        {
            pbxCompras1.Visible = true;
            bwrCompras1 = new BackgroundWorker();
            bwrCompras1.DoWork += new DoWorkEventHandler(bwrCompras1_DoWork);
            bwrCompras1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCompras1_RunWorkerCompleted);
            bwrCompras1.RunWorkerAsync();
        }

        private void bwrCompras1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtCompras1 = CarregaGridCompras1();
        }

        public DataTable CarregaGridCompras1()
        {
            try
            {
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "COMPRAS1 " +
                                "INNER JOIN PECAS ON PECAS.ID=COMPRAS1.IDCODIGO " +
                                "INNER JOIN UNIDADE ON UNIDADE.ID=COMPRAS1.IDUNIDFISCAL ",
                                "COMPRAS1.ID AS [COMPRAS1_ID], " +
                                "COMPRAS1.QTDE AS [COMPRAS1_QTDE], " +
                                "UNIDADE.CODIGO AS [UNIDADE], " +
                                "PECAS.CODIGO AS [PECAS_CODIGO], " +
                                "PECAS.NOME AS [PECAS_NOME], " +
                                "COMPRAS1.QTDESALDO AS [COMPRAS1_QTDESALDO], " +
                                "COMPRAS1.PRECO AS [COMPRAS1_PRECO], " +
                                "COMPRAS1.TOTALNOTA AS [COMPRAS1_TOTALNOTA] ",
                                "COMPRAS1.IDCOMPRAS = " + clsParser.Int32Parse(idCompras.ToString()),
                                "COMPRAS1.ITEM");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrCompras1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvCompra1.DataSource = dtCompras1;
                dgvCompra1.Sort(dgvCompra1.Columns["PECAS_NOME"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid(dgvCompra1, 8);
                pbxCompras1.Visible = false;              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pbxCompras1.Visible = false;
            }
        }


        //////

        private void bwrComprasAtraso_RunWorkerAsync()
        {
            pbxAtraso.Visible = true;
            bwrComprasAtraso = new BackgroundWorker();
            bwrComprasAtraso.DoWork += new DoWorkEventHandler(bwrComprasAtraso_DoWork);
            bwrComprasAtraso.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrComprasAtraso_RunWorkerCompleted);
            bwrComprasAtraso.RunWorkerAsync();
        }

        private void bwrComprasAtraso_DoWork(object sender, DoWorkEventArgs e)
        {
            dtComprasAtraso = CarregaGridComprasAtraso();
        }

        public DataTable CarregaGridComprasAtraso()
        {
            try
            {
                String querywhere = "COMPRASENTREGA.QTDESALDO > 0 AND " +
                                           "COMPRAS1.TERMINO = 'N' AND COMPRAS.ID= " + idCompras;
            
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "COMPRAS " +
                                      "INNER JOIN COMPRAS1 ON COMPRAS.ID = COMPRAS1.IDCOMPRAS " +
                                      "INNER JOIN COMPRASENTREGA ON COMPRAS1.ID = COMPRASENTREGA.IDCOMPRAS1 " +
                                      "INNER JOIN CLIENTE ON COMPRAS.IDFORNECEDOR	= CLIENTE.ID " +
                                      "INNER JOIN PECAS ON PECAS.ID=COMPRAS1.IDCODIGO " +
                                      "INNER JOIN UNIDADE ON UNIDADE.ID=COMPRAS1.IDUNID ",
                                      "COMPRAS.ID AS [ATRASO_ID], " +
                                      "COMPRASENTREGA.ID AS [ATRASO_IDCOMPRASENTREGA], " +
                                      "COMPRAS.IDFORNECEDOR AS [ATRASO_IDFORNECEDOR], " +
                                      "COMPRAS.NUMERO AS [ATRASO_NUMERO], " +
                                      "CLIENTE.COGNOME AS [ATRASO_COGNOME], " +
                                      "CLIENTE.DDD AS [ATRASO_DDD], " +
                                      "CLIENTE.TELEFONE AS [ATRASO_TELEFONE], " +
                                      "COMPRAS1.ID AS [ATRASO_IDCOMPRAS1], " +
                                      "COMPRAS1.TIPOENTRADA AS [ATRASO_TIPOENTRADA], " +
                                      "PECAS.CODIGO AS [ATRASO_CODIGO], " +
                                      "PECAS.NOME AS [ATRASO_NOME], " +
                                      "COMPRASENTREGA.QTDEENTREGA AS [ATRASO_QTDE], " +
                                      "UNIDADE.CODIGO AS [ATRASO_UNID], " +
                                      "COMPRAS1.PRECO AS [ATRASO_PRECO], " +
                                      "(COMPRASENTREGA.QTDEENTREGA * COMPRAS1.PRECO) AS [ATRASO_TOTALMERCADO], " +
                                      "COMPRAS1.CUSTOIPI AS [ATRASO_CUSTOIPI], " +
                                      "COMPRAS1.TOTALNOTA AS [ATRASO_TOTALNOTA], " +
                                      "COMPRASENTREGA.DATAENTREGA AS [ATRASO_DATAENTREGA], " +
                                      "COMPRASENTREGA.QTDESALDO AS [ATRASO_QTDESALDO] ",
                                      querywhere.ToString(),
                                      "COMPRAS.NUMERO");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrComprasAtraso_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvComprasAtraso.DataSource = dtComprasAtraso;
                dgvComprasAtraso.Sort(dgvComprasAtraso.Columns["ATRASO_NUMERO"], ListSortDirection.Ascending);

                clsGridHelper.FontGrid(dgvComprasAtraso, 8);
                pbxAtraso.Visible = false;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pbxAtraso.Visible = false;
            }
        }

        private void dgvCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCompras.CurrentRow != null)
            {
               
                idCompras = clsParser.Int32Parse(dgvCompras.CurrentRow.Cells["ID"].Value.ToString());
                idfornecedor = clsParser.Int32Parse(dgvCompras.CurrentRow.Cells["COMPRAS_IDFORNECEDOR"].Value.ToString());

                bwrCompras1_RunWorkerAsync();
                bwrComprasAtraso_RunWorkerAsync();
            }           
        }

        private void dgvCompras_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvCompras.CurrentRow != null)
            {

                idCompras = clsParser.Int32Parse(dgvCompras.CurrentRow.Cells["ID"].Value.ToString());
                idfornecedor = clsParser.Int32Parse(dgvCompras.CurrentRow.Cells["COMPRAS_IDFORNECEDOR"].Value.ToString());

                bwrCompras1_RunWorkerAsync();
                bwrComprasAtraso_RunWorkerAsync();
            }           
        }

        private void tbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
        }

        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tbxPesquisa.Text, dgvCompras);
            clsGridHelper.SelecionaLinha(idCompras, dgvCompras, "COMPRAS_NUMERO");
            tbxPesquisa.Focus();
            if (dgvCompras.CurrentRow != null)
            {
                idCompras = clsParser.Int32Parse(dgvCompras.CurrentRow.Cells["COMPRAS_NUMERO"].Value.ToString());
            }
            else
            {
                idCompras = 0;
            }
        }
        
    }
}
