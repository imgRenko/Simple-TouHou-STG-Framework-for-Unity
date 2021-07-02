using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/符卡控制器(放在敌人对象子级下)")]
public class SpellCard : STGComponent
{
    // -------------------------------- 符卡 -----------------------------
    // 利用它制作非符以及符卡演出效果。
    // 在 SpellIndex 中设定先后顺序，将你的符卡依次设定好后。SpellIndex为0(或者在数组里最小的)的
    // 将被最先使用（若有相同的一起使用），你也可以决定是否使用计时器。
    // 我们建议 ： 大部分的计时器应该在游戏启动前被Disenabled掉，
    // ------------------------------------------------------------------
    [Button]
    public void SpellCardNameSpawn()
    {
        Name += "Tip Name「Spell Card Name」";

    }
    [FoldoutGroup("必填项", expanded: false)]
    [LabelText("使用符卡的对象")]
    public Enemy Character;
    [HideInInspector]
    [LabelText("不安全_重置用发射器证书")]
    public ShootingData Credentials;
    [FoldoutGroup("GUI方面", expanded: false)]
    [LabelText("时间脚本")]
    private Timer TimerScript;
    [FoldoutGroup("GUI方面", expanded: false)]
    [LabelText("要显示的BOSSHPGUI游戏对象")]
    private GameObject BOSSHP;

    [FoldoutGroup("GUI方面", expanded: false)]
    [LabelText("符卡背景游戏对象")]
    public GameObject SpellCardBG;
    [FoldoutGroup("GUI方面", expanded: false)]
    [LabelText("含有动态立绘的对象")]
    public GameObject DynamicPictureObject;


    [HideInInspector] public bool Use = false;

