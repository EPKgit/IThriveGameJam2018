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

	IEnumerator Start () 
	{
		if(interactionDisplay == null)
			interactionDisplay = this.transform.GetChild(0).gameObject;
		InteractionHide();
		yield return new WaitUntil( () => PlayerInteraction.instance != null);
		PlayerInteraction.instance.interact += AttemptSit;
	}
	
	void InteractionDisplay()
	{
		interactionDisplay.SetActive(true);
	}

	void InteractionHide()
	{
		interactionDisplay.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag.CompareTo("Player") == 0 && !occupied)
			InteractionDisplay();
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag.CompareTo("Player") == 0 )
			InteractionHide();
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
