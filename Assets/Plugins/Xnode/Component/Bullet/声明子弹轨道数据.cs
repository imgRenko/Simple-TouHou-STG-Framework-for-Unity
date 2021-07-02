using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 基础事件.子弹
{
	public class 声明子弹轨道数据 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public Bullet 子弹;
		[Input] public BulletTrackProduct 轨道信息;// = new BulletTrackProduct();
		public bool 允许修改部分数据 = false;
		[ShowIf("允许修改部分数据")] [Input] public AnimationCurve 曲线;
		[ShowIf("允许修改部分数据")] [Input] public float 常数1;
		[ShowIf("允许修改部分数据")] [Input] public float 常数2;
		[ShowIf("允许修改部分数据")] [Input] public float 目的值;
		[ShowIf("允许修改部分数据")] [Input] public float 变化时间;
		[ShowIf("允许修改部分数据")] [Input] public float 随机范围;
		[ShowIf("允许修改部分数据")] [Input] public int 要随机关键帧序号;
		[ShowIf("允许修改部分数据")] [Input] public int 使用次数;
		

		[Output] public FunctionProgress 退出节点;
        // Use this for initialization
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			BulletTrackProduct r = GetInputValue("轨道信息", 轨道信息);
			子弹 = GetInputValue("子弹", 子弹);
			if (允许修改部分数据) {
				常数1 = GetInputValue("常数1",常数1);
				常数2 = GetInputValue("常数2", 常数2);
				变化时间 = GetInputValue("变化时间", 变化时间);
				使用次数 = GetInputValue("使用次数", 使用次数);
				目的值 = GetInputValue("目的值", 目的值);
				曲线 = GetInputValue("曲线", 曲线);
				要随机关键帧序号 = GetInputValue("要随机关键帧序号", 要随机关键帧序号);
				随机范围 = GetInputValue("随机范围", 随机范围);
				r.Const1 = 常数1;
				r.Const2 = 常数2;
				r.Arrvage = 目的值;
				r.calcateCurve = 曲线;
				r.changeTime = 变化时间;
				r.RandomRange = 随机范围;
				r.keyRandomIndex = 要随机关键帧序号;
				r.operationMaxTime = 使用次数;
			}
	
			r.Init(子弹);
			
			子弹.bulletTrackProducts.Add(r);
			子弹.UseSimpleEvent = true;
			ConnectDo("退出节点");

		}

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}