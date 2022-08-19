using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Face{
	public Sprite img;
	public Ages age;
}

public class Frowns{
	public Sprite img;
	public Ages age;
}

public class Nose{
	public Sprite img;
	public Ages age;
}

public class Eyes{
	public Sprite img;
	public Genders gen;
}

public class Mouth{
	public Sprite img;
	public Genders gen;
}

public class Hair{
	public Sprite img;
	public Ages age;
	public Genders gen;
}

public class Body{
	public Sprite img;
	public Genders gen;
	public int statusID;
}

public class ImageManagerScript : MonoBehaviour {
	public Sprite sp;
	/*public Ages agg;
	public Genders gg;
	public int i1, i2 = 0;*/
	public InputField pathF;

	public Text age;
	public Text gen;

	public Text cFaceID;
	public Text cHairID;
	public Text cFrownsID;
	public Text cEyesID;
	public Text cNoseID;
	public Text cMouthID;

	public Text valFacesCnt;
	public Text valHairsCnt;
	public Text valEyesCnt;
	public Text valNosesCnt;
	public Text valFrownsCnt;
	public Text valMouthsCnt;

	public List<Face> faces = new List<Face>();
	public List<Hair> hairs = new List<Hair>();
	public List<Body> bodies = new List<Body>();

	public List<Frowns> frowns = new List<Frowns>();
	public List<Sprite> bgs = new List<Sprite>();
	public List<Eyes> eyes = new List<Eyes>();
	public List<Nose> noses = new List<Nose>();
	public List<Mouth> mouths = new List<Mouth>();

	//----------------------ВЫБОР ДЕТАЛЕЙ--------------------

	public List<Face> facesOnFilter = new List<Face>();
	public List<Hair> hairsOnFilter = new List<Hair>();
	public List<Body> bodiesOnFilter = new List<Body>();

	public List<Frowns> frownsOnFilter = new List<Frowns>();
	public List<Eyes> eyesOnFilter = new List<Eyes>();
	public List<Nose> nosesOnFilter = new List<Nose>();
	public List<Mouth> mouthsOnFilter = new List<Mouth>();

	public Slider valPick;
	public Text WhatIsPicking;

	public int pickingNow;

	public Ages curAge;
	public Genders curGen;
	public int curFacePart;

	public int curFaceID;
	public int curHairID;
	public int curBodyID;
	public int curFrownsID;
	public int curEyesID;
	public int curNoseID;
	public int curMouthID;


	public void SwitchFacePart(){
		if (faces.Count != 0) {
			listFilter ("face");
			if (facesOnFilter.Count != 0) {
				Image img = faceF.GetComponent<Image> ();
				img.sprite = facesOnFilter [curFaceID].img;
			}
		}

		if (hairs.Count != 0) {
			listFilter ("hair");
			if (hairsOnFilter.Count != 0) {
				Image img = hairF.GetComponent<Image> ();
				img.sprite = hairsOnFilter[curHairID].img;
			}
		}

		if (bodies.Count != 0) {
			listFilter ("body");
			if (bodiesOnFilter.Count != 0) {
				Image img = bodyF.GetComponent<Image> ();
				img.sprite = bodiesOnFilter[curBodyID].img;
			}
		}

		if (frowns.Count != 0) {
			listFilter ("frowns");
			if (frownsOnFilter.Count != 0) {
				Image img = frownF.GetComponent<Image> ();
				img.sprite = frownsOnFilter[curFrownsID].img;
			}
		}

		if (eyes.Count != 0) {
			listFilter ("eyes");
			if (eyesOnFilter.Count != 0) {
				Image img = eyeF.GetComponent<Image> ();
				img.sprite = eyesOnFilter [curEyesID].img;
			}
		}

		if (noses.Count != 0) {
			listFilter ("nose");
			if (nosesOnFilter.Count != 0) {
				Image img = noseF.GetComponent<Image> ();
				img.sprite = nosesOnFilter [curNoseID].img;
			}
		}

		if (mouths.Count != 0) {
			listFilter ("mouth");
			if (mouthsOnFilter.Count != 0) {
				Image img = mouthF.GetComponent<Image> ();
				img.sprite = mouthsOnFilter [curMouthID].img;
			}
		}

		age.text = curAge.ToString();
		gen.text = curGen.ToString();
	}

