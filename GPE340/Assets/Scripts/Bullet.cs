using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float fireSpeed;
    public int damageDone;
    public float lifespan = 1.5f;

    private Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }


    void Update()
    {
        //Move Bullet Forward
        transform.position += transform.forward * (fireSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;

        Health otherHealth = otherObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone);
        }


        Destroy(gameObject);
    }
}
