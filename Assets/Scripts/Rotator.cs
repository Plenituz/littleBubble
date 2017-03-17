using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public float rotSpeed;

	void FixedUpdate () {
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles + new Vector3 (0f, 0f, rotSpeed));
	}
}
