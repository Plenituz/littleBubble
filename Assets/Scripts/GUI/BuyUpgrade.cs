using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;

public class BuyUpgrade : MonoBehaviour {

	public Text levelText;
	public Text priceText;
	public Text timeText;
	public Image priceIcon;
	public MoneyDisplay moneyDisplay;

	public int[] prices = new int[]{ 500, 2500, 10000, 25000, 50000 };
	public string[] pricesTexts = new string[]{"500", "2500", "10K", "25K", "50K"};

	FieldInfo level;

	void Start(){
		level = typeof(Inventory).GetField ("bonusLevel_" + gameObject.name);
		UpdateUI ();
	}

	void UpdateUI(){
		int mLevel = (int)level.GetValue (Values.inventory);
		timeText.text = 5 + (mLevel * Values.bonusAddPerLevel) + "'";
		if (mLevel == 5) {
			levelText.text = "MAX";
			priceText.enabled = false;
			priceIcon.enabled = false;
			levelText.rectTransform.localPosition = new Vector2 (levelText.rectTransform.localPosition.x, -54f);
		} else {
			levelText.text = "Level : " + mLevel;
			priceText.text = pricesTexts [mLevel];
		}
	}

	public void OnClick(){
		int mLevel = (int)level.GetValue (Values.inventory);
		if (mLevel == 5)
			return;

		if (Values.GetMoneyCounter () == null)
			GameController.CreateMoneyCounter ();

		if (Values.GetMoneyCounter ().GetMoney () >= prices [mLevel]) {
			Values.GetMoneyCounter ().RemoveMoney (prices [mLevel]);
			Values.GetMoneyCounter ().SaveMoney ();
			level.SetValue (Values.inventory, mLevel + 1);
			Values.SaveInventory ();
			UpdateUI ();
			moneyDisplay.UpdateText ();
		}
	}
}
