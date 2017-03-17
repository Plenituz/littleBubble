using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	
	public Vector2 speed;
	private bool isXPlayerSpeed = false;
	private bool isYPlayerSpeed = false;

	private Rigidbody2D rg;

	void Start(){
		rg = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		float x = isXPlayerSpeed ? Values.playerSpeed * Mathf.Sign(speed.x) : speed.x;
		float y = isYPlayerSpeed ? Values.playerSpeed * Mathf.Sign(speed.y) : speed.y;
		rg.velocity = new Vector2(x, y);
	}

	void SetSpeed(Vector2 v){
		speed = v;
		if (speed.x == float.MaxValue || speed.x == float.MinValue)
			isXPlayerSpeed = true;
		if (speed.y == float.MaxValue || speed.y == float.MinValue)
			isYPlayerSpeed = true;
	}
}
