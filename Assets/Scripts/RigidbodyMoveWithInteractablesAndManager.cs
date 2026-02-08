using UnityEngine;
using static TargetInteractable;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMoveWithInteractablesAndManager : MonoBehaviour
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
        TargetInteractable interactable = other.GetComponent<TargetInteractable>();
        if (interactable == null) return;

        switch (interactable.type)
        {
            case InteractableType.Collectable:
                score += 1;
                interactable.Trigger();
                Debug.Log("Picked up! Score = " + score);
                break;

            case InteractableType.Trap:
                score = 0; // or damage
                interactable.Trigger();
                Debug.Log("Hit trap! Score reset.");
                break;
        }
    }
}
