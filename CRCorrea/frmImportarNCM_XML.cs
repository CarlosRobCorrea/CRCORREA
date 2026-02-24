using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRCorrea
{
    public partial class frmImportarNCM_XML : Form
    {
        public static Int32 id = 0;
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;


        // NFCompra1

        static DataTable dtXmlNFCompra;
        static DataTable dtXmlNFCompra1;

        clsXmlNFCompraInfo clsXmlNFCompraInfo = new clsXmlNFCompraInfo();
        clsXmlNFCompra1Info clsXmlNFCompra1Info = new clsXmlNFCompra1Info();
        clsXmlNFCompraBLL clsXmlNFCompraBLL = new clsXmlNFCompraBLL();
        clsXmlNFCompra1BLL clsXmlNfCompra1BLL = new clsXmlNFCompra1BLL();


        clsNfCompraInfo clsNfCompraInfo = new clsNfCompraInfo();
        clsNfCompra1Info clsNfCompra1Info = new clsNfCompra1Info();
        clsNfCompraBLL clsNfCompraBLL = new clsNfCompraBLL();
        clsNfCompra1BLL clsNfCompra1BLL = new clsNfCompra1BLL();
        clsNfCompraPagarInfo clsNfCompraPagarInfo = new clsNfCompraPagarInfo(); 
        clsNfCompraPagarBLL clsNfCompraPagarBLL = new clsNfCompraPagarBLL();

        static DataTable dtNFCompra;
        static DataTable dtNFCompra1;



        clsClienteBLL clsClienteBLL;
        clsClienteInfo clsClienteInfo;

        Int32 id_Codigo;
        Int32 id_fornecedor;
        Int32 id_Grupo;
        Int32 id_Marca;
        Int32 id_Unidade;
        Int32 id_Ipi;
        String NCM;
        Int32 Status;

        Int32 idXmlNFCompra = 0;
        Int32 idXmlNFCompra1 = 0;

        Boolean carregandoXmlNFCompra = false;

        clsPecasBLL clsPecasBLL = new clsPecasBLL();
        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasPrecoInfo clsPecasPrecoInfo = new clsPecasPrecoInfo();
        clsPecasPrecoBLL clsPecasPrecoBLL = new clsPecasPrecoBLL();

        public frmImportarNCM_XML()
        {
            InitializeComponent();
        }
        public void Init()
        {
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "PECASCLASSIFICA", "CODIGO", "", tbxPecasClassifica);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "PECASTIPO", "CODIGO", "", tbxMarca);

        }

        private void frmNFCompraXMLVis_Load(object sender, EventArgs e)
        {

        }

        private void btnLerXML_Click(object sender, EventArgs e)
        {
            LerXML();
        }

        private void dadosOrigemProcurar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Procurar...";
            ofd.Filter = "Arquivos Textos |*.xml";

            ofd.ShowDialog();

            tbxArquivoOrigem.Text = ofd.FileName;
            if ((tbxArquivoOrigem.Text).Length > 0)
            {
                btnLerXML.Enabled = true;
            }
            else
            {
                btnLerXML.Enabled = false;
            }
        }
        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LerXML()
        {
            var arquivo = tbxArquivoOrigem.Text.Trim();
            var item = "";
            var cProd = "";
            var cProdBarra = "";
            var cProdFornecedor = "";
            var xProd = "";
            var qCom = "";
            var qUnid = "";
            var vUnCom = "";
            var vProd = "";
            var vIPI = "";
            var vICMS = "";
            var cNCM = "";
            var branco = "";

            using (XmlReader meuXml = XmlReader.Create(arquivo))
            {
                var fimitens = false;
                var fornecedor = false;

                while (meuXml.Read())
                {
                    if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "natOp")
                    {
                        tbxNatOper.Text = meuXml.ReadElementContentAsString();
                    }
                    if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "nNF")
                    {
                        tbxNotaFiscal.Text = meuXml.ReadElementContentAsString();
                    }
                    if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "dhEmi")
                    {
                        String Data = meuXml.ReadElementContentAsString();
                        tbxDataEmissao.Text = Data.Substring(8, 2) + "/" +
                        Data.Substring(5, 2) + "/" +
                        Data.Substring(0, 4);
                    }
                    if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "emit")
                    {
                        fornecedor = true;
                    }
                    else if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "dest")
                    {
                        fornecedor = false;
                    }
                    if (fornecedor == true)
                    {
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "cMun")
                        {
                            tbxIBGE.Text = meuXml.ReadElementContentAsString();
                        }

                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "CNPJ")
                        {
                            tbxCNPJ.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "xNome")
                        {
                            tbxCliente.Text = meuXml.ReadElementContentAsString();
                        }

                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "xLgr")
                        {
                            tbxEndereco.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "nro")
                        {
                            tbxNroEnd.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "xBairro")
                        {
                            tbxBairro.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "xMun")
                        {
                            tbxCidade.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "UF")
                        {
                            tbxUF.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "CEP")
                        {
                            tbxCEP.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "fone")
                        {
                            tbxTelefone.Text = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "IE")
                        {
                            tbxIEstadual.Text = meuXml.ReadElementContentAsString();
                        }
                    }


                    //Itens da Nota Fiscal
                    if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "det")
                    {
                        item = meuXml.GetAttribute("nItem");
                        cProd = "";
                        cProdBarra = "";
                        cProdFornecedor = "";
                        xProd = "";
                        qCom = "";
                        qUnid = "";
                        vUnCom = "";
                        vProd = "";
                        vIPI = "";
                        vICMS = "";
                        cNCM = "";
                        branco = "";

                    }
                    else if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "total")
                    {
                        fimitens = true;
                    }
                    if (!fimitens)
                    {
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "cProd")
                        {
                            cProd = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "cEAN")
                        {
                            cProdBarra = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "xProd")
                        {
                            xProd = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "NCM")
                        {
                            cNCM = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "CEST")
                        {
                            branco = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "CFOP")
                        {
                            branco = meuXml.ReadElementContentAsString();
                        }

                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "uCom")
                        {
                            qUnid = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "qCom")
                        {
                            qCom = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "vUnCom")
                        {
                            vUnCom = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "vProd")
                        {
                            vProd = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "vICMSST")
                        {
                            vICMS = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "vIPI")
                        {
                            vIPI = meuXml.ReadElementContentAsString();
                        }
                        if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "vCOFINS")
                        {
                            // Fornecedor que tem Codigo ESPECIAL PARA SEUS ITENS
                            if (cProd.Length < cProdBarra.Length)
                            {
                                cProdFornecedor = cProd;
                                cProd = cProdBarra;
                            }

                            listView1.Items.Add(new ListViewItem(new[] { item, cProd, cProdBarra, cProdFornecedor, xProd, qCom, qUnid, vUnCom, vProd, vICMS, vIPI, cNCM }));
                        }
                    }


                    if (meuXml.NodeType == XmlNodeType.Element && meuXml.Name == "vNF")
                    {
                        tbxValorTotalNF.Text = meuXml.ReadElementContentAsString();
                        tbxValorTotalNF.Text = (clsParser.DecimalParse(tbxValorTotalNF.Text) / 100).ToString("N2");
                        if (clsParser.DecimalParse(tbxValorTotalNF.Text) > 0)
                        {
                            tspAlterar.Enabled = true;
                        }

                    }
                }
                //try
                //{
                //    meuxml.MoveToContent();
                //    XmlSerializer ser = new XmlSerializer(typeof(nfeProc));
                //    nfeProc nota = (nfeProc)ser.Deserialize(meuxml);
                //    MessageBox.Show("XML lido com sucesso!");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Erro ao ler o XML: " + ex.Message);
                //}
            }
            // Verificar se ja existe o Fornecedor
            id_fornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cgc='" + tbxCNPJ.Text.Trim() + "'", "0"));
            if (id_fornecedor > 0)
            {
                // Se existe este fornecedor verificar se a nota fiscal esta cadastrada no xml
                clsXmlNFCompraInfo.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from XmlNFCompra where numero=" + clsParser.Int32Parse(tbxNotaFiscal.Text) + " and idfornecedor= " + id_fornecedor + "", "0"));
                if (clsXmlNFCompraInfo.id == 0)
                {
                    // Se existe a nota fiscal cadastrada no xml
                    if (clsXmlNFCompra1Info.id > 0)
                    {
                        tspAlterar.Enabled = false;
                        btnDadosOrigemProcurar.Enabled = false;
                        // Se existe a nota fiscal cadastrada no cadastro principal
                        Int32 NotaOficial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from NFCompra where numero=" + clsParser.Int32Parse(tbxNotaFiscal.Text) + " and idfornecedor= " + id_fornecedor + "", "0"));
                        /*
                        if (NotaOficial > 0)
                        {
                            // Se existe a nota fiscal cadastrada no cadastro principal
                            MessageBox.Show("Nota Fiscal já cadastrada no sistema.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tspAlterar.Enabled = false;
                            btnDadosOrigemProcurar.Enabled = false;
                        }
                        */
                    }
                    else
                    {
                        tspAlterar.Enabled = true;
                        btnDadosOrigemProcurar.Enabled = true;
                        // Se existe a nota fiscal cadastrada no cadastro principal
                        Int32 NotaOficial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from NFCompra where numero=" + clsParser.Int32Parse(tbxNotaFiscal.Text) + " and idfornecedor= " + id_fornecedor + "", "0"));
                        /*
                        if (NotaOficial > 0)
                        {
                            // Se existe a nota fiscal cadastrada no cadastro principal
                            MessageBox.Show("Nota Fiscal já cadastrada no sistema. " + Environment.NewLine + "   Não pode repetir a inclusão  ", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tspAlterar.Enabled = false;
                            btnDadosOrigemProcurar.Enabled = false;
                        }
                        */
                    }
                }
                else
                {
                    tspAlterar.Enabled = false;
                    MessageBox.Show("Nota Fiscal em Cadastramento via XML");
                }



            }
        }

        private void tspTransferir_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Brush bgBrusch; // cor do fundo
                Brush foreColorBrusch; // cor do texto
                Font Font;
                if (e.Index == tabControl1.SelectedIndex)
                {
                    //mudar aparencia do tabcontrol selecionado
                    Font = new Font(e.Font, FontStyle.Bold | FontStyle.Italic);
                    bgBrusch = new System.Drawing.SolidBrush(Color.Yellow);
                    foreColorBrusch = Brushes.Red;

                }
                else
                {
                    //mudar aparencia do tabcontrol selecionado
                    Font = new Font(e.Font, FontStyle.Regular);
                    bgBrusch = new SolidBrush(Color.FromArgb(200, 252, 255));
                    foreColorBrusch = new SolidBrush(Color.Transparent);

                }
                //Alinhamento do Texto
                var tabName = tabControl1.TabPages[e.Index].Text;
                StringFormat sf = new StringFormat();

                // preencher tab selecionado
                e.Graphics.FillRectangle(bgBrusch, e.Bounds);

                Rectangle r = e.Bounds;
                r = new Rectangle(r.X, r.Y + 3, r.Width + 3, r.Height - 3);

                e.Graphics.DrawString(tabName, Font, foreColorBrusch, r, sf);

                sf.Dispose();
                if (e.Index == tabControl1.SelectedIndex)
                {
                    bgBrusch.Dispose();
                }
                else
                {
                    bgBrusch.Dispose();
                    foreColorBrusch.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void frmNFCompraXMLVis_Activated(object sender, EventArgs e)
        {
            bwrXmlNFCompra_Run();

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
        void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
               clsInfo.zrow.Index != -1 &&
               clsInfo.znomegrid != null &&
               clsInfo.znomegrid != "")
            {
                if (clsInfo.znomegrid == btnCodigo.Name)
                {
                    id_Codigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    // Procurar dados do cadastro e atualizar
                    id_Unidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idunidade from pecas where id = " + id_Codigo + "  ", "0"));
                    tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id='" + id_Unidade + "' ", "0");
                    id_Grupo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idclassifica from pecas where id = " + id_Codigo + "  ", "0"));
                    tbxPecasClassifica.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecasclassifica where id='" + id_Grupo + "' ", "0");
                    id_Marca = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idmarca from pecas where id = " + id_Codigo + "", "0"));
                    tbxMarca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecastipo where id='" + id_Marca + "' ", "0");
                    tbxDescricaoCadastro.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id = " + id_Codigo + " ", "0");
                    tbxPrecoVendaAnterior.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoraprazo from pecaspreco where idcodigo = " + id_Codigo + " order by data desc ", "0").ToString()).ToString("N2");
                    tbxFatorConv.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select fatorconv from pecas where id = " + id_Codigo + " ", "0").ToString()).ToString("N3");
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdUnidadeCom.Name)
                {
                    id_Unidade = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxUnidade.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxUnidade.Select();
                    tbxUnidade.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdPecasClassifica.Name)
                {
                    id_Grupo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPecasClassifica.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxPecasClassifica.Select();
                    tbxPecasClassifica.SelectAll();
                }
                else if (clsInfo.znomegrid == btnMarca.Name)
                {
                    id_Marca = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxMarca.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxMarca.Select();
                    tbxMarca.SelectAll();
                }
            }
            if (ctl.Name == tbxCodigo.Name)
            {
                id_Codigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo = '" + tbxCodigo.Text + "'  ", "0"));
                if(id_Codigo > 0)
                {
                    // Procurar dados do cadastro e atualizar
                    tbxCodigoBarra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigobarra from pecas where id = " + id_Codigo + "  ", "0");
                    id_Unidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idunidade from pecas where id = " + id_Codigo + "  ", "0"));
                    tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id='" + id_Unidade + "' ", "0");
                    id_Grupo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idclassifica from pecas where id = " + id_Codigo + "  ", "0"));
                    tbxPecasClassifica.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecasclassifica where id='" + id_Grupo + "' ", "0");
                    id_Marca = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idmarca from pecas where id = " + id_Codigo + "", "0"));
                    tbxMarca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecastipo where id='" + id_Marca + "' ", "0");
                    tbxDescricaoCadastro.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id = " + id_Codigo + " ", "0");
                    tbxPrecoVendaAnterior.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoraprazo from pecaspreco where idcodigo = " + id_Codigo + " order by data desc ", "0").ToString()).ToString("N2");
                    tbxFatorConv.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select fatorconv from pecas where id = " + id_Codigo + " ", "0").ToString()).ToString("N3");
                    if (clsParser.DecimalParse(tbxPrecoTabela.Text) == 0)
                    {
                        tbxPrecoTabela.Text = clsParser.DecimalParse(tbxPrecoVendaAnterior.Text).ToString("N2");
                    }

                    tbxCodigoBarra.Select();
                    tbxCodigoBarra.SelectAll();

                }
            }
            else if (ctl.Name == tbxUnidade.Name)
            {
                id_Unidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from unidade where codigo='" + tbxUnidade.Text + "' ", "0"));
                if (id_Unidade == 0)
                {
                    tbxUnidade.Text = "";
                }
            }
            else if (ctl.Name == tbxPecasClassifica.Name)
            {
                id_Grupo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecasclassifica where codigo='" + tbxPecasClassifica.Text + "' ", "0"));
                if (id_Grupo == 0)
                {
                    tbxPecasClassifica.Text = "";
                }
            }
            else if (ctl.Name == tbxMarca.Name)
            {
                id_Marca = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecastipo where codigo='" + tbxMarca.Text + "' ", "0"));
                if (id_Marca == 0)
                {
                    id_Marca = clsInfo.zmarca;
                    tbxMarca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from pecastipo where ID=" + clsInfo.zmarca + " ", "");
                }
            }

            tbxValorTotalNF.Text = clsParser.DecimalParse(tbxValorTotalNF.Text).ToString("N2");
            if (clsParser.DecimalParse(tbxValorTotalNF.Text) > 0)
            {
                tspAlterar.Enabled = true;
            } 
            tbxTotalMercado.Text = (clsParser.DecimalParse(tbxQtde.Text) *clsParser.DecimalParse(tbxPreco.Text)).ToString("N2");

            if (clsParser.DecimalParse(tbxTotalMercado.Text) != clsParser.DecimalParse(tbxTotalNFItem.Text))
            {
                tbxTotalMercado.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                tbxTotalMercado.BackColor = System.Drawing.Color.White;
            }
            tbxPrecoTabela.Text = clsParser.DecimalParse(tbxPrecoTabela.Text).ToString("N2");
            if (clsParser.DecimalParse(tbxPrecoTabela.Text) == 0)
            {
                tbxPrecoTabela.BackColor = System.Drawing.Color.Red;
                tbxFator.Text = "";
            }
            else
            {
                tbxPrecoTabela.BackColor = System.Drawing.Color.White;
                if (clsParser.DecimalParse(tbxPreco.Text) > 0)
                {
                    tbxFator.Text = (clsParser.DecimalParse(tbxPrecoTabela.Text) / clsParser.DecimalParse(tbxPreco.Text)).ToString("N4");
                }
                else
                {
                    tbxFator.Text = "";
                }
            }
            tbxFator.Text = clsParser.DecimalParse(tbxFator.Text).ToString("N3");


            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
            // Verificar o Status do Item
            Status = 5;
            if (id_Codigo > 0) { Status -= 1;
                tbxCodigo.BackColor = System.Drawing.Color.Green;
                labelCadastro.Text = "Material já Cadastrado";
                labelCadastro.BackColor = System.Drawing.Color.Green; } else {
                tbxCodigo.BackColor = System.Drawing.Color.Red;
                labelCadastro.Text = "Precisa Cadastrar Material";
                labelCadastro.BackColor = System.Drawing.Color.Red; }

            if (id_Unidade > 0) { Status -= 1; 
                                  tbxUnidade.BackColor = System.Drawing.Color.White; } else {
                                  tbxUnidade.BackColor = System.Drawing.Color.Red; }
        
            if (id_Marca > 0) { Status -= 1;
                                  tbxMarca.BackColor = System.Drawing.Color.White; } else {
                                  tbxMarca.BackColor = System.Drawing.Color.Red; }

           if (id_Grupo > 0) { Status -= 1; 
                                  tbxPecasClassifica.BackColor = System.Drawing.Color.White; } else {
                                  tbxPecasClassifica.BackColor = System.Drawing.Color.Red; }

            if (clsParser.DecimalParse(tbxPrecoTabela.Text) > 0) { Status -= 1; }
            if (clsParser.DecimalParse(tbxTotalMercado.Text) != clsParser.DecimalParse(tbxTotalNFItem.Text))
               { Status += 1; }

            tbxStatus.Text = Status.ToString(); 


        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            tbxNotaFiscal1.Text = tbxNotaFiscal.Text.Trim();
            tbxDataEmissao1.Text = tbxDataEmissao.Text.Trim();
            tbxCliente1.Text = tbxCliente.Text.Trim();
            // Verificar se ja existe o Fornecedor
            clsXmlNFCompraInfo clsXmlNFCompraInfo = new clsXmlNFCompraInfo();
            clsXmlNFCompraInfo.id = 0;
            clsXmlNFCompraInfo.numero = clsParser.Int32Parse(tbxNotaFiscal.Text);
            clsXmlNFCompraInfo.data = clsParser.DateTimeParse(tbxDataEmissao.Text);
            clsXmlNFCompraInfo.status = 0; // 0=sem montagem  1=iniciado montagem 2=montagem concluída
            clsXmlNFCompraInfo.idfornecedor = 0; // Fornecedor ainda não cadastrado
            if (tbxCliente.Text.Trim().Length >19)
            {
                clsXmlNFCompraInfo.fornecedor = tbxCliente.Text.Trim().Substring(0, 19);// nome do fornecedor
            }
            else
            {
                clsXmlNFCompraInfo.fornecedor = tbxCliente.Text.Trim(); // nome do fornecedor

            }
            

            id_fornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where cgc='" + tbxCNPJ.Text.Trim() + "'", "0"));
            if (id_fornecedor > 0)
            {
                clsXmlNFCompraInfo.idfornecedor = id_fornecedor; // Fornecedor ja cadastrado
                lbxFornecedor.Text = "Fornecedor ja Cadastrado";
                //Apenas Pegar os dados do fornecedor
            }
            else
            {
                lbxFornecedor.Text = "Fornecedor não Cadastrado. \nCadastro será feito Automaticamente";
                // Cadastrar o Fornecedor
                clsClienteInfo clsClienteInfo = new clsClienteInfo();
                clsClienteInfo.id = 0;
                clsClienteInfo.idaprovadopor = 0;
                clsClienteInfo.idbanco = 0;
                clsClienteInfo.idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from estados where estado='" + tbxUF.Text + "' ", "0")); ;
                clsClienteInfo.idcnae = 0;
                clsClienteInfo.idcondpagto = clsInfo.zcondpagto;
                clsClienteInfo.idcoordenador = clsInfo.zempresaclienteid;

                clsClienteInfo.idformapagto = clsInfo.zformapagto;
                clsClienteInfo.idramo = clsInfo.zramo;
                clsClienteInfo.idregimeapuracao = clsInfo.zregimeapuracao;
                clsClienteInfo.idsupervisor = 0;
                clsClienteInfo.idtransportadora = clsInfo.zempresaclienteid;
                clsClienteInfo.idredespacho = 0;
                clsClienteInfo.idzona = clsInfo.zzona;
                clsClienteInfo.idvendedor = 0;
                clsClienteInfo.iddizeresnota = 0;

                clsClienteInfo.numero = 0;

                clsClienteInfo.cognome = tbxCliente.Text.Substring(0,19);
                clsClienteInfo.nome = tbxCliente.Text.Trim();
                clsClienteInfo.datanasc = clsParser.DateTimeParse("01/01/1900");
                int x = tbxTelefone.Text.Trim().Length;
                clsClienteInfo.ddd = tbxTelefone.Text.Trim().Substring(0, 2);
                clsClienteInfo.telefone = tbxTelefone.Text.Trim().Substring(2, (x - 2));
                clsClienteInfo.contato = "";
                clsClienteInfo.tipo = "F";
                clsClienteInfo.pessoa = "J";
                clsClienteInfo.ativo = "S";
                clsClienteInfo.cgc = tbxCNPJ.Text;
                clsClienteInfo.ie = tbxIEstadual.Text.Trim();
                clsClienteInfo.imunicipal = "";
                clsClienteInfo.suframa = "";
                clsClienteInfo.email = "";
                clsClienteInfo.homepage = "";
                clsClienteInfo.ftp = "";
                clsClienteInfo.cep = tbxCEP.Text;
                clsClienteInfo.endtipo = "";
                clsClienteInfo.endereco = tbxEndereco.Text.Trim();
                clsClienteInfo.tiponumero = "";
                clsClienteInfo.numeroend = tbxNroEnd.Text;
                clsClienteInfo.andar = "";
                clsClienteInfo.tipocomple = "";
                clsClienteInfo.comple = "";
                clsClienteInfo.bairro = tbxBairro.Text.Trim();
                clsClienteInfo.cidade = tbxCidade.Text.Trim();
                clsClienteInfo.regiao = "";
                if (tbxIBGE.Text.Length > 0)
                {
                    clsClienteInfo.ibge = tbxIBGE.Text.Trim();
                }
                else
                {
                    clsClienteInfo.ibge = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ibge from estados where id=" + clsClienteInfo.idestado + " ", "??");
                    clsClienteInfo.ibge += "00000"; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from cidades where idestado = " + clsClienteInfo.idestado + " and nome='" + clsVisual.RemoveAcentos(tbxCidade.Text).ToUpper() + "' ", "0");
                }
                clsClienteInfo.cadastrado = clsParser.DateTimeParse(tbxDataEmissao.Text);
                clsClienteInfo.codigocliente = "";
                clsClienteInfo.freteincluso = "";
                clsClienteInfo.freteporconta = "";
                clsClienteInfo.meiodetransporte = "";
                clsClienteInfo.credito = "";
                clsClienteInfo.limitecredito = 0;
                clsClienteInfo.agencia = "";
                clsClienteInfo.ctacorrente = "";
                clsClienteInfo.titularcta = "";
                clsClienteInfo.clienteaprovado = "";
                clsClienteInfo.dataaprovado = clsParser.DateTimeParse(tbxDataEmissao.Text);
                clsClienteInfo.temfaturamento = "";
                clsClienteInfo.valorminimo = 0;
                clsClienteInfo.clientedesde = clsParser.DateTimeParse(tbxDataEmissao.Text);
                clsClienteInfo.datacompra = clsParser.DateTimeParse(tbxDataEmissao.Text);
                clsClienteInfo.maiorcompra = 0;
                clsClienteInfo.comissaocomo = "";
                clsClienteInfo.comissaopagto = "";
                clsClienteInfo.comissaopor = "";
                clsClienteInfo.comissaocoordenador = 0;
                clsClienteInfo.comissaosupervisor = 0;
                clsClienteInfo.comissaoaliquota = 0;
                clsClienteInfo.descontacofinsent = "S";
                clsClienteInfo.descontacofinssai = "S";
                clsClienteInfo.descontapispasepent = "S";
                clsClienteInfo.descontapispasepsai = "S";
                clsClienteInfo.dependenteirrf = 0;
                clsClienteInfo.pais = "BRA";
                clsClienteInfo.emailnfe = "";
                clsClienteInfo.isentoipi = "";
                clsClienteInfo.revendedor = "";
                clsClienteInfo.contribuinte = "";
                clsClienteInfo.alc = "";
                clsClienteInfo.consumo = "";
                clsClienteInfo.datafichafinanc = DateTime.Parse(tbxDataEmissao.Text);
                clsClienteInfo.saldofichafinanc = 0;
                clsClienteInfo.observa = "";
                clsClienteInfo.desconto = 0;
                clsClienteInfo.diasemana = "";
                clsClienteInfo.ultdatnf = DateTime.Parse("01/01/1900");
                clsClienteInfo.ultdatorc = DateTime.Parse("01/01/1900");

                // Incluir o fornecedor na base de dados oficial
                clsClienteBLL clsClienteBLL = new clsClienteBLL();
                clsClienteBLL.VerificaInfo(clsClienteInfo);

                clsClienteInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT top 1 MAX(NUMERO) + 1 FROM CLIENTE group by NUMERO ORDER BY NUMERO DESC"));

                if (clsClienteInfo.numero == 0)
                {
                    clsClienteInfo.numero = 1;
                }

                id_fornecedor = clsClienteBLL.Incluir(clsClienteInfo, clsInfo.conexaosqldados);
                clsXmlNFCompraInfo.idfornecedor = id_fornecedor;
            }

            // Carregar os dados do Cabecario da Nota Fiscal
            // Verificar se esta Nota já não esta em andamento
            clsXmlNFCompraInfo.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from XmlNFCompra where numero=" + clsParser.Int32Parse(tbxNotaFiscal.Text) + " ", "0"));
            clsXmlNFCompraInfo.totalnotafiscal = clsParser.DecimalParse(tbxValorTotalNF.Text);
            if (clsXmlNFCompraInfo.id == 0)
            {
                clsXmlNFCompraInfo.id = clsXmlNFCompraBLL.Incluir(clsXmlNFCompraInfo, clsInfo.conexaosqldados);
            }
            idXmlNFCompra = clsXmlNFCompraInfo.id;

            // Carregar os Itens
            // carregando os itens da Nota Fiscal de Entrada XML
            foreach (ListViewItem item in listView1.Items)
            {
                // Faça algo com o item. Por exemplo:
                // string textoDoItem = item.Text;
                // Ou acesse outras propriedades do item, como item.SubItems
                //Console.WriteLine(textoDoItem);
                Status = 5;
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    string dueDate = item.SubItems[i].Text;

                    //MessageBox.Show(dueDate);
                    if (i == 0)
                    {
                        clsXmlNFCompra1Info = new clsXmlNFCompra1Info();
                    }
                    else if (i == 1)
                    {
                        clsXmlNFCompra1Info.codigo = item.SubItems[i].Text;
                    }
                    else if (i == 2)
                    {
                        clsXmlNFCompra1Info.codigobarra = item.SubItems[i].Text;
                    }
                    else if (i == 3)
                    {
                        clsXmlNFCompra1Info.codigofornecedor = item.SubItems[i].Text;
                    }
                    else if (i == 4)
                    {
                        clsXmlNFCompra1Info.descricao = item.SubItems[i].Text;
                    }
                    else if (i == 5)
                    {
                        clsXmlNFCompra1Info.qtde = clsParser.DecimalParse(item.SubItems[i].Text.Replace('.', ','));
                    }
                    else if (i == 6)
                    {
                        clsXmlNFCompra1Info.unid = item.SubItems[i].Text;
                        // Pegar o id da Unidade de Medida
                        id_Unidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from unidade where codigo='" + clsXmlNFCompra1Info.unid + "' ", "1"));
                        clsXmlNFCompra1Info.idunid = id_Unidade;
                        Status -= 1;
                    }
                    else if (i == 7)
                    {
                        clsXmlNFCompra1Info.preco = clsParser.DecimalParse(item.SubItems[i].Text.Replace('.', ','));
                    }
                    else if (i == 8)
                    {
                        clsXmlNFCompra1Info.totalmercado = clsParser.DecimalParse(item.SubItems[i].Text.Replace('.', ','));
                    }
                    else if (i == 9)
                    {
                        clsXmlNFCompra1Info.valoricm = clsParser.DecimalParse(item.SubItems[i].Text.Replace('.', ','));
                    }
                    else if (i == 10)
                    {
                        clsXmlNFCompra1Info.valoripi = clsParser.DecimalParse(item.SubItems[i].Text.Replace('.', ','));
                    }
                    else if (i == 11)
                    {
                        clsXmlNFCompra1Info.ncm = item.SubItems[i].Text;
                        clsXmlNFCompra1Info.idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM IPI where CODIGO='" + clsXmlNFCompra1Info.ncm + "'").ToString());
                        clsXmlNFCompra1Info.totalnfitem = clsXmlNFCompra1Info.totalmercado + clsXmlNFCompra1Info.valoricm + clsXmlNFCompra1Info.valoripi;
                        clsXmlNFCompra1Info.totalmercado = clsXmlNFCompra1Info.totalnfitem;
                        clsXmlNFCompra1Info.precovenda = 0; // Inicializa o total da nota
                        clsXmlNFCompra1Info.fatura = "S";
                        clsXmlNFCompra1Info.numero = clsXmlNFCompraInfo.id;
                        clsXmlNFCompra1Info.foto = null;
                        // Verificar se existe no cadastro pecas
                        clsXmlNFCompra1Info.idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigobarra='" + clsXmlNFCompra1Info.codigobarra + "' ", "0"));
                        if (clsXmlNFCompra1Info.idcodigo > 0)
                        {
                            Status -= 1;
                            // Verificar se existe marca
                            clsXmlNFCompra1Info.idmarca = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idmarca from pecas where id='" + clsXmlNFCompra1Info.idcodigo + "' ", "0"));
                            if (clsXmlNFCompra1Info.idmarca > 0) { Status -= 1; }
                            // Verificar onde se enquadra o material
                            clsXmlNFCompra1Info.idclassifica = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idclassifica from pecas where id='" + clsXmlNFCompra1Info.idcodigo + "' ", "0"));
                            if (clsXmlNFCompra1Info.idclassifica > 0) { Status -= 1; }
                            clsXmlNFCompra1Info.descricaocadastro = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id='" + clsXmlNFCompra1Info.idcodigo + "' ", "0");
                            //clsXmlNFCompra1Info.foto = clsParser.imageToByteArray(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select foto from pecas where id='" + clsXmlNFCompra1Info.idcodigo + "' ", ""));
                            clsXmlNFCompra1Info.aplicacao = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aplicacao from pecas where id='" + clsXmlNFCompra1Info.idcodigo + "' ", "0");
                            // Verificar a lista Preco de venda atual
                            clsXmlNFCompra1Info.precovendaanterior = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoraprazo from pecaspreco where idcodigo = " + clsXmlNFCompra1Info.idcodigo + " order by data desc ", "0").ToString());
                        }
                        clsXmlNFCompra1Info.status = Status;

                        // Incluir Registro na base de dados
                        clsXmlNfCompra1BLL.Incluir(clsXmlNFCompra1Info, clsInfo.conexaosqldados);

                    }
                }
            }
            btnLerXML.Enabled = false;
            gbxFornecedor.Visible = true;
            tabControl1.SelectedIndex = 1;
            gbxItensNota.Visible = true;
            //Carregar os bwr
            bwrXmlNFCompra1_Run();


        }

        private void btnRetornarTela_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void bwrXmlNFCompra_Run()
        {
            if (carregandoXmlNFCompra == false)
            {
                carregandoXmlNFCompra = true;
                //pbxCompras.Visible = true;
                bwrXmlNFCompra = new BackgroundWorker();
                bwrXmlNFCompra.DoWork += new DoWorkEventHandler(bwrXmlNFCompra_DoWork);
                bwrXmlNFCompra.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrXmlNFCompra_RunWorkerCompleted);
                bwrXmlNFCompra.RunWorkerAsync();
            }
        }


        private void bwrXmlNFCompra_DoWork(object sender, DoWorkEventArgs e)
        {

            dtXmlNFCompra = clsXmlNFCompraBLL.GridDados();
        }


        private void bwrXmlNFCompra_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsXmlNFCompraBLL.GridMonta(dgvXmlNFCompra, dtXmlNFCompra, clsInfo.zrowid);
                //clsGridHelper.SelecionaLinha(id, dgvXmlNFCompra, 1);
                PintaGrid(dgvXmlNFCompra);
                carregandoXmlNFCompra = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoXmlNFCompra = false;
            }
        }

        private void bwrXmlNFCompra1_Run()
        {
            bwrXmlNFCompra1 = new BackgroundWorker();
            bwrXmlNFCompra1.DoWork += new DoWorkEventHandler(bwrXmlNFCompra1_DoWork);
            bwrXmlNFCompra1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrXmlNFCompra1_RunWorkerCompleted);
            bwrXmlNFCompra1.RunWorkerAsync();
        }
        private void bwrXmlNFCompra1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtXmlNFCompra1 = clsXmlNFCompra1BLL.GridDados(idXmlNFCompra);
        }
        private void bwrXmlNFCompra1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsXmlNFCompra1BLL.GridMonta(dgvXmlNFCompra1, dtXmlNFCompra1, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvXmlNFCompra1, 1);
                PintaGrid1(dgvXmlNFCompra1);

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
        public void PintaGrid(DataGridView _datagrid)
        {
            
            foreach (DataGridViewRow dgvrNFCompra in _datagrid.Rows)
            {
                //// Destacando itens com cores
                if (dgvrNFCompra.Cells["Status"].Value.ToString() == "0") // NÃO EFETUOU NENHUM ACERTO
                {
                    _datagrid.Rows[dgvrNFCompra.Index].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                { 
                    _datagrid.Rows[dgvrNFCompra.Index].DefaultCellStyle.BackColor = Color.LightGreen;
                    // liberar o botão de transferencia
                    tspTransferir.Enabled = true;
                }
            }
        }

        public void PintaGrid1(DataGridView _datagrid)
        {
            tbxTotalNota1.Text = "0";
            int quantidadeRegistros = _datagrid.Rows.Count;
            int quantidadeRegistrosOK = 0;

            foreach (DataGridViewRow dgvXLMNFCompra1Item in _datagrid.Rows)
            {
                //// Destacando itens com cores
                if (dgvXLMNFCompra1Item.Cells["STATUS"].Value.ToString() != "0")
                {
                    _datagrid.Rows[dgvXLMNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.Red;
                    
                }
                else if (dgvXLMNFCompra1Item.Cells["STATUS"].Value.ToString() == "0")
                {
                    _datagrid.Rows[dgvXLMNFCompra1Item.Index].DefaultCellStyle.BackColor = Color.LightGreen;
                    tbxTotalNota1.Text = (clsParser.DecimalParse(tbxTotalNota1.Text) + clsParser.DecimalParse(dgvXLMNFCompra1Item.Cells["TOTALMERCADO"].Value.ToString())).ToString();
                    quantidadeRegistrosOK += 1;
                }
            }
            tbxTotalNota1.Text = (clsParser.DecimalParse(tbxTotalNota1.Text).ToString("N2"));

            if (quantidadeRegistrosOK == quantidadeRegistros)
            {
                MessageBox.Show("Todos os Itens já foram atualizados e cadastrados no sistema.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);  
                // Mudar o Status da Nota Fiscal Principal para 1

                clsXmlNFCompraInfo clsXmlNFCompraInfo = new clsXmlNFCompraInfo();
                clsXmlNFCompraInfo = clsXmlNFCompraBLL.Carregar(idXmlNFCompra, clsInfo.conexaosqldados);
                clsXmlNFCompraInfo.status = 1; // Nota Fiscal ok
                clsXmlNFCompraBLL.Alterar(clsXmlNFCompraInfo, clsInfo.conexaosqldados);

                bwrXmlNFCompra_Run(); // Atualiza a Grid Principal
                PintaGrid(dgvXmlNFCompra);
            }
            else
            {
                clsXmlNFCompraInfo clsXmlNFCompraInfo = new clsXmlNFCompraInfo();
                clsXmlNFCompraInfo = clsXmlNFCompraBLL.Carregar(idXmlNFCompra, clsInfo.conexaosqldados);
                clsXmlNFCompraInfo.status = 0; //
                clsXmlNFCompraBLL.Alterar(clsXmlNFCompraInfo, clsInfo.conexaosqldados);

                bwrXmlNFCompra_Run(); // Atualiza a Grid Principal
                PintaGrid(dgvXmlNFCompra);

            }
        }

        private void dgvXmlNFCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvXmlNFCompra.CurrentRow != null)
            {
                idXmlNFCompra = (Int32)dgvXmlNFCompra.CurrentRow.Cells[0].Value;
                tbxNotaFiscal1.Text = dgvXmlNFCompra.CurrentRow.Cells[1].Value.ToString();
                tbxDataEmissao1.Text = dgvXmlNFCompra.CurrentRow.Cells[2].Value.ToString();
                tbxCliente1.Text = dgvXmlNFCompra.CurrentRow.Cells[3].Value.ToString();

                btnLerXML.Enabled = false;
                gbxFornecedor.Visible = true;
                tabControl1.SelectedIndex = 1;
                gbxItensNota.Visible = true;

                lbxFornecedor.Text = "Fornecedor já Cadastrado";
                bwrXmlNFCompra1_Run();
            }
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento");
        }

        private void dgvXmlNFCompra1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvXmlNFCompra1.CurrentRow != null)
            {
                // Posicao Cursor
                    id = clsParser.Int32Parse(dgvXmlNFCompra1.CurrentRow.Cells["ID"].Value.ToString());

                    if (dgvXmlNFCompra1.CurrentRow.Index > 0)
                    {
                        id_Anterior = clsParser.Int32Parse(dgvXmlNFCompra1.Rows[dgvXmlNFCompra1.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Anterior = 0;
                    }


                    if (dgvXmlNFCompra1.CurrentRow.Index < dgvXmlNFCompra1.Rows.Count - 1)
                    {
                        id_Proximo = clsParser.Int32Parse(dgvXmlNFCompra1.Rows[dgvXmlNFCompra1.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        id_Proximo = 0;
                    }


                tabControl1.SelectedIndex = 2;
                dgvXmlNFCompra1.Enabled = false;

                tbxNotaFiscal2.Text = tbxNotaFiscal1.Text.Trim();
                tbxDataEmissao2.Text = tbxDataEmissao1.Text.Trim();
                tbxCliente2.Text = tbxCliente1.Text.Trim();

                gbxCabecarioItem.Visible = true;
                gbxItemNota.Visible = true;

                idXmlNFCompra1 = (Int32)dgvXmlNFCompra1.CurrentRow.Cells[0].Value;
                clsXmlNFCompra1Info = clsXmlNfCompra1BLL.Carregar(idXmlNFCompra1, clsInfo.conexaosqldados);

                XmlNFCompra1Campos(clsXmlNFCompra1Info);
            }

        }

        void XmlNFCompra1Campos(clsXmlNFCompra1Info info)
        {
            idXmlNFCompra1 = info.id;
            id_Codigo = info.idcodigo;
            clsPecasInfo = clsPecasBLL.Carregar(id_Codigo, clsInfo.conexaosqldados);

            if (id_Codigo > 0)
            {
                tbxCodigo.Text = info.codigo; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id='" + id_Codigo + "' ", "0");
                tbxCodigoBarra.Text = info.codigobarra; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigobarra from pecas where id='" + id_Codigo + "' ", "0");
                tbxCodigoFornecedor.Text = info.codigofornecedor; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigofornecedor from pecas where id='" + id_Codigo + "' ", "0");
                tbxFatorConv.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select fatorconv from pecas where id = " + id_Codigo + " ", "0").ToString()).ToString("N3");
            }
            id_Grupo = info.idclassifica;
            if (id_Grupo > 0)
            {
                tbxPecasClassifica.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecasclassifica where id='" + id_Grupo + "' ", "0");
            }
            else
            {
                tbxPecasClassifica.Text = "";
            }   
            id_Marca = info.idmarca;
            if (id_Marca > 0)
            {
                tbxMarca.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecastipo where id='" + id_Marca + "' ", "0");
            }
            else
            {
                tbxMarca.Text = "";
            }
            tbxAplicacao.Text = info.aplicacao;
            tbxCodigo.Text = info.codigo;
            tbxCodigoBarra.Text = info.codigobarra;
            tbxCodigoFornecedor.Text = info.codigofornecedor;
            tbxNCM.Text = info.ncm;
            pbxFoto.Image = clsPecasInfo.foto;
            tbxNome.Text = info.descricao;
            tbxDescricaoCadastro.Text = info.descricaocadastro;
            tbxUnidade.Text = info.unid;
            if (id_Unidade == 0 & info.unid.Length >= 2)
            {
                id_Unidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from unidade where codigo='" + tbxUnidade.Text + "' ", "0"));
                if (id_Unidade > 0)
                {
                    tbxUnidade.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    tbxUnidade.BackColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id='" + id_Unidade + "' ", "0");
                tbxUnidade.BackColor = System.Drawing.Color.White;
            }
            tbxQtde.Text = info.qtde.ToString("N3");
            tbxPreco.Text = info.preco.ToString("N5");
            tbxPrecoTabela.Text = info.precovenda.ToString("N2");
            tbxPrecoVendaAnterior.Text = info.precovendaanterior.ToString("N2");

            tbxStatus.Text = info.status.ToString();    
            tbxTotalMercado.Text = info.totalmercado.ToString("N2");
            tbxTotalNFItem.Text = info.totalnfitem.ToString("N2");
            tbxFator.Text = info.fator.ToString("N3");
            Status = 5;
            if (id_Codigo > 0) { Status -= 1; }
            if (id_Unidade > 0) { Status -= 1; }
            if (id_Marca > 0) { Status -= 1; }
            if (id_Grupo > 0) { Status -= 1; }
            if (clsParser.DecimalParse(tbxPrecoTabela.Text) > 0) { tbxPrecoTabela.BackColor = System.Drawing.Color.White; Status -= 1; }
            if (clsParser.DecimalParse(tbxTotalMercado.Text) != clsParser.DecimalParse(tbxTotalNFItem.Text) ) 
               { Status += 1 ;}
            tbxStatus.Text = Status.ToString();
            if (info.fatura == "N")
            {
                rbnFaturaNao.Checked = true;
            }
            else
            {
                rbnFaturaOk.Checked = true;
            }
            tbxCodigo.Select();
            tbxCodigo.SelectAll();
        }

        private void btnItemSair_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            dgvXmlNFCompra1.Enabled = true;

            gbxCabecarioItem.Visible = false;
            gbxItemNota.Visible = false;

        }

        private void btnIdUnidadeCom_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdUnidadeCom.Name;
            frmUnidadeVis frmUnidadeVis = new frmUnidadeVis();
            frmUnidadeVis.Init(id_Unidade);
            clsFormHelper.AbrirForm(this.MdiParent, frmUnidadeVis, clsInfo.conexaosqldados);

        }

        private void btnCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigo.Name;
            frmPecasVis frmPecasVis = new frmPecasVis();
            frmPecasVis.Init(id_Codigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasVis, clsInfo.conexaosqldados);

        }

        private void tspTool_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnIdPecasClassifica_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdPecasClassifica.Name;
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA", id_Grupo, 30, 60, "Grupo de Material");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnMarca.Name;
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECASTIPO", id_Marca,30, 60, "Marca do Produto");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void pbxFoto_Click(object sender, EventArgs e)
        {
            this.SelecionarImagem(ref this.pbxFoto, ref this.pbxFoto);
        }
        private void SelecionarImagem(ref PictureBox pbxCatalogo, ref PictureBox pbxFoto)
        {
            try
            {
                this.oldFoto.FileName = "";

                this.oldFoto.Filter = "Png (*.png;)|*.png|Bitmap (*.bmp;)|*.bmp|Jpeg (*.jpg;)|*.jpg|Todos (*.*)|*.*";

                this.oldFoto.ShowDialog();

                if (this.oldFoto.FileName.ToString().Length > 0)
                {
                    var img = Image.FromFile(this.oldFoto.FileName);

                    // Maior que 100 KB
                    while (clsVisual.TamanhoImagem(img) / 1024 > 100)
                    {
                        img = clsVisual.RedimensionarImagem(img, new Size((int)(img.Width - (img.Width * 0.1)), (int)(img.Height - (img.Height * 0.1))));
                    }

                    pbxCatalogo.Image = img;

                    if (pbxFoto != null)
                    {
                        pbxFoto.Image = pbxCatalogo.Image;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nenhum arquivo selecionado.");
            }
        }

        private void btnItemOk_Click(object sender, EventArgs e)
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: PECAS
                clsXmlNFCompra1Info = new clsXmlNFCompra1Info();
                XmlNFCompra1FillInfo(clsXmlNFCompra1Info);
                clsXmlNfCompra1BLL.Alterar(clsXmlNFCompra1Info, clsInfo.conexaosqldados);
                
                tse.Complete();
            }
            tabControl1.SelectedIndex = 1;
            dgvXmlNFCompra1.Enabled = true;

            gbxCabecarioItem.Visible = false;
            gbxItemNota.Visible = false;

            bwrXmlNFCompra1_Run();
            PintaGrid1(dgvXmlNFCompra1);    

        }
        void XmlNFCompra1FillInfo(clsXmlNFCompra1Info info)
        {

            info.numero = idXmlNFCompra;
            info.aplicacao = tbxAplicacao.Text;
            if (info.aplicacao.Length >= 400)
            {
                info.aplicacao = info.aplicacao.Substring(0, 399);
            }
            info.codigo = tbxCodigo.Text;
            info.codigobarra = tbxCodigoBarra.Text;
            info.codigofornecedor = tbxCodigoFornecedor.Text;
            info.foto = pbxFoto.Image;
            info.id = idXmlNFCompra1;
            info.idclassifica = id_Grupo;
            info.idcodigo = id_Codigo;
            info.idmarca = id_Marca;
            info.idunid = id_Unidade;
            info.idipi = id_Ipi;
            info.descricao = tbxNome.Text;
            if (rbnFaturaNao.Checked == true)
            {
                info.fatura = "N";
            }
            else
            {
                info.fatura = "S";
            }
            info.descricaocadastro = tbxDescricaoCadastro.Text;
            info.fator = clsParser.DecimalParse(tbxFator.Text);
            info.preco = clsParser.DecimalParse(tbxPreco.Text);
            info.precovenda = clsParser.DecimalParse(tbxPrecoTabela.Text);
            info.precovendaanterior = clsParser.DecimalParse(tbxPrecoVendaAnterior.Text);
            info.qtde = clsParser.DecimalParse(tbxQtde.Text);
            info.status = clsParser.Int32Parse(tbxStatus.Text);
            info.totalmercado = clsParser.DecimalParse(tbxTotalMercado.Text);
            info.totalnfitem = clsParser.DecimalParse(tbxTotalNFItem.Text);
            info.unid = tbxUnidade.Text;
            info.ncm = tbxNCM.Text;
            info.idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM IPI where CODIGO='" + tbxNCM.Text + "'").ToString());

        }

        private void tbxPreco_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCodigoIncluir_Click(object sender, EventArgs e)
        {
            if (tbxDescricaoCadastro.Text.Length > 5)
            {
                if (id_Codigo == 0)
                {
                    clsPecasBLL = new clsPecasBLL();
                    clsPecasInfo = new clsPecasInfo();
                    clsPecasInfo.ativo = "S";
                    clsPecasInfo.aplicacao = tbxAplicacao.Text;
                    clsPecasInfo.cadastrado = DateTime.Now;
                    clsPecasInfo.codigo = tbxCodigo.Text;
                    clsPecasInfo.codigobarra = tbxCodigoBarra.Text;
                    clsPecasInfo.codigofornecedor = tbxCodigoFornecedor.Text;
                    clsPecasInfo.compraautomatica = "N";
                    clsPecasInfo.comprarqtde = 0;
                    clsPecasInfo.comprarvia = "N";
                    clsPecasInfo.datacompra = DateTime.Now;
                    clsPecasInfo.datapreco = DateTime.Now;
                    clsPecasInfo.diasentrega = 0;
                    clsPecasInfo.diasestoque = 0;
                    clsPecasInfo.estoquemin = 0;
                    clsPecasInfo.estoqueminaut = "N";
                    if (clsPecasInfo.fatorconv == 0)
                    {
                        clsPecasInfo.fatorconv = clsParser.DecimalParse(tbxFator.Text);
                    }
                    clsPecasInfo.foto = null;
                    clsPecasInfo.id = 0;
                    clsPecasInfo.idclassifica = id_Grupo;
                    clsPecasInfo.idclassifica1 = 0;
                    clsPecasInfo.idclassifica2 = 0;
                    clsPecasInfo.idhistoricobco = 0;
                    clsPecasInfo.idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM IPI where CODIGO='" + tbxNCM.Text + "'").ToString());
                    id_Ipi = clsPecasInfo.idipi;
                    if (tbxNCM.Text.Length > 0 && clsPecasInfo.idipi == 0)
                    {
                        MessageBox.Show("Cadastrar codigo NCM " + tbxNCM.Text);
                    }
                    clsPecasInfo.idmarca = id_Marca;
                    clsPecasInfo.idsittriba = clsInfo.zsituacaotriba;
                    clsPecasInfo.idsittribvenda = clsInfo.zsituacaotribb;
                    clsPecasInfo.idunidade = id_Unidade;
                    clsPecasInfo.idunidadecom = id_Unidade;
                    clsPecasInfo.locacao = "";
                    clsPecasInfo.nome = tbxDescricaoCadastro.Text;
                    clsPecasInfo.pesounit = 0;
                    clsPecasInfo.pesobruto = 0;
                    clsPecasInfo.precocompra = clsParser.DecimalParse(tbxPreco.Text);
                    clsPecasInfo.precovenda = clsParser.DecimalParse(tbxPrecoTabela.Text);
                    clsPecasInfo.qtdeentra = 0;
                    clsPecasInfo.qtdeinicio = 0;
                    clsPecasInfo.qtdeentra = 0;
                    clsPecasInfo.qtdesaida = 0;
                    clsPecasInfo.qtdesaldo = 0;
                    clsPecasInfo.id = clsPecasBLL.Incluir(clsPecasInfo, clsInfo.conexaosqldados);
                    id_Codigo = clsPecasInfo.id;

                    // Verificar a tabela de preços
                    Decimal PrecoVendaCadastro = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoraprazo from pecaspreco where idcodigo = " + id_Codigo + " order by data desc ", "0").ToString());
                    if (PrecoVendaCadastro != clsParser.DecimalParse(tbxPrecoTabela.Text))
                    {
                        // Incluir a Lista de Preços
                        clsPecasPrecoBLL = new clsPecasPrecoBLL();
                        clsPecasPrecoInfo = new clsPecasPrecoInfo();
                        clsPecasPrecoInfo.data = DateTime.Now;
                        clsPecasPrecoInfo.id = 0;
                        clsPecasPrecoInfo.idcodigo = id_Codigo;
                        clsPecasPrecoInfo.valoraprazo = clsParser.DecimalParse(tbxPrecoTabela.Text);
                        clsPecasPrecoInfo.valoravista = clsParser.DecimalParse(tbxPrecoTabela.Text);
                        clsPecasPrecoInfo.id = clsPecasPrecoBLL.Incluir(clsPecasPrecoInfo, clsInfo.conexaosqldados);
                    }
                    TrataCampos((Control)sender);
                }
                else
                {
                    MessageBox.Show("Item já Cadastraado");
                }
            }
            else
            {
                MessageBox.Show("Favor preencher a descrição do material irá se usada no Cadastro");
            }
            
        }

        private void gbxItensNota_Enter(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = MessageBox.Show("Deseja mesmo excluir/apagar todos os itens ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {

                // Apagar a nota de xml 
                SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                SqlCommand scd = new SqlCommand("delete from xmlnfcompra1 where numero = " +
                    idXmlNFCompra, scn);

                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();

                //Ecluindo Orçamento Cond. Pagto Formato da Linguagem C#
                SqlConnection scn1 = new SqlConnection(clsInfo.conexaosqldados);
                SqlCommand scd1 = new SqlCommand("delete from xmlnfcompra where id = " +
                    idXmlNFCompra, scn1);

                scn1.Open();
                scd1.ExecuteNonQuery();
                scn1.Close();

                MessageBox.Show("Termino da Exclusão");

                this.Close();
                this.Dispose();
            }
        }

        private void dgvXmlNFCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnProcessoConcluido_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvXMLNFCompra in dgvXmlNFCompra1.Rows)
            {
                idXmlNFCompra = clsParser.Int32Parse(dgvXMLNFCompra.Cells["ID"].Value.ToString());
                id_Codigo = clsParser.Int32Parse(dgvXMLNFCompra.Cells["IDCODIGO"].Value.ToString());
                if (id_Codigo > 0)
                {
                    String NCM = dgvXMLNFCompra.Cells["NCM"].Value.ToString();
                    if (NCM.Length > 0)
                    {
                        id_Ipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from ipi where codigo='" + NCM.Trim() + "'", "0"));
                    }
                    if (id_Ipi > 0)
                    {
                        // Atualizar o NCM
                        clsPecasBLL = new clsPecasBLL();
                        clsPecasInfo = new clsPecasInfo();
                        clsPecasInfo = clsPecasBLL.Carregar(id_Codigo, clsInfo.conexaosqldados);
                        clsPecasInfo.idipi = id_Ipi;
                        clsPecasBLL.Alterar(clsPecasInfo, clsInfo.conexaosqldados);
                    }
                }
            }
            // Apagar a nota de xml 
            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
            SqlCommand scd = new SqlCommand("delete from xmlnfcompra1", scn);

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

            //Ecluindo Orçamento Cond. Pagto Formato da Linguagem C#
            SqlConnection scn1 = new SqlConnection(clsInfo.conexaosqldados);
            SqlCommand scd1 = new SqlCommand("delete from xmlnfcompra", scn1);

            scn1.Open();
            scd1.ExecuteNonQuery();
            scn1.Close();

            MessageBox.Show("Termino da Exclusão");

            this.Close();
            this.Dispose();
        }
    }

}
