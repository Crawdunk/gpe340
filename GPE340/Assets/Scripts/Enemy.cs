using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health health;
    public GameManager gm;

    public void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        if (health.currentHealth <= 0)
        {
            StartCoroutine(gm.KillAlien());
        }
    }

}
