using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Health health;
    public GameManager gm;
    public NavMeshAgent navMesh;
    public Transform target;
    public Animator animator;
    public Vector3 desiredVelocity;

    public bool hasWeapon;

    public void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        navMesh = GameObject.FindWithTag("Enemy").GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        hasWeapon = true;
    }

    public void Update()
    {
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMesh.desiredVelocity, navMesh.acceleration * Time.deltaTime);
        Vector3 input = transform.InverseTransformDirection(desiredVelocity);
        animator.SetFloat("Right", input.x);
        animator.SetFloat("Forward", input.z);

        navMesh.SetDestination(target.position);

        if (health.currentHealth <= 0)
        {
            StartCoroutine(gm.KillAlien());
        }

        
    }

    private void OnAnimatorMove()
    {
        navMesh.velocity = animator.velocity;
    }

}
