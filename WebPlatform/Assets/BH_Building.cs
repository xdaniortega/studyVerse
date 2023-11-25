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

    public Camera mMainCamera;
    public Camera mSecondCamera;

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

    public void OnPathLoadScene(in string aSceneToLoad)
	{
        SceneManager.LoadScene(aSceneToLoad, LoadSceneMode.Single);
	}

    public void OnPathActiveGameObject(in string aSceneToLoad)
    {
        if(mActiveGameObject != null)
        {
            mActiveGameObject.SetActive(true);
        }

        mMainCamera.gameObject.SetActive(false);
        mSecondCamera.gameObject.SetActive(true);
    }
}
