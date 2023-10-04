using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;

    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameRestart()
    {
        // reset score
        score = 0;
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        Debug.Log("Old score " + score.ToString());
        score += increment;
        Debug.Log("New score " + score.ToString());
        SetScore(score);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
        Debug.Log("Invoked score change");
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }
}