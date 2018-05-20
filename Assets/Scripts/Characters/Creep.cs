using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : Character
{

    public bool sampleVariable;
    public static bool IsAnnoying = false;


    public static Event creepRoam(Character c)
	{
		voidFunction[] a = {
			() => {c.setMood(0); if (c.isSitting()) BusManager.instance.getSeat(BusManager.instance.getSeat(c.id)).Stand(c.gameObject);}
			};
		boolFunction[] t = {
			() => {return true;}//Trigger conditions // probably not standing
			};

		float s = 10f;//Change to 120fSDFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF

		Outcome[] o = new Outcome[5]{
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

                c.moveSeat(selection);

            })},	//actions triggered BY outcome
						new boolFunction[1]{new boolFunction(() => { return !c.isSitting();/*true if standing*/ })}, //conditions that trigger outcome (only triggers if all are true)
								0f,	// how long since the event started to check for the outcome completion													
								false, 
                                6f),	// Is this the final event? true means the event ends														
			new Outcome(new voidFunction[1]{new voidFunction(() => 
            {
                IsAnnoying = false;
                //Debug.Log("Annoyed Someone Maybe");
                int x=0;
                foreach(Character ch in PlayerInteraction.instance.chars.Values){
                    if(ch.id != "Creep" && Vector2.Distance((Vector2)ch.transform.position,(Vector2)c.transform.position) <= 1f && ch.isSitting()){
                        Debug.Log("Ew at: " + ch.id);
                        ch.setMood(ch.mood - 1);
                        x++;
                    }
                }
                if (x>0) c.setMood(c.mood + 1);
            ;})},
						new boolFunction[1]{
                            new boolFunction(() => {return IsAnnoying;                                                })},
								0f,
								false, 
                                2f),
			new Outcome(new voidFunction[1]{new voidFunction(() => { IsAnnoying = true;                   })},
						new boolFunction[1]{new boolFunction(() => { return c.isSitting(); })},
								0f,
								false,
								10f),
            new Outcome(new voidFunction[1]{new voidFunction(() => { c.setMood(c.mood-1); BusManager.instance.getSeat(BusManager.instance.getSeat(c.id)).Stand(c.gameObject);})},
						new boolFunction[1]{new boolFunction(() => { return c.lastTalked == PlayerInteraction.instance.chars["Player"] && Mathf.Abs(EventManager.instance.gameTime - c.timeTalked) < 5.0f && c.isSitting(); })},
								0f,
								false,
                                4f),
            new Outcome(new voidFunction[1]{new voidFunction(() => {

                foreach(Character ch in PlayerInteraction.instance.chars.Values){
                    if(ch.id != "Creep"){
                        
                        ch.setMood(ch.mood + 1);
                    }
                }
                Debug.Log("Creepdone");
                c.moveSeat(3);
             })},
            new boolFunction[1]{new boolFunction(() => { return c.mood == -2;})},
                    0f,
                    true)
		};

		return new Event(a,t,s,o); //return this event
	}
}
