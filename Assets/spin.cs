using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {
	void Update () 
	{
		this.gameObject.transform.Rotate(0,0,-3);
	}
}
