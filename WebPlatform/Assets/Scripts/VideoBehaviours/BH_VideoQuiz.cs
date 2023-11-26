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

	public Color CorrectColor;
	public Color WrongColor;



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
			mQuizTitle.text = mQuizMarkers[mQuizMarkerID].mQuestion;
			mQuizPanel.SetActive(true);
			mVideoDisplay.SetActive(false);
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
        foreach (Button button in mButtons)
        {
			button.interactable = false;
        }
		StartCoroutine(AnimateSelectedAnswer(aInput));
	}

	IEnumerator AnimateSelectedAnswer(string aAnswer)
    {
		Button aPressedButton = null;
		Color colorToUse = WrongColor;
		bool WasRight = false;
        foreach (Button button in mButtons)
        {
			if(button.GetComponentInChildren<TMP_Text>().text == aAnswer)
            {
				aPressedButton = button;
				if(aAnswer == mQuizMarkers[mQuizMarkerID].mCorrectAnswer)
                {
					WasRight = true;
					colorToUse = CorrectColor;
                }
				break;
			}
        }

		Color oldColor = aPressedButton.GetComponent<Image>().color;
		aPressedButton.GetComponent<Image>().color = colorToUse;
		yield return new WaitForSecondsRealtime(3);
		aPressedButton.GetComponent<Image>().color = oldColor;
		yield return new WaitForSecondsRealtime(3);
		yield return new WaitForEndOfFrame();

		foreach (Button button in mButtons)
		{
			button.interactable = true;
		}
		if(!WasRight)
        {
			OnGoBack();
        }
		else
        {
			AnswerCompleted();
		}

	}

	public void AnswerCompleted()
	{
		for (int i = 0; i < mButtons.Length; ++i)
		{
			mButtons[i].onClick.RemoveAllListeners();
		}
		mQuizPanel.SetActive(false);
		mVideoPlayer.Play();
		mVideoDisplay.SetActive(true);

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

	public void OnGoBack()
	{
		Debug.Log("Video went back 1");
		mQuizPanel.SetActive(false);
		mVideoDisplay.SetActive(true);

		if (mQuizMarkerID == 0)
		{
			SetClipWithTime(0.0f);
		}
		else
		{
			SetClipWithTime(mQuizMarkers[mQuizMarkerID - 1].mStopTimestamp + 0.1f);
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
}
