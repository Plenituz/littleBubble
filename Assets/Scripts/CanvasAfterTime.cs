using UnityEngine;
using System.Collections;

public class CanvasAfterTime : MonoBehaviour {
	public float time;

	void Start () {
		StartCoroutine ("f");
	}

	IEnumerator f(){
		yield return new WaitForSeconds (time);
		gameObject.AddComponent<FadeOutCanvas> ();
	}
}
