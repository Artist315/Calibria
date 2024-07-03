using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalComicsUI : MonoBehaviour, IComicsUI
{
    public List<Page> Pages = new List<Page>();

    [SerializeField] private PausePanelFunctions _pausePanel;
    
    private int currentPageNumber = 0;
    private Page currentPage;
    public Page BadFinalPage;
    public bool IsGoodFinal { get; set; } = true;

    public void GoNextPage()
    {
        int index = Pages.IndexOf(currentPage);
        if (Pages.Count > index + 1)
        {
            currentPage.Deactivate();
            currentPage = Pages[index + 1];
            currentPage.gameObject.SetActive(true);
            currentPage.OpenPage();
        }
        else
        {
            int fullscreenSetting = PlayerPrefs.GetInt(PlayerPrefsConstants.InFullscreen);
            int soundSetting = PlayerPrefs.GetInt(PlayerPrefsConstants.GameVolume);

            if (IsGoodFinal)
            {
                PlayerPrefs.DeleteAll();
            }

            PlayerPrefs.SetInt(PlayerPrefsConstants.InFullscreen, fullscreenSetting);
            PlayerPrefs.SetInt(PlayerPrefsConstants.GameVolume, soundSetting);

            SceneManager.LoadScene(SceneIndexConstants.MainMenu);
        }
    }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        EventsManager.OnGamePaused?.Invoke();
        PausePanelFunctions.SetCanPause(false);
        
        Time.timeScale = 0;
        if (IsGoodFinal)
        {
            currentPage = Pages[currentPageNumber];
        }
        else
        {
            currentPage = BadFinalPage;
            Pages.Clear();
            Pages.Add(currentPage);
        }
        currentPage.gameObject.SetActive(true);
        currentPage.OpenPage();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentPage == null)
        {
            currentPage = Pages[currentPageNumber];
            currentPage.OpenPage();
        }
        if (Input.GetMouseButtonDown(0) && !currentPage.IsAnimating)
        {
            currentPage.GoNextSlide();
        }
    }
}
