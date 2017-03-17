using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UseRespawnOnClick : MonoBehaviour {
	public Image bg;

	void Start(){
		if (Values.inventory.respawn <= 0) {
			bg.color = Color.gray;
			GetComponent<Text> ().color = Color.gray;
		}
	}

	void OnClick(){
		if (Values.inventory.respawn > 0) {
			Time.timeScale = 1;
			Values.GetPointCounter ().UnPause ();
			gameObject.GetComponentInParent<Canvas> ().enabled = false;
			Values.inventory.respawn--;
			Values.SaveInventory ();
			GameObject go = Instantiate(Values.v.BonusChest) as GameObject;
			go.SendMessage("Init", new object[]{Bonus.GOLDEN, -10f, -10f});
			go.SendMessage ("Activate");
			Destroy (gameObject.GetComponentInParent<Canvas>().gameObject);
		} else {
			Values.MakeToast ("You don't have any Respawn left ! Get some in the shop !", 2f);
		}

	}

	void OnPreClick(){

	}

	void OnCancelClick(){

	}
}
