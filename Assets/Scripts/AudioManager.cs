using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip[]sounds;

	AudioListener audioListener;
	public bool canPlayAudio=true;

	void Start()
	{
		audioListener = GetComponent<AudioListener> ();
		GetComponent<AudioSource> ().enabled = true;
	}

	void Update()
	{
		if (!canPlayAudio)
		{
			GetComponent<AudioSource> ().enabled = false;
		}
		else
		{
			GetComponent<AudioSource> ().enabled = true;
		}
	}

	public void PlayAudioClip(int clipsIndex,Transform transform)                // method to play a specific audio clip from an array
	{
        if (clipsIndex != -1 && canPlayAudio)                                                            
		{
			AudioSource audio = transform.GetComponent<AudioSource>();
			audio.clip = sounds[clipsIndex];
			//if (audio.isPlaying == false )
			//{
			   audio.Play();                                                  // playing specific audio
			//}
		}
	}
}
