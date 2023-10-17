using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : BasePowerup 
{
    public AudioSource coinSound;
    public GameObject coinObj;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.type = PowerupType.Coin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SpawnPowerup()
    {
        spawned = true;
        coinSound.PlayOneShot(coinSound.clip);
        GameManager.instance.IncreaseScore(1);
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object

    }

    public void DisableCoin()
    {
        SpriteRenderer coinSprite = coinObj.GetComponent<SpriteRenderer>();
        coinSprite.enabled = false;
    }
}
