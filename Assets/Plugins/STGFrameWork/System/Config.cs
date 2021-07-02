using System.Collections;
using UnityEngine;
using System.Xml;

public class Config : MonoBehaviour {
	XmlDocument XmlFile= new XmlDocument();
	XmlNode Key;    
	public Animator aniControl;
	public Dialog Window;
	bool error;
	void Start(){
		StartCoroutine (GameCheck ());   
	}
	IEnumerator Restore(){
		UnityEngine.Screen.SetResolution (1360, 765,false);
		aniControl.speed = 1;
		Window.Hide ("Window_Hide");
		Global.NormalBoolValue [Global.NormalBoolValue.Count - 1] = true;
		yield return null;
	}
	IEnumerator RestoreC(){
		aniControl.speed = 1;
		Window.Hide ("Window_Hide");
		Global.NormalBoolValue [Global.NormalBoolValue.Count - 1] = true;
		yield return null;
	}
	IEnumerator GameCheck ()
	{			
        Global.NormalBoolValue.Add (false);
		if (System.IO.File.Exists (UnityEngine.Application.streamingAssetsPath + @"\GameConfig.xml")) {
			int ScreenWidth = 1360, ScreenHeight = 765;
			bool fullScreen;
			try{
                XmlFile.Load (UnityEngine.Application.streamingAssetsPath + @"\GameConfig.xml");
				Key = XmlFile.SelectSingleNode ("TuoHuoSTGConfigSetting");
			ScreenWidth = System.Convert.ToInt32 (Key ["Environment"].Attributes ["ScreenWidth"].InnerText);
			ScreenHeight = System.Convert.ToInt32 (Key ["Environment"].Attributes ["ScreenHeight"].InnerText);
			fullScreen = Key ["Environment"].Attributes ["ScreenHeight"].InnerText == "True";
				UnityEngine.Screen.SetResolution (ScreenWidth, ScreenHeight, fullScreen);
			}
			catch{
				aniControl.speed = 0;
				Window.gameObject.SetActive (true);
				Window.title.text = "XML 语法错误";
				Window.content.text="Config.xml文件内容存在语法上错误，由此导致的游戏异常已被截取，游戏本地化将被跳过，" +
					"点击OK继续游戏。";
				
				error = true;
				Window.eventDriver[0].ButtonBlindEvent  += RestoreC;

			}
			yield return new WaitUntil (() =>error == false);
			yield return new WaitForSeconds (1.5f);
	/*

            if (ScreenHeight / ScreenWidth != 9.0f/16.0f) {
				aniControl.speed = 0;
				Window.gameObject.SetActive (true);
				Window.title.text = "不正确的分辨率设置";
				Window.content.text="你给出的Config设置的屏幕宽高之比不为16:9，若不为此值，会导致UI错乱。" +
					"当你选择OK后，将使用默认的1360 X 765 分辨率并继续进行游戏，如果之后修改Config到一个正确的值也不能修改这个问题" +
					"，检查你的XML文件语法问题。不然请查阅源代码，查看该功能的具体实现方式，并进行针对性问题修复。";
				Window.eventDriver[0].ButtonBlindEvent  += Restore;

				yield return new WaitUntil (() => Global.NormalBoolValue [Global.NormalBoolValue.Count - 1] == true);
			}		
         */

    
			}
	}
}
