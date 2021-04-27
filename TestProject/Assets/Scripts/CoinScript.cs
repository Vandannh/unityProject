using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    ScoreScript scoreScript;

    private void Start()
    {
        scoreScript = GetComponent<ScoreScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scoreScript.coinCollected();
            Destroy(gameObject);
        }
    }
}
