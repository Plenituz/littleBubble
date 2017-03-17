using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RespawnLeft : MonoBehaviour {

	void Start () {
		GetComponent<Text> ().text = Values.inventory.respawn + " left";
	}
}
