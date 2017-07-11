using UnityEngine;
using System.Collections;

public class SectionBehaviour : MonoBehaviour {

	[SerializeField] Vector2 m_HeightRange;
	[SerializeField] Vector2 m_OffsetRange;
	[SerializeField] float m_Width;
	[SerializeField] bool m_Mirrorable;

	float _sectionLength;
	bool _inSight;

	public bool InSight {
		get {
			return _inSight;
		}
	}

	void Start ()
	{
		_sectionLength = transform.Find ("EndPoint").position.y - transform.position.y + 3f;

		//Randomize Section
		Vector3 offset = new Vector3 (Random.Range (m_OffsetRange.x, m_OffsetRange.y), 0f, 0f);
		if (transform.position.x + offset.x + (m_Width / 2f) > 2.5f)			//If too far right, go left
		{
			offset.x *= -1f;
			if (m_Mirrorable)
				transform.Rotate (new Vector3 (0f, 180f, 0f));
		} 
		else if (transform.position.x - offset.x - (m_Width / 2f) < -2.5f)		//If too far left, go right
		{
			offset.x *= 1f;
		}
		else																	//Go either way
		{
			offset *= Mathf.Pow (-1f, Random.Range (0, 2));
			if (m_Mirrorable)
				transform.Rotate (new Vector3 (0f, 180f * Mathf.Pow (-1f, Random.Range (0, 2)), 0f));
		}

		transform.position += offset;

		Vector3 scale = transform.localScale;
		scale.y = Random.Range (m_HeightRange.x, m_HeightRange.y);
		transform.localScale = scale;

		StartCoroutine (CheckIfInSight ());
	}

	IEnumerator CheckIfInSight ()
	{
		if (!_inSight && Mathf.Abs (transform.position.y) < 5f)
			_inSight = !_inSight;
		
		yield return new WaitForSeconds (0.5f);

		if (_inSight && Mathf.Abs (transform.position.y) > _sectionLength)
		{
			LevelGenerator.Instance.AddNewSection ();
			GameObject.Destroy (gameObject);
		}

		StartCoroutine (CheckIfInSight ());
	}
}
