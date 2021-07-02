using UnityEngine;
using System.Collections;

public class Stage2 : StageControl
{
	public Sprite _window_Sprite_help;
	IEnumerator WindowsDismiss(){
		Global.GamePause = false;
		Global.WindowDialog_A.Hide ();
		yield return null;
	}
	public override IEnumerator SceneControl ()
	{
		Global.GamePause = true;
		Global.WindowDialog_A.Show (_window_Sprite_help, "开发前提示", "这是一个标准的关卡范例，标准的关卡全局设定以及GUI设定都已经设定好了，你只需要一个Stage脚本来控制敌人的出现与移动，另外外加设计一些弹幕，一个关卡就完成了，如果你需要修改一些GUI，例如外观、动作之类的东西，我在此推荐你阅读一下这个项目的源代码，以便获得一些对你开发有好处的信息。（要关闭这个提示，去Stage2.cs脚本的第15行把它删掉即可）","Window_Show");
		Global.WindowDialog_A.eventDriver [0].ButtonBlindEvent += WindowsDismiss;
		yield return null;
	}
}

