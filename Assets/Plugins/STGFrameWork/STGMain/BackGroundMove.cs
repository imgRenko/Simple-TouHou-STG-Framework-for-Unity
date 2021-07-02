using UnityEngine;
[System.Obsolete]
public class BackGroundMove : MonoBehaviour
{
    public Camera BgCamera;
    public float min = 3;
    public float max = 4;
    private Character Player;
    Vector2 _local = Vector2.zero;
    public  Vector2 positionVector2;
	// Use this for initialization
	void Start ()
	{
	    Player = Global.PlayerObject.GetComponent<Character>();
	    positionVector2 = BgCamera.gameObject.transform.position;
        Vector2 l =  BgCamera.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
	    l.x += positionVector2.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
     float percent = Screen.width/BgCamera.WorldToScreenPoint(Player.gameObject.transform.position).x;
	    BgCamera.gameObject.transform.position = new Vector2 (_local.x * percent, positionVector2.y);

        Debug.Log (_local.x * percent);
	    BgCamera.gameObject.transform.position =
	        new Vector2(Mathf.Clamp(_local.x, positionVector2.x - min, positionVector2.x + max),
	            positionVector2.y);

    }
}
