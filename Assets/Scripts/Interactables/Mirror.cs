using cherrydev;
using UnityEngine;

public class Mirror : Interactable
{
    public enum MirrorScene
    {
        Cinderella,
        Jack,
    }

    [Header("UI")]
    [SerializeField] SpriteRenderer bgSpriteRenderer;
    [SerializeField] Sprite bgActiveImage;
    [SerializeField] Sprite bgDefaultImage;
    [SerializeField] ParticleSystem shimmer;

    [Header("Dialog")]
    [SerializeField] MirrorScene mirrorScene;
    [SerializeField] DialogBehaviour dialogBehaviour;
    [SerializeField] DialogNodeGraph startingDialogNodeGraph;
    [SerializeField] DialogNodeGraph returningDialogNodeGraph;
    [SerializeField] string finishedMirrorThought;

    bool isRinging = false;
    bool hasStartedDialog = false;
    bool finishedMirrorTask = false;

    void Start()
    {
        dialogBehaviour.OnEndOfMirrorScene += EndMirrorScene;
    }

    void EndMirrorScene(MirrorScene scene)
    {
        if (mirrorScene == scene)
        {
            finishedMirrorTask = true;
            SetToDefault();

            if (mirrorScene == MirrorScene.Cinderella)
            {
                if (Inventory.Instance.HasObject(Inventory.InventoryObject.Slippers))
                {
                    Inventory.Instance.RemoveObject(Inventory.InventoryObject.Slippers);
                    Inventory.Instance.AddObject(Inventory.InventoryObject.Wine, "Bottle of white wine");
                }
            }
        }
    }

    override public void Interact()
    {
        base.Interact();
        if (isRinging && !hasStartedDialog)
        {
            dialogBehaviour.StartDialog(startingDialogNodeGraph);
            hasStartedDialog = true;
        }
        else if (isRinging && hasStartedDialog)
        {
            dialogBehaviour.StartDialog(returningDialogNodeGraph);
        }
        else if (finishedMirrorTask)
        {
            Think(finishedMirrorThought);
        }
        else
        {
            Think("I'm sure someone will call the Magic Mirror helpline while I'm stuck here.");
        }
    }

    public void StartRinging()
    {
        isRinging = true;
        bgSpriteRenderer.sprite = bgActiveImage;
        shimmer.Play();
    }

    public void SetToDefault()
    {
        isRinging = false;
        bgSpriteRenderer.sprite = bgDefaultImage;
        shimmer.Stop();
    }
}
