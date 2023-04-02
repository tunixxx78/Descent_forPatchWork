using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<GameObject> heroesInGame;
    public List<GameObject> enemysInGame;
    public float enemyHordHealth, enemyHordStrenght;

    public bool plrCanAttack, enemyCanAttack;
    public float attackForce = 1;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        if(GameManager.gm == null)
        {
            GameManager.gm = this;
        }
        else
        {
            if(GameManager.gm != this)
            {
                Destroy(GameManager.gm.gameObject);
                GameManager.gm = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);

        plrCanAttack = false;
        enemyCanAttack = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
