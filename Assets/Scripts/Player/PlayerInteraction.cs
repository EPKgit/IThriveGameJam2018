using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : Singleton<PlayerInteraction>
{

	public delegate void InteractionEvent(GameObject g);
	public InteractionEvent interact;
	public InteractionEvent move;

	void Start () 
	{
		base.EnforceSingleton();
		interact = nocrash;
		move = nocrash;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
			interact(this.gameObject);
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
