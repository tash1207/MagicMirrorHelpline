using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum InventoryObject
    {
        Recipe,
        Slippers,
    }

    public static Inventory Instance { get; private set; }

    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] TMP_Text inventoryText;

    List<string> objectNames;
    List<InventoryObject> inventoryObjects;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        objectNames = new List<string>();
        inventoryObjects = new List<InventoryObject>();
    }

    public void AddObject(InventoryObject invObj, Interactable interactable)
    {
        objectNames.Add(interactable.GetName());
        inventoryObjects.Add(invObj);
    }

    public void AddObject(InventoryObject invObj, string objectName)
    {
        objectNames.Add(objectName);
        inventoryObjects.Add(invObj);
    }

    public bool HasObject(InventoryObject invObj)
    {
        return inventoryObjects.Contains(invObj);
    }

    public void ToggleInventory()
    {
        if (inventoryCanvas.activeSelf)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }

    public void ShowInventory()
    {
        inventoryText.text = GetListOfObjects();
        inventoryCanvas.SetActive(true);
    }

    public void HideInventory()
    {
        inventoryCanvas.SetActive(false);
    }

    string GetListOfObjects()
    {
        string objectListString = "";
        foreach (string item in objectNames)
        {
            objectListString += "- " + item + "\n";
        }

        return objectListString;
    }
}
