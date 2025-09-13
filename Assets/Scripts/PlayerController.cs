using UnityEngine;
using UnityEditor.Animations;
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
    bool isGrounded = false;
    [Header("Essential Components")]
    [SerializeField] Rigidbody2D rb;
    Vector2 Direction;
    [SerializeField] SpriteRenderer Sprite;
    public Animator animator;
    float DeltaTime;
    public static event Action<int> Sounds;
    bool isCrouching;
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
        Sprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DeltaTime = Time.deltaTime;
        if (Input.GetKey(KeyCode.A) && !isCrouching)
        {
            Direction = Vector2.left;
            rb.linearVelocityX = Speed * -100f * DeltaTime;
            //Flips the sprites to the left
            Sprite.flipX = true;
        }

        else if (Input.GetKey(KeyCode.D) && !isCrouching)
        {
            Direction = Vector2.right;
            rb.linearVelocityX = Speed * 100f * DeltaTime;
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
        }



        //Jump and Dash Controllers
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce * 100f);
            Sounds(0);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Direction == Vector2.right)
        {
            rb.AddForce(Vector2.right * DashForce * 200f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Direction == Vector2.left)
        {
            rb.AddForce(Vector2.left * DashForce * 200f);
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }

}
