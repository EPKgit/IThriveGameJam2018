﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void voidFunction();
public delegate bool boolFunction();

public class Outcome  {

	public voidFunction[] actions;
	public boolFunction[] triggers;
	public float startTime;
	protected float callTime;
	protected bool started;
	protected bool endEvent;

	public Outcome(voidFunction[] a, boolFunction[] t, bool e)
	{
		actions = a;// Example: { Character c = eM.getCharacter(Person); if(c.Condition()) c.Move();} 

		triggers = new boolFunction[t.Length + 1];

		triggers[0] = () => {return !started;}; // This event can't run if it's already 
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

		foreach (boolFunction f in triggers)
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


		foreach (voidFunction a in actions)
		{
			a(); // Results of actions. Can affect character moods or cause them to do things
		}
		return endEvent; // if true, ends the event. if false, nothing changes.
	}
}
