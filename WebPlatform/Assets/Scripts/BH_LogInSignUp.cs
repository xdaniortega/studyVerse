using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BH_LogInSignUp : MonoBehaviour
{
	public Material mBlurMaterial;
	public float mBlurInitValue;
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
		GameManager.Instance.mStateChanged += OnStateChanged;
		mBlurMaterial.SetFloat("_xBlur", mBlurInitValue);
		mBlurMaterial.SetFloat("_yBlur", mBlurInitValue);
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
		//todo: Implement multi-threaded login with openfort here.
		GameManager.Instance.ChangeGameState(GameManager.eGameStates.Logged);
	}
}
