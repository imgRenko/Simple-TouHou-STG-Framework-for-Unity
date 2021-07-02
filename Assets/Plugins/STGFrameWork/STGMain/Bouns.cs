using System.Collections;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/奖励类")]
public class Bouns : MonoBehaviour
{

    public BounsType bounsType = BounsType.Score;

    public float Speed = 3,Rotation = 0,DestroyHeight = -2f;
    public Vector2 AcceleratedSpeedDirection;

    public AudioSource ItemSound;
    public bool Use = false,ClosePlayer = false,Fast = false;

    private Character PlayerCharacter;
    public SpriteRenderer Renderer;
    private float MaxSpeed = -2;
    private float slowSpd = 0f;
    private Transform transforms;

    public enum BounsType
    {
        FullPower = 0,
        Power = 1,
        Score = 2,
        LifePrice = 3
    }

    // Use this for initialization
    void Awake()
    {
        PlayerCharacter = Global.PlayerObject.GetComponent<Character>();
        Renderer = GetComponent<SpriteRenderer>();
        transforms = gameObject.transform;
    }

    public void UseBouns(Vector3 Postion)
    {
        switch (bounsType) {
            case BounsType.FullPower:
                Renderer.sprite = Global.FullPowerBouns;
                break;
            case BounsType.Power:
                Renderer.sprite = Global.PowerBouns;
                break;
            case BounsType.Score:
                Renderer.sprite = Global.ScoreBouns;
                break;
            case BounsType.LifePrice:
                Renderer.sprite = Global.LivePriceBouns;
                break;
        }
        ClosePlayer = false;
        transforms.localPosition = Vector3.zero;
        transforms.parent.transform.position = Postion;
        gameObject.SetActive(true);
        Use = true;
        Rotation = Random.Range(15f, -15f);
        Speed = Random.Range(1f, 3f);
        AcceleratedSpeedDirection.y = Random.Range(0, -0.3f);
        transforms.parent.transform.Rotate(new Vector3(0, 0, Rotation));
    }
    /// <summary>
    /// 掉落一个奖励，CustomSprite可以使用自定义的图像。
    /// </summary>
    public void UseBouns(Vector3 Postion, Sprite customSprite)
    {
        if (customSprite != null)
            Renderer.sprite = customSprite;
        else
            return;
        ClosePlayer = false;
        transforms.localPosition = Vector3.zero;
        transforms.parent.transform.position = Postion;
        gameObject.SetActive(true);
        Use = true;
        Rotation = Random.Range(15f, -15f);
        Speed = Random.Range(1f, 3f);
        AcceleratedSpeedDirection.y = Random.Range(0, -0.3f);
        transforms.parent.transform.Rotate(new Vector3(0, 0, Rotation));
    }
    private void Start()
    {
        
    }
    void GetBouns()
    {
        ClosePlayer = false;
        Fast = false;
       
        slowSpd = 0;
        if (bounsType == BounsType.FullPower)
            Global.Power = (Global.Power > 4.0f ? Global.Power + 0.5f : 4.0f);
        if (bounsType == BounsType.Power)
            Global.Power += 0.01f;
        if (bounsType == BounsType.LifePrice &&  Global.PlayerLive_A < Global.MaxLive)
            Global.LivePrice += 1;
        if (bounsType == BounsType.Score)
        {
            Global.AddPlayerScore(Global.MaxBounsScore * 10);
            Global.ScoreBounsCollectionCount++;
            if (Global.ScoreBounsCollectionCount >= 5)
            {
                Global.MaxBounsScore += 20;
                Global.ScoreBounsCollectionCount = 0;
            }
        }
      //  ItemSound.PlayOneShot(ItemSound.clip);
        AudioQueue.Play(ItemSound);
        transforms.position = Vector3.one * 10;
		ClosePlayer = false;
		bounsType = BounsType.Score;
		Speed = 3;
		Rotation = 0;
		StartCoroutine( Destory2());
    }
	IEnumerator Destory2(){
		
		transforms.position = Vector3.one * 10;
		ClosePlayer = false;
		bounsType = BounsType.Score;
		Speed = 3;
		Rotation = 0;
		 yield return new WaitForSeconds (0.2f);
		transforms.parent.gameObject.SetActive(false);
        Use = false;


    }
    void  Destroy()
    {
        Use = false;
        transforms.position = Vector3.one * 10;
        ClosePlayer = false;
        bounsType = BounsType.Score;
        Speed = 3;
        Rotation = 0;
       // yield return new WaitForSeconds (0.2f);
        transforms.parent.gameObject.SetActive(false);

      
    }

    // Update is called once per frame
    void Update()
    {

        if (!Use || Global.GamePause == true)
            return;
        float Distance = Vector2.Distance(transforms.position, PlayerCharacter.gameObject.transform.position);

        if (transforms.position.y <= DestroyHeight)
            Destroy();
        if (0.1f + PlayerCharacter.Radius >= Distance)
            GetBouns();
        if (0.4f + PlayerCharacter.Radius >= Distance)
            ClosePlayer = true;
        if (ClosePlayer)
        {
            if (Fast)
                transforms.position = Vector2.MoveTowards(transforms.position,
                    PlayerCharacter.gameObject.transform.position, 0.15f);
            else
            {
                slowSpd += 0.004f;
                transforms.position = Vector2.MoveTowards(transforms.position,
                    PlayerCharacter.gameObject.transform.position, slowSpd);
            }

        }
     
            transforms.localRotation = Quaternion.Euler(0, 0, 0);
            transforms.position = new Vector3(Mathf.Clamp(transform.position.x, Global.ScreenX_A.x + 1.0f, Global.ScreenX_A.y - 1f), transforms.position.y, transforms.position.z);
            transforms.transform.localRotation =
                Quaternion.Inverse(transforms.parent.transform.localRotation);
            transforms.parent.transform.Translate(Vector3.up / 100.0f * Speed * Global.GlobalSpeed, Space.Self);
            if (AcceleratedSpeedDirection.y > MaxSpeed)
                AcceleratedSpeedDirection.y -= 0.02f;
            transforms.parent.transform.Translate(Speed / 100.0f * AcceleratedSpeedDirection * Global.GlobalSpeed, Space.World);
        
    }

    public static void SetBouns(int BounsNumber, Vector3 postion, BounsType form)
    {
        for (int i = 0; i != BounsNumber; i++)
        {
            Bouns Temp = Global.GameObjectPool_A.ApplyBouns();

            if (Temp == null)
                continue;
            Temp.transforms = Temp.gameObject.transform;
            Temp.bounsType = form;
            Temp.UseBouns(postion);
            Temp.transforms.parent.gameObject.SetActive(true);
          
        }
    }
}
