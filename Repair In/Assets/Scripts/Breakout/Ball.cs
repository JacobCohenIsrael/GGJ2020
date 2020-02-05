using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private BallSettings ballSettings;

    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        AddInitialForce();
    }

    private void AddInitialForce()
    {
        myRigidBody.AddForce(ballSettings.GetInitialForce());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Mathf.Abs(myRigidBody.velocity.y) < Mathf.Abs(myRigidBody.velocity.x))
        {
            Vector2 newVelocity = new Vector2(myRigidBody.velocity.x / Mathf.Abs(myRigidBody.velocity.x) * Mathf.Abs(myRigidBody.velocity.y), myRigidBody.velocity.y / Mathf.Abs(myRigidBody.velocity.y) * Mathf.Abs(myRigidBody.velocity.x));
            myRigidBody.velocity = newVelocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc.isDashing == false)
            {
                pc.GetHit(myRigidBody.velocity.normalized, 1);
            }

            FindObjectOfType<Paddle>().SpawnBall();
            Destroy(this.gameObject);
        }


    }
}
