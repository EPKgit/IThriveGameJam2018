using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
	public float speed;

	private Rigidbody2D rb;

	void Start () 
	{
		base.EnforceSingleton();
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * speed, 0);
		//rb.AddForce(new Vector2(moveHorizontal * speed, 0));
	}
}