using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float movementSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.linearVelocity = movementSpeed * GetInputDirection();
    }

    private Vector2 GetInputDirection()
    {
        Vector2 inputDir;

        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");

        inputDir.Normalize();

        return inputDir;
    }
}
