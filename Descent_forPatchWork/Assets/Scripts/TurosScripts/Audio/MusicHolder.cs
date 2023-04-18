using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicHolder : MonoBehaviour
{
    public static MusicHolder mH;
    public AudioSource music;
    public AudioClip[] musics;
    [SerializeField] float currentVolume, startVolume, wantedVolume;

    private void Awake()
    {
        if (MusicHolder.mH == null)
        {
            MusicHolder.mH = this;
        }
        else
        {
            if (MusicHolder.mH != this)
            {
                Destroy(MusicHolder.mH.gameObject);
                MusicHolder.mH = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);

        music = GameObject.Find("Music").GetComponent<AudioSource>();
        currentVolume = 0;
    }

    private void Start()
    {
        currentVolume = music.volume;
        music.clip = musics[0];
        music.Play();

        StartCoroutine(FadeMusicOn(0));
    }

    IEnumerator FadeMusicOn(int musicIndex)
    {
        music.clip = musics[musicIndex];
        music.Play();
        startVolume = 0;
        currentVolume = 0;

        while(currentVolume < wantedVolume)
        {
            currentVolume = currentVolume + 0.5f * Time.deltaTime;
            music.volume = currentVolume;

            yield return null;
        }
    }

    IEnumerator FadeMusicOff(int musicIndex)
    {
        startVolume = music.volume;
        currentVolume = music.volume;

        while (currentVolume > 0)
        {
            currentVolume = currentVolume - 0.5f * Time.deltaTime;
            music.volume = currentVolume;

            yield return null;
        }

        music.Stop();
        music.volume = startVolume;

        StartCoroutine(FadeMusicOn(musicIndex));
    }

    public void MusicOn(int musicIndex)
    {
        StartCoroutine(FadeMusicOn(musicIndex));
    }

    public void MusicOff(int musicIndex)
    {
        StartCoroutine(FadeMusicOff(musicIndex));
    }
}
