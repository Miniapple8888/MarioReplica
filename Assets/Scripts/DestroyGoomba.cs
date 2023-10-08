using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyGoomba : MonoBehaviour
{
    public UnityEvent goombaDie;
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
        // if collider has tag enemy
        if (col.gameObject.CompareTag("Enemies")) {
            col.gameObject.GetComponent<Animator>().Play("goomba_dead");
            Debug.Log("Goomba dead");
            col.gameObject.GetComponent<EnemyMovement>().SetAlive(false);
        }
    }
}
