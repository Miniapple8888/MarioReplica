using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlockCoin : MonoBehaviour
{
    public GameObject coin;
    private bool collectedCoin = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // mario collides with bottom edge of box
        if (!collectedCoin) {
            collectedCoin = true;
            // spawn coin
            Vector3 offset = new Vector3(30f, 0f, 0f);
            Vector3 spawnPos = transform.position + offset;
            GameObject coinObject = Instantiate(coin, spawnPos, Quaternion.identity);
            Debug.Log("Before coin pos:" + coinObject.transform.position.x);
            coinObject.transform.parent = this.transform.parent;
            coinObject.transform.position = offset;
            Debug.Log("After coin pos:" + coinObject.transform.position.x);
        }
    }
}
