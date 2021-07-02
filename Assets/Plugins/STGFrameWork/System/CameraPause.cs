using UnityEngine;

public class CameraPause : MonoBehaviour
{
	public Animator controled;
	void Update(){
		controled.speed = (Global.GamePause ? 0 : 1);
	}
}

