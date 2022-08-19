using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class Test{
	public List<TestQuestion> questions = new List<TestQuestion> ();
}

[System.Serializable]
public class TestQuestion{
	public int questionId;
	public string questionText;
	public List<TestAnswer> answers = new List<TestAnswer> ();
}

[System.Serializable]
public class TestAnswer{
	public string answerText;
	public StatInfluence influences = new StatInfluence();
}

[System.Serializable]
public class StatInfluence{
	public int ritorica;
	public int cunning;
	public int cruelty;
	public int attentiveness;
	public int survivialism;
	public int trading;
	public int obedience;
}
	
public class EntranceTestScript : MonoBehaviour {

	public TextAsset testFile;

	static Test eTest = new Test();
	public List<TestQuestion> myList = eTest.questions;
	public int curQuestionId;

	public Text questionText;
	public GameObject answersPanel;
	public GameObject thisCanvas;
	public GameObject answerBox;

	public Player newPlayer = new Player();
	public int ritorica;
	public int cunning;
	public int cruelty;
	public int attentiveness;
	public int survivialism;
	public int trading;

	public void ShowStats(){
		LoadPlayerStats ();

		ritorica = newPlayer.statPack.ritorica;
		cunning = newPlayer.statPack.cunning;
		cruelty = newPlayer.statPack.cruelty;
		attentiveness = newPlayer.statPack.attentiveness;
		survivialism = newPlayer.statPack.survivialism;
		trading = newPlayer.statPack.trading;
	}
	//-----------------------------------------------------------
	//----------------------Тестирование------------------------
	//-----------------------------------------------------------
	void Start(){
		//LoadTest ();
	}

	public void LoadTest(){
		LoadQuestions ();
		ShowQuestion (0);
	}

	public GameObject old, butt;

	public void ShowQuestion(int i){

		questionText.text = myList[i].questionText;

		var clones = GameObject.FindGameObjectsWithTag ("Clone");
		foreach (var clone in clones) Destroy(clone);

		GameObject canv = GameObject.Find ("Content");
		canv.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, eTest.questions[curQuestionId].answers.Count * 50.0f + 5.0f);

