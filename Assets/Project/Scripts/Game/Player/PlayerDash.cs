using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject body;
  public GameObject particles;

  [Header("Dash")]
  public float dashDistance = 5f;
  [Range(0.0f, 1.0f)]
  public float dashDuration = 1f;
  public float dashJumpDistance = 10f;
  private float keyPressedCooldown = .2f;
  private float keyPressedTimer = 0;
  private float dashCurrentTime = 0f;
  private Rigidbody playerRigidbody;
  private enum LastKeyPressed
  {
    w,
    s,
    a,
    d
  };
  private LastKeyPressed keyPressed;
  private Vector3 dashPosition;
  void Start()
  {
    playerRigidbody = GetComponent<Rigidbody>();
    dashCurrentTime = dashDuration;
  }

  // Update is called once per frame
  void Update()
  {
    keyPressedTimer -= Time.deltaTime;
    CheckPressedKey(KeyCode.W, LastKeyPressed.w, Vector3.forward, 180);
    CheckPressedKey(KeyCode.A, LastKeyPressed.a, Vector3.left, 90);
    CheckPressedKey(KeyCode.S, LastKeyPressed.s, Vector3.back, 0);
    CheckPressedKey(KeyCode.D, LastKeyPressed.d, Vector3.right, 270);
  }

  private void FixedUpdate()
  {
    if (dashCurrentTime < dashDuration)
    {
      transform.position = Vector3.Lerp(transform.position, dashPosition, dashDuration * Time.fixedDeltaTime);
      dashCurrentTime += Time.fixedDeltaTime;
    }
    else
    {
      GetComponent<PlayerController>().SetIsPlayerDashing(false);
    }
  }

  private void CheckPressedKey(KeyCode keyCode, LastKeyPressed lastKeyPressed, Vector3 direction, int particleRotation)
  {
    if (Input.GetKeyDown(keyCode) && !GetComponent<PlayerController>().GetIsPlayerDashing())
    {
      if (keyPressedTimer > 0 && keyPressed == lastKeyPressed)
      {
        StartRoll(direction);
        particles.transform.rotation = Quaternion.Euler(new Vector3(0, particleRotation, 0));
      }
      else
      {
        keyPressed = lastKeyPressed;
        keyPressedTimer = keyPressedCooldown;
      }
    }
  }

  private void StartRoll(Vector3 direction)
  {
    GetComponent<PlayerController>().SetIsPlayerDashing(true);

    dashCurrentTime = Time.fixedDeltaTime;

    Vector3 dashVector = direction * dashDistance;

    dashPosition = transform.position + dashVector;

    //Adds a vertical jump to the dash
    playerRigidbody.velocity = new Vector3(
      playerRigidbody.velocity.x,
      playerRigidbody.velocity.y + dashJumpDistance,
      playerRigidbody.velocity.z
    );
  }
}
