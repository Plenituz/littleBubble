using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IconSliderHandler : MonoBehaviour {

	public Sprite[] icons;
	public string[] iconNames;

	public void SetIcon(string name){
		int index = 0;
		for (int i = 0; i < iconNames.Length; i++) {
			if (iconNames [i].Equals (name)) {
				index = i;
				break;
			}
		}
		transform.FindChild ("Icon").GetComponent<Image> ().sprite = icons [index];
	}

	public void SetValue(float value){
		transform.FindChild ("Slider").GetComponent<Slider> ().value = value;
	}

	public void SetVisible(bool visible){
		gameObject.SetActive (visible);
	}
}
