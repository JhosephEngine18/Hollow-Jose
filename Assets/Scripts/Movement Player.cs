//Librerias - Funciones Prestadas de otros scripts
using UnityEngine;
using UnityEngine.InputSystem;

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
    public Animator animator;

    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Direction = Vector2.left;
            rb.AddForce(Direction * Speed);
            animator.SetBool("DirectionState", false);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            Direction = Vector2.right;
            rb.AddForce(Direction * Speed);
            animator.SetBool("DirectionState", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce * 100f);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && Direction == Vector2.right)
        {
            rb.AddForce(Vector2.right * DashForce * 100f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Direction == Vector2.left)
        {
            rb.AddForce(Vector2.left * DashForce * 100f);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

}
