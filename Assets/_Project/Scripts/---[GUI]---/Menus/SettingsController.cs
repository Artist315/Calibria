using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public int FrameRateCap = 60;

    [Header("Fullscreen")]
    [SerializeField] private Toggle _fullscreenToggle;

    [Header("Audio")]
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private AudioMixer _audioMixer;

    void Start()
    {
        Application.targetFrameRate = FrameRateCap;
        QualitySettings.vSyncCount = 0;

        LoadFullscreenValues();
        LoadAudioValues();
    }
    
    private void LoadFullscreenValues()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.InFullscreen, 1) == 1)
        {
            _fullscreenToggle.isOn = true;
        }
        else
        {
            _fullscreenToggle.isOn = false;
        }

        _fullscreenToggle.onValueChanged.AddListener(x => ChangeScreenMode(x));
    }

    private void LoadAudioValues()
    {
        float soundVolumeValue = PlayerPrefs.GetFloat(PlayerPrefsConstants.GameVolume, 0.5f);

        _soundSlider.value = soundVolumeValue;

        _audioMixer.SetFloat("GameVolume", Mathf.Log10(soundVolumeValue) * 20);
    }
    
    public void ChangeSoundVolume()
    {
        float soundVolumeValue = _soundSlider.value;

        PlayerPrefs.SetFloat(PlayerPrefsConstants.GameVolume, soundVolumeValue);

        LoadAudioValues();
    }

    public void ChangeScreenMode(bool inFullScreen)
    {
        if (!inFullScreen)
        {
            Screen.SetResolution(960, 540, FullScreenMode.Windowed);
            PlayerPrefs.SetInt(PlayerPrefsConstants.InFullscreen, 0);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
            PlayerPrefs.SetInt(PlayerPrefsConstants.InFullscreen, 1);
        }
    }
}