using UnityEngine;

public class Chair : Interactable
{
    bool hadFirstInteraction = false;

    override public void Interact()
    {
        base.Interact();

        if (hadFirstInteraction)
        {
            Think("It's a chair.");
        }
        else
        {
            string[] thoughts = new string[] {
                "It's a sink.",
                "Wait, no, it's a chair. That's what I meant."
            };
            Think(thoughts);
            hadFirstInteraction = true;
        }
    }
}
