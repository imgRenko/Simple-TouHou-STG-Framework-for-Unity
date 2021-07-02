using UnityEngine;
using UnityEngine.UI;
public class BossPositionSign : MonoBehaviour {
    public Image Sign;
    public Enemy Target;
    public Camera _cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Sign.enabled = Target != null;
        if ( Target != null )
        {
            Vector3 Position = _cam.WorldToScreenPoint (Target.gameObject.transform.position);
            Position.y = Sign.rectTransform.position.y;
            Sign.rectTransform.position = Position;
        }
	}
}
