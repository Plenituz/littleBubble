using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsUpdater : MonoBehaviour {

	void Start () {
		Text t = GetComponent<Text> ();
		t.text = ""+ (int) Values.stats.GetType ().GetField (transform.parent.name).GetValue (Values.stats);
	}
}
