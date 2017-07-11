using UnityEngine;
using System.Collections;

public class RoomBehaviour : MonoBehaviour {

	Transform _roomObjects;

	void Start ()
	{
		_roomObjects = transform.Find ("RoomObjects");

		Vector3 position = _roomObjects.position;
		position.x = 0f;
		_roomObjects.position = position;

		_roomObjects.Find ("EndPoint").parent = transform;
	}
}
