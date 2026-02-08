using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AcceleratedMoveWithCollider : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float accel = 12f;
    [SerializeField] float decel = 16f;
    [SerializeField] int score = 0;
    Vector3 currentVelocity;
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0f);
        Vector3 targetVelocity = dir * maxSpeed;

        float rate = (dir.sqrMagnitude > 0f) ? accel : decel;
        currentVelocity = Vector3.MoveTowards(
          currentVelocity,
          targetVelocity,
          rate * Time.deltaTime
        );

        transform.position += currentVelocity * Time.deltaTime;
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
