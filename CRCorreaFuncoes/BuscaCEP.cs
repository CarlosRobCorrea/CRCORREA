using System.Windows.Forms;

namespace CRCorreaFuncoes
{
   
    public class BuscaCEP
    {
        public string Logradouro = "";
        public string Complemento = "";
        public string Bairro = "";
        public string Localidade = "";
        public string UF = "";
        public string IBGE = "";
        public string DDD = "";
        public string GIA = "";
        public string SIAFI = "";


        public void Buscar(string Cep)
        {
            Cep = Cep.Replace("-", "");
            Cep = Cep.Replace(".", "");
            Cep = Cep.Replace(",", "");
            Cep = Cep.Replace(" ", "");

            if (Cep.Length == 0)
            {
                return;
            }

            if (Cep.Length != 8)
            {
                MessageBox.Show("O numero do CEP deve conter 8 digitos");
                return;
            }

            string url = $"https://viacep.com.br/ws/{Cep}/json/";

            dynamic Retorno = APIs.Requisicao(url);

            if (Retorno == null)
            {
                MessageBox.Show("Erro no site ou na sua conexão");
                return;
            }

            if (Retorno.erro != null)
            {
                MessageBox.Show("O CEP informado não foi localizado");
                return;
            }


            Logradouro = Retorno.logradouro;
            Complemento = Retorno.complemento;
            Bairro = Retorno.bairro;
            Localidade = Retorno.localidade;
            UF = Retorno.uf;
            IBGE = Retorno.ibge;
            DDD = Retorno.ddd;
            GIA = Retorno.gia;
            SIAFI = Retorno.siafi;
        }

    }

}
