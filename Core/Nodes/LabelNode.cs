using FontStashSharp;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

class LabelNode : Node
{
    private SpriteFontBase Font => font.GetFont(fontSize);
    public FontSystem font = Assets.FontYuanTi;
    public float fontSize = 20;
    public string text = "";

    public float characterSpacing = 0;
    public float lineSpacing = 0;
    public TextStyle textStyle = TextStyle.None;
    public FontSystemEffect effect = FontSystemEffect.None;
    public int effectAmount = 1;
    public Vector2 Size => Font.MeasureString(text: text, scale: Transform.scale, characterSpacing: characterSpacing, lineSpacing: lineSpacing, effect: effect, effectAmount: effectAmount);
    public Vector2 OriginOffset => Transform.origin * Size;
    public RectangleF Rectangle => new(Transform.WorldPosition - OriginOffset, Size);

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.DrawString(font: Font, text: text, position: Transform.WorldPosition, color: Transform.color, rotation: Transform.rotation, origin: OriginOffset, scale: Transform.scale, layerDepth: Transform.layerDepth, characterSpacing: characterSpacing, lineSpacing: lineSpacing, textStyle: textStyle, effect: effect, effectAmount: effectAmount);
        // DrawDebug();
        base.Draw();
    }

    // private void DrawDebug()
    // {
    //     if (true)
    //     {
    //         MyGame.SpriteBatch.DrawRectangle(Rectangle, MyGame.DebugColor);
    //         MyGame.SpriteBatch.DrawPoint(position: Rectangle.Center, color: MyGame.DebugColor, size: 4);
    //     }
    // }
}