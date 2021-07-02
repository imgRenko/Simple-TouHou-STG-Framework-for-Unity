using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.对象
{
	public class 设置游戏对象信息 : Node {
		[Input] public FunctionProgress 进入节点;
		public enum Method {
			全局位置,
			局部位置,
			旋转,
			缩放
		}
		[Input] public Vector3 修改的值;
		[Input] public GameObject 目标对象;
		public Method 修改;
		[Output] public FunctionProgress 退出节点;

        public override void FunctionDo(string PortName, List<object> param = null)
        {
			目标对象 = GetInputValue("目标对象", 目标对象);
			修改的值 = GetInputValue("修改的值", 修改的值);
			switch (修改) {
				case Method.全局位置:
					目标对象.transform.position = 修改的值;
					break;
				case Method.局部位置:
					目标对象.transform.localPosition = 修改的值;
					break;
				case Method.旋转:
					目标对象.transform.eulerAngles = 修改的值;
					break;
				case Method.缩放:
					目标对象.transform.localScale = 修改的值;
					break;

			}
			ConnectDo("退出节点");
		}
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port) {
			return null; // Replace this
		}
	} 
}