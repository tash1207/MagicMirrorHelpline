using System;
using cherrydev;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] bool thinkDefaultThought1 = true;

    [Header("UI")]
    [SerializeField] GameObject bgSpriteRenderer;
    [SerializeField] Sprite bgActiveImage;
    [SerializeField] Sprite bgDefaultImage;
    [SerializeField] ParticleSystem shimmer;

    [Header("Dialog")]
    [SerializeField] MirrorScene mirrorScene;
    [SerializeField] GameObject dialogBackground;
    [SerializeField] Image dialogBackgroundImage;
    [SerializeField] DialogBehaviour dialogBehaviour;
    [SerializeField] DialogNodeGraph startingDialogNodeGraph;
    [SerializeField] DialogNodeGraph giveItemDialogNodeGraph;
    [SerializeField] DialogNodeGraph finalDialogNodeGraph;
    [SerializeField] string finishedMirrorThought;

    bool isRinging = false;
    bool hasStartedDialog = false;
    public bool finishedMirrorTask = false;

    float activeBgSize = 0.18f;
    float defaultBgSize = 2.2f;

    void Start()
    {
        dialogBehaviour.OnEndOfMirrorScene += EndMirrorScene;
        dialogBehaviour.AddListenerToDialogFinishedEvent(EndDialogScene);
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
            StartDialogScene();
            dialogBehaviour.StartDialog(startingDialogNodeGraph);
            hasStartedDialog = true;
            OnSceneStarted?.Invoke(mirrorScene);
        }
        else if (isRinging && hasStartedDialog)
        {
            StartDialogScene();
            dialogBehaviour.StartDialog(giveItemDialogNodeGraph);
        }
        else if (isRinging && finishedMirrorTask && finalDialogNodeGraph != null)
        {
            StartDialogScene();
            dialogBehaviour.StartDialog(finalDialogNodeGraph);
        }
        else if (finishedMirrorTask)
        {
            Think(finishedMirrorThought);
        }
        else
        {
            if (thinkDefaultThought1)
            {
                Think("I'm sure someone will call for help while I'm stuck here.");
                thinkDefaultThought1 = false;
            }
            else
            {
                Think("These magic mirrors sure are handy. If only I could use them to call for delivery.");
                thinkDefaultThought1 = true;
            }
        }
    }

    public void StartRinging()
    {
        isRinging = true;
        bgSpriteRenderer.GetComponent<SpriteRenderer>().sprite = bgActiveImage;
        bgSpriteRenderer.transform.localScale = new Vector3(activeBgSize, activeBgSize, 1);
        shimmer.Play();
    }

    public void SetToDefault()
    {
        isRinging = false;
        bgSpriteRenderer.GetComponent<SpriteRenderer>().sprite = bgDefaultImage;
        bgSpriteRenderer.transform.localScale = new Vector3(defaultBgSize, defaultBgSize, 1f);
        shimmer.Stop();
    }

    void StartDialogScene()
    {
        FindAnyObjectByType<Player>().PausePlayerMovement();
        dialogBackgroundImage.sprite = bgActiveImage;
        dialogBackground.SetActive(true);
        MusicManager.Instance.PlayMagicAudio();
    }

    void EndDialogScene()
    {
        FindAnyObjectByType<Player>().pausePlayerMovement = false;
        dialogBackground.SetActive(false);
        MusicManager.Instance.PlayOfficeAudio();
    }
}
