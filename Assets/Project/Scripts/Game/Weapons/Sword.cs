using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
  // Use this for initialization
  public float swingingSpeed = 3f;
  public float attackDuration = .5f;
  public float cooldownSpeed = 3f;
  public float cooldownDuration = .5f;
  private float cooldownTimer;
  private bool isAttacking;

  public bool IsAttacking
  {
    get
    {
      return isAttacking;
    }
  }
  private Quaternion targetRotation;
  void Start()
  {
    targetRotation = Quaternion.Euler(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    float attackSpeed = isAttacking ? swingingSpeed : cooldownSpeed;
    transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * attackSpeed);
    cooldownTimer -= Time.deltaTime;
  }

  public void Attack()
  {
    if (cooldownTimer > 0f)
    {
      return;
    }
    targetRotation = Quaternion.Euler(90, 0, 0);
    cooldownTimer = cooldownDuration;
    StartCoroutine(CooldownWait());
  }

  private IEnumerator CooldownWait()
  {
    isAttacking = true;
    yield return new WaitForSeconds(attackDuration);

    targetRotation = Quaternion.Euler(0, 0, 0);
    isAttacking = false;
  }
}
