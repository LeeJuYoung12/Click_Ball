using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private FadeManager theFade;
    GameObject StartButton, EndButton;
    void Start()
    {
            theFade = FindAnyObjectByType<FadeManager>();
            StartButton = GameObject.Find("StartButton");
            EndButton = GameObject.Find("EndButton");
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        theFade.FadeOut();
        StartButton.SetActive(false);
        EndButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        theFade.FadeIn();

        //Game start        
        SceneManager.LoadScene("GameScene");
        GameManager.isStart = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
