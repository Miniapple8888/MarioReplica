using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBManager : MonoBehaviour
{
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.gameRestart.AddListener(ResetGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
        string qbControllerName = "questionbox";
        foreach(Transform child in transform) {
            Transform qbTransform = child.transform.Find(qbControllerName);
            if (qbTransform != null) {
                GameObject qb = qbTransform.gameObject;
                QuestionBoxPowerupController qbController = qb.GetComponent<QuestionBoxPowerupController>();
                qbController.Reset();
            } else {
                Debug.LogError("No child GameObject found with name: " + qbControllerName);
            }
        }
    }
}
