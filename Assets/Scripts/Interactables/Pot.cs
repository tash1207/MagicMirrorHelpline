using System.Collections;
using UnityEngine;

public class Pot : Interactable
{
    Animator animator;

    int animationTest = 0;
    bool disableInteraction = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    override public void Interact()
    {
        if (disableInteraction) { return; }

        base.Interact();
        StartCoroutine(EnableFirstMirror());

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
            Think("I could use this, if I had any ingredients.");
        }
    }

    IEnumerator EnableFirstMirror()
    {
        disableInteraction = true;
        yield return new WaitForSeconds(2.5f);
        MirrorManager.Instance.EnableMirror1();
        Think("Crap, I guess duty calls. No need to make others suffer just because I'm hungry.");
        disableInteraction = false;
    }
}
