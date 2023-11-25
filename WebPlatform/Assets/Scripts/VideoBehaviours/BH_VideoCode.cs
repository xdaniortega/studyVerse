using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class BH_VideoCode : BH_Video
{
    public GameObject       mCodePanel;
    public TMP_InputField   mCodeInputField;

    [Header("Buttons")]
    public Button           mCompileButton;
    public Button           mBackButton;
    public Button           mCheckButton;

    [System.Serializable]
    public struct VideoCodeMarker
    {
        public float StopTimestamp;

        [TextArea(3, 10)]
        public string InitialCode;
    }

    public VideoCodeMarker[] mCodeMarkers;
    private uint mQuizMarkerID;
    private bool mAllMarkersCompleted;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        mCodePanel.SetActive(false);
        mQuizMarkerID = 0;
        mAllMarkersCompleted = false;

        mBackButton.onClick.AddListener(() => OnGoBack());
        mCompileButton.onClick.AddListener(() => OnCompileCode());
        //todo: Add check callback to remove points
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log("Video code disabled");
        mBackButton.onClick.RemoveAllListeners();
    }

    public void OnGoBack()
    {
        Debug.Log("Video went back 1");
        mCodePanel.SetActive(false);

        if (mQuizMarkerID == 0)
        {
            SetClipWithTime(0.0f);
        }
        else
        {
            SetClipWithTime(mCodeMarkers[mQuizMarkerID - 1].StopTimestamp + 0.1f);
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!mAllMarkersCompleted && mVideoPlayer.isPlaying && mVideoPlayer.canSetTime && mVideoPlayer.time >= mCodeMarkers[mQuizMarkerID].StopTimestamp)
        {
            Debug.Log("Marker reached, video time " + mVideoPlayer.time);
            mVideoPlayer.Pause();
            mCodePanel.SetActive(true);
            mCodeInputField.text = mCodeMarkers[mQuizMarkerID].InitialCode;
        }
    }

    public void SetClipWithTime(float time)
    {
        StartCoroutine(SetTimeRoutine(time));
    }


    IEnumerator SetTimeRoutine(float time)
    {
        mAllMarkersCompleted = true;

        mVideoPlayer.Prepare();
        yield return new WaitUntil(() => mVideoPlayer.isPrepared);
        yield return new WaitUntil(() => mVideoPlayer.canSetTime);

        mVideoPlayer.Play();
        yield return new WaitUntil(() => mVideoPlayer.isPlaying);
        mVideoPlayer.time = time;
        yield return new WaitUntil(() => mVideoPlayer.isPrepared);
        yield return new WaitUntil(() => mVideoPlayer.isPlaying);
        yield return new WaitForSecondsRealtime(1);

        mAllMarkersCompleted = false;
    }

    public void OnCompileCode()
    {
        mCodePanel.SetActive(false);
        mVideoPlayer.Play();

        mQuizMarkerID++;
        if (mQuizMarkerID >= mCodeMarkers.Length)
        {
            mAllMarkersCompleted = true;
        }
    }
}
