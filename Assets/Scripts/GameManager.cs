using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	[SerializeField] CanvasGroup m_MainMenuPanel;
	[SerializeField] CanvasGroup m_ScorePanel;
	[SerializeField] CanvasGroup m_BgPanel;

	[SerializeField] Text m_Score;
	
	public bool IsPlaying { get; set; }

	public float GameTime {
		get {
			return _gameTime;
		}
	}

	AudioSource _audioSource;

	float _gameTime;

	[ContextMenu ("Start Game")]
	public void StartGame ()
	{
		LevelGenerator.Instance.StartGame ();
		BallBehaviour.Instance.ResetBall ();
		IsPlaying = true;
		_gameTime = 0f;
	}

	[ContextMenu ("Stop Game")]
	public void StopGame ()
	{
		IsPlaying = false;
		StartCoroutine (FadePanel (m_MainMenuPanel, true));
		StartCoroutine (FadePanel (m_BgPanel, true, 0.75f));
	}

	void Start ()
	{
		_audioSource = GetComponent<AudioSource> ();
	}

	public void Play ()
	{
		StartCoroutine (FadePanel (m_MainMenuPanel, false));
		StartCoroutine (FadePanel (m_BgPanel, false, 0.75f));
		StartCoroutine (FadePanel (m_ScorePanel, true));
		StartGame ();
	}

	public void Exit ()
	{
		Application.Quit ();
	}

	public void PlayMusic (bool play)
	{
		if (play)
			_audioSource.Play ();
		else
			_audioSource.Pause ();
			
	}

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Space))
		{
			if (IsPlaying)
				StopGame ();
			else
				StartGame ();
		}
		else if (Input.GetKeyUp (KeyCode.Escape))
			StopGame ();

		if (IsPlaying)
		{
			_gameTime += Time.deltaTime;
			m_Score.text = ((int)_gameTime).ToString ();
		}
	}

	IEnumerator FadePanel (CanvasGroup canvasGroup, bool fadeIn, float alpha = 1f)
	{
		float end = fadeIn ? alpha : 0f;
		float delta = fadeIn ? 1f : -1f;

		if (fadeIn)
			canvasGroup.gameObject.SetActive (true);

		while (Mathf.Abs (canvasGroup.alpha - end) > 0.05f)
		{
			canvasGroup.alpha += Time.deltaTime * delta * 2f;
			yield return null;
		}

		canvasGroup.alpha = end;

		if (!fadeIn)
			canvasGroup.gameObject.SetActive (false);
	}
}
