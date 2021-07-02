using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/自定义子弹碰撞盒")]
public class CustomCollision : MonoBehaviour {
    Character PlayerCharacter;
    public float Radius = 0;
	// Use this for initialization
	public bool Check () {
		float Distance = Vector2.Distance(PlayerCharacter.gameObject.transform.position, gameObject.transform.position);
        return Radius + PlayerCharacter.Radius > Distance;
    }
    void Start () { PlayerCharacter = GameObject.Find("Player").GetComponent<Character> (); }
}
