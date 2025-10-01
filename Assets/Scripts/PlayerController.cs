using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof (PlayerCollisions))]

public class PlayerController : MonoBehaviour
{    
    [Header("Movement Settings")]
    [Tooltip("With this setting you will be able to setup how much the player can Jump")]
    [SerializeField] float JumpForce = 5f;
    [Tooltip("With this setting you will be able to setup how much the player can Run")]
    [SerializeField] float Speed = 5f;
    [Tooltip("With this setting you will be able to setup how much the player can Dash")]
    [SerializeField] float DashForce = 5f;
    [Tooltip("With this setting you will be able to setup how much jumps the player can do")]
    [SerializeField] int MaxJumps = 4;
    private bool isGrounded;
    [Header("Essential Components")]
    [SerializeField] Rigidbody2D rb;
    Vector2 Direction;
    [SerializeField] SpriteRenderer Sprite;
    public Animator animator;
    public static event Action<int> Sounds;
    bool isCrouching;
    int counter;
    [SerializeField] LayerMask FloorMask;
    private void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
        Sprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, 0.5f, FloorMask);
        
        if (Input.GetKey(KeyCode.A) && !isCrouching)
        {
            Direction = Vector2.left;
            rb.linearVelocityX = Speed * -1;
            //Flips the sprites to the left
            Sprite.flipX = true;
        }

        else if (Input.GetKey(KeyCode.D) && !isCrouching)
        {
            Direction = Vector2.right;
            rb.linearVelocityX = Speed;
            //Flips the sprites to the right
            Sprite.flipX = false;
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.linearVelocityX = 0;
        }


        //Checks the current velocity from the Player to change its animation state
        if (rb.linearVelocityX == 0 && isGrounded)
        {
            //Idle
            animator.SetInteger("AnimationState", 0);
        }

        else if (rb.linearVelocityX != 0 && isGrounded)
        {
            //Walking
            animator.SetInteger("AnimationState", 1);
        }
        if(Input.GetKey(KeyCode.S) && isGrounded && rb.linearVelocityX == 0)
        {
            //Crouching
            animator.SetInteger("AnimationState", 4);
            isCrouching = true;

        }
        else if(Input.GetKeyUp(KeyCode.S) && isGrounded && rb.linearVelocityX == 0)
        {
            isCrouching = false;
        }


        if (rb.linearVelocityY > 0f && !isGrounded)
        {
            //Jumping
            animator.SetInteger("AnimationState", 2);
        }
        else if (rb.linearVelocityY < 0f && !isGrounded)
        {
            //Falling
            animator.SetInteger("AnimationState", 3);
            animator.SetBool("isFlying", false);
        }

        if (rb.linearVelocityY > 0f && !isGrounded && counter >= 2)
        {
            //Flying
            animator.SetInteger("AnimationState", 5);
            animator.SetBool("isFlying", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && counter > 1 && counter != MaxJumps)
        {
            rb.linearVelocityY = 0f;
        }

        //Jump and Dash Controllers
        if (Input.GetKeyDown(KeyCode.Space) && counter <= MaxJumps)
        {
            rb.AddForce(Vector2.up * JumpForce * 100f);
            Sounds(0);
            counter++;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Direction == Vector2.right)
        {
            rb.AddForce(Vector2.right * DashForce * 200f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Direction == Vector2.left)
        {
            rb.AddForce(Vector2.left * DashForce * 200f);
        }

        if (isGrounded)
        {
            counter = 0;
        }
        
    }
    
}
