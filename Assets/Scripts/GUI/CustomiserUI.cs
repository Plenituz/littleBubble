using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;
using System;

public class CustomiserUI : MonoBehaviour {
	public Text colorText;
	public Text accessoryText;
	public Text colorPrice;
	public Text accessoryPrice;
	public Image colorImage;
	public Image costumeImage;
	public Image moneySpriteColor;
	public Image moneySpriteAccesory;
	public GameObject buttonBuyColor;
	public GameObject buttonBuyCostume;
	public Sprite equipSprite;
	public Sprite moneyBulleSprite;
	public Sprite cadena;
	public MoneyDisplay moneyDisplay;

	private Sprite[] colorsSprites = new Sprite[10];
	private Sprite[] costumesSprites = new Sprite[9];
	public AnimatorControllerParameter controllerRubis;
	public AudioSource source;

	private int posColor = 0;
	private int posAccessory = 0;
	private string[] colors = new string[]{"Base", "Blue", "Red", "Green", "Rubis", "Saphir", "Emerald", "Golden", "Diamond", "Silver"};
	private string[] accessories = new string[]{"None", "Noel", "Swag", "Melon Hat", "Super Sayian", "Gandalf", "Demon", "Bling", "King"};
	private int[] colorsPrices;
	private int[] accessoriesPrices;

	void Awake(){
		colorsSprites = Values.v.colorSprites;
		costumesSprites = Values.v.costumeSprites;
		colorsPrices = new int[] {0, Values.price_Blue, Values.price_Red, Values.price_Green, Values.price_Rubis, 
			Values.price_Saphir, Values.price_Emerald, Values.price_Golden, Values.price_Diamond, int.MaxValue};
		accessoriesPrices = new int[]{0, Values.price_Noel, Values.price_Swag, Values.price_MelonHat, 
			Values.price_Sayian, Values.price_Gandalf, Values.price_Demon, Values.price_Bling, Values.price_King };
		for (int i = 0; i < colors.Length; i++) {
			if(colors[i].Equals(Values.activeColor.ToString())){
				posColor = i;
				break;
			}
		}
		for (int i = 0; i < accessories.Length; i++) {
			if(accessories[i].Replace(" ", "").Replace("Super", "").Equals(Values.activeCostume.ToString())){
				posAccessory = i;
				break;
			}
		}
		UpdateAll ();
	}

	void UpdateAll(){

		colorText.text = colors [posColor];
		accessoryText.text = accessories [posAccessory];
		colorPrice.text = colorsPrices [posColor].ToString();
		accessoryPrice.text = accessoriesPrices [posAccessory].ToString ();
		colorImage.sprite = colorsSprites [posColor];
		costumeImage.sprite = costumesSprites [posAccessory];

		bool cond;
		FieldInfo field = typeof(Inventory).GetField ("color_" + colors [posColor].ToLower ());
		if (field != null) {
			cond = (bool) field.GetValue (Values.inventory);
		} else {
			cond = true;
		}

		if (cond) {//if you already have the color
			colorPrice.text = "Equip";
			moneySpriteColor.sprite = equipSprite;
			buttonBuyColor.SetActive (false);
			if (Values.activeColor.ToString ().Equals (colors[posColor])) {
				colorPrice.text = "Equiped";
				moneySpriteColor.color = Color.green;
			} else {
				colorPrice.text = "Equip";
				moneySpriteColor.color = Color.white;
			}
		} else {
			if (colors [posColor].Equals ("Silver")) {
				colorPrice.text = "Locked !";
				moneySpriteColor.sprite = cadena;
				moneySpriteColor.color = Color.white;
				buttonBuyColor.SetActive (false);
			} else {
				moneySpriteColor.sprite = moneyBulleSprite;
				buttonBuyColor.SetActive (true);
				moneySpriteColor.color = Color.white;
			}
		}

		field = typeof(Inventory).GetField ("accessories_" + accessories [posAccessory].ToLower ().Replace (" ", "").Replace("super", ""));
		if (field != null) {
			cond = (bool)field.GetValue (Values.inventory);
		} else {
			cond = true;
		}
		if (cond) {//si t'as deja le costume
			buttonBuyCostume.SetActive(false);
			moneySpriteAccesory.sprite = equipSprite;
			if (Values.activeCostume.ToString ().Equals (accessories[posAccessory].Replace(" ", "").Replace("Super", ""))) {
				accessoryPrice.text = "Equiped";
				moneySpriteAccesory.color = Color.green;
			} else {
				accessoryPrice.text = "Equip";
				moneySpriteAccesory.color = Color.white;
			}
		} else {
			moneySpriteAccesory.sprite = moneyBulleSprite;
			buttonBuyCostume.SetActive (true);
			moneySpriteAccesory.color = Color.white;
		}
		moneyDisplay.UpdateText ();

	}

