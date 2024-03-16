using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        if (_audioClip != null)
        {
            //_isPaused = false;
            _AudioSource.loop = false;
            _AudioSource.clip = _audioClip;
            _AudioSource.Play();
        }
    }

    internal void Stop()
    {
        //_isPaused = false;
        _AudioSource.Stop();
    }

    internal void Pause()
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
}
