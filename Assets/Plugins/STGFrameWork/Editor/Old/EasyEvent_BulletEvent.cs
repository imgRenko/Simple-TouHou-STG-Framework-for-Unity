///  TouHou Project Unity STG FrameWork
///  Renko's Code 
///  2016 Created.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
[CustomEditor(typeof (EasyEventBase))]
public class EasyEvent_BulletEvent : Editor
{
    
        private ReorderableList infomation,Value;
        public List<string> TalkTitle = new List<string> ();
        public Dictionary<string,string> CharacterType = new Dictionary<string,string>();
        public enum CatchResult {
            RESULT_ADD = 0,
            RESULT_REMOVE = 1,
            RESULT_EQUAL = 2,
            RESULT_MUTIL = 3,
            RESULT_DISMATCH = -1
        }
        public CatchResult Result;
        private void OnEnable(){
            infomation = new ReorderableList (serializedObject, serializedObject.FindProperty ("FunctionList"), true, false, true, true);
            EasyEventBase t = target as EasyEventBase;
            System.Reflection.FieldInfo[] fInfo = typeof(Bullet).GetFields ();  
            infomation.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                SerializedProperty itemData = infomation.serializedProperty.GetArrayElementAtIndex (index);
                if (t.FunctionList [index].Updated == false) {
                    t.FunctionList [index].ShootingValueList.Clear ();
                    t.FunctionList [index].ShootingValueArrayIndex = 0;

                    if (t.FunctionList [index].EType == EasyEventProgress.TypeEnum.Bullet)
                        fInfo = typeof(Bullet).GetFields ();  
                    if (t.FunctionList [index].EType == EasyEventProgress.TypeEnum.Shooting)
                        fInfo = typeof(Shooting).GetFields ();  
                    if (t.FunctionList [index].EType == EasyEventProgress.TypeEnum.PlayerController)
                        fInfo = typeof(PlayerController).GetFields ();  
                    t.FunctionList [index].ShootingValueList.Clear ();
                    for (int i = 0; i != fInfo.Length; ++i) {
                        t.FunctionList [index].ShootingValueList.Add ("<" + fInfo [i].FieldType.Name + "> " + fInfo [i].Name);
                    }
                    t.FunctionList [index].ShootingValueListArray = t.FunctionList [index].ShootingValueList.ToArray ();
                    t.FunctionList [index].Updated = true;
                }
                rect.y += 2;
                rect.height = EditorGUIUtility.singleLineHeight;
                Rect LableRect = rect;
                Rect TypeRect = rect;
                TypeRect.x += 80;
                TypeRect.width -= 290;
                Rect EnumRect = rect;
                EnumRect.x += TypeRect.width + 84;
                EnumRect.width -= TypeRect.width + 84;
                Rect ToggleRect = rect;
                ToggleRect.width = rect.width - 60;
                ToggleRect.y += 18;
                Rect ExpressionRect = rect;
                ExpressionRect.y += 18;
                ExpressionRect.x += 90;
                ExpressionRect.width = rect.width - 90;
                EditorGUI.LabelField (ToggleRect, "输入表达式：");
                t.FunctionList [index].Expression = EditorGUI.TextArea (ExpressionRect, t.FunctionList [index].Expression);

                EditorGUI.LabelField (LableRect, "函数功能名称：");
                t.FunctionList [index].ShootingValueArrayIndex = EditorGUI.Popup (EnumRect, t.FunctionList [index].ShootingValueArrayIndex, t.FunctionList [index].ShootingValueListArray);
                if (t.FunctionList [index].tTemp != t.FunctionList [index].EType) {
                    t.FunctionList [index].tTemp = t.FunctionList [index].EType;
                    t.FunctionList [index].Updated = false;
                }
                t.FunctionList [index].EType = (EasyEventProgress.TypeEnum)EditorGUI.EnumPopup (TypeRect, t.FunctionList [index].EType);
            };
            infomation.elementHeight = 43;
            infomation.drawHeaderCallback = (Rect rect) => {
                GUI.Label (rect, "当子弹被销毁的时候使用 ()");
            }; 
        }
        public override void OnInspectorGUI ()
        {  
            EditorGUILayout.Space ();
            serializedObject.Update ();
            infomation.DoLayoutList ();
            serializedObject.ApplyModifiedProperties ();
            base.DrawDefaultInspector ();
        }

        public string CatchSpring (string expression,int Length){
            return expression.Substring (0, Length); 
        }

}

