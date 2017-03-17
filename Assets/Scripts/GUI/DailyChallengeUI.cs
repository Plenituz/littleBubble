using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DailyChallengeUI : MonoBehaviour {
	public Text[] challengeTexts = new Text[3];
	public Image[] crossValids = new Image[3];
	public Slider[] sliders = new Slider[3];
	public GameObject[] wholeChallenges = new GameObject[3];
	public GameObject kdo;
	public Sprite valid;
	public Sprite cross;
	public RectTransform BG;
	public CanvasRenderer dim;

	public Image img1;
	public Image img2;
	public Text rewardText;

	public Sprite moneyBulle;
	public Sprite shield;
	public Sprite clairevoyance;

	void Awake () {

		UpdateUI ();

		if (FindObjectOfType<EventSystem> () == null) {
			(Instantiate (Values.v.EventSystem) as GameObject).transform.SetParent (gameObject.transform);
		}
	}

	void Start(){
		StartCoroutine (AnimFromTo (0f, 1f, 0.8f, "bounce", (float value) => {
			BG.localScale = new Vector3(value, value, value);
			dim.SetAlpha (value);
		}));
	}

	void UpdateUI(){
		challengeTexts [0].text = DailyChallenges.ch_1.GetDesc ();
		challengeTexts [1].text = DailyChallenges.ch_2.GetDesc ();
		challengeTexts [2].text = DailyChallenges.ch_3.GetDesc ();

		//---------------------------------Appearance
		if (DailyChallenges.ch_1.progress > 0) {
			crossValids [0].enabled = false;
			sliders [0].gameObject.SetActive (true);
			sliders [0].maxValue = DailyChallenges.ch_1.maxProgress;
			sliders [0].value = DailyChallenges.ch_1.progress;
		} else {
			sliders [0].gameObject.SetActive (false);
			crossValids [0].enabled = true;
		}

		if (DailyChallenges.ch_2.progress > 0) {
			crossValids [1].enabled = false;
			sliders [1].gameObject.SetActive (true);
			sliders [1].maxValue = DailyChallenges.ch_2.maxProgress;
			sliders [1].value = DailyChallenges.ch_2.progress;
		} else {
			sliders [1].gameObject.SetActive (false);
			crossValids [1].enabled = true;
		}

		if (DailyChallenges.ch_3.progress > 0) {
			crossValids [2].enabled = false;
			sliders [2].gameObject.SetActive (true);
			sliders [2].maxValue = DailyChallenges.ch_3.maxProgress;
			sliders [2].value = DailyChallenges.ch_3.progress;
		} else {
			sliders [2].gameObject.SetActive (false);
			crossValids [2].enabled = true;
		}
		//-----------------------------------end appearance

		//-----------------------------------valids
		if (DailyChallenges.ch_1.completed) {
			crossValids [0].enabled = true;
			crossValids [0].sprite = valid;
			crossValids [0].color = Color.green;
			sliders [0].gameObject.SetActive (false);
		} else {
			crossValids [0].sprite = cross;
			crossValids [0].color = Color.white;
		}

		if (DailyChallenges.ch_2.completed) {
			crossValids [1].enabled = true;
			crossValids [1].sprite = valid;
			crossValids [1].color = Color.green;
			sliders [1].gameObject.SetActive (false);
		} else {
			crossValids [1].sprite = cross;
			crossValids [1].color = Color.white;
		}

		if (DailyChallenges.ch_3.completed) {
			crossValids [2].enabled = true;
			crossValids [2].sprite = valid;
			crossValids [2].color = Color.green;
			sliders [2].gameObject.SetActive (false);
		} else {
			crossValids [2].sprite = cross;
			crossValids [2].color = Color.white;
		}
		//-----------------------------------end valids
		if (DailyChallenges.ch_1.justCompleted) {
			StartCoroutine ("JustCompleted", 0);
		}
		if (DailyChallenges.ch_2.justCompleted) {
			StartCoroutine ("JustCompleted", 1);
		}
		if (DailyChallenges.ch_3.justCompleted) {
			StartCoroutine ("JustCompleted", 2);
		}

		if (DailyChallenges.ch_1.completed && DailyChallenges.ch_2.completed && DailyChallenges.ch_3.completed && DailyChallenges.ch_1.rewarded == -1) {
			int rand = -1;
			float c = Random.Range (0f, 100f);
			float cumulated = 0;
			for (int i = 0; i < DailyChallenges.rewardPercents.Length; i++) {
				if (c >= cumulated && c < cumulated + DailyChallenges.rewardPercents [i]) {
					rand = i;
					break;
				}
				cumulated += DailyChallenges.rewardPercents [i];
			}

			DailyChallenges.ch_1.rewarded = rand;
			DailyChallenges.ch_2.rewarded = rand;
			DailyChallenges.ch_3.rewarded = rand;
			DailyChallenges.ch_1.justRewarded = true;
			DailyChallenges.ch_2.justRewarded = true;
			DailyChallenges.ch_3.justRewarded = true;

			Values.SaveDailyChallenge ();
			DailyChallenges.rewards [rand] ();
		}

		if (DailyChallenges.ch_1.rewarded != -1) {
			wholeChallenges [0].SetActive (false);
			wholeChallenges [1].SetActive (false);
			wholeChallenges [2].SetActive (false);
			kdo.SetActive (true);
			switch (DailyChallenges.ch_1.rewarded) {
			case 1:
				img1.sprite = moneyBulle;
				rewardText.text = "100 Bulles !";
				break;
			case 2:
				img1.sprite = moneyBulle;
				rewardText.text = "200 Bulles !";
				break;
			case 3:
				img1.sprite = moneyBulle;
				rewardText.text = "400 Bulles !";
				break;
			case 4:
				img1.sprite = moneyBulle;
				rewardText.text = "1000 Bulles !";
				break;
			case 5:
				img1.sprite = moneyBulle;
				rewardText.text = "2000 Bulles !";
				break;
			case 6:
				img1.sprite = shield;
				rewardText.text = "5 shields !";
				break;
			case 7:
				img1.sprite = shield;
				img1.rectTransform.localPosition = new Vector3 (-175f, img1.rectTransform.localPosition.y, img1.rectTransform.localPosition.z);
				img2.enabled = true;
				img2.sprite = clairevoyance;
				rewardText.text = "5 shields + 5 clairvoyance !";
				break;
			case 8:
				img1.sprite = moneyBulle;
				rewardText.text = "10 000 Bulles !";
				break;
			case 9:
				img1.sprite = moneyBulle;
				rewardText.text = "50 000 Bulles !";
				break;
			case 10:
				img1.sprite = Values.v.colorSprites [0];
				rewardText.text = "A new Costume !";
				break;
			case 11:
				img1.sprite = Values.v.colorSprites [0];
				rewardText.text = "A new Color !";
				break;
			case 12:
				img1.sprite = Values.v.colorSprites [9];
				rewardText.text = "Exclusive Silver color !";
				break;
			}
			MoneyDisplay m = FindObjectOfType<MoneyDisplay> ();
			if (m != null)
				m.UpdateText ();
		}
	}

	IEnumerator JustCompleted(int channel){
		if (crossValids [channel].enabled) {
			StartCoroutine (AnimFromTo (1f, 0f, 0.5f, "ease", (float value) => crossValids [channel].rectTransform.localScale = new Vector3 (value, value, value)));
			sliders [channel].value = 0f;
			sliders [channel].gameObject.SetActive (true);
			StartCoroutine (AnimFromTo (0f, 1f, 0.6f, "ease", (float value) => sliders [channel].value = value));
			yield return new WaitForSeconds (0.6f);
		} else {
			crossValids [channel].enabled = true;
		}
		StartCoroutine (AnimFromTo (1f, 0f, 0.4f, "ease", (float value) => sliders [channel].GetComponent<RectTransform> ().localScale = new Vector3 (value, value, value)));
		crossValids [channel].sprite = valid;
		crossValids [channel].color = Color.green;
		StartCoroutine (AnimFromTo (0f, 1f, 0.6f, "ease", (float value) => crossValids [channel].rectTransform.localScale = new Vector3 (value, value, value)));
		switch (channel) {
		case 0:
			DailyChallenges.ch_1.justCompleted = false;
			DailyChallenges.ch_1.completed = true;
			break;
		case 1:
			DailyChallenges.ch_2.justCompleted = false;
			DailyChallenges.ch_2.completed = true;
			break;
		case 2:
			DailyChallenges.ch_3.justCompleted = false;
			DailyChallenges.ch_3.completed = true;
			break;
		}
		Values.SaveDailyChallenge ();
		yield return new WaitForSeconds (0.3f);
		UpdateUI ();
		//explosion !
	}

	public void Close(){
		StartCoroutine (AnimFromTo (1f, 0f, 0.4f, "ease", (float value) => {
			BG.localScale = new Vector3(value, value, value);
			dim.SetAlpha (value);
		}));
		Destroy (gameObject, 0.4f);
	}

	public delegate void OnAnimationUpdateListener(float value);

	IEnumerator AnimFromTo(float from, float to, float duration, string interpolator, OnAnimationUpdateListener l){
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - startTime <= duration) {
			float value = (from + (to - from)*getInterpolation((Time.realtimeSinceStartup - startTime)/duration, interpolator));
			l (value);
			yield return StartCoroutine (WaitForRealSeconds (0.01f));
		}
		l (to);
	}

	IEnumerator WaitForRealSeconds(float time){
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time) {
			yield return null;
		}
	}

	float getInterpolation(float input, string interpolation){
		switch (interpolation) {
		case "ease":
			return (float)(Mathf.Cos((input + 1) * Mathf.PI) / 2.0f) + 0.5f;
		case "bounce":
        // _b(t) = t * t * 8
        // bs(t) = _b(t) for t < 0.3535
        // bs(t) = _b(t - 0.54719) + 0.7 for t < 0.7408
        // bs(t) = _b(t - 0.8526) + 0.9 for t < 0.9644
        // bs(t) = _b(t - 1.0435) + 0.95 for t <= 1.0
        // b(t) = bs(t * 1.1226)
        	input *= 1.1226f;
			if (input < 0.3535f) return bounce(input);
			else if (input < 0.7408f) return bounce(input - 0.54719f) + 0.7f;
			else if (input < 0.9644f) return bounce(input - 0.8526f) + 0.9f;
			else return bounce(input - 1.0435f) + 0.95f;
		default:
			return input;
		}
	}
	
	private static float bounce(float t) {
        return t * t * 8.0f;
    }
}
