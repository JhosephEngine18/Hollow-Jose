using UnityEngine;

public class FlyingKirby : EnemyMovement
{
    public GameObject Point;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetInteger("AnimationState", 5);
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Point.gameObject.transform.position= new Vector2(transform.position.x,Point.transform.position.y);
        if (transform.position.y < Point.transform.position.y)
        {
            animator.SetBool("isFlying", true);
            rb.linearVelocityY = 0;
            rb.AddForce(new Vector2(0,2f), ForceMode2D.Impulse);
        }
        else
        {
            animator.SetBool("isFlying", false);
        }
        
        if (isTimerStarted)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0f)
        {
            isTimerStarted = false;
            Timer = 1f;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = movespeed;
        if (isColliding && !isTimerStarted || !isGrounded && !isTimerStarted)
        {
            Flip();
        }
    }
    
    
}
