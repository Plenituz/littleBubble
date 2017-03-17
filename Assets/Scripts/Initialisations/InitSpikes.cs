using UnityEngine;
using System.Collections;

public class InitSpikes : MonoBehaviour {

	/**
	 * float x
	 * float y
	 */
	void Init(object[] args){
		float x = (float) args [0];
		float y = (float)args [1];

		transform.position = new Vector2 (x, y);
		//negative speed so the spike goes down
		SendMessage ("SetSpeed", new Vector2 (0, float.MinValue));
	}
}
