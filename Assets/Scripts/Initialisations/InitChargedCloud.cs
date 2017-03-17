using UnityEngine;
using System.Collections;

public class InitChargedCloud : MonoBehaviour {

	/**
	 * float x
	 * float y
	 * 
	 */
	void Init(object[] args){
		float x = (float)args [0];
		float y = (float)args [1];

		transform.position = new Vector2 (x, y);
		SendMessage("SetSpeed", new Vector2(0, float.MinValue));
	}
}
