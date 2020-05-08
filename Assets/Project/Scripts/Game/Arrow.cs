using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
  // Use this for initialization
  public float speed;
  public float lifeTime = 2f;
  void Start()
  {
    GetComponent<Rigidbody>().velocity = transform.forward * speed;

  }

  // Update is called once per frame
  void Update()
  {
    lifeTime -= Time.deltaTime;
    if (lifeTime <= 0)
    {
      Destroy(gameObject);
    }
  }
}
