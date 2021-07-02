using UnityEngine;

public class BGOffset : MonoBehaviour {
	public GameObject Target;
	public float rangeOffsetX = 1;
	public float rangeOffsetY = 1;
	PlayerController _char;
	// Use this for initialization
	void Start () {
		_char = Global.PlayerObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 Position = Global.MainCamera.WorldToScreenPoint( _char.gameObject.transform.position);
		Position.x = Mathf.Clamp (Position.x, Screen.width * _char.MinRangeofScreen.x, Screen.width *_char.MaxRangeofScreen.x);
		Position.y = Mathf.Clamp (Position.y, Screen.height * _char.MinRangeofScreen.y, Screen.height *_char.MaxRangeofScreen.y);
		Vector2 _final = Global.MainCamera.WorldToScreenPoint(transform.position);
		_final.x = ((_final.x - Screen.width * _char.MinRangeofScreen.x * 2) / (Screen.width * _char.MaxRangeofScreen.x - Screen.width * _char.MinRangeofScreen.x)) * rangeOffsetX;
		_final.y = ((_final.y - Screen.height * _char.MinRangeofScreen.y * 2) / (Screen.height  * _char.MaxRangeofScreen.y - Screen.height * _char.MinRangeofScreen.y)) * rangeOffsetY;
		transform.position = Global.MainCamera.ScreenToWorldPoint (_final);
	}
}
