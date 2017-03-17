using UnityEngine;
using System.Collections;

public class RateOnClick : MonoBehaviour {
	public string URI;

	void OnClick(){
		Application.OpenURL (URI);
	}
}
