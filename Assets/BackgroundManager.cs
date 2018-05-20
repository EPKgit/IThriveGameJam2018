using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
	public GameObject bus;
	public GameObject bWheel;
	public GameObject fWheel;

	public GameObject bg0;
	public GameObject bg1;
	public GameObject bg2;
	public GameObject bg3;

	//public GameObject[] bg0s;
	public GameObject[] bg1s;
	public GameObject[] bg2s;
	public GameObject[] bg3s;

	//private int bg0index;
	private int bg1index;
	private int bg2index;
	private int bg3index;

	public float busBouncePeriod;
	public float busBounceAmount;
	public float speed;
	public float bg1PercentIncrease;
	public float bg2PercentIncrease;
	public float bg3PercentIncrease;


	void Start () 
	{
		base.EnforceSingleton();
		//bg0index = 0;
		bg1index = 0;
		bg2index = 0;
		bg3index = 0;
	}
	
	public void MoveBackground(float time)
	{
		bus.transform.Translate(Time.deltaTime * Mathf.Sin(busBouncePeriod * time) * Vector3.up * (1/busBounceAmount) );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		bWheel.transform.Rotate(0,0,-3);
		bWheel.transform.Translate(Time.deltaTime * Mathf.Sin(busBouncePeriod * time - 1) * Vector3.up * (1/busBounceAmount) );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		fWheel.transform.Rotate(0,0,-3);
		fWheel.transform.Translate(Time.deltaTime * Mathf.Sin(busBouncePeriod * time - 1) * Vector3.up * (1/busBounceAmount) );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		bg0.transform.Translate(Time.deltaTime * Vector3.left * speed);
		bg1.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease);
		bg2.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease);
		bg3.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease * bg3PercentIncrease);
	}
}
