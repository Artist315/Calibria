using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer.loopPointReached += EndReached;
    }


    private void EndReached(VideoPlayer source)
    {
        LoadLevelAsync(SceneIndexConstants.MainMenu);
    }

    private void LoadLevelAsync(int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);
    }
}
