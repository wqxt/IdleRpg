Shader "Custom/PreciseOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}          // Основная текстура
        _OutlineColor ("Outline Color", Color) = (0, 1, 0, 1) // Цвет обводки
        _OutlineThickness ("Outline Thickness", Float) = 0.05 // Толщина обводки
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Name "OutlinePass"
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off 

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineThickness;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float outline = 0.0;

                // Проверяем соседние пиксели для обводки
                float2 offsets[8] = {
                    float2(-_OutlineThickness, 0), float2(_OutlineThickness, 0),
                    float2(0, -_OutlineThickness), float2(0, _OutlineThickness),
                    float2(-_OutlineThickness, -_OutlineThickness), float2(_OutlineThickness, -_OutlineThickness),
                    float2(-_OutlineThickness, _OutlineThickness), float2(_OutlineThickness, _OutlineThickness)
                };

                float alpha = tex2D(_MainTex, i.uv).a; // Альфа текущего пикселя

                for (int j = 0; j < 8; j++)
                {
                    float neighborAlpha = tex2D(_MainTex, i.uv + offsets[j]).a;
                    if (neighborAlpha < 0.1 && alpha > 0.1) // Если соседний пиксель прозрачный, а текущий непрозрачный
                    {
                        outline = 1.0;
                        break;
                    }
                }

                if (outline > 0.0)
                {
                    return _OutlineColor; // Обводка
                }

                // Возвращаем исходную текстуру
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}

