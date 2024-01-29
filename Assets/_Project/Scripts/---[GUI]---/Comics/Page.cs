using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    public bool IsAnimating { get; internal set; } = false;
    public float PercentageProTick = 10f;

    public GameObject ImagesParent;
    public List<Image> Images = new List<Image>();
    public List<AudioClip> SecondaryAudioClips = new List<AudioClip>();
    public List<AudioClip> MainAudioClips = new List<AudioClip>();

    [SerializeField]
    private AudioSource mainAudioSource;
    [SerializeField]
    private AudioSource secondaryAudioSource;

    private Image currentImage;
    private float currentOpacity = 0f;
    private int currentIndex;

    public void GoNextSlide()
    {
        currentIndex++;

        if (Images.Count > currentIndex)
        {
            NextSlideAudio();

            currentImage = Images[currentIndex]; 
            IsAnimating = true;
            currentImage.color = new Color(255, 255, 255, 0); 
            currentOpacity = 0;
        }
        else
        {
            GoNextPage();
        }
    }

    private void GoNextPage()
    {
        ImagesParent.SetActive(false);
        GetComponentInParent<IComicsUI>().GoNextPage();
    }
    public void OpenPage()
    {
        currentIndex = 0;
        currentOpacity = 0;

        ImagesParent.SetActive(true);

        NextSlideAudio();

        //Image
        foreach (var image in Images)
        {
            image.color = new Color(255, 255, 255, 0);
        }
        IsAnimating = true;
        currentImage = Images.First();
        currentImage.color = new Color(255,255,255,0);
    }

    private void Update()
    {
        if (IsAnimating)
        {
            currentOpacity += PercentageProTick;
            if (currentOpacity < 0.99)
            {
                currentImage.color = new Color(255, 255, 255, currentOpacity);
            }
            else
            {
                IsAnimating = false;
            }
        }
    }

    private void NextSlideAudio()
    {
        //Audio
        var prevAudio = currentIndex > 0 ? MainAudioClips[currentIndex - 1] : null;
        if (MainAudioClips[currentIndex] != null && MainAudioClips[currentIndex] != prevAudio)
        {
            mainAudioSource.clip = MainAudioClips[currentIndex];
            mainAudioSource.Play();
        }

        prevAudio = currentIndex > 0 ? SecondaryAudioClips[currentIndex - 1] : null;
        if (SecondaryAudioClips[currentIndex] != null && SecondaryAudioClips[currentIndex] != prevAudio)
        {
            secondaryAudioSource.clip = SecondaryAudioClips[currentIndex];
            secondaryAudioSource.Play();
        }
    }

    internal void Deactivate()
    {
        mainAudioSource.Stop();
        secondaryAudioSource.Stop();
        this.gameObject.SetActive(false);
    }
}
