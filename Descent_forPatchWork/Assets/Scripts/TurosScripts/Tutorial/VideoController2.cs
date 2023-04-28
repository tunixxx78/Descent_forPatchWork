using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController2 : MonoBehaviour
{
    [SerializeField] VideoClip[] missionStartClips;

    public void SetStartMissionClip(int clipIndex)
    {
        GetComponent<VideoPlayer>().clip = missionStartClips[clipIndex];
    }
}
