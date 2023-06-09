using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager mG;
    public Slider master, music, sfx;
    public Toggle mute;
    [SerializeField] TMP_Text masterValue, musicValue, sfxValue;
    private void Awake()
    {

        if (MenuManager.mG == null)
        {
            MenuManager.mG = this;
        }
        else
        {
            if (MenuManager.mG != this)
            {
                Destroy(this);
            }
        }

        DontDestroyOnLoad(this.gameObject);


        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
            master.value = PlayerPrefs.GetFloat("MasterVolume");
            masterValue.text = Math.Round(master.value, 2).ToString();
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            //SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            music.value = PlayerPrefs.GetFloat("MusicVolume");
            MusicHolder.mH.SetWantedVolume(music.value);
            musicValue.text = Math.Round(music.value, 2).ToString();

        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
            sfx.value = PlayerPrefs.GetFloat("SFXVolume");
            sfxValue.text = Math.Round(sfx.value, 2).ToString();

        }
        if (PlayerPrefs.HasKey("Muted"))
        {
            if(PlayerPrefs.GetInt("Muted") == 1)
            {
                mute.isOn = true;
            }
            else
            {
                mute.isOn = false;
            }
        }
    
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.transform.GetChild(0).gameObject.SetActive(!this.transform.GetChild(0).gameObject.activeInHierarchy);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        masterValue.text = Math.Round(volume, 2).ToString();
        mute.isOn = false;
    }
    public void SetMusicVolume(float volume)
    {
        MusicHolder.mH.SetVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        musicValue.text = Math.Round(volume, 2).ToString();
    }
    public void SetSFXVolume(float volume)
    {
        SFXHolder.sH.SetVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        sfxValue.text = Math.Round(volume, 2).ToString();
    }
    public void Mute(bool isMuted)
    {
        if(isMuted == true)
        {
            PlayerPrefs.SetInt("Muted", 1);
            AudioListener.volume = 0;
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            AudioListener.volume = master.value;
        }
    }
    public void OpenMainMenu()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
