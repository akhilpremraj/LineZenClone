using UnityEngine;
using System.Collections;

public class EnableGravity : MonoBehaviour {

	[SerializeField] GameObject m_GravityParent;

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

		yield return new WaitForSeconds (0.35f);

		foreach (Rigidbody rigidbody in m_GravityParent.GetComponentsInChildren<Rigidbody> ())
			rigidbody.useGravity = true;
	}
}
