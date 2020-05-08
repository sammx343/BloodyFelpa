using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

  // Use this for initialization
  public float duration = 5f;
  public float radius;
  public float explotionDuration = .25f;
  public GameObject explosion;
  public bool exploded = false;
  private float explosionTimer;
  void Start()
  {
    explosionTimer = duration;
    explosion.SetActive(false);
    explosion.transform.localScale = Vector3.one * radius;
  }

  // Update is called once per frame
  void Update()
  {
    explosionTimer -= Time.deltaTime;
    if (explosionTimer <= 0f && !exploded)
    {
      exploded = true;
      Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);

      foreach (Collider collider in hitObjects)
      {
        if (collider.GetComponent<Enemy>() != null)
        {
          collider.GetComponent<Enemy>().Hit();
        }
      }

      StartCoroutine(Explode());
    }
  }

  IEnumerator Explode()
  {
    explosion.SetActive(true);
    yield return new WaitForSeconds(explotionDuration);
    Destroy(gameObject);
  }
}
