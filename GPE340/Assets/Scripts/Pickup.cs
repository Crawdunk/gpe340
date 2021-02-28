using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public Health health;

    void Update()
    {
        transform.Rotate(15,0,0);
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Player")
        {
            gameObject.SetActive(false);
            health.Heal(40);
        }
        
    }
}
