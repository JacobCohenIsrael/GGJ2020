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
}
