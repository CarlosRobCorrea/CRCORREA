using CRCorreaBLL;
using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmMovPecas : Form
    {
        Int32 idproduto;
        Int32 idunidadecompra;
        Int32 idunidadevenda;

        DataTable dtMovPecas;
        DataTable dtMovPecasMes;
        DataTable dtMovPecasCompras;

        //DataGridView rows;
        DataGridViewRowCollection rows;

        clsMovPecasBLL MovpecasBLL;
        clsMovPecasMesBLL MovPecasMesBLL;
        clsNfCompra1BLL NfCompra1BLL;

        
        
         
        ParameterFields pfields = new ParameterFields();

        String cabecalho;

        public frmMovPecas()
        {
            InitializeComponent();
        }

        public void Init(Int32 idproduto,
                         DataGridViewRowCollection _rows)
        {
            this.idproduto = idproduto;
            this.rows = _rows;

            MovpecasBLL = new clsMovPecasBLL();
            MovPecasMesBLL = new clsMovPecasMesBLL();
            NfCompra1BLL = new clsNfCompra1BLL();

            
            

            tbxDataDe.Text = "01/01/" + DateTime.Today.Year.ToString();
            tbxDataAte.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");

            if (idproduto > 0)
            {
                tbxPecasCodigo.ReadOnly = true;
                tbxPecasCodigo.TabStop = false;
                tbxPecasCodigo.BackColor = Color.LemonChiffon;
                btnPecas.Enabled = false;
            }
            else
            {
                tbxPecasCodigo.ReadOnly = false;
                tbxPecasCodigo.TabStop = true;
                tbxPecasCodigo.BackColor = Color.LightGray;
                btnPecas.Enabled = true;
            }
        }

        private void frmMovPecas_Load(object sender, EventArgs e)
        {
            CarregaMovPecas();            
        }

        private void frmMovPecas_Activated(object sender, EventArgs e)
        {
            
        }


        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CarregaMovPecas()
        {
            try
            {
                //Carrega os Dados
                PreecheCamposMovPecas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tbxDataDe.Select();
                tbxDataDe.SelectAll();
            }
        }

        private void PreecheCamposMovPecas()
        {
            Fill_Pecas();
        }

        private void CarregaGridMovPecas()
        {
            try
            {
                dtMovPecas = MovpecasBLL.GridDados(idproduto, clsParser.DateTimeParse(tbxDataDe.Text), clsParser.DateTimeParse(tbxDataAte.Text).AddDays(1), clsInfo.conexaosqldados);

                MovpecasBLL.gridDadosMonta(dgvMovPecas, dtMovPecas);
                
                clsGridHelper.FontGrid(dgvMovPecas, 7);

                if (tbxUnidVenda.Text.Trim() == "KG" ||
                    tbxUnidVenda.Text.Trim() == "MT" ||
                    tbxUnidVenda.Text.Trim() == "LT" ||
                    tbxUnidVenda.Text.Trim() == "M2" ||
                    tbxUnidVenda.Text.Trim() == "M3")
                {
                    dgvMovPecas.Columns["QTDE"].DefaultCellStyle.Format = "N3";
                    dgvMovPecas.Columns["QTDESAIDA"].DefaultCellStyle.Format = "N3";
                    dgvMovPecas.Columns["SALDO"].DefaultCellStyle.Format = "N3";
                }
                else
                {
                    dgvMovPecas.Columns["QTDE"].DefaultCellStyle.Format = "N0";
                    dgvMovPecas.Columns["QTDESAIDA"].DefaultCellStyle.Format = "N0";
                    dgvMovPecas.Columns["SALDO"].DefaultCellStyle.Format = "N0";
                }

                dgvMovPecas.Columns["VALOR"].DefaultCellStyle.Format = "N6";
                dgvMovPecas.Columns["VALORTOTAL"].DefaultCellStyle.Format = "N4";
                dgvMovPecas.Columns["VALORACUMULADO"].DefaultCellStyle.Format = "N4";
                dgvMovPecas.Columns["CUSTOMEDIO"].DefaultCellStyle.Format = "N6";

                CarregaGridMovPecasMes();
                CarregaGridMovPecasCompras();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGridMovPecasMes()
        {
            try
            {
                dtMovPecasMes = MovPecasMesBLL.GridDados(idproduto, clsInfo.conexaosqldados);
                //
                dgvResumoMovimento.DataSource = dtMovPecasMes;
                clsGridHelper.MontaGrid(dgvResumoMovimento,


                                    new String[] { "ID", "IDCODIGO", "Ano/Mês", "Qt Inicio", "Qt Entra", "Qt Saída", "Qt Saldo", "Vl. Acumu.", "Vl. Médio" },

                                    new String[] { "ID", "IDCODIGO", "ANOMES", "QTDEINICIO", "QTDEENTRA", "QTDESAIDA",
                                                   "QTDESALDO", "VALORACUMULADO", "CUSTOMEDIOMES"},

                                    new Int32[] { 0, 0, 50, 68, 68, 68, 68, 70, 70 },

                                    new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,                                    
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,                                                                       
                                    DataGridViewContentAlignment.MiddleRight},

                                    new Boolean[] { false, false, true, true, true, true, true, true, true },
                                    true, 2, ListSortDirection.Descending);
                clsGridHelper.FontGrid(dgvResumoMovimento, 7);
                if (tbxUnidVenda.Text.Trim() == "KG" || tbxUnidVenda.Text.Trim() == "MT" || tbxUnidVenda.Text.Trim() == "LT" ||
                    tbxUnidVenda.Text.Trim() == "M2" || tbxUnidVenda.Text.Trim() == "M3")
                {
                    dgvResumoMovimento.Columns["QTDEINICIO"].DefaultCellStyle.Format = "N3";
                    dgvResumoMovimento.Columns["QTDEENTRA"].DefaultCellStyle.Format = "N3";
                    dgvResumoMovimento.Columns["QTDESAIDA"].DefaultCellStyle.Format = "N3";                                        
                    dgvResumoMovimento.Columns["QTDESALDO"].DefaultCellStyle.Format = "N3";
                }
                else
                {
                    dgvResumoMovimento.Columns["QTDEINICIO"].DefaultCellStyle.Format = "N0";                    
                    dgvResumoMovimento.Columns["QTDEENTRA"].DefaultCellStyle.Format = "N0";
                    dgvResumoMovimento.Columns["QTDESAIDA"].DefaultCellStyle.Format = "N0";
                    dgvResumoMovimento.Columns["QTDESALDO"].DefaultCellStyle.Format = "N0";
                }                
                dgvResumoMovimento.Columns["VALORACUMULADO"].DefaultCellStyle.Format = "N2";
                dgvResumoMovimento.Columns["CUSTOMEDIOMES"].DefaultCellStyle.Format = "N3";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CarregaGridMovPecasCompras()
        {
            try
            {
                dtMovPecasCompras = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "NFCOMPRA " +
                "INNER JOIN NFCOMPRA1 ON NFCOMPRA.ID = NFCOMPRA1.NUMERO " +
                "INNER JOIN CLIENTE ON CLIENTE.ID = NFCOMPRA.IDFORNECEDOR ",
                "NFCOMPRA1.IDCODIGO, NFCOMPRA.NUMERO,NFCOMPRA.DATARECEBIMENTO AS DATA," +
                "NFCOMPRA.SITUACAO,NFCOMPRA1.TIPOPRODUTO,CLIENTE.COGNOME,NFCOMPRA1.QTDE AS [QTDE1]," +
                "NFCOMPRA1.PRECO AS [PRECO1],NFCOMPRA1.TOTALMERCADO AS [TOTALMERCADO1]",
                "NFCOMPRA1.IDCODIGO = " + clsParser.SqlInt32Format(idproduto, true), "NFCOMPRA.DATA");
                //
                dgvPecasUltimasCompras.DataSource = dtMovPecasCompras;
                clsGridHelper.MontaGrid(dgvPecasUltimasCompras,
                                    new String[] { "Id.", "NF", "Data", "Sit.", "Tp", "Fornecedor", "Qtde", "Preço", "Total" },

                                    new String[] { "IDCODIGO", "NUMERO", "DATA", "SITUACAO", "TIPOPRODUTO", "COGNOME", "QTDE1", "PRECO1", "TOTALMERCADO1" },

                                    new Int32[] { 0, 45, 65, 30, 30, 80, 60, 77, 75 },

                                    new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleCenter,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight,
                                    DataGridViewContentAlignment.MiddleRight},

                                    new Boolean[] { false, true, true, true, true, true, true, true, true },
                                    true, 2, ListSortDirection.Descending);
                clsGridHelper.FontGrid(dgvPecasUltimasCompras, 7);
                if (tbxUnidVenda.Text.Trim() == "KG" || tbxUnidVenda.Text.Trim() == "MT" || tbxUnidVenda.Text.Trim() == "LT" ||
                    tbxUnidVenda.Text.Trim() == "M2" || tbxUnidVenda.Text.Trim() == "M3")
                {
                    dgvPecasUltimasCompras.Columns["QTDE1"].DefaultCellStyle.Format = "N3";
                }
                else
                {
                    dgvPecasUltimasCompras.Columns["QTDE1"].DefaultCellStyle.Format = "N0";
                }
                dgvPecasUltimasCompras.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPecasUltimasCompras.Columns["PRECO1"].DefaultCellStyle.Format = "N6";
                dgvPecasUltimasCompras.Columns["TOTALMERCADO1"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CamposMovPecas(Object control, EventArgs _e)
        {
            Control ctl = (Control)control;

            if (ctl.Name == tbxPecasCodigo.Name)
            {
                if (Check_Pecas() == true)
                {
                    Fill_Pecas();
                }
            }
            if (ctl.Name == tbxDataDe.Name)
            {
                    Fill_Pecas();
            }
            if (ctl.Name == tbxDataAte.Name)
            {
                    Fill_Pecas();
            }

        }

        private Boolean Check_Pecas()
        {
            Int32 idtemp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + tbxPecasCodigo.Text + "'","0"));

            if (idtemp == 0)
            {
                idtemp = clsInfo.zpecas;
            }
            else if (idtemp != idproduto)
            {
                idproduto = idtemp;
            }
            else
            {
                return false;
            }

            return true;
        }

        private void Fill_Pecas()
        {
            DataTable PecasCampos = Procedure.RetornaDataTable(clsInfo.conexaosqldados, "select * from pecas where id = " + idproduto + "order by id ");
            //DataTable PecasCampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "PECAS", "*", "ID = " + idproduto.ToString(), "ID");

            if (PecasCampos.Rows.Count > 0)
            {
                tbxPecasCodigo.Text = PecasCampos.Rows[0]["codigo"].ToString();
                tbxPecasDescricaoPeca.Text = PecasCampos.Rows[0]["nome"].ToString();

                idunidadecompra = clsParser.Int32Parse(PecasCampos.Rows[0]["idunidadecom"].ToString());
                if (idunidadecompra > 0) tbxUnidCompra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID = "+ idunidadecompra,"0");                
                //if (idunidadecompra > 0) tbxUnidCompra.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "UNIDADE", "CODIGO", "ID", idunidadecompra);
                
                idunidadevenda = clsParser.Int32Parse(PecasCampos.Rows[0]["idunidade"].ToString());
                if (idunidadecompra > 0) tbxUnidVenda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID = " + idunidadevenda, "0");
                //if (idunidadecompra > 0) tbxUnidVenda.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "UNIDADE", "CODIGO", "ID", idunidadevenda);

                tbxLocalArmazenagem.Text = PecasCampos.Rows[0]["locacao"].ToString();
                
                if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "A") tbxPecasTipoMaterial.Text = "A = Produto Venda";
                else if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "B") tbxPecasTipoMaterial.Text = "B = Produto Beneficiado";
                else if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "C") tbxPecasTipoMaterial.Text = "C = Conjunto Montado";
                else if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "D") tbxPecasTipoMaterial.Text = "D = Despesas";
                else if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "F") tbxPecasTipoMaterial.Text = "F = Dispositivos";
                else if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "I") tbxPecasTipoMaterial.Text = "I = Instrumentos Medição";
                else if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "K") tbxPecasTipoMaterial.Text = "K = Materiais Uso Kan-Ban";
                else if (PecasCampos.Rows[0]["tipoproduto"].ToString().Trim() == "M") tbxPecasTipoMaterial.Text = "M = Materiais Uso e Consumo";

                if (PecasCampos.Rows[0]["ativo"].ToString().Trim() == "S") rbnAtivo.Checked = true;
                else if (PecasCampos.Rows[0]["ativo"].ToString().Trim() == "N") rbnInativo.Checked = true;
                else if (PecasCampos.Rows[0]["ativo"].ToString().Trim() == "R") rbnRevisao.Checked = true;
            }
            else
            {
                tbxPecasCodigo.Text = "";
                tbxPecasDescricaoPeca.Text = "";
                tbxUnidCompra.Text = "";
                tbxUnidVenda.Text = "";
            }

            CarregaGridMovPecas();
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            CamposMovPecas(sender,e);                        
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);                        
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    idproduto = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    CarregaMovPecas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == idproduto)
                        {
                            if (row.Index != 0)
                            {
                                idproduto = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                CarregaMovPecas();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == idproduto)
                        {
                            if (row.Index < rows.Count - 1)
                            {
                                idproduto = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                CarregaMovPecas();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    idproduto = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    CarregaMovPecas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            cabecalho = tbxPecasCodigo.Text + " " + tbxPecasDescricaoPeca.Text;

            DataTable Rel = new DataTable();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESA", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);
            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);

            Rel = dtMovPecas;
            // Imprimir 
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            //frmCrystalReport.Init(clsInfo.caminhorelatorios, "RESOL_MOVPECAS.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
        }     
    }
}
