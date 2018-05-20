using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
	
	public float gameTime;

	public List<Event> events;
	private List<Event> activeEvents;
	private List<Event> removeEvents;
	public List<Character> characters;

	IEnumerator Start () 
	{
		yield return new WaitUntil(() => PlayerInteraction.instance != null);
		events = new List<Event>();
		activeEvents = new List<Event>();
		removeEvents = new List<Event>();

		characters = PlayerInteraction.instance.chars;

		populateEvents();

		gameTime = 0;
		StartCoroutine(RunGame());
	}
	
	IEnumerator RunGame()
	{
		yield return new WaitUntil( () => BackgroundManager.instance != null);
		while(true)
		{	
			gameTime += Time.deltaTime;
			//Debug.Log(gameTime);
			foreach (Event e in events)
			{
				if (e.checkTrigger(gameTime)) // tries to start events
					activeEvents.Add(e);
			}
			foreach (Event e in activeEvents)
			{
				if (e.checkOutcome(gameTime)) // resolves outcomes for event. True means the outcome terminates the event.
					removeEvents.Add(e);
			}
			foreach (Event e in removeEvents)
			{
				activeEvents.Remove(e);
			}
			removeEvents.Clear();
			
			BackgroundManager.instance.MoveBackground(gameTime);

			yield return new WaitForEndOfFrame();
		}
	}
	

	private void populateEvents()
	{
		

		return;
	}

	private void EXAMPLE_EVENT()
	{
		voidFunction[] a = {
			() => {Debug.Log("Action Triggered by event");}, //Actions when triggered
			() => {Debug.Log("Multiple actions can be triggered");}
			};
		boolFunction[] t = {
			() => {Debug.Log("Testing event for trigger (there can be more than one)"); return true;}//Trigger conditions
			};

		float s = 10f;//When to start the whole event

		Outcome[] o = new Outcome[4]{
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 1");}),new voidFunction(() => {Debug.Log("Outcome 1 pt2");})},	//actions triggered BY outcome
						new boolFunction[2]{new boolFunction(() => {Debug.Log("Check Outcome 1"); return true;}),new boolFunction(() => {Debug.Log("Second condition for outcome 1"); return true;})}, //conditions that trigger outcome (only triggers if all are true)
								10f,	// how long since the event started to check for the outcome completion													
								true),	// Is this the final event? true means the event ends														
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 2");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O2 FAILURE"); return true;})},
								20f,
								true),
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 3");}),new voidFunction(() => {Debug.Log("Recurrs");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O3"); return true;})},
								3f,
								false,
								3f), //<< If this final variable is present and not 1f, this outcome repeats itself until the event ends or until its condition becomes false
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 4");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O4 test"); return false;})},
								5f,
								true)
		};

		events.Add(new Event(a,t,s,o)); //Add event to thing
	}

}
