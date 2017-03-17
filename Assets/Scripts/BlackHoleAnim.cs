using UnityEngine;
using System.Collections;

public class BlackHoleAnim : MonoBehaviour {

	void Update () {
		transform.localScale = new Vector3 (
			transform.localScale.x > 0 ? transform.localScale.x - Values.blackholeAnimStep : 2f,
			transform.localScale.y > 0 ? transform.localScale.y - Values.blackholeAnimStep : 2f,
			1f);
	}
}
