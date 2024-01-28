Shader "Custom/pendulumBallShader"
{

    SubShader
    {
        Pass
        {
			CGPROGRAM

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

            float _angularVel;

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
                return float4(0, max(0.2, _angularVel * -1 /2), max(0.2, abs(_angularVel)/2), 1.0);
            }

            ENDCG
        }

        
    }
    FallBack "Diffuse"
}
