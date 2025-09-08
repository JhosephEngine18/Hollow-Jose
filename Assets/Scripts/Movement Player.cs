//Librerias - Funciones Prestadas de otros scripts
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//Public - Da permiso de usar su informacion
//class - forma de declararla
//MovementPlayer - Nombre del Script
// : - Herencia, permite usar las funciones y variables de MonoBehaviour

public class MovementPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float JumpForce = 5f;
    [SerializeField] float Speed = 5f;
    [SerializeField] float DashForce = 5f;
    bool isGrounded = false;
    [Header("Essential Components")]
    [SerializeField] Rigidbody2D rb;
    Vector2 Direction;
    [SerializeField] SpriteRenderer Sprite;
    public Animator animator;
    float DeltaTime;
    

    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
        Sprite.GetComponent<SpriteRenderer>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        DeltaTime = Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            Direction = Vector2.left;
            rb.AddForce(Direction * Speed * 100f * DeltaTime);
            //Flips the sprites to the left
            Sprite.flipX = true;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            Direction = Vector2.right;
            rb.AddForce(Direction * Speed * 100f * DeltaTime);
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
        
        if (rb.linearVelocityY > 0f && !isGrounded)
        {
            //Jumping
            animator.SetInteger("AnimationState", 2);
        }
        else if(rb.linearVelocityY < 0f && !isGrounded)
        {
            //Falling
            animator.SetInteger("AnimationState", 3);
        }



        //Jump and Dash Controllers
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce * 100f);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && Direction == Vector2.right)
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
        if(collision.gameObject.tag == "Floor")
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
