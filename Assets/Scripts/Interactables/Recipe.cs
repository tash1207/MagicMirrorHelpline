using UnityEngine;

public class Recipe : Interactable
{
    override public void Interact()
    {
        base.Interact();

        string[] thoughts;
        
        if (Inventory.Instance.HasAllIngredients())
        {
            thoughts = new string[] {
                "Tom's famous french onion soup recipe...",
                "Omg omg I have all the ingredients!",
                "Time to make some soup!",
            };
        }
        else if (Inventory.Instance.HasAllIngredientsExceptLast())
        {
            thoughts = new string[] {
                "Tom's famous french onion soup recipe...",
                "Looks like I have everything except the spices!",
            };
            performFollowUpAction = !MirrorManager.Instance.HasEnabledLastMirror();
        }
        else
        {
            thoughts = new string[] {
                "Tom's famous french onion soup recipe:",
                "- Homemade broth from the countryside\n- The fruit of the onion",
                "- White wine fit for a royal ball\n- Spices from the bay",
                "- Dried forest bread\n- Enough cheese to satisfy a giant",
            };
        }
        
        Think(thoughts);
    }

    override public void FollowUpAction()
    {
        if (performFollowUpAction)
        {
            if (Inventory.Instance.HasAllIngredients())
            {
                // Potential game over behavior
                // Think("I win!");
            }
            else
            {
                MirrorManager.Instance.EnableLastMirror();
                Think("Oh? Another call?");
            }
        }
    }
}
