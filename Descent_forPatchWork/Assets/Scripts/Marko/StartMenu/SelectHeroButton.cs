using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectHeroButton : MonoBehaviour
{

    [SerializeField] SelectableHero hero;
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;
    [SerializeField] bool isSelected;
    //[SerializeField]
    TMPro.TMP_Text heroRoleText;
     Image image;

    private void Awake()
    {
        heroRoleText = GetComponentInChildren<TMPro.TMP_Text>();
        image = GetComponent<Image>();
        
    }
    void Start()
    {
        image.color = unselectedColor;
        heroRoleText.text = hero.heroRole;
    }

    public void SelectHeroPressed()
    {
        
        isSelected = !isSelected;
        hero.changeSelectedStatus(isSelected); //update hero status with same status as in this button
        if (isSelected)
        {
            image.color = selectedColor;
        }
        else
        {
             image.color = unselectedColor;
        }
    }

    public void UnselectHeroButton()
    {
        if (isSelected)
        {
            SelectHeroPressed();
        }
    }
}
