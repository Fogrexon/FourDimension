Shader "Game/Custom/FourDimensionObjectAO"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Pulse ("Pulse", Float) = 0
    }
    SubShader
    {
        LOD 100
        Cull Off

        Pass {
            Tags { "LightMode" = "ShadowCaster" }
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
            uniform float4 _Position;

            v2g vert (appdata v)
            {
                v2g o;
                o.vertex = mul(unity_ObjectToWorld, v.vertex);
                o.color = v.color;
                o.pos0 = mul(_FourDMatrix, v.pos0) + _Position;
                o.pos1 = mul(_FourDMatrix, v.pos1) + _Position;
                o.pos2 = mul(_FourDMatrix, v.pos2) + _Position;
                o.pos3 = mul(_FourDMatrix, v.pos3) + _Position;
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
                    float3 norm = normalize(cross(t[1].position.xyz - t[0].position.xyz, t[2].position.xyz - t[0].position.xyz));
                    t[0].normal = norm;
                    t[1].normal = norm;
                    t[2].normal = norm;
                    t[0].position = mul(UNITY_MATRIX_VP, t[0].position);
                    t[1].position = mul(UNITY_MATRIX_VP, t[1].position);
                    t[2].position = mul(UNITY_MATRIX_VP, t[2].position);
                    outStream.Append(t[0]);
                    outStream.Append(t[1]);
                    outStream.Append(t[2]);
                    outStream.RestartStrip();
                } else {
                    float3 norm = normalize(cross(t[1].position.xyz - t[0].position.xyz, t[2].position.xyz - t[0].position.xyz));
                    t[0].normal = norm;
                    t[1].normal = norm;
                    t[2].normal = norm;
                    t[3].normal = norm;
                    t[0].position = mul(UNITY_MATRIX_VP, t[0].position);
                    t[1].position = mul(UNITY_MATRIX_VP, t[1].position);
                    t[2].position = mul(UNITY_MATRIX_VP, t[2].position);
                    t[3].position = mul(UNITY_MATRIX_VP, t[3].position);
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

        Pass
        {
            Tags { "RenderType"="Opaque" }
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
                // world position
                float4 pos0 : TEXCOORD0;
                float4 pos1 : TEXCOORD1;
                float4 pos2 : TEXCOORD2;
                float4 pos3 : TEXCOORD3;
                // local position
                float4 pos0_o : TEXCOORD4;
                float4 pos1_o : TEXCOORD5;
                float4 pos2_o : TEXCOORD6;
                float4 pos3_o : TEXCOORD7;
            };

            struct g2f
            {
                float4 position: SV_POSITION;
                float3 normal: TEXCOORD0;
                float4 edge: TEXCOORD1;
                float4 color: COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float4x4 _FourDMatrix;
            uniform float4 _Position;
            float _Pulse;

            v2g vert (appdata v)
            {
                v2g o;
                o.vertex = mul(unity_ObjectToWorld, v.vertex);
                o.color = v.color;
                o.pos0 = mul(_FourDMatrix, v.pos0) + _Position;
                o.pos1 = mul(_FourDMatrix, v.pos1) + _Position;
                o.pos2 = mul(_FourDMatrix, v.pos2) + _Position;
                o.pos3 = mul(_FourDMatrix, v.pos3) + _Position;
                o.pos0_o = v.pos0;
                o.pos1_o = v.pos1;
                o.pos2_o = v.pos2;
                o.pos3_o = v.pos3;
                return o;
            }

            [maxvertexcount(6)]
            void geom(point v2g input[1], inout TriangleStream<g2f> outStream)
            {
                int index = 0;
                v2g v = input[0];
                g2f t[4] = {
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), float4(0.0, 0.0, 0.0, 0.0), v.color
                    },
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), float4(0.0, 0.0, 0.0, 0.0), v.color
                    },
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), float4(0.0, 0.0, 0.0, 0.0), v.color
                    },
                    {
                        float4(0.0, 0.0, 0.0, 1.0), float3(1.0, 0.0, 0.0), float4(0.0, 0.0, 0.0, 0.0), v.color
                    }
                };

                if(v.pos0.w * v.pos1.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos0.xyz, v.pos1.xyz, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos1.w)));
                    float4 pos_o = lerp(v.pos0_o, v.pos1_o, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos1.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    t[index].edge = pos_o;

                    index++;
                }

                if(v.pos0.w * v.pos2.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos0.xyz, v.pos2.xyz, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos2.w)));
                    float4 pos_o = lerp(v.pos0_o, v.pos2_o, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos2.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    t[index].edge = pos_o;

                    index++;
                }

                if(v.pos0.w * v.pos3.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos0.xyz, v.pos3.xyz, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos3.w)));
                    float4 pos_o = lerp(v.pos0_o, v.pos3_o, abs(v.pos0.w) / (abs(v.pos0.w) + abs(v.pos3.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    t[index].edge = pos_o;

                    index++;
                }

                if(v.pos1.w * v.pos2.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos1.xyz, v.pos2.xyz, abs(v.pos1.w) / (abs(v.pos1.w) + abs(v.pos2.w)));
                    float4 pos_o = lerp(v.pos1_o, v.pos2_o, abs(v.pos1.w) / (abs(v.pos1.w) + abs(v.pos2.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    t[index].edge = pos_o;

                    index++;
                }

                if(v.pos1.w * v.pos3.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos1.xyz, v.pos3.xyz, abs(v.pos1.w) / (abs(v.pos1.w) + abs(v.pos3.w)));
                    float4 pos_o = lerp(v.pos1_o, v.pos3_o, abs(v.pos1.w) / (abs(v.pos1.w) + abs(v.pos3.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    t[index].edge = pos_o;

                    index++;
                }

                if(v.pos2.w * v.pos3.w <= 0 && index < 4)
                {
                    float3 pos = lerp(v.pos2.xyz, v.pos3.xyz, abs(v.pos2.w) / (abs(v.pos2.w) + abs(v.pos3.w)));
                    float4 pos_o = lerp(v.pos2_o, v.pos3_o, abs(v.pos2.w) / (abs(v.pos2.w) + abs(v.pos3.w)));
                    t[index].position = float4(pos.x, pos.y, pos.z, 1.0);
                    t[index].edge = pos_o;

                    index++;
                }

                if(index < 4) {
                    float3 norm = normalize(cross(t[1].position.xyz - t[0].position.xyz, t[2].position.xyz - t[0].position.xyz));
                    t[0].normal = norm;
                    t[1].normal = norm;
                    t[2].normal = norm;
                    t[0].position = mul(UNITY_MATRIX_VP, t[0].position);
                    t[1].position = mul(UNITY_MATRIX_VP, t[1].position);
                    t[2].position = mul(UNITY_MATRIX_VP, t[2].position);
                    outStream.Append(t[0]);
                    outStream.Append(t[1]);
                    outStream.Append(t[2]);
                    outStream.RestartStrip();
                } else {
                    float3 norm = normalize(cross(t[1].position.xyz - t[0].position.xyz, t[2].position.xyz - t[0].position.xyz));
                    t[0].normal = norm;
                    t[1].normal = norm;
                    t[2].normal = norm;
                    t[3].normal = norm;
                    t[0].position = mul(UNITY_MATRIX_VP, t[0].position);
                    t[1].position = mul(UNITY_MATRIX_VP, t[1].position);
                    t[2].position = mul(UNITY_MATRIX_VP, t[2].position);
                    t[3].position = mul(UNITY_MATRIX_VP, t[3].position);
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
                float edgeX = lerp(abs(i.edge.x), 0.0, step(0.5, abs(i.edge.x) + 0.00001));
                float edgeY = lerp(abs(i.edge.y), 0.0, step(0.5, abs(i.edge.y) + 0.00001));
                float edgeZ = lerp(abs(i.edge.z), 0.0, step(0.5, abs(i.edge.z) + 0.00001));
                float edgeW = lerp(abs(i.edge.w), 0.0, step(0.5, abs(i.edge.w) + 0.00001));
                float ao = sqrt((edgeX * edgeX + edgeY * edgeY + edgeZ * edgeZ) / 3.0);
                float edge = max(max(max(edgeX, edgeY), edgeZ), edgeW);

                float white = 2.0 * exp(- _Pulse * 2.0) * (cos(_Time.y * 3.0) * 0.2 * max(0.0, min(1.0, 1.3 - _Pulse * 3.0)) + 0.8);
                fixed4 col = lerp(
                    float4(white, white, white, 1.0),
                    lerp(float4(0.0, 0.0, 0.0, 1.0), float4(0.15, 0.15, 0.15, 1.0), ao * 2.0),
                    step(edge, 0.49)
                );
                return col;
            }
            ENDCG

        }
    }
    FallBack "Diffuse"
}
