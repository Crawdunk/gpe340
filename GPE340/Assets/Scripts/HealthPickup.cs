using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{

    public GameManager gm;

    void Awake()
    {
        Destroy(gameObject, lifespan);
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(gm.isPaused)
            return;

        transform.Rotate(0, rotateSpeed, 0);
    }


    protected override void OnPickUp (Player player)
    {
        if (player.health.currentHealth < 100)
        {
            player.health.Heal(50);
            base.OnPickUp(player);
        }       
    }

}
