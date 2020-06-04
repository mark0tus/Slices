using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public TextMeshProUGUI scoreText;

    [HideInInspector]
    public int score = 0;

    void Start()
    {
        
    }

    public void IncrementScore()
    {
            scoreText.text = (++score).ToString();    
    }

    public void IncrementScore(int value)
    {
        if (FindObjectOfType<GameManager>().gameIsOver == false)
        {
            score += value;
            scoreText.text = score.ToString();
        }

    }
}