using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PausePanelFunctions : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _pauseMenuVideo;

    [SerializeField]
    private List<GameObject> objectsToHide = new List<GameObject>();

    private bool _canPause = true;
    private List<GameObject> objectsToShow = new();

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
        CursorScript.ShowCoursor();
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
        _pauseMenuVideo.SetActive(true);
        _pauseMenuVideo.GetComponent<VideoPlayer>().Play();

        objectsToShow.Clear();
        objectsToHide.ForEach(x =>
        {
            if (x.activeSelf)
            {
                objectsToShow.Add(x);
                x.SetActive(false);
            }
        });
    }

    public void ClosePausePanel()
    {
        CursorScript.HideCoursor();
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        _pauseMenuVideo.GetComponent<VideoPlayer>().Stop();
        _pauseMenuVideo.SetActive(false);

        objectsToShow.ForEach(x => x.SetActive(true));
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
