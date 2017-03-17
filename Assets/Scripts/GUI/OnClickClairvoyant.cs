using UnityEngine;
using System.Collections;

public class OnClickClairvoyant : MonoBehaviour {

	void OnClick(){
		PlayerController c = Values.GetPlayer ().GetComponent<PlayerController> ();
		if (!c.clairvoyant) {
			c.clairvoyant = true;
			Values.inventory.clairvoyance--;
			Values.SaveInventory ();
			gameObject.SetActive (false);
		}
	}
}
