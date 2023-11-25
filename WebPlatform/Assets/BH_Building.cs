using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BH_Building : MonoBehaviour
{
    public GameObject mDestinationPoint;

    [Header("Callback values")]
    public string mSceneToLoad;
    public GameObject mActiveGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPathLoadScene(in string aSceneToLoad)
	{

        SceneManager.LoadScene(aSceneToLoad, LoadSceneMode.Single);
	}

    public void OnPathActiveGameObject()
    {

    }
}
