using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Sound system.该类主要目的取代原有游戏编辑中散乱的声音处理函数，将其集中处理到一个地方。
/// </summary>
public class SoundSystem : MonoBehaviour {
	static public Dictionary<string,AudioSource> soundLibrary = new Dictionary<string,AudioSource>();
	static public float volumeProduct = 1;
	static public AudioClip PlaySound (AudioSource target,float volume = 1,bool loop = false){
		if (target == null)
			return null;
		target.volume = volume * volumeProduct;
		target.loop = loop;
		target.Play ();
		return target.clip;
	}
	static public AudioClip PlaySound (AudioSource target,float volume = 1,float delay = 1,bool loop = false){
		if (target == null)
			return null;
		target.volume = volume * volumeProduct;
		target.loop = loop;
		target.PlayDelayed (delay);
		return target.clip;
	}
	static public AudioClip PlaySound (string key,float volume = 1,bool loop = false){
		AudioSource target = FoundSoundWithKey (key);
		if (target == null)
			return null;
		target.volume = volume * volumeProduct;
		target.loop = loop;
		target.Play ();
		return target.clip;
	}
	static public AudioClip PlaySound (string key,float volume = 1,float delay = 1,bool loop = false){
		AudioSource target = FoundSoundWithKey (key);
		if (target == null)
			return null;
		target.volume = volume * volumeProduct;
		target.loop = loop;
		target.PlayDelayed (delay);
		return target.clip;
	}
	static public void Stop(AudioSource target){
		if (target == null)
			return;
		target.Stop ();
	}
	static public void StopAll(){
		foreach (AudioSource clip in soundLibrary.Values) {
			if (clip == null)
				return;
			clip.Stop ();
		}
	}
	static public void Pause(AudioSource target){
		if (target == null)
			return;
		target.Pause ();
	}
	static public void PauseAll(){
		foreach (AudioSource clip in soundLibrary.Values) {
			if (clip == null)
				return;
			clip.Pause ();
		}
	}
	static public void Continue(AudioSource target){
		if (target == null)
			return;
		target.UnPause ();
	}
	static public void ContinueAll(){
		foreach (AudioSource clip in soundLibrary.Values) {
			if (clip == null)
				return;
			clip.UnPause ();
		}
	}
	static public void AddSoundintoSoundLibrary (AudioSource target,string key){
		soundLibrary.Add (key, target);
	}
	static public void DelectSoundfromSoundLibrary (string key){
		soundLibrary.Remove (key);
	}
	static public AudioSource FoundSoundWithKey(string key){
		AudioSource _out;
		soundLibrary.TryGetValue (key, out _out);
		return _out;
	}
}
