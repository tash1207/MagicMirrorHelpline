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
    Vector2 lookDirection = new Vector2(0, -1);

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
            lookDirection.Set(moveInput.x, moveInput.y);
        }

        playerAnimator.SetFloat("LookX", lookDirection.x);
        playerAnimator.SetFloat("LookY", lookDirection.y);
        reversePlayerAnimator.SetFloat("LookX", lookDirection.x);
        reversePlayerAnimator.SetFloat("LookY", -lookDirection.y);
    }
}
