using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }


    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOver());      
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSecondsRealtime(delayInSeconds);
        SceneManager.LoadScene("GameOver");
    }


}
