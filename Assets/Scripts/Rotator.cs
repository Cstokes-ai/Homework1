using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Apply initial random velocity to the pickup
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = randomDirection * speed;
    }

    void Update()
    {
        // Rotate the object continuously
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }
}
