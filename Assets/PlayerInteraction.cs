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
		interact = Debug;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
			interact(this.gameObject);
		if(Input.GetAxis("Horizontal") != 0)
			move(this.gameObject);
	}

	void Debug(GameObject g)
	{

	}
}
