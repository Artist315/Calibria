using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerRandom : MonoBehaviour, IAudioManager
{
    [SerializeField]
    private List<AudioClip> _audioClips;
    private AudioSource _AudioSource;

    private bool _isPaused = false;

    public bool IsPlaying => _AudioSource.isPlaying;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    public void PlayPredefinedAudio()
    {
        var r  =new System.Random();

        var audioClip = _audioClips[r.Next(0,_audioClips.Count)];
        if (audioClip != null)
        {
            //_isPaused = false;
            _AudioSource.loop = false;
            _AudioSource.clip = audioClip;
            _AudioSource.Play();
        }
    }

    internal void Stop()
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

    public void PlayAudio(AudioClip audioClip)
    {
        throw new NotImplementedException();
    }

    public void PlayPredefinedAudioSeriesCycled()
    {
        throw new NotImplementedException();
    }
}
