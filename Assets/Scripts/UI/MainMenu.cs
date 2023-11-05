using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public IntVariable gameScore;
    public GameObject highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        SetHighscore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("World1-1", LoadSceneMode.Single);
    }

    public void ResetHighscore()
    {
        ResetButtonState();
        gameScore.previousHighestValue = 0;
        SetHighscore();
    }

    void SetHighscore()
    {
        highScoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
    }

    void ResetButtonState()
    {
        // Reset button state so it's not clicked
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
