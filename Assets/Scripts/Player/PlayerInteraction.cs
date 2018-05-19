using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : Singleton<PlayerInteraction>
{

	public delegate void InteractionEvent(GameObject g);
	public InteractionEvent sit;
	public InteractionEvent move;
	public InteractionEvent talk;

	void Start () 
	{
		base.EnforceSingleton();
		sit = nocrash;
		move = nocrash;
		talk = nocrash;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
			sit(this.gameObject);
		if(Input.GetAxis("Horizontal") != 0)
			move(this.gameObject);
		if(Input.GetKeyDown(KeyCode.Space))
			for(int x = 0; x < BusManager.instance.size; ++x)
				Debug.Log(x + " " + BusManager.instance.seatOccupied(x));
	}

	void nocrash(GameObject g)
	{

	}
}
