using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicHolder : MonoBehaviour
{
    public static MusicHolder mH;
    [SerializeField] AudioSource songOne;

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
    }

    private void Start()
    {
        songOne.Play();
    }
}
