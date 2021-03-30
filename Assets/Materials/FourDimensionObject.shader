Shader "Custom/FourDimensionObject"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color: COLOR;
                float4 pos0 : TEXCOORD0;
                float4 pos1 : TEXCOORD1;
                float4 pos2 : TEXCOORD2;
                float4 pos3 : TEXCOORD3;
            };

            struct v2g
            {
                float4 vertex : POSITION;
                float4 color: COLOR;
                float4 pos0 : TEXCOORD0;
                float4 pos1 : TEXCOORD1;
                float4 pos2 : TEXCOORD2;
                float4 pos3 : TEXCOORD3;
            };

            struct g2f
            {
                float4 position: SV_POSITION;
                float3 normal: TEXCOORD0;
                float4 color: COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float4x4 _FourDMatrix;
            uniform float _PositionW;

            v2g vert (appdata v)
            {
                v2g o;
                o.vertex = mul(unity_ObjectToWorld, v.vertex);
                o.color = v.color;
                o.pos0 = mul(_FourDMatrix, v.pos0) + float4(0.0, 0.0, 0.0, _PositionW);
                o.pos1 = mul(_FourDMatrix, v.pos1) + float4(0.0, 0.0, 0.0, _PositionW);
                o.pos2 = mul(_FourDMatrix, v.pos2) + float4(0.0, 0.0, 0.0, _PositionW);
                o.pos3 = mul(_FourDMatrix, v.pos3) + float4(0.0, 0.0, 0.0, _PositionW);
                return o;
            }

            [maxvertexcount(6)]
            void geom(point v2g input[1], inout TriangleStream<g2f> outStream)
            {
                int index = 0;
                v2g v = input[0];
                g2f t[4] = {
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), v.color
                    },
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), v.color
                    },
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), v.color
                    },
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), v.color
                    }
                };

                if(v.pos0.w * v.pos1.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos0.xyz, v.pos1.xyz, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos1.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    index++;
                }

                if(v.pos0.w * v.pos2.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos0.xyz, v.pos2.xyz, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos2.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    index++;
                }

                if(v.pos0.w * v.pos3.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos0.xyz, v.pos3.xyz, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos3.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    index++;
                }

                if(v.pos1.w * v.pos2.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos1.xyz, v.pos2.xyz, abs(v.pos1.w) / (abs(v.pos1.w) + abs(v.pos2.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    index++;
                }

                if(v.pos1.w * v.pos3.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos1.xyz, v.pos3.xyz, abs(v.pos1.w) / (abs(v.pos1.w) + abs(v.pos3.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    index++;
                }

                if(v.pos2.w * v.pos3.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos2.xyz, v.pos3.xyz, abs(v.pos2.w) / (abs(v.pos2.w) + abs(v.pos3.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    index++;
                }

                if(index < 4) {
                    float3 norm = UnityObjectToWorldNormal(normalize(cross(t[1].position.xyz - t[0].position.xyz, t[2].position.xyz - t[0].position.xyz)));
                    t[0].normal = norm;
                    t[1].normal = norm;
                    t[2].normal = norm;
                    t[0].position = UnityObjectToClipPos(t[0].position);
                    t[1].position = UnityObjectToClipPos(t[1].position);
                    t[2].position = UnityObjectToClipPos(t[2].position);
                    outStream.Append(t[0]);
                    outStream.Append(t[1]);
                    outStream.Append(t[2]);
                    outStream.RestartStrip();
                } else {
                    float3 norm = UnityObjectToWorldNormal(normalize(cross(t[1].position.xyz - t[0].position.xyz, t[2].position.xyz - t[0].position.xyz)));
                    t[0].normal = norm;
                    t[1].normal = norm;
                    t[2].normal = norm;
                    t[3].normal = norm;
                    t[0].position = UnityObjectToClipPos(t[0].position);
                    t[1].position = UnityObjectToClipPos(t[1].position);
                    t[2].position = UnityObjectToClipPos(t[2].position);
                    t[3].position = UnityObjectToClipPos(t[3].position);
                    outStream.Append(t[0]);
                    outStream.Append(t[1]);
                    outStream.Append(t[2]);
                    outStream.RestartStrip();
                    outStream.Append(t[3]);
                    outStream.Append(t[2]);
                    outStream.Append(t[1]);
                    outStream.RestartStrip();
                }
            }

            fixed4 frag (g2f i) : COLOR
            {
                fixed4 col = i.color;// * abs(dot(_WorldSpaceLightPos0.xyz, i.normal));
                return col;
            }
            ENDCG
        }
    }
}
