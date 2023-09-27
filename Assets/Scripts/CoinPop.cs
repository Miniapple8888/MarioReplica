using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPop : MonoBehaviour
{
    public AudioSource coinSound;
    private float deathDelay = 1.0f; // self destroy after spawn
    // Start is called before the first frame update
    void Start()
    {
        coinSound.PlayOneShot(coinSound.clip);
        Debug.Log("Coin pos:" + this.transform.position);
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + deathDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
