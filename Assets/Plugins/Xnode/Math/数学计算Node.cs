using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学
{
    public class 数学计算Node : XNode.Node
    {
        // Adding [Input] or [Output] is all you need to do to register a field as a valid port on your node 
        [Input(backingValue = ShowBackingValue.Unconnected)] public float a;
        [Input(backingValue = ShowBackingValue.Unconnected)] public float b;
        // The value of an output node field is not used for anything, but could be used for caching output results
        [Output] public Anything result;

        // Will be displayed as an editable field - just like the normal inspector
        public MathType 数学计算方式 = MathType.相加;
        public enum MathType { 相加, 相减, 相乘, 相除, 取余,最大值,最小值,近似, 坐标轴夹角, Log,Log10,柏林噪声,N次方,乒乓,重复 }
        NodePort ap, bp;
        protected override void Init()
        {
            base.Init();
            ap = GetPort("a");
            bp = GetPort("b");
        }
        // GetValue should be overridden to return a value for any specified output port
        public override object GetValue(XNode.NodePort port)
        {
            
            // Get new a and b values from input connections. Fallback to field values if input is not connected
            float a = ap.GetInputValue<float>();
            float b = bp.GetInputValue<float>();

            // After you've gotten your input values, you can perform your calculations and return a value
            object result = 0;
            if (port.fieldName == "result")
                switch (数学计算方式)
                {
                    case MathType.相加: default: result = (float)(a + b); break;
                    case MathType.相减: result = (float)(a - b); break;
                    case MathType.相乘: result = (float)(a * b); break;
                    case MathType.相除: result = (float)(a / b); break;
                    case MathType.取余: result = (float)(a % b); break;
                    case MathType.最大值:result = Mathf.Max(a, b); break;
                    case MathType.最小值: result = Mathf.Max(a, b); break;
                    case MathType.近似: result = Mathf.Approximately(a, b); break;
                    case MathType.坐标轴夹角: result = Mathf.DeltaAngle(a, b); break;
                    case MathType.Log: result = Mathf.Log(a, b); break;
           
                    case MathType.柏林噪声: result = Mathf.PerlinNoise(a, b); break;

                    case MathType.N次方: result = Mathf.Pow(a, b); break;
                    case MathType.乒乓: result = Mathf.PingPong(a, b); break;
                    case MathType.重复: result = Mathf.Repeat(a, b); break;




                }
            return result;
        }
    }
}