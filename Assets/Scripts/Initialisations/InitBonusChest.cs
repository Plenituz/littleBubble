using UnityEngine;
using System.Collections;

public class InitBonusChest : MonoBehaviour {

	/**
	 * Bonus bonus
	 * float x
	 * float y
	 */
	void Init(object[] args){
		Bonus bonus = (Bonus)args [0];
		float x = (float)args [1];
		float y = (float)args [2];

		gameObject.name = bonus.ToString ();
		transform.position = new Vector2 (x, y);

		SendMessage ("SetSpeed", new Vector2(0f, float.MinValue));
	}
}
