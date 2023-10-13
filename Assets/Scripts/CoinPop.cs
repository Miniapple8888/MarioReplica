using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPop : MonoBehaviour
{
    public AudioSource coinSound;
    public Animator coinAnimator;
    //private float deathDelay = 1.0f; // self destroy after spawn
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Debug.Log("Coin popped!");
        //coinAnimator.SetTrigger("PopCoin"); // trigger animation
        coinAnimator.Play("coin_pop");
        coinSound.PlayOneShot(coinSound.clip);
        ///Debug.Log("Coin pos:" + this.transform.position);
        //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + deathDelay);
    }

    public void Pop()
    {
        // disabling coin when finished animation
        gameObject.SetActive(false);
    }
}
