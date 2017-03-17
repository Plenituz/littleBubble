using UnityEngine;
using System.Collections;

public class DestroyOnEmpty : MonoBehaviour {

	void Update () {
		if (transform.childCount == 0)
			Destroy (gameObject);
	}
}
