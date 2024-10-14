using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum State
{
  IDLE,
  RUNNING,
  JUMPING
}

public class PlayerController : MonoBehaviour
{
  public const int SPEED = 8;
  private const int JUMPING_POWER = 6;
  private const float HEIGHT = 1.0f;
  private const float MAX_SPEED = 4.0f;
  private const float FALL_LIMIT = -10.0f;
  private State state;
  private Animator animator;

  public LayerMask groundLayer;
  public TMP_Text livesText;
  public TMP_Text scoreText;

  private Rigidbody2D rigidbody;
  private SpriteRenderer spriteRenderer;
  private Vector2 movement;
  private Vector2 startingPosition;
  private bool isJumping;
  private bool isGrounded;
  private AudioSource audioSource;

  public AudioClip collectSound;



  private void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();

    startingPosition = transform.position;
    isJumping = false;
    state = State.IDLE;

    livesText.text = "Lives: " + GameManager.lives;
    scoreText.text = "Score: " + GameManager.score;
  }

  private void Update()
  {
    Vector2 feetPosition = new Vector2(transform.position.x, transform.position.y - HEIGHT / 2.0f);
    Vector2 groundHitBoxDimensions = new Vector2(0.8f, 0.1f);
    isGrounded = Physics2D.OverlapBox(feetPosition, groundHitBoxDimensions, 0, groundLayer);

    if (transform.position.y <= FALL_LIMIT)
    {
      transform.position = startingPosition;
      rigidbody.velocity = UnityEngine.Vector2.zero;
      GameManager.lives--;
      livesText.text = "Lives: " + GameManager.lives;
      scoreText.text = "Score: " + GameManager.score;

      if (GameManager.lives == 0)
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen");
      }
    }

    movement.x = Input.GetAxis("Horizontal");

    if (movement.x < 0)
    {
      spriteRenderer.flipX = true;
    }
    else if (movement.x > 0)
    {
      spriteRenderer.flipX = false;
    }


    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      isJumping = true;
      animator.Play("Jump");
      state = State.JUMPING;
    }
    else if (Mathf.Abs(movement.x) > 0.1f && isGrounded) // Check if player is moving
    {
      state = State.RUNNING;
      animator.Play("Run");
    }
    else if (isGrounded && Mathf.Abs(movement.x) <= 0.1f)
    {
      state = State.IDLE;
      animator.Play("Idle");
    }
  }
  private void FixedUpdate()
  {
    rigidbody.AddForce(movement * SPEED);

    Vector2 velocity = rigidbody.velocity;

    velocity.x = Mathf.Clamp(velocity.x, -MAX_SPEED, MAX_SPEED);

    rigidbody.velocity = velocity;

    if (isJumping)
    {
      rigidbody.AddForce(Vector2.up * JUMPING_POWER, ForceMode2D.Impulse);
      state = State.JUMPING;
      isJumping = false;
    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Collectable"))
    {
      audioSource.PlayOneShot(collectSound);
      Destroy(collision.gameObject);
      GameManager.score++;
      scoreText.text = "Score: " + GameManager.score;
    }
    if (collision.CompareTag("Enemy"))
    {
      GameManager.lives--;
      livesText.text = "Lives: " + GameManager.lives;
      transform.position = startingPosition;

      if (GameManager.lives == 0)
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen");
      }
    }
    if (collision.CompareTag("Door"))
    {
      if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level1")
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
      }
      else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level2")
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
      }
      else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level3")
      {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");
      }
    }
  }
}
