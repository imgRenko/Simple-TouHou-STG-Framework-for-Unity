using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/开发者调试工具/帧数显示器")]
public class FpsTester : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
	private int totalframes = 60;
    private float fps;
	private float fpstotal = 0;
    private float inser = 1;
    private float totalFpsIndeed = 0;
	//private float meanfps = 0;
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }
    void OnGUI()
    {
        if (fps < 20)
            GUI.color = Color.red;
        if ( fps >= 20 && fps < 40)
            GUI.color = Color.yellow;
        if ( fps >= 40 )
            GUI.color = Color.green;
		GUILayout.Label (fps.ToString ("f1") + "<b> FPS</b>. 处理落率:" + ((1-(fpstotal / inser)/60.0f)*100.0f).ToString("f1")+"% ("+ (fpstotal / inser) .ToString("f1")+ ")");

    }
    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + updateInterval)
        {
            if (!Global.GamePause&&!Global.WrttienSystem)
            {
                fpstotal += fps;
                inser++;
            }
            fps = (float)(frames / (timeNow - lastInterval));
			
           
            totalFpsIndeed += fps;
            frames = 0;
            lastInterval = timeNow;
        }
    }
}