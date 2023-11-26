using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System;

public class BH_Building : MonoBehaviour
{
    public GameObject mDestinationPoint;

    [Header("Callback values")]
    public string mSceneToLoad;
    public GameObject mActiveGameObject;

    public Camera mMainCamera;
    public Camera mSecondCamera;

    public GameObject mPurchasePanel;

    private BH_PlayerMovement mPlayer;
    public Button mPurchaseBuildingButton;
    public GameObject mLockModel;
    public Material mLockMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if(mMainCamera != null)
        {
            mMainCamera.gameObject.SetActive(true);
        }

        if (mSecondCamera != null)
        {
            mSecondCamera.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPathLoadScene(string aSceneToLoad, in BH_PlayerMovement aPlayer)
	{
        SceneManager.LoadScene(aSceneToLoad, LoadSceneMode.Single);
	}

    public void OnPathActiveGameObject(string aSceneToLoad, in BH_PlayerMovement aPlayer)
    {
        if(mActiveGameObject != null)
        {
            mActiveGameObject.SetActive(true);
        }

        mMainCamera.gameObject.SetActive(false);
        mSecondCamera.gameObject.SetActive(true);
    }

    public void OnUnlockBuilding(string aSceneToLoad, in BH_PlayerMovement aPlayer)
    {
        aPlayer.mEnableMovement = false;
        mPlayer = aPlayer;
        mPurchasePanel.SetActive(true);
        mPurchaseBuildingButton.onClick.AddListener(() => Unlock(aSceneToLoad));
    }

    public void Unlock(string aUnlock)
    {
        _ = SendTransactionIntent();
        mPurchasePanel.SetActive(false);
        mPurchaseBuildingButton.onClick.RemoveAllListeners();
        mLockModel.SetActive(false);
        mLockMaterial.SetInteger("IsLocked", 0);
        SceneManager.LoadSceneAsync(aUnlock, LoadSceneMode.Single);
        //mPlayer.mEnableMovement = true;
    }


    public static async Task SendTransactionIntent()
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
                { "player", "pla_0ff9e815-555f-4375-a162-7ff907c25a27" },
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
