using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SliderDesign {
    public GameObject Target;
    public Vector2 targetPos;
    public float Alpha;
    public AnimationCurve curve;
}
[AddComponentMenu("东方STG框架/弹幕设计/弹幕特殊化/弹幕旋钮")]
public class BarrageSlider : MonoBehaviour {
    public float Rotation;
    public float MaxTime;
    public AnimationCurve curve;
    public List<SliderDesign> list = new List<SliderDesign>();
    private float _count;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Global.WrttienSystem && !Global.GamePause)
            _count += 1 * Global.GlobalSpeed;
        
    }
}
