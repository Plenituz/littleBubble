using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextModifier : MonoBehaviour {
	private Text text;

	void Start(){
		text = GetComponent<Text> ();
	}

	public void SetText(string text){
		this.text.text = text;
	}
}
