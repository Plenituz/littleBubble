using UnityEngine;
using System.Collections;

public class Kroug : MonoBehaviour, Boss {
	public float difficulty;

	public float GetDuration (){
		return 15f + 15f + 15f;
	}

	/**
	 * int difficulty
	 */
	void Init(object[] args){
		difficulty = (int)args [0];
		StartCoroutine ("DoYourThing");
	}

	IEnumerator DoYourThing(){
		
		float kroug_minLeft = 205f * Mathf.Deg2Rad;
		float kroug_maxLeft = 245f * Mathf.Deg2Rad;
		float kroug_minRight = -65f * Mathf.Deg2Rad;
		float kroug_maxRight = -25f * Mathf.Deg2Rad;

		float kroug_minSpeed_vert = 3.2f;
		float kroug_maxSpeed_vert = 6f;

		float kroug_waitPhase1 = 15f;
		float kroug_waitPhase2 = 15f;
		float kroug_waitPhase3 = 15f;


		bool left = Random.Range (0, 2) == 1;

		GameObject g = Patterns.SpawnKrougObject (new Vector2 (3.19f, 12f), left ? 
			(Random.Range (kroug_minLeft, kroug_maxLeft)) : (Random.Range (kroug_minRight, kroug_maxRight)), 
			Random.Range (kroug_minSpeed_vert, kroug_maxSpeed_vert));

		yield return new WaitForSeconds (kroug_waitPhase1);
		g.SendMessage ("LetDie");

		left = Random.Range (0, 2) == 1;
		g = Patterns.SpawnKrougObject (new Vector2 (3.19f, 12f), left ? 
			(Random.Range (kroug_minLeft, kroug_maxLeft)) : (Random.Range (kroug_minRight, kroug_maxRight)), 
			Random.Range (kroug_minSpeed_vert, kroug_maxSpeed_vert));

		left = Random.Range (0, 2) == 1;
		GameObject g1 = Patterns.SpawnKrougObject (new Vector2 (3.19f, 12f), left ? 
			(Random.Range (kroug_minLeft, kroug_maxLeft)) : (Random.Range (kroug_minRight, kroug_maxRight)), 
			Random.Range (kroug_minSpeed_vert, kroug_maxSpeed_vert));

		yield return new WaitForSeconds (kroug_waitPhase2);
		g.SendMessage ("LetDie");
		g1.SendMessage ("LetDie");

		left = Random.Range (0, 2) == 1;
		g = Patterns.SpawnKrougObject (new Vector2 (3.19f, 12f), left ? 
			(Random.Range (kroug_minLeft, kroug_maxLeft)) : (Random.Range (kroug_minRight, kroug_maxRight)), 
			Random.Range (kroug_minSpeed_vert, kroug_maxSpeed_vert));

		yield return new WaitForSeconds (2f);

		left = Random.Range (0, 2) == 1;
		g1 = Patterns.SpawnKrougObject (new Vector2 (3.19f, 12f), left ? 
			(Random.Range (kroug_minLeft, kroug_maxLeft)) : (Random.Range (kroug_minRight, kroug_maxRight)), 
			Random.Range (kroug_minSpeed_vert, kroug_maxSpeed_vert));

		yield return new WaitForSeconds (2f);

		left = Random.Range (0, 2) == 1;
		GameObject g2 = Patterns.SpawnKrougObject (new Vector2 (3.19f, 12f), left ? 
			(Random.Range (kroug_minLeft, kroug_maxLeft)) : (Random.Range (kroug_minRight, kroug_maxRight)), 
			Random.Range (kroug_minSpeed_vert, kroug_maxSpeed_vert));

		yield return new WaitForSeconds (kroug_waitPhase3);
		g.SendMessage ("LetDie");
		g1.SendMessage ("LetDie");
		g2.SendMessage ("LetDie");
	}

}
