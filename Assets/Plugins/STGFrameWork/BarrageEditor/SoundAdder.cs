using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using Sirenix.OdinInspector;
public class SoundAdder : MonoBehaviour {
    public AudioClip[] tar;
    public AudioMixerGroup tarGroup;
    [Button]
	// Use this for initialization
	public void AddSoundRightNow () {
        Shooting[] list = GetComponentsInChildren<Shooting> ();
        foreach (Shooting a in list) {
            if (a.PlayerSound)
                continue;

 
            AudioSource r = a.gameObject.AddComponent<AudioSource> ();
            r.clip = tar[Random.Range(0,tar.Length -1)];
            r.outputAudioMixerGroup = tarGroup;
            r.playOnAwake = false;
            a.PlayerSound = true;
            a.Sound = new AudioSource[]{r};
          
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
