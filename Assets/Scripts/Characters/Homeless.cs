using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeless : Character
{

    public bool sampleVariable;

    public static Event stealingFood(Character c)
	{
		voidFunction[] a = {
			() => {c.setMood(0); if (c.isSitting()) BusManager.instance.getSeat(BusManager.instance.getSeat(c.id)).Stand(c.gameObject); c.desired = new Vector2(1.8f,0f); c.moving = true; c.location = -1;}
			};
		boolFunction[] t = {
			() => {return true;}
			};

		float s = 50f;

		Outcome[] o = new Outcome[2]{

			new Outcome(new voidFunction[1]{new voidFunction(() => {
                c.setMood(-1);
            })},	
            new boolFunction[1]{new boolFunction(() => { return true;})}, 
                    7f,													
                    false),

            new Outcome(new voidFunction[1]{new voidFunction(() => {
                c.setMood(-2);
            })},	
            new boolFunction[1]{new boolFunction(() => { return true;})}, 
                    10f,													
                    false),
		};

		return new Event(a,t,s,o); //return this event
	}

}
