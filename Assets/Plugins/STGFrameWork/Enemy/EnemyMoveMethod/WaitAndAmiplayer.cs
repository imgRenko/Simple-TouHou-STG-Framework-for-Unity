public class WaitAndAmiplayer : BulletEventForSingle {
    WaitAndAmiplayer(){
        // 在构造函数进行变量赋值，就可以在Unity属性编辑面板里直接显示被赋值的变量的值
        note = "芙兰朵露在130帧数后对玩家进行一次跟踪,之后恢复正常。";
    }
    public override void OnBulletMoving(Bullet T)
    {
	T.InverseRotation = true;   
	if (T.TotalLiveFrame >= 130) 
	{        
	T.AimToPlayerObject();           
	Destroy(this);    
	}
  }
}
