using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  void Start()
  {
    GameManager.score = 0;
    GameManager.lives = 3;
  }
  void Update()
  {
    if (Input.anyKeyDown)
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
  }
}
