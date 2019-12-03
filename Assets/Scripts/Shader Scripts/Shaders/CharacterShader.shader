Shader "Custom/CharacterShader" 
{
Properties {
    _Color ("Color", 2D) = "white" {}
    _CubeMap ("CubeMap", Cube) = "_Skybox" {}
    _HiddenColor ("Hidden Color", Color) = (1,1,1,1)
		   }
	SubShader {

	Tags { 
		"Queue"="Geometry+5"
		"RenderType"="Opaque"
	}
	Pass
	{
        Name "OpaquePass"
		
		ZWrite Off
		ZTest LEqual
		Lighting Off

		CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
		uniform float4 _HiddenColor;

		struct VertexInput {
            float4 vertex : POSITION;
        };

        struct VertexOutput {
            float4 pos : SV_POSITION;
        };

		VertexOutput vert (VertexInput v) {
            VertexOutput o;
            o.pos = UnityObjectToClipPos(v.vertex);
            return o;
        }

        fixed4 frag(VertexOutput i) : COLOR {
			float4 overlayColor = _HiddenColor;
			return overlayColor;
		}
		ENDCG
	}
}
}