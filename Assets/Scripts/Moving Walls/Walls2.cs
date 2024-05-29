using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls2 : MonoBehaviour
{
    public float Offset = 3;
    public float speed = 2;
    public float pauseTime = 2; // Time to pause in seconds
    public float pauseTimeAtStart = 2; // Time to pause at the beginning

    private float leftLimit;
    private float rightLimit;
    private Rigidbody rb;
    private bool movingRight;
    private bool startMoving = false;

    void Start()
    {
        leftLimit = transform.position.x;
        rightLimit = transform.position.x + Offset;
        rb = GetComponent<Rigidbody>();
        movingRight = true;
    }

    void FixedUpdate()
    {
        if (startMoving == false)
        {
            StartCoroutine(InitialPause());
            startMoving = true;
        }


        if (movingRight)
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
            if (transform.position.x > rightLimit)
            {
                StartCoroutine(PauseBeforeMoving());
                movingRight = false;
            }
        }
        else
        {
            rb.MovePosition(transform.position - Vector3.right * speed * Time.deltaTime);
            if (transform.position.x < leftLimit)
            {
                StartCoroutine(PauseBeforeMoving());
                movingRight = true;
            }
        }
    }

    IEnumerator PauseBeforeMoving()
    {
        float currentSpeed = speed;
        speed = 0; // Stop the platform
        yield return new WaitForSeconds(pauseTime); // Wait for pauseTime seconds
        speed = currentSpeed; // Resume movement
    }

    IEnumerator InitialPause()
    {
        float currentSpeed = speed;
        speed = 0; // Stop the platform
        yield return new WaitForSeconds(pauseTimeAtStart); // Wait for pauseTimeAtStart seconds
        speed = currentSpeed; // Resume movement
    }
}
