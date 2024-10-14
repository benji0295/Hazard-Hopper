using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseScreen : MonoBehaviour
{
  public TMP_Text scoreText;

  private void Start()
  {
    scoreText.text = "Your Score: " + GameManager.score;
  }
  private void Update()
  {
    if (Input.anyKeyDown)
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
  }
}
