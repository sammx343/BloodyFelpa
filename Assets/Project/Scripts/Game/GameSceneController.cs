using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
  [Header("Game")]
  public GameObject player;

  [Header("UI")]
  public GameObject[] hearts;
  public Text healthText;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (player != null)
    {
      for (int index = 0; index < hearts.Length; index++)
      {
        hearts[index].SetActive(index < player.GetComponent<PlayerController>().health);
      }
    }
    else
    {
      for (int index = 0; index < hearts.Length; index++)
      {
        hearts[index].SetActive(false);
      }

    }
  }
}
