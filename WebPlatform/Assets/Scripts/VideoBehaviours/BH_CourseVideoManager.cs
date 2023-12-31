using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BH_CourseVideoManager : MonoBehaviour
{
    [Header("Video Settings")]
    public BH_Video[] mCourseVideos;
    private int mActiveVideo;
    private bool mVideosFinished;

    [Header("Display Settings")]
    public GameObject mCompletedCoursePanel;
    public GameObject mCourseDescriptionPanel;
    public GameObject mInitialCoursePanel;
    public GameObject mLeadboardPanel;
    public TMPro.TMP_Text mCompletedVideoText;
    public Image mBadgeDisplay;
    //public TMPro.TMP_Text mNextVideoText;

    public uint mCourseScore;

    // Start is called before the first frame update
    void Start()
    {
        mCourseScore = 0;
        foreach (BH_Video video in mCourseVideos)
        {
            video.SetCourseManager(this);
        }

        mActiveVideo = -1;
        mVideosFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextVideo()
    {
        if(mVideosFinished || mCourseVideos.Length == 0 || mActiveVideo + 1 >= mCourseVideos.Length)
        {
            mVideosFinished = true;
            mLeadboardPanel.SetActive(true);
            return;
        }

        if(mActiveVideo >= 0)
        {
            mCourseVideos[mActiveVideo].enabled = false;
            mCourseVideos[mActiveVideo].gameObject.SetActive(false);
        }    

        mActiveVideo++;
        mCourseVideos[mActiveVideo].gameObject.SetActive(true);
        mCourseVideos[mActiveVideo].enabled = true;

        mCourseDescriptionPanel.GetComponentInChildren<TMPro.TMP_Text>().text = mCourseVideos[mActiveVideo].mVideoDescription;
        mCourseDescriptionPanel.SetActive(true);

        mInitialCoursePanel.SetActive(false);

    }

    public void OnVideoFinished(VideoPlayer source)
    {
        //Complete video course
        mCourseVideos[mActiveVideo].mVideoPlayer.Stop();
        mCourseVideos[mActiveVideo].mVideoDisplay.SetActive(false);

        //Show next video screen
        mCompletedCoursePanel.SetActive(true);
        mBadgeDisplay.sprite = mCourseVideos[mActiveVideo].mUnlockedBadge;
        mCompletedVideoText.text = mCourseVideos[mActiveVideo].mVideoFinishedDescription;
        //NextVideo();
    }

    public void ButtonNextStep()
    {
        //Claim/mint
        _ = IMetaMask.SendTransactionIntent(mCourseVideos[mActiveVideo].mContractID);
        NextVideo();
        mCompletedCoursePanel.SetActive(false);
    }

    public void FinishCourse()
    {
        _ = IMetaMask.SendTransactionIntent("con_6a9420a6-7311-43fb-8c13-93c3148bb977");
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
