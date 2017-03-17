using UnityEngine;
using System.Collections;

public class Parent : MonoBehaviour {

	public Transform parent;
	public Vector3 offset = new Vector3(0f, 0f, 0f);

	void Update () {
		transform.position = parent.position + offset;
	}
}
