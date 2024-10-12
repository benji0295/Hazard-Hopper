using System;
using System.Collections;
using System.Collections.Generic;
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
  private State state;
  private Animator animator;

  public LayerMask groundLayer;

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
  }

  private void Update()
  {
    Vector2 feetPosition = new Vector2(transform.position.x, transform.position.y - HEIGHT / 2.0f);
    Vector2 groundHitBoxDimensions = new Vector2(0.8f, 0.1f);
    isGrounded = Physics2D.OverlapBox(feetPosition, groundHitBoxDimensions, 0, groundLayer);

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
      Destroy(collision.gameObject);
      audioSource.PlayOneShot(collectSound);
    }
    if (collision.CompareTag("Enemy"))
    {
      transform.position = startingPosition;
    }
    if (collision.CompareTag("Door"))
    {
      Debug.Log("Next Level.");
    }
  }
}
