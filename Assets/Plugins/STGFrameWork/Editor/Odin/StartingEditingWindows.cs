using Sirenix.OdinInspector;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
public class STGEngineWelcome : OdinEditorWindow
{
    Vector2 scrollPosition;
    [MenuItem("STG Engine Develop/欢迎界面")]
    private static void OpenWindow()
    {
        GetWindow<STGEngineWelcome>().Show();
    }
    [OnInspectorGUI("DrawPreview", append: true)]
    public Texture2D Texture;

    private void DrawPreview()
    {
        if (this.Texture == null) return;
        scrollPosition = GUI.BeginScrollView (new Rect (0,0,Screen.width,Screen.height),scrollPosition, new Rect (0, 0, 600, 800),true,true);
        GUILayout.BeginVertical(GUI.skin.box);
        GUI.Label(new Rect(0,10,855,512),this.Texture);
        GUILayout.EndVertical();
    }
}

public class SomeType
{
    [TableColumnWidth(50)]
    public bool Toggle;

    [AssetsOnly]
    public GameObject SomePrefab;

    public string Message;

    [TableColumnWidth(160)]
    [HorizontalGroup("Actions")]
    public void Test1() { }

    [HorizontalGroup("Actions")]
    public void Test2() { }
}