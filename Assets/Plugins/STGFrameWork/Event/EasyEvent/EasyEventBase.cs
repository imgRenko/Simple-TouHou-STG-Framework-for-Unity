using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
[System.Serializable]
public class EasyEventProgress {
    public enum TypeEnum {
        Bullet = 0,
        Shooting = 1,
        PlayerController = 2
    }

    public string  Expression = "Expression";
    // ShootingValueList
    public List <string> ShootingValueList = new List<string>();
    public string[] ShootingValueListArray;
    public int ShootingValueArrayIndex = 0;
    public bool Updated = false ;
    public TypeEnum EType,tTemp;
}
public class EasyEventBase : MonoBehaviour
{
    public List <EasyEventProgress> FunctionList = new List<EasyEventProgress>();
}

