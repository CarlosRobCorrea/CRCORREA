using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{

    public partial class frmCadastrarCNPJ : Form
    {
        private DataTable dtCNPJ;

        DataTable dtCliente;

        clsClienteBLL clsClienteBLL = new clsClienteBLL();

        BackgroundWorker bwrCNPJ;
        Boolean carregandoCNPJ;
        String NumeroCNPJNew = "";

        public frmCadastrarCNPJ()
        {
            InitializeComponent();
        }
        public void Init()
        {
            label1.Text = "";
        }

        private void frmCadastrarCNPJ_Load(object sender, EventArgs e)
        {
            dtCNPJ = new DataTable();
            dtCNPJ.Columns.Add("cnpj", System.Type.GetType("System.String"));

            CriarNumeroCnpj();

        }


        private void CriarNumeroCnpj()
        {
            dtCliente = new DataTable();
            dtCliente = Procedure.RetornaDataTable(clsInfo.conexaosqldados, "Select * from cliente order by cgc");
            Int32 NumFilial = 100;
            String NumCNPJ = "";
            foreach (DataRow row in dtCliente.Rows)
            {

                if (clsParser.DecimalParse(row["cgc"].ToString()) >= 38029184010000 && clsParser.DecimalParse(row["cgc"].ToString()) < 38029185000000)
                {
                    if (clsParser.Int32Parse(row["cgc"].ToString().Substring(8, 4)) == NumFilial)
                    {
                        // Não Grava porque não pode usar este numero
                        NumFilial = NumFilial + 1;
                    }
                    else
                    {
                        if (clsParser.Int32Parse(row["cgc"].ToString().Substring(8, 4)) < NumFilial)
                        {

                        }
                        else if (clsParser.Int32Parse(row["cgc"].ToString().Substring(8, 4)) > NumFilial)
                        {
                            NumCNPJ = "38029184" + NumFilial.ToString();
                            DataRow rowNew = dtCNPJ.NewRow();
                            rowNew["cnpj"] = row["cgc"].ToString();
                            dtCNPJ.Rows.Add(rowNew);
                        }
                    }
                }
            }
            dtCNPJ.AcceptChanges();

            NumeroCNPJNew = "38.029.184/" + NumFilial.ToString().PadLeft(4, '0');

            txtTexto_CNPJ.Text = NumeroCNPJNew;

            String cnpj= NumeroCNPJNew;

            // Criar o Digito Verificador
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            //if (cnpj.Length != 14)
                //MessageBox.Show("Quantidade de Digitos Incorreto");

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            txtTexto_CNPJ_Dig.Text = digito;
            txtCNPJ.Text = NumeroCNPJNew + "-" + txtTexto_CNPJ_Dig.Text;

        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTexto_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
