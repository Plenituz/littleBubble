using UnityEngine;
using System.Collections;

public class InitLaser : MonoBehaviour {

	/**
	 * float x
	 * float y
	 * int size
	 * float angle
	 */
	void Init(object[] args){
		float x = (float)args [0];
		float y = (float)args [1];
		int size = (int)args [2];
		float angle = (float)args [3];

		SendMessage ("SetSpeed", new Vector2 (0, float.MinValue));
		GameObject left = Instantiate (Values.v.LaserLeftPart) as GameObject;
		left.transform.SetParent (transform);

		for (int i = 1; i < size - 1; i++) {
			GameObject g = Instantiate(Values.v.LaserMiddlePart) as GameObject;
			g.transform.SetParent(transform);
			g.transform.position = new Vector2(i, 0f); 
		}

		GameObject right = Instantiate (Values.v.LaserRightPart) as GameObject;
		right.transform.SetParent (transform);
		right.transform.position = new Vector2 (1 + (size-2), 0f);

		transform.position = new Vector2 (x, y);
		transform.rotation = Quaternion.Euler (0, 0, angle);

	}
}
