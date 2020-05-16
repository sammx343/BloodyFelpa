using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sword2 : MonoBehaviour
{
  // Use this for initialization
  public float swingingSpeed = 3f;
  public float attackDuration = .5f;
  public float cooldownSpeed = 3f;
  public float cooldownDuration = .5f;
  public Transform attackPoint;
  public float attackRange;
  public LayerMask enemyLayer;

  private float cooldownTimer;
  private bool isAttacking;
  private Animator animator;
  private TrailRenderer trail;

  public bool IsAttacking
  {
    get
    {
      return isAttacking;
    }
  }


  void Start(){
    animator = GetComponentInParent<Animator>();
    trail = GetComponentInChildren<TrailRenderer>();
    trail.enabled=false;
  }

  // Update is called once per frame
  void Update()
  {
      if(Input.GetKeyDown(KeyCode.E)){
          Attack();
      }
    cooldownTimer -= Time.deltaTime;
  }

  public void Attack()
  {
    animator.SetTrigger("slash");
    trail.enabled=true;
    Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,attackRange, enemyLayer);
    foreach(Collider enemy in hitEnemies){
        Debug.Log("Hit"+enemy.name);
        {
            
        }
    }
    if (cooldownTimer > 0f)
    {
      return;
    }
    cooldownTimer = cooldownDuration;
    StartCoroutine(CooldownWait());
  }

  private IEnumerator CooldownWait()
  {
    isAttacking = true;
    yield return new WaitForSeconds(attackDuration);
    trail.enabled=false;
    isAttacking = false;
  }

  void OnDrawGizmosSelected(){
      if(attackPoint==null) 
        return;
      Gizmos.DrawWireSphere(attackPoint.position,attackRange);
  }
}
