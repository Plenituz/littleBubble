using UnityEngine;
using System.Collections;

public class InitProjectile : MonoBehaviour {

	/**
	 * Vector2 startPos
	 * float startAngle
	 * float speed
	 */
	void Init(object[] args){
		Vector2 startPos = (Vector2)args [0];
		float startAngle = (float)args [1];
		float speed = (float)args [2];

		transform.position = startPos;
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, (startAngle + Mathf.PI/2)*Mathf.Rad2Deg));

		Rigidbody2D rg = GetComponent<Rigidbody2D> ();
		ComplexNumber v = new ComplexNumber (speed, startAngle, ComplexNumber.GEOMETRICAL);
		rg.velocity = new Vector2 (v.getRealPart (), v.getImaginaryPart ());

	}
}
