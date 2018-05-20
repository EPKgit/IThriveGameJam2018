using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lady : Character
{

    public bool sampleVariable;

    public static bool consoleChild = false;
    
    public static Event childTantrum(Character c)
	{
		voidFunction[] a = {
			() => {c.setMood(-1); if (c.isSitting()) BusManager.instance.getSeat(BusManager.instance.getSeat(c.id)).Stand(c.gameObject); c.desired = new Vector2(-4f,0f); c.moving = true; c.location = -1;}
			};
		boolFunction[] t = {
			() => {return consoleChild;}
			};

		float s = 10f;

		Outcome[] o = new Outcome[1]{

			new Outcome(new voidFunction[1]{new voidFunction(() => {
                c.moveSeat(7);
                PlayerInteraction.instance.chars["Child"].moveSeat(8);
                c.setMood(0);
                PlayerInteraction.instance.chars["Child"].setMood(0);
            })},	
            new boolFunction[1]{new boolFunction(() => { return true;})}, 
                    40f,													
                    true)
		};

		return new Event(a,t,s,o); //return this event
	}
}
