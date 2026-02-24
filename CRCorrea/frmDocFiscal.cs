using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;
using System.Drawing;
using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmDocFiscal : Form
    {
        Int32 id;
        clsDocFiscalInfo clsDocFiscalInfo = new clsDocFiscalInfo();
        clsDocFiscalInfo clsDocFiscalInfoOld = new clsDocFiscalInfo();
        clsDocFiscalBLL clsDocFiscalBLL = new clsDocFiscalBLL();

        public frmDocFiscal()
        {
            InitializeComponent();
        }
        public void Init(Int32 _id)
        {
            id = _id;
            clsDocFiscalBLL = new clsDocFiscalBLL();
        }

        private void frmDocFiscal_Load(object sender, EventArgs e)
        {
            DocFiscalCarregar();
        }

        private void frmDocFiscal_Activated(object sender, EventArgs e)
        {

        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }
        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.FormatarCampoNumerico(sender);
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }
        private void ControlKeyDownNumero(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownNumero((TextBox)sender, e);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }
        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void ControlKeyDownTelefone(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownTelefone((TextBox)sender, e);
        }
        void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                //Pegando o Cliente
                //if (clsInfo.znomegrid == btnCliente.Name)
                //{
                //    tbxCliente_Cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();

                //    if (Cliente_Cognome_Check() == true)
                //    {
                //        Cliente_Cognome_Carrega();
                //    }
                //}
                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
            //if (ctl.Name == tbxTransfere_Data.Name)
            //{// Verifica que a data de inicioda escala não pode ser inferior a data da transferencia
            //    if (clsParser.DateTimeParse(tbxTransfere_DataInicio.Text) < clsParser.DateTimeParse(tbxTransfere_Data.Text))
            //    {
            //        MessageBox.Show("A data de inicio da escala não pode ser inferior a data de transferencia");
            //    }
            //}
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    DocFiscalSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    //tbxNumero.Select();
                    //tbxNumero.SelectAll();
                    return;
                }

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DocFiscalCarregar()
        {
            clsDocFiscalInfoOld = new clsDocFiscalInfo();
            clsDocFiscalInfo = new clsDocFiscalInfo();
            if (id == 0)
            {
                clsDocFiscalInfo.ativo = "S";
                clsDocFiscalInfo.codigo = "";
                clsDocFiscalInfo.cognome = "";
                clsDocFiscalInfo.id = id;
                clsDocFiscalInfo.modelo = 0;
                clsDocFiscalInfo.nome = "";
                clsDocFiscalInfo.serie = "";
            }
            else
            {
                clsDocFiscalInfo = clsDocFiscalBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            DocFiscalCampos(clsDocFiscalInfo);
            DocFiscalFillInfo(clsDocFiscalInfoOld);
        }


        void DocFiscalCampos(clsDocFiscalInfo info)
        {
            cbxAtivo.SelectedIndex = clsVisual.SelecionarIndex(info.ativo, 1, cbxAtivo);
            tbxCodigo.Text = info.codigo;
            tbxCogNome.Text= info.cognome;
            id = info.id;
            tbxModelo.Text = info.modelo.ToString();
            tbxNome.Text = info.nome;
            tbxSerie.Text = info.serie;
        }

        void DocFiscalFillInfo(clsDocFiscalInfo info)
        {
            info.ativo = cbxAtivo.Text;
            info.codigo = tbxCodigo.Text;
            info.cognome = tbxCogNome.Text;
            info.id = id;
            info.modelo = clsParser.Int32Parse(tbxModelo.Text);
            info.nome = tbxNome.Text;
            info.serie = tbxSerie.Text;
        }
        private void DocFiscalSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ##################################
                // Tabela: CONTRATO
                clsDocFiscalInfo = new clsDocFiscalInfo();
                DocFiscalFillInfo(clsDocFiscalInfo);

                if (id == 0)
                {
                    clsDocFiscalInfo.id = clsDocFiscalBLL.Incluir(clsDocFiscalInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsDocFiscalBLL.Alterar(clsDocFiscalInfo, clsInfo.conexaosqldados);
                }
                tse.Complete();
            }
        }
    }
}
