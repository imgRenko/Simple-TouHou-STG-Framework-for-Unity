using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/自机类/自机动画控制器")]
public class CharacterAnimController : MonoBehaviour {
	public Character Target;
	// Use this for initialization
	public Animator AnimController;
	public void CharAppear(){
		AnimController.PlayInFixedTime ("PlayerAppear");
	}
}
