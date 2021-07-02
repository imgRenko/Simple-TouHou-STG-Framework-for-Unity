Shader "Unlit/Distort"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DistortStrength("_DistortStrength", range(0,1)) = 0.2
        _DistortTimeFactor("_DistortTimeFactor", range(0,1)) = 1
    }
        SubShader
        {

            ZWrite Off

            //GrabPass
            GrabPass
            {
                //此处给出一个抓屏贴图的名称，抓屏的贴图就可以通过这张贴图来获取，而且每一帧不管有多个物体使用了该shader，只会有一个进行抓屏操作
                //如果此处为空，则默认抓屏到_GrabTexture中，但是据说每个用了这个shader的都会进行一次抓屏！
                "_GrabTempTex"
            }

            Pass
            {
                Tags
                {
                    "RenderType" = "Transparent"
                    "Queue" = "Transparent+1"
                }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                sampler2D _GrabTempTex;
                float4 _GrabTempTex_ST;

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;

                    float4 grabPos : TEXCOORD1;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _DistortStrength;
                float _DistortTimeFactor;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                    o.grabPos = ComputeGrabScreenPos(o.vertex);

                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                // 贴图位置偏移动画
                float4 offset = tex2D(_MainTex, i.uv - _Time.xy * _DistortTimeFactor);

                // 减去的像素大小 越大则越明显
                i.grabPos.xy -= offset.xy * _DistortStrength;

                //根据抓屏位置采样Grab贴图,tex2Dproj等同于tex2D(grabPos.xy / grabPos.w)
                fixed4 color = tex2Dproj(_GrabTempTex, i.grabPos);

                col = color;

                return col;
            }
            ENDCG
        }
        }
}