using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputSystem : MonoBehaviour
{
    public InputSystem_Actions action;

    InputAction jump;
    InputAction move;
    public float MoveSpeed;
    Rigidbody2D rb;

    public Vector2 movevector;


    void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        move = action.Player.Move;
        jump = action.Player.Jump;
        jump.Enable();
        move.Enable();
    }

    private void OnDisable()
    {

        jump.Disable();
        move.Disable();
    }


    private void Update()
    {
        movevector = move.ReadValue<Vector2>();

        rb.linearVelocity = new Vector2(movevector.x * MoveSpeed, movevector.y * MoveSpeed);
    }
}
