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

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
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
            playerAnimator.SetFloat("LookX", moveInput.x);
            playerAnimator.SetFloat("LookY", moveInput.y);
            playerAnimator.SetBool("IsWalking", true);

            reversePlayerAnimator.SetFloat("LookX", moveInput.x);
            reversePlayerAnimator.SetFloat("LookY", -moveInput.y);
            reversePlayerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
            reversePlayerAnimator.SetBool("IsWalking", false);
        }
    }
}
