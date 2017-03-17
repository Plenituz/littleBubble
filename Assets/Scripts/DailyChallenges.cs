using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DailyChallenges
{
	public static Challenge ch_1;
	public static Challenge ch_2;
	public static Challenge ch_3;

	public static bool gameStarted = false;
	public static bool gameEnded = false;

	public static DateTime challengesAssignedAt;

	public static void NotifyOfGameStart(){
		gameEnded = false;
		gameStarted = true;
	}

	public static void NotifyOfGameEnd(){
		gameEnded = true;
		gameStarted = false;
	}

	public static void CreateChallenges(){
		LoadAssignementTime ();
		//Debug.Log (DateTime.Now - challengesAssignedAt );
		//Debug.Log (challengesAssignedAt);
		if (DateTime.Now - challengesAssignedAt >= new TimeSpan (24, 0, 0)) {
			//if there has never been a challenge or it was created more than 24 hours ago
			//then create new ones
			//Debug.Log ("assigning new challenges");
			ch_1 = new Challenge (UnityEngine.Random.Range (0, descriptions.Length));
			do {
				ch_2 = new Challenge (UnityEngine.Random.Range (0, descriptions.Length));
			} while(ch_2.id == ch_1.id);
			do {
				ch_3 = new Challenge (UnityEngine.Random.Range (0, descriptions.Length));
			} while(ch_3.id == ch_1.id || ch_3.id == ch_2.id);
			SaveAssignementTime ();
			Values.SaveDailyChallenge ();
		} else {
			//Debug.Log ("already have challenge : " + ch_1.id + ", " + ch_2.id + ", " + ch_3.id);
		}
		ch_1.StartListening (1);
		ch_2.StartListening (2);
		ch_3.StartListening (3);
	}

	public static void LoadAssignementTime(){
		if (File.Exists (Application.persistentDataPath + Values.CHALLENGE_TIME_PATH)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + Values.CHALLENGE_TIME_PATH, FileMode.Open);
			challengesAssignedAt = (DateTime)bf.Deserialize (file);
			file.Close ();
		}
	}

	public static void SaveAssignementTime(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + Values.CHALLENGE_TIME_PATH);
		bf.Serialize (file, DateTime.Now);
		challengesAssignedAt = DateTime.Now;
		file.Close ();
	}

	public delegate object RewardDelegate(); 

	public static string[] descriptions = {
		"Collect 100 Bulles in 1 game",
		"Collect 200 Bulles in 1 game",
		"Collect 400 Bulles in 1 game",
		"Collect 600 Bulles in 1 game",
		"Collect 5 Bonus in 1 game",
		"Collect 10 Bonus in 1 game",
		"Collect 5 Malus in 1 game",
		"Collect 10 Malus in 1 game",
		"Die between 6500 and 7000 points",
		"Die between 25 000 and 26 000 points",
		"Get to 20 000 points without any Bonus",
		"Die 5 times by Spikes",
		"Die 5 times by Clouds",
		"Die 5 times by Birds",
		"Die 5 times by Lasers",
		"Die 5 times Planes",
		"Score 5000 points without collecting any Bulles",
		"Die against Spookie the squid",
		"Die against Krug the meteor",
		"Die against Cloudy the cloud",
		"Change color",
		"Change costume",
		"Buy something in the shop",
		"Use an item in a game",
		"Use 3 items in a game",
		"Get to 10 000 points 3 times with 3 differents costumes",
		"Beat your high score"
	};

	public static DailyChallengeListener.ChallengeListener[] listeners = new DailyChallengeListener.ChallengeListener[]{
		//---------------------------GET 100 COIN IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = int.MaxValue;
				//{onGoingGame?, BulleBeforeGame}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
				GameController.CreateMoneyCounter();
				obj[1] = Values.GetMoneyCounter().GetMoney();
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				GameController.CreateMoneyCounter();
				if(Values.GetMoneyCounter().GetMoney() - (int) obj[1] >= 100){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------GET 200 COIN IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = int.MaxValue;
				//{onGoingGame?, BulleBeforeGame}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
				GameController.CreateMoneyCounter();
				obj[1] = Values.GetMoneyCounter().GetMoney();
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				GameController.CreateMoneyCounter();
				if(Values.GetMoneyCounter().GetMoney() - (int) obj[1] >= 200){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------GET 400 COIN IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = int.MaxValue;
				//{onGoingGame?, BulleBeforeGame}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
				GameController.CreateMoneyCounter();
				obj[1] = Values.GetMoneyCounter().GetMoney();
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				GameController.CreateMoneyCounter();
				if(Values.GetMoneyCounter().GetMoney() - (int) obj[1] >= 400){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------GET 600 COIN IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = int.MaxValue;
				//{onGoingGame?, BulleBeforeGame}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
				GameController.CreateMoneyCounter();
				obj[1] = Values.GetMoneyCounter().GetMoney();
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				GameController.CreateMoneyCounter();
				if(Values.GetMoneyCounter().GetMoney() - (int) obj[1] >= 600){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------GET 5 BONUS IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = false;
				obj[2] = 0;
				//{onGoingGame?, activeBonusAkreadyCounted?, BonusCount}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				if((int) obj[2] >= 5){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}else{
					obj[1] = false;
					obj[2] = 0;
				}
			}
			if((bool) obj[0] && !((bool) obj[1]) && BonusPowers.active){
				obj[1] = true;
				obj[2] = ((int) obj[2])+1;
			}
			if((bool) obj[1] && !BonusPowers.active){
				obj[1] = false;
			}
		},
		//---------------------------GET 10 BONUS IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = false;
				obj[2] = 0;
				//{onGoingGame?, activeBonusAkreadyCounted?, BonusCount}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				if((int) obj[2] >= 10){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}else{
					obj[1] = false;
					obj[2] = 0;
				}
			}
			if((bool) obj[0] && !((bool) obj[1]) && BonusPowers.active){
				obj[1] = true;
				obj[2] = ((int) obj[2])+1;
			}
			if((bool) obj[1] && !BonusPowers.active){
				obj[1] = false;
			}
		},
		//---------------------------GET 5 MALUS IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = MalusPower.active;
				obj[2] = 0;
				//{onGoingGame?, lastValueofMalusActive?, BonusCount}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				if((int) obj[2] >= 5){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}else{
					obj[1] = MalusPower.active;
					obj[2] = 0;
				}
			}
			if(((bool) obj[0]) && MalusPower.active != (int) obj[1]){
				if(MalusPower.active > (int) obj[1]){
					obj[2] = ((int) obj[2])+1;
				}
				obj[1] = MalusPower.active;
			}
		},
		//---------------------------GET 10 MALUS IN 1 GAME
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = MalusPower.active;
				obj[2] = 0;
				//{onGoingGame?, lastValueofMalusActive?, BonusCount}
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
			}
			if(gameEnded && (bool) obj[0]){
				//if game ended and it's the first tick 
				obj[0] = false;
				if((int) obj[2] >= 10){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}else{
					obj[1] = MalusPower.active;
					obj[2] = 0;
				}
			}
			if((bool) obj[0] && MalusPower.active != (int) obj[1]){
				if(MalusPower.active > (int) obj[1]){
					obj[2] = ((int) obj[2])+1;
				}
				obj[1] = MalusPower.active;
			}
		},
		//---------------------------Die between 6500 and 7000 points
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;
				if(Values.GetPointCounter().GetPoints() >= 6500 && Values.GetPointCounter().GetPoints() <= 7000){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------Die between 25000 and 26000 points
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;
				if(Values.GetPointCounter().GetPoints() >= 25000 && Values.GetPointCounter().GetPoints() <= 26000){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------Get to 20k points without any bonus
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = false;
			}
			if(!((bool)obj[0]) && gameStarted){
				//if not already started and it is started
				obj[0] = true;
				obj[1] = false;
			}
			if(Values.GetPointCounter() != null && Values.GetPointCounter().GetPoints() >= 20000 && !((bool) obj[1])){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;
				obj[1] = false;
			}
			if(BonusPowers.active && !((bool) obj[1])){
				obj[1] = true;
			}
		},
		//---------------------------Die 5 Times by spikes
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.stats.killedBySpikes;
				if(challenge.progress == -1){
					challenge.progress = 0;
					challenge.maxProgress = 5;
				}
			}
			if(Values.stats.killedBySpikes > (int) obj[0]){
				obj[0] = Values.stats.killedBySpikes;
				challenge.progress++;
				Values.SaveDailyChallenge();
			}
			if(challenge.progress >= 5){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
		},
		//---------------------------Die 5 Times by cloud
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.stats.killedByCloud;
				if(challenge.progress == -1){
					challenge.progress = 0;
					challenge.maxProgress = 5;
				}
			}
			if(Values.stats.killedByCloud > (int) obj[0]){
				obj[0] = Values.stats.killedByCloud;
				challenge.progress++;
				Values.SaveDailyChallenge();
			}
			if(challenge.progress >= 5){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
		},
		//---------------------------Die 5 Times by bird
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.stats.killedByBird;
				if(challenge.progress == -1){
					challenge.progress = 0;
					challenge.maxProgress = 5;
				}
			}
			if(Values.stats.killedByBird > (int) obj[0]){
				obj[0] = Values.stats.killedByBird;
				challenge.progress++;
				Values.SaveDailyChallenge();
			}
			if(challenge.progress >= 5){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
		},
		//---------------------------Die 5 Times by Laser
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.stats.killedByLaser;
				if(challenge.progress == -1){
					challenge.progress = 0;
					challenge.maxProgress = 5;
				}
			}
			if(Values.stats.killedByLaser > (int) obj[0]){
				obj[0] = Values.stats.killedByLaser;
				challenge.progress++;
				Values.SaveDailyChallenge();
			}
			if(challenge.progress >= 5){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
		},
		//---------------------------Die 5 Times by Plane
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.stats.killedByPlane;
				if(challenge.progress == -1){
					challenge.progress = 0;
					challenge.maxProgress = 5;
				}
			}
			if(Values.stats.killedByPlane > (int) obj[0]){
				obj[0] = Values.stats.killedByPlane;
				challenge.progress++;
				Values.SaveDailyChallenge();
			}
			if(challenge.progress >= 5){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
		},
		//---------------------------Score 5000 points without collecting any Bulles
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = 0;
				obj[2] = false;//checked ?
			}
			if(gameStarted && !((bool) obj[0])){
				obj[0] = true;//on game started
				GameController.CreateMoneyCounter();
				obj[1] = Values.GetMoneyCounter().GetMoney();
			}
			if(Values.GetPointCounter() != null && Values.GetPointCounter().GetPoints() >= 5000 && !(bool) obj[2]){
				obj[2] = true;
				GameController.CreateMoneyCounter();
				//Debug.Log(Values.GetMoneyCounter().GetMoney() - (int) obj[1]);
				if(Values.GetMoneyCounter().GetMoney() - (int) obj[1] == 0){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;//on game ended
			}
		},
		//---------------------------Die against Spookie the squid
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = Values.stats.killedByBoss;
			}
			if(gameStarted && !((bool) obj[0])){
				obj[0] = true;//on game started
				obj[1] = Values.stats.killedByBoss;
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;//on game ended
				if(Values.stats.killedByBoss > (int) obj[1]){
					//you just got killed by a boss but which one ?
					int points = Values.GetPointCounter().GetPoints();
					if(points < (Values.bossEvery * Values.scoreMultiplier)*2){
						//spookie
						DailyChallengeListener.ClearChannel(channel);
						challenge.justCompleted = true;
						Values.SaveDailyChallenge();
					}
				}
			}
		},
		//---------------------------Die against Krug
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = Values.stats.killedByBoss;
			}
			if(gameStarted && !((bool) obj[0])){
				obj[0] = true;//on game started
				obj[1] = Values.stats.killedByBoss;
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;//on game ended
				if(Values.stats.killedByBoss > (int) obj[1]){
					//you just got killed by a boss but which one ?
					int points = Values.GetPointCounter().GetPoints();
					if(points < (Values.bossEvery * Values.scoreMultiplier)*3){
						//krug
						DailyChallengeListener.ClearChannel(channel);
						challenge.justCompleted = true;
						Values.SaveDailyChallenge();
					}
				}
			}
		},
		//---------------------------Die against Cloudy
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = Values.stats.killedByBoss;
			}
			if(gameStarted && !((bool) obj[0])){
				obj[0] = true;//on game started
				obj[1] = Values.stats.killedByBoss;
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;//on game ended
				if(Values.stats.killedByBoss > (int) obj[1]){
					//you just got killed by a boss but which one ?
					int points = Values.GetPointCounter().GetPoints();
					if(points > (Values.bossEvery * Values.scoreMultiplier)*4){
						//cloudy
						DailyChallengeListener.ClearChannel(channel);
						challenge.justCompleted = true;
						Values.SaveDailyChallenge();
					}
				}
			}
		},
		//---------------------------Change costume
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.activeColor;
			}
			if(Values.activeColor != (Colors) obj[0]){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
		},
		//---------------------------Change costume
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.activeCostume;
			}
			if(Values.activeCostume != (Costume) obj[0]){
				DailyChallengeListener.ClearChannel(channel);
				challenge.justCompleted = true;
				Values.SaveDailyChallenge();
			}
		},
		//---------------------------Buy something in the shop
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				GameController.CreateMoneyCounter();
				obj[0] = Values.GetMoneyCounter().GetMoney();
			}
			if(Values.GetMoneyCounter() != null){
				if(Values.GetMoneyCounter().GetMoney() > (int) obj[0]){
					obj[0] = Values.GetMoneyCounter().GetMoney();
				}else if(Values.GetMoneyCounter().GetMoney() < (int) obj[0]){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------use an item in a game
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.inventory.shield;
				obj[1] = Values.inventory.clairvoyance;
				obj[2] = Values.inventory.respawn;
			}
			if(Values.inventory != null){
				if(Values.inventory.shield > (int) obj[0]){
					obj[0] = Values.inventory.shield;
				}else if(Values.inventory.shield < (int) obj[0]){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}

				if(Values.inventory.clairvoyance > (int) obj[1]){
					obj[1] = Values.inventory.clairvoyance;
				}else if(Values.inventory.clairvoyance < (int) obj[1]){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}

				if(Values.inventory.respawn > (int) obj[2]){
					obj[2] = Values.inventory.respawn;
				}else if(Values.inventory.respawn < (int) obj[2]){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------use 3 item in a game
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = Values.inventory.shield;
				obj[1] = Values.inventory.clairvoyance;
				obj[2] = Values.inventory.respawn;
				challenge.maxProgress = 3;
				if(challenge.progress == -1){
					challenge.progress = 0;
				}
			}
			if(Values.inventory != null){
				if(Values.inventory.shield > (int) obj[0]){
					obj[0] = Values.inventory.shield;
				}else if(Values.inventory.shield < (int) obj[0]){
					obj[0] = Values.inventory.shield;
					challenge.progress++;
					Values.SaveDailyChallenge();
				}

				if(Values.inventory.clairvoyance > (int) obj[1]){
					obj[1] = Values.inventory.clairvoyance;
				}else if(Values.inventory.clairvoyance < (int) obj[1]){
					obj[1] = Values.inventory.clairvoyance;
					challenge.progress++;
					Values.SaveDailyChallenge();
				}

				if(Values.inventory.respawn > (int) obj[2]){
					obj[2] = Values.inventory.respawn;
				}else if(Values.inventory.respawn < (int) obj[2]){
					obj[2] = Values.inventory.respawn;
					challenge.progress++;
					Values.SaveDailyChallenge();
				}

				if(challenge.progress >= 3){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------get to 10 000 points 3 times with 3 differents costumes
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				if(challenge.val[0] == null){
					challenge.val[0] = new ArrayList();
					Values.SaveDailyChallenge();
				}
				if(challenge.progress == -1){
					challenge.progress = 0;
					Values.SaveDailyChallenge();
				}
			}
			if(gameStarted && !((bool) obj[0])){
				obj[0] = true;//game started
			}

			if(gameEnded && (bool) obj[0]){
				obj[0] = false;//game ended
				if(Values.GetPointCounter().GetPoints() >= 10000){
					ArrayList a = (ArrayList) challenge.val[0];
					if(!a.Contains(Values.activeCostume)){
						challenge.progress++;
						((ArrayList) challenge.val[0]).Add(Values.activeCostume);
						Values.SaveDailyChallenge();
					}
				}
				if(challenge.progress >= 3){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		},
		//---------------------------beat your high score
		(DailyChallengeListener.ChallengeListener channel, Challenge challenge, object[] obj) => {
			if(obj[0] == null){
				obj[0] = false;
				obj[1] = Values.stats.highScore;
			}
			if(gameStarted && !((bool) obj[0])){
				obj[0] = true;
			}
			if(gameEnded && (bool) obj[0]){
				obj[0] = false;
				if(Values.GetPointCounter().GetPoints() > (int) obj[1]){
					DailyChallengeListener.ClearChannel(channel);
					challenge.justCompleted = true;
					Values.SaveDailyChallenge();
				}
			}
		}
	};

	public static float[] rewardPercents = new float[]{20f, 15f, 15.5f, 20f, 5f, 10f, 5f, 3f, 2f, 2f, 2f, 0.5f};

	public static RewardDelegate[] rewards = new RewardDelegate[]{
		() => {
			GameController.CreateMoneyCounter();
			Values.GetMoneyCounter().AddMoney(100);
			Values.GetMoneyCounter().SaveMoney();
			return null;
		},
		() => {
			GameController.CreateMoneyCounter();
			Values.GetMoneyCounter().AddMoney(200);
			Values.GetMoneyCounter().SaveMoney();
			return null;
		},
		() => {
			GameController.CreateMoneyCounter();
			Values.GetMoneyCounter().AddMoney(400);
			Values.GetMoneyCounter().SaveMoney();
			return null;
		},
		() => {
			GameController.CreateMoneyCounter();
			Values.GetMoneyCounter().AddMoney(1000);
			Values.GetMoneyCounter().SaveMoney();
			return null;
		},
		() => {
			GameController.CreateMoneyCounter();
			Values.GetMoneyCounter().AddMoney(2000);
			Values.GetMoneyCounter().SaveMoney();
			return null;
		},
		() => {
			Values.inventory.shield += 5;
			Values.SaveInventory();
			return null;
		},
		() => {
			Values.inventory.shield += 5;
			Values.inventory.clairvoyance += 5;
			Values.SaveInventory();
			return null;
		},
		() => {
			GameController.CreateMoneyCounter();
			Values.GetMoneyCounter().AddMoney(10000);
			Values.GetMoneyCounter().SaveMoney();
			return null;
		},
		() => {
			GameController.CreateMoneyCounter();
			Values.GetMoneyCounter().AddMoney(50000);
			Values.GetMoneyCounter().SaveMoney();
			return null;
		},
		() => {
			int r = UnityEngine.Random.Range(1, (int) Costume.King + 1);
			FieldInfo field = typeof(Inventory).GetField("costume_" + ((Costume) r).ToString().ToLower());//todo test that and the one below
			field.SetValue(Values.inventory, true);
			Values.SaveInventory();
			return (Costume) r;
		},
		() => {
			int r = UnityEngine.Random.Range(1, (int) Colors.Diamond);
			FieldInfo field = typeof(Inventory).GetField("color_" + ((Colors) r).ToString().ToLower());
			field.SetValue(Values.inventory, true);
			Values.SaveInventory();
			return (Colors) r;
		},
		() => {
			Values.inventory.color_silver = true;
			Values.SaveInventory();
			return null;
		}
	};
}

[Serializable]
public class Challenge{
	public int id;
	public bool completed = false;
	public bool justCompleted = false;
	public int progress = -1;
	public int maxProgress = -1;
	public object[] val = new object[10];
	public int rewarded = -1;
	public bool justRewarded = false;

	public Challenge(int id){
		this.id = id;
	}

	public string GetDesc(){
		return DailyChallenges.descriptions[id];
	}

	public object CompleteAndGetReward(){
		if (completed)
			return null;
		completed = true;
		return DailyChallenges.rewards [id] ();
	}

	public void StartListening(int channel){
	//	Debug.Log ("trying to listen " + channel + ":" + id + "/" +completed + ":" + justCompleted);
		if (completed || justCompleted)
			return;
		//Debug.Log (channel + " is listening at " + id);
		switch (channel) {
		case 1:
			DailyChallengeListener.ch1Listener = null;
			DailyChallengeListener.ch1Listener += DailyChallenges.listeners [id];
			break;
		case 2:
			DailyChallengeListener.ch2Listener = null;
			DailyChallengeListener.ch2Listener += DailyChallenges.listeners [id];
			break;
		case 3:
			DailyChallengeListener.ch3Listener = null;
			DailyChallengeListener.ch3Listener += DailyChallenges.listeners [id];
			break;
		}
	}
}