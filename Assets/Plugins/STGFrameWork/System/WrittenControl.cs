using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using Sirenix.OdinInspector;
[System.Serializable]
public class EasyEvent{
	public GameObject Target;
	[ShowIf("Command", EventCommand.ENABLE_ENEMY_TARGETSHOOTING)]
	public Shooting targetShooting;
	[ShowIf("Command", EventCommand.ENABLE_ENEMY_BARRAGE)]
	public Enemy targetEnemy;
    [ShowIf("Command", EventCommand.PLAY_MUSIC)]
    public AudioSource targetMusic;
    [ShowIf("Command", EventCommand.CHANGE_MUSIC)]
    public AudioSource targetChangeMusic;
    [ShowIf("Command", EventCommand.CHANGE_MUSIC)]
    public AudioClip targetMusicClip;
    [ShowIf("Command", EventCommand.STOP_MUSIC)]
    public AudioSource stopTargetMusic;
    [ShowIf("Command", EventCommand.MOVE_ENEMY)]
    public Vector2 targetPoint;
    [ShowIf("Command", EventCommand.MOVE_ENEMY)]
    public Enemy targetMoveEnemy;
    [ShowIf("Command", EventCommand.SETENEMY_IMAGE)]
    public Sprite EnemySprite;
    [ShowIf("Command", EventCommand.SETPLAYER_IMAGE)]
    public Sprite PlayerSprite;
    [ShowIf("Command", EventCommand.SETPLAYER_IMAGEPOS)]
    public Vector2 PlayerPos;
    [ShowIf("Command", EventCommand.SETENEMY_IMAGEPOS)]
    public Vector2 EnemyPos;
    [ShowIf("Command", EventCommand.GRAPH_FUNCTION)]
    public STGTriggerGraph Graph;
    [ShowIf("Command", EventCommand.GRAPH_FUNCTION)]
    public string Function;
    public EventCommand Command;

	public int Step = 0;
	public enum EventCommand{
		ACTIVE_OBJECT=0,
		DISACTIVE_OBJECT=1,
		APPEAR_PLAYER=2,
		DISAPPEAR_PLAYER=3,
		APPEAR_OPPO=4,
		DISAPPEAR_OPPO=5,
		ENABLE_ENEMY_BARRAGE = 6,
		ENABLE_ENEMY_TARGETSHOOTING=7,
        CHANGE_MUSIC = 8,
        PLAY_MUSIC = 9,
        STOP_MUSIC = 10,
        MOVE_ENEMY = 11,
        SETPLAYER_IMAGE= 12,
        SETENEMY_IMAGE= 13,
        SETPLAYER_IMAGEPOS = 14,
        SETENEMY_IMAGEPOS = 15,
        GRAPH_FUNCTION = 16


    }

