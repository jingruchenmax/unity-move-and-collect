using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidBodyBasedMoveWithGravity : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float accel = 12f;
    [SerializeField] float decel = 16f;

    [SerializeField] float jumpImpulse = 7f;

    [SerializeField] int score = 0;

    Vector2 currentVelocity;
    Rigidbody2D rb;

    float moveX;
    bool jumpPressed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        float targetX = moveX * maxSpeed;
        float rate = (Mathf.Abs(moveX) > 0f) ? accel : decel;

        currentVelocity.x = Mathf.MoveTowards(
            currentVelocity.x,
            targetX,
            rate * Time.fixedDeltaTime
        );

        currentVelocity.y = rb.velocity.y;

        rb.velocity = currentVelocity;

        if (jumpPressed)
        {
            rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            jumpPressed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Simplified way (tag + directly disable):
        // if (other.CompareTag("Target"))
        // {
        //     score += 1;
        //     other.gameObject.SetActive(false);
        //     Debug.Log("Picked up! Score = " + score);
        // }

        // Clear accessing the other object by explicitly grabbing the other object’s script/component so you can call its behavior.
        TargetInteractable target = other.GetComponent<TargetInteractable>();
        if (target != null)
        {
            score += 1;
            target.Trigger();

            Debug.Log("Picked up! Score = " + score);
        }
    }
}
