using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
using System.Transactions;

namespace CRCorrea
{
    public partial class frmNotaPromissoria : Form
    {
        Int32 idCliente;
        Int32 idCondPagto;
        Int32 idDestinatario;
        Int32 idFormaPagto;
        DataTable dtPromissoriaReceber;
        DataTable dtPromissoriaReceberTempDeletar;

        String query;
        SqlConnection scn;
        SqlCommand scd;
        SqlDataReader sdr;


        public frmNotaPromissoria()
        {
            InitializeComponent();
        }
        public void Init(Int32 _idCliente)
        {
            idCliente = _idCliente;

        }

        private void frmNotaPromissoria_Load(object sender, EventArgs e)
        {
            clsClienteBLL clsClienteBLL = new clsClienteBLL();
            clsClienteInfo clsClienteInfo = new clsClienteInfo();
            clsClienteEnderecoBLL clsClienteEnderecoBLL = new clsClienteEnderecoBLL();
            clsClienteEnderecoInfo clsClienteEnderecoInfo = new clsClienteEnderecoInfo();

            tbxDataemissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            tclPagamentos.SelectedIndex = 0;
            idFormaPagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT id FROM SITUACAOTIPOTITULO where CODIGO='NP'"));
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            // Antes de Imprimir preparar a tabela de promissoriareceber

            //
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = idCliente;
            field.Name = "id";
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            //field = new ParameterField();
            //field.Name = "condpagto";
            //valor = new ParameterDiscreteValue();
            //valor.Value = CondPagto;
            //field.CurrentValues.Add(valor);
            //parameters.Add(field);

            frmCrystalReport.Init(clsInfo.caminhorelatorios, "PROMISSORIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", idCliente);
            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

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
                    clsClienteBLL clsClienteBLL = new clsClienteBLL();
                    clsClienteInfo clsClienteInfo = new clsClienteInfo();

                    idCliente = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    if (idCliente > 0)
                    {
                        clsClienteInfo = clsClienteBLL.Carregar(idCliente, clsInfo.conexaosqldados);
                        tbxCliente_Cognome.Text = clsClienteInfo.cognome;
                        tbxNome.Text = clsClienteInfo.nome;
                        cbxPessoa.SelectedIndex = SelecionarIndex(clsClienteInfo.pessoa, 1, cbxPessoa);
                        tbxCliente_cnpj.Text = clsClienteInfo.cgc;
                        tbxCgc.Text = clsClienteInfo.cgc;

                        switch (cbxPessoa.Text.Substring(0, 1))
                        {
                            case "F":
                                tbxCgc.Text = clsVisual.CamposVisual("CPF", tbxCgc.Text);
                                //tbxIe.Text = clsVisual.CamposVisual("RG", tbxIe.Text);
                                break;
                            case "J":
                                tbxCgc.Text = clsVisual.CamposVisual("CGC", tbxCgc.Text);
                                //tbxIe.Text = clsVisual.CamposVisual("IE", tbxIe.Text);
                                break;
                            default:
                                break;
                        }
                        tbxCep.Text = clsClienteInfo.cep;
                        tbxEndereco.Text = clsClienteInfo.endereco + " - " + clsClienteInfo.numero;
                        tbxBairro.Text = clsClienteInfo.bairro;
                        tbxCidade.Text = clsClienteInfo.cidade;
                        tbxUF.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ESTADO FROM ESTADOS where id=" + clsClienteInfo.idestado);



                        tbxNome.Select();
                        tbxNome.SelectAll();
                    }
                }
                else if (clsInfo.znomegrid == btnCondpagto_codigo.Name)
                {
                    idCondPagto = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxCondpagto_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCondpagto_codigo.Select();
                    tbxCondpagto_codigo.SelectAll();
                }
                
