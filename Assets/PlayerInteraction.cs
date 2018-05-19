using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : Singleton<PlayerInteraction>
{

	public delegate void InteractionEvent(GameObject g);
	public InteractionEvent interact;

	void Start () 
	{
		base.EnforceSingleton();
		interact = Debug;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
			interact(this.gameObject);
	}

	void Debug(GameObject g)
	{

	}


}
