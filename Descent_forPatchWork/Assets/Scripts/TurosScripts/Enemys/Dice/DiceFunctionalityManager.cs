using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceFunctionalityManager : MonoBehaviour
{
    [SerializeField] Sprite[] diceEyeCommands;
    [SerializeField] Image diceEyeImages;

    public void SetDiceEyeCommand(int index)
    {
        diceEyeImages.sprite = diceEyeCommands[index - 1];
    }

    public void SetDiceEyeFunctionalities(int index)
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
