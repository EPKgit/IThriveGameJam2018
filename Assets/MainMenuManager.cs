using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour 
{
	public AudioSource mainMenuMusic;

	void Start () 
	{
		mainMenuMusic.Play();
	}
	
	void Update ()
	{
		
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SwitchToSettings()
	{

	}
}
