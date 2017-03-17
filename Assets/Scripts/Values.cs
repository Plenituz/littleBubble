using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Values : MonoBehaviour {
	/*
	 * if you add a boss you have to redo the DailyChallengeListener for spookie krug and cloudy, do it with real stats
	 * same for the items. you better do a listener with reflection that list through the field directly
	 * 
	 * TODOS :
	 * Animation de destruction des obstacle (PlayerController send a "Kill" message on collision)
	 * compter le score, quand le pouvoir pluie de bulle est activé compter double 
	 * le pouvoir magne et bubble_rain et wind_burst ne se pause pas (il prennent en compte Time.time), voir si on peut faire avec Time.scaledTime ? et le cloudlightning
	 * animate scale of buttons onPreClick ?
	 * bouclier 
	 * reanimation
	 * mettre dontdestroyOnLoad sur le bg du main menu et le faire descendre quand il arrive dans le menu credit et se fait detruire automatiquement apres etre descendu
	 * de meme quand on revient des credits
	 * mettre un gameobject dans la scene "Yeller" qui envoie un message a tous les gamesobjects presents quand la scene start pour demarrer le countdown/movement des objets qui viennent des enciennes scenes
	 * 
	 * ajouter des particules nuage au cul de l'avion
	 * ajouter une option pour desactiver les particules 
	 * 
	 * modifier la hitbox du plane quand il se transforme en spaceship
	 * enlever la fumée de l'avion quand il est en spaceship
	 * 
	 * kroug est pas fini
	 * 
	 * quand le 21 est spawné avant le pattern de ninja sombre tu peux pas survivre
	 * 
	 * todo test the challenge were you have to get killed by x boss in real conditions
	 */

	public GameObject Spikes;
	public GameObject Bird;
	public GameObject Plane;
	public GameObject BonusChest;
	public GameObject MalusChest;
	public GameObject Bulle;
	public GameObject Trampo;
	public GameObject ChargedCloud;
	public GameObject Cloud;
	public GameObject LaserMiddlePart;
	public GameObject LaserRightPart;
	public GameObject LaserLeftPart;
	public GameObject Laser;
	public GameObject BlackHole;
	public GameObject Goutte;
	public GameObject Meteor;
	public GameObject ExploJump;
	public GameObject SpookieProjectile;
	public GameObject Spookie;
	public GameObject KrougObject;
	public GameObject Kroug;
	
	public GameObject explosion_exploBulle;
	public GameObject windToLeftContinue;
	public GameObject windToRightContinue;
	public GameObject windToLeftBurst;
	public GameObject windToRightBurst;
	public GameObject mainMenuBubbles;
	public GameObject planeSmoke;
	public GameObject RubisParticle;
	public GameObject SaphirParticle;
	public GameObject EmeraldParticle;
	public GameObject DiamondParticle;
	public GameObject GoldenParticle;

	public GameObject _PointCounter;
	public GameObject _MoneyCounter;
	public GameObject SpeedIncrementer;
	public GameObject Player;
	public GameObject FakePlayer;
	public GameObject PlayerShield;
	public GameObject Toast;
	public GameObject DailyChallengeDisplay;
	public GameObject EventSystem;

	public GameObject p3;

	public Sprite[] colorSprites = new Sprite[10];
	public Sprite[] costumeSprites = new Sprite[9];

	public AudioClip Achatmp3;
	public AudioClip Refus_Achatmp3;

	public static Values v;
	public static Statistics stats;
	public static Inventory inventory;

	void Awake(){
		if (v == null) {
			v = this;
			DontDestroyOnLoad (gameObject);
			LoadStats ();
			LoadValues ();
			LoadInventory ();
			LoadDailyChallenge ();
		} else if (v != this) {
			Destroy (gameObject);
		}
	}

	public static void LoadInventory(){
		if (File.Exists (Application.persistentDataPath + INVENTORY_PATH)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + INVENTORY_PATH, FileMode.Open);
			inventory = (Inventory) bf.Deserialize (file);
			file.Close ();
		} else {
			inventory = new Inventory ();
			SaveInventory ();
		}
	}

	public static void SaveInventory(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + INVENTORY_PATH);
		bf.Serialize (file, inventory);
		file.Close ();
	}

	public static void LoadValues(){
		if (File.Exists (Application.persistentDataPath + VALUES_PATH)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + VALUES_PATH, FileMode.Open);
			SavedValues s = (SavedValues) bf.Deserialize (file);
			file.Close ();

			sharedOnFacebook = s.sharedOnFacebook;
			sharedOnTwitter = s.sharedOnTwitter;
			moneyMultiplier = s.moneyMultiplier;
			scoreMultiplier = s.scoreMultiplier;
			lateralSpeed = s.lateralSpeed;
			activeColor = s.activeColor;
			activeCostume = s.activeCostume;
		} else {
			SaveValues ();
		}
	}

	public static void SaveValues(){
		SavedValues s = new SavedValues ();
		s.sharedOnTwitter = sharedOnTwitter;
		s.sharedOnFacebook = sharedOnFacebook;
		s.moneyMultiplier = moneyMultiplier;
		s.scoreMultiplier = scoreMultiplier;
		s.lateralSpeed = lateralSpeed;
		s.activeColor = activeColor;
		s.activeCostume = activeCostume;

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + VALUES_PATH);
		bf.Serialize (file, s);
		file.Close ();
	}

	public static void LoadStats(){
		if (File.Exists (Application.persistentDataPath + STATS_PATH)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + STATS_PATH, FileMode.Open);
			stats = (Statistics) bf.Deserialize (file);
			file.Close ();
		} else {
			stats = new Statistics();
		}
	}

	public static void SaveStats(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + STATS_PATH);
		bf.Serialize (file, stats);
		file.Close ();
	}

	public static void SaveDailyChallenge(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + CHALLENGE_PATH);
		Challenge[] c = new Challenge[]{ DailyChallenges.ch_1, DailyChallenges.ch_2, DailyChallenges.ch_3 };
		bf.Serialize (file, c);
		file.Close ();
	}

	public static void LoadDailyChallenge(){
		//File.Delete (Application.persistentDataPath + CHALLENGE_PATH);
		//File.Delete (Application.persistentDataPath + CHALLENGE_TIME_PATH);
		if (File.Exists (Application.persistentDataPath + CHALLENGE_PATH)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + CHALLENGE_PATH, FileMode.Open);
			Challenge[] c = (Challenge[])bf.Deserialize (file);
			DailyChallenges.ch_1 = c [0];
			DailyChallenges.ch_2 = c [1];
			DailyChallenges.ch_3 = c [2];

			DailyChallenges.ch_1.StartListening (1);
			DailyChallenges.ch_2.StartListening (2);
			DailyChallenges.ch_3.StartListening (3);
			file.Close ();
		} else {
			SaveDailyChallenge ();
		}
		DailyChallenges.CreateChallenges ();
	}

	public static GameObject ShowDailyChallenges(){
		return Instantiate (v.DailyChallengeDisplay) as GameObject;
	}

	public static void MakeToast(string text){
		MakeToast (text, 5f);
	}

	public static void MakeToast(string text, float time){
		GameObject toast = Instantiate (v.Toast) as GameObject;
		toast.SendMessage ("SetText", text);
		toast.SendMessage ("SetTime", time);
	}

    public static float Map(float from, float to, float from2, float to2, float value){
        if(value <= from2){
            return from;
        }else if(value >= to2){
            return to;
        }else{
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }


	//----------BIRD-------------
	public static float leftBirdSpawn = -0.3f;//pos
	public static float rightBirdSpawn = 6.5f;//pos
	public static int birdAsteroidFrom = 10000;

	//----------PLANE-------------
	public static float leftPlaneSpawn = -0.63f;//pos
	public static float rightPlaneSpawn = 6.89f;//pos
	public static int planeOVNIFrom = 10000;

	//----------SPIKE-------------
	public static float topSpikeSpawn = 10.27f;//pos

	//----------BULLE-------------
	public static float topBulleSpawn = 11.3f;//pos
	public static float leftBulleSpawn = 0.6f;
	public static float rightBulleSpawn = 5.95f;
	public static float bulleAttractStep = 0.1f;
	public static float bulleScaleStep = 0.03f;

	//----------TRAMPO------------
	public static float bumpVelocity = 250f;
	public static float bumpVelocityPerTick = 15f;

	//----------CLOUD-------------
	public static float cloudSlowPosPerTick = 1.8f;
	public static float cloudLightning_minTimeBetweenFire = 1f;//sec
	public static float cloudLightning_maxTimeBetweenFire = 6f;//sec
	public static float cloudLightning_timeOfFire = 3f;
	public static float cloudLightning_chargingTime = 1f;

	//----------BLACKHOLE----------
	public static float blackholeAnimStep = 0.06f;
	public static float blackholeAttractStep = 0.06f;
	public static int minPointToSpawnBlackHole = 10000;

	//----------BONUS-------------
	public static float miniDuration = 5f; //sec

	public static float exploDuration = 5f; //sec
	public static float exploJump = 2f; //speed
	public static float exploJumpTime = 0.3f; //sec

	public static float fireDuration = 5f; //sec
	public static float fireSpeedAdd = 9f;//speed

	public static float magneSpeedMultiplier = 2f;//speed
	public static float magneDuration = 5f;//sec

	public static float goldDuration = 5f;//sec
	public static float goldWarningDuration = 3f;//sec

	public static float bubble_rainDuration = 5f;//sec
	public static int bubble_rainSpawnPerRound = 1;//nbr de bulle
	public static float bubble_rainTimeBetweenRound = 0.1f;//sec

	public const int bonusMaxLevel = 5;
	public const int bonusAddPerLevel = 1;//sec

	//----------MALUS-------------
	public static float gigaDuration = 8f;
	public static float gigaScaleAdd = 0.1f;
	public static float gigaSpeedSoustract = 2f;

	public static float wind_burstDuration = 15f;
	public static float wind_burstVelocityPerTick = 3f;//violence du burst
	public static float wind_burstVelocityPerBurst = 300f;//"longeur" du burst
	public static float wind_burstMinTimeBetweenBurst = 2f;
	public static float wind_burstMaxtimeBetweenBurst = 8f;

	public static float water_rainDuration = 15f;//sec
	public static float water_rainMaxTimeBetweenRound = 1f;//sec
	public static float water_rainMinTimeBetweenRound = 0.1f;//sec
	public static float water_rainPlayerSpeedMultipliedBy = 2.5f;

	public static float meteor_rainDuration = 15f;//sec
	public static float meteor_rainMaxTimeBetweenRound = 2f;//sec
	public static float meteor_rainMinTimeBetweenRound = 0.8f;//sec
	public static float meteor_rainPlayerSpeedMultipliedBy = 2.5f;
	public static float meteor_rainHorizontalSpeed = 3f;
	public static int meteor_rainMinPoints = 10000;

	public static bool inversControl = false;
	public static float inversControlDuration = 15f;

	//----------BOSS---------------
	public static int bossEvery = 10000;
	//----------Spookie---------------
	public static float spookie_minStep = 13f;
	public static float spookie_baseStep = 30f;
	public static float spookie_stepDecrement = 10f;

	public static float spookie_maxSweep = 160f;
	public static float spookie_baseSweep = 80f;
	public static float spookie_sweepIncrement = 50f;

	public static float spookie_maxSpeed = 8f;
	public static float spookie_minSpeed = 2f;
	public static float spookie_maxRandomSweep = 20f;

	public static float spookie_durationPhase1 = 3f;
	public static float spookie_waitPhase1 = 1.5f;
	public static float spookie_durationPhase2 = 10f;
	public static float spookie_waitPhase2 = 1f;
	public static float spookie_durationPhase3 = 15f;
	public static float spookie_waitPhase3 = 2f;
	public static float spookie_phase3ProjectileMinSpeed = 2f;
	public static float spookie_phase3ProjectileMaxSpeed = 10f;
	public static float spookie_phase3ProjectileIncrementSpeed = 2f;
	public static float spookie_waitPhase3Decrement = 1f;
	public static float spookie_waitPhase3Min = 0.3f;
	public static float spookie_durationPhase5 = 3f;


	//----------PLAYER-------------
	public static float playerStartSpeed = 2f;
	public static float playerMaxSpeed = 8f;
	public static float playerSpeed = 5f;//speed
	public static float playerSpeedIncrement = 0.1f;
	public static float playerSpeedTimeBetweenIncrement = 5f;
	public static float playerHeight = 0.8f;
	public static float playerGoingUpSpeed = 0.5f;
	public static float playerGoingDownSpeed = 2f;
	public static float lateralSpeed = 5f;

	public static Colors activeColor = Colors.Base;
	public static Costume activeCostume = Costume.None;

	//----------OTHER-------------
	public static int respawnCountDown = 5;
	public static int repeatPatternCount = 10;

	//----------MONEY-------------
	public static int moneyPerBulle = 1;
	public static int moneyMultiplier = 1;
	public static int tmpMoneyMultiplier = 0;
	public static int scoreMultiplier = 1;
	public static bool sharedOnFacebook = false;
	public static bool sharedOnTwitter = true;
	public const string STATS_PATH = "/stats.dat";
	public const string MONEY_PATH = "/kk.dat";
	public const string VALUES_PATH = "/init.dat";
	public const string INVENTORY_PATH = "/it.dat";
	public const string CHALLENGE_PATH = "/cha.dat";
	public const string CHALLENGE_TIME_PATH = "/chaTime.dat";
	public const int HOURS_BETWEEN_AD = 2;
	public const int HOURS_BETWEEN_RESPAWN_AD = 1;
	public const float LATERAL_SPEED_SLOW = 5f;
	public const float LATERAL_SPEED_MED = 7.5f;
	public const float LATERAL_SPEED_FAST = 10f;

	//----------PRICES-------------
	public const int price_Blue = 5000;
	public const int price_Red = 5000;
	public const int price_Green = 5000;
	public const int price_Rubis = 10000;
	public const int price_Saphir = 10000;
	public const int price_Emerald = 10000;
	public const int price_Golden = 25000;
	public const int price_Diamond = 50000;

	public const int price_Noel = 10000;
	public const int price_Swag = 5000;
	public const int price_MelonHat = 5000;
	public const int price_Sayian = 5000;
	public const int price_Gandalf = 10000;
	public const int price_Demon = 10000;
	public const int price_Bling = 10000;
	public const int price_King = 25000;

	public const int price_lateral1dot5 = 5000;
	public const int price_lateral2 = 5000;

	public const int price_bonusLevel1 = 500;
	public const int price_bonusLevel2 = 2500;
	public const int price_bonusLevel3 = 10000;
	public const int price_bonusLevel4 = 25000;
	public const int price_bonusLevel5 = 50000;

	public const int price_1shield = 300;
	public const int price_5shield = 1200;
	public const int price_10shield = 2000;
	public const int price_20shield = 3500;
	public const int price_50shield = 8000;

	public const int price_1clairevoyance = 300;
	public const int price_5clairevoyance = 1200;
	public const int price_10clairevoyance = 2000;
	public const int price_20clairevoyance = 3500;
	public const int price_50clairevoyance = 8000;

	public const int price_10respawn = 10000;
	public const int price_20respawn = 17500;
	public const int price_50respawn = 35000;

	public static float offsetY = 10.2f;

	public static float dist(float x, float y, float x1, float y1){
		return Mathf.Sqrt (((x - x1) * (x - x1)) + ((y - y1) * (y - y1)));
	}

	public static PointCounter GetPointCounter(){
		return PointCounter.instance;
	}

	public static MoneyCounter GetMoneyCounter(){
		return MoneyCounter.instance;
	}

	public static GameObject GetPlayer(){
		return ((MonoBehaviour) FindObjectOfType (typeof(PlayerController))).gameObject;
	}

	public static SpeedIncrementer GetSpeedIncrementer(){
		return (SpeedIncrementer) FindObjectOfType (typeof(SpeedIncrementer));
	}
}

public enum SideDirection{
	GOING_LEFT,
	GOING_RIGHT
}

public enum CloudState{
	BASE,
	CHARGING,
	ELECT
}

public enum Bonus{
	MINI,
	FIRE,
	MAGNE,
	GOLDEN,
	BUBBLE_RAIN,
	FLOAT,
	END,
	EXPLO
}

public enum Malus{
	GIGA,
	WIND_BURST,
	WATER_RAIN,
	METEOR_RAIN,
	CONTROL_INVERS,
	END
}

public enum Costume{
	None,
	Noel,
	Swag,
	MelonHat,
	Sayian,
	Gandalf,
	Demon,
	Bling,
	King
}

public enum Colors{
	Base,
	Blue,
	Red,
	Green,
	Rubis,
	Saphir,
	Emerald,
	Golden,
	Diamond,
	Silver
}
