using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BH_PlayerMovement : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;
	public readonly string CLICK_WALK_TAG = "Walkeable";
	public readonly string CLICK_BUILDING_TAG = "InterBuilding";
	public readonly string BUILDING_DEST_POINT = "BuildingDestPoint";
	private bool mEnableMovement;

	public delegate void PathCompleted(in string aSceneToLoad);
	PathCompleted OnPathCompleted;
	private string mSceneToLoad;

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
		if (mEnableMovement)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit))
				{
					// Check if the clicked point is on the NavMesh
					if (hit.collider.CompareTag(CLICK_WALK_TAG))
					{
						// Set the destination for the NavMeshAgent
						navMeshAgent.SetDestination(hit.point);
					}
					else if(hit.collider.CompareTag(CLICK_BUILDING_TAG))
					{
						BH_Building DestChildren = hit.transform.gameObject.GetComponent<BH_Building>();
						if(DestChildren != null)
						{
							mSceneToLoad = DestChildren.mSceneToLoad;				
							navMeshAgent.SetDestination(DestChildren.mDestinationPoint.transform.position);
							OnPathCompleted = TestLog;
						}
						else
						{
							Debug.LogError("No destination children found.");
						}
					}
				}
			}

			if(OnPathCompleted != null && navMeshAgent.hasPath && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				OnPathCompleted(mSceneToLoad);
				OnPathCompleted = null;
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

	public void TestLog(in string aSceneToLoad)
	{
		SceneManager.LoadScene(aSceneToLoad, LoadSceneMode.Single);
	}
}