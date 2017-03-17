using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NoRespawnClick : MonoBehaviour {

	void OnClick(){
		GameController g = (GameController)FindObjectOfType(typeof(GameController));
		g.SendMessage ("PlayerDied");
	}
}
