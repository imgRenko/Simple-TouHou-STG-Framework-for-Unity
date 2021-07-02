using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/敌人类/敌人基本数据设置")]
public class Enemy : STGComponent
{
    [FoldoutGroup("敌人强度", expanded: false)]
    [LabelText("敌人目前生命值")]
    public float HP = 200;
    [FoldoutGroup("敌人强度", expanded: false)]
    [LabelText("敌人最大生命值")]
    public float MaxHP = 200;

    private Vector2 MovePoint;
    [HideInInspector]
    public int ID = 0;
    [HideInInspector]
    public bool Moving = false;
    [HideInInspector]
    public bool ToRight = false;
    [FoldoutGroup("敌人移动设定", expanded: false)]
    public MoveCurves MoveType = MoveCurves.Slerp;
    [HideInInspector]
    public float Speed = 0.05f;
    [FoldoutGroup("敌人动画", expanded: false)]
    [LabelText("播放动画时反转X轴")]
    public bool FilpX = false;
    [FoldoutGroup("敌人个人信息", expanded: false)]
    [LabelText("敌人名称")]
    public string Name = "Marisa";
    [FoldoutGroup("立绘显示", expanded: false)]
    public bool PictrueMaskDisplay = false;
    [FoldoutGroup("敌人立绘显示", expanded: false)]
    public Vector2 PictrueMaskOffset;
    [FoldoutGroup("敌人立绘显示", expanded: false)]
    [LabelText("敌人正常立绘")]
    [PreviewField(196, ObjectFieldAlignment.Left)]
    public Sprite Normal;
    [FoldoutGroup("敌人立绘显示", expanded: false)]
    [LabelText("敌人击败立绘")]
    [PreviewField(196, ObjectFieldAlignment.Left)]
    public Sprite Deadly;
    [FoldoutGroup("敌人立绘显示", expanded: false)]
    [LabelText("敌人受挫立绘")]
    [PreviewField(196, ObjectFieldAlignment.Left)]
    public Sprite Break;
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("允许体术玩家")]
    [Tooltip("这个敌人是否具有判定，即玩家经过时是否以中弹处理。")]
    public bool CanDestroyPlayer = true;
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("作为BOSS出现")]
    public bool isBoss = false;
    [Space]
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("造成伤害时音效")]
    public AudioSource DamageSound;
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("使用符卡时音效")]
    public AudioSource SpellCardSound;
    [HideInInspector]
    public ObjectPool ObjectPoolRef;
    [FoldoutGroup("敌人属性设定(高级)", expanded: false)]
    [LabelText("角色图像显示器")]
    public SpriteRenderer SpriteRender;
    [FoldoutGroup("敌人属性设定(高级)", expanded: false)]
    [LabelText("角色动画控制器")]
    public Animator AnimationController;
    public delegate void EnemyEvent(Enemy E);
    public EnemyEvent WhenEnemyMoving;
    public EnemyEvent WhenEnemyStopMoving;
    public EnemyEvent WhenEnemyDamaged;
    public EnemyEvent WhenEnemyKilled;
    public EnemyEvent WhenEnemyStarts;
    public EnemyEvent WhenEnemyStartsBarrage;
    public EnemyEvent WhenEnemyUpdated;
    [Space]
    [Tooltip("敌人不在屏幕出现的时候，销毁这个敌人。")]
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("出屏销毁")]
    public bool destroyWhenOutScreen = false;
    [HideInInspector]
    public bool BulletControled;
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("播放造成伤害时音效")]
    public bool UseDamageSound = false;
    [FoldoutGroup("敌人被击败时设定", expanded: false)]
    [LabelText("奖励点个数")]
    public int BounsScoreNumber = 3;
    [FoldoutGroup("敌人被击败时设定", expanded: false)]
    [LabelText("奖励P点个数")]
    public int BounsPowerNumber = 4;
    [FoldoutGroup("敌人被击败时设定", expanded: false)]
    [LabelText("奖励大P点个数")]
    public int BounsFullPowerNumber = 0;
    [FoldoutGroup("敌人被击败时设定", expanded: false)]
    [LabelText("奖励残机碎片个数")]
    public int BounsLivePieceNumber = 0;
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("玩家无敌时不造成任何伤害")]
    public bool NobodyNoDamage;
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("敌人动画播放速度随全局速度变化")]
    public bool ControlAnimSpeed = true;
    [Tooltip("声明符卡的时候，使该敌人处于无敌状态")]
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("声明符卡时无敌")]
    public bool expressingSpellNoBody = false;
    private bool Used = false;
    private float DeadTime;
    private bool badlyDamaged = false;
    private float moveTime = 0;
    [FoldoutGroup("敌人移动设定", expanded: false)]
    [LabelText("初始移动")]
    public float RunTime = 120;
    [FoldoutGroup("敌人移动设定", expanded: false)]
    [LabelText("移动曲线")]
    public AnimationCurve animationCurve;
    private Vector2 moveOriPos;
    [FoldoutGroup("事件总控制")] [LabelText("临时浮点变量")] public List<float> TempValue = new List<float>();
    [FoldoutGroup("事件总控制")] [LabelText("临时浮点变量自增值")] public List<float> TempValueIncrease = new List<float>();
    [FoldoutGroup("事件总控制")] [LabelText("临时布尔变量自增值")] public List<bool> TempValueBool = new List<bool>();
    [HideInInspector]
    public bool isGone;
    public enum MoveCurves
    {
        Line = 0,
        Lerp = 1,
        Slerp = 2,
        SlerpUnclamped = 3
    }
    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("HP为0时将销毁")]
    public bool CanDestroy = true;

    [FoldoutGroup("敌人属性设定", expanded: false)]
    [LabelText("敌人出现后立即播放弹幕")]
    public bool UseBarragesWhenGameStart = true;

    // line 直线匀速运动
    // Lerp 直线减速运动
    // Slerp 曲线均速运动
    // SlerpUnclamped 曲线减速运动。
    // Use this for initialization
    [FoldoutGroup("所持有弹幕", expanded: false)]
    [LabelText("弹幕合集(游戏开始后会自动寻找弹幕并填充，不需要修改此项)")]
    public List<SpellCard> SpellCardList = new List<SpellCard>();
    [HideInInspector]
    public List<int> SpellCardListIndex = new List<int>();
    public enum EventList_Enemy { ONMOVING = 1, ONDAMAGED = 2, ONSTOPMOVING = 3, ONKILLED = 4, ONSTARTS = 5, ONSTARTSBARRAGE = 6 }
    [HideInInspector]
    public EnemyState enemyState;
    private bool _forceDisablemoment;
    private float DefaultZ = 0;
    Character Char;
    Transform enemyTransform;
    public void ClearEventsList(EventList_Enemy t)
    {
        switch ((int)t)
        {
            case 1:
                WhenEnemyMoving = null;
                break;
            case 2:
                WhenEnemyDamaged = null;
                break;
            case 3:
                WhenEnemyStopMoving = null;
                break;
            case 4:
                WhenEnemyKilled = null;
                break;
            case 5:
                WhenEnemyStarts = null;
                break;
            case 6:
                WhenEnemyStartsBarrage = null;
                break;
        }
    }
    void Start()
    {
        enemyState = GetComponentInParent<EnemyState>();
        Char = Global.PlayerObject.GetComponent<Character>();
        animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        enemyTransform = gameObject.transform;
        enemyState = enemyTransform.parent.gameObject.GetComponent<EnemyState>();
        MovePoint = enemyTransform.position;
        DefaultZ = enemyTransform.position.z;

        Global.AllEnemy.Add(this);
        ObjectPoolRef = Global.GameObjectPool_A;
        if (WhenEnemyStarts != null)
            WhenEnemyStarts(this);
        ID = Random.Range(0, 0xfffffff);
    }
    /// <summary>
    /// 让敌人开始使用符卡弹幕，因为使用的是符卡弹幕，所以必须要给这个类里的SpellCardList添加符卡才能使用这个函数。
    /// </summary>
    public void UseBarrages()
    {

        if (SpellCardListIndex.Count != 0)
            SpellCardList[SpellCardListIndex.FindIndex((x) => x == Global.FindMin(SpellCardListIndex))].UseSpellCard();
        if (WhenEnemyStartsBarrage != null)
            WhenEnemyStartsBarrage(this);
        Global.bossHP.InstallHPBar();
        if (!Global.isBossPictureShowing)
            return;
        Global.BossMask.Set(Name, Normal);
        badlyDamaged = false;
    }
    /// <summary>
    /// 得到移动控制器（不是子弹类）
    /// </summary>
    /// <returns>The movement controller.</returns>
    public MoveMethod GetMovementController()
    {
        return enemyState.GetComponent<MoveMethod>();
    }
    /// <summary>
    /// 等价于 Enemy.MaxHP = point;Enemy.HP = point;
    /// </summary>
    /// <param name="point"></param>
    public void SetEnemyHP(float point)
    {
        HP = point;
        MaxHP = HP;
    }
    /// <summary>
    /// 作為BOSS出現，使用特殊動畫。
    /// </summary>
    public void AsBoss()
    {
        Global.BossMask.Offset = PictrueMaskOffset;
        Global.BossPicture.PlayInFixedTime("BossPicture", 0, 0);
        Global.ScoreAnimator.PlayInFixedTime("BossBattleStart", 0, 0);
        Global.BossMask.EnterIn();
        if (SpellCardList.Count == 0)
        {

            Global.BossMask.Set(Name, Break);
        }
        else
            Global.BossMask.Set(Name, Normal);
    }
    public void BossDestroy()
    {
        Global.BossMask.Offset = PictrueMaskOffset;
        Global.BossMask.Back();
        Global.BossPicture.PlayInFixedTime("BossSpriteBack", 0, 0);
        Global.ScoreAnimator.PlayInFixedTime("BossBattleBack", 0, 0);
    }
    /// <summary>
    /// 当自己的子弹进入敌人的判定范围的时候。该函数将被有并且设定了Trigger为True的Collision2D游戏对象所触发
    /// </summary>
    /// <param name="C">C.</param>
    void OnCollisionEnter2D(Collision2D C)
    {
        if ((C.gameObject.tag == "PlayerBullet" || C.gameObject.tag == "DestroyItem") && HP > 0)
        {
            PlayerBullet Cb = C.gameObject.GetComponent<PlayerBullet>();
            float totalHP = 0;

            Cb.animControl.PlayInFixedTime(Cb.DestroyAnimName, 0, 0);
            if (Cb.attacked)
                return;
            Cb.attacked = true;
            if (Char.Invincible && NobodyNoDamage && Char.countGrazeWhenNobody == false)
                return;
            if (expressingSpellNoBody == false || Global.SpellCardExpressing == false)
            {
                totalHP += Char.BulletDamage + Global.Power * Char.DamageDecline;
                totalHP += Char.ExtraDamage;
                Global.AddPlayerScore(10);
                if (Global.Power > Global.maxPower_A)
                {
                    totalHP += Char.BulletDamage / 4.0f;
                    totalHP += Char.ExtraDamage / 4.0f + (Global.Power - Global.maxPower_A) * 2.3f;
                    Global.AddPlayerScore(10);
                }
            }
            HP -= (!Global.SpellCardExpressing ?  totalHP :totalHP*0.01f);

            if (WhenEnemyDamaged != null)
                WhenEnemyDamaged(this);

            if (HP / MaxHP < 0.1f)
            {

                DamageSound.clip = Global.BossWillDie_A;
            }
            else
                DamageSound.clip = Global.BossNormalDamage_A;
            if (UseDamageSound)
                AudioQueue.Play(DamageSound);

        }
        if (C.gameObject.tag == "Player")
        {
            if (CanDestroyPlayer)
                Char.Die();
        }
    }
    /// <summary>
    /// 使角色移动到某一个点，这个函数如果使用了子弹作为移动依据（一些参数只能使用子弹作为移动依据，例如forceClosemovement、resetbulletpostion和Time），那么这个函数不会生效，除非你没有任何脚本来操作角色的位置。Time代表过几秒后再自动恢复使用子弹作为移动依据，-1为不使用
    /// </summary>
    /// <param name="Target">Target.</param>
    /// <param name="forceClosemovement">If set to <c>true</c> force closemovement.</param>
    /// <param name="time">Close Movement Time.</param>
    /// <param name="resetbulletpostion">If set to <c>true</c> resetbulletpostion.</param>
    // Update is called once per frame
    public void Move(Vector2 Target, bool forceClosemovement = false, float time = -1, bool resetbulletpostion = false)
    {
        _forceDisablemoment = forceClosemovement;
        ToRight = Target.x < MovePoint.x;
        if (enemyState.Movement != null)
        {
            if (_forceDisablemoment)
            {
                enemyState.Movement.enabled = false;
                if (time > 0)
                    StartCoroutine(WaitTime(time));
            }
            if (resetbulletpostion)
            {
                Vector3 t = Target;
                t.z = DefaultZ;

                enemyState.Movement.gameObject.transform.position = t;
            }
        }
        MovePoint = Target;
        moveOriPos = enemyTransform.localPosition;
        moveTime = 0;
        /*   if (Havemoved)
           {
               if (WhenEnemyMoving != null)
                   WhenEnemyMoving(this);
               Havemoved = false;
           }*/
    }
    public Vector2 GetMoveTargetPoint() {
        return MovePoint;
    }
    IEnumerator WaitTime(float t)
    {
        yield return new WaitForSeconds(t);
        _forceDisablemoment = false;
        enemyState.Movement.enabled = true;
    }
    private void Update()
    {
        if (UpdateWithSelfComponent)
            UpdateInfo();
    }
    public override void UpdateInfo()
    {
        if (Global.GamePause == true)
        {
            AnimationController.speed = 0;
            return;
        }
        if (UseBarragesWhenGameStart && Used == false)
        {
            UseBarrages();
            Used = true;
        }
        HP = Mathf.Clamp(HP, 0, MaxHP);
        if (HP / MaxHP <= 0.1F || (Global.isTimeSpell && Timer.Second / Timer.MaxSecond < 0.1f))
        {

            if (!badlyDamaged)
            {
                if (Global.isBossPictureShowing && PictrueMaskDisplay)
                    Global.BossMask.Set(Name, Deadly);
            }
            badlyDamaged = true;
        }
        else
        {
            badlyDamaged = false;
        }


        //     if (BulletControled == false)
        //  
        AnimationController.speed = ControlAnimSpeed ? Global.GlobalSpeed : 1;
        if (!Moving)
        {
            moveOriPos = gameObject.transform.localPosition;
            moveTime = 0;
        }
        // 目标点于角色位置的距离大于0.1f的时候，代表角色需要移动，并执行下面的代码
        if (Vector2.Distance(MovePoint, gameObject.transform.localPosition) > 0.05f)
        {
            if (WhenEnemyMoving != null)
                WhenEnemyMoving(this);
            Moving = true;
            // Havemoved = true;
            moveTime += Global.GlobalSpeed;
            if (AnimationController != null && BulletControled == false)
            {
                AnimationController.SetBool("Move", Moving);
                AnimationController.SetBool("Right", ToRight);

            }
            if (!BulletControled)
            {
                Vector3 t = MovePoint;
                t.z = DefaultZ;
                float value = animationCurve.Evaluate(moveTime / RunTime);

                if (moveTime >= RunTime)
                {
                    value = 1;
                    Debug.LogError(string.Format("曲线的描绘状况不正确。第{0}帧的曲线值必须为1", RunTime));
                }
                if (MoveType == MoveCurves.Line)
                    enemyTransform.localPosition = Vector2.MoveTowards(enemyTransform.localPosition, t, (moveOriPos-(Vector2)t).magnitude/RunTime);
                if (MoveType == MoveCurves.Lerp)
                    enemyTransform.localPosition = Vector2.Lerp
                        (moveOriPos, t, value);
                if (MoveType == MoveCurves.Slerp)
                    enemyTransform.localPosition = Vector3.Slerp
                        (moveOriPos, t, value);
                if (MoveType == MoveCurves.SlerpUnclamped)
                    enemyTransform.localPosition = Vector3.SlerpUnclamped
                        (moveOriPos, t, value);
                ToRight = MovePoint.x > enemyTransform.localPosition.x;
            }
            else
            {
               // this.enemyTransform.localPosition = Vector3.zero;
                moveTime = 0;
            }
            if (FilpX)
                SpriteRender.flipX = !ToRight;

        }
        else
        {


            if (WhenEnemyStopMoving != null)
                WhenEnemyStopMoving(this);

            ToRight = false;
            Moving = false;

            if (AnimationController != null)
            {
                AnimationController.SetBool("Move", Moving);
                AnimationController.SetBool("Right", ToRight);
            }

        }
        if (destroyWhenOutScreen)
        {
            if (Bullet.IsOutofScreen((Vector2)this.transform.position))
            {
                DestroyEnemy(false);
            }
        }
        if (isGone)
        {
            DeadTime += Global.GlobalSpeed;

            if (DeadTime >= 120)
            {
                isGone = false;
                DeadTime = 0;
                if (!Global.isBossPictureShowing)
                    return;
                Global.BossMask.Set(Name, Normal);
            }
        }

        if (CanDestroy && HP == 0f)
        {
            Debug.Log("Enemy was Killed");
            DestroyEnemy();
        }
        if (HP == 0f && SpellCardList.Count == 0)
        {
            // 如果敌人销毁后不需要消失滚蛋（例如BOSS），那执行这些代码
            enemyState.EnemyStateNow = EnemyState.State.STATE_KILL;
            if (WhenEnemyKilled != null)
                WhenEnemyKilled(this);
        }

    }
    public void DestroyEnemy(bool bouns = true)
    {
        enemyState.Movement.ClearAllEvent();
        enemyState.Used = false;
        enemyState.EnemyStateNow = EnemyState.State.STATE_NO_USING;
        enemyState.Movement.CommandList.Clear();
        enemyState.Movement.SingleParmCurve.Clear();
        gameObject.transform.parent.gameObject.SetActive(false);
        // ShootingHas 代表其拥有发射器
        Shooting[] ShootingHas = this.GetComponentsInChildren<Shooting>();
        if (bouns)
        {
            Bouns.SetBouns(BounsScoreNumber, this.gameObject.transform.position, Bouns.BounsType.Score);
            Bouns.SetBouns(BounsPowerNumber, this.gameObject.transform.position, Bouns.BounsType.Power);
            Bouns.SetBouns(BounsFullPowerNumber, this.gameObject.transform.position, Bouns.BounsType.FullPower);
            Bouns.SetBouns(BounsLivePieceNumber, this.gameObject.transform.position, Bouns.BounsType.LifePrice);
        }
        BulletEventForSingle[] Events = this.gameObject.GetComponents<BulletEventForSingle>();
        if (WhenEnemyKilled != null)
            WhenEnemyKilled(this);
        enemyState.Movement.TotalLiveFrame = 0;
        ClearAll();
        for (int i = 0; i != Events.Length; ++i)
        {
            if (Events[i] != null)
                Destroy(Events[i]);
        }
        for (int i = 0; i < ShootingHas.Length; i++)
        {
            Destroy(ShootingHas[i].gameObject);
        }
    }
}
