using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questIdStorage : MonoBehaviour {
	
	public int thisID;
	public static questIdStorage ins;

	void Awake() {
		ins = this;
	}
}
