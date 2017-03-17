using UnityEngine;
using System.Collections;

public class InitTrampo : MonoBehaviour {

	private int direction;

	/**
	 * float x
	 * float y
	 * SideDirection direction
	 */
	void Init(object[] args){
		float x = (float)args [0];
		float y = (float)args [1];
		SideDirection direction = (SideDirection)args [2];

		if (direction == SideDirection.GOING_LEFT) {
			this.direction = -1;
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
		} else {
			this.direction = 1;
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -45f));
		}
		transform.position = new Vector2 (x, y);

		SendMessage ("SetSpeed", new Vector2(0, float.MinValue));

//		GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0f, 1f), Random.Range (0, 1f), Random.Range (0, 1f), 1f);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			other.SendMessage("AddVelocityBump", Values.bumpVelocity * direction);
			transform.GetChild (0).gameObject.SetActive (true);
		}
	}
}
