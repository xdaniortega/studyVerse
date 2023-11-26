using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class IMetaMask
{
    public static async Task SendTransactionIntent(string aContractID)
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
                { "player", "pla_0ff9e815-555f-4375-a162-7ff907c25a27"},
                { "policy", "pol_bf498daf-3108-498f-925c-4cfe6a07f655" },
                { "optimistic", "false" },
                { "chainId", "421613" },
                { "interactions[0][contract]", aContractID},
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

    public static async Task RequestBadges()
    {
        using (HttpClient client = new HttpClient())
        {
            // Establecer la URL de la API
            string apiUrl = "https://api.openfort.xyz/v1/players/pla_0ff9e815-555f-4375-a162-7ff907c25a27/inventory/nft?chainId=421613";

            // Establecer las cabeceras de la solicitud
            client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8");

            // Realizar la solicitud GET
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            // Manejar la respuesta
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                GameManager.Instance.LoadBadges(responseBody);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}