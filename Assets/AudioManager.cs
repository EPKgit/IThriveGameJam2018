using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager> 
{
	private float mvolume;
	private float svolume;

	public AudioClip MainMenuMusic;
	public AudioClip EngineNoise;

	public AudioClip[] maleSounds;
    public AudioClip[] femaleSounds;

	public AudioSource source;

	void Start () 
	{
		base.EnforceSingleton();
		DontDestroyOnLoad(gameObject);
		mvolume = 1;
		svolume = 1;	
		source.clip = MainMenuMusic;
		source.Play();
	}

	public void GameStarted()
	{
		StartCoroutine(SwitchMusic());
	}

	IEnumerator SwitchMusic()
	{
		source.Stop();
		yield return new WaitForSecondsRealtime(2f);
		source.clip = EngineNoise;
		source.Play();
		//source.pitch = 2;
		source.volume = svolume;
		source.loop = true;
	}

	public void changeMusicVolume(Slider s)
	{
		Debug.Log(mvolume);
		mvolume = s.value;
		source.volume = mvolume;
	}

	public void changeSoundVolume(Slider s)
	{
		svolume = s.value;
	}

	public float getSoundVolume()
	{
		return svolume;
	}

	public void playEmotionSound(int mood, bool isFemale)
	{
		if(isFemale)
            AudioManager.instance.source.PlayOneShot(femaleSounds[mood + 2], AudioManager.instance.getSoundVolume());
        else
            AudioManager.instance.source.PlayOneShot(maleSounds[mood + 2], AudioManager.instance.getSoundVolume());
	}
	
}
