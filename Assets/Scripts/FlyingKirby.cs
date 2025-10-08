using System;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingKirby : MonoBehaviour
{
    public Rigidbody2D rb, rbPlayer;
    public float movespeed = 5f;
    public SpriteRenderer sprite;
    public Animator animator;
    [SerializeField]  LayerMask mask;
    [SerializeField] LayerMask FloorMask; 
    float Timer = 1f;
    bool isTimerStarted = false;
    bool isColliding, isGrounded;
    [SerializeField] GameObject Kirby, Coin;
    int counterattacks;
    Vector3 StartPos;
    private int state;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetInteger("AnimationState", 5);
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        StartPos = transform.position;
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = Physics2D.CircleCast(transform.position, 0.2f, Vector2.right, 1, mask) ||
                      Physics2D.CircleCast(transform.position, 0.2f, Vector2.left, 1, mask);

        if (isTimerStarted)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer <= 0f)
        {
            isTimerStarted = false;
            Timer = 1f;
        }
        if (counterattacks == 3)
        {
            Destroy(gameObject, 0.2f);
        }
        rb.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
        
        
        if (Vector2.Distance(Kirby.transform.position, transform.position) < 1f && rb.linearVelocityX > 0)
        {
            rbPlayer.AddForce(Vector2.left * 500f);
        }
        else if (Vector2.Distance(Kirby.transform.position, transform.position) < 1f && rb.linearVelocityX < 0)
        {
            rbPlayer.AddForce(Vector2.right * 500f);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = movespeed;
        switch (state)
        {
            case 0:
                Searching();
                break;
            case 1:
                ReturningDefaultState();
                break;
        }
        
    }


    bool DetectKirby()
    {
        bool detected = false;
        if (Vector2.Distance(Kirby.transform.position, gameObject.transform.position) < 10f && state != 1)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, Kirby.transform.position, 5f * Time.deltaTime);
            detected = true;
        }
        else if (state != 0)
        {
            detected = false;
            state = 1;
        }
        
        return detected;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack")) 
        {
            rb.AddForce(new Vector2(20f, 0f), ForceMode2D.Impulse);
            counterattacks++;
            Debug.Log(counterattacks);
        }
        isColliding = false;
    }
    
    void Flip()
    {
        movespeed *= -1;
        isTimerStarted = true;
        sprite.flipX = !sprite.flipX;

    }
    
    private void OnDestroy()
    {
        Instantiate(Coin, transform.position, Quaternion.identity );
    }

    bool ReturningDefaultState()
    {
        bool returned = false;
        if (transform.position != StartPos && Vector2.Distance(StartPos, transform.position) > 10f)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position,StartPos, 0.5f);
            returned = false;
        }
        else if (transform.position == StartPos)
        {
            returned = true;
            state = 0;
        }
        
        return returned;
    }

    void Searching()
    {
        if (isColliding && !isTimerStarted && !DetectKirby() || !isGrounded && !isTimerStarted && !DetectKirby())
        {
            Flip();
        }
    }
}

