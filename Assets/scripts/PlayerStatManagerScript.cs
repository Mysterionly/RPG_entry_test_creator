using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


[System.Serializable]
public class PlayerStats{
	public int ritorica;
	public int cunning;
	public int cruelty;
	public int attentiveness;
	public int survivialism;
	public int trading;
}

[System.Serializable]
public class Player{
	public string name;
	public Genders gen;
	public Ages age;
	public PlayerStats statPack;
	public ImageF playerImage;
	public int statusID;
}

public class PlayerStatManagerScript : MonoBehaviour {

	public Player newPlayer;

	public void SaveQuestions() {
		XmlSerializer serializer = new XmlSerializer(typeof(Player));
		FileStream stream = new FileStream("Assets/storages/playerStats.xml", FileMode.Create);
		serializer.Serialize(stream, newPlayer);
		stream.Close();
	}

	public void LoadQuestions() {
		XmlSerializer serializer = new XmlSerializer(typeof(Test));
		FileStream stream = new FileStream("Assets/storages/playerStats.xml", FileMode.Open);
		newPlayer = serializer.Deserialize(stream) as Player;
		stream.Close();
	}
}
