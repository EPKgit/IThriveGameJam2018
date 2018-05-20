using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour 
{

	void Start () 
	{
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
