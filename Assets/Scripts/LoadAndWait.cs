using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadAndWait : MonoBehaviour
{
    public CanvasGroup c;

    void Start()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
        {
            c.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.1f);
        }

    }

    public void ReturnToMain()
    {
        // TODO
        Debug.Log("Return to main menu");
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}
