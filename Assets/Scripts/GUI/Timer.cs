using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {
	private Text t;

	void Awake(){
		t = GetComponent<Text> ();
	}

	public void UpdateDate(TimeSpan ti){
		TimeSpan timespan = new TimeSpan ((int) Mathf.Abs (ti.Hours), (int) Mathf.Abs (ti.Minutes), (int) Mathf.Abs (ti.Seconds));
		string hours = ((timespan.Hours < 10) ? ("0" + timespan.Hours) : timespan.Hours.ToString());
		string minutes = ((timespan.Minutes < 10) ? ("0" + timespan.Minutes) : timespan.Minutes.ToString());
		string seconds = ((timespan.Seconds < 10) ? ("0" + timespan.Seconds) : timespan.Seconds.ToString());
		t.text = hours + ":" + minutes + ":" + seconds;
	}

	public void Enable(bool e){
		t.enabled = e;
	}
}
