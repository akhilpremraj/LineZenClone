using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : Singleton<LevelGenerator> {

	[SerializeField] List<GameObject> m_LevelSections;
	[SerializeField] List<GameObject> m_SpecialSections;
	[SerializeField] int m_MaxSections;
	[SerializeField] int m_SpecialSectionInterval;

	[SerializeField] float m_Speed;

	List<GameObject> _currentSections;
	Vector3 _startPosition;
	int _sectionCount;

	public void StartGame ()
	{
		if (_currentSections != null)
		{
			foreach (GameObject obj in _currentSections)
				GameObject.Destroy (obj);
			_currentSections.Clear ();
			_sectionCount = 0;

			transform.position = _startPosition;
		} 
		else
		{
			_currentSections = new List<GameObject> ();
			_startPosition = transform.position;
		}

		StartCoroutine (AddNewSections ());
	}

	public void AddNewSection ()
	{
		GameObject newSection;

		if(_sectionCount != 0 && _sectionCount % m_SpecialSectionInterval == 0)
			newSection = GameObject.Instantiate (m_SpecialSections[Random.Range (0, m_SpecialSections.Count)]) as GameObject;
		else
			newSection = GameObject.Instantiate (_currentSections.Count == 0 ? m_LevelSections[0] : m_LevelSections[Random.Range (1, m_LevelSections.Count)]) as GameObject;

		if (_currentSections.Count != 0)
			newSection.transform.position = _currentSections [_currentSections.Count - 1].transform.Find ("EndPoint").position;	//Every section would have a child named
																																//EndPoint where the next section is to be inserted
		else
			newSection.transform.position = transform.position;

		newSection.transform.parent = transform;
		_currentSections.Add (newSection);
		_sectionCount++;
	}

	void Update ()
	{
		if (GameManager.Instance.IsPlaying)
			transform.position += new Vector3 (0f, Time.deltaTime * m_Speed, 0f);
	}

	IEnumerator AddNewSections ()
	{
		for (int i = 0; i < m_MaxSections; i++)
		{
			AddNewSection ();
			yield return new WaitForEndOfFrame ();
			yield return new WaitForEndOfFrame ();
		}
	}
}
