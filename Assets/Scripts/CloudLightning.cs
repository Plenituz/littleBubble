using UnityEngine;
using System.Collections;

public class CloudLightning : MonoBehaviour {
	private float startTime;
	public GameObject child;
	private EdgeCollider2D childColl;
	private SpriteRenderer childSpr;
	private Animator animator;

	void Start(){
		childColl = child.GetComponent<EdgeCollider2D> ();
		childSpr = child.GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		StartCoroutine ("Light");
	}

	void SetState(CloudState state){
		animator.SetInteger ("State", (int)state);
	}

	IEnumerator Light(){
		while (true) {
			float timeBeforeFire = Values.cloudLightning_minTimeBetweenFire + Random.Range (0f, Values.cloudLightning_maxTimeBetweenFire - Values.cloudLightning_minTimeBetweenFire);
			yield return new WaitForSeconds(timeBeforeFire - Values.cloudLightning_chargingTime);
			SetState (CloudState.CHARGING);
			yield return new WaitForSeconds (Values.cloudLightning_chargingTime);
			SetState (CloudState.ELECT);
			childSpr.enabled = true;
			childColl.enabled = true;
			yield return new WaitForSeconds (Values.cloudLightning_timeOfFire);
			SetState (CloudState.BASE);
			childSpr.enabled = false;
			childColl.enabled = false;
		}
	}
}
