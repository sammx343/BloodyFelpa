using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Visuals")]
  public GameObject body;
  [Header("Movement")]
  public float movementSpeed = 3f;
  public float jumpingVelocity = 100f;
  public float knockBackForce = 100f;
  public float rotateSpeed = 90f;

  [Header("Equipment")]
  public int health = 5;
  public GameObject sword;
  public GameObject bow;
  public GameObject pistol;
  public GameObject bomb;
  public float throwSpeed = 10;
  public GameObject bullet;
  public GameObject bombPrefab;
  private List<GameObject> weapons;
  private float knockBackTimer;
  private int currentWeapon = 0;
  private bool canJump = false;
  private Rigidbody playerRigidbody;
  private Quaternion targetModelRotation;
  // Use this for initialization
  void Start()
  {
    playerRigidbody = GetComponent<Rigidbody>();
    targetModelRotation = Quaternion.Euler(0, 0, 0);
    weapons = new List<GameObject>();
    weapons.Add(sword);
    weapons.Add(bow);
    weapons.Add(pistol);
    weapons.Add(bomb);
  }

  // Update is called once per frame
  void Update()
  {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
    {
      canJump = true;
    }

    if (knockBackTimer > 0)
    {
      knockBackTimer -= Time.deltaTime;
    }
    else
    {
      ProccessInput();
    }
    body.transform.rotation = Quaternion.Lerp(body.transform.localRotation, targetModelRotation, Time.deltaTime * rotateSpeed);
  }

  void ProccessInput()
  {
    playerRigidbody.velocity = new Vector3(
      0,
      playerRigidbody.velocity.y,
      0
    );

    if (Input.GetKey(KeyCode.D))
    {
      playerRigidbody.velocity = new Vector3(
        movementSpeed,
        playerRigidbody.velocity.y,
        playerRigidbody.velocity.z
      );
      targetModelRotation = Quaternion.Euler(0, 90, 0);
    }

    if (Input.GetKey(KeyCode.A))
    {
      playerRigidbody.velocity = new Vector3(
        -movementSpeed,
        playerRigidbody.velocity.y,
        playerRigidbody.velocity.z
      );
      targetModelRotation = Quaternion.Euler(0, 270, 0);
    }

    if (Input.GetKey(KeyCode.W))
    {
      playerRigidbody.velocity = new Vector3(
        playerRigidbody.velocity.x,
        playerRigidbody.velocity.y,
        movementSpeed
      );
      targetModelRotation = Quaternion.Euler(0, 0, 0);
    }

    if (Input.GetKey(KeyCode.S))
    {
      playerRigidbody.velocity = new Vector3(
        playerRigidbody.velocity.x,
        playerRigidbody.velocity.y,
        -movementSpeed
      );
      targetModelRotation = Quaternion.Euler(0, 180, 0);
    }

    if (Input.GetKeyDown("space") && canJump)
    {
      canJump = false;
      GetComponent<Rigidbody>().velocity = new Vector3(
        GetComponent<Rigidbody>().velocity.x,
        jumpingVelocity,
        GetComponent<Rigidbody>().velocity.z
      );
    }

    if (Input.GetKeyUp(KeyCode.Tab))
    {
      currentWeapon++;

      if (currentWeapon > 3)
      {
        currentWeapon = 0;
      }

      for (int index = 0; index < weapons.Count; index++)
      {
        GameObject weapon = weapons[index];
        if (currentWeapon == index)
        {
          weapon.SetActive(true);
        }
        else
        {
          weapon.SetActive(false);
        }
      }
    }

    if (Input.GetMouseButtonDown(0))
    {
      switch (currentWeapon)
      {
        case 0:
          SwordAttack();
          break;
        case 1:
          BowShoot();
          break;
        case 2:
          BulletShoot();
          break;
        case 3:
          ThrowBomb();
          break;
      }
    }
  }

  public void SwordAttack()
  {
    sword.GetComponent<Sword>().Attack();
  }

  public void BowShoot()
  {
    bow.GetComponent<Bow>().Attack();
  }

  public void BulletShoot()
  {
    GameObject bulletShoot = Instantiate(bullet, pistol.transform.position, pistol.transform.rotation);
    bulletShoot.transform.position = bulletShoot.transform.position + body.transform.forward / 2;
  }

  private void ThrowBomb()
  {
    GameObject bombObject = Instantiate(bombPrefab);
    bombObject.transform.position = transform.position + body.transform.forward;

    Vector3 throwingDirection = (body.transform.forward + Vector3.up).normalized;
    bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwSpeed);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<EnemyBullet>() != null)
    {
      Hit((transform.position - other.transform.position).normalized);
      Destroy(other.gameObject);
    }
  }

  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.GetComponent<Enemy>())
    {
      Hit((transform.position - other.transform.position).normalized);
    }
  }

  private void Hit(Vector3 direction)
  {
    knockBackTimer = 1f;
    Vector3 knockBackdirection = (direction + Vector3.up).normalized;
    playerRigidbody.AddForce(knockBackdirection * knockBackForce);
    health--;
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }
}

