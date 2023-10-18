using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    // TODO: Find a way to disable bounciness of question box
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned) {
            // show disabled sprite
            this.GetComponent<Animator>().SetTrigger("spawned");
            // spawn the powerup
            powerupAnimator.SetTrigger("spawned");
        }
    }

    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Reset()
    {
        powerup.spawned = false;
        this.GetComponent<Animator>().SetTrigger("GameRestart");
        powerupAnimator.SetTrigger("GameRestart");
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

}