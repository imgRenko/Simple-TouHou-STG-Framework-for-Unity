Shader "Unlit/FogWithDepthTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FogDensity("FogDensity",Float) = 1.0
        _FogColor("FogColor",Color) = (1,1,1,1)
        _FogStart("FogStart",Float) = 0.0
        _FogEnd("FogEnd",Float) = 1.0
    }
    SubShader
    {

        CGINCLUDE


            #include "UnityCG.cginc"

            float4x4 _FrustumCornersRay;

            sampler2D _MainTex;
            half4 _MainTex_TexelSize;
            sampler2D _CameraDepthTexture;
            half _FogDensity;
            fixed4 _FogColor;
            float _FogStart;
            float _FogEnd;

            struct v2f
            {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
                half2 uv_depth : TEXCOORD1;
                float4 interpolatedRay : TEXCOORD2;
            };

            v2f vert(appdata_img v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = v.texcoord;
                o.uv_depth = v.texcoord;

                #if UNITY_UV_STARTS_AT_TOP
                if(_MainTex_TexelSize.y < 0)
                    o.uv_depth.y = 1 - o.uv_depth.y;
                #endif

                int index = 0;
                if(v.texcoord.x < 0.5 && v.texcoord.y < 0.5)
                {
                    index = 0;
                }
                else if(v.texcoord.x > 0.5 && v.texcoord.y < 0.5)
                {
                    index = 1;
                }
                else if(v.texcoord.x > 0.5 && v.texcoord.y > 0.5)
                {
                    index = 2;
                }
                else
                {
                    index = 3;
                }

                #if UNITY_UV_STARTS_AT_TOP
                if(_MainTex_TexelSize.y < 0)
                    index = 3 - index;
                #endif

                o.interpolatedRay = _FrustumCornersRay[index];

                return o;
            }

            float GetFogRatioByDistance(float3 worldPos)
            {
                float f = 1 - (_FogEnd - abs(worldPos.z - _WorldSpaceCameraPos.z))/(_FogEnd - _FogStart);
                return f;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float linearDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv_depth));

                float3 worldPos = _WorldSpaceCameraPos + linearDepth * i.interpolatedRay.xyz;

                //float fogDensity = (_FogEnd - worldPos.y) / (_FogEnd - _FogStart);    //若想基于屏幕高度来进行雾的渲染，可以使用这行代码,并屏蔽下面一行的代码
                float fogDensity = GetFogRatioByDistance(worldPos);
                fogDensity = saturate(fogDensity * _FogDensity);

                fixed4 finalColor = tex2D(_MainTex,i.uv);
                finalColor.rgb = lerp(finalColor.rgb,_FogColor.rgb,fogDensity);

                return finalColor;
            }



        ENDCG


        Pass
        {
            Ztest Always Cull Off Zwrite Off

            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

            ENDCG

        }
    }

    Fallback Off
}

