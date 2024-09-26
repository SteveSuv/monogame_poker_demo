using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

class AssetsLoader
{
    private GraphicsDevice _graphicsDevice;

    public AssetsLoader(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;

        FontSystemDefaults.FontResolutionFactor = 2;
        FontSystemDefaults.KernelWidth = 2;
        FontSystemDefaults.KernelHeight = 2;
    }
    public Texture2D loadTexture2D(string path)
    {
        return Texture2D.FromStream(_graphicsDevice, new FileStream(path, FileMode.Open));
    }
    public SoundEffect loadSoundEffect(string path)
    {
        return SoundEffect.FromStream(new FileStream(path, FileMode.Open));
    }

    public FontSystem loadFont(string path)
    {
        var fontSystem = new FontSystem();
        fontSystem.AddFont(File.ReadAllBytes(path));
        return fontSystem;
    }
    public SoundEffectInstance playSound(SoundEffect soundEffect, float pitch = 0, float volume = 1, bool isLooped = false)
    {
        var soundEffectInstance = soundEffect.CreateInstance();
        soundEffectInstance.Pitch = pitch;
        soundEffectInstance.Volume = volume;
        soundEffectInstance.IsLooped = isLooped;
        soundEffectInstance.Play();
        return soundEffectInstance;
    }

    public SoundEffectInstance playSound(string path, float pitch = 0, float volume = 1, bool isLooped = false)
    {
        var soundEffect = loadSoundEffect(path);
        var soundEffectInstance = soundEffect.CreateInstance();
        soundEffectInstance.Pitch = pitch;
        soundEffectInstance.Volume = volume;
        soundEffectInstance.IsLooped = isLooped;
        soundEffectInstance.Play();
        return soundEffectInstance;
    }
}
