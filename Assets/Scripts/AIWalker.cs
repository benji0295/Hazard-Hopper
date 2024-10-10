using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalker : MonoBehaviour
{
  public float threshold = 10.0f;
  public float speed = 4.0f;
  public LayerMask groundLayer;

  private Rigidbody2D rigidbody;
  private AIUtility.State state = AIUtility.State.IDLE;
  private GameObject player;


  private void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    player = GameObject.FindGameObjectWithTag("Player");
  }

  private void Update()
  {
    if (state == AIUtility.State.IDLE)
    {
      float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

      if (distanceToPlayer <= threshold) state = AIUtility.State.WALKING;
    }
    else if (state == AIUtility.State.WALKING)
    {
      // Left sensor
      if (!IsGrounded(-0.3f, -0.3f) && speed < 0) speed *= -1;

      // Right sensor
      if (!IsGrounded(0.3f, -0.3f) && speed > 0) speed *= -1;

      Vector2 velocity = rigidbody.velocity;
      velocity.x = speed;
      rigidbody.velocity = velocity;
    }
  }

  public bool IsGrounded(float xOffset, float yOffset)
  {
    Vector2 hitBoxPosition = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);

    var dimensions = new Vector2(0.1f, 0.1f);

    var isGrounded = Physics2D.OverlapBox(hitBoxPosition, dimensions, 0, groundLayer);

    return isGrounded;
  }
}
