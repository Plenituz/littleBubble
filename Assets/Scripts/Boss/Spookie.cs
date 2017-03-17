using UnityEngine;
using System.Collections;

public class Spookie : MonoBehaviour, Boss {
	public float difficulty;
	private Path path;

	/**
	 * int difficulty
	 */
	void Init(object[] args){
		difficulty = (int)args [0];
		StartCoroutine ("DoYourThing");
	}

	public float GetDuration (){
		return Values.spookie_durationPhase1 + Values.spookie_durationPhase2 + Values.spookie_durationPhase3 + Values.spookie_durationPhase3 + Values.spookie_durationPhase5;
	}

	IEnumerator DoYourThing(){
		yield return Phase1 ();
		difficulty += 0.5f;
		yield return Phase2 ();
		difficulty += 0.5f;
		yield return Phase3 ();
		yield return Phase4 ();
		yield return Phase5 ();
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
		Values.stats.bossKilled++;
	}

	IEnumerator Phase1(){
		//<phase 1>
		path = Phase1Path ();
		float duration = Values.spookie_durationPhase1;
		Anim a = new Anim (0f, 1.0f, duration, new AccelerateDeccelerateInterpolator (), OnAnimUpdate);
		a.Start ();
		yield return new WaitForSeconds (duration);
		SpawnConfiguredWave ();
		yield return new WaitForSeconds (Values.spookie_waitPhase1);
		SpawnConfiguredWave ();
		//</phase 1>
	}

	IEnumerator Phase2(){
		//<phase 2>
		path = Phase2Path();
		float duration = Values.spookie_durationPhase2;
		Anim a = new Anim (0f, 1.0f, duration, null, OnAnimUpdate);
		a.Start ();
		float startTime = Time.time;
		while (Time.time - startTime < Values.spookie_durationPhase2) {
			yield return new WaitForSeconds (Values.spookie_waitPhase2);
			SpawnConfiguredWave ();
		}
		//</phase 2>
	}

	IEnumerator Phase3(){
		//<phase 3>
		path = Phase3Path();
		float duration = Values.spookie_durationPhase3;
		Anim a = new Anim (0f, 1.0f, duration, null, OnAnimUpdate);
		a.Start ();
		float startTime = Time.time;
		while (Time.time - startTime < Values.spookie_durationPhase3) {
			yield return new WaitForSeconds (Mathf.Clamp(Values.spookie_waitPhase3 - (Values.spookie_waitPhase3Decrement * difficulty), 0.3f, 5f));
			Patterns.SpawnSpookieProjectile (transform.position, -90f * Mathf.Deg2Rad, 
				Mathf.Clamp (Values.spookie_phase3ProjectileMinSpeed + (difficulty * Values.spookie_phase3ProjectileIncrementSpeed), 
					Values.spookie_phase3ProjectileMinSpeed, Values.spookie_phase3ProjectileMaxSpeed));
		}
		//</phase 3>
	}

	IEnumerator Phase4(){
		//<phase 4>
		path = Phase4Path();
		float duration = Values.spookie_durationPhase3;
		Anim a = new Anim (0f, 1.0f, duration, null, OnAnimUpdate);
		a.Start ();
		float startTime = Time.time;
		while (Time.time - startTime < Values.spookie_durationPhase3) {
			yield return new WaitForSeconds (Mathf.Clamp(Values.spookie_waitPhase3/4 - (Values.spookie_waitPhase3Decrement * difficulty), 0.3f, 5f));
			Patterns.SpawnSpookieProjectile (transform.position, -90f * Mathf.Deg2Rad, 
				Mathf.Clamp (Values.spookie_phase3ProjectileMinSpeed + (difficulty * Values.spookie_phase3ProjectileIncrementSpeed), 
					Values.spookie_phase3ProjectileMinSpeed, Values.spookie_phase3ProjectileMaxSpeed));
			yield return new WaitForSeconds (Mathf.Clamp(Values.spookie_waitPhase3/4 - (Values.spookie_waitPhase3Decrement * difficulty), 0.3f, 5f));
			Patterns.SpawnSpookieProjectile (transform.position, -90f * Mathf.Deg2Rad, 
				Mathf.Clamp (Values.spookie_phase3ProjectileMinSpeed + (difficulty * Values.spookie_phase3ProjectileIncrementSpeed), 
					Values.spookie_phase3ProjectileMinSpeed, Values.spookie_phase3ProjectileMaxSpeed));
			yield return new WaitForSeconds (Mathf.Clamp(Values.spookie_waitPhase3/4 - (Values.spookie_waitPhase3Decrement * difficulty), 0.3f, 5f));
			SpawnConfiguredWave ();
		}
		//</phase 4>
	}

	IEnumerator Phase5(){
		//<phase 5>
		path = Phase5Path();
		float duration = Values.spookie_durationPhase5;
		Anim a = new Anim (0f, 1.0f, duration, null, OnAnimUpdate);
		a.Start ();
		yield return new WaitForSeconds (Values.spookie_durationPhase5);
		//</phase 5>
	}

