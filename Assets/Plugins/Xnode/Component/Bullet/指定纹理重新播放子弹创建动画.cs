using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹
{
    [NodeWidth(320)]
    public class 指定纹理重新播放子弹创建动画 : Node
    {
        [InfoBox("理论可以使用任何动画。如果调用销毁动画，子弹将销毁，可前往子弹原型处创建新的动画。如果创建动画没有播放，这个节点会被忽略。")]
        [Input] public FunctionProgress 进入节点;
        [Input] public Sprite 初始纹理;
        [Input] public Bullet 子弹;
        [Output] public FunctionProgress 退出节点;
        // Use this for initialization
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            子弹 = GetInputValue("子弹", 子弹);
            if (子弹.CreateAnimationPlayed == false)
            {
                ConnectDo("退出节点");
                return;
            }
            初始纹理 = GetInputValue("初始纹理", 初始纹理);
            子弹.BulletSpriteRenderer.sprite = 初始纹理;
            子弹.SetSignedFrame(子弹.TotalLiveFrame);
            子弹.CreateAnimationPlayed = false;
          //  子弹.AnimationControl.enabled = true;
            //子弹.AnimationControl.PlayInFixedTime(动画名称, 0, 0);
            ConnectDo("退出节点");
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}