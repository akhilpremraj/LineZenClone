using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShrinkBehaviour : MonoBehaviour {

	[SerializeField] List<GameObject> m_WallList;
	[SerializeField] float m_Offset;
	[SerializeField] float m_Time;
	[SerializeField] iTween.EaseType m_EaseType;

	SectionBehaviour _sectionBehaviour;

	void Start ()
	{
		_sectionBehaviour = GetComponent<SectionBehaviour> ();
		StartCoroutine (WaitAndAnimate ());
	}

	IEnumerator WaitAndAnimate ()
	{
		yield return new WaitForEndOfFrame ();
		while (!_sectionBehaviour.InSight)
			yield return null;
		
		foreach (GameObject obj in m_WallList)
			iTween.MoveBy (obj, iTween.Hash ("amount", new Vector3 (m_Offset, 0f, 0f), "time", m_Time, "easetype", m_EaseType, "space", Space.Self));
	}
}
