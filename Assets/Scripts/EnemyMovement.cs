using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb, rbPlayer;
    public float movespeed = 5f;
    public SpriteRenderer sprite;
    public Animator animator;
    [SerializeField] LayerMask mask;
    [SerializeField] LayerMask FloorMask; 
    float Timer = 1f;
    bool isTimerStarted = false;
    bool isColliding, isGrounded;
    private RaycastHit2D Distance_E_P;
    [SerializeField] GameObject Player, Coin;
    int counterattacks;

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
        if (Vector2.Distance(Player.transform.position, transform.position) < 1f && rb.linearVelocityX > 0)
        {
            rbPlayer.AddForce(Vector2.left * 500f);
        }
        else if (Vector2.Distance(Player.transform.position, transform.position) < 1f && rb.linearVelocityX < 0)
        {
            rbPlayer.AddForce(Vector2.right * 500f);
        }

        if (counterattacks == 3 && gameObject)
        {
            Destroy(gameObject, 0.2f);
        }
        
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
    void Flip()
    {
        movespeed *= -1;
        isTimerStarted = true;
        sprite.flipX = !sprite.flipX;

    }
    
    
    private void OnDestroy()
    {
        Instantiate(Coin, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            counterattacks++;
        }
    }


}
