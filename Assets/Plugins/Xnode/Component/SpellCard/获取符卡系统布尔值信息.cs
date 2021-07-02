using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.符卡系统
{
    public class 获取符卡系统布尔值信息 : Node
    {
        [Input] public SpellCard 符卡系统;
        [Output] public bool 结果;
        public enum SpellCardData
        {
            Use = 0,
            显示击破时文字 = 1,
            用XML文件记录符卡收率 = 2,
            不销毁全屏子弹 = 3,
            不销毁符卡对象 = 4,
            无时间限制 = 5,
            自动使用下一张符卡 = 6,
            重置发射器时销毁全屏子弹 = 7,
            这是时符 = 8,
            显示BOSSHPGUI = 9,
            显示计时器 = 10,
            播放符卡动画 = 11,
            隐藏游戏原背景 = 12,
            允许改变关卡流程状态 = 13,
            在屏幕底下使用位置标记 = 14,
            启动游戏时自关闭 = 15,
            启用符卡移动器 = 16,
            于使用符卡前重置 = 17,
            可复用的移动器 = 18
        }
        public SpellCardData 符卡系统属性;
        public override object GetValue(NodePort port)
        {
            符卡系统 = GetInputValue<SpellCard>("符卡系统", null); if (符卡系统 == null) { return 0; }
            switch (符卡系统属性)
            {
                case SpellCardData.Use:
                    结果 = 符卡系统.Use;
                    break;
                case SpellCardData.显示击破时文字:
                    结果 = 符卡系统.ShowTip;
                    break;
                case SpellCardData.用XML文件记录符卡收率:
                    结果 = 符卡系统.spellRecord;
                    break;
                case SpellCardData.不销毁全屏子弹:
                    结果 = 符卡系统.DonDestroyBullets;
                    break;
                case SpellCardData.不销毁符卡对象:
                    结果 = 符卡系统.DonDestroySpell;
                    break;
                case SpellCardData.无时间限制:
                    结果 = 符卡系统.NoTimeLimit;
                    break;
                case SpellCardData.自动使用下一张符卡:
                    结果 = 符卡系统.nextSpell;
                    break;
                case SpellCardData.重置发射器时销毁全屏子弹:
                    结果 = 符卡系统.resetShootingDestroyingBullets;
                    break;
                case SpellCardData.这是时符:
                    结果 = 符卡系统.TimeSpell;
                    break;
                case SpellCardData.显示BOSSHPGUI:
                    结果 = 符卡系统.UseHP;
                    break;
                case SpellCardData.显示计时器:
                    结果 = 符卡系统.UseTimer;
                    break;
                case SpellCardData.播放符卡动画:
                    结果 = 符卡系统.ShowAnimation;
                    break;
                case SpellCardData.隐藏游戏原背景:
                    结果 = 符卡系统.HideDefaultBackGround;
                    break;
                case SpellCardData.允许改变关卡流程状态:
                    结果 = 符卡系统.ChangeRoadSign;
                    break;
                case SpellCardData.在屏幕底下使用位置标记:
                    结果 = 符卡系统.UseEnemyPositonSign;
                    break;
                case SpellCardData.启动游戏时自关闭:
                    结果 = 符卡系统.ObjectDisactive;
                    break;
                case SpellCardData.启用符卡移动器:
                    结果 = 符卡系统.enableThen;
                    break;
                case SpellCardData.于使用符卡前重置:
                    结果 = 符卡系统.resetBeforeSpellCard;
                    break;
                case SpellCardData.可复用的移动器:
                    结果 = 符卡系统.resetReuse;
                    break;
            }
            return 结果;
        }
    }
}