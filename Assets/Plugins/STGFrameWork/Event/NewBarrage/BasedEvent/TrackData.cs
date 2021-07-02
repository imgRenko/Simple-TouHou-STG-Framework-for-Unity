using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用作子弹类的间接移动数据
/// </summary>
[System.Serializable]
public class TrackData  {
	public AnimationCurve animationCurve;
	public bool calculateFrame = true;
	public Vector3 targetPos;
	public bool regularPattern = false;
	[Title("完成定向移动后")]
	public List<CalculateTrackInfo> calculateTrackInfos = new List<CalculateTrackInfo>();
}
public class CalculateTrackInfo {
	
	public BasedEvent_BulletLocomotion.BulletChange bulletChange;
}