                else if (clsInfo.znomegrid == btnIdDestinatario.Name)  // Destinatario
                {
                    clsClienteBLL clsClienteBLL = new clsClienteBLL();
                    clsClienteInfo clsClienteInfo = new clsClienteInfo();

                    idDestinatario = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    {
                        clsClienteInfo = clsClienteBLL.Carregar(idDestinatario, clsInfo.conexaosqldados);
                        tbxDestinatario_Cognome.Text = clsClienteInfo.cognome;
                        tbxDestinatario_Nome.Text = clsClienteInfo.nome;
                        tbxDestinatario_Pessoa.Text = clsClienteInfo.pessoa;
                        tbxDestinatarioCNPJ.Text = clsClienteInfo.cgc;
                        tbxDestinatario_Cnpj.Text = clsClienteInfo.cgc;

                        switch (tbxDestinatario_Pessoa.Text.Substring(0, 1))
                        {
                            case "F":
                                tbxDestinatario_Cnpj.Text = clsVisual.CamposVisual("CPF", tbxDestinatario_Cnpj.Text);
                                //tbxIe.Text = clsVisual.CamposVisual("RG", tbxIe.Text);
                                break;
                            case "J":
                                tbxDestinatario_Cnpj.Text = clsVisual.CamposVisual("CGC", tbxDestinatario_Cnpj.Text);
                                //tbxIe.Text = clsVisual.CamposVisual("IE", tbxIe.Text);
                                break;
                            default:
                                break;
                        }

                        tbxDataemissao.Select();
                        tbxDataemissao.SelectAll();
                    }
                }

        }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################

