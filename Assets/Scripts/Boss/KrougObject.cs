using UnityEngine;
using System.Collections;

public class KrougObject : MonoBehaviour {
	Vector2 velocityVector;
	Rigidbody2D rg;
	float right;
	float left;
	float up;
	float down;

	bool entered = false;
	bool exitBounce = true;
	bool init = true;
	/**
	 * Vector2 position
	 * float startAngle
	 * float speed
	 */
	void Init(object[] args){
		Vector2 position = (Vector2)args [0];
		float startAngle = (float)args [1];
		float speed = (float)args [2];

		GetComponent<Rotator> ().rotSpeed = Random.Range (1f, 3f);
		rg = GetComponent<Rigidbody2D> ();
		CircleCollider2D coll = GetComponent<CircleCollider2D> ();

		transform.position = position;

		ComplexNumber c = new ComplexNumber (speed, startAngle, ComplexNumber.GEOMETRICAL);
		velocityVector = new Vector2 (c.getRealPart (), c.getImaginaryPart ());
		rg.velocity = velocityVector;

		right = (6.629f - coll.bounds.extents.x);
		left = (-0.357f + coll.bounds.extents.x);
		up = (10.596f - coll.bounds.extents.y);
		down = (-0.584f +  coll.bounds.extents.y);
	}

	void FixedUpdate(){
		if (entered) {
			if(exitBounce)
				if (transform.position.y >= up || transform.position.y <= down) {
					velocityVector = new Vector2 (velocityVector.x, -velocityVector.y);
					rg.velocity = velocityVector;
				}
		}
		if (exitBounce) {
			if (transform.position.x >= right || transform.position.x <= left) {
				velocityVector = new Vector2 (-velocityVector.x, velocityVector.y);
				rg.velocity = velocityVector;
			}
		}
		if (init) {
			if (transform.position.x < right && transform.position.x > left && transform.position.y < up && transform.position.y > down) {
				entered = true;
				init = false;
			}
		}
	}

	void LetDie(){
		exitBounce = false;
	}
}
