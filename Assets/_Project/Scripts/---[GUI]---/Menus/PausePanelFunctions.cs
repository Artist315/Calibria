using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PausePanelFunctions : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _pauseMenuVideo;

    private bool _canPause = true;

    private void Update()
    {
        if (_canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenPausePanel();
            }
        }
    }
    
    public void OpenPausePanel()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
        _pauseMenuVideo.SetActive(true);
        _pauseMenuVideo.GetComponent<VideoPlayer>().Play();
    }

    public void ClosePausePanel()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        _pauseMenuVideo.GetComponent<VideoPlayer>().Stop();
        _pauseMenuVideo.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneIndexConstants.MainMenu);
    }

    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        _settingsPanel.SetActive(false);
    }

    public void SetCanPause(bool flag)
    {
        _canPause = flag;
    }
}
