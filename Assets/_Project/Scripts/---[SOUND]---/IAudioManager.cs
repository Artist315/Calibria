using UnityEngine;

public interface IAudioManager
{
    bool IsPlaying { get; }

    void PlayAudio(AudioClip audioClip);
    void PlayPredefinedAudio();
    public void PlayPredefinedAudioSeriesCycled(); 
    public void Pause();
}