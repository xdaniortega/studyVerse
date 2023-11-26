/*using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyBehavior : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TransactionIntentRequest());
    }

    IEnumerator TransactionIntentRequest()
    {
        string url = "https://api.openfort.xyz/v1/transaction_intents";
        
        // Nuevo cuerpo de la solicitud
        string jsonBody = "{ \"player\": \"pla_ed6e5eb0-9036-43a6-b92b-e49c878e23eb\", " +
                          "\"policy\": \"pol_bf498daf-3108-498f-925c-4cfe6a07f655\", " +
                          "\"optimistic\": false, " +
                          "\"chainId\": 421613, " +
                          "\"interactions\": [ " +
                          "{ " +
                          "\"contract\": \"con_24db99b9-b887-4bce-8469-65152a4550f5\", " +
                          "\"functionName\": \"mintNFTs\", " +
                          "\"functionArgs\": { \"0\": 1 } " +
                          "} " +
                          "] " +
                          "}";

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonBody))
        {
            // Configurar encabezados
            string apiKey = "sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8";
            www.SetRequestHeader("Authorization", "Bearer " + apiKey);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Request complete! Response: " + www.downloadHandler.text);
            }
        }
    }
}*/

/*
MINTEJAR UN NFT DE 
curl https://api.openfort.xyz/v1/transaction_intents \
  -u sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8: \
  -d player=pla_ed6e5eb0-9036-43a6-b92b-e49c878e23eb \
  -d policy=pol_bf498daf-3108-498f-925c-4cfe6a07f655 \
  -d optimistic=false \
  -d chainId=421613 \
  -d "interactions[0][contract]=con_24db99b9-b887-4bce-8469-65152a4550f5" \
  -d "interactions[0][functionName]=mintNFTs" \
  -d "interactions[0][functionArgs][0]=1"


curl https://api.openfort.xyz/v1/transaction_intents \
  -u YOUR_SECRET_KEY="sk_test_96701b7a-54ea-54c3-b772-c475bb1af2e8" \
  -d player=pla_2e6317a5-9aab-442e-92e0-2385db28775e \
  -d policy=pol_bf498daf-3108-498f-925c-4cfe6a07f655 \
  -d optimistic=true \
  -d chainId=421613 \
  -d "interactions[0][contract]=con_24db99b9-b887-4bce-8469-65152a4550f5" \
  -d "interactions[0][functionName]=mintNFTs" \
  -d "interactions[0][functionArgs][0]=1"
  */

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
                { "interactions[0][contract]", "con_24db99b9-b887-4bce-8469-65152a4550f5" },
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
