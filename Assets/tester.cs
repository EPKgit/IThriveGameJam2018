using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1))
			this.GetComponent<Character>().setMood(-2);
			if(Input.GetKeyDown(KeyCode.F2))
			this.GetComponent<Character>().setMood(-1);
			if(Input.GetKeyDown(KeyCode.F3))
			this.GetComponent<Character>().setMood(0);
			if(Input.GetKeyDown(KeyCode.F4))
			this.GetComponent<Character>().setMood(1);
			if(Input.GetKeyDown(KeyCode.F5))
			this.GetComponent<Character>().setMood(2);
	}
}
