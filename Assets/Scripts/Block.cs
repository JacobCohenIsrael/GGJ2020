using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

	[SerializeField] Sprite[] breakageSprites;
	[SerializeField] SpriteRenderer breakageRenderer;

	int hits = 0;


    // Start is called before the first frame update
    void Start()
    {
		breakageRenderer.sprite = breakageSprites[hits];
    }

	public void BreakBlock()
	{
		hits++;
		if (hits == breakageSprites.Length)
		{
			Destroy(gameObject);
		}
		else
		{
			breakageRenderer.sprite = breakageSprites[hits];
		}
	}

	public void FixBlock()
	{
		if (hits > 0)
		{
			hits--;
			breakageRenderer.sprite = breakageSprites[hits];
		}
	}
}
