using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.自机
{
    public class 获取自机布尔值信息 : Node
    {
        [Input] public Character 自机;
        [Output] public bool 结果;
        NodePort 自机Port;
        public enum CharacterEnumData
        {
            Invincible = 0,
            countGrazeWhenNobody = 1,
            ControlAniSpeed = 2,
            GameOverRightNow = 3,
            DestroyingBullet = 4,
            isGone = 5
        }
       
        public CharacterEnumData 自机属性;
        public override object GetValue(NodePort port)
        {
            自机 = GetInputValue<Character>("自机", 自机); if (自机 == null) { return 0; }
            switch (自机属性)
            {
                case CharacterEnumData.Invincible:
                    结果 = 自机.Invincible;
                    break;
                case CharacterEnumData.countGrazeWhenNobody:
                    结果 = 自机.countGrazeWhenNobody;
                    break;
                case CharacterEnumData.ControlAniSpeed:
                    结果 = 自机.ControlAniSpeed;
                    break;
                case CharacterEnumData.GameOverRightNow:
                    结果 = 自机.GameOverRightNow;
                    break;
                case CharacterEnumData.DestroyingBullet:
                    结果 = 自机.DestroyingBullet;
                    break;
                case CharacterEnumData.isGone:
                    结果 = 自机.isGone;
                    break;
            }
            return 结果;
        }
    }
}