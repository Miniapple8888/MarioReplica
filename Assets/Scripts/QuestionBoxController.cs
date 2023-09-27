using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Animator questionBox;
    public GameObject coin;
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
            Vector3 coinRelativePos = new Vector3(0f, 0f, -0.33f);
            Vector3 spawnPos = transform.parent.transform.position + coinRelativePos;
            GameObject coinObject = Instantiate(coin, spawnPos, Quaternion.identity);
            coinObject.transform.parent = this.transform.parent;
            coinObject.transform.position = this.transform.parent.transform.position + coinRelativePos;
            // get rid of bouncing effect
            Destroy(spring);
            Destroy(rg);
        }
    }
}
