//using UnityEditor.Animations;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    // Use this for initialization
    public List<Sprite> SpriteList = new List<Sprite>();
    static public List<Sprite> AllSprites = new List<Sprite>();
    static public List<Enemy> AllEnemy = new List<Enemy>();
    static public GameObject SpellCardShow_A;
    static public GameObject PlayerObject;
    static public GameObject CommandWidelyUsed_A;
    static public GameObject GameBg;
    static public BOSSHP bossHP;
    public BOSSHP defaultBOSSHP;
    static public Timer spellCardTimer;
    public BossPositionSign bossPositionSign_A;
    static public BossPositionSign EnemySign;
    static public Text SpellCardName_A;
    static public Text SpellCardHistory_A;
    static public Text SpellCardBouns_A;
    static public Text AddScore;
    static public AudioClip BossWillDie_A;
    static public AudioClip BossNormalDamage_A;
    static public long Score = 0;
    static public int maxLive = 8;
    static public int TrialTime = 10;
    static public int PlayerLive_A = 2;
    static public int SpellCard = 3;
    static public int LivePrice = 0;
    static public int SpellCardPrice = 0;
    static public int MaxLivePrice = 5;
    static public int MaxSpellCardPrice = 3;
    static public int MaxLive = 8;
    static public int MaxSpellCard = 8;
    static public int graze = 0;
    static public int MaxBounsScore = 5000;
    static public int RetryRemaining = 3;
    static public int StageIndex = 1;
    static public float GlobalSpeed = 1;
    static public float Power = 0f;
    static public float maxPower_A = 4.0f;
    static public int AddedScore = 0;
    static public string alphaSpritePath;
    static public int ScoreBounsCollectionCount = 0;
    static public bool GamePause = false;
    static public bool SpellFailed = false;
    static public bool WrttienSystem = false;
    static public bool dialoging = false;
    static public bool isBossPictureShowing = false;
    static public bool RoadBossStage = false;
    static public Bullet BulletOrigin;
    static public Sprite FullPowerBouns;
    static public Sprite PowerBouns;
    static public Sprite ScoreBouns;
    static public Sprite LivePriceBouns;
    static public Camera MainCamera;
    static public GameTip Tip;
    static public ObjectPool GameObjectPool_A;
    static public Image CharacterImage_Ref;

    static public GameObject playerBulletBased;
    static public GameObject playerFollowBulletBased;
    static public Material[] MaterialCollections_A;
    static public StageClear stageClear_A;
    static public GameOver GameOverScript_A;
    static public SpellTip SpellCardTip_A;
    static public RuntimeAnimatorController[] AnimationControlFile_A;
    static public Animator SideCardAnim;
    static public SpellCard SpellCardNow;
    static public Bullet BasicBullet;
    static public Dialog WindowDialog_A;
    static public AudioSource BGM;
    static public Vector2 ScreenX_A;
    static public Vector2 ScreenY_A;
    static public Animator BossPicture, PlayerPicture, ScoreAnimator;
    static public PlayerPicMask PlayerMask, BossMask;
    static public AudioClip Extend, FullModePower;
    static public GameObject RadioClipNormal;
    static public int MissCount = 0;
    static public string CommandString = "";
    static public bool isGameover = false;
    static public bool isTimeSpell = false;
    static public long AddedStore;
    static public float SpellCardTimeSpeed = 1;
    static public List<Sprite> DestroySprites = new List<Sprite>();
    static public List<bool> GlobalValue_bool = new List<bool>();
    static public List<bool> NormalBoolValue = new List<bool>();
    static public List<int> GlobalValue_int = new List<int>();
    static public List<float> GlobalValue_float = new List<float>();
    static public List<StageControl> StageList_A = new List<StageControl>();
    static public Character PlayerCharacterScript;
    /// <summary>
    /// 在UNITY運行遊戲的性能瓶頸下的彈幕，可以依據這個參數來修改子彈的移動速度。例如UNITY下面測試是30幀數，彈幕測試時覺得舒服，可以調整此參數使子彈在獨立EXE運行的時候的速度減半，以免造成調試困難。
    /// 
    /// </summary>
    static public float GlobalBulletSpeed = 1f;
    static public long MaxScore = 2000000;
    public Bullet BulletOrigin_A;
    public Animator BossPicture_A, PlayerPicture_A, ScoreAnimator_A;
    public PlayerPicMask PlayerMask_A, BossMask_A;
    public Vector2 ScreenX;
    public Vector2 ScreenY;
    public AudioSource BGM_A;
    public AudioClip Extend_A, FullModePower_A;
    public GameObject RadioClipNormal_A;
    public List<StageControl> stageList = new List<StageControl>();
    public Sprite FullPowerSprite;
    public Sprite PowerSprite;
    public Sprite ScoreSprite;
    public Sprite LivePriceSprite;
    public GameObject Player;
    public GameObject Bg_A;
    public GameObject CommandWidelyUsed;
    public GameObject GamePausePanel;

    public GameObject playerBullet;
    public GameObject playerFollowBullet;
    public Timer SpellCardTimer;
    public AudioClip BossWillDie;
    public AudioClip BossNormalDamage;
    public GameObject SpellCardShow;
    public StageClear stageClear;
    public GameTip Tip_A;
    public Text SpellCardName;
    public Text SpellCardHistory;
    public Text SpellCardBouns;
    public Text AddScoreOrginal;
    public SpellTip SpellCardTip;
    public Image CharacterImage;
    public Camera YourCamera;
    public ObjectPool GameObjectPool;
    public Dialog WindowDialog;
    public GameOver GameOverScript;
    public Material[] MaterialCollections;
    public RuntimeAnimatorController[] AnimationControlFile;
    public Animator SideCardAnim_A;
    public int PlayerScore = 0;
    public int PlayerLive = 2;
    public int PlayerSpellCard = 3;
    public int PlayerLivePrice = 0;
    public int PlayerSpellCardPrice = 0;
    public int Playergraze = 0;
    public int PlayerMaxBounsScore = 5000;
    public float Playerpower = 0f;
    public float maxPower = 4.0f;
    public float DEBUG_GlobalSpeed = 1;
    public bool DEBUG_ChangeGlobalSpeed = false;
    public List<Sprite> DestroySprites_A = new List<Sprite>();
    static bool doOnce = false;
    // 以上为旧版本内容
    [HideInInspector]
    static public Animator ScoreShowerAnimationControl;
    float timecount = 0;

    Animator _cameraAnim;
    [SerializeField]
    static public Rank GameRank = Rank.NORMAL;
    static public int RANKCOUNT = 6;
    // 场景不提供难度的更改，要求在Title画面中更改。
    public enum Rank
    {
        EASY = 0,
        NORMAL = 1,
        HARD = 2,
        LUNATIC = 3,
        EXTRA = 4,
        OVERDRIVE = 5
    }
    static public bool SpellCardExpressing = false;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(ScreenX.x, -99), new Vector2(ScreenX.x, 99));
        Gizmos.DrawLine(new Vector2(ScreenX.y, -99), new Vector2(ScreenX.y, 99));
        Gizmos.DrawLine(new Vector2(-99, ScreenY.x), new Vector2(99, ScreenY.x));
        Gizmos.DrawLine(new Vector2(-99, ScreenY.y), new Vector2(99, ScreenY.y));
    }

    static public void JumpStage(int target = 0)
    {
        StageIndex = target;
        StageList_A[StageIndex].enabled = true;
    }
    /// <summary>
    /// 关于这个函数，它被用来增加玩家分数。在这里填入想要加的分数。
    /// </summary>
    /// <param name="score">Score.</param>
    static public void AddPlayerScore(long score = 10, bool showScoreIncreament = true)
    {
        Score += score / 10;
        AddedStore += score / 10;
        //   if (AddScore != null){
        //	AddScore.text = "+" + (score).ToString ();

        if (showScoreIncreament)
            ScoreShowerAnimationControl.PlayInFixedTime("ScoreAddScore", 0, 0);
        else
        {
            Score += score / 10;
        }
    }
    static public void ShowCharCard()
    {
        SideCardAnim.SetBool("Show", true);

    }
    static public void HideCharCard()
    {
        SideCardAnim.SetBool("Show", false);
    }
    void Start()
    {
        ScoreShowerAnimationControl = AddScore.GetComponent<Animator>();
        AnimationControlFile_A = AnimationControlFile;
        //游戏启动时，屏蔽输入法
        Win32Help.SetImeEnable(false);
    }
    void Awake()
    {
        playerBulletBased = playerBullet;
        playerFollowBulletBased = playerFollowBullet;
        Application.targetFrameRate = 60;
        // 阻止游戏被迫暂停的情况
        WrttienSystem = false;
        GamePause = false;
        isGameover = false;
        GlobalSpeed = 1.0f;
        StressTester.c = 0;
        isBossPictureShowing = false;
        TrialTime = 10;
        AllEnemy.Clear();
        RoadBossStage = false;
        isTimeSpell = false;
        doOnce = false;
        bossHP = defaultBOSSHP;
        spellCardTimer = SpellCardTimer;
        EnemySign = bossPositionSign_A;
        // 初始化程序代码
        alphaSpritePath = Application.dataPath + @"Assets\Graphics\Effect\Alpha.png";
        ScreenX_A = ScreenX;
        ScreenY_A = ScreenY;
        SpellCardTip_A = SpellCardTip;
        LivePriceBouns = LivePriceSprite;
        stageClear = stageClear_A;
        BGM = BGM_A;
        DestroySprites = DestroySprites_A;
        GameBg = Bg_A;

        BossMask = BossMask_A;
        PlayerMask = PlayerMask_A;
        RadioClipNormal = RadioClipNormal_A;
        CommandWidelyUsed_A = CommandWidelyUsed;
        MaterialCollections_A = MaterialCollections;
        Score = PlayerScore;
        SideCardAnim = SideCardAnim_A;
        ScoreAnimator = ScoreAnimator_A;
        SpellCardHistory_A = SpellCardHistory;
        stageList = StageList_A;
        Tip = Tip_A;
        BulletOrigin = BulletOrigin_A;
        PlayerLive_A = PlayerLive;
        SpellCard = PlayerSpellCard;
        LivePrice = PlayerLivePrice;
        Extend = Extend_A;
        FullModePower = FullModePower_A;
        AddScore = AddScoreOrginal;
        SpellCardPrice = MaxSpellCardPrice;
        graze = Playergraze;
        MaxBounsScore = PlayerMaxBounsScore;
        Power = Playerpower;
        //	PlayerObject = Player;
        MainCamera = YourCamera;
        AllSprites = SpriteList;
        GameObjectPool_A = GameObjectPool;
        SpellCardName_A = SpellCardName;
        SpellCardShow_A = SpellCardShow;
        SpellCardBouns_A = SpellCardBouns;
        BossWillDie_A = BossWillDie;
        GameOverScript_A = GameOverScript;
        BossNormalDamage_A = BossNormalDamage;
        FullPowerBouns = FullPowerSprite;
        PowerBouns = PowerSprite;
        ScoreBouns = ScoreSprite;
        CharacterImage_Ref = CharacterImage;
        maxPower_A = maxPower;
        WindowDialog_A = WindowDialog;
        BossPicture = BossPicture_A;
        PlayerPicture = PlayerPicture_A;
        bossHP = defaultBOSSHP;
        MissCount = 0;
      
        BasicBullet = Global.GameObjectPool_A.BulletBased.GetComponent<Bullet>();
    }

    static public int FindMin(List<int> a)
    {
        int min = 0xfffffff;
        for (int i = 0; i != a.Count; ++i)
        {
            if (a[i] < min)
                min = a[i];
        }
        return min;
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerLive_A < 0)
            PlayerLive_A = 0;
        if (PlayerLive_A > maxLive)
            PlayerLive_A = maxLive;
        if (WrttienSystem == false && !GamePause)
        {
            if (Power > maxPower)
            {
                timecount = timecount + 1 * GlobalSpeed;
                if (timecount > 10 && WrttienSystem == false)
                {
                    Power = Mathf.Clamp(Power - 0.01f, maxPower, 0xfffffff);
                    timecount = 0;
                }

            }
            if (Power >= maxPower_A && !doOnce)
            {

                Tip.AS.clip = FullModePower;
                Tip.SetTitle(FullModePower, "Full Power Mode!!");
                doOnce = true;
            }
            else if (Power < maxPower_A)
            {
                doOnce = false;
            }
        }
        if (LivePrice >= MaxLivePrice && PlayerLive_A < MaxLive)
        {
            LivePrice -= MaxLivePrice;
            PlayerLive_A++;
            Tip.AS.clip = Extend;
            Tip.SetTitle(Extend, "Extend!!");
            Character _char = PlayerObject.GetComponent<Character>();
            if (PlayerLive >= 2 && Global.SpellCardNow != null)
                PlayerMask.Set(_char.CharacterName, _char.Normal);
        }
        if (DEBUG_ChangeGlobalSpeed)
            GlobalSpeed = DEBUG_GlobalSpeed;
    }
    public int ArrayIndexLimit(int index, int max)
    {
        return Mathf.Clamp(index, 0, max);
    }
}
