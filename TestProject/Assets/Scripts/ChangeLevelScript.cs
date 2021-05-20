using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangeLevelScript : MonoBehaviour
{
    [SerializeField] private int coinsToCollect;
    [SerializeField] private Text popUp;

    private void Start()
    {
        popUp.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            ScoreScript scoreScript = player.GetComponent<ScoreScript>();
            if (scoreScript.getScore() >= coinsToCollect)
            {
                SoundManager.PlaySound("nextLevel");
                SceneManager.LoadScene(sceneName: "Level_2");
            } else 
            {
                popUp.enabled = true;
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popUp.enabled = false;
        }
    }

}

