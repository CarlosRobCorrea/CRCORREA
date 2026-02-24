using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Policy;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


namespace CRCorreaFuncoes
{
    public class APIs
    {
        public static dynamic Requisicao(String Url)
        {
            dynamic json;

            try
            {
                using (HttpClient http = new HttpClient())
                { // envio da url
                    using (HttpResponseMessage Resposta = http.GetAsync(Url).Result)
                    {
                        json = JsonConvert.DeserializeObject(Resposta.Content.ReadAsStringAsync().Result);
                    }
                }

            }
            catch (Exception)
            {

                json = null;
            }


            return json;
        }
        public static dynamic Enviar_Serializar(String Url)
        {
            dynamic json;

            try
            {
                using (HttpClient http = new HttpClient())
                { // envio da url
                    using (HttpResponseMessage Resposta = http.GetAsync(Url).Result)
                    {
                        json = JsonConvert.SerializeObject(Resposta.Content.ReadAsStringAsync().Result);
                    }
                }

            }
            catch (Exception)
            {

                json = null;
            }


            return json;
        }

    }
}
