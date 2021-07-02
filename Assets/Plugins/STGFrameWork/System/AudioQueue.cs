using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour {
    static List<AudioSource> playingNow = new List<AudioSource>();

    // Use this for initialization
    static public void Play(AudioSource target)
    {
        if (target == null)
            return;
        if (playingNow.Count == 0)
        {
            playingNow.Add(target);
            target.Play();
        }
		bool isGoing = false;
		List<AudioSource> removeList = new List<AudioSource>();
		foreach (var b in playingNow)
		{
			if (b == null)
				removeList.Add(b);
		}
		foreach (var c in removeList) {
			playingNow.Remove(c);
		
		}
		foreach (var b in playingNow) {
			if (b.clip == target.clip && b.time/b.clip.length <0.1f)
				isGoing = false;
			else
				isGoing = true;
		}

		if (isGoing) {
			if (target.enabled == true)
			{
				playingNow.Add(target);
				target.Play();
			}
		}
			
    }
	
	
	// Update is called once per frame
	void Update () {
		List<AudioSource> removeList = new List<AudioSource>();
		foreach (var a in playingNow) {
			if (a == null ||a.isPlaying == false)
				removeList.Add (a);
		
		}
		foreach (var c in removeList)
		{
			playingNow.Remove(c);

		}
	}
}
