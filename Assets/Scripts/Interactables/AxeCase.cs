using UnityEngine;

public class AxeCase : Interactable
{
    [SerializeField] GameObject axe;

    override public void Interact()
    {
        base.Interact();
        string thought = "I'm so happy that magical glass cleans itself up.";
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.SnowWhiteReward))
        {
            string[] thoughts = new string[] {
                thought,
                "I guess I can put this up there."
            };
            Think(thoughts);
            performFollowUpAction = true;
        }
        else if (Inventory.Instance.HasObject(Inventory.InventoryObject.Axe))
        {
            string[] thoughts = new string[] {
                thought,
                "I guess I can put this back."
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
            if (Inventory.Instance.HasObject(Inventory.InventoryObject.SnowWhiteReward))
            {
                axe.GetComponent<Axe>().SetHuntsman(true);
                Inventory.Instance.RemoveObject(Inventory.InventoryObject.SnowWhiteReward);
                Think("I wonder if anyone will notice the difference.");
            }
            else if (Inventory.Instance.HasObject(Inventory.InventoryObject.Axe))
            {
                axe.GetComponent<Axe>().SetHuntsman(false);
                Inventory.Instance.RemoveObject(Inventory.InventoryObject.Axe);
            }
            axe.SetActive(true);
        }
    }
}
