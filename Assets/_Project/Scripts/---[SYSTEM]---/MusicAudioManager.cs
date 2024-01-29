using System.Collections;
using UnityEngine;

public class MusicAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _firstTrack, _secondTrack;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        _audioSource.clip = _firstTrack;
        _audioSource.Play();
        
        yield return new WaitUntil(() => !_audioSource.isPlaying);
    
        _audioSource.clip = _secondTrack;
        _audioSource.Play();
        
        yield return new WaitUntil(() => !_audioSource.isPlaying);

        StartCoroutine(PlayMusic());
    }
}
