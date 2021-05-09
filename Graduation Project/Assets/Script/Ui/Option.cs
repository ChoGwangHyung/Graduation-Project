﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    [SerializeField] private Text figure = null;

    [SerializeField] private Slider soundSlider = null;

    void Start()
    {
        soundSlider.value = DataController.instance.gameDataInstance.soundVol;
        soundSlider.onValueChanged.AddListener(OnSoundVolumeChange);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        figure.text = string.Format("{0:N1}", soundSlider.value * 100) + "%";
    }

    public void OnSoundVolumeChange(float value)
    {
        AudioController.instance.SetVolume(value);
    }

    public void GetBack()
    {
        gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        DataController.instance.SaveData();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Begin Stage 1");
    }
}
