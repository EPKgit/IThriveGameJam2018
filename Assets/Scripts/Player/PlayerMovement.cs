using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
	public float speed;

	private Rigidbody2D rb;
	private Animator animator;
	private SpriteRenderer sprite;

	private bool canMove;
	public bool CanMove
	{
		get
		{
			return canMove;
		}

		set
		{
			canMove = value;
			rb.velocity = Vector2.zero;
		}
	}

	void Start () 
	{
		canMove = true;
		base.EnforceSingleton();
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}
	
	void Update ()
	{
		if(!canMove) return;
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * speed, 0);
		animator.SetFloat("XVelocity", moveHorizontal);
		if(moveHorizontal == -1)
			sprite.flipX = false;
		else
			sprite.flipX = true;
		//rb.AddForce(new Vector2(moveHorizontal * speed, 0));
	}
}