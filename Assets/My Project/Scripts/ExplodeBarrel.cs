using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBarrel : MonoBehaviour
{
 [SerializeField] GameObject Explosion;

 public void Explode()
{
    Instantiate(Explosion, this.transform.position, this.transform.rotation);
    Destroy(gameObject, 0.1f);
}
 
}
