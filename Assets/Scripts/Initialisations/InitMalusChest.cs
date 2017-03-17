using UnityEngine;
using System.Collections;

public class InitMalusChest : MonoBehaviour {

	/**
	 * Malus malus
	 * float x
	 * float y
	 */
	void Init(object[] args){
		Malus malus = (Malus)args [0];
		float x = (float)args [1];
		float y = (float)args [2];

		gameObject.name = malus.ToString ();
		transform.position = new Vector2 (x, y);

		SendMessage ("SetSpeed", new Vector2(0f, float.MinValue));
	}
}
