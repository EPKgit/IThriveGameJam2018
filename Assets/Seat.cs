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
		PlayerInteraction.instance.interact += AttemptSit;
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

	void AttemptSit(GameObject g)
	{
		Debug.Log("attempt to sit " + g.name);
		if(occupied || Vector2.Distance(g.transform.position, this.transform.position) > maxInteractionDistance) return;
		Debug.Log("sits");
		Sit(g);
	}

	void Sit(GameObject g)
	{
		occupant = g;
	}

	void Stand()
	{
		occupant = null;
	}

}
