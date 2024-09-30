using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

class SpriteAtlas
{
    public Texture2D texture;
    public string Name => texture.Name;
    public required int regionWidth;
    public required int regionHeight;
    public required int maxRegionCount;
    public int margin = 0;
    public int spacing = 0;
    private Texture2DAtlas Atlas;

    public SpriteAtlas(Texture2D texture)
    {
        this.texture = texture;
        Console.WriteLine(Name);

    }

    public Texture2DAtlas GetAtlas()
    {
        if (Atlas != null)
        {
            return Atlas;
        }
        else
        {
            Atlas = Texture2DAtlas.Create(name: Name, texture: texture, regionWidth: regionWidth, regionHeight: regionHeight, maxRegionCount: maxRegionCount, margin: margin, spacing: spacing);
            return Atlas;
        }
    }
}