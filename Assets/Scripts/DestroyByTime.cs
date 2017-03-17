using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float time = 2f;

	void Start () {
		StartCoroutine (Timer ());
	}

	IEnumerator Timer(){
		yield return new WaitForSeconds(time);
		Destroy (gameObject);
	}
}
