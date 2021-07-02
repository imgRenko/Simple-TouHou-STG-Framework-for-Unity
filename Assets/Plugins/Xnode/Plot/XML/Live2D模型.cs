using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using System;
using System.Diagnostics;
using XNode;
using Live2D.Cubism.Core;
using UnityEditor;

namespace 基础事件.新剧情系统.XML生成
{
	public class Live2D模型 : Node
	{
		[InfoBox("模型位置定位和文字气泡等不一样，它由场景位置决定，你可以打开编辑器，在其中打开模型，然后观察右上角的绿色背景的坐标指示器，将其中的数值填入其中即可。")]
		[Input] public FunctionProgress 进入节点;
		[Input] public string 模型路径;
		[Input] public string 动作;
		[Input] public Vector2 原位置;
		[Input] public Vector2 目的位置;
		[Input] public float 初始透明度;
		[Input] public float 目的透明度;
		[Input] public int 序号;
		[Input] public int 层次级别;
		[Output] public FunctionProgress 退出节点;
		
#if UNITY_EDITOR
		[Button]
		public void AnimationEditor编辑器中打开() {
			if (UnityEditor.EditorUtility.DisplayDialog("将打开编辑器", "将在Live2D Animation Editor确认和编辑你所指定的Live2D模型的动画和信息，要这样做吗？", "确认", "取消"))
			{
			}
			else { return; }
			string path = "";
			原位置 = GetInputValue("原位置", 原位置);
			string txtPath = Application.streamingAssetsPath + @"\Live2DEditorPath.txt";
			if (File.Exists(txtPath))
				path = File.ReadAllText(txtPath);
			if (File.Exists(path))
			{
				bool _running = false;
				foreach (var process in Process.GetProcesses()) {
					if (process.Id == Live2DEditorLinker.ProcessId)
					{
						_running = true;
						break;
					}
				}
				if (!_running)
				{
					模型路径 = GetInputValue("模型路径", 模型路径);
					动作 = GetInputValue("动作", 动作);
					string Modelpath = 模型路径;
					var all = Resources.FindObjectsOfTypeAll<GameObject>();
					GameObject instance = null;
					foreach (GameObject item in all)
					{
						if (item.gameObject.name == Modelpath)
						{
							instance = item;
							break;
						}
					}

					var prefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(instance);
					string Info = string.Empty;
					string findPath = UnityEditor.AssetDatabase.GetAssetPath(prefabAsset);

					Directory.SetCurrentDirectory(Directory.GetParent(Application.dataPath).FullName);
					string rd = Directory.GetCurrentDirectory();
					findPath = rd+ @"\"+Path.GetDirectoryName(findPath) + @"\"+Path.GetFileNameWithoutExtension(findPath) + ".model3.json";
					Info += "src;" + findPath+"\r\n";
					if (!File.Exists(findPath))
					{
						UnityEditor.EditorUtility.DisplayDialog("找不到模型文件", "找不到模型文件，请确认文件是否合法，是否填错，装有live2D模型的文件夹名称必须和文件夹内扩展名为model3.json文件的名称一致。\r\n路径为："
							+findPath, "确认");
						return;
					}
					if (动作 != string.Empty)
					{
						string r = string.Empty;
						Animator animator = instance.GetComponent<Animator>();
						
						RuntimeAnimatorController ac = animator.runtimeAnimatorController;
						if (ac == null)
						{
							UnityEditor.EditorUtility.DisplayDialog("模型没有安装Animator", "模型没有安装Animator组件，前往模型所在对象添加", "确认");
							return;
						}
						AnimationClip[] clips = ac.animationClips;
						bool check = false;
						foreach (var a in clips)
						{

							if (a.name == 动作)
							{
								r = UnityEditor.AssetDatabase.GetAssetPath(a);
								check = true; 
								break;
							}
						}
						if (check == false)
						{
							if(UnityEditor.EditorUtility.DisplayDialog("找不到动画", "找不到角色的动画，继续打开编辑器吗？", "确认", "取消"))
								{
							}
								else { return; }
						}
						else
						{
							r = rd + @"\" + Path.GetDirectoryName(r) + @"\" + Path.GetFileNameWithoutExtension(r) + ".motion3.json";
							Info += "motion;" + r + "\r\n";
							if (!File.Exists(r))
							{
								if (UnityEditor.EditorUtility.DisplayDialog("找不到动作文件", "找不到动作文件，请确认文件是否合法，是否填错，但仍可以继续打开编辑器，继续吗？\r\n路径为："
									+ findPath, "确认", "取消"))
								{
								}
								else { return; }
							}
						}
					}
					else {
					
							if (UnityEditor.EditorUtility.DisplayDialog("动作信息没有填写", "动作信息没有填写，在使用此节点将使用Animator默认的状态，此时仍可以继续打开编辑器，继续吗？", "确认", "取消"))
							{
							}
							else { return; }
						
					}
					Process editor = null;
					editor = new Process();
					editor.StartInfo.FileName = path;
					editor.Start();
					Live2DEditorLinker.ProcessId = editor.Id;
					Info += "pos;" + 目的位置.x.ToString() + "," + 目的位置.y.ToString();
					string txt = Path.GetDirectoryName(path) + @"\Live2D Cubism Animation Editor_Data\StreamingAssets\state" + editor.Id.ToString() + ".txt";
					if (!File.Exists(txt))
					File.WriteAllText(txt, Info );
				}
				else {
					UnityEditor.EditorUtility.DisplayDialog("不能多次启动编辑器", "你无法在这个Unity进程里多次启动编辑器。", "确认");
				}
			}
			else {

				if (UnityEditor.EditorUtility.DisplayDialog("不存在的路径", "无法启动Live2D Animation Editor编辑器，因为你没有指定路径。你可以现在指定，但需要你按下确认按钮。", "确认", "取消")) {
					string e = UnityEditor.EditorUtility.OpenFilePanel("选取编辑器位置", "desktop", "exe");
					File.WriteAllText(Application.streamingAssetsPath + @"\Live2DEditorPath.txt", e);
					
					UnityEditor.EditorUtility.DisplayDialog("已经完成指定", "已完成编辑器位置指定任务，重试操作以继续。", "确认");
						}

			}

		}
#endif
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			//Sample <Live2D src="object_live2d_046_101.asset" original-pos ="0,0" target-pos="-2.45,1" transform - speed = "0.1" original - alpha = "0" target - alpha = "1" index = "1" zlayout = "2" />
			 DialogSystemInit.createdXMLFileContent += "\r\n";
			Vector2 originalPos = GetInputValue("原位置", 原位置);
			Vector2 tarPos = GetInputValue("目的位置", 目的位置);
			模型路径 = GetInputValue("模型路径", 模型路径);
			初始透明度 = GetInputValue("初始透明度", 初始透明度);
			目的透明度 = GetInputValue("目的透明度", 目的透明度);
			动作 = GetInputValue("动作", 动作);
			序号 = GetInputValue("序号", 序号);
			层次级别 = GetInputValue("层次级别", 层次级别);
			string Content = "<Live2D src=\"" + 模型路径 + "\" original-pos=\"" + originalPos.x.ToString() + "," + originalPos.y.ToString() + "\" target-pos=\"" + tarPos.x.ToString() + "," + tarPos.y.ToString() + "\" transform-speed=\"0.2\" original-alpha=\"" + 初始透明度.ToString() + "\" target-alpha=\"" + 目的透明度.ToString() + "\" index=\"" + 序号.ToString() + "\" zlayout=\"" + 层次级别.ToString() + "\" motion=\"" + 动作 +"\"/>";
			DialogSystemInit.createdXMLFileContent += Content;
			ConnectDo("退出节点");

		}
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}