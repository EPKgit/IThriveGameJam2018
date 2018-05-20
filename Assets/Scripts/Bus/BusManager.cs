using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusManager : Singleton<BusManager>
{
	public GameObject[] emotionBubbles;
	public GameObject[] gseats;
	[HideInInspector]public int size;

	private Seat[] seats;

	void Start () 
	{
		base.EnforceSingleton();
		size = gseats.Length;
		seats = new Seat[size];
		for(int x = 0; x < size; ++x)
			if( (seats[x] = gseats[x].GetComponent<Seat>()) == null )
				Debug.Log("FATAL ERROR: SEAT NOT ATTACHED TO GAMEOBJECT" + gseats[x].name);
	}

	public Vector2 seatLocation(int index)
	{
		if(index < 0 || index >= size)
			return new Vector2(0,0);
		return gseats[index].transform.position;
	}

	public bool seatOccupied(int index)
	{
		if(index >= 0 && index < size)
			return seats[index].occupied;
		return false;
	}
	
	public int adjacentSeat(int index)
	{
		if(index == 0)
			return 0;
		if(index == size - 1)
			return size - 2;
		return Random.Range(0, 1) == 0 ? index - 1 : index + 1;
	}

	public Seat getSeat(int index)
	{
		if(index >= 0 && index < size)
			return seats[index];
		return null;
	}

	public int getSeat(string id)
	{
		for(int x = 0; x < size; ++x)
			if(seats[x].occupant != null && seats[x].occupant.GetComponent<Character>() != null && seats[x].occupant.GetComponent<Character>().id.CompareTo(id) == 0)
				return x;
		return -1;
	}	

	public void CreateEmotionBubble(int mood, GameObject g)
	{
		GameObject temp = Instantiate(emotionBubbles[mood + 2]);
		temp.transform.position = new Vector3(0, 2, 0);
		temp.transform.SetParent(g.transform, false);
		StartCoroutine(DeleteBubble(temp));
	}

	private IEnumerator DeleteBubble(GameObject b)
	{
		yield return new WaitForSecondsRealtime(2f);
		Destroy(b);
	}
	
}
