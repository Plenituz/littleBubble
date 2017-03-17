using UnityEngine;
using System.Collections;

public class SpeedIncrementer : MonoBehaviour {
	float startTime;
	float supposedPlayerSpeed;

	void Start () {
		startTime = Time.time * Time.timeScale;
		Values.playerSpeed = Values.playerStartSpeed;
		supposedPlayerSpeed = Values.playerSpeed;
	}
	
	void Update () {
		if (Time.time * Time.timeScale - startTime >= Values.playerSpeedTimeBetweenIncrement) {
			startTime = Time.time * Time.timeScale;
			Values.playerSpeed += Values.playerSpeedIncrement;
		}
		if (supposedPlayerSpeed >= Values.playerMaxSpeed) {
			Values.playerSpeed = (Values.playerSpeed - Values.playerMaxSpeed) + Values.playerMaxSpeed;
			Destroy(gameObject);
		}
	}
}
