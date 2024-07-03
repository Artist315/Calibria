using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private List<GameObject> optionalObjectsToHide = new List<GameObject>();

    private static bool _canPause = true;
    private bool _isPasued = false;
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
        EventsManager.OnGamePaused?.Invoke();
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
        _pauseMenuVideo.SetActive(true);
        _pauseMenuVideo.GetComponent<VideoPlayer>().Play();

        objectsToShow.Clear();
        objectsToHide.ForEach(x => x.SetActive(false));
        optionalObjectsToHide.ForEach(x =>
        {
            if (x.activeSelf)
            {
                objectsToShow.Add(x);
                x.SetActive(false);
            }
        });
        _isPasued = true;
    }

    public void ClosePausePanel()
    {
        if (!_isPasued)
        {
            return;
        }
        if (!objectsToShow.Any())
        {
            Debug.Log("N o obj");
        }
        EventsManager.OnGameContinued?.Invoke();
        CursorScript.HideCoursor();
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        _pauseMenuVideo.GetComponent<VideoPlayer>().Stop();
        _pauseMenuVideo.SetActive(false);

        objectsToHide.ForEach(x => x.SetActive(true));
        objectsToShow.ForEach(x => x.SetActive(true));
        objectsToShow.Clear();
        _isPasued = false;
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

    public static void SetCanPause(bool flag)
    {
        _canPause = flag;
    }
}
