using UnityEngine;
using System.Collections;

public class MalusPower : MonoBehaviour {
	public static int active = 0;

	void Activate(){
		StartCoroutine ("AbsorbTo", Values.GetPlayer ().transform.position);
	}

	IEnumerator AbsorbTo(Vector3 pos){
		Destroy (GetComponent<EdgeCollider2D> ());
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
		active++;
		StartCoroutine (gameObject.name);
	}

	IEnumerator GIGA(){
		GameObject player = Values.GetPlayer ();
		player.transform.localScale += new Vector3(Values.gigaScaleAdd, Values.gigaScaleAdd, 0f);
		player.SendMessage ("RemoveSpeed", Values.gigaSpeedSoustract);
		yield return new WaitForSeconds(Values.gigaDuration);
		player.transform.localScale -= new Vector3(Values.gigaScaleAdd, Values.gigaScaleAdd, 0f);
		player.SendMessage ("AddSpeed", Values.gigaSpeedSoustract);
		active--;
		Destroy (gameObject);
	}

	IEnumerator WIND_BURST(){
		Destroy (GetComponent<Mover> ());
		Destroy (GetComponent<Rigidbody2D> ());
		float startTime = Time.time;
		float timeLeft = Values.wind_burstDuration;
		GameObject wind1 = Instantiate (Values.v.windToLeftContinue) as GameObject;
		GameObject wind2 = Instantiate (Values.v.windToRightContinue) as GameObject;
		wind1.transform.SetParent (transform);
		wind2.transform.SetParent (transform);
		GameObject player = Values.GetPlayer ();
		player.SendMessage ("AddVelocityBurst", Values.wind_burstVelocityPerBurst * (Random.Range(0, 2) == 0 ? 1 : -1));
		while (true) {
			timeLeft = Values.wind_burstDuration - (Time.time - startTime);
			if(timeLeft <= 0)
				break;
			float nextBurst = Values.wind_burstMinTimeBetweenBurst + 
				Random.Range(0f, Values.wind_burstMaxtimeBetweenBurst - Values.wind_burstMinTimeBetweenBurst);
			nextBurst = nextBurst > timeLeft ? timeLeft : nextBurst;
			yield return new WaitForSeconds(nextBurst);
			int r = (Random.Range(0, 2) == 0 ? 1 : -1);
			Instantiate(r == 1 ? Values.v.windToRightBurst : Values.v.windToLeftBurst);
			player.SendMessage ("AddVelocityBurst", Values.wind_burstVelocityPerBurst * r);
		}
		wind1.GetComponent<ParticleSystem> ().Stop ();
		wind2.GetComponent<ParticleSystem> ().Stop ();
		yield return new WaitForSeconds (1f);
		active--;
		Destroy (wind1);
		Destroy (wind2);
		Destroy (gameObject);
	}

	IEnumerator WATER_RAIN(){
		float startTime = Time.time;
		while (Time.time - startTime < Values.water_rainDuration) {
				GameObject goutte = Instantiate(Values.v.Goutte) as GameObject;
				goutte.transform.position = new Vector2(Random.Range(Values.leftBulleSpawn, Values.rightBulleSpawn), Values.topBulleSpawn);
				goutte.SendMessage("SetSpeed", new Vector2(0f, -Values.playerSpeed*Values.water_rainPlayerSpeedMultipliedBy));
			yield return new WaitForSeconds(Random.Range(Values.water_rainMinTimeBetweenRound, Values.water_rainMaxTimeBetweenRound));
		}
		active--;
		Destroy (gameObject);
	}

	IEnumerator METEOR_RAIN(){
		if (Values.GetPointCounter ().GetPoints () < Values.meteor_rainMinPoints) {
			StopCoroutine ("METEOR_RAIN");
			StartCoroutine ("WATER_RAIN");
		} else {
			float startTime = Time.time;
			while (Time.time - startTime < Values.meteor_rainDuration) {
				GameObject goutte = Instantiate (Values.v.Meteor) as GameObject;
				int dir = Random.Range (0, 2) == 0 ? 1 : -1;
				goutte.transform.position = new Vector2 (dir == 1 ? -0.5f : 6.77f, 
			                                        Random.Range (5f, 10.3f));
				goutte.SendMessage ("SetSpeed", new Vector2 (Values.meteor_rainHorizontalSpeed * dir, -Values.playerSpeed * 2));
				goutte.transform.localRotation = Quaternion.Euler (0f, 0f, 45f * dir);
				yield return new WaitForSeconds (Random.Range (Values.meteor_rainMinTimeBetweenRound, Values.meteor_rainMaxTimeBetweenRound));
			}
			active--;
			Destroy (gameObject);
		}
	}

	IEnumerator CONTROL_INVERS(){
		Values.inversControl = true;
		yield return new WaitForSeconds (Values.inversControlDuration);
		Values.inversControl = false;
		active--;
		Destroy (gameObject);
	}

	void StopMeteorWaterAndWind(){
		StopCoroutine ("METEOR_RAIN");
		StopCoroutine ("WATER_RAIN");
		StopCoroutine ("WIND_BURST");
		if (gameObject.name.Equals ("METEOR_RAIN") || gameObject.name.Equals ("WATER_RAIN") || gameObject.name.Equals ("WIND_BURST")) {
			active--;
			Destroy(gameObject);
		}
	}

}
