using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
public class Dialog : MonoBehaviour {
	[Header ("GUI OBJECT REFERENCE")]
	public Text title;
	public Text content;
	public Image Img;
	public DialogButtonBlind[] eventDriver;
	[Header ("ANIMATION CONTROLLER")]
	public Animator aniControl;

	public delegate IEnumerator GUIEvent();
	//public event GUIEvent buttonEvent;
	public void Show(string ani = "Window_Show")
	{
		Global.dialoging = true;
		gameObject.SetActive (true);
		aniControl.PlayInFixedTime (ani, 0, 0);
	}
	public void Show(Sprite spr,string ani = "Window_Show")
	{
		Global.dialoging = true;
		gameObject.SetActive (true);
		aniControl.PlayInFixedTime (ani, 0, 0);
		Img.sprite = spr;
	}
	public void Show(Sprite spr,string Title,string ani = "Window_Show")
	{
		Global.dialoging = true;
		gameObject.SetActive (true);
		aniControl.PlayInFixedTime (ani, 0, 0);
		Img.sprite = spr;
		title.text = Title;
	}
	public void Show(Sprite spr,string Title,string Content,string ani = "Window_Show")
	{
		Global.dialoging = true;
		gameObject.SetActive (true);
		aniControl.PlayInFixedTime (ani, 0, 0);
		Img.sprite = spr;
		title.text = Title;
		content.text = Content;
	}
	public void Show(string Title,string Content,string ani = "Window_Show")
	{
		Global.dialoging = true;
		gameObject.SetActive (true);
		aniControl.PlayInFixedTime (ani, 0, 0);
		title.text = Title;
		content.text = Content;
	}
	public void Show(string NoteName,string Language = "CN")
	{		
		gameObject.SetActive (true);
		//aniControl.PlayInFixedTime (ani, 0, 0);
        try
        {
            if (File.Exists(Application.streamingAssetsPath + @"\StringTable\GameString.xml"))
            {
                XmlDocument document = new XmlDocument();
                document.Load(Application.streamingAssetsPath + @"\StringTable\GameString.xml");
                XmlNode d = document.SelectSingleNode("TuoHuoSTGConfigSetting/" + Language);
                title.text = d[NoteName].Attributes["Title"].InnerText;
                content.text = d[NoteName].Attributes["Content"].InnerText;
                Img.sprite = Resources.Load<Sprite>(Application.dataPath+ @d[NoteName].Attributes["ImagePath"].InnerText);
            }
        }
        catch {
            Debug.LogError("读取的XML文件存在错误，请查阅是否是某一些文件或是源代码出现了问题。由此Dialog不能显示");
        }
	}
	public void Hide(string ani = "Window_Hide"){
		Global.dialoging = false;
		aniControl.PlayInFixedTime (ani, 0, 0);
	}
	public void ActiveFALSE ()
	{
		gameObject.SetActive (false);
	}
	public void DriveEvent()
	{
		foreach (DialogButtonBlind a in eventDriver) {
			a.DriveEvent ();
		}
	}
	public void ClearEvent(){
		foreach (DialogButtonBlind a in eventDriver) {
			a.ButtonBlindEvent = null;
		}
	}
	public IEnumerator GamePause (){
		Global.GamePause = true;
		yield return null;
	}
	public IEnumerator GameContinue (){
		Global.GamePause = false;
		yield return null;
	}
	public IEnumerator Dismiss(){
		
		Hide ("Window_Hide");
		yield return null;
	}
}
