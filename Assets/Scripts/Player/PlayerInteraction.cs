using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : Singleton<PlayerInteraction>
{

	public List<Character> chars;
	public float maxInteractionDistance;

	public delegate void InteractionEvent(GameObject g);
	public InteractionEvent sit;
	public InteractionEvent move;
	public InteractionEvent talk;

	void Start () 
	{
		base.EnforceSingleton();
		sit = nocrash;
		move = nocrash;
		talk = AttemptTalk;
		chars = new List<Character>();
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
			sit(this.gameObject);
		if(Input.GetAxis("Horizontal") != 0)
			move(this.gameObject);
		if(Input.GetKeyDown(KeyCode.Space))
			talk(this.gameObject);
	}

	void nocrash(GameObject g)
	{

	}

	void AttemptTalk(GameObject g)
	{
		float mindist = float.MaxValue;
		Character min = null;
		foreach(Character c in chars)
		{
			if(Vector2.Distance(c.gameObject.transform.position, transform.position) < mindist && c != GetComponent<Character>())
			{
				mindist = Vector2.Distance(c.gameObject.transform.position, transform.position);
				min = c;
			}
		}
		if(min != null && mindist < maxInteractionDistance)
			min.Talk(GetComponent<Character>());
	}
}
