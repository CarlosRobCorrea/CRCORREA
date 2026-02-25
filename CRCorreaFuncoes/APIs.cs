using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace CRCorreaFuncoes
{
    public class FocusNFeResponse
    {
        public String status { get; set; }
        public String status_sefaz { get; set; }
        public String mensagem_sefaz { get; set; }
        public String chave_nfe { get; set; }
        public String numero { get; set; }
        public String serie { get; set; }
        public String caminho_xml_nota_fiscal { get; set; }
        public String caminho_danfe { get; set; }
        public String qrcode_url { get; set; }
        public String url_consulta { get; set; }

        // campos de erro
        public String codigo { get; set; }
        public String mensagem { get; set; }
        public String resposta_raw { get; set; }
    }

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

        /// <summary>
        /// Emite NFC-e via API Focus NFe com autenticacao Basic Auth.
        /// </summary>
        /// <param name="jsonNfce">JSON ja serializado com os dados da NFC-e</param>
        /// <param name="referencia">Referencia unica da nota (ex: "PED2025-00001")</param>
        /// <returns>FocusNFeResponse com status da emissao</returns>
        public static FocusNFeResponse EmitirNFCe(String jsonNfce, String referencia)
        {
            FocusNFeResponse resposta = new FocusNFeResponse();

            try
            {
                String token = clsInfo.focusnfe_token;
                String urlBase = clsInfo.focusnfe_url;

                // Focus NFe usa Basic Auth: token como username, senha vazia
                String credenciais = Convert.ToBase64String(Encoding.UTF8.GetBytes(token + ":"));

                String url = urlBase + "/v2/nfce?ref=" + referencia + "&completa=1";

                using (HttpClient http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", credenciais);

                    var content = new StringContent(jsonNfce, Encoding.UTF8, "application/json");

                    HttpResponseMessage httpResponse = http.PostAsync(url, content).Result;
                    String respostaString = httpResponse.Content.ReadAsStringAsync().Result;

                    resposta = JsonConvert.DeserializeObject<FocusNFeResponse>(respostaString);

                    if (resposta == null)
                    {
                        resposta = new FocusNFeResponse();
                        resposta.status = "erro";
                        resposta.mensagem = "Resposta vazia da API. HTTP Status: " + httpResponse.StatusCode;
                    }

                    resposta.resposta_raw = "HTTP " + (int)httpResponse.StatusCode + ": " + respostaString;

                    if (!httpResponse.IsSuccessStatusCode && String.IsNullOrEmpty(resposta.mensagem))
                    {
                        resposta.mensagem = resposta.resposta_raw;
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.status = "erro";
                resposta.mensagem = "Erro ao chamar API Focus NFe: " + ex.Message;
                if (ex.InnerException != null)
                    resposta.mensagem += " | " + ex.InnerException.Message;
            }

            return resposta;
        }

        /// <summary>
        /// Consulta o status de uma NFC-e ja emitida na Focus NFe.
        /// </summary>
        /// <param name="referencia">Referencia unica da nota</param>
        /// <returns>FocusNFeResponse com status atual</returns>
        public static FocusNFeResponse ConsultarNFCe(String referencia)
        {
            FocusNFeResponse resposta = new FocusNFeResponse();

            try
            {
                String token = clsInfo.focusnfe_token;
                String urlBase = clsInfo.focusnfe_url;
                String credenciais = Convert.ToBase64String(Encoding.UTF8.GetBytes(token + ":"));
                String url = urlBase + "/v2/nfce/" + referencia + "?completa=1";

                using (HttpClient http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", credenciais);

                    HttpResponseMessage httpResponse = http.GetAsync(url).Result;
                    String respostaString = httpResponse.Content.ReadAsStringAsync().Result;

                    resposta = JsonConvert.DeserializeObject<FocusNFeResponse>(respostaString);

                    if (resposta == null)
                    {
                        resposta = new FocusNFeResponse();
                        resposta.status = "erro";
                        resposta.mensagem = "Resposta vazia. HTTP Status: " + httpResponse.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.status = "erro";
                resposta.mensagem = "Erro ao consultar NFC-e: " + ex.Message;
            }

            return resposta;
        }
    }
}
