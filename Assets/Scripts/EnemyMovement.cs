using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed = 5f;
    public SpriteRenderer sprite;
    public Animator animator;
    [SerializeField] LayerMask mask;
    [SerializeField] LayerMask FloorMask; 
    protected float Timer = 1f;
    protected bool isTimerStarted = false;
    protected bool isColliding, isGrounded;
    [SerializeField] protected float distance;
    private void Start()
    {
        rb = rb.GetComponent<Rigidbody2D>();
        animator.SetInteger("AnimationState", 1);
    }

    private void Update()
    {
        isColliding = Physics2D.CircleCast(transform.position, 0.2f, Vector2.right, 1, mask) ||
                      Physics2D.CircleCast(transform.position, 0.2f, Vector2.left, 1, mask);
        isGrounded = Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, 20f, FloorMask);
            
        if (isTimerStarted)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0f)
        {
            isTimerStarted = false;
            Timer = 1f;
        }
        Debug.Log(Timer);
        Debug.Log(isTimerStarted);
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = movespeed;
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
        if (isColliding && !isTimerStarted || !isGrounded && !isTimerStarted)
        {
            Flip();
        }

    }
    protected void Flip()
    {
        movespeed *= -1;
        isTimerStarted = true;
        sprite.flipX = !sprite.flipX;

    }
    
}
