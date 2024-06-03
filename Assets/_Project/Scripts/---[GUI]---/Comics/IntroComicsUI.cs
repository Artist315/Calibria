using System.Collections.Generic;
using UnityEngine;

public class IntroComicsUI : MonoBehaviour, IComicsUI
{
    public List<Page> Pages = new List<Page>();
    
    [SerializeField] private PausePanelFunctions _pausePanel;
    
    private int currentPageNumber = 0;
    private Page currentPage;

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
            PlayerPrefs.SetInt(PlayerPrefsConstants.IntroComicsEnded, 1);
            Time.timeScale = 1;
            
            _pausePanel.SetCanPause(true);
            this.gameObject.SetActive(false);
            EventsManager.OnGameStarted?.Invoke();
        }
    }
    
    void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.IntroComicsEnded, 0) == 1)
        {
            EventsManager.OnGameStarted?.Invoke();
            gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;

            _pausePanel.SetCanPause(false);

            currentPage = Pages[currentPageNumber];
            currentPage.gameObject.SetActive(true);
            currentPage.OpenPage();
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentPage == null)
        {
            currentPage = Pages[currentPageNumber];
            currentPage.gameObject.SetActive(true);
            currentPage.OpenPage();
        }
        if (Input.GetMouseButtonDown(0) && !currentPage.IsAnimating)
        {
            currentPage.GoNextSlide();
        }
    }
}
