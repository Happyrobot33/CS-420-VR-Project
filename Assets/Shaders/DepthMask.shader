Shader "Custom/DepthMask" {
    SubShader {
        Tags {"Queue" = "Geometry+501" }

        // Don't draw in the RGBA channels; just the depth buffer:

        ColorMask 0 // Enables color writes to none of the channels.
        ZWrite On // Enables writing to the depth buffer.
 
        // Pass = contains instructions for setting the state of the GPU, and the shader programs that run on the GPU
        // Do nothing specific in the pass:
        Pass {}
    }
}