using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnClickDouble : MonoBehaviour {
	public Purchaser purchaser;
	public Text text;
	public Image bg;
	public AudioSource source;

	void Start(){
		if (Values.moneyMultiplier >= 2) {
			bg.color = Color.gray;
			text.enabled = true;
		}
	}

	public void OnClick(){
		source.Play ();
		//if(Values.moneyMultiplier == 1)
		purchaser.BuyDoubleBulle ();
	}

	void Update(){
		if (bg.color != Color.gray && Values.moneyMultiplier >= 2) {
			bg.color = Color.gray;
			text.enabled = true;
		}
	}
}
