using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
	public float gameTime;

	public Event[] events;
	//public Queue<Event> events;

	void Start () 
	{
		gameTime = 0;
		StartCoroutine(RunGame());
	}
	
	IEnumerator RunGame()
	{
		gameTime += Time.deltaTime;
		yield return null;
	}

}
