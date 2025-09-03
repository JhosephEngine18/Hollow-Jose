//Librerias - Funciones Prestadas de otros scripts
using UnityEngine;
using UnityEngine.InputSystem;

//Public - Da permiso de usar su informacion
//class - forma de declararla
//MovementPlayer - Nombre del Script
// : - Herencia, permite usar las funciones y variables de MonoBehaviour

public class MovementPlayer : MonoBehaviour
{

    float movement = 0f;
    float jump = 1f;
    public float speed = 0f;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = 0;
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left);
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up);
        }

        Vector2 moveX = new Vector2(movement * Time.deltaTime * speed, 0);
        Vector2 jump = new Vector2(0f, 1f);
        transform.position += (Vector3)moveX;
    }
}
