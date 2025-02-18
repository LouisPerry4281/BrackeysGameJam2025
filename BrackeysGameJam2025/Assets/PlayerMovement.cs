using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rollPower;

    bool isRolling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            StartCoroutine(Roll());
        }

        HandleAnimations();
        FlipPlayer();
    }

    void FixedUpdate()
    {
        if (!isRolling)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        rb.linearVelocity = movementSpeed * GetInputDirection();
    }

    private IEnumerator Roll()
    {
        isRolling = true;

        Vector2 initialDir = GetInputDirection();

        if (initialDir == Vector2.zero)
        {
            initialDir = Vector2.right;
        }

        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = initialDir * rollPower;

        yield return new WaitForSeconds(0.5f);

        isRolling = false;

        yield return null;
    }

    private void FlipPlayer()
    {
        if (rb.linearVelocityX > 0)
        {
            sr.flipX = false;
        }

        else if (rb.linearVelocityX < 0)
        {
            sr.flipX = true;
        }
    }

    private void HandleAnimations()
    {
        if (isRolling)
        {
            //Roll Anim
            return;
        }

        if (rb.linearVelocity != Vector2.zero)
        {
            anim.Play("PlayerRun");
        }

        else
        {
            anim.Play("PlayerIdle");
        }
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


