using UnityEngine;
using System.Collections;

public class BallBehaviour : Singleton<BallBehaviour> {

	[SerializeField] LayerMask m_LayerMask;

	Rigidbody _rigidbody;
	float _mouseOffset;

	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody> ();
	}

	void LateUpdate ()
	{
		if (GameManager.Instance.IsPlaying)
		{
			if (Input.GetMouseButtonDown (0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, 100, m_LayerMask.value))
					_mouseOffset = transform.position.x - hit.point.x;
			}
			else if (Input.GetMouseButton (0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, 100, m_LayerMask.value))
				{
					Vector3 position = transform.position;
					position.x = _mouseOffset + hit.point.x;
					transform.position = position;
				}
			}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (GameManager.Instance.IsPlaying && col.collider.gameObject.tag != "Safe")
		{
			Debug.Log ("Collided with object : " + col.collider.gameObject.name);
			_rigidbody.velocity = col.impulse * -10f;
			GameManager.Instance.StopGame ();
		}
	}

	public void ResetBall ()
	{
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;

		transform.position = Vector3.zero;
	}
}
