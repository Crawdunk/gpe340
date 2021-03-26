using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    private Rigidbody[] childRigidbodies;
    private Collider[] childColliders;
    private Rigidbody topRigidbody;
    private Collider topCollider;

    private Animator animator;

    public bool isRagdoll;

    // Start is called before the first frame update
    void Start()
    {
        topCollider = GetComponent<Collider>();
        topRigidbody = GetComponent<Rigidbody>();
        childColliders = GetComponentsInChildren<Collider>();
        childRigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void StartRagdoll()
    {
        isRagdoll = true;

        foreach (Collider collider in childColliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = false;
        }

        animator.enabled = false;
        topCollider.enabled = false;
        topRigidbody.isKinematic = true;
    }

    public void StopRagdoll()
    {
        foreach (Collider collider in childColliders)
        {
            collider.enabled = false;
        }

        foreach (Rigidbody rb in childRigidbodies)
        {
            rb.isKinematic = true;
        }

        animator.enabled = true;
        topCollider.enabled = true;
        topRigidbody.isKinematic = false;
    }
}
