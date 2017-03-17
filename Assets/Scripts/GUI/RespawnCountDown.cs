using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RespawnCountDown : MonoBehaviour {
	private Text text;
	private bool counting = false;

	void Start () {
		text = GetComponent<Text> ();
	}
	
	void Update () {
		if (!counting && GetComponentInParent<Canvas> ().enabled) {
			StartCoroutine ("CountDown");
			counting = true;
		}
	}

	IEnumerator CountDown(){
		for (int i = Values.respawnCountDown; i > -1; i--) {
			if (!(GetComponentInParent<Canvas> ().enabled)) {
				StopCoroutine ("CountDown");
				counting = false;
				yield return null;
			}
			text.text = i + "";
			yield return StartCoroutine (WaitForRealSeconds (1));
		}
		counting = false;
		GameController g = (GameController)FindObjectOfType(typeof(GameController));
		g.SendMessage ("PlayerDied");
	}

	IEnumerator WaitForRealSeconds(float time){
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time) {
			yield return null;
		}
	}
}
