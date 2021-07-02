using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.符卡系统
{
    public class 设置符卡系统游戏对象信息 : Node
    {
        [Input] public FunctionProgress 进入节点;[Input] public GameObject 目的值;[Input] public SpellCard 符卡系统;
        [Output] public FunctionProgress 退出节点;
        public enum SpellCardData
        {
            要显示的BOSSHPGUI游戏对象 = 0,
            符卡背景游戏对象 = 1,
            含有动态立绘的对象 = 2
        }
        public SpellCardData 符卡系统属性;
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            符卡系统 = GetInputValue<SpellCard>("符卡系统", null); if (符卡系统 == null) return; 目的值 = GetInputValue<GameObject>("目的值", 目的值); switch (符卡系统属性)
            {
                case SpellCardData.要显示的BOSSHPGUI游戏对象:
                    Global.bossHP = 目的值.GetComponent<BOSSHP>();
                    break;
                case SpellCardData.符卡背景游戏对象:
                    符卡系统.SpellCardBG = 目的值;
                    break;
                case SpellCardData.含有动态立绘的对象:
                    符卡系统.DynamicPictureObject = 目的值;
                    break;
            }
            ConnectDo("退出节点");
        }
    }
}