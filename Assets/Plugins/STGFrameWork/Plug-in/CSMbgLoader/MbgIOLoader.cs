using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
public class MbgIOLoader : MonoBehaviour
{
    [InfoBox("在转换Mbg弹幕时,其中涉及到的所有绑定，均默认为深度绑定。若你的mgb弹幕中含有浅层绑定的弹幕，请为此修正。否则显示的效果极有可能不一致。（其中涉及到的一系列可能导致导入失败的情况在工程文件的文档有提及，它名字为help.doc）")]
    public string Url;
    [ButtonGroup]
    public void Load() { 
    
    }
}

public class CommonData {
    public int layerID;
    public int componentID;
    public Vector2 Position;
}
public class ShootData
{
    public int layerID;

    public int componentID;

    public bool bindingState;

    public int bindingID;

    public bool relativeDirection;

    public float Undef; // A BANNED PARAM

    public Vector2 Position;

    public float Delay;

    public float MaxFrame;

    public Vector2 shotPos;

    public float Radius, radiusAngle;

    public int Way;

    public float Timer;

    public float Angle;

    public float Range;

    public float shootingSpeed;

    public float shootingSpdDirection;

    public float shootingAccSpeed;

    public float shootingAccSpdDirection;

    public float bulletMaxFrame;

    public float bulletType;

    public Vector2 bulletScale;

    public Color bulletColor;

    public float bulletDirection;

    public bool reversalRotation;

    public float bulletSpeed;

    public float bulletSpdDirection;

    public float bulletAccSpeed;

    public float bulletAccSpdDirection;

    public Vector2 shotScale;

    public bool createAnim = true, destoryAnim = true, highLight, Trail, OutScreen = true, Invincible = false;

    public string shootingEventGroup;

    public string bulleteventGroup;

    /// Random Info



    public static readonly float totalParamAmount = 74;
}
public class LayerData : CommonData
{ 
}

public class LaserData : CommonData
{
    public int ID;
public int LayoutID;
public Vector2 ForcePos;
    public float from = 0;
    public float last = 0;
    public Vector2 Scale = Vector2.one;
    public bool Circle = false;
    public int type;
    public int controlID = 0;
    public float Speed = 2;
    public float spdDirection = 0;
    // Null
    public float AccSpd = 0;
    public float AccSpdDirection = 0;
    // Null;
//发射器事件组（说明|间隔|间隔增量|事件1;事件2;......;&说明|间隔|...),
//子弹事件组（说明|间隔|间隔增量|事件1;事件2;......;&说明|间隔|...),

//速度随机+量,
//速度方向随机+量,
//加速度随机+量,
////加速度方向随机+量,
//绑定ID,
//深度绑定
}

public class TriggerData : CommonData
{ 
}
public class ForceData : CommonData
{ 

}
public class MgbData {
    public List<ShootingData> shootingDatas = new List<ShootingData>();
    public List<LayerData> layerDatas = new List<LayerData>();
    public List<LaserData> laserDatas = new List<LaserData>();
    public List<TriggerData> triggerDatas = new List<TriggerData>();
    public List<ForceData> forceDatas = new List<ForceData>();
}
public class MgbManager {
    public GameObject nullObject;
    public string bulletCollectionsArrayFileUrl;
    public string proerietyArrayUrl;
    public static MgbManager Instance;
    public static MgbManager instance
    {
        get {
            if (Instance == null)
                Instance = new MgbManager();
            return Instance;
        }
     
    }
    public Vector2 MgbPos2UnityPos(Vector2 mgbPos) {
        float unitX = Mathf.Lerp(Global.ScreenX_A.x, Global.ScreenX_A.y, mgbPos.x / 630.0f);
        float unitY = Mathf.Lerp(Global.ScreenY_A.x, Global.ScreenY_A.y, mgbPos.y / 480.0f);
        return new Vector2(unitX, unitY);
    }
    public float MgbUnit2UnityUnit(float unit) {
        return Mathf.LerpUnclamped(0,(Mathf.Abs(Global.ScreenX_A.x) + Mathf.Abs(Global.ScreenX_A.y))/2,unit / 10.0f);
    
    }
    public void CreateEmptyObject(string Name,Transform parent) { 
        
    
    }
    public void CloneGameObject(string Name, Transform Parent,GameObject targetObject)
    {
        

    }
    public List<string> LoadFile(string path)
    {
        List<string> lst = new List<string>();
        StreamReader sr = new StreamReader(path);
        while (!sr.EndOfStream)
        {
            string str = sr.ReadLine();
            lst.Add(str);
            //string[] strd = str.Split(',');

            // 如果包含图层信息;

            if (str.Contains("layout"))
            { 
                
            
            }
        
        }



        return lst;
    }
    public void AnalysisContent() { 
        
    }
    public void CreateLayer() { 
    
    }
}