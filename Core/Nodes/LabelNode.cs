using FontStashSharp;
using Microsoft.Xna.Framework;

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
    public new Vector2 Size => Font.MeasureString(text: text, scale: scale, characterSpacing: characterSpacing, lineSpacing: lineSpacing, effect: effect, effectAmount: effectAmount);


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.DrawString(font: Font, text: text, position: WorldPosition, color: color, rotation: rotation, origin: OriginOffset, scale: scale, layerDepth: LayerDepth, characterSpacing: characterSpacing, lineSpacing: lineSpacing, textStyle: textStyle, effect: effect, effectAmount: effectAmount);
        base.Draw();
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}