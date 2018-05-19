using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
	
	public float gameTime;

	public Event[] events;
	private List<Event> activeEvents;
	//public Queue<Event> events;

	void Start () 
	{
		activeEvents = new List<Event>();
		gameTime = 0;
		StartCoroutine(RunGame());
	}
	
	IEnumerator RunGame()
	{
		while(true)
		{	
			gameTime += Time.deltaTime;
			/*
			foreach (Event e in events)
			{
				if (!e.checkTriggers(gameTime)) // tries to start events. True means it's a one time event, so don't add to activeEvents
					activeEvents.Add(e);
			}
			foreach (Event e in activeEvents)
			{
				if (e.checkOutcomes(gameTime)) // resolves outcomes for event. True means the outcome terminates the event.
					activeEvents.Remove(e);
			}
			*/
			BackgroundManager.instance.MoveBackground(Time.deltaTime);
			yield return null;
		}
	}
	

}
