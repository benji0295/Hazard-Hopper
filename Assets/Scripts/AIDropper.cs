using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class AIDropper : MonoBehaviour
{

  private float riseSpeed = 2.0f;
  private float waitTime = 2.0f;

  private GameObject player;
  private Rigidbody2D rigidbody;
  private Vector2 startingPosition;
  private bool isDropping = false;

  private void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    startingPosition = transform.position;
  }

  private void Update()
  {
    if (!isDropping)
    {
      rigidbody.gravityScale = 0;

      transform.position = Vector2.MoveTowards(transform.position, startingPosition, riseSpeed * Time.deltaTime);

      if (Vector2.Distance(transform.position, startingPosition) <= 0.1f)
      {
        StartCoroutine(Drop());
      }
    }
  }
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Ground") && isDropping)
    {
      rigidbody.velocity = Vector2.zero;
      
      isDropping = false;
    }
  }
  IEnumerator Drop()
  {
    yield return new WaitForSeconds(waitTime);
    rigidbody.gravityScale = 1;
    isDropping = true;
  }
}
