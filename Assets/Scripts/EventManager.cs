using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
	
	public float gameTime;

	public List<Event> events;
	private List<Event> activeEvents;
	//public Queue<Event> events;

	void Start () 
	{
		events = new List<Event>();
		activeEvents = new List<Event>();
		gameTime = 0;
		StartCoroutine(RunGame());
	}
	
	IEnumerator RunGame()
	{
		yield return new WaitUntil( () => BackgroundManager.instance != null);
		while(true)
		{	
			gameTime += Time.deltaTime;
			
			foreach (Event e in events)
			{
				if (!e.checkTrigger(gameTime)) // tries to start events. True means it's a one time event, so don't add to activeEvents
					activeEvents.Add(e);
			}
			foreach (Event e in activeEvents)
			{
				if (e.checkOutcome(gameTime)) // resolves outcomes for event. True means the outcome terminates the event.
					activeEvents.Remove(e);
			}
			
			BackgroundManager.instance.MoveBackground(gameTime);
			yield return null;
		}
	}
	

}
