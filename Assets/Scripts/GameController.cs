using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System;

public class GameController : MonoBehaviour {
	bool playerDied = false;
	public GameObject wannaRespawn;

	void Start () {
		CreatePointCounter ();
		CreateMoneyCounter ();
		CreatePlayer ();
		CreateSpeedIncrementer ();
		StartCoroutine ("GenerateLevel");



		/*string fileName = "tmpPattern.txt";
		if (File.Exists(fileName))
		{
			Debug.Log(fileName+" already exists.");
			File.Delete(fileName);
		}
		StreamWriter sr = File.CreateText (fileName);
		GameObject g = Instantiate (Values.v.p3) as GameObject;
		for (int i = 0; i < g.transform.childCount; i++) {
			Transform child = g.transform.GetChild(i);
			if(child.CompareTag("Plane") || child.CompareTag("Bird")){
				sr.WriteLine("yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);");
				sr.WriteLine("Spawn" + child.tag + "(" + child.position.y + "f, Values.playerSpeed * (2f/3), SideDirection.);");
			}else if(child.CompareTag("Trampo")){
				sr.WriteLine("Spawn" + child.tag + "(" + child.position.x + "f, " + child.position.y + "f + Values.offsetY, SideDirection.);");
			}else if(child.CompareTag("BonusChest")){
				sr.WriteLine("SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), " + child.position.x + "f, " + child.position.y + "f  + Values.offsetY);");
			}else if (child.CompareTag("MalusChest")){
				sr.WriteLine("SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), " + child.position.x + "f, " + child.position.y + "f  + Values.offsetY);");
			}else if( child.CompareTag("BlackHole")){
				sr.WriteLine("SpawnBlackHole(" + child.position.x + "f, " + child.position.y + "f + Values.offsetY, 3f);");
			}else{
				sr.WriteLine("Spawn" + child.tag + "(" + child.position.x + "f, " + child.position.y + "f + Values.offsetY);");
			}
		}
		sr.WriteLine ("yield return null;");
		sr.Close();
		Destroy (g);*/

	}

	void PlayerDied(){
		Time.timeScale = 0;
		Values.GetPointCounter ().Pause ();
		if (playerDied) {
			Values.stats.distanceTraveled += Values.GetPointCounter ().GetPoints ();
			if (Values.GetPointCounter ().GetPoints () > Values.stats.highScore)
				Values.stats.highScore = Values.GetPointCounter ().GetPoints ();
			Values.SaveStats ();
			Values.GetMoneyCounter ().SaveMoney ();
			//keep the object for the transition
			GameObject[] objects = FindObjectsOfType<GameObject> ();
			Values.GetPlayer ().AddComponent<FadeOutSprite> ();
			for (int i = 0; i < objects.Length; i++) {
				if (objects [i].name.Contains ("Slider"))
					Destroy (objects [i]);
				if (objects [i].name.Contains ("Clone") || objects [i].name.Contains ("Background") || objects [i].name.Contains ("HUD")) {
					DontDestroyOnLoad (objects [i]);
					try{
						Destroy (objects [i].GetComponent<Mover> ());
						objects[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					}catch(Exception){
					}
					if (objects [i].name.Contains ("HUD")) {
						objects [i].AddComponent<FadeOutCanvas> ();
					}
					Destroy (objects [i], 2f);

					if (objects [i].name.Contains ("PointCounter"))
						Destroy (objects [i], 0.2f);
				}
			}
			Destroy (Values.v.gameObject);
			Instantiate (Resources.FindObjectsOfTypeAll<Values> ()[0]);
			DailyChallenges.NotifyOfGameEnd ();
			SceneManager.LoadScene ("DeathScreen");
			Time.timeScale = 1;
		} else {
			playerDied = true;
			wannaRespawn.SetActive (true);
		}
	}

	void Reset(){
		TickAction.actions = null;
		Values.inversControl = false;
		BonusPowers.active = false;
		MalusPower.active = 0;

		CreatePointCounter ();
		CreateMoneyCounter ();
		CreatePlayer ();
		CreateSpeedIncrementer ();
	}

	IEnumerator GenerateLevel(){
		Reset ();
		int difficulty = 0;
		int nextBoss = Values.bossEvery * Values.scoreMultiplier;
		string nextBossName = "Spookie";
		PointCounter pointCounter = Values.GetPointCounter ();

		Values.stats.gamePlayed++;
		ArrayList list = new ArrayList ();
		DailyChallenges.NotifyOfGameStart ();
		while (true) {
			if (pointCounter.GetPoints () >= nextBoss) {
				GameObject boss = null;
				switch (nextBossName) {
				case "Spookie":
					boss = Instantiate (Values.v.Spookie) as GameObject;
					nextBossName = "Kroug";
					break;
				case "Kroug":
					boss = Instantiate (Values.v.Kroug) as GameObject;
					nextBossName = "Spookie";
					break;
				case "Cloudy":

					nextBossName = "Spookie";
					break;
				}
				boss.SendMessage ("Init", new object[]{ difficulty });
				Boss b = boss.GetComponent<Boss> ();
				yield return new WaitForSeconds (b.GetDuration ());
				nextBoss = pointCounter.GetPoints () + (Values.bossEvery*Values.scoreMultiplier);
				difficulty = difficulty == 0 ? 2 : difficulty * 2;
			}
			int spawned = UnityEngine.Random.Range (1, Patterns.patternCount + 1);
			if (list.Contains (spawned)) {
				//print (spawned + "was too recent");
				continue;
			}
			if(Patterns.Check(spawned)){
				//print(spawned + "cant be spawned");
				continue;
			}
			//print ("spawned" + spawned);
			list.Add (spawned);
			if (list.Count >= Values.repeatPatternCount) {
				list.RemoveAt (0);
			}
			Patterns.execute (spawned);
			float startTime = Time.time;
			while (Time.time - startTime < Values.offsetY / Values.playerSpeed) {
				yield return new WaitForEndOfFrame ();
			}
			//yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		}
	}

	public static void CreatePlayer(){
		PlayerController g = ((PlayerController)FindObjectOfType (typeof(PlayerController)));
		if (g == null) {
			Instantiate(Values.v.Player);
		}
	}

	public static void DestroyPlayer(){
		Destroy (Values.GetPlayer ());
	}

	public static void CreatePointCounter(){
		PointCounter c = Values.GetPointCounter ();
		if (c == null) {
			Instantiate(Values.v._PointCounter);
		}
	}

	public static void DestroyPointCounter(){
		Destroy (Values.GetPointCounter ().gameObject);
	}

	public static void CreateMoneyCounter(){
		MoneyCounter c = Values.GetMoneyCounter ();
		if (c == null) {
			Instantiate(Values.v._MoneyCounter);
		}
	}

	public static void DestroyMoneyCounter(){
		Destroy (Values.GetMoneyCounter ().gameObject);
	}

	public static void CreateSpeedIncrementer(){
		SpeedIncrementer s = Values.GetSpeedIncrementer ();
		if (s == null) {
			Instantiate(Values.v.SpeedIncrementer);
		}
	}

	public static void DestroySpeedIncrementer(){
		if (Values.GetSpeedIncrementer () != null)
			Destroy (Values.GetSpeedIncrementer ().gameObject);
	}
}
