using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int mood;
    public string id;
    public int location;
    public float speed;
    public BusManager busManager;
    private Rigidbody2D rb;
    private Animator animator;
    private bool sliding;
    public bool Sliding{
        get
        {
            return sliding;
        }

        set
        {
            sliding = value;
            rb.velocity = Vector2.zero;
        }
    }
    public Vector2 desired;
    public Sittable sit;
	// Use this for initialization
	IEnumerator Start () {
       
        
        yield return new WaitUntil( () => BusManager.instance != null);
        busManager = BusManager.instance;
        location = busManager.getSeat(id);
        mood = 0;
        sit = GetComponent<Sittable>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sliding = false;
       // moveSeat(2);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (sliding == true) {
            if (Vector2.Distance(rb.position,desired)==float.Epsilon)
            {
                sliding = false;
                rb.velocity = Vector2.zero;
                rb.position = desired;
                

            }
            else
            {
                if (desired.x > rb.position.x)
                {
                    rb.velocity = new Vector2(.5f * speed, 0);
                }
                if (desired.x < rb.position.x)
                {
                    rb.velocity = new Vector2(-.5f * speed, 0);
                }
            }
        }
      




    }

    int getMood () {
        return mood;
    }

    int getLocation()
    {
        return busManager.getSeat(id); 
    }

    

    void moveSeat(int seat) {
     //   Debug.Log("in");
        
        if (location != seat) {
            
            //Debug.Log("location is not seat");
            desired = busManager.seatLocation(seat);
            //Debug.Log(desired);
                
            
                //Debug.Log("position != desired");
                
                if (!busManager.seatOccupied(seat))
                {
                    location = seat;
                Debug.Log(location); 
                   
                    sliding =true;
                }    
            }
            }
        
   
}
