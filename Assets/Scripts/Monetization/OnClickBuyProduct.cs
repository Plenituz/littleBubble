using UnityEngine;
using System.Collections;

public class OnClickBuyProduct : MonoBehaviour {
	public Purchaser purchaser;
	public string product;

	void OnClick(){
		purchaser.SendMessage (product);
	}

	void OnPreClick(){
	}

	void OnCancelClick(){
	}
}
