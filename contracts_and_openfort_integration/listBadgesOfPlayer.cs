using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        await MakeRequest();
    }

    static async Task MakeRequest()
    {
        using (HttpClient client = new HttpClient())
        {
            // Establecer la URL de la API
            string apiUrl = "https://api.openfort.xyz/v1/players/pla_ed6e5eb0-9036-43a6-b92b-e49c878e23eb/inventory/nft?chainId=421613";

            // Establecer las cabeceras de la solicitud
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8");

            // Realizar la solicitud GET
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            // Manejar la respuesta
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}
