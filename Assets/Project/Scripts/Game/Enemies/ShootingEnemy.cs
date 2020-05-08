using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {

  // Use this for initialization
  public GameObject model;
  public float timeToRotate = 2f;
  public float rotationSpeed = 6f;
  public float rotationTimer;

  // 1 yes, -1 no
  public int rotateClockWise = 1;
  public GameObject bulletPrefab;
  public float timeToShoot = 1f;
  private float shootingTimer;
  private Quaternion targetRotation;
  private int targetAngle;
  void Start () {
    rotationTimer = timeToRotate;
    shootingTimer = timeToShoot;
  }

  // Update is called once per frame
  void Update () {
    rotationTimer -= Time.deltaTime;
    if (rotationTimer <= 0f) {
      rotationTimer = timeToRotate;
      targetAngle += rotateClockWise * 90;
    }

    transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.Euler (0, targetAngle, 0), Time.deltaTime * rotationSpeed);

    shootingTimer -= Time.deltaTime;
    if (shootingTimer <= 0f) {
      shootingTimer = timeToShoot;

      GameObject bulletObject = Instantiate (bulletPrefab);
      bulletObject.transform.position = transform.position + model.transform.forward;
      bulletObject.transform.forward = model.transform.forward;
    }

  }
}