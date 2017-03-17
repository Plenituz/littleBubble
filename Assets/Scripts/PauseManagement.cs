using UnityEngine;
using System.Collections;

public class PauseManagement : MonoBehaviour {
	public bool paused = false;
	public GameObject pauseMenu;

	void OnClick(){
		if (paused) {
			paused = false;
			Time.timeScale = 1f;
			pauseMenu.SetActive (false);
		} else {
			paused = true;
			Time.timeScale = 0f;
			pauseMenu.SetActive (true);
		}
	}

	void Close(){
		Time.timeScale = 1f;
	}
}
