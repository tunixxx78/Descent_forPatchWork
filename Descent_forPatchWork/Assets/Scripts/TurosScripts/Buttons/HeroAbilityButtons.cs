using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroAbilityButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;
    [SerializeField] int btnId, tempValue = 0;
    [SerializeField] bool isClicked = false;
    [SerializeField] Sprite[] icons;
    
    private void Awake()
    {
        if (buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.gameObject.CompareTag("AbilityButton"))
        {

            if (btnId == 0 && this.isClicked == false)
            {
                this.isClicked = true;
                SFXHolder.sH.button.Play();

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 5;
                        GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
            else if (btnId == 0 && this.isClicked == true)
            {
                this.isClicked = false;
                SFXHolder.sH.button.Play();

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 1;
                        GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }

            if (btnId == 1 && this.isClicked == false)
            {
                this.isClicked = true;
                SFXHolder.sH.button.Play();

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 3;
                        GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
            else if (btnId == 1 && this.isClicked == true)
            {
                this.isClicked = false;
                SFXHolder.sH.button.Play();

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 1;
                        GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }

            if (btnId == 2 && this.isClicked == false)
            {
                this.isClicked = true;
                SFXHolder.sH.button.Play();

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 2;
                        GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        //this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
            else if (btnId == 2 && this.isClicked == true)
            {
                this.isClicked = false;
                SFXHolder.sH.button.Play();

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 1;
                        GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        //this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }

            if (btnId == 3)
            {

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        if (tempValue == 0 || tempValue == 1 || tempValue == 2)
                        {
                            SFXHolder.sH.button.Play();
                            GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength++;
                            GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        }
                        if (tempValue == 3)
                        {
                            SFXHolder.sH.button.Play();
                            GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 50;
                            GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        }
                        if (tempValue == 4)
                        {
                            SFXHolder.sH.button.Play();
                            GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 1;
                            GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        }

                        tempValue++;

                        if(tempValue >= 5)
                        {
                            tempValue = 0;
                        }
                    }
                }

            }

            if(btnId == 4 && this.isClicked == true)
            {
                this.isClicked = false;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[1];

            }
            else if(btnId == 4 && this.isClicked == false)
            {
                this.isClicked = true;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[0];
            }

            if (btnId == 5)
            {

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    if (GameManager.gm.activePlayer == GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex)
                    {
                        if (tempValue == 0 || tempValue == 1 || tempValue == 2 || tempValue == 3)
                        {
                            SFXHolder.sH.button.Play();
                            GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength++;
                            GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        }
                        if (tempValue == 4)
                        {
                            SFXHolder.sH.button.Play();
                            GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength = 1;
                            GameManager.gm.attackForce = GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength;
                        }

                        tempValue++;

                        if (tempValue >= 5)
                        {
                            tempValue = 0;
                        }
                    }
                }

            }

            if (btnId == 6 && this.isClicked == true)
            {
                this.isClicked = false;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[1];
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hb.thisHeroIsTakingDamageOnActivation = true;
                GetComponentInParent<StatsButton>().transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (btnId == 6 && this.isClicked == false)
            {
                this.isClicked = true;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[0];
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hb.thisHeroIsTakingDamageOnActivation = false;
                GetComponentInParent<StatsButton>().transform.GetChild(1).gameObject.SetActive(false);
            }

            if (btnId == 7 && this.isClicked == true)
            {
                this.isClicked = false;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[1];
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hb.thisHeroIsTakingDamageOnActivation2 = true;
                GetComponentInParent<StatsButton>().transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (btnId == 7 && this.isClicked == false)
            {
                this.isClicked = true;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[0];
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hb.thisHeroIsTakingDamageOnActivation2 = false;
                GetComponentInParent<StatsButton>().transform.GetChild(2).gameObject.SetActive(false);
            }

            if (btnId == 8 && this.isClicked == true)
            {
                this.isClicked = false;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[1];
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hb.thisHeroIsGettingHealthOnActivation = true;
                GetComponentInParent<StatsButton>().transform.GetChild(0).gameObject.SetActive(true);

            }
            else if (btnId == 8 && this.isClicked == false)
            {
                this.isClicked = true;
                SFXHolder.sH.button.Play();

                this.gameObject.GetComponent<Image>().sprite = this.icons[0];
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hb.thisHeroIsGettingHealthOnActivation = false;
                GetComponentInParent<StatsButton>().transform.GetChild(0).gameObject.SetActive(false);
            }

        }
    }
}
