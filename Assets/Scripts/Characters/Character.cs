using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
{
    public bool isPlayer;
    public string id;
    [HideInInspector]public int mood;
    [HideInInspector]public int location;
    public float speed;
    public int startingSeat;

    private Rigidbody2D rb;
    private Animator animator;
    [HideInInspector]public SpriteRenderer sprite;
    [SerializeField]private bool moving;
    [SerializeField]private bool coolingDown;

    public Character lastTalked;
   
    [HideInInspector]public Vector2 desired;

	IEnumerator Start () 
    { 
        yield return new WaitUntil( () => BusManager.instance != null && PlayerInteraction.instance != null);

        PlayerInteraction.instance.chars.Add(this);
        
        mood = 0;
        
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        coolingDown = false;
        moving = false;
        
        location = -1;//startingSeat;
        moveSeat(startingSeat);
    }

   
   public float dist;
    void FixedUpdate () {
        if(isPlayer) return;
        //Debug.Log(rb.velocity);
        //Debug.Log(Mathf.Abs(desired.x - rb.position.x));
        if (moving) 
        {
            dist = Mathf.Abs(desired.x-rb.position.x);
            if (Mathf.Abs(desired.x-rb.position.x)<=0.55)
            {
                //Debug.Log("in");
                moving = false;
                rb.velocity = Vector2.zero;
                transform.position = desired;
                Seat s = BusManager.instance.getSeat(location);
                s.AttemptSit(this.gameObject);
            }
            else
            {
                float xVel = (desired.x > transform.position.x) ? 1 : -1;
                rb.velocity = new Vector2(xVel * speed, 0);
                animator.SetFloat("XVelocity", xVel);
                sprite.flipX = (xVel == -1);
            }
        }
    }

    public int getMood () {
        return mood;
    }
   
    public int getLocation()
    {
        return BusManager.instance.getSeat(id); 
    }

    
    public void Talk(Character c) 
    {
        if(coolingDown) { Debug.Log("FAILED TALK"); return; }
        StartCoroutine(StartCooldown(0));
        lastTalked = c;
    }

    IEnumerator StartCooldown(float f)
    {
        coolingDown = true;
        if(f == 0) f = 1f;
        yield return new WaitForSeconds(f);
        coolingDown = false;
    }


    public void moveSeat(int seat) 
    {
        if (location != seat) 
        {
            moving = true;
            desired = BusManager.instance.seatLocation(seat);
            location = seat;
        }
    }
}
