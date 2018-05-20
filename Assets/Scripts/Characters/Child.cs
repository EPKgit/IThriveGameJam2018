using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Character
{

    public static bool actingUp;
    public static bool walkingRight = false;


    public static Event childTantrum(Character c)
	{
		voidFunction[] a = {
			() => {c.setMood(-1); if (c.isSitting()) BusManager.instance.getSeat(BusManager.instance.getSeat(c.id)).Stand(c.gameObject); c.desired = new Vector2(-5f,0f); c.moving = true; c.location = -1;}
			};
		boolFunction[] t = {
			() => {return true;}//Trigger conditions // probably not standing
			};

		float s = 20f;//Change to 120fSDFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF

		Outcome[] o = new Outcome[4]{

			new Outcome(new voidFunction[1]{new voidFunction(() => {
                c.setMood(-2);
                walkingRight = true;
                c.desired = new Vector2(2.5f,0);
                c.moving = true;

            })},	//actions triggered BY outcome
            new boolFunction[1]{new boolFunction(() => { return !walkingRight && c.transform.position.x < -4.5f;/*true if standing*/ })}, //conditions that trigger outcome (only triggers if all are true)
                    0f,	// how long since the event started to check for the outcome completion													
                    false, 
                    2f),	// Is this the final event? true means the event ends	

            new Outcome(new voidFunction[1]{new voidFunction(() => {
                c.setMood(-1);
                walkingRight = false;
                c.desired = new Vector2(-5f,0);
                c.moving = true;

            })},	//actions triggered BY outcome
            new boolFunction[1]{new boolFunction(() => { return walkingRight && c.transform.position.x > 2f;/*true if standing*/ })}, //conditions that trigger outcome (only triggers if all are true)
                    0f,	// how long since the event started to check for the outcome completion													
                    false, 
                    2f),

            new Outcome(new voidFunction[1]{new voidFunction(() => {
                c.setMood(-2);
                walkingRight = true;
                c.desired = new Vector2(2.5f,0);
                c.moving = true;

            })},	//actions triggered BY outcome
            new boolFunction[1]{new boolFunction(() => { return !walkingRight && c.transform.position.x < -4.5f;/*true if standing*/ })}, //conditions that trigger outcome (only triggers if all are true)
                    0f,	// how long since the event started to check for the outcome completion													
                    false, 
                    2f),	// Is this the final event? true means the event ends	

            new Outcome(new voidFunction[1]{new voidFunction(() => {
                Lady.consoleChild = true;
                c.desired = new Vector2(-5f,0);
                c.moving = true;

            })},	//actions triggered BY outcome
            new boolFunction[1]{new boolFunction(() => { return true;})}, //conditions that trigger outcome (only triggers if all are true)
                    20f,	// how long since the event started to check for the outcome completion													
                    true)
		};

		return new Event(a,t,s,o); //return this event
	}

}
