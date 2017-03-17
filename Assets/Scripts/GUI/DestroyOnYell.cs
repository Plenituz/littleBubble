using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyOnYell : MonoBehaviour {

	public bool dontDestroyOnLoad;
	public string[] scenes;
	public bool isInverted;
	public bool switchCanvasCamera;
	public float delay = 0f;
	public int orderInLayer = 0;
	public bool orderInLayerBool = true;

	void Awake(){
		if (dontDestroyOnLoad)
			DontDestroyOnLoad (gameObject);
	}

	void SceneStarted(){
		ArrayList r = new ArrayList ();
		r.AddRange (scenes);
		if (switchCanvasCamera) {
			Canvas c = GetComponent<Canvas> ();
			c.worldCamera = Camera.main;
			if(orderInLayerBool)
				c.sortingOrder = orderInLayer;
		}
		if (isInverted) {
			if (!r.Contains (SceneManager.GetActiveScene ().name)) {
				Destroy (gameObject, delay);
			}
		} else {
			if (r.Contains (SceneManager.GetActiveScene ().name)) {
				Destroy (gameObject, delay);
			}
		}
	}
}
