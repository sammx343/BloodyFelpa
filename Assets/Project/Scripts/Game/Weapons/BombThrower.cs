using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject bombPrefab;

  public void Attack(float throwSpeed)
  {
    GameObject bombObject = Instantiate(bombPrefab);
    bombObject.transform.position = transform.position + transform.forward;

    Vector3 throwingDirection = (transform.forward + Vector3.up).normalized;
    bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwSpeed);
  }
}
