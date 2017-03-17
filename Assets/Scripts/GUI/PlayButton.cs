using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour {

	void OnClick(){
		SceneManager.LoadScene ("bubbleMain");
	}

	void OnPreClick(){
		Instantiate (Values.v.mainMenuBubbles);
	}

	void OnCancelClick(){
	}
}
