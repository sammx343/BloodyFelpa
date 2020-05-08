using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
  public GameObject body;
  [Header("Equipment")]
  public GameObject sword;
  public GameObject bow;
  public GameObject pistol;
  public GameObject bombThrower;
  public float bombThrowSpeed = 10;
  private List<GameObject> weapons;
  private int currentWeapon = 0;

  void Start()
  {
    weapons = new List<GameObject>();
    weapons.Add(sword);
    weapons.Add(bow);
    weapons.Add(pistol);
    weapons.Add(bombThrower);
  }

  void Update()
  {
    SwitchWeapon();
    UseWeapon();
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
    pistol.GetComponent<Pistol>().Attack();
  }

  private void ThrowBomb()
  {
    bombThrower.GetComponent<BombThrower>().Attack(bombThrowSpeed);
  }

  private void SwitchWeapon()
  {
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
  }

  private void UseWeapon()
  {
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
}