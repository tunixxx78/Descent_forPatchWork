using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceGenerator : MonoBehaviour
{
    public Button btn;
    public int diceSize;
    public TMP_Text diceResultText;
    private void Start()
    {
        btn = btn.GetComponent<Button>();
        btn.onClick.AddListener(ThrowDice);
    }
    public void ThrowDice()
    {
        diceResultText.text = Random.Range(1, diceSize++).ToString();
    }
        
}
