using UnityEngine;
using System.Collections;

public class ArrowCustomiser : MonoBehaviour {
	public MonoBehaviour master;
	public string message;

	void OnClick(){
		master.SendMessage (message);
	}
}
