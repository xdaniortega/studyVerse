using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        await GetContractData();
    }

    static async Task GetContractData()
    {
        using (HttpClient client = new HttpClient())
        {
            // Specify the contract ID, function name, and function arguments
            string contractId = "con_6a9420a6-7311-43fb-8c13-93c3148bb977";
            string functionName = "scoreBoard";
            string functionArgs = "0xACa471334576a8ED39B6Ad57097C84eE74078Fb7";

            // Construct the URL with path and query parameters
            string apiUrl = $"https://api.openfort.xyz/v1/contracts/{contractId}/read?functionName={functionName}&functionArgs={functionArgs}";

            // Make the GET request
            var response = await client.GetAsync(apiUrl);

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                // Read and process the response content as needed
                string responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Data: " + responseData);
            }
            else
            {
                Console.WriteLine("Error in the request. Status Code: " + response.StatusCode);
            }
        }
    }
}
