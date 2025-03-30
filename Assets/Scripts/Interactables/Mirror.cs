using UnityEngine;

public class Mirror : Interactable
{
    [SerializeField] SpriteRenderer bgSpriteRenderer;
    [SerializeField] Sprite bgActiveImage;
    [SerializeField] Sprite bgDefaultImage;
    [SerializeField] ParticleSystem shimmer;

    bool isRinging = false;

    override public void Interact()
    {
        base.Interact();
        if (isRinging)
        {
            Think("Hi, this is the Magic Mirror helpline. What's your emergency?");
            SetToDefault();
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
