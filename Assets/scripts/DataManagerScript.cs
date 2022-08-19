using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class DataManagerScript : MonoBehaviour {

	public static DataManagerScript ins;
	public PersonDB persons;

	void Awake() {
		ins = this;
	}

	public void SavePersons() {
		XmlSerializer serializer = new XmlSerializer(typeof(PersonDB));
		FileStream stream = new FileStream("Assets/storages/personaGenerated.xml", FileMode.Create);
		serializer.Serialize(stream, persons);
		stream.Close();
	}

	public void LoadPersons() {
		XmlSerializer serializer = new XmlSerializer(typeof(PersonDB));
		FileStream stream = new FileStream("Assets/storages/personaGenerated.xml", FileMode.Open);
		persons = serializer.Deserialize(stream) as PersonDB;
		stream.Close();
	}

	public void AddNew(){
		persons.personList.Add (new Person());
		persons.personList [persons.personList.Count - 1].name = persons.personList.Count.ToString();
	}
}

[System.Serializable]
public class ImageF {
	public int bodyID;
	public int hairID;
	public int faceID;
	public int browsID;
	public int eyesID;
	public int noseID;
	public int mouthID;
}

[System.Serializable]
public class Person {
	public string name;
	public Ages age;
	public Genders gen;
	public ImageF img;
	public int statusID;
	public int positionID;
	public float relation;
}

[System.Serializable]
public class PersonDB {
	public List<Person> personList = new List<Person>();
}

public enum Ages {
	child,
	young, 
	mA,
	old
};

public enum Genders {
	male,
	female
};