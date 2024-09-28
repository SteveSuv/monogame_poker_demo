using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

static class AssetsLoader
{
    public static Texture2D loadTexture2D(string path)
    {
        return Texture2D.FromFile(MyGame.graphics.GraphicsDevice, path.Substring(1));
    }
    public static SoundEffect loadSoundEffect(string path)
    {
        return SoundEffect.FromFile(path.Substring(1));
    }

    public static FontSystem loadFont(string path)
    {
        var fontSystem = new FontSystem();
        fontSystem.AddFont(File.ReadAllBytes(path.Substring(1)));
        return fontSystem;
    }
}
