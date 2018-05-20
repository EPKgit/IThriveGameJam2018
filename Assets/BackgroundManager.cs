using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : Singleton<BackgroundManager>
{
	public GameObject bus;
	public GameObject bWheel;
	public GameObject fWheel;

	public GameObject bg0;
	public GameObject bg1;
	public GameObject bg2;
	public GameObject bg3;

	public GameObject[] bg2s;
	public GameObject[] bg3s;

	private int bg2index;
	private int bg3index;

	public float busBouncePeriod;
	public float busBounceAmount;
	public float speed;
	public float bg1PercentIncrease;
	public float bg2PercentIncrease;
	public float bg3PercentIncrease;

	public GameObject loadingScreen;

	[SerializeField]private bool loading;


	void Start () 
	{
		base.EnforceSingleton();
		bg2index = 0;
		bg3index = 0;
		loading = true;
		StartCoroutine(WaitForLoad());
	}

	IEnumerator WaitForLoad()
	{
		yield return new WaitForSecondsRealtime(2f);
		Time.timeScale = 1.2f;
		loadingScreen.SetActive(false);
		loading = false;
	}

	/*void Update()
	{
		if(Input.GetKeyDown(KeyCode.F))
			Time.timeScale = 20;
		if(Input.GetKeyUp(KeyCode.F))
			Time.timeScale = 1;
	}*/
	
	public void MoveBackground(float time)
	{
		if(loading) return;
		DoBusBounce(time);
		bg0.transform.Translate(Time.deltaTime * Vector3.left * speed);
		bg1.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease);
		bg2.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease);
		bg3.transform.Translate(Time.deltaTime * Vector3.left * speed * bg1PercentIncrease * bg2PercentIncrease * bg3PercentIncrease);
		SpawnBG2(time);
		SpawnBG3(time);
		if(time >= 245)
			EndGame();
	}

	void EndGame()
	{
		AudioManager.instance.GameEnded();
		SceneManager.LoadScene("Credits");
	}

	void SpawnBG3(float time)
	{
		if(time >= 110 && bg3index < 1)
			bg3index = 1;
		if(time % 3.8 <= Time.deltaTime)
		{
			GameObject temp;
			if(bg3index == 0)
				temp = Instantiate(bg3s[bg3index], new Vector3(41, -4f, 0), Quaternion.identity);
			else if(bg3index == 1)
			{
				temp = Instantiate(bg3s[bg3index], new Vector3(41, -4f, 0), Quaternion.identity);
				bg3index = 2;
			}
			else
				temp = Instantiate(bg3s[bg3index], new Vector3(41, -4f, 0), Quaternion.identity);	
			temp.transform.SetParent(bg3.transform);
		}
	}
	
	void SpawnBG2(float time)
	{
		if(time >= 110 && bg2index < 1)
			bg2index = 1;
		if(time % 3.8 <= Time.deltaTime)
		{
			GameObject temp;
			if(bg2index == 0)
				temp = Instantiate(bg2s[bg2index], new Vector3(41, 0, 0), Quaternion.identity);
			else if(bg2index == 1)
			{
				temp = Instantiate(bg2s[bg2index], new Vector3(41, -2.8f, 0), Quaternion.identity);
				bg2index = 2;
			}
			else
				temp = Instantiate(bg2s[bg2index], new Vector3(41, -2.8f, 0), Quaternion.identity);	
			temp.transform.SetParent(bg2.transform);
		}
	}

	void DoBusBounce(float time)
	{
		bus.transform.Translate(Time.deltaTime * Mathf.Sin(busBouncePeriod * time) * Vector3.up * (1/busBounceAmount) );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		bWheel.transform.Rotate(0,0,-3);
		bWheel.transform.Translate(Time.deltaTime * Mathf.Sin(busBouncePeriod * time - 1) * Vector3.up * (1/busBounceAmount) );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
		fWheel.transform.Rotate(0,0,-3);
		fWheel.transform.Translate(Time.deltaTime * Mathf.Sin(busBouncePeriod * time - 1) * Vector3.up * (1/busBounceAmount) );//(Mathf.Sin(time) < 0 ? Vector2.up : Vector2.down) * busBounce);
	}
}
