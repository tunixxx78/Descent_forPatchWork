using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInformation : MonoBehaviour
{
    public Sprite[] blockImages;
    MapsController mapsController;

    private void Awake()
    {
        mapsController = FindObjectOfType<MapsController>();
    }

    public void setAreaImageInfo()
    {
        for (int i = 0; i < blockImages.Length; i++)
        {
            mapsController.mapPiecesArea.Add(blockImages[i]);
        }
    }

    public void SetBattleImageInfo()
    {
        for (int i = 0; i < blockImages.Length; i++)
        {
            mapsController.mapPiecesBattle.Add(blockImages[i]);
        }
    }

}

