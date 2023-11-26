using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        await SendTransactionIntent();
    }

    static async Task SendTransactionIntent()
    {
        using (HttpClient client = new HttpClient())
        {
            // Configurar la URL del endpoint
            string apiUrl = "https://api.openfort.xyz/v1/transaction_intents";

            // Configurar las credenciales de autenticación
            string apiKey = "sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8";
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(apiKey + ":"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

            // Configurar los datos del formulario
            var formData = new Dictionary<string, string>
            {
                { "player", "pla_ed6e5eb0-9036-43a6-b92b-e49c878e23eb" },
                { "policy", "pol_bf498daf-3108-498f-925c-4cfe6a07f655" },
                { "optimistic", "false" },
                { "chainId", "421613" },
                { "interactions[0][contract]", "con_6a9420a6-7311-43fb-8c13-93c3148bb977" },
                { "interactions[0][functionName]", "mintNFTs" },
                { "interactions[0][functionArgs][0]", "1" }
            };

            // Realizar la solicitud POST
            var response = await client.PostAsync(apiUrl, new FormUrlEncodedContent(formData));

            // Manejar la respuesta
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Solicitud exitosa. Código de estado: " + response.StatusCode);
            }
            else
            {
                Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
            }
        }
    }
}
/*

curl https://api.openfort.xyz/v1/transaction_intents \
  -u sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8: \                                                           
  -d player=pla_ed6e5eb0-9036-43a6-b92b-e49c878e23eb \
  -d policy=pol_07cde1b9-5d89-47d3-a872-c5a3f5dc257c\                   
  -d optimistic=false \
  -d chainId=421613 \
  -d "interactions[0][contract]=con_6a9420a6-7311-43fb-8c13-93c3148bb977" \
  -d "interactions[0][functionName]=setScoreForPlayer" \
  -d "interactions[0][functionArgs][0]=0xACa471334576a8ED39B6Ad57097C84eE74078Fb7" \
  -d "interactions[0][functionArgs][1]=89"
*/