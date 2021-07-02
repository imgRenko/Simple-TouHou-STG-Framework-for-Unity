using UnityEngine;
using XNode;
namespace 变量 {
    public class 生成颜色 : XNode.Node {
        [Input] public float r, g, b,a;
        [Output] public Color color = new Color(255,255,255,255);

        public override object GetValue(XNode.NodePort port) {
            color.r = GetInputValue<float>("r", this.r);
            color.g = GetInputValue<float>("g", this.g);
            color.b = GetInputValue<float>("b", this.b);
			  color.a = GetInputValue<float>("a", this.a);
            return color;
        }
    }
}