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
                { "interactions[0][contract]", "con_66650eaa-f936-4946-bbbb-8b3341e9f3a9" },
                { "interactions[0][functionName]", "mintNFTs" },
                { "interactions[0][functionArgs][0]", "0" },
                { "interactions[0][value]", "100000000000000" }
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
/*curl https://api.openfort.xyz/v1/transaction_intents \
  -u sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8: \
  -d player=pla_ed6e5eb0-9036-43a6-b92b-e49c878e23eb \
  -d policy=pol_bf498daf-3108-498f-925c-4cfe6a07f655 \
  -d optimistic=false \
  -d chainId=421613 \
  -d "interactions[0][contract]=con_66650eaa-f936-4946-bbbb-8b3341e9f3a9" \
  -d "interactions[0][functionName]=mintNFTs" \
  -d "interactions[0][functionArgs][0]=0" \
  -d "interactions[0][value]=100000000000000"*/
