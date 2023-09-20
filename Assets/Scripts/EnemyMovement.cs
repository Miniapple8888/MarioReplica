using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyRG;
    // Start is called before the first frame update
    void Start()
    {
        enemyRG = GetComponent<Rigidbody2D>();
        // get starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }

    void ComputeVelocity()
    {
        velocity = new Vector2(moveRight * maxOffset / enemyPatrolTime, 0);
    }

    void Movegoomba()
    {
        enemyRG.MovePosition(enemyRG.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyRG.position.x - originalX) < maxOffset) {
            Movegoomba(); // not out of boundary of place it can move
        } else {
            moveRight *= -1; // flip direction
            ComputeVelocity();
            Movegoomba();
        }
    }
}
