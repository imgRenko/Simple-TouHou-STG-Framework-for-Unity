using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.对象
{
	public class 获取游戏对象信息 : Node {
		
		public enum Method {
			全局位置,
			局部位置,
			旋转,
			缩放
		}
		[Output] public Vector3 修改的值;
		[Input] public GameObject 目标对象;
		public Method 修改;

        public override object GetValue(NodePort port) {
			目标对象 = GetInputValue("目标对象", 目标对象);
			if (目标对象 != null){
			switch (修改) {
				case Method.全局位置:
					return 目标对象.transform.position;
					//break;
				case Method.局部位置:
					return 目标对象.transform.localPosition ;
					//break;
				case Method.旋转:
					return 目标对象.transform.eulerAngles;
					//break;
				case Method.缩放:
					return 目标对象.transform.localScale;
					//break;

			}
			}
			return null;
		}
	} 
}