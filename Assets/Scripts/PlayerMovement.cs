using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator reversePlayerAnimator;

    [Header("Settings")]
    [SerializeField] float moveSpeed = 4.5f;
    
    Rigidbody2D rb2d;
    Vector2 moveInput;
    Vector2 lookDirection;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = moveInput * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.x != 0f || moveInput.y != 0f)
        {
            lookDirection.Set(moveInput.x, moveInput.y);
            lookDirection.Normalize();

            playerAnimator.SetFloat("LookX", lookDirection.x);
            playerAnimator.SetFloat("LookY", lookDirection.y);
            playerAnimator.SetBool("IsWalking", true);

            reversePlayerAnimator.SetFloat("LookX", lookDirection.x);
            reversePlayerAnimator.SetFloat("LookY", -lookDirection.y);
            reversePlayerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
            reversePlayerAnimator.SetBool("IsWalking", false);
        }
    }

    void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Interact pressed");
            Interactable interactable = GetCollidedInteractable();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void OnPickUp(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("PickUp pressed");
            Interactable interactable = GetCollidedInteractable();

            if (interactable != null)
            {
                interactable.PickUp();
            }
        }
    }

    Interactable GetCollidedInteractable()
    {
        Vector2 raycastOrigin = new Vector2(rb2d.position.x, rb2d.position.y);
        RaycastHit2D hit = Physics2D.Raycast(
            raycastOrigin, lookDirection, 2f, LayerMask.GetMask("Interactable"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
            {
                return interactable;
            }
        }

        return null;
    }
}
