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
    public int score = 0; // why don't we make this private then?

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
        if (!onGroundState && countScoreState) {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f) {
                countScoreState = false;
                gameManager.IncreaseScore(1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState  = true;
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
