using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class IOpenfort : MonoBehaviour
{
    void LoginUser()
    {
        StartCoroutine(LoginRequest());
    }

    IEnumerator LoginRequest()
    {
        string url = "https://api.openfort.xyz/iam/v1/auth/login";
        string jsonBody = "{ \"email\": \"test@example.com\", \"password\": \"test\" }";

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(url, jsonBody))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer pk_test_4955365a-4ddb-56f7-aadf-3cd756de8f7c");

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
}