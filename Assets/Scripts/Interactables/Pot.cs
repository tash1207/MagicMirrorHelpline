using System.Collections;
using UnityEngine;

public class Pot : Interactable
{
    Animator animator;

    int animationTest = 0;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    override public void Interact()
    {
        base.Interact();

        if (Inventory.Instance.HasObject(Inventory.InventoryObject.Recipe))
        {
            if (animationTest == 0)
            {
                animator.SetBool("isFull", true);
                animationTest = 1;
            }
            else if (animationTest == 1)
            {
                animator.SetBool("isSteaming", true);
                animationTest = 2;
                Think("Now we're cooking!");
            }
            else if (animationTest == 2)
            {
                animator.SetBool("isFull", false);
                animator.SetBool("isSteaming", false);
                animationTest = 0;
                Think("Yummm!");
            }
        }
        else
        {
            string[] thoughts = new string[] {
                "I could use this, if I had any ingredients.",
            };
            Think(thoughts);
            performFollowUpAction = !MirrorManager.Instance.HasEnabledFirstMirror();
        }
    }

    override public void FollowUpAction()
    {
        if (performFollowUpAction)
        {
            MirrorManager.Instance.EnableMirror1();
            Think("Crap, I guess duty calls. No need to make others suffer just because I'm hungry.");
        }
    }
}
