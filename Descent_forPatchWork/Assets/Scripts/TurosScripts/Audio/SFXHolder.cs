using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXHolder : MonoBehaviour
{
    public static SFXHolder sH;
    public AudioSource button, sword, arrow;

    private void Awake()
    {
        if (SFXHolder.sH == null)
        {
            SFXHolder.sH = this;
        }
        else
        {
            if (SFXHolder.sH != this)
            {
                Destroy(SFXHolder.sH.gameObject);
                SFXHolder.sH = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume(float volume)
    {
        button.volume = volume;
        sword.volume = volume;
        arrow.volume = volume;
    }
}
