using UnityEngine;

public class Pencil : Interactable
{
    void Awake()
    {
        objectName = "Pencil";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        string[] thoughts = new string[] {
            "A magical pencil?",
            "Nope, just a regular pencil."
        };
        Think(thoughts);
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.Pencil, this);
            return true;
        }
        return false;
    }
}
