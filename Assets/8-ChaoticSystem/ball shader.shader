Shader "Custom/pendulumBallShader"
{

    SubShader
    {
        Pass
        {
			CGPROGRAM

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

            float2 _vel;

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
                return float4(0, abs(_vel.y )/5, abs(_vel.x)/5, 1.0);
            }

            ENDCG
        }

        
    }
    FallBack "Diffuse"
}
