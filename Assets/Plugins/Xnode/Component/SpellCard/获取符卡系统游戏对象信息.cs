using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.符卡系统
{
    public class 获取符卡系统游戏对象信息 : Node
    {
        [Input] public SpellCard 符卡系统;
        [Output] public GameObject 结果;
        public enum SpellCardData
        {
            要显示的BOSSHPGUI游戏对象 = 0,
            符卡背景游戏对象 = 1,
            含有动态立绘的对象 = 2
        }
        public SpellCardData 符卡系统属性;
        public override object GetValue(NodePort port)
        {
            符卡系统 = GetInputValue<SpellCard>("符卡系统", null); if (符卡系统 == null) { return 0; }
            switch (符卡系统属性)
            {
                case SpellCardData.要显示的BOSSHPGUI游戏对象:
                    结果 = Global.bossHP.gameObject;
                    break;
                case SpellCardData.符卡背景游戏对象:
                    结果 = 符卡系统.SpellCardBG;
                    break;
                case SpellCardData.含有动态立绘的对象:
                    结果 = 符卡系统.DynamicPictureObject;
                    break;
            }
            return 结果;
        }
    }
}