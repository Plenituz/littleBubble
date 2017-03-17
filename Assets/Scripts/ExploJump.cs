using UnityEngine;
using System.Collections;

public class ExploJump : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("ExploJumping");
	}

	IEnumerator ExploJumping(){
		Values.playerSpeed += Values.exploJump;
		MonoBehaviour[] birds = (MonoBehaviour[]) FindObjectsOfType(typeof(InitBird));
		foreach (MonoBehaviour bird in birds) {
			bird.SendMessage("Scare");
		}
		yield return new WaitForSeconds (Values.exploJumpTime);
		Values.playerSpeed -= Values.exploJump;
		Destroy (gameObject);
	}
}
