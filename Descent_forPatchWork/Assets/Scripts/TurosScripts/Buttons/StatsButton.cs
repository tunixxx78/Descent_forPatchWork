using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StatsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;
    [SerializeField] bool isNegative = false, isHero, isKillButton;
    [SerializeField] GameObject parentObject;

    private void Awake()
    {
        if(buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

        /*
        if (isHero)
        {
            parentObject = GetComponentInParent<HeroOne>().gameObject;
        }
        else
        {
            parentObject = GetComponentInParent<EnemyOne>().gameObject;
        }
        */

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON DOWN!");

        if (isHero)
        {
            if (isNegative)
            {
                if (this.gameObject.CompareTag("Health"))
                {
                    SFXHolder.sH.button.Play();

                    //parentObject.GetComponent<HeroOne>().hb.plrHealth--;

                    for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                    {
                        if (GameManager.gm.activePlayer == i)
                        {
                            GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrHealth--;
                        }
                    }


                }
                if (this.gameObject.CompareTag("Strenght"))
                {
                    SFXHolder.sH.button.Play();

                    for (int j = 0; j < GameManager.gm.heroesInGame.Count; j++)
                    {
                        if (GameManager.gm.activePlayer == j)
                        {
                            GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrStrength--;
                            GameManager.gm.attackForce = GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrStrength;
                        }
                    }
                    //parentObject.GetComponent<HeroOne>().hb.plrStrength--;
                    //GameManager.gm.attackForce = parentObject.GetComponent<HeroOne>().hb.plrStrength;
                }
                if (this.gameObject.CompareTag("Level"))
                {
                    SFXHolder.sH.button.Play();

                    for (int k = 0; k < GameManager.gm.heroesInGame.Count; k++)
                    {
                        if(GameManager.gm.activePlayer == k)
                        {
                            GameManager.gm.heroesInGame[k].GetComponent<HeroOne>().hb.plrLevel--;
                        }
                        
                    }
                    //parentObject.GetComponent<HeroOne>().hb.plrLevel--;
                }
                if (this.gameObject.CompareTag("Action"))
                {
                    SFXHolder.sH.button.Play();

                    for (int a = 0; a < GameManager.gm.heroesInGame.Count; a++)
                    {
                        if(GameManager.gm.activePlayer == a)
                        {
                            GameManager.gm.heroesInGame[a].GetComponent<HeroOne>().hb.plrActionPoints--;
                        }
                    }
                }

            }
            else
            {
                if (this.gameObject.CompareTag("Health"))
                {

                    SFXHolder.sH.button.Play();
                    //parentObject.GetComponent<HeroOne>().hb.plrHealth++;

                    for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                    {
                        if (GameManager.gm.activePlayer == i)
                        {
                            GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrHealth++;
                        }
                    }
                }
                if (this.gameObject.CompareTag("Strenght"))
                {

                    SFXHolder.sH.button.Play();

                    for (int j = 0; j < GameManager.gm.heroesInGame.Count; j++)
                    {
                        if (GameManager.gm.activePlayer == j)
                        {
                            GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrStrength++;
                            GameManager.gm.attackForce = GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrStrength;
                        }
                    }

                    //parentObject.GetComponent<HeroOne>().hb.plrStrength++;
                    //GameManager.gm.attackForce = parentObject.GetComponent<HeroOne>().hb.plrStrength;
                }
                if (this.gameObject.CompareTag("Level"))
                {
                    SFXHolder.sH.button.Play();

                    for (int k = 0; k < GameManager.gm.heroesInGame.Count; k++)
                    {
                        if(GameManager.gm.activePlayer == k)
                        {
                            GameManager.gm.heroesInGame[k].GetComponent<HeroOne>().hb.plrLevel++;
                        }
                        
                    }
                    //parentObject.GetComponent<HeroOne>().hb.plrLevel++;
                }
                if (this.gameObject.CompareTag("Action"))
                {
                    SFXHolder.sH.button.Play();

                    for (int a = 0; a < GameManager.gm.heroesInGame.Count; a++)
                    {
                        if (GameManager.gm.activePlayer == a)
                        {
                            GameManager.gm.heroesInGame[a].GetComponent<HeroOne>().hb.plrActionPoints++;
                        }
                    }
                }
            }
        }
        else
        {
            if (isNegative)
            {
                if (this.gameObject.CompareTag("Health"))
                {
                    SFXHolder.sH.button.Play();

                    parentObject.GetComponent<EnemyOne>().eB.enemyHealth--;
                    GameManager.gm.enemyHordHealth--;
                }
                if (this.gameObject.CompareTag("Strenght"))
                {
                    SFXHolder.sH.button.Play();

                    parentObject.GetComponent<EnemyOne>().eB.enemyStrength--;
                    GameManager.gm.enemyHordStrenght--;

                }

            }
            else
            {
                if (this.gameObject.CompareTag("Health"))
                {
                    SFXHolder.sH.button.Play();

                    parentObject.GetComponent<EnemyOne>().eB.enemyHealth++;
                    GameManager.gm.enemyHordHealth++;
                }
                if (this.gameObject.CompareTag("Strenght"))
                {
                    SFXHolder.sH.button.Play();

                    parentObject.GetComponent<EnemyOne>().eB.enemyStrength++;
                    GameManager.gm.enemyHordStrenght++;
                }

            }

            if (isKillButton)
            {
                SFXHolder.sH.button.Play();

                GameManager.gm.enemyHordHealth -= GetComponentInParent<EnemyOne>().eB.enemyHealth;
                GameManager.gm.enemyHordStrenght -= GetComponentInParent<EnemyOne>().eB.enemyStrength;

                Destroy(parentObject);
            }
        }
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }
}
