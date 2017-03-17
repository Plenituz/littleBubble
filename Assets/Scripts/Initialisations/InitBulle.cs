using UnityEngine;
using System.Collections;

public class InitBulle : MonoBehaviour {

	/**
	 * float x
	 * float y
	 */
	void Init(object[] args){
		float x = (float)args [0];
		float y = (float)args [1];

		transform.position = new Vector2 (x, y);

		SendMessage("SetSpeed", new Vector2(0f, float.MinValue));
	}

	IEnumerator AbsorbTo(Vector3 pos){
		Destroy (GetComponent<CircleCollider2D> ());
		float distance = Values.dist (pos.x, pos.y, transform.position.x, transform.position.y);
		while (distance > Values.bulleAttractStep && transform.localScale.x > 0) {
			transform.localScale -= new Vector3 (Values.bulleScaleStep, Values.bulleScaleStep, 0);
			distance = Values.dist (pos.x, pos.y, transform.position.x, transform.position.y);
			transform.position = pos + (transform.position - pos).normalized*(distance - Values.bulleAttractStep);
			yield return null;
		}
		Destroy (gameObject);

	}
}
