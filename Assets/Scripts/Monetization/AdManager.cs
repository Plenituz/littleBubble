using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {
	#if UNITY_ADS
	public const string GAME_ID = "1041511";
	public const string ZONE = "rewardedVideo";
	public const float MAX_TIME_TO_LOAD = 5f;

	public static AdManager instance;

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
	#if DEBUG
			Advertisement.Initialize (GAME_ID, false);
	#else
			Advertisement.Initialize (GAME_ID, false);
	#endif
		} else {
			Destroy (gameObject);
		}

	}

	public void ShowAd(System.Action<ShowResult> callback){
		ShowOptions options = new ShowOptions ();
		options.resultCallback = callback;
		StartCoroutine ("WaitForReadyAndShowAd", options);
	}

	IEnumerator WaitForReadyAndShowAd(ShowOptions options){
		float startTime = Time.time;
		while (!Advertisement.IsReady ()) {
			if (Time.time - startTime > MAX_TIME_TO_LOAD) {
				Values.MakeToast ("Could not load ad, sorry !\nTry restarting the app while having an internet connection");
				StopCoroutine ("WaitForReadyAndShowAd");
				Advertisement.Initialize (GAME_ID, true);
			}				
			yield return null;
		}
		Advertisement.Show(ZONE, options);
	}
	#endif
}
