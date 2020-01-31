using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;
	public bool isTouchingLadder;
	public bool isTouchingWalkway;

	Rigidbody2D myRigidbody;
	BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		Move();
    }

	private void Move()
	{
		float horizontalMove, verticalMove;

		horizontalMove = Input.GetAxisRaw("Horizontal");
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
		{
			verticalMove = Input.GetAxisRaw("Vertical");
		}
		else
			verticalMove = -1;

		Vector2 playerVelocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);
		myRigidbody.velocity = playerVelocity;
	}
}