	public void listFilter(string s){
		if (s == "face"){
			facesOnFilter.Clear ();
				foreach (Face f in faces) {
					if (f.age == curAge)
						facesOnFilter.Add (f);
			}
		}

		else if(s == "frowns"){
			frownsOnFilter.Clear ();
			foreach (Frowns fr in frowns) {
				if (fr.age == curAge)
					frownsOnFilter.Add (fr);
			}
		}

		else if(s == "nose"){
			nosesOnFilter.Clear ();
			foreach (Nose n in noses) {
				if (n.age == curAge)
					nosesOnFilter.Add (n);
			}
		}

		else if(s == "eyes"){
			eyesOnFilter.Clear ();
			foreach (Eyes e in eyes) {
				if (e.gen == curGen)
					eyesOnFilter.Add (e);
			}
		}

		else if(s == "mouth"){
			mouthsOnFilter.Clear ();
			foreach (Mouth m in mouths) {
				if (m.gen == curGen)
					mouthsOnFilter.Add (m);
			}
		}

		else if(s == "hair"){
			hairsOnFilter.Clear ();
			foreach (Hair h in hairs) {
				if (h.gen == curGen && h.age == curAge)
					hairsOnFilter.Add (h);
			}
		}

		cFaceID.text = (curFaceID+1).ToString ();
		cHairID.text = (curHairID+1).ToString ();
		cFrownsID.text = (curFrownsID+1).ToString ();
		cEyesID.text = (curEyesID+1).ToString ();
		cNoseID.text = (curNoseID+1).ToString ();
		cMouthID.text = (curMouthID+1).ToString ();

		valFacesCnt.text = facesOnFilter.Count.ToString ();
		valHairsCnt.text = hairsOnFilter.Count.ToString ();
		valFrownsCnt.text = frownsOnFilter.Count.ToString ();
		valEyesCnt.text = eyesOnFilter.Count.ToString ();
		valNosesCnt.text = nosesOnFilter.Count.ToString ();
		valMouthsCnt.text = mouthsOnFilter.Count.ToString ();
	}

	//-----------------НАЖАТИЕ КНОПОК------------------------
	public void Press1(){
		pickingNow = 1;
		valPick.maxValue = 1;
		if (curGen == Genders.male)
			valPick.value = 0;
		else 
			valPick.value = 1;

		WhatIsPicking.text = "пол";
	}
	public void Press2(){
		pickingNow = 2;
		valPick.maxValue = 3;
		if (curAge == Ages.child)
			valPick.value = 0;
		else if (curAge == Ages.young)
			valPick.value = 1;
		else if (curAge == Ages.mA)
			valPick.value = 2;
		else if (curAge == Ages.old)
			valPick.value = 3;
		WhatIsPicking.text = "возраст";
	}
	public void Press3(){
		pickingNow = 3;
		SwitchFacePart ();
		valPick.maxValue = facesOnFilter.Count-1;
		valPick.value = curFaceID;
		WhatIsPicking.text = "лицо";
	}
	public void Press4(){
		pickingNow = 4;
		valPick.maxValue = hairsOnFilter.Count-1;
		valPick.value = curHairID;
		WhatIsPicking.text = "волосы";
	}
	public void Press5(){
		pickingNow = 5;
		valPick.maxValue = frownsOnFilter.Count-1;
		valPick.value = curFrownsID;
		WhatIsPicking.text = "брови";
	}
	public void Press6(){
		pickingNow = 6;
		valPick.maxValue = eyesOnFilter.Count-1;
		valPick.value = curEyesID;
		WhatIsPicking.text = "глаза";
	}
	public void Press7(){
		pickingNow = 7;
		valPick.maxValue = nosesOnFilter.Count-1;
		valPick.value = curNoseID;
		WhatIsPicking.text = "нос";
	}
	public void Press8(){
		pickingNow = 8;
		valPick.maxValue = mouthsOnFilter.Count-1;
		valPick.value = curMouthID;
		WhatIsPicking.text = "рот";
	}
	public void Press9(){
		pickingNow = 9;
		valPick.maxValue = 1;
	}

