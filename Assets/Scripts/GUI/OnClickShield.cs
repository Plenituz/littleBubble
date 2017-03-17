using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickShield : MonoBehaviour {
	public Image img;
	public Text t;
	PlayerController controller;

	void Start(){
		controller = Values.GetPlayer ().GetComponent<PlayerController> ();
	}

	void OnClick(){
		if (!controller.shielded && Values.inventory.shield > 0) {
			controller.shielded = true;
			Values.inventory.shield--;
			Values.SaveInventory ();
			img.enabled = false;
			t.enabled = false;
		}
	}

	void Update(){
		if (!controller.shielded && !img.enabled && Values.inventory.shield > 0) {
			img.enabled = true;
			t.enabled = true;
		}
	}

}
