using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Visuals")]
  public GameObject body;
  public GameObject teddyBody;

  [Header("Movement")]
  public float movementSpeed = 3f;
  public float jumpingVelocity = 100f;
  public float knockBackForce = 100f;
  public float rotateSpeed = 90f;
  public float playerSpeedX = 0;
  public float playerSpeedZ = 0;
  public int health = 5;
  private float knockBackTimer;
  private bool canJump = false;
  private Rigidbody playerRigidbody;
  private Quaternion targetModelRotation;
  private bool IsPlayerDashing = false;
  // Use this for initialization
  void Start()
  {
    playerRigidbody = GetComponent<Rigidbody>();
    targetModelRotation = Quaternion.Euler(0, 0, 0);
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

    teddyBody.GetComponent<Animator>().SetFloat("speedXZ", Mathf.Max(Mathf.Abs(playerRigidbody.velocity.x), Mathf.Abs(playerRigidbody.velocity.z)));
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


  public bool GetIsPlayerDashing()
  {
    return IsPlayerDashing;
  }
  public void SetIsPlayerDashing(bool value)
  {
    IsPlayerDashing = value;
  }
}