	public void SwitchF(){
		if(pickingNow == 1){
			if (curGen == Genders.male) {
				curGen = Genders.female;
				valPick.value = 1;
			}

			curFaceID = 0;
			curBodyID = 0;
			curHairID = 0;
			curEyesID = 0;
			curFrownsID = 0;
			curNoseID = 0;
			curMouthID = 0;
		}

		else if (pickingNow == 2) {
			if (curAge == Ages.child) {
				curAge = Ages.young;
				valPick.value = 1;
			}
			else if (curAge == Ages.young) {
				curAge = Ages.mA;
				valPick.value = 2;
			}
			else if (curAge == Ages.mA) {
				curAge = Ages.old;
				valPick.value = 3;
			}

			curFaceID = 0;
			curBodyID = 0;
			curHairID = 0;
			curEyesID = 0;
			curFrownsID = 0;
			curNoseID = 0;
			curMouthID = 0;
		}

		else if (pickingNow == 3 && curFaceID < (facesOnFilter.Count -1)) {
			curFaceID++;
			valPick.value = curFaceID;
		}
		else if (pickingNow == 4 && curHairID < (hairsOnFilter.Count -1)) {
			curHairID++;
			valPick.value = curHairID;
		}
		else if (pickingNow == 5 && curFrownsID < (frownsOnFilter.Count -1)) {
			curFrownsID++;
			valPick.value = curFrownsID;
		}
		else if (pickingNow == 6 && curEyesID < (eyesOnFilter.Count -1)) {
			curEyesID++;
			valPick.value = curEyesID;
		}
		else if (pickingNow == 7 && curNoseID < (nosesOnFilter.Count -1)) {
			curNoseID++;
			valPick.value = curNoseID;
		}
		else if (pickingNow == 8 && curMouthID < (mouthsOnFilter.Count -1)) {
			curMouthID++;
			valPick.value = curMouthID;
		}
		else {
			
		}


		SwitchFacePart ();
	}

	public void SwitchB(){
		if(pickingNow == 1){
			if (curGen == Genders.female) {
				curGen = Genders.male;
				valPick.value = 0;
			}
			curFaceID = 0;
			curBodyID = 0;
			curHairID = 0;
			curEyesID = 0;
			curFrownsID = 0;
			curNoseID = 0;
			curMouthID = 0;
		}

		else if (pickingNow == 2) {
			if (curAge == Ages.young) {
				curAge = Ages.child;
				valPick.value = 0;
			} 
			else if (curAge == Ages.mA) {
				curAge = Ages.young;
				valPick.value = 1;
			}
			else if (curAge == Ages.old) {
				curAge = Ages.mA;
				valPick.value = 2;
			}

			curFaceID = 0;
			curBodyID = 0;
			curHairID = 0;
			curEyesID = 0;
			curFrownsID = 0;
			curNoseID = 0;
			curMouthID = 0;
		}

		else if (pickingNow == 3 && curFaceID > 0) {
			curFaceID--;
			valPick.value = curFaceID;
		}
		else if (pickingNow == 4 && curHairID > 0) {
			curHairID--;
			valPick.value = curHairID;
		}
		else if (pickingNow == 5 && curFrownsID > 0) {
			curFrownsID--;
			valPick.value = curFrownsID;
		}
		else if (pickingNow == 6 && curEyesID > 0) {
			curEyesID--;
			valPick.value = curEyesID;
		}
		else if (pickingNow == 7 && curNoseID > 0) {
			curNoseID--;
			valPick.value = curNoseID;
		}
		else if (pickingNow == 8 && curMouthID > 0) {
			curMouthID--;
			valPick.value = curMouthID;
		}
		else {

		}

		SwitchFacePart ();
	}

	//-------------------------------------------------------
	//-------------------------------------------------------

	public void onSliderValueChange(){
		if (pickingNow == 3) {
			curFaceID = (int)valPick.value;
		} else if (pickingNow == 4) {
			curHairID = (int)valPick.value;
		}
		else if (pickingNow == 5) {
			curFrownsID = (int)valPick.value;
		}
		else if (pickingNow == 6) {
			curEyesID = (int)valPick.value;
		}
		else if (pickingNow == 7) {
			curNoseID = (int)valPick.value;
		}

		else if (pickingNow == 8) {
			curMouthID = (int)valPick.value;
		}

		else if (pickingNow == 1) {
			if (valPick.value == 1)
				curGen = Genders.female;
			else
				curGen = Genders.male;

			curFaceID = 0;
			curBodyID = 0;
			curHairID = 0;
			curEyesID = 0;
			curFrownsID = 0;
			curNoseID = 0;
			curMouthID = 0;
		}

		else if (pickingNow == 2) {
			if (valPick.value == 0)
				curAge = Ages.child;
			else if (valPick.value == 1)
				curAge = Ages.young;
			else if (valPick.value == 2)
				curAge = Ages.mA;
			else
				curAge = Ages.old;

			curFaceID = 0;
			curBodyID = 0;
			curHairID = 0;
			curEyesID = 0;
			curFrownsID = 0;
			curNoseID = 0;
			curMouthID = 0;
		}

		SwitchFacePart ();
	}

	//-------------------------------------------------------
	//-------------------------------------------------------

	public string fileName;
	public FileInfo[] info;

