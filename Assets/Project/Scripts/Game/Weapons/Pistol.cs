using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject bulletPrefab;

  public void Attack()
  {
    GameObject bulletShoot = Instantiate(bulletPrefab, transform.position, transform.rotation);
    bulletShoot.transform.position = bulletShoot.transform.position + transform.forward;
  }
}
