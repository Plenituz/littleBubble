using UnityEngine;
using System.Collections;

public class InitBlackHole : MonoBehaviour {

	private float radius;

	/**
	 * float x
	 * float y
	 * float radius
	 */
	void Init(object[] args){
		float x = (float)args [0];
		float y = (float)args [1];
		float radius = (float)args [2];

		this.radius = radius;
		transform.position = new Vector2 (x, y);
		SendMessage("SetSpeed", new Vector2(0f, float.MinValue));
	}

	void FixedUpdate(){
		GameObject player = ((MonoBehaviour) FindObjectOfType (typeof(PlayerController))).gameObject;
		float distance = Values.dist (player.transform.position.x, player.transform.position.y, transform.position.x, transform.position.y);
		if (distance <= radius) {
			player.transform.position = transform.position + (player.transform.position - transform.position).normalized*(distance - Values.blackholeAttractStep);
		}
	}
}
