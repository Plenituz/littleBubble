using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.IO;
using System;

public class OnClickWatchAd : MonoBehaviour {
	#if UNITY_ADS
	public MoneyDisplay text;
	private DateTime lastPubWatched;
	private Timer timer;

	void Awake(){
	//	File.Delete (Application.persistentDataPath + "/l.dat");
		timer = GetComponentInChildren<Timer> ();
		if (File.Exists (Application.persistentDataPath + "/l.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/l.dat", FileMode.Open);
			lastPubWatched = (DateTime)bf.Deserialize (file);
			file.Close ();
		}
	}

	void Update(){
		TimeSpan t = DateTime.Now - lastPubWatched;
		timer.UpdateDate ((t - new TimeSpan(Values.HOURS_BETWEEN_AD, 0, 0)));
		if (t.TotalHours < Values.HOURS_BETWEEN_AD) {
			if (GetComponent<ButtonHandler> () != null) {
				GetComponent<Image> ().color = Color.gray;
				Destroy (GetComponent<ButtonHandler> ());
				timer.Enable (true);
			}
		} else{
			if (GetComponent<ButtonHandler> () == null) {
				GetComponent<Image> ().color = Color.white;
				gameObject.AddComponent<ButtonHandler> ();
				timer.Enable (false);
			}
		}
	}

	public void SaveDate(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/l.dat");
		bf.Serialize (file, DateTime.Now);
		lastPubWatched = DateTime.Now;
		file.Close ();
	}

	void OnClick(){
		Values.MakeToast ("Loading ad...", 2f);
		if (!Advertisement.isInitialized)
			GetComponent<Image> ().color = Color.gray;//toast.maketext
		AdManager.instance.ShowAd (AdCallback);
	}

	void AdCallback(ShowResult result){
		print (result);
		switch (result) {
		case ShowResult.Skipped:
		case ShowResult.Failed:
			Values.MakeToast ("Something went wrong with the ad! Sorry about that");
			break;
		case ShowResult.Finished:
			Values.MakeToast ("You earn 500 currency!");
			SaveDate ();
			Values.GetMoneyCounter ().AddMoney (500);
			Destroy (GetComponent<ButtonHandler> ());
			GetComponent<Image> ().color = Color.gray;
			Values.GetMoneyCounter ().SaveMoney ();
			text.UpdateText ();
			timer.Enable (true);
			break;
		}
	}

	void OnPreClick(){
	}

	void OnCancelClick(){
	}
	#endif
}
