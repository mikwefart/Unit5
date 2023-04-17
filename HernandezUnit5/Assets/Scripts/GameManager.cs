using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

{
    public GameObject targetPrefab;
public Text scoreText;
public Text timerText;
public float gameTime = 60.0f;
public int maxTargets = 20;
public int scorePerTarget = 10;

private bool isGameActive;
private float timeRemaining;
private int score;

// Start is called before the first frame update
void Start()
{
    isGameActive = false;
    timeRemaining = gameTime;
    score = 0;
    UpdateScoreText();
    UpdateTimerText();
}

// Update is called once per frame
void Update()
{
    if (isGameActive)
    {
        timeRemaining -= Time.deltaTime;
        UpdateTimerText();

        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }
}

public void StartGame()
{
    isGameActive = true;
    StartCoroutine(SpawnTarget());
}

public void EndGame()
{
    isGameActive = false;
    StopCoroutine(SpawnTarget());
}

public void AddScore(int scoreToAdd)
{
    score += scoreToAdd;
    UpdateScoreText();
}

IEnumerator SpawnTarget()
{
    while (isGameActive && maxTargets > 0)
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        Instantiate(targetPrefab, RandomSpawnPosition(), targetPrefab.transform.rotation);
        maxTargets--;
    }
}

private Vector3 RandomSpawnPosition()
{
    float spawnX = Random.Range(-4.5f, 4.5f);
    float spawnY = Random.Range(-2.5f, 2.5f);
    return new Vector3(spawnX, spawnY, 0);
}

private void UpdateScoreText()
{
    scoreText.text = "Score: " + score;
}

private void UpdateTimerText()
{
    timerText.text = "Time: " + Mathf.FloorToInt(timeRemaining);
}
}
