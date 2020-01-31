using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private PaddleSettings paddleSettings;

    private Vector3 nextPosition;

    private Ball ball;


    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        nextPosition = Vector2.MoveTowards(transform.position, ball.gameObject.transform.position, paddleSettings.GetSpeed() * Time.deltaTime);
        nextPosition.y = transform.position.y;
        transform.position = nextPosition;
    }
}
