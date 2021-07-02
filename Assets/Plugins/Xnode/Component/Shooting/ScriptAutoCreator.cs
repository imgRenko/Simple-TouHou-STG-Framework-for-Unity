using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityScript.Scripting.Pipeline;

public class ScriptAutoCreator : MonoBehaviour
{
    public string Path;
    public bool isForce;
    public bool Set;
    FieldInfo[] fInfo;//;
    FieldInfo fInfoSelected;
    [HideInInspector]
    public Type type;
    public enum Type
    {
        Float = 0,
        Vector3 = 1,
        Vector2 = 2,
        Sprite = 3,
        GameObject = 4,
        Int = 5,
        Color = 6,
        Array = 7,
       Bool = 8,
       String = 9
    }

    [Button]
    public void CreateScript (){
        type = Type.Float;
        Create();
        type = Type.Vector3;
        Create();
        type = Type.Vector2;
        Create();
        type = Type.GameObject;
        Create();
         type = Type.Int;
        Create();
        type = Type.String;
        Create(); 
       type = Type.Sprite;
        Create();
        type = Type.Bool;
        Create();
        type = Type.Array;
        Create();
        type = Type.Color;
        Create();
    }
    public void Create()
    {
        string Using = "using System.Collections; \r\n using System.Collections.Generic;\r\n using UnityEngine; \r\n using XNode; \r\n";
        string Namespace = "namespace 基础事件." + (isForce ? "力场 \r\n{" : "自机 \r\n{");
        string Class = "public class " + (Set ? "设置" : "获取") + (isForce ? "力场" : "自机")+ GetInfo() + "信息:Node \r\n {";
        string EnterProgress = (Set ? "[Input] public FunctionProgress 进入节点;" : "");
        string TargetValue = (Set ? "[Input] public " + GetValueInfo() + " 目的值;" : "");
        string port = "[Input] public " + (isForce ? "Force 力场; \r\n" : "Character 自机; \r\n");
        string OuterProgress = (Set ? "[Output] public FunctionProgress 退出节点;\r\n" : "[Output] public " + GetValueInfo() + " 结果;\r\n");
        string Enum = "public enum " + (isForce ? "ForceData { \r\n" : "CharacterEnumData { \r\n");
        List<string> att = new List<string>();
        List<FieldInfo> Field = new List<FieldInfo>();
        string EnumContent = "";
        if (!isForce)
            fInfo = typeof(Character).GetFields();
        else
            fInfo = typeof(Force).GetFields();
        foreach (var a in fInfo)
        {
         
            bool typeCheck = true;
            if (type == Type.Color)
                typeCheck = a.FieldType == typeof(Color);
            if (type == Type.Float)
                typeCheck = a.FieldType == typeof(float);
            if (type == Type.Vector3)
                typeCheck = a.FieldType == typeof(Vector3);
            if (type == Type.Vector2)
                typeCheck = a.FieldType == typeof(Vector2);
            if (type == Type.Sprite)
                typeCheck = a.FieldType == typeof(Sprite);
            if (type == Type.GameObject)
                typeCheck = a.FieldType == typeof(GameObject);
            if (type == Type.Int)
                typeCheck = a.FieldType == typeof(int);
            if (type == Type.Array)
                typeCheck = a.FieldType.ToString().Contains("List");
            if (type == Type.Bool)
                typeCheck = a.FieldType == typeof(bool);
            if (type == Type.String)
                typeCheck = a.FieldType == typeof(string);
            if (typeCheck)
            {
                object[] Translate = a.GetCustomAttributes(typeof(LabelTextAttribute), true);
                
                for (int g = 0; g != Translate.Length; ++g)
                {
                    LabelTextAttribute _Lable = (LabelTextAttribute)Translate[g];
                    att.Add(_Lable.Text);
                    
                }

                if (Translate.Length == 0)
                    att.Add(a.Name);
                Field.Add(a);
            }
        }
        int i = 0;
        if (att.Count == 0)
            return; 
        foreach (var b in att)
        {
            if (i != att.Count - 1)
                EnumContent += att[i] + "=" + i.ToString() + ",\r\n";
            else
                EnumContent += att[i] + "=" + i.ToString() + "\r\n";
            ++i;
        }



        EnumContent += "}\r\n" + (isForce ? "public ForceData 力场属性;\r\n" : "public CharacterEnumData 自机属性; \r\n");
        string GetValue = (Set ? " public override void FunctionDo(string PortName,List<object> param = null) {\r\n" : "public override object GetValue(NodePort port) \r\n{");
        string FunctionContent = "";
        if (!isForce)
        {

            if (!Set)
            {
                FunctionContent += "自机 = GetInputValue<Character>(\"自机\", null);if (自机 == null){ return 0;} \r\n";
                FunctionContent += "switch(自机属性) \r\n {";
                for (int r = 0; r != Field.Count; ++r)
                {
                    FunctionContent += "case CharacterEnumData." + att[r] + ":" + "\r\n";
                    FunctionContent += "结果=" + "自机." + Field[r].Name + ";\r\n";
                    FunctionContent += "break;\r\n";
                }
                FunctionContent += "}return 结果;}}}";
            }
            else
            {
                FunctionContent += " 自机 = GetInputValue<Character>(\"自机\", null);if (自机 == null) return;目的值 = GetInputValue<" + GetValueInfo() + ">(\"目的值\", 目的值); ";
                FunctionContent += "switch(自机属性) \r\n {";
                for (int r = 0; r != Field.Count; ++r)
                {
                    FunctionContent += "case CharacterEnumData." + att[r] + ":" + "\r\n";
                    if (GetValueInfo() == "float" && Field[r].FieldType == typeof(int))
                        FunctionContent += "自机." + Field[r].Name + "=(int)目的值" + ";\r\n";
                    else
                    {
                       
                            FunctionContent += "自机." + Field[r].Name + "=目的值" + ";\r\n";
                    }
                    FunctionContent += "break;\r\n";
                }
                FunctionContent += "}ConnectDo(\"退出节点\");}}}";

            }

        }
        else {

            if (!Set)
            {
                FunctionContent += "力场 = GetInputValue<Force>(\"力场\", null);if (力场 == null){ return 0;} \r\n";
                FunctionContent += "switch(力场属性) \r\n {";
                for (int r = 0; r != Field.Count; ++r)
                {
                    FunctionContent += "case ForceData." + att[r] + ":" + "\r\n";
                    FunctionContent += "结果=" + "力场." + Field[r].Name + ";\r\n";
                    FunctionContent += "break;\r\n";
                   
                }
                FunctionContent += "}return 结果;}}}";
            }
            else
            {
                FunctionContent += " 力场 = GetInputValue<Force>(\"力场\", null);if (力场 == null) return;目的值 = GetInputValue<" + GetValueInfo() + ">(\"目的值\", 目的值); ";
                FunctionContent += "switch(力场属性) \r\n {";
                for (int r = 0; r != Field.Count; ++r)
                {
                    FunctionContent += "case ForceData." + att[r] + ":" + "\r\n";
                    if (GetValueInfo() == "float" && Field[r].FieldType == typeof(int))
                        FunctionContent += "力场." + Field[r].Name + "=(int)目的值" + ";\r\n";
                    else
                        FunctionContent += "力场." + Field[r].Name + "=目的值" + ";\r\n";
                    FunctionContent += "break;\r\n";
                }
                FunctionContent += "} ConnectDo(\"退出节点\");}}}";

            }

        }

  
     
        System.IO.File.WriteAllText((Path + (Set ? "设置" : "获取") + (isForce ? "力场" : "自机")+ GetInfo() + "信息"+".cs"), Using + Namespace + Class + EnterProgress + TargetValue + port + OuterProgress + Enum + EnumContent + GetValue + FunctionContent, System.Text.Encoding.UTF8);
    }
    public string GetInfo()
    {
        switch (type)
        {
            case Type.Color:
                return "颜色";
            case Type.Float:
                return "浮点数";
            case Type.GameObject:
                return "游戏对象";
            case Type.Int:
                return "整数";
            case Type.Sprite:
                return "精灵纹理";
            case Type.Vector2:
                return "二维向量";
            case Type.Vector3:
                return "三维向量";
            case Type.Array:
                return "数组";
            case Type.Bool:
                return "布尔值";
            case Type.String:
                return "字符串";
        }
        return string.Empty;
    }
    public string GetValueInfo()
    {
        switch (type)
        {
            case Type.Color:
                return "Color";
            case Type.Float:
                return "float";
            case Type.GameObject:
                return "GameObject";
            case Type.Int:
                return "int";
            case Type.Sprite:
                return "Sprite";
            case Type.Vector2:
                return "Vector2";
            case Type.Vector3:
                return "Vector3";
            case Type.Array:
                return "object";
            case Type.Bool:
                return "bool";
            case Type.String:
                return "string";
        }
        return string.Empty;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
