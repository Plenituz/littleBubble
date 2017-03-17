using UnityEngine;
using System.Collections;

public class SpookieEye : MonoBehaviour {
	Transform player;
	Vector2 startPos;
	public float dist = 1f;

	void Start () {
		player = Values.GetPlayer ().transform;
		startPos = transform.position;
	}
	
	void Update () {
		startPos = transform.parent.position;
		Vector2 m = Vector2.MoveTowards (startPos, player.transform.position, dist);
		transform.position = m;
	}
}
