
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    Rigidbody2D rg;
    Vector3 startPos;
    BoxCollider2D boxCollider;
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        rg.simulated = false;
        boxCollider.enabled = false;
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
        startPos = this.transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned) {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)
            //DestroyPowerup();
            gameObject.SetActive(false);

        } else if (col.gameObject.layer == 10) { // else if hitting Pipe, flip travel direction 
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        Debug.Log("I am spawned as well");
        rg.simulated = true;
        boxCollider.enabled = true;
        spawned = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object

    }
}