using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 流程控制
{
	[System.Serializable]
	public class FrameRangeInfo
	{
		public float min, max;
		public string minS, maxS;
	}
	[NodeWidth(260)]
	public class 范围判断分支 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public float 输入;
		[Output] public FunctionProgress 其他情况;
		public float 最小范围, 最大范围;
		public bool 包含等于号;
		[HideInInspector]
		public List<FrameRangeInfo> frameRangeInfos = new List<FrameRangeInfo>();
		[Button]
		public void 添加范围()
		{
			if (最小范围 == 最大范围)
			{
				Debug.LogError("最小范围，最大范围不能相等");
				return;
			}
			if (最小范围 > 最大范围)
			{
				Debug.LogError("最小范围大于最大范围");
				return;
			}
			AddDynamicOutput(typeof(FunctionProgress), ConnectionType.Override, TypeConstraint.None, 最小范围.ToString() + "-" + 最大范围.ToString());
			Init();
		}
		[Button]
		public void 删除范围()
		{
			
			
			

			RemoveDynamicPort(最小范围.ToString() + "-" + 最大范围.ToString());
			Init();

		}
        protected override void Init()
        {
			frameRangeInfos.Clear();
			List<NodePort> dynamicPorts = new List<NodePort>(DynamicPorts);
			for (int i = 0; i != dynamicPorts.Count; ++i) {
				string[] minInfo =  dynamicPorts[i].fieldName.Split('-');
				FrameRangeInfo info = new FrameRangeInfo();
				info.min = System.Convert.ToSingle(minInfo[0]);
				info.max = System.Convert.ToSingle(minInfo[1]);
				info.minS = info.min.ToString();
				info.maxS = info.max.ToString();
				frameRangeInfos.Add(info);
			}
			Debug.Log("已更新范围判断数据");
        }
        // Use this for initialization
        public override void FunctionDo(string PortName, List<object> param = null)
		{
			
			输入 = GetInputValue("输入", 输入);
			bool yes = false;
			foreach (var item in frameRangeInfos)
			{
				if (输入 > item.min && 输入 < item.max && !包含等于号)
				{
					
					ConnectDo(item.minS + "-" + item.maxS);
					yes = true;
				}
				if (输入 >= item.min && 输入 <= item.max && 包含等于号)
				{
					ConnectDo(item.minS + "-" + item.maxS);
					yes = true;
				}
			}
			if (!yes)
				ConnectDo("其他情况");
		}

	
	}
}