using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : WeaponAgent
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public Vector3 desiredVelocity;

    public Animator animator;
    public Health health;
    private RagdollController ragdoll;

    public float shootRange;
    public float playerDistance;
    private bool isChasing;
    private bool isAlive;

    private GameManager gm;
    public HealthBar healthBar;

    void Awake()
    {
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<RagdollController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        EquipWeapon(equippedWeapon);
        isChasing = false;
        isAlive = true;
        gm = GameObject.FindObjectOfType<GameManager>();

        healthBar.SetHealth(health.currentHealth);
    }

    void Update()
    {
        if(gm.isPaused)
            return;

        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerDistance = Vector3.Distance(target.position, transform.position);

        if (!target)
        {
            navMeshAgent.isStopped = true;
            animator.SetFloat("Right", 0f);
            animator.SetFloat("Forward", 0f);
            return;
        }

        if (ragdoll.isRagdoll)
        {
            navMeshAgent.isStopped = true;
            isAlive = false;
        }

        if (playerDistance <= 10 && isAlive)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (!isChasing)
        {
            navMeshAgent.isStopped = true;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }

        navMeshAgent.SetDestination(target.position);

        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMeshAgent.desiredVelocity, navMeshAgent.acceleration * Time.deltaTime);
        Vector3 input = transform.InverseTransformDirection (desiredVelocity);
        animator.SetFloat ("Right", input.x);
        animator.SetFloat ("Forward", input.z);
    }

    public void OnAnimatorIK(int layerIndex)
    {
        if (weaponIsEquipped == true)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.position);
            animator.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.rotation);
            animator.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.rotation);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }

    }

    void LateUpdate()
    {
        healthBar.SetHealth(health.currentHealth);
    }

}
