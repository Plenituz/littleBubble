using UnityEngine;
using System.Reflection;
using System.Collections;
using UnityEngine.UI;

public class CounterItem : MonoBehaviour {
	public string item;
	private int val;
	private FieldInfo field;
	private Text text;

	void Start () {
		text = GetComponent<Text> ();
		field = typeof(Inventory).GetField (item);
		text.text = "You already have " + field.GetValue (Values.inventory);
		val = (int) field.GetValue (Values.inventory);
	}

	void Update(){
		if (val != (int) field.GetValue (Values.inventory)) {
			val = (int) field.GetValue (Values.inventory);
			text.text = "You already have " + field.GetValue (Values.inventory);
		}
	}
}
