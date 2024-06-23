using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private List<GameObject> objectsToActive = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var objectToActive in objectsToActive)
        {
            objectToActive.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    private void EndReached(VideoPlayer source)
    {
        foreach (var objectToActive in objectsToActive)
        {
            objectToActive.SetActive(true);
            videoPlayer.gameObject.SetActive(false);
        }
    }

    //private IEnumerator IntroVideo()
    //{
    //    yield return new WaitUntil(() => videoClip.is.Pickup != null);
    //    yield return new WaitUntil(() => _whiskeyTutorialClient.Pickup.PickupName == PickupsEnum.Whiskey);
    //    _whiskeyClientArrow.SetTrigger(_animIDClose);
    //}
}
