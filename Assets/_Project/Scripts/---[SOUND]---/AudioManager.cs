using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{
    private AudioSource _AudioSource;

    [SerializeField]
    private AudioClip _audioClip;
    private bool _isPaused = false;

    public bool IsPlaying => _AudioSource.isPlaying;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        _AudioSource.clip = audioClip;
        _AudioSource.Play();
    }
    public void PlayPredefinedAudio()
    {
        if (_audioClip != null)
        {
            //_isPaused = false;
            _AudioSource.loop = false;
            _AudioSource.clip = _audioClip;
            _AudioSource.Play();
        }
    }

    public void Stop()
    {
        //_isPaused = false;
        _AudioSource.Stop();
    }

    public void Pause()
    {
        _isPaused = true;
        _AudioSource.Pause();
    }
    internal void Resume()
    {
        if (_isPaused)
        {
            _isPaused = false;
            _AudioSource.Play();

        }
    }

    public void PlayPredefinedAudioSeriesCycled()
    {
        if (_audioClip != null)
        {
            //_isPaused = false;
            _AudioSource.loop = true;
            _AudioSource.clip = _audioClip;
            _AudioSource.Play();
        }
    }
}
