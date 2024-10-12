using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

class SoundComponent : Component
{
    public float pitch = 0;
    public float volume = 1;
    public bool isLooped = false;
    public bool autoPlay = false;
    public SoundEffectInstance soundEffectInstance;
    public required SoundEffect soundEffect;

    public override void Initialize()
    {
        soundEffectInstance = soundEffect.CreateInstance();

        if (autoPlay)
        {
            soundEffectInstance.Play();
        }
    }

    public override void Update(GameTime gameTime)
    {
        soundEffectInstance.Pitch = pitch;
        soundEffectInstance.Volume = volume;
        soundEffectInstance.IsLooped = isLooped;
    }

    public void Play()
    {
        soundEffectInstance.Stop();
        soundEffectInstance.Play();
    }
}