	public void NextColor(){
		if (posColor != colors.Length-1) {
			posColor++;
		} else {
			posColor = 0;
		}

		UpdateAll ();
	}

	public void PrevColor(){
		if (posColor != 0) {
			posColor--;
		} else {
			posColor = colors.Length-1;
		}

		UpdateAll ();
	}

	public void NextAccessory(){
		if (posAccessory != accessories.Length-1) {
			posAccessory++;
		} else {
			posAccessory = 0;
		}
		UpdateAll ();
	}

	public void PrevAccessory(){
		if (posAccessory != 0) {
			posAccessory--;
		} else {
			posAccessory = accessories.Length-1;
		}
		UpdateAll ();
	}

	public void BuyAccessory(){
		bool cond;
		FieldInfo field = typeof(Inventory).GetField ("accessories_" + accessories [posAccessory].ToLower ().Replace (" ", "").Replace("super", ""));
		if (field != null) {
			cond = (bool)field.GetValue (Values.inventory);
		} else {
			cond = false;
		}
		if (cond) {//si t'as deja le costume
			return;
		} else {
			if(Values.GetMoneyCounter() == null)
				GameController.CreateMoneyCounter ();
			if (Values.GetMoneyCounter ().GetMoney () >= accessoriesPrices [posAccessory]) {
				Values.GetMoneyCounter ().RemoveMoney (accessoriesPrices [posAccessory]);
				field.SetValue (Values.inventory, true);
				Values.SaveInventory ();
				Values.GetMoneyCounter ().RemoveMoney (accessoriesPrices [posAccessory]);
				Values.GetMoneyCounter ().SaveMoney ();
				UpdateAll ();
				source.clip = Values.v.Achatmp3;
				source.Play ();
			} else {
				source.clip = Values.v.Refus_Achatmp3;
				source.Play ();
				Values.MakeToast ("You don't have enough Bulles !");
			}
		}
	}

	public void BuyColor(){
		bool cond;
		FieldInfo field = typeof(Inventory).GetField ("color_" + colors [posColor].ToLower ());
		if (field != null) {
			cond = (bool) field.GetValue (Values.inventory);
		} else {
			cond = false;
		}

		if (cond) {//if you already have the color
			return;
		} else {
			if (Values.GetMoneyCounter () == null)
				GameController.CreateMoneyCounter ();
			if (Values.GetMoneyCounter ().GetMoney () >= colorsPrices [posColor]) {
				Values.GetMoneyCounter ().RemoveMoney (colorsPrices [posColor]);
				field.SetValue (Values.inventory, true);
				Values.SaveInventory ();
				Values.GetMoneyCounter ().RemoveMoney (colorsPrices [posColor]);
				Values.GetMoneyCounter ().SaveMoney ();
				UpdateAll ();
				source.clip = Values.v.Achatmp3;
				source.Play ();
			} else {
				source.clip = Values.v.Refus_Achatmp3;
				source.Play ();
				Values.MakeToast ("You don't have enough Bulles !");
			}
		}
	}

	public void EquipColor(){
		if (posColor == 0) {
			Values.activeColor = Colors.Base;
			UpdateAll ();
			Values.SaveValues ();
			return;
		}
		bool cond;
		FieldInfo field = typeof(Inventory).GetField ("color_" + colors [posColor].ToLower ());
		if (field != null) {
			cond = (bool) field.GetValue (Values.inventory);
		} else {
			cond = false;
		}

		if (cond) {
			Values.activeColor = (Colors)Enum.Parse (typeof(Colors), colors [posColor], true);
			Values.SaveValues ();
			UpdateAll ();
		}
	}

	public void EquipCostume(){
		if (posAccessory == 0) {
			Values.activeCostume = Costume.None;
			UpdateAll ();
			Values.SaveValues ();
			return;
		}
		bool cond;
		FieldInfo field = typeof(Inventory).GetField ("accessories_" + accessories [posAccessory].ToLower ().Replace (" ", "").Replace("super", ""));
		if (field != null) {
			cond = (bool)field.GetValue (Values.inventory);
		} else {
			cond = false;
		}

		if (cond) {
			Values.activeCostume = (Costume)Enum.Parse (typeof(Costume), accessories [posAccessory].Replace (" ", "").Replace ("Super", ""), true);
			Values.SaveValues ();
			UpdateAll ();
		}
	}
}
