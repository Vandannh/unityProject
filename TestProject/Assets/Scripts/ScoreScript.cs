using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    [SerializeField] private Text scoreText;
    private int score = 0;

    void Start()
    {
        scoreText.text = "Gold Collected: " + score;  
    }
    public void coinCollected()
    {
        score++;
        scoreText.text = "Gold Collected: " + score;
    }

    public int getScore()
    {
        return score;
    }
    
}
