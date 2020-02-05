using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

	public List<Block> blocksList;
    public GameObject gearHeartPrefab;
    public int blockScore = 10;
    public float time2survive = 120;

    public AudioClip[] destructionsSFX;

	bool gameRunning;

	private int score = 0;
	float floatingScore = 0f;
    public int survivalBonusPerSecond = 1;
	[SerializeField] float scoreIncreaseRate = 5f;
	[SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI loseText;
	[SerializeField] TextMeshProUGUI winText;
	[SerializeField] Button playAgainButton;

    HorizontalLayoutGroup health;

	private void Awake()
	{
		int count = FindObjectsOfType<GameManager>().Length;
		if (count > 1)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
			DontDestroyOnLoad(gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
		StartGame();

    }

	private void StartGame()
	{
        health = GetComponentInChildren<HorizontalLayoutGroup>(true);
        while (health.transform.childCount < 5)
        {
            Instantiate(gearHeartPrefab, health.transform);
        }

        gameRunning = true;
		winText.gameObject.SetActive(false);
		loseText.gameObject.SetActive(false);
		playAgainButton.gameObject.SetActive(false);
		FindObjectOfType<BottomTrigger>().SetGameManager(this);
		blocksList.Clear();
		Block[] blocksArray = FindObjectsOfType<Block>();
		foreach (Block block in blocksArray)
		{
			blocksList.Add(block);
		}

        score = blocksArray.Length * blockScore;
        floatingScore = 0f;
        scoreText.text = score.ToString();
        StartCoroutine(SurvivalBonus());
        StartCoroutine(SurvivalTime());
    }

	// Update is called once per frame
	void Update()
    {
		if (gameRunning)
		{
			//IncreaseScoreOverTime();
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
    }

    IEnumerator SurvivalTime()
    {
        float runningTimeToSurvive = time2survive;
        while (runningTimeToSurvive > 0)
        {
            runningTimeToSurvive -= Time.deltaTime;
            timeText.text = ((int)runningTimeToSurvive).ToString();
            timeText.color = Color.Lerp(Color.red, Color.white, runningTimeToSurvive / time2survive);
            yield return null;
        }
        LevelWon();
    }


    IEnumerator SurvivalBonus()
    {
        floatingScore = 0;
        float time = 0;
        while (true)
        {
            while(time < 1)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            score += survivalBonusPerSecond;
            UpdateScore();
            yield return null;
        }
    }

    private void IncreaseScoreOverTime()
	{
		floatingScore += scoreIncreaseRate * Time.deltaTime;
		UpdateScore();
	}

    public void OnBlockDestroyed(Vector3 pos, Block block)
    {
        score -= (blockScore * 10);
		blocksList.Remove(block);
        PlayBlockDestroyed(pos);
    }

    private void PlayBlockDestroyed(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(destructionsSFX[UnityEngine.Random.Range(0, destructionsSFX.Length)], pos);
    }

	private void UpdateScore()
	{
		//if(floatingScore < 0)
		//{
		//	floatingScore = 0f;
		//}
		//score += (int)floatingScore;
		scoreText.text = "Score: " + score.ToString();
	}

	public void LevelWon()
    {
        score += health.transform.childCount * 20;
        UpdateScore();
        StopAllCoroutines();
		gameRunning = false;
		winText.gameObject.SetActive(true);
		playAgainButton.gameObject.SetActive(true);
		SceneManager.LoadScene("PlayAgainScreen");
    }

    public void LevelLost()
    {
        StopAllCoroutines();
        gameRunning = false;
		loseText.gameObject.SetActive(true);
		playAgainButton.gameObject.SetActive(true);
		SceneManager.LoadScene("PlayAgainScreen");
    }

	public void RestartGame()
	{
        SceneManager.sceneLoaded += Fuck;
        SceneManager.LoadScene(1);


    }

    private void Fuck(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name != "City 1")
            return;

        StartGame();
        SceneManager.sceneLoaded -= Fuck;
    }

    public void RemoveHeart()
    {
        Destroy(health.transform.GetChild(0).gameObject);
    }
}
