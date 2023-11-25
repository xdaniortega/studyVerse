using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BH_Video : MonoBehaviour
{
	public VideoPlayer mVideoPlayer;
	public GameObject mVideoDisplay;
	public string mVideoURL;
	public string mVideoDescription;

	public Button mPlayButton;
	private BH_CourseVideoManager CourseManager;

	public void SetCourseManager(in BH_CourseVideoManager aInputManager)
    {
		CourseManager = aInputManager;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
		mPlayButton.onClick.AddListener(() => StartVideo());

		mVideoDisplay.SetActive(false);
		mPlayButton.GetComponentInChildren<TMP_Text>().text = "LOADING...";
		mPlayButton.interactable = false;

		mVideoPlayer.playOnAwake = false;
		mVideoPlayer.source = VideoSource.Url;
		mVideoPlayer.url = mVideoURL;
		mVideoPlayer.Prepare();
		mVideoPlayer.prepareCompleted += OnVideoLoaded;
		mVideoPlayer.loopPointReached += CourseManager.OnVideoFinished;
	}

	protected virtual void OnDisable()
    {
		Debug.Log("Video base disabled");
		mVideoPlayer.prepareCompleted -= OnVideoLoaded;
		mVideoPlayer.loopPointReached -= CourseManager.OnVideoFinished;
		mPlayButton.onClick.RemoveAllListeners();
	}

	// Update is called once per frame
	protected virtual void Update()
    {
        
    }

	void OnVideoLoaded(VideoPlayer source)
	{
		Debug.Log("Video ready");
		mPlayButton.interactable = true;
		mPlayButton.GetComponentInChildren<TMP_Text>().text = "START";
	}

	public void StartVideo()
	{
		Debug.Log("Video started with start button");
		mVideoPlayer.Play();
		mPlayButton.transform.parent.gameObject.SetActive(false);
		//mPlayButton.gameObject.SetActive(false);
		mVideoDisplay.SetActive(true);
	}
}
