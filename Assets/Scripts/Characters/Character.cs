using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int mood;
    public string id;
    public int location;
    public int speed;
    public BusManager busManager;
    private Rigidbody2D rb;
    private Animator animator;
    private bool sliding;
    public Vector2 desired;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitUntil( () => BusManager.instance != null);
        busManager = BusManager.instance;
        desired = busManager.seatLocation(2);
        mood = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sliding = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (sliding == true) {
            slide(desired);
        }
		
	}

    int getMood () {
        return mood;
    }

    int getLocation()
    {
        return location; 
    }

    

    void moveSeat(int seat) {
        if (location != seat) {
            while (rb.position!=desired)
            {
                if (!busManager.seatOccupied(seat))
                {
                    location = seat;
                    desired= busManager.seatLocation(seat);
                    sliding =true;
                
                }
            }
            sliding = false;
            

        }
    }
    void slide(Vector2 seat)
    {
        
       
            if (seat.x > rb.position.x)
            {
                rb.velocity=(Vector2.right * speed * 0);
            }
        if (seat.x < rb.position.x)
        {
                rb.velocity = (Vector2.left * speed * 0);
            }
        

    }
}
