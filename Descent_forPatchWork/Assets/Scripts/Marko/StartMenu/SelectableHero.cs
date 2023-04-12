using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Testing
//For Beginning of the campaign, selecting heroes
public class SelectableHero : MonoBehaviour
{

    public string heroName;
    public string heroRole;

    public int currentHealth;
    public int maxHealth;

    public int currentActionPoints;
    public int maxActionPoints;

    public int atk;
    public int def;
    public int fate;
    public int exp;

    public int maxCombatItems;
    public int maxItems;

    [SerializeField] bool selected = false;
    //[SerializeField] Material material;
    [SerializeField] Material selectedMaterial;
    [SerializeField] Material unselectedMaterial;
    //public List<CombatItem> combatItems;
    public List<string> combatItems  = new List<string>();
    public List<JourneyItem> journeyItems;
    public List<HiddenItem> hiddenItems;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = unselectedMaterial;
    }

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
