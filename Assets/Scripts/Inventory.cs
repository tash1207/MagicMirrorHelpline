using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public enum InventoryObject
    {
        Recipe,
        Slippers,
        True,
        Wine,
        Axe,
        Pencil,
        MusicBox,
        CrystalBall,
        Onion,
        Baguette,
        Cheese,
        Spices,
        Broth,
        SnowWhiteReward,
        RedRidingHoodReward,
        RubberDuck,
    }

    public static Inventory Instance { get; private set; }

    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] TMP_Text inventoryText;

    List<string> objectNames;
    List<InventoryObject> inventoryObjects;

    public bool inventoryEnabled = false;
    public bool inventoryTipShown = false;

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

    public void RemoveObject(InventoryObject invObj)
    {
        int index = inventoryObjects.IndexOf(invObj);
        objectNames.RemoveAt(index);
        inventoryObjects.RemoveAt(index);
    }

    public bool HasObject(InventoryObject invObj)
    {
        return invObj == InventoryObject.True || inventoryObjects.Contains(invObj);
    }

    public void EnableInventory()
    {
        inventoryEnabled = true;
    }

    public void ShowInventoryTip()
    {
        if (!inventoryTipShown)
        {
            bool isKeyboardMouse = true;
            if (gameObject.TryGetComponent<PlayerInput>(out PlayerInput playerInput))
            {
                isKeyboardMouse = playerInput.currentControlScheme == "Keyboard&Mouse";
            }

            string firstTip = "I should see if there's anything useful lying around the office.";
            string secondTip = isKeyboardMouse ?
                "Press R to view inventory." :
                "Press the north button on the controller to view inventory.";

            string[] tips = new string[] {
                firstTip,
                secondTip
            };
            InternalDialogManager.Instance.ShowDialog(tips);
            inventoryTipShown = true;
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryEnabled) { return; }

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

    public bool HasASecondaryIngredient()
    {
        return HasObject(InventoryObject.Cheese) ||
                HasObject(InventoryObject.Baguette) ||
                HasObject(InventoryObject.Onion);
    }

    public bool HasAllIngredientsExceptLast()
    {
        return HasObject(InventoryObject.Wine) &&
                HasObject(InventoryObject.Cheese) &&
                HasObject(InventoryObject.Baguette) &&
                HasObject(InventoryObject.Broth) &&
                HasObject(InventoryObject.Onion);
    }

    public bool HasAllIngredients()
    {
        return HasAllIngredientsExceptLast() && HasObject(InventoryObject.Spices);
    }
}
