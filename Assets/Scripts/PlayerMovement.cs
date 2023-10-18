using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
// <a href="https://www.flaticon.com/free-icons/reset" title="reset icons">Reset icons created by Dixit Lakhani_02 - Flaticon</a>

public class PlayerMovement : MonoBehaviour 
{
    public GameConstants gameConstants;
    float deathImpulse;
    float upSpeed;
    float maxSpeed;
    float speed;
    // assignable in the inspector
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public Transform gameCamera;
    //public UnityEvent gameOver;
    private bool onGroundState = true;
    private bool jumpedState = false;
    private bool faceRightState = true;
    private bool moving = false;
    private Rigidbody2D marioRG;
    private SpriteRenderer marioSprite;
    private int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);

    // state
    [System.NonSerialized]
    public bool alive = true;

    void Awake()
    {
        // subscribe to GameRestart event
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set constants
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;

        Application.targetFrameRate = 10;
        marioRG = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator.SetBool("onGround", onGroundState);
        // subscribe to scene manager change
        SceneManager.activeSceneChanged += SetStartingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioRG.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState) {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioRG.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");

        } else if (value == 1 && !faceRightState) {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioRG.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies")) {
            if (other.gameObject.GetComponent<EnemyMovement>().GetAlive()) {
                // mario dies
                GameObject mainCam = GameObject.FindWithTag("MainCamera");
                mainCam.GetComponent<AudioSource>().Stop(); // stop bg music
                marioAnimator.Play("mario_die");
                marioAudio.PlayOneShot(marioDeath);
                alive = false;
                GameManager.instance.GameOver(); // this is happening while the animation is still playing which is why deathimpulse occurs after game is resumed
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) && !onGroundState) {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // Fixed Update is called 50 times a second
    void FixedUpdate()
    {
        if (alive && moving) {
            Move(faceRightState == true ? 1 : -1);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState) {
            marioRG.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        } 
    }

    public void JumpHold()
    {
        if (alive && jumpedState) {
            // jump higher
            marioRG.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;
        }
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // doesn't go beyond maxSpeed
        if (marioRG.velocity.magnitude < maxSpeed)
            marioRG.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0) {
            moving = false;
        } else {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
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

    public void SetStartingPosition(Scene current, Scene next)
    {
        if (next.name == "World1-2") {
            this.transform.position = new Vector3(-4.944819f, -3.443201f, 0f);
        }
    }

    public void GameRestart()
    {
        // reset position
        marioRG.transform.position = new Vector3(-6f, -3.23f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0,0,-12.28f);
        // restart playing background music
        gameCamera.GetComponent<AudioSource>().Play();
    }

}
