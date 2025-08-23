using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController inventoryInstance;
    private int potionQuantity = 2;
    private int meatQuantity = 1;
    private string chosenItem = "Meat";

    void Awake()
    {
        if (inventoryInstance == null)
        {
            inventoryInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setInventory(int p, int m)
    {
        potionQuantity = p;
        meatQuantity = m;
    }

    public void switchItem()
    {
        chosenItem = (chosenItem == "Potion") ? "Meat" : "Potion"; 
    }

    public void addMeat()
    {
        meatQuantity++;
    }

    public void addPotion()
    {
        potionQuantity++;
    }

    public void useMeat()
    {
        meatQuantity--;
    }

    public void usePotion()
    {
        potionQuantity--;
    }

    public int getMeatQuantity()
    {
        return meatQuantity;
    }

    public int getPotionQuantity()
    {
        return potionQuantity;
    }

    public string getChosenItem()
    {
        return chosenItem;
    }

}
