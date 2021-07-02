using UnityEngine;
using System.Collections;

public class GameOverEvent : MonoBehaviour
{
	[Multiline]
	public string Note = "于此处输入注释。";
	void Start ()
	{
		GameOver a = GetComponent <GameOver> ();
		if (a == null)
			Debug.LogError ("没有可以使用的脚本");
		else {
			a.DisableEvent += Disable;
			a.EnableEvent += Enable;
		}
	}
	public IEnumerator Disable ()
	{
		yield return null;
	}
	public IEnumerator Enable ()
	{
		yield return null;
	}
}