    [Tooltip("击破符卡时是否显示击破时文字。")]
    [FoldoutGroup("GUI方面", expanded: false)]
    [LabelText("显示击破时文字")]
    public bool ShowTip = true;
    [FoldoutGroup("符卡记录", expanded: false)]
    [Tooltip("用XML文件记录符卡收率")]
    [LabelText("用XML文件记录符卡收率")]
    public bool spellRecord = true;
    [FoldoutGroup("符卡设置", expanded: false)]
    [Tooltip("销毁符卡时，不再销毁全屏子弹")]
    [LabelText("不销毁全屏子弹")]
    public bool DonDestroyBullets = false;
    [FoldoutGroup("符卡设置", expanded: false)]
    [Tooltip("该符卡完成任务的时候，不将拥有该符卡组件的游戏对象销毁")]
    [LabelText("不销毁符卡对象")]
    public bool DonDestroySpell = false;
    [FoldoutGroup("符卡设置", expanded: false)]
    [Tooltip("忽略符卡时间，这张符卡只能通过击破BOSS这种方式来收取")]
    [LabelText("无时间限制")]
    public bool NoTimeLimit = false;
    [FoldoutGroup("符卡设置", expanded: false)]
    [Tooltip("自动使用下一张符卡")]
    [LabelText("自动使用下一张符卡")]
    public bool nextSpell = true;
    [FoldoutGroup("符卡设置", expanded: false)]
    [Tooltip("重置符卡发射器时，销毁全屏子弹")]
    [LabelText("重置发射器时销毁全屏子弹")]
    public bool resetShootingDestroyingBullets = false;
    [FoldoutGroup("符卡设置", expanded: false)]
    [Tooltip("将这张符卡用作时符")]
    [LabelText("这是时符")]
    public bool TimeSpell = false;
    [FoldoutGroup("GUI方面", expanded: false)]
    [Tooltip("显示BOSS血量GUI")]
    [LabelText("显示BOSSHPGUI")]
    public bool UseHP = false;
    [FoldoutGroup("GUI方面", expanded: false)]
    [Tooltip("使用时间计时器")]
    [LabelText("显示计时器")]
    public bool UseTimer = true;
    [FoldoutGroup("GUI方面", expanded: false)]
    [Tooltip("播放符卡动画")]
    [LabelText("播放符卡动画")]
    public bool ShowAnimation = false;
    [FoldoutGroup("GUI方面", expanded: false)]
    [Tooltip("播放符卡时，隐藏游戏原背景")]
    [LabelText("隐藏游戏原背景")]
    public bool HideDefaultBackGround = false;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡使用时间秒数")]
    public float Time = 60;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡使用时间毫秒")]
    public float MixSecond = 0;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡血量")]
    public float MaxHP = 2000;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡发射器重置时间")]
    public float ResetTime = 300.0f;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("下一张符卡等待时间")]
    public float nextSpellCardWaitingTime;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡已使用时间")]
    public float SpellCardUsingTime = 0;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡重置发射器计时")]
    public float ResetTotalFrame = 0;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡使用时间总帧数")]
    public float maxFrame = 0;
    [FoldoutGroup("GUI方面", expanded: false)]
    [LabelText("播放符卡动画的序号")]
    public int animationIndex = 0;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("符卡序号")]
    public int SpellIndex = 0;
    [FoldoutGroup("符卡奖励设置", expanded: false)]
    public long MaxBouns = 16000;
    [FoldoutGroup("符卡奖励设置", expanded: false)]
    public long MinBouns = 10000;
    [FoldoutGroup("符卡奖励设置", expanded: false)]
    public int ScoreBounsNumber = 24;
    [FoldoutGroup("符卡奖励设置", expanded: false)]
    public int PowerBounsNumber = 5;
    [FoldoutGroup("符卡奖励设置", expanded: false)]
    public int LiveBounsNumber = 0;
    [FoldoutGroup("符卡记录", expanded: false)]
    [Multiline]
    [LabelText("符卡记录信息文件名称")]
    public string XmlfileName = "SpellCardXmlData.xml";
    [FoldoutGroup("符卡记录", expanded: false)]
    [Multiline]
    [LabelText("符卡名字")]
    public string Name = "「New Spell Card」";
    [FoldoutGroup("GUI方面", expanded: false)]
    [PreviewField(196, ObjectFieldAlignment.Left)]
    [LabelText("符卡动画立绘")]
    public Sprite CharacterSprite;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("重置用发射器列表")]
    public List<Shooting> ResetShooting = new List<Shooting>();
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("发射器对象")]
    public GameObject[] ShootingObject;
    [FoldoutGroup("符卡设置", expanded: false)]
    [LabelText("允许改变关卡流程状态")]
    public bool ChangeRoadSign = true;
    [FoldoutGroup("GUI方面", expanded: false)]
    [LabelText("在屏幕底下使用位置标记")]
    public bool UseEnemyPositonSign = false;
    [Tooltip("是否在启动游戏时，将ShootingObject里所有指定的对象全部设定为不启用状态。")]
    [FoldoutGroup("其他", expanded: false)]
    [LabelText("启动游戏时自关闭")]
    public bool ObjectDisactive = true;
    [FoldoutGroup("敌人符卡移动状况", expanded: false)]
    [LabelText("启用符卡移动器")]
    public bool enableThen = false;

    [FoldoutGroup("敌人符卡移动状况", expanded: false)]
    [LabelText("于使用符卡前重置")]
    public bool resetBeforeSpellCard = true;
    [FoldoutGroup("敌人符卡移动状况", expanded: false)]
    [LabelText("可复用的移动器")]
    public bool resetReuse = true;
    [FoldoutGroup("敌人符卡移动状况", expanded: false)]
    [LabelText("重置时要移动到的点")]
    public Vector2 resetPoint = Vector2.zero;
    [FoldoutGroup("敌人符卡移动状况", expanded: false)]
    [LabelText("最大使用时间")]
    public float maxMovementFrame = 200;
    [FoldoutGroup("敌人符卡移动状况", expanded: false)]
    public List<CharacterMovementFrame> frameSetting = new List<CharacterMovementFrame>();
    [FoldoutGroup("GUI方面", expanded: false)]
    private BossPositionSign EnemyPositionSign;


    public delegate void SpellCardEvent(List<Shooting> ShootingList, SpellCard TargetSpellCard, Enemy Character);

    public SpellCardEvent OnSpellCardUsage;
    public SpellCardEvent OnSpellCardUsing;
    public SpellCardEvent OnSpellCardDestroy;
    public SpellCardEvent BeforeNextSpellCard;

    static public float WaittingTime = 2;
    private Animator SpellAnimator;
    private long _bouns = 0;
    private int LivesRemaining = 0;
    private bool OnceUse = false;
    private float _Blend = 0;
    private static GameObject _dynamicPicObject = null;
    void Start()
    {
        InitSpellCard();
    }
    [Sirenix.OdinInspector.Button]
    public void AddAndUseSpellCard()
    {
        if (Application.isPlaying)
        {
            InitSpellCard();
            UseSpellCard();
        }
    }
    [Sirenix.OdinInspector.Button]
    public void InitSpellCard() {

        if ( !Application.isPlaying || Character.SpellCardList.Contains(this))
            return;
        
        if (DynamicPictureObject != null)
            DynamicPictureObject.SetActive(false);
       

        Character.SpellCardList.Add(this);
        Character.SpellCardListIndex.Add(SpellIndex);
        
        SpellAnimator = Global.SpellCardShow_A.GetComponent<Animator>();
        if (ObjectDisactive)
            for (int i = 0; i != ShootingObject.Length; ++i)
                ShootingObject[i].SetActive(false);
        BOSSHP = Global.bossHP.gameObject;
        TimerScript = Global.spellCardTimer;
        EnemyPositionSign = Global.EnemySign;
    }
    public static GameObject GetClonedDynamicPictureObject()
    {
        return _dynamicPicObject;
    }
  
    [Sirenix.OdinInspector.Button]
    public void JumpToThisSpellCard() {
        if (Application.isPlaying)
        UseSpellCard(true);
    }

    public void UseSpellCard(bool debugSpellCard = false, bool updateHPBar = false)
    {
        if (debugSpellCard && Global.SpellCardNow != null)
        {
            if (ObjectDisactive)
                for (int i = 0; i != Global.SpellCardNow.ShootingObject.Length; ++i)
                    Global.SpellCardNow.ShootingObject[i].SetActive(false);
            StartCoroutine(Global.GameObjectPool_A.DestroyBullets(true, false));
            Global.SpellCardNow.Use = false;
            Global.SpellCardNow.SpellCardUsingTime = 0;
            Global.bossHP.InstallHPBar();
        }
        if (TimeSpell)
            Global.isTimeSpell = true;
        else
            Global.isTimeSpell = false;
        MoveMethod _movement = Character.GetMovementController();
        if (_movement != null)
        {
            _movement.resetPoint = resetPoint;
            _movement.ResetMovement(resetBeforeSpellCard);
            _movement.ResetPoint = resetReuse;
            _movement.frameSetting = frameSetting;
        }
        WriteSpellDataToXMLFile();
        Global.SpellCardNow = this;
        Global.SpellCardExpressing = true;
        if (EnemyPositionSign != null && UseEnemyPositonSign == true)
        {
            if (UseEnemyPositonSign)
            EnemyPositionSign.Target = Character;
        }
        Timer.Second = Time;
        Timer.MinSecond = MixSecond;
        Timer.MaxSecond = Time;
        Timer.MaxMinSecond = MixSecond;
        SpellCardUsingTime = 0;
        LivesRemaining = Global.PlayerLive_A;
        Use = true;
        Character.MaxHP = MaxHP;
        Character.HP = MaxHP;
        if (updateHPBar)
            Global.bossHP.InstallHPBar();
        Global.SpellFailed = false;
        if (SpellCardBG != null)
        {
            SpellCardBG.SetActive(true);
            if (HideDefaultBackGround)
                if (Global.GameBg != null)
                    Global.GameBg.SetActive(false);
        }
        if (OnSpellCardUsage != null)
            OnSpellCardUsage(ResetShooting, this, Character);
        foreach (GameObject a in ShootingObject)
        {
            a.SetActive(true);
        }

        maxFrame = (Timer.Second -4)* 60 + Timer.MinSecond / 100 * 60;
        if (UseTimer)
        {
            if (NoTimeLimit == false)
            {
                if (TimerScript == null)
                {

                    Debug.LogError("必须指定时间脚本原型，符卡系统无法执行");
                    return;
                }
                TimerScript.gameObject.SetActive(true);
                TimerScript.gameObject.GetComponent<Animator>().Play("Timer_Approaching", 0, 0);
                if (Timer.Second < 10)
                    TimerScript.TimeWillUp = true;
                TimerScript.Count = true;

                if (Timer.Second > TimerScript.maxTime)
                {
                    TimerScript.SecondText.text = TimerScript.maxTime.ToString();
                }
                if (MixSecond < 10)
                    TimerScript.MinSecondText.text = ".0" + ((int)Timer.MinSecond).ToString();
                else
                    TimerScript.MinSecondText.text = "." + ((int)Timer.MinSecond).ToString();
                if (Timer.Second < 10)
                {
                    TimerScript.SecondText.text = "0" + Timer.Second.ToString();

                }
                else
                    TimerScript.SecondText.text = Timer.Second.ToString();
            }
        }
        if (UseHP)
        {
            if (BOSSHP == null)
            {
                BOSSHP = GameObject.Find("BOSSHP");
                if (BOSSHP == null)
                {
                    Debug.LogError("必须指定BOSS血条UI原型，符卡系统无法执行");
                    return;
                }
            }
            BOSSHP HPUI = BOSSHP.GetComponent<BOSSHP>();
            HPUI.FrontImage.fillAmount = 0;
            HPUI.Boss = Character;
            HPUI.Spellcard = this;
            HPUI.BackImage.rectTransform.position = HPUI.Camera.WorldToScreenPoint(HPUI.Boss.transform.position);
            BOSSHP.SetActive(true);
        }
        if (ShowAnimation)
        {
            if (Character.SpellCardSound != null)
                Character.SpellCardSound.Play();
            Global.SpellCardShow_A.SetActive(true);
            Global.SpellCardName_A.text = Name;
            Global.CharacterImage_Ref.sprite = CharacterSprite;
            if (DynamicPictureObject != null)
            {

                DynamicPictureObject.SetActive(true);
            }
            if (animationIndex != 0)
            {

                Global.SpellCardShow_A.GetComponent<Animator>()
                    .PlayInFixedTime("SpellCardBegin" + animationIndex, 0, 0);
            }

            else
            {
                Global.SpellCardShow_A.GetComponent<Animator>().PlayInFixedTime("SpellCardBegin", 0, 0);
            }
        }
        else
        {
            Global.SpellCardExpressing = false;
        }
    }
    IEnumerator NextSpell()
    {

        SpellCardUsingTime = 0;
        Bouns.SetBouns(ScoreBounsNumber, Character.gameObject.transform.position, Bouns.BounsType.Score);
        Bouns.SetBouns(PowerBounsNumber, Character.gameObject.transform.position, Bouns.BounsType.Power);
        Bouns.SetBouns(LiveBounsNumber, Character.gameObject.transform.position, Bouns.BounsType.LifePrice);
        if (DonDestroyBullets == false)
        {
            StartCoroutine(Character.ObjectPoolRef.DestroyBullets(true, true));
        }
        if (ResetShooting == null)
        {
            Debug.LogWarning("可重置的发射器列表是空的，请添加它（在Resetshooting添加那些需要过一段时间重新启用的发射器）");
            yield return null;
        }
        else
        {
            for (int i = 0; i != ResetShooting.Count; i++)
            {
                if (ResetShooting[i] == null)
                    continue;
                if (ResetShooting[i].Continue)
                {
                    continue;
                }
                Destroy(ResetShooting[i]);
            }

            if (OnSpellCardDestroy != null)
                OnSpellCardDestroy(ResetShooting, this, Character);
        }
        yield return new WaitUntil(() => !Global.GamePause);
        yield return new WaitUntil(() => !Global.WrttienSystem);
        yield return new WaitForSeconds(nextSpellCardWaitingTime);
        
        if (UseTimer && NoTimeLimit == false)
            if (TimerScript.gameObject.activeSelf)
                TimerScript.gameObject.GetComponent<Animator>().Play("Timer_Disapproaching", 0, 0);
        var index = Character.SpellCardList.FindIndex((x) => x == this);
        Character.SpellCardList.RemoveAt(index);
        Character.SpellCardListIndex.RemoveAt(index);
        if (Character.SpellCardListIndex.Count != 0)
        {
            if (BeforeNextSpellCard != null)
                BeforeNextSpellCard(ResetShooting, Character.SpellCardList[
                    Character.SpellCardListIndex.FindIndex((x) => x == Global.FindMin(Character.SpellCardListIndex))], Character);

            yield return new WaitForSeconds(WaittingTime);
            yield return new WaitUntil(() => !Global.GamePause);
            Character.SpellCardList[
                Character.SpellCardListIndex.FindIndex((x) => x == Global.FindMin(Character.SpellCardListIndex))]
                .UseSpellCard();
        }
        else
        {
            if (EnemyPositionSign != null && UseEnemyPositonSign == true)
            {
                EnemyPositionSign.Target = null;
            }
        }
        if (DonDestroySpell == false)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);

        StopCoroutine("NextSpell");
    }
    /// <summary>
    /// 重新使用这张符卡，所有的位于符卡对象子级下的发射器将会被重新初始化，警告：不包括脚本的初始化。要初始化变量，请在RollBack函数处添加你的变量初始化语句。ScoreZero将会令玩家的分数为0.
    /// </summary>
    /// <param name="ScoreZero"></param>
    public void ReuseSpellCard(bool ScoreZero)
    {
        ResetTotalFrame = 0;
        if (Global.SpellCardExpressing)
            return;
        foreach (GameObject a in ShootingObject)
        {
            Transform[] B = a.GetComponentsInChildren<Transform>(false);
            for (int iB = 0; iB != B.Length; ++iB)
            {
                Shooting[] A = B[iB].gameObject.GetComponentsInChildren<Shooting>(false);
                if (A != null)
                {
                    for (int i = 0; i != A.Length; ++i)
                    {
                        if (A[i] == null || A[i].gameObject.activeSelf == false)
                            continue;
                        if (!A[i].Canceled)
                            A[i].enabled = true;
                        A[i].TotalFrame = 0;
                        A[i].ResetCountTime();
                        A[i].ReturnOriginalData(true, true);
                    }
                }
                for (int c = 0; c != ResetShooting.Count; ++c)
                {
                    if (ResetShooting[c] == null)
                        continue;
                    ResetShooting[c].enabled = true;
                    ResetShooting[c].TotalFrame = 0;
                    ResetShooting[c].ResetCountTime();
                    ResetShooting[c].ReturnOriginalData(true, true);

                }
                if (Credentials.DataList != null)
                    for (int i = 0; i != Credentials.DataList.Count; ++i)
                    {
                        Credentials.DataList[i].Ref.enabled = Credentials.DataList[i].DefaultEnabled;
                        Credentials.DataList[i].Ref.transform.localPosition = Credentials.DataList[i].DefaultPostion;
                    }
            }
        }
        StartCoroutine(Global.GameObjectPool_A.DestroyBullets(true, false));
        if (ScoreZero)
        {
            Global.Score = 0;
            Global.TrialTime--;
            Global.MaxBounsScore = 0;
            Global.graze = 0;
        }
        this.UseSpellCard();

    }
    void Update()
    {
        if (Use == false || Global.GamePause || Global.WrttienSystem)
            return;
        SpellCardUsingTime += 1 * Global.GlobalSpeed;
        if (Global.PlayerLive_A > LivesRemaining)
        {
            LivesRemaining = Global.PlayerLive_A;
        }
        if (Global.PlayerLive_A < LivesRemaining)
        {
            Global.SpellFailed = true;
            LivesRemaining = Global.PlayerLive_A;
            Debug.Log("Died!");
        }
        if (!Global.SpellCardExpressing && ShowAnimation)
        {
            if (Global.PlayerObject.transform.position.y > 1.5f)
            {
                _Blend = Mathf.Lerp(_Blend, 1, 0.2f);
                if (SpellAnimator.isActiveAndEnabled)
                    SpellAnimator.SetFloat("Alpha", _Blend);
            }
            else
            {
                _Blend = Mathf.Lerp(_Blend, 0, 0.2f);
                if (SpellAnimator.isActiveAndEnabled)
                    SpellAnimator.SetFloat("Alpha", _Blend);
            }
        }
        ++ResetTotalFrame;
        if (ResetTotalFrame >= ResetTime)
        {
            if (resetShootingDestroyingBullets)
                StartCoroutine(Global.GameObjectPool_A.DestroyBullets(false));
            for (int i = 0; i != ResetShooting.Count; ++i)
            {
                if (ResetShooting[i] == null || ResetShooting[i].gameObject.activeSelf == false)
                    continue;
                ResetShooting[i].TotalFrame = 0;
                ResetShooting[i].CanShoot = true;
                ResetShooting[i].ResetCountTime();
                ResetShooting[i].ReturnOriginalData(false);
                int a = Credentials.SearchShootingIndex(ResetShooting[i]);
                if (a != -1)
                {
                    ResetShooting[i].enabled = Credentials.DataList[a].DefaultEnabled;
                    ResetShooting[i].transform.localPosition = Credentials.DataList[a].DefaultPostion;
                }
            }

            ResetTotalFrame = 0;
        }
        if (OnSpellCardUsing != null)
            OnSpellCardUsing(ResetShooting, this, Character);
        if (UseTimer)
        {
            if (Global.SpellFailed == false)
            {
                if (TimeSpell == false)
                {
                
                    _bouns = (long)Mathf.Lerp(MinBouns, MaxBouns, ((Timer.MinTime + Timer.Second * 1000.0f) / 16.66666f ) / Mathf.Abs( maxFrame)) * 10;
                    float e = _bouns / 10;
                    e = Mathf.Ceil(e);
                    _bouns = (long)(e * 10);
                    if (_bouns > (MaxBouns + MinBouns) * 10)
                        _bouns = (MaxBouns + MinBouns) * 10;
                }
                else
                {

                    _bouns = (MaxBouns + MinBouns) * 10;
                }
                Global.SpellCardBouns_A.text = _bouns.ToString();
            }
            else
            {
                _bouns = 0;
                Global.SpellCardBouns_A.text = "Failed";

            }

            if (Timer.Second < 0 && !NoTimeLimit)
            {
                Character.HP = 0;
                if (!TimeSpell)
                    _bouns = 0;
                if (TimeSpell == false)
                {
                    Global.SpellFailed = true;
                    Debug.Log("Time Out!");
                }
            }

        }
        if (Character.HP <= 0)
            BOSSHP.SetActive(false);
        if (Character.HP <= 0 && OnceUse == false && TimeSpell == false)
            SpellDie();
        if (Timer.Second < 0 && OnceUse == false && TimeSpell == true)
            SpellDie();
    }
    /// <summary>
    /// 符卡击破事件。
    /// </summary>
    public void SpellDie()
    {
        OnceUse = true;
        Global.isTimeSpell = false;
        if (Character.SpellCardList.Count == 1 && ChangeRoadSign)
            Global.RoadBossStage = false;
        if (ShowTip == true)
        {

            Global.SpellCardTip_A.SpellCardTip.gameObject.SetActive(true);
            Global.SpellCardTip_A.SpellCardTip.text =
                Global.SpellFailed ? "SpellCard Bouns Failed" : "Get SpellCard Bouns Successfully";
            if (Global.SpellFailed)
            {

                Global.SpellCardTip_A.ScoreBounsTip.text = "";
            }
            else
            {
                WriteSpellDataToXMLFile(true);
                Global.SpellCardTip_A.ScoreBounsTip.text = (_bouns).ToString();
            }
        }
        if (SpellCardBG != null)
        {
            SpellCardBG.SetActive(false);
            if (Global.GameBg != null)
                Global.GameBg.SetActive(true);
        }
        foreach (GameObject a in ShootingObject)
        {
            a.SetActive(false);
        }
        BOSSHP.SetActive(false);
        if (Character != null && Global.isBossPictureShowing)
        {
            Character.isGone = true;
            Global.BossMask.Set(Character.Name, Character.Break);
        }
        if (UseTimer && NoTimeLimit == false)
        {
            Timer.Second = 0;
            Timer.MinSecond = 0;
            TimerScript.Count = false;
        }
        Global.AddPlayerScore(_bouns);
        if (ShowAnimation)
            if (Global.SpellCardShow_A.activeSelf)
                Global.SpellCardShow_A.GetComponent<Animator>().PlayInFixedTime("SpellCardOver", 0, 0);
        Global.isTimeSpell = false;
        if (nextSpell)
            StartCoroutine(NextSpell());
    }
    /// <summary>
    /// 写符卡数据进入一个XML文件中。
    /// </summary>
    /// <param name="success"></param>
	public void WriteSpellDataToXMLFile(bool success = false)
    {
        if (!spellRecord)
            return;
        if (File.Exists(Application.streamingAssetsPath + @"\SpellData\" + Global.PlayerObject.GetComponent<Character>().CharacterName + "_" + XmlfileName))
        {
            XmlDocument document = new XmlDocument();
            document.Load(Application.streamingAssetsPath + @"\SpellData\" + Global.PlayerObject.GetComponent<Character>().CharacterName + "_" + XmlfileName);
            XmlNode d = document.SelectSingleNode("SPELLCARD_DATA");
            int i = System.Convert.ToInt32(d["SpellCardInfomation"].Attributes["SpellCardTrialTime"].InnerText);
            int b = System.Convert.ToInt32(d["SpellCardInfomation"].Attributes["SpellCardSuccessfulTime"].InnerText);
            if (success == false)
            {
                ++i;
                d["SpellCardInfomation"].Attributes["SpellCardTrialTime"].InnerText = i.ToString();
            }
            else
            {
                b++;
                d["SpellCardInfomation"].Attributes["SpellCardSuccessfulTime"].InnerText = b.ToString();
            }
            Global.SpellCardHistory_A.text = b.ToString() + "/" + i.ToString();
            document.Save(Application.streamingAssetsPath + @"\SpellData\" + Global.PlayerObject.GetComponent<Character>().CharacterName + "_" + XmlfileName);
            //document.SelectSingleNode ();
        }
        else
        {
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", null));
            XmlElement Root = document.CreateElement("SPELLCARD_DATA");
            document.AppendChild(Root);
            XmlElement Book = document.CreateElement("SpellCardInfomation");
            Book.SetAttribute("SpellCardName", Name);
            Book.SetAttribute("SpellCardTrialTime", 1.ToString());
            Book.SetAttribute("SpellCardSuccessfulTime", 0.ToString());
            Root.AppendChild(Book);
            document.Save(Application.streamingAssetsPath + @"\SpellData\" + Global.PlayerObject.GetComponent<Character>().CharacterName + "_" + XmlfileName);
            Global.SpellCardHistory_A.text = "0/1";
        }
    }
}
[System.Serializable]
public class ShootingData
{
    public List<SingleShootingData> DataList = new List<SingleShootingData>();

    public int SearchShootingIndex(Shooting A)
    {
        return DataList.FindIndex((x) =>
        {
            return x.Ref == A;
        });
    }
}
[System.Serializable]
public class SingleShootingData
{
    public bool DefaultEnabled = false;
    public Vector3 DefaultPostion;
    public Shooting Ref;
    public SingleShootingData(Vector3 P, bool Enable, Shooting RefS)
    {
        DefaultPostion = P;
        DefaultEnabled = Enable;
        Ref = RefS;
    }

}
