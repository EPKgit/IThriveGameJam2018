using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcome  {
/*
	public Func<>[] actions;
	public Func<bool>[] triggers;
	public float startTime;
	private float callTime;
	private bool started;
	private bool endEvent;

	Outcome(Func<>[] a, Func<bool>[] t, bool e)
	{
		actions = a;// Example: { Character c = eM.getCharacter(Person); if(c.Condition()) c.Move();} 

		triggers = new Func<bool>[t.Length + 1];

		triggers[0] = {return !started;}; // This event can't run if it's already 
		for (int i = 0; i < t.Length; i++)
		{
			triggers[i+1] = t[i];
		}

		endEvent = e; // Does this outcome end the event
	}

	public bool checkTrigger(float time)
	{
		if (time < startTime)
			return false; // Only trigger if enough time has passed

		foreach (Func<bool> f in triggers)
		{
			if (!f()) // Iterate through triggers, if any of them haven't happened 
				return false;
		}
		callTime = time;
		return triggered();
	}

	private bool triggered()
	{
		started = true; // won't be triggered again.


		foreach (Func<> a in actions)
		{
			a(); // Results of actions. Can affect character moods or cause them to do things
		}
		return e; // if true, ends the event. if false, nothing changes.
	}
*/
}
