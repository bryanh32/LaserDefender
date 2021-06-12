using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{


    int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType(GetType()).Length;

        if (numberOfGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public int GetScore()
    {
        return score;
    }


    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
