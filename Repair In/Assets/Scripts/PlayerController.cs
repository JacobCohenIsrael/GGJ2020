using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 4f;
    [SerializeField] float dashSpeed = 8f;
    [SerializeField] float runSpeed = 6f;
    [SerializeField] int life = 5;
    [SerializeField] float hitPush = 2;

    Rigidbody2D myRigidbody;
	BoxCollider2D myCollider;
    Animator myAnimator;
    SpriteRenderer myRender;
    private CircleCollider2D repairCollider;
    private ContactFilter2D contactFilter2D = new ContactFilter2D();
    private RoboSounds roboSounds;

    public bool isDashing = false;
    bool inHurtAnim = false;
    bool isRepairing = false;
    bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myRender = GetComponentInChildren<SpriteRenderer>();
        repairCollider = GetComponentInChildren<CircleCollider2D>();
        contactFilter2D.layerMask = LayerMask.GetMask("BlocksLayer");
        roboSounds = GetComponent<RoboSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            return;

        if (!inHurtAnim)
        {
            if (isRepairing)
            {
                myRigidbody.velocity = Vector2.zero;
            }
            else if (isDashing)
            {
                bool isTouchingLedderOrFloor = myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer"));
                if (isTouchingLedderOrFloor)
                {
                    Vector2 playerVelocity = new Vector2(transform.localScale.x * dashSpeed, 0);
                    myRigidbody.velocity = playerVelocity;
                }
                else
                {
                    isDashing = false;
                }
            }
            else
            {
                Dash();
                Repair();
                Move();
            }
        }
		LoseLife();
    }

    public void IsFalling()
    {
        isFalling = true;
    }

    public void GetHit(Vector2 direction, int damage)
    {
        inHurtAnim = true;
        life -= damage;
        roboSounds.PlayHit();
        if (life <= 0)
        {
            myAnimator.Play("long die");
            this.enabled = false;
            return;
        }

        damage = UnityEngine.Random.Range(1, 4);

        if (damage == 1)
        {
            direction *= 0.5f;
            myAnimator.Play("hurt 1");
        }
        else if (damage == 2)
        {
            myAnimator.Play("hurt 2");
        }
        else if (damage == 3)
        {
            direction *= 1.5f;
            myAnimator.Play("hurt 3");
        }

        if (myRigidbody.velocity.y <= -1)
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);

        myRigidbody.velocity += direction * hitPush;
        isRepairing = false;
        isDashing = false;

        FindObjectOfType<GameManager>().RemoveHeart();
    }

    public void EndHurtAnim()
    {
        inHurtAnim = false;
    }

	private void LoseLife()
	{
		if(myCollider.IsTouchingLayers(LayerMask.GetMask("LoseCollider")))
		{
			life--;
			if (life == 0)
			{
				Debug.Log("YOU LOSE!!!!");   //  INSERT LOSE SCREEN HERE
			}
			else
				Respawn();
		}
	}

	private void Respawn()
	{
		//throw new NotImplementedException();
	}

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            bool isTouchingLedderOrFloor = myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer"));

            if (isTouchingLedderOrFloor)
            {
                myAnimator.Play("dash");
                roboSounds.PlayDash();
                isDashing = true;
            }
        }
    }

    public void OnDashEnd()
    {
        isDashing = false;
    }

    private void Repair()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bool isTouchingLedderOrFloor = myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer"));

            if (isTouchingLedderOrFloor)
            {
                myAnimator.Play("repair");
                roboSounds.PlayFixing();
            }
        }
    }

    public void StartRepairing()
    {
        isRepairing = true;
    }

    public void RepairBlock()
    {
        List< Collider2D> results = new List<Collider2D>();
        int blocksHit = repairCollider.OverlapCollider(contactFilter2D, results);

        if (blocksHit > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                results[i].GetComponent<Block>()?.FixBlock();
            }
        }
    }

    public void OnRepairEnd()
    {
        isRepairing = false;
    }

	private void Move()
	{
		float horizontalMove, verticalMove = 0;
        bool isTouchingLedderOrFloor = myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer"));
        horizontalMove = Input.GetAxisRaw("Horizontal");


        if (isTouchingLedderOrFloor)
        {
            if (isFalling)
            {
                isFalling = false;
                GetHit(Vector2.up, 1);
                return;
            }

            verticalMove = Input.GetAxisRaw("Vertical");
        }
        else
        {
            verticalMove = -1;
        }

        if (verticalMove != 0)
            horizontalMove = 0;

        Vector2 playerVelocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);
		myRigidbody.velocity = playerVelocity;

        if (verticalMove == 0 && horizontalMove == 0)
        {
            myAnimator.Play("robot idle");
            return;
        }

        if (isTouchingLedderOrFloor && verticalMove != 0)
        {
            myAnimator.Play("climbing");
            return;
        }

        if (!isTouchingLedderOrFloor)
        {
            AnimatorStateInfo animatorStateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
            if (!(animatorStateInfo.IsName("fall") || animatorStateInfo.IsName("fall loop")))
                myAnimator.Play("fall");
            return;
        }

        if (isTouchingLedderOrFloor && horizontalMove != 0)
        {
            myAnimator.Play("walk");

            transform.localScale = horizontalMove < 0 ? new Vector3(-1, 1, 1) : Vector3.one;

            return;
        }

    }
}
