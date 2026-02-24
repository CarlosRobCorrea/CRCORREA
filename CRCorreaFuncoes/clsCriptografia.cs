using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaFuncoes
{
    public class clsCriptografia
    {
        public string Criptografar(String _texto)
        {
            Byte valor;
            Byte valor2;

            Int32 textoTamanho;

            Char caracterTemp;

            String chave = "AplisoftAplisoftApli";
            String resultado = "";

            textoTamanho = _texto.Length;

            for (Int32 X = 0; X < _texto.Length; X++)
            {
                valor = byte.Parse(System.Text.Encoding.ASCII.GetBytes(_texto.Substring(X, 1)).GetValue(0).ToString());

                if (X + 1 == _texto.Length)
                {
                    valor2 = byte.Parse(System.Text.Encoding.ASCII.GetBytes(chave.Substring((X + 1) % chave.Length, 1)).GetValue(0).ToString());
                }
                else
                {
                    valor2 = byte.Parse(System.Text.Encoding.ASCII.GetBytes(chave.Substring((X % chave.Length) + 1, 1)).GetValue(0).ToString());
                }

                caracterTemp = Convert.ToChar(valor ^ valor2);

                if (Byte.Parse(System.Text.Encoding.ASCII.GetBytes(caracterTemp.ToString()).GetValue(0).ToString()) == 0 || byte.Parse(System.Text.Encoding.ASCII.GetBytes(caracterTemp.ToString()).GetValue(0).ToString()) == 32)
                {
                    caracterTemp = '_';
                }

                resultado += caracterTemp;
            }

            return resultado.Substring(0, textoTamanho);
        }

    }
}
