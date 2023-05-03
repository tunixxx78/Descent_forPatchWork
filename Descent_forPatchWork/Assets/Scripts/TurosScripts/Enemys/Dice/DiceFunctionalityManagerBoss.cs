using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceFunctionalityManagerBoss : MonoBehaviour
{
    [SerializeField] Sprite[] holder1;
    [SerializeField] Sprite[] holder2;
    [SerializeField] Sprite[] holder3;
    [SerializeField] Sprite[] holder4;
    [SerializeField] Image diceEyeImages;

    public void SetDiceEyeCommandBoss(int index)
    {
        if(GameManager.gm.round == 0)
        {
            diceEyeImages.sprite = holder1[index - 1];
        }
        if (GameManager.gm.round == 1)
        {
            diceEyeImages.sprite = holder2[index - 1];
        }
        if (GameManager.gm.round == 2)
        {
            diceEyeImages.sprite = holder3[index - 1];
        }
        if (GameManager.gm.round == 3)
        {
            diceEyeImages.sprite = holder4[index - 1];
        }

    }

    public void SetDiceEyeFunctionalitiesBoss(int index)
    {
        Debug.Log("NOPAN LUVUKSI ON SAATU: " + index);

        if(GameManager.gm.round == 0 || GameManager.gm.round == 1)
        {
            if(index == 2)
            {
                for(int i = 0; i < GameManager.gm.enemysInGame.Count; i++)
                {
                    GameManager.gm.enemysInGame[i].GetComponent<EnemyOne>().eB.enemyHealth--;
                    GameManager.gm.enemyHordHealth--;
                }
            }
            if (index == 4)
            {
                for (int i = 0; i < GameManager.gm.enemysInGame.Count; i++)
                {
                    GameManager.gm.enemysInGame[i].GetComponent<EnemyOne>().eB.enemyHealth++;
                    GameManager.gm.enemyHordHealth++;
                }
            }
            if (index == 6)
            {
                for (int i = 0; i < GameManager.gm.enemysInGame.Count; i++)
                {
                    GameManager.gm.enemysInGame[i].GetComponent<EnemyOne>().eB.enemyStrength++;
                    
                }
            }
        }
    }
}