	public GameObject faceF;
	public GameObject hairF;
	public GameObject bodyF;
	public GameObject frownF;
	public GameObject eyeF;
	public GameObject noseF;
	public GameObject mouthF;
	public GameObject bgF;

	public int r;

	public void GenerateAndDisplayRND(){

		curBodyID = Random.Range (0, bodiesOnFilter.Count);
		curFaceID = Random.Range (0, facesOnFilter.Count);
		curHairID = Random.Range (0, hairsOnFilter.Count);
		curFrownsID = Random.Range (0, frownsOnFilter.Count);
		curEyesID = Random.Range (0, eyesOnFilter.Count);
		curNoseID = Random.Range (0, nosesOnFilter.Count);
		curMouthID = Random.Range (0, mouthsOnFilter.Count);

		SwitchFacePart ();
	}

	// ----------------------------------------------------------------------------------------
	// -------------------------------------ЗАГРУЗКА СПРАЙТОВ----------------------------------
	// ----------------------------------------------------------------------------------------

	public Sprite LoadNewSprite(string FilePath){
		Sprite NewSprite;
		Texture2D SpriteTexture = LoadTexture(FilePath);

		Texture2D nTex;
		nTex = LoadTexture(FilePath);

		NewSprite = Sprite.Create(nTex, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), 1024.0f);
		return NewSprite;
	}

	public Texture2D Tex2D;

	public Texture2D LoadTexture(string FilePath){
		byte[] FileData;

		if (File.Exists (FilePath)) {
			FileData = File.ReadAllBytes (FilePath);
			Tex2D = new Texture2D (2, 2);
			if (Tex2D.LoadImage (FileData)) {
				return Tex2D;
			}
		}
		return null;
	}

	public void LoadSprites(){

		/*if (File.Exists (pathF.text)) {
			string[] loadedImgs = Directory.GetFiles (pathF.text, "*.png");
			foreach (string f in loadedImgs) {
				string fName = f.Substring (pathF.text.Length + 1);
				File.Copy (Path.Combine (pathF.text, fName), Path.Combine (path, fName));
			}
		}*/

		//-------------------


		bgs.Clear ();
		frowns.Clear ();
		eyes.Clear ();
		noses.Clear ();
		mouths.Clear ();
		faces.Clear ();
		hairs.Clear ();
		bodies.Clear ();


		//if (pathF.text != "" && Directory.Exists(pathF.text)){
			
		string path = pathF.text;
		var dir = new DirectoryInfo (path + "/");
			info = dir.GetFiles("*.png");
	
			foreach (FileInfo f in info) {
				fileName = f.Name;

				//path = pathF.text + fileName;
				//int i = fileName.IndexOf (".");
				//fileName = fileName.Remove (i, 4);
				//Sprite sp = Resources.Load<Sprite>("sprites/personaFrame/" + fileName);


			AddNewImageToSprite (fileName, path + "/" + fileName);//pathF.text + "/" + fileName);
			}

			/*sp = hairs [i2].img;
			agg = hairs [i2].age;
			gg = hairs [i2].gen;
			i1 = hairs.Count;*/
		//}

		SwitchFacePart ();
		Press1 ();
	}

	public void AddNewImageToSprite(string fileName, string fullPath){

		Sprite sp = LoadNewSprite (fullPath);

		if(fileName.IndexOf ("_fem") != -1){
			if (fileName.IndexOf ("eyes") != -1) {
				eyes.Add (new Eyes ());
				eyes [eyes.Count - 1].gen = Genders.female;
				eyes [eyes.Count - 1].img = sp;
			}
			else if (fileName.IndexOf ("mouth") != -1) {
				mouths.Add (new Mouth ());
				mouths [mouths.Count - 1].gen = Genders.female;
				mouths [mouths.Count - 1].img = sp;
			}
			else if (fileName.IndexOf ("body") != -1) {

			}
		}

		if(fileName.IndexOf ("_male") != -1){
			if (fileName.IndexOf ("eyes") != -1) {
				eyes.Add (new Eyes ());
				eyes [eyes.Count - 1].gen = Genders.male;
				eyes [eyes.Count - 1].img = sp;
			}
			else if (fileName.IndexOf ("mouth") != -1) {
				mouths.Add (new Mouth ());
				mouths [mouths.Count - 1].gen = Genders.male;
				mouths [mouths.Count - 1].img = sp;
			}
		}

		// --------------------------------------РЕБЕНОК----------------------------------------------------
		if (fileName.IndexOf ("_child") != -1) {

			if (fileName.IndexOf ("face") != -1) {
				faces.Add (new Face ());
				faces [faces.Count - 1].img = sp;
				faces [faces.Count - 1].age = Ages.child;
			}

			if (fileName.IndexOf ("frowns") != -1) {
				frowns.Add (new Frowns ());
				frowns[frowns.Count - 1].img = sp;
				frowns[frowns.Count - 1].age = Ages.child;
			}

			if (fileName.IndexOf ("nose") != -1) {
				noses.Add (new Nose ());
				noses [noses.Count - 1].img = sp;
				noses [noses.Count - 1].age = Ages.child;
			}

			if (fileName.IndexOf ("_male") != -1) {		
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.male;
					hairs [hairs.Count - 1].age = Ages.child;
				}
			}

			if (fileName.IndexOf ("_fem") != -1) {	
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.female;
					hairs [hairs.Count - 1].age = Ages.child;
				}
			}
		}
		// -------------------------------------------МОЛОДОЙ-----------------------------------------------
		if (fileName.IndexOf ("_young") != -1) {

			if (fileName.IndexOf ("face") != -1) {
				faces.Add (new Face ());
				faces [faces.Count - 1].img = sp;
				faces [faces.Count - 1].age = Ages.young;
			}

			if (fileName.IndexOf ("frowns") != -1) {
				frowns.Add (new Frowns ());
				frowns[frowns.Count - 1].img = sp;
				frowns[frowns.Count - 1].age = Ages.young;
			}

			if (fileName.IndexOf ("nose") != -1) {
				noses.Add (new Nose ());
				noses [noses.Count - 1].img = sp;
				noses [noses.Count - 1].age = Ages.young;
			}

			if (fileName.IndexOf ("_male") != -1) {		
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.male;
					hairs [hairs.Count - 1].age = Ages.young;
				}
			}

			if (fileName.IndexOf ("_fem") != -1) {	
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.female;
					hairs [hairs.Count - 1].age = Ages.young;
				}
			}		
		}
		// --------------------------------------------СРЕДНИХ ЛЕТ----------------------------------------------
		if (fileName.IndexOf ("_mA") != -1) {

			if (fileName.IndexOf ("face") != -1) {
				faces.Add (new Face ());
				faces [faces.Count - 1].img = sp;
				faces [faces.Count - 1].age = Ages.mA;
			}


			if (fileName.IndexOf ("frowns") != -1) {
				frowns.Add (new Frowns ());
				frowns[frowns.Count - 1].img = sp;
				frowns[frowns.Count - 1].age = Ages.mA;
			}


			if (fileName.IndexOf ("nose") != -1) {
				noses.Add (new Nose ());
				noses [noses.Count - 1].img = sp;
				noses [noses.Count - 1].age = Ages.mA;
			}

			if (fileName.IndexOf ("_male") != -1) {		
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.male;
					hairs [hairs.Count - 1].age = Ages.mA;
				}
				else if (fileName.IndexOf ("body") != -1) {

				}
			}

			if (fileName.IndexOf ("_fem") != -1) {	
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.female;
					hairs [hairs.Count - 1].age = Ages.mA;
				}
			}
		}
		// --------------------------------------------СТАРЫЙ----------------------------------------------
		if (fileName.IndexOf ("_old") != -1) {

			if (fileName.IndexOf ("face") != -1) {
				faces.Add (new Face ());
				faces [faces.Count - 1].img = sp;
				faces [faces.Count - 1].age = Ages.old;
			}


			if (fileName.IndexOf ("frowns") != -1) {
				frowns.Add (new Frowns ());
				frowns[frowns.Count - 1].img = sp;
				frowns[frowns.Count - 1].age = Ages.old;
			}


			if (fileName.IndexOf ("nose") != -1) {
				noses.Add (new Nose ());
				noses [noses.Count - 1].img = sp;
				noses [noses.Count - 1].age = Ages.old;
			}

			if (fileName.IndexOf ("_male") != -1) {		
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.male;
					hairs [hairs.Count - 1].age = Ages.old;
				}
				else if (fileName.IndexOf ("body") != -1) {

				}
			}

			if (fileName.IndexOf ("_fem") != -1) {	
				if (fileName.IndexOf ("hair") != -1) {
					hairs.Add (new Hair ());
					hairs [hairs.Count - 1].img = sp;
					hairs [hairs.Count - 1].gen = Genders.female;
					hairs [hairs.Count - 1].age = Ages.old;
				}
			}
		}
		// ------------------------------------------------------------------------------------------
														//БЕЗ ДИФФЕРЕНЦИРОВАНИя
		else {
			if (fileName.IndexOf ("bg") != -1)
				bgs.Add (sp);
		}
	} //end addNewImgSprite
}
