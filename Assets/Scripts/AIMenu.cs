using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMenu : MonoBehaviour
{
  public float speed = 4.0f;
  public float rightLimit = 20.0f;
  public LayerMask groundLayer;

  private Rigidbody2D playerRigidbody;
  private Rigidbody2D sawRigidbody;
  private Vector2 playerStartingPosition;
  private Vector2 sawStartingPosition;
  private GameObject player;
  private GameObject saw;

  private void Start()
  {
    playerRigidbody = GetComponent<Rigidbody2D>();
    sawRigidbody = GetComponent<Rigidbody2D>();
    player = GameObject.FindGameObjectWithTag("Player");
    saw = GameObject.FindGameObjectWithTag("Enemy");
    playerStartingPosition = player.transform.position;
    sawStartingPosition = saw.transform.position;

  }

  private void Update()
  {
    Vector2 playerVelocity = playerRigidbody.velocity;
    Vector2 sawVelocity = sawRigidbody.velocity;

    playerVelocity.x = speed;
    sawVelocity.x = speed;

    playerRigidbody.velocity = playerVelocity;
    sawRigidbody.velocity = sawVelocity;

    if (player.transform.position.x >= rightLimit || saw.transform.position.x >= rightLimit)
    {
      player.transform.position = playerStartingPosition;
      saw.transform.position = sawStartingPosition;
    }
  }
}
