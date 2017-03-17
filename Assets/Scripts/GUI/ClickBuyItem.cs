using UnityEngine;
using System.Collections;

public class ClickBuyItem : MonoBehaviour {
	public GameObject menu;

	public void OnClick(){
		menu.SetActive (true);
	}
}
