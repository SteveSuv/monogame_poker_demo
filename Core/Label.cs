using FontStashSharp;
using Microsoft.Xna.Framework;

class Label(string text) : Actor
{
    public DynamicSpriteFont Font => Assets.FontPuhuiti.GetFont(fontSize);
    public float fontSize = 20;
    public string text = text;
    public float characterSpacing = 0;
    public float lineSpacing = 0;
    public TextStyle textStyle = TextStyle.None;
    public FontSystemEffect effect = FontSystemEffect.None;
    public int effectAmount = 1;

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch.DrawString(font: Font, text: text, position: position, color: color, rotation: rotation, origin: origin * Size, scale: scale, layerDepth: layerDepth, characterSpacing: characterSpacing, lineSpacing: lineSpacing, textStyle: textStyle, effect: effect, effectAmount: effectAmount);
        DrawDebug();
    }

    public override Vector2 GetSize()
    {
        return Font.MeasureString(text: text, scale: scale, characterSpacing: characterSpacing, lineSpacing: lineSpacing, effect: effect, effectAmount: effectAmount);
    }
}