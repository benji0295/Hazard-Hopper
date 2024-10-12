using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJumper : MonoBehaviour
{
  private const float JUMPING_FORCE = 4.0f;
  private const float JUMP_DELAY = 0.5f;
  private const float HEIGHT = 1.0f;

  public LayerMask groundLayer;
  public Sprite jumpingSprite;

  private Rigidbody2D rigidbody;
  private bool isJumping = false;
  private float timeRemaining;
  private void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    timeRemaining = JUMP_DELAY;
  }

  private void Update()
  {
    Vector2 feetPosition = new Vector2(transform.position.x, transform.position.y - HEIGHT / 2.0f);
    Vector2 groundHitBoxDimensions = new Vector2(0.8f, 0.1f);
    bool isGrounded = Physics2D.OverlapBox(feetPosition, groundHitBoxDimensions, 0, groundLayer);

    if (isGrounded)
    {
      timeRemaining -= Time.deltaTime;

      if (timeRemaining <= 0)
      {
        isJumping = true;
        timeRemaining = JUMP_DELAY;
      }
    }
  }
  private void FixedUpdate()
  {
    if (isJumping)
    {
      isJumping = false;
      rigidbody.AddForce(Vector2.up * JUMPING_FORCE, ForceMode2D.Impulse);
    }
  }
}
