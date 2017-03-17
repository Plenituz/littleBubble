using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class OnClickTest : MonoBehaviour {
	private bool g = false;

	void OnClick(){
		if (g)
			GetComponent<Image> ().color = Color.red;
		else
			GetComponent<Image> ().color = Color.blue;
		g = !g;
	}

	void OnPreClick(){
		GetComponent<Image> ().color = Color.grey;
	}

	void OnCancelClick(){
		if (!g)
			GetComponent<Image> ().color = Color.red;
		else
			GetComponent<Image> ().color = Color.blue;
	}
}
