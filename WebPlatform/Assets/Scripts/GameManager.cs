using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum eGameStates
	{
		Initializing,
		WaitingLogin,
		Logged,
	}

	public static GameManager Instance { get; private set; }
	public eGameStates mGameState;

	public delegate void OnStateChanged(in eGameStates aNewState);
	public event OnStateChanged mStateChanged;

	public string mPlayerID;

	private GameObject mBadgeDisplay;

	private void Awake()
	{
		// If there is an instance, and it's not me, delete myself.

		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
			mPlayerID = string.Empty;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	private void Start()
	{
		ChangeGameState(eGameStates.WaitingLogin);
	}

	public void ChangeGameState(in eGameStates aNewState)
	{
		mGameState = aNewState;
		if(mStateChanged != null)
		{
			mStateChanged(aNewState);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void RetrieveBadges(GameObject aBadgeDisplay)
    {
		mBadgeDisplay = aBadgeDisplay;
		_ = IMetaMask.RequestBadges();
    }

	public void LoadBadges(string badgesJson)
    {
		mBadgeDisplay.SetActive(true);
    }
}
