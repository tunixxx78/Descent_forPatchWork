using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MapButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;
    [SerializeField] Animator mapAnimator;
    [SerializeField] string animationTrigger;

    MapsController maps;

    private void Awake()
    {
        if (buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

        mapAnimator = GameObject.Find("MapPanel").GetComponent<Animator>();
        maps = FindObjectOfType<MapsController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON DOWN!");

        if (this.gameObject.CompareTag("Map"))
        {
            maps.mapsButton.SetActive(false);
            mapAnimator.SetTrigger(animationTrigger);
        }

        if (this.gameObject.CompareTag("WorldMapButton"))
        {
            mapAnimator.SetTrigger(animationTrigger);
            GetComponent<BlockInformation>().setAreaImageInfo();

            maps.SeteRealArea();
        }

        if (this.gameObject.CompareTag("AreaMapButton"))
        {
            mapAnimator.SetTrigger(animationTrigger);
            GetComponent<BlockInformation>().SetBattleImageInfo();

            maps.SetRealBattleMap();
        }


        if (this.gameObject.CompareTag("ExitMapButton"))
        {
            mapAnimator.SetTrigger(animationTrigger);
            maps.mapPiecesArea.Clear();
            maps.mapPiecesBattle.Clear();

            GameManager.gm.heroesInGame.Clear();
            GameManager.gm.enemysInGame.Clear();

            maps.mapsButton.SetActive(true);
            maps.enemyHordPanel.SetActive(false);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }
}
