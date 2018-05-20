using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> 
{
	private float mvolume;
	public float s_volume;

	public float m_volume
	{
		get
		{
			return mvolume;
		}

		set
		{
			mvolume = value;
		}
	}

	void Start () 
	{
		mvolume = 1;
		s_volume = 1;	
	}
	
}
