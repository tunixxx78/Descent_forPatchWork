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
    public Sprite[] diceSprites;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        btn = btn.GetComponent<Button>();
        btn.onClick.AddListener(ThrowDice);
    }
    public void ThrowDice()
    {
        int roll = Random.Range(1, diceSize);
        StartCoroutine(AnimateRoll(10));
        image.sprite = diceSprites[roll];
    }

    IEnumerator AnimateRoll(int cycles)
    {
        for(int i = 0; i <= cycles; i++)
        {
            image.sprite = diceSprites[Random.Range(1, diceSize)];
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        yield return null;
    }
}
