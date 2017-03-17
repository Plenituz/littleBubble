using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreMultiplierUI : MonoBehaviour {

	public Image validx2;
	public Image validx4;
	public Image validx6;
	public Image moneyIconx2;
	public Image moneyIconx4;
	public Image moneyIconx6;
	public Text pricex2;
	public Text pricex4;
	public Text pricex6;
	public MoneyDisplay moneyDisplay;

	void Start () {
		UpdateUI ();
	}

	void UpdateUI(){
		if (Values.scoreMultiplier == 2) {
			validx2.color = Color.green;
			validx4.color = Color.white;
			validx6.color = Color.white;
		} else if (Values.scoreMultiplier == 4) {
			validx2.color = Color.white;
			validx4.color = Color.green;
			validx6.color = Color.white;
		} else if (Values.scoreMultiplier == 6) {
			validx2.color = Color.white;
			validx4.color = Color.white;
			validx6.color = Color.green;
		}

		if (Values.inventory.scoreMultiplier_x2) {
			validx2.enabled = true;
			moneyIconx2.enabled = false;
			pricex2.enabled = false;

			pricex4.text = "25K";
			moneyIconx4.sprite = moneyIconx2.sprite;
		} else {
			validx2.enabled = false;
		}

		if (Values.inventory.scoreMultiplier_x4) {
			validx4.enabled = true;
			moneyIconx4.enabled = false;
			pricex4.enabled = false;

			pricex6.text = "50K";
			moneyIconx6.sprite = moneyIconx2.sprite;
		} else {
			validx4.enabled = false;
		}

		if (Values.inventory.scoreMultiplier_x6) {
			validx6.enabled = true;
			pricex6.enabled = false;
			moneyIconx6.enabled = false;
		} else {
			validx6.enabled = false;
		}
		moneyDisplay.UpdateText ();
	}

	public void Clickx2(){
		if (Values.inventory.scoreMultiplier_x2) {
			Values.scoreMultiplier = 2;
			Values.SaveValues ();
		} else {
			GameController.CreateMoneyCounter ();
			if (Values.GetMoneyCounter ().GetMoney () >= 10000) {
				Values.GetMoneyCounter ().RemoveMoney (10000);
				Values.GetMoneyCounter ().SaveMoney ();

				Values.inventory.scoreMultiplier_x2 = true;
				Values.SaveInventory ();

				Values.scoreMultiplier = 2;
				Values.SaveValues ();
			}
		}
		UpdateUI ();
	}

	public void Clickx4(){
		if (Values.inventory.scoreMultiplier_x4) {
			Values.scoreMultiplier = 4;
			Values.SaveValues ();
		} else if (Values.inventory.scoreMultiplier_x2) {
			GameController.CreateMoneyCounter ();
			if (Values.GetMoneyCounter ().GetMoney () >= 25000) {
				Values.GetMoneyCounter ().RemoveMoney (25000);
				Values.GetMoneyCounter ().SaveMoney ();

				Values.inventory.scoreMultiplier_x4 = true;
				Values.SaveInventory ();

				Values.scoreMultiplier = 4;
				Values.SaveValues ();
			}
		}
		UpdateUI ();
	}

	public void Clickx6(){
		if (Values.inventory.scoreMultiplier_x6) {
			Values.scoreMultiplier = 6;
			Values.SaveValues ();
		} else if (Values.inventory.scoreMultiplier_x2 && Values.inventory.scoreMultiplier_x4) {
			GameController.CreateMoneyCounter ();
			if (Values.GetMoneyCounter ().GetMoney () >= 50000) {
				Values.GetMoneyCounter ().RemoveMoney (50000);
				Values.GetMoneyCounter ().SaveMoney ();

				Values.inventory.scoreMultiplier_x6 = true;
				Values.SaveInventory ();

				Values.scoreMultiplier = 6;
				Values.SaveValues ();
			}
		}
		UpdateUI ();
	}
}
