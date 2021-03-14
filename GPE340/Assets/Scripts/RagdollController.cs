using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    private Rigidbody[] childRigidbodies;
    private Collider[] childColliders;
    private Collider topCollider;
    private Rigidbody topRigidbody;
    private Animator animator;
    public Player player;

    void Start()
    {
        //Get top objects off this object
        topCollider = GetComponent<Collider>();
        topRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //Get Child Objects off all children (Note: Includes Self)
        childColliders = GetComponentsInChildren<Collider>();
        childRigidbodies = GetComponentsInChildren<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        DeactivateRagdoll();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateRagdoll();
            player.isDead = true;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            DeactivateRagdoll();
            player.isDead = false;
        }
    }

    public void ActivateRagdoll()
    {
        //turn on all child colliders
        foreach (Collider collider in childColliders)
        {
            collider.enabled = true;
        }

        //turn on all child rigidbodies
        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = false;
        }

        //Turn off animator
        animator.enabled = false;

        //Turn off top collider
        topCollider.enabled = false;

        //Turn off top rigidbody
        topRigidbody.isKinematic = true;
    }

    public void DeactivateRagdoll()
    {
        //turn off all child colliders
        foreach (Collider collider in childColliders)
        {
            collider.enabled = false;
        }

        //turn off all child rigidbodies
        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = true;
        }

        //Turn on animator
        animator.enabled = true;

        //Turn on top collider
        topCollider.enabled = true;

        //Turn on top rigidbody
        topRigidbody.isKinematic = false;

        
    }
}
