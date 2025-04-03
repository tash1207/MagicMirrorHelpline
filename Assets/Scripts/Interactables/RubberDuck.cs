using UnityEngine;

public class RubberDuck : Interactable
{
    void Awake()
    {
        objectName = "Rubber duck";
        canPickUp = true;
        dontDestroyOnPickup = true;
    }

    override public void Interact()
    {
        base.Interact();

        string thought = "My lucky rubber ducky!";
        if (Inventory.Instance.inventoryEnabled)
        {
            string[] thoughts = new string[] {
                thought,
                "You're coming with me, bud."
            };
            Think(thoughts);
        }
        else
        {
            string[] thoughts = new string[] {
                thought,
                "I was wondering where you went."
            };
            Think(thoughts);
        }
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.RubberDuck, this);
            return true;
        }
        return false;
    }
}
