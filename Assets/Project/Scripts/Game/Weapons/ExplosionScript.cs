using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

  // Use this for initialization
  public GameObject[] particlePrefabs;
  public int particlesAmount = 3;
  void Start()
  {
    for (int i = 0; i < particlesAmount; i++)
    {
      GameObject particlePrefab = Instantiate(
        particlePrefabs[Random.Range(0, particlePrefabs.Length)]
      );

      particlePrefab.transform.position = transform.position;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
