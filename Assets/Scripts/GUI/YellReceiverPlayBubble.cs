using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class YellReceiverPlayBubble : MonoBehaviour {

	void SceneStarted(){
		if (SceneManager.GetActiveScene ().name.Equals ("bubbleMain")) {
			transform.position = new Vector3 (3f, 8f, 0);
		}
	}
}
