using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4.5f;
    
    Rigidbody2D rb2d;
    Vector2 moveInput;

    void Start()
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
    }
}
