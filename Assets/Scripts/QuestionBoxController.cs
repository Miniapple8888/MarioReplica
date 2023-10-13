using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Animator questionBox;
    public GameObject coin;
    public GameObject powerup;
    private SpringJoint2D spring;
    private Rigidbody2D rg;
    private bool isDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        questionBox.SetBool("disabled", isDisabled);
        spring = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        if (coin == null) {
            Debug.LogError("Coin is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // mario collides with bottom edge of box
        if (!isDisabled) {
            isDisabled = true;
            questionBox.SetBool("disabled", isDisabled);
            // spawn coin
            if (coin != null) {
                coin.SetActive(true);
            }
            // spawn powerup
            if (powerup != null) {
                Animator powerupAnim = powerup.GetComponent<Animator>();
                powerupAnim.Play("spawn");
            }
            // powerupSource.PlayOneShot(coinSound.clip); dunno if there is sound
            // get rid of bouncing effect
            //Destroy(spring);
            //spring.enabled = false;
            //Destroy(rg);
            //rg.isKinematic = false;
        }
    }

    public void Reset()
    {
        isDisabled = false;
        questionBox.SetBool("disabled", isDisabled);
        //rg.isKinematic = true;
        //spring.enabled = true;
    }
}