	Path Phase1Path(){
		Path path = new Path (new Vector2 (getWidth () * 1.14714693079055f, getHeight () * 0.62069820246106f));
		path.CubicTo (new Vector2 (getWidth () * 1.28063587925188f, getHeight () * 0.42456380425773f), 
			new Vector2 (getWidth () * 1.13445270122094f, getHeight () * 0.19793160060859f), 
			new Vector2 (getWidth () * 0.82063766409562f, getHeight () * 0.11450100782027f));
		path.CubicTo (new Vector2 (getWidth () * 0.56958563439537f, getHeight () * 0.0477565335896f), 
			new Vector2 (getWidth () * 0.27949641372447f, getHeight () * 0.12084812260507f), 
			new Vector2 (getWidth () * 0.17270525495541f, getHeight () * 0.27775564116773f));
		path.CubicTo (new Vector2 (getWidth () * 0.08727232794017f, getHeight () * 0.40328165601786f), 
			new Vector2 (getWidth () * 0.18082956187996f, getHeight () * 0.54832626635331f), 
			new Vector2 (getWidth () * 0.38167118564017f, getHeight () * 0.60172184573784f));
		path.CubicTo (new Vector2 (getWidth () * 0.54234448464833f, getHeight () * 0.64443830924546f), 
			new Vector2 (getWidth () * 0.72800158587771f, getHeight () * 0.59765969227556f), 
			new Vector2 (getWidth () * 0.7963479274899f, getHeight () * 0.49723888039546f));
		path.CubicTo (new Vector2 (getWidth () * 0.7963479274899f, getHeight () * 0.49723888039546f), 
			new Vector2 (getWidth () * 0.93343728646901f, getHeight () * 0.24718694111223f), 
			new Vector2 (getWidth () * 0.52304113960969f, getHeight () * 0.43207353011611f));
		path.CubicTo (new Vector2 (getWidth () * 0.11264499275036f, getHeight () * 0.61696011912f), 
			new Vector2 (getWidth () * 0.36471572254721f, getHeight () * 0.74195386943248f), 
			new Vector2 (getWidth () * 0.4230461393597f, getHeight () * 0.73674579650279f));
		path.CubicTo (new Vector2 (getWidth () * 0.48137655617219f, getHeight () * 0.73153772357311f), 
			new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f));
		path.Seal ();
		return path;
	}

	Path Phase2Path(){
		Path path = new Path (new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f));
		path.CubicTo (new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.07931332600037f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.07931332600037f, getHeight () * 0.68987314013561f));
		path.CubicTo (new Vector2 (getWidth () * 0.07931332600037f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.95010311984401f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.95010311984401f, getHeight () * 0.68987314013561f));
		path.CubicTo (new Vector2 (getWidth () * 0.95010311984401f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.07931332600037f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.07931332600037f, getHeight () * 0.68987314013561f));
		path.CubicTo (new Vector2 (getWidth () * 0.07931332600037f, getHeight () * 0.68987314013561f), 
			new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f),
			new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f));
		path.Seal ();
		return path;
	}

	Path Phase3Path(){
		return Phase2Path ();
	}
	Path Phase4Path(){
		return Phase2Path ();
	}

	Path Phase5Path(){
		Path path = new Path (new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f));
		path.CubicTo (new Vector2 (getWidth () * 0.5f, getHeight () * 0.68987314013561f),
			new Vector2 (getWidth () * 0.5f, getHeight () * 1.5f), 
			new Vector2 (getWidth () * 0.5f, getHeight () * 1.5f));
		path.Seal ();
		return path;
	}

	void SpawnConfiguredWave(){
		SpawnWave (transform.position, Mathf.Clamp (Values.spookie_baseStep - (difficulty * Values.spookie_stepDecrement), Values.spookie_minStep, 360f),
			Mathf.Clamp (Values.spookie_baseSweep + difficulty * Values.spookie_sweepIncrement, Values.spookie_baseSweep, Values.spookie_maxSweep),
			Random.Range (0f, Values.spookie_maxRandomSweep),
			Mathf.Clamp (1f + (difficulty * 2f), Values.spookie_minSpeed, Values.spookie_maxSpeed));
	}


	void OnAnimUpdate(float value){
		Vector2 n = path.GetPointAtPercent (value) + new Vector2 (0f, 1f);
		//Debug.DrawLine(transform.position, n, Color.black, 5f);
		transform.position = n;
	}

	float getWidth(){
		return 6.25f;
	}

	float getHeight(){
		return 10.5f;
	}

	float getInterpolation(float input){
		//return (float)(Mathf.Cos((input + 1) * Mathf.PI) / 2.0f) + 0.5f;
        float result;
		float mFactor = 1.0f;
        if (mFactor == 1.0f) {
            result = (float)(1.0f - (1.0f - input) * (1.0f - input));
        } else {
            result = (float)(1.0f - Mathf.Pow((1.0f - input), 2 * mFactor));
        }
        return result;
	}

	public static void SpawnWave(Vector2 pos, float step, float sweep, float randomSweep, float speed){
		sweep += Random.Range (-randomSweep, randomSweep);
		for(float angle = 270f - sweep/2;angle <= 270f + sweep/2; angle += step){
			Patterns.SpawnSpookieProjectile (pos, angle * Mathf.Deg2Rad, speed);
		}
	}


}
