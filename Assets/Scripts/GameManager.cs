using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public List<Block> blocksList;

    // Start is called before the first frame update
    void Start()
    {
		Block[] blocksArray = FindObjectsOfType<Block>();
		foreach(Block block in blocksArray)
		{
			blocksList.Add(block);
		}
    }

    // Update is called once per frame
    void Update()
    {
		TestBreakage();
    }

	// FOR DEBUGGING
	private void TestBreakage()
	{
		if(Input.GetKeyDown(KeyCode.Space) && blocksList[0] != null)
		{
			blocksList[0].BreakBlock();
		}
	}
}
