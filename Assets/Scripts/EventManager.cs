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
	//public Queue<Event> events;

	void Start () 
	{
		characters = new List<Character>(GetComponents<Character>()); 
		events = new List<Event>();
		activeEvents = new List<Event>();
		removeEvents = new List<Event>();

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
	

	private bool populateEvents()
	{
		voidFunction[] a = {
			() => {Debug.Log("E1 Triggered");}, //Actions when triggered
			() => {Debug.Log("E1 Triggered pt2");}
			};
		boolFunction[] t = {
			() => {Debug.Log("E1 Check"); return true;}//Trigger conditions
			};

		float s = 10f;//When to start

		bool e = true;// ??

		Outcome[] o = new Outcome[4]{
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 1");}),new voidFunction(() => {Debug.Log("Outcome 1 pt2");})},	//a
						new boolFunction[1]{new boolFunction(() => {Debug.Log("Check Outcome 1"); return true;})},			//t
								10f,															//s
								true),															//e
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 2");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O2 FAILURE"); return true;})},
								20f,
								true),
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 3");}),new voidFunction(() => {Debug.Log("Recurrs");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O3"); return true;})},
								3f,
								false,
								3f), //deltaS
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 4");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O4 test"); return false;})},
								5f,
								true)
		};

		events.Add(new Event(a,t,s,e,o));


		a = new voidFunction[1]{
			() => {Debug.Log("E2 Triggered- Failure");}
		};
		t = new boolFunction[1]{
			() => {Debug.Log("E2 Check"); return false;}
		};

		s = 2f;

		e = true;

		o = new Outcome[4]{
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 1");}),new voidFunction(() => {Debug.Log("Outcome 1 pt2");})},	//a
						new boolFunction[1]{new boolFunction(() => {Debug.Log("Check Outcome 1"); return true;})},			//t
								10f,															//s
								true),															//e
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 2");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O2 FAILURE"); return true;})},
								20f,
								true),
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 3");}),new voidFunction(() => {Debug.Log("Recurrs");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O3"); return true;})},
								3f,
								false),
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 4");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O4 test"); return false;})},
								5f,
								true)
		};

		events.Add(new Event(a,t,s,e,o));


		a = new voidFunction[1]{
			() => {Debug.Log("E3 Triggered");}
		};
		t = new boolFunction[1]{
			() => {Debug.Log("E3 Check"); return true;}
		};

		s = 5f;

		e = true;

		o = new Outcome[4]{
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Event 3 Outcome should not have happened");}),new voidFunction(() => {Debug.Log("Failure");})},	//a
						new boolFunction[1]{new boolFunction(() => {Debug.Log("Check Outcome 1"); return true;})},			//t
								2f,															//s
								true),															//e
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 2");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O2 FAILURE"); return true;})},
								20f,
								true),
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Event 3");}),new voidFunction(() => {Debug.Log("E3 over");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O3"); return true;})},
								1f,
								true),
			new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 4");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O4 test"); return false;})},
								5f,
								true)
		};

		events.Add(new Event(a,t,s,e,o));

		return true;
	}

}
