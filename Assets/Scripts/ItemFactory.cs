using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    private static System.Random rand = new System.Random();
    public static GameObject createItem(Transform spawn)
    {
        int random = rand.Next(0,2);
        if(random == 0) return Instantiate(Resources.Load("Meat") as GameObject) as GameObject;
        else return Instantiate(Resources.Load("Potion") as GameObject) as GameObject;
    }
}
