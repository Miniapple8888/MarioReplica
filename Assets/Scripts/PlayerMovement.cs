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
    public float speed = 20;
    public float deathImpulse = 5.0f;
    public TextMeshProUGUI scoreText;
    public Button resetButton;
    public GameObject overlay;
    public GameObject gameOverText;
    public GameObject enemies;
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public Transform gameCamera;
    private bool onGroundState = true;
    private float maxSpeed = 20;
    private bool faceRightState = true;
    private Rigidbody2D marioRG;
    private SpriteRenderer marioSprite;

    // state
    [System.NonSerialized]
    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 10;
        marioRG = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator.SetBool("onGround", onGroundState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState) {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioRG.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
        if (Input.GetKeyDown("d") && !faceRightState) {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioRG.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioRG.velocity.x));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("Collided with goomba!");
            // play death animation
            marioAnimator.Play("mario_die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
            GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // Fixed Update is called 50 times a second
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        if (alive) {
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
                marioAnimator.SetBool("onGround", onGroundState);
            }

            // toggle flipping mario when facing different direction
            if (Input.GetKeyDown("a") && faceRightState) {
                faceRightState = false;
                marioSprite.flipX = true;
            }

            if (Input.GetKeyDown("d") && !faceRightState) {
                faceRightState = true;
                marioSprite.flipX = false;
            }   
        }
        
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioRG.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        GameOver();
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
        faceRightState = true;
        marioSprite.flipX = false;
        scoreText.text =  "Score: 0";
        foreach (Transform eachChild in enemies.transform) {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        jumpGoomba.score = 0;
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
        scoreText.transform.localPosition = new Vector3(-842.0f, 488.0f, 0.0f);
        resetButton.transform.localPosition = new Vector3(898.0f, 481.0f, 0.0f);
        overlay.SetActive(false);
        gameOverText.SetActive(false);
        // reset camera
        gameCamera.position = new Vector3(0,0,-12.28f);
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
