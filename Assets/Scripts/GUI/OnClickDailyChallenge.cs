using UnityEngine;
using System.Collections;

public class OnClickDailyChallenge : MonoBehaviour {
	public GameObject pauseMenu;
	public PauseManagement pauseManagement;

	void OnClick(){
		pauseMenu.SetActive (false);
		pauseManagement.paused = false;
		GameObject g = Values.ShowDailyChallenges ();
		g.AddComponent<PauseManagement> ();
	}
}
