using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Transform centerTransform;
    // Reference to Rigidbody2D component of the ball game object
    Rigidbody2D rb;

    // Range option so moveSpeedModifier can be modified in Inspector
    // this variable helps to simulate objects acceleration
    [Range(0.1f, 2f)]
    public float moveSpeedModifier = 0.5f;

    // Direction variables that read acceleration input to be added
    // as velocity to Rigidbody2d component
    float dirX, dirY;

    // Setting bool variable that ball is alive at the beginning
    static bool isDead;

    // Variable to allow or disallow movement when ball is alive or dead
    static bool moveAllowed;
    

    // Use this for initialization
    void Start()
    {

        // Ball is alive at the start
        isDead = false;

        // Getting Rigidbody2D component of the ball game object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsGameOver() || GameManager.IsGamePaused()) return;

        // Getting devices accelerometer data in X and Y direction
        // multiplied by move speed modifier
        dirX = (Mathf.Abs(Input.acceleration.x) >= 0.06) ? Input.acceleration.x * moveSpeedModifier : 0;
        dirY = (Mathf.Abs(Input.acceleration.y) >= 0.06) ? Input.acceleration.y * moveSpeedModifier : 0;

        Mathf.Clamp(dirX, -1 * moveSpeedModifier, 1 * moveSpeedModifier);
        Mathf.Clamp(dirY, -1 * moveSpeedModifier, 1 * moveSpeedModifier);

        if (GameManager.i.missedPoint > 0)
        {
            var heading = (Vector2)(transform.position - centerTransform.position);

            var distance = heading.magnitude;
            var direction = distance != 0 ? heading / distance : new Vector2(Random.Range(0.1f, 1), Random.Range(0.1f, 1)).normalized; // This is now the normalized direction.

            dirX += direction.x * moveSpeedModifier * GameManager.i.missedPoint;
            dirY += direction.y * moveSpeedModifier * GameManager.i.missedPoint;

            GameManager.i.missedPoint = 0;
        }

    }

    

    void FixedUpdate()
    {
        if (GameManager.IsGameOver()) return;

        // Setting a velocity to Rigidbody2D component according to accelerometer data
        if (moveAllowed)
            rb.MovePosition( (Vector2)transform.position + new Vector2(rb.velocity.x + dirX, rb.velocity.y + dirY));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.GameOver();
    }

    public void AllowMove()
    {
        moveAllowed = true;
    }

}
