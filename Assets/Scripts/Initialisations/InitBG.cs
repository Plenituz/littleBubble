using UnityEngine;
using System.Collections;

public class InitBG : MonoBehaviour {

	void FixedUpdate () {
		SendMessage("SetSpeed", new Vector2(0, -Values.playerSpeed)/10);
	}
}
