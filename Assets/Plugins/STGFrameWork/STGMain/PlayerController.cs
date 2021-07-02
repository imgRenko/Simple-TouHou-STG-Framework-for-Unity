using System.Runtime.Remoting.Channels;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/自机类/自机移动器")]
public class PlayerController : MonoBehaviour
{
    public Character Player;
    [HideInInspector]
    public GameObject PlayerBulletBased;
    [HideInInspector]
    public GameObject FollowPlayerBulletBased;
    public GameObject[] ShootingPosition;
    public GameObject[] FollowShootingPosition;
    public Animator PointAnimation;
    public Vector2 MinRangeofScreen = new Vector2(0.01f, 0.04f);
    public Vector2 MaxRangeofScreen = new Vector2(0.99f, 0.96f);
    public Sprite BulletSprite;
    public Sprite FollowBulletSprite;
    public bool useSpriteFilp = true;
    public float ShotBulletsinTimes = 0.07f;
    public float NormalSpeed = 1.0f;
    public float RapidSpeed = 3.0f;
    [HideInInspector]
    public float Speed = 3.0f;
    public bool Controled = true;
    public delegate void CharacterEvent(Character tar);
    public event CharacterEvent OnCharacterMoving;
    public event CharacterEvent OnCharacterShooting;
    public event CharacterEvent OnCharacterSlowDown;
    public event CharacterEvent OnCharacterStoping;
    public event CharacterEvent OnCharacterUpdating;
    public event CharacterEvent OnCharacterGrazed;
    bool _moving = false;
    private Transform thisTransform;
    private float _totalframe = 0;
  
    // Use this for initialization
    void Start()
    {
        thisTransform = transform;
        PlayerBulletBased = Global.playerBulletBased;
        FollowPlayerBulletBased = Global.playerFollowBulletBased;
        Player.Controller = this;
    }
    public void UseGrazedEvent()
    {
        if (OnCharacterGrazed != null)
            OnCharacterGrazed(Player);
    }
    void ShootingBullet()
    {
       
        if (Global.GamePause || Global.WrttienSystem || Global.isTimeSpell)
            return;
        if (_totalframe >= ShotBulletsinTimes)
        {
            _totalframe = 0;
        }
        else
        {
            return;
        }

        Player.PlaySound(1, 0);
        for (int i = 0; i != ShootingPosition.Length; ++i)
        {
            if (ShootingPosition[i].activeSelf == false)
                continue;
            PlayerBullet playerBullet = Global.GameObjectPool_A.ApplyPlayerBullet();
          
            if (playerBullet == null)
                return;
            
            playerBullet.Used = true;
            playerBullet.ResetPos((Vector2)ShootingPosition[i].transform.position);
            playerBullet.Follow = false;
            playerBullet.BulletSprite.sprite = BulletSprite;
           //s playerBullet.animControl.PlayInFixedTime("PlayerBulletAnim", 0, 0);
        }
        for (int i = 0; i != FollowShootingPosition.Length; ++i)
        {
            if (FollowShootingPosition[i].activeSelf == false)
                continue;
            PlayerBullet playerBullet = Global.GameObjectPool_A.ApplyPlayerBullet();

            if (playerBullet == null)
                return;
           

            playerBullet.Follow = true;
            playerBullet.Used = true;
            Vector2 defPos = (Vector2)FollowShootingPosition[i].transform.position;
            playerBullet.ResetPos(defPos);
            playerBullet.OriginalPos = defPos;
            playerBullet.BulletSprite.sprite = FollowBulletSprite;
            playerBullet.AttackNumber = -1;
           // playerBullet.animControl.PlayInFixedTime("PlayerBulletAnim", 0, 0);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Global.GamePause == true)
            return;
        _moving = false;
        if (OnCharacterUpdating != null)
            OnCharacterUpdating(Player);
        if (Controled == false)
            return;
        _totalframe = _totalframe + 1 * Global.GlobalSpeed;
        if (Input.GetButton("Submit") && Global.WrttienSystem == false)
        {

            ShootingBullet();
            if (OnCharacterShooting != null)
                OnCharacterShooting(Player);
        }
        if (Input.GetButton("Slow"))
        {
            Speed = NormalSpeed; PointAnimation.SetBool("NoShiftPressed", true);
            if (OnCharacterSlowDown != null)
                OnCharacterSlowDown(Player);
        }
        else { Speed = RapidSpeed; PointAnimation.SetBool("NoShiftPressed", false); }
        if (Input.GetButton("Horizontal"))
        {
            _moving = true;
            if (Input.GetAxis("Horizontal") > 0)
            {
                thisTransform.Translate(Vector2.right * Global.GlobalSpeed * Speed / 50);
                if (useSpriteFilp)
                    Player.PlayerPicture.flipX = true;
                Player.AnimationController.SetBool("Right", true);
                Player.AnimationController.SetBool("Left", false);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                thisTransform.Translate(Vector2.left * Global.GlobalSpeed * Speed / 50);
                if (useSpriteFilp)
                    Player.PlayerPicture.flipX = false;
                Player.AnimationController.SetBool("Right", false);
                Player.AnimationController.SetBool("Left", true);

            }
        }
        else
        {
            Player.AnimationController.SetBool("Right", false);
            Player.AnimationController.SetBool("Left", false);
        }
        if (Input.GetButton("Vertical"))
        {
            _moving = true;
            if (Input.GetAxis("Vertical") > 0)
                thisTransform.Translate(Vector2.up * Global.GlobalSpeed * Speed / 50);
            if (Input.GetAxis("Vertical") < 0)
                thisTransform.Translate(Vector2.down * Global.GlobalSpeed * Speed / 50);
        }

        Vector3 Position = Global.MainCamera.WorldToScreenPoint(transform.position);
        Position.x = Mathf.Clamp(Position.x, Screen.width * MinRangeofScreen.x, Screen.width * MaxRangeofScreen.x);
        Position.y = Mathf.Clamp(Position.y, Screen.height * MinRangeofScreen.y, Screen.height * MaxRangeofScreen.y);
        if (_moving)
        {
            if (OnCharacterMoving != null)
                OnCharacterMoving(Player);
        }
        else
        {
            if (OnCharacterStoping != null)
                OnCharacterStoping(Player);
        }
        gameObject.transform.position = Global.MainCamera.ScreenToWorldPoint(Position);
    }
}
