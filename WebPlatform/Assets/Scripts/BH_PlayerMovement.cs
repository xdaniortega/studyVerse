using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BH_PlayerMovement : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;
	public readonly string CLICK_COMPARE_TAG = "Walkeable";
	private bool mEnableMovement;
	void Awake()
	{
		// Assuming your NavMeshAgent is attached to the same GameObject
		navMeshAgent = GetComponent<NavMeshAgent>();
		mEnableMovement = false;

		GameManager.Instance.mStateChanged += OnStateChanged;

		// Ensure that NavMeshAgent is not null
		if (navMeshAgent == null)
		{
			Debug.LogError("NavMeshAgent component not found on this GameObject.");
		}
	}

	private void OnDestroy()
	{
		GameManager.Instance.mStateChanged -= OnStateChanged;
	}

	void Update()
	{
		if (mEnableMovement && Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				// Check if the clicked point is on the NavMesh
				if (hit.collider.CompareTag(CLICK_COMPARE_TAG))
				{
					// Set the destination for the NavMeshAgent
					navMeshAgent.SetDestination(hit.point);
				}
			}
		}
	}

	public void OnStateChanged(in GameManager.eGameStates aNewState)
	{
		switch (aNewState)
		{
			case GameManager.eGameStates.Logged:
				mEnableMovement = true;
				break;
			default:
				mEnableMovement	= false;
				break;
		}
	}
}
