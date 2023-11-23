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
		}
	}

	private void Start()
	{
		ChangeGameState(eGameStates.WaitingLogin);
	}

	public void ChangeGameState(in eGameStates aNewState)
	{
		if(mStateChanged != null)
		{
			mStateChanged(aNewState);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
