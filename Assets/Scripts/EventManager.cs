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
	private Dictionary<string,Character> characters;

	IEnumerator Start () 
	{
		yield return new WaitUntil(() => PlayerInteraction.instance != null && PlayerInteraction.instance.chars.Count >= 5);
		
		base.EnforceSingleton();
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
		//EXAMPLE_EVENT();
		events.Add(Homeless.stealingFood(characters["Homeless"]));
		events.Add(Lady.childTantrum(characters["Lady"]));
		events.Add(Child.childTantrum(characters["Child"]));
		events.Add(Creep.creepRoam(characters["Creep"]));
		return;
	}

	private void EXAMPLE_EVENT()
	{
		voidFunction[] a = {
			() => {return;}, //Actions when triggered
			() => {BusManager.instance.getSeat(BusManager.instance.getSeat(characters["Creep"].id)).Stand(characters["Creep"].gameObject);}
			};
		boolFunction[] t = {
			() => {characters["Creep"].setMood(0);return characters["Creep"].isSitting();}//Trigger conditions // probably not standing
			};

		float s = 1f;//When to start the whole event
        float annoyanceTimer = 0f;
        bool IsAnnoying = false;
		Outcome[] o = new Outcome[3]{
			new Outcome(new voidFunction[1]{new voidFunction(() => {
                List<int> openSeats = new List<int>();
                for (int i = 0; i < 8; ++i) {
                    if (!BusManager.instance.seatOccupied(i))
                    {
                        openSeats.Add(i);
                    }
                }
                System.Random rng = new System.Random();
                int selection = openSeats[(int)(rng.NextDouble() * openSeats.Count)];/*select a random open seat and move to*/
                Debug.Log("Moving Creep to Seat #" + selection);
                //IsAnnoying = true;
                characters["Creep"].moveSeat(selection);
                //annoyanceTimer = gameTime;
            })},	//actions triggered BY outcome
						new boolFunction[1]{new boolFunction(() => { return !characters["Creep"].isSitting();/*true if standing*/ })}, //conditions that trigger outcome (only triggers if all are true)
								10f,	// how long since the event started to check for the outcome completion													
								false, 
                                15f),	// Is this the final event? true means the event ends														
			new Outcome(new voidFunction[1]{new voidFunction(() => {Debug.Log("Annoyed Someone Maybe");})},
						new boolFunction[1]{
                            new boolFunction(() => {return IsAnnoying && gameTime >= 10f + annoyanceTimer;                                                })},
								20f,
								false, 
                                5f),
			new Outcome(new voidFunction[1]{new voidFunction(() => { IsAnnoying = true; annoyanceTimer = gameTime;                                                             })},
						new boolFunction[1]{new boolFunction(() => { return BusManager.instance.getSeat("Creep") != -1; })},
								3f,
								false,
								10f) //<< If this final variable is present and not 1f, this outcome repeats itself until the event ends or until its condition becomes false
			/*new Outcome(new voidFunction[2]{new voidFunction(() => {Debug.Log("Outcome 4");}),new voidFunction(() => {Debug.Log("FAILURE");})},
						new boolFunction[1]{new boolFunction(() => {Debug.Log("O4 test"); return false;})},
								5f,
								true)*/
		};

		events.Add(new Event(a,t,s,o)); //Add event to thing
	}
}
