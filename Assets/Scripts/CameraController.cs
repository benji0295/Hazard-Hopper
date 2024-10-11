using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  private GameObject player;
  public float leftBound = 0.0f;
  public float rightBound = 21.0f;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  void Update()
  {
    Vector3 playerPosition = player.transform.position;
    Vector3 cameraPosition = transform.position;

    cameraPosition.x = Mathf.Clamp(playerPosition.x, leftBound, rightBound);

    transform.position = cameraPosition;
  }
}
