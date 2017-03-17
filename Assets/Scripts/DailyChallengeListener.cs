using UnityEngine;
using System.Collections;

public class DailyChallengeListener : MonoBehaviour {

	public delegate void ChallengeListener(ChallengeListener channel, Challenge challenge, object[] obj);
	public static ChallengeListener ch1Listener;
	public static ChallengeListener ch2Listener;
	public static ChallengeListener ch3Listener;

	public static object[] ch1Obj = new object[10];
	public static object[] ch2Obj = new object[10];
	public static object[] ch3Obj = new object[10];

	public static DailyChallengeListener d;

	void Start(){
		if (d == null) {
			d = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Update () {
		if(ch1Listener != null && !DailyChallenges.ch_1.completed)
			ch1Listener (ch1Listener, DailyChallenges.ch_1, ch1Obj);
		if(ch2Listener != null && !DailyChallenges.ch_2.completed)
			ch2Listener (ch2Listener, DailyChallenges.ch_2, ch2Obj);
		if(ch3Listener != null && !DailyChallenges.ch_3.completed)
			ch3Listener (ch3Listener, DailyChallenges.ch_3, ch3Obj);
	}

	public static void ClearChannel(ChallengeListener c){
		if (c == ch1Listener) {
			ch1Listener = null;
		}
		if (c == ch2Listener) {
			ch1Listener = null;
		}
		if (c == ch3Listener) {
			ch1Listener = null;
		}
	}
}
