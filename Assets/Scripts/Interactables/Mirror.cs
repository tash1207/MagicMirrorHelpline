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
    [SerializeField] Sprite charSprite;
    [SerializeField] ParticleSystem shimmer;

    [Header("Dialog")]
    [SerializeField] MirrorScene mirrorScene;
    [SerializeField] GameObject dialogBackground;
    [SerializeField] Image dialogBackgroundImage;
    [SerializeField] Image dialogCharacterImage;
    [SerializeField] DialogBehaviour dialogBehaviour;
    [SerializeField] DialogNodeGraph startingDialogNodeGraph;
    [SerializeField] DialogNodeGraph giveItemDialogNodeGraph;
    [SerializeField] DialogNodeGraph finalDialogNodeGraph;
    [SerializeField] string finishedMirrorThought;

    bool isRinging = false;
    bool hasStartedDialog = false;
    public bool finishedMirrorTask = false;
    bool hasReceivedReward = false;
    bool hasReceivedAdditionalItem = false;
    bool isActiveDialogScene = false;

    float activeBgSize = 0.18f;
    float defaultBgSize = 2.2f;
    float rewardDialogDelay = 0.5f;

    void Start()
    {
        dialogBehaviour.OnEndOfMirrorScene += EndMirrorScene;
        dialogBehaviour.AddListenerToDialogFinishedEvent(EndDialogScene);
        dialogBehaviour.BindExternalFunction("UsedPencil", UsedPencil);
        dialogBehaviour.BindExternalFunction("UsedSlippers", UsedSlippers);
        dialogBehaviour.BindExternalFunction("UsedAxe", UsedAxe);
        dialogBehaviour.BindExternalFunction("UsedMusicBox", UsedMusicBox);
        dialogBehaviour.BindExternalFunction("UsedRedReward", UsedRedReward);
        dialogBehaviour.BindExternalFunction("UsedWhiteReward", UsedWhiteReward);
        dialogBehaviour.BindExternalFunction("GetOnion", GetOnion);
        dialogBehaviour.BindExternalFunction("GetSpices", GetSpices);
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

    void UsedRedReward()
    {
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.RedRidingHoodReward))
        {
            Inventory.Instance.RemoveObject(Inventory.InventoryObject.RedRidingHoodReward);
        }
    }

    void UsedWhiteReward()
    {
        if (Inventory.Instance.HasObject(Inventory.InventoryObject.SnowWhiteReward))
        {
            Inventory.Instance.RemoveObject(Inventory.InventoryObject.SnowWhiteReward);
        }
    }

    void GetOnion()
    {
        if (!Inventory.Instance.HasObject(Inventory.InventoryObject.Onion))
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.Onion, "Onion??");
        }
    }

    void GetSpices()
    {
        if (!Inventory.Instance.HasObject(Inventory.InventoryObject.Spices))
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.Spices, "Bay spices");
        }
    }

    void EndMirrorScene(MirrorScene scene)
    {
        if (mirrorScene == scene)
        {
            finishedMirrorTask = true;
            SetToDefault();
            OnSceneFinished?.Invoke(mirrorScene);
        }
    }

    void HandleFinishedSceneInventoryChanges(MirrorScene scene)
    {
        if (mirrorScene == MirrorScene.Cinderella)
        {
            if (!hasReceivedReward)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.Wine, "Bottle of white wine");
                InternalDialogManager.Instance.ShowDialogAfterSeconds("Received Bottle of white wine", rewardDialogDelay);
                hasReceivedReward = true;
            }
        }
        else if (mirrorScene == MirrorScene.Jack)
        {
            if (!hasReceivedReward)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.Cheese, "Giant chunk of cheese");
                InternalDialogManager.Instance.ShowDialogAfterSeconds("Received Giant chunk of cheese", rewardDialogDelay);
                hasReceivedReward = true;
            }
        }
        else if (mirrorScene == MirrorScene.LittleMermaid)
        {
            if (!hasReceivedReward)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.CrystalBall, "Crystal ball");
                InternalDialogManager.Instance.ShowDialogAfterSeconds("Received Crystal ball", rewardDialogDelay);
                hasReceivedReward = true;
            }
            else if (!hasReceivedAdditionalItem)
            {
                InternalDialogManager.Instance.ShowDialogAfterSeconds("Received Bay spices", rewardDialogDelay);
                hasReceivedAdditionalItem = true;
            }
        }
        else if (mirrorScene == MirrorScene.RedRiding)
        {
            if (!hasReceivedReward)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.RedRidingHoodReward, "Stinky wolf fur");
                Inventory.Instance.AddObject(Inventory.InventoryObject.Baguette, "Old baguettes");
                InternalDialogManager.Instance.ShowDialogAfterSeconds("Received Stinky wolf fur and Old baguettes", rewardDialogDelay);
                hasReceivedReward = true;
            }
        }
        else if (mirrorScene == MirrorScene.RobinHood)
        {
            if (Inventory.Instance.HasObject(Inventory.InventoryObject.CrystalBall))
            {
                Inventory.Instance.RemoveObject(Inventory.InventoryObject.CrystalBall);
            }

            if (!hasReceivedReward)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.Broth, "Pot of broth");
                InternalDialogManager.Instance.ShowDialogAfterSeconds("Received Pot of broth", rewardDialogDelay);
                hasReceivedReward = true;
            }
        }
        else if (mirrorScene == MirrorScene.SnowWhite)
        {
            if (!hasReceivedReward)
            {
                Inventory.Instance.AddObject(Inventory.InventoryObject.SnowWhiteReward, "Huntsman's axe");
                if (!Inventory.Instance.HasObject(Inventory.InventoryObject.Onion))
                {
                    Inventory.Instance.AddObject(Inventory.InventoryObject.Onion, "Onion??");
                }
                InternalDialogManager.Instance.ShowDialogAfterSeconds("Received Huntsman's axe... and Onion??", rewardDialogDelay);
                hasReceivedReward = true;
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
        else if (isRinging && finishedMirrorTask && finalDialogNodeGraph != null)
        {
            StartDialogScene();
            dialogBehaviour.StartDialog(finalDialogNodeGraph);
        }
        else if (finishedMirrorTask)
        {
            Think(finishedMirrorThought);
        }
        else if (isRinging && hasStartedDialog)
        {
            StartDialogScene();
            dialogBehaviour.StartDialog(giveItemDialogNodeGraph);
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
        FindAnyObjectByType<Player>().EnterMirrorScene();
        dialogBackgroundImage.sprite = bgActiveImage;
        dialogCharacterImage.sprite = charSprite;
        dialogBackground.SetActive(true);
        MusicManager.Instance.PlayMagicAudio();
        isActiveDialogScene = true;
    }

    void EndDialogScene()
    {
        if (isActiveDialogScene)
        {
            FindAnyObjectByType<Player>().ExitMirrorScene();
            dialogBackground.SetActive(false);
            MusicManager.Instance.PlayOfficeAudio();
            isActiveDialogScene = false;

            Inventory.Instance.ShowInventoryTip();

            if (finishedMirrorTask)
            {
                HandleFinishedSceneInventoryChanges(mirrorScene);
            }
        }
    }
}
