using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        mPurchasePanel.SetActive(false);
        mPurchaseBuildingButton.onClick.RemoveAllListeners();
        mLockModel.SetActive(false);
        mLockMaterial.SetInteger("IsLocked", 0);
        SceneManager.LoadSceneAsync(aUnlock, LoadSceneMode.Single);
        //mPlayer.mEnableMovement = true;
    }
}
