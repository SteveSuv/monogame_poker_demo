sampler2D TextureSampler : register(s0);
float4 PixelSize : register(c0);
float BlurAmount : register(c1);

float4 MainPS(float2 texCoord : TEXCOORD) : SV_Target
{
    float4 color = tex2D(TextureSampler, texCoord) * 0.2270270270;

    // 高斯模糊采样
    color += tex2D(TextureSampler, texCoord + float2(PixelSize.x * 1.0, 0)) * 0.3162162162;
    color += tex2D(TextureSampler, texCoord - float2(PixelSize.x * 1.0, 0)) * 0.3162162162;
    color += tex2D(TextureSampler, texCoord + float2(0, PixelSize.y * 1.0)) * 0.3162162162;
    color += tex2D(TextureSampler, texCoord - float2(0, PixelSize.y * 1.0)) * 0.3162162162;

    return color;
}

technique GaussianBlur
{
    pass P0
    {
        PixelShader = compile ps_4_0 MainPS();
    }
}
