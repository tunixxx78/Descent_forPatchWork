using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] VideoClip[] tutorialClips;

    public void SetTutorialClip(int indexNumber)
    {
        GetComponent<VideoPlayer>().clip = tutorialClips[indexNumber - 1];
    }
}
