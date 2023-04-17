using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FaithCard")]
public class FaithCard : ScriptableObject
{
    private int faithValue;
    private bool multiply;
    // Start is called before the first frame update

    public void SetFaithValue(int x)
    {
        faithValue = x;
    }

    public int GetFaithValue()
    {
        return faithValue;
    }
}
