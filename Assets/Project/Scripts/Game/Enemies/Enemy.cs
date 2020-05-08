using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public int health = 1;
  public virtual void Hit()
  {
    health--;
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }

  public void OnTriggerEnter(Collider collision)
  {
    if (collision.GetComponent<Sword>() != null)
    {
      Debug.Log("hits");
      if (collision.GetComponent<Sword>().IsAttacking)
      {
        Hit();
      }
    }
    else if (collision.GetComponent<Arrow>() != null)
    {
      Hit();
      Destroy(collision.gameObject);
    }
    else if (collision.GetComponent<BulletController>() != null)
    {
      Hit();
    }
  }
}
