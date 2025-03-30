using UnityEngine;

public class Recipe : Interactable
{
    void Awake()
    {
        objectName = "Tom's soup recipe";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        Think("Tom's famous french onion soup recipe.");
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.Recipe, this);
            return true;
        }
        return false;
    }
}
