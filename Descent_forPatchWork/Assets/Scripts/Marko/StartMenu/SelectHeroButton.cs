using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroButton : MonoBehaviour
{

    [SerializeField] SelectableHero hero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectHeroPressed()
    {
        hero.SelectHero();
    }
}
