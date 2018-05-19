using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusManager : Singleton<BusManager>
{
	public GameObject[] gseats;

	private Seat[] seats;
	private int size;

	void Start () 
	{
		base.EnforceSingleton();
		size = gseats.Length;
		seats = new Seat[size];
		for(int x = 0; x < size; ++x)
			if( (seats[x] = gseats[x].GetComponent<Seat>()) == null )
				Debug.Log("FATAL ERROR: SEAT NOT ATTACHED TO GAMEOBJECT" + gseats[x].name);
	}

	Vector2 seatLocation(int index)
	{
		if(index < 0 || index >= size)
			return new Vector2(0,0);
		return gseats[index].transform.position;
	}

	bool seatOccupied(int index)
	{
		if(index >= 0 && index < size)
			return seats[index].occupied;
		return false;
	}
	
	
}
