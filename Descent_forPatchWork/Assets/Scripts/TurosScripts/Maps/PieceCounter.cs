using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCounter : MonoBehaviour
{
    [SerializeField] int pieceCount;

    private void Awake()
    {
        pieceCount = this.gameObject.transform.childCount;
    }

    public int GetChildCount()
    {
        return pieceCount;
    }
}
