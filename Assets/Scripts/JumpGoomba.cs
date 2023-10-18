using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class JumpGoomba : MonoBehaviour
{
    GameManager gameManager;
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;

    [System.NonSerialized]
    public int score = 0;

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && onGroundCheck()) {
            onGroundState = false;
            countScoreState = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState  = true;
        if (!onGroundState && countScoreState) {
            if (col.gameObject.CompareTag("Enemies")) {
                if (col.gameObject.GetComponent<EnemyMovement>().GetAlive()) {
                    col.gameObject.GetComponent<Animator>().Play("goomba_dead");
                    GameManager.instance.IncreaseScore(1);
                    Debug.Log("Goomba dead");
                    col.gameObject.GetComponent<EnemyMovement>().SetAlive(false);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask)) {
            Debug.Log("on ground");
            return true;
        } else {
            Debug.Log("not on ground");
            return false;
        }
    }
}
