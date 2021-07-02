using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.声效
{
	public class 播放声效 : Node
	{
		[Input] public FunctionProgress 进入节点;

	 public AudioClip 声音段;
	
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			GameObject temp = GameObject.Instantiate(Global.RadioClipNormal);
			AudioSource au = temp.GetComponent<AudioSource>();
			au.clip = 声音段;
			Destroy(temp, 2f);
			AudioQueue.Play(au);
			ConnectDo("退出节点");
		}
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}