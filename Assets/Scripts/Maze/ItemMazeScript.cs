using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMazeScript : MonoBehaviour
{
    [SerializeField] private GameObject itemSpawn;
    public void generateItem(Vector3 pos)
    {
        GameObject item = ItemFactory.createItem(itemSpawn.transform);
        Vector3 offset = new Vector3(itemSpawn.transform.position.x, itemSpawn.transform.position.y + 0.5f, itemSpawn.transform.position.z);
        item.transform.position = pos + offset;
    }
}
