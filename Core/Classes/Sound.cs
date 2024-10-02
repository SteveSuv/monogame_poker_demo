using Microsoft.Xna.Framework.Audio;

class Sound
{
    public float pitch = 0;
    public float volume = 1;
    public bool isLooped = false;
    private readonly SoundEffectInstance _soundEffectInstance;
    private readonly SoundEffect _soundEffect;

    public Sound(SoundEffect soundEffect)
    {
        _soundEffect = soundEffect;
        _soundEffectInstance = _soundEffect.CreateInstance();
    }

    private void UpdateInstance()
    {
        _soundEffectInstance.Pitch = pitch;
        _soundEffectInstance.Volume = volume;
        _soundEffectInstance.IsLooped = isLooped;
    }

    public void Play()
    {
        UpdateInstance();
        _soundEffectInstance.Stop();
        _soundEffectInstance.Play();
    }

    public void Stop()
    {
        UpdateInstance();
        _soundEffectInstance.Stop();
    }

    public void Pause()
    {
        UpdateInstance();
        _soundEffectInstance.Pause();
    }

    public void Resume()
    {
        UpdateInstance();
        _soundEffectInstance.Resume();
    }
}