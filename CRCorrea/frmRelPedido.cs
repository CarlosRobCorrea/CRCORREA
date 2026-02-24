using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelPedido : Form
    {
        Int32 idfornecedor;
        Int32 idfornecedor1;

        
        
        
        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        ParameterFields pfields = new ParameterFields();

        String sql;
        String query;
        String ordem;
        String cabecalho;

        public frmRelPedido()
        {
            InitializeComponent();
        }

        public void Init()
        {
            
            // Resumo
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResFornecedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResFornecedorAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxResUfDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxResUfAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxResVendedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxResVendedorAte);
            // Item
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxItemClienteDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxItemClienteAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxItemUfDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxItemUfAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxItemVendedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxItemVendedorAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxItemCodDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxItemCodAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from zonas order by codigo", tbxItemZonaDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from zonas order by codigo", tbxItemZonaAte);
        }

        private void tspResRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRelPedidoVendas_Load(object sender, EventArgs e)
        {
            tabFiltros.SelectedIndex = 1;

            lbxResOrdem.SelectedIndex = 0;
            
            lbxItemOrdem.SelectedIndex = 0;
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {            
            clsVisual.ControlLeave(sender);
            clsVisual.FormatarCampoNumerico(sender);
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
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnItemFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteItem1";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }
        private void btnItemCodDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCodigoItem";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECAS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnItemCodAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCodigoItem1";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECAS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        
        private void btnItemUfDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvUfItem";
            frmEstadosPes frmEstadosPes = new frmEstadosPes();
            frmEstadosPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);

        }
        private void btnItemUfAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvUfItem1";
            frmEstadosPes frmEstadosPes = new frmEstadosPes();
            frmEstadosPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);

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

        private void btnItemCustoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvZona";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "ZONAS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnItemCustoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvZona1";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "ZONAS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        // Resumo
        private void bntResFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteRes";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos",0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnResFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteRes1";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos",0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }



        public void Lupa()
        {  
            if (clsInfo.znomegrid == "dgvClienteItem")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idfornecedor > 0 )
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
                    if (idfornecedor1 > 0 )
                    {
                        tbxItemClienteAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor1 + " ");
                    }
                }
                tbxItemClienteAte.Select();
            }

            if (clsInfo.znomegrid == "dgvCodigoItem")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxItemCodDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvCodigoItem1")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 )
                    {
                        tbxItemCodAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvUfItem")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 )
                    {
                        tbxItemUfDe.Text = clsInfo.zrow.Cells["ESTADO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvUfItem1")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 )
                    {
                        tbxItemUfAte.Text = clsInfo.zrow.Cells["ESTADO"].Value.ToString();
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

            if (clsInfo.znomegrid == "dgvVendedorItem1")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxItemVendedorAte.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvZona")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxItemZonaDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvZona1")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 )
                    {
                        tbxItemZonaAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
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
                    if (idfornecedor > 0 )
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
                    if (idfornecedor1 > 0 )
                    {
                        tbxResFornecedorAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE WHERE ID =" + idfornecedor1 + " ");
                    }
                }
                tbxResFornecedorAte.Select();
            }

            if (clsInfo.znomegrid == "dgvEstadoRes")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 )
                    {
                        tbxResUfDe.Text = clsInfo.zrow.Cells["ESTADO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvEstadoRes1")
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 )
                    {
                        tbxResUfAte.Text = clsInfo.zrow.Cells["ESTADO"].Value.ToString();
                    }
                }
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

        private void frmRelPedidoVendas_Activated(object sender, EventArgs e)
        {
            Lupa();
        }

        private void tspItemImprimir_Click(object sender, EventArgs e)
        {
            //
            sql = "";
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            if (rbnItemNumero.Checked == true)
            {
                cabecalho = "Relatorio Pedidos de Venda [Itens] [Numero Pedido]";
            }
            else if (rbnItemCliente.Checked == true)
            {
                cabecalho = "Relatorio Pedidos de Venda [Itens] [Cliente]";
            }
            else if (rbnItemRepresentante.Checked == true)
            {
                cabecalho = "Relatorio Pedidos de Venda [Itens] [Representante]";
            }
            else if (rbnItemDtEntrega.Checked == true)
            {
                cabecalho = "Relatorio Pedidos de Venda [Itens] [Data Entrega]";
            }
            else if (rbnItemCodigo.Checked == true)
            {
                cabecalho = "Relatorio Pedidos de Venda [Itens] [Codigo Produto]";
            }

            // Cliente
            if (tbxItemClienteDe.Text.Length > 0 && tbxItemClienteAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "CLIENTE.COGNOME >= '" + tbxItemClienteDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da " + tbxItemClienteDe.Text;
            }
            if (tbxItemClienteDe.Text.Length == 0 && tbxItemClienteAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "CLIENTE.COGNOME <= '" + tbxItemClienteAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da " + tbxItemClienteAte.Text;
            }
            if (tbxItemClienteDe.Text.Length > 0 && tbxItemClienteAte.Text.Length > 0)
            {
                query = "CLIENTE.COGNOME >= '" + tbxItemClienteDe.Text + "' AND CLIENTE.COGNOME <= '" + tbxItemClienteAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxItemClienteDe.Text + "  até a " + tbxItemClienteAte.Text;
            }
            if (tbxItemClienteDe.Text.Length == 0 && tbxResFornecedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }
            //Data Emissao
            if (clsParser.SqlDateTimeParse(tbxItemDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoDe.Text + " 00:00", true) +
                "AND PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxResDtEmissaoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxItemDtEmissaoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoDe.Text + " 00:00", true) +
                "AND PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Emissão: " + tbxItemDtEmissaoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxItemDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoDe.Text + " 00:00", true) +
                "AND PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxItemDtEmissaoDe.Text + "  Até a data de Emissão: " + tbxItemDtEmissaoAte.Text;
            }
            // UF
            if (tbxItemUfDe.Text.Length > 0 && tbxItemUfAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " ESTADOS.estado >= '" + tbxItemUfDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da  " + tbxItemUfDe.Text;
            }
            if (tbxItemUfDe.Text.Length == 0 && tbxItemUfAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " ESTADOS.estado <= '" + tbxItemUfAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da " + tbxItemUfAte.Text;
            }
            if (tbxItemUfDe.Text.Length > 0 && tbxItemUfAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " ESTADOS.estado >= '" + tbxItemUfDe.Text + "' AND ESTADOS.estado <= '" + tbxItemUfAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxItemUfDe.Text + "  até a " + tbxItemUfAte.Text;
            }
            if (tbxItemUfDe.Text.Length == 0 && tbxItemUfAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }
            // Vendedor
            if (tbxItemVendedorDe.Text.Length > 0 && tbxItemVendedorAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " REPRESENTANTE.COGNOME >= '" + tbxItemVendedorDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da  " + tbxItemVendedorDe.Text;
            }
            if (tbxItemVendedorDe.Text.Length == 0 && tbxItemVendedorAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " REPRESENTANTE.COGNOME <= '" + tbxItemVendedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da " + tbxItemVendedorAte.Text;
            }
            if (tbxItemVendedorDe.Text.Length > 0 && tbxItemVendedorAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " REPRESENTANTE.COGNOME >= '" + tbxItemVendedorDe.Text + "' AND REPRESENTANTE.COGNOME <= '" + tbxItemVendedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxItemVendedorDe.Text + "  até a " + tbxItemVendedorAte.Text;
            }
            if (tbxItemVendedorDe.Text.Length == 0 && tbxItemVendedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }
            // Codigo // Produto
            if (tbxItemCodDe.Text.Length > 0 && tbxItemCodAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PECAS.CODIGO >= '" + tbxItemCodDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da  " + tbxItemCodDe.Text;
            }
            if (tbxItemCodDe.Text.Length == 0 && tbxItemCodAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PECAS.CODIGO <= '" + tbxItemCodAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da " + tbxItemCodAte.Text;
            }
            if (tbxItemCodDe.Text.Length > 0 && tbxItemCodAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PECAS.CODIGO >= '" + tbxItemCodDe.Text + "' AND PECAS.CODIGO <= '" + tbxItemCodAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxItemCodDe.Text + "  até a " + tbxItemCodAte.Text;
            }
            if (tbxItemCodDe.Text.Length == 0 && tbxItemCodAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }
            // Data de Entrega
            if (clsParser.SqlDateTimeParse(tbxItemDtEntregaDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxItemDtEntregaAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PEDIDO2.ENTREGA >= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaDe.Text + " 00:00", true) +
                "AND PEDIDO2.ENTREGA <= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaAte.Text + " 23:59", true);
                
                cabecalho = cabecalho + Environment.NewLine + "Da data de Entrega: " + tbxItemDtEntregaDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxItemDtEntregaDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxItemDtEntregaAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PEDIDO2.ENTREGA >= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaDe.Text + " 00:00", true) +
                "AND PEDIDO2.ENTREGA <= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Entrega: " + tbxItemDtEntregaAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxItemDtEntregaDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxItemDtEntregaAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PEDIDO2.ENTREGA >= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaDe.Text + " 00:00", true) +
                "AND PEDIDO2.ENTREGA <= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Entrega: " + tbxItemDtEntregaDe.Text + "  Até a data de Entrega: " + tbxItemDtEntregaAte.Text;
            }
            // Zona/Região/Rota
            if (tbxItemZonaDe.Text.Length > 0 && tbxItemZonaAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PECAS.CODIGO >= '" + tbxItemZonaDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da  " + tbxItemZonaDe.Text;
            }
            if (tbxItemZonaDe.Text.Length == 0 && tbxItemZonaAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PECAS.CODIGO <= '" + tbxItemZonaAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da " + tbxItemZonaAte.Text;
            }
            if (tbxItemZonaDe.Text.Length > 0 && tbxItemZonaAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PECAS.CODIGO >= '" + tbxItemZonaDe.Text + "' AND PECAS.CODIGO <= '" + tbxItemZonaAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxItemZonaDe.Text + "  até a " + tbxItemZonaAte.Text;
            }
            if (tbxItemZonaDe.Text.Length == 0 && tbxItemZonaAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }


            // Verificar se o Pedido tem Saldo ?
            if (rbnItensPendente.Checked == true)
            { // pedidos pendentes
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PEDIDO2.QTDESALDO > 0 AND PEDIDO1.CANCELADO <> 'S' ";

            }
            else if (rbnItensEntregue.Checked == true)
            {  // pedidos entregues
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PEDIDO2.QTDESALDO = 0 AND PEDIDO1.CANCELADO <> 'S'";

            }
            else if (rbnItensCancelado.Checked == true)
            {  // pedidos entregues
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " PEDIDO1.CANCELADO = 'S' ";

            }
            //Ordem
            if (rbnItemNumero.Checked == true)
            {
                lbxItemOrdem.SelectedIndex = 0;
                ordem = " PEDIDO.NUMERO , PEDIDO2.ENTREGA, PECAS.CODIGO ";

            }
            else if (rbnItemCliente.Checked == true)
            {
                if (lbxItemOrdem.SelectedIndex == 0)
                {
                    ordem = "CLIENTE.COGNOME, PEDIDO2.ENTREGA, PECAS.CODIGO ";
                }
                if (lbxItemOrdem.SelectedIndex == 1)
                {
                    ordem = "CLIENTE.COGNOME, PECAS.CODIGO, PEDIDO2.ENTREGA ";
                }
            }
            else if (rbnItemRepresentante.Checked == true)
            {
                if (lbxItemOrdem.SelectedIndex == 0)
                {
                    ordem = "REPRESENTANTE.COGNOME, PEDIDO2.ENTREGA, PECAS.CODIGO ";
                }
                else if (lbxItemOrdem.SelectedIndex == 1)
                {
                    ordem = "REPRESENTANTE.COGNOME, PECAS.CODIGO, PEDIDO2.ENTREGA ";
                }
                else if (lbxItemOrdem.SelectedIndex == 2)
                {
                    ordem = "REPRESENTANTE.COGNOME, CLIENTE.COGNOME, PEDIDO2.ENTREGA, PECAS.CODIGO ";
                }
            }
            else if (rbnItemDtEntrega.Checked == true)
            {
                if (lbxItemOrdem.SelectedIndex == 0)
                {
                    ordem = "PEDIDO2.ENTREGA, CLIENTE.COGNOME, PECAS.CODIGO ";
                }
                else if (lbxItemOrdem.SelectedIndex == 1)
                {
                    ordem = "PEDIDO2.ENTREGA, ZONAS.CODIGO, CLIENTE.COGNOME, PECAS.CODIGO ";
                }
                else if (lbxItemOrdem.SelectedIndex == 2)
                {
                    ordem = "PEDIDO2.ENTREGA, PECAS.CODIGO, CLIENTE.COGNOME ";
                }
            }
            else if (rbnItemCodigo.Checked == true)
            {
                if (lbxItemOrdem.SelectedIndex == 0)
                {
                    ordem = "PECAS.CODIGO, CLIENTE.COGNOME, PEDIDO2.ENTREGA ";
                }
                else if (lbxItemOrdem.SelectedIndex == 1)
                {
                    ordem = "PECAS.CODIGO, PEDIDO2.ENTREGA, CLIENTE.COGNOME ";
                }
            }

            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnItemAnalitica.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Analitica";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Sintetica";
            }
            //  
            if (rbnItemSemTotal.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Sem Sub-Total";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Com Sub-Total";
            }
            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxItemOrdem.Text;
            // QUERY

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
            sql = "SELECT PEDIDO.ID,PEDIDO.FILIAL,PEDIDO.NUMERO,PEDIDO.DATA,PEDIDO.IDUSUARIO,USUARIO.USUARIO,PEDIDO.IDCLIENTE,CLIENTE.COGNOME AS [CLIENTE],PEDIDO.IDREPRESENTANTE,REPRESENTANTE.COGNOME AS [REPRESENTANTE],PEDIDO.COMISSAOREPRESENTANTE,PEDIDO.IDCOORDENADOR,COORDENADOR.COGNOME AS [COORDENADOR],PEDIDO.COMISSAOCOORDENADOR,PEDIDO.IDSUPERVISOR,SUPERVISOR.COGNOME AS [SUPERVISOR],PEDIDO.COMISSAOSUPERVISOR,PEDIDO.SITUACAO,PEDIDO.OBSERVA,PEDIDO.CONSUMO,PEDIDO.SETOR,PEDIDO.SETORFATOR,PEDIDO.FRETE,PEDIDO.FRETEPAGA,PEDIDO.TRANSPORTE,PEDIDO.TIPOFRETE,PEDIDO.FRETETIPO,PEDIDO.IDTRANSPORTADORA,TRANSPORTADORA.COGNOME AS [TRANSPORTADORA],PEDIDO.FRETEQTDE,PEDIDO.FRETEUNID,PEDIDO.FRETEPRECOUNI,PEDIDO.FRETETOTAL,PEDIDO.FRETEBASEICMS,PEDIDO.FRETEICMALIQ,PEDIDO.FRETEVALORICMS,PEDIDO.IDFORMAPAGTO,SITUACAOTIPOTITULO.CODIGO AS [FORMAPAGTO],PEDIDO.IDCONDPAGTO,CONDPAGTO.CODIGO AS [CONDPAGTO],PEDIDO.TOTALMERCADORIA,PEDIDO.TOTALDESCONTO,PEDIDO.TOTALBCIPI,PEDIDO.TOTALIPI,PEDIDO.TOTALFRETE,PEDIDO.TOTALSEGURO,PEDIDO.TOTALOUTRAS,PEDIDO.TOTALBASEICM,PEDIDO.TOTALICM,PEDIDO.TOTALBASEICMSUBST,PEDIDO.TOTALICMSUBST,PEDIDO.TOTALBCPISPASEP,PEDIDO.TOTALPISPASEP,PEDIDO.TOTALBCCOFINS1,PEDIDO.TOTALCOFINS1,PEDIDO.TOTALPREVISTO,PEDIDO.TOTALFATURAR,PEDIDO.TOTALPEDIDO,PEDIDO.ISENTOIPI,PEDIDO.REVENDEDOR,PEDIDO.ZFM,PEDIDO.AUTORIZA_IDUSUARIO,PEDIDO.AUTORIZA_DATA,PEDIDO.LIMITECREDITOATUAL,PEDIDO.LIMITECREDITOPEDIDOS,PEDIDO.LIMITECREDITORECEBER,PEDIDO.LIMITECREDITORECEBERATRASO,PEDIDO.LIMITECREDITODESCRICAO,PEDIDO.QTDEPEDIDO,PEDIDO.QTDEENTREGUE,PEDIDO.QTDEDEFEITO,PEDIDO.QTDESUCATA,PEDIDO.QTDEBAIXADA,PEDIDO.QTDEOSAUX,PEDIDO.QTDESALDO,PEDIDO.DIVERGENCIA,PEDIDO1.ID AS [IDPEDIDO1],PEDIDO1.IDORCAMENTO,ORCAMENTO.NUMERO AS [ORCAMENTO],PEDIDO1.IDORCAMENTOITEM,PEDIDO1.ITEM,PEDIDO1.IDCODIGO,PECAS.CODIGO AS [CODIGOPC],PECAS.NOME AS [NOMEPC],PEDIDO1.COMPLEMENTO,PEDIDO1.COMPLEMENTO1,PEDIDO1.DESCRICAOESP,PEDIDO1.MSG,PEDIDO1.IDTIPONOTA,TIPONOTA.CODIGO AS [CODIGOTIPONTA],PEDIDO1.CONSUMO,PEDIDO1.IDCFOP,CFOP.CFOP,PEDIDO1.CODIGOEMP01,PEDIDO1.CODIGOEMP02,PEDIDO1.CODIGOEMP03  " +
                    ",PEDIDO1.CODIGOEMP04,PEDIDO1.QTDE AS [QTDE1],PEDIDO1.QTDEENTREGUE AS [QTDEENTREGUE1],PEDIDO1.QTDEDEFEITO AS [QTDEDEFEITO1],PEDIDO1.QTDESUCATA AS [QTDESUCATA1],PEDIDO1.QTDEBAIXADA AS [QTDEBAIXADA1],PEDIDO1.QTDEOSAUX AS [QTDEOSAUX1],PEDIDO1.QTDESALDO AS [QTDESALDO1],PEDIDO1.IDUNIDADE,UNIDADE.CODIGO AS [UNID],PEDIDO1.PRECOLISTA,PEDIDO1.PRECO,PEDIDO1.TOTALMERCADO,PEDIDO1.DESCONTO,PEDIDO1.VALORDESCONTO,PEDIDO1.IDIPI,IPI.CODIGO AS [CLASSFISCAL],PEDIDO1.IPI,PEDIDO1.BCIPI,PEDIDO1.CUSTOIPI,PEDIDO1.IDSITTRIBIPI,SITTRIBUTARIAA.CODIGO AS [CODSTITRIBA],PEDIDO1.BASEICM,PEDIDO1.ICM,PEDIDO1.REDUCAO,PEDIDO1.CUSTOICM,PEDIDO1.IDSITTRIBB,SITTRIBUTARIAB.CODIGO AS [CODSITTRIBB],PEDIDO1.BASEICMSUBST,PEDIDO1.ICMSUBST,PEDIDO1.ICMSSUBSTREDUCAO,PEDIDO1.MVA,PEDIDO1.ICMSSUBSTTOTAL,PEDIDO1.IDSITTRIBPISPASEP,SITTRIBPIS.CODIGO AS [CODSITTRIBPIS],PEDIDO1.BCPISPASEP,PEDIDO1.ALIQPISPASEP,PEDIDO1.PISPASEP,PEDIDO1.IDSITTRIBCOFINS1,SITTRIBCOFINS.CODIGO AS [CODSITTRIBCOFINS],PEDIDO1.BCCOFINS1,PEDIDO1.ALIQCOFINS1,PEDIDO1.COFINS1,PEDIDO1.VALORFRETE,PEDIDO1.VALORSEGURO,PEDIDO1.VALOROUTRAS,PEDIDO1.TOTALPREVISTO,PEDIDO1.TOTALFATURAR,PEDIDO1.TOTALNOTA,PEDIDO1.CALCULOAUTOMATICO,PEDIDO1.PROGRAMACAOTIPO,PEDIDO1.CANCELADO,ORDEMSERVICO.NUMERO AS [NROOS],PEDIDO2.ENTREGA,PEDIDO2.QTDE AS [QTDE2],PEDIDO2.QTDEENTREGUE  AS [QTDEENTREGUE2],PEDIDO2.QTDEDEFEITO AS [QTDEDEFEITO2],PEDIDO2.QTDEBAIXADA AS [QTDEBAIXADA2],PEDIDO2.QTDESUCATA AS [QTDESUCATA2],PEDIDO2.QTDEOSAUX  AS [QTDEOSAUX2],PEDIDO2.QTDESALDO  AS [QTDESALDO2],PEDIDO2.DIVERGENCIA,PEDIDO2.MOTIVO01,PEDIDO2.MOTIVO02, " +
                    "ESTADOS.estado AS [UF], ZONAS.CODIGO AS [ZONACOD], ZONAS.NOME AS [ZONANOME] " +
                    "FROM PEDIDO " +
                    "LEFT JOIN PEDIDO1 ON PEDIDO1.IDPEDIDO=PEDIDO.ID " +
                    "LEFT JOIN PEDIDO2 ON PEDIDO2.IDPEDIDO1=PEDIDO1.ID  " +
                    "LEFT JOIN USUARIO ON USUARIO.ID=PEDIDO.IDUSUARIO " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=PEDIDO.IDCLIENTE " +
                    "LEFT JOIN CLIENTE REPRESENTANTE ON REPRESENTANTE.ID=PEDIDO.IDREPRESENTANTE " +
                    "LEFT JOIN CLIENTE COORDENADOR ON COORDENADOR.ID=PEDIDO.IDCOORDENADOR " +
                    "LEFT JOIN CLIENTE SUPERVISOR ON SUPERVISOR.ID=PEDIDO.IDSUPERVISOR " +
                    "LEFT JOIN CLIENTE TRANSPORTADORA ON TRANSPORTADORA.ID=PEDIDO.IDTRANSPORTADORA " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=PEDIDO.IDFORMAPAGTO " +
                    "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID=PEDIDO.IDCONDPAGTO " +
                    "LEFT JOIN ORCAMENTO ON ORCAMENTO.ID=PEDIDO1.IDORCAMENTO " +
                    "LEFT JOIN ORCAMENTO1 ON ORCAMENTO1.IDORCAMENTO=ORCAMENTO.ID " +
                    "LEFT JOIN PECAS ON PECAS.ID=PEDIDO1.IDCODIGO " +
                    "LEFT JOIN TIPONOTA ON TIPONOTA.ID=PEDIDO1.IDTIPONOTA " +
                    "LEFT JOIN CFOP ON CFOP.ID=PEDIDO1.IDCFOP " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID=PEDIDO1.IDUNIDADE " +
                    "LEFT JOIN IPI ON IPI.ID=PEDIDO1.IDIPI " +
                    "LEFT JOIN SITTRIBIPI ON SITTRIBIPI.ID=PEDIDO1.IDSITTRIBIPI " +
                    "LEFT JOIN SITTRIBUTARIAA ON SITTRIBUTARIAA.ID=PEDIDO1.IDSITTRIBA " +
                    "LEFT JOIN SITTRIBUTARIAB ON SITTRIBUTARIAB.ID=PEDIDO1.IDSITTRIBB " +
                    "LEFT JOIN SITTRIBPIS ON SITTRIBPIS.ID=PEDIDO1.IDSITTRIBPISPASEP " +
                    "LEFT JOIN SITTRIBCOFINS ON SITTRIBCOFINS.ID=PEDIDO1.IDSITTRIBCOFINS1 " +
                    "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID=PEDIDO2.IDORDEMSERVICO " +
                    "LEFT JOIN ESTADOS ON ESTADOS.ID=CLIENTE.IDESTADO " +
                    "LEFT JOIN ZONAS ON ZONAS.ID=CLIENTE.IDZONA ";


            if (query.ToString().Length > 0)
            {
                sql = sql + "where " + query + " ";
            }
            sql = sql + " order by " + ordem;
            /// 
            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            //scd.Parameters.Add("@id", SqlDbType.Int).Value = idPagar;
            Rel = new DataTable();
            sda.Fill(Rel);
            //            Rel = Rel.DefaultView.ToTable();
            // Se for sem quebra


            if (rbnItemNumero.Checked == true)
            {
                // simples
                if (rbnItemSemTotal.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_PEDIDODEVENDAS1_ANA.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_PEDIDODEVENDAS1_ANA_NUMERO.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                }
            }
            else if (rbnItemCliente.Checked == true)
            {
                // simples
                if (rbnItemSemTotal.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_PEDIDODEVENDAS1_ANA.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    if (lbxItemOrdem.SelectedIndex == 0)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_CLIENTE_ENTCOD.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_CLIENTE_CODENT.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }
            }
            else if (rbnItemRepresentante.Checked == true)
            {
                // simples
                if (rbnItemSemTotal.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_PEDIDODEVENDAS1_ANA.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    if (lbxItemOrdem.SelectedIndex == 0)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_VENDEDOR_ENTCOD.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_VENDEDOR_CODENT.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_VENDEDOR_CLIENTCOD.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    } 
                }
            }
            else if (rbnItemDtEntrega.Checked == true)
            {
                // simples
                if (rbnItemSemTotal.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_PEDIDODEVENDAS1_ANA.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    if (lbxItemOrdem.SelectedIndex == 0)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_ENTREGA_CLICOD.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_ENTREGA_ZONCLICOD.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_ENTREGA_CODCLI.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }
            }
            else if (rbnItemCodigo.Checked == true)
            {
                // simples
                if (rbnItemSemTotal.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_PEDIDODEVENDAS1_ANA.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    if (lbxItemOrdem.SelectedIndex == 0)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_CODIGO_CLIENT.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS1_ANA_CODIGO_ENT.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }
            }
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

            // Cabeçalho  -- objetivo da lista
            if (rbnResSimples.Checked == true)
            {
                cabecalho = "Relatorio Simples Pedidos de Venda [Cabeçalho]";
            }
            // Cliente
            if (tbxResFornecedorDe.Text.Length > 0 && tbxResFornecedorAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
               // query = "CLIENTE.COGNOME >= '" + tbxResFornecedorDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da " + tbxResFornecedorDe.Text;
            }
            if (tbxResFornecedorDe.Text.Length == 0 && tbxResFornecedorAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                //query = "CLIENTE.COGNOME <= '" + tbxResFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da "  + tbxResFornecedorAte.Text;
            }
            if (tbxResFornecedorDe.Text.Length > 0 && tbxResFornecedorAte.Text.Length > 0)
            {
               // query = "CLIENTE.COGNOME >= '" + tbxResFornecedorDe.Text + "' AND CLIENTE.COGNOME <= '" + tbxResFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxResFornecedorDe.Text + "  até a " +  tbxResFornecedorAte.Text;
            }
            if (tbxResFornecedorDe.Text.Length == 0 && tbxResFornecedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }
            //Data Emissao
            if (clsParser.SqlDateTimeParse(tbxResDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxResDtEmissaoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                   // query = query + " AND ";
                }
               // query = query + "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxResDtEmissaoDe.Text + " 00:00", true) +
              //  "AND PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxResDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxResDtEmissaoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxResDtEmissaoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxResDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                //    query = query + " AND ";
                }
                //query = query + "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxResDtEmissaoDe.Text + " 00:00", true) +
                //"AND PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxResDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Emissão: " + tbxResDtEmissaoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxResDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxResDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                //    query = query + " AND ";
                }
               //q/uery = query + "PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(tbxResDtEmissaoDe.Text + " 00:00", true) +
               // "AND PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(tbxResDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxResDtEmissaoDe.Text + "  Até a data de Emissão: " + tbxResDtEmissaoAte.Text;
            }
            // UF
            if (tbxResUfDe.Text.Length > 0 && tbxResUfAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                  //  query = query + " AND ";
                }
                //query = query + " ESTADOS.estado >= '" + tbxResUfDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da  " + tbxResUfDe.Text;
            }
            if (tbxResUfDe.Text.Length == 0 && tbxResUfAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                 //   query = query + " AND ";
                }
                //query = query + " ESTADOS.estado <= '" + tbxResUfAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da " + tbxResUfAte.Text;
            }
            if (tbxResUfDe.Text.Length > 0 && tbxResUfAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                 //   query = query + " AND ";
                }
                //query = query + " ESTADOS.estado >= '" + tbxResUfDe.Text + "' AND ESTADOS.estado <= '" + tbxResUfAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxResUfDe.Text + "  até a " + tbxResUfAte.Text;
            }
            if (tbxResUfDe.Text.Length == 0 && tbxResUfAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }
            // Vendedor
            if (tbxResVendedorDe.Text.Length > 0 && tbxResVendedorAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + " REPRESENTANTE.COGNOME >= '" + tbxResVendedorDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " a Partir da  " + tbxResVendedorDe.Text;
            }
            if (tbxResVendedorDe.Text.Length == 0 && tbxResVendedorAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                   // query = query + " AND ";
                }
                //query = query + " REPRESENTANTE.COGNOME <= '" + tbxResVendedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo da " + tbxResVendedorAte.Text;
            }
            if (tbxResVendedorDe.Text.Length > 0 && tbxResVendedorAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
               // query = query + " REPRESENTANTE.COGNOME >= '" + tbxResVendedorDe.Text + "' AND REPRESENTANTE.COGNOME <= '" + tbxResVendedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " da " + tbxResVendedorDe.Text + "  até a " + tbxResVendedorAte.Text;
            }
            if (tbxResVendedorDe.Text.Length == 0 && tbxResVendedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            // Verificar se o Pedido tem Saldo ?
            if (rbnResPendentes.Checked == true)
            { // pedidos pendentes
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + " PEDIDO.QTDESALDO > 0 AND PEDIDO1.CANCELADO <> 'S'";

            }
            else if (rbnResEntregues.Checked == true)
            {  // pedidos entregues
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + " PEDIDO.QTDESALDO = 0 AND PEDIDO1.CANCELADO <> 'S'";

            }

            else if (rbnResCancelado.Checked == true)
            {  // pedidos entregues
                if (query.ToString().Length > 0)
                {
                  //  query = query + " AND ";
                }
                //query = query + " PEDIDO1.CANCELADO = 'S' ";

            }
            //Ordem
            if (lbxResOrdem.SelectedIndex == 0)
            {
            }
            if (lbxResOrdem.SelectedIndex == 1)
            {
                ordem = "CLIENTE.COGNOME, PEDIDO.NUMERO ";                
            }
            if (lbxResOrdem.SelectedIndex == 2)
            {
                ordem = "REPRESENTANTE.COGNOME, PEDIDO.NUMERO ";
            }
            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnResAnalitica.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Analitica";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Sintetica";
            }
            //  
            if (rbnResSemSub.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Sem Sub-Total";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Com Sub-Total";
            }
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxResOrdem.Text;
            DataTable Rel = new DataTable();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "empresa", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
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
            sql = "SELECT PEDIDO.ID,PEDIDO.FILIAL,PEDIDO.NUMERO,PEDIDO.DATA,PEDIDO.IDUSUARIO,USUARIO.USUARIO,PEDIDO.IDCLIENTE,CLIENTE.COGNOME AS [CLIENTE],PEDIDO.IDREPRESENTANTE,REPRESENTANTE.COGNOME AS [REPRESENTANTE],PEDIDO.COMISSAOREPRESENTANTE,PEDIDO.IDCOORDENADOR,COORDENADOR.COGNOME AS [COORDENADOR],PEDIDO.COMISSAOCOORDENADOR,PEDIDO.IDSUPERVISOR,SUPERVISOR.COGNOME AS [SUPERVISOR],PEDIDO.COMISSAOSUPERVISOR,PEDIDO.SITUACAO,PEDIDO.OBSERVA,PEDIDO.CONSUMO,PEDIDO.SETOR,PEDIDO.SETORFATOR,PEDIDO.FRETE,PEDIDO.FRETEPAGA,PEDIDO.TRANSPORTE,PEDIDO.TIPOFRETE,PEDIDO.FRETETIPO,PEDIDO.IDTRANSPORTADORA,TRANSPORTADORA.COGNOME AS [TRANSPORTADORA],PEDIDO.FRETEQTDE,PEDIDO.FRETEUNID,PEDIDO.FRETEPRECOUNI,PEDIDO.FRETETOTAL,PEDIDO.FRETEBASEICMS,PEDIDO.FRETEICMALIQ,PEDIDO.FRETEVALORICMS,PEDIDO.IDFORMAPAGTO,SITUACAOTIPOTITULO.CODIGO AS [FORMAPAGTO],PEDIDO.IDCONDPAGTO,CONDPAGTO.CODIGO AS [CONDPAGTO],PEDIDO.TOTALMERCADORIA,PEDIDO.TOTALDESCONTO,PEDIDO.TOTALBCIPI,PEDIDO.TOTALIPI,PEDIDO.TOTALFRETE,PEDIDO.TOTALSEGURO,PEDIDO.TOTALOUTRAS,PEDIDO.TOTALBASEICM,PEDIDO.TOTALICM,PEDIDO.TOTALBASEICMSUBST,PEDIDO.TOTALICMSUBST,PEDIDO.TOTALBCPISPASEP,PEDIDO.TOTALPISPASEP,PEDIDO.TOTALBCCOFINS1,PEDIDO.TOTALCOFINS1,PEDIDO.TOTALPREVISTO,PEDIDO.TOTALFATURAR,PEDIDO.TOTALPEDIDO,PEDIDO.ISENTOIPI,PEDIDO.REVENDEDOR,PEDIDO.ZFM,PEDIDO.AUTORIZA_IDUSUARIO,PEDIDO.AUTORIZA_DATA,PEDIDO.LIMITECREDITOATUAL,PEDIDO.LIMITECREDITOPEDIDOS,PEDIDO.LIMITECREDITORECEBER,PEDIDO.LIMITECREDITORECEBERATRASO,PEDIDO.LIMITECREDITODESCRICAO,PEDIDO.QTDEPEDIDO,PEDIDO.QTDEENTREGUE,PEDIDO.QTDEDEFEITO,PEDIDO.QTDESUCATA,PEDIDO.QTDEBAIXADA,PEDIDO.QTDEOSAUX,PEDIDO.QTDESALDO,PEDIDO.DIVERGENCIA,PEDIDO1.ID AS [IDPEDIDO1],PEDIDO1.IDORCAMENTO,ORCAMENTO.NUMERO AS [ORCAMENTO],PEDIDO1.IDORCAMENTOITEM,PEDIDO1.ITEM,PEDIDO1.IDCODIGO,PECAS.CODIGO AS [CODIGOPC],PECAS.NOME AS [NOMEPC],PEDIDO1.COMPLEMENTO,PEDIDO1.COMPLEMENTO1,PEDIDO1.DESCRICAOESP,PEDIDO1.MSG,PEDIDO1.IDTIPONOTA,TIPONOTA.CODIGO AS [CODIGOTIPONTA],PEDIDO1.CONSUMO,PEDIDO1.IDCFOP,CFOP.CFOP,PEDIDO1.CODIGOEMP01,PEDIDO1.CODIGOEMP02,PEDIDO1.CODIGOEMP03  " +
                    ",PEDIDO1.CODIGOEMP04,PEDIDO1.QTDE AS [QTDE1],PEDIDO1.QTDEENTREGUE AS [QTDEENTREGUE1],PEDIDO1.QTDEDEFEITO AS [QTDEDEFEITO1],PEDIDO1.QTDESUCATA AS [QTDESUCATA1],PEDIDO1.QTDEBAIXADA AS [QTDEBAIXADA1],PEDIDO1.QTDEOSAUX AS [QTDEOSAUX1],PEDIDO1.QTDESALDO AS [QTDESALDO1],PEDIDO1.IDUNIDADE,UNIDADE.CODIGO AS [UNID],PEDIDO1.PRECOLISTA,PEDIDO1.PRECO,PEDIDO1.TOTALMERCADO,PEDIDO1.DESCONTO,PEDIDO1.VALORDESCONTO,PEDIDO1.IDIPI,IPI.CODIGO AS [CLASSFISCAL],PEDIDO1.IPI,PEDIDO1.BCIPI,PEDIDO1.CUSTOIPI,PEDIDO1.IDSITTRIBIPI,SITTRIBUTARIAA.CODIGO AS [CODSTITRIBA],PEDIDO1.BASEICM,PEDIDO1.ICM,PEDIDO1.REDUCAO,PEDIDO1.CUSTOICM,PEDIDO1.IDSITTRIBB,SITTRIBUTARIAB.CODIGO AS [CODSITTRIBB],PEDIDO1.BASEICMSUBST,PEDIDO1.ICMSUBST,PEDIDO1.ICMSSUBSTREDUCAO,PEDIDO1.MVA,PEDIDO1.ICMSSUBSTTOTAL,PEDIDO1.IDSITTRIBPISPASEP,SITTRIBPIS.CODIGO AS [CODSITTRIBPIS],PEDIDO1.BCPISPASEP,PEDIDO1.ALIQPISPASEP,PEDIDO1.PISPASEP,PEDIDO1.IDSITTRIBCOFINS1,SITTRIBCOFINS.CODIGO AS [CODSITTRIBCOFINS],PEDIDO1.BCCOFINS1,PEDIDO1.ALIQCOFINS1,PEDIDO1.COFINS1,PEDIDO1.VALORFRETE,PEDIDO1.VALORSEGURO,PEDIDO1.VALOROUTRAS,PEDIDO1.TOTALPREVISTO,PEDIDO1.TOTALFATURAR,PEDIDO1.TOTALNOTA,PEDIDO1.CALCULOAUTOMATICO,PEDIDO1.PROGRAMACAOTIPO,PEDIDO1.CANCELADO,ORDEMSERVICO.NUMERO AS [NROOS],PEDIDO2.ENTREGA,PEDIDO2.QTDE AS [QTDE2],PEDIDO2.QTDEENTREGUE  AS [QTDEENTREGUE2],PEDIDO2.QTDEDEFEITO AS [QTDEDEFEITO2],PEDIDO2.QTDEBAIXADA AS [QTDEBAIXADA2],PEDIDO2.QTDESUCATA AS [QTDESUCATA2],PEDIDO2.QTDEOSAUX  AS [QTDEOSAUX2],PEDIDO2.QTDESALDO  AS [QTDESALDO2],PEDIDO2.DIVERGENCIA,PEDIDO2.MOTIVO01,PEDIDO2.MOTIVO02, " +
                    "ESTADOS.estado AS [UF], ZONAS.CODIGO AS [ZONACOD], ZONAS.NOME AS [ZONANOME] " +
                    "FROM PEDIDO " +
                    "LEFT JOIN PEDIDO1 ON PEDIDO1.IDPEDIDO=PEDIDO.ID " +
                    "LEFT JOIN PEDIDO2 ON PEDIDO2.IDPEDIDO1=PEDIDO1.ID  " +
                    "LEFT JOIN USUARIO ON USUARIO.ID=PEDIDO.IDUSUARIO " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=PEDIDO.IDCLIENTE " +
                    "LEFT JOIN CLIENTE REPRESENTANTE ON REPRESENTANTE.ID=PEDIDO.IDREPRESENTANTE " +
                    "LEFT JOIN CLIENTE COORDENADOR ON COORDENADOR.ID=PEDIDO.IDCOORDENADOR " +
                    "LEFT JOIN CLIENTE SUPERVISOR ON SUPERVISOR.ID=PEDIDO.IDSUPERVISOR " +
                    "LEFT JOIN CLIENTE TRANSPORTADORA ON TRANSPORTADORA.ID=PEDIDO.IDTRANSPORTADORA " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=PEDIDO.IDFORMAPAGTO " +
                    "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID=PEDIDO.IDCONDPAGTO " +
                    "LEFT JOIN ORCAMENTO ON ORCAMENTO.ID=PEDIDO1.IDORCAMENTO " +
                    "LEFT JOIN ORCAMENTO1 ON ORCAMENTO1.IDORCAMENTO=ORCAMENTO.ID " +
                    "LEFT JOIN PECAS ON PECAS.ID=PEDIDO1.IDCODIGO " +
                    "LEFT JOIN TIPONOTA ON TIPONOTA.ID=PEDIDO1.IDTIPONOTA " +
                    "LEFT JOIN CFOP ON CFOP.ID=PEDIDO1.IDCFOP " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID=PEDIDO1.IDUNIDADE " +
                    "LEFT JOIN IPI ON IPI.ID=PEDIDO1.IDIPI " +
                    "LEFT JOIN SITTRIBIPI ON SITTRIBIPI.ID=PEDIDO1.IDSITTRIBIPI " +
                    "LEFT JOIN SITTRIBUTARIAA ON SITTRIBUTARIAA.ID=PEDIDO1.IDSITTRIBA " +
                    "LEFT JOIN SITTRIBUTARIAB ON SITTRIBUTARIAB.ID=PEDIDO1.IDSITTRIBB " +
                    "LEFT JOIN SITTRIBPIS ON SITTRIBPIS.ID=PEDIDO1.IDSITTRIBPISPASEP " +
                    "LEFT JOIN SITTRIBCOFINS ON SITTRIBCOFINS.ID=PEDIDO1.IDSITTRIBCOFINS1 " +
                    "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID=PEDIDO2.IDORDEMSERVICO " +
                    "LEFT JOIN ESTADOS ON ESTADOS.ID=CLIENTE.IDESTADO " +
                    "LEFT JOIN ZONAS ON ZONAS.ID=CLIENTE.IDZONA "; 
            if (query.ToString().Length > 0)
            {
                sql = sql + "where " + query + " ";
            }
            sql = sql + " order by " + ordem;
            /// 
            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            Rel = new DataTable();
            sda.Fill(Rel);
            if (rbnResSimples.Checked == true)
            {
                if (rbnResAnalitica.Checked == true)
                {
                    if (rbnResSemSub.Checked == true)
                    { // simples
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_PEDIDODEVENDAS.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    { // com sub total - analitica
                        if (lbxResOrdem.SelectedIndex == 0) // por numero de nota
                        {
                            MessageBox.Show("Por numero de Pedido não efetua totalização.");
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_PEDIDODEVENDAS.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxResOrdem.SelectedIndex == 1) // total por cliente
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_PEDIDODEVENDAS_ANA_CLIENTE.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {  // total por VENDEDOR
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_PEDIDODEVENDAS_ANA_VENDEDOR.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                    }
                }
                else
                { // Sintetica ou Analitica é a mesma
                    MessageBox.Show("As listas Sinteticas não foram criadas..Incluidas dentro dos itens");
                }
            }
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

        private void frmRelPedidoVendas_Activated_1(object sender, EventArgs e)
        {
            Lupa();
        }

        private void bntResVendedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvVendedorRes";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void bntResVendedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvVendedorRes1";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void VerificarOrdensItem()
        {
            if (rbnItemNumero.Checked == true)
            {
                rbnModelo1.Text = "Resumida";
                rbnModelo2.Text = "";
                rbnModelo2.Visible = false;
                rbnModelo3.Visible = false;
                lbxItemOrdem.Items.Clear();
                lbxItemOrdem.Items.Add("Por Numero Pedido + Data Entrega + Codigo Produto ");
                lbxItemOrdem.SelectedIndex = 0;    
            }
            else if (rbnItemCliente.Checked == true)
            {
                rbnModelo1.Text = "Resumida";
                rbnModelo2.Text = "";
                rbnModelo2.Visible = false;
                rbnModelo3.Visible = false;
                lbxItemOrdem.Items.Clear();
                lbxItemOrdem.Items.Add("Por Cliente + Data Entrega + Codigo Produto ");
                lbxItemOrdem.Items.Add("Por Cliente + Codigo Produto + Data Entrega ");
                lbxItemOrdem.SelectedIndex = 0;    
            }
            else if (rbnItemRepresentante.Checked == true)
            {
                rbnModelo1.Text = "Resumida";
                rbnModelo2.Text = "";
                rbnModelo2.Visible = false;
                rbnModelo3.Visible = false;
                lbxItemOrdem.Items.Clear();
                lbxItemOrdem.Items.Add("Por Vendedor + Data Entrega + Codigo Produto ");
                lbxItemOrdem.Items.Add("Por Vendedor + Codigo Produto + Data Entrega ");
                lbxItemOrdem.Items.Add("Por Vendedor + Cliente + Data Entrega + Codigo Produto");
                lbxItemOrdem.SelectedIndex = 0;
            }
            else if (rbnItemDtEntrega.Checked == true)
            {
                rbnModelo1.Text = "Resumida";
                rbnModelo2.Text = "";
                rbnModelo2.Visible = false;
                rbnModelo3.Visible = false;
                lbxItemOrdem.Items.Clear();
                lbxItemOrdem.Items.Add("Por Data Entrega + Cliente + Codigo Produto ");
                lbxItemOrdem.Items.Add("Por Data Entrega + Rota + Cliente + Codigo Produto ");
                lbxItemOrdem.Items.Add("Por Data Entrega + Codigo Produto + Cliente ");
                lbxItemOrdem.SelectedIndex = 0;
            }
            else if (rbnItemCodigo.Checked == true)
            {
                rbnModelo1.Text = "Resumida";
                rbnModelo2.Text = "";
                rbnModelo2.Visible = false;
                rbnModelo3.Visible = false;
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
    }
}
