using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sittable : MonoBehaviour 
{

	public bool sitting;

	private Animator animator;
	private bool coolingDown;

	void Start()
	{
		sitting = false;
		coolingDown = false;
		animator = GetComponent<Animator>();
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
		coolingDown = true;
		PlayerMovement.instance.CanMove = false;
		StartCoroutine(SitCooldown());
		sitting = true;
		this.gameObject.transform.position = s.transform.position;
		animator.SetBool("IsSitting", true);
		animator.SetFloat("XVelocity", 0);
		return true;
	}

	bool Stand()
	{

		if(coolingDown) return false;
		sitting = false;
		animator.SetBool("IsSitting", false);
		return true;
	}

	IEnumerator SitCooldown()
	{
		yield return new WaitForSeconds(1f);
		PlayerMovement.instance.CanMove = true;
		coolingDown = false;
	}
}
