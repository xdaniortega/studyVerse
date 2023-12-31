using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class BH_LogInSignUp : MonoBehaviour
{

	struct sLoginData
    {
		public string playerId;
		public string token;
    }
	public Material mBlurMaterial;
	public float mBlurInitValue;

	[Header("Inputs")]
	public TMP_InputField mEmailInput;
	public TMP_InputField mPasswordInput;

	public GameObject mABadgeDisplay;

	public void OnStateChanged(in GameManager.eGameStates aNewState)
	{
		switch (aNewState)
		{
			case GameManager.eGameStates.Initializing:
				break;
			case GameManager.eGameStates.WaitingLogin:
				this.gameObject.SetActive(true);
				break;
			case GameManager.eGameStates.Logged:
				this.gameObject.SetActive(false);
				break;
			default:
				break;
		}
	}

	// Start is called before the first frame update
	void Awake()
    {
		if(GameManager.Instance.mGameState == GameManager.eGameStates.Logged)
        {
			GameManager.Instance.RetrieveBadges(mABadgeDisplay);
		}


		if(GameManager.Instance.mGameState == GameManager.eGameStates.Initializing /*|| string.IsNullOrEmpty(GameManager.Instance.mPlayerID)*/)
        {
			GameManager.Instance.mStateChanged += OnStateChanged;
			mBlurMaterial.SetFloat("_xBlur", mBlurInitValue);
			mBlurMaterial.SetFloat("_yBlur", mBlurInitValue);
        }
		else
        {
			this.gameObject.SetActive(false);
			mBlurMaterial.SetFloat("_xBlur", 0.0f);
			mBlurMaterial.SetFloat("_yBlur", 0.0f);
		}
    }

	private void OnDestroy()
	{
		GameManager.Instance.mStateChanged -= OnStateChanged;
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void OnLoginPressed()
	{
		//IMetaMask.SendTransactionIntent();
		StartCoroutine(CRLogin());
	}

	IEnumerator CRLogin()
    {
		string url = "https://api.openfort.xyz/iam/v1/auth/login";
		string jsonBody = "{ \"email\": \"" + mEmailInput.text + "\", \"password\": \"" + mPasswordInput.text + "\" }";

		using (UnityWebRequest www = UnityWebRequest.Post(url, jsonBody, "application/json"))
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
				Debug.Log("Login completed");
				GameManager.Instance.ChangeGameState(GameManager.eGameStates.Logged);
			}
		}
	}
}
