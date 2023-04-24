using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager mG;
    public Slider master, music, sfx;
    public Toggle mute;
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
                Destroy(MenuManager.mG.gameObject);
                MenuManager.mG = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);


        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
            master.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            music.value = PlayerPrefs.GetFloat("MusicVolume");  
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
            sfx.value = PlayerPrefs.GetFloat("SFXVolume");
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
            Debug.Log("ALO");
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
    }
    public void SetMusicVolume(float volume)
    {
        MusicHolder.mH.SetVolume(volume);
    }
    public void SetSFXVolume(float volume)
    {
        SFXHolder.sH.SetVolume(volume);
    }
    public void Mute(bool mute)
    {
        if(mute == true)
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
