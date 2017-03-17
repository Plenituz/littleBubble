using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LateralSpeedUI : MonoBehaviour {

	public Image validSlow;
	public Image validMed;
	public Image validFast;
	public Image moneyIconMed;
	public Image moneyIconFast;
	public Text priceMed;
	public Text priceFast;
	public Sprite cadena;
	public MoneyDisplay moneyDisplay;

	void Start () {
		UpdateUI ();
	}

	void UpdateUI(){
		if (Values.lateralSpeed == Values.LATERAL_SPEED_SLOW) {
			validSlow.color = Color.green;
			validMed.color = Color.white;
			validFast.color = Color.white;
		} else if (Values.lateralSpeed == Values.LATERAL_SPEED_MED) {
			validSlow.color = Color.white;
			validMed.color = Color.green;
			validFast.color = Color.white;
		} else if (Values.lateralSpeed == Values.LATERAL_SPEED_FAST) {
			validSlow.color = Color.white;
			validMed.color = Color.white;
			validFast.color = Color.green;
		}
		if (Values.inventory.lateralSpeed_x15) {
			validMed.enabled = true;
			moneyIconMed.enabled = false;
			priceMed.enabled = false;
		} else {
			validMed.enabled = false;
			validFast.enabled = false;
			priceFast.text = "Locked";
			moneyIconFast.sprite = cadena;
		}

		if (Values.inventory.lateralSpeed_x2) {
			validFast.enabled = true;
			priceFast.enabled = false;
			moneyIconFast.enabled = false;
		} else {
			if (Values.inventory.lateralSpeed_x15) {
				priceFast.text = "5000";
				moneyIconFast.sprite = moneyIconMed.sprite;
				validFast.enabled = false;
			}
		}
		moneyDisplay.UpdateText ();
	}

	public void ClickSlow(){
		Values.lateralSpeed = Values.LATERAL_SPEED_SLOW;
		Values.SaveValues ();
		UpdateUI ();
	}

	public void ClickMedium(){
		if (Values.inventory.lateralSpeed_x15) {
			Values.lateralSpeed = Values.LATERAL_SPEED_MED;
			Values.SaveValues ();
		} else {
			GameController.CreateMoneyCounter ();
			if (Values.GetMoneyCounter ().GetMoney () >= 5000) {
				Values.GetMoneyCounter ().RemoveMoney (5000);
				Values.GetMoneyCounter ().SaveMoney ();

				Values.inventory.lateralSpeed_x15 = true;
				Values.SaveInventory ();

				Values.lateralSpeed = Values.LATERAL_SPEED_MED;
				Values.SaveValues ();
			}
		}
		UpdateUI ();
	}

	public void ClickFast(){
		if (Values.inventory.lateralSpeed_x2) {
			Values.lateralSpeed = Values.LATERAL_SPEED_FAST;
			Values.SaveValues ();
		} else if(Values.inventory.lateralSpeed_x15){
			GameController.CreateMoneyCounter ();
			if (Values.GetMoneyCounter ().GetMoney () >= 5000) {
				Values.GetMoneyCounter ().RemoveMoney (5000);
				Values.GetMoneyCounter ().SaveMoney ();

				Values.inventory.lateralSpeed_x2 = true;
				Values.SaveInventory ();

				Values.lateralSpeed = Values.LATERAL_SPEED_FAST;
				Values.SaveValues ();
			}
		}
		UpdateUI ();
	}

}
