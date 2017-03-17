using UnityEngine;
using System.Collections;

public class InitBird : MonoBehaviour {

	private bool scared = false;
	private Vector2 speedVector;

	public Sprite asteroid;

	/**
	 * float y
	 * float speed
	 * SideDirection direction
	 */ 
	void Init(object[] args){
		float y = (float) args [0];
		float speed = (float) args [1];
		SideDirection direction = (SideDirection) args [2];

		if (direction == SideDirection.GOING_LEFT) {
			speed *= -1;
			transform.rotation = Quaternion.Euler (new Vector3 (0f, 180f, 0f));
		}
		transform.position = new Vector2 (direction == SideDirection.GOING_RIGHT ? Values.leftBirdSpawn : Values.rightBirdSpawn,
		                                 y);
		speedVector = new Vector2 (speed, float.MinValue);
		SendMessage ("SetSpeed", speedVector);

		if (Values.GetPointCounter ().GetPoints () > Values.birdAsteroidFrom * Values.scoreMultiplier) {
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			sr.sprite = asteroid;
			transform.localScale = new Vector3 (0.15f, 0.15f, 1f);
			CircleCollider2D c = gameObject.AddComponent<CircleCollider2D> ();
			c.radius = 2.14f;
			gameObject.AddComponent<Rotator> ().rotSpeed = 1.4f;
			Destroy (GetComponent<EdgeCollider2D> ());
		}
	}

	/**
	 * used with the explo power
	 */
	void Scare(){
		GameObject player = Values.GetPlayer ();
		if (!scared && transform.position.y > player.transform.position.y) {
			scared = true;
			SendMessage("SetSpeed", -speedVector);
		}
	}
}
