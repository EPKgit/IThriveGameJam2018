using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
	public float maxInteractionDistance;
	public GameObject occupant;
	public bool occupied { get { return occupant != null; } }
	public GameObject interactionDisplay;

	private GameObject player;

	IEnumerator Start () 
	{
		if(interactionDisplay == null)
			interactionDisplay = this.transform.GetChild(0).gameObject;
		InteractionHide();
		yield return new WaitUntil( () => PlayerInteraction.instance != null);
		PlayerInteraction.instance.sit += AttemptSit;
		PlayerInteraction.instance.move += Stand;
		player = PlayerInteraction.instance.gameObject;
	}

	void Update ()
	{
		if(player == null) return;
		if(Vector2.Distance(player.transform.position, this.transform.position) < maxInteractionDistance)
			InteractionDisplay();
		else
			InteractionHide();
	}
	
	void InteractionDisplay()
	{
		interactionDisplay.SetActive(true);
	}

	void InteractionHide()
	{
		interactionDisplay.SetActive(false);
	}

	public void AttemptSit(GameObject g)
	{
		if(occupied || Vector2.Distance(g.transform.position, this.transform.position) > maxInteractionDistance) return;
		Sit(g);
	}

	void Sit(GameObject g)
	{
		occupant = g;
		if(g.GetComponent<Sittable>() != null)
		{
			g.GetComponent<Sittable>().SetSitting(true, this);
		}
	}

	void Stand(GameObject g)
	{
		if(g == occupant)
		{
			if(g.GetComponent<Sittable>() != null)
			{
				if(g.GetComponent<Sittable>().SetSitting(false, this))
				{
					occupant = null;
				}
			}
		}
	}

}
