using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using System.Collections;

public class AmountItem : MonoBehaviour {
	public string item;
	private FieldInfo field;
	private Text text;


	void Start () {
		field = typeof(Inventory).GetField (item);
		text = GetComponent<Text> ();
		text.text = field.GetValue (Values.inventory).ToString ();
		if ((int)field.GetValue (Values.inventory) <= 0) {
			gameObject.transform.parent.gameObject.SetActive (false);
		}
	}

	void Update(){
		if (!field.GetValue (Values.inventory).ToString ().Equals (text.text)) {
			text.text = field.GetValue (Values.inventory).ToString ();
		}
	}
}
