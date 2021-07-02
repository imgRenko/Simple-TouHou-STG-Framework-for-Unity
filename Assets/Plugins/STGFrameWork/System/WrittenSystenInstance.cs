using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class WrittenSystenInstance : MonoBehaviour {
	[FoldoutGroup("剧情GUI设定", expanded: false)]
	public Image PlayerImage;
	[FoldoutGroup("剧情GUI设定", expanded: false)]
	public Image Message;
	[FoldoutGroup("剧情GUI设定", expanded: false)]
	public Image OppoSiteImage;
	[FoldoutGroup("剧情GUI设定", expanded: false)]
	public Text CharacterName;
	[FoldoutGroup("剧情GUI设定", expanded: false)]
	public Text TalkText;

	[FoldoutGroup("剧情GUI设定", expanded: false)]
	public GameObject Live2DPlayerCopy;
	[FoldoutGroup("剧情GUI设定", expanded: false)]
	public GameObject Live2DCopy;
}
