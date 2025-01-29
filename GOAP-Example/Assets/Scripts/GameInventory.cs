using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInventory 
{
    List<GameObject> Items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        Items.Add(item);
    }

    public void RemoveItem(GameObject item)
    {
        Items.Remove(item);
    }
    
    public GameObject FindItemWithTag(string tagName)
    {
        foreach (GameObject item in Items)
        {
            if (item.CompareTag(tagName))
            {
                return item;
            }
        }
        return null;
    }
}
