using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

static class AssetsLoader
{
    public static Texture2D LoadTexture2D(string path)
    {
        // return Texture2D.FromStream(MyGame.graphics.GraphicsDevice, new FileStream(path.Substring(1), FileMode.Open));
        return Texture2D.FromFile(MyGame.graphics.GraphicsDevice, path[1..]);
    }
    public static SoundEffect LoadSoundEffect(string path)
    {
        // return SoundEffect.FromStream(new FileStream(path.Substring(1), FileMode.Open));
        return SoundEffect.FromFile(path[1..]);
    }

    public static FontSystem LoadFont(string path)
    {
        var fontSystem = new FontSystem();
        fontSystem.AddFont(File.ReadAllBytes(path[1..]));
        return fontSystem;
    }
}
