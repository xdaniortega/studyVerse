using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BH_CameraFollow : MonoBehaviour
{
	public GameObject mPlayer;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		this.transform.LookAt(mPlayer.transform);
	}
}
