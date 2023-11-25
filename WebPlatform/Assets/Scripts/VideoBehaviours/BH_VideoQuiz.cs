using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BH_VideoQuiz : BH_Video
{
	private static System.Random rng = new System.Random();

	[Serializable]
	public struct VideoQuizMarker
	{
		public float mStopTimestamp;
		public string mQuestion;
		public string mWrong1;
		public string mWrong2;
		public string mWrong3;
		public string mCorrectAnswer;
	}

	public VideoQuizMarker[] mQuizMarkers;
	private uint mQuizMarkerID;
	private bool mAllMarkersCompleted;

	public GameObject mQuizPanel;
	public Button[] mButtons;
	public TMP_Text mQuizTitle;



	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		mQuizPanel.SetActive(false);
		mQuizMarkerID = 0;
		mAllMarkersCompleted = false;
    }

	// Update is called once per frame
	protected override void Update()
    {
		base.Update();

		if (!mAllMarkersCompleted && mVideoPlayer.isPlaying && mVideoPlayer.time >= mQuizMarkers[mQuizMarkerID].mStopTimestamp)
		{
			Debug.LogWarning("Video paused // " + mQuizMarkers[mQuizMarkerID].mStopTimestamp + " // " + mVideoPlayer.time);
			mVideoPlayer.Pause();

			List<string> Answers = new List<string> { mQuizMarkers[mQuizMarkerID].mWrong1, mQuizMarkers[mQuizMarkerID].mWrong2, mQuizMarkers[mQuizMarkerID].mWrong3, mQuizMarkers[mQuizMarkerID].mCorrectAnswer };
			Shuffle<string>(Answers);

			mQuizTitle.text = mQuizMarkers[mQuizMarkerID].mQuestion;
			mQuizPanel.SetActive(true);
			for (int i = 0; i < mButtons.Length; ++i)
			{
				mButtons[i].GetComponentInChildren<TMP_Text>().text = Answers[i];
				string Test = Answers[i];
				mButtons[i].onClick.AddListener(() => CheckAnswer(Test));
			}
		}
    }

	public void CheckAnswer(string aInput)
	{
		Debug.Log("Answer: " + aInput);
		AnswerCompleted();
	}

	public void AnswerCompleted()
	{
		for (int i = 0; i < mButtons.Length; ++i)
		{
			mButtons[i].onClick.RemoveAllListeners();
		}
		mQuizPanel.SetActive(false);
		mVideoPlayer.Play();

		mQuizMarkerID++;
		if(mQuizMarkerID >= mQuizMarkers.Length)
		{
			mAllMarkersCompleted = true;
		}
	}

	public static void Shuffle<T>(IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
