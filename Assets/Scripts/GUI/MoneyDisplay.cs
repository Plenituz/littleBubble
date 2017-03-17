using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour {
	Text t;

	void Start () {
		t = GetComponent<Text> ();
		if (Values.GetMoneyCounter () == null) {
			GameController.CreateMoneyCounter ();
		}
		t.text = Values.GetMoneyCounter ().GetMoney ().ToString ();
		UpdateText ();
	}

	public void UpdateText(){
		if(t != null)
			t.text = Values.GetMoneyCounter ().GetMoney ().ToString ();
	}

}
