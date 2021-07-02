using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystemInstance : MonoBehaviour {
	public DialogSystemImage backGround;
	public DialogSystemImage backGroundColor;
	public DialogSystemCharacterImage CharImgBased;
	public DialogSystemLive2DCameraController dialogSystemLive2DCameraController;
	public DialogSystemTextMessage TextMessage;
	public List<DialogSystemDictionary> FunctionSetting = new List<DialogSystemDictionary>();
	public GameObject Live2DModelPlace,Main,Live2DCamera;
	public AnimationCurve breathCurve;
}
