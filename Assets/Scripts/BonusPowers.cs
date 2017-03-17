using UnityEngine;
using System.Collections;

public class BonusPowers : MonoBehaviour {

	/**
	 * This is on BonusChest (Prefab)
	 */

	private static IconSliderHandler handler;
	public static bool active = false;
	public static string activePower;
	public Sprite[] icons = new Sprite[7];
	private PlayerController playerController;
	private SpriteRenderer sr;
	private Sprite basicSprite;

	void Awake(){
		playerController = Values.GetPlayer ().GetComponent<PlayerController> ();
		sr = GetComponent<SpriteRenderer> ();
		basicSprite = sr.sprite;
		if (playerController.clairvoyant) {
			Destroy (transform.GetChild (0).gameObject);
			switch (name) {
			case "MINI":
				sr.sprite = icons [0];
				break;
			case "EXPLO":
				sr.sprite = icons [1];
				break;
			case "FIRE":
				sr.sprite = icons [2];
				break;
			case "MAGNE":
				sr.sprite = icons [3];
				break;
			case "GOLDEN":
				sr.sprite = icons [4];
				break;
			case "BUBBLE_RAIN":
				sr.sprite = icons [5];
				break;
			case "FLOAT":
				sr.sprite = icons [6];
				break;
			}
		}
		IconSliderHandler[] o = Resources.FindObjectsOfTypeAll<IconSliderHandler> ();
		foreach (IconSliderHandler g in o) {
			if (g.CompareTag ("ActiveBonus")) {
				handler = g;
				break;
			}
		}
	}

	void Update(){
		if (playerController.clairvoyant && sr.sprite == basicSprite) {
			try{
				Destroy (transform.GetChild (0).gameObject);
			}catch(System.Exception){
			}
			switch (name) {
			case "MINI":
				sr.sprite = icons [0];
				break;
			case "EXPLO":
				sr.sprite = icons [1];
				break;
			case "FIRE":
				sr.sprite = icons [2];
				break;
			case "MAGNE":
				sr.sprite = icons [3];
				break;
			case "GOLDEN":
				sr.sprite = icons [4];
				break;
			case "BUBBLE_RAIN":
				sr.sprite = icons [5];
				break;
			case "FLOAT":
				sr.sprite = icons [6];
				break;
			}
		}
	}

	public void Activate(){
		if (active)
			return;
		StartCoroutine ("AbsorbTo", Values.GetPlayer ().transform.position);
		handler.SetValue (1.0f);
		handler.SetIcon (gameObject.name);
		handler.SetVisible (true);
	}

	void Reset(){
		handler.SetVisible (false);
		active = false;
		activePower = "";
	}

	IEnumerator AbsorbTo(Vector3 pos){
		Destroy (GetComponent<CircleCollider2D> ());
		float distance = Values.dist (pos.x, pos.y, transform.position.x, transform.position.y);
		while (distance > Values.bulleAttractStep && transform.localScale.x > 0) {
			transform.localScale -= new Vector3 (Values.bulleScaleStep, Values.bulleScaleStep, 0);
			distance = Values.dist (pos.x, pos.y, transform.position.x, transform.position.y);
			transform.position = pos + (transform.position - pos).normalized*(distance - Values.bulleAttractStep);
			yield return null;
		}
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		for (int i = 0; i < transform.childCount; i++) {
			Destroy (transform.GetChild (i).gameObject);
		}
		active = true;
		activePower = gameObject.name;
		StartCoroutine (gameObject.name);
	}

	IEnumerator WaitAndUpdateSlider(float time){
		float startTime = Time.time;
		while (Time.time - startTime < time) {
			handler.SetValue (Mathf.Abs(((Time.time - startTime) / time)-1f));
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator MINI(){
		//find the player since there should only be one at anytime
		GameObject player = Values.GetPlayer ();
		player.transform.localScale /= 2f;
		yield return WaitAndUpdateSlider(Values.miniDuration + (Values.inventory.bonusLevel_MINI * Values.bonusAddPerLevel));
		player.transform.localScale *= 2f;
		Destroy (gameObject);
		Reset ();
	}

	IEnumerator EXPLO(){
		//find the player
		GameObject player = Values.GetPlayer ();
		//store the original sprite and replace it with the new one
		player.SendMessage ("SetState", (int)Bonus.EXPLO);
		//click handler
		TickAction.actions += ExploClickHandler;
		yield return WaitAndUpdateSlider(Values.exploDuration + (Values.inventory.bonusLevel_EXPLO * Values.bonusAddPerLevel));
		//after wait get tide of the click handler
		TickAction.actions -= ExploClickHandler;
		//set back the sprite
		player.SendMessage ("SetState", (int) 0f);
		Destroy (gameObject);
		Reset ();
	}

	void ExploJumpHandler(){
		GameObject player = Values.GetPlayer ();
		GameObject exp = Instantiate (Values.v.explosion_exploBulle) as GameObject;
		exp.transform.position = player.transform.position;
		Instantiate (Values.v.ExploJump);
	}

	void ExploClickHandler(){
		if (Input.GetKeyDown ("space"))//control touch here
			ExploJumpHandler ();
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).position.y/Screen.height < 0.2f) {
			ExploJumpHandler ();
		}
	}

