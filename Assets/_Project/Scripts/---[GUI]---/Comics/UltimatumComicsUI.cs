using System.Collections.Generic;
using UnityEngine;

public class UltimatumComicsUI : MonoBehaviour, IComicsUI
{
    public List<Page> Pages = new List<Page>();

    [SerializeField] private PausePanelFunctions _pausePanel;

    private int currentPageNumber = 0;
    private Page currentPage;
    private bool active = false;

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
            //PlayerPrefs.SetInt(PlayerPrefsConstants.NewGame, 1);
            this.gameObject.SetActive(false);

            EventsManager.OnGameContinued?.Invoke();
            _pausePanel.SetCanPause(true);
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        EventsManager.OnGamePaused?.Invoke();

        _pausePanel.SetCanPause(false);
        
        Time.timeScale = 0;
        active = true;
        currentPage = Pages[currentPageNumber];
        currentPage.gameObject.SetActive(true);
        currentPage.OpenPage();
    }

    void Update()
    {
        if (active)
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
}
