using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottomTrigger : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	public void SetGameManager(GameManager theBoss)
	{
		gameManager = theBoss;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
            gameManager.LevelLost();


        if (collision.CompareTag("Ball"))
            //gameManager.LevelWon();
            FindObjectOfType<Paddle>().SpawnBall();
    }
}
