using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Testing
//For Beginning of the campaign, selecting heroes
public class SelectableHero : MonoBehaviour
{

    public string name;
    public string role;
    [SerializeField] int maxHealth = 5;
    [SerializeField] int currentXP = 0;
    [SerializeField] int level = 1;
    [SerializeField] bool selected = false;
    //[SerializeField] Material material;
    [SerializeField] Material selectedMaterial;
    [SerializeField] Material unselectedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = unselectedMaterial;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        SelectHero();
    //    }
    //}

    public void SelectHero()
    {
        selected = !selected;
        if(selected)
        {
            GetComponent<Renderer>().material = selectedMaterial;
            //material = selectedMaterial;
        }else
        {
            //material = unselectedMaterial;
            GetComponent<Renderer>().material = unselectedMaterial;
        }
    }

    public bool IsSelected()
    {
        return this.selected;
    }
}
