using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceGenerator : MonoBehaviour
{
    public Button btn;
    public int diceSize, diceResult;
    public TMP_Text diceResultText;
    public Sprite[] diceSprites;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        btn = btn.GetComponent<Button>();
        btn.onClick.AddListener(() => { StartCoroutine(AnimateRoll(10)); });
    }
    public void ThrowDice()
    {
        StartCoroutine(AnimateRoll(10));
    }

    IEnumerator AnimateRoll(int cycles)
    {
        int roll = 0;
        for(int i = 0; i <= cycles; i++)
        {
            roll = Random.Range(0, diceSize);
            image.sprite = diceSprites[roll];
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        diceResult = roll + 1;
        image.sprite = diceSprites[roll];
        yield return null;
    }
}
