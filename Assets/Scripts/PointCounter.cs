using UnityEngine;
using System.Collections;

public class PointCounter : MonoBehaviour {
	public int points = 0;
	public bool paused = false;

	public static PointCounter instance;

	void Awake(){
		instance = this;
	}

	public void AddPoint(int p){
		if (paused)
			return;
		points += p;
	}

	public void RemovePoint(int p){
		if (paused)
			return;
		points -= p;
	}

	public int GetPoints(){
		return points;
	}

	public void Pause(){
		paused = true;
	}

	public void UnPause(){
		paused = false;
	}
}
