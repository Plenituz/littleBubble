using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

public class MoneyCounter : MonoBehaviour {
	public int money = 0;

	public static MoneyCounter instance;

	void Awake(){
		instance = this;
		LoadMoney ();
	}

	public void AddMoney(int m){
		money += m;
	}

	public void RemoveMoney(int m){
		money -= m;
	}

	public int GetMoney(){
		return money;
	}

	public void LoadMoney(){
		if (File.Exists (Application.persistentDataPath + Values.MONEY_PATH)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + Values.MONEY_PATH, FileMode.Open);
			money = (int) bf.Deserialize (file);
			file.Close ();
		} else {
			money = 0;
		}
	}

	public void SaveMoney(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + Values.MONEY_PATH);
		bf.Serialize (file, money);
		file.Close ();
	}

}
