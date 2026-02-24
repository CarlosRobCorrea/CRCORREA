using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmComprasVis : Form
    {

        clsBasicReport clsBr;

        SqlConnection scn;
        SqlCommand scd;

        BackgroundWorker bwrCompras;

        Int32 id;
        String filtro_status;

        static DataTable dtCompras;

        Boolean carregandoCompras;

        DataTable dtCompras1;

        DataTable dtComprasAcumula;
        String query;
        Decimal valorcalculado;

        public frmComprasVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBr = new clsBasicReport(this, dgvCompras, ttpCompras, gbxCompras);
        }

        private void frmComprasVis_Load(object sender, EventArgs e)
        {
            nudAnoDe.Value = DateTime.Now.Year - 1;
            nudAnoAte.Value = DateTime.Now.Year;

        }

        private void frmComprasVis_Activated(object sender, EventArgs e)
        {
            bwrCompras_Run();
            bwrComprasAcumula_RunWorkerAsync();                           
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
            bwrCompras_Run();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmCompras frmCompras = new frmCompras();
            frmCompras.Init(0, dgvCompras.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmCompras, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvCompras.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvCompras.CurrentRow.Cells["ID"].Value.ToString());
                frmCompras frmCompras = new frmCompras();
                frmCompras.Init(id, dgvCompras.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmCompras, clsInfo.conexaosqldados);

                //frmCotacaoAprova frmCotacaoAprova = new frmCotacaoAprova();
                //frmCotacaoAprova.Init(id, dgvCompras.Rows);
                //clsFormHelper.AbrirForm(this.MdiParent, frmCotacaoAprova, clsInfo.conexaosqldados);
            }
        }

        private void tspImprimirMnu_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Pedidos de Compras", clsInfo.conexaosqldados);
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            if (dgvCompras.CurrentRow != null)
            {
                //DataSets.DADOS.DataReports PedidoCompras = new DataSets.DADOS.DataReports();
                //PedidoCompras.PedidoCompra((Int32)dgvCompras.CurrentRow.Cells[0].Value, clsInfo.conexaosqldados, this);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void bwrCompras_DoWork(object sender, DoWorkEventArgs e)
        {
            if (rbnTodos.Checked == true)
            {
                filtro_status = "T";
            }
            else if (rbnEmAberto.Checked == true)
            {
                filtro_status = "A";
            }
            else if (rbnFechado.Checked == true)
            {
                filtro_status = "S";
            }
            dtCompras = clsComprasBLL.GridDados(clsInfo.zfilial, Int32.Parse(nudAnoDe.Value.ToString()), Int32.Parse(nudAnoAte.Value.ToString()), filtro_status);
        }
        private void bwrCompras_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //pbxCompras.Visible = false;
                clsComprasBLL.GridMonta(dgvCompras, dtCompras, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvCompras, 1);
                //tbxQtdeCompras.Text = dtCompras.Rows.Count.ToString("N0");

                Filtrar();

                carregandoCompras = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoCompras = false;
            }
        }

        private void rbnTodos_Click(object sender, EventArgs e)
        {
            bwrCompras_Run();
        }

        private void dgvCompras_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void dgvCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCompras.CurrentRow != null)
                {
                    id = Int32.Parse(dgvCompras.CurrentRow.Cells[0].Value.ToString());
                    bwrCompras1_Run();
                }
            }
            catch
            {

            }
        }

        private void bwrCompras1_Run()
        {
                pbxCompras1.Visible = false;
                bwrCompras1 = new BackgroundWorker();
                bwrCompras1.DoWork += new DoWorkEventHandler(bwrCompras1_DoWork);
                bwrCompras1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCompras1_RunWorkerCompleted);
                bwrCompras1.RunWorkerAsync();
        }

        private void bwrCompras1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtCompras1 = clsCompras1BLL.GridDados(id);
        }

        private void bwrCompras1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsCompras1BLL.GridMonta(dgvCompras1, dtCompras1, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvCompras1, 1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                
            }
        }

        private void bwrComprasAcumula_RunWorkerAsync()
        {
            scn = new SqlConnection(clsInfo.conexaosqldados);
            query = "SELECT SUM((COMPRAS.TOTALPREVISTO)) FROM COMPRAS " +
                    " WHERE COMPRAS.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString());
 
            scd = new SqlCommand(query, scn);
            scn.Open();
            valorcalculado = clsParser.DecimalParse(scd.ExecuteScalar().ToString());
            tbxTotalvalorpendente.Text = valorcalculado.ToString("N2");
            scn.Close();

            bwrComprasAcumula = new BackgroundWorker();
            bwrComprasAcumula.DoWork += new DoWorkEventHandler(bwrComprasAcumula_DoWork);
            bwrComprasAcumula.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrComprasAcumula_RunWorkerCompleted);
            bwrComprasAcumula.RunWorkerAsync();
        }

        private void bwrComprasAcumula_DoWork(object sender, DoWorkEventArgs e)
        {
            dtComprasAcumula = CarregaGridComprasAcumula();
        }
         
        public DataTable CarregaGridComprasAcumula()
        {
            if (valorcalculado != 0)
            {
                try
                {
                    //por cliente
                    query = "SELECT CLIENTE.COGNOME, " +
                            "SUM(COMPRAS.TOTALPREVISTO) AS [SALDODEVEDOR], " +
                            "(((SUM(COMPRAS.TOTALPREVISTO) * 100) / CONVERT(DECIMAL(12,2), '" + valorcalculado.ToString().Replace(',', '.') + "'))) AS PORCENTAGEM " +
                            "from COMPRAS " +
                            "LEFT JOIN CLIENTE ON CLIENTE.ID=COMPRAS.IDFORNECEDOR " +
                            " WHERE COMPRAS.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";

                    query = query + " GROUP BY CLIENTE.COGNOME";
                    SqlDataAdapter sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                    //scd.Parameters.Add("@id", SqlDbType.Int).Value = idPagar;
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
            else
            {
                return null;
            }
        }

        private void bwrComprasAcumula_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dtComprasAcumula != null)
            {
                try
                {

                    dgvComprasAcumula.DataSource = dtComprasAcumula;
                    clsGridHelper.MontaGrid(dgvComprasAcumula,
                            new String[] { "Fornecedor", "Saldo Devedor", "%" },
                            new String[] { "COGNOME", "SALDODEVEDOR", "PORCENTAGEM" },
                            new int[] { 120, 80, 50 },
                            new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleRight},
                            new bool[] { true, true, true },
                            true, 2, ListSortDirection.Descending);
                    clsGridHelper.FontGrid(dgvComprasAcumula, 7);

                    dgvComprasAcumula.Columns["SALDODEVEDOR"].DefaultCellStyle.Format = "N2";
                    dgvComprasAcumula.Columns["PORCENTAGEM"].DefaultCellStyle.Format = "N2";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }       

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            Filtrar();
        }

        void Filtrar()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvCompras);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String query = "";
            SqlDataAdapter sda;
            query = "SELECT " +
                        "ID " +
                    "FROM " +
                        "COMPRASENTREGA ";

            clsComprasEntregaBLL ComprasEntregaBLL;
            clsComprasEntregaInfo ComprasEntregaInfo;

            ComprasEntregaBLL = new clsComprasEntregaBLL();
            ComprasEntregaInfo = ComprasEntregaBLL.Carregar(0, clsInfo.conexaosqldados);
            
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                ComprasEntregaInfo = ComprasEntregaBLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), clsInfo.conexaosqldados);
                ComprasEntregaBLL.Alterar(ComprasEntregaInfo, clsInfo.conexaosqldados);
            }
        }

        private void tspImprimirMnu_Click_1(object sender, EventArgs e)
        {
            frmRelCompra frmRelCompra = new frmRelCompra();
            frmRelCompra.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelCompra, clsInfo.conexaosqldados);
        }
    }
}
