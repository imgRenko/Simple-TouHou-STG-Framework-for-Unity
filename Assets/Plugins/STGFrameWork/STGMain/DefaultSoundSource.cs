using System.Collections.Generic;
using UnityEngine;

public class DefaultSoundSource : MonoBehaviour {
	public List<AudioSource> soundClip = new List<AudioSource>();
	public List<string> key = new List<string>();
	void Start () {
		for (int i = 0; i != soundClip.Count; ++i)
			SoundSystem.AddSoundintoSoundLibrary (soundClip [i], key [i]);
	}
}
