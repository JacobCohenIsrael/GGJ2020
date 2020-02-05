using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{

	[SerializeField] Sprite[] breakageSprites;
	[SerializeField] SpriteRenderer breakageRenderer;
    [SerializeField] BlockHitEvent blockHitEvent;
    [SerializeField] BlockHitEvent blockFixedEvent;

    int hits = 0;


    // Start is called before the first frame update
    void Start()
    {
		breakageRenderer.sprite = breakageSprites[hits];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BreakBlock();
    }

    private void OnCollisionStay(Collision collision)
    {
        BreakBlock();
    }


    public void BreakBlock()
	{
		hits++;
		if (hits == breakageSprites.Length)
		{
			Destroy(gameObject);
            FindObjectOfType<GameManager>().OnBlockDestroyed(transform.position, this);
		}
		else
		{
			breakageRenderer.sprite = breakageSprites[hits];
		}
        if (blockHitEvent != null)
        {
            blockHitEvent.Invoke(transform.position);
        }
	}

	public void FixBlock()
	{
		if (hits > 0)
		{
			hits--;
			breakageRenderer.sprite = breakageSprites[hits];
            blockFixedEvent.Invoke(transform.position);

        }
	}
}
