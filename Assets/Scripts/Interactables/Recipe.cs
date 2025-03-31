using UnityEngine;

public class Recipe : Interactable
{
    override public void Interact()
    {
        base.Interact();
        string[] thoughts = new string[] {
            "Tom's famous french onion soup recipe:",
            "- Homemade broth from the countryside\n- The fruit of the onion",
            "- White wine fit for a royal ball\n- Spices from the bay",
            "- Dried forest bread\n- Enough cheese to satisfy a giant",
        };
        Think(thoughts);
    }
}
