using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sittable : MonoBehaviour 
{

	public bool sitting;

	private Animator animator;

	void Start()
	{
		sitting = false;
		animator = GetComponent<Animator>();
	}

	

	public void SetSitting(bool value, Seat s)
	{
		sitting = value;
		this.gameObject.transform.position = s.transform.position;
		animator.SetBool("IsSitting", value);
	}
}
