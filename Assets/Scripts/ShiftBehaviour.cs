using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShiftBehaviour : MonoBehaviour {

	[SerializeField] GameObject m_Section;
	[SerializeField] float m_Offset;
	[SerializeField] float m_Time;
	[SerializeField] iTween.EaseType m_EaseType;

	SectionBehaviour _sectionBehaviour;

	void Start ()
	{
		_sectionBehaviour = GetComponent<SectionBehaviour> ();

		float flip = Mathf.Pow (-1f, Random.Range (0, 2));
		Vector3 position = m_Section.transform.localPosition;
		position.x *= flip;
		m_Offset *= flip;
		m_Section.transform.localPosition = position;

		StartCoroutine (WaitAndAnimate ());
	}

	IEnumerator WaitAndAnimate ()
	{
		yield return new WaitForEndOfFrame ();
		while (!_sectionBehaviour.InSight)
			yield return null;

		iTween.MoveBy (m_Section, iTween.Hash ("amount", new Vector3 (m_Offset, 0f, 0f), "time", m_Time, "easetype", m_EaseType, "space", Space.Self));
	}
}
