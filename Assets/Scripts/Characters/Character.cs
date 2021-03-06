﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
{
    public bool isFemale;

    public bool isPlayer;
    public string id;
    /*[HideInInspector]*/public int mood;
    [HideInInspector]public int location;
    public float speed;
    public int startingSeat;

    private Rigidbody2D rb;
    private Animator animator;
    [HideInInspector]public SpriteRenderer sprite;
    [SerializeField]public bool moving;
    [SerializeField]private bool coolingDown;

    public Character lastTalked;
    public float timeTalked;
   
    /*[HideInInspector]*/public Vector2 desired;

	IEnumerator Start () 
    { 
        yield return new WaitUntil( () => BusManager.instance != null && PlayerInteraction.instance != null && AudioManager.instance != null);
        PlayerInteraction.instance.chars.Add(id,this);
        
        mood = 0;
        
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        coolingDown = false;
        moving = false;
        
        location = -1;//startingSeat;
        moveSeat(startingSeat);
    }

    public void setMood(int newMood)
    {
        mood = newMood;
        switch (mood)
        {
            case -3:
                mood = -2;
                break;
            case -2:
                break;
            case -1:
                break;
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                mood = 2;
                break;
            default:
                break;
        }
        AudioManager.instance.playEmotionSound(mood, isFemale);
        BusManager.instance.CreateEmotionBubble(mood, this.gameObject);
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
                moving = false;
                animator.SetFloat("XVelocity", 0);
                Seat s = BusManager.instance.getSeat(location);
                if(s != null)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = desired;
                    s.AttemptSit(this.gameObject);
                    if(!GetComponent<Sittable>().sitting)
                    {
                        location = -1;
                        moving = true;
                        desired.x += (Random.Range(0, 1) == 0) ? -2 : 2;
                    }
                }
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
        timeTalked = EventManager.instance.gameTime;
    }

    IEnumerator StartCooldown(float f)
    {
        coolingDown = true;
        if(f == 0) f = 1f;
        yield return new WaitForSeconds(f);
        coolingDown = false;
    }

    public bool isSitting()
    {
        return BusManager.instance.getSeat(id) != -1;
    }

    public void moveSeat(int seat) 
    {
        if (BusManager.instance.getSeat(id) != seat) 
        {
            if (isSitting())
                BusManager.instance.getSeat(BusManager.instance.getSeat(id)).Stand(gameObject);
            moving = true;
            if (seat != -1 && BusManager.instance.getSeat(seat).occupied)
            {
                seat = -1;
                desired = (BusManager.instance.seatLocation(seat) + (Vector2)rb.position)/2;       
            }
            else if (seat == -1)
            {
                desired = (Vector2)rb.position;
            }
            else
                desired = BusManager.instance.seatLocation(seat);
            location = seat;
        }
    }
}
