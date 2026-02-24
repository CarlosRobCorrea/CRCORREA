using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmCompras : Form
    {

        ParameterFields pfields = new ParameterFields();

        clsComprasBLL clsComprasBLL;
        clsComprasInfo clsComprasInfo;
        clsComprasInfo clsComprasInfoOld;

        DataGridViewRowCollection rows;
        DataGridViewRowCollection rowsitens;
        // compras
        Int32 id;
        Int32 filial;
        Int32 compras_idautorizante;
        Int32 compras_idcontato;
        Int32 compras_idcomprador;
        Int32 compras_idemitente;
        Int32 compras_idfornecedor;
        Int32 compras_idformapagto;
        Int32 compras_idcondpagto;
        Int32 compras_idtransportadora;

        // compras1
        DataTable dtCompras1;

        clsCompras1BLL clsCompras1BLL;
        clsCompras1Info clsCompras1Info;
        clsCompras1Info clsCompras1InfoOld;

        Int32 compras1_id;
        Int32 compras1_idcompras;
        Int32 compras1_posicao;
        Int32 compras1_idcentrocusto;
        Int32 compras1_idcfop;
        Int32 compras1_idcfopfis;
        Int32 compras1_idcodigo;
        Int32 compras1_idcodigo1;
        Int32 compras1_idcodigoctabil;
        Int32 compras1_idcotacao;
        Int32 compras1_idcotacaoitem;
        Int32 compras1_idcotacao2;
        Int32 compras1_iddestino;
        Int32 compras1_idhistorico;
        Int32 compras1_idipi;
        Int32 compras1_idos;
        Int32 compras1_idsittriba;
        Int32 compras1_idsittribb;
        Int32 compras1_idsittribipi;
        Int32 compras1_idsittribpis;
        Int32 compras1_idsittribcofins1;
        Int32 compras1_idsolicitacao;
        Int32 compras1_idtiponota;
        Int32 compras1_idunid;
        Int32 compras1_idunidfiscal;

        // comprasentrega
        DataTable dtComprasEntrega;

        clsComprasEntregaBLL clsComprasEntregaBLL;
        clsComprasEntregaInfo clsComprasEntregaInfo;
        clsComprasEntregaInfo clsComprasEntregaInfoOld;

        Int32 comprasentrega_id;
        Int32 comprasentrega_posicao;
        Int32 comprasentrega_idcompras;
        Int32 comprasentrega_idcompras1;
        Int32 comprasentrega_idos;

        String tipoproduto;

        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();



        public frmCompras()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;

            clsComprasBLL = new clsComprasBLL(); 

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxCliente_Cognome);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from situacaotipotitulo order by codigo", tbxFormaPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + '=' + nome from condpagto where ativo = 's' order by codigo", tbxCondPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxCognomeTransporte);
            
            //compras1
            clsCompras1BLL = new clsCompras1BLL(); 

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", Compras1_tbxPecas_codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cfop from cfop order by cfop", Compras1_tbxCfopfis);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cfop from cfop order by cfop", Compras1_tbxCfop);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", Compras1_tbxCentrocusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", Compras1_tbxHistoricos_codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", Compras1_tbxPecas_codigo2);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from unidade order by codigo", Compras1_tbxUnidadeFiscal);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from unidade order by codigo", Compras1_tbxUnidade);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from ipi order by codigo", Compras1_tbxIpi_codigo);

            clsVisual.FillComboBox(Compras1_cbxTiponota, "select codigo + ' = ' + nome from tiponota order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Compras1_cbxSittriba, "select codigo from sittributariaa order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Compras1_cbxSittribb, "select codigo from sittributariab order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Compras1_cbxSittribipi, "select codigo from sittribipi order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Compras1_cbxSittribpis, "select codigo from sittribpis order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(Compras1_cbxSittribcofins1, "select codigo from sittribcofins order by codigo", clsInfo.conexaosqldados);


            Compras1_cbxSittriba.SelectedIndex = 0;
            Compras1_cbxSittribb.SelectedIndex = 0;
            Compras1_cbxSittribipi.SelectedIndex = 0;
            Compras1_cbxSittribpis.SelectedIndex = 0;
            Compras1_cbxSittribcofins1.SelectedIndex = 0;

            //comprasentrega
            clsComprasEntregaBLL = new clsComprasEntregaBLL(); 

        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            ComprasCarregar();
        }

        private void frmCompras_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Salvar() == DialogResult.Cancel)
                {
                    return;
                }

                DataGridView dgv = new DataGridView();
                DataGridViewTextBoxColumn dgvC = new DataGridViewTextBoxColumn();
                
                dgvC.Name = "ID";
                dgvC.HeaderText = "ID";

                dgv.Columns.Add(dgvC);
                dgv.Rows.Add(new Object[] { id });

                clsInfo.zrow = dgv.Rows[0];

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message + "/nStack Stace: " + ex.StackTrace, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    ComprasCarregar();
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
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                ComprasCarregar();
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
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                ComprasCarregar();
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
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    ComprasCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            ComprasSalvar();
            // Imprimir Pedido Compra

            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = id;
            field.Name = "id";
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            DialogResult resultado1;
            resultado1 = MessageBox.Show("Deseja Imprimir Foto no Pedido Compra ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (resultado1 == DialogResult.Yes)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "PEDIDOCOMPRA_FOTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else if (resultado1 == DialogResult.Cancel)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "PEDIDOCOMPRA_FOTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "PEDIDOCOMPRA_FOTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                tspSalvar.PerformClick();
            }
            else
            {
                if (id > 0)
                {
                    DataGridView dgv = new DataGridView();
                    DataGridViewTextBoxColumn dgvC = new DataGridViewTextBoxColumn();

                    dgvC.Name = "ID";
                    dgvC.HeaderText = "ID";

                    dgv.Columns.Add(dgvC);
                    dgv.Rows.Add(new Object[] { id });

                    clsInfo.zrow = dgv.Rows[0];
                }

                this.Close();
            }

        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);

            ComprasEntregaSomar();
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
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1 &&
                clsInfo.znomegrid != null &&
                clsInfo.znomegrid != "")
            {
                // ###############################
                // Verifica os botões de pesquisa
                // ###############################

                if (clsInfo.znomegrid == btnIdCliente.Name)  // FORNECEDOR
                {
                    tbxCliente_Cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();

                    if (Check_Fornecedor() == true) // Detectou mudança de Fornecedor
                    {
                        Fill_Fornecedor();          // Carrega os dados do Fornecedor escolhido
                    }

                    tbxCliente_Cognome.Select();
                    tbxCliente_Cognome.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdComprador.Name)   // COMPRADOR
                {
                    tbxComprador.Text = clsInfo.zrow.Cells["USUARIO"].Value.ToString();

                    if (Check_Comprador() == true)
                    {
                        Fill_Comprador();
                    }

                    tbxComprador.Select();
                    tbxComprador.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdFormaPagto.Name)  // FORMA DE PAGAMENTO
                {
                    tbxFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxFormaPagto.Text += "=" + clsInfo.zrow.Cells["NOME"].Value.ToString();

                    if (Check_Formapagto() == true)
                    {
                        Fill_Formapagto();
                    }

                    tbxFormaPagto.Select();
                    tbxFormaPagto.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdCondPagto.Name)   // CONDIÇÃO DE PAGAMENTO
                {
                    tbxCondPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCondPagto.Text += "=" + clsInfo.zrow.Cells["NOME"].Value.ToString();

                    if (Check_Condpagto() == true)
                    {
                        Fill_Condpagto();
                    }

                    tbxCondPagto.Select();
                    tbxCondPagto.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdTransportadora.Name)  // TRANSPORTADORA
                {
                    tbxCognomeTransporte.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();

                    if (Check_Transportadora() == true)
                    {
                        Fill_Transportadora();
                    }

                    tbxCognomeTransporte.Select();
                    tbxCognomeTransporte.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnIdCodigo.Name)    // PRODUTO
                {
                    Compras1_tbxPecas_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Pecas() == true)
                    {
                        Fill_Pecas();
                    }

                    Compras1_tbxPecas_codigo.Select();
                    Compras1_tbxPecas_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnIdCodigo2.Name)   // PRODUTO DESTINO
                {
                    Compras1_tbxPecas_codigo2.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Pecas2() == true)
                    {
                        Fill_Pecas2();
                    }

                    Compras1_tbxPecas_codigo2.Select();
                    Compras1_tbxPecas_codigo2.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnIdcfopFiscal.Name)    // CFOP FISCAL
                {
                    Compras1_tbxCfopfis.Text = clsInfo.zrow.Cells["CFOP"].Value.ToString();

                    if (Check_CfopFis() == true)
                    {
                        Fill_CfopFis();
                    }

                    Compras1_tbxCfopfis.Select();
                    Compras1_tbxCfopfis.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnIdcfop.Name)  // CFOP
                {
                    Compras1_tbxCfop.Text = clsInfo.zrow.Cells["CFOP"].Value.ToString();

                    if (Check_Cfop() == true)
                    {
                        Fill_Cfop();
                    }

                    Compras1_tbxCfop.Select();
                    Compras1_tbxCfop.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnSitTriba.Name) // SIT. TRIB. A
                {
                    Compras1_cbxSittriba.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["codigo"].Value.ToString(), 0, Compras1_cbxSittriba);

                    if (Check_Sittriba() == true)
                    {
                        Fill_Sittriba();
                    }

                    Compras1_cbxSittriba.Select();
                }
                else if (clsInfo.znomegrid == Compras1_btnSitTribb.Name) // SIT. TRIB. B
                {
                    Compras1_cbxSittribb.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["codigo"].Value.ToString(), 0, Compras1_cbxSittriba);

                    if (Check_Sittribb() == true)
                    {
                        Fill_Sittribb();
                    }

                    Compras1_cbxSittribb.Select();
                }
                else if (clsInfo.znomegrid == Compras1_btnCentrocusto.Name)  // CENTRO DE CUSTO
                {
                    Compras1_tbxCentrocusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_CentroCusto() == true)
                    {
                        Fill_CentroCusto();
                    }

                    Compras1_tbxCentrocusto.Select();
                    Compras1_tbxCentrocusto.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnHistorico.Name)   // HISTORICO
                {
                    Compras1_tbxHistoricos_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Historico() == true)
                    {
                        Fill_Historico();
                    }

                    Compras1_tbxHistoricos_codigo.Select();
                    Compras1_tbxHistoricos_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnUnidade_codigo.Name)  // UNIDADE
                {
                    Compras1_tbxUnidade.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Unidade() == true)
                    {
                        Fill_Unidade();
                    }

                    Compras1_tbxUnidade.Select();
                    Compras1_tbxUnidade.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnUnidadeFis_codigo.Name)  // UNIDADE FISCAL
                {
                    Compras1_tbxUnidadeFiscal.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_UnidFiscal() == true)
                    {
                        Fill_UnidFiscal();
                    }

                    Compras1_tbxUnidadeFiscal.Select();
                    Compras1_tbxUnidadeFiscal.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnClassfiscal.Name) // CLASS. FISCAL
                {
                    Compras1_tbxIpi_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_ClassFiscal() == true)
                    {
                        Fill_ClassFiscal();
                    }

                    Compras1_tbxIpi_codigo.Select();
                    Compras1_tbxIpi_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnSittribipi.Name)   // CST IPI
                {
                    Compras1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["CODIGO"].Value.ToString(), 0, Compras1_cbxSittribipi);

                    if (Check_Sittribipi() == true)
                    {
                        Fill_Sittribipi();
                    }

                    Compras1_cbxSittribipi.Select();
                    Compras1_cbxSittribipi.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnSittribpis.Name)   // CST PIS
                {
                    Compras1_cbxSittribpis.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["CODIGO"].Value.ToString(), 0, Compras1_cbxSittribpis);

                    if (Check_Sittribpis() == true)
                    {
                        Fill_Sittribpis();
                    }

                    Compras1_cbxSittribpis.Select();
                    Compras1_cbxSittribpis.SelectAll();
                }
                else if (clsInfo.znomegrid == Compras1_btnSittribcofins1.Name)   // CST COFINS
                {
                    Compras1_cbxSittribcofins1.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["CODIGO"].Value.ToString(), 0, Compras1_cbxSittribcofins1);

                    if (Check_Sittribcofins() == true)
                    {
                        Fill_Sittribcofins();
                    }

                    Compras1_cbxSittribcofins1.Select();
                    Compras1_cbxSittribcofins1.SelectAll();
                }
            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################

                if (ctl.Name == tbxCliente_Cognome.Name)    // FORNECEDOR
                {
                    if (Check_Fornecedor() == true) // Detectou mudança de Fornecedor
                    {
                        Fill_Fornecedor();          // Carrega os dados do Fornecedor escolhido
                    }
                }
                else if (ctl.Name == tbxSetorfator.Name) // COMPRADOR
                {
                    cbxSituacao.Select();
                }

                else if (ctl.Name == tbxComprador.Name) // COMPRADOR
                {
                    if (Check_Comprador() == true)
                    {
                        Fill_Comprador();
                    }
                    tbxFormaPagto.Select();
                }
                else if (ctl.Name == tbxFormaPagto.Name)    // FORMA DE PAGAMENTO
                {
                    if (Check_Formapagto() == true)
                    {
                        Fill_Formapagto();
                    }
                }
                else if (ctl.Name == tbxCondPagto.Name)     // CONDIÇÃO DE PAGAMENTO
                {
                    if (Check_Condpagto() == true)
                    {
                        Fill_Condpagto();
                    }
                }
                else if (ctl.Name == tbxCognomeTransporte.Name) // TRANSPORTADORA
                {
                    if (Check_Transportadora() == true)
                    {
                        Fill_Transportadora();
                    }
                }
                else if (ctl.Name == Compras1_tbxPecas_codigo.Name) // PRODUTO
                {
                    if (Check_Pecas() == true)
                    {
                        Fill_Pecas();
                    }
                }
                else if (ctl.Name == Compras1_tbxPecas_codigo2.Name)    // PRODUTO DESTINO
                {
                    if (Check_Pecas2() == true)
                    {
                        Fill_Pecas2();
                    }
                }
                else if (ctl.Name == Compras1_cbxTiponota.Name) // TIPO DA NF
                {
                    String tipoNota = Compras1_cbxTiponota.Text.PadRight(8, ' ').Substring(0, 8);
                    Compras1_cbxTiponota.SelectedIndex = clsVisual.SelecionarIndex(tipoNota, 8, Compras1_cbxTiponota);
                    if (Compras1_cbxTiponota.SelectedIndex == -1)
                    {
                        Compras1_cbxTiponota.SelectedIndex = 0;
                    }
                    TipoNotaCarregar();

                }
                else if (ctl.Name == Compras1_tbxCfopfis.Name)  // CFOP FISCAL
                {
                    if (Check_CfopFis() == true)
                    {
                        Fill_CfopFis();
                    }
                }
                else if (ctl.Name == Compras1_tbxCfop.Name) // CFOP
                {
                    if (Check_Cfop() == true)
                    {
                        Fill_Cfop();
                    }
                }
                else if (ctl.Name == Compras1_cbxSittriba.Name) // SIT. TRIB. A
                {
                    if (Check_Sittriba() == true)
                    {
                        Fill_Sittriba();
                    }
                }
                else if (ctl.Name == Compras1_cbxSittribb.Name) // SIT. TRIB. B
                {
                    if (Check_Sittribb() == true)
                    {
                        Fill_Sittribb();
                    }
                }
                else if (ctl.Name == Compras1_tbxCentrocusto.Name)  // CENTRO DE CUSTO
                {
                    if (Check_CentroCusto() == true)
                    {
                        Fill_CentroCusto();
                    }
                }
                else if (ctl.Name == Compras1_tbxHistoricos_codigo.Name)    // HISTORICO
                {
                    if (Check_Historico() == true)
                    {
                        Fill_Historico();
                    }
                }
                else if (ctl.Name == Compras1_tbxUnidade.Name)  // UNIDADE
                {
                    if (Check_Unidade() == true)
                    {
                        Fill_Unidade();
                    }
                }
                else if (ctl.Name == Compras1_tbxUnidadeFiscal.Name)  // UNIDADE FISCAL
                {
                    if (Check_UnidFiscal() == true)
                    {
                        Fill_UnidFiscal();
                    }
                }
                else if (ctl.Name == Compras1_tbxIpi_codigo.Name)   // CLASS. FISCAL
                {
                    if (Check_ClassFiscal() == true)
                    {
                        Fill_ClassFiscal();
                    }
                }
                else if (ctl.Name == Compras1_cbxSittribipi.Name)   // CST IPI
                {
                    if (Check_Sittribipi() == true)
                    {
                        Fill_Sittribipi();
                    }
                }
                else if (ctl.Name == Compras1_cbxSittribpis.Name)   // CST PIS
                {
                    if (Check_Sittribpis() == true)
                    {
                        Fill_Sittribpis();
                    }
                }
                else if (ctl.Name == Compras1_cbxSittribcofins1.Name)   // CST COFINS
                {
                    if (Check_Sittribcofins() == true)
                    {
                        Fill_Sittribcofins();
                    }
                }
                else if (tclCompras.SelectedIndex == 1)
                {
                    if (ctl.Name == Compras1_tbxQtdeFiscal.Name)
                    {
                        if (clsParser.DecimalParse(Compras1_tbxQtde.Text) <= 0)
                        {
                            Compras1_tbxQtdeFiscal.Text = clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text).ToString("N3");
                            Compras1_tbxQtde.Text = Compras1_tbxQtdeFiscal.Text;
                        }

                        Calcular();
                    }
                    else if (ctl.Name == Compras1_tbxPreco.Name ||
                             ctl.Name == Compras1_cbxSittriba.Name ||
                             ctl.Name == Compras1_cbxSittribb.Name ||
                             ctl.Name == Compras1_tbxIcm.Name ||
                             ctl.Name == Compras1_tbxBasemp.Name ||
                             ctl.Name == Compras1_tbxIcminterno.Name ||
                             ctl.Name == Compras1_tbxIcmssubstreducao.Name ||
                             ctl.Name == Compras1_tbxReducao.Name ||
                             ctl.Name == Compras1_tbxIpi.Name ||
                             ctl.Name == Compras1_tbxAliqpispasep.Name ||
                             ctl.Name == Compras1_tbxAliqcofins1.Name)
                    {
                        Calcular();
                    }
                    if (ctl.Name == Compras1_tbxQtde.Name)
                    {
                        Compras1_tbxQtde.Text = clsParser.DecimalParse(Compras1_tbxQtde.Text).ToString("N3");
                        Calcular();
                    }
                    else if (ctl.Name == Compras1_tbxPreco.Name ||
                             ctl.Name == Compras1_cbxSittriba.Name ||
                             ctl.Name == Compras1_cbxSittribb.Name ||
                             ctl.Name == Compras1_tbxIcm.Name ||
                             ctl.Name == Compras1_tbxBasemp.Name ||
                             ctl.Name == Compras1_tbxIcminterno.Name ||
                             ctl.Name == Compras1_tbxIcmssubstreducao.Name ||
                             ctl.Name == Compras1_tbxReducao.Name ||
                             ctl.Name == Compras1_tbxIpi.Name ||
                             ctl.Name == Compras1_tbxAliqpispasep.Name ||
                             ctl.Name == Compras1_tbxAliqcofins1.Name)
                    {
                        Calcular();
                    }

                    else if (ctl.Name == ComprasEntrega_tbxQtdeentrega.Name)
                    {
                        ComprasEntrega_tbxQtdeentrega.Text = (clsParser.DecimalParse(ComprasEntrega_tbxQtdeentrega.Text)).ToString("N3");

                        ComprasEntrega_tbxQtdesaldo.Text = (clsParser.DecimalParse(ComprasEntrega_tbxQtdeentrega.Text) -
                            (clsParser.DecimalParse(ComprasEntrega_tbxQtdebaixada.Text) +
                             clsParser.DecimalParse(ComprasEntrega_tbxQtdedefeito.Text) +
                             clsParser.DecimalParse(ComprasEntrega_tbxQtdeentregue.Text) +
                             clsParser.DecimalParse(ComprasEntrega_tbxQtdeosaux.Text) +
                             clsParser.DecimalParse(ComprasEntrega_tbxQtdesucata.Text))).ToString("N3");

                        Compras1_tbxTotalPrevisto.Text = (clsParser.DecimalParse(Compras1_tbxPreco.Text) * clsParser.DecimalParse(Compras1_tbxQtdesaldo.Text)).ToString("N2");
                    }
                    else if (ctl.Name == Compras1_lbxTipoProduto.Name)
                    {
                        if (Compras1_lbxTipoProduto.Text.Substring(0, 2) == "30")
                        {
                            gbxPrestacaoServico.Enabled = true;
                        }
                        else
                        {
                            gbxPrestacaoServico.Enabled = false;
                            Compras1_ckxIrrf.Checked = false;
                            Compras1_ckxInss.Checked = false;
                            Compras1_ckxPisCofinsCsll.Checked = false;
                            Compras1_ckxIss.Checked = false;
                            Compras1_ckxPis.Checked = false;
                            Compras1_ckxCofins.Checked = false;
                            Compras1_ckxCsll.Checked = false;
                        }
                        Compras1_tbxQtdeFiscal.Select();
                        Compras1_tbxQtdeFiscal.SelectAll();
                    }
                }
            }

            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private Boolean Check_Fornecedor()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxCliente_Cognome.Text + "'"));

            if (idtmp == compras_idfornecedor)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zempresaclienteid;
            }

            compras_idfornecedor = idtmp;

            return true;
        }

        private void Fill_Fornecedor()
        {
            tbxCliente_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + compras_idfornecedor);
            
            String pessoa = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PESSOA from CLIENTE where ID=" + compras_idfornecedor + "");
            tbxCliente_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CGC from CLIENTE where ID=" + compras_idfornecedor);

            if (pessoa == "j") tbxCliente_cnpj.Text = clsVisual.CamposVisual("CGC", tbxCliente_cnpj.Text);
            else tbxCliente_cnpj.Text = clsVisual.CamposVisual("CPF", tbxCliente_cnpj.Text);

            tbxCliente_telefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TELEFONE from CLIENTE where ID=" + compras_idfornecedor);
            tbxCliente_contato.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONTATO from CLIENTE where ID=" + compras_idfornecedor);

            if (id == 0)
            {
                compras_idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCONDPAGTO from CLIENTE where ID=" + compras_idfornecedor));
                tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id = " + compras_idcondpagto + " ");
                compras_idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDFORMAPAGTO from CLIENTE where ID=" + compras_idfornecedor));
                tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where id = " + compras_idformapagto + " ");
                tbxFormaPagto.Text += "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where id = " + compras_idformapagto + " ");

                // Meio de Transporte
                String meiotransporte = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select MEIODETRANSPORTE from cliente where id=" + compras_idfornecedor);
                if (meiotransporte == "S")
                {
                    rbnTransporteS.Checked = true;
                }
                else if (meiotransporte == "R")
                {
                    rbnTransporteR.Checked = true;
                }
                else if (meiotransporte == "T")
                {
                    rbnTransporteT.Checked = true;
                }
                else
                {
                    rbnTransporteN.Checked = true;
                }

                // Frete Incluso
                String freteincluso = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FRETEINCLUSO from cliente where id=" + compras_idfornecedor);
                if (freteincluso == "C")
                {
                    rbnFreteC.Checked = true;
                }
                else
                {
                    rbnFreteF.Checked = true;
                }
                // Frete por Conta
                String freteporconta = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FRETEPORCONTA from cliente where id=" + compras_idfornecedor);
                if (freteporconta == "0")
                {
                    rbnFretepaga0.Checked = true;
                }
                else
                {
                    rbnFretepaga1.Checked = true;
                }
                rbnTipofrete0.Checked = true;
                compras_idtransportadora = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDTRANSPORTADORA from cliente where id=" + compras_idfornecedor));
                tbxCognomeTransporte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + compras_idtransportadora + " ");
            }

        }

        private Boolean Check_Transportadora()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxCognomeTransporte.Text + "'"));

            if (idtmp == compras_idtransportadora)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zempresaclienteid;
            }

            compras_idtransportadora = idtmp;

            return true;
        }

        private void Fill_Transportadora()
        {
            tbxCognomeTransporte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + compras_idtransportadora);
        }

        private Boolean Check_Comprador()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from USUARIO where USUARIO='" + tbxComprador.Text + "'"));

            if (idtmp == compras_idcomprador)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zempresaclienteid;
            }

            compras_idcomprador = idtmp;

            return true;
        }

        private void Fill_Comprador()
        {
            tbxComprador.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO where ID=" + compras_idcomprador);
        }

        private Boolean Check_Formapagto()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo='" + tbxFormaPagto.Text.Split('=')[0] + "'"));

            if (idtmp == compras_idformapagto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zformapagto;
            }

            compras_idformapagto = idtmp;

            return true;
        }

        private void Fill_Formapagto()
        {
            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo where id=" + compras_idformapagto) + "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from situacaotipotitulo where id=" + compras_idformapagto);
        }

        private Boolean Check_Condpagto()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo='" + tbxCondPagto.Text.Split('=')[0] + "'"));

            if (idtmp == compras_idcondpagto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcondpagto;
            }

            compras_idcondpagto = idtmp;

            return true;
        }

        private void Fill_Condpagto()
        {
            tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM CONDPAGTO where id=" + compras_idcondpagto) + "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM CONDPAGTO where id=" + compras_idcondpagto);
        }

        private Boolean Check_Pecas()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + Compras1_tbxPecas_codigo.Text + "'"));

            if (idtmp == compras1_idcodigo)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zpecas;
            }

            compras1_idcodigo = idtmp;
            tipoproduto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOPRODUTO from PECAS WHERE ID= " + compras1_idcodigo);
            Compras1_lbxTipoProduto.SelectedIndex = Compras1_lbxTipoProduto.FindString(tipoproduto);
            if (Compras1_lbxTipoProduto.SelectedIndex == -1)
            {
                Compras1_lbxTipoProduto.SelectedIndex = 0;
            }

            
            //Compras1_lbxTipoProduto.SelectedIndex = clsVisual.SelecionarIndex(tipoproduto, 2, Compras1_lbxTipoProduto);

            if (compras1_iddestino == clsInfo.zpecas) compras1_iddestino = compras1_idcodigo;

            if (Compras1_rbnTipodestinoIte.Checked == true)
            {
                if (compras1_idcodigo1 == clsInfo.zpecas)
                {
                    compras1_idcodigo1 = compras1_idcodigo;
                    Fill_Pecas2();
                }
            }

            compras1_idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idhistoricobco FROM PECAS where id=" + compras1_idcodigo));
            if (compras1_idhistorico == 0) compras1_idhistorico = clsInfo.zhistoricos;
            Fill_Historico();

            //  sit tributaria origem
            compras1_idsittriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idsittriba FROM PECAS where id=" + compras1_idcodigo));
            Fill_Sittriba();

            // sit tributaria icm
            compras1_idsittribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idsittribvenda FROM PECAS where id=" + compras1_idcodigo));
            Fill_Sittribb();

            // ipi // classificação fiscal
            compras1_idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idipi FROM PECAS where id=" + compras1_idcodigo));
            if (compras1_idipi == 0) compras1_idipi = clsInfo.zipi;
            Fill_ClassFiscal();
            Compras1_tbxIpi.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ALIQUOTA FROM IPI where id=" + compras1_idipi)).ToString("N2");
            if (clsParser.DecimalParse(Compras1_tbxIpi.Text) > 0)
            {
                Compras1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex("50", 0, Compras1_cbxSittribipi);
            }
            else
            {
                Compras1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex("51", 0, Compras1_cbxSittribipi);
            }

            // UNIDADE FISCAL
            compras1_idunidfiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADECOM FROM PECAS where id=" + compras1_idcodigo));
            if (compras1_idunidfiscal == 0) compras1_idunidfiscal = clsInfo.zunidade;
            Fill_UnidFiscal();

            // UNIDADE
            compras1_idunid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADE FROM pecas where id=" + compras1_idcodigo));
            if (compras1_idunid == 0) compras1_idunid = clsInfo.zunidade;
            Fill_Unidade();

            // baseMP
            Compras1_tbxBasemp.Text = "0"; // (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT BASEMP FROM PECAS where id=" + compras1_idcodigo))).ToString("N2");

            // peso
            Compras1_tbxPeso.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT PESOUNIT FROM PECAS where id=" + compras1_idcodigo))).ToString("N3");

            //Ipi_Carrega();

            CarregaImpostos();

            return true;
        }

        private void Fill_Pecas()
        {
            clsPecasInfo = clsPecasBLL.Carregar(compras1_idcodigo, clsInfo.conexaosqldados);

            Compras1_tbxPecas_codigo.Text = clsPecasInfo.codigo; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + nfcompra1_idcodigo);
            Compras1_tbxPecas_nome.Text = clsPecasInfo.nome; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + nfcompra1_idcodigo);
            pbxFoto.Image = clsPecasInfo.foto;
            // PEGAR ULTIMO PREÇO PAGO FORNECEDOR
            Compras1_tbxPreco.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select nfcompra1.preco from nfcompra1 inner join nfcompra on nfcompra1.numero = nfcompra.id where nfcompra1.idcodigo = " + compras1_idcodigo + " order by nfcompra.data desc ", "0").ToString()).ToString("N6");
            Compras1_tbxCodigoemp04.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aplicacao from pecas where id=" + compras1_idcodigo);
            if (id == 0)
            {
                Compras1_lbxTipoProduto.SelectedIndex = Compras1_lbxTipoProduto.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOPRODUTO from pecas where ID= " + compras1_idcodigo));
                if (Compras1_lbxTipoProduto.SelectedIndex == -1)
                {
                    Compras1_lbxTipoProduto.SelectedIndex = 0;
                }
                compras1_idcentrocusto = clsInfo.zcentrocustos;
                Compras1_tbxCentrocusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from centrocustoS where id=" + compras1_idcentrocusto);
                compras1_idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOBCO from pecas where id=" + compras1_idcodigo));
                Compras1_tbxHistoricos_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where id=" + compras1_idhistorico);

                compras1_idunidfiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADECOM FROM PECAS WHERE ID=" + compras1_idcodigo));
                if (compras1_idunidfiscal == 0)
                {
                    compras1_idunidfiscal = clsInfo.zunidade;
                }
                Compras1_tbxUnidadeFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + compras1_idunidfiscal);

                compras1_idunid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADE FROM PECAS WHERE ID=" + compras1_idcodigo));
                if (compras1_idunid == 0)
                {
                    compras1_idunid = clsInfo.zunidade;
                }
                Compras1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + compras1_idunid);

                Compras1_tbxPeso.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select pesounit from pecas where ID= " + compras1_idcodigo);
                Compras1_tbxFatorConversao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FATORCONV from pecas where ID= " + compras1_idcodigo);

                compras1_idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idipi from pecas where ID= " + compras1_idcodigo));
                if (compras1_idipi == 0)
                {
                    compras1_idipi = clsInfo.zipi;
                }
                Compras1_tbxIpi.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquotaipi from pecas where ID= " + compras1_idcodigo))).ToString("N2");
                Compras1_tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from ipi where ID= " + compras1_idipi);
                //tbxPreco.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoravista from pecaspreco where IDcodigo= " + pedido1_idcodigo + " ORDER BY DATA DESC"))).ToString("N4");
            }
        }

        private Boolean Check_Pecas2()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + Compras1_tbxPecas_codigo2.Text + "'"));

            if (idtmp == compras1_idcodigo1)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zpecas;
            }

            compras1_idcodigo1 = idtmp;

            return true;
        }

        private void Fill_Pecas2()
        {
            Compras1_tbxPecas_codigo2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM PECAS where id=" + compras1_idcodigo1);
            Compras1_tbxPecas_nome2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM PECAS where id=" + compras1_idcodigo1);
        }


        private void TipoNotaCarregar()
        {
            // carregar todos os dados do tipo nota
            // carregar os impostos tembém 
            // qdo for fazer vide coating ou resol frmpedido // TipoNotaCarregar
        }

        private Boolean Check_CfopFis()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CFOP where CFOP='" + Compras1_tbxCfopfis.Text + "'"));

            if (idtmp == compras1_idcfopfis)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcfop;
            }

            compras1_idcfopfis = idtmp;

            return true;
        }

        private void Fill_CfopFis()
        {
            Compras1_tbxCfopfis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CFOP FROM CFOP where id=" + compras1_idcfopfis);
        }

        private Boolean Check_Cfop()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CFOP where CFOP='" + Compras1_tbxCfop.Text + "'"));

            if (idtmp == compras1_idcfop)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcfop;
            }

            compras1_idcfop = idtmp;

            return true;
        }

        private void Fill_Cfop()
        {
            Compras1_tbxCfop.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CFOP FROM CFOP where id=" + compras1_idcfop);
        }

        private Boolean Check_Sittriba()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAA where CODIGO='" + Compras1_cbxSittriba.Text + "'"));

            if (idtmp == compras1_idsittriba)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsituacaotriba;
            }

            compras1_idsittriba = idtmp;

            return true;
        }

        private void Fill_Sittriba()
        {
            String sittriba = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBUTARIAA where id=" + compras1_idsittriba);
            Compras1_cbxSittriba.SelectedIndex = clsVisual.SelecionarIndex(sittriba, 0, Compras1_cbxSittriba);
        }

        private Boolean Check_Sittribb()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAB where CODIGO='" + Compras1_cbxSittribb.Text + "'"));

            if (idtmp == compras1_idsittribb)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsituacaotribb;
            }

            compras1_idsittribb = idtmp;

            return true;
        }

        private void Fill_Sittribb()
        {
            String sittribb = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBUTARIAB where id=" + compras1_idsittribb);
            Compras1_cbxSittribb.SelectedIndex = clsVisual.SelecionarIndex(sittribb, 0, Compras1_cbxSittribb);
        }

        private Boolean Check_CentroCusto()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where codigo='" + Compras1_tbxCentrocusto.Text + "'"));

            if (idtmp == compras1_idcentrocusto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcentrocustos;
            }

            compras1_idcentrocusto = idtmp;

            return true;
        }

        private void Fill_CentroCusto()
        {
            Compras1_tbxCentrocusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM CENTROCUSTOS where id=" + compras1_idcentrocusto);
        }

        private Boolean Check_Historico()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where codigo='" + Compras1_tbxHistoricos_codigo.Text + "'"));

            if (idtmp == compras1_idhistorico)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zhistoricos;
            }

            compras1_idhistorico = idtmp;

            return true;
        }

        private void Fill_Historico()
        {
            Compras1_tbxHistoricos_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + compras1_idhistorico);
        }

        private Boolean Check_UnidFiscal()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where codigo='" + Compras1_tbxUnidadeFiscal.Text + "'"));

            if (idtmp == compras1_idunidfiscal)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zunidade;
            }

            compras1_idunidfiscal = idtmp;

            return true;
        }

        private void Fill_UnidFiscal()
        {
            Compras1_tbxUnidadeFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM UNIDADE where id=" + compras1_idunidfiscal);
        }

        private Boolean Check_Unidade()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where codigo='" + Compras1_tbxUnidade.Text + "'"));

            if (idtmp == compras1_idunid)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zunidade;
            }

            compras1_idunid = idtmp;

            return true;
        }

        private void Fill_Unidade()
        {
            Compras1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM UNIDADE where id=" + compras1_idunid);
        }

        private Boolean Check_ClassFiscal()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from IPI where codigo='" + Compras1_tbxIpi_codigo.Text + "'"));

            if (idtmp == compras1_idipi)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zipi;
            }

            compras1_idipi = idtmp;

            Compras1_tbxIpi.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ALIQUOTA FROM IPI where id=" + compras1_idipi)).ToString("N2");

            return true;
        }

        private void Fill_ClassFiscal()
        {
            Compras1_tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM IPI where id=" + compras1_idipi);
        }

        private Boolean Check_Sittribipi()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribipi where codigo='" + Compras1_cbxSittribipi.Text + "'"));

            if (idtmp == compras1_idsittribipi)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribipi;
            }

            compras1_idsittribipi = idtmp;

            return true;
        }

        private void Fill_Sittribipi()
        {
            String cstipi  = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBIPI where id=" + compras1_idsittribipi);
            Compras1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex(cstipi, 0, Compras1_cbxSittribipi);
        }

        private Boolean Check_Sittribpis()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribpis where codigo='" + Compras1_cbxSittribpis.Text + "'"));

            if (idtmp == compras1_idsittribpis)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribpis;
            }

            compras1_idsittribpis = idtmp;

            return true;
        }

        private void Fill_Sittribpis()
        {
            String cstpis = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBPIS where id=" + compras1_idsittribpis);
            Compras1_cbxSittribpis.SelectedIndex = clsVisual.SelecionarIndex(cstpis, 0, Compras1_cbxSittribpis);
        }

        private Boolean Check_Sittribcofins()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribcofins where codigo='" + Compras1_cbxSittribcofins1.Text + "'"));

            if (idtmp == compras1_idsittribcofins1)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribcofins;
            }

            compras1_idsittribcofins1 = idtmp;

            return true;
        }

        private void Fill_Sittribcofins()
        {
            String cstcofins1 = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBCOFINS where id=" + compras1_idsittribcofins1);
            Compras1_cbxSittribcofins1.SelectedIndex = clsVisual.SelecionarIndex(cstcofins1, 0, Compras1_cbxSittribcofins1);
        }

        void CarregaImpostos()
        {
            Int32 iduforigem = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cliente where id=" + compras_idfornecedor));

            // Icms
            if (Compras1_cbxSittriba.SelectedIndex != -1 && Compras1_cbxSittriba.Text.Substring(0, 1) == "1") // Se for de origem estrangeira, utiliza-se alíquota interna do estado
            {
                Compras1_tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + clsInfo.zipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
                Compras1_tbxBasemp.Text = clsParser.DecimalParse("0").ToString("n2");
            }
            else
            {
                /*
                if (item_tiponota_devolucao == true) // Beneficiamento
                {
                    Item_tbxIcm.Text = clsParser.DecimalParse(Procedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + clsInfo.zipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
                    Item_tbxBasemp.Text = clsParser.DecimalParse(Procedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + clsInfo.zipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
                }
                else
                {*/
                Compras1_tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + compras1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
                Compras1_tbxBasemp.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + compras1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
                //}
            }

            // ST
            Compras1_tbxIcminterno.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + compras1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
            Compras1_tbxIcmssubstreducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducaoiva from ipiestadosicms where idipi=" + compras1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
            Compras1_tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + compras1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");

            if (iduforigem != clsInfo.zempresa_ufid && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + clsInfo.zempresa_ufid) == "MT")
            {
                Compras1_tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from tab_cnae_uf where idtabcnae=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcnae from cliente where id=" + clsInfo.zempresaclienteid) + " and iduf=" + clsInfo.zempresa_ufid)).ToString("n2");
            }
            else
            {
                Compras1_tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + compras1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
            }

            // Pis
            Compras1_tbxAliqpispasep.Text = "0"; // clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select leipispasep from empresagere where EMPRESA = "+ clsInfo.zempresaid,"0")).ToString("n2");

            // Cofins
            Compras1_tbxAliqcofins1.Text = "0"; // clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select leicofins from empresagere where EMPRESA = " + clsInfo.zempresaclienteid,"0")).ToString("n2");

            Calcular();
        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                ComprasSalvar();
            }
            return drt;

        }
        private Boolean HouveModificacoes()
        {
            clsComprasInfo = new clsComprasInfo();
            ComprasFillInfo(clsComprasInfo);
            if (clsComprasBLL.Equals(clsComprasInfo, clsComprasInfoOld) == false)
            {
                return true;
            }
            return false;
        }
        private void ComprasSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: NFCOMPRA
                clsComprasInfo = new clsComprasInfo();
                ComprasFillInfo(clsComprasInfo);

                if (id == 0)
                {
                    clsComprasInfo.id = clsComprasBLL.Incluir(clsComprasInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsComprasBLL.Alterar(clsComprasInfo, clsInfo.conexaosqldados);
                }
                
                // ITENS DO PEDIDO DE COMPRAS
                foreach (DataRow row in dtCompras1.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["IDCOMPRAS"] = clsComprasInfo.id;
                    }
                }

                foreach (DataRow row in dtCompras1.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsCompras1BLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsCompras1Info = new clsCompras1Info();
                        Compras1GridToInfo(clsCompras1Info, Int32.Parse(row["compras1_posicao"].ToString()));

                        if (clsCompras1Info.id == 0)
                        {
                            clsCompras1Info.id = clsCompras1BLL.Incluir(clsCompras1Info, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsCompras1BLL.Alterar(clsCompras1Info, clsInfo.conexaosqldados);
                        }
                        // PLANEJAMENTO DE ENTREGA DO PEDIDO DE COMPRAS
                        foreach (DataRow rowEntrega in dtComprasEntrega.Rows)
                        {
                            if (rowEntrega.RowState != DataRowState.Deleted &&
                                rowEntrega.RowState != DataRowState.Detached &&
                                rowEntrega.RowState != DataRowState.Unchanged)
                            {
                                if (rowEntrega["compras1_posicao"].ToString() == row["compras1_posicao"].ToString())
                                {
                                    rowEntrega["IDCOMPRAS"] = clsComprasInfo.id;
                                    rowEntrega["IDCOMPRAS1"] = clsCompras1Info.id;
                                }
                            }
                        }
                    }
                }

                foreach (DataRow row in dtComprasEntrega.Rows)
                {
                    if (row.RowState == DataRowState.Detached)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsComprasEntregaBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsComprasEntregaInfo = new clsComprasEntregaInfo();
                        ComprasEntregaGridToInfo(clsComprasEntregaInfo, Int32.Parse(row["comprasentrega_posicao"].ToString()));

                        if (clsComprasEntregaInfo.id == 0)
                        {
                            clsComprasEntregaInfo.id = clsComprasEntregaBLL.Incluir(clsComprasEntregaInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsComprasEntregaBLL.Alterar(clsComprasEntregaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }

                id = clsComprasInfo.id;

                tse.Complete();
            }
        }


        private void ComprasCarregar()
        {
            clsComprasInfoOld = new clsComprasInfo();

            if (id == 0)
            {
                clsComprasInfo = new clsComprasInfo();
                clsComprasInfo.ano = DateTime.Now.Year;
                clsComprasInfo.cofins = 0;
                clsComprasInfo.cofins1 = 0;
                clsComprasInfo.contato = "";
                clsComprasInfo.csll = 0;
                clsComprasInfo.data = DateTime.Now;
                clsComprasInfo.filial = clsInfo.zfilial;
                clsComprasInfo.frete = "C";
                clsComprasInfo.fretepaga = "0";
                clsComprasInfo.id = 0;
                clsComprasInfo.idautorizante = clsInfo.zusuarioid;
                clsComprasInfo.idcomprador = clsInfo.zusuarioid;
                clsComprasInfo.idcondpagto = clsInfo.zcondpagto;
                clsComprasInfo.idcontato = clsInfo.zusuarioid;
                clsComprasInfo.idemitente = clsInfo.zusuarioid;
                clsComprasInfo.idformapagto = clsInfo.zformapagto;
                clsComprasInfo.idfornecedor = clsInfo.zempresaclienteid;
                clsComprasInfo.idtransportadora = clsInfo.zempresaclienteid;
                clsComprasInfo.inss = 0;
                clsComprasInfo.irrf = 0;
                clsComprasInfo.iss = 0;
                clsComprasInfo.numero = 0;
                clsComprasInfo.observa = "";
                clsComprasInfo.pis = 0;
                clsComprasInfo.piscofinscsll = 0;
                clsComprasInfo.pispasep = 0;
                clsComprasInfo.qtdebaixada = 0;
                clsComprasInfo.qtdedefeito = 0;
                clsComprasInfo.qtdeentregue = 0;
                clsComprasInfo.qtdeosaux = 0;
                clsComprasInfo.qtdesaldo = 0;
                clsComprasInfo.qtdesucata = 0;
                clsComprasInfo.setor = "N";
                clsComprasInfo.setorfator = 1;
                clsComprasInfo.situacao = "0";
                clsComprasInfo.termino = "N";
                clsComprasInfo.tipofrete = "N";
                clsComprasInfo.totalbaseicm = 0;
                clsComprasInfo.totalbaseicmsubst = 0;
                clsComprasInfo.totalfrete = 0;
                clsComprasInfo.totalicm = 0;
                clsComprasInfo.totalicmsubst = 0;
                clsComprasInfo.totalipi = 0;
                clsComprasInfo.totalmercadoria = 0;
                clsComprasInfo.totaloutras = 0;
                clsComprasInfo.totalpeca = 0;
                clsComprasInfo.totalpecaentra = 0;
                clsComprasInfo.totalpecatransfe = 0;
                clsComprasInfo.totalpedido = 0;
                clsComprasInfo.totalpeso = 0;
                clsComprasInfo.totalprevisto = 0;
                clsComprasInfo.totalseguro = 0;
                clsComprasInfo.transporte = "N";

            }
            else
            {
                clsComprasInfo = clsComprasBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            ComprasCampos(clsComprasInfo);
            ComprasFillInfo(clsComprasInfoOld);
            //

            // carregando os itens da Nota Fiscal de Entrada
            dtCompras1 = clsCompras1BLL.GridDados(clsComprasInfo.id);

            DataColumn dcPosicao = new DataColumn("compras1_posicao", Type.GetType("System.Int32"));
            dtCompras1.Columns.Add(dcPosicao);

            for (Int32 i = 1; i <= dtCompras1.Rows.Count; i++)
            {
                dtCompras1.Rows[i - 1]["compras1_posicao"] = i;
            }

            dtCompras1.AcceptChanges();
            clsCompras1BLL.GridMonta(dgvCompras1, dtCompras1, compras1_posicao);

            // visualizar a programação de entrega deste item, para substituir o GridDados
            dtComprasEntrega = new DataTable();
            dtComprasEntrega = clsComprasEntregaBLL.GridDados(0);

            DataTable dtTemp;
            for (Int32 i = 0; i < dtCompras1.Rows.Count; i++)
            {
                dtTemp = clsComprasEntregaBLL.GridDados(clsParser.Int32Parse(dtCompras1.Rows[i]["id"].ToString()));

                for (Int32 i2 = 0; i2 < dtTemp.Rows.Count; i2++)
                {
                    dtComprasEntrega.Rows.Add(dtTemp.Rows[i2].ItemArray);
                }
            }

            DataColumn dcComprasEntrega_posicao = new DataColumn("comprasentrega_posicao", Type.GetType("System.Int32"));
            DataColumn dcComprasEntrega_compras1_posicao = new DataColumn("compras1_posicao", Type.GetType("System.Int32"));
            dtComprasEntrega.Columns.Add(dcComprasEntrega_posicao);
            dtComprasEntrega.Columns.Add(dcComprasEntrega_compras1_posicao);

            for (Int32 i = 1; i <= dtComprasEntrega.Rows.Count; i++)
            {
                dtComprasEntrega.Rows[i - 1]["comprasentrega_posicao"] = i;
            }

            for (Int32 i = 0; i < dtCompras1.Rows.Count; i++)
            {
                for (Int32 i2 = 0; i2 < dtComprasEntrega.Rows.Count; i2++)
                {
                    if (dtComprasEntrega.Rows[i2]["idcompras1"].ToString() == dtCompras1.Rows[i]["id"].ToString())
                    {
                        dtComprasEntrega.Rows[i2]["compras1_posicao"] = dtCompras1.Rows[i]["compras1_posicao"];
                    }
                }
            }

            dtComprasEntrega.AcceptChanges();
            clsComprasEntregaBLL.GridMonta(dgvComprasEntrega, dtComprasEntrega, comprasentrega_posicao);

            tclCompras.SelectedIndex = 0;
            if (clsParser.DecimalParse(tbxTotalpedido.Text) > 0)
            {
                if (gbxFormapagto.Enabled == true)
                {
                    tbxFormaPagto.Select();
                }
            }
            else
            {
                tbxCliente_Cognome.Select();
            }
        }
        private void ComprasCampos(clsComprasInfo info)
        {
            id = info.id;
            tbxAno.Text = info.ano.ToString();
            tbxCofins.Text = info.cofins.ToString("N2");
            tbxTotalcofins1.Text = info.cofins1.ToString("N2");
            tbxCliente_contato.Text = info.contato;
            tbxCsll.Text = info.csll.ToString("N2");
            tbxData.Text = info.data.ToString("dd/MM/yyyy");
           
            filial = info.filial;
            if (info.frete == "C")
            {
                rbnFreteC.Checked = true;
            }
            else
            {
                rbnFreteF.Checked = true;
            }

            if (info.fretepaga == "0")
            {
                rbnFretepaga0.Checked = true;
            }
            else
            {
                rbnFretepaga1.Checked = true;
            }
            //info.idautorizante;
            compras_idautorizante = info.idautorizante;
            if (compras_idautorizante == 0) { compras_idautorizante = clsInfo.zusuarioid; }
            tbxAutorizante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT USUARIO FROM USUARIO where id=" + compras_idautorizante);
            if (compras_idemitente == 0) { compras_idemitente = clsInfo.zusuarioid; }
            tbxEmitente.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT USUARIO FROM USUARIO where id=" + compras_idemitente);

            compras_idcomprador = info.idcomprador;
            if (compras_idcomprador == 0) { compras_idcomprador = clsInfo.zusuarioid; }
            Fill_Comprador();
            
            compras_idcondpagto = info.idcondpagto;
            if (compras_idcondpagto == 0) { compras_idcondpagto = clsInfo.zcondpagto; }
            Fill_Condpagto();

            compras_idcontato = info.idcontato;
            compras_idemitente = info.idemitente;
            if (compras_idemitente == 0) { compras_idemitente = clsInfo.zusuarioid; }

            tbxEmitente.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT USUARIO FROM USUARIO where id=" + compras_idemitente);
            
            compras_idformapagto = info.idformapagto;
            if (compras_idformapagto == 0) { compras_idformapagto = clsInfo.zformapagto; }
            Fill_Formapagto();
            
            compras_idfornecedor = info.idfornecedor;
            if (compras_idfornecedor == 0) { compras_idfornecedor = clsInfo.zempresaclienteid; }
            Fill_Fornecedor();

            compras_idtransportadora = info.idtransportadora;
            if (compras_idtransportadora == 0) { compras_idtransportadora = clsInfo.zempresaclienteid; }
            Fill_Transportadora();
            
            tbxInss.Text = info.inss.ToString("N2");
            tbxIrrf.Text = info.irrf.ToString("N2");
            tbxIss.Text = info.iss.ToString("N2");
            tbxNumero.Text = info.numero.ToString();
            tbxObserva.Text = info.observa;
            tbxPis.Text = info.pis.ToString("N2");
            tbxPisCofinsCsll.Text = info.piscofinscsll.ToString("N2");
            tbxTotalpispasep.Text = info.pispasep.ToString("N2");
            tbxQtdebaixada.Text = info.qtdebaixada.ToString("N3");
            tbxQtdedefeito.Text = info.qtdedefeito.ToString("N3");
            tbxQtdeentregue.Text = info.qtdeentregue.ToString("N3");
            tbxQtdeosaux.Text = info.qtdeosaux.ToString("N3");
            tbxQtdesaldo.Text = info.qtdesaldo.ToString("N3");
            tbxQtdesucata.Text = info.qtdesucata.ToString("N3");
            cbxSetor.SelectedIndex = cbxSetor.FindString(info.setor);
            if (cbxSetor.SelectedIndex == -1)
            {
                cbxSetor.SelectedIndex = 0;
            }
            tbxSetorfator.Text = info.setorfator.ToString("N2");
            cbxSituacao.SelectedIndex = cbxSituacao.FindString(info.situacao);
            if (cbxSituacao.SelectedIndex == -1)
            {
                cbxSituacao.SelectedIndex = 0;
            }
            if (info.termino == "S")
            {
                tbxTermino.Text = "S = Ped. Fechado";
            }
            else
            {
                tbxTermino.Text = "N = Ped. Em Aberto";
            }
            if (info.tipofrete == "0")
            {
                rbnTipofrete0.Checked = true;
            }
            else
            {
                rbnTipofrete1.Checked = true;
            }
            tbxTotalbaseicm.Text = info.totalbaseicm.ToString("N2");
            tbxTotalbaseicmsubst.Text = info.totalbaseicmsubst.ToString("N2");
            tbxTotalfrete.Text = info.totalfrete.ToString("N2");
            tbxTotalicm.Text = info.totalicm.ToString("N2");
            tbxTotalicmsubst.Text = info.totalicmsubst.ToString("N2");
            tbxTotalipi.Text = info.totalipi.ToString("N2");
            tbxTotalmercadoria.Text = info.totalmercadoria.ToString("N2");
            tbxTotaloutras.Text = info.totaloutras.ToString("N2");
            tbxTotalpeca.Text = info.totalpeca.ToString("N3");
            //info.totalpecaentra;
            //info.totalpecatransfe;
            tbxTotalpedido.Text = info.totalpedido.ToString("N2"); ;
            tbxTotalpeso.Text = info.totalpeso.ToString("N3");
            tbxTotalprevisto.Text = info.totalprevisto.ToString("N2");
            tbxTotalseguro.Text = info.totalseguro.ToString("N2");
            if (info.transporte == "S")
            {
                rbnTransporteS.Checked = true;
            }
            else if (info.transporte == "R")
            {
                rbnTransporteR.Checked = true;
            }
            else if (info.transporte == "N")
            {
                rbnTransporteN.Checked = true;
            }
            else
            {
                rbnTransporteT.Checked = true;
            }
            //
            Calcular();
            
        }
        private void ComprasFillInfo(clsComprasInfo info)
        {  
            info.id = id;

            info.ano = clsParser.Int32Parse(tbxAno.Text);
            info.cofins = clsParser.DecimalParse(tbxCofins.Text);
            info.cofins1 = clsParser.DecimalParse(tbxTotalcofins1.Text);
            info.contato = tbxCliente_contato.Text;
            info.csll = clsParser.DecimalParse(tbxCsll.Text);
            info.data = DateTime.Parse(tbxData.Text);
            info.filial = filial;
            if (rbnFreteC.Checked)
                info.frete = "C";
            else if (rbnFreteF.Checked)
                info.frete = "F";

            if (rbnFretepaga0.Checked)
                info.fretepaga = "0";
            else if (rbnFretepaga1.Checked)
                info.fretepaga = "1";

            info.idautorizante = compras_idautorizante;
            info.idcomprador = compras_idcomprador;
            info.idcondpagto = compras_idcondpagto;
            info.idcontato = compras_idcontato;
            info.idemitente = compras_idemitente;
            info.idformapagto = compras_idformapagto;
            info.idfornecedor = compras_idfornecedor;
            info.idtransportadora = compras_idtransportadora;
            info.inss = clsParser.DecimalParse(tbxInss.Text);
            info.irrf = clsParser.DecimalParse(tbxIrrf.Text);
            info.iss = clsParser.DecimalParse(tbxInss.Text);
            info.numero = clsParser.Int32Parse(tbxNumero.Text);
            info.observa = tbxObserva.Text;
            info.pis = clsParser.DecimalParse(tbxPis.Text);
            info.piscofinscsll = clsParser.DecimalParse(tbxPisCofinsCsll.Text);
            info.pispasep = clsParser.DecimalParse(tbxTotalpispasep.Text);
            info.qtdebaixada = clsParser.DecimalParse(tbxQtdebaixada.Text);
            info.qtdedefeito = clsParser.DecimalParse(tbxQtdedefeito.Text);
            info.qtdeentregue = clsParser.DecimalParse(tbxQtdeentregue.Text);
            info.qtdeosaux = clsParser.DecimalParse(tbxQtdeosaux.Text);
            info.qtdesaldo = clsParser.DecimalParse(tbxQtdesaldo.Text);
            info.qtdesucata = clsParser.DecimalParse(tbxQtdesucata.Text); 
            info.setor = cbxSetor.Text.Substring(0, 1);
            info.setorfator = clsParser.DecimalParse(tbxSetorfator.Text);
            info.situacao = cbxSituacao.Text.Substring(0, 1);
            info.termino = tbxTermino.Text.Substring(0,1);

            if (rbnTipofrete0.Checked)
                info.tipofrete = "0";
            else if (rbnTipofrete1.Checked)
                info.tipofrete = "1";

            info.totalbaseicm = clsParser.DecimalParse(tbxTotalbaseicm.Text);
            info.totalbaseicmsubst = clsParser.DecimalParse(tbxTotalbaseicmsubst.Text);
            info.totalfrete = clsParser.DecimalParse(tbxTotalfrete.Text);
            info.totalicm = clsParser.DecimalParse(tbxTotalicm.Text);
            info.totalicmsubst = clsParser.DecimalParse(tbxTotalicmsubst.Text);
            info.totalipi = clsParser.DecimalParse(tbxTotalipi.Text);
            info.totalmercadoria = clsParser.DecimalParse(tbxTotalmercadoria.Text);
            info.totaloutras = clsParser.DecimalParse(tbxTotaloutras.Text);
            info.totalpeca = clsParser.DecimalParse(tbxTotalpeca.Text);
            //info.totalpecaentra;
            //info.totalpecatransfe;
            info.totalpedido = clsParser.DecimalParse(tbxTotalpedido.Text);
            info.totalpeso = clsParser.DecimalParse(tbxTotalpeso.Text);
            info.totalprevisto = clsParser.DecimalParse(tbxTotalprevisto.Text);
            info.totalseguro = clsParser.DecimalParse(tbxTotalseguro.Text);

            if (rbnTransporteS.Checked)
                info.transporte = "S";
            else if (rbnTransporteR.Checked)
                info.transporte = "R";
            else if (rbnTransporteN.Checked)
                info.transporte = "N";
            else if (rbnTransporteT.Checked)
                info.transporte = "T";


        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", compras_idfornecedor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnIdClienteContato_Click(object sender, EventArgs e)
        {
/*            clsInfo.znomegrid = btnIdCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init(cliente_tipo.Todos, compras_idfornecedor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes);
 */
        }

        private void btnIdComprador_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdComprador.Name;
            frmUsuarioPes frmUsuarioPes = new frmUsuarioPes();
            frmUsuarioPes.Init(compras_idcomprador, clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(this.MdiParent, frmUsuarioPes, clsInfo.conexaosqldados);
        }

        private void btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", compras_idformapagto, "Situacao Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnIdCondPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCondPagto.Name;
            frmCondPagtoPes frmCondPagtoPes = new frmCondPagtoPes();
            frmCondPagtoPes.Init(clsInfo.conexaosqldados, compras_idcondpagto);

            clsFormHelper.AbrirForm(this.MdiParent, frmCondPagtoPes, clsInfo.conexaosqldados);

        }

        private void btnIdTransportadora_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdTransportadora.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", compras_idtransportadora);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void tspCompras1Incluir_Click(object sender, EventArgs e)
        {
            compras1_posicao = 0;
            Compras1Carregar();

        }

        private void tspCompras1Alterar_Click(object sender, EventArgs e)
        {
            if (dgvCompras1.CurrentRow != null)
            {
                compras1_posicao = Int32.Parse(dgvCompras1.CurrentRow.Cells["compras1_posicao"].Value.ToString());
                Compras1Carregar();
            }
        }

        private void tspCompras1Excluir_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Desabilitado - Favor Cancelar as Entregas - Vide Manual");
            
            try
            {
                if (dgvCompras1.CurrentRow != null)
                {
                    DialogResult drt;
                    drt = MessageBox.Show("Deseja Excluir este Item de Compra?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (drt == DialogResult.Yes)
                    {
                        DataRow[] rowsEntrega = dtComprasEntrega.Select("compras1_posicao=" + dgvCompras1.CurrentRow.Cells["compras1_posicao"].Value.ToString());
                        for (Int32 i = 0; i < rowsEntrega.Length; i++)
                        {
                            rowsEntrega[i].Delete();
                        }

                        dtCompras1.Select("compras1_posicao=" + dgvCompras1.CurrentRow.Cells["compras1_posicao"].Value.ToString())[0].Delete();

                        ComprasSomar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Compras1Carregar()
        {
            rowsitens =  dgvCompras1.Rows;
            clsCompras1Info = new clsCompras1Info();
            clsCompras1InfoOld = new clsCompras1Info();

            if (compras1_posicao == 0)
            {
                compras1_posicao = dtCompras1.Rows.Count + 1;
                
                clsCompras1Info.baseicm = 0;
                clsCompras1Info.baseicmsubst = 0;
                clsCompras1Info.basemp = 0;
                clsCompras1Info.calculoautomatico = "S";
                clsCompras1Info.codigoemp01 = "";
                clsCompras1Info.codigoemp02 = "";
                clsCompras1Info.codigoemp03 = "";
                clsCompras1Info.codigoemp04 = "";
                clsCompras1Info.cofins = 0;
                clsCompras1Info.cofins1 = 0;
                clsCompras1Info.cofinsporc = clsInfo.zfiscalcofins;
                clsCompras1Info.complemento = "";
                clsCompras1Info.complemento1 = "";
                clsCompras1Info.consumo = "N";
                clsCompras1Info.creditaricm = "N";
                clsCompras1Info.csll = 0;
                clsCompras1Info.csllporc = clsInfo.zfiscalcsll;
                clsCompras1Info.custoicm = 0;
                clsCompras1Info.custoipi = 0;
                clsCompras1Info.desconto = 0;
                clsCompras1Info.descricaoesp = "";
                clsCompras1Info.fatorconv = 0;
                clsCompras1Info.icm = 0;
                clsCompras1Info.icminterno = 0;
                clsCompras1Info.icmsubst = 0;
                clsCompras1Info.id = 0;
                clsCompras1Info.idcentrocusto = clsInfo.zcentrocustos;
                clsCompras1Info.idcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CFOP where CFOP='" + "5102" + "' "));
                clsCompras1Info.idcfopfis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CFOP where CFOP='" + "5102" + "' "));
                clsCompras1Info.idcodigo = clsInfo.zpecas;
                clsCompras1Info.idcodigo1 = clsInfo.zpecas;
                clsCompras1Info.idcodigoctabil = clsInfo.zcontacontabil;
                clsCompras1Info.idcompras = id;
                clsCompras1Info.idcotacao = clsInfo.zcotacao;
                clsCompras1Info.idcotacaoitem = clsInfo.zcotacao1;
                clsCompras1Info.idcotacao2 = clsInfo.zcotacao2;
                clsCompras1Info.iddestino = clsInfo.zpecas;
                clsCompras1Info.idhistorico = clsInfo.zhistoricos;
                clsCompras1Info.idipi = clsInfo.zipi;
                clsCompras1Info.idos = clsInfo.zordemservico;
                clsCompras1Info.idsittriba = clsInfo.zsituacaotriba;
                clsCompras1Info.idsittribb = clsInfo.zsituacaotribb;
                clsCompras1Info.idsittribipi = clsInfo.zsittribipi;
                clsCompras1Info.idsittribpis = clsInfo.zsittribpis;
                clsCompras1Info.idsittribcofins1 = clsInfo.zsittribcofins;
                clsCompras1Info.idsolicitacao = clsInfo.zsolicitacao;
                clsCompras1Info.idtiponota = clsInfo.ztiponota;
                clsCompras1Info.idunid = clsInfo.zunidade;
                clsCompras1Info.idunidfiscal = clsInfo.zunidade;
                clsCompras1Info.inss = 0;
                clsCompras1Info.inssporc = clsInfo.zfiscalinss;
                clsCompras1Info.ipi = 0;
                clsCompras1Info.irrf = 0;
                clsCompras1Info.irrfporc = clsInfo.zfiscalirrf;
                clsCompras1Info.iss = 0;
                clsCompras1Info.issporc = clsInfo.zfiscaliss;
                clsCompras1Info.item = 0;
                clsCompras1Info.msg = "";
                clsCompras1Info.observar = "";
                clsCompras1Info.peso = 0;
                clsCompras1Info.pis = 0;
                clsCompras1Info.piscofinscsll = 0;
                clsCompras1Info.piscofinscsllporc = clsInfo.zfiscalpiscofins;
                clsCompras1Info.pispasep = 0;
                clsCompras1Info.pisporc = clsInfo.zfiscalpis;
                clsCompras1Info.preco = 0;
                clsCompras1Info.precobruto = 0;
                clsCompras1Info.qtde = 0;
                clsCompras1Info.qtdebaixada = 0;
                clsCompras1Info.qtdedefeito = 0;
                clsCompras1Info.qtdeentra = 0;
                clsCompras1Info.qtdeentregue = 0;
                clsCompras1Info.qtdefiscal = 0;
                clsCompras1Info.qtdeosaux = 0;
                clsCompras1Info.qtdesaldo = 0;
                clsCompras1Info.qtdesucata = 0;
                clsCompras1Info.qtdetotal = 0;
                clsCompras1Info.qtdetransfe = 0;
                clsCompras1Info.reducao = 0;
                clsCompras1Info.termino = "N";
                clsCompras1Info.tipodestino = "I";
                clsCompras1Info.totalmercado = 0;
                clsCompras1Info.totalnota = 0;
                clsCompras1Info.totalpeso = 0;
                clsCompras1Info.totalprevisto = 0;
                clsCompras1Info.valordesconto = 0;
                clsCompras1Info.valorfrete = 0;
                clsCompras1Info.valorfreteicms = "N";
                clsCompras1Info.valoroutras = 0;
                clsCompras1Info.valoroutrasicms = "N";
                clsCompras1Info.valorseguro = 0;
                clsCompras1Info.valorseguroicms = "N";
            }
            else
            {
                Compras1GridToInfo(clsCompras1Info, compras1_posicao);
                Decimal por = clsCompras1Info.irrfporc + clsCompras1Info.inssporc + clsCompras1Info.piscofinscsllporc + clsCompras1Info.issporc + clsCompras1Info.pisporc + clsCompras1Info.cofinsporc + clsCompras1Info.csllporc;
                if (Compras1_lbxTipoProduto.Text.PadRight(2, ' ').Substring(0, 2) == "30")
                {
                    if (por == 0)
                    {
                        clsCompras1Info.irrfporc = clsInfo.zfiscalirrf;
                        clsCompras1Info.inssporc = clsInfo.zfiscalinss;
                        clsCompras1Info.piscofinscsllporc = clsInfo.zfiscalpiscofins;
                        clsCompras1Info.issporc = clsInfo.zfiscaliss;
                        clsCompras1Info.pisporc = clsInfo.zfiscalpis;
                        clsCompras1Info.cofinsporc = clsInfo.zfiscalcofins;
                        clsCompras1Info.csllporc = clsInfo.zfiscalcsll;
                    }
                }
                else
                {
                    clsCompras1Info.irrfporc = 0;
                    clsCompras1Info.inssporc = 0;
                    clsCompras1Info.piscofinscsllporc = 0;
            
                    clsCompras1Info.issporc = 0;
                    clsCompras1Info.pisporc = 0;
                    clsCompras1Info.cofinsporc = 0;
                    clsCompras1Info.csllporc = 0;
                }
            }

            Compras1Campos(clsCompras1Info);
            Compras1FillInfo(clsCompras1InfoOld);

            tclCompras.SelectedIndex = 1;
            //gbxCompras1.Visible = true;

            Compras1_tbxPecas_codigo.Select();

            ComprasEntregaSomar();
            if (clsParser.DecimalParse(Compras1_tbxQtdeProgramada.Text) > 0)
            {
                gbxPrePlanejamento.Visible = false;
                gbxQtdes.Visible = true;
                gbxQtdes.Width = 203;
                gbxQtdes.Height = 135;

            }
            else
            {
                gbxPrePlanejamento.Visible = true;
                gbxQtdes.Visible = false;
                gbxQtdes.Width = 23;
                gbxQtdes.Height = 135;
            }
            if (clsParser.DecimalParse(Compras1_tbxQtdeProgramada.Text) != clsParser.DecimalParse(Compras1_tbxQtdesaldo.Text))
            {
                // NÃO PODE EXCLUIR PROGRAMAÇÃO DEPOIS QUE JÁ TEVE ALGUMA MOVIMENTAÇÃO
                tspComprasEntregaExcluir.Visible = false;
            }

            dtComprasEntrega.DefaultView.RowFilter = "compras1_posicao=" + compras1_posicao;
        }

        private void Compras1GridToInfo(clsCompras1Info info, Int32 posicao)
        {
            DataRow row = dtCompras1.Select("compras1_posicao = " + posicao)[0];

            info.aliqpispasep = clsParser.DecimalParse(row["aliqpispasep"].ToString());
            info.aliqcofins1 = clsParser.DecimalParse(row["aliqcofins1"].ToString());
            info.baseicm = clsParser.DecimalParse(row["baseicm"].ToString());
            info.baseicmsubst = clsParser.DecimalParse(row["baseicmsubst"].ToString());
            info.basemp = clsParser.DecimalParse(row["basemp"].ToString());
            info.bcipi = clsParser.DecimalParse(row["bcipi"].ToString());
            info.bcpispasep = clsParser.DecimalParse(row["bcpispasep"].ToString());
            info.bccofins1 = clsParser.DecimalParse(row["bccofins1"].ToString());
            info.calculoautomatico = row["calculoautomatico"].ToString();
            info.codigoemp01 = row["codigoemp01"].ToString();
            info.codigoemp02 = row["codigoemp02"].ToString();
            info.codigoemp03 = row["codigoemp03"].ToString();
            info.codigoemp04 = row["codigoemp04"].ToString();
            info.cofins = clsParser.DecimalParse(row["cofins"].ToString());
            info.cofins1 = clsParser.DecimalParse(row["cofins1"].ToString());
            info.cofinsporc = clsParser.DecimalParse(row["cofinsporc"].ToString());
            info.complemento = row["complemento"].ToString();
            info.complemento1 = row["complemento1"].ToString();
            info.consumo = row["consumo"].ToString();
            info.creditaricm = row["creditaricm"].ToString();
            info.csll = clsParser.DecimalParse(row["csll"].ToString());
            info.csllporc = clsParser.DecimalParse(row["csllporc"].ToString());
            info.custoicm = clsParser.DecimalParse(row["custoicm"].ToString());
            info.custoipi = clsParser.DecimalParse(row["custoipi"].ToString());
            info.descricaoesp = row["descricaoesp"].ToString();
            info.fatorconv = clsParser.DecimalParse(row["fatorconv"].ToString());
            info.icm = clsParser.DecimalParse(row["icm"].ToString());
            info.icminterno = clsParser.DecimalParse(row["icminterno"].ToString());
            info.icmsubst = clsParser.DecimalParse(row["icmsubst"].ToString());
            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idcentrocusto = clsParser.Int32Parse(row["idcentrocusto"].ToString());
            info.idcfop = clsParser.Int32Parse(row["idcfop"].ToString());
            info.idcfopfis = clsParser.Int32Parse(row["idcfopfis"].ToString());
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idcodigo1 = clsParser.Int32Parse(row["idcodigo1"].ToString());
            info.idcodigoctabil = clsParser.Int32Parse(row["idcodigoctabil"].ToString());
            info.idcompras = clsParser.Int32Parse(row["idcompras"].ToString());
            info.idcotacao = clsParser.Int32Parse(row["idcotacao"].ToString());
            info.idcotacaoitem = clsParser.Int32Parse(row["idcotacaoitem"].ToString());
            info.idcotacao2 = clsParser.Int32Parse(row["idcotacao2"].ToString());
            info.iddestino = clsParser.Int32Parse(row["iddestino"].ToString());
            info.idhistorico = clsParser.Int32Parse(row["idhistorico"].ToString());
            info.idipi = clsParser.Int32Parse(row["idipi"].ToString());
            info.idos = clsParser.Int32Parse(row["idos"].ToString());
            info.idsittriba = clsParser.Int32Parse(row["idsittriba"].ToString());
            info.idsittribb = clsParser.Int32Parse(row["idsittribb"].ToString());
            info.idsittribipi = clsParser.Int32Parse(row["idsittribipi"].ToString());
            info.idsittribpis = clsParser.Int32Parse(row["idsittribpis"].ToString());
            info.idsittribcofins1 = clsParser.Int32Parse(row["idsittribcofins1"].ToString());
            info.idsolicitacao = clsParser.Int32Parse(row["idsolicitacao"].ToString());
            info.idtiponota = clsParser.Int32Parse(row["idtiponota"].ToString());
            info.idunid = clsParser.Int32Parse(row["idunid"].ToString());
            info.idunidfiscal = clsParser.Int32Parse(row["idunidfiscal"].ToString());
            info.inss = clsParser.DecimalParse(row["inss"].ToString());
            info.inssporc = clsParser.DecimalParse(row["inssporc"].ToString());
            info.ipi = clsParser.DecimalParse(row["ipi"].ToString());
            info.irrf = clsParser.DecimalParse(row["irrf"].ToString());
            info.irrfporc = clsParser.DecimalParse(row["irrfporc"].ToString());
            info.iss = clsParser.DecimalParse(row["iss"].ToString());
            info.issporc = clsParser.DecimalParse(row["issporc"].ToString());
            info.msg = row["msg"].ToString();
            info.observar = row["observar"].ToString();
            info.peso = clsParser.DecimalParse(row["peso"].ToString());
            info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
            info.pis = clsParser.DecimalParse(row["pis"].ToString());
            info.piscofinscsll = clsParser.DecimalParse(row["piscofinscsll"].ToString());
            info.piscofinscsllporc = clsParser.DecimalParse(row["piscofinscsllporc"].ToString());
            info.pispasep = clsParser.DecimalParse(row["pispasep"].ToString());
            info.pisporc = clsParser.DecimalParse(row["pisporc"].ToString());
            info.preco = clsParser.DecimalParse(row["preco"].ToString());
            info.precobruto = clsParser.DecimalParse(row["precobruto"].ToString());
            info.qtde = clsParser.DecimalParse(row["qtde"].ToString());

            info.qtdebaixada = clsParser.DecimalParse(row["qtdebaixada"].ToString());
            info.qtdedefeito = clsParser.DecimalParse(row["qtdedefeito"].ToString());
            info.qtdeentra = clsParser.DecimalParse(row["qtdeentra"].ToString());
            info.qtdeentregue = clsParser.DecimalParse(row["qtdeentregue"].ToString());
            info.qtdefiscal = clsParser.DecimalParse(row["qtdefiscal"].ToString());

            info.qtdeosaux = clsParser.DecimalParse(row["qtdeosaux"].ToString());
            info.qtdesaldo = clsParser.DecimalParse(row["qtdesaldo"].ToString());
            info.qtdesucata = clsParser.DecimalParse(row["qtdesucata"].ToString());
            info.qtdetotal = clsParser.DecimalParse(row["qtdetotal"].ToString());
            info.qtdetransfe = clsParser.DecimalParse(row["qtdetransfe"].ToString());
            info.reducao = clsParser.DecimalParse(row["reducao"].ToString());
//          info.situacao = row["situacao"].ToString();
            info.termino = row["termino"].ToString();
            info.tipodestino = row["tipodestino"].ToString();
            info.totalmercado = clsParser.DecimalParse(row["totalmercado"].ToString());
            info.totalnota = clsParser.DecimalParse(row["totalnota"].ToString());
            info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
            info.totalprevisto = clsParser.DecimalParse(row["totalprevisto"].ToString());
            info.valordesconto = clsParser.DecimalParse(row["valordesconto"].ToString());
            info.valorfrete = clsParser.DecimalParse(row["valorfrete"].ToString());
            info.valorfreteicms = row["valorfreteicms"].ToString();
            info.valoroutras = clsParser.DecimalParse(row["valoroutras"].ToString());
            info.valoroutrasicms = row["valoroutrasicms"].ToString();
            info.valorseguro = clsParser.DecimalParse(row["valorseguro"].ToString());
            info.valorseguroicms = row["valorseguroicms"].ToString();
        }

        private void Compras1Campos(clsCompras1Info info)
        {

            compras1_id = info.id;
            compras1_idcompras = info.idcompras;

            Compras1_tbxBaseicm.Text = info.baseicm.ToString("N2");
            Compras1_tbxBaseicmsubst.Text = info.baseicmsubst.ToString("N2");
            Compras1_tbxBasemp.Text = info.basemp.ToString("N2");
            Compras1_tbxBcipi.Text = info.bcipi.ToString("n2");
            if (info.calculoautomatico == "S") { Compras1_ckxCalculoautomatico.Checked = true; }
            Compras1_tbxCodigoemp01.Text = info.codigoemp01;
            Compras1_tbxCodigoemp02.Text = info.codigoemp02;
            Compras1_tbxCodigoemp03.Text = info.codigoemp03;
            Compras1_tbxCodigoemp04.Text = info.codigoemp04;
            Compras1_tbxCofins.Text = info.cofins.ToString("N2");
            Compras1_tbxCofins1.Text = info.cofins1.ToString("N2");
            Compras1_tbxCofinsPorc.Text = info.cofinsporc.ToString("N2");
            Compras1_tbxComplemento.Text = info.complemento;
            Compras1_tbxComplemento1.Text = info.complemento1;
            if (info.creditaricm == "S") { Compras1_ckxCreditarIcm.Checked = true; }
            Compras1_tbxCsll.Text = info.csll.ToString("N2");
            Compras1_tbxCsllPorc.Text = info.csllporc.ToString("N2");
            Compras1_tbxCustoicm.Text = info.custoicm.ToString("N2");
            Compras1_tbxCustoipi.Text = info.custoipi.ToString("N2");
            //info.desconto
            Compras1_tbxDescricaoEsp.Text = info.descricaoesp;
            Compras1_tbxFatorConversao.Text = info.fatorconv.ToString("N6");
            //   ==================== Compras1_tbxHistoricos_codigo =
            Compras1_tbxIcm.Text = info.icm.ToString("N2");
            Compras1_tbxIcminterno.Text = info.icminterno.ToString("N2");
            Compras1_tbxIcmsubst.Text = info.icmsubst.ToString("N2");

            if (info.idcentrocusto == 0) info.idcentrocusto = clsInfo.zcentrocustos;
            compras1_idcentrocusto = info.idcentrocusto;
            Fill_CentroCusto();

            if (info.idcfop == 0) info.idcfop = clsInfo.zcfop;
            compras1_idcfop = info.idcfop;
            Fill_Cfop();

            if (info.idcfopfis == 0) info.idcfopfis = clsInfo.zcfop;
            compras1_idcfopfis = info.idcfopfis;
            Fill_CfopFis();

            if (info.idcodigo == 0) info.idcodigo = clsInfo.zpecas;
            compras1_idcodigo = info.idcodigo;
            Fill_Pecas();

            tipoproduto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOPRODUTO from PECAS WHERE ID= " + compras1_idcodigo + " ");
            Compras1_lbxTipoProduto.SelectedIndex = Compras1_lbxTipoProduto.FindString(tipoproduto);
            if (Compras1_lbxTipoProduto.SelectedIndex == -1) Compras1_lbxTipoProduto.SelectedIndex = 0;

            if (info.idcodigo1 == 0) info.idcodigo1 = clsInfo.zpecas;
            compras1_idcodigo1 = info.idcodigo1;
            Fill_Pecas2();

            if (info.idcodigoctabil == 0) info.idcodigoctabil = clsInfo.zpecas;
            compras1_idcodigoctabil = info.idcodigoctabil;

            if (info.idcotacao == 0) info.idcotacao = clsInfo.zcotacao;
            compras1_idcotacao = info.idcotacao;
            Compras1_tbxCotacao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from COTACAO WHERE ID= " + compras1_idcotacao);

            compras1_idcotacaoitem = info.idcotacaoitem;
            compras1_idcotacao2 = info.idcotacao2;

            if (info.idhistorico == 0)info.idhistorico = clsInfo.zhistoricos;
            compras1_idhistorico = info.idhistorico;
            Fill_Historico();

            if (info.idipi == 0) info.idipi = clsInfo.zipi;
            compras1_idipi = info.idipi;
            Fill_ClassFiscal();

            compras1_idsittribipi = info.idsittribipi;
            if (compras1_idsittribipi == 0) compras1_idsittribipi = clsInfo.zsittribipi;
            Fill_Sittribipi();

            Compras1_tbxAliqpispasep.Text = info.aliqpispasep.ToString("n2");
            Compras1_tbxBcpispasep.Text = info.bcpispasep.ToString("n2");

            compras1_idsittribpis = info.idsittribpis;
            if (compras1_idsittribpis == 0) compras1_idsittribpis = clsInfo.zsittribpis;
            Fill_Sittribpis();

            Compras1_tbxAliqcofins1.Text = info.aliqcofins1.ToString("n2");
            Compras1_tbxBccofins1.Text = info.bccofins1.ToString("n2");

            compras1_idsittribcofins1 = info.idsittribcofins1;
            if (compras1_idsittribcofins1 == 0) compras1_idsittribcofins1 = clsInfo.zsittribcofins;
            Fill_Sittribcofins();

            if (info.idos == 0) info.idos = clsInfo.zordemservico;
            compras1_idos = info.idos;

            //info.idositem

            if (info.idsittriba == 0)info.idsittriba = clsInfo.zsituacaotriba;
            compras1_idsittriba = info.idsittriba;
            Fill_Sittriba();

            if (info.idsittribb == 0) info.idsittribb = clsInfo.zsituacaotribb;
            compras1_idsittribb = info.idsittribb;
            Fill_Sittribb();

            if (info.idsolicitacao == 0) info.idsolicitacao = clsInfo.zsolicitacao;
            compras1_idsolicitacao = info.idsolicitacao;
            Compras1_tbxSolicitacao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from SOLICITACAO WHERE ID= " + compras1_idsolicitacao);


            if (info.idtiponota == 0)
            {
                info.idtiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from tiponota where codigo='" + "0000-000" + "'"));
            }
            compras1_idtiponota = info.idtiponota;
            Compras1_cbxTiponota.SelectedIndex = Compras1_cbxTiponota.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where ID= " + compras1_idtiponota));
            if (Compras1_cbxTiponota.SelectedIndex == -1)
            {
                Compras1_cbxTiponota.SelectedIndex = 0;
            }

            if (info.idunid == 0) info.idunid = clsInfo.zunidade;
            compras1_idunid = info.idunid;
            Fill_Unidade();

            if (info.idunidfiscal == 0) info.idunidfiscal = clsInfo.zunidade;
            compras1_idunidfiscal = info.idunidfiscal;
            Fill_UnidFiscal();

            Compras1_tbxInss.Text = info.inss.ToString("N2"); 
            Compras1_tbxInssPorc.Text = info.inssporc.ToString("N2");
            Compras1_tbxIpi.Text = info.ipi.ToString("N2");
            Compras1_tbxIrrf.Text = info.irrf.ToString("N2");
            Compras1_tbxIrrfPorc.Text = info.irrfporc.ToString("N2");
            Compras1_tbxIss.Text = info.iss.ToString("N2");
            Compras1_tbxIssPorc.Text = info.issporc.ToString("N2");
            // info.item.ToString("N0");
            //info.msg;
            //info.observar;
            Compras1_tbxPeso.Text = info.peso.ToString("N3");
            Compras1_tbxTotalPeso.Text = info.totalpeso.ToString("N3"); //info.totalpeso;
            Compras1_tbxPis.Text = info.pis.ToString("N2");
            Compras1_tbxPisCofinsCsll.Text = info.piscofinscsll.ToString("N2");
            Compras1_tbxPisCofinsCsllPorc.Text = info.piscofinscsllporc.ToString("N2");
            Compras1_tbxPispasep.Text = info.pispasep.ToString("N2");
            Compras1_tbxPisPorc.Text = info.pisporc.ToString("N2");
            Compras1_tbxPreco.Text = info.preco.ToString("N6");
            //info.precobruto;
            Compras1_tbxQtde.Text = info.qtde.ToString("N3");
            Compras1_tbxQtdebaixada.Text = info.qtdebaixada.ToString("N3");
            Compras1_tbxQtdedefeito.Text = info.qtdedefeito.ToString("N3");
            //info.qtdeentra.ToString("N3");
            Compras1_tbxQtdeentregue.Text = info.qtdeentregue.ToString("N3");
            Compras1_tbxQtdeFiscal.Text = info.qtdefiscal.ToString("N3");
            Compras1_tbxQtdeosaux.Text = info.qtdeosaux.ToString("N3");
            Compras1_tbxQtdesaldo.Text = info.qtdesaldo.ToString("N3");
            Compras1_tbxQtdesucata.Text = info.qtdesucata.ToString("N3");
            // info.qtdetotal;
            //info.qtdetransfe
            Compras1_tbxReducao.Text = info.reducao.ToString("N2");
            // info.situacao;
            // info.termino
            if (info.tipodestino == "M")
            {
                Compras1_rbnTipodestinoMaq.Checked = true;
            }
            else
            {
                Compras1_rbnTipodestinoIte.Checked = true;
            }
            //Compras1_lbxTipoProduto.SelectedIndex = (Compras1_lbxTipoProduto.Text.Substring(0,2).FindString(info.tipoentrada));
            //Compras1_lbxTipoProduto.SelectedIndex = clsVisual.SelecionarIndex(info.tipoentrada, 2, Compras1_lbxTipoProduto);
            //if (Compras1_lbxTipoProduto.SelectedIndex == -1)
            //{
            //    Compras1_lbxTipoProduto.SelectedIndex = 0;
            //}
            Compras1_tbxTotalmercado.Text = info.totalmercado.ToString("N2");
            Compras1_tbxTotalnota.Text = info.totalnota.ToString("N2");
            Compras1_tbxTotalPeso.Text = info.totalpeso.ToString("N3");
            Compras1_tbxTotalPrevisto.Text = info.totalprevisto.ToString("N2");
            //info.valordesconto;
            Compras1_tbxValorfrete.Text = info.valorfrete.ToString("N2");
            if (info.valorfreteicms == "S")
            {
                Compras1_ckxValorFreteIcms.Checked = true;
                Compras1_ckxValorFreteIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                Compras1_ckxValorFreteIcms.Text = "Não Incide ICMS";
            }
            Compras1_tbxValoroutras.Text = info.valoroutras.ToString("N2");
            if (info.valoroutrasicms == "S")
            {
                Compras1_ckxValorOutrasIcms.Checked = true;
                Compras1_ckxValorOutrasIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                Compras1_ckxValorOutrasIcms.Text = "Não Incide ICMS";
            }
            Compras1_tbxValorseguro.Text = info.valorseguro.ToString("N2");
            if (info.valorseguroicms == "S")
            {
                Compras1_ckxValorSeguroIcms.Checked = true;
                Compras1_ckxValorSeguroIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                Compras1_ckxValorSeguroIcms.Text = "Não Incide ICMS";
            }

            //info.vidautil;

            // IMPOSTOS DE MÃO DE OBRA
            if (Compras1_lbxTipoProduto.Text.Substring(0, 2) == "30")
            {
                gbxPrestacaoServico.Enabled = true;
            }
            else
            {
                gbxPrestacaoServico.Enabled = false;
            }

            if (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) > 0 & DateTime.Parse(tbxData.Text).ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy") )
            {
                Item_gbxProduto.Enabled = false;
            }
            else
            {
                Item_gbxProduto.Enabled = true;
                Compras1_tbxPecas_codigo.Select();
            }

            // padronizar a Programação de Entrega
            tbxQtdeEntregas.Text = "1";
            tbxDiaFixo.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            lbxTipoCalculo.SelectedIndex = lbxTipoCalculo.FindString("Unica");
        }

        private void Compras1FillInfo(clsCompras1Info info)
        {
            info.id = compras1_id;
            info.idcompras = compras1_idcompras; // id

            info.aliqpispasep = clsParser.DecimalParse(Compras1_tbxAliqpispasep.Text);
            info.aliqcofins1 = clsParser.DecimalParse(Compras1_tbxAliqcofins1.Text);

            info.baseicm = clsParser.DecimalParse(Compras1_tbxBaseicm.Text);
            info.baseicmsubst = clsParser.DecimalParse(Compras1_tbxBaseicmsubst.Text);
            info.basemp = clsParser.DecimalParse(Compras1_tbxBasemp.Text);
            info.bcipi = clsParser.DecimalParse(Compras1_tbxBcipi.Text);
            info.bcpispasep = clsParser.DecimalParse(Compras1_tbxBcpispasep.Text);
            info.bccofins1 = clsParser.DecimalParse(Compras1_tbxBccofins1.Text);

            if (Compras1_ckxCalculoautomatico.Checked == true)
            {
                info.calculoautomatico = "S";
            }
            else
            {
                info.calculoautomatico = "N";
            }
            info.codigoemp01 = Compras1_tbxCodigoemp01.Text;
            info.codigoemp02 = Compras1_tbxCodigoemp02.Text;
            info.codigoemp03 = Compras1_tbxCodigoemp03.Text;
            info.codigoemp04 = Compras1_tbxCodigoemp04.Text;
            info.cofins = clsParser.DecimalParse(Compras1_tbxCofins.Text);
            info.cofins1 = clsParser.DecimalParse(Compras1_tbxCofins1.Text);
            info.cofinsporc = clsParser.DecimalParse(Compras1_tbxCofinsPorc.Text);
            info.complemento = Compras1_tbxComplemento.Text;
            info.complemento1 = Compras1_tbxComplemento1.Text;
            if (Compras1_ckxCreditarIcm.Checked == true)
            {
                info.creditaricm = "S";
            }
            else
            {
                info.creditaricm = "N";
            }
            info.csll = clsParser.DecimalParse(Compras1_tbxCsll.Text);
            info.csllporc = clsParser.DecimalParse(Compras1_tbxCsllPorc.Text);
            info.custoicm = clsParser.DecimalParse(Compras1_tbxCustoicm.Text);
            info.custoipi = clsParser.DecimalParse(Compras1_tbxCustoipi.Text);
            //info.desconto
            info.descricaoesp = Compras1_tbxDescricaoEsp.Text;
            info.fatorconv = clsParser.DecimalParse(Compras1_tbxFatorConversao.Text);
            info.icm = clsParser.DecimalParse(Compras1_tbxIcm.Text);
            info.icminterno = clsParser.DecimalParse(Compras1_tbxIcminterno.Text);
            info.icmsubst = clsParser.DecimalParse(Compras1_tbxIcmsubst.Text);
            info.idcentrocusto = compras1_idcentrocusto;
            info.idcfop = compras1_idcfop;
            info.idcfopfis = compras1_idcfopfis;
            info.idcodigo = compras1_idcodigo;
            info.idcodigo1 = compras1_idcodigo1;
            info.idcodigoctabil = compras1_idcodigoctabil;
            info.idcotacao = compras1_idcotacao;
            info.idcotacaoitem = compras1_idcotacaoitem;
            info.idcotacao2 = compras1_idcotacao2;
            info.iddestino = compras1_iddestino;
            info.idhistorico = compras1_idhistorico;
            info.idipi = compras1_idipi;
            info.idos = compras1_idos;
            //info.idositem;
            info.idsittriba = compras1_idsittriba;
            info.idsittribb = compras1_idsittribb;
            info.idsittribipi = compras1_idsittribipi;
            info.idsittribcofins1 = compras1_idsittribcofins1;
            info.idsittribpis = compras1_idsittribpis;
            info.idsolicitacao = compras1_idsolicitacao;
            info.idtiponota = compras1_idtiponota;
            info.idunid = compras1_idunid;
            info.idunidfiscal = compras1_idunidfiscal;
            info.inss = clsParser.DecimalParse(Compras1_tbxInss.Text);

            info.inssporc = clsParser.DecimalParse(Compras1_tbxInssPorc.Text);
            info.ipi = clsParser.DecimalParse(Compras1_tbxIpi.Text);
            info.irrf = clsParser.DecimalParse(Compras1_tbxIrrf.Text);
            info.irrfporc = clsParser.DecimalParse(Compras1_tbxIrrfPorc.Text);
            info.iss = clsParser.DecimalParse(Compras1_tbxIss.Text);
            info.issporc = clsParser.DecimalParse(Compras1_tbxIssPorc.Text);
            //info.item 
            //info.msg
            //info.observar
            info.peso = clsParser.DecimalParse(Compras1_tbxPeso.Text);
            info.pis = clsParser.DecimalParse(Compras1_tbxPis.Text);
            info.piscofinscsll = clsParser.DecimalParse(Compras1_tbxPisCofinsCsll.Text);
            info.piscofinscsllporc = clsParser.DecimalParse(Compras1_tbxPisCofinsCsllPorc.Text);
            info.pispasep = clsParser.DecimalParse(Compras1_tbxPispasep.Text);
            info.pisporc = clsParser.DecimalParse(Compras1_tbxPisPorc.Text);
            info.preco = clsParser.DecimalParse(Compras1_tbxPreco.Text);
            //info.precobruto;
            info.qtde = clsParser.DecimalParse(Compras1_tbxQtde.Text);
            info.qtdebaixada = clsParser.DecimalParse(Compras1_tbxQtdebaixada.Text);
            info.qtdedefeito = clsParser.DecimalParse(Compras1_tbxQtdedefeito.Text);
            //info.qtdeentra;
            info.qtdeentregue = clsParser.DecimalParse(Compras1_tbxQtdeentregue.Text);
            info.qtdefiscal = clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text);
            info.qtdeosaux = clsParser.DecimalParse(Compras1_tbxQtdeosaux.Text);
            info.qtdesaldo = clsParser.DecimalParse(Compras1_tbxQtdesaldo.Text);
            info.qtdesucata = clsParser.DecimalParse(Compras1_tbxQtdesucata.Text);
            //info.qtdetotal 
            //info.qtdetransfe 
            info.reducao = clsParser.DecimalParse(Compras1_tbxReducao.Text);
            // info.situacao;
            //info.termino;
            
            if (Compras1_rbnTipodestinoMaq.Checked == true)
            {
                info.tipodestino = "M";
            }
            else
            {
                info.tipodestino = "I";  //Compras1_rbnTipodestinoIte.Checked = true;
            }
            //info.tipoentrada = Compras1_lbxTipoProduto.Text.Substring(0, 2);
            info.totalmercado = clsParser.DecimalParse(Compras1_tbxTotalmercado.Text);
            info.totalnota = clsParser.DecimalParse(Compras1_tbxTotalnota.Text);
            info.totalpeso = clsParser.DecimalParse(Compras1_tbxTotalPeso.Text);
            info.totalprevisto =  clsParser.DecimalParse(Compras1_tbxTotalPrevisto.Text);
            //info.valordesconto;
            info.valorfrete = clsParser.DecimalParse(Compras1_tbxValorfrete.Text);
            if (Compras1_ckxValorFreteIcms.Checked == true)
            {
                info.valorfreteicms = "S";
            }
            info.valoroutras = clsParser.DecimalParse(Compras1_tbxValoroutras.Text);
            if (Compras1_ckxValorOutrasIcms.Checked == true)
            {
                info.valoroutrasicms = "S";
            }
            info.valorseguro = clsParser.DecimalParse(Compras1_tbxValorseguro.Text);
            if (Compras1_ckxValorSeguroIcms.Checked == true)
            {
                info.valorseguroicms = "S";
            }
            //info.vidautil;
        }

        private void Compras1FillInfoToGrid(clsCompras1Info info)
        {
            DataRow row;
            DataRow[] rows = dtCompras1.Select("compras1_posicao = " + compras1_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtCompras1.NewRow();
            }
            row["id"] = info.id;
            row["idcompras"] = info.idcompras;

            row["aliqpispasep"] = info.aliqpispasep;
            row["aliqcofins1"] = info.aliqcofins1;
            row["baseicm"] = info.baseicm;
            row["baseicmsubst"] = info.baseicmsubst;
            row["basemp"] = info.basemp;
            row["bcpispasep"] = info.bcpispasep;
            row["bccofins1"] = info.bccofins1;
            row["bcipi"] = info.bcipi;
            row["calculoautomatico"] = info.calculoautomatico;
            row["codigoemp01"] = info.codigoemp01;
            row["codigoemp02"] = info.codigoemp02;
            row["codigoemp03"] = info.codigoemp03;
            row["codigoemp04"] = info.codigoemp04;
            row["cofins"] = info.cofins;
            row["cofins1"] = info.cofins1;
            row["cofinsporc"] = info.cofinsporc;
            row["complemento"] = info.complemento;
            row["complemento1"] = info.complemento1;
            row["consumo"] = info.consumo;
            row["creditaricm"] = info.creditaricm;
            row["csll"] = info.csll;
            row["csllporc"] = info.csllporc;
            row["custoicm"] = info.custoicm;
            row["custoipi"] = info.custoipi;
            //info.desconto;
            row["descricaoesp"] = info.descricaoesp;
            row["fatorconv"] = info.fatorconv;
            //row["fatura"] = info.fatura;
            //row["faturando"] = info.faturando;
            row["icm"] = info.icm;
            row["icminterno"] = info.icminterno;
            row["icmsubst"] = info.icmsubst;
            row["idcentrocusto"] = info.idcentrocusto;
            row["idcfop"] = info.idcfop;
            row["idcfopfis"] = info.idcfopfis;
            row["idcodigo"] = info.idcodigo;
            row["idcodigo1"] = info.idcodigo1;
            row["idcodigoctabil"] = info.idcodigoctabil;
            row["idcotacao"] = info.idcotacao;
            row["idcotacaoitem"] = info.idcotacaoitem;
            row["idcotacao2"] = info.idcotacao2;
            row["iddestino"] = info.iddestino;
            row["idhistorico"] = info.idhistorico;
            row["idipi"] = info.idipi;
            row["idos"] = info.idos;
            //row["idositem"] = info.idositem;
            row["idsittriba"] = info.idsittriba;
            row["idsittribb"] = info.idsittribb;
            row["idsittribipi"] = info.idsittribipi;
            row["idsittribpis"] = info.idsittribpis;
            row["idsittribcofins1"] = info.idsittribcofins1;
            row["idsolicitacao"] = info.idsolicitacao;
            row["idtiponota"] = info.idtiponota;
            row["idunid"] = info.idunid;
            row["idunidfiscal"] = info.idunidfiscal;
            row["inss"] = info.inss;
            row["inssporc"] = info.inssporc;
            row["ipi"] = info.ipi;
            row["irrf"] = info.irrf;
            row["irrfporc"] = info.irrfporc;
            row["iss"] = info.iss;
            row["issporc"] = info.issporc;
            row["msg"] = info.msg;
            row["observar"] = info.observar;
            row["peso"] = info.peso;
            row["totalpeso"] = info.totalpeso;
            row["pis"] = info.pis;
            row["piscofinscsll"] = info.piscofinscsll;
            row["piscofinscsllporc"] = info.piscofinscsllporc;
            row["pispasep"] = info.pispasep;
            row["pisporc"] = info.pisporc;
            row["preco"] = info.preco;
            row["precobruto"] = info.precobruto;
            row["qtde"] = info.qtde;
            row["qtdebaixada"] = info.qtdebaixada;
            row["qtdedefeito"] = info.qtdedefeito;
            //row["qtdeentra"] = info.qtdeentra;
            //row["qtdeentregue"] = info.qtdeentregue;
            row["qtdefiscal"] = info.qtdefiscal;
            row["qtdeosaux"] = info.qtdeosaux;
            row["qtdesaldo"] = info.qtdesaldo;
            row["qtdesucata"] = info.qtdesucata;
            row["qtdetotal"] = info.qtdetotal;
            row["qtdetransfe"] = info.qtdetransfe;
            row["reducao"] = info.reducao;
            //row["situacao"] = info.situacao;
            row["termino"] = info.termino;
            row["tipodestino"] = info.tipodestino;
            //row["tipoentrada"] = info.tipoentrada;
            row["totalmercado"] = info.totalmercado;
            row["totalnota"] = info.totalnota;
            row["totalpeso"] = info.totalpeso;
            row["totalprevisto"] = info.totalprevisto;
            //info.valordesconto
            row["valorfrete"] = info.valorfrete;
            row["valorfreteicms"] = info.valorfreteicms;
            row["valoroutras"] = info.valoroutras;
            row["valoroutrasicms"] = info.valoroutrasicms;
            row["valorseguro"] = info.valorseguro;
            row["valorseguroicms"] = info.valorseguroicms;

            // Colunas que petencem a outras tabelas
            row["unidfiscal"] = Compras1_tbxUnidadeFiscal.Text;
            row["unid"] = Compras1_tbxUnidade.Text;
            row["cfop"] = Compras1_tbxCfopfis.Text;
            row["codigo"] = Compras1_tbxPecas_codigo.Text;
            if (Compras1_tbxPecas_codigo.Text == "0")
            {
                row["DESCRICAO"] = Compras1_tbxComplemento.Text;
                row["PECASDESCRI"] = Compras1_tbxComplemento.Text;
            }
            else
            {
                row["DESCRICAO"] = Compras1_tbxPecas_nome.Text;
                row["PECASDESCRI"] = Compras1_tbxPecas_nome.Text;
            }

            if (rows.Length == 0)
            {
                row["compras1_posicao"] = compras1_posicao;
                dtCompras1.Rows.Add(row);
            }
        }
        private void ComprasSomar()
        {
            //
            tbxTotalbaseicm.Text = "0";
            tbxTotalbaseicmsubst.Text = "0";
            tbxCofins.Text = "0";
            tbxTotalcofins1.Text = "0";
            tbxCsll.Text = "0";
            tbxTotalicm.Text = "0";
            tbxTotalipi.Text = "0";
            // info.desconto = Decimal.Parse(row["desconto"].ToString());
            tbxInss.Text = "0";
            tbxTotalipi.Text = "0";
            tbxIrrf.Text = "0";
            tbxIss.Text = "0";
            tbxPis.Text = "0";
            tbxPisCofinsCsll.Text = "0";
            tbxTotalpispasep.Text = "0";
            tbxQtdebaixada.Text = "0";
            tbxQtdedefeito.Text = "0";
            tbxQtdeentregue.Text = "0";
            tbxTotalpeca.Text = "0";
            tbxTotalpeso.Text = "0";
            tbxQtdeosaux.Text = "0";
            tbxQtdesaldo.Text = "0";
            tbxQtdesucata.Text = "0";
            tbxTotalmercadoria.Text = "0";
            tbxTotalpedido.Text = "0";
            tbxTotalprevisto.Text = "0";
            tbxTotalfrete.Text = "0";
            tbxTotaloutras.Text = "0";
            tbxTotalseguro.Text = "0";


            foreach (DataRow row in dtCompras1.Rows)
            {
                if (row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Detached)
                {
                    continue;   // não somar se apagou
                }
                else
                {
                    // Somar os campos do cabeçalho
                    tbxTotalbaseicm.Text = (clsParser.DecimalParse(tbxTotalbaseicm.Text) + clsParser.DecimalParse(row["baseicm"].ToString())).ToString("N2");
                    tbxTotalbaseicmsubst.Text = (clsParser.DecimalParse(tbxTotalbaseicmsubst.Text) + clsParser.DecimalParse(row["baseicmsubst"].ToString())).ToString("N2");
                    tbxCofins.Text = (clsParser.DecimalParse(tbxCofins.Text) + clsParser.DecimalParse(row["cofins"].ToString())).ToString("N2");
                    tbxTotalcofins1.Text = (clsParser.DecimalParse(tbxTotalcofins1.Text) + clsParser.DecimalParse(row["cofins1"].ToString())).ToString("N2");
                    tbxCsll.Text = (clsParser.DecimalParse(tbxCsll.Text) + clsParser.DecimalParse(row["csll"].ToString())).ToString("N2");
                    tbxTotalicm.Text = (clsParser.DecimalParse(tbxTotalicm.Text) + clsParser.DecimalParse(row["custoicm"].ToString())).ToString("N2");
                    tbxTotalipi.Text = (clsParser.DecimalParse(tbxTotalipi.Text) + clsParser.DecimalParse(row["custoipi"].ToString())).ToString("N2");
                    // info.desconto = (clsParser.DecimalParse(row["desconto"].ToString());
                    tbxInss.Text = (clsParser.DecimalParse(tbxInss.Text) + clsParser.DecimalParse(row["inss"].ToString())).ToString("N2");
                    //tbxTotalipi.Text = (clsParser.DecimalParse(tbxTotalipi.Text) + clsParser.DecimalParse(row["ipi"].ToString())).ToString("N2");
                    tbxIrrf.Text = (clsParser.DecimalParse(tbxIrrf.Text) + clsParser.DecimalParse(row["irrf"].ToString())).ToString("N2");
                    tbxIss.Text = (clsParser.DecimalParse(tbxIss.Text) + clsParser.DecimalParse(row["iss"].ToString())).ToString("N2");
                    tbxPis.Text = (clsParser.DecimalParse(tbxPis.Text) + clsParser.DecimalParse(row["pis"].ToString())).ToString("N2");
                    tbxPisCofinsCsll.Text = (clsParser.DecimalParse(tbxPisCofinsCsll.Text) + clsParser.DecimalParse(row["piscofinscsll"].ToString())).ToString("N2");
                    tbxTotalpispasep.Text = (clsParser.DecimalParse(tbxTotalpispasep.Text) + clsParser.DecimalParse(row["pispasep"].ToString())).ToString("N2");
                    tbxQtdebaixada.Text = (clsParser.DecimalParse(tbxQtdebaixada.Text) + clsParser.DecimalParse(row["qtdebaixada"].ToString())).ToString("N3");
                    tbxQtdedefeito.Text = (clsParser.DecimalParse(tbxQtdedefeito.Text) + clsParser.DecimalParse(row["qtdedefeito"].ToString())).ToString("N3");
                    tbxQtdeentregue.Text = (clsParser.DecimalParse(tbxQtdeentregue.Text) + clsParser.DecimalParse(row["qtdeentregue"].ToString())).ToString("N3");
                    tbxTotalpeca.Text = (clsParser.DecimalParse(tbxTotalpeca.Text) + clsParser.DecimalParse(row["qtdefiscal"].ToString())).ToString("N3");
                    tbxQtdeosaux.Text = (clsParser.DecimalParse(tbxQtdeosaux.Text) + clsParser.DecimalParse(row["qtdeosaux"].ToString())).ToString("N3");
                    tbxQtdesaldo.Text = (clsParser.DecimalParse(tbxQtdesaldo.Text) + clsParser.DecimalParse(row["qtdesaldo"].ToString())).ToString("N3");
                    tbxQtdesucata.Text = (clsParser.DecimalParse(tbxQtdesucata.Text) + clsParser.DecimalParse(row["qtdesucata"].ToString())).ToString("N3");
                    tbxTotalpeso.Text = (clsParser.DecimalParse(tbxTotalpeso.Text) + clsParser.DecimalParse(row["totalpeso"].ToString())).ToString("N3");
                    tbxTotalmercadoria.Text = (clsParser.DecimalParse(tbxTotalmercadoria.Text) + clsParser.DecimalParse(row["totalmercado"].ToString())).ToString("N2");
                    tbxTotalpedido.Text = (clsParser.DecimalParse(tbxTotalpedido.Text) + clsParser.DecimalParse(row["totalnota"].ToString())).ToString("N2");
                    tbxTotalprevisto.Text = (clsParser.DecimalParse(tbxTotalprevisto.Text) + clsParser.DecimalParse(row["totalprevisto"].ToString())).ToString("N2");
                    tbxTotalfrete.Text = (clsParser.DecimalParse(tbxTotalfrete.Text) + clsParser.DecimalParse(row["valorfrete"].ToString())).ToString("N2");
                    tbxTotaloutras.Text = (clsParser.DecimalParse(tbxTotaloutras.Text) + clsParser.DecimalParse(row["valoroutras"].ToString())).ToString("N2");
                    tbxTotalseguro.Text = (clsParser.DecimalParse(tbxTotalseguro.Text) + clsParser.DecimalParse(row["valorseguro"].ToString())).ToString("N2");
                }
            }
        }
        private void Calcular()
        {
            if (Compras1_ckxCalculoautomatico.Checked == true &&
                Compras1_cbxSittriba.SelectedIndex != -1 &&
                clsParser.DecimalParse(Compras1_tbxPreco.Text) > 0)
            {
                if (compras1_id > 0 &&
                    clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text) != clsParser.DecimalParse(Compras1_tbxQtdesaldo.Text))
                {
                    return;
                }

                clsNotafiscal NF = new clsNotafiscal();
                
                NF.iduforigem = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDESTADO from CLIENTE where ID=" + compras_idfornecedor));
                NF.idufdestino = clsInfo.zempresa_ufid;
                NF.idipi = compras1_idipi;
                NF.idcliente = clsInfo.zempresaclienteid;
                NF.revendedor = false;
                NF.isentoipi = false;
                NF.arealivrecomercio = false;
                NF.consumo = false;

                NF.qtde = clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text);
                NF.pesounit = clsParser.DecimalParse(Compras1_tbxPeso.Text);
                NF.preco = clsParser.DecimalParse(Compras1_tbxPreco.Text);

                NF.origem = Compras1_cbxSittriba.Text.Substring(0, 1);
                NF.icmscst = Compras1_cbxSittribb.Text.Substring(0, 2);
                NF.icms = clsParser.DecimalParse(Compras1_tbxIcm.Text);
                NF.icmsreducao = clsParser.DecimalParse(Compras1_tbxBasemp.Text);

                NF.icmsst = clsParser.DecimalParse(Compras1_tbxIcminterno.Text);
                NF.icmsstreducao = clsParser.DecimalParse(Compras1_tbxIcmssubstreducao.Text);
                NF.icmsstmva = clsParser.DecimalParse(Compras1_tbxReducao.Text);

                if (Compras1_cbxSittribipi.SelectedIndex == -1)
                {
                    Compras1_cbxSittribipi.SelectedIndex = 0;
                }

                NF.ipicst = Compras1_cbxSittribipi.Text.Substring(0, 2);
                NF.ipi = clsParser.DecimalParse(Compras1_tbxIpi.Text);

                NF.piscst = Compras1_cbxSittribpis.Text;
                NF.pis = clsParser.DecimalParse(Compras1_tbxAliqpispasep.Text);
                NF.cofinscst = Compras1_cbxSittribcofins1.Text;
                NF.cofins = clsParser.DecimalParse(Compras1_tbxAliqcofins1.Text);

                NF.valoroutrassomabcicms = Compras1_ckxValorOutrasIcms.Checked;
                NF.valoroutras = clsParser.DecimalParse(Compras1_tbxValoroutras.Text);

                NF.valorfretesomabcicms = Compras1_ckxValorFreteIcms.Checked;
                NF.valorfrete = clsParser.DecimalParse(Compras1_tbxValorfrete.Text);
                
                NF.valorsegurosomabcicms = Compras1_ckxValorSeguroIcms.Checked;
                NF.valorseguro = clsParser.DecimalParse(Compras1_tbxValorseguro.Text);

                NF.CalcularNota();

                if (Compras1_cbxSittribipi.SelectedIndex == -1)
                {
                    Compras1_cbxSittribipi.SelectedIndex = 0;
                }

                Compras1_cbxSittribipi.SelectedIndex = SelecionarIndex(NF.ipicst, 2, Compras1_cbxSittribipi);
                Ipi_Check();

                Compras1_cbxSittribb.SelectedIndex = SelecionarIndex(NF.icmscst, 2, Compras1_cbxSittribb);
                Sittribb_Check();

                Compras1_cbxSittribpis.SelectedIndex = SelecionarIndex(NF.piscst, 2, Compras1_cbxSittribpis);
                Sittribpis_Check();

                Compras1_cbxSittribcofins1.SelectedIndex = SelecionarIndex(NF.cofinscst, 2, Compras1_cbxSittribcofins1);
                Sittribcofins_Check();

                Compras1_tbxQtdeFiscal.Text = NF.qtde.ToString("n3");
                Compras1_tbxTotalPeso.Text = NF.pesototal.ToString("n3");
                Compras1_tbxPreco.Text = NF.preco.ToString("n6");
                Compras1_tbxBaseicm.Text = NF.icmsbc.ToString("N2");
                Compras1_tbxIcm.Text = NF.icms.ToString("N2");
                Compras1_tbxBasemp.Text = NF.icmsreducao.ToString("N2");
                Compras1_tbxCustoicm.Text = NF.icmstotal.ToString("N2");
                Compras1_tbxBaseicmsubst.Text = NF.icmsstbc.ToString("N2");
                Compras1_tbxIcminterno.Text = NF.icmsst.ToString("N2");
                Compras1_tbxIcmssubstreducao.Text = NF.icmsstreducao.ToString("N2");
                Compras1_tbxReducao.Text = NF.icmsstmva.ToString("N2");
                Compras1_tbxIcmsubst.Text = NF.icmssttotal.ToString("N2");
                Compras1_tbxBcipi.Text = NF.ipibc.ToString("N2");
                Compras1_tbxIpi.Text = NF.ipi.ToString("N2");
                Compras1_tbxCustoipi.Text = NF.ipitotal.ToString("N2");
                Compras1_tbxBcpispasep.Text = NF.pisbc.ToString("N2");
                Compras1_tbxAliqpispasep.Text = NF.pis.ToString("N2");
                Compras1_tbxPispasep.Text = NF.pistotal.ToString("N2");
                Compras1_tbxBccofins1.Text = NF.cofinsbc.ToString("N2");
                Compras1_tbxAliqcofins1.Text = NF.cofins.ToString("N2");
                Compras1_tbxCofins1.Text = NF.cofinstotal.ToString("N2");
                Compras1_tbxTotalmercado.Text = NF.totalmercadoria.ToString("N2");
                Compras1_tbxTotalnota.Text = NF.totalnota.ToString("N2");
                Compras1_tbxValoroutras.Text = NF.valoroutras.ToString("N2");
                Compras1_tbxValorfrete.Text = NF.valorfrete.ToString("N2");
                Compras1_tbxValorseguro.Text = NF.valorseguro.ToString("N2");

                // Somar Impostos Prestação de Serviços
                // prestação de serviços (impostos)
                Decimal por = clsParser.DecimalParse(clsCompras1Info.irrfporc.ToString()) + clsParser.DecimalParse(clsCompras1Info.inssporc.ToString()) + clsParser.DecimalParse(clsCompras1Info.piscofinscsllporc.ToString()) + clsParser.DecimalParse(clsCompras1Info.issporc.ToString()) + clsParser.DecimalParse(clsCompras1Info.pisporc.ToString()) + clsParser.DecimalParse(clsCompras1Info.cofinsporc.ToString()) + clsParser.DecimalParse(clsCompras1Info.csllporc.ToString());
                if (Compras1_lbxTipoProduto.Text.PadRight(2, ' ').Substring(0, 2) == "30")
                {
                    if (por == 0)
                    {
                        clsCompras1Info.irrfporc = clsInfo.zfiscalirrf;
                        clsCompras1Info.inssporc = clsInfo.zfiscalinss;
                        clsCompras1Info.piscofinscsllporc = clsInfo.zfiscalpiscofins;
                        clsCompras1Info.issporc = clsInfo.zfiscaliss;
                        clsCompras1Info.pisporc = clsInfo.zfiscalpis;
                        clsCompras1Info.cofinsporc = clsInfo.zfiscalcofins;
                        clsCompras1Info.csllporc = clsInfo.zfiscalcsll;
                        Compras1_tbxIrrfPorc.Text = clsCompras1Info.irrfporc.ToString("N2");
                        Compras1_tbxInssPorc.Text = clsCompras1Info.inssporc.ToString("N2");
                        Compras1_tbxPisCofinsCsllPorc.Text = clsCompras1Info.piscofinscsllporc.ToString("N2");
                        Compras1_tbxIssPorc.Text = clsCompras1Info.issporc.ToString("N2");
                        Compras1_tbxPisPorc.Text = clsCompras1Info.pisporc.ToString("N2");
                        Compras1_tbxCofinsPorc.Text = clsCompras1Info.cofinsporc.ToString("N2");
                        Compras1_tbxCsllPorc.Text = clsCompras1Info.csllporc.ToString("N2");
                    }
                }

                Compras1_tbxIrrfPorc.Text = clsParser.DecimalParse(Compras1_tbxIrrfPorc.Text).ToString("N2");
                Compras1_tbxIrrf.Text = clsParser.DecimalParse(Compras1_tbxIrrf.Text).ToString("N2");
                Compras1_tbxInssPorc.Text = clsParser.DecimalParse(Compras1_tbxInssPorc.Text).ToString("N2");
                Compras1_tbxInss.Text = clsParser.DecimalParse(Compras1_tbxInss.Text).ToString("N2");
                Compras1_tbxPisCofinsCsllPorc.Text = clsParser.DecimalParse(Compras1_tbxPisCofinsCsllPorc.Text).ToString("N2");
                Compras1_tbxPisCofinsCsll.Text = clsParser.DecimalParse(Compras1_tbxPisCofinsCsll.Text).ToString("N2");
                Compras1_tbxIssPorc.Text = clsParser.DecimalParse(Compras1_tbxIssPorc.Text).ToString("N2");
                Compras1_tbxIss.Text = clsParser.DecimalParse(Compras1_tbxIss.Text).ToString("N2");
                Compras1_tbxPisPorc.Text = clsParser.DecimalParse(Compras1_tbxPisPorc.Text).ToString("N2");
                Compras1_tbxPis.Text = clsParser.DecimalParse(Compras1_tbxPis.Text).ToString("N2");
                Compras1_tbxCofinsPorc.Text = clsParser.DecimalParse(Compras1_tbxCofinsPorc.Text).ToString("N2");
                Compras1_tbxCofins.Text = clsParser.DecimalParse(Compras1_tbxCofins.Text).ToString("N2");
                Compras1_tbxCsllPorc.Text = clsParser.DecimalParse(Compras1_tbxCsllPorc.Text).ToString("N2");
                Compras1_tbxCsll.Text = clsParser.DecimalParse(Compras1_tbxCsll.Text).ToString("N2");

                if (Compras1_ckxIrrf.Checked == true)
                {
                    Compras1_ckxIrrf.Text = "Sim Tem I.R.R.F.";
                    if (clsParser.DecimalParse(Compras1_tbxIrrfPorc.Text) > 0)
                    {
                        Compras1_tbxIrrf.Text = (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) * (clsParser.DecimalParse(Compras1_tbxIrrfPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    Compras1_ckxIrrf.Text = "Não Tem I.R.R.F.";
                    Compras1_tbxIrrf.Text = 0.ToString("N2");
                }
                if (Compras1_ckxInss.Checked == true)
                {
                    Compras1_ckxInss.Text = "Sim Tem I.N.S.S.";
                    if (clsParser.DecimalParse(Compras1_tbxInssPorc.Text) > 0)
                    {
                        Compras1_tbxInss.Text = (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) * (clsParser.DecimalParse(Compras1_tbxInssPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    Compras1_ckxInss.Text = "Não Tem I.N.S.S.";
                    Compras1_tbxInss.Text = 0.ToString("N2");
                }
                if (Compras1_ckxPisCofinsCsll.Checked == true)
                {
                    Compras1_ckxPisCofinsCsll.Text = "Sim Pis/Cofins/Csll";
                    if (clsParser.DecimalParse(Compras1_tbxPisCofinsCsllPorc.Text) > 0)
                    {
                        Compras1_tbxPisCofinsCsll.Text = (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) * (clsParser.DecimalParse(Compras1_tbxPisCofinsCsllPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    Compras1_ckxPisCofinsCsll.Text = "Não Pis/Cofins/Csll";
                    Compras1_tbxPisCofinsCsll.Text = 0.ToString("N2");
                }
                if (Compras1_ckxIss.Checked == true)
                {
                    Compras1_ckxIss.Text = "Sim Tem I.S.S.";
                    if (clsParser.DecimalParse(Compras1_tbxIssPorc.Text) > 0)
                    {
                        Compras1_tbxIss.Text = (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) * (clsParser.DecimalParse(Compras1_tbxIssPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    Compras1_ckxIss.Text = "Não Tem I.S.S.";
                    Compras1_tbxIss.Text = 0.ToString("N2");
                }
                if (Compras1_ckxPis.Checked == true)
                {
                    Compras1_ckxPis.Text = "Sim Tem PIS";
                    if (clsParser.DecimalParse(Compras1_tbxPisPorc.Text) > 0)
                    {
                        Compras1_tbxPis.Text = (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) * (clsParser.DecimalParse(Compras1_tbxPisPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    Compras1_ckxPis.Text = "Não Tem PIS";
                    Compras1_tbxPis.Text = 0.ToString("N2");
                }
                if (Compras1_ckxCofins.Checked == true)
                {
                    Compras1_ckxCofins.Text = "Sim Tem Cofins";
                    if (clsParser.DecimalParse(Compras1_tbxCofinsPorc.Text) > 0)
                    {
                        Compras1_tbxCofins.Text = (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) * (clsParser.DecimalParse(Compras1_tbxCofinsPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    Compras1_ckxCofins.Text = "Não Tem Cofins";
                    Compras1_tbxCofins.Text = 0.ToString("N2");
                }
                if (Compras1_ckxCsll.Checked == true)
                {
                    Compras1_ckxCsll.Text = "Sim Tem Cofins";
                    if (clsParser.DecimalParse(Compras1_tbxCsllPorc.Text) > 0)
                    {
                        Compras1_tbxCsll.Text = (clsParser.DecimalParse(Compras1_tbxTotalnota.Text) * (clsParser.DecimalParse(Compras1_tbxCsllPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    Compras1_ckxCsll.Text = "Não Tem Cofins";
                    Compras1_tbxCsll.Text = 0.ToString("N2");
                }

                // calcular o valor unitario que vai entrar no estoque como preço unit
                if (Compras1_ckxCreditarIcm.Checked == true)
                {
                    if (clsParser.DecimalParse(Compras1_tbxTotalmercado.Text) > 0)
                    {
                        Compras1_tbxPrecoCustounit.Text = ((clsParser.DecimalParse(Compras1_tbxTotalmercado.Text) - clsParser.DecimalParse(Compras1_tbxCustoicm.Text)) / clsParser.DecimalParse(Compras1_tbxQtde.Text)).ToString("N6");
                    }
                }
                else
                {
                    if (clsParser.DecimalParse(Compras1_tbxTotalmercado.Text) > 0)
                    {
                        Compras1_tbxPrecoCustounit.Text = ((clsParser.DecimalParse(Compras1_tbxTotalmercado.Text)) / clsParser.DecimalParse(Compras1_tbxQtde.Text)).ToString("N6");
                    }
                }

            }
        }
        private void Compras1SomaICM()
        {
            Compras1_tbxBaseicm.Text = (clsParser.DecimalParse(Compras1_tbxTotalmercado.Text)).ToString("N2");
            // adicionando valores na base do icms
            if (Compras1_ckxValorOutrasIcms.Checked == true)
            {
                Compras1_tbxBaseicm.Text = (clsParser.DecimalParse(Compras1_tbxBaseicm.Text) + 
                                           clsParser.DecimalParse(Compras1_tbxValoroutras.Text)).ToString("N2");
            }
            if (Compras1_ckxValorFreteIcms.Checked == true)
            {
                Compras1_tbxBaseicm.Text = (clsParser.DecimalParse(Compras1_tbxBaseicm.Text) + 
                                           clsParser.DecimalParse(Compras1_tbxValorfrete.Text)).ToString("N2");
            }
            if (Compras1_ckxValorSeguroIcms.Checked == true)
            {
                Compras1_tbxBaseicm.Text = (clsParser.DecimalParse(Compras1_tbxBaseicm.Text) + 
                                           clsParser.DecimalParse(Compras1_tbxValorseguro.Text)).ToString("N2");
            }
            if (clsParser.DecimalParse(Compras1_tbxBasemp.Text) == 100)
            {
                Compras1_tbxBaseicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                Compras1_tbxIcm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                Compras1_tbxCustoicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            }
            else if (clsParser.DecimalParse(Compras1_tbxBasemp.Text) == 0)
            {
                if (clsParser.DecimalParse(Compras1_tbxIcm.Text) > 0)
                {
                    Compras1_tbxCustoicm.Text = (clsParser.DecimalParse(Compras1_tbxBaseicm.Text) * (clsParser.DecimalParse(Compras1_tbxIcm.Text) / 100)).ToString("N2");
                }
                else
                {
                    Compras1_tbxCustoicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                }
            }
            else
            {
                Compras1_tbxBaseicm.Text = (clsParser.DecimalParse(Compras1_tbxBaseicm.Text) * (clsParser.DecimalParse(Compras1_tbxBasemp.Text) / 100)).ToString("N2");
                if (clsParser.DecimalParse(Compras1_tbxIcm.Text) > 0)
                {
                    Compras1_tbxCustoicm.Text = (clsParser.DecimalParse(Compras1_tbxBaseicm.Text) * (clsParser.DecimalParse(Compras1_tbxIcm.Text) / 100)).ToString("N2");
                }
                else
                {
                    Compras1_tbxCustoicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                }
            }
        }


        Int32 SelecionarIndex(String valor, Int32 nLetras, ComboBox c)
        {
            String s = "";
            Int32 index = 0;
            Int32 resultado = 0;

            foreach (Object obj in c.Items)
            {
                s = obj.ToString();

                if (s.Substring(0, nLetras) == valor)
                {
                    resultado = index;
                    break;
                }

                index++;
            }

            return resultado;
        }
        private Boolean Ipi_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ipi where codigo='" + Compras1_tbxIpi_codigo.Text + "'"));

            if (idtmp == compras1_idipi)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zipi;
            }

            compras1_idipi = idtmp;

            return true;
        }
        private Boolean Sittriba_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariaa where codigo='" + Compras1_cbxSittriba.Text + "'"));

            if (idtmp == compras1_idsittriba)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsituacaotriba;
            }

            compras1_idsittriba = idtmp;

            return true;
        }
        private Boolean Sittribb_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariab where codigo='" + Compras1_cbxSittribb.Text + "'"));

            if (idtmp == compras1_idsittribb)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsituacaotribb;
            }

            compras1_idsittribb = idtmp;

            return true;
        }
        private Boolean Sittribcofins_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribcofins where codigo='" + Compras1_cbxSittribcofins1.Text + "'"));

            if (idtmp ==  compras1_idsittribcofins1)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribcofins;
            }

            compras1_idsittribcofins1 = idtmp;

            return true;
        }

        private Boolean Sittribipi_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribipi where codigo='" + Compras1_cbxSittribipi.Text + "'"));

            if (idtmp == compras1_idsittribipi)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribipi;
            }

            compras1_idsittribipi = idtmp;

            return true;
        }
        private Boolean Sittribpis_Check()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribpis where codigo='" + Compras1_cbxSittribpis.Text + "'"));

            if (idtmp == compras1_idsittribpis)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribpis;
            }

            compras1_idsittribpis = idtmp;

            return true;
        }


        private void ComprasEntregaSomar()
        {
            //
            Compras1_tbxQtdeProgramada.Text = "0";
            Compras1_tbxQtdeentregue.Text = "0";
            Compras1_tbxQtdedefeito.Text = "0";
            Compras1_tbxQtdesucata.Text = "0";
            Compras1_tbxQtdebaixada.Text = "0";
            Compras1_tbxQtdeosaux.Text = "0";
            Compras1_tbxQtdesaldo.Text = "0";

            foreach (DataRow row in dtComprasEntrega.Rows)
            {
                if (row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Detached)
                {
                    continue;   // não somar se apagou
                }
                else
                {
                    if (row["compras1_posicao"].ToString() == compras1_posicao.ToString())
                    {
                        // Somar os campos do cabeçalho
                        Compras1_tbxQtdeentregue.Text = (clsParser.DecimalParse(Compras1_tbxQtdeentregue.Text) + clsParser.DecimalParse(row["qtdeentregue"].ToString())).ToString("N3");
                        Compras1_tbxQtdedefeito.Text = (clsParser.DecimalParse(Compras1_tbxQtdedefeito.Text) + clsParser.DecimalParse(row["qtdedefeito"].ToString())).ToString("N3");
                        Compras1_tbxQtdesucata.Text = (clsParser.DecimalParse(Compras1_tbxQtdesucata.Text) + clsParser.DecimalParse(row["qtdesucata"].ToString())).ToString("N3");
                        Compras1_tbxQtdebaixada.Text = (clsParser.DecimalParse(Compras1_tbxQtdebaixada.Text) + clsParser.DecimalParse(row["qtdebaixada"].ToString())).ToString("N3");
                        Compras1_tbxQtdeosaux.Text = (clsParser.DecimalParse(Compras1_tbxQtdeosaux.Text) + clsParser.DecimalParse(row["qtdeosaux"].ToString())).ToString("N3");
                        Compras1_tbxQtdesaldo.Text = (clsParser.DecimalParse(Compras1_tbxQtdesaldo.Text) + clsParser.DecimalParse(row["qtdesaldo"].ToString())).ToString("N3");
                        Compras1_tbxQtdeProgramada.Text = (clsParser.DecimalParse(Compras1_tbxQtdeProgramada.Text) + clsParser.DecimalParse(row["qtdeentrega"].ToString())).ToString("N3");
                    }
                }
            }

            Compras1_tbxTotalPrevisto.Text = (clsParser.DecimalParse(Compras1_tbxPreco.Text) * clsParser.DecimalParse(Compras1_tbxQtdesaldo.Text)).ToString("N2");
            if (clsParser.DecimalParse(Compras1_tbxQtdesaldo.Text) > 0)
            {
                // Termino = "N"
                tbxTermino.Text = "N = Ped. Em Aberto";
            }
        }


        private void Compras1_btnIdCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnIdCodigo.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }
        private void Compras1_btnIdcfopFiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnIdcfopFiscal.Name;
            frmCfopPes frmCfopPes = new frmCfopPes();
            frmCfopPes.Init(compras1_idcfopfis);

            clsFormHelper.AbrirForm(this.MdiParent, frmCfopPes, clsInfo.conexaosqldados);
        }

        private void Compras1_btnIdcfop_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnIdcfop.Name;
            frmCfopPes frmCfopPes = new frmCfopPes();
            frmCfopPes.Init(compras1_idcfop);

            clsFormHelper.AbrirForm(this.MdiParent, frmCfopPes, clsInfo.conexaosqldados);
        }

        private void Compras1_btnCentrocusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnCentrocusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", compras1_idcentrocusto, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void Compras1_btnHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnHistorico.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", compras1_idhistorico, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void Compras1_btnIdCodigo2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnIdCodigo2.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void Compras1_btnUnidadeFis_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnUnidadeFis_codigo.Name;
            frmUnidadePes frmUnidadePes = new frmUnidadePes();
            frmUnidadePes.Init(compras1_idunidfiscal);

            clsFormHelper.AbrirForm(this.MdiParent, frmUnidadePes, clsInfo.conexaosqldados);
        }

        private void Compras1_btnUnidade_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnUnidade_codigo.Name;
            frmUnidadePes frmUnidadePes = new frmUnidadePes();
            frmUnidadePes.Init(compras1_idunid);

            clsFormHelper.AbrirForm(this.MdiParent, frmUnidadePes, clsInfo.conexaosqldados);
        }

        private void Compras1_btnSittribipi_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnSittribipi.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBIPI", compras1_idsittribipi, "Situacao Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void Compras1_btnClassfiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnClassfiscal.Name;
            frmIpiPes frmIpiPes = new frmIpiPes();
            frmIpiPes.Init(compras1_idipi);

            clsFormHelper.AbrirForm(this.MdiParent, frmIpiPes, clsInfo.conexaosqldados);

        }

        private void Compras1_btnSitTriba_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnSitTriba.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAA", compras1_idsittriba, "Situacao Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void Compras1_btnSitTribb_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnSitTribb.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAB", compras1_idsittribb, "Situacao Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void Compras1_btnSittribpis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnSittribpis.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBPIS", compras1_idsittribpis, "Situacao Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void Compras1_btnSittribcofins1_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Compras1_btnSittribcofins1.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBCOFINS", compras1_idsittribcofins1, "Situacao Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void tspCompras1Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se a qtde total das entregas está correto
                // Se não estiver gera erro
                Decimal totalprogramado = 0;
                foreach (DataRow row in dtComprasEntrega.Select("compras1_posicao=" + compras1_posicao))
                {
                    if (row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Deleted)
                    {
                        totalprogramado += clsParser.DecimalParse(row["qtdeentrega"].ToString());
                    }
                }

                if (totalprogramado != clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text))
                {
                    throw new Exception("A soma do total programado não é válido. A soma deve ser igual a Qtde Fiscal.");
                }

                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar este item ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsCompras1Info = new clsCompras1Info();
                    Compras1FillInfo(clsCompras1Info);
                    Compras1FillInfoToGrid(clsCompras1Info);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                ComprasSomar();

                //gbxCompras1.Visible = false;
                tclCompras.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private Boolean HouveModificacoes1()
        {
            clsCompras1Info = new clsCompras1Info();
            Compras1FillInfo(clsCompras1Info);
            if (clsCompras1BLL.Equals(clsCompras1Info, clsCompras1InfoOld) == false)
            {
                return true;
            }
            return false;
        }
        private DialogResult Salvar1()
        {
            DialogResult drt;
            drt = DialogResult.Yes;
            
            drt = MessageBox.Show("Deseja Salvar este Item do Pedido e Continuar ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                Compras1Salvar();
            }
            return drt;
        }
        private void Compras1Salvar()
        {
            try
            {
                // Verificar se a qtde total das entregas está correto
                // Se não estiver gera erro
                Decimal totalprogramado = 0;
                foreach (DataRow row in dtComprasEntrega.Select("compras1_posicao=" + compras1_posicao))
                {
                    if (row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Deleted)
                    {
                        totalprogramado += clsParser.DecimalParse(row["qtdeentrega"].ToString());
                    }
                }

                if (totalprogramado != clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text))
                {
                    throw new Exception("A soma do total programado não é válido. A soma deve ser igual a Qtde Fiscal.");
                }

//                DialogResult drt;
//                drt = MessageBox.Show("Deseja salvar / alterar este item ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                //if (drt == DialogResult.Yes)
               // {
                    clsCompras1Info = new clsCompras1Info();
                    Compras1FillInfo(clsCompras1Info);
                    Compras1FillInfoToGrid(clsCompras1Info);
               // }
               // else if (drt == DialogResult.Cancel)
               // {
               //     return;
               // }

                ComprasSomar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tspCompras1Primeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras1.Rows != null && dgvCompras1.Rows.Count > 0)
                {
                    Salvar1();

                    compras1_posicao = Int32.Parse(dgvCompras1.Rows[0].Cells["compras1_posicao"].Value.ToString());
                    DataRow rowNfvenda1 = dtCompras1.Select("compras1_posicao=" + compras1_posicao)[0];
                    Compras1Carregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void tspCompras1Anterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras1.Rows != null && dgvCompras1.Rows.Count > 0)
                {
                    Salvar1();

                    //if (ModificouRegistroNfvenda1() == true)
                    //{

                    //}


                    foreach (DataGridViewRow row in dgvCompras1.Rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == compras1_id)
                        {
                            if (row.Index < rowsitens.Count - 1)
                            {
                                compras1_posicao = Int32.Parse(dgvCompras1.Rows[dgvCompras1.Rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells["compras1_posicao"].Value.ToString());

                                DataRow rowNfvenda1 = dtCompras1.Select("compras1_posicao=" + compras1_posicao)[0];

                                //DataRow rowNfvenda1 = dtNfvenda1.Select("posicao" + item_posicao)[0];

                                Compras1Carregar();

                                if (dtCompras1.Rows.Count > 0)
                                {
                                    foreach (DataRow rowNfvenda in dtCompras1.Rows)
                                    {
                                        if (rowNfvenda.RowState == DataRowState.Added &&
                                            rowNfvenda["compras_posicao"].ToString().Trim() == "" &&
                                            rowNfvenda["compras1_posicao"].ToString() != compras1_posicao.ToString())
                                        {
                                            rowNfvenda.Delete();
                                        }
                                    }
                                }

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
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspCompras1Proximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras1.Rows != null && dgvCompras1.Rows.Count > 0)
                {
                    Salvar1();
                    /*
                    if (HouveModificacoes1() == true)
                    {
                        if (Salvar1() == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    */


                    foreach (DataGridViewRow row in dgvCompras1.Rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == compras1_id)
                        {
                            if (row.Index < rowsitens.Count - 1)
                            {
                                compras1_posicao = Int32.Parse(dgvCompras1.Rows[dgvCompras1.Rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells["compras1_posicao"].Value.ToString());
                                DataRow rowNfvenda1 = dtCompras1.Select("compras1_posicao=" + compras1_posicao)[0];

                                //DataRow rowNfvenda1 = dtNfvenda1.Select("posicao" + item_posicao)[0];

                                Compras1Carregar();

                                if (dtCompras1.Rows.Count > 0)
                                {
                                    foreach (DataRow rowNfvenda in dtCompras1.Rows)
                                    {
                                        if (rowNfvenda.RowState == DataRowState.Added &&
                                            rowNfvenda["compras_posicao"].ToString().Trim() == "" &&
                                            rowNfvenda["compras1_posicao"].ToString() != compras1_posicao.ToString())
                                        {
                                            rowNfvenda.Delete();
                                        }
                                    }
                                }

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
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspCompras1Ultimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras1.Rows != null && dgvCompras1.Rows.Count > 0)
                {
                    Salvar1();

                    //if (ModificouRegistroNfvenda1() == true)
                    //{

                    //}


                    foreach (DataGridViewRow row in dgvCompras1.Rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == compras1_id)
                        {
                            if (row.Index < rowsitens.Count - 1)
                            {
                                compras1_posicao = Int32.Parse(dgvCompras1.Rows[dgvCompras1.Rows.Count - 1].Cells["compras1_posicao"].Value.ToString());

                                DataRow rowNfvenda1 = dtCompras1.Select("compras1_posicao=" + compras1_posicao)[0];

                                //DataRow rowNfvenda1 = dtNfvenda1.Select("posicao" + item_posicao)[0];

                                Compras1Carregar();

                                if (dtCompras1.Rows.Count > 0)
                                {
                                    foreach (DataRow rowNfvenda in dtCompras1.Rows)
                                    {
                                        if (rowNfvenda.RowState == DataRowState.Added &&
                                            rowNfvenda["compras_posicao"].ToString().Trim() == "" &&
                                            rowNfvenda["compras1_posicao"].ToString() != compras1_posicao.ToString())
                                        {
                                            rowNfvenda.Delete();
                                        }
                                    }
                                }

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
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspCompras1Retornar_Click(object sender, EventArgs e)
        {
            tclCompras.SelectedIndex = 0;
            //gbxCompras1.Visible = false;
        }

        private void tspComprasEntregaIncluir_Click(object sender, EventArgs e)
        {
            comprasentrega_posicao = 0;
            ComprasEntregaCarregar();
        }

        private void tspComprasEntregaAlterar_Click(object sender, EventArgs e)
        {
            if (dgvComprasEntrega.CurrentRow != null)
            {
                comprasentrega_posicao = Int32.Parse(dgvComprasEntrega.CurrentRow.Cells["comprasentrega_posicao"].Value.ToString());
                ComprasEntregaCarregar();
            }

        }

        private void tspComprasEntregaExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvComprasEntrega.CurrentRow != null)
                {
                    DialogResult drt;
                    drt = MessageBox.Show("Deseja Excluir esta Programação de Entrega?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (drt == DialogResult.Yes)
                    {
                        dtComprasEntrega.Select("comprasentrega_posicao=" + dgvComprasEntrega.CurrentRow.Cells["comprasentrega_posicao"].Value.ToString())[0].Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspComprasEntregaSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar esta programação ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsComprasEntregaInfo = new clsComprasEntregaInfo();
                    ComprasEntregaFillInfo(clsComprasEntregaInfo);
                    ComprasEntregaFillInfoToGrid(clsComprasEntregaInfo);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                ComprasEntregaSomar();

                gbxCotacaoEntregaRegistro.Visible = false;
                tclComprasEntrega.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             

        }

        private void tspComprasEntregaRetornar_Click(object sender, EventArgs e)
        {
            gbxCotacaoEntregaRegistro.Visible = false;
            tclComprasEntrega.SelectedIndex = 0;

        }


        private void dgvCompras1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspCompras1Alterar.PerformClick();
        }
        private void Compras1_ckxValorOutrasIcms_Click(object sender, EventArgs e)
        {
            if (Compras1_ckxValorOutrasIcms.Checked == true)
            {
                Compras1_ckxValorOutrasIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                Compras1_ckxValorOutrasIcms.Text = "Não Incide ICMS";
            }
        }
        private void Compras1_ckxValorFreteIcms_Click(object sender, EventArgs e)
        {
            if (Compras1_ckxValorFreteIcms.Checked == true)
            {
                Compras1_ckxValorFreteIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                Compras1_ckxValorFreteIcms.Text = "Não Incide ICMS";
            }
        }
        private void Compras1_ckxValorSeguroIcms_Click(object sender, EventArgs e)
        {
            if (Compras1_ckxValorSeguroIcms.Checked == true)
            {
                Compras1_ckxValorSeguroIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                Compras1_ckxValorSeguroIcms.Text = "Não Incide ICMS";
            }
        }

        private void Compras1_ckxCreditarIcm_Click(object sender, EventArgs e)
        {
            if (Compras1_ckxCreditarIcm.Checked == true)
            {
                Compras1_ckxCreditarIcm.Text = "Sim Creditar ICMs";
            }
            else
            {
                Compras1_ckxCreditarIcm.Text = "Não Creditar ICMs";
            }
            Calcular();
        }

        private void Compras1_ckxCalculoautomatico_Click(object sender, EventArgs e)
        {
            if (Compras1_ckxCalculoautomatico.Checked == true)
            {
                Compras1_ckxCalculoautomatico.Text = "Cálculo Automáticos";
            }
            else
            {
                Compras1_ckxCalculoautomatico.Text = "Cálculo Manual";
            }
        }

        private void Compras1_ckxIrrf_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Compras1_ckxInss_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Compras1_ckxPisCofinsCsll_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Compras1_ckxIss_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Compras1_ckxPis_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Compras1_ckxCofins_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Compras1_ckxCsll_Click(object sender, EventArgs e)
        {
            Calcular();
        }
        private void ComprasEntregaCarregar()
        {
            clsComprasEntregaInfo = new clsComprasEntregaInfo();
            clsComprasEntregaInfoOld = new clsComprasEntregaInfo();

            if (comprasentrega_posicao == 0)
            {
                comprasentrega_posicao = dtComprasEntrega.Rows.Count + 1;

                clsComprasEntregaInfo.id = 0;
                clsComprasEntregaInfo.idcompras = id;
                clsComprasEntregaInfo.idcompras1 = clsCompras1Info.id;
                clsComprasEntregaInfo.idos = clsInfo.zordemservico;

                clsComprasEntregaInfo.dataentrega = DateTime.Now.AddDays(1);
                clsComprasEntregaInfo.qtdebaixada = 0;
                clsComprasEntregaInfo.qtdedefeito = 0;
                clsComprasEntregaInfo.qtdeentrega = clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text);
                clsComprasEntregaInfo.qtdeentregue = 0;
                clsComprasEntregaInfo.qtdeosaux = 0;
                clsComprasEntregaInfo.qtdesaldo = clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text);
                clsComprasEntregaInfo.qtdesucata = 0;
            }
            else
            {
                ComprasEntregaGridToInfo(clsComprasEntregaInfo, comprasentrega_posicao);
            }

            ComprasEntregaCampos(clsComprasEntregaInfo);
            ComprasEntregaFillInfo(clsComprasEntregaInfoOld);

            tclComprasEntrega.SelectedIndex = 1;
            gbxCotacaoEntregaRegistro.Visible = true;

            if (gbxProgramacao.Enabled == true)
            {
                ComprasEntrega_tbxDataEntrega.Select();
            }

        }
        private void ComprasEntregaFillInfo(clsComprasEntregaInfo info)
        {
            info.id = comprasentrega_id;
            info.idcompras = comprasentrega_idcompras; // id
            info.idcompras1 = comprasentrega_idcompras1; // id
            info.idos = comprasentrega_idos;

            info.dataentrega = DateTime.Parse(ComprasEntrega_tbxDataEntrega.Text);
            
            info.qtdebaixada = clsParser.DecimalParse(ComprasEntrega_tbxQtdebaixada.Text);
            info.qtdedefeito = clsParser.DecimalParse(ComprasEntrega_tbxQtdedefeito.Text);
            info.qtdeentrega = clsParser.DecimalParse(ComprasEntrega_tbxQtdeentrega.Text); 
            info.qtdeentregue = clsParser.DecimalParse(ComprasEntrega_tbxQtdeentregue.Text);
            info.qtdeosaux = clsParser.DecimalParse(ComprasEntrega_tbxQtdeosaux.Text);
            info.qtdesucata = clsParser.DecimalParse(ComprasEntrega_tbxQtdesucata.Text);
            info.qtdesaldo = info.qtdeentrega - (info.qtdebaixada + info.qtdedefeito + info.qtdeentregue + info.qtdeosaux + info.qtdesucata);
            gbxProgramacao.Enabled = true;
            tspComprasEntregaSalvar.Enabled = true;
            tspComprasEntregaBaixar.Enabled = true;

            if (info.qtdesaldo == 0)
            {
                if ((info.qtdebaixada + info.qtdedefeito + info.qtdeentregue + info.qtdeosaux + info.qtdesucata) > 0)
                {
                    gbxProgramacao.Enabled = false;
                    tspComprasEntregaSalvar.Enabled = false;
                    tspComprasEntregaBaixar.Enabled = false;
                }
            }
            
        }

        private void ComprasEntregaFillInfoToGrid(clsComprasEntregaInfo info)
        {
            DataRow row;
            DataRow[] rows = dtComprasEntrega.Select("comprasentrega_posicao = " + comprasentrega_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtComprasEntrega.NewRow();
            }
            row["id"] = info.id;
            row["idcompras"] = info.idcompras;
            row["idcompras1"] = info.idcompras1;
            row["idos"] = info.idos;

            row["dataentrega"] = info.dataentrega;
            row["qtdebaixada"] = info.qtdebaixada;
            row["qtdedefeito"] = info.qtdedefeito;
            row["qtdeentrega"] = info.qtdeentrega;
            row["qtdeentregue"] = info.qtdeentregue;
            row["qtdeosaux"] = info.qtdeosaux;
            row["qtdesaldo"] = info.qtdesaldo;
            row["qtdesucata"] = info.qtdesucata;
            // Colunas que petencem a outras tabelas
            /*            row["codigo"] = Orcamento1_tbxCodigo.Text;
                        row["nome"] = Orcamento1_tbxCodigoNome.Text;
                        row["unid"] = Orcamento1_Pecas_tbxUnidade.Text;
            */
            if (rows.Length == 0)
            {
                row["compras1_posicao"] = compras1_posicao;
                row["comprasentrega_posicao"] = comprasentrega_posicao;
                
                dtComprasEntrega.Rows.Add(row);
            }
        }
        private void ComprasEntregaGridToInfo(clsComprasEntregaInfo info, Int32 posicao)
        {
            DataRow row = dtComprasEntrega.Select("comprasentrega_posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());
            info.idcompras = Int32.Parse(row["idcompras"].ToString());
            info.idcompras1 = Int32.Parse(row["idcompras1"].ToString()); 
            info.idos = Int32.Parse(row["idos"].ToString());
            info.dataentrega = DateTime.Parse(row["dataentrega"].ToString());
            info.qtdebaixada = clsParser.DecimalParse(row["qtdebaixada"].ToString());
            info.qtdedefeito = clsParser.DecimalParse(row["qtdedefeito"].ToString());
            info.qtdeentrega = clsParser.DecimalParse(row["qtdeentrega"].ToString());
            info.qtdeentregue = clsParser.DecimalParse(row["qtdeentregue"].ToString());
            info.qtdeosaux = clsParser.DecimalParse(row["qtdeosaux"].ToString());
            info.qtdesaldo = clsParser.DecimalParse(row["qtdesaldo"].ToString());
            info.qtdesucata = clsParser.DecimalParse(row["qtdesucata"].ToString());
        }
        private void ComprasEntregaCampos(clsComprasEntregaInfo info)
        {

            comprasentrega_id = info.id;
            comprasentrega_idcompras = info.idcompras;
            comprasentrega_idcompras1 = info.idcompras1;
            comprasentrega_idos = info.idos;
            ComprasEntrega_tbxDataEntrega.Text = info.dataentrega.ToString("dd/MM/yyyy");
            ComprasEntrega_tbxQtdebaixada.Text = info.qtdebaixada.ToString("N3");
            ComprasEntrega_tbxQtdedefeito.Text = info.qtdedefeito.ToString("N3");
            ComprasEntrega_tbxQtdeentrega.Text = info.qtdeentrega.ToString("N3");
            ComprasEntrega_tbxQtdeentregue.Text = info.qtdeentregue.ToString("N3");
            ComprasEntrega_tbxQtdeosaux.Text = info.qtdeosaux.ToString("N3");
            ComprasEntrega_tbxQtdesaldo.Text = info.qtdesaldo.ToString("N3");
            ComprasEntrega_tbxQtdesucata.Text = info.qtdesucata.ToString("N3");
        }
        private void dgvComprasEntrega_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspComprasEntregaAlterar.PerformClick();
        }

        private void btnCalculaProgramacao_Click(object sender, EventArgs e)
        {
            try
            {
                clsProgramacaoEntrega clsProgramacaoEntrega = new clsProgramacaoEntrega();

                clsProgramacaoEntrega.Periodo periodo;

                if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "A")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Anual;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString() == "Semestral")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semestre;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "M")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Mes;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString().Substring(0, 1) == "Q")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Quinzena;
                }
                else if (lbxTipoCalculo.SelectedItem.ToString() == "Semanal")
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semana;
                }
                else
                {
                    periodo = clsProgramacaoEntrega.Periodo.Semana;
                }

                DataTable dtTemp = clsProgramacaoEntrega.GerarProgramacao(clsParser.DecimalParse(Compras1_tbxQtdeFiscal.Text),
                                                        0,
                                                        clsParser.Int32Parse(tbxQtdeEntregas.Text),
                                                        periodo,
                                                        clsParser.DateTimeParse(tbxDiaFixo.Text),
                                                        compras1_idunidfiscal,
                                                        ckxPulaSabDom.Checked);

                foreach (DataRow row in dtComprasEntrega.Rows)
                {
                    if (row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Deleted)
                    {
                        if (row["compras1_posicao"].ToString() == compras1_posicao.ToString())
                        {
                            row.Delete();
                        }
                    }
                }

                foreach (DataRow row in dtTemp.Rows)
                {
                    DataRow rowEntrega = dtComprasEntrega.NewRow();

                    rowEntrega["comprasentrega_posicao"] = dtComprasEntrega.Rows.Count + 1;
                    rowEntrega["compras1_posicao"] = compras1_posicao;

                    rowEntrega["id"] = 0;
                    rowEntrega["idcompras"] = 0;
                    rowEntrega["idcompras1"] = 0;
                    rowEntrega["idos"] = 0; ;

                    rowEntrega["qtdeentrega"] = row["qtde"];
                    rowEntrega["qtdesaldo"] = row["qtde"];
                    rowEntrega["dataentrega"] = row["entrega"];
                    rowEntrega["qtdebaixada"] = 0;
                    rowEntrega["qtdedefeito"] = 0;
                    rowEntrega["qtdeentregue"] = 0;
                    rowEntrega["qtdeosaux"] = 0;
                    rowEntrega["qtdesucata"] = 0;

                    dtComprasEntrega.Rows.Add(rowEntrega);
                }
                ComprasEntregaSomar();
                if (clsParser.DecimalParse(Compras1_tbxQtdeProgramada.Text) > 0)
                {
                    gbxPrePlanejamento.Visible = false;
                    gbxQtdes.Visible = true;
                    gbxQtdes.Width = 203;
                    gbxQtdes.Height = 135;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName);
            }
        }

        private void ckxPulaSabDom_Click(object sender, EventArgs e)
        {
            if (ckxPulaSabDom.Checked == true)
            {
                ckxPulaSabDom.Text = "Sim apenas dias uteis";
            }
            else
            {
                ckxPulaSabDom.Text = "Não contar dias uteis";
            }
        }

        private void tspComprasEntregaBaixar_Click(object sender, EventArgs e)
        {
            try
            {
                ComprasEntrega_tbxQtdebaixada.Text = (clsParser.DecimalParse(ComprasEntrega_tbxQtdebaixada.Text) + clsParser.DecimalParse(ComprasEntrega_tbxQtdesaldo.Text)).ToString("N3");
                ComprasEntrega_tbxQtdesaldo.Text = 0.ToString("N3");

                DialogResult drt;
                drt = MessageBox.Show("Deseja mesmo Baixar/Cancelar esta Entrega da programação ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsComprasEntregaInfo = new clsComprasEntregaInfo();
                    ComprasEntregaFillInfo(clsComprasEntregaInfo);

                    ComprasEntregaFillInfoToGrid(clsComprasEntregaInfo);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                ComprasEntregaSomar();

                gbxCotacaoEntregaRegistro.Visible = false;
                tclComprasEntrega.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspCompras1Cancelar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Estou em Desenvolvimento - Por Enquanto entre na data da Entrega e Cancele !!!");
            /*
            if (clsCompras1Info.id > 0)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja mesmo Baixar/Cancelar esta Entrega da programação ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    dtComprasEntrega = clsComprasEntregaBLL.GridDados(clsCompras1Info.id);
                    foreach (DataRow row in dtComprasEntrega.Rows)
                    {
                        if (row.RowState == DataRowState.Deleted ||
                            row.RowState == DataRowState.Detached)
                        {
                            continue;   // não somar se apagou
                        }
                        else
                        {
                                // Somar os campos do cabeçalho

                                Compras1_tbxQtdeentregue.Text = (Decimal.Parse(Compras1_tbxQtdeentregue.Text) + Decimal.Parse(row["qtdeentregue"].ToString())).ToString("N3");
                                Compras1_tbxQtdedefeito.Text = (Decimal.Parse(Compras1_tbxQtdedefeito.Text) + Decimal.Parse(row["qtdedefeito"].ToString())).ToString("N3");
                                Compras1_tbxQtdesucata.Text = (Decimal.Parse(Compras1_tbxQtdesucata.Text) + Decimal.Parse(row["qtdesucata"].ToString())).ToString("N3");
                                Compras1_tbxQtdebaixada.Text = (Decimal.Parse(Compras1_tbxQtdebaixada.Text) + Decimal.Parse(row["qtdebaixada"].ToString())).ToString("N3");
                                Compras1_tbxQtdeosaux.Text = (Decimal.Parse(Compras1_tbxQtdeosaux.Text) + Decimal.Parse(row["qtdeosaux"].ToString())).ToString("N3");
                                Compras1_tbxQtdesaldo.Text = (Decimal.Parse(Compras1_tbxQtdesaldo.Text) + Decimal.Parse(row["qtdesaldo"].ToString())).ToString("N3");
                                Compras1_tbxQtdeProgramada.Text = (Decimal.Parse(Compras1_tbxQtdeProgramada.Text) + Decimal.Parse(row["qtdeentrega"].ToString())).ToString("N3");
                        }
                    }

                }
            }
             */

        }

        private void btnLiberado_Click(object sender, EventArgs e)
        {

        }

     


    }
}
