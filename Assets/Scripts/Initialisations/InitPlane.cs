using UnityEngine;
using System.Collections;

public class InitPlane : MonoBehaviour {
	public Sprite OVNI;
	public EdgeCollider2D ovniCollider;
	public EdgeCollider2D planeCollider;

	/**
	 * float y
	 * float speed
	 * SideDirection direction
	 */ 
	void Init(object[] args){
		float y = (float) args [0];
		float speed = (float) args [1];
		SideDirection direction = (SideDirection) args [2];

		//GameObject smoke = Instantiate (Values.v.planeSmoke) as GameObject;
		//smoke.SendMessage ("Init", new object[]{gameObject});

		if (direction == SideDirection.GOING_LEFT)
			speed *= -1;
		transform.position = new Vector2 (direction == SideDirection.GOING_RIGHT ? Values.leftPlaneSpawn : Values.rightPlaneSpawn,
		                                 y);
		SendMessage ("SetSpeed", new Vector2 (speed, float.MinValue));
		if(direction == SideDirection.GOING_LEFT)
			transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, 180f));

		if (Values.GetPointCounter ().GetPoints () > Values.planeOVNIFrom * Values.scoreMultiplier) {
			transform.localScale = new Vector3 (0.45f, 0.45f, 1f);
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			sr.sprite = OVNI;
			Destroy (planeCollider);
			ovniCollider.enabled = true;
		}
	}
}
