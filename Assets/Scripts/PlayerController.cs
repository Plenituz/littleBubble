using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rg;
	private CircleCollider2D coll;
	private Animator animator;
	private PointCounter pointCounter;
	private MoneyCounter moneyCounter;

	public float velocityBurstBuffer = 0f;
	public float velocityBumpBuffer = 0f;
	public ParticleSystem leftSparks;
	public ParticleSystem rightSparks;
	public SpriteRenderer costume;
	public float speed = 5f;
	public bool killable = true;
	public bool inCloud = false;
	public bool shielded = false;
	public bool clairvoyant = false;
	private GameObject shieldObj;
	private float lastHitByLaser = 0f;

	SerialPort stream;
	Vector3 startPosition;
	//Quaternion startRotation;
	public float smooth = 2.0F;
	float convert = 360f/1024f;
	public string port = "COM4";
	
	void Start(){
		/*stream = new SerialPort("COM4", 9600);
		stream.ReadTimeout = 50;
		stream.Open();*/

		rg = GetComponent<Rigidbody2D> ();
		coll = GetComponent<CircleCollider2D> ();
		animator = GetComponent<Animator> ();
		pointCounter = Values.GetPointCounter ();
		moneyCounter = Values.GetMoneyCounter ();
		speed = Values.lateralSpeed;
		animator.SetInteger ("Color", (int) Values.activeColor);
		costume.sprite = Values.v.costumeSprites [(int)Values.activeCostume];
		switch (Values.activeColor) {
		case Colors.Rubis:
			GameObject particleEffect = Instantiate (Values.v.RubisParticle) as GameObject;
			particleEffect.GetComponent<Parent>().parent = transform;
			break;
		case Colors.Saphir:
			particleEffect = Instantiate (Values.v.SaphirParticle) as GameObject;
			particleEffect.GetComponent<Parent>().parent = transform;
			break;
		case Colors.Emerald:
			particleEffect = Instantiate (Values.v.EmeraldParticle) as GameObject;
			particleEffect.GetComponent<Parent>().parent = transform;
			break;
		case Colors.Diamond:
			particleEffect = Instantiate (Values.v.DiamondParticle) as GameObject;
			particleEffect.GetComponent<Parent>().parent = transform;
			break;
		case Colors.Golden:
			particleEffect = Instantiate (Values.v.GoldenParticle) as GameObject;
			particleEffect.GetComponent<Parent>().parent = transform;
			break;
		}
		//StartCoroutine ("thread");
	}

	void FixedUpdate () {
		pointCounter.AddPoint ((int) (Values.playerSpeed * Values.scoreMultiplier));

		float moveH = Input.GetAxis ("Horizontal");
		if (Input.touchCount > 0) {
			Touch t = Input.GetTouch(0);
			if(t.position.x > Screen.width/2)
				moveH = 1.0f;
			else
				moveH = -1.0f;
		}
		float velocityBurstTick = 0f;
		float velocityBumpTick = 0f;
		float velocityDown = 0f;
		if (velocityBurstBuffer != 0f) {
			velocityBurstTick = Mathf.Abs (velocityBurstBuffer) >= Values.wind_burstVelocityPerTick ? 
				Values.wind_burstVelocityPerTick * Mathf.Sign (velocityBurstBuffer)
					: 
				velocityBurstBuffer;
			velocityBurstBuffer -= velocityBurstTick;
		}
		if (velocityBumpBuffer != 0f) {
			velocityBumpTick = Mathf.Abs (velocityBumpBuffer) >= Values.bumpVelocityPerTick ? 
				Values.bumpVelocityPerTick * Mathf.Sign (velocityBumpBuffer)
					: 
					velocityBumpBuffer;
			velocityBumpBuffer -= velocityBumpTick;
		}

		if (killable && inCloud) {
			velocityDown = -Values.cloudSlowPosPerTick;
			inCloud = false;
		} else if (transform.position.y < Values.playerHeight)
			velocityDown = Values.playerGoingUpSpeed;
		else if (transform.position.y > Values.playerHeight) {
			float distance = Values.dist (transform.position.x, transform.position.y, transform.position.x, Values.playerHeight);
			velocityDown = distance < Values.playerGoingDownSpeed ? -distance : -Values.playerGoingDownSpeed;
		}else
			velocityDown = 0f;
		if (Values.inversControl) {
			moveH *= -1;
		}

		rg.velocity = new Vector2 ((moveH * speed) + velocityBurstTick + (velocityBumpTick), velocityDown + (Mathf.Abs(velocityBumpTick)/2));
		if (velocityBumpTick != 0f) {
			ComplexNumber c = new ComplexNumber (rg.velocity.x, rg.velocity.y, ComplexNumber.NUMERICAL);
			ComplexNumber c2 = new ComplexNumber (c.getModulus (), velocityBumpTick > 0 ? 45f * Mathf.Deg2Rad : 135f * Mathf.Deg2Rad, ComplexNumber.GEOMETRICAL);
			rg.velocity = new Vector2 (c2.getRealPart (), c2.getImaginaryPart ());
		}

		float right = (6.25f - coll.bounds.extents.x);
		float left = (coll.bounds.extents.x);

		//particles
		if (transform.position.x > right) {
			if (!rightSparks.isPlaying)
				rightSparks.Play ();
		}else if (rightSparks.isPlaying)
			rightSparks.Stop ();
		
		if (transform.position.x < left) {
			if (!leftSparks.isPlaying)
				leftSparks.Play ();
		}else if (leftSparks.isPlaying)
			leftSparks.Stop ();

		//clamping
		if (transform.position.x > right) 
			transform.position = new Vector2 (right, rg.position.y);
		if (transform.position.x < left)
			transform.position = new Vector2 (left, rg.position.y);

		if (shielded && shieldObj == null) {
			shieldObj = Instantiate (Values.v.PlayerShield) as GameObject;
			shieldObj.transform.SetParent (transform);
			shieldObj.transform.localPosition = Vector3.zero;
			shieldObj.transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
		}
		if (!shielded && shieldObj != null) {
			Destroy (shieldObj);
		}

		///arduino
		/// 
		/// 
		/*string arduinoString = ReadFromArduino(100);

		if(string.IsNullOrEmpty(arduinoString))
			return;
		int sensor1 = 0;
		//try{
			sensor1 = Int32.Parse (arduinoString);
		//}catch(Exception e){

		//}
		float a = sensor1 * (6f/1023f);
		transform.position = new Vector2 (a, transform.position.y);
		//int sensor2 = Int32.Parse(splitted[1]);
		//Debug.Log(a);*/
	}

	IEnumerator thread(){
		while (true) {
			string arduinoString = ReadFromArduino(100);


			if (string.IsNullOrEmpty (arduinoString)) {
				yield return new WaitForEndOfFrame ();

				continue;
			}
			Debug.Log (arduinoString);
			int sensor1 = 0;
			//try{
			sensor1 = int.Parse (arduinoString);
			//}catch(Exception e){

			//}
			float a = sensor1 * (6f/1023f);
			transform.position = new Vector2 (a, transform.position.y);

			yield return new WaitForEndOfFrame ();
		}
	}

	void AddVelocityBurst(float burst){
		velocityBurstBuffer += burst;
	}

	void AddVelocityBump(float bump){
		velocityBumpBuffer += bump;
	}

	void Killable(bool v){
		killable = v;
	}

	void SetState(int state){
		animator.SetInteger ("State", state);
	}

	void AddSpeed(float s){
		speed += s;
	}

	void RemoveSpeed(float s){
		speed -= s;
	}

	void OnTriggerEnter2D(Collider2D other){
		//Values.MakeToast (other.tag);

		if (other.tag.Equals("Laser")){
			if (Time.time - lastHitByLaser < 0.5f) {
				return;
			}
			lastHitByLaser = Time.time;
		}
		switch (other.tag) {
		case "Cloud":
		case "ChargedCloud":
			inCloud = true;
			break;
		case "Trampo":
			Values.stats.trampoTouched++;
			break;
		case "Boundary":
			break;
		case "MalusChest":
			if (!(BonusPowers.active && (BonusPowers.activePower.Equals("FIRE") || BonusPowers.activePower.Equals("GOLDEN")))) {
				other.SendMessage ("Activate");
				Values.stats.malusChestTaken++;
			}
			break;
		case "BonusChest":
			other.SendMessage ("Activate");
			Values.stats.bonusChestTaken++;
			break;
		case "Bulle":
			moneyCounter.AddMoney (Values.moneyPerBulle * (Values.moneyMultiplier + Values.tmpMoneyMultiplier));
			other.SendMessage ("AbsorbTo", transform.position);
			Values.stats.totalBulleCount++;
			break;
		default:
			if (shielded && killable) {
				shielded = false;
				GameObject go = Instantiate(Values.v.BonusChest) as GameObject;
				go.SendMessage("Init", new object[]{Bonus.GOLDEN, -10f, -10f});
				go.SendMessage ("Activate");
				//explosion ?
				break;
			}

			if(killable){
				GameController g = (GameController)FindObjectOfType(typeof(GameController));
				//g.SendMessage("Reset");
				if(g != null)
					g.SendMessage("PlayerDied");
				Values.stats.deathNumber++;
				switch (other.tag) {
				case "Spikes":
					Values.stats.killedBySpikes++;
					break;
				case "Bird":
					Values.stats.killedByBird++;
					break;
				case "Plane":
					Values.stats.killedByPlane++;
					break;
				case "Killer":
				case "Lightning":
					Values.stats.killedByCloud++;
					break;
				case "Laser":
					Values.stats.killedByLaser++;
					break;
				case "BlackHole":
					Values.stats.killedByBlackHole++;
					break;
				case "Goutte":
					Values.stats.killedByRain++;
					break;
				case "Boss":
					Values.stats.killedByBoss++;
					break;
				}
			}else
				other.SendMessage("Kill");
			break;
		}
	}
	void OnTriggerStay2D(Collider2D other){
		switch (other.tag) {
		case "Cloud":
		case "ChargedCloud":
			inCloud = true;
			break;
		}
	}

	string readLine;
	public string ReadFromArduino (int timeout = 0) {
		stream.ReadTimeout = timeout;        
		try {
			readLine = stream.ReadLine();
			stream.DiscardInBuffer();
			return readLine;
		}
		catch (Exception e) {
			stream.DiscardInBuffer();
			return null;
		}

	}

}
