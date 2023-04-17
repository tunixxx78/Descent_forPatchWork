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
     Image image;
    void Start()
    {
        image = GetComponent<Image>();
        image.color = unselectedColor;
    }


    public void SelectHeroPressed()
    {
        
        hero.SelectHero();
        isSelected = !isSelected;
        if (isSelected)
        {
            image.color = selectedColor;
        }
        else
        {
             image.color = unselectedColor;
        }
    }
}
