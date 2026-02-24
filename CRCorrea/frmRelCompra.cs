using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelCompra : Form
    {
        

        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        ParameterFields pfields = new ParameterFields();

        String query;
        String selecionar;
        String ordem;
        String cabecalho;

        public frmRelCompra()
        {
            InitializeComponent();
        }

        public void Init()
        {
            

            pfields = new ParameterFields();

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select numero from compras order by numero", tbxItemNroPedidoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select numero from compras order by numero", tbxItemNroPedidoAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo = 'F' order by cognome", tbxItemFornecedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo = 'F' order by cognome", tbxItemFornecedorAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxItemCodDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxItemCodAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecasclassifica order by codigo", tbxItemGrupoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecasclassifica order by codigo", tbxItemGrupoAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecasclassifica1 order by codigo", tbxItemSubGrupoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecasclassifica1 order by codigo", tbxItemSubGrupoAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecasclassifica2 order by codigo", tbxItemItemSubGrupoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecasclassifica2 order by codigo", tbxItemItemSubGrupoAte);

            rbnItemCodigo.Checked = true;
            rbnModelo1.Checked = true;
            // rbnItemDocumentosTodos.Checked = true;
            rbnItemAnalitica.Checked = true;
            rbnItemSemTotal.Checked = true;

            lbxItemTipoProduto.SelectedIndex = 0;
            lbxItemTipoEntrada.SelectedIndex = 0;
            lbxItemConsumo.SelectedIndex = 0;
            lbxItemOrdem.SelectedIndex = 0;
        }

        private void tspItemImprimir_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            selecionar = "";
            query = "";
            ordem = "";

            cabecalho = "Relatório de Pedido de Compras";

            // Cabeçalho  -- objetivo da lista
            if (rbnItemCodigo.Checked == true) { cabecalho += " [Código]"; }
            if (rbnItemFornecedor.Checked == true) { cabecalho += " [Cta Contabil]"; }
            if (rbnItemDataEntrega.Checked == true) { cabecalho += " [Remessa Enviada]"; }
            //if (rbnItemDocumentosS_Despesas.Checked == true) { cabecalho += "[Pedidos Atrasados]"; }
            
            query += "compras.filial = " + clsInfo.zfilial + " ";

            //filtros
            // Número do Pediso
            if (tbxItemNroPedidoDe.Text.Length > 0 && tbxItemNroPedidoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "compras.numero >= '" + tbxItemNroPedidoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Número: a Partir do " + tbxItemNroPedidoDe.Text;
            }
            if (tbxItemNroPedidoDe.Text.Length == 0 && tbxItemNroPedidoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "compras.numero <= '" + tbxItemNroPedidoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Número: abaixo do " + tbxItemNroPedidoAte.Text;
            }
            if (tbxItemNroPedidoDe.Text.Length > 0 && tbxItemNroPedidoAte.Text.Length > 0)
            {
                query = "compras.numero >= '" + tbxItemNroPedidoDe.Text + "' AND compras.numero <= '" + tbxItemNroPedidoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Número: do " + tbxItemNroPedidoDe.Text + "  até o " + tbxItemNroPedidoAte.Text;
            }
            if (tbxItemNroPedidoDe.Text.Length == 0 && tbxItemNroPedidoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Fornecedor 

            if (tbxItemFornecedorDe.Text.Length > 0 && tbxItemFornecedorAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "cliente.cognome >= '" + tbxItemFornecedorDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: a Partir do " + tbxItemFornecedorDe.Text;
            }
            if (tbxItemFornecedorDe.Text.Length == 0 && tbxItemFornecedorAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "cliente.cognome <= '" + tbxItemFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: abaixo do " + tbxItemFornecedorAte.Text;
            }
            if (tbxItemFornecedorDe.Text.Length > 0 && tbxItemFornecedorAte.Text.Length > 0)
            {
                query = "cliente.cognome >= '" + tbxItemFornecedorDe.Text + "' AND cliente.cognome <= '" + tbxItemFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: do " + tbxItemFornecedorDe.Text + "  até o " + tbxItemFornecedorAte.Text;
            }
            if (tbxItemFornecedorDe.Text.Length == 0 && tbxItemFornecedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Data

            if (tbxItemDtEmissaoDe.Text.Length > 0 && tbxItemDtEmissaoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                } 
                query = query + "compras.data >= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoDe.Text + " 00:00", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + " Data: a Partir de " + tbxItemDtEmissaoDe.Text;
            }
            if (tbxItemDtEmissaoDe.Text.Length == 0 && tbxItemDtEmissaoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "compras.data  <= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: abaixo de " + tbxItemDtEmissaoAte.Text;
            }
            if (tbxItemDtEmissaoDe.Text.Length > 0 && tbxItemDtEmissaoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "compras.data  >= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoDe.Text + " 00:00", true) + " AND compras.data  <= " + clsParser.SqlDateTimeFormat(tbxItemDtEmissaoAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: de " + tbxItemDtEmissaoDe.Text + "  até  " + tbxItemDtEmissaoAte.Text;
            }
            if (tbxItemDtEmissaoDe.Text.Length == 0 && tbxItemDtEmissaoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Data Entrega

            if (tbxItemDtEntregaDe.Text.Length > 0 && tbxItemDtEntregaAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "comprasentrega.dataentrega >= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaDe.Text + " 00:00", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + " Data: a Partir de " + tbxItemDtEntregaDe.Text;
            }
            if (tbxItemDtEntregaDe.Text.Length == 0 && tbxItemDtEntregaAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "comprasentrega.dataentrega <= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: abaixo de " + tbxItemDtEntregaAte.Text;
            }
            if (tbxItemDtEntregaDe.Text.Length > 0 && tbxItemDtEntregaAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "comprasentrega.dataentrega  >= " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaDe.Text + " 00:00", true) + " AND comprasentrega.dataentrega   <=  " + clsParser.SqlDateTimeFormat(tbxItemDtEntregaAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: de " + tbxItemDtEntregaDe.Text + "  até o " + tbxItemDtEntregaAte.Text;
            }
            if (tbxItemDtEntregaDe.Text.Length == 0 && tbxItemDtEntregaAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Código do Material 

            if (tbxItemCodDe.Text.Length > 0 && tbxItemCodAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " pecas.codigo >= '" + tbxItemCodDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Material: a Partir do " + tbxItemCodDe.Text;
            }
            if (tbxItemCodDe.Text.Length == 0 && tbxItemCodAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecas.codigo  <= '" + tbxItemCodAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Material: abaixo do " + tbxItemCodAte.Text;
            }
            if (tbxItemCodDe.Text.Length > 0 && tbxItemCodAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecas.codigo  >= '" + tbxItemCodDe.Text + "' AND pecas.codigo  <= '" + tbxItemCodAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Material: do " + tbxItemCodDe.Text + "  até o " + tbxItemCodAte.Text;
            }
            if (tbxItemCodDe.Text.Length == 0 && tbxItemCodAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Material Grupo tbxItemGrupoDe

            if (tbxItemGrupoDe.Text.Length > 0 && tbxItemGrupoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica.codigo >= '" + tbxItemGrupoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Grupo de Material: a Partir do " + tbxItemGrupoDe.Text;
            }
            if (tbxItemGrupoDe.Text.Length == 0 && tbxItemGrupoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica.codigo  <= '" + tbxItemGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Grupo de Material: abaixo do " + tbxItemGrupoAte.Text;
            }
            if (tbxItemGrupoDe.Text.Length > 0 && tbxItemGrupoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica.codigo  >= '" + tbxItemGrupoDe.Text + "' AND pecasclassifica.codigo  <= '" + tbxItemGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Grupo de Material: do " + tbxItemGrupoDe.Text + "  até o " + tbxItemGrupoAte.Text;
            }
            if (tbxItemGrupoDe.Text.Length == 0 && tbxItemGrupoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            // Item de Sub-Grupo

            if (tbxItemSubGrupoDe.Text.Length > 0 && tbxItemSubGrupoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica1.codigo >= '" + tbxItemSubGrupoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Item de Sub-Grupo: a Partir do " + tbxItemSubGrupoDe.Text;
            }
            if (tbxItemSubGrupoDe.Text.Length == 0 && tbxItemSubGrupoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica1.codigo <= '" + tbxItemSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Item de Sub-Grupo: abaixo do " + tbxItemSubGrupoAte.Text;
            }
            if (tbxItemSubGrupoDe.Text.Length > 0 && tbxItemSubGrupoDe.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica1.codigo  >= '" + tbxItemSubGrupoDe.Text + "' and pecasclassifica1.codigo <= '" + tbxItemSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Item de Sub-Grupo: do " + tbxItemSubGrupoDe.Text + "  até o " + tbxItemSubGrupoAte.Text;
            }
            if (tbxItemSubGrupoDe.Text.Length == 0 && tbxItemSubGrupoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            // Item do Item de Sub-Grupo

            if (tbxItemItemSubGrupoDe.Text.Length > 0 && tbxItemItemSubGrupoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica2.codigo >= '" + tbxItemItemSubGrupoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Item do Item de Sub-Grupo: a Partir do " + tbxItemItemSubGrupoDe.Text;
            }
            if (tbxItemItemSubGrupoDe.Text.Length == 0 && tbxItemItemSubGrupoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica2.codigo <= '" + tbxItemItemSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Item do Item de Sub-Grupo: abaixo do " + tbxItemSubGrupoAte.Text;
            }
            if (tbxItemItemSubGrupoDe.Text.Length > 0 && tbxItemItemSubGrupoDe.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica2.codigo  >= '" + tbxItemItemSubGrupoDe.Text + "' and pecasclassifica2.codigo <= '" + tbxItemItemSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Item do Item de Sub-Grupo: do " + tbxItemItemSubGrupoDe.Text + "  até o " + tbxItemItemSubGrupoAte.Text;
            }
            if (tbxItemItemSubGrupoDe.Text.Length == 0 && tbxItemItemSubGrupoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            // Tipo do Produto
            if (lbxItemTipoProduto.SelectedIndex == 0)
            {
                cabecalho = cabecalho + " Todos os Tipos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'A' ";
                cabecalho = cabecalho + " Apenas tipo Venda";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'B' ";
                cabecalho = cabecalho + " Apenas Tipo Industrialização";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'C' ";
                cabecalho = cabecalho + " Apenas Tipo Conjunto";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'D' ";
                cabecalho = cabecalho + " Apenas Tipo Despesas/Impostos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 5)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'F' ";
                cabecalho = cabecalho + " Apenas Tipo Dispositivos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 6)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'I' ";
                cabecalho = cabecalho + " Apenas Tipo Instrumentos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 7)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'K' ";
                cabecalho = cabecalho + " Apenas Tipo Kan-Ban";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 8)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'M' ";
                cabecalho = cabecalho + " Apenas Tipo Mat.Prima";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 9)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'N' ";
                cabecalho = cabecalho + " Apenas Tipo MP/Emb Clientes";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 10)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'O' ";
                cabecalho = cabecalho + " Apenas Tipo Uso e Consumo";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 11)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'P' ";
                cabecalho = cabecalho + " Apenas Tipo Componente";
            }

            // Tipo da Entrada
            if (lbxItemTipoEntrada.SelectedIndex == 0)
            {
                cabecalho = cabecalho + " Todos os Tipos de Saída";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "cast(compras1.tipoentrada as int) between 0 and 9";
                cabecalho = cabecalho + " Apenas [T0= 00 Até 09 Vendas]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 0";
                cabecalho = cabecalho + " Apenas [00=Venda/Compra de Produto ok]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 1";
                cabecalho = cabecalho + " Apenas [01 - Venda de Produto C/Defeito]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 2";
                cabecalho = cabecalho + " Apenas [02 - Venda/Devolução Pçs Danificadas]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 5)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 3";
                cabecalho = cabecalho + " Apenas [03 - Venda/Devolução Pçs Baixadas]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 6)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 4";
                cabecalho = cabecalho + " Apenas [04 - Produto Retrabalhado]";
            }

            else if (lbxItemTipoEntrada.SelectedIndex == 7)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 9";
                cabecalho = cabecalho + " Apenas [09 - Complemento]";
            }

            else if (lbxItemTipoEntrada.SelectedIndex == 8)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 10";
                cabecalho = cabecalho + " Apenas [10 - Devolução de Embalagem]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 9)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) between 20 and 29";
                cabecalho = cabecalho + " Apenas [T2 =  Do tipo 20 ao 29 - Devolução de Mercadoria]";
            }

            else if (lbxItemTipoEntrada.SelectedIndex == 10)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 20";
                cabecalho = cabecalho + " Apenas [20 - Devolução de Mercadoria Beneficiada]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 11)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 21";
                cabecalho = cabecalho + " Apenas [21 - Devolução de Amostra]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 12)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 22";
                cabecalho = cabecalho + " Apenas [22 - Devolução de Mercadoria Não Beneficiada]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 13)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 30";
                cabecalho = cabecalho + " Apenas [30=Prestação Serviço]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 14)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) between 40 and 49";
                cabecalho = cabecalho + " Apenas [T4 =  Do tipo 40 ao 49]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 15)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 40";
                cabecalho = cabecalho + " Apenas [40 - Remessa]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 16)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 41";
                cabecalho = cabecalho + " Apenas [41 - Remessa MP Terceiro]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 17)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 42";
                cabecalho = cabecalho + " Apenas [42 - Remessa MP Terceiro com contra ordem]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 18)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 44";
                cabecalho = cabecalho + " Apenas [44 - Remessa p/Retrabalho [Deduz Ctas Pagar]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 19)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 45";
                cabecalho = cabecalho + " Apenas [45 - Remessa p/Retrabalho [Sem Debito]]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 20)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(compras1.tipoentrada  as int) = 90";
                cabecalho = cabecalho + " Apenas [90=Imobilizado]";
            }

            // Consumo // Industrialização
            if (lbxItemConsumo.SelectedIndex == 0)
            {
                //cabecalho = cabecalho + Environment.NewLine + "Todas as Situações";
            }
            else if (lbxItemConsumo.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(compras.consumo,1) = 'N' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Revenda/Industrializacao";
            }
            else if (lbxItemConsumo.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(compras.consumo,1) = 'C' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Consumo";
            }

            //Pendencia e atrasos
            if (rbnItemPendente.Checked == true)
            {                
                if (rbnApenasAtraso.Checked == true)
                {
                    if (query.Length > 0) { query += " AND "; }
                    query = query + "comprasentrega.qtdesaldo > 0 and comprasentrega.dataentrega  < GETDATE() ";
                    cabecalho = cabecalho + Environment.NewLine + "Apenas o que está em atraso";
                }
                else if (rbnTodasEntregas.Checked == true)
                {
                    if (query.Length > 0) { query += " AND "; }
                    query = query + "comprasentrega.qtdesaldo > 0 ";
                    cabecalho = cabecalho + Environment.NewLine + "";
                }
                else if (rbnEntregasFuturas.Checked == true)
                {
                    if (query.Length > 0) { query += " AND "; }
                    query = query + "comprasentrega.qtdesaldo > 0 and comprasentrega.dataentrega  > GETDATE() ";
                    cabecalho = cabecalho + Environment.NewLine + "Apenas entregas futuras";
                }
            }
            if (rbnItemTodos.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Todas os pedidos";             
            }

            //ordenação
            if (lbxItemOrdem.SelectedIndex == 0)
            {
                ordem = "cliente.cognome, comprasentrega.dataentrega, compras.numero, pecas.codigo";
            }
            if (lbxItemOrdem.SelectedIndex == 1)
            {
                ordem = " pecas.codigo, comprasentrega.dataentrega, compras.numero, cliente.cognome";
            }
            if (lbxItemOrdem.SelectedIndex == 2)
            {
                ordem = "comprasentrega.dataentrega, cliente.cognome,  pecas.codigo";
            }
            if (lbxItemOrdem.SelectedIndex == 3)
            {
                ordem = "compras.numero";
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

            //Parametros           
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
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

            // Dados
            //Layouts

            //####################################################//
            //# ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY#//
            //####################################################//
            //Modelo 1
            if (rbnModelo1.Checked == true)
            {
                DataTable Rel = new DataTable();
                

                Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                    "comprasentrega " +
                    "left join compras1 on compras1.id = comprasentrega.idcompras1 " +    
                    "left join pecas on pecas.id = compras1.idcodigo " + 
                    "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica  " +
                    "left join pecasclassifica1 on pecasclassifica1.id = pecas.idclassifica1 " +
                    "left join pecasclassifica2 on pecasclassifica2.id = pecas.idclassifica2 " +
                    "left join unidade on unidade.id = compras1.idunidfiscal  " +   
                    "left join compras on compras.id = compras1.idcompras " +    
                    "left join cliente on cliente.id = compras.idfornecedor"    ,
                    "distinct " + 
                    "compras.filial, " +
                    "compras.numero, " +
                    "compras.data, " +
                    "cliente.cognome, " +
                    "cliente.ddd, " +
                    "cliente.telefone as [Telefone] , " +
                    "pecas.codigo, " +
                    "pecas.nome as nome_peca, " +
                    "pecasclassifica.nome as nome_grupo, " +
                    "pecasclassifica.codigo as codigo_grupo, " +
                    "pecasclassifica1.nome as nome_sub_grupo, " +
                    "pecasclassifica1.codigo as codigo_sub_grupo, " +
                    "pecasclassifica2.nome as nome_item_sub, " +
                    "pecasclassifica2.codigo as codigo_item_sub, " +
                    "compras1.complemento, " +
                    "compras1.complemento1, " +
                    "comprasentrega.qtdeentrega, " +
                    "comprasentrega.qtdesaldo, " +
                    "unidade.codigo as codunidade, " +
                    "compras1.preco, " +
                    "comprasentrega.qtdesaldo * compras1.preco as [Total_Mercadoria], " +
                    "comprasentrega.dataentrega ",
                    query,
                    ordem);

                if (rbnItemAnalitica.Checked == true)
                {
                    //Analitico
                    if (rbnItemSemTotal.Checked == true)
                    {
                        // sem subtotal
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                "DADOS_COMPRAS_ANA_SEMSUB.RPT",
                                Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        //com subtotal 
                        if (lbxItemOrdem.SelectedIndex == 0)
                        {
                            cabecalho = cabecalho + Environment.NewLine + "Agrupado por Fornecedor";

                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                   "DADOS_COMPRAS_ANA_COMSUB_FORNECEDOR.RPT",
                                   Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxItemOrdem.SelectedIndex == 1)
                        {
                            cabecalho = cabecalho + Environment.NewLine + "Agrupado por Código";

                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                   "DADOS_COMPRAS_ANA_COMSUB_CODIGO.RPT",
                                   Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxItemOrdem.SelectedIndex == 2)
                        {
                            cabecalho = cabecalho + Environment.NewLine + "Agrupado por Data de Entrega";

                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                       "DADOS_COMPRAS_ANA_COMSUB_DATA_ENTREGA.RPT",
                                       Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxItemOrdem.SelectedIndex == 3)
                        {
                            cabecalho = cabecalho + Environment.NewLine + "Agrupado por Número de Pedido";

                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                       "DADOS_COMPRAS_ANA_COMSUB_PEDIDO.RPT",
                                       Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                    }
                }
                else
                {
                    if (lbxItemOrdem.SelectedIndex == 0)
                    {
                        cabecalho = cabecalho + Environment.NewLine + "Agrupado por Número de Fornecedor";

                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                    "DADOS_COMPRAS_SIN_FORNECEDOR.RPT",
                                    Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }

                    else if (lbxItemOrdem.SelectedIndex == 1)
                    {
                        cabecalho = cabecalho + Environment.NewLine + "Agrupado por Código";

                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                               "DADOS_COMPRAS_SIN_CODIGO.RPT",
                               Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxItemOrdem.SelectedIndex == 2)
                    {
                        cabecalho = cabecalho + Environment.NewLine + "Agrupado por Data de Entrega";

                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                   "DADOS_COMPRAS_SIN_DATA_ENTREGA.RPT",
                                   Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxItemOrdem.SelectedIndex == 3)
                    {
                        cabecalho = cabecalho + Environment.NewLine + "Agrupado por Número de Pedido";

                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                   "DADOS_COMPRAS_SIN_PEDIDO.RPT",
                                   Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }
            }

            //Modelo 2
            if (rbnModelo2.Checked == true)
            {                
                DataTable RelDivergencia = new DataTable();

                RelDivergencia = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                    "compras " +
                    "left join cliente on cliente.id = compras.idfornecedor " +
                    "inner join compras1 on compras1.idcompras = compras.id " +
                    "left join pecas on pecas.id = compras1.idcodigo " +
                    "inner join comprasentrega on comprasentrega.idcompras1=compras1.id " +
                    "left join nfcompra1 on nfcompra1.iditempedidoentrega = comprasentrega.id " +
                    "inner join nfcompra on nfcompra.id = nfcompra1.numero " +
                    "left join unidade on unidade.id=compras1.idunidfiscal " +
                    "left join unidade unidadenfe on unidadenfe.id=nfcompra1.idunidfiscal ",
                    "compras.numero, " +
                    "cliente.cognome, " +
                    "comprasentrega.dataentrega, " +
                    "nfcompra.datarecebimento, " +
                    "comprasentrega.qtdeentrega, " +
                    "nfcompra1.qtdefiscal, " +
                    "nfcompra.numero as nfe, " +
                    "unidade.codigo as UnidPed, " +
                    "unidadenfe.codigo as UnidNFE, " +
                    "compras1.preco as precoped, " +
                    "nfcompra1.preco as preconfe, " +
                    "pecas.codigo, pecas.nome, " +
                    "DATEDIFF(DD, nfcompra.datarecebimento, " +
                    "comprasentrega.dataentrega) AS QtdeDias " ,
                    "compras.numero > 0 AND DATEDIFF(DD, nfcompra.datarecebimento, comprasentrega.dataentrega) <> 0 " +
                    "AND compras1.idunidfiscal <> nfcompra1.idunidfiscal and compras1.preco <> nfcompra1.preco " +
                    "and comprasentrega.qtdeentrega <> nfcompra1.qtdefiscal and " + query,
                    ordem);                   

              
                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                                  "DADOS_COMPRAS_DIVERGENCIA.RPT",
                                                  RelDivergencia, pfields, "", clsInfo.conexaosqldados);

                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            if (rbnModelo3.Checked == true)
            {                
                DataTable RelAtraso = new DataTable();

                RelAtraso = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                    "compras " +
                    "left join cliente on cliente.id = compras.idfornecedor "+
                    "inner join compras1 on compras1.idcompras = compras.id "+
                    "left join pecas on pecas.id = compras1.idcodigo "+
                    "inner join comprasentrega on comprasentrega.idcompras1=compras1.id "+
                    "left join nfcompra1 on nfcompra1.iditempedidoentrega = comprasentrega.id "+
                    "inner join nfcompra on nfcompra.id = nfcompra1.numero ",
                    "compras.numero, "+
                    "cliente.cognome, "+
                    "comprasentrega.dataentrega, "+ 
                    "comprasentrega.qtdeentrega, "+
                    "nfcompra.numero as nfe, "+ 
                    "nfcompra.datarecebimento, "+ 
                    "nfcompra1.qtdefiscal, "+
                    "pecas.codigo, "+
                    "pecas.nome, "+
                    "DATEDIFF(DD, nfcompra.datarecebimento, comprasentrega.dataentrega) AS QtdeDias " ,
                    "compras.numero > 0 AND DATEDIFF(DD, nfcompra.datarecebimento, comprasentrega.dataentrega) > 0",
                    ordem);

                cabecalho = cabecalho + Environment.NewLine + "Análise de Entregas Atrasadas";

                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                                  "DADOS_COMPRAS_ATRASO.RPT",
                                                  RelAtraso, pfields, "", clsInfo.conexaosqldados);

                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
        
        }

        

       
        private void tspItemRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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

        private void btnItemNumeroPedidoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemNumeroPedidoDe.Name;
            frmComprasPes frmComprasPes = new frmComprasPes();
            frmComprasPes.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmComprasPes, clsInfo.conexaosqldados);
        }

        private void btnItemNumeroPedidoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemNumeroPedidoAte.Name;
            frmComprasPes frmComprasPes = new frmComprasPes();
            frmComprasPes.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmComprasPes, clsInfo.conexaosqldados);
        }

        private void btnItemFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemFornecedorDe.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Fornecedor", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnItemFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemFornecedorAte.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Fornecedor", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnItemCodDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemCodDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecas", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnItemCodAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemCodAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecas", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnItemGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemGrupoDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnItemGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemGrupoAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnItemSubGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemSubGrupoDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica1", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnItemSubGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemSubGrupoAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica1", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnItemItemSubGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemItemSubGrupoDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica2", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnItemItemSubGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemItemSubGrupoAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica2", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }
        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1 &&
                clsInfo.znomegrid != null &&
                clsInfo.znomegrid != "")
            {
                // ###############################
                // Verifica os botões de pesquisa ABA Item
                // ###############################
                // Numero do Pedido
                if (clsInfo.znomegrid == btnItemNumeroPedidoDe.Name)
                {
                    tbxItemNroPedidoDe.Text = clsInfo.zrow.Cells["numero"].Value.ToString();
                    if (tbxItemNroPedidoAte.Text.Length <= 0)
                    {
                        tbxItemNroPedidoAte.Text = tbxItemNroPedidoDe.Text;
                    }
                    tbxItemNroPedidoDe.Select();
                    tbxItemNroPedidoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnItemNumeroPedidoAte.Name)
                {
                    tbxItemNroPedidoAte.Text = clsInfo.zrow.Cells["numero"].Value.ToString();
                    tbxItemNroPedidoAte.Select();
                    tbxItemNroPedidoAte.SelectAll();
                }
                //Fornecedor

                if (clsInfo.znomegrid == btnItemFornecedorDe.Name)
                {
                    tbxItemFornecedorDe.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    if (tbxItemFornecedorAte.Text.Length <= 0)
                    {
                        tbxItemFornecedorAte.Text = tbxItemFornecedorDe.Text;
                    }
                    tbxItemFornecedorDe.Select();
                    tbxItemFornecedorDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnItemFornecedorAte.Name)
                {
                    tbxItemFornecedorAte.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    tbxItemFornecedorAte.Select();
                    tbxItemFornecedorAte.SelectAll();
                }

                //Item Código

                if (clsInfo.znomegrid == btnItemCodDe.Name)
                {
                    tbxItemCodDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxItemCodAte.Text.Length <= 0)
                    {
                        tbxItemCodAte.Text = tbxItemCodDe.Text;
                    }
                    tbxItemCodDe.Select();
                    tbxItemCodDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnItemCodAte.Name)
                {
                    tbxItemCodAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxItemCodAte.Select();
                    tbxItemCodAte.SelectAll();
                }

                //Zona

                if (clsInfo.znomegrid == btnItemGrupoDe.Name)
                {
                    tbxItemGrupoDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxItemGrupoAte.Text.Length <= 0)
                    {
                        tbxItemGrupoAte.Text = tbxItemGrupoDe.Text;
                    }
                    tbxItemGrupoDe.Select();
                    tbxItemGrupoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnItemGrupoAte.Name)
                {
                    tbxItemGrupoAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxItemGrupoAte.Select();
                    tbxItemGrupoAte.SelectAll();
                }

                //Item de Sub-Grupo

                if (clsInfo.znomegrid == btnItemSubGrupoDe.Name)
                {
                    tbxItemSubGrupoDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxItemSubGrupoAte.Text.Length <= 0)
                    {
                        tbxItemSubGrupoAte.Text = tbxItemSubGrupoDe.Text;
                    }
                    tbxItemSubGrupoDe.Select();
                    tbxItemSubGrupoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnItemSubGrupoAte.Name)
                {
                    tbxItemSubGrupoAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxItemSubGrupoAte.Select();
                    tbxItemSubGrupoAte.SelectAll();
                }

                //Item item de subgrupo

                if (clsInfo.znomegrid == btnItemItemSubGrupoDe.Name)
                {
                    tbxItemItemSubGrupoDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxItemItemSubGrupoAte.Text.Length <= 0)
                    {
                        tbxItemItemSubGrupoAte.Text = tbxItemItemSubGrupoDe.Text;
                    }
                    tbxItemItemSubGrupoDe.Select();
                    tbxItemItemSubGrupoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnItemItemSubGrupoAte.Name)
                {
                    tbxItemItemSubGrupoAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxItemItemSubGrupoAte.Select();
                    tbxItemItemSubGrupoAte.SelectAll();
                }
                //////////////////
                // ###############################
                // Verifica os botões de pesquisa ABA Especificação
                // Numero do Pedido
                if (clsInfo.znomegrid == btnEspNumeroPedidoDe.Name)
                {
                    tbxEspNroPedidoDe.Text = clsInfo.zrow.Cells["numero"].Value.ToString();
                    if (tbxEspNroPedidoAte.Text.Length <= 0)
                    {
                        tbxEspNroPedidoAte.Text = tbxEspNroPedidoDe.Text;
                    }
                    tbxEspNroPedidoDe.Select();
                    tbxEspNroPedidoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnEspNumeroPedidoAte.Name)
                {
                    tbxEspNroPedidoAte.Text = clsInfo.zrow.Cells["numero"].Value.ToString();
                    tbxEspNroPedidoAte.Select();
                    tbxEspNroPedidoAte.SelectAll();
                }
                //Fornecedor

                if (clsInfo.znomegrid == btnEspFornecedorDe.Name)
                {
                    tbxEspFornecedorDe.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    if (tbxEspFornecedorAte.Text.Length <= 0)
                    {
                        tbxEspFornecedorAte.Text = tbxEspFornecedorDe.Text;
                    }
                    tbxEspFornecedorDe.Select();
                    tbxEspFornecedorDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnEspFornecedorAte.Name)
                {
                    tbxEspFornecedorAte.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    tbxEspFornecedorAte.Select();
                    tbxEspFornecedorAte.SelectAll();
                }

                //Esp Código

                if (clsInfo.znomegrid == btnEspCodDe.Name)
                {
                    tbxEspCodDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxEspCodAte.Text.Length <= 0)
                    {
                        tbxEspCodAte.Text = tbxEspCodDe.Text;
                    }
                    tbxEspCodDe.Select();
                    tbxEspCodDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnEspCodAte.Name)
                {
                    tbxEspCodAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxEspCodAte.Select();
                    tbxEspCodAte.SelectAll();
                }

                //Zona

                if (clsInfo.znomegrid == btnEspGrupoDe.Name)
                {
                    tbxEspGrupoDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxEspGrupoAte.Text.Length <= 0)
                    {
                        tbxEspGrupoAte.Text = tbxEspGrupoDe.Text;
                    }
                    tbxEspGrupoDe.Select();
                    tbxEspGrupoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnEspGrupoAte.Name)
                {
                    tbxEspGrupoAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxEspGrupoAte.Select();
                    tbxEspGrupoAte.SelectAll();
                }

                //Esp de Sub-Grupo

                if (clsInfo.znomegrid == btnEspSubGrupoDe.Name)
                {
                    tbxEspSubGrupoDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxEspSubGrupoAte.Text.Length <= 0)
                    {
                        tbxEspSubGrupoAte.Text = tbxEspSubGrupoDe.Text;
                    }
                    tbxEspSubGrupoDe.Select();
                    tbxEspSubGrupoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnEspSubGrupoAte.Name)
                {
                    tbxEspSubGrupoAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxEspSubGrupoAte.Select();
                    tbxEspSubGrupoAte.SelectAll();
                }

                //Esp Esp de subgrupo

                if (clsInfo.znomegrid == btnEspItemSubGrupoDe.Name)
                {
                    tbxEspItemSubGrupoDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxEspItemSubGrupoAte.Text.Length <= 0)
                    {
                        tbxEspItemSubGrupoAte.Text = tbxEspItemSubGrupoDe.Text;
                    }
                    tbxEspItemSubGrupoDe.Select();
                    tbxEspItemSubGrupoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnEspItemSubGrupoAte.Name)
                {
                    tbxEspItemSubGrupoAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxEspItemSubGrupoAte.Select();
                    tbxEspItemSubGrupoAte.SelectAll();
                }



            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa // 1a ABA
                // ###############################
                //Número do Pedido 
                if (ctl.Name == tbxItemNroPedidoDe.Name)    // 
                {
                    if (tbxItemNroPedidoDe.Text.Length > 0 && tbxItemNroPedidoAte.Text.Length <= 0)
                    {
                        tbxItemNroPedidoAte.Text = tbxItemNroPedidoDe.Text;
                    }
                }

                //Fornecedor

                if (ctl.Name == tbxItemFornecedorDe.Name)
                {
                    if (tbxItemFornecedorDe.Text.Length > 0 && tbxItemFornecedorAte.Text.Length <= 0)
                    {
                        tbxItemFornecedorAte.Text = tbxItemFornecedorDe.Text;
                    }
                }

                //Data Emissão

                if (ctl.Name == tbxItemDtEmissaoDe.Name)    // 
                {
                    if (tbxItemDtEmissaoDe.Text.Length > 0 && tbxItemDtEmissaoAte.Text.Length <= 0)
                    {
                        tbxItemDtEmissaoAte.Text = tbxItemDtEmissaoDe.Text;
                    }
                }

                //Data Entrega

                if (ctl.Name == tbxItemDtEntregaDe.Name)    // 
                {
                    if (tbxItemDtEntregaDe.Text.Length > 0 && tbxItemDtEntregaAte.Text.Length <= 0)
                    {
                        tbxItemDtEntregaAte.Text = tbxItemDtEntregaDe.Text;
                    }
                }

                //Código do Item

                if (ctl.Name == tbxItemCodDe.Name)    // 
                {
                    if (tbxItemCodDe.Text.Length > 0 && tbxItemCodAte.Text.Length <= 0)
                    {
                        tbxItemCodAte.Text = tbxItemCodDe.Text;
                    }
                }

                //Grupo

                if (ctl.Name == tbxItemGrupoDe.Name)    // 
                {
                    if (tbxItemGrupoDe.Text.Length > 0 && tbxItemGrupoDe.Text.Length <= 0)
                    {
                        tbxItemGrupoAte.Text = tbxItemGrupoDe.Text;
                    }
                }

                //Item de Sub_grupo

                if (ctl.Name == tbxItemSubGrupoDe.Name)    // 
                {
                    if (tbxItemSubGrupoDe.Text.Length > 0 && tbxItemSubGrupoAte.Text.Length <= 0)
                    {
                        tbxItemSubGrupoAte.Text = tbxItemSubGrupoDe.Text;
                    }
                }

                //Item Item de Sub_grupo

                if (ctl.Name == tbxItemItemSubGrupoDe.Name)    // 
                {
                    if (tbxItemItemSubGrupoDe.Text.Length > 0 && tbxItemItemSubGrupoAte.Text.Length <= 0)
                    {
                        tbxItemItemSubGrupoAte.Text = tbxItemItemSubGrupoDe.Text;
                    }
                }

                // ###############################
                // Verifica os campos de pesquisa // 2a ABA
                // ###############################
                //Número do Pedido 
                if (ctl.Name == tbxEspNroPedidoDe.Name)    // 
                {
                    if (tbxEspNroPedidoDe.Text.Length > 0 && tbxEspNroPedidoAte.Text.Length <= 0)
                    {
                        tbxEspNroPedidoAte.Text = tbxEspNroPedidoDe.Text;
                    }
                }

                //Fornecedor

                if (ctl.Name == tbxEspFornecedorDe.Name)
                {
                    if (tbxEspFornecedorDe.Text.Length > 0 && tbxEspFornecedorAte.Text.Length <= 0)
                    {
                        tbxEspFornecedorAte.Text = tbxEspFornecedorDe.Text;
                    }
                }

                //Data Emissão

                if (ctl.Name == tbxEspDtEmissaoDe.Name)    // 
                {
                    if (tbxEspDtEmissaoDe.Text.Length > 0 && tbxEspDtEmissaoAte.Text.Length <= 0)
                    {
                        tbxEspDtEmissaoAte.Text = tbxEspDtEmissaoDe.Text;
                    }
                }

                //Data Entrega

                if (ctl.Name == tbxEspDtEntregaDe.Name)    // 
                {
                    if (tbxEspDtEntregaDe.Text.Length > 0 && tbxEspDtEntregaAte.Text.Length <= 0)
                    {
                        tbxEspDtEntregaAte.Text = tbxEspDtEntregaDe.Text;
                    }
                }

                //Código do Esp

                if (ctl.Name == tbxEspCodDe.Name)    // 
                {
                    if (tbxEspCodDe.Text.Length > 0 && tbxEspCodAte.Text.Length <= 0)
                    {
                        tbxEspCodAte.Text = tbxEspCodDe.Text;
                    }
                }

                //Grupo

                if (ctl.Name == tbxEspGrupoDe.Name)    // 
                {
                    if (tbxEspGrupoDe.Text.Length > 0 && tbxEspGrupoDe.Text.Length <= 0)
                    {
                        tbxEspGrupoAte.Text = tbxEspGrupoDe.Text;
                    }
                }

                //Item de Sub_grupo

                if (ctl.Name == tbxEspSubGrupoDe.Name)    // 
                {
                    if (tbxEspSubGrupoDe.Text.Length > 0 && tbxEspSubGrupoAte.Text.Length <= 0)
                    {
                        tbxEspSubGrupoAte.Text = tbxEspSubGrupoDe.Text;
                    }
                }

                //Item Item de Sub_grupo

                if (ctl.Name == tbxEspItemSubGrupoDe.Name)    // 
                {
                    if (tbxEspItemSubGrupoDe.Text.Length > 0 && tbxEspItemSubGrupoAte.Text.Length <= 0)
                    {
                        tbxEspItemSubGrupoAte.Text = tbxEspItemSubGrupoDe.Text;
                    }
                }
            }

            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private void frmRelCompra_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void frmRelCompra_Load(object sender, EventArgs e)
        {
            lbxEspOrdem.SelectedIndex = 0;
        }

        private void rbnModelo1_CheckedChanged(object sender, EventArgs e)
        {
            if(rbnModelo1.Checked == true)
            {
                gbxItemPendencia.Enabled = true;
                gbxTipoPendencia.Enabled = true;
                gbxItemAnalSint.Enabled = true;
                gbxItemAnalSint.Enabled = true;
                gbxItemSub.Enabled = true;
                rbnItemPendente.Checked = true;

            }
            else if (rbnModelo2.Checked == true)
            {
                rbnItemTodos.Checked = true;
                gbxItemPendencia.Enabled = false;
                rbnTodasEntregas.Checked = true;
                gbxTipoPendencia.Enabled = false;
                rbnItemAnalitica.Checked = true;
                gbxItemAnalSint.Enabled = false;
                rbnItemComTotal.Checked = true;
                gbxItemAnalSint.Enabled = false;
                rbnItemSemTotal.Checked = true;
                gbxItemSub.Enabled = false;
            }
            else if (rbnModelo3.Checked == true)
            {
                rbnItemTodos.Checked = true;
                gbxItemPendencia.Enabled = false;
                rbnTodasEntregas.Checked = true;
                gbxTipoPendencia.Enabled = false;
                rbnItemAnalitica.Checked = true;
                gbxItemAnalSint.Enabled = false;
                rbnItemComTotal.Checked = true;
                gbxItemAnalSint.Enabled = false;
                rbnItemSemTotal.Checked = true;
                gbxItemSub.Enabled = false;
            }
        }
        private void rbnItemTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnItemTodos.Checked == true)
            {
                rbnTodasEntregas.Checked = true;
                gbxTipoPendencia.Enabled = false;
            }
            else
            {
                gbxTipoPendencia.Enabled = true;
            }
        }

        private void rbnItemAnalitica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnItemAnalitica.Checked == true)
            {
                gbxItemSub.Enabled = true;
            }
            else
            {
                rbnItemComTotal.Checked = true;
                gbxItemSub.Enabled = false;
            }
        }
        private void tspItemRetornar_Click_1(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void tspEspImprimir_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            query = "";
            selecionar = "";
            ordem = "";

            cabecalho = "Todos os Pedidos de Compra Emitidos";
            //filtros
            // Número do Pediso
            if (tbxEspNroPedidoDe.Text.Length > 0 && tbxEspNroPedidoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "compras.numero >= '" + tbxEspNroPedidoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Número: a Partir do " + tbxEspNroPedidoDe.Text;
            }
            if (tbxEspNroPedidoDe.Text.Length == 0 && tbxEspNroPedidoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "compras.numero <= '" + tbxEspNroPedidoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Número: abaixo do " + tbxEspNroPedidoAte.Text;
            }
            if (tbxEspNroPedidoDe.Text.Length > 0 && tbxEspNroPedidoAte.Text.Length > 0)
            {
                query = "compras.numero >= '" + tbxEspNroPedidoDe.Text + "' AND compras.numero <= '" + tbxEspNroPedidoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Número: do " + tbxEspNroPedidoDe.Text + "  até o " + tbxEspNroPedidoAte.Text;
            }
            if (tbxEspNroPedidoDe.Text.Length == 0 && tbxEspNroPedidoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Fornecedor 

            if (tbxEspFornecedorDe.Text.Length > 0 && tbxEspFornecedorAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "cliente.cognome >= '" + tbxEspFornecedorDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: a Partir do " + tbxEspFornecedorDe.Text;
            }
            if (tbxEspFornecedorDe.Text.Length == 0 && tbxEspFornecedorAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "cliente.cognome <= '" + tbxEspFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: abaixo do " + tbxEspFornecedorAte.Text;
            }
            if (tbxEspFornecedorDe.Text.Length > 0 && tbxEspFornecedorAte.Text.Length > 0)
            {
                query = "cliente.cognome >= '" + tbxEspFornecedorDe.Text + "' AND cliente.cognome <= '" + tbxEspFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: do " + tbxEspFornecedorDe.Text + "  até o " + tbxEspFornecedorAte.Text;
            }
            if (tbxEspFornecedorDe.Text.Length == 0 && tbxEspFornecedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Data

            if (tbxEspDtEmissaoDe.Text.Length > 0 && tbxEspDtEmissaoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "compras.data >= " + clsParser.SqlDateTimeFormat(tbxEspDtEmissaoDe.Text + " 00:00", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + " Data: a Partir de " + tbxEspDtEmissaoDe.Text;
            }
            if (tbxEspDtEmissaoDe.Text.Length == 0 && tbxEspDtEmissaoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "compras.data  <= " + clsParser.SqlDateTimeFormat(tbxEspDtEmissaoAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: abaixo de " + tbxEspDtEmissaoAte.Text;
            }
            if (tbxEspDtEmissaoDe.Text.Length > 0 && tbxEspDtEmissaoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "compras.data  >= " + clsParser.SqlDateTimeFormat(tbxEspDtEmissaoDe.Text + " 00:00", true) + " AND compras.data  <= " + clsParser.SqlDateTimeFormat(tbxEspDtEmissaoAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: de " + tbxEspDtEmissaoDe.Text + "  até  " + tbxEspDtEmissaoAte.Text;
            }
            if (tbxEspDtEmissaoDe.Text.Length == 0 && tbxEspDtEmissaoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Data Entrega

            if (tbxEspDtEntregaDe.Text.Length > 0 && tbxEspDtEntregaAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "comprasentrega.dataentrega >= " + clsParser.SqlDateTimeFormat(tbxEspDtEntregaDe.Text + " 00:00", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + " Data: a Partir de " + tbxEspDtEntregaDe.Text;
            }
            if (tbxEspDtEntregaDe.Text.Length == 0 && tbxEspDtEntregaAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "comprasentrega.dataentrega <= " + clsParser.SqlDateTimeFormat(tbxEspDtEntregaAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: abaixo de " + tbxEspDtEntregaAte.Text;
            }
            if (tbxEspDtEntregaDe.Text.Length > 0 && tbxEspDtEntregaAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "comprasentrega.dataentrega  >= " + clsParser.SqlDateTimeFormat(tbxEspDtEntregaDe.Text + " 00:00", true) + " AND comprasentrega.dataentrega   <=  " + clsParser.SqlDateTimeFormat(tbxEspDtEntregaAte.Text + " 23:59", true) + " ";
                cabecalho = cabecalho + Environment.NewLine + "Data: de " + tbxEspDtEntregaDe.Text + "  até o " + tbxEspDtEntregaAte.Text;
            }
            if (tbxEspDtEntregaDe.Text.Length == 0 && tbxEspDtEntregaAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Código do Material 

            if (tbxEspCodDe.Text.Length > 0 && tbxEspCodAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " pecas.codigo >= '" + tbxEspCodDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Material: a Partir do " + tbxEspCodDe.Text;
            }
            if (tbxEspCodDe.Text.Length == 0 && tbxEspCodAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecas.codigo  <= '" + tbxEspCodAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Material: abaixo do " + tbxEspCodAte.Text;
            }
            if (tbxEspCodDe.Text.Length > 0 && tbxEspCodAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecas.codigo  >= '" + tbxEspCodDe.Text + "' AND pecas.codigo  <= '" + tbxEspCodAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Material: do " + tbxEspCodDe.Text + "  até o " + tbxEspCodAte.Text;
            }
            if (tbxEspCodDe.Text.Length == 0 && tbxEspCodAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Material Grupo tbxEspGrupoDe

            if (tbxEspGrupoDe.Text.Length > 0 && tbxEspGrupoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica.codigo >= '" + tbxEspGrupoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Grupo de Material: a Partir do " + tbxEspGrupoDe.Text;
            }
            if (tbxEspGrupoDe.Text.Length == 0 && tbxEspGrupoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica.codigo  <= '" + tbxEspGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Grupo de Material: abaixo do " + tbxEspGrupoAte.Text;
            }
            if (tbxEspGrupoDe.Text.Length > 0 && tbxEspGrupoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica.codigo  >= '" + tbxEspGrupoDe.Text + "' AND pecasclassifica.codigo  <= '" + tbxEspGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Grupo de Material: do " + tbxEspGrupoDe.Text + "  até o " + tbxEspGrupoAte.Text;
            }
            if (tbxEspGrupoDe.Text.Length == 0 && tbxEspGrupoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            // Esp de Sub-Grupo

            if (tbxEspSubGrupoDe.Text.Length > 0 && tbxEspSubGrupoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica1.codigo >= '" + tbxEspSubGrupoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Esp de Sub-Grupo: a Partir do " + tbxEspSubGrupoDe.Text;
            }
            if (tbxEspSubGrupoDe.Text.Length == 0 && tbxEspSubGrupoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica1.codigo <= '" + tbxEspSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Esp de Sub-Grupo: abaixo do " + tbxEspSubGrupoAte.Text;
            }
            if (tbxEspSubGrupoDe.Text.Length > 0 && tbxEspSubGrupoDe.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica1.codigo  >= '" + tbxEspSubGrupoDe.Text + "' and pecasclassifica1.codigo <= '" + tbxEspSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Esp de Sub-Grupo: do " + tbxEspSubGrupoDe.Text + "  até o " + tbxEspSubGrupoAte.Text;
            }
            if (tbxEspSubGrupoDe.Text.Length == 0 && tbxEspSubGrupoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            // Esp do Esp de Sub-Grupo

            if (tbxEspItemSubGrupoDe.Text.Length > 0 && tbxEspItemSubGrupoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica2.codigo >= '" + tbxEspItemSubGrupoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Esp do Esp de Sub-Grupo: a Partir do " + tbxEspItemSubGrupoDe.Text;
            }
            if (tbxEspItemSubGrupoDe.Text.Length == 0 && tbxEspItemSubGrupoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica2.codigo <= '" + tbxEspItemSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Esp do Esp de Sub-Grupo: abaixo do " + tbxEspSubGrupoAte.Text;
            }
            if (tbxEspItemSubGrupoDe.Text.Length > 0 && tbxEspItemSubGrupoDe.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "pecasclassifica2.codigo  >= '" + tbxEspItemSubGrupoDe.Text + "' and pecasclassifica2.codigo <= '" + tbxEspItemSubGrupoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Esp do Esp de Sub-Grupo: do " + tbxEspItemSubGrupoDe.Text + "  até o " + tbxEspItemSubGrupoAte.Text;
            }
            if (tbxEspItemSubGrupoDe.Text.Length == 0 && tbxEspItemSubGrupoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //ordenação
            if (lbxEspOrdem.SelectedIndex == 0)
            {
                ordem = "cliente.cognome, comprasentrega.dataentrega, compras.numero, pecas.codigo";
            }
            else if (lbxEspOrdem.SelectedIndex == 1)
            {
                ordem = " pecas.codigo, comprasentrega.dataentrega, compras.numero, cliente.cognome";
            }
            else if (lbxEspOrdem.SelectedIndex == 2)
            {
                ordem = "comprasentrega.dataentrega, cliente.cognome,  pecas.codigo";
            }
            else
            {
                ordem = "compras.numero";
            }

            if (rbnEspecificasComSubTotal.Checked)
            {
                cabecalho = cabecalho + Environment.NewLine + "Com Sub-Total";
            }
            else if (rbnEspecificasSemSubtotal.Checked)
            {
                cabecalho = cabecalho + Environment.NewLine + "Sem Sub-Total";
            }

            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxEspOrdem.Text;

            //Parametros           
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
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



            //####################################################//
            DataTable Rel = new DataTable();
            selecionar = "select compras.filial, compras.numero, compras.data, cliente.cognome, cliente.ddd, cliente.telefone as [Telefone] , " +
                        "pecas.codigo, pecas.nome as nome_peca, pecasclassifica.nome as nome_grupo,pecasclassifica.codigo as codigo_grupo,  " +
                        "pecasclassifica1.nome as nome_sub_grupo, pecasclassifica1.codigo as codigo_sub_grupo,pecasclassifica2.nome as nome_item_sub, " +
                        "pecasclassifica2.codigo as codigo_item_sub, compras1.complemento,compras1.complemento1,comprasentrega.qtdeentrega," +
                        "comprasentrega.qtdesaldo, unidade.codigo as codunidade,compras1.preco, comprasentrega.qtdeentrega * compras1.preco as [Total_Mercadoria],  " +
                        "comprasentrega.dataentrega, compras.observa as observar, compras.setor as setor, maqct.nome as centrocusto " +
                        "from comprasentrega " +
                        "left join compras1 on compras1.id = comprasentrega.idcompras1 " +
                        "left join pecas on pecas.id = compras1.idcodigo " +
                        "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica " +
                        "left join pecasclassifica1 on pecasclassifica1.idclassifica = pecasclassifica.id " +
                        "left join pecasclassifica2 on pecasclassifica2.id = pecasclassifica1.idclassifica " +
                        "left join unidade on unidade.id = compras1.idunidfiscal " +
                        "left join compras on compras.id = compras1.idcompras " +
                        "left join maqct on maqct.id = compras1.idcentrocusto " +
                        "left join cliente on cliente.id = compras.idfornecedor ";  

            if (query.Length > 2)
            {
                selecionar = selecionar + " WHERE " + query;
            }
            selecionar = selecionar + " ORDER BY " + ordem;

            DataTable dtTemp = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selecionar, clsInfo.conexaosqldados);
            sda.Fill(dtTemp);
            Rel = dtTemp;
            frmCrystalReport frmCrystalReport = new frmCrystalReport();

            if (rbnEspecificasSemSubtotal.Checked)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_COMPRAS_EMITIDOSPORITEM.RPT",Rel, pfields, "", clsInfo.conexaosqldados);
            }
            else if (rbnEspecificasComSubTotal.Checked)
            {
                if (lbxEspOrdem.SelectedIndex == 0)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_COMPRAS_EMITIDOSPORITEM_COMSUB_FORNECEDOR.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                }
                else if (lbxEspOrdem.SelectedIndex == 1)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_COMPRAS_EMITIDOSPORITEM_COMSUB_CODIGO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                }
                else  if( lbxEspOrdem.SelectedIndex == 2)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_COMPRAS_EMITIDOSPORITEM_COMSUB_DATA.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                }
                else if (lbxEspOrdem.SelectedIndex == 3)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_COMPRAS_EMITIDOSPORITEM_COMSUB_NUMERO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                }
            }
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
        }

        private void tspEspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnEspNumeroPedidoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspNumeroPedidoDe.Name;
            frmComprasPes frmComprasPes = new frmComprasPes();
            frmComprasPes.Init();

        }

        private void btnEspNumeroPedidoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspNumeroPedidoAte.Name;
            frmComprasPes frmComprasPes = new frmComprasPes();
            frmComprasPes.Init();

        }

        private void btnEspFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspFornecedorDe.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Fornecedor", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnEspFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspFornecedorAte.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Fornecedor", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnEspCodDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspCodDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecas", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

        private void btnEspCodAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspCodAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecas", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

        private void btnEspGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspGrupoDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

        private void btnEspGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspGrupoAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

        private void btnEspSubGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspSubGrupoDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica1", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

        private void btnEspSubGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspSubGrupoAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica1", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

        private void btnEspItemSubGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspItemSubGrupoDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica2", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

        private void btnEspItemSubGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspItemSubGrupoAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "pecasclassifica2", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

        }

    }
}
