using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
	public GameObject bg0;
	public GameObject bg1;
	public GameObject bg2;
	public GameObject bg3;

	public float speed;
	public float bg1PercentIncrease;
	public float bg2PercentIncrease;
	public float bg3PercentIncrease;

	void Start () 
	{
		base.EnforceSingleton();
	}
	
	public void MoveBackground(float time)
	{
		bg0.transform.Translate(time * Vector3.left * speed);
		bg1.transform.Translate(time * Vector3.left * speed * bg1PercentIncrease);
		bg2.transform.Translate(time * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease);
		bg3.transform.Translate(time * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease * bg3PercentIncrease);
	}
}
