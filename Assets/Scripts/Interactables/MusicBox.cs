using UnityEngine;

public class MusicBox : Interactable
{
    void Awake()
    {
        objectName = "Music box";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        string[] thoughts = new string[] {
            "Steve uses this music box to stay awake, but it also has a sleep setting.",
        };
        Think(thoughts);
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.MusicBox, this);
            return true;
        }
        return false;
    }
}
