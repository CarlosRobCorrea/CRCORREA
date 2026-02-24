using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaFuncoes
{
    public class clsDiversos
    {
        public string NumeroCNPJ = "";

        public void CriarNumeroCnpj(String cgc)
        {
            DataTable dtCNPJ;
            dtCNPJ = new DataTable();
            dtCNPJ.Columns.Add("cnpj", System.Type.GetType("System.String"));

            DataTable dtCliente;
            dtCliente = new DataTable();
            dtCliente = Procedure.RetornaDataTable(clsInfo.conexaosqldados, "Select * from cliente order by cgc");
            Int32 NumFilial = 100;
            String NumCNPJ = "";
            foreach (DataRow row in dtCliente.Rows)
            {

                if (clsParser.DecimalParse(row["cgc"].ToString()) >= 57788830010000 && clsParser.DecimalParse(row["cgc"].ToString()) < 57788831000000)
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
                            NumCNPJ = "57788830" + NumFilial.ToString();
                            DataRow rowNew = dtCNPJ.NewRow();
                            rowNew["cnpj"] = row["cgc"].ToString();
                            dtCNPJ.Rows.Add(rowNew);
                        }
                    }
                }
            }
            dtCNPJ.AcceptChanges();

            String NumeroCNPJNew = "57.788.830/" + NumFilial.ToString().PadLeft(4, '0');

            //txtTexto_CNPJ.Text = NumeroCNPJNew;

            String cnpj = NumeroCNPJNew;

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
            //txtTexto_CNPJ_Dig.Text = digito;
            //txtCNPJ.Text = NumeroCNPJNew + "-" + txtTexto_CNPJ_Dig.Text;
            cgc = NumeroCNPJNew + "-" + digito;
            NumeroCNPJ = cgc;
        }

    }
}
