using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

  // Use this for initialization
  public float shootingForce = 10f;
  public Vector3 shootingRotation;
  public float shootTimer = 3f;
  public GameObject explosionPrefab;

  void Start()
  {
    GetComponent<Rigidbody>().AddForce(transform.forward * shootingForce);
  }

  // Update is called once per frame
  void Update()
  {
    shootTimer -= Time.deltaTime;
    if (shootTimer <= 0)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    Instantiate(explosionPrefab, transform);
    Destroy(gameObject, 0.001f);
  }
}