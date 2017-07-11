using UnityEngine;
using System.Collections;

public class BounceBehaviour : MonoBehaviour {

	GameObject _child;

	void Start ()
	{
		_child = transform.Find ("Child").gameObject;
		iTween.MoveBy (_child, iTween.Hash ("amount", new Vector3 (2f, 0f, 0f), "time", Random.Range (1.5f, 3f),
			"easetype", iTween.EaseType.easeInOutQuad, "looptype", iTween.LoopType.pingPong, "space", Space.Self));
	}
}
