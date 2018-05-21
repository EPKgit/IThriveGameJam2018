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
	public AudioClip CreditsMusic;

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
		StartCoroutine(SwitchMusic1());
	}

	IEnumerator SwitchMusic1()
	{
		source.Stop();
		yield return new WaitUntil( () => BackgroundManager.instance != null && BackgroundManager.instance.loading == false);
		source.clip = EngineNoise;
		source.volume = svolume;
		source.loop = true;
		source.Play();
	}

	public void GameEnded()
	{
		StartCoroutine(SwitchMusic2());
	}

	IEnumerator SwitchMusic2()
	{
		source.Stop();
		yield return new WaitForSecondsRealtime(2f);
		source.clip = CreditsMusic;
		source.volume = mvolume;
		source.loop = true;
		source.Play();
	}

	public void changeMusicVolume(Slider s)
	{
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
            AudioManager.instance.source.PlayOneShot(femaleSounds[mood + 2], svolume * 0.3f);
        else
            AudioManager.instance.source.PlayOneShot(maleSounds[mood + 2], svolume * 0.3f);
	}
	
}
