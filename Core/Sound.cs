using Microsoft.Xna.Framework.Audio;

class Sound
{
    public float pitch = 0;
    public float volume = 1;
    public bool isLooped = false;
    private SoundEffectInstance _soundEffectInstance;

    private SoundEffect _soundEffect;

    public Sound(SoundEffect soundEffect)
    {
        _soundEffect = soundEffect;
        _soundEffectInstance = _soundEffect.CreateInstance();
    }

    private void updateInstance()
    {
        _soundEffectInstance.Pitch = pitch;
        _soundEffectInstance.Volume = volume;
        _soundEffectInstance.IsLooped = isLooped;
    }

    public void Play()
    {
        updateInstance();
        _soundEffectInstance.Stop();
        _soundEffectInstance.Play();
    }

    public void Stop()
    {
        updateInstance();
        _soundEffectInstance.Stop();
    }

    public void Pause()
    {
        updateInstance();
        _soundEffectInstance.Pause();
    }

    public void Resume()
    {
        updateInstance();
        _soundEffectInstance.Resume();
    }
}