    public bool Enable = true;
}
[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/旧剧情系统(传统GAL式)/剧情文本控制中心（兼容旧版，将废弃）")]
public class WrittenControl : MonoBehaviour
{/// <summary>
 /// 该类实现剧情文本的控制与游戏节奏的控制   
 /// </summary>
    [HideInInspector]
    public XmlDocument wanttoread = new XmlDocument();
    [FoldoutGroup("XML设定", expanded: false)]
    public string XmlFilePath = "Assets/Stream/S1/TalkEnemy.config";
    [FoldoutGroup("XML设定", expanded: false)]
    public bool useXml = true;
    [FoldoutGroup("剧情系统设定", expanded: false)]
    public List<string> Name = new List<string>();
    [FoldoutGroup("剧情系统设定", expanded: false)]
    public List<string> text = new List<string>();
    [FoldoutGroup("剧情系统设定", expanded: false)]
    public List<string> num = new List<string>();
    [FoldoutGroup("剧情系统设定", expanded: false)]
    [PreviewField(196, ObjectFieldAlignment.Left)]
    [SerializeField]
    public List<Sprite> SPRITE = new List<Sprite>();
    [FoldoutGroup("剧情系统设定", expanded: false)]
    public List<AudioSource> Voice = new List<AudioSource>();
    [FoldoutGroup("剧情细节设定", expanded: false)]
    public string PlayerName = "Aya";
    [FoldoutGroup("剧情细节设定", expanded: false)]
    public string OppoSiteName = "";
    [FoldoutGroup("剧情细节设定", expanded: false)]
    public int MaxStep = 0;
    [FoldoutGroup("剧情细节设定", expanded: false)]
    public int StepNow = -1;
    [HideInInspector]
    [FoldoutGroup("剧情细节设定", expanded: false)]
    public int LastStep;
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
    [FoldoutGroup("简单事件设定", expanded: false)]
    [SerializeField]
    public List<EasyEvent> eventList = new List<EasyEvent>();
    public delegate void Event();
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
    void Awake()
    {
        if (useXml)
        {
            if (File.Exists(Application.streamingAssetsPath + "/Plot/" + XmlFilePath) == false)
            {
                StepNow = MaxStep - 1;
                Debug.Log("找不到Xml文件");
                return;
            }
            LastStep = MaxStep - 1;
            XmlFilePath = Application.streamingAssetsPath + "/Plot/" + XmlFilePath;
            wanttoread.Load(XmlFilePath);
            key = wanttoread.SelectSingleNode("Story/StoryText");
            for (int i = 1; i != MaxStep; ++i)
            {
                if (key == null)
                    continue;
                text.Add(key["Text" + i.ToString()].Attributes["value"].InnerText.ToString());
                Name.Add(key["Text" + i.ToString()].Attributes["name"].InnerText.ToString());
                num.Add(i.ToString());
            }
        }
        PlayerImageAnimationController = PlayerImage.GetComponent<Animator>();
        OppoSiteImageAnimationController = OppoSiteImage.GetComponent<Animator>();
        MessagePlayer = Message.GetComponent<Animator>();
    }
    IEnumerator WrttienSystemFinished()
    {
        Global.ShowCharCard();
        Global.ScoreAnimator.PlayInFixedTime("BossBattleBack", 0, 0);
        yield return new WaitForSeconds(0.6f);
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

                switch (eventList[i].Command)
                {
                    case EasyEvent.EventCommand.ACTIVE_OBJECT:
                        eventList[i].Target.SetActive(true);
                        break;
                    case EasyEvent.EventCommand.DISACTIVE_OBJECT:
                        eventList[i].Target.SetActive(false);
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
                        eventList[i].targetEnemy.UseBarrages();
                        break;
                    case EasyEvent.EventCommand.ENABLE_ENEMY_TARGETSHOOTING:
                        eventList[i].targetEnemy.UseBarrages();
                        break;
                    case EasyEvent.EventCommand.PLAY_MUSIC:
                        eventList[i].targetMusic.Play();
                        break;
                    case EasyEvent.EventCommand.CHANGE_MUSIC:
                        eventList[i].targetChangeMusic.clip = eventList[i].targetMusicClip;
                        eventList[i].targetChangeMusic.Play();
                        break;
                    case EasyEvent.EventCommand.STOP_MUSIC:
                        eventList[i].stopTargetMusic.Stop();
                        break;
                    case EasyEvent.EventCommand.GRAPH_FUNCTION:
                        XNode.Node nodeGot = null;
                        foreach (var a in eventList[i].Graph.nodes)
                        {
                            if (a == null)
                                continue;
                            if (a.name == eventList[i].Function)
                            {
                                nodeGot = a;
                                break;
                            }
                        }
                        if(nodeGot != null)
                        nodeGot.ConnectDo("继续");
                        break;
                }
                eventList.RemoveAt(i);
                break;
            }
        }
    }
    public bool SetWrittenSystem()
    {
        /// <summary>
        /// 建立了此系统，你所定义的游戏进度都将暂时停止！注意，放置在Update函数中（是Stream XX.cs中的函数）两个剧情系统不允许共同生效。会产生冲突。
        /// </summary>
        if (StepNow >= MaxStep - 1)
        {

            StartCoroutine(WrttienSystemFinished());
            enabled = false;
            return true;

        }
        if (Global.GamePause)
            return false;
        if (Global.WrttienSystem == false)
        {
            StartCoroutine(Global.GameObjectPool_A.DestroyBullets(true));
            TalkText.gameObject.SetActive(true);
            Global.WrttienSystem = true;
            CharacterName.enabled = true;
            TalkText.enabled = true;
            Message.enabled = true;
            PlayerImage.enabled = true;
            OppoSiteImage.enabled = true;
            EventSimplyProgress();
            StartCoroutine(NextTextAppear());
            Global.HideCharCard();
            Global.ScoreAnimator.PlayInFixedTime("BossBattleStart", 0, 0);
            if (TalkingEvent != null)
            {
                TalkingEvent();
            }
        }
        else
        {
            if (Input.GetButtonUp("Submit") == true)
            {
                EventSimplyProgress();
                StartCoroutine(NextTextAppear());
            }
        }
        return false;
    }
    public void MakePlayerAppear()
    {
        PlayerAppear = true;
        PlayerImageAnimationController.PlayInFixedTime("PlayerPicture", 0, 0);
    }
    public void MakeOppositeAppear()
    {

        OppoAppear = true;

        OppoSiteImageAnimationController.PlayInFixedTime("OppositePicture", 0, 0);
    }
    public void MakeImageDisappear(int Type)
    {
        if (Type == 1)
        {
            PlayerAppear = false;
            PlayerImageAnimationController.PlayInFixedTime("PlayerPictureDead", 0, 0);
        }
        if (Type == 2)
        {
            OppoAppear = false;
            OppoSiteImageAnimationController.PlayInFixedTime("OppositePictureDead", 0, 0);
        }
    }
    public void MakeMessageDisplay(bool IsDiji = false)
    {
        if (StepNow < Name.Count)
        {
            if (IsDiji == false)
            {
                if (Name[StepNow] != Name[StepNow - 1 < 0 ? 0 : StepNow - 1])
                {
                    if (OppoAppear)
                        OppoSiteImageAnimationController.PlayInFixedTime("OppositePicture_AfterSpeaking", 0, 0);
                    if (PlayerAppear)
                        PlayerImageAnimationController.PlayInFixedTime("PlayerPicture_Speaking", 0, 0);
                    //Message.transform.localRotation = Quaternion.Euler (0, 180, 0)
                }
                MessagePlayer.PlayInFixedTime("MessageAppear", 0, 0);
                if (SPRITE[StepNow] != null)
                    PlayerImage.sprite = SPRITE[StepNow];
            }
            else
            {
                if (Name[StepNow] != Name[StepNow - 1 < 0 ? 0 : StepNow - 1])
                {
                    if (OppoAppear)
                        OppoSiteImageAnimationController.PlayInFixedTime("OppositePicture_Speaking", 0, 0);
                    if (PlayerAppear)
                        PlayerImageAnimationController.PlayInFixedTime("PlayerPicture_AfterSpeaking", 0, 0);
                }
                if (SPRITE[StepNow] != null)
                    OppoSiteImage.sprite = SPRITE[StepNow];
                MessagePlayer.PlayInFixedTime("MessageAppear_Opposite", 0, 0);
                //Message.transform.localRotation = Quaternion.Euler (0, 0, 0);
            }
        }
    }
    void Update()
    {
        SetWrittenSystem();

    }
    public IEnumerator NextTextAppear()
    {

        ++StepNow;
        if (TalkingEvent != null)
            TalkingEvent();
        if (StepNow >= MaxStep)
        {
            StepNow = MaxStep;

            StopCoroutine("AppendText");
            yield return null;
        }

        if (StepNow == MaxStep - 1)
        {
            if (PlayerAppear)
                MakeImageDisappear(1);
            if (OppoAppear)
                MakeImageDisappear(2);
            CharacterName.text = "";
            TalkText.text = "";
            MessagePlayer.PlayInFixedTime("MessageDead", 0, 0);
            Global.WrttienSystem = false;
            StopCoroutine("AppendText");
            yield return null;
        }
        else
        {
            if (Global.WrttienSystem)
            {
                CharacterName.text = Name[StepNow];
                MessagePlayer.PlayInFixedTime("MessageDead", 0, 0);
                if (CharacterName.text == PlayerName)
                {
                    if (SPRITE[StepNow] != null)
                        PlayerImage.sprite = SPRITE[StepNow];

                    if (Name[StepNow] != Name[StepNow - 1 < 0 ? 0 : StepNow - 1])
                    {
                        if (OppoAppear)
                            OppoSiteImageAnimationController.PlayInFixedTime("OppositePicture_Speaking", 0, 0);
                        if (PlayerAppear)
                            PlayerImageAnimationController.PlayInFixedTime("PlayerPicture_AfterSpeaking", 0, 0);
                    }
                    if (PlayerAppear == false)
                        yield return new WaitForSeconds(0.4f);
                    MakeMessageDisplay(false);

                }
                if (CharacterName.text == OppoSiteName)
                {
                    if (Name[StepNow] != Name[StepNow - 1 < 0 ? 0 : StepNow - 1])
                    {
                        if (OppoAppear)
                            OppoSiteImageAnimationController.PlayInFixedTime("OppositePicture_AfterSpeaking", 0, 0);
                        if (PlayerAppear)
                            PlayerImageAnimationController.PlayInFixedTime("PlayerPicture_Speaking", 0, 0);
                        //Message.transform.localRotation = Quaternion.Euler (0, 180, 0)
                    }
                    if (SPRITE[StepNow] != null)
                        OppoSiteImage.sprite = SPRITE[StepNow];
                    if (OppoAppear == false)
                        yield return new WaitForSeconds(0.4f);
                    MakeMessageDisplay(true);

                }
                if (StepNow < text.Count)
                    TalkText.text = text[StepNow];
                if (StepNow < Voice.Count)
                {
                    if (Voice[StepNow] != null)
                        Voice[StepNow].Play();
                }
            }
        }
    }

}
