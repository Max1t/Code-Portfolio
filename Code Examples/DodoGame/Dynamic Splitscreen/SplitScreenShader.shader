 
Shader "Hidden/Custom/SplitScreen"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    sampler2D _OtherTex;
	sampler2D _MaskTex;

    
    float4 Frag(VaryingsDefault i) : SV_Target
    {
	   float4 colorMain = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
	   float4 color2 = tex2D(_OtherTex, i.texcoord);
	   float4 colorMask = tex2D(_MaskTex, i.texcoord);


	   float4 c = lerp(colorMain, color2, colorMask.r);
	   return c;
    }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

            #pragma vertex VertDefault
            #pragma fragment Frag

            ENDHLSL
        }
    }
}