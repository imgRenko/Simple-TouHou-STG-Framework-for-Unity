using UnityEngine;

namespace 数学 {
    public class 读取向量 : XNode.Node {
        [Output] public float x, y, z;
        [Input] public Vector3 vector;

        public override object GetValue(XNode.NodePort port) {
			vector = GetInputValue<Vector3>("vector", vector);
			if (port.fieldName == "x")
            return vector.x;
            if (port.fieldName == "y")
                return vector.y;
            if (port.fieldName == "z")
                return vector.z;
            return 0;
        }
    }
}