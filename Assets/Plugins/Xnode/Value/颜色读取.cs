using UnityEngine;
using XNode;
namespace 变量 {
    public class 颜色读取 : XNode.Node {
        [Output] public float r, g, b,a;
        [Input] public Color color;

        public override object GetValue(XNode.NodePort port) {
            color = GetInputValue<Color>("color", color);
			if (port.fieldName == "r")
				return color.r;
            if (port.fieldName == "g")
                return color.g;
            if (port.fieldName == "b")
                return color.b;
			            if (port.fieldName == "a")
                return color.a;
            return 0;
        }
    }
}