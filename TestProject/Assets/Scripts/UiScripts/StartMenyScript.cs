using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenyScript : MonoBehaviour
{
    public void PlayBtnPressed()
    {
        SceneManager.LoadScene(sceneName: "Level_1");
    }
}
