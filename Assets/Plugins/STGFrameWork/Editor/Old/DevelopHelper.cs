using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class DevelopHelper : OdinMenuEditorWindow {

	//bool _started = false;
    public Bullet checkBullet;
	private GameObject _local_EnemyObject;
	private Animator _local_Enemy_AnimContr;
	private Sprite _Enemy_Sprite;
	[MenuItem("STG Engine Develop/Quickly Develop")]

	private static void OpenWindow() {
		var window = GetWindow<DevelopHelper>();
		//window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
	
	}

	protected override OdinMenuTree BuildMenuTree()
	{
		var tree = new OdinMenuTree(false);
		tree.Selection.SupportsMultiSelect = false;
		tree.Add("欢迎使用 Unity THSTG", GeneralDrawerConfig.Instance);
		tree.Add("编辑器设置", GeneralDrawerConfig.Instance);
		tree.Add("Utilities", new TextureUtilityEditor());
		tree.AddAllAssetsAtPath("Odin Settings", "Assets/Plugins/Sirenix", typeof(ScriptableObject), true, true);
		return tree;
	}
}
public class WelcomeWindow { 
	

}
public class TextureUtilityEditor
{
	[BoxGroup("Tool"), HideLabel, EnumToggleButtons]
	public Tool Tool;

	public List<Texture> Textures;

	[Button(ButtonSizes.Large), HideIf("Tool", Tool.Rotate)]
	public void SomeAction() { }

	[Button(ButtonSizes.Large), ShowIf("Tool", Tool.Rotate)]
	public void SomeOtherAction() { }
}