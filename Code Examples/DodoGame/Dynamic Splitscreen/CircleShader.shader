Shader "Metropolia/Circle"
{
	Properties
	{
		_MainTex("Main Tex", 2D) = "white" {}
		_OffsetX("Offset X", float) = 0
		_OffsetY("Offset Y", float) = 0
		_Radius("Radius", float) = 1
		_LineWidth("Line Width", float) = 0.01
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			float _OffsetX;
			float _OffsetY;
			float _Radius;
			float _LineWidth;
			sampler2D _MainTex;

			float Circle(float2 p)
			{
				p.x *= _ScreenParams.x / _ScreenParams.y;
				float2 o = float2(_OffsetX, _OffsetY);
				return (length(p - o) + 1 - _Radius);
			}

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
				float r = Circle(i.uv);
				if (r > 1.0 - _LineWidth)
				{
					return fixed4(0, 0, 0, 1);
				}
				return c;
			}

			ENDCG
		}
	}
}