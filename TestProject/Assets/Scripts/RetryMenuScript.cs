using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class RetryMenuScript : MonoBehaviour
{
    [SerializeField] static Button retryBtn;
    [SerializeField] string currentScene;
    [SerializeField] static Image background;

    // Start is called before the first frame update
    void Start()
    {
        retryBtn =  transform.GetChild(1).gameObject.GetComponent<Button>();
        background = transform.GetChild(0).gameObject.GetComponent<Image>();

        retryBtn.onClick.AddListener(ReloadScene);
        retryBtn.gameObject.SetActive(false);
        background.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(sceneName: currentScene);
    }

    public static void Show() {
        retryBtn.gameObject.SetActive(true);
        background.enabled = true;
    }

}
