using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	public static T instance;

	public void EnforceSingleton () 
	{
		if(instance != null)
			Destroy(this);
		instance = (T)this;
	}
}
