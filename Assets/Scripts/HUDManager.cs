using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{

    private Vector3[] scoreTextPos = {
        new Vector3(-842.0f, 488.0f, 0.0f),
        new Vector3(-39.0f, 35.0f, 0.0f)
    };
    private Vector3[] resetButtonPos = {
        new Vector3(898.0f, 481.0f, 0.0f),
        new Vector3(-2.0f, -62.0f, 0.0f)
    };
    public GameObject scoreText;
    public Transform resetButton;

    public GameObject overlay;
    public GameObject gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        // remove gameover screen, resets text to original position
        scoreText.transform.localPosition = scoreTextPos[0];
        resetButton.transform.localPosition = resetButtonPos[0];
        overlay.SetActive(false);
        gameOverText.SetActive(false);
        
    }

    public void SetScore(int score)
    {
        Debug.Log("Setting Score");
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        overlay.SetActive(true);
        gameOverText.SetActive(true);
        scoreText.transform.localPosition = scoreTextPos[1];
        resetButton.transform.localPosition = resetButtonPos[1];
    }
}
