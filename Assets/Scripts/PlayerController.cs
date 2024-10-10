using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public const int SPEED = 8;
  private const int JUMPING_POWER = 6;
  private const float HEIGHT = 1.0f;
  private const float MAX_SPEED = 4.0f;

  public LayerMask groundLayer;

  private Rigidbody2D rigidbody;
  private Vector2 movement;
  private bool isJumping;


  private void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    isJumping = false;
  }

  private void Update()
  {
    Vector2 feetPosition = new Vector2(transform.position.x, transform.position.y - HEIGHT / 2.0f);
    Vector2 groundHitBoxDimensions = new Vector2(0.8f, 0.1f);
    bool isGrounded = Physics2D.OverlapBox(feetPosition, groundHitBoxDimensions, 0, groundLayer);

    movement.x = Input.GetAxis("Horizontal");

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      isJumping = true;
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
      isJumping = false;
    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Collectable"))
    {
      Destroy(collision.gameObject);
    }
  }
}
