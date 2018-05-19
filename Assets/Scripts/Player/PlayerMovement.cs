using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
	public float speed;

	private Rigidbody2D rb;
	private Animator animator;

	void Start () 
	{
		base.EnforceSingleton();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	void Update ()
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * speed, 0);
		animator.SetFloat("XVelocity", moveHorizontal);
		//rb.AddForce(new Vector2(moveHorizontal * speed, 0));
	}
}