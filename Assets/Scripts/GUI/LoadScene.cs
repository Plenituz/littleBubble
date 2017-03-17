using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour {
	public string sceneToLoad;

	void OnClick(){
		SceneManager.LoadScene (sceneToLoad);
		Destroy (this);
	}

	void OnPreClick(){
	}
	void OnCancelClick(){
	}
}
