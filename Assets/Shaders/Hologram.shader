Shader "Custom/HologramColorShader"
{
    Properties
    {
        // Color base del holograma
        _MainColor("Color Base", Color) = (1,1,1,1)
        
        // Textura de ruido para la distorsión (sí es textura, pero ya no usamos imagen de “color”)
        _NoiseTex("Textura de Ruido", 2D) = "white" {}
        
        // Parámetros de distorsión y velocidad de "scroll" del ruido
        _ScrollSpeed("Velocidad de Scroll", Range(0,2)) = 1
        _DistortionScale("Escala de Distorsion", Range(0,1)) = 0.1
        
        // Control de opacidad general
        _Opacity("Opacidad", Range(0,1)) = 1
        
        // Efecto Fresnel para bordes
        _FresnelColor("Color de Borde (Fresnel)", Color) = (0,1,1,1)
        _FresnelPower("Intensidad Fresnel", Range(0,10)) = 2
    }

    SubShader
    {
        // Se renderiza como transparente y en la cola de transparencia
        Tags{ "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        // Mezcla alfa típica
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Estructuras para vértices y fragmentos
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos         : SV_POSITION;
                float3 worldPos    : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                float2 uv          : TEXCOORD0;
            };

            // Variables (samplers, colores, floats) 
            sampler2D _NoiseTex;
            float4    _NoiseTex_ST;
            
            float4 _MainColor;
            float  _ScrollSpeed;
            float  _DistortionScale;
            float  _Opacity;
            float4 _FresnelColor;
            float  _FresnelPower;

            // Vertex Shader
            v2f vert (appdata v)
            {
                v2f o;
                // Posición en espacio de clip
                o.pos = UnityObjectToClipPos(v.vertex);

                // UV para la textura de ruido (por si queremos manipularlos)
                o.uv = TRANSFORM_TEX(v.uv, _NoiseTex);

                // Pasamos datos de mundo para Fresnel
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));

                return o;
            }

            // Fragment Shader
            fixed4 frag (v2f i) : SV_Target
            {
                // Desplazamos el UV de ruido con el tiempo, para que “corra” verticalmente
                float offset = _Time.y * _ScrollSpeed;
                float2 noiseUV = i.uv;
                noiseUV.y += offset;

                // Obtenemos la intensidad de ruido
                float noiseValue = tex2D(_NoiseTex, noiseUV).r;

                // Calculamos una distorsión a partir del ruido
                // (Se podría usar más si quisiéramos desplazar algo, pero aquí queda como demostración)
                float2 distortion = (noiseValue - 0.5) * _DistortionScale;

                // Calculamos Fresnel (cómo se ven los bordes mirando hacia la cámara)
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float fresnelTerm = pow(1.0 - dot(i.worldNormal, viewDir), _FresnelPower);

                // Color base sólido
                fixed4 baseColor = _MainColor;
                
                // Mezclamos Fresnel con la parte del ruido para darle un matiz dinámico
                fixed4 fresnelColor = _FresnelColor * fresnelTerm * noiseValue;

                // Sumamos el color base + efecto Fresnel
                fixed4 finalColor = baseColor + fresnelColor;

                // Ajustamos opacidad total
                finalColor.a = _Opacity;

                return finalColor;
            }
            ENDCG
        }
    }

    // Se puede dejar un Fallback a Unlit/Transparent para asegurar compatibilidad
    Fallback "Unlit/Transparent"
}
