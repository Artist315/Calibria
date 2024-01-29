using UnityEngine;

public class MenusAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void PlaySound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
