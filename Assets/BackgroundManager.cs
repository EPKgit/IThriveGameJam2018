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

	public float busBouncePeriod;
	public float busBounceAmount;
	public float speed;
	public float bg1PercentIncrease;
	public float bg2PercentIncrease;
	public float bg3PercentIncrease;

	public LayerMask layer;

	private float busStartingY;
	private float fWStartingY;
	private float bWStartingY;
	

	void Start () 
	{
		base.EnforceSingleton();
		Physics2D.IgnoreLayerCollision(layer, layer, true);
		busStartingY = bus.transform.position.y;
		fWStartingY = fWheel.transform.position.y;
		bWStartingY = bWheel.transform.position.y;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F))
			Time.timeScale = 20;
		if(Input.GetKeyUp(KeyCode.F))
			Time.timeScale = 1;
	}

	public void MoveBackground(float time)
	{
		Vector3 pos = bus.transform.position;
		bus.transform.position = new Vector3(pos.x, busStartingY + (Time.deltaTime * Mathf.Sin(busBouncePeriod * time) * busBounceAmount), pos.z );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		bWheel.transform.Rotate(0,0,-3);
		fWheel.transform.Rotate(0,0,-3);
		pos = bWheel.transform.position;
		bWheel.transform.position = new Vector3(pos.x, fWStartingY + (Time.deltaTime * Mathf.Sin(busBouncePeriod * time - 1) * busBounceAmount), pos.z );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		pos = fWheel.transform.position;
		fWheel.transform.position = new Vector3(pos.x, bWStartingY + (Time.deltaTime * Mathf.Sin(busBouncePeriod * time - 1) * busBounceAmount), pos.z );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		
		bg0.transform.Translate(Time.deltaTime * Vector3.left * speed);
		bg1.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease);
		if(bg1.transform.position.x + 1000 < bus.transform.position.x)
			Debug.Log("1");
		bg2.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease);
		if(bg2.transform.position.x + 1000 < bus.transform.position.x)
			Debug.Log("2");
		bg3.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease * bg3PercentIncrease);
		if(bg3.transform.position.x + 1000 < bus.transform.position.x)
			Debug.Log("3");
	}
}
