using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator reversePlayerAnimator;

    [Header("Settings")]
    [SerializeField] float moveSpeed = 4.5f;
    [SerializeField] float sprintMoveSpeed = 6.5f;
    
    Rigidbody2D rb2d;
    Vector2 moveInput;
    Vector2 lookDirection;
    bool isSprinting;

    bool inMirrorScene = false;
    public bool pausePlayerMovement = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ShowOpeningInternalDialog();
    }

    void FixedUpdate()
    {
        if (pausePlayerMovement) { return; }
        rb2d.linearVelocity = moveInput * (isSprinting ? sprintMoveSpeed : moveSpeed);
    }

    void OnMove(InputValue value)
    {
        if (pausePlayerMovement || inMirrorScene) { return; }
        moveInput = value.Get<Vector2>();

        if (moveInput.x != 0f || moveInput.y != 0f)
        {
            lookDirection.Set(moveInput.x, moveInput.y);
            lookDirection.Normalize();

            playerAnimator.SetFloat("LookX", lookDirection.x);
            playerAnimator.SetFloat("LookY", lookDirection.y);
            playerAnimator.SetBool("IsWalking", true);

            reversePlayerAnimator.SetFloat("LookX", lookDirection.x);
            reversePlayerAnimator.SetFloat("LookY", lookDirection.y);
            reversePlayerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
            reversePlayerAnimator.SetBool("IsWalking", false);
        }
    }

    void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    void OnInteract(InputValue value)
    {
        if (pausePlayerMovement || inMirrorScene) { return; }
        if (value.isPressed)
        {
            Interactable interactable = GetCollidedInteractable();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void OnInventory(InputValue value)
    {
        if (value.isPressed)
        {
            GetComponent<Inventory>().ToggleInventory();
        }
    }

    void OnNextDialog(InputValue value)
    {
        if (inMirrorScene) { return; }
        if (value.isPressed)
        {
            InternalDialogManager.Instance.NextDialog();
        }
    }

    void OnMute(InputValue value)
    {
        if (value.isPressed)
        {
            AudioSource audioSource = MusicManager.Instance.GetComponent<AudioSource>();
            audioSource.mute = !audioSource.mute;
        }
    }

    Interactable GetCollidedInteractable()
    {
        Vector2 raycastOrigin = new Vector2(rb2d.position.x, rb2d.position.y);
        RaycastHit2D hit = Physics2D.Raycast(
            raycastOrigin, lookDirection, 1.7f, LayerMask.GetMask("Interactable"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
            {
                return interactable;
            }
        }

        return null;
    }

    public void EnterMirrorScene()
    {
        inMirrorScene = true;
        PausePlayerMovement();
    }

    public void ExitMirrorScene()
    {
        inMirrorScene = false;
        pausePlayerMovement = false;
    }

    public void PausePlayerMovement()
    {
        pausePlayerMovement = true;
        rb2d.linearVelocity = new Vector2(0, 0);
        playerAnimator.SetBool("IsWalking", false);
        reversePlayerAnimator.SetBool("IsWalking", false);
    }

    void ShowOpeningInternalDialog()
    {
        PausePlayerMovement();
        string[] openingThoughts = new string[] {
            "Ugh, I can't believe they locked me in the office for the weekend. Again.",
            "If I had a nickel for every time this happened…",
            "I'd have two nickels. Which is still //way// too many.",
            "I'm not going to starve this time, at least. I'll find food if it's the last thing I do.",
            "...",
            "Which it won't be. Because I won't starve.",
            "But I am already hungry. I should check the kitchen for food."
        };

        InternalDialogManager.Instance.ShowDialog(openingThoughts);
    }
}
