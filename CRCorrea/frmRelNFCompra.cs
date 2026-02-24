using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

using CrystalDecisions.Shared;

namespace CRCorrea
{
    public partial class frmRelNFCompra : Form
    {
        String Cabecalho;
        public frmRelNFCompra()
        {
            InitializeComponent();
        }
        public void Init()
        {
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "PECASCLASSIFICA", "CODIGO", "", tbxItemGrupo);
        }
        private void frmRelNFCompra_Load(object sender, EventArgs e)
        {
            tbxItemDtEmissaoDe.Text = "01/" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Today.Year.ToString();
            tbxItemDtEmissaoAte.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");

            lbxResOrdem.SelectedIndex = 0;
        }
        private void frmRelNFCompra_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void tspItemImprimir_Click(object sender, EventArgs e)
        {

            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();

            ParameterFields parameters = new ParameterFields();
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();

            field.Name = "EMPRESA";
            valor.Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome FROM EMPRESA where id=" + clsInfo.zempresaid + " ");
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            field = new ParameterField();
            field.Name = "CABECALHO";
            valor = new ParameterDiscreteValue();
            Cabecalho = "";
            if (rbnCabecalho.Checked == true)
            {
                Cabecalho = "Notas de Entrada Resumo do Periodo de : " ;
                Cabecalho = Cabecalho + tbxItemDtEmissaoDe.Text + " ate " + tbxItemDtEmissaoAte.Text;
            }
            else if (rbnDetalhes.Checked == true)
            {
                Cabecalho = "Entrada de Notas Detalhada ";
                Cabecalho = Cabecalho + tbxItemDtEmissaoDe.Text + " ate " + tbxItemDtEmissaoAte.Text;
            }
            else if (rbnApuracoes.Checked == true)
            {
                Cabecalho = "Apurações";
            }
            valor.Value = Cabecalho;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Da Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoDe";
            valor = new ParameterDiscreteValue();
            if (tbxItemDtEmissaoDe.Text.Trim() == "")
            {
                tbxItemDtEmissaoDe.Text = "01/01/1900";
            }
            valor.Value = tbxItemDtEmissaoDe.Text;
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
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Fornecedor
            field = new ParameterField();
            field.Name = "Fornecedor";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxItemFornecedorDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            if (rbnCabecalho.Checked == true)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "NFCOMPRA_CABECALHO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else if (rbnDetalhes.Checked == true)
            {
                if (lbxResOrdem.SelectedIndex == 0)
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "NFCOMPRA_ITENS.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                }
                else if (lbxResOrdem.SelectedIndex == 1) // por codigo
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "NFCOMPRA_ITENS.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                }
                else if (lbxResOrdem.SelectedIndex == 2)  // por fornecedor
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "NFCOMPRA_ITENS.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                }
                else if (lbxResOrdem.SelectedIndex == 3)  // por descrição material
                {
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "NFCOMPRA_ITENS_NOME.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                }

                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else if (rbnApuracoes.Checked ==true)          
            { 

            }
        }

        private void tspItemRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnItemFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnItemFornecedorDe";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnItemFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnItemFornecedorAte";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }
        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1)
            {
                if (clsInfo.znomegrid == btnItemFornecedorDe.Name)
                {
                    tbxItemFornecedorDe.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    //tbxItemFornecedorDe.Select();
                    //tbxItemFornecedorDe.SelectAll();
                }
            }
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
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

        private void btnItemCodDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnItemCodDe";
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnItemCodAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnItemCodAte";
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnItemGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "btnItemGrupoDe";
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }

        private void rbnCabecalho_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnDetalhes_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}

