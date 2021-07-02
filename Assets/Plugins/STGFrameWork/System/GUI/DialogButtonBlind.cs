using UnityEngine;
using UnityEngine.UI;

public class DialogButtonBlind : MonoBehaviour
{
	public Button EventButton;
	public Dialog.GUIEvent ButtonBlindEvent;
	public void DriveEvent()
	{
		if (ButtonBlindEvent != null)
			StartCoroutine (ButtonBlindEvent ());
	}
}

