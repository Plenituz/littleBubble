using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOutCanvas : MonoBehaviour {
	
	void Update () {
		if (GetComponent<Slider> () != null)
			Destroy (gameObject);

		int count = 0;
		for (int i = 0; i < transform.childCount; i++) {
			CanvasRenderer c = transform.GetChild (i).GetComponent<CanvasRenderer> ();
			c.SetAlpha (c.GetAlpha () - 0.05f);
			if (c.transform.childCount != 0 && c.GetComponent<FadeOutCanvas> () == null) {
				c.gameObject.AddComponent<FadeOutCanvas> ();
			}
			if (c.GetAlpha () <= 0f)
				count++;
		}
		if (count >= transform.childCount)
			Destroy (this);
	}
}