                if (ctl.Name == tbxCliente_Cognome.Name)    // FORNECEDOR
                {
                }
            }
            tbxValortotal.Text = clsParser.DecimalParse(tbxValortotal.Text).ToString("N2");

            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private void frmNotaPromissoria_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
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

        private void ControlKeyDownCep(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownCep((TextBox)sender, e);
        }

        private void ControlKeyDownNumero(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownNumero((TextBox)sender, e);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnCondpagto_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCondpagto_codigo.Name;
            frmCondPagtoPes frmCondPagtoPes = new frmCondPagtoPes();
            frmCondPagtoPes.Init(clsInfo.conexaosqldados, idCondPagto);
            clsFormHelper.AbrirForm(this.MdiParent, frmCondPagtoPes, clsInfo.conexaosqldados);

        }

        private void tspContratoReceberVis_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsbCondpagto_Incluir_Click(object sender, EventArgs e)
        {

        }

        private void tsbCondpagto_Alterar_Click(object sender, EventArgs e)
        {

        }

        private void tsbCondpagto_Excluir_Click(object sender, EventArgs e)
        {

        }

        private void tsbCondpagto_Calcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (idCondPagto > 0)
                {
                    PromissoriaCalcularPagamentos();
                }
                else
                {
                    throw new Exception("Antes de iniciar o re-cálculo é necessário escolher uma 'Condição de Pagamento'.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void tsbCondpagto_Salvar_Click(object sender, EventArgs e)
        {
            tclPagamentos.SelectedIndex = 0;
        }

        private void tsbCondpagto_RetornarPagto_Click(object sender, EventArgs e)
        {

           tclPagamentos.SelectedIndex = 0;
        }
        private void PromissoriaCalcularPagamentos()
        {
            Decimal totalcobranca = 0;

            totalcobranca = clsParser.DecimalParse(tbxValortotal.Text);
            // Verifica se irá ou não descontar pis/cofins
            Boolean descontapis;
            Boolean descontacofins;
            descontapis = false; //(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTAPISPASEPSAI from cliente where id=" + nfcompra_idfornecedor) == "S");
            descontacofins = false; //(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTACOFINSSAI from cliente where id=" + nfcompra_idfornecedor) == "S");

            clsCondpagtoBLL CondpagtoBLL = new clsCondpagtoBLL();
            clsCondpagtoInfo CondpagtoInfo = CondpagtoBLL.Carregar(idCondPagto, clsInfo.conexaosqldados);
            if (CondpagtoInfo == null)
            {
                throw new Exception("Condição de Pagamento não foi escolhida, deve escolher a Condição de Pagamento antes de calcularos pagamentos.");
            }

            dgvPagamentos.DataSource = null;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT * from PROMISSORIARECEBER  ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
//            sda.SelectCommand.Parameters.Add("IDNOTA", SqlDbType.Int).Value = idNota;
            dtPromissoriaReceber = new DataTable();
            sda.Fill(dtPromissoriaReceber);

            //DataColumn dcId1 = new DataColumn("ID", Type.GetType("System.Int32"));
            //dtPromissoriaReceber.Columns.Add(dcId1);

            if (dtPromissoriaReceberTempDeletar == null)
            {
                dtPromissoriaReceberTempDeletar = new DataTable();
                dtPromissoriaReceberTempDeletar = dtPromissoriaReceber.Copy();
            }
            else
            {
                foreach (DataRow row in dtPromissoriaReceber.Rows)
                {
                    if (row.RowState != DataRowState.Added &&
                        row.RowState != DataRowState.Detached)
                    {
                        dtPromissoriaReceberTempDeletar.Rows.Add(row);
                    }
                }
            }

            dtPromissoriaReceber = clsFinanceiro.GerarFatura(DateTime.Parse(tbxDataemissao.Text),
                                                        totalcobranca,
                                                        0,
                                                        0,
                                                        0,
                                                        false,
                                                        0,
                                                        false,
                                                        0,
                                                        idFormaPagto,
                                                        idCondPagto,
                                                        "N", "S");

            DataColumn dcId = new DataColumn("ID", Type.GetType("System.Int32"));
            DataColumn dcIdNota = new DataColumn("IDNOTA", Type.GetType("System.Int32"));
            DataColumn dcPosicaoRec = new DataColumn("POSICAOREC", Type.GetType("System.Int32"));

            dtPromissoriaReceber.Columns.Add(dcId);
            dtPromissoriaReceber.Columns.Add(dcIdNota);
            dtPromissoriaReceber.Columns.Add(dcPosicaoRec);

            Int32 posicaorec = 1;
            for (Int32 x = 0; x < dtPromissoriaReceber.Rows.Count; x++)
            {
                if (dtPromissoriaReceber.Rows[x].RowState != DataRowState.Detached &&
                    dtPromissoriaReceber.Rows[x].RowState != DataRowState.Deleted)
                {
                    dtPromissoriaReceber.Rows[x]["ID"] = 0;
                    dtPromissoriaReceber.Rows[x]["IDNOTA"] = 1;
                    dtPromissoriaReceber.Rows[x]["POSICAOREC"] = posicaorec;
                    dtPromissoriaReceber.Rows[x]["IDTIPOPAGA"] = idFormaPagto;
                    posicaorec++;
                }
            }

            dgvPagamentos.DataSource = dtPromissoriaReceber;

            clsGridHelper.MontaGrid2(dgvPagamentos, clsNfCompraPagarBLL.dtGridColunas, true);

            // Gravar na tabela [PROMISSORIARECEBER]
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("delete PROMISSORIARECEBER", scn);
            scn.Open();
            sdr = scd.ExecuteReader();
            scn.Close();

            clsPromissoriaReceberBLL clsPromissoriaReceberBLL = new clsPromissoriaReceberBLL();
            clsPromissoriaReceberInfo clsPromissoriaReceberInfo = new clsPromissoriaReceberInfo();
            foreach (DataRow row in dtPromissoriaReceber.Rows)
            {
                clsPromissoriaReceberInfo.boletonro = row["boletonro"].ToString();
                clsPromissoriaReceberInfo.data = clsParser.DateTimeParse(tbxDataemissao.Text);
                clsPromissoriaReceberInfo.datavencimento = clsParser.SqlDateTimeParse(row["data"].ToString()).Value;
                clsPromissoriaReceberInfo.dv = row["dv"].ToString();
                clsPromissoriaReceberInfo.idcliente = idCliente;
                clsPromissoriaReceberInfo.idtipopaga = 0;
                clsPromissoriaReceberInfo.posicao = clsParser.Int32Parse(row["posicao"].ToString());
                clsPromissoriaReceberInfo.posicaofim = clsParser.Int32Parse(row["posicaorec"].ToString());
                clsPromissoriaReceberInfo.tipopaga = row["tipopaga"].ToString();
                clsPromissoriaReceberInfo.valor = clsParser.DecimalParse(row["valor"].ToString());
                clsPromissoriaReceberInfo.id = clsPromissoriaReceberBLL.Incluir(clsPromissoriaReceberInfo, clsInfo.conexaosqldados);
            }
        }

        private void btnIdDestinatario_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDestinatario.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", idCliente);
            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }

    }
}