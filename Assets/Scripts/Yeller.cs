using UnityEngine;
using System.Collections;
using System.Reflection;

public class Yeller : MonoBehaviour {

	void Start () {
		GameObject[] g = (GameObject[]) FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in g) {
			foreach (MonoBehaviour m in go.GetComponents<MonoBehaviour>()) {
				if (m.GetType ().GetMethod ("SceneStarted", BindingFlags.NonPublic | BindingFlags.Instance) != null)
					go.SendMessage ("SceneStarted");
			}

		}
		Destroy (gameObject);
	}

	void SceneStarted(){
	}

}
