using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

static class AssetsLoader
{
    // private GraphicsDevice _graphicsDevice;

    // public AssetsLoader(GraphicsDevice graphicsDevice)
    // {
    //     _graphicsDevice = graphicsDevice;
    // }
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
    // public SoundEffectInstance playSound(SoundEffect soundEffect, float pitch = 0, float volume = 1, bool isLooped = false)
    // {
    //     var soundEffectInstance = soundEffect.CreateInstance();
    //     soundEffectInstance.Pitch = pitch;
    //     soundEffectInstance.Volume = volume;
    //     soundEffectInstance.IsLooped = isLooped;
    //     soundEffectInstance.Play();
    //     return soundEffectInstance;
    // }

    // public SoundEffectInstance playSound(string path, float pitch = 0, float volume = 1, bool isLooped = false)
    // {
    //     var soundEffect = loadSoundEffect(path);
    //     var soundEffectInstance = soundEffect.CreateInstance();
    //     soundEffectInstance.Pitch = pitch;
    //     soundEffectInstance.Volume = volume;
    //     soundEffectInstance.IsLooped = isLooped;
    //     soundEffectInstance.Play();
    //     return soundEffectInstance;
    // }
}
