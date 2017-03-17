using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class MoveUp : MonoBehaviour {
	public float duration = 2f;
	public float startDelay;
	public float offsetY = 10f;
	public bool invert = false;
	public bool deleteAfter = false;

	private RectTransform rt;
	private float initialY;
	private float startTime;

	void Start () {
		DontDestroyOnLoad (transform.parent.gameObject);
		rt = GetComponent<RectTransform> ();
		initialY = rt.position.y;
		if(!invert)
			rt.position = new Vector3 (rt.position.x, rt.position.y - offsetY, rt.position.z);
		StartCoroutine ("anim");
	}

	IEnumerator anim(){
		try{
			GetComponent<ButtonHandler>().enabled = false;
		}catch(Exception){
		}
		float from = initialY - offsetY;
		if(deleteAfter)
			from = initialY - (Screen.height*offsetY);
		float to = initialY;
		if (invert) {
			float tmp = from;
			from = to;
			to = tmp;
		}
		yield return new WaitForSeconds (startDelay);
		startTime = Time.time;
		while (Time.time - startTime <= duration) {
			float value = (from + (to - from)*getInterpolation((Time.time - startTime)/duration));
			rt.position = new Vector3 (rt.position.x, value, rt.position.z);
			yield return new WaitForEndOfFrame();
		}
		try{
			GetComponent<ButtonHandler>().enabled = true;
		}catch(Exception){
		}
		if (deleteAfter)
			Destroy (gameObject);
	}

	float getInterpolation(float input){
		return (float)(Mathf.Cos((input + 1) * Mathf.PI) / 2.0f) + 0.5f;
	}

	void SceneStarted(){
		if (!SceneManager.GetActiveScene ().name.Equals ("MainMenu")) {
			offsetY = 1f;
			enabled = true;
		}
	}

}
