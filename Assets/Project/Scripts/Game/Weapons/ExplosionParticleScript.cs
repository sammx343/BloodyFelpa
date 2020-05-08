using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleScript : MonoBehaviour
{

  // Use this for initialization
  public float explosionForce = 150f;
  public float lifeTime = 1f;
  void Start()
  {
    Vector3 randomDirection = new Vector3(
      Random.Range(-1f, 1f),
      Random.Range(-1f, 1f),
      Random.Range(-1f, 1f)
    );

    float randomForce = Random.Range(0f, explosionForce);
    GetComponent<Rigidbody>().AddForce(randomDirection.normalized * explosionForce);
  }

  // Update is called once per frame
  void Update()
  {
    lifeTime -= Time.deltaTime;
    transform.localScale *= 0.95f;
    if (lifeTime <= 0)
    {
      Destroy(gameObject);
    }
  }

  // public static double NextGaussianDouble(this Random r)
  // {
  //   double u, v, S;

  //   do
  //   {
  //     u = 2.0 * Random.Range(-1f, 1f) - 1.0;
  //     v = 2.0 * Random.Range(-1f, 1f) - 1.0;
  //     S = u * u + v * v;
  //   }
  //   while (S >= 1.0);

  //   //double fac = Mathf.Sqrt(-2.0 *  (double) Mathf.Log(S) / S);
  //   return u * fac;
  // }
}
