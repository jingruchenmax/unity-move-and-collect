using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidBodyBasedMove : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float accel = 12f;
    [SerializeField] float decel = 16f;

    [SerializeField] int score = 0;

    Vector2 currentVelocity;
    Rigidbody2D rb;

    float moveX;
    float moveY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 dir = new Vector2(moveX, moveY);
        Vector2 targetVelocity = dir * maxSpeed;

        float rate = (dir.sqrMagnitude > 0f) ? accel : decel;

        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            targetVelocity,
            rate * Time.fixedDeltaTime
        );

        rb.velocity = currentVelocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            score += 1;
            other.gameObject.SetActive(false);
            Debug.Log("Picked up! Score = " + score);
        }
    }
}
