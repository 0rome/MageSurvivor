using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float currentMoveSpeed = 10f;
    [SerializeField] private float maxMoveSpeed = 30f;


    //Parametrs
    public float MoveSpeed => currentMoveSpeed;
    public float MaxSpeed => maxMoveSpeed;

    //Components
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x =Input.GetAxisRaw("Horizontal");
        movement.y =Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * currentMoveSpeed;
    }
    public void AddMoveSpeed(float moveSpeed)
    {
        currentMoveSpeed += moveSpeed;
    }
}
