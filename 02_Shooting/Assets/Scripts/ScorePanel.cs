using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    public float scoreUpSpeed = 50.0f;
    int targetScore = 0;
    float currentScore = 0.0f;

    private void Awake()
    {
        Transform panel = transform.GetChild(0);
        Transform point = panel.GetChild(1);
        scoreText = point.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.onScoreChange += SetTargetScore;

        targetScore = 0;
        currentScore = 0;
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScore < targetScore)
        {
            currentScore += Time.deltaTime * scoreUpSpeed;
            currentScore = Mathf.Min(currentScore, targetScore);
            scoreText.text = $"{currentScore:f0}";
        }
    }
    void SetTargetScore(int newScore)
    {
        targetScore = newScore;
    }
}
