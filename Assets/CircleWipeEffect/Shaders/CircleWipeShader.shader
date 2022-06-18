Shader "Unlit/CircleWipeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FadeTex("Fade Texture", 2D) = "white" {}
        _Radius ("Wipe Radius", Float) = 0
        _Horizontal("Horizontal ratio", Float) = 0
        _Vertical("Vertical ratio", Float) = 0
        _RadiusSpeed("Radius Speed", Float) = 1
        _FadeColour("Fade Colour", Color) = (0, 0, 0, 0)
        _Offset("Offset", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _FadeTex;
            float4 _MainTex_ST;
            float _Radius;
            float _Horizontal;
            float _Vertical;
            float _RadiusSpeed;
            fixed4 _FadeColour : COLOR;
            float4 _Offset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 fadeCol = _FadeColour * tex2D(_FadeTex, i.uv);
                float3 pos = float3((i.uv.x - _Offset.x - 0.5) / _Vertical,
                                    (i.uv.y - _Offset.y - 0.5) / _Horizontal, 0);


                return length(pos) > _Radius / _RadiusSpeed ? fadeCol : col;
            }
            ENDCG
        }
    }
}
