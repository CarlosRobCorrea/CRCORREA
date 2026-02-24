using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.ReportAppServer.Prompting;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelPecas : Form
    {
        ParameterFields pfields = new ParameterFields();

        Int32 idCliente2De;
        Int32 idCliente2Ate;

        Int32 idPecas2De;
        Int32 idPecas2Ate;

        Int32 idFabricante;
        Int32 idClassifica;
        Int32 idClassifica1;

        String cabecalho;
        String query;
        String where;
        String ordem;
        String selecionar;

        String tipoproduto = "";
        String tipoentrada = "";
        String fabricante = "";

        String ativo = "";

        // tipo 1
        BackgroundWorker bwrFabricante;
        DataTable dtFabricante1;

        // tipo 3
        BackgroundWorker bwrPecasTipo3;
        DataTable dtPecasTipo3;

        // Catalogo
        DataTable dtPecasCatalogo;
        //clsRptCatalogoInfo clsRptCatalogoInfo  = new clsRptCatalogoInfo();
        //clsRptCatalogoBLL clsRptCatalogoBLL = new clsRptCatalogoBLL();

        SqlConnection scn;
        SqlCommand scd;
        SqlDataReader sdr;

        clsRptPecasBLL clsRptPecasBLL = new clsRptPecasBLL();
        clsRptPecasInfo clsRptPecasInfo = new clsRptPecasInfo();

        clsRptPecasAcumulaBLL clsRptPecasAcumulaBLL = new clsRptPecasAcumulaBLL();
        clsRptPecasAcumulaInfo clsRptPecasAcumulaInfo = new clsRptPecasAcumulaInfo();

        Int32 _pecas_guiaatual;
        Int32 pecas_guiaatual
        {
            get { return _pecas_guiaatual; }
            set
            {
                if (value == 0 || value == 1 || value == 2 || value == 3)   // Posíveis posição para o SelectedIndex do TabControl
                {
                    _pecas_guiaatual = value;
                }
                else
                {
                    return;
                }

                //tclRelPecas.SelectedIndex = _pecas_guiaatual;
            }
        }

        public frmRelPecas()
        {
            InitializeComponent();
        }

        public void Init()
        {
            toolStrip1.Enabled = true;
            toolStrip2.Enabled = true;

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select nome from pecas order by nome", tbxDaDescricao);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select nome from pecas order by nome", tbxAteDescricao);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecastipo order by codigo", tbxFabricante);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecastipo order by codigo", tbxFabricante2);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecastipo order by codigo", tbxFabricante3);
        }

        private void frmRelPecas_Load(object sender, EventArgs e)
        {

            lbxTipoProduto.SelectedIndex = lbxTipoProduto.FindString("00");
            if (lbxTipoProduto.SelectedIndex == -1)
            {
                lbxTipoProduto.SelectedIndex = 0;
            }
            if (lbxOrdem1.SelectedIndex == -1)
            {
                lbxOrdem1.SelectedIndex = 0;
            }
            DateTime dtemissao = DateTime.Today.AddMonths(-12);

            tbxDtEmissaoDe.Text = "01/" + dtemissao.Month.ToString().PadLeft(2, '0') + "/" + dtemissao.Year.ToString();

            tbxDtEmissaoAte.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
        }

        private void frmRelPecas_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
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

        private void ControlKeyDownDataHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownDataHora((TextBox)sender, e);
        }

        private void tspImprimir_1_Click(object sender, EventArgs e)
        {
            cabecalho = "Relatorio Simples dos Produtos Cadastrados";
            // Zerar arquivo RPT
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("delete rptpecas", scn);
            scn.Open();
            sdr = scd.ExecuteReader();
            scn.Close();

            // Verificar se vai imprimir de todos os fabricantes
            where = "";
            if (tbxFabricante.Text == "")
            {
                idFabricante = 0;
            }
            else
            {
                idFabricante = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from PECASTIPO where CODIGO ='" + tbxFabricante.Text + "' ").ToString());
                where = "where pecas.idmarca = " + idFabricante;
            }
            // Verificar o tipo de material que deseja ver
            //// Tipo do Produto
            tipoproduto = lbxTipoProduto.Text.Substring(0, 2).Trim();
            if (tipoproduto == "T")
            {
                tipoproduto = "_";
            }
            else
            {
                if (where.Length > 7)
                {
                    where = where + " and pecas.tipoproduto = '" + tipoproduto + "' ";
                }
                else
                {
                    where = " where pecas.tipoproduto = '" + tipoproduto + "' ";
                }
            }
            //// Todos / Ativo / Inativo
            if (rbnAtivo1.Checked == true)
            {
                ativo = "S";
                if (where.Length > 7)
                {
                    where = where + " and pecas.ativo = '" + ativo + "' ";
                }
                else
                {
                    where = " where pecas.ativo = '" + ativo + "' ";
                }

            }
            else if (rbnInativo1.Checked == true)
            {
                ativo = "N";
                if (where.Length > 7)
                {
                    where = where + " and pecas.ativo = '" + ativo + "' ";
                }
                else
                {
                    where = " where pecas.ativo = '" + ativo + "' ";
                }
            }
            else { ativo = ""; }
            //// Filtrar pela descrição do produto
            if (tbxDaDescricao.Text.Trim().Length > 1 && tbxAteDescricao.Text.Trim().Length > 1)
            {
                if (where.Length > 7)
                {
                    where = where + " and pecas.nome >= '" + tbxDaDescricao.Text + "' and pecas.nome <= '" + tbxAteDescricao.Text + "' ";
                }
            }
            if (tbxGrupo1De.Text.Trim().Length > 2)
            {
                if (where.Length > 7)
                {
                    where = where + " and pecas.idclassifica = " + idClassifica + " ";
                }

            }
            if (tbxSubGrupo1De.Text.Trim().Length > 2)
            {
                if (where.Length > 7)
                {
                    where = where + " and pecas.idclassifica1 = " + idClassifica1 + " ";
                }

            }
            if (rbnPecasNCM.Checked == true)
            {
                if (rbnNCMTodos.Checked == true)
                { // Todos os itens com e sem icm
                    
                }
                else if (rbnNCMComNCM.Checked == true)
                { // Todos os itens com e sem icm
                    where = where + " and IPI.CODIGO = '0'" ;
                }
                else
                {
                    where = where + " and IPI.CODIGO <> '0'";
                }


            }

            query = "SELECT PECAS.CODIGO, PECAS.NOME, PECAS.ATIVO, UNIDADE.CODIGO as UNID, PECASCLASSIFICA.CODIGO AS GRUPO, " +
                    "PECASTIPO.CODIGO AS FABRICANTE, PECASCLASSIFICA1.CODIGO AS SUBGRUPO1, PECASCLASSIFICA2.CODIGO AS SUBGRUPO2, " +
                    "PECAS.FOTO, PECAS.PRECOVENDA, PECAS.PRECOCOMPRA, PECAS.QTDESALDO, PECAS.ESTOQUEMIN, IPI.CODIGO AS [CODIGOIPI] " +
                    "FROM  PECAS " +
                    "left join unidade on unidade.id = pecas.idunidade " +
                    "left join pecastipo on pecastipo.id = pecas.idmarca " +
                    "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica " +
                    "left join pecasclassifica1 on pecasclassifica1.id = pecas.idclassifica1 " +
                    "left join pecasclassifica2 on pecasclassifica2.id = pecas.idclassifica2 " +
                    "left join ipi on IPI.id = pecas.idIPI ";

            query = query + where;
            // colocar na ordem
            if (lbxOrdem1.SelectedIndex == 0)
            {
                query = query + " ORDER BY PECAS.NOME, PECAS.CODIGO ";
            }
            else if (lbxOrdem1.SelectedIndex == 1)
            {
                query = query + " ORDER BY PECAS.CODIGO";
            }
            else { query = query + " ORDER BY PECAS.NOME"; }

            // Preparar a Tabela ja Filtrando os dados solicitados
            DataTable dtaux = new DataTable();
            dtaux = Procedure.RetornaDataTable(clsInfo.conexaosqldados, query);
            foreach (DataRow row in dtaux.Rows)
            {


                clsRptPecasBLL = new clsRptPecasBLL();
                clsRptPecasInfo = new clsRptPecasInfo();
                clsRptPecasInfo.codigo = row["CODIGO"].ToString();
                clsRptPecasInfo.nome = row["NOME"].ToString();
                clsRptPecasInfo.ativo = row["ATIVO"].ToString();
                clsRptPecasInfo.unidade = row["UNID"].ToString();
                clsRptPecasInfo.grupo = row["GRUPO"].ToString();
                clsRptPecasInfo.subgrupo1 = row["SUBGRUPO1"].ToString();
                clsRptPecasInfo.subgrupo2 = row["SUBGRUPO2"].ToString();
                clsRptPecasInfo.fabricante = row["FABRICANTE"].ToString();
                clsRptPecasInfo.precocompra = clsParser.DecimalParse(row["PRECOCOMPRA"].ToString());
                clsRptPecasInfo.precovenda = clsParser.DecimalParse(row["PRECOVENDA"].ToString());
                clsRptPecasInfo.qtdesaldo = clsParser.DecimalParse(row["QTDESALDO"].ToString());
                //clsRptPecasInfo.foto = Image.FromFile(row["FOTO"].ToString());
                clsRptPecasInfo.foto = clsParser.DecodificarFotoDatatablerowcell(row["FOTO"]);
                clsRptPecasInfo.estoquemin = clsParser.DecimalParse(row["ESTOQUEMIN"].ToString());
                clsRptPecasInfo.codigoipi = row["CODIGOIPI"].ToString();
                clsRptPecasBLL.Incluir(clsRptPecasInfo, clsInfo.conexaosqldados);
            }


            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = cabecalho;
            field.Name = "CABECALHO";
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Empresa
            valor = new ParameterDiscreteValue();
            field = new ParameterField();
            valor.Value = clsInfo.zempresacliente_cognome;
            field.Name = "EMPRESA";
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Imprimir 
            if (rbnPecasSimples.Checked == true)
            {
                if (rbnComFoto.Checked == false)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_Simples.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_Simples_Foto.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
            }
            else if (rbnPecasPreco.Checked == true)
            {
                if (rbnComFoto.Checked == true)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_Preco.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_Preco.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
            }
            else if (rbnPecasNCM.Checked == true)
            {
                if (rbnComFoto.Checked == true)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_NCM.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                else
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_NCM.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
            }

        }

        private void tspRetornar_1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspImprimir_2_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            query = "";
            ordem = "";
            // tipo da Peças
            query += "PECAS.TIPOPRODUTO  <= 'C' ";

            cabecalho = "Lista de Preços (Ultimo Preço .. Vigente Atualmente)";
            // Cliente

            //datas otas fiscais
            DataTable dtTemp = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selecionar, clsInfo.conexaosqldados);
            sda.Fill(dtTemp);
