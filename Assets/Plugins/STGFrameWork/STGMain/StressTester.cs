using UnityEngine;

public class StressTester : MonoBehaviour
{
    static public int c = 0;
    static public int b = 0;
    void OnGUI()
    {
        if (Application.isEditor)
            GUILayout.Label("\nBulletNumber:" + c.ToString() + "/" + Global.GameObjectPool_A.MaxBulletNumber.ToString());
    }

}