		int j = 0;
		foreach (TestAnswer a in eTest.questions[i].answers) {
			butt = Instantiate (answerBox);
			butt.tag = "Clone";
			butt.transform.SetParent (canv.transform, false);
			butt.GetComponentInChildren<Text> ().text = eTest.questions [i].answers [j].answerText;
			butt.GetComponent<questIdStorage> ().thisID = j;
			j++;
		}
	}

	public int selectedId;

	public void AnswerChoosen(){
		selectedId = EventSystem.current.currentSelectedGameObject.GetComponent<questIdStorage> ().thisID;

		cruelty += myList [curQuestionId].answers [selectedId].influences.cruelty;
		ritorica += myList [curQuestionId].answers [selectedId].influences.ritorica;
		cunning += myList [curQuestionId].answers [selectedId].influences.cunning;
		attentiveness += myList [curQuestionId].answers [selectedId].influences.attentiveness;
		survivialism += myList [curQuestionId].answers [selectedId].influences.survivialism;
		trading += myList [curQuestionId].answers [selectedId].influences.trading;

		if (curQuestionId < myList.Count-1) {
			curQuestionId++;
			ShowQuestion (curQuestionId);
		}
	}
	//-----------------------------------------------------------
	//----------------------Загрузка вопросов--------------------
	//-----------------------------------------------------------

	public void SavePlayerStats() {
		XmlSerializer serializer = new XmlSerializer(typeof(Player));
		FileStream stream = new FileStream("Assets/storages/playerStats.xml", FileMode.Create);
		serializer.Serialize(stream, newPlayer);
		stream.Close();
	}

	public void LoadPlayerStats() {
		XmlSerializer serializer = new XmlSerializer(typeof(Player));
		FileStream stream = new FileStream("Assets/storages/playerStats.xml", FileMode.Open);
		newPlayer = serializer.Deserialize(stream) as Player;
		stream.Close();
	}

	public void SaveQuestions() {
		//string path = GameObject.Find("XMLpath").GetComponent<InputField>().text;
		string path = "Assets/storages/entranceTest.xml";
		eTest.questions = myList;
		if (path.Length != 0 && File.Exists (path)) {
			XmlSerializer serializer = new XmlSerializer (typeof(Test));
			FileStream stream = new FileStream (path, FileMode.Create);
			serializer.Serialize (stream, eTest);
			stream.Close ();
		}
	}

	public void LoadQuestions() {

		//string path = GameObject.Find("XMLpath").GetComponent<InputField>().text;
		string path = "Assets/storages/entranceTest.xml";

		XmlSerializer serializer = new XmlSerializer(typeof(Test));
		if (path.Length != 0 && File.Exists (path)) {
			FileStream stream = new FileStream (path, FileMode.Open);
			//FileStream stream = new FileStream("Assets/storages/entranceTest.xml", FileMode.Open);
			eTest = serializer.Deserialize (stream) as Test;
			stream.Close ();
			myList = eTest.questions;

			ShowQuestion (curQuestionId);
		}
	}

	//-----------------------------------------------------------
	//----------------------Меню добавления вопросов-------------
	//-----------------------------------------------------------

	public void RenewAnswer(){
		selectedId = 0;
		GameObject.Find ("cruelty").GetComponentInChildren<InputField> ().text = "0";
		GameObject.Find ("ritorica").GetComponentInChildren<InputField> ().text = "0";
		GameObject.Find ("cunning").GetComponentInChildren<InputField> ().text = "0";
		GameObject.Find ("attentiveness").GetComponentInChildren<InputField> ().text = "0";
		GameObject.Find ("survivial").GetComponentInChildren<InputField> ().text = "0";
		GameObject.Find ("trade").GetComponentInChildren<InputField> ().text = "0";
		GameObject.Find ("obedience").GetComponentInChildren<InputField> ().text = "0";

		GameObject.Find ("answerText").GetComponent<InputField> ().text = "-";
	}

	public void renewQuestList(){
		GameObject EDITquestionBox = GameObject.Find ("EDITquestionBox");
		GameObject EDITquestion = GameObject.Find ("EDITquestion");

		var clones = GameObject.FindGameObjectsWithTag ("CloneQ");
		foreach (var clone in clones) Destroy(clone);

		int j =0;
		foreach (TestQuestion q in myList) {
			butt = Instantiate (EDITquestionBox);
			butt.tag = "CloneQ";
			butt.transform.SetParent (EDITquestion.transform, false);
			butt.GetComponentInChildren<Text> ().text = myList [j].questionId.ToString() + ". " + myList[j].questionText;
			butt.GetComponent<questIdStorage> ().thisID = j;
			j++;
		}

		EDITquestion.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, myList.Count * 100.0f + 5.0f);
	}

	public string wat;
	public void EDITLoadQuestions() {

		/*StringReader stream = new StringReader (testFile.text);
		wat = stream.ReadToEnd ();
		stream.Close ();*/


		//string path = GameObject.Find("XMLpath").GetComponent<InputField>().text;
		string path = "Assets/storages/entranceTest1.xml";

		myList.Clear ();
		curQuestionId = 0;

		XmlSerializer serializer = new XmlSerializer(typeof(Test));

		if (path.Length != 0 && File.Exists (path)) {
			FileStream stream = new FileStream (path, FileMode.Open);

			//StringReader stream = new StringReader (testFile.text);
			eTest = serializer.Deserialize (stream) as Test;
			stream.Close ();
			myList = eTest.questions;

			EDITShowQuestion (curQuestionId);
			renewQuestList ();
		}
	}

	public void EDITNewQuestion(){

		myList.Add (new TestQuestion());
		myList [myList.Count - 1].questionText = "НОВЫЙ ВОПРОС" + myList.Count;

		//List<TestQuestion> tqq = new List<TestQuestion> ();

		for (int i = 0; i < myList.Count; i++) {
			if (myList.Find (p => p.questionId == i) == null)
				myList [myList.Count - 1].questionId = i;
		}

		var clones = GameObject.FindGameObjectsWithTag ("CloneQ");
		foreach (var clone in clones) Destroy(clone);

		renewQuestList ();

		curQuestionId = myList.Count - 1;
		EDITShowQuestion (curQuestionId);
		RenewAnswer ();
	}

	public void EDITDeleteQuestion(){
		var clones = GameObject.FindGameObjectsWithTag ("CloneQ");
		foreach (var clone in clones) Destroy(clone);

		if (myList.Count > 0){
			myList.Remove(myList[curQuestionId]);
			curQuestionId = 0;

			if (myList.Count > 0) {
				renewQuestList ();
				EDITShowQuestion (curQuestionId);
				RenewAnswer ();
			}
		}
	}

	public GameObject questText;
	public void EDITRenewQuestion(){
		questText = GameObject.Find ("questText");
		myList [curQuestionId].questionText = questText.GetComponent<InputField> ().text;
		renewQuestList ();
	}

	public void EDITShowQuestion(int i){

		GameObject EDITanswerBox = GameObject.Find ("EDITanswerBox");
		GameObject EDITquestionText = GameObject.Find ("questText");

		EDITquestionText.GetComponent<InputField>().text = myList[i].questionText;

		var clones = GameObject.FindGameObjectsWithTag ("Clone");
		foreach (var clone in clones) Destroy(clone);

		GameObject canv = GameObject.Find ("answersContent");
		canv.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, myList[curQuestionId].answers.Count * 60.0f + 5.0f);

		int j = 0;
		foreach (TestAnswer a in eTest.questions[i].answers) {
			butt = Instantiate (EDITanswerBox);
			butt.tag = "Clone";
			butt.transform.SetParent (canv.transform, false);
			butt.GetComponentInChildren<Text> ().text = eTest.questions [i].answers [j].answerText;
			butt.GetComponent<questIdStorage> ().thisID = j;
			j++;
		}
	}

	public void EDITAnswerChoosen(){
		selectedId = EventSystem.current.currentSelectedGameObject.GetComponent<questIdStorage> ().thisID;

		GameObject.Find ("cruelty").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.cruelty.ToString ();
		GameObject.Find ("ritorica").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.ritorica.ToString ();
		GameObject.Find ("cunning").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.cunning.ToString ();
		GameObject.Find ("attentiveness").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.attentiveness.ToString ();
		GameObject.Find ("survivial").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.survivialism.ToString ();
		GameObject.Find ("trade").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.trading.ToString ();
		GameObject.Find ("obedience").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.obedience.ToString ();

		GameObject.Find("answerText").GetComponent<InputField>().text = myList [curQuestionId].answers [selectedId].answerText;
	}

	public void EDITQuestionChoosen(){
		selectedId = EventSystem.current.currentSelectedGameObject.GetComponent<questIdStorage> ().thisID;
		curQuestionId = selectedId;
		EDITShowQuestion (curQuestionId);
		RenewAnswer ();

	}

	public void EDITnewAnswer(){
		myList [curQuestionId].answers.Add (new TestAnswer ());
		selectedId = myList [curQuestionId].answers.Count - 1;
		myList [curQuestionId].answers [myList [curQuestionId].answers.Count - 1].answerText = "НОВЫЙ ВАРИАНТ ОТВЕТА " + myList [curQuestionId].answers.Count;

		EDITShowQuestion (curQuestionId);
		RenewAnswer ();

		GameObject.Find ("cruelty").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.cruelty.ToString ();
		GameObject.Find ("ritorica").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.ritorica.ToString ();
		GameObject.Find ("cunning").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.cunning.ToString ();
		GameObject.Find ("attentiveness").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.attentiveness.ToString ();
		GameObject.Find ("survivial").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.survivialism.ToString ();
		GameObject.Find ("trade").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.trading.ToString ();
		GameObject.Find ("obedience").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.obedience.ToString ();

		GameObject.Find("answerText").GetComponent<InputField>().text = myList [curQuestionId].answers [selectedId].answerText;

		GameObject canv = GameObject.Find ("answersContent");
		canv.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, myList[curQuestionId].answers.Count * 60.0f + 5.0f);
	}

	public void EDITdeleteAnswer(){
		if (myList [curQuestionId].answers.Count > 0) {
			myList [curQuestionId].answers.Remove (myList [curQuestionId].answers [selectedId]);
			selectedId = 0;

			if (myList [curQuestionId].answers.Count > 0) {
				EDITShowQuestion (curQuestionId);
				RenewAnswer ();

				GameObject.Find ("cruelty").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.cruelty.ToString ();
				GameObject.Find ("ritorica").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.ritorica.ToString ();
				GameObject.Find ("cunning").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.cunning.ToString ();
				GameObject.Find ("attentiveness").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.attentiveness.ToString ();
				GameObject.Find ("survivial").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.survivialism.ToString ();
				GameObject.Find ("trade").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.trading.ToString ();
				GameObject.Find ("obedience").GetComponentInChildren<InputField> ().text = myList [curQuestionId].answers [selectedId].influences.obedience.ToString ();

				GameObject.Find ("answerText").GetComponent<InputField> ().text = myList [curQuestionId].answers [selectedId].answerText;
			} else {
				EDITShowQuestion (curQuestionId);
				RenewAnswer ();
			}
		}
	}

	public void EDITrenewAnswer(){

		myList [curQuestionId].answers [selectedId].influences.cruelty = int.Parse(GameObject.Find ("cruelty").GetComponentInChildren<InputField> ().text);
		myList [curQuestionId].answers [selectedId].influences.ritorica = int.Parse (GameObject.Find ("ritorica").GetComponentInChildren<InputField> ().text);
		myList [curQuestionId].answers [selectedId].influences.cunning = int.Parse(GameObject.Find ("cunning").GetComponentInChildren<InputField> ().text);
		myList [curQuestionId].answers [selectedId].influences.attentiveness = int.Parse(GameObject.Find ("attentiveness").GetComponentInChildren<InputField> ().text);
		myList [curQuestionId].answers [selectedId].influences.survivialism = int.Parse(GameObject.Find ("survivial").GetComponentInChildren<InputField> ().text);
		myList [curQuestionId].answers [selectedId].influences.trading = int.Parse (GameObject.Find ("trade").GetComponentInChildren<InputField> ().text);
		myList [curQuestionId].answers [selectedId].influences.obedience = int.Parse(GameObject.Find ("obedience").GetComponentInChildren<InputField> ().text);

		myList [curQuestionId].answers [selectedId].answerText = GameObject.Find ("answerText").GetComponent<InputField> ().text;

		EDITShowQuestion (curQuestionId);
	}

	public void quit(){
		Application.Quit ();
	}
}
