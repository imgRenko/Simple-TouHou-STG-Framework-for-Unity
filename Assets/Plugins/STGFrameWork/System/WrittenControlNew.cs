using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using Sirenix.OdinInspector;
[System.Serializable]
public class PlotData {
    public string Name;
    [Multiline]
    public string dialogContent;
    [PreviewField(100, ObjectFieldAlignment.Left)]
    
    public Sprite Sprite;
    public AudioClip Voice;
   
}
[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/旧剧情系统(传统GAL式)/剧情文本控制中心（新版）")]
public class WrittenControlNew: MonoBehaviour
{/// <summary>
 /// 该类实现剧情文本的控制与游戏节奏的控制   
 /// </summary>
	[HideInInspector]
    public XmlDocument wanttoread = new XmlDocument();
	[FoldoutGroup("流程设定", expanded: false)] 
    public string XmlFilePath = "Assets/Stream/S1/TalkEnemy.config";
	[FoldoutGroup("流程设定", expanded: false)] 
	public bool useXml = false ;

	[FoldoutGroup("剧情系统设定", expanded: false)] 
    [SerializeField]
	public List<PlotData> PlotDatas= new List<PlotData>();

	[FoldoutGroup("剧情细节设定", expanded: false)] 
    public string PlayerName ="Aya";
	[FoldoutGroup("剧情细节设定", expanded: false)] 
	public string OppoSiteName ="";
	[FoldoutGroup("剧情细节设定", expanded: false)] 
    [HideInInspector]
    public int MaxStep = 0;
	[FoldoutGroup("剧情细节设定", expanded: false)] 
	public int StepNow = -1;
	[HideInInspector]
	[FoldoutGroup("剧情细节设定", expanded: false)] 
    public int LastStep;
    [FoldoutGroup("剧情细节设定", expanded: false)]
    [HideInInspector]
    public WrittenSystenInstance writtenSystemInstance;
    [HideInInspector]
    public AudioSource audioSource;
    [FoldoutGroup("简单事件设定", expanded: false)] 
	[SerializeField]
	public List<EasyEvent> eventList = new List<EasyEvent> ();
    public delegate void Event ();
    public event Event TalkingEvent;
    private Animator PlayerImageAnimationController;
	private Animator OppoSiteImageAnimationController;
	private Animator MessagePlayer;

	public XmlNode key;
	[HideInInspector]
	public int CountTime = 0;
	[HideInInspector]
	public int NextTimeAppendText = 0;
	[HideInInspector]
    public bool PlayerAppear = false;
	[HideInInspector]
    public bool OppoAppear = false;
    void Awake ()
    {
        GameObject Temp = GameObject.Find("OldPlotOperationSystem");
        if (Temp == null)
        {
            Debug.LogError("剧情系统未启用，找到WrittenSystem->OldPlotOperationSystem，将它启用后，此组件将可以生效。");
            enabled = false;
            return;
        }
        writtenSystemInstance = Temp.GetComponent<WrittenSystenInstance>();
        audioSource = GetComponent<AudioSource>();
    

        if (useXml) {
			if (File.Exists (Application.streamingAssetsPath + "/Plot/" + XmlFilePath) == false) {
				StepNow = MaxStep;
				Debug.Log ("找不到Xml文件");
				return;
			}
			LastStep = MaxStep;
			XmlFilePath = Application.streamingAssetsPath + "/Plot/" + XmlFilePath;
			wanttoread.Load (XmlFilePath);
			key = wanttoread.SelectSingleNode ("Story/StoryText");
			for (int i = 1; i != MaxStep; ++i) {
				if (key == null)
					continue;
                PlotData r = new PlotData();
                r.dialogContent = key["Text" + i.ToString()].Attributes["value"].InnerText.ToString();
                r.Name = key["Text" + i.ToString()].Attributes["name"].InnerText.ToString();
               
				
			}
		}
        PlayerImageAnimationController = writtenSystemInstance.PlayerImage.GetComponent<Animator> ();
        OppoSiteImageAnimationController = writtenSystemInstance.OppoSiteImage.GetComponent<Animator> ();
        MessagePlayer = writtenSystemInstance.Message.GetComponent<Animator> ();
        MaxStep = PlotDatas.Count;
    }
    IEnumerator WrttienSystemFinished ()
    {
        Global.ShowCharCard ();
        yield return new WaitForSeconds (0.6f);
        Global.WrttienSystem = false;
     
    }
    public void EventSimplyProgress()
    {
        if (eventList.Count == 0)
            return;
        for (int i = 0; i != eventList.Count; ++i)
        {
            if (eventList[i].Step - 1 == StepNow)
            {
                if (eventList[i].Enable == false)
                    continue;
                DoEvent(eventList[i]);
                 eventList[i].Enable = false;
                
            }
        }
    }
    public void DoEvent(EasyEvent tar) {
        switch (tar.Command)
        {
            case EasyEvent.EventCommand.ACTIVE_OBJECT:
                tar.Target.SetActive(true);
                break;
            case EasyEvent.EventCommand.DISACTIVE_OBJECT:
                tar.Target.SetActive(false);
                break;
            case EasyEvent.EventCommand.APPEAR_PLAYER:
                MakePlayerAppear();
                break;
            case EasyEvent.EventCommand.DISAPPEAR_PLAYER:
                MakeImageDisappear(1);
                break;
            case EasyEvent.EventCommand.APPEAR_OPPO:
                MakeOppositeAppear();
                break;
            case EasyEvent.EventCommand.DISAPPEAR_OPPO:
                MakeImageDisappear(2);
                break;
            case EasyEvent.EventCommand.ENABLE_ENEMY_BARRAGE:
                tar.targetEnemy.UseBarrages();
                break;
            case EasyEvent.EventCommand.ENABLE_ENEMY_TARGETSHOOTING:
                tar.targetEnemy.UseBarrages();
                break;
            case EasyEvent.EventCommand.PLAY_MUSIC:
                tar.targetMusic.Play();
                break;
            case EasyEvent.EventCommand.CHANGE_MUSIC:
                tar.targetChangeMusic.clip = tar.targetMusicClip;
                tar.targetChangeMusic.Play();
                break;
            case EasyEvent.EventCommand.STOP_MUSIC:
                tar.stopTargetMusic.Stop();
                break;
            case EasyEvent.EventCommand.MOVE_ENEMY:
                tar.targetMoveEnemy.Move(tar.targetPoint);
                break;
            case EasyEvent.EventCommand.SETPLAYER_IMAGE:
                writtenSystemInstance.PlayerImage.sprite = tar.PlayerSprite;
                break;
            case EasyEvent.EventCommand.SETENEMY_IMAGE:
                writtenSystemInstance.OppoSiteImage.sprite = tar.EnemySprite;
                break;
            case EasyEvent.EventCommand.SETPLAYER_IMAGEPOS:
                writtenSystemInstance.PlayerImage.rectTransform.anchoredPosition = tar.PlayerPos;
                break;
            case EasyEvent.EventCommand.SETENEMY_IMAGEPOS:
                writtenSystemInstance.OppoSiteImage.rectTransform.anchoredPosition = tar.EnemyPos;
                break;
            case EasyEvent.EventCommand.GRAPH_FUNCTION:
                XNode.Node nodeGot = null;
                foreach (var a in tar.Graph.nodes)
                {
                    if (a == null)
                        continue;
                    if (a.name == tar.Function)
                    {
                        nodeGot = a;
                        break;
                    }
                }
                if (nodeGot != null)
                    nodeGot.ConnectDo("继续");
                break;
        }
    } 
    public bool SetWrittenSystem ()
    {
        /// <summary>
        /// 建立了此系统，你所定义的游戏进度都将暂时停止！注意，放置在Update函数中（是Stream XX.cs中的函数）两个剧情系统不允许共同生效。会产生冲突。
        /// </summary>
        if ( StepNow >= MaxStep  )
        {
            
            StartCoroutine (WrttienSystemFinished ());
            enabled = false;
            return true;

        }
        if (Global.GamePause)
            return false;
        if ( Global.WrttienSystem == false )
        {
            StartCoroutine(Global.GameObjectPool_A.DestroyBullets (true));
            writtenSystemInstance.TalkText.gameObject.SetActive (true);
            Global.WrttienSystem = true;
            writtenSystemInstance.CharacterName.enabled = true;
            writtenSystemInstance.TalkText.enabled = true;
            writtenSystemInstance.Message.enabled = true;
            writtenSystemInstance.PlayerImage.enabled = true;
            writtenSystemInstance.OppoSiteImage.enabled = true;
            EventSimplyProgress();
            StartCoroutine(NextTextAppear ());
            Global.HideCharCard ();

            if ( TalkingEvent != null )
            {
                TalkingEvent ();
            }
        }
        else
        {
            if ( Input.GetButtonUp ("Submit") == true )
            {
                EventSimplyProgress();
                StartCoroutine(NextTextAppear ());
            }
        }
        return false;
    }
    public void MakePlayerAppear ()
    {
        PlayerAppear = true;
        
        PlayerImageAnimationController.SetBool("Speak", true);
    }
    public void MakeOppositeAppear ()
    {

        OppoAppear = true;

        OppoSiteImageAnimationController.SetBool("Speak", true);
    }
    public void MakeImageDisappear (int Type)
    {
        if ( Type == 1 )
        {
            PlayerAppear = false;
            PlayerImageAnimationController.SetBool ("Dead", true);
        }
        if ( Type == 2 )
        {
            OppoAppear = false;
            OppoSiteImageAnimationController.SetBool("Dead", true);
        }
    }
    public void MakeMessageDisplay (bool IsDiji = false)
    {
        if (StepNow < PlotDatas.Count)
        {
            if (IsDiji == false)
            {
                if (PlotDatas[StepNow] != PlotDatas[StepNow - 1 < 0 ? 0 : StepNow - 1])
                {
                    if (OppoAppear)
                        OppoSiteImageAnimationController.SetBool("Speak", false);
                    if (PlayerAppear)
                        PlayerImageAnimationController.SetBool("Speak", true);
                    //Message.transform.localRotation = Quaternion.Euler (0, 180, 0)
                }
                MessagePlayer.PlayInFixedTime("MessageAppear", 0, 0);
                if (PlotDatas[StepNow].Sprite != null)
                    writtenSystemInstance.PlayerImage.sprite = PlotDatas[StepNow].Sprite;
            }
            else
            {
                if (PlotDatas[StepNow].Name != PlotDatas[StepNow - 1 < 0 ? 0 : StepNow - 1].Name)
                {
                    if (OppoAppear)
                        OppoSiteImageAnimationController.SetBool("Speak", true);
                    if (PlayerAppear)
                        PlayerImageAnimationController.SetBool("Speak",false);
                }
                if (PlotDatas[StepNow].Sprite != null)
                    writtenSystemInstance.OppoSiteImage.sprite = PlotDatas[StepNow].Sprite;
                MessagePlayer.PlayInFixedTime("MessageAppear_Opposite", 0, 0);
                //Message.transform.localRotation = Quaternion.Euler (0, 0, 0);
            }
        }
    }
    void Update() {
        SetWrittenSystem();

    }
    public IEnumerator NextTextAppear ()
    {
       
        ++StepNow;
        if ( TalkingEvent != null )
            TalkingEvent ();
        if ( StepNow >= MaxStep )
        {
            StepNow = MaxStep;
           
            StopCoroutine("AppendText");
            yield return null;
        }

        if (StepNow == MaxStep )
        {
            if (PlayerAppear)
                MakeImageDisappear(1);
            if (OppoAppear)
                MakeImageDisappear(2);
            writtenSystemInstance.CharacterName.text = "";
            writtenSystemInstance.TalkText.text = "";
            MessagePlayer.PlayInFixedTime("MessageDead", 0, 0);
            Global.WrttienSystem = false;
            StopCoroutine("AppendText");
            yield return null;
        }
        else
        {
            if (Global.WrttienSystem)
            {
                writtenSystemInstance.CharacterName.text = PlotDatas[StepNow].Name;
                MessagePlayer.PlayInFixedTime("MessageDead", 0, 0);
                if (writtenSystemInstance.CharacterName.text == PlayerName)
                {
                    if (PlotDatas[StepNow].Sprite != null)
                        writtenSystemInstance.PlayerImage.sprite = PlotDatas[StepNow].Sprite;

                    if (PlotDatas[StepNow].Name != PlotDatas[StepNow - 1 < 0 ? 0 : StepNow - 1].Name)
                    {
                        if (OppoAppear)
                            OppoSiteImageAnimationController.SetBool("Speak", true);
                        if (PlayerAppear)
                            PlayerImageAnimationController.SetBool("Speak", false);
                    }
                    if (PlayerAppear == false)
                        yield return new WaitForSeconds(0.4f);
                    MakeMessageDisplay(false);

                }
                if (writtenSystemInstance.CharacterName.text == OppoSiteName)
                {
                    if (PlotDatas[StepNow].Name != PlotDatas[StepNow - 1 < 0 ? 0 : StepNow - 1].Name)
                    {
                        if (OppoAppear)
                            OppoSiteImageAnimationController.SetBool("Speak", false);
                        if (PlayerAppear)
                            PlayerImageAnimationController.SetBool("Speak", true);
                        //Message.transform.localRotation = Quaternion.Euler (0, 180, 0)
                    }
                    if (PlotDatas[StepNow].Sprite != null)
                        writtenSystemInstance.OppoSiteImage.sprite = PlotDatas[StepNow].Sprite;
                    if (OppoAppear == false)
                        yield return new WaitForSeconds(0.4f);
                    MakeMessageDisplay(true);

                }
                if (StepNow < PlotDatas.Count)
                    writtenSystemInstance.TalkText.text = PlotDatas[StepNow].dialogContent;
                if (StepNow < PlotDatas.Count)
                {
                    if (audioSource != null && PlotDatas[StepNow].Voice != null)
                    {
                        audioSource.clip = PlotDatas[StepNow].Voice;
                        audioSource.Play();
                    }
            }
        }
        }
    }

}
