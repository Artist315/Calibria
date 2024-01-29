using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{

    public delegate void GameStateEvent();
    private void Awake()
    {
        //OnGameStarted   = new UnityEvent();
        //OnGamePaused    = new UnityEvent();
        //OnGameContinued = new UnityEvent();
    }
    //public static UnityEvent OnGameStarted;
    //public static UnityEvent OnGamePaused;
    //public static UnityEvent OnGameContinued;

    public static GameStateEvent OnGameStarted;
    public static GameStateEvent OnGamePaused;
    public static GameStateEvent OnGameContinued;
}
