using UnityEngine;
using System;
using System.Collections;

public class FadeOutSprite : MonoBehaviour {
	
	void Update () {
		int count = 0;
		SpriteRenderer cc = GetComponent<SpriteRenderer> ();
		cc.color = new Color (cc.color.r, cc.color.g, cc.color.b, cc.color.a - 0.05f);
		for (int i = 0; i < transform.childCount; i++) {
			try{
				SpriteRenderer c = transform.GetChild (i).GetComponent<SpriteRenderer> ();
				c.color = new Color (c.color.r, c.color.g, c.color.b, c.color.a - 0.05f);
				if (c.color.a <= 0)
					count++;
			}catch(Exception){
			}
		}
		if (count >= transform.childCount)
			Destroy (this);
	}
}
