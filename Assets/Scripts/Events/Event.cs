using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : Outcome
{
	
	public Outcome[] outcomes; 
	

	Event(Func<>[] a, Func<bool>[] t, Outcome[] o) : base(a, t)
	{
		outcomes = o; // it acts like an outcome- is triggered, causes things, can end
						// except it has a list of outcomes it iterates through like the event manager =
	}

	public bool checkOutcomes(float time) 
	{
		// similar to event manager... keeps checking outcomes until one of them causes an end
		foreach (Outcome o in outcomes)
		{
			if (o.checkTriggers(time - callTime))
				return true; // ends the event
		}
		return false;
	}
}