	IEnumerator FIRE(){
		GameObject player = Values.GetPlayer ();
		player.SendMessage ("Killable", false);
		player.SendMessage ("SetState", (int) Bonus.FIRE);
		Values.playerSpeed += Values.fireSpeedAdd;
		GameObject fakePlayer = Instantiate (Values.v.FakePlayer) as GameObject;
		fakePlayer.GetComponent<SpriteRenderer> ().sprite = player.GetComponent<SpriteRenderer> ().sprite;
		fakePlayer.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.494117f, 0.494117f, 1.0f);
		fakePlayer.GetComponent<Parent> ().parent = player.transform;
		fakePlayer.GetComponent<Parent> ().offset = new Vector3 (0f, 0.36f, 0f);
		fakePlayer.transform.localScale = new Vector3 (0.1384818f, 0.1384818f, 1f);
		GameObject cost = player.transform.FindChild ("Costume").gameObject;
		cost.transform.localScale = new Vector3 (0.5160695f, 0.5160695f, 1f);
		cost.transform.localPosition = new Vector3 (0f, 1.37f, 0f);


		CircleCollider2D collider = player.GetComponent<CircleCollider2D> ();
		Vector3 oldScale = player.transform.localScale;
		Vector2 oldOffset = collider.offset;
		float oldRadius = collider.radius;
		collider.offset = new Vector2 (0f, 1.22f);
		collider.radius = 1.25f;
		player.transform.localScale = new Vector3 (0.3f, 0.3f, 1f);

		yield return WaitAndUpdateSlider(Values.fireDuration + (Values.inventory.bonusLevel_FIRE * Values.bonusAddPerLevel));
		Values.playerSpeed -= Values.fireSpeedAdd;
		player.SendMessage ("SetState", (int) 0f);
		player.SendMessage ("Killable", true);

		collider.offset = oldOffset;
		collider.radius = oldRadius;
		player.transform.localScale = oldScale;

		cost.transform.localPosition = new Vector3 (0f, 0f, 0f);
		cost.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		Destroy (fakePlayer);
		Destroy (gameObject);
		Reset ();
	}

	IEnumerator MAGNE(){
		float startTime = Time.time;
		GameObject player = Values.GetPlayer ();
		while (Time.time - startTime < (Values.magneDuration + (Values.inventory.bonusLevel_MAGNE * Values.bonusAddPerLevel))) {
			handler.SetValue (Mathf.Abs (((Time.time - startTime) / (Values.magneDuration + (Values.inventory.bonusLevel_MAGNE * Values.bonusAddPerLevel))) - 1f));
			MonoBehaviour[] bulles = (MonoBehaviour[])FindObjectsOfType (typeof(InitBulle));
			foreach(MonoBehaviour bulle in bulles){
				if(bulle == null)
					continue;
				
				bulle.gameObject.transform.position = new Vector3(
					bulle.gameObject.transform.position.x - (DistX(bulle.gameObject.transform.position, player.transform.position) * Time.smoothDeltaTime * Values.magneSpeedMultiplier),
					bulle.gameObject.transform.position.y < player.transform.position.y ?
					bulle.gameObject.transform.position.y
					:
					bulle.gameObject.transform.position.y - (DistY(bulle.gameObject.transform.position, player.transform.position) * Time.smoothDeltaTime * Values.magneSpeedMultiplier),
					0f);
			}
			yield return null;
		}
		Destroy (gameObject);
		Reset ();
	}

	float DistX(Vector3 p1, Vector3 p2){
		return (p1.x - p2.x);
	}
	float DistY(Vector3 p1, Vector3 p2){
		return (p1.y - p2.y);
	}

	IEnumerator GOLDEN(){
		GameObject player = Values.GetPlayer ();
		SpriteRenderer sr = player.GetComponent<SpriteRenderer> ();
		//player.SendMessage ("SetState", (int)Bonus.GOLDEN);
		sr.color = Color.yellow;
		player.SendMessage ("Killable", false);

		float startTime = Time.time;
		float last = Time.time;
		while (Time.time - startTime < (Values.goldDuration + (Values.inventory.bonusLevel_GOLDEN * Values.bonusAddPerLevel))) {
			handler.SetValue (Mathf.Abs (1f - ((Time.time - startTime) / (Values.goldDuration + Values.inventory.bonusLevel_GOLDEN * Values.bonusAddPerLevel))));
			if (Time.time - startTime > (Values.goldDuration + (Values.inventory.bonusLevel_GOLDEN * Values.bonusAddPerLevel)) - Values.goldWarningDuration) {
				//clignotte
				if (Time.time - last > 0.2f) {
					last = Time.time;
					sr.color = sr.color == Color.yellow ? Color.white : Color.yellow;
				}
			}
			yield return new WaitForEndOfFrame ();
		}

		sr.color = Color.white;
		player.SendMessage ("Killable", true);
		Destroy (gameObject);
		Reset ();
	}

	IEnumerator BUBBLE_RAIN(){
		float startTime = Time.time;
		Values.tmpMoneyMultiplier = 1;
		while (Time.time - startTime < (Values.bubble_rainDuration + (Values.inventory.bonusLevel_BUBBLE_RAIN * Values.bonusAddPerLevel))) {
			handler.SetValue (Mathf.Abs (((Time.time - startTime) / (Values.bubble_rainDuration + (Values.inventory.bonusLevel_BUBBLE_RAIN * Values.bonusAddPerLevel))) - 1f));
			for(int i = 0; i < Values.bubble_rainSpawnPerRound; i++){
				Patterns.SpawnBulle(Random.Range(Values.leftBulleSpawn, Values.rightBulleSpawn), Values.topBulleSpawn);
			}
			yield return new WaitForSeconds(Values.bubble_rainTimeBetweenRound);
		}
		Values.tmpMoneyMultiplier = 0;
		Destroy (gameObject);
		Reset ();
	}

	IEnumerator FLOAT(){
		MalusPower[] m = (MalusPower[]) FindObjectsOfType (typeof(MalusPower));
		foreach (MalusPower p in m) {
			p.SendMessage("StopMeteorWaterAndWind");
		}
		yield return new WaitForSeconds (0.1f);
		Destroy(gameObject);
		Reset ();
	}

		                                 
}
