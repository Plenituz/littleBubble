using UnityEngine;
using System.Collections;

public class BackgroundPlacer : MonoBehaviour {

	public Sprite bg3;
	public Sprite bg4;
	public Sprite bg5;
	public Sprite bg6;
	public Sprite bg7;
	public Sprite bg8;
	public Sprite bg9;
	private int at = 0;
	private Sprite[] sprites;

	public delegate void NextSwitchDelegate();
	public static NextSwitchDelegate onNextSwitch;

	void FixedUpdate () {//bg images have to be the same size each and bigger than the screen height
		if (transform.position.y <= -GetComponentInChildren<SpriteRenderer> ().bounds.size.y) {
			SpriteRenderer[] sps = GetComponentsInChildren<SpriteRenderer> ();
			Sprite tmp = sps [0].sprite;
			sps [0].sprite = sps [1].sprite;
			sps [1].sprite = tmp;
			transform.position += new Vector3(0, GetComponentInChildren<SpriteRenderer> ().bounds.size.y, 0);
			if(onNextSwitch != null)
				onNextSwitch ();
		}
	}

	void Start(){
		sprites = new Sprite[]{bg3, bg4, bg5, bg6, bg7, bg8, bg9 };
		onNextSwitch += FirstChange;
	}

	void FirstChange(){
		SpriteRenderer[] sps = GetComponentsInChildren<SpriteRenderer> ();
		sps [1].sprite = sprites [at++];
		if (at == sprites.Length) {
			onNextSwitch -= FirstChange;
			onNextSwitch += LastSwitch;
		}
	}

	void LastSwitch(){
		SpriteRenderer[] sps = GetComponentsInChildren<SpriteRenderer> ();
		sps [1].sprite = sprites [at-1];
		onNextSwitch = null;
	}
}
