using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JukeBoxMusic : MonoBehaviour
{

    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private List<AudioClip> _audioClipList;

    [SerializeField]
    private AudioClip _switchAudioSound;

    private ActionTrigger _actionTrigger;
    private IEnumerator<AudioClip> _audioClipEnum;
    private bool _isSwitching = false;
    private bool _isActive    = false;

    private void Awake()
    {
        EventsManager.OnGameStarted += (StartPlaying);
        EventsManager.OnGamePaused += (Pause);
        EventsManager.OnGameContinued += (Resume);

        if (_audioManager == null)
        {
            _audioManager = GetComponentInChildren<AudioManager>();
        }
        if (_audioClipList.Any())
        {
            _audioClipEnum = _audioClipList.GetEnumerator();
        }
        _actionTrigger = GetComponentInChildren<ActionTrigger>();

        _actionTrigger.SubscribedAction = PlayNextClip;

    }
    private void StartPlaying()
    {
        _isActive = true;
        PlayNextClip();
    }

    private void Update()
    {
        if (!_audioManager.IsPlaying && !_isSwitching && _isActive)
        {
            PlayNextClip();
        }

        if (_isSwitching && !_audioManager.IsPlaying)
        {
            if (_audioClipEnum.MoveNext())
            {
                _audioManager.PlayAudio(_audioClipEnum.Current);
            }
            else
            {
                _audioClipEnum.Reset();
                _audioClipEnum.MoveNext();
                _audioManager.PlayAudio(_audioClipEnum.Current);
            }
            _isSwitching = false;
        }
    }

    private void Pause()
    {
        _isActive = false;
        _audioManager.Pause();
    }
    private void Resume()
    {
        _isActive = true;
        _audioManager.Resume();
    }

    private void PlayNextClip()
    {
        if (_isSwitching == false)
        {
            _isSwitching = true;
            _audioManager.PlayAudio(_switchAudioSound);
        };

    }

    IEnumerator SwitchAudio()
    {
        _audioManager.PlayAudio(_switchAudioSound);
        while (_audioManager.IsPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        if (_audioClipEnum.MoveNext())
        {
            _audioManager.PlayAudio(_audioClipEnum.Current);
        }
        else
        {
            _audioClipEnum.Reset();
            _audioClipEnum.MoveNext();
            _audioManager.PlayAudio(_audioClipEnum.Current);
        }
        _isSwitching = false;
    }

    private void OnDestroy()
    {
        EventsManager.OnGameStarted -= (StartPlaying);
        EventsManager.OnGamePaused -= (Pause);
        EventsManager.OnGameContinued -= (Resume);
    }
}
