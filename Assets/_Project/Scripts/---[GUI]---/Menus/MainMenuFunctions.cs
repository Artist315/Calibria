using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _authorsPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private Slider _loadingSlider;
    
    private void Update()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.GameStarted, 0) == 1 && !_continueButton.activeSelf)
        {
            _continueButton.SetActive(true);
        }
    }
    
    public void ContinueGame()
    {
        _mainMenuPanel.SetActive(false);
        _loadingScreen.SetActive(true);
        
        StartCoroutine(LoadLevelAsync(SceneIndexConstants.MainLevel));
        Time.timeScale = 1f;
    }

    public void StartNewGame()
    {
        _mainMenuPanel.SetActive(false);
        _loadingScreen.SetActive(true);
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(PlayerPrefsConstants.GameStarted, 1);

        StartCoroutine(LoadLevelAsync(SceneIndexConstants.MainLevel));
        Time.timeScale = 1f;
    }
    
    public void OpenSettingsPanel(GameObject panelToClose)
    {
        _settingsPanel.SetActive(true);
        panelToClose.SetActive(false);
    }
    
    public void OpenAuthorsPanel(GameObject panelToClose)
    {
        _authorsPanel.SetActive(true);
        panelToClose.SetActive(false);
    }
    
    public void OpenMainMenuPanel(GameObject panelToClose)
    {
        _mainMenuPanel.SetActive(true);
        panelToClose.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
    
    private IEnumerator LoadLevelAsync(int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            _loadingSlider.value = progressValue;
            yield return null;
        }
    }

    private void PlaySound(AudioClip clip)
    {
    }
}
