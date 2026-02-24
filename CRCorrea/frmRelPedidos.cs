using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelPedidos : Form
    {
        Int32 idfornecedor;
        Int32 idfornecedor1;


        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        ParameterFields pfields = new ParameterFields();

        String sql;
        String query;
        String ordem;
        String cabecalho;
        String cabecalho1;

        Int32 idcodigo;
        DateTime dataaux;

        String Mes01Nom;
        String Mes02Nom;
        String Mes03Nom;
        String Mes04Nom;
        String Mes05Nom;
        String Mes06Nom;
        String Mes07Nom;
        String Mes08Nom;
        String Mes09Nom;
        String Mes10Nom;
        String Mes11Nom;
        String Mes12Nom;

        DateTime Mes01Data;
        DateTime Mes02Data;
        DateTime Mes03Data;
        DateTime Mes04Data;
        DateTime Mes05Data;
        DateTime Mes06Data;
        DateTime Mes07Data;
        DateTime Mes08Data;
        DateTime Mes09Data;
        DateTime Mes10Data;
        DateTime Mes11Data;
        DateTime Mes12Data;

        Decimal ValorMes01;
        Decimal ValorMes02;
        Decimal ValorMes03;
        Decimal ValorMes04;
        Decimal ValorMes05;
        Decimal ValorMes06;
        Decimal ValorMes07;
        Decimal ValorMes08;
        Decimal ValorMes09;
        Decimal ValorMes10;
        Decimal ValorMes11;
        Decimal ValorMes12;
        Decimal TotalAno;
        Decimal MediaAno;
        String Grupo;

        private bool disposing = false;

        clsRptAcumulaAnoBLL clsRptAcumulaAnoBLL = new clsRptAcumulaAnoBLL();
        clsRptAcumulaAnoInfo clsRptAcumulaAnoInfo = new clsRptAcumulaAnoInfo();

        public frmRelPedidos()
        {
            InitializeComponent();
        }

        public void Init()
        {

            // Resumo
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResFornecedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResFornecedorAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxResVendedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxResVendedorAte);
            // Item
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxItemClienteDe);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxItemClienteAte);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxItemUfDe);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxItemUfAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxItemVendedorDe);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxItemVendedorAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select nome from pecas order by nome", tbxItemCodDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select nome from pecas order by nome", tbxItemCodAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from zonas order by codigo", tbxItemGrupo);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from zonas order by codigo", tbxItemZonaAte);
        }

        private void tspResRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmRelPedidos_Activated(object sender, EventArgs e)
        {
            Lupa();
        }

        private void frmRelPedidos_Load(object sender, EventArgs e)
        {
            clsRptAcumulaAnoBLL = new clsRptAcumulaAnoBLL();

            tabFiltros.SelectedIndex = 1;

            lbxResOrdem.SelectedIndex = 0;

            lbxItemOrdem.SelectedIndex = 0;

            tbxItemDtEmissaoDe.Text = "01/" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Today.Year.ToString();
            tbxItemDtEmissaoAte.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");

            dataaux = DateTime.Now.AddMonths(1);
            tbxDataAte.Text = "01" + "/" + dataaux.Month.ToString().PadLeft(2, '0') + "/" + dataaux.Year.ToString();
            tbxDataAte.Text = clsParser.DateTimeParse(tbxDataAte.Text).AddDays(-1).ToString("dd/MM/yyyy");
            dataaux = clsParser.DateTimeParse(tbxDataAte.Text).AddMonths(-11);
            tbxDataDe.Text = "01" + "/" + dataaux.Month.ToString().PadLeft(2, '0') + "/" + dataaux.Year.ToString();

            GrupoProduto_Carrega();  // CARREGAR os grupos
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

        private void ControlKeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisual.ControlKeyPressNumero((TextBox)sender, e, true);
        }

        private void btnItemFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteItem";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }

        private void btnItemFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteItem1";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }
        private void btnItemCodDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCodigoItem";
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnItemCodAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCodigoItem1";
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnItemVendedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvVendedorItem";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("V", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnItemVendedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvVendedorItem1";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("V", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }


        // Resumo
        private void bntResFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteRes";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

        private void btnResFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteRes1";
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }



        public void Lupa()
        {
            if (clsInfo.znomegrid == "dgvClienteItem")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idfornecedor > 0)
                    {
                        tbxItemClienteDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor + " ");
                    }
                }
                tbxItemClienteDe.Select();
            }

            if (clsInfo.znomegrid == "dgvClienteItem1")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idfornecedor1 > 0)
                    {
                        //tbxItemClienteAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor1 + " ");
                    }
                }
                //bxItemClienteAte.Select();
            }

            if (clsInfo.znomegrid == "dgvCodigoItem")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxItemCodDe.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvCodigoItem1")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxItemCodAte.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvGrupo")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxItemGrupo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvVendedorItem")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxItemVendedorDe.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    }
                }
            }



            ////


            ////
            if (clsInfo.znomegrid == "dgvClienteRes")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idfornecedor > 0)
                    {
                        tbxResFornecedorDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor + " ");
                    }
                }
                tbxResFornecedorDe.Select();
            }

            if (clsInfo.znomegrid == "dgvClienteRes1")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idfornecedor1 > 0)
                    {
                        tbxResFornecedorAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor1 + " ");
                    }
                }
                tbxResFornecedorAte.Select();
            }

            if (clsInfo.znomegrid == "dgvVendedorRes")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idfornecedor > 0)
                    {
                        tbxResVendedorDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor + " ");
                    }
                }
                tbxResVendedorDe.Select();
            }

            if (clsInfo.znomegrid == "dgvVendedorRes1")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idfornecedor1 > 0)
                    {
                        tbxResVendedorAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor1 + " ");
                    }
                }
                tbxResVendedorAte.Select();
            }

            clsInfo.zrow = null;
            clsInfo.znomegrid = "";
        }


        private void tspItemImprimir_Click(object sender, EventArgs e)
        {
            //
            sql = "";
            cabecalho = "";
            cabecalho1 = "";
            query = "";
            ordem = "";

            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();

            ParameterFields parameters = new ParameterFields();
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();

            field.Name = "EMPRESA";
            valor.Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from empresa where id=" + clsInfo.zempresaid + " ");
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cabeçalho
            field = new ParameterField();
            field.Name = "CABECALHO";
            valor = new ParameterDiscreteValue();
            cabecalho = "Relatorio Simples Pedidos de Venda [Cabeçalho]";
            valor.Value = cabecalho;
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            // Parametros do Relatorio
            // Cliente de
            field = new ParameterField();
            field.Name = "ClienteDe";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxItemClienteDe.Text;
            if (tbxItemClienteDe.Text.Length > 0)
            {
                cabecalho1 += " Cliente : " + tbxItemClienteDe.Text;
            }
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            field = new ParameterField();
            field.Name = "DataEmissaoDe";
            valor = new ParameterDiscreteValue();
            if (tbxItemDtEmissaoDe.Text.Trim() == "")
            {
                tbxItemDtEmissaoDe.Text = "01/01/1900";
            }
            valor.Value = tbxItemDtEmissaoDe.Text;
            cabecalho1 += " De : " + tbxItemDtEmissaoDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Ate Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoAte";
            valor = new ParameterDiscreteValue();
            if (tbxItemDtEmissaoAte.Text.Trim() == "")
            {
                tbxItemDtEmissaoAte.Text = "01/01/2100";
            }
            valor.Value = tbxItemDtEmissaoAte.Text;
            cabecalho1 += " Ate : " + tbxItemDtEmissaoAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Do Vendedor
            field = new ParameterField();
            field.Name = "VendedorDe";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxItemVendedorDe.Text;
            if (tbxItemVendedorDe.Text.Length > 0)
            {
                cabecalho1 += " Vendedor = " + tbxItemVendedorDe.Text;
            }
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Do Nome do Material
            field = new ParameterField();
            field.Name = "MaterialDe";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxItemCodDe.Text;
            if (tbxItemCodDe.Text.Length > 0)
            {
                cabecalho1 += " Material De = " + tbxItemCodDe.Text.Substring(0, 10); 
            }
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Ate Nome do Material
            field = new ParameterField();
            field.Name = "MaterialAte";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxItemCodAte.Text;
            if (tbxItemCodAte.Text.Length > 0)
            {
                cabecalho1 += " Até = " + tbxItemCodAte.Text.Substring(0, 10); 
            }
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cabeçalho1
            field = new ParameterField();
            field.Name = "CABECALHO1";
            valor = new ParameterDiscreteValue();
            //cabecalho1 = "";"
            valor.Value = cabecalho1;
            field.CurrentValues.Add(valor);
            parameters.Add(field);



            if (rbnItemNumero.Checked == true)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "PedidoVendas_itens.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            }
            else if (rbnItemCliente.Checked == true)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "dados_pedidodevendas_itens_cliente.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            }
            else if (rbnItemCodigo.Checked == true)
            {
                if (rbnItemAnalitica.Checked == true)
                {
                    // Sem Subtotal
                    if (rbnItemSemTotal.Checked == true)
                    {
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "PedidoVendas_itens.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "PedidoVendas_itens_SubTotal.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    }
                }
                else
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "PedidoVendas_itens_Resumo.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                }
            }
            else
            {
                MessageBox.Show("Opção não fornecida");
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "dados_pedidodevendas_itens.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            }
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

        }

        private void tspItemRetornar_Click_1(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void AddAutoComplete(TextBox _textbox, TextBox _colecao)
        {
            if (_textbox.InvokeRequired)
            {
                Invoke(new AddnewDelegate(AddAutoComplete), _textbox, _colecao);
            }
            else
            {
                _textbox.AutoCompleteCustomSource = _colecao.AutoCompleteCustomSource;
            }
        }

        private void tspResImprimir_Click(object sender, EventArgs e)
        {
            //
            sql = "";
            cabecalho = "";
            query = "";
            ordem = "";

            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();

            ParameterFields parameters = new ParameterFields();
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();

            field.Name = "EMPRESA";
            valor.Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from empresas where id=" + clsInfo.zempresaid + " ");
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cabeçalho
            field = new ParameterField();
            field.Name = "CABECALHO";
            valor = new ParameterDiscreteValue();
            cabecalho = "Relatorio Simples Pedidos de Venda [Cabeçalho]";
            valor.Value = cabecalho;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Parametros do Relatorio
            // Cliente de
            field = new ParameterField();
            field.Name = "ClienteDe";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxResFornecedorDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cliente ate
            field = new ParameterField();
            field.Name = "ClienteAte";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxResFornecedorAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Da Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoDe";
            valor = new ParameterDiscreteValue();
            if (tbxResDtEmissaoDe.Text.Trim() == "")
            {
                tbxResDtEmissaoDe.Text = "01/01/1900";
            }
            valor.Value = tbxResDtEmissaoDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Ate Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoAte";
            valor = new ParameterDiscreteValue();
            if (tbxResDtEmissaoAte.Text.Trim() == "")
            {
                tbxResDtEmissaoAte.Text = "01/01/2100";
            }
            valor.Value = tbxResDtEmissaoAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cliente de
            field = new ParameterField();
            field.Name = "VendedorDe";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxResVendedorDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cliente ate
            field = new ParameterField();
            field.Name = "VendedorAte";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxResVendedorAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            frmCrystalReport.Init(clsInfo.caminhorelatorios, "dados_pedidodevendas.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
        }
            private void tspApuRetornar_Click(object sender, EventArgs e)
            {
                this.Close();
                this.Dispose();
            }

            private void bntResEstadoDe_Click(object sender, EventArgs e)
            {
                clsInfo.znomegrid = "dgvEstadoRes";
                frmEstadosPes frmEstadosPes = new frmEstadosPes();
                frmEstadosPes.Init(0);

                clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);
            }

            private void bntResEstadoAte_Click(object sender, EventArgs e)
            {
                clsInfo.znomegrid = "dgvEstadoRes1";
                frmEstadosPes frmEstadosPes = new frmEstadosPes();
                frmEstadosPes.Init(0);

                clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);
            }

            private void rbnCabecalho_Click(object sender, EventArgs e)
            {
                tabFiltros.SelectedTab = tabCabecalho;
            }

            private void rbnDetalhes_Click(object sender, EventArgs e)
            {
                tabFiltros.SelectedTab = tabItens;
            }

            private void frmRelPedidos_Activated_1(object sender, EventArgs e)
            {
                Lupa();
            }

            private void bntResVendedorDe_Click(object sender, EventArgs e)
            {
                clsInfo.znomegrid = "dgvVendedorRes";
                frmClienteVis frmClienteVis = new frmClienteVis();
                frmClienteVis.Init("Todos", 0);

                clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
            }

            private void bntResVendedorAte_Click(object sender, EventArgs e)
            {
                clsInfo.znomegrid = "dgvVendedorRes1";
                frmClienteVis frmClienteVis = new frmClienteVis();
                frmClienteVis.Init("Todos", 0);

                clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
            }

            private void VerificarOrdensItem()
            {
                if (rbnItemNumero.Checked == true)
                {
                    rbnModelo1.Text = "Resumida";
                    rbnComCusto.Text = "";
                    rbnComCusto.Visible = false;
                    lbxItemOrdem.Items.Clear();
                    lbxItemOrdem.Items.Add("Por Numero Pedido + Data Entrega + Codigo Produto ");
                    lbxItemOrdem.SelectedIndex = 0;
                }
                else if (rbnItemCliente.Checked == true)
                {
                    rbnModelo1.Text = "Resumida";
                    rbnComCusto.Text = "";
                    rbnComCusto.Visible = false;
                    lbxItemOrdem.Items.Clear();
                    lbxItemOrdem.Items.Add("Por Cliente + Data Entrega + Codigo Produto ");
                    lbxItemOrdem.Items.Add("Por Cliente + Codigo Produto + Data Entrega ");
                    lbxItemOrdem.SelectedIndex = 0;
                }
                else if (rbnItemRepresentante.Checked == true)
                {
                    rbnModelo1.Text = "Resumida";
                    rbnComCusto.Text = "";
                    rbnComCusto.Visible = false;
                    lbxItemOrdem.Items.Clear();
                    lbxItemOrdem.Items.Add("Por Vendedor + Data Entrega + Codigo Produto ");
                    lbxItemOrdem.Items.Add("Por Vendedor + Codigo Produto + Data Entrega ");
                    lbxItemOrdem.Items.Add("Por Vendedor + Cliente + Data Entrega + Codigo Produto");
                    lbxItemOrdem.SelectedIndex = 0;
                }
                else if (rbnItemDtEntrega.Checked == true)
                {
                    rbnModelo1.Text = "Resumida";
                    rbnComCusto.Text = "";
                    rbnComCusto.Visible = false;
                    lbxItemOrdem.Items.Clear();
                    lbxItemOrdem.Items.Add("Por Data Entrega + Cliente + Codigo Produto ");
                    lbxItemOrdem.Items.Add("Por Data Entrega + Rota + Cliente + Codigo Produto ");
                    lbxItemOrdem.Items.Add("Por Data Entrega + Codigo Produto + Cliente ");
                    lbxItemOrdem.SelectedIndex = 0;
                }
                else if (rbnItemCodigo.Checked == true)
                {
                    rbnModelo1.Text = "Resumida";
                    rbnComCusto.Text = "";
                    rbnComCusto.Visible = false;
                    lbxItemOrdem.Items.Clear();
                    lbxItemOrdem.Items.Add("Por Codigo Produto + Cliente + Dt Entrega ");
                    lbxItemOrdem.Items.Add("Por Codigo Produto + Dt Entrega + Cliente ");
                    lbxItemOrdem.SelectedIndex = 0;
                }
            }

            private void rbnItemNumero_Click(object sender, EventArgs e)
            {
                VerificarOrdensItem();
            }

            private void rbnItemCliente_Click(object sender, EventArgs e)
            {
                VerificarOrdensItem();
            }

            private void rbnItemRepresentante_Click(object sender, EventArgs e)
            {
                VerificarOrdensItem();
            }

            private void rbnItemDtEntrega_Click(object sender, EventArgs e)
            {
                VerificarOrdensItem();
            }

            private void rbnItemCodigo_CheckedChanged(object sender, EventArgs e)
            {
                VerificarOrdensItem();
            }

            private void rbnDetalhes_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void rbnApuracoes_Click(object sender, EventArgs e)
            {
                tabFiltros.SelectedTab = tabApuracao;
            }

            private void GrupoProduto_Carrega()
            {
                lbxGrupoProduto.Items.Clear();
                String query;
                SqlDataAdapter sda;
                query = "select ID, CODIGO + ' - ' + NOME AS [CODIGONOME] FROM PECASCLASSIFICA ORDER BY CODIGO";

                DataTable dtTemp = new DataTable();
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtTemp);
                foreach (DataRow row in dtTemp.Rows)
                {
                    lbxGrupoProduto.Items.Add(row["CODIGONOME"].ToString());
                }
            }

            private void tspApuImprimir_Click(object sender, EventArgs e)
            {
                if (rbnApuraQtdeAno.Checked == true)
                {
                    // Apagar o Arquivo RPT
                    SqlConnection scn;
                    SqlCommand scd;
                    SqlDataReader sdr;
                    scn = new SqlConnection(clsInfo.conexaosqldados);

                    scd = new SqlCommand("delete rptacumulaano ", scn);
                    scn.Open();
                    sdr = scd.ExecuteReader();
                    scn.Close();

                    // Classificar os Periodos para pode acumular mensalmente
                    Mes01Data = clsParser.DateTimeParse(tbxDataDe.Text);
                    Mes01Nom = Mes01Data.Month.ToString().PadLeft(2, '0') + "/" + Mes01Data.Year.ToString();
                    Mes02Data = Mes01Data.AddMonths(1);
                    Mes02Nom = Mes02Data.Month.ToString().PadLeft(2, '0') + "/" + Mes02Data.Year.ToString();
                    Mes03Data = Mes02Data.AddMonths(1);
                    Mes03Nom = Mes03Data.Month.ToString().PadLeft(2, '0') + "/" + Mes03Data.Year.ToString();
                    Mes04Data = Mes03Data.AddMonths(1);
                    Mes04Nom = Mes04Data.Month.ToString().PadLeft(2, '0') + "/" + Mes04Data.Year.ToString();
                    Mes05Data = Mes04Data.AddMonths(1);
                    Mes05Nom = Mes05Data.Month.ToString().PadLeft(2, '0') + "/" + Mes05Data.Year.ToString();
                    Mes06Data = Mes05Data.AddMonths(1);
                    Mes06Nom = Mes06Data.Month.ToString().PadLeft(2, '0') + "/" + Mes06Data.Year.ToString();
                    Mes07Data = Mes06Data.AddMonths(1);
                    Mes07Nom = Mes07Data.Month.ToString().PadLeft(2, '0') + "/" + Mes07Data.Year.ToString();
                    Mes08Data = Mes07Data.AddMonths(1);
                    Mes08Nom = Mes08Data.Month.ToString().PadLeft(2, '0') + "/" + Mes08Data.Year.ToString();
                    Mes09Data = Mes08Data.AddMonths(1);
                    Mes09Nom = Mes09Data.Month.ToString().PadLeft(2, '0') + "/" + Mes09Data.Year.ToString();
                    Mes10Data = Mes09Data.AddMonths(1);
                    Mes10Nom = Mes10Data.Month.ToString().PadLeft(2, '0') + "/" + Mes10Data.Year.ToString();
                    Mes11Data = Mes10Data.AddMonths(1);
                    Mes11Nom = Mes11Data.Month.ToString().PadLeft(2, '0') + "/" + Mes11Data.Year.ToString();
                    Mes12Data = Mes11Data.AddMonths(1);
                    Mes12Nom = Mes12Data.Month.ToString().PadLeft(2, '0') + "/" + Mes12Data.Year.ToString();
                    // Zerar os Valores
                    ValorMes01 = 0;
                    ValorMes02 = 0;
                    ValorMes03 = 0;
                    ValorMes04 = 0;
                    ValorMes05 = 0;
                    ValorMes06 = 0;
                    ValorMes07 = 0;
                    ValorMes08 = 0;
                    ValorMes09 = 0;
                    ValorMes10 = 0;
                    ValorMes11 = 0;
                    ValorMes12 = 0;

                    Grupo = lbxGrupoProduto.Text;

                    // Apurar a Quantidade de Produtos Vendidos no Periodo de 12 Meses
                    DataTable dtaux = new DataTable();
                    dtaux = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
                            "select pedido.data, pedido1.qtde, pedido1.totalnota, pecas.id as [idcodigo], pecas.codigo, pecas.nome as produto, pecasclassifica.codigo as grupo " +
                            "from pedido " +
                            "left join pedido1 on pedido1.idpedido = pedido.id " +
                            "left join pecas on pecas.id = pedido1.idcodigo " +
                            "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica " +
                            "WHERE pedido.data >= " + clsParser.SqlDateTimeFormat(tbxDataDe.Text + " 00:00", true) +
                            " AND  pedido.data <= " + clsParser.SqlDateTimeFormat(tbxDataAte.Text + " 23:59", true) +
                            " order by pecas.nome, pedido.data");
                    Boolean bokAcumula = false;
                    Boolean bokGravar = false;
                    Boolean bokGrupo = false;
                    String PecasCodigo = "";
                    foreach (DataRow row in dtaux.Rows)
                    {
                        if (Grupo.PadRight(3).Substring(0, 3) == "NDC")
                        {
                            // ver todos os grupos
                            bokGrupo = true;
                        }
                        else if (Grupo.PadRight(3).Substring(0, 3) == row["grupo"].ToString())
                        {
                            bokGrupo = true;
                        }
                        else
                        {
                            bokGrupo = false;
                        }
                        if (bokGrupo == true)
                        {
                            if (PecasCodigo == "")
                            {
                                idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
                                PecasCodigo = row["codigo"].ToString();
                            }
                            else if (PecasCodigo != row["codigo"].ToString())
                            {  // Acumular , Zerar e Continuar
                                bokGravar = true;
                            }
                            else if (PecasCodigo == row["codigo"].ToString())
                            {  // Acumular 
                                bokAcumula = true;
                            }
                            if (bokGravar == true)
                            {
                                GravarRegistro();
                                idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
                                PecasCodigo = row["codigo"].ToString();
                                bokGravar = false;

                            }

                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes01Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes01Data.Month)
                            {
                                ValorMes01 = ValorMes01 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes02Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes02Data.Month)
                            {
                                ValorMes02 = ValorMes02 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes03Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes03Data.Month)
                            {
                                ValorMes03 = ValorMes03 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes04Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes04Data.Month)
                            {
                                ValorMes04 = ValorMes04 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes05Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes05Data.Month)
                            {
                                ValorMes05 = ValorMes05 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes06Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes06Data.Month)
                            {
                                ValorMes06 = ValorMes06 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes07Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes07Data.Month)
                            {
                                ValorMes07 = ValorMes07 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes08Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes08Data.Month)
                            {
                                ValorMes08 = ValorMes08 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes09Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes09Data.Month)
                            {
                                ValorMes09 = ValorMes09 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes10Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes10Data.Month)
                            {
                                ValorMes10 = ValorMes10 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes11Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes11Data.Month)
                            {
                                ValorMes11 = ValorMes11 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                            else
                            if (clsParser.DateTimeParse(row["data"].ToString()).Year == Mes12Data.Year && clsParser.DateTimeParse(row["data"].ToString()).Month == Mes12Data.Month)
                            {
                                ValorMes12 = ValorMes12 + clsParser.DecimalParse(row["qtde"].ToString());
                            }
                        }
                    }
                    GravarRegistro();

                    cabecalho = "Qtde de Unidades Vendidas por Produto do Grupo < " + Grupo + " > no Periodo de : " + tbxDataDe.Text + " ate " + tbxDataAte.Text;
                    // Imprimir Relatorio
                    frmCrystalReport frmCrystalReport;
                    frmCrystalReport = new frmCrystalReport();

                    ParameterFields parameters = new ParameterFields();
                    ParameterDiscreteValue valor = new ParameterDiscreteValue();
                    ParameterField field = new ParameterField();

                    field.Name = "EMPRESA";
                    valor.Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from empresas where id=" + clsInfo.zempresaid + " ");
                    field.CurrentValues.Add(valor);
                    parameters.Add(field);
                    // Cabeçalho
                    field = new ParameterField();
                    field.Name = "CABECALHO";
                    valor = new ParameterDiscreteValue();
                    valor.Value = cabecalho;
                    field.CurrentValues.Add(valor);
                    parameters.Add(field);
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "Vendas_Periodo12.rpt", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else if (rbnApuraCusto.Checked == true)
                {  // Imprimir os Itens com seus Custos
                    cabecalho = "";
                    query = "";
                    ordem = "";

                    // Cabeçalho  -- objetivo da lista
                    cabecalho = "Relatorio dos Itens dos Pedidos Com Custo";

                    //Data Emissao
                    cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxDataDe.Text;
                    cabecalho = cabecalho + Environment.NewLine + "Até a data de Emissão: " + tbxDataAte.Text;
                    //if (rbnNorSemTotal.Checked == true)
                    //{
                    //    cabecalho = cabecalho + " Sem Sub-Total ";
                    //}
                    //else
                    //{
                    //    cabecalho = cabecalho + " Com Sub-Total ";
                    //}

                    // Ordem da Lista williams
                    //cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxNorOrdem.Text;

                    // QUERY
                    DataTable Rel = new DataTable();

                    frmCrystalReport frmCrystalReport;
                    frmCrystalReport = new frmCrystalReport();

                    ParameterFields parameters = new ParameterFields();
                    ParameterDiscreteValue valor = new ParameterDiscreteValue();
                    ParameterField field = new ParameterField();

                    field.Name = "EMPRESA";
                    valor.Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome FROM EMPRESA where id=" + clsInfo.zempresaid + " ");
                    field.CurrentValues.Add(valor);
                    parameters.Add(field);
                    // Cabeçalho
                    field = new ParameterField();
                    field.Name = "CABECALHO";
                    valor = new ParameterDiscreteValue();
                    valor.Value = cabecalho;
                    field.CurrentValues.Add(valor);
                    parameters.Add(field);

                    // Da Data emissao
                    field = new ParameterField();
                    field.Name = "DataEmissaoDe";
                    valor = new ParameterDiscreteValue();
                    if (tbxDataDe.Text.Trim() == "")
                    {
                        tbxDataDe.Text = "01/01/1900";
                    }
                    valor.Value = tbxDataDe.Text;
                    field.CurrentValues.Add(valor);
                    parameters.Add(field);
                    // Ate Data emissao
                    field = new ParameterField();
                    field.Name = "DataEmissaoAte";
                    valor = new ParameterDiscreteValue();
                    if (tbxDataAte.Text.Trim() == "")
                    {
                        tbxDataAte.Text = "01/01/2100";
                    }
                    valor.Value = tbxDataAte.Text;
                    field.CurrentValues.Add(valor);
                    parameters.Add(field);

                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "PEDIDOVENDAS_ITENSCUSTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            
                }

            }
            private void GravarRegistro()
            {
                TotalAno = ValorMes01 + ValorMes02 + ValorMes03 + ValorMes04 + ValorMes05 + ValorMes06 + ValorMes07 + ValorMes08 + ValorMes09 + ValorMes10 + ValorMes11 + ValorMes12;
                MediaAno = (TotalAno / 12);

                clsRptAcumulaAnoInfo = new clsRptAcumulaAnoInfo();
                clsRptAcumulaAnoInfo.idcodigo = idcodigo;
                clsRptAcumulaAnoInfo.mediaano = MediaAno;
                clsRptAcumulaAnoInfo.mes01data = Mes01Data;
                clsRptAcumulaAnoInfo.mes01nom = Mes01Nom;
                clsRptAcumulaAnoInfo.mes02data = Mes02Data;
                clsRptAcumulaAnoInfo.mes02nom = Mes02Nom;
                clsRptAcumulaAnoInfo.mes03data = Mes03Data;
                clsRptAcumulaAnoInfo.mes03nom = Mes03Nom;
                clsRptAcumulaAnoInfo.mes04data = Mes04Data;
                clsRptAcumulaAnoInfo.mes04nom = Mes04Nom;
                clsRptAcumulaAnoInfo.mes05data = Mes05Data;
                clsRptAcumulaAnoInfo.mes05nom = Mes05Nom;
                clsRptAcumulaAnoInfo.mes06data = Mes06Data;
                clsRptAcumulaAnoInfo.mes06nom = Mes06Nom;
                clsRptAcumulaAnoInfo.mes07data = Mes07Data;
                clsRptAcumulaAnoInfo.mes07nom = Mes07Nom;
                clsRptAcumulaAnoInfo.mes08data = Mes08Data;
                clsRptAcumulaAnoInfo.mes08nom = Mes08Nom;
                clsRptAcumulaAnoInfo.mes09data = Mes09Data;
                clsRptAcumulaAnoInfo.mes09nom = Mes09Nom;
                clsRptAcumulaAnoInfo.mes10data = Mes10Data;
                clsRptAcumulaAnoInfo.mes10nom = Mes10Nom;
                clsRptAcumulaAnoInfo.mes11data = Mes11Data;
                clsRptAcumulaAnoInfo.mes11nom = Mes11Nom;
                clsRptAcumulaAnoInfo.mes12data = Mes12Data;
                clsRptAcumulaAnoInfo.mes12nom = Mes12Nom;
                clsRptAcumulaAnoInfo.totalano = TotalAno;
                clsRptAcumulaAnoInfo.valormes01 = ValorMes01;
                clsRptAcumulaAnoInfo.valormes02 = ValorMes02;
                clsRptAcumulaAnoInfo.valormes03 = ValorMes03;
                clsRptAcumulaAnoInfo.valormes04 = ValorMes04;
                clsRptAcumulaAnoInfo.valormes05 = ValorMes05;
                clsRptAcumulaAnoInfo.valormes06 = ValorMes06;
                clsRptAcumulaAnoInfo.valormes07 = ValorMes07;
                clsRptAcumulaAnoInfo.valormes08 = ValorMes08;
                clsRptAcumulaAnoInfo.valormes09 = ValorMes09;
                clsRptAcumulaAnoInfo.valormes10 = ValorMes10;
                clsRptAcumulaAnoInfo.valormes11 = ValorMes11;
                clsRptAcumulaAnoInfo.valormes12 = ValorMes12;

                clsRptAcumulaAnoInfo.id = clsRptAcumulaAnoBLL.Incluir(clsRptAcumulaAnoInfo, clsInfo.conexaosqldados);

                ValorMes01 = 0;
                ValorMes02 = 0;
                ValorMes03 = 0;
                ValorMes04 = 0;
                ValorMes05 = 0;
                ValorMes06 = 0;
                ValorMes07 = 0;
                ValorMes08 = 0;
                ValorMes09 = 0;
                ValorMes10 = 0;
                ValorMes11 = 0;
                ValorMes12 = 0;

            }
            private void TrataCampos(Control ctl)
            {
                if (clsInfo.zrow != null && clsInfo.znomegrid != "")
                {
                    //Pegando o Vendedor
                    //if (clsInfo.znomegrid == btnIdCliente.Name)
                    //{
                    //    tbxCliente.Text = clsInfo.zrow.Cells["nom_reduzido"].Value.ToString();
                    //    tbxCliente.Select();
                    //    tbxCliente.SelectAll();
                    //}
                }
                if (!disposing)
                {
                    if (ctl.Name == tbxDataDe.Name)
                    {
                        if (clsParser.DateTimeParse(tbxDataDe.Text) <= clsParser.DateTimeParse(tbxDataAte.Text))
                        {
                            tbxDataAte.Text = clsParser.DateTimeParse(tbxDataDe.Text).AddMonths(12).ToString("dd/MM/yyyy");
                            tbxDataAte.Text = clsParser.DateTimeParse(tbxDataAte.Text).AddDays(-1).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            MessageBox.Show("Verifique Data Ate !");
                        }
                    }
                    if (ctl.Name == tbxDataAte.Name)
                    {
                        if (clsParser.DateTimeParse(tbxDataDe.Text) <= clsParser.DateTimeParse(tbxDataAte.Text))
                        {
                            //tbxDataDe.Text = clsParser.DateTimeParse(tbxDataAte.Text).AddMonths(-12).ToString("dd/MM/yyyy");
                            //tbxDataDe.Text = clsParser.DateTimeParse(tbxDataDe.Text).AddDays(1).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            MessageBox.Show("Verifique Data De !");
                        }
                    }
                    clsInfo.znomegrid = "";
                    clsInfo.zrow = null;
                }
            }

        private void tabCabecalho_Click(object sender, EventArgs e)
        {

        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvGrupo";

            frmPecasClassificaVis frmPecasClassificaVis = new frmPecasClassificaVis();
            frmPecasClassificaVis.Init();
            clsFormHelper.AbrirForm(this, frmPecasClassificaVis, clsInfo.conexaosqldados);
        }
    }
    
}
