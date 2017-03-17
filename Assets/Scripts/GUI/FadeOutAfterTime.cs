using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOutAfterTime : MonoBehaviour {
	public float time;
	public float paddingXInScreenPercent;
	public float paddingYInScreenPercent;

	private float fadeOutTime = 0.5f;
	private Image i;
	private Text t;

	void Awake(){
		i = GetComponentInChildren<Image> ();
		t = GetComponentInChildren<Text> ();
	}

	void Start () {
		Destroy (gameObject, time + fadeOutTime);
		StartCoroutine ("WaitAndFade");
		FadeOutAfterTime[] instances = FindObjectsOfType<FadeOutAfterTime> ();
		for (int i = 1; i < instances.Length; i++) {
			instances [i].GetComponentInChildren<Image> ().transform.position += new Vector3 (0f, (Screen.height*0.025f) + LayoutUtility.GetPreferredHeight(instances [i - 1].GetComponentInChildren<Image> ().rectTransform));
			instances [i].GetComponentInChildren<Text> ().transform.position += new Vector3 (0f, (Screen.height *0.025f) + LayoutUtility.GetPreferredHeight(instances [i - 1].GetComponentInChildren<Image> ().rectTransform));
		}
	}
	
	IEnumerator WaitAndFade(){
		yield return new WaitForSeconds (time);
		while (true) {
			i.color = new Color (i.color.r, i.color.g, i.color.b, i.color.a - 0.05f);
			t.color = new Color (t.color.r, t.color.g, t.color.b, t.color.a - 0.05f);
			yield return null;
		}
	}

	public void SetTime(float time){
		this.time = time;
	}

	public void SetText(string text){
		t.text = text;
		UpdateSize ();
	}

	public void UpdateSize(){
		float width = LayoutUtility.GetPreferredWidth (t.rectTransform);
		float height = LayoutUtility.GetPreferredHeight (t.rectTransform);

		i.rectTransform.sizeDelta = new Vector2 (width + (paddingXInScreenPercent * Screen.width), height + (paddingYInScreenPercent * Screen.height));
	}
}
