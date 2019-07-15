Shader "Custom/ChipFaceShader" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
    }
    SubShader {
        Pass {
            Material {
                Diffuse [_Color]
            }
            Lighting On
            // Blend in the alpha texture using the lerp operator
            SetTexture [_MainTex] {
                combine texture lerp (texture) previous
            }
        }
    }
}
