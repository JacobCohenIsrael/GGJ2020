using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private PaddleSettings paddleSettings;
    [SerializeField]
    private GameObject ballPrefab;
    private float leftLimit;
    private float rightLimit;

    private Vector3 nextPosition;

    private Ball ball;
    public AudioClip[] bounceSounds;
    AudioSource audioSource;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        leftLimit = GameObject.Find("Left").transform.position.x;
        rightLimit = GameObject.Find("Right").transform.position.x;
        audioSource = GetComponent<AudioSource>();

        InvokeRepeating("SpawnBall", 20, 20);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(bounceSounds[Random.Range(0, bounceSounds.Length)]);
    }



    public void SpawnBall()
    {
        StartCoroutine("SpawnBallDelay");
    }

    IEnumerator SpawnBallDelay()
    {
        yield return new WaitForSeconds(4);
        GameObject _ball = Instantiate(ballPrefab, this.transform.position + Vector3.up, Quaternion.identity);
        ball = _ball.GetComponent<Ball>();
    }

    private void Update()
    {
        if (ball != null)
        {
            nextPosition = Vector2.MoveTowards(transform.position, ball.gameObject.transform.position, paddleSettings.GetSpeed() * Time.deltaTime);

        }
        else
        {
            nextPosition = Vector2.MoveTowards(transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0,0)), paddleSettings.GetSpeed() * Time.deltaTime);
        }
        nextPosition.x = Mathf.Clamp(nextPosition.x, leftLimit, rightLimit);
        nextPosition.y = transform.position.y;
        transform.position = nextPosition;

    }
}
