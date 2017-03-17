using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Advertisements;

public class WatchAdToRespawnOnClick : MonoBehaviour {
	#if UNITY_ADS
	private DateTime lastPubWatched;
	private Timer timer;
	public Image bg;
	public RespawnCountDown respawnCountDown;

	void Awake(){
		timer = GetComponentInChildren<Timer> ();
		if (File.Exists (Application.persistentDataPath + "/respawn.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/respawn.dat", FileMode.Open);
			lastPubWatched = (DateTime)bf.Deserialize (file);
			file.Close ();
		}
	}

	void Update(){
		TimeSpan t = DateTime.Now - lastPubWatched;
		timer.UpdateDate ((t - new TimeSpan(Values.HOURS_BETWEEN_RESPAWN_AD, 0, 0)));
		if (t.TotalHours < Values.HOURS_BETWEEN_RESPAWN_AD) {
			if (GetComponent<ButtonHandler> () != null) {
				GetComponent<Text> ().color = Color.gray;//chaneg that with image
				bg.color = Color.gray;
				Destroy (GetComponent<ButtonHandler> ());
				timer.Enable (true);
			}
		} else{
			if (GetComponent<ButtonHandler> () == null) {
				GetComponent<Text> ().color = Color.white;//chaneg that with image
				bg.color = Color.white;
				gameObject.AddComponent<ButtonHandler> ();
				timer.Enable (false);
			}
		}
	}

	public void SaveDate(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/respawn.dat");
		bf.Serialize (file, DateTime.Now);
		lastPubWatched = DateTime.Now;
		file.Close ();
	}

	void OnClick(){
		Values.MakeToast ("Loading ad...", 2f);
		Destroy (respawnCountDown);
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
			SaveDate ();
			Destroy (gameObject.GetComponentInParent<Canvas>().gameObject);

			Time.timeScale = 1;
			Values.GetPointCounter ().UnPause ();
			gameObject.GetComponentInParent<Canvas> ().enabled = false;
			GameObject go = Instantiate(Values.v.BonusChest) as GameObject;
			go.SendMessage("Init", new object[]{Bonus.GOLDEN, -10f, -10f});
			go.SendMessage ("Activate");
			break;
		}
	}

	IEnumerator WaitAndSave(){
		yield return new WaitForSeconds(3f);
		Values.GetMoneyCounter ().SaveMoney ();
	}

	void OnPreClick(){
	}

	void OnCancelClick(){
	}
	#endif
}
