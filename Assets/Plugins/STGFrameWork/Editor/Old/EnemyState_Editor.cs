using UnityEditor;
[CustomEditor(typeof(EnemyState))]
public class EnemyState_Editor : Editor {
    public override void OnInspectorGUI()
    {
        EnemyState Target = target as EnemyState;
        EditorGUILayout.HelpBox ("不建议修改的元数据。", MessageType.Warning);
        if (Target.tag != "BOSS")
        {
            EditorGUILayout.LabelField("Animator控制文件序号（定义在Global的AnimationControlFile中）");
            Target.ClipFileIndex = EditorGUILayout.IntField(Target.ClipFileIndex);
        }
        base.DrawDefaultInspector();
    }
}
