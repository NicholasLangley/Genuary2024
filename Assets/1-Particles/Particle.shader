Shader "Custom/NewSurfaceShader"
{

    SubShader
    {
        Pass
        {
			CGPROGRAM

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

            float3 _dir;
            float _age;

            struct VertexData{
                float4 position : POSITION;
            };

            struct v2f{
                float4 position : SV_POSITION;
            };

            v2f MyVertexProgram(VertexData v)
            {
                v2f i;
                i.position = UnityObjectToClipPos(v.position);
                return i;
            }

            float4 MyFragmentProgram(v2f i) : SV_TARGET
            {
                float3 color = _age + _dir * _age;
                return float4(color, 1.0);
            }

            ENDCG
        }

        
    }
    FallBack "Diffuse"
}
