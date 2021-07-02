using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Bullet Bull;
    Quaternion c;
    private void Awake()
    {
        c = Quaternion.Euler(0, 0, 0); 
        Bull = gameObject.transform.parent.gameObject.GetComponent<Bullet>();
    }
    public void SetThisGameObjectForFalse ()
	{
		gameObject.SetActive (false);
	}
	public void SetThisGameObjectFatherForFalse ()
	{
		gameObject.transform.parent.gameObject.SetActive (false);

	}
	public void SetGamePause(){
        //Global.GlobalSpeed = 0
        if (Global.SpellCardExpressing)
            return; 
		Global.GamePause = true;
	}
	public void SetGameContinue(){
        //Global.GlobalSpeed = 0;
		Global.GamePause = false;
	}
	public void SetUsingSpellCardFALSE ()
	{
		Global.SpellCardExpressing = false;
	}

	public void SetUsingSpellCardTRUE ()
	{
		Global.SpellCardExpressing = true;
	}

	public void PlayAnimation ()
	{
		//Animator anim = GetComponent<Animator>();
		SpriteRenderer rend = GetComponent<SpriteRenderer>();

		//a/nim.enabled = false;
		//anim.speed = Global.GlobalSpeed;
		Bull.CreateAnimationPlayed = true;
		rend.sprite = Bull.BulletSprite;
		rend.color = Bull.BulletColor;
		Bull.gameObject.transform.localScale = Bull.Scale;
        Bull.Use = true;
		Bull.UseCollision = true;
	}
	public void DisplaySprite()
	{
		
		GetComponent<SpriteRenderer>().sprite = Bull.BulletSprite;
		GetComponent<SpriteRenderer>().color = Bull.BulletColor;

	}
	public void PlayAnimation2 ()
	{
		GetComponent<Animator> ().Play ("Bullet_Breaking");
	}

	public void Sprite ()
	{
		GetComponent<SpriteRenderer> ().sprite = Bull.BulletSprite;
	}

	public void Break ()
	{
		if (!Bull.AsEnemyMovement)
		{
			Global.GameObjectPool_A.BulletList.Enqueue(Bull);
			StressTester.c--;
			Bull.BulletTransform.position = new Vector3(999, 999, 0);
		}
		if (Bull.showTrails)
            Bull.Trail.gameObject.SetActive(false);
		Bull.RestoreNormalLevel();
		Bull.RestoreNormalLevelForSubBullet();
		
        Bull.BulletSpriteController.transform.localRotation = c ;
        gameObject.transform.localRotation = c;
      //  Bull.AnimationControl.enabled = false;
        Bull.Scale = Vector3.one;

		Bull.BulletSprite = null;
		Bull.SpriteTransform.localScale = Vector3.one;
		Bull.CreateAnimationPlayed = false;
		Bull.noDestroy = false;
		Bull.UseCustomAnimation = false;
		Bull.nextFramewaitTime = 0;
		Bull.Use = false;
		Bull.OtherObject = null;
        Bull.Rotation = 0;
        Bull.BulletSpriteRenderer.sprite = null;
	}
	public void SetBulletStageForFalse ()
	{
		gameObject.transform.parent.gameObject.GetComponent<Bullet> ().Use = false;
	}
}
