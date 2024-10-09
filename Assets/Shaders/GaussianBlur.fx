sampler2D InputTexture : register(s0);
float2 TexelSize;

float4 MainPS(float2 texCoord : TEXCOORD) : COLOR
{
    float4 color = float4(0, 0, 0, 0);
    
    // 高斯模糊权重
    float weights[5] = { 0.22758, 0.22456, 0.19627, 0.12862, 0.05449 };

    // 将纹理坐标移动到周围的像素
    for (int i = -2; i <= 2; i++)
    {
        color += tex2D(InputTexture, texCoord + float2(i * TexelSize.x, 0.0)) * weights[i + 2];
        color += tex2D(InputTexture, texCoord + float2(0.0, i * TexelSize.y)) * weights[i + 2];
    }
    
    return color;
}

technique GaussianBlur
{
    pass P0
    {
        PixelShader = compile ps_2_0 MainPS();
    }
}
