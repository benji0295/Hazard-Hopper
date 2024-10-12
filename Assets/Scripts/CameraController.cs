using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
  private GameObject player;


  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  void Update()
  {
    var playerPosition = player.transform.position;
    var cameraPosition = transform.position;

    cameraPosition.x = playerPosition.x;
    cameraPosition.y = playerPosition.y;

    transform.position = cameraPosition;
  }
}
