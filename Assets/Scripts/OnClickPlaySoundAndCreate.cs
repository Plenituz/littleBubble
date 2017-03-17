using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class OnClickPlaySoundAndCreate : MonoBehaviour {
	public AudioClip clip;
	public AudioMixerGroup group;

	void Start () {
		AudioSource s = gameObject.AddComponent<AudioSource> ();
		s.outputAudioMixerGroup = group;
		s.clip = clip;
		s.playOnAwake = false;

		gameObject.AddComponent<OnClickPlaySound> ().source = s;
		Destroy (this);
	}
	

}
