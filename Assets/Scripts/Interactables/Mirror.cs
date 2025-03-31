using System;
using cherrydev;
using UnityEngine;

public class Mirror : Interactable
{
    public enum MirrorScene
    {
        Cinderella,
        Jack,
        LittleMermaid,
        RedRiding,
        RobinHood,
        SnowWhite,
    }

    public Action<MirrorScene> OnSceneStarted;
    public Action<MirrorScene> OnSceneFinished;

    [Header("UI")]
    [SerializeField] SpriteRenderer bgSpriteRenderer;
    [SerializeField] Sprite bgActiveImage;
    [SerializeField] Sprite bgDefaultImage;
    [SerializeField] ParticleSystem shimmer;

    [Header("Dialog")]
    [SerializeField] MirrorScene mirrorScene;
    [SerializeField] DialogBehaviour dialogBehaviour;
    [SerializeField] DialogNodeGraph startingDialogNodeGraph;
    [SerializeField] DialogNodeGraph giveItemDialogNodeGraph;
    [SerializeField] DialogNodeGraph finalDialogNodeGraph;
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
            OnSceneFinished?.Invoke(mirrorScene);

            if (mirrorScene == MirrorScene.Cinderella)
            {
                if (Inventory.Instance.HasObject(Inventory.InventoryObject.Slippers))
                {
                    // TODO: check if we used slippers or pencil
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
            OnSceneStarted?.Invoke(mirrorScene);
        }
        else if (isRinging && hasStartedDialog)
        {
            dialogBehaviour.StartDialog(giveItemDialogNodeGraph);
        }
        else if (isRinging && finishedMirrorTask && finalDialogNodeGraph != null)
        {
            dialogBehaviour.StartDialog(finalDialogNodeGraph);
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
