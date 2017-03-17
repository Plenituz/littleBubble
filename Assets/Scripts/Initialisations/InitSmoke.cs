using UnityEngine;
using System.Collections;

public class InitSmoke : MonoBehaviour {
	private GameObject pos;

	void Init(object[] args){
		pos = (GameObject) args [0];
	}
	
	void Update () {
		if (pos == null)
			Destroy (gameObject);
		else
			transform.position = new Vector3 (pos.transform.position.x, pos.transform.position.y, pos.transform.position.z);
	}
}
