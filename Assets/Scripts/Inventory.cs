using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum InventoryObject
    {
        Recipe,
    }

    public static Inventory Instance { get; private set; }

    List<Interactable> interactables;
    List<InventoryObject> inventoryObjects;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        interactables = new List<Interactable>();
        inventoryObjects = new List<InventoryObject>();
    }

    public void AddObject(InventoryObject invObj, Interactable interactable)
    {
        interactables.Add(interactable);
        inventoryObjects.Add(invObj);
    }

    public bool HasObject(InventoryObject invObj)
    {
        return inventoryObjects.Contains(invObj);
    }

    public string GetListOfObjects()
    {
        string objectListString = "";
        foreach (Interactable item in interactables)
        {
            objectListString += item.GetName() + "\n";
        }

        return objectListString;
    }
}
