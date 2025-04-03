using UnityEngine;

public class Desk : Interactable
{
    [SerializeField] GameObject rubberDuck;

    override public void Interact()
    {
        base.Interact();
        string thought = "It's my desk.";
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.RubberDuck))
        {
            string[] thoughts = new string[] {
                thought,
                "I can put ducky back."
            };
            Think(thoughts);
            performFollowUpAction = true;
        }
        else
        {
            Think(thought);
            performFollowUpAction = false;
        }
    }

    override public void FollowUpAction()
    {
        if (performFollowUpAction)
        {
            if (Inventory.Instance.HasObject(Inventory.InventoryObject.RubberDuck))
            {
                Inventory.Instance.RemoveObject(Inventory.InventoryObject.RubberDuck);
                Think("Welcome back home, little buddy.");
            }
            rubberDuck.SetActive(true);
        }
    }
}
