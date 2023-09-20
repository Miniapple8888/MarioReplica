using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// <a href="https://www.flaticon.com/free-icons/reset" title="reset icons">Reset icons created by Dixit Lakhani_02 - Flaticon</a>

public class PlayerMovement : MonoBehaviour
{
    public JumpGoomba jumpGoomba;
    public float upSpeed = 10;
    public TextMeshProUGUI scoreText;
    public Button resetButton;
    public GameObject overlay;
    public GameObject gameOverText;
    public GameObject enemies;
    private bool onGroundState = true;
    private float speed = 10;
    private float maxSpeed = 20;
    private bool facingRightState = true;
    private Rigidbody2D marioRG;
    private SpriteRenderer marioSprite;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 10;
        marioRG = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("Collided with goomba!");
            GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    // Fixed Update is called 50 times a second
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        if (Mathf.Abs(moveHorizontal) > 0) { // if player wants to move
            Vector2 movement = new Vector2(moveHorizontal, 0);
            // check it doesn't go beyond max speed
            if (marioRG.velocity.magnitude < maxSpeed)
                marioRG.AddForce(movement * speed);
        }

        // when player releases movement key, stop
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
            marioRG.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState) {
            marioRG.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }

        // toggle flipping mario when facing different direction
        if (Input.GetKeyDown("a") && facingRightState) {
            facingRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !facingRightState) {
            facingRightState = true;
            marioSprite.flipX = false;
        }
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        ResetGame();
        Time.timeScale = 1.0f; // resume time
    }

    private void ResetGame()
    {
        marioRG.transform.position = new Vector3(-6f, -3.23f, 0.0f);
        facingRightState = true;
        marioSprite.flipX = false;
        scoreText.text =  "Score: 0";
        foreach (Transform eachChild in enemies.transform) {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        jumpGoomba.score = 0;
        scoreText.transform.localPosition = new Vector3(-842.0f, 488.0f, 0.0f);
        resetButton.transform.localPosition = new Vector3(898.0f, 481.0f, 0.0f);
        overlay.SetActive(false);
        gameOverText.SetActive(false);
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0.0f; 
        scoreText.transform.localPosition = new Vector3(-39.0f, 35.0f, 0.0f);
        resetButton.transform.localPosition = new Vector3(-2.0f, -62.0f, 0.0f);
        Debug.Log("X:" +scoreText.transform.position.x.ToString());
        Debug.Log("y:" +scoreText.transform.position.y.ToString());
        overlay.SetActive(true);
        gameOverText.SetActive(true);
    }
}