//            Rel = dtTemp;

            /*Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "PECAS " +
            "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO " +
            "LEFT JOIN CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE " +
            "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO " +
            "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=NFVENDA.IDFORMAPAGTO " +
            "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR " +
            "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID = NFVENDA.IDSUPERVISOR " +
            "LEFT JOIN CLIENTE CLIENTE4 ON CLIENTE4.ID = NFVENDA.IDCOORDENADOR " +
            "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA ",
            "NFVENDA.ID AS IDNFV, NFVENDA.FILIAL AS FILIALNFV, NFVENDA.NUMERO AS NUMERONFV, NFVENDA.DATA AS DATANFV, " +
            "NFVENDA.DATASAIDA AS DATASAIDANFV, DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME, " +
            "NFVENDA.SERIE, NFVENDA.TIPOENTRADA, CLIENTE.COGNOME as CLIENTE, NFVENDA.SITUACAO, " +
            "SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO, SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME, " +
            "CLIENTE1.COGNOME AS VENDEDOR, NFVENDA.TIPOCOMI, NFVENDA.COMISSAO, NFVENDA.TIPOCOMIGER, NFVENDA.COMISSAOGER, " +
            "NFVENDA.MODELO, NFVENDA.FRETE, NFVENDA.FRETEPAGA, NFVENDA.TRANSPORTE, CLIENTE2.COGNOME AS TRANSPORTADORA, " +
            "NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST, NFVENDA.TOTALICMSUBST, " +
            "NFVENDA.TOTALPESO, NFVENDA.TOTALPESOBRUTO, NFVENDA.TOTALMERCADORIA, NFVENDA.TOTALIPI, NFVENDA.TOTALFRETE, " +
            "NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP, NFVENDA.COFINS1, " +
            "NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, NFVENDA.VALORCOMISSAOGER ",
            query, ordem);
            */

            //if (rbnPecasPreco_ListaUltima.Checked == true)
            //{
            //    frmCrystalReport frmCrystalReport;
            //    frmCrystalReport = new frmCrystalReport();
            //    frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_PECASPRECO_CLIENTEULTIMO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

            //    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            //}
            //else
            //{
            //    frmCrystalReport frmCrystalReport;
            //    frmCrystalReport = new frmCrystalReport();
            //    frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_PECAS_BASE_ICMS_MP.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
            //    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            //}
        }

        private void tspRetornar_2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspRetornar_3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Inicio - PecasTipo2
        // Inicio - PecasTipo3
        private void bwrPecasTipo3_DoWork(object sender, DoWorkEventArgs e)
        {
            dtPecasTipo3 = clsPecasTipoBLL.GridDados(clsInfo.conexaosqldados);
            dtPecasTipo3.Rows.Add(dtPecasTipo3.NewRow());
            dtPecasTipo3.Rows[dtPecasTipo3.Rows.Count - 1]["CODIGO"] = "  ";
            dtPecasTipo3.Rows[dtPecasTipo3.Rows.Count - 1]["NOME"] = "TODOS";
            dtPecasTipo3.DefaultView.Sort = "CODIGO ASC";
            dtPecasTipo3 = dtPecasTipo3.DefaultView.ToTable();
        }

        private void rbnPecasSimples_Click(object sender, EventArgs e)
        {
            tclRelPecas.SelectedIndex = 0;
            //tbxCliente1De.Select();
        }

        private void rbnPecasPrecos_Click(object sender, EventArgs e)
        {
            tclRelPecas.SelectedIndex = 2;
            lbxTipoProduto2.SelectedIndex = 2;

        }

        private void rbnPecasEstoque_Click(object sender, EventArgs e)
        {
            tclRelPecas.SelectedIndex = 1;
            lbxTipoProduto2.SelectedIndex = 1;
            rbnAtivo2.Select();
        }

        private void tspRetornar_0_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspImprimir_0_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            selecionar = "";
            query = "";
            ordem = "";
            tipoproduto = "";
            tipoentrada = "";
            // Cabeçalho  -- objetivo da lista
            if (rbnPecasSimples.Checked == true)
            {
                cabecalho = "Relatorio Produtos/Peças/Materiais (Simples sem Cliente)";
            }

            else
            {
                MessageBox.Show("Opção desabilitada por enquanto");
            }
            if (tipoproduto != "")
            {
                query += "PECAS.TIPOPRODUTO = '" + tipoproduto + "'";
            }
            // Tipo da Entrada
            tipoentrada = lbxTipoProduto.Text.Substring(0, 2).Trim();
            if (tipoentrada != "")
            {
                if (query.Length > 0) { query += " AND "; }

                query += "PECAS.TIPOENTRADA = '" + tipoentrada + "'";
            }
            // Cliente
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            //valor.Value = id;
            //field.Name = "id";
            //field.CurrentValues.Add(valor);
            //parameters.Add(field);

            DialogResult resultado1;
            resultado1 = MessageBox.Show("Deseja Imprimir Relatorio de Peças Simples ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (resultado1 == DialogResult.Yes)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "PECAS_SIMPLES.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }



        }

        private void tspImprimirEstoque_Click(object sender, EventArgs e)
        {
            cabecalho = "Relatorio Estoque dos Produtos Cadastrados";
            // Zerar arquivo RPT
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("delete rptpecas", scn);
            scn.Open();
            sdr = scd.ExecuteReader();
            scn.Close();

            // Verificar se vai imprimir de todos os fabricantes
            where = "";
            if (tbxFabricante2.Text == "")
            {
                idFabricante = 0;
            }
            else
            {
                idFabricante = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from PECASTIPO where CODIGO ='" + tbxFabricante2.Text + "' ").ToString());
                where = "where pecas.idmarca = " + idFabricante;
            }
            // Verificar o tipo de material que deseja ver
            //// Tipo do Produto
            tipoproduto = lbxTipoProduto2.Text.Substring(0, 2).Trim();
            if (tipoproduto == "T")
            {
                tipoproduto = "_";
            }
            else
            {
                if (where.Length > 7)
                {
                    where = where + " and pecas.tipoproduto = '" + tipoproduto + "' ";
                }
                else
                {
                    where = " where pecas.tipoproduto = '" + tipoproduto + "' ";
                }
            }
            //// Todos / Ativo / Inativo
            if (rbnAtivo2.Checked == true)
            {
                ativo = "S";
                if (where.Length > 7)
                {
                    where = where + " and pecas.ativo = '" + ativo + "' ";
                }
                else
                {
                    where = " where pecas.ativo = '" + ativo + "' ";
                }

            }
            else if (rbnInativo2.Checked == true)
            {
                ativo = "N";
                if (where.Length > 7)
                {
                    where = where + " and pecas.ativo = '" + ativo + "' ";
                }
                else
                {
                    where = " where pecas.ativo = '" + ativo + "' ";
                }
            }
            else { ativo = ""; }
            //// Apenas com Saldo Negativo
            if (rbnSemSaldo.Checked == true)
            {
                if (where.Length > 7)
                {
                    where = where + " and pecas.qtdesaldo <  0 ";
                }
            }
            //if (tbxGrupo1De.Text.Trim().Length > 2)
            //{
            //    if (where.Length > 7)
            //    {
            //        where = where + " and pecas.idclassifica = " + idClassifica + " ";
            //    }

            //}
            query = "SELECT PECAS.CODIGO, PECAS.NOME, PECAS.ATIVO, UNIDADE.CODIGO as UNID, PECASCLASSIFICA.CODIGO AS GRUPO, " +
                    "PECASTIPO.CODIGO AS FABRICANTE, PECASCLASSIFICA1.CODIGO AS SUBGRUPO1, PECASCLASSIFICA2.CODIGO AS SUBGRUPO2, " +
                    "PECAS.FOTO, PECAS.PRECOVENDA, PECAS.PRECOCOMPRA, PECAS.QTDESALDO, PECAS.ESTOQUEMIN " +
                    "FROM  PECAS " +
                    "left join unidade on unidade.id = pecas.idunidade " +
                    "left join pecastipo on pecastipo.id = pecas.idmarca " +
                    "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica " +
                    "left join pecasclassifica1 on pecasclassifica1.id = pecas.idclassifica1 " +
                    "left join pecasclassifica2 on pecasclassifica2.id = pecas.idclassifica2 ";

            query = query + where;
            // colocar na ordem
            if (lbxOrdem1.SelectedIndex == 0)
            {
                query = query + " ORDER BY PECAS.NOME, PECAS.CODIGO ";
            }
            else if (lbxOrdem1.SelectedIndex == 1)
            {
                query = query + " ORDER BY PECAS.CODIGO";
            }
            else { query = query + " ORDER BY PECAS.NOME"; }

            // Preparar a Tabela ja Filtrando os dados solicitados
            DataTable dtaux = new DataTable();
            dtaux = Procedure.RetornaDataTable(clsInfo.conexaosqldados, query);
            foreach (DataRow row in dtaux.Rows)
            {
                clsRptPecasBLL = new clsRptPecasBLL();
                clsRptPecasInfo = new clsRptPecasInfo();
                clsRptPecasInfo.codigo = row["CODIGO"].ToString();
                clsRptPecasInfo.nome = row["NOME"].ToString();
                clsRptPecasInfo.ativo = row["ATIVO"].ToString();
                clsRptPecasInfo.unidade = row["UNID"].ToString();
                clsRptPecasInfo.grupo = row["GRUPO"].ToString();
                clsRptPecasInfo.subgrupo1 = row["SUBGRUPO1"].ToString();
                clsRptPecasInfo.subgrupo2 = row["SUBGRUPO2"].ToString();
                clsRptPecasInfo.fabricante = row["FABRICANTE"].ToString();
                clsRptPecasInfo.precocompra = clsParser.DecimalParse(row["PRECOCOMPRA"].ToString());
                clsRptPecasInfo.precovenda = clsParser.DecimalParse(row["PRECOVENDA"].ToString());
                if (clsRptPecasInfo.precocompra == 0 && clsRptPecasInfo.precovenda >0)
                {
                    clsRptPecasInfo.precocompra = ((clsRptPecasInfo.precovenda * 70) / 100);
                }

                clsRptPecasInfo.qtdesaldo = clsParser.DecimalParse(row["QTDESALDO"].ToString());
                //clsRptPecasInfo.foto = Image.FromFile(row["FOTO"].ToString());
                clsRptPecasInfo.foto = clsParser.DecodificarFotoDatatablerowcell(row["FOTO"]);
                clsRptPecasInfo.estoquemin = clsParser.DecimalParse(row["ESTOQUEMIN"].ToString());
                clsRptPecasBLL.Incluir(clsRptPecasInfo, clsInfo.conexaosqldados);
            }


            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = cabecalho;
            field.Name = "CABECALHO";
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Empresa
            valor = new ParameterDiscreteValue();
            field = new ParameterField();
            valor.Value = clsInfo.zempresacliente_cognome;
            field.Name = "EMPRESA";
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Imprimir 
            if (rbnComFoto2.Checked == true)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_Estoque_Simples_Foto.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_Estoque_Simples.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
        }

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                 clsInfo.zrow.Index != -1 &&
                 clsInfo.znomegrid != null &&
                 clsInfo.znomegrid != "")
            {
                //Verifica os Botões
                //cliente2 de
                if (clsInfo.znomegrid == btnCatClientede.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idCliente2De = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxFabricante3.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID =" + idCliente2De, "").ToString();
                    }
                    tspApuracao.Select();
                }
                if (clsInfo.znomegrid == btnIdClassifica1De.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idClassifica = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxGrupo1De.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECASCLASSIFICA where ID =" + idClassifica, "").ToString();
                    }
                    tspApuracao.Select();
                }
                if (clsInfo.znomegrid == btnIdClassifica11De.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idClassifica1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxSubGrupo1De.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA1 where id = " + idClassifica1);
                    }
                    tspApuracao.Select();
                }

            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################

                //Cliente
            }
            tbxDaDescricao.Text = clsVisual.RemoveAcentos(tbxDaDescricao.Text);
            tbxAteDescricao.Text = clsVisual.RemoveAcentos(tbxAteDescricao.Text);

            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private void tspApuracao_Click(object sender, EventArgs e)
        {
            String query;
            SqlConnection scn;
            SqlCommand scd;

            cabecalho = "Relatorio Produtos Mais Vendidos de:" + tbxDtEmissaoDe.Text +" ate " + tbxDtEmissaoAte.Text;
            // Zerar arquivo RPT
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("delete rptpecasacumula", scn);
            scn.Open();
            sdr = scd.ExecuteReader();
            scn.Close();

            where = where + " and pecas.tipoproduto = '0' ";
            //// Tipo da Lista
            if (rbnApura.Checked == true)
            {  // Mais Vendidos
                query = "SELECT pedido1.IDCODIGO, " +
                        "sum(PEDIDO1.qtde) as qtde, " +
                        "sum(PEDIDO1.TOTALCUSTOITEM) as totalcusto, " +
                        "sum(PEDIDO1.TOTALNOTA) as totalnota " +
                        "FROM  PEDIDO1 " +
                        "left join pedido on pedido.id = pedido1.IDPEDIDO " +
                        "WHERE " +
                        "pedido.data >= " + clsParser.SqlDateTimeFormat(tbxDtEmissaoDe.Text + " 00:00", true) +
                        "AND pedido.data <= " + clsParser.SqlDateTimeFormat(tbxDtEmissaoAte.Text + " 23:59", true) +
                        "GROUP BY pedido1.IDCODIGO " +
                        "ORDER BY pedido1.IDCODIGO ";

                // Preparar a Tabela ja Filtrando os dados solicitados
                DataTable dtaux = new DataTable();
                dtaux = Procedure.RetornaDataTable(clsInfo.conexaosqldados, query);

                foreach (DataRow row in dtaux.Rows)
                {
                    clsRptPecasAcumulaBLL = new clsRptPecasAcumulaBLL();
                    clsRptPecasAcumulaInfo = new clsRptPecasAcumulaInfo();
                    clsRptPecasAcumulaInfo.codigo = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS where id =" + clsParser.Int32Parse(row["idCODIGO"].ToString()) + " ").ToString(); 
                    clsRptPecasAcumulaInfo.nome = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id =" + clsParser.Int32Parse(row["idCODIGO"].ToString()) + " ").ToString(); 
                    clsRptPecasAcumulaInfo.participacao = 0;
                    clsRptPecasAcumulaInfo.qtde = clsParser.DecimalParse(row["qtde"].ToString());
                    clsRptPecasAcumulaInfo.valorcusto = clsParser.DecimalParse(row["totalcusto"].ToString());
                    clsRptPecasAcumulaInfo.valorvenda = clsParser.DecimalParse(row["totalnota"].ToString());
                    clsRptPecasAcumulaInfo.valorapurado = clsRptPecasAcumulaInfo.valorvenda - clsRptPecasAcumulaInfo.valorcusto;
                    if (clsRptPecasAcumulaInfo.valorcusto == 0)
                    {
                        clsRptPecasAcumulaInfo.valorcusto = ((clsRptPecasAcumulaInfo.valorvenda * 70) / 100);
                    }



                    clsRptPecasAcumulaBLL.Incluir(clsRptPecasAcumulaInfo, clsInfo.conexaosqldados);
                }

                frmCrystalReport frmCrystalReport;
                frmCrystalReport = new frmCrystalReport();
                ParameterFields parameters = new ParameterFields();
                // Cabeçalho
                ParameterDiscreteValue valor = new ParameterDiscreteValue();
                ParameterField field = new ParameterField();
                valor.Value = cabecalho;
                field.Name = "CABECALHO";
                field.CurrentValues.Add(valor);
                parameters.Add(field);
                // Empresa
                valor = new ParameterDiscreteValue();
                field = new ParameterField();
                valor.Value = clsInfo.zempresacliente_cognome;
                field.Name = "EMPRESA";
                field.CurrentValues.Add(valor);
                parameters.Add(field);
                // Imprimir 
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "Pecas_Apuracao.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
        }

        private void tspRetornar3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCatClientede_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCatClientede.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idCliente2De);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnCatClienteate_Click(object sender, EventArgs e)
        {

        }

        private void btnIdCliente3De_Click(object sender, EventArgs e)
        {

        }

        private void lbxTipoEntrada1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gbxRelPecas_1_Enter(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void btnIdClassifica1De_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdClassifica1De.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA", idClassifica, "Grupo");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnIdClassifica11De_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdClassifica11De.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA1", idClassifica1, "IDCLASSIFICA", idClassifica.ToString());
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdClassifica21De_Click(object sender, EventArgs e)
        {

        }

        private void gbxRelPecas_3_Enter(object sender, EventArgs e)
        {

        }

        private void lbxTipoProduto2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbnSemSaldo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbxFabricante3_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbnPecasPreco_Click(object sender, EventArgs e)
        {
            tclRelPecas.SelectedIndex = 0;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnPecasNCM_Click(object sender, EventArgs e)
        {
            tclRelPecas.SelectedIndex = 0;
            gbxNCM.Visible = true;

        }
    }
}
