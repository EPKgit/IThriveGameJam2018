using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sittable : MonoBehaviour {

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}
	private bool sitting;
	
	public bool Sitting
	{
		get
		{
			return sitting;
		}

		set
		{
			sitting = value;
			animator.SetBool("IsSitting", value);
		}
	}
}
