using UnityEngine;
using System.Collections;

public class DoIShowDaily : MonoBehaviour {
	public GameObject daily;

	void Start () {
		if (DailyChallenges.ch_1.rewarded != -1 && !DailyChallenges.ch_1.justRewarded) {
			Destroy (daily);
		}
		if (DailyChallenges.ch_1.justRewarded) {
			DailyChallenges.ch_1.justRewarded = false;
			DailyChallenges.ch_2.justRewarded = false;
			DailyChallenges.ch_3.justRewarded = false;
			Values.SaveDailyChallenge ();
		}
	}
}
