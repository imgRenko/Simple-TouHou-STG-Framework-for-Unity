using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 获取子弹颜色信息 : Node
    {
        [Input] public Bullet 子弹;
        [Output] public Color 结果;
        public enum BulletData
        {
            子弹颜色, 子弹销毁时颜色
        }
        public BulletData 子弹属性;
        public override object GetValue(NodePort port)
        {
            子弹 = GetInputValue<Bullet>("子弹"); 
            if (子弹 == null) {
                结果 = Color.white;
                return 结果; 
            }
            switch (子弹属性)
            {

                case BulletData.子弹颜色:
                    结果 =  子弹.BulletColor;
                    break;
                case BulletData.子弹销毁时颜色:
                    结果 = 子弹.BrokenBulletColor;
                    break;
            }
            return 结果;
        }
    }
}