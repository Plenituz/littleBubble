using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {
	private RectTransform rt;
	private bool downIn = false;
	private Color originalColor;
	private CanvasRenderer canvasRenderer;
	private float ease = 0.2f;

	void Start () {
		canvasRenderer = GetComponent<CanvasRenderer> ();
		originalColor = canvasRenderer.GetColor ();
		rt = GetComponent<RectTransform> ();
	}
	
	void Update () {
		
		if ((Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)) {
			float mousePosX = Input.mousePosition.x / Screen.width;
			float mousePosY = Input.mousePosition.y / Screen.height;

			if (Input.GetMouseButtonDown (0)) {
				if (mousePosX > rt.anchorMin.x && mousePosX < rt.anchorMax.x && mousePosY > rt.anchorMin.y && mousePosY < rt.anchorMax.y) {
					downIn = true;
					SendMessage ("OnPreClick");
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				if (downIn && mousePosX > rt.anchorMin.x && mousePosX < rt.anchorMax.x && mousePosY > rt.anchorMin.y && mousePosY < rt.anchorMax.y) 
					SendMessage ("OnClick");
				else 
					SendMessage ("OnCancelClick");
				downIn = false;
			}
		}
		if (Input.touchCount > 0) {
			Touch t = Input.GetTouch (0);
			float mousePosX = t.position.x / Screen.width;
			float mousePosY = t.position.y / Screen.height;
			print (t.phase);
			switch (t.phase) {
			case TouchPhase.Began:
				if (mousePosX > rt.anchorMin.x && mousePosX < rt.anchorMax.x && mousePosY > rt.anchorMin.y && mousePosY < rt.anchorMax.y) {
					downIn = true;
					SendMessage ("OnPreClick");
				}
				break;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				if (downIn && mousePosX > rt.anchorMin.x && mousePosX < rt.anchorMax.x && mousePosY > rt.anchorMin.y && mousePosY < rt.anchorMax.y) {
					SendMessage ("OnClick");
				} else {
					SendMessage ("OnCancelClick");
				}
				downIn = false;
				break;
			}

		}
	}

	IEnumerator LerpColor(object[] args){
		Color from = (Color)args [0];
		Color to = (Color)args [1];
		float t = (float)args [2];

		canvasRenderer.SetColor (from);
		while (!canvasRenderer.GetColor ().Equals (to)) {
			canvasRenderer.SetColor (Color.Lerp(canvasRenderer.GetColor (), to, t));
			yield return new WaitForEndOfFrame ();
		}
	}

	void OnPreClick(){
		StopCoroutine ("LerpColor");
		StartCoroutine ("LerpColor", new object[]{canvasRenderer.GetColor (), new Color (originalColor.r - 0.2f, originalColor.g - 0.2f, originalColor.b - 0.2f, originalColor.a), ease});
	}

	void OnCancelClick(){
		StopCoroutine ("LerpColor");
		StartCoroutine ("LerpColor", new object[]{canvasRenderer.GetColor (), originalColor, ease});
	}

	void OnClick(){
		StopCoroutine ("LerpColor");
		StartCoroutine ("LerpColor", new object[]{canvasRenderer.GetColor (), originalColor, ease});
	}
}
