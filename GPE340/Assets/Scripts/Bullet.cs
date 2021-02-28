using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float fireSpeed;
    public float lifespan = 1.5f;

    private Rigidbody rb;

    public int damageDone;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }

    void Update()
    {

        transform.position += transform.forward * (fireSpeed * Time.deltaTime);

    }

    public void OnTriggerEnter(Collider other)
    {
        //Get the object to hit
        GameObject otherObject = other.gameObject;

        //If it has health, take damage.
        Health otherHealth = otherObject.GetComponent<Health>();
        if(otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone);
        }

        //Destroy the bullet
        Destroy(gameObject);
    }
}      