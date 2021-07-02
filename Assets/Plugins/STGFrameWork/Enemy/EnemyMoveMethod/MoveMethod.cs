using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
[System.Serializable]
public class CharacterMovementFrame {
    [LabelText("使用此参数的时间点")]
    public float Frame;
    public enum Type{
        MoveTargetPoint = 0,
        RandomInRange = 1
    }
    [LabelText("移动类型")]
    public Type moveType = Type.RandomInRange;
    [ShowIf("moveType", Type.RandomInRange)]
    [LabelText("X轴随机偏移值")]
    public float XOffset = 0;
    [ShowIf("moveType", Type.RandomInRange)]
    [LabelText("Y轴随机偏移值")]
    public float YOffset = 0;
    [ShowIf("moveType", Type.MoveTargetPoint)]
    [LabelText("要移动到的固定点")]
    public Vector2 TargetPoint;
    [LabelText("运动时间")]
    public float RunTime = 45;
    [LabelText("运动曲线")]
    public AnimationCurve animationCurve;
    [LabelText("敌人运动方式")]
    public Enemy.MoveCurves enemyMoveType = Enemy.MoveCurves.Slerp;
    [HideInInspector]
    public bool Used = false;
}
[AddComponentMenu("东方STG框架/框架核心/敌人类/敌人移动系统")]
public class MoveMethod : MonoBehaviour {
    [InfoBox("此移动系统是敌人移动系统中的一套，你可以使用这个简单的移动系统做简单的移动，也可以使用子弹让其做更复杂的运动，但是运算量更大，二者不可兼容，如果需要切换，在子弹处勾选作为敌人移动器使用，然后将此移动系统反激活。")]
    [ProgressBar (0,100)]
    [LabelText("移动器剩余寿命")]
    public float Progress = 0;
    [LabelText("移动器最大生命周期")]
    public float maxFrame = 200;
    [LabelText("循环使用移动器")]
    public bool Circle = true;
    [LabelText("每次循环重置一次位置")]
    public bool ResetPoint = true;
    [LabelText("声明符卡时重置移动器")]
    public bool resetInSpellExpressing = true;
    [LabelText("重置移动器时将敌人位置重置的位置")]
    public Vector2 resetPoint = Vector2.zero;
    [SerializeField]
    [LabelText("移动设置")]
    public List<CharacterMovementFrame> frameSetting  = new List<CharacterMovementFrame>();

    private Enemy Character;   
    private float Count;

    public void ResetMovement(bool ResetPoint = true){
        if (Character == null)
            return;
        // Character.MoveType = Enemy.MoveCurves.Slerp;
        if (ResetPoint)
            Character.Move(resetPoint);
          
        Count = 0;
        foreach (CharacterMovementFrame setting in frameSetting) {
            setting.Used = false;
        }
    }

    public void SetMovementFrameSetting(List<CharacterMovementFrame> _frameSetting){
        frameSetting = _frameSetting;
    }

    void Start(){
        Character = GetComponentInChildren<Enemy> ();
        if (Character == null)
            this.enabled = false;
    }

	void Update () {
        if (Global.WrttienSystem || Global.GamePause || Character.Moving || frameSetting.Count == 0)	
            return;
        Count += Global.GlobalSpeed;
        Progress = Count / maxFrame * 100;
        if (Progress >= 100 && Circle == true ) {
            ResetMovement (ResetPoint);
            Count = 0;
            Progress = 0;
        }
        foreach (CharacterMovementFrame setting in frameSetting) {
            if (Count >= setting.Frame && !setting.Used) {
                setting.Used = true;

                if (setting.moveType == CharacterMovementFrame.Type.RandomInRange){
                    Vector2 _temp = new Vector2 (Mathf.Clamp(Random.Range(-setting.XOffset,setting.XOffset),Global.ScreenX_A.x,Global.ScreenX_A.y),Mathf.Clamp(Random.Range(-setting.YOffset,setting.YOffset),Global.ScreenY_A.x,Global.ScreenY_A.y));
                   ;
                    Character.Move(Character.GetMoveTargetPoint() + _temp);
                    Character.MoveType = setting.enemyMoveType;
                }
                if (setting.moveType == CharacterMovementFrame.Type.MoveTargetPoint){
                    Character.MoveType = setting.enemyMoveType;
                    Character.Move(setting.TargetPoint);
                }
                Character.animationCurve = setting.animationCurve;
                Character.RunTime = setting.RunTime;
               
            }
        }

	}
}
