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

	public Button mPlayButton;
    // Start is called before the first frame update
    protected virtual void Start()
    {
		mPlayButton.interactable = false;
		mPlayButton.GetComponentInChildren<TMP_Text>().text = "LOADING...";
		mPlayButton.gameObject.SetActive(true);
		mVideoDisplay.SetActive(false);
		mPlayButton.onClick.AddListener(() => StartVideo());



		mVideoPlayer.playOnAwake = false;
		mVideoPlayer.source = VideoSource.Url;
		mVideoPlayer.url = mVideoURL;
		mVideoPlayer.Prepare();
		mVideoPlayer.prepareCompleted += OnVideoLoaded;
		mVideoPlayer.loopPointReached += OnVideoFinished;
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
		mPlayButton.gameObject.SetActive(false);
		mVideoDisplay.SetActive(true);
	}

	void OnVideoFinished(VideoPlayer source)
	{
		Debug.Log("Video finished");
	}
}
