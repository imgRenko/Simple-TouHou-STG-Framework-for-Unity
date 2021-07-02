using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public class LinkPointsSetting{
    public GameObject tracedObject;
    public GameObject linkedObject;
    public int bulletAmountInLines = 5;
    public float Depth = 5;
    
    public enum DrawMethod {
        Slerp = 1,
        Link = 0
    };
    public DrawMethod drawMethod;
    private List<Bullet> controledBullets = new List<Bullet>();

    public void AddControledBullets(Bullet target){
       
        controledBullets.Add(target);
        target.Speed = 0;
    }
    public void UpdatePostionChange() {
        Vector3 r = new Vector3 (0, 0, Depth);
        float unitedDistance = 1.0f / bulletAmountInLines;
        for (int i = 0; i != controledBullets.Count; ++i)
        {
            if (controledBullets[i].Use == false)
                controledBullets.RemoveAt(i);
        }
        for (int i = 0; i != controledBullets.Count; ++i) {
            switch (drawMethod) {
                case DrawMethod.Link:
            controledBullets[i].transform.position =
                        Vector3.Lerp(tracedObject.transform.position + r, linkedObject.transform.position+ r, unitedDistance * i);
                    break;
                case DrawMethod.Slerp:
                    controledBullets[i].transform.position =
                        Vector3.Slerp(tracedObject.transform.position+ r, linkedObject.transform.position+ r, unitedDistance * i);
                    break;
        } }
    }
}
[AddComponentMenu("东方STG框架/弹幕设计/弹幕特殊化/链条弹幕")]
public class Link : MonoBehaviour {
    
    [Header("EDITING")]
    [InfoBox("Offered by framework author.")]
    [UnityEngine.SerializeField]
    public List<LinkPointsSetting> pointSetting = new List<LinkPointsSetting>();
    public Shooting orderedShooting;

    [Header("REFFRENCE")]
    public float shotTime = 30.0f;
    public int maxShotCount = 1;
    private float timeCounter = 0;
    private float ShotCounter = 0;


	void Update () {
        
        foreach (LinkPointsSetting index in pointSetting) 
            index.UpdatePostionChange ();

        timeCounter += Global.GlobalSpeed;

        if (timeCounter < shotTime || ShotCounter >= maxShotCount)
            return;
        
        timeCounter = 0;
        ShotCounter++;
        orderedShooting.Canceled = true;

        foreach (LinkPointsSetting index in pointSetting) {
            for (int i = 0; i != index.bulletAmountInLines; ++i) {
                index.AddControledBullets (orderedShooting.Shot (1, true, false)[0]);
            }
           
        }
	}
}
