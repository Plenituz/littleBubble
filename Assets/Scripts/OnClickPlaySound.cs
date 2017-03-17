using UnityEngine;
using System.Collections;

public class OnClickPlaySound : MonoBehaviour {
	public AudioSource source;

	void OnClick(){
		source.Play ();
	}
}
