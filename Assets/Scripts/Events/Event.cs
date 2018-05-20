using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : Outcome
{
	public Outcome[] outcomes; 
	

	public Event(voidFunction[] a, boolFunction[] t, float s, Outcome[] o, float deltaS = 1f) : base(a, t, s, true, deltaS)
	{
		outcomes = o; // it acts like an outcome- is triggered, causes things, can end
						// except it has a list of outcomes it iterates through like the event manager 


	}

	public bool checkOutcome(float time) 
	{
		// similar to event manager... keeps checking outcomes until one of them causes an end
		foreach (Outcome o in outcomes)
		{
			if (o.checkTrigger(time - callTime))
				return true; // ends the event
		}
		return false;
	}
}
