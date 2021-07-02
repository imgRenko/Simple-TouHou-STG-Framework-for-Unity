using Live2D.Cubism.Core;
using Live2D.Cubism.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLive2DController : MonoBehaviour {
    public CubismModel modelInfo;
    public Transform Movement;
    public AnimationCurve BreathCurve;
    public CubismParameter BreathParam;
    public int index;
    public CubismRenderController controller;
    public Vector2 OriginalPos;
    public Vector2 TargetPos;
    public float originalAlpha, targetAlpha;
    public float Speed;
   // private int i = 0;
   // private bool a = false;
    private float t = 0;
    private void Awake() {
        Movement = this.gameObject.transform;
    }
    private void Update()
    {
        t += Time.deltaTime;
        BreathParam.Value = BreathCurve.Evaluate(t);
       
        Movement.localPosition = Vector2.Lerp(Movement.localPosition, TargetPos, Speed);
        controller.Opacity = Mathf.Lerp(controller.Opacity, targetAlpha,Speed);
    }
}
