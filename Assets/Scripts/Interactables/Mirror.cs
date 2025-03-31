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
        dialogBehaviour.BindExternalFunction("UsedPencil", UsedPencil);
        dialogBehaviour.BindExternalFunction("UsedSlippers", UsedSlippers);
        dialogBehaviour.BindExternalFunction("UsedCrystalBall", UsedCrystalBall);
        dialogBehaviour.BindExternalFunction("UsedAxe", UsedAxe);
        dialogBehaviour.BindExternalFunction("UsedMusicBox", UsedMusicBox);
    }

    void UsedPencil()
    {
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.Pencil))
        {
            Inventory.Instance.RemoveObject(Inventory.InventoryObject.Pencil);
        }
    }

    void UsedSlippers()
    {
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.Slippers))
        {
            Inventory.Instance.RemoveObject(Inventory.InventoryObject.Slippers);
        }
    }

    void UsedCrystalBall()
    {
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.CrystalBall))
        {
            Inventory.Instance.RemoveObject(Inventory.InventoryObject.CrystalBall);
        }
    }

    void UsedAxe()
    {
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.Axe))
        {
            Inventory.Instance.RemoveObject(Inventory.InventoryObject.Axe);
        }
    }

    void UsedMusicBox()
    {
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.MusicBox))
        {
            Inventory.Instance.RemoveObject(Inventory.InventoryObject.MusicBox);
        }
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
                Inventory.Instance.AddObject(Inventory.InventoryObject.Wine, "Bottle of white wine");
            }
            else if (mirrorScene == MirrorScene.Jack)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.Cheese, "Giant chunk of cheese");
            }
            else if (mirrorScene == MirrorScene.LittleMermaid)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.CrystalBall, "Crystal ball");
            }
            else if (mirrorScene == MirrorScene.RedRiding)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.Baguette, "Old baguettes");
            }
            else if (mirrorScene == MirrorScene.RobinHood)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.Broth, "Pot of broth");
            }
            else if (mirrorScene == MirrorScene.SnowWhite)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.Onion, "Onion");
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
