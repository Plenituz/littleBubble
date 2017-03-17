using UnityEngine;
using System.Reflection;
using System.Collections;

public class OnClickBuyAndClose : MonoBehaviour {
	public GameObject menu;
	public string item;
	public int price;
	public int amount;
	public MoneyDisplay moneyDisplay;
	public AudioSource source;

	public void OnClick(){
		
		GameController.CreateMoneyCounter ();
		if (Values.GetMoneyCounter ().GetMoney () >= price) {
			Values.GetMoneyCounter ().RemoveMoney (price);
			Values.GetMoneyCounter ().SaveMoney ();

			FieldInfo field = typeof(Inventory).GetField (item);
			field.SetValue (Values.inventory, (int) (field.GetValue (Values.inventory)) + amount);
			Values.SaveInventory ();
			moneyDisplay.UpdateText ();
			menu.SetActive (false);
			try{
				source.clip = Values.v.Achatmp3;
				source.Play ();
			}catch(UnassignedReferenceException){
			}
		} else {
			source.clip = Values.v.Refus_Achatmp3;
			source.Play ();
			Values.MakeToast ("You're not rich enough !");
		}
	}
}
