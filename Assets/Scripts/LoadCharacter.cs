using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{

    [SerializeField] private GameObject Wizard;
    [SerializeField] private GameObject Paladin;

    void Start()
    {
        string chosenChar = PlayerPrefs.GetString("chosenCharacter");
        if (chosenChar.Equals("Paladin"))
        {
            Paladin.SetActive(true);
            Wizard.SetActive(false);
        }
        else if (chosenChar.Equals("Wizard"))
        {
            Paladin.SetActive(false);
            Wizard.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
