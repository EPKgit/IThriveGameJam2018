using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sittable : MonoBehaviour 
{

	public bool sitting;
	public float sitHeight;

	private Animator animator;
	private BoxCollider2D col;
	private bool coolingDown;
	private float normalHeight;

	void Start()
	{
		sitting = false;
		coolingDown = false;
		animator = GetComponent<Animator>();
		col = GetComponent<BoxCollider2D>();

		normalHeight = col.size.y;
	}

	

	public bool SetSitting(bool value, Seat s)
	{
		if(value)
			return Sit(s);
		else
			return Stand();
	}

	bool Sit(Seat s)
	{
		if(GetComponent<PlayerMovement>() != null)
		{
			coolingDown = true;
			if(PlayerMovement.instance != null) PlayerMovement.instance.CanMove = false;
			StartCoroutine(SitCooldown());
		}
		sitting = true;
		col.size = new Vector2(col.size.x, sitHeight);
		Vector3 pos = gameObject.transform.position;
		this.gameObject.transform.position = new Vector3(s.transform.position.x, pos.y, pos.z);
		animator.SetBool("IsSitting", true);
		animator.SetFloat("XVelocity", 0);
		GetComponent<SpriteRenderer>().sortingOrder = 20;
		return true;
	}

	bool Stand()
	{

		if(coolingDown) return false;
		sitting = false;
		col.size = new Vector2(col.size.x, normalHeight);
		animator.SetBool("IsSitting", false);
		GetComponent<SpriteRenderer>().sortingOrder = 30;
		return true;
	}

	IEnumerator SitCooldown()
	{
		yield return new WaitForSeconds(1f);
		PlayerMovement.instance.CanMove = true;
		coolingDown = false;
	}
}
