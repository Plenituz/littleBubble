using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TickAction : MonoBehaviour {

	public Text debugText;

	public delegate void ActionsDelegate();
	public static ActionsDelegate actions;

	#if UNITY_EDITOR
	bool inv = true;
	#endif
	TextModifier moneyText;
	TextModifier pointText;
	MoneyCounter moneyCounter;
	PointCounter pointCounter;

	void Start(){
		#if !UNITY_EDITOR
		Destroy(debugText.gameObject);
		#endif

		moneyCounter = Values.GetMoneyCounter ();
		pointCounter = Values.GetPointCounter ();

		TextModifier[] texts = (TextModifier[])FindObjectsOfType (typeof(TextModifier));
		foreach (TextModifier t in texts) {
			if(t != null && t.CompareTag("PointsText")){
				pointText = t;
			}
			if(t != null && t.CompareTag("MoneyText")){
				moneyText = t;
			}
		}
		#if false
		DailyChallenges.ch_1.justRewarded = true;
		Values.SaveDailyChallenge ();
		#endif
	}

	void Update () {
		if (actions != null) {
			try {
				actions ();
			} catch (Exception e) {
				print (e);
				print ("ERROR IN TICK ACTION, EMPTYING DELEGATE");
				actions = null;
			}
		}
		#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.H)) {
			Values.GetPlayer ().SendMessage ("Killable", !inv);
			inv = !inv;
		}
		debugText.text = "" + (inv ? "killable " : "unkillable ") +  "s:" + Values.playerSpeed;
		#endif

		moneyText.SetText (moneyCounter.GetMoney ().ToString());
		pointText.SetText (pointCounter.GetPoints ().ToString());
	}
}
