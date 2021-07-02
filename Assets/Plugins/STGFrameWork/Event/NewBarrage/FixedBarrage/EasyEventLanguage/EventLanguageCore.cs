///  TouHou Project Unity STG FrameWork
///  Renko's Code 
///  2020 Created.

using System;
using System.Linq;
using UnityEngine;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using Sirenix.OdinInspector;

using System.Collections.Generic;
[System.Serializable]
public class ValueContent { 


}
public class EventLanguageCore : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<string, KeyValuePair<string, object>> keyValuePairs = new Dictionary<string, KeyValuePair<string, object>>();

    public void CreateValue(string name, string type, object content) { }
    public void CreateValue(string name, string type) { }
}